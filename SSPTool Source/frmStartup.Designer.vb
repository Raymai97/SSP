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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStartup))
        Me.btnMaker = New System.Windows.Forms.Button()
        Me.btnReader = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lnkHomepage = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'btnMaker
        '
        Me.btnMaker.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(167, Byte), Integer))
        Me.btnMaker.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(167, Byte), Integer))
        Me.btnMaker.FlatAppearance.BorderSize = 0
        Me.btnMaker.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(147, Byte), Integer))
        Me.btnMaker.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnMaker.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMaker.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMaker.ForeColor = System.Drawing.Color.White
        Me.btnMaker.Image = Global.SSP.My.Resources.Resources.SSPMaker
        Me.btnMaker.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnMaker.Location = New System.Drawing.Point(14, 127)
        Me.btnMaker.Margin = New System.Windows.Forms.Padding(2)
        Me.btnMaker.Name = "btnMaker"
        Me.btnMaker.Padding = New System.Windows.Forms.Padding(12, 6, 3, 9)
        Me.btnMaker.Size = New System.Drawing.Size(205, 100)
        Me.btnMaker.TabIndex = 0
        Me.btnMaker.Text = "SSPMaker"
        Me.btnMaker.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnMaker.UseVisualStyleBackColor = False
        '
        'btnReader
        '
        Me.btnReader.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(167, Byte), Integer))
        Me.btnReader.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(167, Byte), Integer))
        Me.btnReader.FlatAppearance.BorderSize = 0
        Me.btnReader.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(147, Byte), Integer))
        Me.btnReader.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnReader.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReader.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReader.ForeColor = System.Drawing.Color.White
        Me.btnReader.Image = Global.SSP.My.Resources.Resources.SSPReader
        Me.btnReader.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnReader.Location = New System.Drawing.Point(223, 127)
        Me.btnReader.Margin = New System.Windows.Forms.Padding(2)
        Me.btnReader.Name = "btnReader"
        Me.btnReader.Padding = New System.Windows.Forms.Padding(12, 6, 3, 9)
        Me.btnReader.Size = New System.Drawing.Size(205, 100)
        Me.btnReader.TabIndex = 1
        Me.btnReader.Text = "SSPReader"
        Me.btnReader.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnReader.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 12)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(420, 110)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'lnkHomepage
        '
        Me.lnkHomepage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkHomepage.AutoSize = True
        Me.lnkHomepage.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkHomepage.Location = New System.Drawing.Point(308, 242)
        Me.lnkHomepage.Margin = New System.Windows.Forms.Padding(3)
        Me.lnkHomepage.Name = "lnkHomepage"
        Me.lnkHomepage.Size = New System.Drawing.Size(124, 17)
        Me.lnkHomepage.TabIndex = 2
        Me.lnkHomepage.TabStop = True
        Me.lnkHomepage.Text = "Visit SSP homepage"
        '
        'frmStartup
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(444, 271)
        Me.Controls.Add(Me.lnkHomepage)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnReader)
        Me.Controls.Add(Me.btnMaker)
        Me.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(450, 300)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(450, 300)
        Me.Name = "frmStartup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MaiSSPTool v1.1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnMaker As System.Windows.Forms.Button
    Friend WithEvents btnReader As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lnkHomepage As System.Windows.Forms.LinkLabel
End Class
