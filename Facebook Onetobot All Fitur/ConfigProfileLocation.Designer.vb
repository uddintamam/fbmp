<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConfigProfileLocation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConfigProfileLocation))
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.btnRefreshData = New System.Windows.Forms.Button()
        Me.btnSelectFolder = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(12, 62)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.ReadOnly = True
        Me.txtPath.Size = New System.Drawing.Size(436, 20)
        Me.txtPath.TabIndex = 322
        '
        'lblHeader
        '
        Me.lblHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lblHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(460, 38)
        Me.lblHeader.TabIndex = 324
        Me.lblHeader.Text = "ATUR LOKASI PENYIMPANAN DATA"
        Me.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnRefreshData
        '
        Me.btnRefreshData.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.btnRefreshData.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefreshData.FlatAppearance.BorderSize = 0
        Me.btnRefreshData.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefreshData.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.btnRefreshData.ForeColor = System.Drawing.Color.White
        Me.btnRefreshData.Image = Global.Facebook_Onetobot_All_Fitur.My.Resources.Resources.save_16
        Me.btnRefreshData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefreshData.Location = New System.Drawing.Point(344, 87)
        Me.btnRefreshData.Name = "btnRefreshData"
        Me.btnRefreshData.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.btnRefreshData.Size = New System.Drawing.Size(104, 33)
        Me.btnRefreshData.TabIndex = 323
        Me.btnRefreshData.Text = "Simpan"
        Me.btnRefreshData.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRefreshData.UseVisualStyleBackColor = False
        '
        'btnSelectFolder
        '
        Me.btnSelectFolder.BackColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(182, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.btnSelectFolder.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSelectFolder.FlatAppearance.BorderSize = 0
        Me.btnSelectFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectFolder.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.btnSelectFolder.ForeColor = System.Drawing.Color.White
        Me.btnSelectFolder.Image = Global.Facebook_Onetobot_All_Fitur.My.Resources.Resources.folder_16
        Me.btnSelectFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSelectFolder.Location = New System.Drawing.Point(12, 87)
        Me.btnSelectFolder.Name = "btnSelectFolder"
        Me.btnSelectFolder.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.btnSelectFolder.Size = New System.Drawing.Size(132, 33)
        Me.btnSelectFolder.TabIndex = 321
        Me.btnSelectFolder.Text = "Buka Folder"
        Me.btnSelectFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSelectFolder.UseVisualStyleBackColor = False
        '
        'ConfigProfileLocation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(460, 137)
        Me.Controls.Add(Me.lblHeader)
        Me.Controls.Add(Me.btnRefreshData)
        Me.Controls.Add(Me.txtPath)
        Me.Controls.Add(Me.btnSelectFolder)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ConfigProfileLocation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Konfigurasi Folder Profile"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtPath As TextBox
    Friend WithEvents btnSelectFolder As Button
    Friend WithEvents btnRefreshData As Button
    Friend WithEvents lblHeader As Label
End Class
