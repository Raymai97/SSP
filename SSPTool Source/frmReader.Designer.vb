<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReader
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
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.btnOpenSSP = New System.Windows.Forms.Button()
        Me.btnCloseSSP = New System.Windows.Forms.Button()
        Me.grpSSPInfo = New System.Windows.Forms.GroupBox()
        Me.btnStopExtract = New System.Windows.Forms.Button()
        Me.txtInfo = New System.Windows.Forms.TextBox()
        Me.grpExtract = New System.Windows.Forms.GroupBox()
        Me.btnExtractAll = New System.Windows.Forms.Button()
        Me.btnExtract = New System.Windows.Forms.Button()
        Me.txtFileSize = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.numIndex = New System.Windows.Forms.NumericUpDown()
        Me.grpSSPInfo.SuspendLayout()
        Me.grpExtract.SuspendLayout()
        CType(Me.numIndex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 261)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(594, 10)
        Me.ProgressBar1.TabIndex = 7
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatus.Location = New System.Drawing.Point(9, 244)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(3)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(573, 15)
        Me.lblStatus.TabIndex = 8
        Me.lblStatus.Text = "Idle..."
        '
        'btnOpenSSP
        '
        Me.btnOpenSSP.Location = New System.Drawing.Point(12, 12)
        Me.btnOpenSSP.Name = "btnOpenSSP"
        Me.btnOpenSSP.Size = New System.Drawing.Size(100, 30)
        Me.btnOpenSSP.TabIndex = 0
        Me.btnOpenSSP.Text = "Open SSP"
        Me.btnOpenSSP.UseVisualStyleBackColor = True
        '
        'btnCloseSSP
        '
        Me.btnCloseSSP.Enabled = False
        Me.btnCloseSSP.Location = New System.Drawing.Point(12, 48)
        Me.btnCloseSSP.Name = "btnCloseSSP"
        Me.btnCloseSSP.Size = New System.Drawing.Size(100, 30)
        Me.btnCloseSSP.TabIndex = 1
        Me.btnCloseSSP.Text = "Close SSP"
        Me.btnCloseSSP.UseVisualStyleBackColor = True
        '
        'grpSSPInfo
        '
        Me.grpSSPInfo.Controls.Add(Me.btnStopExtract)
        Me.grpSSPInfo.Controls.Add(Me.txtInfo)
        Me.grpSSPInfo.Controls.Add(Me.grpExtract)
        Me.grpSSPInfo.Enabled = False
        Me.grpSSPInfo.Location = New System.Drawing.Point(118, 12)
        Me.grpSSPInfo.Name = "grpSSPInfo"
        Me.grpSSPInfo.Size = New System.Drawing.Size(464, 226)
        Me.grpSSPInfo.TabIndex = 10
        Me.grpSSPInfo.TabStop = False
        Me.grpSSPInfo.Text = "SSP Info"
        '
        'btnStopExtract
        '
        Me.btnStopExtract.Enabled = False
        Me.btnStopExtract.Location = New System.Drawing.Point(293, 190)
        Me.btnStopExtract.Name = "btnStopExtract"
        Me.btnStopExtract.Size = New System.Drawing.Size(130, 30)
        Me.btnStopExtract.TabIndex = 7
        Me.btnStopExtract.Text = "Stop extract"
        Me.btnStopExtract.UseVisualStyleBackColor = True
        '
        'txtInfo
        '
        Me.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInfo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInfo.Location = New System.Drawing.Point(6, 22)
        Me.txtInfo.Multiline = True
        Me.txtInfo.Name = "txtInfo"
        Me.txtInfo.ReadOnly = True
        Me.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtInfo.Size = New System.Drawing.Size(246, 198)
        Me.txtInfo.TabIndex = 2
        Me.txtInfo.WordWrap = False
        '
        'grpExtract
        '
        Me.grpExtract.Controls.Add(Me.btnExtractAll)
        Me.grpExtract.Controls.Add(Me.btnExtract)
        Me.grpExtract.Controls.Add(Me.txtFileSize)
        Me.grpExtract.Controls.Add(Me.Label2)
        Me.grpExtract.Controls.Add(Me.Label1)
        Me.grpExtract.Controls.Add(Me.numIndex)
        Me.grpExtract.Location = New System.Drawing.Point(258, 22)
        Me.grpExtract.Name = "grpExtract"
        Me.grpExtract.Size = New System.Drawing.Size(200, 162)
        Me.grpExtract.TabIndex = 0
        Me.grpExtract.TabStop = False
        Me.grpExtract.Text = "Extract!"
        '
        'btnExtractAll
        '
        Me.btnExtractAll.Location = New System.Drawing.Point(35, 119)
        Me.btnExtractAll.Name = "btnExtractAll"
        Me.btnExtractAll.Size = New System.Drawing.Size(130, 30)
        Me.btnExtractAll.TabIndex = 6
        Me.btnExtractAll.Text = "Extract all..."
        Me.btnExtractAll.UseVisualStyleBackColor = True
        '
        'btnExtract
        '
        Me.btnExtract.Location = New System.Drawing.Point(50, 83)
        Me.btnExtract.Name = "btnExtract"
        Me.btnExtract.Size = New System.Drawing.Size(100, 30)
        Me.btnExtract.TabIndex = 5
        Me.btnExtract.Text = "Extract!"
        Me.btnExtract.UseVisualStyleBackColor = True
        '
        'txtFileSize
        '
        Me.txtFileSize.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFileSize.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFileSize.Location = New System.Drawing.Point(90, 47)
        Me.txtFileSize.Name = "txtFileSize"
        Me.txtFileSize.ReadOnly = True
        Me.txtFileSize.Size = New System.Drawing.Size(100, 20)
        Me.txtFileSize.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(87, 24)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "File size:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 24)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Index:"
        '
        'numIndex
        '
        Me.numIndex.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numIndex.Location = New System.Drawing.Point(16, 45)
        Me.numIndex.Name = "numIndex"
        Me.numIndex.Size = New System.Drawing.Size(60, 27)
        Me.numIndex.TabIndex = 3
        '
        'frmReader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 271)
        Me.Controls.Add(Me.grpSSPInfo)
        Me.Controls.Add(Me.btnCloseSSP)
        Me.Controls.Add(Me.btnOpenSSP)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(600, 300)
        Me.MinimumSize = New System.Drawing.Size(600, 300)
        Me.Name = "frmReader"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Simple Sequence Pack Reader"
        Me.grpSSPInfo.ResumeLayout(false)
        Me.grpSSPInfo.PerformLayout
        Me.grpExtract.ResumeLayout(false)
        Me.grpExtract.PerformLayout
        CType(Me.numIndex,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents btnOpenSSP As System.Windows.Forms.Button
    Friend WithEvents btnCloseSSP As System.Windows.Forms.Button
    Friend WithEvents grpSSPInfo As System.Windows.Forms.GroupBox
    Friend WithEvents grpExtract As System.Windows.Forms.GroupBox
    Friend WithEvents txtFileSize As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents numIndex As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtInfo As System.Windows.Forms.TextBox
    Friend WithEvents btnExtractAll As System.Windows.Forms.Button
    Friend WithEvents btnExtract As System.Windows.Forms.Button
    Friend WithEvents btnStopExtract As System.Windows.Forms.Button
End Class
