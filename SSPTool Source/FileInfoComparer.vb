' A simple FileInfoComparer
' Written by MaiSoft (Raymai97) on 9 Dec 2013.

Imports System.IO

Public Class FileInfoComparer
    Implements IComparer(Of FileInfo)

    Private SortBy As FileInfoSortType
    Private SortOrder As SortOrder

    Public Sub New(SortBy As FileInfoSortType, SortOrder As SortOrder)
        Me.SortBy = SortBy
        Me.SortOrder = SortOrder
    End Sub

    Public Function Compare(x As System.IO.FileInfo, y As System.IO.FileInfo) As Integer Implements System.Collections.Generic.IComparer(Of System.IO.FileInfo).Compare
        Dim ret As Integer
        Using c As New NaturalComparer
            Select Case SortBy
                Case FileInfoSortType.ByFileName
                    ret = c.Compare(x.FullName, y.FullName)
                Case FileInfoSortType.BySize
                    ret = x.Length.CompareTo(y.Length)
            End Select
        End Using
        If SortOrder = Windows.Forms.SortOrder.Descending Then ret *= -1
        Return ret
    End Function
End Class

Public Enum FileInfoSortType
    ByFileName
    BySize
End Enum