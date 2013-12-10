' MaiSoft Simple Sequence Pack (MaiSSP) Reader v1.1
' Written by MaiSoft (Raymai97) on 10 Dec 2013.

Imports System.IO
Imports System.ComponentModel

Public Class SSPReader
    Implements IDisposable

    ' Public Vars / Properites
    ''' <summary> How many bytes can SSPReader read to RAM at once? Minimum cache size is 1MB. </summary>
    Public CacheSize As Integer = 1024 * 1024 * 10

    ''' <summary>Specifies the index of file you want to extract.</summary>
    Public Property ExtractIndices() As New List(Of Integer)
    ''' <summary>Specifies the path you want the files extract to.</summary>
    Public Property ExtractPath() As String

    Public ReadOnly Property FileCount() As Integer
        Get
            Return _FileCount
        End Get
    End Property
    Public ReadOnly Property FilePos(Index As Integer) As Long
        Get
            Return _FilePos(Index) + _HeaderSize
        End Get
    End Property
    Public ReadOnly Property FileSize(Index As Integer) As Long
        Get
            Return _FileSize(Index)
        End Get
    End Property
    Public ReadOnly Property HeaderSize() As Integer
        Get
            Return _HeaderSize
        End Get
    End Property
    Public ReadOnly Property IsBusy() As Boolean
        Get
            Return Bw.IsBusy
        End Get
    End Property
    Public ReadOnly Property SSPPath() As String
        Get
            Return SSPStream.Name
        End Get
    End Property
    Public ReadOnly Property SSPSize() As Long
        Get
            Return SSPStream.Length
        End Get
    End Property

    ' Public Events
    Public Event Extracting(FileIndex As Integer, Chunk As Integer, TotalChunk As Integer)
    Public Event Done(Cancelled As Boolean, Err As Exception)

    'Public Methods

    Public Sub New(SSPPath As String)
        Bw.WorkerSupportsCancellation = True
        Bw.WorkerReportsProgress = True
        Init(SSPPath)
    End Sub

    ''' <summary>Return the bytes of a file at specified index. If the file is big, don't use this or your RAM will doom.</summary>
    Public Function ObtainByIndex(Index As Integer) As Byte()
        SSPStream.Seek(FilePos(Index), SeekOrigin.Begin)
        If FileSize(Index) > Integer.MaxValue Then
            Throw New Exception("Size of file is too large to read into RAM.")
            Return Nothing
        Else
            Return BReader.ReadBytes(Convert.ToInt32(FileSize(Index)))
        End If
    End Function

    Public Sub StartExtract()
        Bw.RunWorkerAsync()
    End Sub

    Public Sub StopExtract()
        Bw.CancelAsync()
    End Sub

    ' Private area

    Private _HeaderSize As Integer
    Private _FilePos As New List(Of Long)
    Private _FileSize As New List(Of Long)
    Private _FileCount As Integer
    Private SSPStream As FileStream
    Private BReader As BinaryReader
    Private LastErr As Exception
    Private WithEvents Bw As New BackgroundWorker

    Private Sub Init(SSPPath As String)
        SSPStream = New FileStream(SSPPath, FileMode.Open, FileAccess.Read)
        BReader = New BinaryReader(SSPStream)
        'Check if header start with MaiSSP~
        Dim Header As String = System.Text.Encoding.ASCII.GetChars(BReader.ReadBytes(7))
        If Header <> "MaiSSP~" Then
            Throw New Exception("Not a vaild SSP file!")
        End If
        'Get Header Length
        Dim c As Char = Nothing
        SSPStream.Seek(7, SeekOrigin.Begin)
        Do
            c = Convert.ToChar(SSPStream.ReadByte)
            If Integer.TryParse(c, 0) Then
                _HeaderSize = Convert.ToInt32(_HeaderSize & c)
            Else
                Exit Do
            End If
        Loop
        If _HeaderSize < 14 Then Throw New Exception("Not a vaild SSP file!")
        'Read and Parse Header
        Dim HeaderBytes(_HeaderSize - 1) As Byte
        SSPStream.Seek(0, SeekOrigin.Begin)
        BReader.Read(HeaderBytes, 0, _HeaderSize)
        Header = System.Text.Encoding.ASCII.GetChars(HeaderBytes)
        Header = Header.Substring(Header.IndexOf("//") + 2)
        Header = Header.Substring(0, Header.IndexOf("//"))
        Dim FilesPosInfo() As String = Header.Split("/"c)
        _FileCount = FilesPosInfo.Length
        Dim FilePos, FileSize As Long
        For Each Info As String In FilesPosInfo
            If Long.TryParse(Info, FileSize) Then
                _FilePos.Add(FilePos)
                FilePos += FileSize
                _FileSize.Add(FileSize)
            End If
        Next
    End Sub

    Private Sub Bw_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Bw.DoWork
        Try
            For Each FileIndex As Integer In ExtractIndices
                Dim DestPath As String = Path.GetFullPath(ExtractPath & "\" & FileIndex.ToString)
                Using DestBw As New BinaryWriter(New FileStream(DestPath, FileMode.Create, FileAccess.Write))
                    Dim FileLength As Long = FileSize(FileIndex)
                    SSPStream.Seek(FilePos(FileIndex), SeekOrigin.Begin)
                    ' We cannot just read and write the file directly... If the file is 1GB our RAM are doomed...
                    ' We have to write it chunk by chunk...
                    If CacheSize < 1024 * 1024 * 1 Then CacheSize = 1024 * 1024 * 1
                    Dim ChunkCount As Integer = 1
                    If FileLength > CacheSize Then
                        ' 2100 / 1024 = 2.05.... mean need 2+1 chunk
                        ChunkCount = Convert.ToInt32(FileLength \ CacheSize)
                        If (FileLength Mod CacheSize) <> 0 Then
                            ChunkCount += 1
                        End If
                    End If
                    ' If cache is 1MB, file is 2.5MB, last chunk size would be 0.5MB...
                    ' Due to VB.NET bug, CLng(1) * is need to make sure it calculated in Long before casting to Integer
                    Dim LastChunkSize As Integer = Convert.ToInt32(FileLength - (CLng(1) * CacheSize * (ChunkCount - 1)))
                    For a As Integer = 1 To ChunkCount
                        If Bw.CancellationPending Then Throw New Exception("User cancelled the operation.")
                        Bw.ReportProgress(0, New Progress(ProgressType.Extracting, FileIndex, a, ChunkCount))
                        If a = ChunkCount Then
                            DestBw.Write(BReader.ReadBytes(LastChunkSize))
                        Else
                            DestBw.Write(BReader.ReadBytes(CacheSize))
                        End If
                        DestBw.Flush()
                    Next
                End Using
            Next
            Bw.ReportProgress(0, New Progress(ProgressType.Done, False, Nothing, Nothing))
        Catch ex As Exception
            Bw.ReportProgress(0, New Progress(ProgressType.Done, True, ex, Nothing))
        End Try
    End Sub

    Private Sub Bw_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles Bw.ProgressChanged
        Dim Progress As Progress = CType(e.UserState, Progress)
        Select Case Progress.Type
            Case ProgressType.Extracting
                Dim FileIndex, Chunk, TotalChunk As Integer
                FileIndex = CType(Progress.Data, Integer)
                Chunk = CType(Progress.Data2, Integer)
                TotalChunk = CType(Progress.Data3, Integer)
                RaiseEvent Extracting(FileIndex, Chunk, TotalChunk)
            Case ProgressType.Done
                Dim Cancelled As Boolean = CType(Progress.Data, Boolean)
                Dim Err As Exception = CType(Progress.Data2, Exception)
                RaiseEvent Done(Cancelled, Err)
        End Select
    End Sub

    Private Structure Progress
        Public Type As ProgressType
        Public Data As Object
        Public Data2 As Object
        Public Data3 As Object
        Public Sub New(Type As ProgressType, Data As Object, Data2 As Object, Data3 As Object)
            Me.Type = Type
            Me.Data = Data
            Me.Data2 = Data2
            Me.Data3 = Data3
        End Sub
    End Structure

    Private Enum ProgressType
        Extracting
        Done
    End Enum

#Region "IDisposable Support"
    Private disposed As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                SSPStream.Dispose()
            End If
        End If
        Me.disposed = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
