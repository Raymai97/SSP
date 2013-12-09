Public Class frmStartup

    Private Sub btnStartMaker_Click(sender As System.Object, e As System.EventArgs) Handles btnStartMaker.Click
        Me.Hide()
        frmMaker.ShowDialog()
        Me.Show()
    End Sub

    Private Sub btnReader_Click(sender As System.Object, e As System.EventArgs) Handles btnReader.Click
        Me.Hide()
        frmReader.ShowDialog()
        Me.Show()
    End Sub

End Class
