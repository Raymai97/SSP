<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStartup
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnStartMaker = New System.Windows.Forms.Button()
        Me.btnReader = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnStartMaker
        '
        Me.btnStartMaker.Location = New System.Drawing.Point(182, 12)
        Me.btnStartMaker.Name = "btnStartMaker"
        Me.btnStartMaker.Size = New System.Drawing.Size(146, 48)
        Me.btnStartMaker.TabIndex = 0
        Me.btnStartMaker.Text = "Start Maker"
        Me.btnStartMaker.UseVisualStyleBackColor = True
        '
        'btnReader
        '
        Me.btnReader.Location = New System.Drawing.Point(334, 12)
        Me.btnReader.Name = "btnReader"
        Me.btnReader.Size = New System.Drawing.Size(146, 48)
        Me.btnReader.TabIndex = 1
        Me.btnReader.Text = "Start Reader"
        Me.btnReader.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(164, 48)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Which program do you want to use?"
        '
        'frmStartup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 30.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(494, 71)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnReader)
        Me.Controls.Add(Me.btnStartMaker)
        Me.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmStartup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MaiSoft Simple Sequence Pack"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnStartMaker As System.Windows.Forms.Button
    Friend WithEvents btnReader As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
