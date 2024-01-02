#Region " Impors Namespace form 2"
Imports System.Net
Imports System.Net.NetworkInformation
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium.Remote
Imports OpenQA.Selenium.Support.UI
Imports OpenQA.Selenium.Support.Extensions
Imports SeleniumUndetectedChromeDriver
Imports SeleniumExtras.WaitHelpers
Imports System.Collections.ObjectModel
Imports Microsoft.VisualBasic.FileIO
Imports System.Web
Imports System.IO
Imports System.Threading
Imports System.Environment
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Net.Http
Imports System.Configuration
Imports OpenQA.Selenium.Interactions
Imports System.Text.RegularExpressions
#End Region

Public Class FormBase_old
    Public versi = "1.4 Update 24 Nov 2023"
#Region "public Properti"
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
#End Region

    Public Class tabInfo
        Public Property tabAppName As String
        Public Property tabIndex As Integer
    End Class

    Public Property tabInfoList As List(Of tabInfo) = New List(Of tabInfo)()
#Region "public Function"
    'runChromeDriver di gunakan untuk mendeklarasikan browser berdasarkan profile yang dipilih
    'func ini di gunakan seluruh userControl
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

        Return Profiles.Find(Function(p) p.ProfileName = profileName)
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

        Return Profiles.Find(Function(p) p.ProfileName = profileName)
    End Function


    Public Function sleep(second As Integer)
        For i = 0 To second * 100
            System.Threading.Thread.Sleep(10)
            Application.DoEvents()
        Next
        Return True
    End Function

#End Region

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

#Region "Event Control"
    Private Sub FormBase_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If isLogout Then
            DialogResult = DialogResult.Cancel
        Else
            DialogResult = DialogResult.OK
        End If
    End Sub
    Private Sub FormBase_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
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

    Private Sub FormBase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        '/// ==============
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
    End Sub


    Public Function findTab(tabName As String) As Integer
        For Each tabInf In tabInfoList
            If tabInf.tabAppName = tabName Then
                Return tabInf.tabIndex
            End If
        Next
        Return -1
    End Function
    '//event click sesuai menu yang di pilih dengan mengubah index dari TabControl sesuai dengan module/usercontrol 
    '//yang telah di inisialisasikan pada event load, index di dapat sesuai urutan dari atas ke bawah
    Private Sub HOMEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HOMEToolStripMenuItem.Click
        Dim tabIndex = findTab("Home")
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        End If
    End Sub

    Private Sub VIDEOINPUTDATACSVPRODUKUMUMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOINPUTDATACSVPRODUKUMUMToolStripMenuItem.Click
        Dim tabName = "PostGeneral"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim postControl As New PostFBGeneralPostControl(FiturPostFBEnum.General)
            createNewTab(tabName, postControl)
        End If
    End Sub

    Private Sub FITURLOGINFACEBOOKToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FITURLOGINFACEBOOKToolStripMenuItem.Click
        Dim tabName = "LoginFB"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim loginControl As New LoginFacebook()
            createNewTab(tabName, loginControl)
        End If
    End Sub

    Private Sub VIDEOJALANKANFITURPOSTFBMPPRODUKUMUMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOJALANKANFITURPOSTFBMPPRODUKUMUMToolStripMenuItem.Click
        Dim tabName = "GeneralDraft"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim draftControl As New PostFBGeneralPostControl(FiturPostFBEnum.OnlyDraft)
            createNewTab(tabName, draftControl)
        End If
    End Sub

    Private Sub UPLOADFITURANTIDUPLIKATBUKANHERBALToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UPLOADFITURANTIDUPLIKATBUKANHERBALToolStripMenuItem.Click
        Dim tabName = "PostAntiDuplicate"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim antiDuplicateControl As New PostFBGeneralPostControl(FiturPostFBEnum.DraftAndPost)
            createNewTab(tabName, antiDuplicateControl)
        End If
    End Sub

    Private Sub FITURADDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FITURADDToolStripMenuItem.Click

    End Sub
    Private Sub PERBAHARUIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PERBAHARUIToolStripMenuItem.Click
        Dim tabName = "ReNewPost"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim reNewPostControl As New ReNewPostControl(FiturManagePostEnum.Renew)
            createNewTab(tabName, reNewPostControl)
        End If
    End Sub

    Private Sub FITUREDITToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FITUREDITToolStripMenuItem.Click
    End Sub

    Private Sub BALASPESANToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BALASPESANToolStripMenuItem.Click
        Dim tabName = "MessageReplyer"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim messageReplyerControl As New MessageReplyerControl()
            createNewTab(tabName, messageReplyerControl)
        End If
    End Sub

    Private Sub POSTINGGRUPFACEBOOKVERSIWEBBUKANLITEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles POSTINGGRUPFACEBOOKVERSIWEBBUKANLITEToolStripMenuItem.Click
        Dim tabName = "PostGroupFB"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim postGroupFBControl As New PostGroupFBControl()
            createNewTab(tabName, postGroupFBControl)
        End If
    End Sub

    Private Sub FITURSCRAPEToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim tabName = "ScrapDataUserGroupFB"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim scrapDataUserGroupFBControl As New ScrapDataUserGroupFBControl()
            createNewTab(tabName, scrapDataUserGroupFBControl)
        End If
    End Sub

    Private Sub CONTOHFORMToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'TabControl.SelectedIndex = 9
    End Sub

    Private Sub UPLOADFITURBIASAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UPLOADFITURBIASAToolStripMenuItem.Click
        Dim tabName = "PostFBProperti"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim postFBPropertiControl As New PostFBPropertiControl(FiturPostFBEnum.General)
            createNewTab(tabName, postFBPropertiControl)
        End If
    End Sub

    Private Sub UPLOADFITURDRAFToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UPLOADFITURDRAFToolStripMenuItem.Click
        Dim tabName = "DraftFBProperti"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim draftFBPropertiControl As New PostFBPropertiControl(FiturPostFBEnum.OnlyDraft)
            createNewTab(tabName, draftFBPropertiControl)
        End If
    End Sub

    Private Sub UPLOADFITURANTIDUPLIKATToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UPLOADFITURANTIDUPLIKATToolStripMenuItem.Click
        Dim tabName = "DraftAndPostFBProperti"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim draftAndPostFBPropertiControl As New PostFBPropertiControl(FiturPostFBEnum.DraftAndPost)
            createNewTab(tabName, draftAndPostFBPropertiControl)
        End If
    End Sub

    Private Sub UPLOADFITURBIASAToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles UPLOADFITURBIASAToolStripMenuItem1.Click
        Dim tabName = "PostFBMobil"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim postFBMobilControl As New PostFBMobilControl(FiturPostFBEnum.General)
            createNewTab(tabName, postFBMobilControl)
        End If
    End Sub

    Private Sub UPLOADFITURDRAFToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles UPLOADFITURDRAFToolStripMenuItem1.Click
        Dim tabName = "DraftFBMobil"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim draftFBMobilControl As New PostFBMobilControl(FiturPostFBEnum.OnlyDraft)
            createNewTab(tabName, draftFBMobilControl)
        End If
    End Sub

    Private Sub UPLOADFITURANTIDUPLIKATToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles UPLOADFITURANTIDUPLIKATToolStripMenuItem1.Click
        Dim tabName = "DraftAndPostFBMobil"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim draftAndostFBMobilControl As New PostFBMobilControl(FiturPostFBEnum.DraftAndPost)
            createNewTab(tabName, draftAndostFBMobilControl)
        End If
    End Sub

    Private Sub UPLOADFITURBIASAToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles UPLOADFITURBIASAToolStripMenuItem2.Click
        Dim tabName = "PostFBMotor"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim postFBMotorControl As New PostFBMotorControl(FiturPostFBEnum.General)
            createNewTab(tabName, postFBMotorControl)
        End If
    End Sub

    Private Sub UPLOADFITURDRAFToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles UPLOADFITURDRAFToolStripMenuItem2.Click
        Dim tabName = "DraftFBMotor"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim draftFBMotorControl As New PostFBMotorControl(FiturPostFBEnum.OnlyDraft)
            createNewTab(tabName, draftFBMotorControl)
        End If
    End Sub

    Private Sub UPLOADFITURANTIDUPLIKATToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles UPLOADFITURANTIDUPLIKATToolStripMenuItem2.Click
        Dim tabName = "DraftAndPostFBMotor"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim draftAndPostFBMotorControl As New PostFBMotorControl(FiturPostFBEnum.DraftAndPost)
            createNewTab(tabName, draftAndPostFBMotorControl)
        End If
    End Sub

    Private Sub FiturInteraksiMenuItem_Click(sender As Object, e As EventArgs) Handles FiturInteraksiMenuItem.Click
        Dim tabName = "InteractionFB"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim interactionFBControl As New InteractionFBControl()
            createNewTab(tabName, interactionFBControl)
        End If
    End Sub

    Private Sub POSTINGFBMPVERSILITEMTOUCHMODEBARBARAKUNBARUToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles POSTINGFBMPVERSILITEMTOUCHMODEBARBARAKUNBARUToolStripMenuItem.Click
        Dim tabName = "PostFBLite"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim postFBLiteControl As New PostFBLiteControl()
            createNewTab(tabName, postFBLiteControl)
        End If
    End Sub

    Private Sub KONFIGURASIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KONFIGURASIToolStripMenuItem.Click
        Dim tabName = "SettingGeneral"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim settingGeneral As New SettingGeneral()
            createNewTab(tabName, settingGeneral)
        End If
    End Sub

    Private Sub KIRIMPESANToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KIRIMPESANToolStripMenuItem.Click
        Dim tabName = "SenderMessage"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim control As New MessageSenderControl()
            createNewTab(tabName, control)
        End If
    End Sub

    Private Sub HAPUSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HAPUSToolStripMenuItem.Click
        Dim tabName = "DeletePost"
        Dim tabIndex = findTab(tabName)
        If tabIndex > -1 Then
            TabControl.SelectedIndex = tabIndex
        Else
            Dim reNewPostControl As New ReNewPostControl(FiturManagePostEnum.Delete)
            createNewTab(tabName, reNewPostControl)
        End If
    End Sub

    Private Sub POSTINGSTATUSBERANDAFACEBOOKPERSONALToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TabControl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl.SelectedIndexChanged

    End Sub

    Private Sub DOWNLOADCSVBAHANPOSTINGPRODUKUMUMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DOWNLOADCSVBAHANPOSTINGPRODUKUMUMToolStripMenuItem.Click
        Dim webAddress As String = "https://drive.google.com/file/d/1xumaCDlEzMyPQ4riM6rEf4ojRSHeQS2w/view?usp=sharing"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALCARAINPUTCSVBAHANPOSTINGToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALCARAINPUTCSVBAHANPOSTINGToolStripMenuItem.Click
        Dim webAddress As String = "https://www.youtube.com/watch?v=OdxJLhBWAFM"
        Process.Start(webAddress)

    End Sub

    Private Sub VIDEOTUTORIALJALANKANFITURUPLOADPRODUKUMUMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALJALANKANFITURUPLOADPRODUKUMUMToolStripMenuItem.Click
        Dim webAddress As String = "https://www.youtube.com/watch?v=ekZrUrGXvNE"
        Process.Start(webAddress)
    End Sub

    Private Sub DOWNLOADCSVBAHANINPUTAKUNFACEBOOKToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DOWNLOADCSVBAHANINPUTAKUNFACEBOOKToolStripMenuItem.Click
        Dim webAddress As String = "https://drive.google.com/file/d/1N9yAH6YRYb2a2KVibiOJx4gAVC05qWw1/view?usp=sharing"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALCARALOGINAKUNFACEBOOKToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALCARALOGINAKUNFACEBOOKToolStripMenuItem.Click
        Dim webAddress As String = "https://youtu.be/bKd_JFcn4Lk"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALCARACEKAKUNFACEBOOKFACEBOOKSETELAHLOGINToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALCARACEKAKUNFACEBOOKFACEBOOKSETELAHLOGINToolStripMenuItem.Click
        Dim webAddress As String = "https://youtu.be/gZlql8ZEXtE"
        Process.Start(webAddress)
    End Sub

    Private Sub DOWNLOADCSVBAHANPOSTINGGRUPFACEBOOKToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DOWNLOADCSVBAHANPOSTINGGRUPFACEBOOKToolStripMenuItem.Click
        Dim webAddress As String = "https://drive.google.com/file/d/1EgDxdrd-DOOtnI2bug1wNLft5_ovUN5u/view?usp=sharing"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALINPUTCSVBAHANPOSTINGGRUPFACEBOOKToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALINPUTCSVBAHANPOSTINGGRUPFACEBOOKToolStripMenuItem.Click
        Dim webAddress As String = "https://www.youtube.com/watch?v=31Am9IcFCYE"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALJALANKANFITURPOSTINGGRUPFACEBOOKToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALJALANKANFITURPOSTINGGRUPFACEBOOKToolStripMenuItem.Click
        Dim webAddress As String = "https://www.youtube.com/watch?v=LIJVNFdi5ps"
        Process.Start(webAddress)
    End Sub

    Private Sub DOWNLOADCSVBAHANPOSTINGVERSILITEBARBARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DOWNLOADCSVBAHANPOSTINGVERSILITEBARBARToolStripMenuItem.Click
        Dim webAddress As String = "https://drive.google.com/file/d/100jwk5L2rBijw85knYa-Khp-oblk_iBK/view?usp=sharing"
        Process.Start(webAddress)
    End Sub

    Private Sub DOWNLOADCSVBAHANPOSTINGMOTORToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DOWNLOADCSVBAHANPOSTINGMOTORToolStripMenuItem.Click
        Dim webAddress As String = "https://drive.google.com/file/d/1KoFt-cp5ye9_kVMD8VsceT-VBiRXA0EC/view?usp=sharing"
        Process.Start(webAddress)
    End Sub

    Private Sub DOWNLOADCSVBAHANPOSTINGMOBILToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DOWNLOADCSVBAHANPOSTINGMOBILToolStripMenuItem.Click
        Dim webAddress As String = "https://drive.google.com/file/d/1KoFt-cp5ye9_kVMD8VsceT-VBiRXA0EC/view?usp=sharing"
        Process.Start(webAddress)
    End Sub

    Private Sub DOWNLOADCSVBAHANPOSTINGPRODUKUMUMToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        Dim webAddress As String = "https://drive.google.com/file/d/1yjVxXr9DBSete1DW-RZRXIGqYw14B5CP/view?usp=sharing"
        Process.Start(webAddress)
    End Sub

    Private Sub DOWNLOADCSVBAHANPOSTINGToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim webAddress As String = "https://drive.google.com/file/d/1oje-tiNB64aYqV-Xp-GN-G0UqBxbuhXl/view?usp=sharing"
        Process.Start(webAddress)
    End Sub

    Private Sub DOWNLOADCSVBAHANPOSTINGPROPERTIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DOWNLOADCSVBAHANPOSTINGPROPERTIToolStripMenuItem.Click
        Dim webAddress As String = "https://drive.google.com/file/d/16sIXCD4bYt1jGvMtqaejGT_XVd3-nKUM/view?usp=sharing"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALHAPUSAKUNFACEBOOKDIBANNEDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALHAPUSAKUNFACEBOOKDIBANNEDToolStripMenuItem.Click
        Dim webAddress As String = "https://youtu.be/1AVnA0BW7_A"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALINPUTCSVBAHANPOSTINGMOTORToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALINPUTCSVBAHANPOSTINGMOTORToolStripMenuItem.Click
        Dim webAddress As String = "https://youtu.be/EQFDbEleQB4"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALINPUTCSVBAHANPOSTINGMOBILToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALINPUTCSVBAHANPOSTINGMOBILToolStripMenuItem.Click
        Dim webAddress As String = "https://youtu.be/asnKSM4g_Qc"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALINPUTCSVBAHANPOSTINGVERSILITEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALINPUTCSVBAHANPOSTINGVERSILITEToolStripMenuItem.Click
        Dim webAddress As String = "https://youtu.be/QNye6JOK4RE"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALJALANKANFITURUPLOADMOTORToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALJALANKANFITURUPLOADMOTORToolStripMenuItem.Click
        Dim webAddress As String = "https://youtu.be/mN820uPvfss"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALJALANKANFITURUPLOADMOBILToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALJALANKANFITURUPLOADMOBILToolStripMenuItem.Click
        Dim webAddress As String = "https://youtu.be/2MvpE7xp0I8"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALINPUTCSVBAHANPOSTINGPROPERTIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALINPUTCSVBAHANPOSTINGPROPERTIToolStripMenuItem.Click
        Dim webAddress As String = "https://youtu.be/1dXrtluoG5w"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALJALANKANFITURUPLOADPROPERTIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALJALANKANFITURUPLOADPROPERTIToolStripMenuItem.Click
        Dim webAddress As String = "https://youtu.be/Y6vbjAvzXRs"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALJALANKANFITURBALASPESANToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALJALANKANFITURBALASPESANToolStripMenuItem.Click
        Dim webAddress As String = "https://youtu.be/NbnMn8UQb3A"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALJALANKANFITURUPLOADToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALJALANKANFITURUPLOADToolStripMenuItem.Click
        Dim webAddress As String = "https://youtu.be/6KP_oNYT0fE"
        Process.Start(webAddress)
    End Sub

    Private Sub DOWNLOADCSVBAHANINPUTDATABALASPESANFBMPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DOWNLOADCSVBAHANINPUTDATABALASPESANFBMPToolStripMenuItem.Click
        Dim webAddress As String = "https://drive.google.com/file/d/1yjVxXr9DBSete1DW-RZRXIGqYw14B5CP/view?usp=sharing"
        Process.Start(webAddress)
    End Sub

    Private Sub DOWNLOADCSVBAHANBLASTMESSENGERFACEBOOKPERSONALToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DOWNLOADCSVBAHANBLASTMESSENGERFACEBOOKPERSONALToolStripMenuItem.Click
        Dim webAddress As String = "https://drive.google.com/file/d/1oje-tiNB64aYqV-Xp-GN-G0UqBxbuhXl/view?usp=sharing"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALJALANKANFITURKONFIGURASIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALJALANKANFITURKONFIGURASIToolStripMenuItem.Click
        Dim webAddress As String = "https://youtu.be/uLGcF8Gk42c"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALJALANKANFITURPERBAHARUIPOSTFBMPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALJALANKANFITURPERBAHARUIPOSTFBMPToolStripMenuItem.Click
        Dim webAddress As String = "https://youtu.be/hLU-4VLDgFA"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALJALANKANFITURHAPUSPOSTFBMPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALJALANKANFITURHAPUSPOSTFBMPToolStripMenuItem.Click
        Dim webAddress As String = "https://www.youtube.com/watch?v=_zN0xnAcZoQ"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALJALANKANFITURUPLOADVERSILITEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALJALANKANFITURUPLOADVERSILITEToolStripMenuItem.Click
        Dim webAddress As String = "https://youtu.be/bI9MW4_f7yQe"
        Process.Start(webAddress)
    End Sub

    Private Sub VIDEOTUTORIALJALANKANFITURINTERAKSIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIDEOTUTORIALJALANKANFITURINTERAKSIToolStripMenuItem.Click
        Dim webAddress As String = "https://youtu.be/i5cAViwYAi4"
        Process.Start(webAddress)

    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub
#End Region
End Class