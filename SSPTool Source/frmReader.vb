' MaiSoft Simple Sequence Pack (MaiSSP) Reader UI Sample v1.1
' Written by MaiSoft (Raymai97) on 10 Dec 2013.

Imports SSP.AppShared

Public Class frmReader

    Private WithEvents SSP As SSPReader
    Private ExitAfterCancelled As Boolean
    Private ExtractPath As String

    Public Sub OpenSSP(SSPPath As String)
        Try
            SSP = New SSPReader(SSPPath)
        Catch ex As Exception
            MessageBox.Show("SSP cannot be opened!" & vbCrLf & ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try
    End Sub

    Private Sub CloseSSP()
        SSP.Dispose()
        SSP = Nothing
    End Sub

    Private Sub UpdateCtrl()
        If SSP Is Nothing Then
            btnOpenSSP.Enabled = True
            grpSSPInfo.Enabled = False
            btnCloseSSP.Enabled = False
            txtInfo.Clear()
            txtFileSize.Clear()
            numIndex.Value = 0
        Else
            btnOpenSSP.Enabled = False
            grpSSPInfo.Enabled = True
            btnCloseSSP.Enabled = True
            Dim Info As String = SSP.SSPPath & vbCrLf
            Info &= FormattedSize(SSP.SSPSize) & vbCrLf & vbCrLf
            Info &= "Header size: " & FormattedSize(SSP.HeaderSize) & vbCrLf
            Info &= "Data size: " & FormattedSize(SSP.SSPSize - SSP.HeaderSize) & vbCrLf & vbCrLf
            Info &= "Total file count: " & SSP.FileCount.ToString
            txtInfo.Text = Info
            numIndex.Maximum = SSP.FileCount - 1
            numIndex.Value = 0
            txtFileSize.Text = FormattedSize(SSP.FileSize(0))
        End If
    End Sub

    Private Sub UpdateCtrl2(Busy As Boolean)
        If Busy Then
            Me.AllowDrop = False
            grpExtract.Enabled = False
            btnCloseSSP.Enabled = False
            btnStopExtract.Enabled = True
            btnStopExtract.Select()
        Else
            Me.AllowDrop = True
            grpExtract.Enabled = True
            btnCloseSSP.Enabled = True
            btnStopExtract.Enabled = False
            btnStopExtract.Text = "Stop extract"
            lblStatus.Text = "Idle..."
            ProgressBar1.Value = 0
        End If
    End Sub

    Private Sub frmReader_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) = False Then Return
        Dim SSPPath As String = CType(e.Data.GetData(DataFormats.FileDrop), String())(0)
        OpenSSP(SSPPath)
        UpdateCtrl()
        Me.Activate()
    End Sub

    Private Sub frmReader_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub frmReader_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If SSP IsNot Nothing Then
            If SSP.IsBusy Then
                If MessageBox.Show("Files are still extracting from SSP! Are you sure you want to exit now?", "Wait!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                    ExitAfterCancelled = True
                    SSP.StopExtract()
                End If
                e.Cancel = True
            Else
                CloseSSP()
            End If
        End If
    End Sub

    Private Sub frmReader_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        UpdateCtrl()
        ExitAfterCancelled = False
    End Sub

    Private Sub btnOpenSSP_Click(sender As System.Object, e As System.EventArgs) Handles btnOpenSSP.Click
        Dim ofd As New OpenFileDialog
        ofd.Multiselect = False
        ofd.FileName = ""
        ofd.Filter = "MaiSoft Simple Sequence Pack|*.ssp"
        ofd.Title = "Select SSP file"
        ofd.ShowDialog()
        If ofd.FileName <> "" Then
            OpenSSP(ofd.FileName)
            UpdateCtrl()
        End If
    End Sub

    Private Sub btnCloseSSP_Click(sender As System.Object, e As System.EventArgs) Handles btnCloseSSP.Click
        CloseSSP()
        UpdateCtrl()
    End Sub

    Private Sub txtInfo_GotFocus(sender As Object, e As System.EventArgs) Handles txtInfo.GotFocus
        txtInfo.SelectionStart = txtInfo.Text.Length
    End Sub

    Private Sub numIndex_ValueChanged(sender As System.Object, e As System.EventArgs) Handles numIndex.ValueChanged
        If SSP Is Nothing Then Return
        txtFileSize.Text = FormattedSize(SSP.FileSize(Convert.ToInt32(numIndex.Value)))
    End Sub

    Private Sub btnExtract_Click(sender As System.Object, e As System.EventArgs) Handles btnExtract.Click
        Dim fbd As New FolderBrowserDialog
        fbd.Description = "Extract to:"
        fbd.ShowDialog()
        If fbd.SelectedPath <> "" Then
            With SSP
                .ExtractIndices.Clear()
                .ExtractIndices.Add(Convert.ToInt32(numIndex.Value))
                .ExtractPath = fbd.SelectedPath
                .StartExtract()
            End With
            UpdateCtrl2(True)
        End If
    End Sub

    Private Sub btnExtractAll_Click(sender As System.Object, e As System.EventArgs) Handles btnExtractAll.Click
        Dim fbd As New FolderBrowserDialog
        fbd.Description = "Extract to:"
        fbd.ShowDialog()
        If fbd.SelectedPath <> "" Then
            With SSP
                .ExtractIndices.Clear()
                For i As Integer = 0 To SSP.FileCount - 1
                    .ExtractIndices.Add(i)
                Next
                .ExtractPath = fbd.SelectedPath
                .StartExtract()
            End With
            UpdateCtrl2(True)
        End If
    End Sub

    Private Sub btnStopExtract_Click(sender As System.Object, e As System.EventArgs) Handles btnStopExtract.Click
        SSP.StopExtract()
        btnStopExtract.Text = "Stopping..."
        btnStopExtract.Enabled = False
    End Sub

    Private Sub SSP_Extracting(FileIndex As Integer, Chunk As Integer, TotalChunk As Integer) Handles SSP.Extracting
        Dim PercentPerFile As Double = 1 / SSP.FileCount * 100
        Dim PercentOfChunk As Double = Chunk / TotalChunk * PercentPerFile
        ProgressBar1.Value = Convert.ToInt32(FileIndex * PercentPerFile + PercentOfChunk)
        lblStatus.Text = "Extracting file at index " & FileIndex.ToString
    End Sub

    Private Sub SSP_Done(Cancelled As Boolean, Err As System.Exception) Handles SSP.Done
        If Cancelled Then
            If ExitAfterCancelled Then
                CloseSSP()
                Me.Close()
            Else
                Dim Msg As String = "SSP Extraction failed!"
                If Err IsNot Nothing Then
                    Msg &= vbCrLf & Err.Message
                End If
                MessageBox.Show(Msg, "Oops...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            lblStatus.Text = "Done!"
            ProgressBar1.Value = 100
            MessageBox.Show("SSP Extraction has done successfully!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        UpdateCtrl2(False)
    End Sub

End Class