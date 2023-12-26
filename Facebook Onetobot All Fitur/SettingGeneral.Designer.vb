<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingGeneral
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.numStart = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.numRange = New System.Windows.Forms.NumericUpDown()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.btnSelectFolder = New System.Windows.Forms.Button()
        Me.numWaitElement = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.btnSaveSetting = New System.Windows.Forms.Button()
        Me.btnLogut = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        CType(Me.numStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numRange, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.numWaitElement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
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
        Me.lblHeader.TabIndex = 20
        Me.lblHeader.Text = "KONFIGURASI UMUM"
        Me.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label2.Location = New System.Drawing.Point(6, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 16)
        Me.Label2.TabIndex = 343
        Me.Label2.Text = "Start Baris CSV"
        '
        'numStart
        '
        Me.numStart.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.numStart.Location = New System.Drawing.Point(300, 19)
        Me.numStart.Name = "numStart"
        Me.numStart.Size = New System.Drawing.Size(95, 23)
        Me.numStart.TabIndex = 344
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label3.Location = New System.Drawing.Point(6, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(155, 16)
        Me.Label3.TabIndex = 343
        Me.Label3.Text = "Selisih ke Baris Akhir CSV"
        '
        'numRange
        '
        Me.numRange.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.numRange.Location = New System.Drawing.Point(300, 49)
        Me.numRange.Name = "numRange"
        Me.numRange.Size = New System.Drawing.Size(95, 23)
        Me.numRange.TabIndex = 344
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Panel2)
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 32)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(20)
        Me.Panel4.Size = New System.Drawing.Size(810, 391)
        Me.Panel4.TabIndex = 364
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Controls.Add(Me.GroupBox3)
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Controls.Add(Me.numWaitElement)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Location = New System.Drawing.Point(20, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(10, 20, 0, 0)
        Me.Panel2.Size = New System.Drawing.Size(759, 240)
        Me.Panel2.TabIndex = 363
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.numStart)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.numRange)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 60)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(412, 85)
        Me.GroupBox1.TabIndex = 361
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Baris CSV"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.LinkLabel1)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox3.Location = New System.Drawing.Point(10, 199)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(749, 41)
        Me.GroupBox3.TabIndex = 360
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Bantuan"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.Location = New System.Drawing.Point(6, 16)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(208, 16)
        Me.LinkLabel1.TabIndex = 3
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Video Tutorial Jalankan Konfigurasi"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel7)
        Me.Panel1.Controls.Add(Me.btnSelectFolder)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.Panel1.Size = New System.Drawing.Size(749, 34)
        Me.Panel1.TabIndex = 359
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.txtPath)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(10, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(0, 10, 10, 10)
        Me.Panel7.Size = New System.Drawing.Size(597, 34)
        Me.Panel7.TabIndex = 363
        '
        'txtPath
        '
        Me.txtPath.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPath.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.txtPath.Location = New System.Drawing.Point(0, 10)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.ReadOnly = True
        Me.txtPath.Size = New System.Drawing.Size(587, 16)
        Me.txtPath.TabIndex = 363
        '
        'btnSelectFolder
        '
        Me.btnSelectFolder.BackColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(182, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnSelectFolder.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSelectFolder.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSelectFolder.FlatAppearance.BorderSize = 0
        Me.btnSelectFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectFolder.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.btnSelectFolder.ForeColor = System.Drawing.Color.White
        Me.btnSelectFolder.Image = Global.Facebook_Onetobot_All_Fitur.My.Resources.Resources.folder_16
        Me.btnSelectFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSelectFolder.Location = New System.Drawing.Point(607, 0)
        Me.btnSelectFolder.Name = "btnSelectFolder"
        Me.btnSelectFolder.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.btnSelectFolder.Size = New System.Drawing.Size(132, 34)
        Me.btnSelectFolder.TabIndex = 362
        Me.btnSelectFolder.Text = "Buka Folder"
        Me.btnSelectFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSelectFolder.UseVisualStyleBackColor = False
        '
        'numWaitElement
        '
        Me.numWaitElement.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.numWaitElement.Location = New System.Drawing.Point(310, 158)
        Me.numWaitElement.Name = "numWaitElement"
        Me.numWaitElement.Size = New System.Drawing.Size(95, 23)
        Me.numWaitElement.TabIndex = 344
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label4.Location = New System.Drawing.Point(411, 160)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 16)
        Me.Label4.TabIndex = 343
        Me.Label4.Text = "Detik"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label1.Location = New System.Drawing.Point(17, 160)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(254, 16)
        Me.Label1.TabIndex = 343
        Me.Label1.Text = "Waktu Maksimal Loading Halaman Browser"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.btnSaveSetting)
        Me.Panel5.Controls.Add(Me.btnLogut)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(20, 316)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(770, 55)
        Me.Panel5.TabIndex = 360
        '
        'btnSaveSetting
        '
        Me.btnSaveSetting.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.btnSaveSetting.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSaveSetting.FlatAppearance.BorderSize = 0
        Me.btnSaveSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveSetting.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveSetting.ForeColor = System.Drawing.Color.White
        Me.btnSaveSetting.Image = Global.Facebook_Onetobot_All_Fitur.My.Resources.Resources.save_16
        Me.btnSaveSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSaveSetting.Location = New System.Drawing.Point(0, 9)
        Me.btnSaveSetting.Name = "btnSaveSetting"
        Me.btnSaveSetting.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.btnSaveSetting.Size = New System.Drawing.Size(112, 38)
        Me.btnSaveSetting.TabIndex = 324
        Me.btnSaveSetting.Text = "Simpan"
        Me.btnSaveSetting.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSaveSetting.UseVisualStyleBackColor = False
        '
        'btnLogut
        '
        Me.btnLogut.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.btnLogut.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLogut.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnLogut.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogut.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogut.ForeColor = System.Drawing.Color.White
        Me.btnLogut.Image = Global.Facebook_Onetobot_All_Fitur.My.Resources.Resources.login_32
        Me.btnLogut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLogut.Location = New System.Drawing.Point(529, 0)
        Me.btnLogut.Name = "btnLogut"
        Me.btnLogut.Padding = New System.Windows.Forms.Padding(20, 0, 20, 0)
        Me.btnLogut.Size = New System.Drawing.Size(241, 55)
        Me.btnLogut.TabIndex = 313
        Me.btnLogut.Text = "          LOGOUT APLIKASI"
        Me.btnLogut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLogut.UseVisualStyleBackColor = False
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
        'SettingGeneral
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.lblHeader)
        Me.Name = "SettingGeneral"
        Me.Size = New System.Drawing.Size(810, 423)
        CType(Me.numStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numRange, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.numWaitElement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblHeader As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents numStart As NumericUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents numRange As NumericUpDown
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents btnLogut As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents txtPath As TextBox
    Friend WithEvents btnSelectFolder As Button
    Friend WithEvents btnSaveSetting As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents numWaitElement As NumericUpDown
    Friend WithEvents Label1 As Label
    Friend WithEvents Label4 As Label
End Class
