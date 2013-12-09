<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMaker
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
        Me.btnSaveSSP = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.btnAddFiles = New System.Windows.Forms.Button()
        Me.btnAddFolder = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnClearList = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnMoveUp = New System.Windows.Forms.Button()
        Me.btnMoveDown = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFileCount = New System.Windows.Forms.TextBox()
        Me.btnTips = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTotalSize = New System.Windows.Forms.TextBox()
        Me.lv = New SSP.MaiListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'btnSaveSSP
        '
        Me.btnSaveSSP.Location = New System.Drawing.Point(549, 399)
        Me.btnSaveSSP.Name = "btnSaveSSP"
        Me.btnSaveSSP.Size = New System.Drawing.Size(153, 30)
        Me.btnSaveSSP.TabIndex = 9
        Me.btnSaveSSP.Text = "Save SSP... (F12)"
        Me.btnSaveSSP.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 461)
        Me.ProgressBar1.Maximum = 110
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(714, 10)
        Me.ProgressBar1.TabIndex = 3
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblStatus.AutoEllipsis = True
        Me.lblStatus.Location = New System.Drawing.Point(9, 444)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(3)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(693, 15)
        Me.lblStatus.TabIndex = 4
        Me.lblStatus.Text = "Idle..."
        '
        'btnAddFiles
        '
        Me.btnAddFiles.Location = New System.Drawing.Point(549, 43)
        Me.btnAddFiles.Name = "btnAddFiles"
        Me.btnAddFiles.Size = New System.Drawing.Size(153, 30)
        Me.btnAddFiles.TabIndex = 1
        Me.btnAddFiles.Text = "Add files (Ins)"
        Me.btnAddFiles.UseVisualStyleBackColor = True
        '
        'btnAddFolder
        '
        Me.btnAddFolder.Location = New System.Drawing.Point(549, 79)
        Me.btnAddFolder.Name = "btnAddFolder"
        Me.btnAddFolder.Size = New System.Drawing.Size(153, 30)
        Me.btnAddFolder.TabIndex = 2
        Me.btnAddFolder.Text = "Add by folder (Ctrl+Ins)"
        Me.btnAddFolder.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 12)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(609, 25)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "SSP is ideal for storing thousands small files. If you have other requirement, co" & _
    "nsider other solutions instead."
        '
        'btnClearList
        '
        Me.btnClearList.Location = New System.Drawing.Point(549, 151)
        Me.btnClearList.Name = "btnClearList"
        Me.btnClearList.Size = New System.Drawing.Size(153, 30)
        Me.btnClearList.TabIndex = 4
        Me.btnClearList.Text = "Clear list (Ctrl+Del)"
        Me.btnClearList.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(549, 115)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(153, 30)
        Me.btnRemove.TabIndex = 3
        Me.btnRemove.Text = "Remove selected (Del)"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnMoveUp
        '
        Me.btnMoveUp.Location = New System.Drawing.Point(549, 201)
        Me.btnMoveUp.Name = "btnMoveUp"
        Me.btnMoveUp.Size = New System.Drawing.Size(153, 30)
        Me.btnMoveUp.TabIndex = 5
        Me.btnMoveUp.Text = "Move up (Ctrl+Up)"
        Me.btnMoveUp.UseVisualStyleBackColor = True
        '
        'btnMoveDown
        '
        Me.btnMoveDown.Location = New System.Drawing.Point(549, 237)
        Me.btnMoveDown.Name = "btnMoveDown"
        Me.btnMoveDown.Size = New System.Drawing.Size(153, 30)
        Me.btnMoveDown.TabIndex = 6
        Me.btnMoveDown.Text = "Move down (Ctrl+Down)"
        Me.btnMoveDown.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(549, 334)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 15)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Files in List:"
        '
        'txtFileCount
        '
        Me.txtFileCount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFileCount.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFileCount.Location = New System.Drawing.Point(622, 330)
        Me.txtFileCount.Name = "txtFileCount"
        Me.txtFileCount.ReadOnly = True
        Me.txtFileCount.Size = New System.Drawing.Size(80, 22)
        Me.txtFileCount.TabIndex = 7
        Me.txtFileCount.Text = "0"
        '
        'btnTips
        '
        Me.btnTips.Location = New System.Drawing.Point(627, 7)
        Me.btnTips.Name = "btnTips"
        Me.btnTips.Size = New System.Drawing.Size(75, 25)
        Me.btnTips.TabIndex = 10
        Me.btnTips.Text = "Tips"
        Me.btnTips.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(557, 362)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 15)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Total size:"
        '
        'txtTotalSize
        '
        Me.txtTotalSize.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTotalSize.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalSize.Location = New System.Drawing.Point(622, 358)
        Me.txtTotalSize.Name = "txtTotalSize"
        Me.txtTotalSize.ReadOnly = True
        Me.txtTotalSize.Size = New System.Drawing.Size(80, 22)
        Me.txtTotalSize.TabIndex = 8
        Me.txtTotalSize.Text = "0"
        '
        'lv
        '
        Me.lv._AllowDragDropItem = True
        Me.lv._DoubleBuffer = True
        Me.lv._DragDropScrollEdgeDist = 40
        Me.lv._DragDropScrollInteval = 100
        Me.lv._ExcludeColumn = "0/1"
        Me.lv._NoCustomDraw = True
        Me.lv._ShowSortArrow = True
        Me.lv._SortingColumn = -1
        Me.lv._SortingOrder = System.Windows.Forms.SortOrder.Ascending
        Me.lv._UseVistaStyle = True
        Me.lv.AllowDrop = True
        Me.lv.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
        Me.lv.FullRowSelect = True
        Me.lv.HideSelection = False
        Me.lv.Location = New System.Drawing.Point(12, 43)
        Me.lv.MultiSelect = False
        Me.lv.Name = "lv"
        Me.lv.Size = New System.Drawing.Size(531, 386)
        Me.lv.TabIndex = 0
        Me.lv.UseCompatibleStateImageBehavior = False
        Me.lv.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = ""
        Me.ColumnHeader1.Width = 0
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = ""
        Me.ColumnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader2.Width = 40
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "File Path"
        Me.ColumnHeader3.Width = 380
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Size (KB)"
        Me.ColumnHeader4.Width = 80
        '
        'frmMaker
        '
        Me.AllowDrop = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(714, 471)
        Me.Controls.Add(Me.btnTips)
        Me.Controls.Add(Me.txtTotalSize)
        Me.Controls.Add(Me.txtFileCount)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnMoveDown)
        Me.Controls.Add(Me.btnMoveUp)
        Me.Controls.Add(Me.btnClearList)
        Me.Controls.Add(Me.btnAddFolder)
        Me.Controls.Add(Me.btnAddFiles)
        Me.Controls.Add(Me.lv)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.btnSaveSSP)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(720, 500)
        Me.MinimumSize = New System.Drawing.Size(720, 500)
        Me.Name = "frmMaker"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Simple Sequence Pack Maker"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents btnSaveSSP As System.Windows.Forms.Button
    Private WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Private WithEvents lblStatus As System.Windows.Forms.Label
    Private WithEvents btnAddFiles As System.Windows.Forms.Button
    Private WithEvents btnAddFolder As System.Windows.Forms.Button
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents btnClearList As System.Windows.Forms.Button
    Private WithEvents btnRemove As System.Windows.Forms.Button
    Private WithEvents btnMoveUp As System.Windows.Forms.Button
    Private WithEvents btnMoveDown As System.Windows.Forms.Button
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents txtFileCount As System.Windows.Forms.TextBox
    Private WithEvents btnTips As System.Windows.Forms.Button
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents txtTotalSize As System.Windows.Forms.TextBox
    Private WithEvents lv As SSP.MaiListView
    Private WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Private WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Private WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Private WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
End Class
