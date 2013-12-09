Imports SSP.AppShared

Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            Dim icon As Icon = GetIconOf(System.Windows.Forms.Application.ExecutablePath, True)
            frmStartup.Icon = icon
            frmMaker.Icon = icon
            frmReader.Icon = icon
        End Sub

        Private Sub MyApplication_UnhandledException(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            dlgError.lbl.Text = e.Exception.Message
            dlgError.txt.Text = e.Exception.ToString
            dlgError.ShowDialog()
            e.ExitApplication = False
        End Sub
    End Class


End Namespace

