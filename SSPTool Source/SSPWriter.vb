' MaiSoft Simple Sequence Pack (MaiSSP) Writer v1.0
' Written by MaiSoft (Raymai97) on 9 Dec 2013.

Imports System.IO
Imports System.ComponentModel

Public Class SSPWriter

    ' Public Vars / Properites
    ''' <summary>Files to append into SSP. Unless you want your SSP broke, don't try to modify Files when IsBusy is True!</summary>
    Public Files As New List(Of FileInfo)
    ''' <summary> How many bytes can SSPWriter read to RAM at once? Minimum cache size is 1MB. </summary>
    Public CacheSize As Integer = 1024 * 1024 * 10

    ' Public Events
    Public Event WritingHeader()
    Public Event Writing(ByVal FileIndex As Integer, ByVal Chuck As Integer, ByVal TotalChuck As Integer)
    Public Event Done(Cancelled As Boolean, Err As Exception)

    ' Public Methods
    Public Sub New()
        Bw.WorkerReportsProgress = True
        Bw.WorkerSupportsCancellation = True
    End Sub

    Public Function IsBusy() As Boolean
        Return Bw.IsBusy
    End Function

    Public Sub StartWrite(SSPPath As String)
        Me.SSPPath = SSPPath
        Bw.RunWorkerAsync()
    End Sub

    Public Sub StopWrite()
        Bw.CancelAsync()
    End Sub

    ' Private area
    Private SSPPath As String
    Private WithEvents Bw As New BackgroundWorker

    Private Sub BgWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Bw.DoWork
        Try
            Using SSPStream As New FileStream(SSPPath, FileMode.Create, FileAccess.Write)
                If Files.Count = 0 Then Throw New Exception("At least one file is needed to make SSP!")

                Bw.ReportProgress(0, New Progress(ProgressType.WritingHeader, Nothing, Nothing, Nothing))
                Dim Writer As New StreamWriter(SSPStream, System.Text.Encoding.ASCII)
                Dim Header As String = "MaiSSP~//"
                For a As Integer = 0 To Files.Count - 1
                    Header &= Files(a).Length & "/"
                Next
                Header &= "/"
                Header = Header.Insert(7, (Header.Length + Header.Length.ToString.Length).ToString)
                Writer.Write(Header)
                Writer.Flush()

                Dim BWriter As New BinaryWriter(SSPStream)
                For i As Integer = 0 To Files.Count - 1
                    Using BReader As New BinaryReader(New FileStream(Files(i).FullName, FileMode.Open, FileAccess.Read))
                        ' We cannot just read and write the file directly... If the file is 1GB our RAM are doomed...
                        ' We have to write it chunk by chunk...
                        If CacheSize < 1024 * 1024 * 1 Then CacheSize = 1024 * 1024 * 1
                        Dim ChunkCount As Integer = 1
                        If Files(i).Length > CacheSize Then
                            ' 2100 / 1024 = 2.05.... mean need 2+1 chunk
                            ChunkCount = Convert.ToInt32(Files(i).Length \ CacheSize)
                            If (Files(i).Length Mod CacheSize) <> 0 Then
                                ChunkCount += 1
                            End If
                        End If
                        ' If cache is 1MB, file is 2.5MB, last chunk size would be 0.5MB...
                        Dim LastChunkSize As Integer = Convert.ToInt32(Files(i).Length - (CacheSize * (ChunkCount - 1)))
                        For a As Integer = 1 To ChunkCount
                            If Bw.CancellationPending Then Throw New Exception("User cancelled the operation.")
                            Bw.ReportProgress(0, New Progress(ProgressType.Writing, i, a, ChunkCount))
                            If a = ChunkCount Then
                                BWriter.Write(BReader.ReadBytes(LastChunkSize))
                            Else
                                BWriter.Write(BReader.ReadBytes(CacheSize))
                            End If
                            BWriter.Flush()
                        Next
                    End Using
                Next
            End Using
            Bw.ReportProgress(0, New Progress(ProgressType.Done, False, Nothing, Nothing))
        Catch ex As Exception
            Bw.ReportProgress(0, New Progress(ProgressType.Done, True, ex, Nothing))
        End Try
    End Sub

    Private Sub BgWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles Bw.ProgressChanged
        Dim Progress As Progress = CType(e.UserState, Progress)
        Select Case Progress.Type
            Case ProgressType.WritingHeader
                RaiseEvent WritingHeader()
            Case ProgressType.Writing
                Dim FileIndex As Integer = CType(Progress.Data, Integer)
                Dim Chuck As Integer = CType(Progress.Data2, Integer)
                Dim TotalChuck As Integer = CType(Progress.Data3, Integer)
                RaiseEvent Writing(FileIndex, Chuck, TotalChuck)
            Case ProgressType.Done
                Dim Cancelled As Boolean = CType(Progress.Data, Boolean)
                Dim Err As Exception = CType(Progress.Data2, Exception)
                RaiseEvent Done(Cancelled, Err)
        End Select
    End Sub

    Private Structure Progress
        Public Type As ProgressType
        Public Data, Data2, Data3 As Object
        Public Sub New(Type As ProgressType, Data As Object, Data2 As Object, Data3 As Object)
            Me.Type = Type
            Me.Data = Data
            Me.Data2 = Data2
            Me.Data3 = Data3
        End Sub
    End Structure

    Private Enum ProgressType
        WritingHeader
        Writing
        Done
    End Enum

End Class
