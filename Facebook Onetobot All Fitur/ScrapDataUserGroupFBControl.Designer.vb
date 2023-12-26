<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScrapDataUserGroupFBControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.gridReport = New System.Windows.Forms.DataGridView()
        Me.userIdSuccessCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NamaUserCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gridProfile = New System.Windows.Forms.DataGridView()
        Me.digunakanCol = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.profileChromeCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.userIdforPostCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.passwordforPostCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StartCsvCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sampaiCsvCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnStartProcess = New System.Windows.Forms.Button()
        Me.txtCsv = New System.Windows.Forms.TextBox()
        Me.btnImportCsv = New System.Windows.Forms.Button()
        Me.btnRefreshData = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.btnSimpanCsv = New System.Windows.Forms.Button()
        Me.cbxDataProfile = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkRunAllProfile = New System.Windows.Forms.CheckBox()
        CType(Me.gridReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridProfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gridReport
        '
        Me.gridReport.AllowUserToAddRows = False
        Me.gridReport.AllowUserToDeleteRows = False
        Me.gridReport.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.gridReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridReport.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.userIdSuccessCol, Me.NamaUserCol})
        Me.gridReport.GridColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.gridReport.Location = New System.Drawing.Point(469, 66)
        Me.gridReport.Name = "gridReport"
        Me.gridReport.Size = New System.Drawing.Size(331, 349)
        Me.gridReport.TabIndex = 319
        '
        'userIdSuccessCol
        '
        Me.userIdSuccessCol.HeaderText = "URL USER"
        Me.userIdSuccessCol.Name = "userIdSuccessCol"
        Me.userIdSuccessCol.Width = 120
        '
        'NamaUserCol
        '
        Me.NamaUserCol.HeaderText = "NAMA"
        Me.NamaUserCol.Name = "NamaUserCol"
        Me.NamaUserCol.Width = 160
        '
        'gridProfile
        '
        Me.gridProfile.AllowUserToAddRows = False
        Me.gridProfile.AllowUserToDeleteRows = False
        Me.gridProfile.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.gridProfile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridProfile.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.digunakanCol, Me.profileChromeCol, Me.userIdforPostCol, Me.passwordforPostCol, Me.StartCsvCol, Me.sampaiCsvCol})
        Me.gridProfile.GridColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.gridProfile.Location = New System.Drawing.Point(7, 139)
        Me.gridProfile.Name = "gridProfile"
        Me.gridProfile.Size = New System.Drawing.Size(446, 155)
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
        '
        'userIdforPostCol
        '
        Me.userIdforPostCol.HeaderText = "USER ID"
        Me.userIdforPostCol.Name = "userIdforPostCol"
        Me.userIdforPostCol.Width = 150
        '
        'passwordforPostCol
        '
        Me.passwordforPostCol.HeaderText = "PASSWORD"
        Me.passwordforPostCol.Name = "passwordforPostCol"
        Me.passwordforPostCol.Visible = False
        '
        'StartCsvCol
        '
        Me.StartCsvCol.HeaderText = "START BARIS CSV"
        Me.StartCsvCol.Name = "StartCsvCol"
        Me.StartCsvCol.ReadOnly = True
        '
        'sampaiCsvCol
        '
        Me.sampaiCsvCol.HeaderText = "SAMPAI BARIS CSV"
        Me.sampaiCsvCol.Name = "sampaiCsvCol"
        Me.sampaiCsvCol.ReadOnly = True
        '
        'btnStartProcess
        '
        Me.btnStartProcess.BackColor = System.Drawing.Color.LightGreen
        Me.btnStartProcess.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnStartProcess.ForeColor = System.Drawing.Color.Black
        Me.btnStartProcess.Location = New System.Drawing.Point(7, 300)
        Me.btnStartProcess.Name = "btnStartProcess"
        Me.btnStartProcess.Size = New System.Drawing.Size(317, 32)
        Me.btnStartProcess.TabIndex = 312
        Me.btnStartProcess.Text = "START ROBOT SCRAP DATA MEMBER GROUP"
        Me.btnStartProcess.UseVisualStyleBackColor = False
        '
        'txtCsv
        '
        Me.txtCsv.Location = New System.Drawing.Point(149, 66)
        Me.txtCsv.Name = "txtCsv"
        Me.txtCsv.ReadOnly = True
        Me.txtCsv.Size = New System.Drawing.Size(196, 20)
        Me.txtCsv.TabIndex = 311
        '
        'btnImportCsv
        '
        Me.btnImportCsv.BackColor = System.Drawing.Color.LightGreen
        Me.btnImportCsv.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImportCsv.ForeColor = System.Drawing.Color.Black
        Me.btnImportCsv.Location = New System.Drawing.Point(20, 63)
        Me.btnImportCsv.Name = "btnImportCsv"
        Me.btnImportCsv.Size = New System.Drawing.Size(123, 23)
        Me.btnImportCsv.TabIndex = 310
        Me.btnImportCsv.Text = "IMPORT FILE CSV"
        Me.btnImportCsv.UseVisualStyleBackColor = False
        '
        'btnRefreshData
        '
        Me.btnRefreshData.BackColor = System.Drawing.Color.LightGreen
        Me.btnRefreshData.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefreshData.ForeColor = System.Drawing.Color.Black
        Me.btnRefreshData.Location = New System.Drawing.Point(330, 300)
        Me.btnRefreshData.Name = "btnRefreshData"
        Me.btnRefreshData.Size = New System.Drawing.Size(123, 32)
        Me.btnRefreshData.TabIndex = 310
        Me.btnRefreshData.Text = "REFRESH DATA"
        Me.btnRefreshData.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI Semibold", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(810, 50)
        Me.lblHeader.TabIndex = 320
        Me.lblHeader.Text = "FITUR SCRAP DATA MEMBER GROUP"
        Me.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnSimpanCsv
        '
        Me.btnSimpanCsv.BackColor = System.Drawing.Color.LightGreen
        Me.btnSimpanCsv.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSimpanCsv.ForeColor = System.Drawing.Color.Black
        Me.btnSimpanCsv.Location = New System.Drawing.Point(7, 338)
        Me.btnSimpanCsv.Name = "btnSimpanCsv"
        Me.btnSimpanCsv.Size = New System.Drawing.Size(446, 32)
        Me.btnSimpanCsv.TabIndex = 312
        Me.btnSimpanCsv.Text = "DOWNLOAD CSV"
        Me.btnSimpanCsv.UseVisualStyleBackColor = False
        '
        'cbxDataProfile
        '
        Me.cbxDataProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxDataProfile.FormattingEnabled = True
        Me.cbxDataProfile.Location = New System.Drawing.Point(20, 112)
        Me.cbxDataProfile.Name = "cbxDataProfile"
        Me.cbxDataProfile.Size = New System.Drawing.Size(264, 21)
        Me.cbxDataProfile.TabIndex = 345
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(18, 94)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 15)
        Me.Label1.TabIndex = 344
        Me.Label1.Text = "PROFILE CHROME"
        '
        'chkRunAllProfile
        '
        Me.chkRunAllProfile.AutoSize = True
        Me.chkRunAllProfile.Location = New System.Drawing.Point(290, 115)
        Me.chkRunAllProfile.Name = "chkRunAllProfile"
        Me.chkRunAllProfile.Size = New System.Drawing.Size(163, 17)
        Me.chkRunAllProfile.TabIndex = 346
        Me.chkRunAllProfile.Text = "JALANKAN SEMUA PROFIL"
        Me.chkRunAllProfile.UseVisualStyleBackColor = True
        '
        'ScrapDataUserGroupFBControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.chkRunAllProfile)
        Me.Controls.Add(Me.cbxDataProfile)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblHeader)
        Me.Controls.Add(Me.gridReport)
        Me.Controls.Add(Me.gridProfile)
        Me.Controls.Add(Me.btnSimpanCsv)
        Me.Controls.Add(Me.btnStartProcess)
        Me.Controls.Add(Me.txtCsv)
        Me.Controls.Add(Me.btnRefreshData)
        Me.Controls.Add(Me.btnImportCsv)
        Me.Name = "ScrapDataUserGroupFBControl"
        Me.Size = New System.Drawing.Size(810, 418)
        CType(Me.gridReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridProfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gridReport As System.Windows.Forms.DataGridView
    Friend WithEvents gridProfile As System.Windows.Forms.DataGridView
    Friend WithEvents btnStartProcess As System.Windows.Forms.Button
    Friend WithEvents txtCsv As System.Windows.Forms.TextBox
    Friend WithEvents btnImportCsv As System.Windows.Forms.Button
    Friend WithEvents btnRefreshData As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents btnSimpanCsv As System.Windows.Forms.Button
    Friend WithEvents userIdSuccessCol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NamaUserCol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cbxDataProfile As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents digunakanCol As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents profileChromeCol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents userIdforPostCol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents passwordforPostCol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StartCsvCol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sampaiCsvCol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkRunAllProfile As CheckBox
End Class
