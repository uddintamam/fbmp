<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ContohGeneralPostControl
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
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cbxCategory = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cbxConditionProd = New System.Windows.Forms.ComboBox()
        Me.btnStartProcess = New System.Windows.Forms.Button()
        Me.txtCsv = New System.Windows.Forms.TextBox()
        Me.btnImportCsv = New System.Windows.Forms.Button()
        Me.btnRefreshData = New System.Windows.Forms.Button()
        Me.cbxDataProfile = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkRunAllProfile = New System.Windows.Forms.CheckBox()
        Me.gridProfile = New System.Windows.Forms.DataGridView()
        Me.digunakanCol = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.profileChromeCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.userIdforPostCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.passwordforPostCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StartCsvCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sampaiCsvCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pesanErrorCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.userIdSuccessCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn24 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn23 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.statusCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gridReport = New System.Windows.Forms.DataGridView()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabProfile = New System.Windows.Forms.TabPage()
        Me.tabReport = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnForceClose = New System.Windows.Forms.Button()
        CType(Me.gridProfile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.tabProfile.SuspendLayout()
        Me.tabReport.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
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
        Me.lblHeader.TabIndex = 19
        Me.lblHeader.Text = "FITUR AUTO POST FBMP PRODUK CONTOH BIASA"
        Me.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(8, 19)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(117, 15)
        Me.Label19.TabIndex = 317
        Me.Label19.Text = "KATEGORI PRODUK?"
        '
        'cbxCategory
        '
        Me.cbxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxCategory.FormattingEnabled = True
        Me.cbxCategory.Items.AddRange(New Object() {"1. Peralatan", "2. Mebel", "3. Peralatan rumah tangga", "4. Kebun", "5. Perkakas", "6. Video Game", "7. Buku, Film & Musik", "8. Tas & Koper", "9.Pakaian & Sepatu Wanita", "10. Pakaian & Sepatu Pria", "11. Perhiasan & Aksesori", "12. Kesehatan & Kecantikan", "13. Kebutuhan Hewan Peliharaan", "14. Bayi & Anak-anak", "15. Mainan & Game", "16. Elektronik & Komputer", "17. Telepon seluler", "18. Sepeda", "19. Seni & Kerajinan", "20. Olahraga & Outdoor", "21. Komponen otomotif", "22. Alat Musik", "23. Barang Antik & Koleksi", "24. Cuci Gudang", "25. Lain-lain"})
        Me.cbxCategory.Location = New System.Drawing.Point(11, 37)
        Me.cbxCategory.Name = "cbxCategory"
        Me.cbxCategory.Size = New System.Drawing.Size(230, 21)
        Me.cbxCategory.TabIndex = 316
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(8, 67)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(111, 15)
        Me.Label18.TabIndex = 315
        Me.Label18.Text = "KONDISI PRODUK?"
        '
        'cbxConditionProd
        '
        Me.cbxConditionProd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxConditionProd.FormattingEnabled = True
        Me.cbxConditionProd.Items.AddRange(New Object() {"Baru", "Bekas - Seperti Baru", "Bekas - Baik", "Bekas - Cukup Baik"})
        Me.cbxConditionProd.Location = New System.Drawing.Point(11, 85)
        Me.cbxConditionProd.Name = "cbxConditionProd"
        Me.cbxConditionProd.Size = New System.Drawing.Size(230, 21)
        Me.cbxConditionProd.TabIndex = 314
        '
        'btnStartProcess
        '
        Me.btnStartProcess.BackColor = System.Drawing.Color.LightGreen
        Me.btnStartProcess.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStartProcess.ForeColor = System.Drawing.Color.Black
        Me.btnStartProcess.Location = New System.Drawing.Point(29, 368)
        Me.btnStartProcess.Name = "btnStartProcess"
        Me.btnStartProcess.Size = New System.Drawing.Size(415, 32)
        Me.btnStartProcess.TabIndex = 312
        Me.btnStartProcess.Text = "START ROBOT POST PRODUK CONTOH BIASA"
        Me.btnStartProcess.UseVisualStyleBackColor = False
        '
        'txtCsv
        '
        Me.txtCsv.Location = New System.Drawing.Point(217, 72)
        Me.txtCsv.Name = "txtCsv"
        Me.txtCsv.ReadOnly = True
        Me.txtCsv.Size = New System.Drawing.Size(268, 20)
        Me.txtCsv.TabIndex = 311
        '
        'btnImportCsv
        '
        Me.btnImportCsv.BackColor = System.Drawing.Color.LightGreen
        Me.btnImportCsv.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImportCsv.ForeColor = System.Drawing.Color.Black
        Me.btnImportCsv.Location = New System.Drawing.Point(29, 70)
        Me.btnImportCsv.Name = "btnImportCsv"
        Me.btnImportCsv.Size = New System.Drawing.Size(182, 23)
        Me.btnImportCsv.TabIndex = 310
        Me.btnImportCsv.Text = "IMPORT FILE CSV"
        Me.btnImportCsv.UseVisualStyleBackColor = False
        '
        'btnRefreshData
        '
        Me.btnRefreshData.BackColor = System.Drawing.Color.LightGreen
        Me.btnRefreshData.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefreshData.ForeColor = System.Drawing.Color.Black
        Me.btnRefreshData.Location = New System.Drawing.Point(450, 368)
        Me.btnRefreshData.Name = "btnRefreshData"
        Me.btnRefreshData.Size = New System.Drawing.Size(145, 32)
        Me.btnRefreshData.TabIndex = 310
        Me.btnRefreshData.Text = "REFRESH DATA"
        Me.btnRefreshData.UseVisualStyleBackColor = False
        '
        'cbxDataProfile
        '
        Me.cbxDataProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxDataProfile.FormattingEnabled = True
        Me.cbxDataProfile.Location = New System.Drawing.Point(29, 121)
        Me.cbxDataProfile.Name = "cbxDataProfile"
        Me.cbxDataProfile.Size = New System.Drawing.Size(208, 21)
        Me.cbxDataProfile.TabIndex = 339
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(26, 103)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 15)
        Me.Label1.TabIndex = 315
        Me.Label1.Text = "PROFIL CHROME"
        '
        'chkRunAllProfile
        '
        Me.chkRunAllProfile.AutoSize = True
        Me.chkRunAllProfile.Location = New System.Drawing.Point(243, 124)
        Me.chkRunAllProfile.Name = "chkRunAllProfile"
        Me.chkRunAllProfile.Size = New System.Drawing.Size(163, 17)
        Me.chkRunAllProfile.TabIndex = 340
        Me.chkRunAllProfile.Text = "JALANKAN SEMUA PROFIL"
        Me.chkRunAllProfile.UseVisualStyleBackColor = True
        '
        'gridProfile
        '
        Me.gridProfile.AllowUserToAddRows = False
        Me.gridProfile.AllowUserToDeleteRows = False
        Me.gridProfile.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.gridProfile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridProfile.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.digunakanCol, Me.profileChromeCol, Me.userIdforPostCol, Me.passwordforPostCol, Me.StartCsvCol, Me.sampaiCsvCol})
        Me.gridProfile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridProfile.GridColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.gridProfile.Location = New System.Drawing.Point(3, 3)
        Me.gridProfile.Name = "gridProfile"
        Me.gridProfile.Size = New System.Drawing.Size(727, 165)
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
        Me.userIdforPostCol.HeaderText = "USER ID"
        Me.userIdforPostCol.Name = "userIdforPostCol"
        Me.userIdforPostCol.Width = 220
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
        Me.StartCsvCol.Width = 140
        '
        'sampaiCsvCol
        '
        Me.sampaiCsvCol.HeaderText = "SAMPAI BARIS CSV"
        Me.sampaiCsvCol.Name = "sampaiCsvCol"
        Me.sampaiCsvCol.ReadOnly = True
        Me.sampaiCsvCol.Width = 140
        '
        'pesanErrorCol
        '
        Me.pesanErrorCol.HeaderText = "PESAN"
        Me.pesanErrorCol.Name = "pesanErrorCol"
        Me.pesanErrorCol.Width = 250
        '
        'userIdSuccessCol
        '
        Me.userIdSuccessCol.HeaderText = "USER ID"
        Me.userIdSuccessCol.Name = "userIdSuccessCol"
        '
        'DataGridViewTextBoxColumn24
        '
        Me.DataGridViewTextBoxColumn24.HeaderText = "NAMA PROFILE CHROME"
        Me.DataGridViewTextBoxColumn24.Name = "DataGridViewTextBoxColumn24"
        Me.DataGridViewTextBoxColumn24.Width = 250
        '
        'DataGridViewTextBoxColumn23
        '
        Me.DataGridViewTextBoxColumn23.HeaderText = "JUDUL"
        Me.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23"
        Me.DataGridViewTextBoxColumn23.Width = 200
        '
        'statusCol
        '
        Me.statusCol.HeaderText = "STATUS"
        Me.statusCol.Name = "statusCol"
        Me.statusCol.Width = 80
        '
        'gridReport
        '
        Me.gridReport.AllowUserToAddRows = False
        Me.gridReport.AllowUserToDeleteRows = False
        Me.gridReport.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.gridReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridReport.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.statusCol, Me.DataGridViewTextBoxColumn23, Me.DataGridViewTextBoxColumn24, Me.userIdSuccessCol, Me.pesanErrorCol})
        Me.gridReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridReport.GridColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.gridReport.Location = New System.Drawing.Point(3, 3)
        Me.gridReport.Name = "gridReport"
        Me.gridReport.Size = New System.Drawing.Size(727, 165)
        Me.gridReport.TabIndex = 319
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabProfile)
        Me.TabControl1.Controls.Add(Me.tabReport)
        Me.TabControl1.Location = New System.Drawing.Point(29, 165)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(741, 197)
        Me.TabControl1.TabIndex = 341
        '
        'tabProfile
        '
        Me.tabProfile.Controls.Add(Me.gridProfile)
        Me.tabProfile.Location = New System.Drawing.Point(4, 22)
        Me.tabProfile.Name = "tabProfile"
        Me.tabProfile.Padding = New System.Windows.Forms.Padding(3)
        Me.tabProfile.Size = New System.Drawing.Size(733, 171)
        Me.tabProfile.TabIndex = 0
        Me.tabProfile.Text = "PROFILE"
        Me.tabProfile.UseVisualStyleBackColor = True
        '
        'tabReport
        '
        Me.tabReport.Controls.Add(Me.gridReport)
        Me.tabReport.Location = New System.Drawing.Point(4, 22)
        Me.tabReport.Name = "tabReport"
        Me.tabReport.Padding = New System.Windows.Forms.Padding(3)
        Me.tabReport.Size = New System.Drawing.Size(733, 171)
        Me.tabReport.TabIndex = 1
        Me.tabReport.Text = "LAPORAN"
        Me.tabReport.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbxCategory)
        Me.GroupBox1.Controls.Add(Me.cbxConditionProd)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Location = New System.Drawing.Point(507, 53)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(259, 121)
        Me.GroupBox1.TabIndex = 342
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "KRITERIA"
        '
        'btnForceClose
        '
        Me.btnForceClose.BackColor = System.Drawing.Color.LightGreen
        Me.btnForceClose.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnForceClose.ForeColor = System.Drawing.Color.Black
        Me.btnForceClose.Location = New System.Drawing.Point(601, 368)
        Me.btnForceClose.Name = "btnForceClose"
        Me.btnForceClose.Size = New System.Drawing.Size(169, 32)
        Me.btnForceClose.TabIndex = 346
        Me.btnForceClose.Text = "TUTUP SEMUA BROWSER"
        Me.btnForceClose.UseVisualStyleBackColor = False
        '
        'ContohGeneralPostControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnForceClose)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.chkRunAllProfile)
        Me.Controls.Add(Me.cbxDataProfile)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnStartProcess)
        Me.Controls.Add(Me.txtCsv)
        Me.Controls.Add(Me.btnRefreshData)
        Me.Controls.Add(Me.btnImportCsv)
        Me.Controls.Add(Me.lblHeader)
        Me.Name = "ContohGeneralPostControl"
        Me.Size = New System.Drawing.Size(810, 418)
        CType(Me.gridProfile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.tabProfile.ResumeLayout(False)
        Me.tabReport.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cbxCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cbxConditionProd As System.Windows.Forms.ComboBox
    Friend WithEvents btnStartProcess As System.Windows.Forms.Button
    Friend WithEvents txtCsv As System.Windows.Forms.TextBox
    Friend WithEvents btnImportCsv As System.Windows.Forms.Button
    Friend WithEvents btnRefreshData As System.Windows.Forms.Button
    Friend WithEvents cbxDataProfile As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkRunAllProfile As CheckBox
    Friend WithEvents gridProfile As DataGridView
    Friend WithEvents pesanErrorCol As DataGridViewTextBoxColumn
    Friend WithEvents userIdSuccessCol As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn24 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn23 As DataGridViewTextBoxColumn
    Friend WithEvents statusCol As DataGridViewTextBoxColumn
    Friend WithEvents gridReport As DataGridView
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents tabProfile As TabPage
    Friend WithEvents tabReport As TabPage
    Friend WithEvents digunakanCol As DataGridViewCheckBoxColumn
    Friend WithEvents profileChromeCol As DataGridViewTextBoxColumn
    Friend WithEvents userIdforPostCol As DataGridViewTextBoxColumn
    Friend WithEvents passwordforPostCol As DataGridViewTextBoxColumn
    Friend WithEvents StartCsvCol As DataGridViewTextBoxColumn
    Friend WithEvents sampaiCsvCol As DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnForceClose As Button
End Class
