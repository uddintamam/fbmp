﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PostFBMobilControl
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
        Me.gridReport = New System.Windows.Forms.DataGridView()
        Me.statusCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn23 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn24 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.userIdSuccessCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pesanErrorCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gridProfile = New System.Windows.Forms.DataGridView()
        Me.digunakanCol = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.profileChromeCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.userIdforPostCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.passwordforPostCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StartCsvCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sampaiCsvCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cbxTahun = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cbxMerek = New System.Windows.Forms.ComboBox()
        Me.cbxBody = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbxColor = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbxCondition = New System.Windows.Forms.ComboBox()
        Me.cbxFuel = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbxColor2 = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkTdkMasalah = New System.Windows.Forms.CheckBox()
        Me.cbxTransmission = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabProfile = New System.Windows.Forms.TabPage()
        Me.tabReport = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtCsv = New System.Windows.Forms.TextBox()
        Me.btnImportCsv = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.cbxDataProfile = New System.Windows.Forms.ComboBox()
        Me.chkRunAllProfile = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.btnStartProcess = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnForceClose = New System.Windows.Forms.Button()
        Me.btnPause = New System.Windows.Forms.Button()
        Me.btnRefreshData = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.gridReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridProfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.tabProfile.SuspendLayout()
        Me.tabReport.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblHeader
        '
        Me.lblHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(182, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.lblHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI Semibold", 16.75!, System.Drawing.FontStyle.Bold)
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(801, 33)
        Me.lblHeader.TabIndex = 19
        Me.lblHeader.Text = "FITUR AUTO POST FBMP MOBIL"
        Me.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'gridReport
        '
        Me.gridReport.AllowUserToAddRows = False
        Me.gridReport.AllowUserToDeleteRows = False
        Me.gridReport.BackgroundColor = System.Drawing.Color.White
        Me.gridReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridReport.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.statusCol, Me.DataGridViewTextBoxColumn23, Me.DataGridViewTextBoxColumn24, Me.userIdSuccessCol, Me.pesanErrorCol})
        Me.gridReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridReport.GridColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.gridReport.Location = New System.Drawing.Point(3, 3)
        Me.gridReport.Name = "gridReport"
        Me.gridReport.Size = New System.Drawing.Size(747, 73)
        Me.gridReport.TabIndex = 319
        '
        'statusCol
        '
        Me.statusCol.HeaderText = "STATUS"
        Me.statusCol.Name = "statusCol"
        Me.statusCol.Width = 80
        '
        'DataGridViewTextBoxColumn23
        '
        Me.DataGridViewTextBoxColumn23.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn23.HeaderText = "JUDUL"
        Me.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23"
        '
        'DataGridViewTextBoxColumn24
        '
        Me.DataGridViewTextBoxColumn24.HeaderText = "NAMA PROFILE CHROME"
        Me.DataGridViewTextBoxColumn24.Name = "DataGridViewTextBoxColumn24"
        Me.DataGridViewTextBoxColumn24.Visible = False
        Me.DataGridViewTextBoxColumn24.Width = 250
        '
        'userIdSuccessCol
        '
        Me.userIdSuccessCol.HeaderText = "USER ID"
        Me.userIdSuccessCol.Name = "userIdSuccessCol"
        '
        'pesanErrorCol
        '
        Me.pesanErrorCol.HeaderText = "PESAN"
        Me.pesanErrorCol.Name = "pesanErrorCol"
        Me.pesanErrorCol.Width = 250
        '
        'gridProfile
        '
        Me.gridProfile.AllowUserToAddRows = False
        Me.gridProfile.AllowUserToDeleteRows = False
        Me.gridProfile.BackgroundColor = System.Drawing.Color.White
        Me.gridProfile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridProfile.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.digunakanCol, Me.profileChromeCol, Me.userIdforPostCol, Me.passwordforPostCol, Me.StartCsvCol, Me.sampaiCsvCol})
        Me.gridProfile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridProfile.GridColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.gridProfile.Location = New System.Drawing.Point(3, 3)
        Me.gridProfile.Name = "gridProfile"
        Me.gridProfile.Size = New System.Drawing.Size(747, 73)
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
        'StartCsvCol
        '
        Me.StartCsvCol.HeaderText = "START BARIS CSV"
        Me.StartCsvCol.Name = "StartCsvCol"
        Me.StartCsvCol.ReadOnly = True
        Me.StartCsvCol.Width = 150
        '
        'sampaiCsvCol
        '
        Me.sampaiCsvCol.HeaderText = "SAMPAI BARIS CSV"
        Me.sampaiCsvCol.Name = "sampaiCsvCol"
        Me.sampaiCsvCol.ReadOnly = True
        Me.sampaiCsvCol.Width = 150
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label19.Location = New System.Drawing.Point(12, 25)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(44, 16)
        Me.Label19.TabIndex = 327
        Me.Label19.Text = "Tahun"
        '
        'cbxTahun
        '
        Me.cbxTahun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxTahun.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.cbxTahun.FormattingEnabled = True
        Me.cbxTahun.Items.AddRange(New Object() {"2024", "2023", "2022", "2021", "2020", "2019", "2018", "2017", "2016", "2015", "2014", "2013", "2012", "2011", "2010", "2009", "2008", "2007", "2006", "2005", "2004", "2003", "2002", "2001", "2000", "1999", "1998", "1997", "1996", "1995", "1994", "1993", "1992", "1991", "1990", "1989", "1988", "1987", "1986", "1985", "1984", "1983", "1982", "1981", "1980", "1979", "1978", "1977", "1976", "1975", "1974", "1973", "1972", "1971", "1970", "1969", "1968", "1967", "1966", "1965", "1964", "1963", "1962", "1961", "1960", "1959", "1958", "1957", "1956", "1955", "1954", "1953", "1952", "1951", "1950"})
        Me.cbxTahun.Location = New System.Drawing.Point(15, 43)
        Me.cbxTahun.Name = "cbxTahun"
        Me.cbxTahun.Size = New System.Drawing.Size(122, 24)
        Me.cbxTahun.TabIndex = 326
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label18.Location = New System.Drawing.Point(12, 72)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(43, 16)
        Me.Label18.TabIndex = 325
        Me.Label18.Text = "Merek"
        '
        'cbxMerek
        '
        Me.cbxMerek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxMerek.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.cbxMerek.FormattingEnabled = True
        Me.cbxMerek.Items.AddRange(New Object() {"Acura", "Aston Martin", "Alfa Romeo", "Audi", "BMW", "Chery", "Chevrolet", "Chrysler", "DFSK", "Daihatsu", "Datsun", "Dodge", "Ferrari", "FIAT", "Ford", "Geely", "HINO", "Honda", "Hummer", "Hyundai", "INFINITI", "Isuzu", "Jaguar", "Jeep", "Kia", "Lamborghini", "Land Rover", "Lexus", "MINI", "Maserati", "Mazda", "McLaren", "Mercedes-Benz", "Mitsubishi", "Nissan", "Opel", "Peugeot", "Piaggio", "Porsche", "Proton", "Renault", "Smart", "Subaru", "Suzuki", "Tata", "Timor", "Toyota", "Volvo", "Wuling"})
        Me.cbxMerek.Location = New System.Drawing.Point(15, 90)
        Me.cbxMerek.Name = "cbxMerek"
        Me.cbxMerek.Size = New System.Drawing.Size(122, 24)
        Me.cbxMerek.TabIndex = 324
        '
        'cbxBody
        '
        Me.cbxBody.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxBody.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.cbxBody.FormattingEnabled = True
        Me.cbxBody.Items.AddRange(New Object() {"Coupe", "Truk", "Sedan", "Hatchback", "SUV", "Convertible", "Wagon", "Minivan", "Mobil Kecil", "Lainnya"})
        Me.cbxBody.Location = New System.Drawing.Point(15, 138)
        Me.cbxBody.Name = "cbxBody"
        Me.cbxBody.Size = New System.Drawing.Size(122, 24)
        Me.cbxBody.TabIndex = 326
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label2.Location = New System.Drawing.Point(12, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 16)
        Me.Label2.TabIndex = 327
        Me.Label2.Text = "Bodi Mobil"
        '
        'cbxColor
        '
        Me.cbxColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxColor.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.cbxColor.FormattingEnabled = True
        Me.cbxColor.Items.AddRange(New Object() {"Hitam", "Biru", "Cokelat", "Emas", "Hijau", "Abu-abu", "Merah muda", "Ungu", "Merah", "Silver", "Oranye", "Putih", "Kuning", "Charcoal", "Off white", "Cokelat Muda", "Krem", "Burgundy", "Turkis", "Lainnya"})
        Me.cbxColor.Location = New System.Drawing.Point(146, 43)
        Me.cbxColor.Name = "cbxColor"
        Me.cbxColor.Size = New System.Drawing.Size(122, 24)
        Me.cbxColor.TabIndex = 326
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label3.Location = New System.Drawing.Point(143, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(94, 16)
        Me.Label3.TabIndex = 327
        Me.Label3.Text = "Warna Exterior"
        '
        'cbxCondition
        '
        Me.cbxCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxCondition.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.cbxCondition.FormattingEnabled = True
        Me.cbxCondition.Items.AddRange(New Object() {"Luar biasa baik", "Sangat baik", "Baik", "Cukup", "Buruk"})
        Me.cbxCondition.Location = New System.Drawing.Point(147, 138)
        Me.cbxCondition.Name = "cbxCondition"
        Me.cbxCondition.Size = New System.Drawing.Size(121, 24)
        Me.cbxCondition.TabIndex = 326
        '
        'cbxFuel
        '
        Me.cbxFuel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxFuel.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.cbxFuel.FormattingEnabled = True
        Me.cbxFuel.Items.AddRange(New Object() {"Diesel", "Listrik", "Bensin", "Fleksibel", "Hybrid", "Bensin", "Plug-in hybrid", "Lainnya"})
        Me.cbxFuel.Location = New System.Drawing.Point(277, 43)
        Me.cbxFuel.Name = "cbxFuel"
        Me.cbxFuel.Size = New System.Drawing.Size(123, 24)
        Me.cbxFuel.TabIndex = 326
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label4.Location = New System.Drawing.Point(144, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 16)
        Me.Label4.TabIndex = 327
        Me.Label4.Text = "Kondisi Mobil"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label5.Location = New System.Drawing.Point(274, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 16)
        Me.Label5.TabIndex = 327
        Me.Label5.Text = "Bahan Bakar"
        '
        'cbxColor2
        '
        Me.cbxColor2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxColor2.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.cbxColor2.FormattingEnabled = True
        Me.cbxColor2.Items.AddRange(New Object() {"Hitam", "Biru", "Cokelat", "Emas", "Hijau", "Abu-abu", "Merah muda", "Ungu", "Merah", "Silver", "Oranye", "Putih", "Kuning", "Charcoal", "Off white", "Cokelat Muda", "Krem", "Burgundy", "Turkis", "Lainnya"})
        Me.cbxColor2.Location = New System.Drawing.Point(146, 90)
        Me.cbxColor2.Name = "cbxColor2"
        Me.cbxColor2.Size = New System.Drawing.Size(122, 24)
        Me.cbxColor2.TabIndex = 326
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label6.Location = New System.Drawing.Point(143, 72)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(92, 16)
        Me.Label6.TabIndex = 327
        Me.Label6.Text = "Warna Interior"
        '
        'chkTdkMasalah
        '
        Me.chkTdkMasalah.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.chkTdkMasalah.Location = New System.Drawing.Point(277, 120)
        Me.chkTdkMasalah.Name = "chkTdkMasalah"
        Me.chkTdkMasalah.Size = New System.Drawing.Size(123, 44)
        Me.chkTdkMasalah.TabIndex = 342
        Me.chkTdkMasalah.Text = "Tidak Memiliki Masalah"
        Me.chkTdkMasalah.UseVisualStyleBackColor = True
        '
        'cbxTransmission
        '
        Me.cbxTransmission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxTransmission.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.cbxTransmission.FormattingEnabled = True
        Me.cbxTransmission.Items.AddRange(New Object() {"Transmisi Manual", "Transmisi Otomatis"})
        Me.cbxTransmission.Location = New System.Drawing.Point(277, 90)
        Me.cbxTransmission.Name = "cbxTransmission"
        Me.cbxTransmission.Size = New System.Drawing.Size(123, 24)
        Me.cbxTransmission.TabIndex = 326
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label7.Location = New System.Drawing.Point(274, 72)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 16)
        Me.Label7.TabIndex = 327
        Me.Label7.Text = "Transmisi"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabProfile)
        Me.TabControl1.Controls.Add(Me.tabReport)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(20, 192)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(761, 105)
        Me.TabControl1.TabIndex = 344
        '
        'tabProfile
        '
        Me.tabProfile.Controls.Add(Me.gridProfile)
        Me.tabProfile.Location = New System.Drawing.Point(4, 22)
        Me.tabProfile.Name = "tabProfile"
        Me.tabProfile.Padding = New System.Windows.Forms.Padding(3)
        Me.tabProfile.Size = New System.Drawing.Size(753, 79)
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
        Me.tabReport.Size = New System.Drawing.Size(753, 79)
        Me.tabReport.TabIndex = 1
        Me.tabReport.Text = "LAPORAN"
        Me.tabReport.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.cbxMerek)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.chkTdkMasalah)
        Me.GroupBox1.Controls.Add(Me.cbxTahun)
        Me.GroupBox1.Controls.Add(Me.cbxBody)
        Me.GroupBox1.Controls.Add(Me.cbxCondition)
        Me.GroupBox1.Controls.Add(Me.cbxColor)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.cbxColor2)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cbxFuel)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cbxTransmission)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox1.Location = New System.Drawing.Point(360, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(409, 172)
        Me.GroupBox1.TabIndex = 345
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Kriteria"
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
        Me.btnClose.Location = New System.Drawing.Point(746, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(55, 32)
        Me.btnClose.TabIndex = 365
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.TabControl1)
        Me.Panel4.Controls.Add(Me.Panel9)
        Me.Panel4.Controls.Add(Me.Panel6)
        Me.Panel4.Controls.Add(Me.Panel7)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 33)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(20)
        Me.Panel4.Size = New System.Drawing.Size(801, 413)
        Me.Panel4.TabIndex = 366
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.GroupBox1)
        Me.Panel9.Controls.Add(Me.GroupBox4)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(20, 20)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(761, 172)
        Me.Panel9.TabIndex = 363
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Panel3)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox4.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(360, 172)
        Me.GroupBox4.TabIndex = 364
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Masukan"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel2)
        Me.Panel3.Controls.Add(Me.Panel5)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(3, 16)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 0, 0, 10)
        Me.Panel3.Size = New System.Drawing.Size(354, 153)
        Me.Panel3.TabIndex = 359
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Controls.Add(Me.btnImportCsv)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.Panel2.Size = New System.Drawing.Size(354, 34)
        Me.Panel2.TabIndex = 358
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtCsv)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(10, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 10, 10, 10)
        Me.Panel1.Size = New System.Drawing.Size(202, 34)
        Me.Panel1.TabIndex = 363
        '
        'txtCsv
        '
        Me.txtCsv.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCsv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCsv.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.txtCsv.Location = New System.Drawing.Point(0, 10)
        Me.txtCsv.Name = "txtCsv"
        Me.txtCsv.ReadOnly = True
        Me.txtCsv.Size = New System.Drawing.Size(192, 16)
        Me.txtCsv.TabIndex = 363
        '
        'btnImportCsv
        '
        Me.btnImportCsv.BackColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(182, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnImportCsv.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnImportCsv.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnImportCsv.FlatAppearance.BorderSize = 0
        Me.btnImportCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImportCsv.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.btnImportCsv.ForeColor = System.Drawing.Color.White
        Me.btnImportCsv.Image = Global.Facebook_Onetobot_All_Fitur.My.Resources.Resources.folder_16
        Me.btnImportCsv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImportCsv.Location = New System.Drawing.Point(212, 0)
        Me.btnImportCsv.Name = "btnImportCsv"
        Me.btnImportCsv.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.btnImportCsv.Size = New System.Drawing.Size(132, 34)
        Me.btnImportCsv.TabIndex = 362
        Me.btnImportCsv.Text = "Buka File CSV"
        Me.btnImportCsv.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnImportCsv.UseVisualStyleBackColor = False
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.cbxDataProfile)
        Me.Panel5.Controls.Add(Me.chkRunAllProfile)
        Me.Panel5.Controls.Add(Me.Label10)
        Me.Panel5.Location = New System.Drawing.Point(9, 40)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.Panel5.Size = New System.Drawing.Size(389, 40)
        Me.Panel5.TabIndex = 357
        '
        'cbxDataProfile
        '
        Me.cbxDataProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxDataProfile.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbxDataProfile.FormattingEnabled = True
        Me.cbxDataProfile.Location = New System.Drawing.Point(0, 16)
        Me.cbxDataProfile.Name = "cbxDataProfile"
        Me.cbxDataProfile.Size = New System.Drawing.Size(203, 24)
        Me.cbxDataProfile.TabIndex = 343
        '
        'chkRunAllProfile
        '
        Me.chkRunAllProfile.AutoSize = True
        Me.chkRunAllProfile.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.chkRunAllProfile.Location = New System.Drawing.Point(203, 18)
        Me.chkRunAllProfile.Name = "chkRunAllProfile"
        Me.chkRunAllProfile.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.chkRunAllProfile.Size = New System.Drawing.Size(140, 20)
        Me.chkRunAllProfile.TabIndex = 356
        Me.chkRunAllProfile.Text = "Jalankan Bersama"
        Me.chkRunAllProfile.UseVisualStyleBackColor = True
        Me.chkRunAllProfile.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(37, 16)
        Me.Label10.TabIndex = 342
        Me.Label10.Text = "Profil"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.btnStartProcess)
        Me.Panel6.Controls.Add(Me.GroupBox3)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel6.Location = New System.Drawing.Point(20, 297)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(761, 55)
        Me.Panel6.TabIndex = 360
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
        Me.btnStartProcess.Location = New System.Drawing.Point(461, 0)
        Me.btnStartProcess.Name = "btnStartProcess"
        Me.btnStartProcess.Padding = New System.Windows.Forms.Padding(20, 0, 20, 0)
        Me.btnStartProcess.Size = New System.Drawing.Size(300, 55)
        Me.btnStartProcess.TabIndex = 313
        Me.btnStartProcess.Text = "          START ROBOT POST FBMP"
        Me.btnStartProcess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnStartProcess.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnForceClose)
        Me.GroupBox3.Controls.Add(Me.btnPause)
        Me.GroupBox3.Controls.Add(Me.btnRefreshData)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(396, 55)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Opsi"
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
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.GroupBox5)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel7.Location = New System.Drawing.Point(20, 352)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(761, 41)
        Me.Panel7.TabIndex = 362
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.LinkLabel2)
        Me.GroupBox5.Controls.Add(Me.LinkLabel3)
        Me.GroupBox5.Controls.Add(Me.LinkLabel1)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox5.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(761, 41)
        Me.GroupBox5.TabIndex = 0
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Bantuan"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.Location = New System.Drawing.Point(554, 16)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(203, 16)
        Me.LinkLabel2.TabIndex = 0
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Download CSV / Bahan Input Data"
        '
        'LinkLabel3
        '
        Me.LinkLabel3.AutoSize = True
        Me.LinkLabel3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel3.Location = New System.Drawing.Point(9, 16)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.Size = New System.Drawing.Size(233, 16)
        Me.LinkLabel3.TabIndex = 0
        Me.LinkLabel3.TabStop = True
        Me.LinkLabel3.Text = "Video Tutorial Input CSV bahan Posting"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.Location = New System.Drawing.Point(255, 16)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(186, 16)
        Me.LinkLabel1.TabIndex = 0
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Video Tutorial Jalankan Posting"
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
        Me.DataGridViewTextBoxColumn4.HeaderText = "START BARIS CSV"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 150
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "SAMPAI BARIS CSV"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 150
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "STATUS"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.Width = 80
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn7.HeaderText = "JUDUL"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "NAMA PROFILE CHROME"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.Visible = False
        Me.DataGridViewTextBoxColumn8.Width = 250
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "USER ID"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "PESAN"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.Width = 250
        '
        'PostFBMobilControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lblHeader)
        Me.Name = "PostFBMobilControl"
        Me.Size = New System.Drawing.Size(801, 446)
        CType(Me.gridReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridProfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.tabProfile.ResumeLayout(False)
        Me.tabReport.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents gridReport As System.Windows.Forms.DataGridView
    Friend WithEvents gridProfile As System.Windows.Forms.DataGridView
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cbxTahun As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cbxMerek As System.Windows.Forms.ComboBox
    Friend WithEvents cbxBody As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cbxColor As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cbxCondition As ComboBox
    Friend WithEvents cbxFuel As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents cbxColor2 As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents chkTdkMasalah As CheckBox
    Friend WithEvents cbxTransmission As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents tabProfile As TabPage
    Friend WithEvents tabReport As TabPage
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents digunakanCol As DataGridViewCheckBoxColumn
    Friend WithEvents profileChromeCol As DataGridViewTextBoxColumn
    Friend WithEvents userIdforPostCol As DataGridViewTextBoxColumn
    Friend WithEvents passwordforPostCol As DataGridViewTextBoxColumn
    Friend WithEvents StartCsvCol As DataGridViewTextBoxColumn
    Friend WithEvents sampaiCsvCol As DataGridViewTextBoxColumn
    Friend WithEvents statusCol As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn23 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn24 As DataGridViewTextBoxColumn
    Friend WithEvents userIdSuccessCol As DataGridViewTextBoxColumn
    Friend WithEvents pesanErrorCol As DataGridViewTextBoxColumn
    Friend WithEvents btnClose As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel9 As Panel
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents txtCsv As TextBox
    Friend WithEvents btnImportCsv As Button
    Friend WithEvents Panel5 As Panel
    Friend WithEvents cbxDataProfile As ComboBox
    Friend WithEvents chkRunAllProfile As CheckBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents btnStartProcess As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents btnForceClose As Button
    Friend WithEvents btnPause As Button
    Friend WithEvents btnRefreshData As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents LinkLabel2 As LinkLabel
    Friend WithEvents LinkLabel3 As LinkLabel
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As DataGridViewTextBoxColumn
End Class
