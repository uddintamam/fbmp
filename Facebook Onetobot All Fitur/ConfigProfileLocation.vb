Imports System.Configuration
Imports System.IO

Public Class ConfigProfileLocation
    Private Sub btnRefreshData_Click(sender As Object, e As EventArgs) Handles btnRefreshData.Click
        ' Membuka file konfigurasi
        If String.IsNullOrEmpty(txtPath.Text) Then
            MessageBox.Show("Lokasi Penyimpanan Profil Wajib di isi", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

        'Dim mustUpdatedDB As Integer = ConfigurationManager.AppSettings("mustUpdatedDB")
        'If mustUpdatedDB > 0 Then
        '    Dim dataSet As New DataSet
        '    Dim profileFile = txtPath.Text & "\UserData.xml"
        '    If File.Exists(profileFile) Then
        '        ' Muat data dari file XML
        '        dataSet.ReadXml(profileFile)

        '        Dim dataTable As DataTable = dataSet.Tables(0)

        '        For Each row As DataRow In dataTable.Rows
        '            row(3) = 0
        '        Next

        '        dataSet.WriteXml(profileFile)

        '        MessageBox.Show("ada beberapa perubahan dan perlu login ulang, silahkan menuju halaman login facebook")
        '    End If

        '    config.AppSettings.Settings("mustUpdatedDB").Value = 0
        'End If

        ' Mengubah nilai di app.config
        config.AppSettings.Settings("binProfile").Value = txtPath.Text

        ' Menyimpan perubahan
        config.Save(ConfigurationSaveMode.Modified)

        ' Memuat ulang konfigurasi
        ConfigurationManager.RefreshSection("appSettings")

        MessageBox.Show("Konfigurasi Lokasi Profile Berhasil disimpan", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)

        DialogResult = DialogResult.OK
    End Sub

    Private Sub btnSelectFolder_Click(sender As Object, e As EventArgs) Handles btnSelectFolder.Click
        Dim folderBrowser As New FolderBrowserDialog()

        ' Menetapkan judul dan deskripsi dialog
        folderBrowser.Description = "Pilih folder tempat menyimpan file"
        folderBrowser.SelectedPath = "C:\" ' Path default yang ditetapkan

        ' Menampilkan dialog dan memeriksa apakah pengguna menekan tombol OK
        If folderBrowser.ShowDialog() = DialogResult.OK Then
            ' Menggunakan path yang dipilih oleh pengguna
            Dim selectedPath As String = folderBrowser.SelectedPath
            txtPath.Text = selectedPath
            Console.WriteLine("Folder yang dipilih: " & selectedPath)

            ' Sekarang, Anda dapat menggunakan path ini untuk menyimpan atau melakukan operasi lainnya.
        Else
            ' Pengguna membatalkan pemilihan
            Console.WriteLine("Pemilihan dibatalkan.")
        End If
    End Sub

    Private Sub ConfigProfileLocation_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
    End Sub
End Class