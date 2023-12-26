#Region " Impors Namespace"
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.IO
Imports Newtonsoft.Json
#End Region
Public Class Login
    Public versi = "1.4 Update 24 Nov 2023"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Not CheckInternetConnection() Then
            MessageBox.Show("Anda tidak terhubung ke Internet", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
            Return
        End If

        '// ICON UNTUK WAKTU JALAN
        Timer1.Interval = 50 ' Interval waktu dalam milidetik
        Timer1.Enabled = True
        '// CEK UPDATE VERSI TOOLS
        Me.Text = "Facebook Onetobot Domination" + versi
        Dim client = New WebClient
        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        '//================ CEK VERSI DARI TOOLS PADA USER ===============
        Dim newSoftwareVersion = client.DownloadString("https://member.onetobot.com/lisensiOnetobot/OnetobotLisensiUpdate.txt")
        If newSoftwareVersion <> versi Then
            Dim laporan As DialogResult = MessageBox.Show("Sudah ada versi terbaru dari tools ini. Apakah Anda ingin memperbarui ke versi terbaru?", "INFO UPDATE TOOLS KOBOBOT", MessageBoxButtons.YesNo)
            If laporan = DialogResult.Yes Then
                Dim result As DialogResult = MessageBox.Show("Klik OK untuk mengunduh versi terbaru dan silakan download update di bulan yang paling baru versinya.", "INFO UPDATE TOOLS KOBOBOT", MessageBoxButtons.OKCancel)
                If result = DialogResult.OK Then
                    System.Diagnostics.Process.Start("https://drive.google.com/drive/folders/1KJ87RHYiTJo3fjL5SARYwy1ah4hwlz7R?usp=sharing")
                End If
            End If
        End If

        Dim mac = getMacAddress()
        Dim apiUrl As String = "https://api.onetobot.com/api/v1/login"
        'Dim apiUrl As String = "http://192.168.0.180:4000/api/v1/login"

        Dim dataObject As New apiLogin() ' Gantilah YourDataClass dengan nama class objek Anda
        dataObject.email = ""
        dataObject.password = ""
        dataObject.mac_address = mac
        dataObject.softwareId = 1

        Try
            Dim response As responseService = SendJson(apiUrl, dataObject).Result

            If response.success Then
                Me.Hide()

                'Dim modules As New List(Of String)
                'modules.Add(ModuleRegistration.FBMP_Motor.ToString())

                Dim licensePackage = New LicensePackage(response.isTrial, response.packageList)


                Using formBase As New FormBase(licensePackage)
                    If formBase.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        Me.Close()
                    End If
                End Using
                'Else
                'MsgBox(response.message, MsgBoxStyle.DefaultButton2, "Peringatan")
                'MessageBox.Show(response.message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try

    End Sub
    Public Function verificationStatus(type As String, user As String, skey As String, MacAddress As String, produk As String)
        Try
            'type => login , register

            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            '======= Check if end point exist =========
            Dim webClient As New System.Net.WebClient
            webClient.Headers("User-Agent") = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv55.0) Gecko/20100101 Firefox/55.0"

            Dim url = "https://kobobot.com/software/verification_v2.php?type=" + type + "&email=" + user + "&secreet_key=" + skey + "&mac_address=" + MacAddress + "&product=" + produk
            'Process.Start(url)
            'MsgBox(url)
            'Dim url = "https://verification.kobobot.com/verification.php?type=" + type + "&email=" + user + "&secreet_key=" + skey + "&mac_address=" + MacAddress + "&product=" + produk
            Dim gdata = webClient.DownloadString(url)
            'MsgBox(gdata)


            Return gdata
        Catch ex As Exception
            File.WriteAllText("verificationLogs.txt", ex.ToString)
            Return ("Server Down")
        End Try
    End Function
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim mac = getMacAddress()
        Dim type = "register"
        If File.Exists("verification.txt") Then
            type = "login"
        End If

        Dim apiUrl As String = "https://api.onetobot.com/api/v1/login"
        'Dim apiUrl As String = "http://192.168.0.180:4000/api/v1/login"

        Dim dataObject As New apiLogin() ' Gantilah YourDataClass dengan nama class objek Anda
        dataObject.email = txtEmail.Text
        dataObject.password = txtPass.Text
        dataObject.mac_address = mac
        dataObject.softwareId = 1

        Try
            Dim response As responseService = SendJson(apiUrl, dataObject).Result
            If response.success Then
                Me.Hide()
                'Dim modules As New List(Of String)
                'modules.Add(ModuleRegistration.FBMP_Motor.ToString())

                Dim licensePackage = New LicensePackage(response.isTrial, response.packageList)

                Using formBase As New FormBase(licensePackage)
                    If formBase.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        Me.Close()
                    End If
                End Using
            Else
                MsgBox(response.message)
                'MessageBox.Show(response.message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Left -= 5 ' Menggeser label ke kiri sejauh 5 piksel pada setiap interval timer
        ' Jika label mencapai batas tertentu, Anda bisa mengatur ulang posisinya
        If Label3.Left + Label3.Width <= 0 Then
            Label3.Left = Me.Width
        End If
    End Sub

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

    Function CheckInternetConnection() As Boolean
        Try
            Dim ping As New Ping()
            Dim reply As PingReply = ping.Send("www.google.com", 1000) ' Ganti dengan alamat host yang dapat dijangkau

            If reply.Status = IPStatus.Success Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label142_Click(sender As Object, e As EventArgs) Handles Label142.Click

    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click

    End Sub

    Private Sub txtEmail_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged

    End Sub

    Private Sub txtPass_TextChanged(sender As Object, e As EventArgs) Handles txtPass.TextChanged

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub

    Private Sub Label213_Click(sender As Object, e As EventArgs) Handles Label213.Click

    End Sub
End Class '// CLAS PENUTUP SEMUA KODE


Public Class apiLogin
    Public Property softwareId As Integer
    Public Property email As String
    Public Property password As String
    Public Property mac_address As String
End Class

Public Class responseService
    Public Property success As Boolean
    Public Property isTrial As Boolean
    Public Property packageList As List(Of String)
    Public Property message As String
End Class