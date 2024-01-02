Imports System.Configuration
Imports System.IO
Imports System.Net
Imports System.Threading
Imports SeleniumUndetectedChromeDriver

Public Class FormBase
    Public versi = "1.4 Update 24 Nov 2023"

    Dim sidebar As String = "Close"
    'Properti ini akan di gunakan di semua UserControl

    'isload digunakan untuk memberitahukan jika form/usercontrol sedang proses loading awal
    Public Property isLoad As Boolean = False
    Public Property isLogout As Boolean = False

    ' Daftar thread yang akan dibuat secara dinamis
    Public Property threads As List(Of Thread) = New List(Of Thread)
    Private stopEvent As New ManualResetEvent(False)
    Public suspendEvent As New ManualResetEvent(True)

    'beberapa profile yang sedang berjalan akan ditampung disini
    Public Property Profiles As List(Of ChromeProfile) = New List(Of ChromeProfile)()
    Public Property licensePackage As LicensePackage

    Public Property linkTrial As String = "https://youtu.be/1AVnA0BW7_A"

    Public Class tabInfo
        Public Property tabAppName As String
        Public Property tabIndex As Integer
    End Class

    Public Property tabInfoList As List(Of tabInfo) = New List(Of tabInfo)()

    Public Sub New()
        InitializeComponent()

        Dim modules As New List(Of String)
        modules.Add(ModuleRegistration.FBMP_Motor.ToString())

        licensePackage = New LicensePackage(True, modules)
    End Sub

    Public Sub New(_licensePackage As LicensePackage)
        InitializeComponent()

        licensePackage = _licensePackage
    End Sub

    Public Function runChromeDriver(profileName As String, accountCode As String, Optional windowsSize As Integer = 0) As ChromeProfile

        Dim existingProfile = Profiles.Find(Function(p) p.AccountCode = accountCode)
        If existingProfile IsNot Nothing Then
            If existingProfile.Driver IsNot Nothing Then
                existingProfile.Driver.Quit()
            End If
            Profiles.Remove(existingProfile)
        End If

        Dim driver As UndetectedChromeDriver = Nothing

        Dim newProfile As New ChromeProfile(profileName, accountCode, driver, True, windowsSize)
        Profiles.Add(newProfile)

        Return Profiles.Find(Function(p) p.AccountCode = accountCode)
    End Function

    Public Function runChromeLiteDriver(profileName As String, accountCode As String) As ChromeProfile

        Dim existingProfile = Profiles.Find(Function(p) p.AccountCode = accountCode)
        If existingProfile IsNot Nothing Then
            If existingProfile.Driver IsNot Nothing Then
                existingProfile.Driver.Quit()
            End If
            Profiles.Remove(existingProfile)
        End If

        Dim driver As UndetectedChromeDriver = Nothing

        Dim newProfile As New ChromeProfile(profileName, accountCode, driver, True)
        Profiles.Add(newProfile)

        Return Profiles.Find(Function(p) p.AccountCode = accountCode)
    End Function

    Public Function sleep(second As Integer)
        For i = 0 To second * 100
            System.Threading.Thread.Sleep(10)
            Application.DoEvents()
        Next
        Return True
    End Function

    Public Function findTab(tabName As String) As Integer
        For Each tabInf In tabInfoList
            If tabInf.tabAppName = tabName Then
                Return tabInf.tabIndex
            End If
        Next
        Return -1
    End Function


#Region "private Function (hanya bisa di panggil di dalam FormBase)"
    Private Sub addUserControl(userControl As UserControl, tabName As String)

        ' Membuat instance TabPage baru
        Dim tabPage As New TabPage(tabName)
        userControl.Dock = DockStyle.Fill
        userControl.Parent = Me
        tabPage.Controls.Clear()
        tabPage.Controls.Add(userControl)
        userControl.BringToFront()

        ' Menambahkan TabPage ke dalam TabControl
        TabControl.TabPages.Add(tabPage)
    End Sub

    Public Sub createNewTab(tabName As String, userControl As UserControl)

        addUserControl(userControl, tabName)

        Dim newTab As New tabInfo()
        newTab.tabAppName = tabName
        newTab.tabIndex = tabInfoList.Count

        tabInfoList.Add(newTab)

        TabControl.SelectedIndex = tabInfoList.Count - 1
    End Sub

    Public Sub addTabControl()
        TabControl.TabPages.Clear()
        tabInfoList.Clear()
        '===========================================
        '//bagian inisialisasi Halaman Home
        '//index 0
        Dim homeControl As New HomeControl()
        createNewTab("Home", homeControl)
        '//selsai inisialisasi Halaman Home
        '===========================================
    End Sub
#End Region


    Sub clear()
        BtnDasboard.Text = ""
        btnPost.Text = ""

    End Sub
    Sub setname()
        BtnDasboard.Text = "     Dashboard"
        btnPost.Text = "Riset Keyword"
    End Sub
    Private Sub BtnMenu_Click(sender As Object, e As EventArgs)
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'If sidebar = "Open" Then
        '    leftside.Width += 15
        '    imagepanel.Height += 25
        '    imagepanel.Width += 25
        '    If leftside.Width >= 150 Then
        '        Panel5.Height -= 10
        '        pnlPost.Height -= 10
        '        BtnDasboard.Image = Global.Facebook_Onetobot_All_Fitur.My.Resources.Resources.home_16
        '        btnPost.Image = Global.Facebook_Onetobot_All_Fitur.My.Resources.Resources.online_shop_16
        '        imagepanel.Visible = True
        '        setname()
        '        sidebar = "Close"
        '        Timer1.Stop()
        '    End If
        'Else
        '    leftside.Width -= 15
        '    imagepanel.Height -= 25
        '    imagepanel.Width -= 25
        '    If leftside.Width <= 70 Then
        '        Panel5.Height += 10
        '        pnlPost.Height += 10
        '        BtnDasboard.Image = Global.Facebook_Onetobot_All_Fitur.My.Resources.Resources.home_32
        '        btnPost.Image = Global.Facebook_Onetobot_All_Fitur.My.Resources.Resources.online_shop_32
        '        clear()
        '        imagepanel.Visible = False
        '        sidebar = "Open"
        '        Timer1.Stop()
        '    End If
        'End If
    End Sub

    Private Sub RisetKeyword_Click(sender As Object, e As EventArgs) Handles btnPost.Click

        HidePanelMenu()
        InActiveAllButtonLvl1()
        pnlPostIndc.Visible = True
        pnlPostLite.Visible = True
        pnlPostProperti.Visible = True
        pnlPostMobil.Visible = True
        pnlPostMotor.Visible = True
        pnlPostUmum.Visible = True

        ActiveButton(True, btnPost)
    End Sub

    Private Sub BtnDasboard_Click(sender As Object, e As EventArgs) Handles BtnDasboard.Click
        HidePanelMenu()
        pnlDashboardIndc.Visible = True
        InActiveAllButtonLvl1()
        ActiveButton(True, BtnDasboard)

        Dim tabIndex = findTab("Home")
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        End If
    End Sub

    Private Sub disableButton(disable As Boolean, button As Button)
        If disable Then
            button.BackColor = Color.FromArgb(128, 128, 128)
            button.Cursor = Cursors.Default
        Else
            button.BackColor = Color.FromArgb(57, 182, 236)
            button.Cursor = Cursors.Hand
        End If
    End Sub

    Private Sub ActiveButton(active As Boolean, button As Button)
        If active Then
            button.BackColor = Color.FromArgb(44, 152, 198)
        Else
            button.BackColor = Color.FromArgb(57, 182, 236)
        End If
    End Sub

    Private Sub InActiveAllButtonLvl1()
        For Each registered In licensePackage.ModulRegistered
            Select Case registered
                Case ModuleRegistration.POST_Group.ToString
                    ActiveButton(False, btnPostGroup)
                Case ModuleRegistration.Interaksi.ToString
                    ActiveButton(False, btnInteraksi)
            End Select
        Next

        ActiveButton(False, btnPost)
        ActiveButton(False, BtnDasboard)
        'ActiveButton(False, btnPostGroup)
        ActiveButton(False, btnPesan)
        ActiveButton(False, btnKelolaPost)
        'ActiveButton(False, btnInteraksi)
        ActiveButton(False, btnKonfigurasi)
        ActiveButton(False, btnLogin)
    End Sub

    Private Sub InActivePostButton()
        For Each registered In licensePackage.ModulRegistered
            Select Case registered
                Case ModuleRegistration.FBMP_Umum.ToString
                    ActiveButton(False, btnPostUmum)
                Case ModuleRegistration.FBMP_Motor.ToString
                    ActiveButton(False, btnPostMotor)
                Case ModuleRegistration.FBMP_Mobil.ToString
                    ActiveButton(False, btnPostMobil)
                Case ModuleRegistration.FBMP_Properti.ToString
                    ActiveButton(False, btnPostProperti)
                Case ModuleRegistration.FBMP_Lite.ToString
                    ActiveButton(False, btnPostLite)
            End Select
        Next
    End Sub

    Private Sub HidePanelMenu()
        pnlDashboardIndc.Visible = False
        pnlPostIndc.Visible = False
        pnlPostUmum.Visible = False
        pnlPostUmum.Height = 30
        pnlDetailPostUmum.Visible = False
        pnlPostMotor.Visible = False
        pnlPostMotor.Height = 30
        pnlPostMotorDetail.Visible = False
        pnlPostMobil.Visible = False
        pnlPostMobil.Height = 30
        pnlPostMobilDetail.Visible = False
        pnlPostProperti.Visible = False
        pnlPostProperti.Height = 30
        pnlPostPropertiDetail.Visible = False
        pnlPostLite.Visible = False
        pnlPostGroupIndc.Visible = False
        pnlPesanIndc.Visible = False
        pnlPesanBalas.Visible = False
        pnlPesanKirim.Visible = False
        pnlKelolaPostIndc.Visible = False
        pnlKelolaPostPerbarui.Visible = False
        pnlKelolaPostHapus.Visible = False
        pnlInteraksiIndc.Visible = False
        pnlKonfigurasiIndc.Visible = False
        pnlLoginIndc.Visible = False

        InActivePostButton()
        InActivePostUmumButton()
        InActivePostMotorButton()
        InActivePostMobilButton()
        InActivePostPropertiButton()
        InActivePesanButton()
        InActiveKelolaPostButton()
    End Sub

    Private Sub ShowPanelMenuPost()
        pnlPostIndc.Visible = True
        pnlPostUmum.Visible = True
        pnlPostMotor.Visible = True
        pnlPostMobil.Visible = True
        pnlPostProperti.Visible = True
        pnlPostLite.Visible = True
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        HidePanelMenu()
        pnlDashboardIndc.Visible = True
        InActiveAllButtonLvl1()
        ActiveButton(True, BtnDasboard)
        Dim locationProfile As String = ConfigurationManager.AppSettings("binProfile")
        If String.IsNullOrEmpty(locationProfile) Or Not Directory.Exists(locationProfile) Then
            Using configPath As New ConfigProfileLocation()
                configPath.TopMost = True
                If Not configPath.ShowDialog = DialogResult.OK Then
                    Me.Close()
                    Return
                End If
            End Using
        End If

        'TabControl.Height = 1500
        addTabControl()
        TabControl.SelectedIndex = 0

        If licensePackage IsNot Nothing AndAlso Not licensePackage.IsTrial Then
            PanelTop.BackColor = Color.FromArgb(15, 102, 139)
            PanelButtom.BackColor = Color.FromArgb(15, 102, 139)
            lblPaket.Text = "PRO"
        Else
            PanelTop.BackColor = Color.FromArgb(255, 128, 0)
            PanelButtom.BackColor = Color.FromArgb(255, 128, 0)
            lblPaket.Text = "TRIAL"
        End If

        disableButton(True, btnPostUmum)
        disableButton(True, btnPostMotor)
        disableButton(True, btnPostMobil)
        disableButton(True, btnPostProperti)
        disableButton(True, btnPostLite)
        disableButton(True, btnPostGroup)
        disableButton(True, btnPesanBalas)
        disableButton(True, btnPesanKirim)
        disableButton(True, btnKelolaPostPerbarui)
        disableButton(True, btnKelolaPostHapus)
        disableButton(True, btnInteraksi)
        For Each registered In licensePackage.ModulRegistered
            Select Case registered
                Case ModuleRegistration.FBMP_Umum.ToString
                    disableButton(False, btnPostUmum)
                Case ModuleRegistration.FBMP_Motor.ToString
                    disableButton(False, btnPostMotor)
                Case ModuleRegistration.FBMP_Mobil.ToString
                    disableButton(False, btnPostMobil)
                Case ModuleRegistration.FBMP_Properti.ToString
                    disableButton(False, btnPostProperti)
                Case ModuleRegistration.FBMP_Lite.ToString
                    disableButton(False, btnPostLite)
                Case ModuleRegistration.POST_Group.ToString
                    disableButton(False, btnPostGroup)
                Case ModuleRegistration.Balas_Pesan.ToString
                    disableButton(False, btnPesanBalas)
                Case ModuleRegistration.Kirim_Pesan.ToString
                    disableButton(False, btnPesanKirim)
                Case ModuleRegistration.Perbaharui_FBMP.ToString
                    disableButton(False, btnKelolaPostPerbarui)
                Case ModuleRegistration.Hapus_FBMP.ToString
                    disableButton(False, btnKelolaPostHapus)
                Case ModuleRegistration.Interaksi.ToString
                    disableButton(False, btnInteraksi)
            End Select
        Next

        ''/// ==============
        ''// CEK UPDATE VERSI TOOLS
        'Me.Text = "Facebook Onetobot Domination" + versi
        'Dim client = New WebClient
        'ServicePointManager.Expect100Continue = True
        'ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        ''//================ CEK VERSI DARI TOOLS PADA USER ===============
        'Dim newSoftwareVersion = client.DownloadString("https://member.onetobot.com/lisensiOnetobot/OnetobotLisensiUpdate.txt")
        'If newSoftwareVersion <> versi Then
        '    Dim laporan As DialogResult = MessageBox.Show("Sudah ada versi terbaru dari tools ini. Apakah Anda ingin memperbarui ke versi terbaru?", "INFO UPDATE TOOLS KOBOBOT", MessageBoxButtons.YesNo)
        '    If laporan = DialogResult.Yes Then
        '        Dim result As DialogResult = MessageBox.Show("Klik OK untuk mengunduh versi terbaru dan silakan download update di bulan yang paling baru versinya.", "INFO UPDATE TOOLS KOBOBOT", MessageBoxButtons.OKCancel)
        '        If result = DialogResult.OK Then
        '            System.Diagnostics.Process.Start("https://drive.google.com/drive/folders/1KJ87RHYiTJo3fjL5SARYwy1ah4hwlz7R?usp=sharing")
        '        End If
        '    End If
        'End If
    End Sub

    Private Sub btnGetAllin_Click(sender As Object, e As EventArgs)
        HidePanelMenu()
        ActiveButton(False, btnPost)
        ActiveButton(False, BtnDasboard)
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        For Each profile In Profiles
            If profile.IsOnProcess Then
                MessageBox.Show("Tidak Bisa menutup Aplikasi, Masih terdapat proses yang belum selesai", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                e.Cancel = True
                Return
            End If
        Next

        For Each profile In Profiles
            If profile.Driver IsNot Nothing Then
                profile.Driver.Quit()
            End If
        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnPostUmum.Click
        If licensePackage IsNot Nothing AndAlso Not licensePackage.ModulRegistered.Contains(ModuleRegistration.FBMP_Umum.ToString()) Then
            Process.Start(linkTrial)
            Return
        End If

        HidePanelMenu()
        ShowPanelMenuPost()
        InActivePostButton()
        ActiveButton(True, btnPostUmum)
        pnlDetailPostUmum.Visible = True
        pnlPostUmum.Height = 130
    End Sub

    Private Sub btnPostMotor_Click(sender As Object, e As EventArgs) Handles btnPostMotor.Click
        If licensePackage IsNot Nothing AndAlso Not licensePackage.ModulRegistered.Contains(ModuleRegistration.FBMP_Motor.ToString()) Then
            Process.Start(linkTrial)
            Return
        End If

        HidePanelMenu()
        ShowPanelMenuPost()
        InActivePostButton()
        ActiveButton(True, btnPostMotor)
        pnlPostMotorDetail.Visible = True
        pnlPostMotor.Height = 130
    End Sub

    Private Sub btnPostMobil_Click(sender As Object, e As EventArgs) Handles btnPostMobil.Click
        If licensePackage IsNot Nothing AndAlso Not licensePackage.ModulRegistered.Contains(ModuleRegistration.FBMP_Mobil.ToString()) Then
            Process.Start(linkTrial)
            Return
        End If
        HidePanelMenu()
        ShowPanelMenuPost()
        InActivePostButton()
        ActiveButton(True, btnPostMobil)
        pnlPostMobilDetail.Visible = True
        pnlPostMobil.Height = 130
    End Sub

    Private Sub btnPostProperti_Click(sender As Object, e As EventArgs) Handles btnPostProperti.Click
        If licensePackage IsNot Nothing AndAlso Not licensePackage.ModulRegistered.Contains(ModuleRegistration.FBMP_Properti.ToString()) Then
            Process.Start(linkTrial)
            Return
        End If
        HidePanelMenu()
        ShowPanelMenuPost()
        InActivePostButton()
        ActiveButton(True, btnPostProperti)
        pnlPostPropertiDetail.Visible = True
        pnlPostProperti.Height = 130
    End Sub

    Private Sub btnPostLite_Click(sender As Object, e As EventArgs) Handles btnPostLite.Click
        If licensePackage IsNot Nothing AndAlso Not licensePackage.ModulRegistered.Contains(ModuleRegistration.FBMP_Lite.ToString()) Then
            Process.Start(linkTrial)
            Return
        End If
        HidePanelMenu()
        ShowPanelMenuPost()
        InActivePostButton()
        ActiveButton(True, btnPostLite)
        pnlPostLiteDetail.Visible = True

        Dim tabName = "PostFBLite"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim postFBLiteControl As New PostFBLiteControl()
            createNewTab(tabName, postFBLiteControl)
        End If
    End Sub

    Private Sub InActivePostUmumButton()
        ActiveButton(False, btnPostUmumBiasa)
        ActiveButton(False, btnPostUmumDraft)
        ActiveButton(False, btnPostUmumAntiDuplikat)
    End Sub

    Private Sub btnPostUmumBiasa_Click(sender As Object, e As EventArgs) Handles btnPostUmumBiasa.Click
        InActivePostUmumButton()
        ActiveButton(True, btnPostUmumBiasa)
        Dim tabName = "PostGeneral"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim postControl As New PostFBGeneralPostControl(FiturPostFBEnum.General)
            createNewTab(tabName, postControl)
        End If
    End Sub

    Private Sub btnPostUmumDraft_Click(sender As Object, e As EventArgs) Handles btnPostUmumDraft.Click
        InActivePostUmumButton()
        ActiveButton(True, btnPostUmumDraft)
        Dim tabName = "GeneralDraft"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim draftControl As New PostFBGeneralPostControl(FiturPostFBEnum.OnlyDraft)
            createNewTab(tabName, draftControl)
        End If
    End Sub

    Private Sub btnPostUmumAntiDuplikat_Click(sender As Object, e As EventArgs) Handles btnPostUmumAntiDuplikat.Click
        InActivePostUmumButton()
        ActiveButton(True, btnPostUmumAntiDuplikat)
        Dim tabName = "PostAntiDuplicate"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim antiDuplicateControl As New PostFBGeneralPostControl(FiturPostFBEnum.DraftAndPost)
            createNewTab(tabName, antiDuplicateControl)
        End If
    End Sub

    Private Sub InActivePostMotorButton()
        ActiveButton(False, btnPostMotorBiasa)
        ActiveButton(False, btnPostMotorDraft)
        ActiveButton(False, btnPostMotorAntiDuplikat)
    End Sub

    Private Sub btnPostMotorBiasa_Click(sender As Object, e As EventArgs) Handles btnPostMotorBiasa.Click
        InActivePostMotorButton()
        ActiveButton(True, btnPostMotorBiasa)
        Dim tabName = "PostFBMotor"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim postFBMotorControl As New PostFBMotorControl(FiturPostFBEnum.General)
            createNewTab(tabName, postFBMotorControl)
        End If
    End Sub

    Private Sub btnPostMotorDraft_Click(sender As Object, e As EventArgs) Handles btnPostMotorDraft.Click
        InActivePostMotorButton()
        ActiveButton(True, btnPostMotorDraft)
        Dim tabName = "DraftFBMotor"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim draftFBMotorControl As New PostFBMotorControl(FiturPostFBEnum.OnlyDraft)
            createNewTab(tabName, draftFBMotorControl)
        End If
    End Sub

    Private Sub btnPostMotorAntiDuplikat_Click(sender As Object, e As EventArgs) Handles btnPostMotorAntiDuplikat.Click
        InActivePostMotorButton()
        ActiveButton(True, btnPostMotorAntiDuplikat)
        Dim tabName = "DraftAndPostFBMotor"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim draftAndPostFBMotorControl As New PostFBMotorControl(FiturPostFBEnum.DraftAndPost)
            createNewTab(tabName, draftAndPostFBMotorControl)
        End If
    End Sub

    Private Sub InActivePostMobilButton()
        ActiveButton(False, btnPostMobilBiasa)
        ActiveButton(False, btnPostMobilDraft)
        ActiveButton(False, btnPostMobilAntiDuplikat)
    End Sub

    Private Sub btnPostMobilBiasa_Click(sender As Object, e As EventArgs) Handles btnPostMobilBiasa.Click
        InActivePostMobilButton()
        ActiveButton(True, btnPostMobilBiasa)
        Dim tabName = "PostFBMobil"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim postFBMobilControl As New PostFBMobilControl(FiturPostFBEnum.General)
            createNewTab(tabName, postFBMobilControl)
        End If
    End Sub

    Private Sub btnPostMobilDraft_Click(sender As Object, e As EventArgs) Handles btnPostMobilDraft.Click
        InActivePostMobilButton()
        ActiveButton(True, btnPostMobilDraft)
        Dim tabName = "DraftFBMobil"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim draftFBMobilControl As New PostFBMobilControl(FiturPostFBEnum.OnlyDraft)
            createNewTab(tabName, draftFBMobilControl)
        End If
    End Sub

    Private Sub btnPostMobilAntiDuplikat_Click(sender As Object, e As EventArgs) Handles btnPostMobilAntiDuplikat.Click
        InActivePostMobilButton()
        ActiveButton(True, btnPostMobilAntiDuplikat)
        Dim tabName = "DraftAndPostFBMobil"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim draftAndostFBMobilControl As New PostFBMobilControl(FiturPostFBEnum.DraftAndPost)
            createNewTab(tabName, draftAndostFBMobilControl)
        End If
    End Sub

    Private Sub InActivePostPropertiButton()
        ActiveButton(False, btnPostPropertiBiasa)
        ActiveButton(False, btnPostPropertiDraft)
        ActiveButton(False, btnPostPropertiAntiDuplikat)
    End Sub

    Private Sub btnPostPropertiBiasa_Click(sender As Object, e As EventArgs) Handles btnPostPropertiBiasa.Click
        InActivePostPropertiButton()
        ActiveButton(True, btnPostPropertiBiasa)
        Dim tabName = "PostFBProperti"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim postFBPropertiControl As New PostFBPropertiControl(FiturPostFBEnum.General)
            createNewTab(tabName, postFBPropertiControl)
        End If
    End Sub

    Private Sub btnPostPropertiDraft_Click(sender As Object, e As EventArgs) Handles btnPostPropertiDraft.Click
        InActivePostPropertiButton()
        ActiveButton(True, btnPostPropertiDraft)
        Dim tabName = "DraftFBProperti"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim draftFBPropertiControl As New PostFBPropertiControl(FiturPostFBEnum.OnlyDraft)
            createNewTab(tabName, draftFBPropertiControl)
        End If
    End Sub

    Private Sub btnPostPropertiAntiDuplikat_Click(sender As Object, e As EventArgs) Handles btnPostPropertiAntiDuplikat.Click
        InActivePostPropertiButton()
        ActiveButton(True, btnPostPropertiAntiDuplikat)
        Dim tabName = "DraftAndPostFBProperti"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim draftAndPostFBPropertiControl As New PostFBPropertiControl(FiturPostFBEnum.DraftAndPost)
            createNewTab(tabName, draftAndPostFBPropertiControl)
        End If
    End Sub

    Private Sub btnPostGroup_Click(sender As Object, e As EventArgs) Handles btnPostGroup.Click
        If licensePackage IsNot Nothing AndAlso Not licensePackage.ModulRegistered.Contains(ModuleRegistration.POST_Group.ToString()) Then
            Process.Start(linkTrial)
            Return
        End If
        HidePanelMenu()
        pnlPostGroupIndc.Visible = True

        InActiveAllButtonLvl1()
        ActiveButton(True, btnPostGroup)

        Dim tabName = "PostGroupFB"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim postGroupFBControl As New PostGroupFBControl()
            createNewTab(tabName, postGroupFBControl)
        End If
    End Sub

    Private Sub btnPesan_Click(sender As Object, e As EventArgs) Handles btnPesan.Click
        HidePanelMenu()
        InActiveAllButtonLvl1()
        ShowPanelMenuPesan()

        ActiveButton(True, btnPesan)
    End Sub

    Private Sub ShowPanelMenuPesan()
        pnlPesanIndc.Visible = True
        pnlPesanBalas.Visible = True
        pnlPesanKirim.Visible = True
    End Sub

    Private Sub InActivePesanButton()
        For Each registered In licensePackage.ModulRegistered
            Select Case registered
                Case ModuleRegistration.Balas_Pesan.ToString
                    ActiveButton(False, btnPesanBalas)
                Case ModuleRegistration.Kirim_Pesan.ToString
                    ActiveButton(False, btnPesanKirim)
            End Select
        Next
    End Sub

    Private Sub btnPesanBalas_Click(sender As Object, e As EventArgs) Handles btnPesanBalas.Click
        If licensePackage IsNot Nothing AndAlso Not licensePackage.ModulRegistered.Contains(ModuleRegistration.Balas_Pesan.ToString()) Then
            Process.Start(linkTrial)
            Return
        End If

        InActivePesanButton()
        ActiveButton(True, btnPesanBalas)

        Dim tabName = "MessageReplyer"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim messageReplyerControl As New MessageReplyerControl()
            createNewTab(tabName, messageReplyerControl)
        End If
    End Sub

    Private Sub btnPesanKirim_Click(sender As Object, e As EventArgs) Handles btnPesanKirim.Click
        If licensePackage IsNot Nothing AndAlso Not licensePackage.ModulRegistered.Contains(ModuleRegistration.Kirim_Pesan.ToString()) Then
            Process.Start(linkTrial)
            Return
        End If
        InActivePesanButton()
        ActiveButton(True, btnPesanKirim)

        Dim tabName = "SenderMessage"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim control As New MessageSenderControl()
            createNewTab(tabName, control)
        End If
    End Sub

    Private Sub btnKelolaPost_Click(sender As Object, e As EventArgs) Handles btnKelolaPost.Click
        HidePanelMenu()
        InActiveAllButtonLvl1()
        ShowPanelKelolaPost()

        ActiveButton(True, btnKelolaPost)
    End Sub

    Private Sub ShowPanelKelolaPost()
        pnlKelolaPostIndc.Visible = True
        pnlKelolaPostHapus.Visible = True
        pnlKelolaPostPerbarui.Visible = True
    End Sub

    Private Sub InActiveKelolaPostButton()
        For Each registered In licensePackage.ModulRegistered
            Select Case registered
                Case ModuleRegistration.Perbaharui_FBMP.ToString
                    ActiveButton(False, btnKelolaPostPerbarui)
                Case ModuleRegistration.Hapus_FBMP.ToString
                    ActiveButton(False, btnKelolaPostHapus)
            End Select
        Next
    End Sub

    Private Sub btnKelolaPostPerbarui_Click(sender As Object, e As EventArgs) Handles btnKelolaPostPerbarui.Click
        If licensePackage IsNot Nothing AndAlso Not licensePackage.ModulRegistered.Contains(ModuleRegistration.Perbaharui_FBMP.ToString()) Then
            Process.Start(linkTrial)
            Return
        End If
        InActiveKelolaPostButton()
        ActiveButton(True, btnKelolaPostPerbarui)

        Dim tabName = "ReNewPost"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim reNewPostControl As New ReNewPostControl(FiturManagePostEnum.Renew)
            createNewTab(tabName, reNewPostControl)
        End If
    End Sub

    Private Sub btnKelolaPostHapus_Click(sender As Object, e As EventArgs) Handles btnKelolaPostHapus.Click
        If licensePackage IsNot Nothing AndAlso Not licensePackage.ModulRegistered.Contains(ModuleRegistration.Hapus_FBMP.ToString()) Then
            Process.Start(linkTrial)
            Return
        End If
        InActiveKelolaPostButton()
        ActiveButton(True, btnKelolaPostHapus)

        Dim tabName = "DeletePost"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim reNewPostControl As New ReNewPostControl(FiturManagePostEnum.Delete)
            createNewTab(tabName, reNewPostControl)
        End If
    End Sub

    Private Sub btnInteraksi_Click(sender As Object, e As EventArgs) Handles btnInteraksi.Click
        If licensePackage IsNot Nothing AndAlso Not licensePackage.ModulRegistered.Contains(ModuleRegistration.Interaksi.ToString()) Then
            Process.Start(linkTrial)
            Return
        End If
        HidePanelMenu()
        InActiveAllButtonLvl1()

        pnlInteraksiIndc.Visible = True
        ActiveButton(True, btnInteraksi)

        Dim tabName = "InteractionFB"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim interactionFBControl As New InteractionFBControl()
            createNewTab(tabName, interactionFBControl)
        End If
    End Sub

    Private Sub btnKonfigurasi_Click(sender As Object, e As EventArgs) Handles btnKonfigurasi.Click
        HidePanelMenu()
        InActiveAllButtonLvl1()

        pnlKonfigurasiIndc.Visible = True
        ActiveButton(True, btnKonfigurasi)

        Dim tabName = "SettingGeneral"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim settingGeneral As New SettingGeneral()
            createNewTab(tabName, settingGeneral)
        End If
    End Sub

    Private Sub FormBase_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If isLogout Then
            DialogResult = DialogResult.Cancel
        Else
            DialogResult = DialogResult.OK
        End If
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        HidePanelMenu()
        InActiveAllButtonLvl1()

        pnlLoginIndc.Visible = True
        ActiveButton(True, btnLogin)

        Dim tabName = "LoginFB"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim loginControl As New LoginFacebook()
            createNewTab(tabName, loginControl)
        End If
    End Sub

    Public Sub CloseAndRemoveTabPage(tabName As String)

        Dim index As Integer = findTab(tabName)
        Dim tabPage As TabPage = FindTabPageByName(tabName)
        ' Pastikan tabPage bukan Nothing dan tabControl memiliki tabPage
        If tabPage IsNot Nothing AndAlso TabControl.TabPages.Contains(tabPage) Then
            ' Menutup tabPage (jika ingin mengeksekusi kode khusus saat menutup tabPage, tempatkan kode di sini)
            tabPage.Dispose()

            ' Menghapus tabPage dari koleksi TabPages
            TabControl.TabPages.Remove(tabPage)

            tabInfoList.RemoveAt(index)
            Dim i As Integer = 0
            For Each tabInfo In (From tabInfo2 In tabInfoList Order By TabIndex)
                tabInfo.tabIndex = i
                i += 1
            Next
        End If
    End Sub

    Private Function FindTabPageByName(tabName As String) As TabPage
        ' Cari tabPage berdasarkan nama
        Dim index As Integer = findTab(tabName)

        ' Jika ditemukan, kembalikan tabPage; jika tidak, kembalikan Nothing
        If index >= 0 Then
            Return TabControl.TabPages(index)
        Else
            Return Nothing
        End If
    End Function
End Class
