' MaiSoft Simple Sequence Pack (MaiSSP) Writer UI Sample v1.0
' Written by MaiSoft (Raymai97) on 9 Dec 2013.

Imports System.IO
Imports SSP.AppShared

Public Class frmMaker

    Private SSPPath As String
    Private Files As New List(Of FileInfo)
    Private ExitAfterCancelled, FileSaved As Boolean
    Private ShowFileNameOnly, ShowSizeInMB As Boolean
    Private WithEvents SSPWriter As New SSPWriter

    Sub AddFiles()
        If SSPWriter.IsBusy Then Return
        Dim ofd As New OpenFileDialog
        ofd.FileName = ""
        ofd.Multiselect = True
        ofd.Title = "Add files..."
        If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            For Each FilePath As String In ofd.FileNames
                Files.Add(New FileInfo(FilePath))
            Next
            ResetSortArrow() : FileSaved = False
            RefreshList2()
            SelectItem(Files.Count - 1)
        End If
    End Sub

    Sub AddFilesInFolder()
        If SSPWriter.IsBusy Then Return
        Dim fbd As New FolderBrowserDialog
        fbd.Description = "Add all files that located in..."
        fbd.ShowDialog()
        If fbd.SelectedPath <> "" Then
            Dim DirInfo As New DirectoryInfo(fbd.SelectedPath)
            For Each File As FileInfo In DirInfo.GetFiles
                Files.Add(File)
            Next
            ResetSortArrow() : FileSaved = False
            RefreshList2()
            SelectItem(Files.Count - 1)
        End If
    End Sub

    Sub AddFilesByDragDrop(e As DragEventArgs)
        If SSPWriter.IsBusy Then Return
        If e.Data.GetDataPresent(DataFormats.FileDrop) = False Then Return
        For Each FilePath As String In CType(e.Data.GetData(DataFormats.FileDrop), String())
            If File.Exists(FilePath) = False Then Continue For ' Only Files...
            Files.Add(New FileInfo(FilePath))
        Next
        ResetSortArrow() : FileSaved = False
        RefreshList2()
        SelectItem(Files.Count - 1)
    End Sub

    Sub ClearList()
        If SSPWriter.IsBusy Then Return
        If MessageBox.Show("Are you sure you want to clear list?", "Seriously?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            ClearList2()
        End If
    End Sub

    Sub ClearList2()
        Files.Clear()
        ResetSortArrow() : FileSaved = False
        RefreshList2()
    End Sub

    Sub RemoveSelected()
        If SSPWriter.IsBusy Then Return
        If lv.SelectedItems.Count <> 1 Then Return
        Files.RemoveAt(lv.SelectedIndices(0))
        ResetSortArrow() : FileSaved = False
        RefreshList()
    End Sub

    Sub MoveUp()
        If SSPWriter.IsBusy Then Return
        If lv.SelectedItems.Count <> 1 Then Return
        Dim OldIndex As Integer = lv.SelectedIndices(0)
        Dim NewIndex As Integer = OldIndex - 1
        If OldIndex = 0 Then Return
        lv.BeginUpdate() : ResetSortArrow() : FileSaved = False
        ReorderFile(OldIndex, NewIndex)
        ReorderItem(OldIndex, NewIndex)
        SelectItem(NewIndex)
        lv.EndUpdate()
    End Sub

    Sub MoveDown()
        If SSPWriter.IsBusy Then Return
        If lv.SelectedItems.Count <> 1 Then Return
        Dim OldIndex As Integer = lv.SelectedIndices(0)
        Dim NewIndex As Integer = OldIndex + 1
        If OldIndex = lv.Items.Count - 1 Then Return
        lv.BeginUpdate() : ResetSortArrow() : FileSaved = False
        ReorderFile(OldIndex, NewIndex)
        ReorderItem(OldIndex, NewIndex)
        SelectItem(NewIndex)
        lv.EndUpdate()
    End Sub

    Sub ReorderFile(OldIndex As Integer, NewIndex As Integer)
        Dim File As FileInfo = Files(OldIndex)
        Files.RemoveAt(OldIndex)
        Files.Insert(NewIndex, File)
    End Sub

    Sub ReorderItem(OldIndex As Integer, NewIndex As Integer)
        ReorderItem2(OldIndex, NewIndex)
        ' Update item index column
        lv.Items(OldIndex).SubItems(1).Text = lv.Items(OldIndex).Index.ToString
        lv.Items(NewIndex).SubItems(1).Text = lv.Items(NewIndex).Index.ToString
    End Sub

    Sub ReorderItem2(OldIndex As Integer, NewIndex As Integer)
        Dim Item As ListViewItem = lv.Items(OldIndex)
        lv.Items.RemoveAt(OldIndex)
        lv.Items.Insert(NewIndex, Item)
    End Sub

    Sub SelectItem(Index As Integer)
        With lv.Items
            If .Count = 0 Then Return
            If Index < 0 Then
                Index = 0
            ElseIf Index > .Count - 1 Then
                Index = .Count - 1
            End If
            .Item(Index).Selected = True
            lv.FocusedItem = .Item(Index)
            lv.EnsureVisible(Index)
        End With
    End Sub

    Sub RefreshList()
        ' Remember and try to select back what was selected before refresh
        Dim LastSelectedIndex As Integer
        If lv.SelectedItems.Count = 1 Then LastSelectedIndex = lv.SelectedIndices(0)
        RefreshList2()
        SelectItem(LastSelectedIndex)
    End Sub

    Sub RefreshList2()
        If SSPWriter.IsBusy Then Return
        lv.BeginUpdate() : Me.Cursor = Cursors.WaitCursor : lblStatus.Text = "Refreshing list..." : Me.Refresh()
        With lv.Items
            .Clear()
            For a As Integer = 0 To Files.Count - 1
                Dim c0, c1, c2, c3 As String
                c0 = Files(a).Length.ToString
                c1 = a.ToString
                If ShowFileNameOnly Then
                    c2 = Path.GetFileName(Files(a).FullName)
                Else
                    c2 = Files(a).FullName
                End If
                If ShowSizeInMB Then
                    c3 = Math.Round(Files(a).Length / 1024 / 1024, 2).ToString("0.00") & "MB"
                Else
                    c3 = Math.Round(Files(a).Length / 1024, 2).ToString("0.00") & "KB"
                End If
                Dim i As New ListViewItem
                i.Text = c0
                i.SubItems.Add(c1)
                i.SubItems.Add(c2)
                i.SubItems.Add(c3)
                .Add(i)
            Next
        End With
        lv.EndUpdate() : ResetStatus()
        RefreshInfo()
    End Sub

    Sub RefreshInfo()
        Dim TotalSize As Long
        For Each File As FileInfo In Files
            TotalSize += File.Length
        Next
        txtFileCount.Text = Files.Count.ToString
        txtTotalSize.Text = FormattedSize(TotalSize)
    End Sub

    Sub SaveSSP()
        If SSPWriter.IsBusy Then
            btnSaveSSP.Text = "Stopping..."
            SSPWriter.StopWrite()
        Else
            Dim sfd As New SaveFileDialog
            sfd.Filter = "MaiSoft Simple Sequence Pack|*.ssp"
            sfd.FileName = ""
            sfd.Title = "Save SSP..."
            sfd.ShowDialog()
            If sfd.FileName <> "" Then
                SSPPath = sfd.FileName
                SSPWriter.Files = Me.Files
                SSPWriter.StartWrite(SSPPath)
            End If
        End If
        UpdateCtrl()
    End Sub

    Sub ResetStatus()
        ProgressBar1.Value = 0
        lblStatus.Text = "Idle..."
        Me.Cursor = Cursors.Default
    End Sub

    Sub ResetSortArrow()
        lv._SortingOrder = SortOrder.None
        lv._SortingColumn = -1
    End Sub

    Sub ResetIndexColumn()
        For a As Integer = 0 To lv.Items.Count - 1
            lv.Items(a).SubItems(1).Text = a.ToString
        Next
    End Sub

    Sub UpdateCtrl()
        If SSPWriter.IsBusy Then
            btnAddFiles.Enabled = False
            btnAddFolder.Enabled = False
            btnClearList.Enabled = False
            btnMoveDown.Enabled = False
            btnMoveUp.Enabled = False
            btnRemove.Enabled = False
            btnSaveSSP.Text = "Stop writing (F12)"
            lv.AllowDrop = False
            lv._ShowSortArrow = False
            Me.AllowDrop = False
            Me.Cursor = Cursors.WaitCursor
        Else
            btnAddFiles.Enabled = True
            btnAddFolder.Enabled = True
            btnClearList.Enabled = True
            btnMoveDown.Enabled = True
            btnMoveUp.Enabled = True
            btnRemove.Enabled = True
            btnSaveSSP.Text = "Save SSP... (F12)"
            lv.AllowDrop = True
            lv._ShowSortArrow = True
            Me.AllowDrop = True
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Sub UpdateLvHeader()
        If ShowFileNameOnly Then
            lv.Columns(2).Text = "File Name"
        Else
            lv.Columns(2).Text = "File Path"
        End If
        If ShowSizeInMB Then
            lv.Columns(3).Text = "Size (MB)"
        Else
            lv.Columns(3).Text = "Size (KB)"
        End If
    End Sub

    Private Sub SSPWriter_Done(Cancelled As Boolean, Err As Exception) Handles SSPWriter.Done
        If Cancelled Then
            File.Delete(SSPPath)
            If ExitAfterCancelled Then Me.Close()
            Dim Msg As String = "Failed to save SSP!"
            If Err IsNot Nothing Then
                Msg &= vbCrLf & Err.Message
            End If
            MessageBox.Show(Msg, "Oops...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            lblStatus.Text = "Done!"
            ProgressBar1.Value = 110
            FileSaved = True
            MessageBox.Show("SSP has been saved successfully!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        UpdateCtrl()
        ResetStatus()
    End Sub

    Private Sub SSPWriter_Writing(ByVal FileIndex As Integer, ByVal Chuck As Integer, ByVal TotalChuck As Integer) Handles SSPWriter.Writing
        Dim FilePath As String = Files(FileIndex).FullName
        Dim PercentPerFile As Double = 1 / Files.Count * 100
        Dim PercentOfChuck As Double = Chuck / TotalChuck * PercentPerFile
        ProgressBar1.Value = Convert.ToInt32(10 + FileIndex * PercentPerFile + PercentOfChuck)
        lblStatus.Text = String.Format("Writing {0} ({1}/{2})", FilePath, FileIndex + 1, SSPWriter.Files.Count)
    End Sub

    Private Sub SSPWriter_WritingHeader() Handles SSPWriter.WritingHeader
        lblStatus.Text = "Writing header..."
        ProgressBar1.Value = 3
    End Sub

    Private Sub frmMaker_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If SSPWriter IsNot Nothing AndAlso SSPWriter.IsBusy Then
            If MessageBox.Show("SSP is still saving! Are you sure you want to exit?", "Wait!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                SSPWriter.StopWrite()
                ExitAfterCancelled = True
            End If
            e.Cancel = True
        ElseIf Files.Count <> 0 AndAlso Not FileSaved Then
            If MessageBox.Show("Your SSP hasn't saved yet! Are you sure you want to exit?", "Wait!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmMaker_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Modifiers = Keys.Control Then
            Select Case e.KeyCode
                Case Keys.Insert : AddFilesInFolder()
                Case Keys.Delete : ClearList()
                Case Keys.Up : MoveUp()
                Case Keys.Down : MoveDown()
            End Select
        Else
            Select Case e.KeyCode
                Case Keys.F5 : RefreshList()
                Case Keys.Insert : AddFiles()
                Case Keys.Delete : RemoveSelected()
                Case Keys.F12 : SaveSSP()
            End Select
        End If
    End Sub

    Private Sub frmMaker_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ClearList2()
        ExitAfterCancelled = False
    End Sub

    Private Sub frmMaker_Lv_DragDrop(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragDrop, lv.DragDrop
        AddFilesByDragDrop(e)
    End Sub

    Private Sub frmMaker_Lv_DragEnter(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragEnter, lv.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub btnTips_Click(sender As System.Object, e As System.EventArgs) Handles btnTips.Click
        frmMakerTips.ShowDialog()
    End Sub

    Private Sub btnAddFiles_Click(sender As System.Object, e As System.EventArgs) Handles btnAddFiles.Click
        AddFiles()
    End Sub

    Private Sub btnAddFolder_Click(sender As System.Object, e As System.EventArgs) Handles btnAddFolder.Click
        AddFilesInFolder()
    End Sub

    Private Sub btnRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnRemove.Click
        RemoveSelected()
    End Sub

    Private Sub btnClearList_Click(sender As System.Object, e As System.EventArgs) Handles btnClearList.Click
        ClearList()
    End Sub

    Private Sub btnMoveUp_Click(sender As System.Object, e As System.EventArgs) Handles btnMoveUp.Click
        MoveUp()
    End Sub

    Private Sub btnMoveDown_Click(sender As System.Object, e As System.EventArgs) Handles btnMoveDown.Click
        MoveDown()
    End Sub

    Private Sub btnSaveSSP_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveSSP.Click
        SaveSSP()
    End Sub

    Private Sub lv_ColumnWidthChanged(sender As System.Object, e As System.Windows.Forms.ColumnWidthChangedEventArgs) Handles lv.ColumnWidthChanged
        If lv.Columns(0).Width <> 0 Then lv.Columns(0).Width = 0
    End Sub

    Private Sub lv_ColumnWidthChanging(sender As System.Object, e As System.Windows.Forms.ColumnWidthChangingEventArgs) Handles lv.ColumnWidthChanging
        If e.ColumnIndex = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub lv_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles lv.KeyDown
        ' Tell Windows not to mess with my Ctrl+Up and Ctrl+Down hotkey
        If e.Modifiers = Keys.Control Then
            If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then e.Handled = True
        End If
    End Sub

    Private Sub lv_ColumnClick(sender As System.Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lv.ColumnClick
        If SSPWriter.IsBusy Then Return
        Dim order As SortOrder = lv._SortingOrder
        Dim sorter As IComparer
        ' Column0 stores the file size in bytes, so we sort by column0, which is better than KB/MB of column2
        Select Case e.Column
            Case 2
                sorter = New lvItemSorter(2, order)
            Case 3
                sorter = New lvItemSorter(0, order)
            Case Else
                Return
        End Select
        lv.BeginUpdate() : FileSaved = False : Me.Cursor = Cursors.WaitCursor : lblStatus.Text = "Sorting..." : Me.Refresh()
        ' It sorts automatically after you set the sorter
        lv.ListViewItemSorter = sorter
        ' After sort, null the sorter or our listview will be fxxxing slow, 
        ' and set _SortingOrder to restore our sorting arrow
        lv.ListViewItemSorter = Nothing
        lv._SortingOrder = order
        ' After sort, item index will be messed up, so we reset it
        ResetIndexColumn()
        ' EnsureVisible so user won't lost their selected item
        If lv.SelectedIndices.Count = 1 Then
            lv.EnsureVisible(lv.SelectedIndices(0))
        End If
        ' At last, sort Files in RAM
        Select Case e.Column
            Case 2
                Files.Sort(New FileInfoComparer(FileInfoSortType.ByFileName, order))
            Case 3
                Files.Sort(New FileInfoComparer(FileInfoSortType.BySize, order))
        End Select
        lv.EndUpdate() : ResetStatus()
    End Sub

    Private Sub lv_ColumnRightClicked(Column As System.Windows.Forms.ColumnHeader) Handles lv.ColumnRightClicked
        If SSPWriter.IsBusy Then Return
        Select Case Column.Index
            Case 2
                ShowFileNameOnly = Not ShowFileNameOnly
            Case 3
                ShowSizeInMB = Not ShowSizeInMB
            Case Else
                Return
        End Select
        UpdateLvHeader()
        ResetSortArrow() : FileSaved = False
        RefreshList()
    End Sub

    Private Sub lv_ItemDragDropped(DraggedItem As System.Windows.Forms.ListViewItem, DroppedItem As System.Windows.Forms.ListViewItem) Handles lv.ItemDragDropped
        Dim OldIndex As Integer = DraggedItem.Index
        Dim NewIndex As Integer = DroppedItem.Index
        lv.BeginUpdate() : ResetSortArrow() : FileSaved = False
        ReorderFile(OldIndex, NewIndex)
        ReorderItem2(OldIndex, NewIndex)
        lblStatus.Text = "Reseting column index..." : Me.Cursor = Cursors.WaitCursor : Me.Refresh()
        ResetIndexColumn()
        SelectItem(NewIndex)
        lv.EndUpdate() : ResetStatus()
    End Sub

    Private Class lvItemSorter
        Implements IComparer

        Private Column As Integer
        Private Order As SortOrder

        Public Sub New(Column As Integer, Order As SortOrder)
            Me.Column = Column
            Me.Order = Order
        End Sub

        Public Function Compare(x As Object, y As Object) As Integer Implements System.Collections.IComparer.Compare
            Dim ret As Integer
            Dim i1, i2 As ListViewItem
            i1 = CType(x, ListViewItem)
            i2 = CType(y, ListViewItem)
            Using c As New NaturalComparer
                ret = c.Compare(i1.SubItems(Column).Text, i2.SubItems(Column).Text)
            End Using
            If Order = SortOrder.Descending Then ret *= -1
            Return ret
        End Function
    End Class
End Class
