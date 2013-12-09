Imports System.Runtime.InteropServices

Public Class AppShared

    Public Shared Function FormattedSize(SizeInByte As Long) As String
        Select Case SizeInByte
            Case Is > 1024 * 1024 * 1024
                Return Math.Round(SizeInByte / 1024 / 1024 / 1024, 2).ToString("0.00") & "GB"
            Case Is > 1024 * 1024
                Return Math.Round(SizeInByte / 1024 / 1024, 2).ToString("0.00") & "MB"
            Case Is > 1024
                Return Math.Round(SizeInByte / 1024, 2).ToString("0.00") & "KB"
            Case Else
                Return SizeInByte.ToString & "Bytes"
        End Select
    End Function

    ' Let this exe icon as its form's icon
    Declare Function SHGetFileInfo Lib "shell32.dll" (ByVal pszPath As String, _
  ByVal dwFileAttributes As Integer, _
  ByRef psfi As SHFILEINFO, _
  ByVal cbFileInfo As Integer, _
  ByVal uFlags As SHGFI) As IntPtr

    Structure SHFILEINFO
        Public hIcon As IntPtr
        Public iIcon As Integer
        Public dwAttributes As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> _
        Public szDisplayName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> _
        Public szTypeName As String
    End Structure

    <Flags()> _
    Enum SHGFI
        ICON = &H100
        DISPLAYNAME = &H200
        TYPENAME = &H400
        ATTRIBUTES = &H800
        ICONLOCATION = &H1000
        EXETYPE = &H2000
        SYSICONINDEX = &H4000
        LINKOVERLAY = &H8000
        SELECTED = &H10000
        ATTR_SPECIFIED = &H20000
        LARGEICON = &H0
        SMALLICON = &H1
        OPENICON = &H2
        SHELLICONSIZE = &H4
        PIDL = &H8
        USEFILEATTRIBUTES = &H10
        ADDOVERLAYS = &H20
        OVERLAYINDEX = &H40
    End Enum

    ''' <summary>Get the display icon of stuff, such as the icon of a folder. If the stuff is icon file, such as exe and dll, will return the icon that used to display.</summary>
    ''' <param name="StuffPath">Stuff can be either file or folder.</param>
    Public Shared Function GetIconOf(ByVal StuffPath As String, ByVal LargeIcon As Boolean) As Icon
        Dim info As New SHFILEINFO
        Dim cbFileInfo As Integer = Marshal.SizeOf(info)
        Dim flags As SHGFI
        If LargeIcon Then
            flags = SHGFI.ICON Or SHGFI.LARGEICON
        Else
            flags = SHGFI.ICON Or SHGFI.SMALLICON
        End If
        SHGetFileInfo(StuffPath, 256, info, cbFileInfo, flags)
        Return Drawing.Icon.FromHandle(info.hIcon)
    End Function

End Class
