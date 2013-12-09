' MaiListView - a better ListView
' Written by MaiSoft (Raymai97) on 9 Dec 2013.

Imports System.Runtime.InteropServices
Imports System.ComponentModel

Public Class MaiListView
    Inherits ListView

#Region "Event"

    Public Event ColumnRightClicked(Column As ColumnHeader)
    Public Event ItemDragDropped(DraggedItem As ListViewItem, DroppedItem As ListViewItem)

#End Region

#Region "Property"

    <Description("Enable to use Vista nice-looking listview style. Changing this in runtime give no effect.")> _
    Public Property _UseVistaStyle() As Boolean = True
    <Description("Make drawing speed faster, but it will not custom draw anything, including custom backcolor of ListViewItem!")> _
    Public Property _NoCustomDraw() As Boolean = True
    <Description("Enable to eliminate flickering problem. Changing this in runtime give no effect.")> _
    Public Property _DoubleBuffer() As Boolean = True
    <Description("If enabled, it will show sorting arrow on column, but will not sort the list.")> _
    Public Property _ShowSortArrow() As Boolean = True
    <Description("Let's say you don't want column 0 and 1 involved in sorting arrow routine, type ""0/1"".")> _
    Public Property _ExcludeColumn() As String
    <Description("If enabled, user can drag and drop ListViewItem. Ideal for item reordering.")> _
    Public Property _AllowDragDropItem() As Boolean = False
    <Description("When dragging item and mouse is within the scroll edge distance (nearer top or bottom edge of ListView), ListView will scroll up or down. Set _DragDropScrollInteval as 0 to disable this.")> _
    Public Property _DragDropScrollEdgeDist() As Integer = 0
    <Description("The shorter the inteval, the more frequent the scrolling occur. Set it wisely, too frequently can cause unwanted behavior. Set 0 to disable this.")> _
    Public Property _DragDropScrollInteval() As Integer
        Get
            If DragDropScrollTimer.Enabled Then
                Return DragDropScrollTimer.Interval
            Else
                Return 0
            End If
        End Get
        Set(value As Integer)
            If value = 0 Then
                DragDropScrollTimer.Enabled = False
            Else
                DragDropScrollTimer.Enabled = True
                DragDropScrollTimer.Interval = value
            End If
        End Set
    End Property

    Public Property _SortingColumn As Integer
        Get
            Return SortingColumn
        End Get
        Set(value As Integer)
            SetSortArrow(value)
        End Set
    End Property
    Public Property _SortingOrder As SortOrder
        Get
            Return SortingOrder
        End Get
        Set(value As SortOrder)
            SortingOrder = value
            _SetSortArrow(SortingColumn, SortingOrder)
        End Set
    End Property

#End Region

#Region "API Declaration"
    Private Declare Unicode Function SetWindowTheme Lib "uxtheme.dll" (ByVal hWnd As IntPtr, ByVal pszSubAppName As String, ByVal pszSubIdList As String) As Integer

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Int32, wParam As IntPtr, lParam As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Int32, wParam As IntPtr, ByRef lParam As HDITEM) As IntPtr
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Int32, wParam As Int32, ByRef lParam As LVITEM) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function GetScrollInfo(hWnd As IntPtr, _
        fnBar As ScrollBarDirection, _
        <MarshalAs(UnmanagedType.Struct)> ByRef lpsi As SCROLLINFO) As Integer
    End Function
#End Region

#Region "Const and ENUM"
    Private Const NM_CUSTOMDRAW As Int32 = -12
    ' Get scroll offset
    Enum ScrollInfoMask As Int32
        SIF_RANGE = &H1
        SIF_PAGE = &H2
        SIF_POS = &H4
        SIF_DISABLENOSCROLL = &H8
        SIF_TRACKPOS = &H10
        SIF_ALL = (SIF_RANGE Or SIF_PAGE Or SIF_POS Or SIF_TRACKPOS)
    End Enum
    Enum ScrollBarDirection As Int32
        SB_HORZ = &H0
        SB_VERT = &H1
        SB_CTL = &H2
        SB_BOTH = &H3
    End Enum
    ' Scroll
    Const WM_SCROLL As Int32 = &H115S
    ' ListView Message
    Const LVM_FIRST As Int32 = &H1000
    Const LVM_GETHEADER As Int32 = LVM_FIRST + 31
    Const LVM_SETITEMSTATE As Int32 = LVM_FIRST + 43
    Const LVM_GETITEMSTATE As Int32 = LVM_FIRST + 44
    Const LVM_GETSELECTEDCOUNT As Int32 = LVM_FIRST + 50
    ' ListView Header
    Const HDM_FIRST As Int32 = &H1200
    Const HDM_GETITEM As Int32 = HDM_FIRST + 11
    Const HDM_SETITEM As Int32 = HDM_FIRST + 12
    ' ListView Header Sort Arrow
    Const HDI_FORMAT As Int32 = &H4
    Const HDF_SORTDOWN As Int32 = &H200
    Const HDF_SORTUP As Int32 = &H400
    ' LVITEM Mask
    Const LVIF_STATE As Int32 = &H8
    ' LVITEM State
    Const LVIS_FOCUSED As Int32 = &H1
    Const LVIS_SELECTED As Int32 = &H2
    Const LVIS_CUT As Int32 = &H4
    Const LVIS_DROPHILITED As Int32 = &H8 ' Highlight ListViewItem as drag-drop target
#End Region

#Region "Structure"
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure NMHDR
        Public hwndFrom As IntPtr
        Public idFrom As Int32
        Public code As Int32
    End Structure
    ' Sort Arrow
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure HDITEM
        Public mask As Int32
        Public cxy As Int32
        <MarshalAs(UnmanagedType.LPTStr)> Public pszText As String
        Public hbm As IntPtr
        Public cchTextMax As Int32
        Public fmt As Int32
        Public lParam As IntPtr
        Public iImage As Int32
        Public iOrder As Int32
        Public type As Int32
        Public pvFilter As IntPtr
        Public state As Int32
    End Structure
    ' Get scroll offset
    <Serializable(), StructLayout(LayoutKind.Sequential)> _
    Structure SCROLLINFO
        Public cbSize As Int32
        Public fMask As Int32
        Public nMin As Int32
        Public nMax As Int32
        Public nPage As Int32
        Public nPos As Int32
        Public nTrackPos As Int32
    End Structure
    ' Set ListViewItem State
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure LVITEM
        Public mask As Int32
        Public iItem As Int32
        Public iSubItem As Int32
        Public state As Int32
        Public stateMask As Int32
        Public pszText As String
        Public cchTextMax As Int32
        Public iImage As Int32
        Public lParam As Int32
        Public iIndent As Int32
    End Structure
#End Region

#Region "Variables"

    Private SortingColumn As Integer = -1
    Private SortingOrder As SortOrder
    Private MouseOverHeader As Boolean
    Private Dragging As Boolean
    Private WithEvents DragDropScrollTimer As New Timer

#End Region

#Region "Method"

    Protected Overrides Sub OnHandleCreated(e As System.EventArgs)
        If _UseVistaStyle And isXPOrAbove() Then SetWindowTheme(Me.Handle, "explorer", Nothing)
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, _DoubleBuffer)
        ' _AllowDragDropItem need AllowDrop = True
        If Me._AllowDragDropItem Then Me.AllowDrop = True
        MyBase.OnHandleCreated(e)
    End Sub

    Protected Overrides Sub OnColumnClick(e As System.Windows.Forms.ColumnClickEventArgs)
        ShowSortArrow_ColumnClick(e)
        MyBase.OnColumnClick(e)
    End Sub

    Protected Overrides Sub OnDragEnter(e As System.Windows.Forms.DragEventArgs)
        DragDropItem_DragEnter(e)
        MyBase.OnDragEnter(e)
    End Sub

    Protected Overrides Sub OnDragOver(e As System.Windows.Forms.DragEventArgs)
        DragDropItem_DragOver(e)
        MyBase.OnDragOver(e)
    End Sub

    Protected Overrides Sub OnDragDrop(e As System.Windows.Forms.DragEventArgs)
        DragDropItem_DragDrop(e)
        MyBase.OnDragDrop(e)
    End Sub

    Protected Overrides Sub OnItemDrag(e As System.Windows.Forms.ItemDragEventArgs)
        DragDropItem_ItemDrag(e)
        MyBase.OnItemDrag(e)
    End Sub

    ' When mouse over column header, OnMouseLeave will be raised
    Protected Overrides Sub OnMouseEnter(e As System.EventArgs)
        MouseOverHeader = False
        MyBase.OnMouseEnter(e)
    End Sub

    Protected Overrides Sub OnMouseLeave(e As System.EventArgs)
        MouseOverHeader = True
        MyBase.OnMouseLeave(e)
    End Sub

    Protected Overrides Sub OnMouseUp(e As System.Windows.Forms.MouseEventArgs)
        DragDropItem_MouseUp(e)
        MyBase.OnMouseUp(e)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Select Case m.Msg
            Case &H7B 'WM_CONTEXTMENU
                ' Column Right-Click
                If MouseOverHeader Then
                    Dim p As Point = Me.PointToClient(MousePosition)
                    p.X += GetScrollOffsetX()
                    Dim totalWidth As Integer = 0
                    For Each column As ColumnHeader In Me.Columns
                        totalWidth += column.Width
                        If p.X < totalWidth Then
                            RaiseEvent ColumnRightClicked(column)
                            Exit For
                        End If
                    Next
                End If
            Case &H204E
                If _NoCustomDraw Then
                    Dim hdr As NMHDR = CType(m.GetLParam(GetType(NMHDR)), NMHDR)
                    If hdr.code = NM_CUSTOMDRAW Then
                        m.Result = IntPtr.Zero
                        Return
                    End If
                End If
        End Select
        MyBase.WndProc(m)
    End Sub

    ' AllowDragDropItem
    Private Sub DragDropItem_DragEnter(e As System.Windows.Forms.DragEventArgs)
        If Not _AllowDragDropItem Then Return
        If e.Data.GetDataPresent(GetType(ListViewItem)) Then
            e.Effect = DragDropEffects.Move
        End If
    End Sub

    Private Sub DragDropItem_DragOver(e As System.Windows.Forms.DragEventArgs)
        If Not _AllowDragDropItem Then Return
        If e.Data.GetDataPresent(GetType(ListViewItem)) = False Then Return
        Dragging = True
        Static LastHoverIndex As Integer
        Dim p As Point = Me.PointToClient(New Point(e.X, e.Y))
        Dim HoverItem As ListViewItem = Me.GetItemAt(p.X, p.Y)
        If HoverItem Is Nothing Then Return
        If LastHoverIndex = HoverItem.Index Then Return
        HighlightItem(LastHoverIndex, True)
        HighlightItem(HoverItem.Index, False)
        LastHoverIndex = HoverItem.Index
    End Sub

    Private Sub DragDropItem_DragDrop(e As System.Windows.Forms.DragEventArgs)
        If Not _AllowDragDropItem Then Return
        If Me.SelectedItems.Count = 0 Then Return
        Dim p As Point = Me.PointToClient(New Point(e.X, e.Y))
        Dim DraggedItem As ListViewItem = Me.SelectedItems(0)
        Dim DroppedItem As ListViewItem = Me.GetItemAt(p.X, p.Y)
        If DroppedItem Is Nothing Then Return
        If DraggedItem Is DroppedItem Then Return
        ' Drag drop seem done, so no more dragging
        Dragging = False
        RaiseEvent ItemDragDropped(DraggedItem, DroppedItem)
    End Sub

    Private Sub DragDropItem_ItemDrag(e As System.Windows.Forms.ItemDragEventArgs)
        If Not _AllowDragDropItem Then Return
        Me.DoDragDrop(Me.SelectedItems(0), DragDropEffects.Move)
    End Sub

    Private Sub DragDropItem_MouseUp(e As System.Windows.Forms.MouseEventArgs)
        If Not _AllowDragDropItem Then Return
        'De-highlight all ListViewItem that highlighted as Drag-and-drop target
        HighlightItem(-1, True)
        Dragging = False
    End Sub

    Private Sub DragDropScrollTimer_Tick(sender As Object, e As System.EventArgs) Handles DragDropScrollTimer.Tick
        If Not Dragging Then Return
        Dim p As Point = Me.PointToClient(MousePosition)
        If p.Y < _DragDropScrollEdgeDist Then
            ScrollUp()
        ElseIf p.Y > Me.ClientSize.Height - _DragDropScrollEdgeDist Then
            ScrollDown()
        End If
    End Sub

    ' ShowSortArrow
    Private Sub ShowSortArrow_ColumnClick(e As ColumnClickEventArgs)
        If Not _ShowSortArrow Then Return
        If _ExcludeColumn Is Nothing Then _ExcludeColumn = ""
        Dim ignore As Boolean
        For Each s As String In _ExcludeColumn.Split("/"c)
            If s = e.Column.ToString Then
                ignore = True
                Exit For
            End If
        Next
        If Not ignore Then SetSortArrow(e.Column)
    End Sub


    Private Function GetScrollOffsetX() As Integer
        Dim sinfo As New SCROLLINFO
        sinfo.cbSize = Marshal.SizeOf(sinfo)
        sinfo.fMask = ScrollInfoMask.SIF_POS
        GetScrollInfo(Me.Handle, ScrollBarDirection.SB_HORZ, sinfo)
        Return sinfo.nPos
    End Function

    Private Function isXPOrAbove() As Boolean
        Dim ver As Version = Environment.OSVersion.Version
        Return ver.Major >= 5 AndAlso ver.Minor >= 1
    End Function

    ' Highlight ListViewItem as drag-drop target
    Private Sub HighlightItem(ItemIndex As Integer, Reverse As Boolean)
        Dim LVITEM As New LVITEM
        With LVITEM
            .mask = LVIF_STATE
            If Reverse Then
                .state = 0
            Else
                .state = -1
            End If
            .stateMask = LVIS_DROPHILITED
        End With
        SendMessage(Me.Handle, LVM_SETITEMSTATE, ItemIndex, LVITEM)
    End Sub

    Private Sub ScrollUp()
        _Scroll(0)
    End Sub

    Private Sub ScrollDown()
        _Scroll(1)
    End Sub

    Private Sub SetSortArrow(NewSortingColumn As Integer)
        If NewSortingColumn = SortingColumn Then
            Select Case SortingOrder
                Case SortOrder.Ascending : SortingOrder = SortOrder.Descending
                Case SortOrder.Descending : SortingOrder = SortOrder.Ascending
            End Select
        Else
            SortingColumn = NewSortingColumn
            SortingOrder = SortOrder.Ascending
        End If
        _SetSortArrow(SortingColumn, SortingOrder)
    End Sub

    Private Sub _Scroll(Direction As Integer)
        SendMessage(Me.Handle, WM_SCROLL, Direction, Nothing)
    End Sub

    Private Sub _SetSortArrow(ColumnIndex As Integer, Order As SortOrder)
        Dim pColumnHeader As IntPtr = SendMessage(Me.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero)
        For n As Integer = 0 To Me.Columns.Count - 1
            Dim item As New HDITEM
            item.mask = HDI_FORMAT
            If SendMessage(pColumnHeader, HDM_GETITEM, New IntPtr(n), item) = IntPtr.Zero Then
                Throw New Win32Exception()
            End If
            If n = ColumnIndex And Order <> SortOrder.None Then
                Select Case Order
                    Case SortOrder.Ascending
                        item.fmt = item.fmt And Not HDF_SORTDOWN
                        item.fmt = item.fmt Or HDF_SORTUP
                    Case SortOrder.Descending
                        item.fmt = item.fmt And Not HDF_SORTUP
                        item.fmt = item.fmt Or HDF_SORTDOWN
                End Select
            Else
                item.fmt = item.fmt And Not HDF_SORTDOWN And Not HDF_SORTUP
            End If
            If SendMessage(pColumnHeader, HDM_SETITEM, New IntPtr(n), item) = IntPtr.Zero Then
                Throw New Win32Exception()
            End If
        Next
    End Sub

#End Region

End Class