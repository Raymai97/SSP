Public Class frmStartup

    Private Sub btnMaker_Click(sender As System.Object, e As System.EventArgs) Handles btnMaker.Click
        Me.Hide()
        frmMaker.ShowDialog()
        Me.Show()
    End Sub

    Private Sub btnReader_Click(sender As System.Object, e As System.EventArgs) Handles btnReader.Click
        Me.Hide()
        frmReader.ShowDialog()
        Me.Show()
    End Sub

    Private Sub lnkHomepage_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHomepage.LinkClicked
        Process.Start("https://github.com/Raymai97/SSP")
    End Sub

End Class
