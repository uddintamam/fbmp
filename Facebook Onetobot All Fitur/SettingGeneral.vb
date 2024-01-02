Imports System.Configuration
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.IO
Imports Newtonsoft.Json

Public Class SettingGeneral

    Dim baseForm As FormBase = DirectCast(Parent, FormBase)
    Private Sub btnSaveSetting_Click(sender As Object, e As EventArgs) Handles btnSaveSetting.Click
        ' Membuka file konfigurasi
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

        ' Mengubah nilai di app.config
        config.AppSettings.Settings("binProfile").Value = txtPath.Text

        config.AppSettings.Settings("startNum").Value = numStart.Value
        config.AppSettings.Settings("rangeNum").Value = numRange.Value
        config.AppSettings.Settings("waitElement").Value = numWaitElement.Value

        ' Menyimpan perubahan
        config.Save(ConfigurationSaveMode.Modified)

        ' Memuat ulang konfigurasi
        ConfigurationManager.RefreshSection("appSettings")
        baseForm.addTabControl()
        MessageBox.Show("Konfigurasi Berhasil disimpan", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub SettingGeneral_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        baseForm = DirectCast(Parent, FormBase)
        txtPath.Text = ConfigurationManager.AppSettings("binProfile")
        numStart.Value = ConfigurationManager.AppSettings("startNum")
        numRange.Value = ConfigurationManager.AppSettings("rangeNum")
        numWaitElement.Value = ConfigurationManager.AppSettings("waitElement")
    End Sub
    '// KODE UNTUK LOGOUT APLIKASI ATAU PINDAH APLIKASI
    Private Sub btnLogut_Click(sender As Object, e As EventArgs) Handles btnLogut.Click
        If MessageBox.Show("Apakah anda yakin akan Logout Aplikasi?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
            Dim apiUrl As String = Login.apiLogoutUrl
            Dim mac = getMacAddress()

            Dim dataObject As New apiLogin() ' Gantilah YourDataClass dengan nama class objek Anda
            dataObject.mac_address = mac
            dataObject.softwareId = Login.softwareId

            Try
                Dim response As responseService = SendJson(apiUrl, dataObject).Result
                If response.success Then
                    baseForm.isLogout = True
                    baseForm.Close()
                Else
                    MsgBox(response.message)
                    'MessageBox.Show(response.message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

            Catch ex As Exception
                Console.WriteLine("Error: " & ex.Message)
            End Try
        End If

    End Sub
    Public Function getMacAddress()
        Dim nics() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
        Dim mac = nics(0).GetPhysicalAddress.ToString
        If Trim(mac) = "" Then
            If File.Exists("cmac.txt") Then
                mac = File.ReadAllText("cmac.txt")
            Else
                mac = GenerateRandomString(30)
                File.WriteAllText("cmac.txt", mac)
            End If
        End If
        Return mac
    End Function
    Public Function GenerateRandomString(ByRef iLength As Integer) As String
        Dim rdm As New Random()
        Dim allowChrs() As Char = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLOMNOPQRSTUVWXYZ0123456789".ToCharArray()
        Dim sResult As String = ""

        For i As Integer = 0 To iLength - 1
            sResult += allowChrs(rdm.Next(0, allowChrs.Length))
        Next
        Return sResult
    End Function

    Async Function SendJson(apiUrl As String, dataObject As Object) As Task(Of responseService)
        Dim ret As responseService = New responseService()
        Dim jsonData As String = JsonConvert.SerializeObject(dataObject)
        Dim httpRequest As HttpWebRequest = WebRequest.Create(apiUrl)
        httpRequest.Method = "POST"
        httpRequest.ContentType = "application/json"

        Try
            Using streamWriter As New StreamWriter(httpRequest.GetRequestStream())
                If Not String.IsNullOrEmpty(jsonData) Then
                    streamWriter.Write(jsonData)
                    streamWriter.Flush()
                    streamWriter.Close()
                End If
            End Using
        Catch ex As Exception

        End Try

        Try
            Dim response As WebResponse = httpRequest.GetResponse()
            Using Stream As Stream = response.GetResponseStream()
                Using reader As TextReader = New StreamReader(Stream)
                    Dim obj = reader.ReadToEnd
                    ret = JsonConvert.DeserializeObject(Of responseService)(obj)
                End Using
            End Using

        Catch ex As WebException
            Using response As WebResponse = ex.Response
                Using responseStream As Stream = response.GetResponseStream()
                    Using reader As New StreamReader(responseStream)
                        Dim obj = reader.ReadToEnd
                        ret = JsonConvert.DeserializeObject(Of responseService)(obj)
                    End Using
                End Using
            End Using
        End Try
        Return ret
    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Dim tabName = "SettingGeneral"
        baseForm.CloseAndRemoveTabPage(tabName)
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim webAddress As String = "https://youtu.be/uLGcF8Gk42c"
        Process.Start(webAddress)
    End Sub
End Class
