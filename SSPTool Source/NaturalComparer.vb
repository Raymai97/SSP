' Code source: http://www.codeproject.com/Articles/22517/Natural-Sort-Comparer

Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Public Class NaturalComparer
    Inherits Comparer(Of String)
    Implements IDisposable

    Private table As Dictionary(Of String, String())

    Public Sub New()
        table = New Dictionary(Of String, String())()
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        table.Clear()
        table = Nothing
    End Sub

    Public Overrides Function Compare(x As String, y As String) As Integer
        If x = y Then
            Return 0
        End If
        Dim x1() As String = Nothing
        Dim y1() As String = Nothing
        If Not table.TryGetValue(x, x1) Then
            x1 = Regex.Split(x.Replace(" ", ""), "([0-9]+)")
            table.Add(x, x1)
        End If
        If Not table.TryGetValue(y, y1) Then
            y1 = Regex.Split(y.Replace(" ", ""), "([0-9]+)")
            table.Add(y, y1)
        End If
        Dim i As Integer = 0
        While i < x1.Length AndAlso i < y1.Length
            If x1(i) <> y1(i) Then
                Return PartCompare(x1(i), y1(i))
            End If
            i += 1
        End While
        If y1.Length > x1.Length Then
            Return 1
        ElseIf x1.Length > y1.Length Then
            Return -1
        Else
            Return 0
        End If
    End Function

    Private Shared Function PartCompare(left As String, right As String) As Integer
        Dim x, y As Integer
        If Not Integer.TryParse(left, x) Then
            Return left.CompareTo(right)
        End If
        If Not Integer.TryParse(right, y) Then
            Return left.CompareTo(right)
        End If
        Return x.CompareTo(y)
    End Function
End Class
