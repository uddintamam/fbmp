<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ReNewPostControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.gridProfile = New System.Windows.Forms.DataGridView()
        Me.digunakanCol = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.profileChromeCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.userIdforPostCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.passwordforPostCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.totalRenewCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.btnStartProcess = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnForceClose = New System.Windows.Forms.Button()
        Me.btnPause = New System.Windows.Forms.Button()
        Me.btnRefreshData = New System.Windows.Forms.Button()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.gbChoice = New System.Windows.Forms.GroupBox()
        Me.numMaxOldest = New System.Windows.Forms.NumericUpDown()
        Me.chkDraft = New System.Windows.Forms.CheckBox()
        Me.chkAgainst = New System.Windows.Forms.CheckBox()
        Me.chkOldest = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cbxDataProfile = New System.Windows.Forms.ComboBox()
        Me.chkRunAllProfile = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.gridProfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.gbChoice.SuspendLayout()
        CType(Me.numMaxOldest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblHeader
        '
        Me.lblHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(182, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.lblHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(810, 32)
        Me.lblHeader.TabIndex = 19
        Me.lblHeader.Text = "FITUR AUTO PERBAHARUI POSTINGAN"
        Me.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'gridProfile
        '
        Me.gridProfile.AllowUserToAddRows = False
        Me.gridProfile.AllowUserToDeleteRows = False
        Me.gridProfile.BackgroundColor = System.Drawing.Color.White
        Me.gridProfile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridProfile.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.digunakanCol, Me.profileChromeCol, Me.userIdforPostCol, Me.passwordforPostCol, Me.totalRenewCol})
        Me.gridProfile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridProfile.GridColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.gridProfile.Location = New System.Drawing.Point(3, 16)
        Me.gridProfile.Name = "gridProfile"
        Me.gridProfile.Size = New System.Drawing.Size(764, 194)
        Me.gridProfile.TabIndex = 318
        '
        'digunakanCol
        '
        Me.digunakanCol.HeaderText = ""
        Me.digunakanCol.Name = "digunakanCol"
        Me.digunakanCol.Width = 30
        '
        'profileChromeCol
        '
        Me.profileChromeCol.HeaderText = "PROFILE CHROME"
        Me.profileChromeCol.Name = "profileChromeCol"
        Me.profileChromeCol.ReadOnly = True
        Me.profileChromeCol.Visible = False
        Me.profileChromeCol.Width = 150
        '
        'userIdforPostCol
        '
        Me.userIdforPostCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.userIdforPostCol.HeaderText = "USER ID"
        Me.userIdforPostCol.Name = "userIdforPostCol"
        '
        'passwordforPostCol
        '
        Me.passwordforPostCol.HeaderText = "PASSWORD"
        Me.passwordforPostCol.Name = "passwordforPostCol"
        Me.passwordforPostCol.Visible = False
        '
        'totalRenewCol
        '
        Me.totalRenewCol.HeaderText = "TOTAL"
        Me.totalRenewCol.Name = "totalRenewCol"
        Me.totalRenewCol.ReadOnly = True
        Me.totalRenewCol.Width = 140
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.GroupBox2)
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Controls.Add(Me.Panel6)
        Me.Panel4.Controls.Add(Me.Panel3)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 32)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(20)
        Me.Panel4.Size = New System.Drawing.Size(810, 399)
        Me.Panel4.TabIndex = 363
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.gridProfile)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(20, 70)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(770, 213)
        Me.GroupBox2.TabIndex = 363
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Detail"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.btnStartProcess)
        Me.Panel5.Controls.Add(Me.GroupBox1)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(20, 283)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(770, 55)
        Me.Panel5.TabIndex = 360
        '
        'btnStartProcess
        '
        Me.btnStartProcess.BackColor = System.Drawing.Color.Cyan
        Me.btnStartProcess.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStartProcess.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnStartProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStartProcess.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStartProcess.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.btnStartProcess.Image = Global.Facebook_Onetobot_All_Fitur.My.Resources.Resources.shuttle_32
        Me.btnStartProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnStartProcess.Location = New System.Drawing.Point(447, 0)
        Me.btnStartProcess.Name = "btnStartProcess"
        Me.btnStartProcess.Padding = New System.Windows.Forms.Padding(20, 0, 20, 0)
        Me.btnStartProcess.Size = New System.Drawing.Size(323, 55)
        Me.btnStartProcess.TabIndex = 313
        Me.btnStartProcess.Text = "          START ROBOT"
        Me.btnStartProcess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnStartProcess.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnForceClose)
        Me.GroupBox1.Controls.Add(Me.btnPause)
        Me.GroupBox1.Controls.Add(Me.btnRefreshData)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(396, 55)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Opsi"
        '
        'btnForceClose
        '
        Me.btnForceClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnForceClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnForceClose.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnForceClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnForceClose.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.btnForceClose.ForeColor = System.Drawing.Color.White
        Me.btnForceClose.Image = Global.Facebook_Onetobot_All_Fitur.My.Resources.Resources.delete_16
        Me.btnForceClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnForceClose.Location = New System.Drawing.Point(227, 16)
        Me.btnForceClose.Name = "btnForceClose"
        Me.btnForceClose.Size = New System.Drawing.Size(166, 36)
        Me.btnForceClose.TabIndex = 348
        Me.btnForceClose.Text = "Tutup Semua Browser"
        Me.btnForceClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnForceClose.UseVisualStyleBackColor = False
        '
        'btnPause
        '
        Me.btnPause.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.btnPause.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPause.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPause.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.btnPause.ForeColor = System.Drawing.Color.White
        Me.btnPause.Image = Global.Facebook_Onetobot_All_Fitur.My.Resources.Resources.pause_16
        Me.btnPause.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPause.Location = New System.Drawing.Point(115, 16)
        Me.btnPause.Name = "btnPause"
        Me.btnPause.Padding = New System.Windows.Forms.Padding(15, 0, 15, 0)
        Me.btnPause.Size = New System.Drawing.Size(112, 36)
        Me.btnPause.TabIndex = 321
        Me.btnPause.Text = "Pause"
        Me.btnPause.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPause.UseVisualStyleBackColor = False
        '
        'btnRefreshData
        '
        Me.btnRefreshData.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.btnRefreshData.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefreshData.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnRefreshData.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefreshData.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.btnRefreshData.ForeColor = System.Drawing.Color.White
        Me.btnRefreshData.Image = Global.Facebook_Onetobot_All_Fitur.My.Resources.Resources.rotate_16
        Me.btnRefreshData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefreshData.Location = New System.Drawing.Point(3, 16)
        Me.btnRefreshData.Name = "btnRefreshData"
        Me.btnRefreshData.Size = New System.Drawing.Size(112, 36)
        Me.btnRefreshData.TabIndex = 320
        Me.btnRefreshData.Text = "Refresh Data"
        Me.btnRefreshData.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRefreshData.UseVisualStyleBackColor = False
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.GroupBox3)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel6.Location = New System.Drawing.Point(20, 338)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(770, 41)
        Me.Panel6.TabIndex = 362
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.LinkLabel1)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(770, 41)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Bantuan"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.Location = New System.Drawing.Point(6, 16)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(186, 16)
        Me.LinkLabel1.TabIndex = 3
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Video Tutorial Jalankan Posting"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel2)
        Me.Panel3.Controls.Add(Me.Panel1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(20, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 0, 0, 10)
        Me.Panel3.Size = New System.Drawing.Size(770, 50)
        Me.Panel3.TabIndex = 359
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.gbChoice)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(396, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.Panel2.Size = New System.Drawing.Size(374, 40)
        Me.Panel2.TabIndex = 358
        '
        'gbChoice
        '
        Me.gbChoice.Controls.Add(Me.numMaxOldest)
        Me.gbChoice.Controls.Add(Me.chkDraft)
        Me.gbChoice.Controls.Add(Me.chkAgainst)
        Me.gbChoice.Controls.Add(Me.chkOldest)
        Me.gbChoice.Dock = System.Windows.Forms.DockStyle.Left
        Me.gbChoice.Location = New System.Drawing.Point(10, 0)
        Me.gbChoice.Name = "gbChoice"
        Me.gbChoice.Size = New System.Drawing.Size(344, 40)
        Me.gbChoice.TabIndex = 0
        Me.gbChoice.TabStop = False
        Me.gbChoice.Text = "Pilihan"
        '
        'numMaxOldest
        '
        Me.numMaxOldest.Enabled = False
        Me.numMaxOldest.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.numMaxOldest.Location = New System.Drawing.Point(91, 11)
        Me.numMaxOldest.Name = "numMaxOldest"
        Me.numMaxOldest.Size = New System.Drawing.Size(60, 23)
        Me.numMaxOldest.TabIndex = 357
        '
        'chkDraft
        '
        Me.chkDraft.AutoSize = True
        Me.chkDraft.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.chkDraft.Location = New System.Drawing.Point(266, 15)
        Me.chkDraft.Name = "chkDraft"
        Me.chkDraft.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.chkDraft.Size = New System.Drawing.Size(61, 20)
        Me.chkDraft.TabIndex = 356
        Me.chkDraft.Text = "Draf"
        Me.chkDraft.UseVisualStyleBackColor = True
        '
        'chkAgainst
        '
        Me.chkAgainst.AutoSize = True
        Me.chkAgainst.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.chkAgainst.Location = New System.Drawing.Point(163, 14)
        Me.chkAgainst.Name = "chkAgainst"
        Me.chkAgainst.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.chkAgainst.Size = New System.Drawing.Size(97, 20)
        Me.chkAgainst.TabIndex = 356
        Me.chkAgainst.Text = "Melanggar"
        Me.chkAgainst.UseVisualStyleBackColor = True
        '
        'chkOldest
        '
        Me.chkOldest.AutoSize = True
        Me.chkOldest.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.chkOldest.Location = New System.Drawing.Point(6, 15)
        Me.chkOldest.Name = "chkOldest"
        Me.chkOldest.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.chkOldest.Size = New System.Drawing.Size(85, 20)
        Me.chkOldest.TabIndex = 356
        Me.chkOldest.Text = "Terlama"
        Me.chkOldest.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cbxDataProfile)
        Me.Panel1.Controls.Add(Me.chkRunAllProfile)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.Panel1.Size = New System.Drawing.Size(396, 40)
        Me.Panel1.TabIndex = 357
        '
        'cbxDataProfile
        '
        Me.cbxDataProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxDataProfile.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbxDataProfile.FormattingEnabled = True
        Me.cbxDataProfile.Location = New System.Drawing.Point(0, 16)
        Me.cbxDataProfile.Name = "cbxDataProfile"
        Me.cbxDataProfile.Size = New System.Drawing.Size(246, 24)
        Me.cbxDataProfile.TabIndex = 343
        '
        'chkRunAllProfile
        '
        Me.chkRunAllProfile.AutoSize = True
        Me.chkRunAllProfile.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.chkRunAllProfile.Location = New System.Drawing.Point(250, 18)
        Me.chkRunAllProfile.Name = "chkRunAllProfile"
        Me.chkRunAllProfile.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.chkRunAllProfile.Size = New System.Drawing.Size(140, 20)
        Me.chkRunAllProfile.TabIndex = 356
        Me.chkRunAllProfile.Text = "Jalankan Bersama"
        Me.chkRunAllProfile.UseVisualStyleBackColor = True
        Me.chkRunAllProfile.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 16)
        Me.Label3.TabIndex = 342
        Me.Label3.Text = "Profil"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(182, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.btnClose.ForeColor = System.Drawing.Color.Transparent
        Me.btnClose.Image = Global.Facebook_Onetobot_All_Fitur.My.Resources.Resources.delete_16
        Me.btnClose.Location = New System.Drawing.Point(755, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(55, 32)
        Me.btnClose.TabIndex = 370
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "PROFILE CHROME"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        Me.DataGridViewTextBoxColumn1.Width = 150
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.HeaderText = "USER ID"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "PASSWORD"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Visible = False
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "STATUS"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 140
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "TOTAL DIPERBAHARUI"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 140
        '
        'ReNewPostControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.lblHeader)
        Me.Name = "ReNewPostControl"
        Me.Size = New System.Drawing.Size(810, 431)
        CType(Me.gridProfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.gbChoice.ResumeLayout(False)
        Me.gbChoice.PerformLayout()
        CType(Me.numMaxOldest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents gridProfile As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents digunakanCol As DataGridViewCheckBoxColumn
    Friend WithEvents profileChromeCol As DataGridViewTextBoxColumn
    Friend WithEvents userIdforPostCol As DataGridViewTextBoxColumn
    Friend WithEvents passwordforPostCol As DataGridViewTextBoxColumn
    Friend WithEvents totalRenewCol As DataGridViewTextBoxColumn
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents btnStartProcess As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnForceClose As Button
    Friend WithEvents btnPause As Button
    Friend WithEvents btnRefreshData As Button
    Friend WithEvents Panel6 As Panel
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents cbxDataProfile As ComboBox
    Friend WithEvents chkRunAllProfile As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents btnClose As Button
    Friend WithEvents gbChoice As GroupBox
    Friend WithEvents chkOldest As CheckBox
    Friend WithEvents chkAgainst As CheckBox
    Friend WithEvents chkDraft As CheckBox
    Friend WithEvents numMaxOldest As NumericUpDown
End Class
