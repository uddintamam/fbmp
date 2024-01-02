Imports SeleniumUndetectedChromeDriver
Imports System.Threading
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports SeleniumExtras.WaitHelpers
Imports System.IO
Imports OpenQA.Selenium.Chrome
Imports System.Environment
Imports System.Configuration
Imports System.Net.NetworkInformation

Public Enum FiturPostFBEnum
    General
    OnlyDraft
    DraftAndPost
End Enum

Public Enum FiturManagePostEnum
    Renew
    Delete
End Enum

Public Enum ModuleRegistration
    FBMP_Umum
    FBMP_Motor
    FBMP_Mobil
    FBMP_Properti
    FBMP_Lite
    POST_Group
    Balas_Pesan
    Kirim_Pesan
    Perbaharui_FBMP
    Hapus_FBMP
    Interaksi
End Enum


Public Class ChromeProfile

#Region "Public Property"
    Public Property ProfileName As String
    Public Property AccountCode As String
    Public Property Driver As UndetectedChromeDriver
    Public Property IsOnProcess As Boolean

    Public Shared ReadOnly locationProfile As String = ConfigurationManager.AppSettings("binProfile")

    Public Shared ReadOnly binDebugPath As String = Application.StartupPath
    Public Shared ReadOnly FolderChromeCustom As String = GetFolderPath(SpecialFolder.LocalApplicationData) + "\Google\Chrome\"
    Public Shared ReadOnly FixCustom As String = locationProfile + "\Profile"
    Public Shared ReadOnly MyDoumentProfile As String = locationProfile + "\Profile"
    Public Shared ReadOnly dataUser As String = locationProfile & "\UserData.xml"
    Public Shared ReadOnly waitElement As Integer = CInt(ConfigurationManager.AppSettings("waitElement"))
#End Region

#Region "Contructor"
    Public Sub New(profileName As String, accountCode As String, driver As UndetectedChromeDriver, isOnProcess As Boolean, windowsSize As Integer)
        Me.ProfileName = profileName
        Me.AccountCode = accountCode
        Me.Driver = CreateDriver(windowsSize, accountCode)
        Me.IsOnProcess = isOnProcess
    End Sub

    Public Sub New(profileName As String, accountCode As String, driver As UndetectedChromeDriver, isOnProcess As Boolean)
        Me.ProfileName = profileName
        Me.AccountCode = accountCode
        Me.Driver = CreateLiteDriver(accountCode)
        Me.IsOnProcess = isOnProcess
    End Sub

#End Region

#Region "Kumpulan Fungsi yang bisa di gunakan dimana saja"
    Public Function CreateDriver(windowsSize As Integer, accountCode As String) As UndetectedChromeDriver
        Dim newDriver As UndetectedChromeDriver = Nothing
        Dim options As ChromeOptions = New ChromeOptions()
        '======== BACA PROFILE ==============
        options.AddArgument("--user-data-dir=" + MyDoumentProfile + "\" + accountCode)
        'End If
        'options.AddArgument("--window-size=500,650")
        If windowsSize = 0 Then
            Dim random As New Random()
            Dim randomX As Integer = random.Next(0, Screen.PrimaryScreen.Bounds.Width - 800) ' 800 adalah lebar jendela
            Dim randomY As Integer = random.Next(0, Screen.PrimaryScreen.Bounds.Height - 600) ' 600 adalah tinggi jendela

            options.AddArgument($"--window-position={randomX},{randomY}")
            options.AddArgument("--window-size=500,650")
        Else
            options.AddArgument("--start-maximized")
        End If
        ' options.AddArgument("--start-maximized")
        options.AddArgument("--disable-popup-blocking")

        options.AddArgument("--disable-notifications")
        options.AddArgument("--disable-infobars")
        options.AddArgument("--mute-audio")

        Dim timeout As TimeSpan = TimeSpan.FromSeconds(300)
        options.AddAdditionalOption("ms:inactivityTimeout", CInt(timeout.TotalMilliseconds))

        Dim fixchromeDriverPath = Path.Combine(binDebugPath, String.Concat("chromedriver.exe"))
        ' KODINGAN TAMBAH CHROME DRIVER JIKA BELUM ADA
        'If Not File.Exists(fixchromeDriverPath) Then
        '    File.Copy(Path.Combine(binDebugPath, String.Concat("chromedriver.exe")), fixchromeDriverPath)
        'End If
        Dim chrome_path = Path.Combine(binDebugPath + "\chrome-win\chrome-win", "chrome.exe")
        Dim prefs = New Dictionary(Of String, Object)()
        sleep(1)

        ' Inisialisasi ChromeDriver
        Try
            newDriver = UndetectedChromeDriver.Create(hideCommandPromptWindow:=True, driverExecutablePath:=fixchromeDriverPath, browserExecutablePath:=chrome_path, options:=options)
        Catch ex As Exception
            MessageBox.Show("Harap Tutup browser profile " & accountCode, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
        Return newDriver
    End Function

    Public Function CreateLiteDriver(accountCode As String) As UndetectedChromeDriver
        Dim newDriver As UndetectedChromeDriver = Nothing
        Dim options As ChromeOptions = New ChromeOptions()
        '======== BACA PROFILE ==============
        options.AddArgument("--user-data-dir=" + MyDoumentProfile + "\" + accountCode)

        options.AddArgument("--user-agent=Mozilla/5.0 (Linux; Android 8.1.0; Pixel C Build/OPM8.190605.005; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/89.0.4389.105 Safari/537.36 [FB_IAB/FB4A;FBAV/312.0.0.45.117;]")
        options.AddArgument("--window-size=500,650")

        options.AddArgument("--disable-notifications")
        options.AddArgument("--disable-infobars")
        options.AddArgument("--mute-audio")

        Dim timeout As TimeSpan = TimeSpan.FromSeconds(300)
        options.AddAdditionalOption("ms:inactivityTimeout", CInt(timeout.TotalMilliseconds))

        Dim fixchromeDriverPath = Path.Combine(binDebugPath, String.Concat("chromedriver.exe"))
        ' KODINGAN TAMBAH CHROME DRIVER JIKA BELUM ADA
        'If Not File.Exists(fixchromeDriverPath) Then
        '    File.Copy(Path.Combine(binDebugPath, String.Concat("chromedriver.exe")), fixchromeDriverPath)
        'End If
        Dim chrome_path = Path.Combine(binDebugPath + "\chrome-win\chrome-win", "chrome.exe")
        Dim prefs = New Dictionary(Of String, Object)()
        sleep(1)

        ' Inisialisasi ChromeDriver
        Try
            newDriver = UndetectedChromeDriver.Create(hideCommandPromptWindow:=True, driverExecutablePath:=fixchromeDriverPath, browserExecutablePath:=chrome_path, options:=options)
        Catch ex As Exception
            MessageBox.Show("Harap Tutup browser profile " & accountCode, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
        Return newDriver
    End Function

    Public Function sleep(second As Integer)
        For i = 0 To second * 100
            System.Threading.Thread.Sleep(10)
            Application.DoEvents()
        Next
        Return True
    End Function

    Public Function CheckInternetConnection() As Boolean
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

    Public Function LoginProcess(UserId As String, Password As String, jedarandom As Integer) As StatusPost
        Dim statusPost As New StatusPost()
        Dim elementList As IReadOnlyCollection(Of IWebElement) =
                               Driver.FindElements(By.XPath("//form[contains(@action, '/logout.php')]"))
        ' Cek apakah elemen ada atau tidak ada
        If elementList.Count > 0 Then
            ' Elemen ada (exist)
            'elementList(0).Submit()
            'Thread.Sleep(jedarandom * 2)
            statusPost.Status = True
            Return statusPost
        End If

        Try
            If UserId IsNot Nothing Then

                Try
                    Dim waitforelement As WebDriverWait = New WebDriverWait(Driver, TimeSpan.FromSeconds(waitElement))
                    waitforelement.Until(ExpectedConditions.ElementExists(By.Id("email")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "halaman Login tidak tersedia"
                    Return statusPost
                End Try

                elementList =
Driver.FindElements(By.Id("email"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    elementList(0).SendKeys(UserId)
                End If

                Thread.Sleep(jedarandom)

                elementList =
Driver.FindElements(By.Id("pass"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    ' Suntikkan (inject) teks ke elemen input password
                    elementList(0).SendKeys(Password)

                    Thread.Sleep(jedarandom)

                    elementList =
Driver.FindElements(By.XPath("//button[@type='submit']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        statusPost.Status = True
                    End If
                End If

                Thread.Sleep(jedarandom)

                elementList =
                        Driver.FindElements(By.XPath("//input[@name='approvals_code']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    statusPost.Status = False
                    statusPost.Message = "Belum Verifikasi"
                    Return statusPost
                End If

                elementList =
                    Driver.FindElements(By.XPath("//div[contains(@aria-label, 'Messenger')][contains(@role, 'button')]"))
                If elementList.Count = 0 Then
                    statusPost.Status = False
                    statusPost.Message = "Akun Di Banned"
                    Return statusPost
                End If
            End If
        Catch ex As Exception
            statusPost.Status = False
            statusPost.Message = "Login Gagal"
        End Try
        Return statusPost
    End Function

    Public Function CheckLoginProcess(dataRow As DataRow) As StatusPost
        Dim statusPost As New StatusPost()
        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing

        Dim isValid = 0

        Dim isLogin = True
        Try
            elementList =
                               Driver.FindElements(By.XPath("//form[contains(@action, '/logout.php')]"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 Then
                Thread.Sleep(1000)
                Dim elementList1 As IReadOnlyCollection(Of IWebElement) =
                                  Driver.FindElements(By.XPath("//div[contains(@aria-label, 'Messenger')][contains(@role, 'button')]"))

                ' Cek apakah elemen ada atau tidak ada
                If elementList1.Count = 0 Then
                    isValid = 2
                Else
                    isValid = 1
                End If
            End If
        Catch ex As Exception
            isValid = 3
        End Try

        If isValid = 0 Or isValid = 3 Then
            statusPost.Status = False
            statusPost.Message = "Akun belum login, Harap cek login kembali"

            dataRow(3) = 0
            updateData(dataRow)
            Return statusPost
        ElseIf isValid = 2 Then
            statusPost.Status = False
            statusPost.Message = "Akun Bibanned"

            dataRow(3) = 2
            updateData(dataRow)
            Return statusPost
        End If

        statusPost.Status = True
        Return statusPost
    End Function

    Public Function LoginLiteProcess(UserId As String, Password As String, jedarandom As Integer) As StatusPost
        Dim statusPost As New StatusPost()

        Driver.GoToUrl("https://m.facebook.com/bookmarks")

        Dim elementList As IReadOnlyCollection(Of IWebElement) =
                               Driver.FindElements(By.XPath("//a[contains(@href, '/logout.php')]"))
        ' Cek apakah elemen ada atau tidak ada
        If elementList.Count > 0 Then
            ' Elemen ada (exist)
            'elementList(0).Click()

            'Thread.Sleep(jedarandom * 2)
            statusPost.Status = True
            Return statusPost
        End If

        Try
            If UserId IsNot Nothing Then
                Try
                    Dim waitforelement As WebDriverWait = New WebDriverWait(Driver, TimeSpan.FromSeconds(waitElement))
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//input[@name ='email']")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "halaman Login tidak tersedia"
                    Return statusPost
                End Try

                elementList =
Driver.FindElements(By.XPath("//input[@name ='email']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    elementList(0).SendKeys(UserId)
                End If

                Thread.Sleep(jedarandom)

                elementList =
Driver.FindElements(By.XPath("//input[@name ='pass']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    ' Suntikkan (inject) teks ke elemen input password
                    elementList(0).SendKeys(Password)

                    Thread.Sleep(jedarandom)

                    elementList =
Driver.FindElements(By.XPath("//button[@type='button'][@name='login']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                    End If

                    elementList =
Driver.FindElements(By.XPath("//button[@type='submit'][@value ='Oke']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                    End If
                End If
            End If
            statusPost.Status = True
        Catch ex As Exception
            statusPost.Status = False
            statusPost.Message = "Login Gagal"
        End Try
        Return statusPost
    End Function

    Public Function CheckLoginLiteProcess(dataRow As DataRow) As StatusPost
        Dim statusPost As New StatusPost()


        Dim isValid = False

        Try
            Driver.GoToUrl("https://m.facebook.com/bookmarks")

            Dim elementList As IReadOnlyCollection(Of IWebElement) =
                               Driver.FindElements(By.XPath("//a[contains(@href, '/logout.php')]"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 Then
                isValid = True
            End If
        Catch ex As Exception
        End Try

        If Not isValid Then
            statusPost.Status = False
            statusPost.Message = "Akun Di Banned / belum login, Harap cek login kembali"

            dataRow(3) = 0
            updateData(dataRow)
            Return statusPost
        End If
        Driver.GoToUrl("https://m.facebook.com")

        statusPost.Status = True
        Return statusPost

    End Function

    Public Sub updateProgress(form As UserControl, userId As String, Progress As Integer)
        Try
            ' Loop melalui setiap kontrol dalam UserControl.Controls
            For Each ctrl As Control In form.Controls
                ' Periksa apakah kontrol saat ini adalah DataGridView dan memiliki nama "gridProfile"
                If TypeOf ctrl Is TabControl AndAlso ctrl.Name = "TabControl1" Then
                    ' Mengonversi kontrol ke tipe DataGridView
                    Dim tabControl As TabControl = DirectCast(ctrl, TabControl)
                    If tabControl.TabCount > 0 Then
                        For Each tabPage As TabPage In tabControl.Controls
                            If tabPage.Controls.Count > 0 AndAlso TypeOf tabPage.Controls(0) Is DataGridView AndAlso
                                tabPage.Controls(0).Name = "gridProfile" Then
                                Dim gridProfile As DataGridView = DirectCast(tabPage.Controls(0), DataGridView)

                                For Each row As DataGridViewRow In gridProfile.Rows
                                    If row.Cells("userIdforPostCol").Value = userId Then
                                        ' Ditemukan baris yang sesuai, update nilai progress bar
                                        gridProfile.Invoke(Sub() row.Cells("ProgressCol").Value = Progress)
                                        Exit For
                                    End If
                                Next
                            End If
                        Next
                    End If
                    Exit For
                ElseIf TypeOf ctrl Is DataGridView AndAlso ctrl.Name = "gridProfile" Then
                    Dim gridProfile As DataGridView = DirectCast(ctrl, DataGridView)
                    For Each row As DataGridViewRow In gridProfile.Rows
                        If row.Cells("userIdforPostCol").Value = userId Then
                            ' Ditemukan baris yang sesuai, update nilai progress bar
                            gridProfile.Invoke(Sub() row.Cells("ProgressCol").Value = Progress)
                            Exit For
                        End If
                    Next
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Sub updateProgress(ByRef gridProfile As DataGridView, userId As String, Progress As Integer)
        Try
            For Each row As DataGridViewRow In gridProfile.Rows
                If row.Cells("userIdforPostCol").Value = userId Then
                    ' Ditemukan baris yang sesuai, update nilai progress bar
                    gridProfile.Invoke(Sub() row.Cells("ProgressCol").Value = Progress)
                    Exit For
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Sub updateData(dataProfile As DataRow)
        If File.Exists(ChromeProfile.dataUser) Then
            Dim dataSet As New DataSet
            dataSet.ReadXml(ChromeProfile.dataUser)
            ' Periksa apakah targetUserId sudah ada dalam data
            Dim userTable As DataTable = dataSet.Tables("UserData")

            Dim userId As String = dataProfile(0)
            Dim isLogin As Integer = dataProfile(3)
            For Each rowtable As DataRow In userTable.Rows
                If rowtable("UserId").ToString() = userId Then
                    rowtable("IsLogin") = isLogin
                    Exit For
                End If
            Next

            dataSet.WriteXml(ChromeProfile.dataUser)
        End If
    End Sub
#End Region

    '==============================================================
    '//Fungsi-fungsi proses Robot

#Region "Kumpulan Fungsi Posting Umum/ Draft / posting & Draft"
    '//Function untuk Posting Umum
    Public Function PostGeneralFB(jedarandom As Integer, fields As String(), waitforelement As WebDriverWait, categoryProd As Integer,
                                  conditionProd As Integer, ByRef form As DataGridView, userId As String, startRow As Integer, endRow As Integer,
                                  ByRef suspendEvent As ManualResetEvent) As StatusPost
        Dim statusPost As New StatusPost()

        Dim KataKunciProduk As String = String.Empty
        Dim HargaProduknya As Integer = 0
        Dim KeteranganProduk As String = String.Empty
        Dim LokasiProdukku As String = String.Empty
        Dim Foto1 As String = String.Empty
        Dim Foto2 As String = String.Empty
        Dim Foto3 As String = String.Empty
        Dim Foto4 As String = String.Empty
        Dim Foto5 As String = String.Empty
        Dim Foto6 As String = String.Empty
        Dim Foto7 As String = String.Empty
        Dim Foto8 As String = String.Empty
        Dim Foto9 As String = String.Empty
        Dim Foto10 As String = String.Empty
        Dim LabelProduk As String = String.Empty
        Dim JedaPost As Integer = 0
        Dim JedaDraf As Integer = 0
        Dim prevProgress As Integer = CInt((startRow - 1) * 100 / endRow)

        Dim delayInterval As Integer = 10 ' Penundaan dalam milidetik (10 ms)

        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing

        Try
            If fields.Length >= 17 Then
                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 1 / 5 / endRow))
                WaitHandle.WaitAny({suspendEvent})

                '====================================
                '//masuk ke halaman marketplace/create
                Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                Thread.Sleep(jedarandom * 3)
                WaitHandle.WaitAny({suspendEvent})

                elementList =
    Driver.FindElements(By.XPath("//span[contains (text(),'Meninjau Permintaan')]"))

                If elementList.Count > 0 Then
                    statusPost.Status = False
                    statusPost.Message = "Peninjauan Belum disejui Facebook"

                    Return statusPost
                End If
                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains (@href,'/marketplace/create/item/')]")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                    Return statusPost
                End Try

                elementList =
    Driver.FindElements(By.XPath("//a[contains (@href,'/marketplace/create/item/')]"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    elementList(0).Click()
                Else
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                    Return statusPost
                End If
                'Driver.GoToUrl("https://www.facebook.com/marketplace/create/item")
                Thread.Sleep(jedarandom * 5)
                WaitHandle.WaitAny({suspendEvent})
                '====================================

#Region "Validasi Halaman sebelum proses input"

                elementList =
Driver.FindElements(By.XPath("//span[text()='Maaf, ada masalah']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then


                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If

                Thread.Sleep(jedarandom)
                WaitHandle.WaitAny({suspendEvent})

                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//input[@type='file']")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                    Return statusPost
                End Try

                elementList =
Driver.FindElements(By.XPath("//input[@type='file']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count = 0 Then
                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If
#End Region

                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 2 / 5 / endRow))
                WaitHandle.WaitAny({suspendEvent})

                If Not String.IsNullOrEmpty(fields(0)) Then

                    KataKunciProduk = fields(0)
                    HargaProduknya = Convert.ToInt64(fields(1))
                    KeteranganProduk = fields(2)
                    LokasiProdukku = fields(3)
                    Foto1 = fields(4)
                    Foto2 = fields(5)
                    Foto3 = fields(6)
                    Foto4 = fields(7)
                    Foto5 = fields(8)
                    Foto6 = fields(9)
                    Foto7 = fields(10)
                    Foto8 = fields(11)
                    Foto9 = fields(12)
                    Foto10 = fields(13)
                    LabelProduk = fields(14)
                    JedaPost = Convert.ToInt64(fields(15))
                    JedaDraf = Convert.ToInt64(fields(16))

                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

#Region "Validasi Halaman sebelum proses input tahap 2"

                    elementList =
Driver.FindElements(By.XPath("//span[text()='Batas tercapai']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        Thread.Sleep(jedarandom)

                        statusPost.Status = False
                        statusPost.Message = "Batas Posting di marketplace sudah tercapai"

                        Return statusPost
                    End If

#End Region

#Region "Proses Input dan Inject ke elment berdasarkan CSV"
                    'driver.Navigate.Refresh()
                    ' FOTO PRODUK VERSI LITE
                    elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                    If elementList.Count = 0 Then
                        statusPost.Status = False
                        statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                        Return statusPost
                    End If

                    Dim fotoValue As String = String.Empty

                    Try
                        ' INPUT FOTO PRODUK POST FBMP
                        Dim fotoList As New List(Of String)()
                        For x As Integer = 1 To 10
                            Dim fotoField As String = fields(x + 3)
                            If Not String.IsNullOrEmpty(fotoField) Then
                                If Not File.Exists(fotoField) Then
                                    statusPost.Status = False
                                    statusPost.Message = "File FOTO " & x & " Tidak ditemukan (Saran simpan foto di folder document)"

                                    Return statusPost
                                End If

                                fotoList.Add(fotoField)
                            End If
                        Next
                        If fotoList.Count > 0 Then
                            Dim fotoString As String = String.Join(vbNewLine, fotoList)
                            Dim inputFoto As IWebElement = Driver.FindElement(By.XPath("//input[@type='file']"))
                            inputFoto.SendKeys(fotoString)
                            Thread.Sleep(fotoList.Count)
                        End If
                    Catch ex As Exception
                    End Try
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    '// KATA KUNCI PRODK 
                    elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        form.Invoke(Sub() Clipboard.SetText(KataKunciProduk))
                        elementList(0).Click()
                        elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                    End If

                    'If elementList.Count > 0 Then
                    '    form.Invoke(Sub() Clipboard.SetText(KataKunciProduk))
                    '    elementList(0).Click()
                    '    elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                    'End If
                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 3 / 5 / endRow))
                    WaitHandle.WaitAny({suspendEvent})

                    'driver.FindElement(By.XPath("//input[@type='text']")).SendKeys(KataKunciProduk)
                    Thread.Sleep(jedarandom)
                    '// HARGA PRODUK
                    If elementList.Count > 1 Then
                        elementList(1).SendKeys(HargaProduknya)
                    End If
                    'driver.FindElement(By.XPath("//input[@name='price']")).SendKeys(HargaProduknya)
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})
                    '// KATEGORI PRODUK VERSI LITE 
                    elementList = Driver.FindElements(By.XPath("//label[@aria-label='Kategori']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                    End If
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    Dim cateSelect As Integer = 0

                    If categoryProd <= 4 Then
                        cateSelect = categoryProd + 2
                    ElseIf categoryProd > 4 AndAlso categoryProd <= 6 Then
                        cateSelect = categoryProd + 3
                    ElseIf categoryProd > 6 AndAlso categoryProd <= 10 Then
                        cateSelect = categoryProd + 4
                    ElseIf categoryProd > 10 AndAlso categoryProd <= 14 Then
                        cateSelect = categoryProd + 5
                    ElseIf categoryProd > 14 AndAlso categoryProd <= 16 Then
                        cateSelect = categoryProd + 6
                    ElseIf categoryProd > 16 AndAlso categoryProd <= 22 Then
                        cateSelect = categoryProd + 7
                    ElseIf categoryProd > 22 AndAlso categoryProd <= 24 Then
                        cateSelect = categoryProd + 8
                    End If

                    elementList = Driver.FindElements(
                    By.XPath("//div[@role='dialog']/div/div/div/span/div/div[" & cateSelect.ToString() & "]/div[@role='button']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                    End If
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    elementList = Driver.FindElements(
                    By.XPath("//label[@aria-label='Kondisi']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()

                        Thread.Sleep(jedarandom)

                        elementList = Driver.FindElements(
                        By.XPath("//div[@role='listbox']/div/div/div/div/div[1]/div/div[" & (conditionProd + 1).ToString() & "]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(0).Click()
                        End If
                    End If

                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    '// KETERANGAN PRODUK
                    elementList = Driver.FindElements(
                        By.XPath("//label[@aria-label='Keterangan']/div/div/textarea"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        form.Invoke(Sub() Clipboard.SetText(KeteranganProduk))
                        elementList(0).Click()
                        elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                        'For Each karakter As Char In KeteranganProduk
                        '    elementList(0).SendKeys(karakter) ' Kirim karakter ke elemen input
                        '    Thread.Sleep(delayInterval) ' Tunggu sebentar
                        'Next
                    End If
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})
                    '// LABEL PRODUK
                    elementList = Driver.FindElements(
                                By.XPath("//*[@aria-label='Label produk']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).SendKeys(LabelProduk)
                    End If

                    'Thread.Sleep(jedarandom)

                    Thread.Sleep(jedarandom * 2)
                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 4 / 5 / endRow))
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    '// LOKASI PRODUK
                    Dim ulangi As Boolean = True
                    Dim alamatStr = LokasiProdukku
                    Do While ulangi
                        elementList = Driver.FindElements(
                        By.XPath("//input[@aria-label='Lokasi']"))
                        If elementList.Count > 0 Then

                            Thread.Sleep(jedarandom)
                            elementList(0).SendKeys(Keys.Control & "a") ' Memilih semua teks dalam elemen
                            Thread.Sleep(jedarandom)
                            elementList(0).SendKeys(Keys.Delete)
                            Thread.Sleep(jedarandom)
                            elementList(0).SendKeys(alamatStr)
                            Thread.Sleep(jedarandom)
                            elementList = Driver.FindElements(
                          By.XPath("//ul[contains (@aria-label,'disarankan')]/li"))
                            ' Cek apakah elemen ada atau tidak ada
                            If elementList.Count > 0 Then
                                elementList(0).Click()
                                ulangi = False
                                Thread.Sleep(jedarandom)
                            Else
                                If alamatStr.Length <> 0 Then
                                    alamatStr = alamatStr.Substring(0, alamatStr.Length - 1)
                                Else
                                    ulangi = False
                                End If
                            End If

                        End If
                    Loop

                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                        By.XPath("//div[@aria-label='Berikutnya']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        If elementList(0).Enabled Then
                            elementList(0).Click()
                        End If
                    End If

                    Thread.Sleep(jedarandom * 2)
                    WaitHandle.WaitAny({suspendEvent})

                    Try
                        waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//div[@aria-label='Publikasikan']")))
                    Catch ex As WebDriverTimeoutException
                        statusPost.Status = False
                        statusPost.Message = "tidak bisa mempublikasikan"
                        Return statusPost
                    End Try
                    elementList = Driver.FindElements(
                            By.XPath("//div[@aria-label='Publikasikan']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                    Else
                        statusPost.Status = False
                        statusPost.Message = "Gagal Saat Posting"

                        Return statusPost
                    End If
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    elementList = Driver.FindElements(
                        By.XPath("//div[@aria-label='Tutup']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 AndAlso elementList(0).Enabled Then
                        Dim ariaDisabledValue As String = elementList(0).GetAttribute("aria-disabled")
                        If ariaDisabledValue IsNot Nothing AndAlso
                            ariaDisabledValue.ToLower() <> "true" Then
                            elementList(0).Click()
                        End If
                    End If

                    Thread.Sleep(jedarandom * JedaPost)
                    WaitHandle.WaitAny({suspendEvent})

#End Region

                End If

                statusPost.Status = True
                statusPost.Message = String.Empty
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = "kolom pada CSV tidak memenuhi kriteria"
                Return statusPost
            End If
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try

        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function

    '//Function untuk membuat Draft Baru
    Public Function PostGeneralOnlyDraftFB(jedarandom As Integer, fields As String(), waitforelement As WebDriverWait, categoryProd As Integer,
                                           conditionProd As Integer, form As DataGridView, userId As String, startRow As Integer, endRow As Integer,
                                           ByRef suspendEvent As ManualResetEvent) As StatusPost
        Dim statusPost As New StatusPost()
        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing
        Dim delayInterval As Integer = 10 ' Penundaan dalam milidetik (10 ms)

        Dim KataKunciProduk As String = String.Empty
        Dim HargaProduknya As Integer = 0
        Dim KeteranganProduk As String = String.Empty
        Dim LokasiProdukku As String = String.Empty
        Dim Foto1 As String = String.Empty
        Dim Foto2 As String = String.Empty
        Dim Foto3 As String = String.Empty
        Dim Foto4 As String = String.Empty
        Dim Foto5 As String = String.Empty
        Dim Foto6 As String = String.Empty
        Dim Foto7 As String = String.Empty
        Dim Foto8 As String = String.Empty
        Dim Foto9 As String = String.Empty
        Dim Foto10 As String = String.Empty
        Dim LabelProduk As String = String.Empty
        Dim JedaPost As Integer = 0
        Dim JedaDraf As Integer = 0
        WaitHandle.WaitAny({suspendEvent})

        Dim prevProgress As Integer = CInt((startRow - 1) * 100 / endRow)
        Try
            If fields.Length >= 17 Then

                '====================================
                '//masuk ke halaman marketplace/create
                Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                Thread.Sleep(jedarandom * 3)
                WaitHandle.WaitAny({suspendEvent})
                elementList =
    Driver.FindElements(By.XPath("//span[contains (text(),'Meninjau Permintaan')]"))

                If elementList.Count > 0 Then
                    statusPost.Status = False
                    statusPost.Message = "Peninjauan Belum disejui Facebook"

                    Return statusPost
                End If
                WaitHandle.WaitAny({suspendEvent})
                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains (@href,'/marketplace/create/item/')]")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                    Return statusPost
                End Try
                WaitHandle.WaitAny({suspendEvent})
                elementList =
    Driver.FindElements(By.XPath("//a[contains (@href,'/marketplace/create/item/')]"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    elementList(0).Click()
                Else
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                    Return statusPost
                End If
                Thread.Sleep(jedarandom * 5)
                WaitHandle.WaitAny({suspendEvent})
                '======================================


                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 1 / 5 / endRow))


#Region "Validasi Halaman sebelum proses input"
                elementList =
    Driver.FindElements(By.XPath("//span[text()='Maaf, ada masalah']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If

                Thread.Sleep(jedarandom)
                WaitHandle.WaitAny({suspendEvent})

                elementList =
    Driver.FindElements(By.XPath("//input[@type='file']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count = 0 Then
                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If
#End Region

                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 2 / 5 / endRow))
                WaitHandle.WaitAny({suspendEvent})
                If Not String.IsNullOrEmpty(fields(0)) Then
                    KataKunciProduk = fields(0)
                    HargaProduknya = Convert.ToInt64(fields(1))
                    KeteranganProduk = fields(2)
                    LokasiProdukku = fields(3)
                    Foto1 = fields(4)
                    Foto2 = fields(5)
                    Foto3 = fields(6)
                    Foto4 = fields(7)
                    Foto5 = fields(8)
                    Foto6 = fields(9)
                    Foto7 = fields(10)
                    Foto8 = fields(11)
                    Foto9 = fields(12)
                    Foto10 = fields(13)
                    LabelProduk = fields(14)
                    JedaPost = Convert.ToInt64(fields(15))
                    JedaDraf = Convert.ToInt64(fields(16))
                    '// PILIH CSV NYA PAKAI APA
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

#Region "Validasi Halaman sebelum proses input tahap 2"
                    '// JIKA POSTINGAN BATAS TERCAPAI MAKA MUNCUL NOTIF
                    elementList =
    Driver.FindElements(By.XPath("//span[text()='Batas tercapai']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        Thread.Sleep(jedarandom)

                        statusPost.Status = False
                        statusPost.Message = "Batas Posting di marketplace sudah tercapai"

                        Return statusPost
                    End If
#End Region
                    WaitHandle.WaitAny({suspendEvent})

#Region "Proses Input dan Inject ke elment berdasarkan CSV"
                    Thread.Sleep(jedarandom)
                    Try
                        Try
                            waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//input[@type='file']")))
                        Catch ex As WebDriverTimeoutException
                            statusPost.Status = False
                            statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                            Return statusPost
                        End Try
                        WaitHandle.WaitAny({suspendEvent})

                        elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                        If elementList.Count = 0 Then
                            statusPost.Status = False
                            statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                            Return statusPost
                        End If
                        ' INPUT FOTO PRODUK POST FBMP
                        Dim fotoList As New List(Of String)()
                        For x As Integer = 1 To 10
                            Dim fotoField As String = fields(x + 3)
                            If Not String.IsNullOrEmpty(fotoField) Then
                                If Not File.Exists(fotoField) Then
                                    statusPost.Status = False
                                    statusPost.Message = "File FOTO " & x & " Tidak ditemukan (Saran simpan foto di folder document)"

                                    Return statusPost
                                End If

                                fotoList.Add(fotoField)
                            End If
                        Next
                        If fotoList.Count > 0 Then
                            Dim fotoString As String = String.Join(vbNewLine, fotoList)
                            Dim inputFoto As IWebElement = Driver.FindElement(By.XPath("//input[@type='file']"))
                            inputFoto.SendKeys(fotoString)
                            Thread.Sleep(fotoList.Count)
                        End If
                    Catch ex As Exception
                    End Try
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})
                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 3 / 5 / endRow))
                    '// KATA KUNCI PRODK 
                    elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        form.Invoke(Sub() Clipboard.SetText(KataKunciProduk))
                        elementList(0).Click()
                        elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks

                    End If
                    'If elementList.Count > 0 Then
                    '    form.Invoke(Sub() Clipboard.SetText(KataKunciProduk))
                    '    elementList(0).Click()
                    '    elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                    'End If

                    WaitHandle.WaitAny({suspendEvent})
                    Thread.Sleep(jedarandom)
                    '// HARGA PRODUK
                    'elementList = driver.FindElements(By.XPath("//*[@aria-label='Harga']"))
                    If elementList.Count > 1 Then
                        elementList(1).SendKeys(HargaProduknya)
                    End If
                    'driver.FindElement(By.XPath("//input[@name='price']")).SendKeys(HargaProduknya)
                    WaitHandle.WaitAny({suspendEvent})
                    Thread.Sleep(jedarandom)
                    '// KATEGORI PRODUK 
                    elementList = Driver.FindElements(By.XPath("//label[@aria-label='Kategori']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    Thread.Sleep(jedarandom)

                    Dim cateSelect As Integer = 0

                    If categoryProd <= 4 Then
                        cateSelect = categoryProd + 2
                    ElseIf categoryProd > 4 AndAlso categoryProd <= 6 Then
                        cateSelect = categoryProd + 3
                    ElseIf categoryProd > 6 AndAlso categoryProd <= 10 Then
                        cateSelect = categoryProd + 4
                    ElseIf categoryProd > 10 AndAlso categoryProd <= 14 Then
                        cateSelect = categoryProd + 5
                    ElseIf categoryProd > 14 AndAlso categoryProd <= 16 Then
                        cateSelect = categoryProd + 6
                    ElseIf categoryProd > 16 AndAlso categoryProd <= 22 Then
                        cateSelect = categoryProd + 7
                    ElseIf categoryProd > 22 AndAlso categoryProd <= 24 Then
                        cateSelect = categoryProd + 8
                    End If

                    elementList = Driver.FindElements(
                            By.XPath("//div[@role='dialog']/div/div/div/span/div/div[" & cateSelect.ToString() & "]/div[@role='button']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                    End If
                    WaitHandle.WaitAny({suspendEvent})
                    Thread.Sleep(jedarandom)
                    '// KONDISI PRODUK
                    elementList = Driver.FindElements(
                           By.XPath("//label[@aria-label='Kondisi']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                                By.XPath("//div[@role='listbox']/div/div/div/div/div[1]/div/div[" & (conditionProd + 1).ToString() & "]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(0).Click()
                        End If
                    End If

                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 4 / 5 / endRow))

                    WaitHandle.WaitAny({suspendEvent})
                    Thread.Sleep(jedarandom)
                    '// KETERANGAN PRODUK
                    elementList = Driver.FindElements(
                                By.XPath("//label[@aria-label='Keterangan']/div/div/textarea"))
                    ' Cek apakah elemen ada atau tidak ada
                    'If elementList.Count > 0 Then
                    '    For Each karakter As Char In KeteranganProduk
                    '        elementList(0).SendKeys(karakter) ' Kirim karakter ke elemen input
                    '        Thread.Sleep(delayInterval) ' Tunggu sebentar
                    '    Next
                    'End If

                    If elementList.Count > 0 Then
                        form.Invoke(Sub() Clipboard.SetText(KeteranganProduk))
                        elementList(0).Click()
                        elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                    End If


                    WaitHandle.WaitAny({suspendEvent})
                    Thread.Sleep(jedarandom)
                    '// LABEL PRODUK
                    elementList = Driver.FindElements(
                                By.XPath("//*[@aria-label='Label produk']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).SendKeys(LabelProduk)
                    End If

                    'Thread.Sleep(jedarandom)

                    Thread.Sleep(jedarandom * 2)

                    '// KLIK SIMPAN DRAF
                    elementList = Driver.FindElements(
                                                  By.XPath("//div[contains(@aria-label,'Simpan draf')]"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        Try
                            elementList(0).Click()
                        Catch ex As Exception

                        End Try
                    End If

                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    elementList = Driver.FindElements(
                                By.XPath("//div[@aria-label='Tutup']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 AndAlso elementList(0).Enabled Then
                        Dim ariaDisabledValue As String = elementList(0).GetAttribute("aria-disabled")
                        If ariaDisabledValue IsNot Nothing AndAlso
                            ariaDisabledValue.ToLower() <> "true" Then
                            elementList(0).Click()
                        End If
                    End If
                    Thread.Sleep(jedarandom * JedaDraf)
                    WaitHandle.WaitAny({suspendEvent})
#End Region
                End If

                statusPost.Status = True
                statusPost.Message = String.Empty
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = "kolom pada CSV tidak memenuhi kriteria"
                Return statusPost
            End If
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try
        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function

    '//Function untuk edit Draft dan Posting
    Public Function EditGeneraFromDraftFB(jedarandom As Integer, fields As String(), waitforelement As WebDriverWait, categoryProd As Integer,
                                          conditionProd As Integer, form As DataGridView, userId As String, startRow As Integer, endRow As Integer,
                                          ByRef suspendEvent As ManualResetEvent) As StatusPost
        Dim statusPost As New StatusPost()
        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing
        Dim delayInterval As Integer = 10 ' Penundaan dalam milidetik (10 ms)
        Dim prevProgress As Integer = CInt((startRow - 1) * 100 / endRow)
        WaitHandle.WaitAny({suspendEvent})

        Try
            If fields.Length >= 17 Then
                Dim KataKunciProduk As String = fields(0)
                Dim LokasiProdukku As String = fields(3)
                Dim JedaPost As Integer = Convert.ToInt64(fields(15))
                Dim UrlPost As String = "https://www.facebook.com"

                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 1 / 5 / endRow))
                WaitHandle.WaitAny({suspendEvent})

                Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                Thread.Sleep(jedarandom * 3)
                WaitHandle.WaitAny({suspendEvent})
                Driver.Navigate.Refresh()
                Thread.Sleep(jedarandom * 5)
                WaitHandle.WaitAny({suspendEvent})

                elementList =
    Driver.FindElements(By.XPath("//span[contains (text(),'Meninjau Permintaan')]"))

                If elementList.Count > 0 Then
                    statusPost.Status = False
                    statusPost.Message = "Peninjauan Belum disejui Facebook"

                    Return statusPost
                End If
                WaitHandle.WaitAny({suspendEvent})


                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains (@href,'/marketplace/create/item/')]")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                    Return statusPost
                End Try
                WaitHandle.WaitAny({suspendEvent})

                '// KLIK PILIH DRAFT
                elementList = Driver.FindElements(
                                              By.XPath("//a[contains(@href,'/marketplace/edit/?listing_id')]"))

                If elementList.Count > 0 Then
                    '//Pilih Draft jika tersedia
                    elementList(0).Click()
                    UrlPost = Driver.Url
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})
                    Driver.Navigate.Refresh()
                    Thread.Sleep(jedarandom * 2)
                    WaitHandle.WaitAny({suspendEvent})
                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 2 / 5 / endRow))

                    'Proses Posting Dari Draft
                    statusPost = postFromDreaft(jedarandom, LokasiProdukku, JedaPost, UrlPost, suspendEvent)
                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 4 / 5 / endRow))
                    WaitHandle.WaitAny({suspendEvent})

                    Return statusPost
                Else
                    '//Buat Draft Baru jika tidak menemukan Draft
                    statusPost = PostGeneralOnlyDraftFB(jedarandom, fields, waitforelement, categoryProd, conditionProd, form, userId, startRow, endRow, suspendEvent)

                    If statusPost.Status Then
                        Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                        WaitHandle.WaitAny({suspendEvent})
                        Thread.Sleep(jedarandom * 3)
                        Driver.Navigate.Refresh()
                        Thread.Sleep(jedarandom * 3)
                        WaitHandle.WaitAny({suspendEvent})

                        Try
                            waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains(@href,'/marketplace/edit/?listing_id')]")))
                        Catch ex As WebDriverTimeoutException
                            statusPost.Status = False
                            statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                            Return statusPost
                        End Try
                        WaitHandle.WaitAny({suspendEvent})

                        '// KLIK PILIH DRAFT
                        elementList = Driver.FindElements(
                                                      By.XPath("//a[contains(@href,'/marketplace/edit/?listing_id')]"))

                        If elementList.Count > 0 Then
                            elementList(0).Click()
                            UrlPost = Driver.Url
                            Thread.Sleep(jedarandom)
                            WaitHandle.WaitAny({suspendEvent})

                            postFromDreaft(jedarandom, LokasiProdukku, JedaPost, UrlPost, suspendEvent)
                            WaitHandle.WaitAny({suspendEvent})
                        End If
                    Else
                        Return statusPost
                    End If
                End If
            Else
                statusPost.Status = False
                statusPost.Message = "kolom pada CSV tidak memenuhi kriteria"
                Return statusPost
            End If
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try
        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function


    '//Function Posting Dari Edit Draft
    Public Function postFromDreaft(jedarandom As Integer, LokasiProdukku As String, JedaPost As Integer, UrlPost As String, ByRef suspendEvent As ManualResetEvent) As StatusPost
        Dim statusPost As New StatusPost()
        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing
        Dim waitforelement As WebDriverWait = New WebDriverWait(Driver, TimeSpan.FromSeconds(waitElement))

        Try
            Driver.GoToUrl(UrlPost)
            Thread.Sleep(jedarandom * 2)
            WaitHandle.WaitAny({suspendEvent})
            Driver.Navigate.Refresh()
            Thread.Sleep(jedarandom * 2)
            WaitHandle.WaitAny({suspendEvent})

            elementList =
    Driver.FindElements(By.XPath("//span[text()='Maaf, ada masalah']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 Then
                Thread.Sleep(jedarandom)

                statusPost.Status = False
                statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                Return statusPost
            End If
            WaitHandle.WaitAny({suspendEvent})

            Try
                waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//input[@type='file']")))
            Catch ex As WebDriverTimeoutException
                statusPost.Status = False
                statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                Return statusPost
            End Try
            WaitHandle.WaitAny({suspendEvent})

            elementList =
    Driver.FindElements(By.XPath("//input[@type='file']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count = 0 Then
                Thread.Sleep(jedarandom)

                statusPost.Status = False
                statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                Return statusPost
            End If

            WaitHandle.WaitAny({suspendEvent})
            '// LOKASI PRODUK
            Dim ulangi As Boolean = True
            Dim alamatStr = LokasiProdukku
            Do While ulangi
                elementList = Driver.FindElements(
                    By.XPath("//input[@aria-label='Lokasi']"))
                If elementList.Count > 0 Then

                    Thread.Sleep(jedarandom)
                    elementList(0).SendKeys(Keys.Control & "a") ' Memilih semua teks dalam elemen
                    Thread.Sleep(jedarandom)
                    elementList(0).SendKeys(Keys.Delete)
                    Thread.Sleep(jedarandom)
                    elementList(0).SendKeys(alamatStr)
                    Thread.Sleep(jedarandom)
                    elementList = Driver.FindElements(
                          By.XPath("//ul[contains (@aria-label,'disarankan')]/li"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        ulangi = False
                        Thread.Sleep(jedarandom)
                    Else
                        If alamatStr.Length <> 0 Then
                            alamatStr = alamatStr.Substring(0, alamatStr.Length - 1)
                        Else
                            ulangi = False
                        End If
                    End If

                End If
            Loop

            Thread.Sleep(jedarandom)
            WaitHandle.WaitAny({suspendEvent})

            elementList = Driver.FindElements(
                    By.XPath("//div[@aria-label='Berikutnya']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 Then
                If elementList(0).Enabled Then
                    elementList(0).Click()
                End If
            End If

            Thread.Sleep(jedarandom)
            WaitHandle.WaitAny({suspendEvent})

            Try
                waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//div[@aria-label='Publikasikan']")))
            Catch ex As WebDriverTimeoutException
                statusPost.Status = False
                statusPost.Message = "Tidak Bisa Mempublikasikan"

                Return statusPost
            End Try

            WaitHandle.WaitAny({suspendEvent})
            elementList = Driver.FindElements(
                            By.XPath("//div[@aria-label='Publikasikan']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 Then
                elementList(0).Click()
            Else
                statusPost.Status = False
                statusPost.Message = "Gagal Saat Posting"

                Return statusPost
            End If

            Thread.Sleep(jedarandom)
            WaitHandle.WaitAny({suspendEvent})

            elementList = Driver.FindElements(
                    By.XPath("//div[@aria-label='Tutup']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 AndAlso elementList(0).Enabled Then
                Dim ariaDisabledValue As String = elementList(0).GetAttribute("aria-disabled")
                If ariaDisabledValue IsNot Nothing AndAlso
                            ariaDisabledValue.ToLower() <> "true" Then
                    elementList(0).Click()
                End If
            End If

            Thread.Sleep(jedarandom * JedaPost)
            WaitHandle.WaitAny({suspendEvent})
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try
        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function

#End Region

#Region "Kumpulan Fungsi Posting Contoh diambil dari (Umum/ Draft / posting & Draft)"
    '//Function untuk Posting Umum
    Public Function PostContohFB(jedarandom As Integer, fields As String(), waitforelement As WebDriverWait, categoryProd As Integer, conditionProd As Integer, form As DataGridView, userId As String, startRow As Integer, endRow As Integer) As StatusPost
        Dim statusPost As New StatusPost()

        Dim KataKunciProduk As String = String.Empty
        Dim HargaProduknya As Integer = 0
        Dim KeteranganProduk As String = String.Empty
        Dim LokasiProdukku As String = String.Empty
        Dim Foto1 As String = String.Empty
        Dim Foto2 As String = String.Empty
        Dim Foto3 As String = String.Empty
        Dim Foto4 As String = String.Empty
        Dim Foto5 As String = String.Empty
        Dim Foto6 As String = String.Empty
        Dim Foto7 As String = String.Empty
        Dim Foto8 As String = String.Empty
        Dim Foto9 As String = String.Empty
        Dim Foto10 As String = String.Empty
        Dim LabelProduk As String = String.Empty
        Dim JedaPost As Integer = 0
        Dim JedaDraf As Integer = 0

        Dim prevProgress As Integer = CInt((startRow - 1) * 100 / endRow)
        Dim delayInterval As Integer = 10 ' Penundaan dalam milidetik (10 ms)

        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing

        Try
            If fields.Length >= 17 Then

                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 1 / 5 / endRow))

                '====================================
                '//masuk ke halaman marketplace/create
                Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                Thread.Sleep(jedarandom * 3)
                elementList =
    Driver.FindElements(By.XPath("//span[contains (text(),'Meninjau Permintaan')]"))

                If elementList.Count > 0 Then
                    statusPost.Status = False
                    statusPost.Message = "Peninjauan Belum disejui Facebook"

                    Return statusPost
                End If
                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains (@href,'/marketplace/create/item/')]")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                    Return statusPost
                End Try
                elementList =
    Driver.FindElements(By.XPath("//a[contains (@href,'/marketplace/create/item/')]"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    elementList(0).Click()
                Else
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                    Return statusPost
                End If
                Thread.Sleep(jedarandom * 5)
                '====================================

#Region "Validasi Halaman sebelum proses input"

                elementList =
    Driver.FindElements(By.XPath("//span[text()='Maaf, ada masalah']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then


                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If

                Thread.Sleep(jedarandom)

                elementList =
    Driver.FindElements(By.XPath("//input[@type='file']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count = 0 Then
                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If
#End Region

                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 2 / 5 / endRow))

                If Not String.IsNullOrEmpty(fields(0)) Then

                    KataKunciProduk = fields(0)
                    HargaProduknya = Convert.ToInt64(fields(1))
                    KeteranganProduk = fields(2)
                    LokasiProdukku = fields(3)
                    Foto1 = fields(4)
                    Foto2 = fields(5)
                    Foto3 = fields(6)
                    Foto4 = fields(7)
                    Foto5 = fields(8)
                    Foto6 = fields(9)
                    Foto7 = fields(10)
                    Foto8 = fields(11)
                    Foto9 = fields(12)
                    Foto10 = fields(13)
                    LabelProduk = fields(14)
                    JedaPost = Convert.ToInt64(fields(15))
                    JedaDraf = Convert.ToInt64(fields(16))

                    Thread.Sleep(jedarandom)

#Region "Validasi Halaman sebelum proses input tahap 2"

                    elementList =
    Driver.FindElements(By.XPath("//span[text()='Batas tercapai']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        Thread.Sleep(jedarandom)

                        statusPost.Status = False
                        statusPost.Message = "Batas Posting di marketplace sudah tercapai"

                        Return statusPost
                    End If

#End Region

#Region "Proses Input dan Inject ke elment berdasarkan CSV"
                    'driver.Navigate.Refresh()
                    ' FOTO PRODUK VERSI LITE
                    Try
                        waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//input[@type='file']")))
                    Catch ex As WebDriverTimeoutException
                        statusPost.Status = False
                        statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                        Return statusPost
                    End Try

                    elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                    If elementList.Count = 0 Then
                        statusPost.Status = False
                        statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                        Return statusPost
                    End If

                    Dim fotoValue As String = String.Empty

                    Try
                        ' INPUT FOTO PRODUK POST FBMP
                        Dim fotoList As New List(Of String)()
                        For x As Integer = 1 To 10
                            Dim fotoField As String = fields(x + 3)
                            If Not String.IsNullOrEmpty(fotoField) Then
                                If Not File.Exists(fotoField) Then
                                    statusPost.Status = False
                                    statusPost.Message = "File FOTO " & x & " Tidak ditemukan (Saran simpan foto di folder document)"

                                    Return statusPost
                                End If

                                fotoList.Add(fotoField)
                            End If
                        Next
                        If fotoList.Count > 0 Then
                            Dim fotoString As String = String.Join(vbNewLine, fotoList)
                            Dim inputFoto As IWebElement = Driver.FindElement(By.XPath("//input[@type='file']"))
                            inputFoto.SendKeys(fotoString)
                            Thread.Sleep(fotoList.Count)
                        End If
                    Catch ex As Exception
                    End Try
                    Thread.Sleep(jedarandom)

                    '// KATA KUNCI PRODK 
                    elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        form.Invoke(Sub() Clipboard.SetText(KataKunciProduk))
                        elementList(0).Click()
                        elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                    End If
                    'If elementList.Count > 0 Then
                    '    form.Invoke(Sub() Clipboard.SetText(KataKunciProduk))
                    '    elementList(0).Click()
                    '    elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                    'End If

                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 3 / 5 / endRow))

                    'driver.FindElement(By.XPath("//input[@type='text']")).SendKeys(KataKunciProduk)
                    Thread.Sleep(jedarandom)
                    '// HARGA PRODUK
                    If elementList.Count > 1 Then
                        elementList(1).SendKeys(HargaProduknya)
                    End If
                    'driver.FindElement(By.XPath("//input[@name='price']")).SendKeys(HargaProduknya)
                    Thread.Sleep(jedarandom)
                    '// KATEGORI PRODUK VERSI LITE 
                    elementList = Driver.FindElements(By.XPath("//label[@aria-label='Kategori']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                    End If
                    Thread.Sleep(jedarandom)

                    Dim cateSelect As Integer = 0

                    If categoryProd <= 4 Then
                        cateSelect = categoryProd + 2
                    ElseIf categoryProd > 4 AndAlso categoryProd <= 6 Then
                        cateSelect = categoryProd + 3
                    ElseIf categoryProd > 6 AndAlso categoryProd <= 10 Then
                        cateSelect = categoryProd + 4
                    ElseIf categoryProd > 10 AndAlso categoryProd <= 14 Then
                        cateSelect = categoryProd + 5
                    ElseIf categoryProd > 14 AndAlso categoryProd <= 16 Then
                        cateSelect = categoryProd + 6
                    ElseIf categoryProd > 16 AndAlso categoryProd <= 22 Then
                        cateSelect = categoryProd + 7
                    ElseIf categoryProd > 22 AndAlso categoryProd <= 24 Then
                        cateSelect = categoryProd + 8
                    End If

                    elementList = Driver.FindElements(
                    By.XPath("//div[@role='dialog']/div/div/div/span/div/div[" & cateSelect.ToString() & "]/div[@role='button']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                    End If
                    Thread.Sleep(jedarandom)

                    elementList = Driver.FindElements(
                    By.XPath("//label[@aria-label='Kondisi']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()

                        Thread.Sleep(jedarandom)

                        elementList = Driver.FindElements(
                        By.XPath("//div[@role='listbox']/div/div/div/div/div[1]/div/div[" & (conditionProd + 1).ToString() & "]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(0).Click()
                        End If
                    End If

                    Thread.Sleep(jedarandom)

                    '// KETERANGAN PRODUK
                    elementList = Driver.FindElements(
                        By.XPath("//label[@aria-label='Keterangan']/div/div/textarea"))
                    ' Cek apakah elemen ada atau tidak ada
                    'If elementList.Count > 0 Then
                    '    For Each karakter As Char In KeteranganProduk
                    '        elementList(0).SendKeys(karakter) ' Kirim karakter ke elemen input
                    '        Thread.Sleep(delayInterval) ' Tunggu sebentar
                    '    Next
                    'End If
                    If elementList.Count > 0 Then
                        form.Invoke(Sub() Clipboard.SetText(KeteranganProduk))
                        elementList(0).Click()
                        elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                    End If

                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 4 / 5 / endRow))
                    Thread.Sleep(jedarandom)

                    '// LOKASI PRODUK
                    Dim ulangi As Boolean = True
                    Dim alamatStr = LokasiProdukku
                    Do While ulangi
                        elementList = Driver.FindElements(
                            By.XPath("//input[@aria-label='Lokasi']"))
                        If elementList.Count > 0 Then

                            Thread.Sleep(jedarandom)
                            elementList(0).SendKeys(Keys.Control & "a") ' Memilih semua teks dalam elemen
                            Thread.Sleep(jedarandom)
                            elementList(0).SendKeys(Keys.Delete)
                            Thread.Sleep(jedarandom)
                            elementList(0).SendKeys(alamatStr)
                            Thread.Sleep(jedarandom)
                            elementList = Driver.FindElements(
                          By.XPath("//ul[contains (@aria-label,'disarankan')]/li"))
                            ' Cek apakah elemen ada atau tidak ada
                            If elementList.Count > 0 Then
                                elementList(0).Click()
                                ulangi = False
                                Thread.Sleep(jedarandom)
                            Else
                                If alamatStr.Length <> 0 Then
                                    alamatStr = alamatStr.Substring(0, alamatStr.Length - 1)
                                Else
                                    ulangi = False
                                End If
                            End If

                        End If
                    Loop

                    Thread.Sleep(jedarandom)
                    elementList = Driver.FindElements(
                        By.XPath("//div[@aria-label='Berikutnya']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        If elementList(0).Enabled Then
                            elementList(0).Click()
                        End If
                    End If

                    Thread.Sleep(jedarandom)

                    Try
                        waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//div[@aria-label='Publikasikan']")))
                    Catch ex As WebDriverTimeoutException
                        statusPost.Status = False
                        statusPost.Message = "tidak bisa mempublikasikan"
                        Return statusPost
                    End Try

                    elementList = Driver.FindElements(
                        By.XPath("//div[@aria-label='Publikasikan']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                    Else
                        statusPost.Status = False
                        statusPost.Message = "Gagal Saat Posting"

                        Return statusPost
                    End If
                    Thread.Sleep(jedarandom)

                    elementList = Driver.FindElements(
                        By.XPath("//div[@aria-label='Tutup']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 AndAlso elementList(0).Enabled Then
                        Dim ariaDisabledValue As String = elementList(0).GetAttribute("aria-disabled")
                        If ariaDisabledValue IsNot Nothing AndAlso
                        ariaDisabledValue.ToLower() <> "true" Then
                            elementList(0).Click()
                        End If
                    End If

                    Thread.Sleep(jedarandom * JedaPost)

#End Region

                End If
                statusPost.Status = True
                statusPost.Message = String.Empty
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = "kolom pada CSV tidak memenuhi kriteria"
                Return statusPost
            End If
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try
        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function

    '//Function untuk membuat Draft Baru
    Public Function PostContohOnlyDraftFB(jedarandom As Integer, fields As String(), waitforelement As WebDriverWait, categoryProd As Integer, conditionProd As Integer, form As DataGridView, userId As String, startRow As Integer, endRow As Integer) As StatusPost
        Dim statusPost As New StatusPost()
        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing
        Dim delayInterval As Integer = 10 ' Penundaan dalam milidetik (10 ms)

        Dim KataKunciProduk As String = String.Empty
        Dim HargaProduknya As Integer = 0
        Dim KeteranganProduk As String = String.Empty
        Dim LokasiProdukku As String = String.Empty
        Dim Foto1 As String = String.Empty
        Dim Foto2 As String = String.Empty
        Dim Foto3 As String = String.Empty
        Dim Foto4 As String = String.Empty
        Dim Foto5 As String = String.Empty
        Dim Foto6 As String = String.Empty
        Dim Foto7 As String = String.Empty
        Dim Foto8 As String = String.Empty
        Dim Foto9 As String = String.Empty
        Dim Foto10 As String = String.Empty
        Dim LabelProduk As String = String.Empty
        Dim JedaPost As Integer = 0
        Dim JedaDraf As Integer = 0

        Dim prevProgress As Integer = CInt((startRow - 1) * 100 / endRow)
        Try
            If fields.Length >= 17 Then

                '====================================
                '//masuk ke halaman marketplace/create
                Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                Thread.Sleep(jedarandom)
                elementList =
    Driver.FindElements(By.XPath("//span[contains (text(),'Meninjau Permintaan')]"))

                If elementList.Count > 0 Then
                    statusPost.Status = False
                    statusPost.Message = "Peninjauan Belum disejui Facebook"

                    Return statusPost
                End If
                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains (@href,'/marketplace/create/item/')]")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                    Return statusPost
                End Try
                elementList =
    Driver.FindElements(By.XPath("//a[contains (@href,'/marketplace/create/item/')]"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    elementList(0).Click()
                Else
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                    Return statusPost
                End If
                Thread.Sleep(jedarandom * 5)
                '======================================


                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 1 / 5 / endRow))


#Region "Validasi Halaman sebelum proses input"
                elementList =
    Driver.FindElements(By.XPath("//span[text()='Maaf, ada masalah']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If

                Thread.Sleep(jedarandom)

                elementList =
    Driver.FindElements(By.XPath("//input[@type='file']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count = 0 Then
                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If
#End Region

                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 2 / 5 / endRow))
                If Not String.IsNullOrEmpty(fields(0)) Then
                    KataKunciProduk = fields(0)
                    HargaProduknya = Convert.ToInt64(fields(1))
                    KeteranganProduk = fields(2)
                    LokasiProdukku = fields(3)
                    Foto1 = fields(4)
                    Foto2 = fields(5)
                    Foto3 = fields(6)
                    Foto4 = fields(7)
                    Foto5 = fields(8)
                    Foto6 = fields(9)
                    Foto7 = fields(10)
                    Foto8 = fields(11)
                    Foto9 = fields(12)
                    Foto10 = fields(13)
                    LabelProduk = fields(14)
                    JedaPost = Convert.ToInt64(fields(15))
                    JedaDraf = Convert.ToInt64(fields(16))
                    '// PILIH CSV NYA PAKAI APA
                    Thread.Sleep(jedarandom)

#Region "Validasi Halaman sebelum proses input tahap 2"
                    '// JIKA POSTINGAN BATAS TERCAPAI MAKA MUNCUL NOTIF
                    elementList =
    Driver.FindElements(By.XPath("//span[text()='Batas tercapai']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        Thread.Sleep(jedarandom)

                        statusPost.Status = False
                        statusPost.Message = "Batas Posting di marketplace sudah tercapai"

                        Return statusPost
                    End If
#End Region

#Region "Proses Input dan Inject ke elment berdasarkan CSV"
                    Thread.Sleep(jedarandom)
                    Try
                        ' INPUT FOTO PRODUK POST FBMP
                        Try
                            waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//input[@type='file']")))
                        Catch ex As WebDriverTimeoutException
                            statusPost.Status = False
                            statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                            Return statusPost
                        End Try

                        Dim fotoList As New List(Of String)()
                        For x As Integer = 1 To 10
                            Dim fotoField As String = fields(x + 3)
                            If Not String.IsNullOrEmpty(fotoField) Then
                                If Not File.Exists(fotoField) Then
                                    statusPost.Status = False
                                    statusPost.Message = "File FOTO " & x & " Tidak ditemukan (Saran simpan foto di folder document)"

                                    Return statusPost
                                End If

                                fotoList.Add(fotoField)
                            End If
                        Next
                        If fotoList.Count > 0 Then
                            Dim fotoString As String = String.Join(vbNewLine, fotoList)
                            Dim inputFoto As IWebElement = Driver.FindElement(By.XPath("//input[@type='file']"))
                            inputFoto.SendKeys(fotoString)
                            Thread.Sleep(fotoList.Count)
                        End If
                    Catch ex As Exception
                    End Try
                    Thread.Sleep(jedarandom)
                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 3 / 5 / endRow))
                    '// KATA KUNCI PRODK 
                    elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        form.Invoke(Sub() Clipboard.SetText(KataKunciProduk))
                        elementList(0).Click()
                        elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                    End If

                    'If elementList.Count > 0 Then
                    '    form.Invoke(Sub() Clipboard.SetText(KataKunciProduk))
                    '    elementList(0).Click()
                    '    elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                    'End If
                    Thread.Sleep(jedarandom)
                    '// HARGA PRODUK
                    'elementList = driver.FindElements(By.XPath("//*[@aria-label='Harga']"))
                    If elementList.Count > 1 Then
                        elementList(1).SendKeys(HargaProduknya)
                    End If
                    'driver.FindElement(By.XPath("//input[@name='price']")).SendKeys(HargaProduknya)
                    Thread.Sleep(jedarandom)
                    '// KATEGORI PRODUK 
                    elementList = Driver.FindElements(By.XPath("//label[@aria-label='Kategori']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                    End If

                    Thread.Sleep(jedarandom)

                    Dim cateSelect As Integer = 0

                    If categoryProd <= 4 Then
                        cateSelect = categoryProd + 2
                    ElseIf categoryProd > 4 AndAlso categoryProd <= 6 Then
                        cateSelect = categoryProd + 3
                    ElseIf categoryProd > 6 AndAlso categoryProd <= 10 Then
                        cateSelect = categoryProd + 4
                    ElseIf categoryProd > 10 AndAlso categoryProd <= 14 Then
                        cateSelect = categoryProd + 5
                    ElseIf categoryProd > 14 AndAlso categoryProd <= 16 Then
                        cateSelect = categoryProd + 6
                    ElseIf categoryProd > 16 AndAlso categoryProd <= 22 Then
                        cateSelect = categoryProd + 7
                    ElseIf categoryProd > 22 AndAlso categoryProd <= 24 Then
                        cateSelect = categoryProd + 8
                    End If

                    elementList = Driver.FindElements(
                            By.XPath("//div[@role='dialog']/div/div/div/span/div/div[" & cateSelect.ToString() & "]/div[@role='button']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                    End If
                    Thread.Sleep(jedarandom)
                    '// KONDISI PRODUK
                    elementList = Driver.FindElements(
                           By.XPath("//label[@aria-label='Kondisi']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                                By.XPath("//div[@role='listbox']/div/div/div/div/div[1]/div/div[" & (conditionProd + 1).ToString() & "]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(0).Click()
                        End If
                    End If

                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 4 / 5 / endRow))

                    Thread.Sleep(jedarandom)
                    '// KETERANGAN PRODUK
                    elementList = Driver.FindElements(
                                By.XPath("//label[@aria-label='Keterangan']/div/div/textarea"))
                    ' Cek apakah elemen ada atau tidak ada
                    'If elementList.Count > 0 Then
                    '    For Each karakter As Char In KeteranganProduk
                    '        elementList(0).SendKeys(karakter) ' Kirim karakter ke elemen input
                    '        Thread.Sleep(delayInterval) ' Tunggu sebentar
                    '    Next
                    'End If

                    If elementList.Count > 0 Then
                        form.Invoke(Sub() Clipboard.SetText(KeteranganProduk))
                        elementList(0).Click()
                        elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                    End If

                    Thread.Sleep(jedarandom)
                    '// LABEL PRODUK
                    elementList = Driver.FindElements(
                                By.XPath("//*[@aria-label='Label produk']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).SendKeys(LabelProduk)
                    End If

                    'Thread.Sleep(jedarandom)

                    Thread.Sleep(jedarandom * 2)

                    '// KLIK SIMPAN DRAF
                    elementList = Driver.FindElements(
                                                  By.XPath("//div[contains(@aria-label,'Simpan draf')]"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        Try
                            elementList(0).Click()
                        Catch ex As Exception

                        End Try
                    End If

                    Thread.Sleep(jedarandom)

                    elementList = Driver.FindElements(
                                By.XPath("//div[@aria-label='Tutup']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 AndAlso elementList(0).Enabled Then
                        Dim ariaDisabledValue As String = elementList(0).GetAttribute("aria-disabled")
                        If ariaDisabledValue IsNot Nothing AndAlso
                            ariaDisabledValue.ToLower() <> "true" Then
                            elementList(0).Click()
                        End If
                    End If

                    Thread.Sleep(jedarandom * JedaDraf)
#End Region
                End If

                statusPost.Status = True
                statusPost.Message = String.Empty
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = "kolom pada CSV tidak memenuhi kriteria"
                Return statusPost
            End If
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try

        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function

    '//Function untuk edit Draft dan Posting
    Public Function EditContohFromDraftFB(jedarandom As Integer, fields As String(), waitforelement As WebDriverWait, categoryProd As Integer, conditionProd As Integer, form As DataGridView, userId As String, startRow As Integer, endRow As Integer) As StatusPost
        Dim statusPost As New StatusPost()
        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing
        Dim delayInterval As Integer = 10 ' Penundaan dalam milidetik (10 ms)

        Dim prevProgress As Integer = CInt((startRow - 1) * 100 / endRow)
        Try
            If fields.Length >= 17 Then
                Dim KataKunciProduk As String = fields(0)
                Dim LokasiProdukku As String = fields(3)
                Dim JedaPost As Integer = Convert.ToInt64(fields(15))
                Dim UrlPost As String = "https://www.facebook.com"

                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 1 / 5 / endRow))

                Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                Thread.Sleep(jedarandom * 3)

                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains (@href,'/marketplace/create/item/')]")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                    Return statusPost
                End Try
                '// KLIK PILIH DRAFT
                elementList = Driver.FindElements(
                                              By.XPath("//a[contains(@href,'/marketplace/edit/?listing_id')]"))

                If elementList.Count > 0 Then
                    '//Pilih Draft jika tersedia
                    elementList(0).Click()
                    UrlPost = Driver.Url
                    Thread.Sleep(jedarandom)
                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 2 / 5 / endRow))

                    'Proses Posting Dari Draft
                    'statusPost = postFromDreaft(jedarandom, LokasiProdukku, JedaPost, UrlPost)
                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 4 / 5 / endRow))

                    Return statusPost
                Else
                    '//Buat Draft Baru jika tidak menemukan Draft
                    statusPost = PostContohOnlyDraftFB(jedarandom, fields, waitforelement, categoryProd, conditionProd, form, userId, startRow, endRow)

                    If statusPost.Status Then
                        Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                        Thread.Sleep(jedarandom * 3)

                        Try
                            waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains (@href,'/marketplace/create/item/')]")))
                        Catch ex As WebDriverTimeoutException
                            statusPost.Status = False
                            statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                            Return statusPost
                        End Try
                        '// KLIK PILIH DRAFT
                        elementList = Driver.FindElements(
                                                      By.XPath("//a[contains(@href,'/marketplace/edit/?listing_id')]"))

                        If elementList.Count > 0 Then
                            elementList(0).Click()
                            UrlPost = Driver.Url
                            Thread.Sleep(jedarandom)

                            'postFromDreaft(jedarandom, LokasiProdukku, JedaPost, UrlPost)
                        End If
                    Else
                        Return statusPost
                    End If
                End If
            Else
                statusPost.Status = False
                statusPost.Message = "kolom pada CSV tidak memenuhi kriteria"
                Return statusPost
            End If
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try

        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function


    '//Function Posting Dari Edit Draft
    Public Function postContohFromDreaft(jedarandom As Integer, LokasiProdukku As String, JedaPost As Integer, UrlPost As String) As StatusPost
        Dim statusPost As New StatusPost()
        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing
        Dim waitforelement As WebDriverWait = New WebDriverWait(Driver, TimeSpan.FromSeconds(waitElement))

        Try
            Driver.GoToUrl(UrlPost)
            Thread.Sleep(jedarandom * 2)
            Driver.Navigate.Refresh()
            Thread.Sleep(jedarandom * 2)

            elementList =
    Driver.FindElements(By.XPath("//span[text()='Maaf, ada masalah']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 Then
                Thread.Sleep(jedarandom)

                statusPost.Status = False
                statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                Return statusPost
            End If

            Try
                waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//input[@type='file']")))
            Catch ex As WebDriverTimeoutException
                statusPost.Status = False
                statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                Return statusPost
            End Try

            elementList =
    Driver.FindElements(By.XPath("//input[@type='file']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count = 0 Then
                Thread.Sleep(jedarandom)

                statusPost.Status = False
                statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                Return statusPost
            End If

            '// LOKASI PRODUK
            Dim ulangi As Boolean = True
            Dim alamatStr = LokasiProdukku
            Do While ulangi
                elementList = Driver.FindElements(
                            By.XPath("//input[@aria-label='Lokasi']"))
                If elementList.Count > 0 Then

                    Thread.Sleep(jedarandom)
                    elementList(0).SendKeys(Keys.Control & "a") ' Memilih semua teks dalam elemen
                    Thread.Sleep(jedarandom)
                    elementList(0).SendKeys(Keys.Delete)
                    Thread.Sleep(jedarandom)
                    elementList(0).SendKeys(alamatStr)
                    Thread.Sleep(jedarandom)
                    elementList = Driver.FindElements(
                          By.XPath("//ul[contains (@aria-label,'disarankan')]/li"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        ulangi = False
                        Thread.Sleep(jedarandom)
                    Else
                        If alamatStr.Length <> 0 Then
                            alamatStr = alamatStr.Substring(0, alamatStr.Length - 1)
                        Else
                            ulangi = False
                        End If
                    End If

                End If
            Loop

            Thread.Sleep(jedarandom)

            elementList = Driver.FindElements(
                    By.XPath("//div[@aria-label='Berikutnya']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 Then
                If elementList(0).Enabled Then
                    elementList(0).Click()
                End If
            End If

            Thread.Sleep(jedarandom)
            Try
                waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//div[@aria-label='Publikasikan']")))
            Catch ex As WebDriverTimeoutException
                statusPost.Status = False
                statusPost.Message = "tidak bisa mempublikasikan"
                Return statusPost
            End Try

            elementList = Driver.FindElements(
                    By.XPath("//div[@aria-label='Publikasikan']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 Then
                elementList(0).Click()
            Else
                statusPost.Status = False
                statusPost.Message = "Gagal Saat Posting"

                Return statusPost
            End If

            Thread.Sleep(jedarandom)

            elementList = Driver.FindElements(
                    By.XPath("//div[@aria-label='Tutup']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 AndAlso elementList(0).Enabled Then
                Dim ariaDisabledValue As String = elementList(0).GetAttribute("aria-disabled")
                If ariaDisabledValue IsNot Nothing AndAlso
                            ariaDisabledValue.ToLower() <> "true" Then
                    elementList(0).Click()
                End If
            End If

            Thread.Sleep(jedarandom * JedaPost)
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try

        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function

#End Region

#Region "Fungsi Posting di Halaman Group"
    Public Function PostGroupFB(jedarandom As Integer, fields As String(), waitforelement As WebDriverWait, form As DataGridView, userId As String,
                                startRow As Integer, endRow As Integer, ByRef suspendEvent As ManualResetEvent) As StatusPost
        Dim statusPost As New StatusPost()
        Dim urlGroup As String = String.Empty
        Dim ContentDesc As String = String.Empty
        Dim Foto1 As String = String.Empty
        Dim Foto2 As String = String.Empty
        Dim Foto3 As String = String.Empty
        Dim Foto4 As String = String.Empty
        Dim Foto5 As String = String.Empty
        Dim JedaPost As Integer = 0

        Dim prevProgress As Integer = CInt((startRow - 1) * 100 / endRow)
        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing

        Try
            If fields.Length >= 7 Then
                If Not String.IsNullOrEmpty(fields(0)) Then

                    urlGroup = fields(0)
                    ContentDesc = fields(1)
                    Foto1 = fields(2)
                    Foto2 = fields(3)
                    Foto3 = fields(4)
                    Foto4 = fields(5)
                    Foto5 = fields(6)
                    JedaPost = Convert.ToInt64(fields(7))

                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 1 / 5 / endRow))

                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    Driver.GoToUrl(urlGroup)

                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    Try
                        waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//div[contains(@class, 'x6s0dn4 x78zum5 x1l90r2v x1pi30zi x1swvt13 xz9dl7a')]/div[contains(@role, 'button')]")))
                    Catch ex As WebDriverTimeoutException
                        statusPost.Status = False
                        statusPost.Message = "Halaman Group tidak tersedia"
                        Return statusPost
                    End Try
                    WaitHandle.WaitAny({suspendEvent})

                    elementList =
    Driver.FindElements(By.XPath("//div[contains(@class, 'x6s0dn4 x78zum5 x1l90r2v x1pi30zi x1swvt13 xz9dl7a')]/div[contains(@role, 'button')]"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        Thread.Sleep(jedarandom)

                        If elementList(0).Enabled Then
                            elementList(0).Click()

                            Thread.Sleep(jedarandom)
                        Else
                            statusPost.Status = False
                            statusPost.Message = "tidak bisa melanjutkan"

                            Return statusPost
                        End If

                        updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 2 / 5 / endRow))
                        WaitHandle.WaitAny({suspendEvent})

                        elementList =
                        Driver.FindElements(By.XPath("//div[contains(@aria-label, 'Buat postingan')][contains(@role, 'textbox')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            form.Invoke(Sub() Clipboard.SetText(ContentDesc))
                            elementList(0).Click()
                            elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                        Else
                            statusPost.Status = False
                            statusPost.Message = "tidak bisa melanjutkan, group tidak tersedia"

                            Return statusPost
                        End If

                        updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 3 / 5 / endRow))
                        WaitHandle.WaitAny({suspendEvent})

                        Dim fotoValue As String = String.Empty
                        Try
                            waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//input[@type='file']")))
                        Catch ex As WebDriverTimeoutException
                            statusPost.Status = False
                            statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                            Return statusPost
                        End Try
                        WaitHandle.WaitAny({suspendEvent})

                        If Not String.IsNullOrEmpty(Foto1) Then
                            ' Memeriksa apakah folder ada
                            If Not File.Exists(Foto1) Then
                                statusPost.Status = False
                                statusPost.Message = "File FOTO 1 Tidak ditemukan"

                                Return statusPost
                            End If

                            fotoValue &= Foto1
                        End If
                        If Not String.IsNullOrEmpty(Foto2) Then
                            If Not File.Exists(Foto2) Then
                                statusPost.Status = False
                                statusPost.Message = "File FOTO 2 Tidak ditemukan"

                                Return statusPost
                            End If
                            fotoValue &= vbNewLine
                            fotoValue &= Foto2
                        End If
                        If Not String.IsNullOrEmpty(Foto3) Then
                            If Not File.Exists(Foto3) Then
                                statusPost.Status = False
                                statusPost.Message = "File FOTO 3 Tidak ditemukan"

                                Return statusPost
                            End If
                            fotoValue &= vbNewLine
                            fotoValue &= Foto3
                        End If
                        If Not String.IsNullOrEmpty(Foto4) Then
                            If Not File.Exists(Foto4) Then
                                statusPost.Status = False
                                statusPost.Message = "File FOTO 4 Tidak ditemukan"

                                Return statusPost
                            End If
                            fotoValue &= vbNewLine
                            fotoValue &= Foto4
                        End If
                        If Not String.IsNullOrEmpty(Foto5) Then
                            If Not File.Exists(Foto5) Then
                                statusPost.Status = False
                                statusPost.Message = "File FOTO 5 Tidak ditemukan"

                                Return statusPost
                            End If
                            fotoValue &= vbNewLine
                            fotoValue &= Foto5
                        End If

                        Thread.Sleep(jedarandom)
                        WaitHandle.WaitAny({suspendEvent})

                        If Not String.IsNullOrEmpty(fotoValue) Then
                            elementList = Driver.FindElements(By.XPath("//div[contains(@class, 'xr9ek0c xfs2ol5 xjpr12u x12mruv9')]/input[@type='file']"))
                            If elementList.Count > 0 Then
                                elementList(0).SendKeys(fotoValue)

                                Thread.Sleep(jedarandom * 3)
                            End If
                        End If

                        updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 4 / 5 / endRow))

                        WaitHandle.WaitAny({suspendEvent})
                        Thread.Sleep(jedarandom * 2) ' Tunggu beberapa detik untuk memuat

                        Try
                            Dim textarea As IWebElement =
                                    Driver.FindElement(By.XPath("//div[contains(@aria-label, 'Media yang dilampirkan')][contains(@role, 'group')]"))
                            If textarea IsNot Nothing Then
                                Dim jsExecutor As IJavaScriptExecutor = DirectCast(Driver, IJavaScriptExecutor)
                                jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", textarea)

                            End If
                        Catch ex As Exception

                        End Try

                        Thread.Sleep(jedarandom * 2) ' Tunggu beberapa detik untuk memuat

                        elementList = Driver.FindElements(By.XPath("//div[contains(@role, 'dialog')]/div/div/form[contains(@method, 'POST')]"))
                        If elementList.Count > 0 Then
                            elementList(0).Submit()

                            Thread.Sleep(jedarandom * 3)
                        End If
                    Else
                        statusPost.Status = False
                        statusPost.Message = "tidak bisa melanjutkan, group tidak tersedia"

                        Return statusPost
                    End If

                    Thread.Sleep(jedarandom * JedaPost)
                    WaitHandle.WaitAny({suspendEvent})
                End If

                statusPost.Status = True
                statusPost.Message = String.Empty
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = "kolom pada CSV tidak memenuhi kriteria"
                Return statusPost
            End If
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try
        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function
#End Region

#Region "Kumpulan Fungsi Posting Properti/ Draft / posting & Draft"
    Public Function PostPropertiFB(jedarandom As Integer, fields As String(), waitforelement As WebDriverWait, categoryProd As Integer,
                                   conditionProd As Integer, form As DataGridView, userId As String, startRow As Integer, endRow As Integer,
                                   ByRef suspendEvent As ManualResetEvent) As StatusPost
        Dim statusPost As New StatusPost()

        Dim jmlKmrTidur As Integer = 0
        Dim jmlKmrMandi As Integer = 0
        Dim harga As Integer = 0
        Dim alamat As String = String.Empty
        Dim meterPersegi As Integer = 0
        Dim keterangan As String = String.Empty
        Dim Foto1 As String = String.Empty
        Dim Foto2 As String = String.Empty
        Dim Foto3 As String = String.Empty
        Dim Foto4 As String = String.Empty
        Dim Foto5 As String = String.Empty
        Dim Foto6 As String = String.Empty
        Dim Foto7 As String = String.Empty
        Dim Foto8 As String = String.Empty
        Dim Foto9 As String = String.Empty
        Dim Foto10 As String = String.Empty
        Dim JedaPost As Integer = 0
        Dim JedaDraf As Integer = 0

        Dim prevProgress As Integer = CInt((startRow - 1) * 100 / endRow)
        Dim delayInterval As Integer = 10 ' Penundaan dalam milidetik (10 ms)

        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing

        Try
            If fields.Length >= 18 Then

                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 1 / 5 / endRow))

                Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                Thread.Sleep(jedarandom)
                WaitHandle.WaitAny({suspendEvent})
                Driver.Navigate.Refresh()
                Thread.Sleep(jedarandom * 3)
                WaitHandle.WaitAny({suspendEvent})
                elementList =
    Driver.FindElements(By.XPath("//span[contains (text(),'Meninjau Permintaan')]"))

                If elementList.Count > 0 Then
                    statusPost.Status = False
                    statusPost.Message = "Peninjauan Belum disejui Facebook"

                    Return statusPost
                End If
                WaitHandle.WaitAny({suspendEvent})
                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains (@href,'/marketplace/create/rental/')]")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                    Return statusPost
                End Try
                WaitHandle.WaitAny({suspendEvent})
                elementList =
    Driver.FindElements(By.XPath("//a[contains (@href,'/marketplace/create/rental/')]"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    elementList(0).Click()
                Else
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                    Return statusPost
                End If

                Thread.Sleep(jedarandom * 3)

                WaitHandle.WaitAny({suspendEvent})
                elementList =
    Driver.FindElements(By.XPath("//span[text()='Maaf, ada masalah']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then


                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If

                Thread.Sleep(jedarandom)
                WaitHandle.WaitAny({suspendEvent})
                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//input[@type='file']")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                    Return statusPost
                End Try

                WaitHandle.WaitAny({suspendEvent})
                elementList =
    Driver.FindElements(By.XPath("//input[@type='file']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count = 0 Then
                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If


                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 2 / 5 / endRow))
                WaitHandle.WaitAny({suspendEvent})

                If Not String.IsNullOrEmpty(fields(0)) Then

                    jmlKmrTidur = CInt(fields(0))
                    jmlKmrMandi = CInt(fields(1))
                    harga = CInt(fields(2))
                    alamat = fields(3)
                    meterPersegi = CInt(fields(4))
                    keterangan = fields(5)
                    Foto1 = fields(6)
                    Foto2 = fields(7)
                    Foto3 = fields(8)
                    Foto4 = fields(9)
                    Foto5 = fields(10)
                    Foto6 = fields(11)
                    Foto7 = fields(12)
                    Foto8 = fields(13)
                    Foto9 = fields(14)
                    Foto10 = fields(15)
                    JedaPost = Convert.ToInt64(fields(16))
                    JedaDraf = Convert.ToInt64(fields(17))

                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    elementList =
    Driver.FindElements(By.XPath("//span[text()='Batas tercapai']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        Thread.Sleep(jedarandom)

                        statusPost.Status = False
                        statusPost.Message = "Batas Posting di marketplace sudah tercapai"

                        Return statusPost
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    Try
                        waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//input[@type='text']")))
                    Catch ex As WebDriverTimeoutException
                        statusPost.Status = False
                        statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                        Return statusPost
                    End Try
                    elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                    If elementList.Count = 0 Then
                        statusPost.Status = False
                        statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                        Return statusPost
                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    Try
                        ' INPUT FOTO PRODUK POST FBMP
                        Dim fotoList As New List(Of String)()
                        For x As Integer = 1 To 10
                            Dim fotoField As String = fields(x + 5)
                            If Not String.IsNullOrEmpty(fotoField) Then
                                If Not File.Exists(fotoField) Then
                                    statusPost.Status = False
                                    statusPost.Message = "File FOTO " & x & " Tidak ditemukan"

                                    Return statusPost
                                End If

                                fotoList.Add(fotoField)
                            End If
                        Next
                        If fotoList.Count > 0 Then
                            Dim fotoString As String = String.Join(vbNewLine, fotoList)
                            Dim inputFoto As IWebElement = Driver.FindElement(By.XPath("//input[@type='file']"))
                            inputFoto.SendKeys(fotoString)
                            Thread.Sleep(fotoList.Count)
                        End If
                    Catch ex As Exception
                    End Try
                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Rumah Dijual atau Dikontrakkan')]"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(categoryProd).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Jenis properti')]"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(conditionProd).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    '// JML KMR TIDUR
                    elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).SendKeys(jmlKmrTidur) ' Kirim karakter ke elemen input
                        Thread.Sleep(jedarandom) ' Tunggu sebentar
                    End If

                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 3 / 5 / endRow))
                    WaitHandle.WaitAny({suspendEvent})

                    '// JML KMR MANDI
                    If elementList.Count > 1 Then
                        elementList(1).SendKeys(jmlKmrMandi) ' Kirim karakter ke elemen input
                        Thread.Sleep(jedarandom) ' Tunggu sebentar
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    '// HARGA
                    If elementList.Count > 2 Then
                        elementList(2).SendKeys(harga) ' Kirim karakter ke elemen input
                        Thread.Sleep(jedarandom) ' Tunggu sebentar
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    '// LOKASI
                    If elementList.Count > 3 Then
                        Thread.Sleep(jedarandom)
                        elementList(3).SendKeys(Keys.Control & "a") ' Memilih semua teks dalam elemen
                        Thread.Sleep(jedarandom)
                        elementList(3).SendKeys(Keys.Delete)
                        Thread.Sleep(jedarandom)
                        elementList(3).SendKeys(alamat)
                        Thread.Sleep(jedarandom)

                        elementList = Driver.FindElements(
                            By.XPath("//ul[contains (@aria-label,'disarankan')]/li"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(0).Click()

                            Thread.Sleep(jedarandom)
                        End If
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    '// METER PERSEGI
                    elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 4 Then
                        elementList(4).SendKeys(meterPersegi) ' Kirim karakter ke elemen input
                        Thread.Sleep(jedarandom) ' Tunggu sebentar
                    End If

                    Thread.Sleep(jedarandom)

                    WaitHandle.WaitAny({suspendEvent})
                    '// KETERANGAN PRODUK
                    elementList = Driver.FindElements(
                            By.XPath("//label/div/div/textarea"))
                    ' Cek apakah elemen ada atau tidak ada
                    'If elementList.Count > 0 Then
                    '    For Each karakter As Char In keterangan
                    '        elementList(0).SendKeys(karakter) ' Kirim karakter ke elemen input
                    '        Thread.Sleep(delayInterval) ' Tunggu sebentar
                    '    Next
                    'End If

                    If elementList.Count > 0 Then
                        form.Invoke(Sub() Clipboard.SetText(keterangan))
                        elementList(0).Click()
                        elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                    End If

                    'Thread.Sleep(jedarandom)

                    Thread.Sleep(jedarandom * 2)

                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 4 / 5 / endRow))
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})


                    elementList = Driver.FindElements(
                            By.XPath("//div[@aria-label='Berikutnya']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        If elementList(0).Enabled Then
                            elementList(0).Click()
                        End If
                    End If

                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    Try
                        waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//div[@aria-label='Publikasikan']")))
                    Catch ex As WebDriverTimeoutException
                        statusPost.Status = False
                        statusPost.Message = "tidak bisa mempublikasikan"
                        Return statusPost
                    End Try

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                            By.XPath("//div[@aria-label='Publikasikan']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                    Else
                        statusPost.Status = False
                        statusPost.Message = "Gagal Saat Posting"

                        Return statusPost
                    End If
                    Thread.Sleep(jedarandom)

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                            By.XPath("//div[@aria-label='Tutup']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 AndAlso elementList(0).Enabled Then
                        Dim ariaDisabledValue As String = elementList(0).GetAttribute("aria-disabled")
                        If ariaDisabledValue IsNot Nothing AndAlso
                            ariaDisabledValue.ToLower() <> "true" Then
                            elementList(0).Click()
                        End If
                    End If


                    Thread.Sleep(jedarandom * JedaPost)
                End If

                WaitHandle.WaitAny({suspendEvent})
                statusPost.Status = True
                statusPost.Message = String.Empty
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = "kolom pada CSV tidak memenuhi kriteria"
                Return statusPost
            End If
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try

        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function

    Public Function PostPropertiOnlyDraftFB(jedarandom As Integer, fields As String(), waitforelement As WebDriverWait, categoryProd As Integer,
                                            conditionProd As Integer, form As DataGridView, userId As String, startRow As Integer, endRow As Integer,
                                            ByRef suspendEvent As ManualResetEvent) As StatusPost
        Dim statusPost As New StatusPost()
        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing
        Dim delayInterval As Integer = 10 ' Penundaan dalam milidetik (10 ms)

        Dim jmlKmrTidur As Integer = 0
        Dim jmlKmrMandi As Integer = 0
        Dim harga As Integer = 0
        Dim alamat As String = String.Empty
        Dim meterPersegi As Integer = 0
        Dim keterangan As String = String.Empty
        Dim Foto1 As String = String.Empty
        Dim Foto2 As String = String.Empty
        Dim Foto3 As String = String.Empty
        Dim Foto4 As String = String.Empty
        Dim Foto5 As String = String.Empty
        Dim Foto6 As String = String.Empty
        Dim Foto7 As String = String.Empty
        Dim Foto8 As String = String.Empty
        Dim Foto9 As String = String.Empty
        Dim Foto10 As String = String.Empty
        Dim JedaPost As Integer = 0
        Dim JedaDraf As Integer = 0

        Dim prevProgress As Integer = CInt((startRow - 1) * 100 / endRow)
        Try
            If fields.Length >= 18 Then

                '====================================
                '//masuk ke halaman marketplace/create
                Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                Thread.Sleep(jedarandom)
                WaitHandle.WaitAny({suspendEvent})
                Driver.Navigate.Refresh()
                Thread.Sleep(jedarandom * 3)
                WaitHandle.WaitAny({suspendEvent})
                elementList =
    Driver.FindElements(By.XPath("//span[contains (text(),'Meninjau Permintaan')]"))

                If elementList.Count > 0 Then
                    statusPost.Status = False
                    statusPost.Message = "Peninjauan Belum disejui Facebook"

                    Return statusPost
                End If
                WaitHandle.WaitAny({suspendEvent})
                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains (@href,'/marketplace/create/rental/')]")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                    Return statusPost
                End Try
                elementList =
    Driver.FindElements(By.XPath("//a[contains (@href,'/marketplace/create/rental/')]"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    elementList(0).Click()
                Else
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                    Return statusPost
                End If
                Thread.Sleep(jedarandom * 3)
                WaitHandle.WaitAny({suspendEvent})
                '======================================


                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 1 / 5 / endRow))


#Region "Validasi Halaman sebelum proses input"
                elementList =
    Driver.FindElements(By.XPath("//span[text()='Maaf, ada masalah']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If

                Thread.Sleep(jedarandom)

                WaitHandle.WaitAny({suspendEvent})
                elementList =
    Driver.FindElements(By.XPath("//input[@type='file']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count = 0 Then
                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If
#End Region

                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 2 / 5 / endRow))
                WaitHandle.WaitAny({suspendEvent})
                If Not String.IsNullOrEmpty(fields(0)) Then
                    jmlKmrTidur = CInt(fields(0))
                    jmlKmrMandi = CInt(fields(1))
                    harga = CInt(fields(2))
                    alamat = fields(3)
                    meterPersegi = CInt(fields(4))
                    keterangan = fields(5)
                    Foto1 = fields(6)
                    Foto2 = fields(7)
                    Foto3 = fields(8)
                    Foto4 = fields(9)
                    Foto5 = fields(10)
                    Foto6 = fields(11)
                    Foto7 = fields(12)
                    Foto8 = fields(13)
                    Foto9 = fields(14)
                    Foto10 = fields(15)
                    JedaPost = Convert.ToInt64(fields(16))
                    JedaDraf = Convert.ToInt64(fields(17))
                    '// PILIH CSV NYA PAKAI APA
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

#Region "Validasi Halaman sebelum proses input tahap 2"
                    '// JIKA POSTINGAN BATAS TERCAPAI MAKA MUNCUL NOTIF
                    elementList =
    Driver.FindElements(By.XPath("//span[text()='Batas tercapai']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        Thread.Sleep(jedarandom)

                        statusPost.Status = False
                        statusPost.Message = "Batas Posting di marketplace sudah tercapai"

                        Return statusPost
                    End If
#End Region
                    WaitHandle.WaitAny({suspendEvent})

#Region "Proses Input dan Inject ke elment berdasarkan CSV"
                    Thread.Sleep(jedarandom)
                    Try
                        Try
                            waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//input[@type='text']")))
                        Catch ex As WebDriverTimeoutException
                            statusPost.Status = False
                            statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                            Return statusPost
                        End Try

                        WaitHandle.WaitAny({suspendEvent})
                        elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                        If elementList.Count = 0 Then
                            statusPost.Status = False
                            statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                            Return statusPost
                        End If

                        WaitHandle.WaitAny({suspendEvent})
                        ' INPUT FOTO PRODUK POST FBMP
                        Dim fotoList As New List(Of String)()
                        For x As Integer = 1 To 10
                            Dim fotoField As String = fields(x + 5)
                            If Not String.IsNullOrEmpty(fotoField) Then
                                If Not File.Exists(fotoField) Then
                                    statusPost.Status = False
                                    statusPost.Message = "File FOTO " & x & " Tidak ditemukan (Saran simpan foto di folder document)"

                                    Return statusPost
                                End If

                                fotoList.Add(fotoField)
                            End If
                        Next
                        If fotoList.Count > 0 Then
                            Dim fotoString As String = String.Join(vbNewLine, fotoList)
                            Dim inputFoto As IWebElement = Driver.FindElement(By.XPath("//input[@type='file']"))
                            inputFoto.SendKeys(fotoString)
                            Thread.Sleep(fotoList.Count)
                        End If
                    Catch ex As Exception
                    End Try
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})
                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 3 / 5 / endRow))
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Rumah Dijual atau Dikontrakkan')]"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(categoryProd).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Jenis properti')]"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(conditionProd).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If

                    '// JML KMR TIDUR
                    elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).SendKeys(jmlKmrTidur) ' Kirim karakter ke elemen input
                        Thread.Sleep(jedarandom) ' Tunggu sebentar
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 3 / 5 / endRow))

                    '// JML KMR MANDI
                    If elementList.Count > 1 Then
                        elementList(1).SendKeys(jmlKmrMandi) ' Kirim karakter ke elemen input
                        Thread.Sleep(jedarandom) ' Tunggu sebentar
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    '// HARGA
                    If elementList.Count > 2 Then
                        elementList(2).SendKeys(harga) ' Kirim karakter ke elemen input
                        Thread.Sleep(jedarandom) ' Tunggu sebentar
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    '// METER PERSEGI
                    elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 4 Then
                        elementList(4).SendKeys(meterPersegi) ' Kirim karakter ke elemen input
                        Thread.Sleep(delayInterval) ' Tunggu sebentar
                    End If

                    Thread.Sleep(jedarandom)

                    WaitHandle.WaitAny({suspendEvent})
                    '// KETERANGAN PRODUK
                    elementList = Driver.FindElements(
                            By.XPath("//label/div/div/textarea"))
                    ' Cek apakah elemen ada atau tidak ada
                    'If elementList.Count > 0 Then
                    '    For Each karakter As Char In keterangan
                    '        elementList(0).SendKeys(karakter) ' Kirim karakter ke elemen input
                    '        Thread.Sleep(delayInterval) ' Tunggu sebentar
                    '    Next
                    'End If
                    If elementList.Count > 0 Then
                        form.Invoke(Sub() Clipboard.SetText(keterangan))
                        elementList(0).Click()
                        elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                    End If

                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 4 / 5 / endRow))

                    'Thread.Sleep(jedarandom)

                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    '// KLIK SIMPAN DRAF
                    elementList = Driver.FindElements(
                                                  By.XPath("//div[contains(@aria-label,'Simpan draf')]"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        Try
                            elementList(0).Click()
                        Catch ex As Exception

                        End Try
                    End If

                    Thread.Sleep(jedarandom)

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                                By.XPath("//div[@aria-label='Tutup']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 AndAlso elementList(0).Enabled Then
                        Dim ariaDisabledValue As String = elementList(0).GetAttribute("aria-disabled")
                        If ariaDisabledValue IsNot Nothing AndAlso
                            ariaDisabledValue.ToLower() <> "true" Then
                            elementList(0).Click()
                        End If
                    End If


                    Thread.Sleep(jedarandom * JedaDraf)
#End Region
                End If

                WaitHandle.WaitAny({suspendEvent})
                statusPost.Status = True
                statusPost.Message = String.Empty
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = "kolom pada CSV tidak memenuhi kriteria"
                Return statusPost
            End If
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try

        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function

    '//Function untuk edit Draft dan Posting
    Public Function EditPropertiFromDraftFB(jedarandom As Integer, fields As String(), waitforelement As WebDriverWait, categoryProd As Integer,
                                            conditionProd As Integer, form As DataGridView, userId As String, startRow As Integer, endRow As Integer,
                                            ByRef suspendEvent As ManualResetEvent) As StatusPost
        Dim statusPost As New StatusPost()
        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing
        Dim delayInterval As Integer = 10 ' Penundaan dalam milidetik (10 ms)

        Dim prevProgress As Integer = CInt((startRow - 1) * 100 / endRow)
        Try
            If fields.Length >= 18 Then
                Dim LokasiProdukku As String = fields(3)
                Dim JedaPost As Integer = Convert.ToInt64(fields(16))
                Dim UrlPost As String = "https://www.facebook.com"
                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 1 / 5 / endRow))

                Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                Thread.Sleep(jedarandom)
                WaitHandle.WaitAny({suspendEvent})
                Driver.Navigate.Refresh()
                Thread.Sleep(jedarandom * 3)
                WaitHandle.WaitAny({suspendEvent})

                elementList =
    Driver.FindElements(By.XPath("//span[contains (text(),'Meninjau Permintaan')]"))

                If elementList.Count > 0 Then
                    statusPost.Status = False
                    statusPost.Message = "Peninjauan Belum disejui Facebook"

                    Return statusPost
                End If

                WaitHandle.WaitAny({suspendEvent})
                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains (@href,'/marketplace/create/item/')]")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                    Return statusPost
                End Try
                WaitHandle.WaitAny({suspendEvent})
                '// KLIK PILIH DRAFT
                elementList = Driver.FindElements(
                                              By.XPath("//a[contains(@href,'/marketplace/edit/?listing_id')]"))

                If elementList.Count > 0 Then
                    '//Pilih Draft jika tersedia
                    elementList(0).Click()
                    UrlPost = Driver.Url
                    Thread.Sleep(jedarandom)
                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 2 / 5 / endRow))

                    WaitHandle.WaitAny({suspendEvent})
                    'Proses Posting Dari Draft
                    statusPost = postPropertiFromDraft(jedarandom, LokasiProdukku, JedaPost, UrlPost, suspendEvent)
                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 4 / 5 / endRow))
                    Return statusPost
                Else
                    '//Buat Draft Baru jika tidak menemukan Draft
                    statusPost = PostPropertiOnlyDraftFB(jedarandom, fields, waitforelement, categoryProd, conditionProd, form, userId, startRow, endRow, suspendEvent)

                    If statusPost.Status Then
                        Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                        Thread.Sleep(jedarandom)
                        WaitHandle.WaitAny({suspendEvent})
                        Driver.Navigate.Refresh()
                        Thread.Sleep(jedarandom * 3)
                        WaitHandle.WaitAny({suspendEvent})

                        Try
                            waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains (@href,'/marketplace/create/item/')]")))
                        Catch ex As WebDriverTimeoutException
                            statusPost.Status = False
                            statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                            Return statusPost
                        End Try

                        WaitHandle.WaitAny({suspendEvent})
                        '// KLIK PILIH DRAFT
                        elementList = Driver.FindElements(
                                                      By.XPath("//a[contains(@href,'/marketplace/edit/?listing_id')]"))

                        If elementList.Count > 0 Then
                            elementList(0).Click()
                            UrlPost = Driver.Url
                            Thread.Sleep(jedarandom)

                            postPropertiFromDraft(jedarandom, LokasiProdukku, JedaPost, UrlPost, suspendEvent)
                        End If
                    Else
                        Return statusPost
                    End If
                End If
            Else
                statusPost.Status = False
                statusPost.Message = "kolom pada CSV tidak memenuhi kriteria"
                Return statusPost
            End If
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try

        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function

    Public Function postPropertiFromDraft(jedarandom As Integer, LokasiProdukku As String, JedaPost As Integer, UrlPost As String, ByRef suspendEvent As ManualResetEvent) As StatusPost
        Dim statusPost As New StatusPost()
        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing
        Dim waitforelement As WebDriverWait = New WebDriverWait(Driver, TimeSpan.FromSeconds(waitElement))

        Try
            Driver.GoToUrl(UrlPost)
            Thread.Sleep(jedarandom * 2)
            WaitHandle.WaitAny({suspendEvent})
            Driver.Navigate.Refresh()
            Thread.Sleep(jedarandom * 2)
            WaitHandle.WaitAny({suspendEvent})

            elementList =
    Driver.FindElements(By.XPath("//span[text()='Maaf, ada masalah']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 Then
                Thread.Sleep(jedarandom)

                statusPost.Status = False
                statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                Return statusPost
            End If
            WaitHandle.WaitAny({suspendEvent})
            Try
                waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//input[@type='text']")))
            Catch ex As WebDriverTimeoutException
                statusPost.Status = False
                statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                Return statusPost
            End Try
            WaitHandle.WaitAny({suspendEvent})
            elementList =
    Driver.FindElements(By.XPath("//input[@type='file']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count = 0 Then
                Thread.Sleep(jedarandom)

                statusPost.Status = False
                statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                Return statusPost
            End If

            WaitHandle.WaitAny({suspendEvent})
            '// LOKASI PRODUK
            Dim ulangi As Boolean = True
            Dim alamatStr = LokasiProdukku
            Do While ulangi
                elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                If elementList.Count > 3 Then
                    Thread.Sleep(jedarandom)
                    elementList(3).SendKeys(Keys.Control & "a") ' Memilih semua teks dalam elemen
                    Thread.Sleep(jedarandom)
                    elementList(3).SendKeys(Keys.Delete)
                    Thread.Sleep(jedarandom)
                    elementList(3).SendKeys(alamatStr)
                    Thread.Sleep(jedarandom)
                    elementList = Driver.FindElements(
                          By.XPath("//ul[contains (@aria-label,'disarankan')]/li"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        ulangi = False
                        Thread.Sleep(jedarandom)
                    Else
                        If alamatStr.Length <> 0 Then
                            alamatStr = alamatStr.Substring(0, alamatStr.Length - 1)
                        Else
                            ulangi = False
                        End If
                    End If

                End If
            Loop

            Thread.Sleep(jedarandom)

            WaitHandle.WaitAny({suspendEvent})
            elementList = Driver.FindElements(
                    By.XPath("//div[@aria-label='Berikutnya']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 Then
                If elementList(0).Enabled Then
                    elementList(0).Click()
                End If
            End If

            Thread.Sleep(jedarandom)
            WaitHandle.WaitAny({suspendEvent})
            Try
                waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//div[@aria-label='Publikasikan']")))
            Catch ex As WebDriverTimeoutException
                statusPost.Status = False
                statusPost.Message = "tidak bisa mempublikasikan"
                Return statusPost
            End Try

            WaitHandle.WaitAny({suspendEvent})
            elementList = Driver.FindElements(
                    By.XPath("//div[@aria-label='Publikasikan']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 Then
                elementList(0).Click()
            Else
                statusPost.Status = False
                statusPost.Message = "Gagal Saat Posting"

                Return statusPost
            End If

            Thread.Sleep(jedarandom)

            WaitHandle.WaitAny({suspendEvent})
            elementList = Driver.FindElements(
                    By.XPath("//div[@aria-label='Tutup']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 AndAlso elementList(0).Enabled Then
                Dim ariaDisabledValue As String = elementList(0).GetAttribute("aria-disabled")
                If ariaDisabledValue IsNot Nothing AndAlso
                            ariaDisabledValue.ToLower() <> "true" Then
                    elementList(0).Click()
                End If
            End If


            Thread.Sleep(jedarandom * JedaPost)
            WaitHandle.WaitAny({suspendEvent})
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try

        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function

#End Region

#Region "Kumpulan Fungsi Posting Mobil/ Draft / posting & Draft"
    Public Function PostMobilFB(jedarandom As Integer, fields As String(), waitforelement As WebDriverWait, tahun As Integer, merek As String,
                                   bodi As Integer, warna As Integer, warna2 As Integer, conditionProd As Integer, fuel As Integer, tdkMasalah As Boolean, transmission As Integer,
                                    form As DataGridView, userId As String, startRow As Integer, endRow As Integer, ByRef suspendEvent As ManualResetEvent) As StatusPost
        Dim statusPost As New StatusPost()

        Dim model As String = String.Empty
        Dim jarakTempuh As Integer = 0
        Dim harga As Integer = 0
        Dim alamat As String = String.Empty
        Dim keterangan As String = String.Empty
        Dim Foto1 As String = String.Empty
        Dim Foto2 As String = String.Empty
        Dim Foto3 As String = String.Empty
        Dim Foto4 As String = String.Empty
        Dim Foto5 As String = String.Empty
        Dim Foto6 As String = String.Empty
        Dim Foto7 As String = String.Empty
        Dim Foto8 As String = String.Empty
        Dim Foto9 As String = String.Empty
        Dim Foto10 As String = String.Empty
        Dim JedaPost As Integer = 0
        Dim JedaDraf As Integer = 0


        Dim delayInterval As Integer = 10 ' Penundaan dalam milidetik (10 ms)

        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing

        Dim prevProgress As Integer = CInt((startRow - 1) * 100 / endRow)
        Try
            If fields.Length >= 17 Then

                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 1 / 5 / endRow))

                Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                Thread.Sleep(jedarandom * 3)
                WaitHandle.WaitAny({suspendEvent})
                elementList =
    Driver.FindElements(By.XPath("//span[contains (text(),'Meninjau Permintaan')]"))

                If elementList.Count > 0 Then
                    statusPost.Status = False
                    statusPost.Message = "Peninjauan Belum disejui Facebook"

                    Return statusPost
                End If
                WaitHandle.WaitAny({suspendEvent})
                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains (@href,'/marketplace/create/vehicle/')]")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                    Return statusPost
                End Try
                WaitHandle.WaitAny({suspendEvent})
                elementList =
    Driver.FindElements(By.XPath("//a[contains (@href,'/marketplace/create/vehicle/')]"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    elementList(0).Click()
                Else
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                    Return statusPost
                End If
                Thread.Sleep(jedarandom * 3)
                WaitHandle.WaitAny({suspendEvent})
                elementList =
    Driver.FindElements(By.XPath("//span[text()='Maaf, ada masalah']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then


                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If

                Thread.Sleep(jedarandom)
                WaitHandle.WaitAny({suspendEvent})

                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//input[@type='file']")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                    Return statusPost
                End Try
                WaitHandle.WaitAny({suspendEvent})

                elementList =
    Driver.FindElements(By.XPath("//input[@type='file']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count = 0 Then
                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If
                WaitHandle.WaitAny({suspendEvent})


                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 2 / 5 / endRow))

                If Not String.IsNullOrEmpty(fields(0)) Then

                    model = (fields(0))
                    jarakTempuh = CInt(fields(1))
                    harga = CInt(fields(2))
                    alamat = fields(3)
                    keterangan = fields(4)
                    Foto1 = fields(5)
                    Foto2 = fields(6)
                    Foto3 = fields(7)
                    Foto4 = fields(8)
                    Foto5 = fields(9)
                    Foto6 = fields(10)
                    Foto7 = fields(11)
                    Foto8 = fields(12)
                    Foto9 = fields(13)
                    Foto10 = fields(14)
                    JedaPost = Convert.ToInt64(fields(15))
                    JedaDraf = Convert.ToInt64(fields(16))

                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    elementList =
    Driver.FindElements(By.XPath("//span[text()='Batas tercapai']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        Thread.Sleep(jedarandom)

                        statusPost.Status = False
                        statusPost.Message = "Batas Posting di marketplace sudah tercapai"

                        Return statusPost
                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    Try
                        waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//input[@type='text']")))
                    Catch ex As WebDriverTimeoutException
                        statusPost.Status = False
                        statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                        Return statusPost
                    End Try
                    WaitHandle.WaitAny({suspendEvent})

                    elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                    If elementList.Count = 0 Then
                        statusPost.Status = False
                        statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                        Return statusPost
                    End If
                    Thread.Sleep(jedarandom * 3)
                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Jenis kendaraan')]"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(0).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    Try
                        ' INPUT FOTO PRODUK POST FBMP
                        Dim fotoList As New List(Of String)()
                        For x As Integer = 1 To 10
                            Dim fotoField As String = fields(x + 4)
                            If Not String.IsNullOrEmpty(fotoField) Then
                                If Not File.Exists(fotoField) Then
                                    statusPost.Status = False
                                    statusPost.Message = "File FOTO " & x & " Tidak ditemukan (Saran simpan foto di folder document)"

                                    Return statusPost
                                End If

                                fotoList.Add(fotoField)
                            End If
                        Next
                        If fotoList.Count > 0 Then
                            Dim fotoString As String = String.Join(vbNewLine, fotoList)
                            Dim inputFoto As IWebElement = Driver.FindElement(By.XPath("//input[@type='file']"))
                            inputFoto.SendKeys(fotoString)
                            Thread.Sleep(fotoList.Count)
                        End If
                    Catch ex As Exception
                    End Try
                    WaitHandle.WaitAny({suspendEvent})


                    '// LOKASI
                    Dim ulangi As Boolean = True
                    Dim alamatStr = alamat
                    Do While ulangi
                        elementList = Driver.FindElements(
                       By.XPath("//input[contains (@aria-label,'Lokasi')][@role='combobox'][@type='text']"))
                        If elementList.Count > 0 Then

                            Thread.Sleep(jedarandom)
                            elementList(0).SendKeys(Keys.Control & "a") ' Memilih semua teks dalam elemen
                            Thread.Sleep(jedarandom)
                            elementList(0).SendKeys(Keys.Delete)
                            Thread.Sleep(jedarandom)
                            elementList(0).SendKeys(alamatStr)
                            Thread.Sleep(jedarandom)
                            elementList = Driver.FindElements(
                          By.XPath("//ul[contains (@aria-label,'disarankan')]/li"))
                            ' Cek apakah elemen ada atau tidak ada
                            If elementList.Count > 0 Then
                                elementList(0).Click()
                                ulangi = False
                                Thread.Sleep(jedarandom)
                            Else
                                If alamatStr.Length <> 0 Then
                                    alamatStr = alamatStr.Substring(0, alamatStr.Length - 1)
                                Else
                                    ulangi = False
                                End If
                            End If

                        End If
                    Loop

                    WaitHandle.WaitAny({suspendEvent})

                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Tahun')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(tahun).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Merek')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            For Each element In elementList
                                Try
                                    Dim elementOptn As IReadOnlyCollection(Of IWebElement) = element.FindElements(By.XPath("div/div/div/span"))
                                    If elementOptn.Count > 0 AndAlso elementOptn(0).Text = merek Then
                                        element.Click()
                                        Exit For
                                    End If
                                Catch ex As Exception
                                End Try
                            Next
                        End If
                        Thread.Sleep(jedarandom)
                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    '// JML KMR MODEL
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Model')][@role='combobox']"))

                    Dim indexInput = 0
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            For Each element In elementList
                                Try
                                    Dim elementOptn As IReadOnlyCollection(Of IWebElement) = element.FindElements(By.XPath("div/div/div/span"))
                                    If elementOptn.Count > 0 AndAlso elementOptn(0).Text = model Then
                                        element.Click()
                                        Exit For
                                    End If
                                Catch ex As Exception
                                End Try
                            Next
                        End If
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                        WaitHandle.WaitAny({suspendEvent})

                        indexInput = 1
                    Else
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(By.XPath("//input[@type='text']"))

                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 1 Then
                            elementList(1).SendKeys(model) ' Kirim karakter ke elemen input
                            Thread.Sleep(jedarandom) ' Tunggu sebentar
                        End If
                        WaitHandle.WaitAny({suspendEvent})

                        indexInput = 0
                    End If

                    '// PILH PALING ATAS
                    Try
                        ' Temukan elemen dengan atribut role="listbox"
                        Dim listBoxElement As IWebElement = Driver.FindElement(By.XPath("//*[@role='listbox']"))

                        If listBoxElement IsNot Nothing Then
                            ' Temukan pilihan pertama dalam listbox
                            Dim firstOption As IWebElement = listBoxElement.FindElement(By.XPath(".//div[contains(@role, 'option')][1]"))

                            If firstOption IsNot Nothing Then
                                ' Klik pilihan pertama dalam listbox
                                firstOption.Click()
                            End If
                        End If
                    Catch ex As Exception
                    End Try
                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 3 / 5 / endRow))
                    WaitHandle.WaitAny({suspendEvent})

                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 2 - indexInput Then
                        elementList(2 - indexInput).SendKeys(jarakTempuh) ' Kirim karakter ke elemen input
                        Thread.Sleep(jedarandom) ' Tunggu sebentar
                    End If

                    '// HARGA
                    If elementList.Count > 3 - indexInput Then
                        elementList(3 - indexInput).SendKeys(harga) ' Kirim karakter ke elemen input
                        Thread.Sleep(jedarandom) ' Tunggu sebentar
                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Tipe body')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(bodi).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Warna eksterior')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(warna).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Warna eksterior')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(warna).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Warna Interior')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(warna2).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If

                    '// JML KMR MODEL
                    elementList = Driver.FindElements(By.XPath("//input[@type='checkbox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        If tdkMasalah Then
                            elementList(0).Click() ' Kirim karakter ke elemen input
                        End If
                        Thread.Sleep(jedarandom) ' Tunggu sebentar
                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Kondisi kendaraan')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(conditionProd).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Jenis bahan bakar')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(fuel).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Transmisi')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(transmission).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If
                    WaitHandle.WaitAny({suspendEvent})
                    '// KETERANGAN PRODUK
                    elementList = Driver.FindElements(
                            By.XPath("//label[@aria-label='Keterangan']/div/div/textarea"))
                    ' Cek apakah elemen ada atau tidak ada
                    'If elementList.Count > 0 Then
                    '    For Each karakter As Char In keterangan
                    '        elementList(0).SendKeys(karakter) ' Kirim karakter ke elemen input
                    '        Thread.Sleep(delayInterval) ' Tunggu sebentar
                    '    Next
                    'End If

                    If elementList.Count > 0 Then
                        form.Invoke(Sub() Clipboard.SetText(keterangan))
                        elementList(0).Click()
                        elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                    End If

                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 4 / 5 / endRow))
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})


                    elementList = Driver.FindElements(
                            By.XPath("//div[@aria-label='Berikutnya']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        If elementList(0).Enabled Then
                            elementList(0).Click()
                        End If
                    End If

                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    Try
                        waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//div[@aria-label='Publikasikan']")))
                    Catch ex As WebDriverTimeoutException
                        statusPost.Status = False
                        statusPost.Message = "tidak bisa mempublikasikan"
                        Return statusPost
                    End Try

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                            By.XPath("//div[@aria-label='Publikasikan']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                    Else
                        statusPost.Status = False
                        statusPost.Message = "Gagal Saat Posting"

                        Return statusPost
                    End If
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    elementList = Driver.FindElements(
                            By.XPath("//div[@aria-label='Tutup']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 AndAlso elementList(0).Enabled Then
                        Dim ariaDisabledValue As String = elementList(0).GetAttribute("aria-disabled")
                        If ariaDisabledValue IsNot Nothing AndAlso
                            ariaDisabledValue.ToLower() <> "true" Then
                            elementList(0).Click()
                        End If
                    End If


                    Thread.Sleep(jedarandom * JedaPost)
                    WaitHandle.WaitAny({suspendEvent})
                End If

                statusPost.Status = True
                statusPost.Message = String.Empty
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = "kolom pada CSV tidak memenuhi kriteria"
                Return statusPost
            End If
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try

        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function

    Public Function PostMobilOnlyDraftFB(jedarandom As Integer, fields As String(), waitforelement As WebDriverWait, tahun As Integer, merek As String,
                                   bodi As Integer, warna As Integer, warna2 As Integer, conditionProd As Integer, fuel As Integer, tdkMasalah As Boolean, transmission As Integer,
                                         form As DataGridView, userId As String, startRow As Integer, endRow As Integer, ByRef suspendEvent As ManualResetEvent) As StatusPost
        Dim statusPost As New StatusPost()
        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing
        Dim delayInterval As Integer = 10 ' Penundaan dalam milidetik (10 ms)

        Dim model As String = String.Empty
        Dim jarakTempuh As Integer = 0
        Dim harga As Integer = 0
        Dim alamat As String = String.Empty
        Dim keterangan As String = String.Empty
        Dim Foto1 As String = String.Empty
        Dim Foto2 As String = String.Empty
        Dim Foto3 As String = String.Empty
        Dim Foto4 As String = String.Empty
        Dim Foto5 As String = String.Empty
        Dim Foto6 As String = String.Empty
        Dim Foto7 As String = String.Empty
        Dim Foto8 As String = String.Empty
        Dim Foto9 As String = String.Empty
        Dim Foto10 As String = String.Empty
        Dim JedaPost As Integer = 0
        Dim JedaDraf As Integer = 0

        Dim prevProgress As Integer = CInt((startRow - 1) * 100 / endRow)
        Try
            If fields.Length >= 17 Then

                '====================================
                '//masuk ke halaman marketplace/create
                Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                Thread.Sleep(jedarandom * 3)
                WaitHandle.WaitAny({suspendEvent})
                elementList =
    Driver.FindElements(By.XPath("//span[contains (text(),'Meninjau Permintaan')]"))

                If elementList.Count > 0 Then
                    statusPost.Status = False
                    statusPost.Message = "Peninjauan Belum disejui Facebook"

                    Return statusPost
                End If
                WaitHandle.WaitAny({suspendEvent})
                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains (@href,'/marketplace/create/vehicle/')]")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                    Return statusPost
                End Try
                elementList =
    Driver.FindElements(By.XPath("//a[contains (@href,'/marketplace/create/vehicle/')]"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    elementList(0).Click()
                Else
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                    Return statusPost
                End If
                Thread.Sleep(jedarandom * 3)
                WaitHandle.WaitAny({suspendEvent})
                '======================================


                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 1 / 5 / endRow))


#Region "Validasi Halaman sebelum proses input"
                elementList =
    Driver.FindElements(By.XPath("//span[text()='Maaf, ada masalah']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If

                Thread.Sleep(jedarandom)
                WaitHandle.WaitAny({suspendEvent})

                elementList =
    Driver.FindElements(By.XPath("//input[@type='file']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count = 0 Then
                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If
#End Region

                WaitHandle.WaitAny({suspendEvent})
                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 2 / 5 / endRow))
                If Not String.IsNullOrEmpty(fields(0)) Then
#Region "proses input"
                    model = (fields(0))
                    jarakTempuh = CInt(fields(1))
                    harga = CInt(fields(2))
                    alamat = fields(3)
                    keterangan = fields(4)
                    Foto1 = fields(5)
                    Foto2 = fields(6)
                    Foto3 = fields(7)
                    Foto4 = fields(8)
                    Foto5 = fields(9)
                    Foto6 = fields(10)
                    Foto7 = fields(11)
                    Foto8 = fields(12)
                    Foto9 = fields(13)
                    Foto10 = fields(14)
                    JedaPost = Convert.ToInt64(fields(15))
                    JedaDraf = Convert.ToInt64(fields(16))

                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    elementList =
    Driver.FindElements(By.XPath("//span[text()='Batas tercapai']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        Thread.Sleep(jedarandom)

                        statusPost.Status = False
                        statusPost.Message = "Batas Posting di marketplace sudah tercapai"

                        Return statusPost
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    Try
                        waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//input[@type='file']")))
                    Catch ex As WebDriverTimeoutException
                        statusPost.Status = False
                        statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                        Return statusPost
                    End Try

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                    If elementList.Count = 0 Then
                        statusPost.Status = False
                        statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                        Return statusPost
                    End If
                    Thread.Sleep(jedarandom * 3)
                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Jenis kendaraan')]"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(0).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    Try

                        ' INPUT FOTO PRODUK POST FBMP
                        Dim fotoList As New List(Of String)()
                        For x As Integer = 1 To 10
                            Dim fotoField As String = fields(x + 4)
                            If Not String.IsNullOrEmpty(fotoField) Then
                                If Not File.Exists(fotoField) Then
                                    statusPost.Status = False
                                    statusPost.Message = "File FOTO " & x & " Tidak ditemukan (Saran simpan foto di folder document)"

                                    Return statusPost
                                End If

                                fotoList.Add(fotoField)
                            End If
                        Next
                        If fotoList.Count > 0 Then
                            Dim fotoString As String = String.Join(vbNewLine, fotoList)
                            Dim inputFoto As IWebElement = Driver.FindElement(By.XPath("//input[@type='file']"))
                            inputFoto.SendKeys(fotoString)
                            Thread.Sleep(fotoList.Count)
                        End If
                    Catch ex As Exception
                    End Try
                    WaitHandle.WaitAny({suspendEvent})

                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Tahun')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(tahun).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Merek')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            For Each element In elementList
                                Try
                                    Dim elementOptn As IReadOnlyCollection(Of IWebElement) = element.FindElements(By.XPath("div/div/div/span"))
                                    If elementOptn.Count > 0 AndAlso elementOptn(0).Text = merek Then
                                        element.Click()
                                        Exit For
                                    End If
                                Catch ex As Exception
                                End Try
                            Next
                        End If
                        Thread.Sleep(jedarandom)
                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    '// JML KMR MODEL
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Model')][@role='combobox']"))
                    Dim indexInput = 0
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            For Each element In elementList
                                Try
                                    Dim elementOptn As IReadOnlyCollection(Of IWebElement) = element.FindElements(By.XPath("div/div/div/span"))
                                    If elementOptn.Count > 0 AndAlso elementOptn(0).Text = model Then
                                        element.Click()
                                        Exit For
                                    End If
                                Catch ex As Exception
                                End Try
                            Next
                        End If
                        Thread.Sleep(jedarandom)
                        WaitHandle.WaitAny({suspendEvent})
                        elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                        indexInput = 1
                    Else
                        Thread.Sleep(jedarandom)
                        WaitHandle.WaitAny({suspendEvent})
                        elementList = Driver.FindElements(By.XPath("//input[@type='text']"))

                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 1 Then
                            elementList(1).SendKeys(model) ' Kirim karakter ke elemen input
                            Thread.Sleep(jedarandom) ' Tunggu sebentar
                        End If
                        indexInput = 0
                    End If
                    '// PILH PALING ATAS
                    WaitHandle.WaitAny({suspendEvent})
                    Try
                        ' Temukan elemen dengan atribut role="listbox"
                        Dim listBoxElement As IWebElement = Driver.FindElement(By.XPath("//*[@role='listbox']"))

                        If listBoxElement IsNot Nothing Then
                            ' Temukan pilihan pertama dalam listbox
                            Dim firstOption As IWebElement = listBoxElement.FindElement(By.XPath(".//div[contains(@role, 'option')][1]"))

                            If firstOption IsNot Nothing Then
                                ' Klik pilihan pertama dalam listbox
                                firstOption.Click()
                            End If
                        End If
                    Catch ex As Exception
                    End Try
                    WaitHandle.WaitAny({suspendEvent})
                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 3 / 5 / endRow))

                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 2 - indexInput Then
                        elementList(2 - indexInput).SendKeys(jarakTempuh) ' Kirim karakter ke elemen input
                        Thread.Sleep(jedarandom) ' Tunggu sebentar
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    '// HARGA
                    If elementList.Count > 3 - indexInput Then
                        elementList(3 - indexInput).SendKeys(harga) ' Kirim karakter ke elemen input
                        Thread.Sleep(jedarandom) ' Tunggu sebentar
                    End If

                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Tipe body')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(bodi).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Warna eksterior')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(warna).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Warna eksterior')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(warna).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Warna Interior')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(warna2).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    '// JML KMR MODEL
                    elementList = Driver.FindElements(By.XPath("//input[@type='checkbox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        If tdkMasalah Then
                            elementList(0).Click() ' Kirim karakter ke elemen input
                        End If
                        Thread.Sleep(jedarandom) ' Tunggu sebentar
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Kondisi kendaraan')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(conditionProd).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Jenis bahan bakar')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(fuel).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Transmisi')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(transmission).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If
                    WaitHandle.WaitAny({suspendEvent})
                    '// KETERANGAN PRODUK
                    elementList = Driver.FindElements(
                            By.XPath("//label[@aria-label='Keterangan']/div/div/textarea"))
                    ' Cek apakah elemen ada atau tidak ada
                    'If elementList.Count > 0 Then
                    '    For Each karakter As Char In keterangan
                    '        elementList(0).SendKeys(karakter) ' Kirim karakter ke elemen input
                    '        Thread.Sleep(delayInterval) ' Tunggu sebentar
                    '    Next
                    'End If
                    If elementList.Count > 0 Then
                        form.Invoke(Sub() Clipboard.SetText(keterangan))
                        elementList(0).Click()
                        elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                    End If

                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 4 / 5 / endRow))
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    'Thread.Sleep(jedarandom)

                    Thread.Sleep(jedarandom * 2)

                    WaitHandle.WaitAny({suspendEvent})
                    '// KLIK SIMPAN DRAF
                    elementList = Driver.FindElements(
                                                  By.XPath("//div[contains(@aria-label,'Simpan draf')]"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        Try
                            elementList(0).Click()
                        Catch ex As Exception

                        End Try
                    End If

                    Thread.Sleep(jedarandom)

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                                By.XPath("//div[@aria-label='Tutup']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 AndAlso elementList(0).Enabled Then
                        Dim ariaDisabledValue As String = elementList(0).GetAttribute("aria-disabled")
                        If ariaDisabledValue IsNot Nothing AndAlso
                            ariaDisabledValue.ToLower() <> "true" Then
                            elementList(0).Click()
                        End If
                    End If


                    Thread.Sleep(jedarandom * JedaDraf)
                    WaitHandle.WaitAny({suspendEvent})
#End Region
                End If

                statusPost.Status = True
                statusPost.Message = String.Empty
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = "kolom pada CSV tidak memenuhi kriteria"
                Return statusPost
            End If
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try

        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function

    '//Function untuk edit Draft dan Posting
    Public Function EditMobilFromDraftFB(jedarandom As Integer, fields As String(), waitforelement As WebDriverWait, tahun As Integer, merek As String,
                                        bodi As Integer, warna As Integer, warna2 As Integer, conditionProd As Integer, fuel As Integer, tdkMasalah As Boolean, transmission As Integer,
                                         form As DataGridView, userId As String, startRow As Integer, endRow As Integer, ByRef suspendEvent As ManualResetEvent) As StatusPost
        Dim statusPost As New StatusPost()
        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing
        Dim delayInterval As Integer = 10 ' Penundaan dalam milidetik (10 ms)

        Dim prevProgress As Integer = CInt((startRow - 1) * 100 / endRow)
        Try
            If fields.Length >= 17 Then
                Dim LokasiProdukku As String = fields(3)
                Dim JedaPost As Integer = Convert.ToInt64(fields(15))
                Dim UrlPost As String = "https://www.facebook.com"
                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 1 / 5 / endRow))
                WaitHandle.WaitAny({suspendEvent})

                Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                Thread.Sleep(jedarandom)
                WaitHandle.WaitAny({suspendEvent})
                Driver.Navigate.Refresh()
                Thread.Sleep(jedarandom * 3)
                WaitHandle.WaitAny({suspendEvent})

                elementList =
    Driver.FindElements(By.XPath("//span[contains (text(),'Meninjau Permintaan')]"))

                If elementList.Count > 0 Then
                    statusPost.Status = False
                    statusPost.Message = "Peninjauan Belum disejui Facebook"

                    Return statusPost
                End If
                WaitHandle.WaitAny({suspendEvent})

                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains (@href,'/marketplace/create/item/')]")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                    Return statusPost
                End Try
                '// KLIK PILIH DRAFT
                WaitHandle.WaitAny({suspendEvent})
                elementList = Driver.FindElements(
                                              By.XPath("//a[contains(@href,'/marketplace/edit/?listing_id')]"))

                If elementList.Count > 0 Then
                    '//Pilih Draft jika tersedia
                    elementList(0).Click()
                    UrlPost = Driver.Url
                    Thread.Sleep(jedarandom)
                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 2 / 5 / endRow))

                    'Proses Posting Dari Draft
                    statusPost = postMobilFromDraft(jedarandom, LokasiProdukku, JedaPost, UrlPost, suspendEvent)
                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 4 / 5 / endRow))
                    WaitHandle.WaitAny({suspendEvent})
                    Return statusPost
                Else
                    '//Buat Draft Baru jika tidak menemukan Draft
                    statusPost = PostMobilOnlyDraftFB(jedarandom, fields, waitforelement, tahun, merek, bodi,
                                                      warna, warna2, conditionProd, fuel, tdkMasalah, transmission, form, userId, startRow, endRow, suspendEvent)

                    If statusPost.Status Then
                        Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                        Thread.Sleep(jedarandom)
                        WaitHandle.WaitAny({suspendEvent})
                        Driver.Navigate.Refresh()
                        Thread.Sleep(jedarandom * 3)
                        WaitHandle.WaitAny({suspendEvent})

                        Try
                            waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains (@href,'/marketplace/create/item/')]")))
                        Catch ex As WebDriverTimeoutException
                            statusPost.Status = False
                            statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                            Return statusPost
                        End Try

                        WaitHandle.WaitAny({suspendEvent})
                        '// KLIK PILIH DRAFT
                        elementList = Driver.FindElements(
                                                      By.XPath("//a[contains(@href,'/marketplace/edit/?listing_id')]"))

                        If elementList.Count > 0 Then
                            elementList(0).Click()
                            UrlPost = Driver.Url
                            Thread.Sleep(jedarandom)

                            postMobilFromDraft(jedarandom, LokasiProdukku, JedaPost, UrlPost, suspendEvent)
                            WaitHandle.WaitAny({suspendEvent})
                        End If
                    Else
                        Return statusPost
                    End If
                End If
            Else
                statusPost.Status = False
                statusPost.Message = "kolom pada CSV tidak memenuhi kriteria"
                Return statusPost
            End If
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try

        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function

    Public Function postMobilFromDraft(jedarandom As Integer, LokasiProdukku As String, JedaPost As Integer, UrlPost As String, ByRef suspendEvent As ManualResetEvent) As StatusPost
        Dim statusPost As New StatusPost()
        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing

        Try
            Driver.GoToUrl(UrlPost)
            Thread.Sleep(jedarandom * 2)
            WaitHandle.WaitAny({suspendEvent})
            Driver.Navigate.Refresh()
            Thread.Sleep(jedarandom * 2)
            WaitHandle.WaitAny({suspendEvent})
            elementList =
    Driver.FindElements(By.XPath("//span[text()='Maaf, ada masalah']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 Then
                Thread.Sleep(jedarandom)

                statusPost.Status = False
                statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                Return statusPost
            End If
            WaitHandle.WaitAny({suspendEvent})

            elementList =
    Driver.FindElements(By.XPath("//input[@type='file']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count = 0 Then
                Thread.Sleep(jedarandom)

                statusPost.Status = False
                statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                Return statusPost
            End If
            WaitHandle.WaitAny({suspendEvent})

            '// LOKASI
            Dim ulangi As Boolean = True
            Dim alamatStr = LokasiProdukku
            Do While ulangi
                elementList = Driver.FindElements(
                       By.XPath("//input[contains (@aria-label,'Lokasi')][@role='combobox'][@type='text']"))
                If elementList.Count > 0 Then

                    Thread.Sleep(jedarandom)
                    elementList(0).SendKeys(Keys.Control & "a") ' Memilih semua teks dalam elemen
                    Thread.Sleep(jedarandom)
                    elementList(0).SendKeys(Keys.Delete)
                    Thread.Sleep(jedarandom)
                    elementList(0).SendKeys(alamatStr)
                    Thread.Sleep(jedarandom)
                    elementList = Driver.FindElements(
                          By.XPath("//ul[contains (@aria-label,'disarankan')]/li"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        ulangi = False
                        Thread.Sleep(jedarandom)
                    Else
                        If alamatStr.Length <> 0 Then
                            alamatStr = alamatStr.Substring(0, alamatStr.Length - 1)
                        Else
                            ulangi = False
                        End If
                    End If

                End If
            Loop
            Thread.Sleep(jedarandom)
            WaitHandle.WaitAny({suspendEvent})

            elementList = Driver.FindElements(
                    By.XPath("//div[@aria-label='Berikutnya']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 Then
                If elementList(0).Enabled Then
                    elementList(0).Click()
                End If
            End If

            Thread.Sleep(jedarandom)
            WaitHandle.WaitAny({suspendEvent})
            Dim waitforelement As WebDriverWait = New WebDriverWait(Driver, TimeSpan.FromSeconds(waitElement))

            Try
                waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//div[@aria-label='Publikasikan']")))
            Catch ex As WebDriverTimeoutException
                statusPost.Status = False
                statusPost.Message = "tidak bisa mempublikasikan"
                Return statusPost
            End Try

            WaitHandle.WaitAny({suspendEvent})
            elementList = Driver.FindElements(
                    By.XPath("//div[@aria-label='Publikasikan']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 Then
                elementList(0).Click()
            Else
                statusPost.Status = False
                statusPost.Message = "Gagal Saat Posting"

                Return statusPost
            End If

            Thread.Sleep(jedarandom)
            WaitHandle.WaitAny({suspendEvent})

            elementList = Driver.FindElements(
                    By.XPath("//div[@aria-label='Tutup']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 AndAlso elementList(0).Enabled Then
                Dim ariaDisabledValue As String = elementList(0).GetAttribute("aria-disabled")
                If ariaDisabledValue IsNot Nothing AndAlso
                            ariaDisabledValue.ToLower() <> "true" Then
                    elementList(0).Click()
                End If
            End If


            Thread.Sleep(jedarandom * JedaPost)
            WaitHandle.WaitAny({suspendEvent})
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try

        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function

#End Region

#Region "Kumpulan Fungsi Posting Motor/ Draft / posting & Draft"
    Public Function PostMotorFB(jedarandom As Integer, fields As String(), waitforelement As WebDriverWait, tahun As Integer, merek As String,
                                warna As Integer, fuel As Integer, transmission As Integer, form As DataGridView, userId As String, startRow As Integer,
                                endRow As Integer, ByRef suspendEvent As ManualResetEvent) As StatusPost
        Dim statusPost As New StatusPost()

        Dim model As String = String.Empty
        Dim jarakTempuh As Integer = 0
        Dim harga As Integer = 0
        Dim alamat As String = String.Empty
        Dim keterangan As String = String.Empty
        Dim Foto1 As String = String.Empty
        Dim Foto2 As String = String.Empty
        Dim Foto3 As String = String.Empty
        Dim Foto4 As String = String.Empty
        Dim Foto5 As String = String.Empty
        Dim Foto6 As String = String.Empty
        Dim Foto7 As String = String.Empty
        Dim Foto8 As String = String.Empty
        Dim Foto9 As String = String.Empty
        Dim Foto10 As String = String.Empty
        Dim JedaPost As Integer = 0
        Dim JedaDraf As Integer = 0


        Dim delayInterval As Integer = 10 ' Penundaan dalam milidetik (10 ms)

        Dim prevProgress As Integer = CInt((startRow - 1) * 100 / endRow)
        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing

        Try
            If fields.Length >= 17 Then

                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 1 / 5 / endRow))
                WaitHandle.WaitAny({suspendEvent})

                Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                Thread.Sleep(jedarandom * 3)
                WaitHandle.WaitAny({suspendEvent})
                elementList =
    Driver.FindElements(By.XPath("//span[contains (text(),'Meninjau Permintaan')]"))

                If elementList.Count > 0 Then
                    statusPost.Status = False
                    statusPost.Message = "Peninjauan Belum disejui Facebook"

                    Return statusPost
                End If
                WaitHandle.WaitAny({suspendEvent})
                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains (@href,'/marketplace/create/vehicle/')]")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                    Return statusPost
                End Try
                WaitHandle.WaitAny({suspendEvent})
                elementList =
    Driver.FindElements(By.XPath("//a[contains (@href,'/marketplace/create/vehicle/')]"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    elementList(0).Click()
                Else
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                    Return statusPost
                End If
                Thread.Sleep(jedarandom * 3)
                WaitHandle.WaitAny({suspendEvent})

                elementList =
    Driver.FindElements(By.XPath("//span[text()='Maaf, ada masalah']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then


                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If

                Thread.Sleep(jedarandom)
                WaitHandle.WaitAny({suspendEvent})

                elementList =
    Driver.FindElements(By.XPath("//input[@type='file']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count = 0 Then
                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If


                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 2 / 5 / endRow))

                WaitHandle.WaitAny({suspendEvent})
                If Not String.IsNullOrEmpty(fields(0)) Then

                    model = (fields(0))
                    jarakTempuh = CInt(fields(1))
                    harga = CInt(fields(2))
                    alamat = fields(3)
                    keterangan = fields(4)
                    Foto1 = fields(5)
                    Foto2 = fields(6)
                    Foto3 = fields(7)
                    Foto4 = fields(8)
                    Foto5 = fields(9)
                    Foto6 = fields(10)
                    Foto7 = fields(11)
                    Foto8 = fields(12)
                    Foto9 = fields(13)
                    Foto10 = fields(14)
                    JedaPost = Convert.ToInt64(fields(15))
                    JedaDraf = Convert.ToInt64(fields(16))

                    Thread.Sleep(jedarandom)

                    WaitHandle.WaitAny({suspendEvent})
                    elementList =
    Driver.FindElements(By.XPath("//span[text()='Batas tercapai']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        Thread.Sleep(jedarandom)

                        statusPost.Status = False
                        statusPost.Message = "Batas Posting di marketplace sudah tercapai"

                        Return statusPost
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    Try
                        waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//input[@type='text']")))
                    Catch ex As WebDriverTimeoutException
                        statusPost.Status = False
                        statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                        Return statusPost
                    End Try

                    elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                    If elementList.Count = 0 Then
                        statusPost.Status = False
                        statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                        Return statusPost
                    End If
                    Thread.Sleep(jedarandom * 3)
                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Jenis kendaraan')]"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 1 Then
                            elementList(1).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    Try
                        ' INPUT FOTO PRODUK POST FBMP
                        Dim fotoList As New List(Of String)()
                        For x As Integer = 1 To 10
                            Dim fotoField As String = fields(x + 4)
                            If Not String.IsNullOrEmpty(fotoField) Then
                                If Not File.Exists(fotoField) Then
                                    statusPost.Status = False
                                    statusPost.Message = "File FOTO " & x & " Tidak ditemukan (Saran simpan foto di folder document)"

                                    Return statusPost
                                End If

                                fotoList.Add(fotoField)
                            End If
                        Next
                        If fotoList.Count > 0 Then
                            Dim fotoString As String = String.Join(vbNewLine, fotoList)
                            Dim inputFoto As IWebElement = Driver.FindElement(By.XPath("//input[@type='file']"))
                            inputFoto.SendKeys(fotoString)
                            Thread.Sleep(fotoList.Count)
                        End If
                    Catch ex As Exception
                    End Try

                    WaitHandle.WaitAny({suspendEvent})

                    '// LOKASI
                    Dim ulangi As Boolean = True
                    Dim alamatStr = alamat
                    Do While ulangi
                        elementList = Driver.FindElements(
                       By.XPath("//input[contains (@aria-label,'Lokasi')][@role='combobox'][@type='text']"))
                        If elementList.Count > 0 Then

                            Thread.Sleep(jedarandom)
                            elementList(0).SendKeys(Keys.Control & "a") ' Memilih semua teks dalam elemen
                            Thread.Sleep(jedarandom)
                            elementList(0).SendKeys(Keys.Delete)
                            Thread.Sleep(jedarandom)
                            elementList(0).SendKeys(alamatStr)
                            Thread.Sleep(jedarandom)
                            elementList = Driver.FindElements(
                          By.XPath("//ul[contains (@aria-label,'disarankan')]/li"))
                            ' Cek apakah elemen ada atau tidak ada
                            If elementList.Count > 0 Then
                                elementList(0).Click()
                                ulangi = False
                                Thread.Sleep(jedarandom)
                            Else
                                If alamatStr.Length <> 0 Then
                                    alamatStr = alamatStr.Substring(0, alamatStr.Length - 1)
                                Else
                                    ulangi = False
                                End If
                            End If

                        End If
                    Loop


                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Tahun')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(tahun).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Merek')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            For Each element In elementList
                                Try
                                    Dim elementOptn As IReadOnlyCollection(Of IWebElement) = element.FindElements(By.XPath("div/div/div/span"))
                                    If elementOptn.Count > 0 AndAlso elementOptn(0).Text = merek Then
                                        element.Click()
                                        Exit For
                                    End If
                                Catch ex As Exception
                                End Try
                            Next
                        End If
                        Thread.Sleep(jedarandom)
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    '// JML KMR MODEL
                    elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 1 Then
                        elementList(1).SendKeys(model) ' Kirim karakter ke elemen input
                        Thread.Sleep(jedarandom) ' Tunggu sebentar
                    End If

                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 3 / 5 / endRow))

                    WaitHandle.WaitAny({suspendEvent})
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 2 Then
                        elementList(2).SendKeys(jarakTempuh) ' Kirim karakter ke elemen input
                        Thread.Sleep(jedarandom) ' Tunggu sebentar
                    End If

                    '// HARGA
                    If elementList.Count > 3 Then
                        elementList(3).SendKeys(harga) ' Kirim karakter ke elemen input
                        Thread.Sleep(jedarandom) ' Tunggu sebentar
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Warna eksterior')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(warna).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Warna eksterior')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(warna).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If
                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Jenis bahan bakar')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(fuel).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Transmisi')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(transmission).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If
                    '// KETERANGAN PRODUK
                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                            By.XPath("//label[@aria-label='Keterangan']/div/div/textarea"))
                    ' Cek apakah elemen ada atau tidak ada
                    'If elementList.Count > 0 Then
                    '    For Each karakter As Char In keterangan
                    '        elementList(0).SendKeys(karakter) ' Kirim karakter ke elemen input
                    '        Thread.Sleep(delayInterval) ' Tunggu sebentar
                    '    Next
                    'End If
                    If elementList.Count > 0 Then
                        form.Invoke(Sub() Clipboard.SetText(keterangan))
                        elementList(0).Click()
                        elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                    End If

                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 4 / 5 / endRow))
                    Thread.Sleep(jedarandom)


                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                            By.XPath("//div[@aria-label='Berikutnya']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        If elementList(0).Enabled Then
                            elementList(0).Click()
                        End If
                    End If

                    Thread.Sleep(jedarandom)

                    WaitHandle.WaitAny({suspendEvent})
                    Try
                        waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//div[@aria-label='Publikasikan']")))
                    Catch ex As WebDriverTimeoutException
                        statusPost.Status = False
                        statusPost.Message = "tidak bisa mempublikasikan"
                        Return statusPost
                    End Try

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                            By.XPath("//div[@aria-label='Publikasikan']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                    Else
                        statusPost.Status = False
                        statusPost.Message = "Gagal Saat Posting"

                        Return statusPost
                    End If
                    Thread.Sleep(jedarandom)

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                            By.XPath("//div[@aria-label='Tutup']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 AndAlso elementList(0).Enabled Then
                        Dim ariaDisabledValue As String = elementList(0).GetAttribute("aria-disabled")
                        If ariaDisabledValue IsNot Nothing AndAlso
                            ariaDisabledValue.ToLower() <> "true" Then
                            elementList(0).Click()
                        End If
                    End If


                    Thread.Sleep(jedarandom * JedaPost)
                    WaitHandle.WaitAny({suspendEvent})
                End If
                statusPost.Status = True
                statusPost.Message = String.Empty
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = "kolom pada CSV tidak memenuhi kriteria"
                Return statusPost
            End If
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try

        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function

    Public Function PostMotorOnlyDraftFB(jedarandom As Integer, fields As String(), waitforelement As WebDriverWait, tahun As Integer, merek As String,
                                         warna As Integer, fuel As Integer, transmission As Integer, form As DataGridView, userId As String, startRow As Integer,
                                         endRow As Integer, ByRef suspendEvent As ManualResetEvent) As StatusPost
        Dim statusPost As New StatusPost()
        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing
        Dim delayInterval As Integer = 10 ' Penundaan dalam milidetik (10 ms)

        Dim model As String = String.Empty
        Dim jarakTempuh As Integer = 0
        Dim harga As Integer = 0
        Dim alamat As String = String.Empty
        Dim keterangan As String = String.Empty
        Dim Foto1 As String = String.Empty
        Dim Foto2 As String = String.Empty
        Dim Foto3 As String = String.Empty
        Dim Foto4 As String = String.Empty
        Dim Foto5 As String = String.Empty
        Dim Foto6 As String = String.Empty
        Dim Foto7 As String = String.Empty
        Dim Foto8 As String = String.Empty
        Dim Foto9 As String = String.Empty
        Dim Foto10 As String = String.Empty
        Dim JedaPost As Integer = 0
        Dim JedaDraf As Integer = 0

        Dim prevProgress As Integer = CInt((startRow - 1) * 100 / endRow)
        Try
            If fields.Length >= 17 Then

                '====================================
                '//masuk ke halaman marketplace/create
                Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                Thread.Sleep(jedarandom * 3)
                WaitHandle.WaitAny({suspendEvent})
                elementList =
    Driver.FindElements(By.XPath("//span[contains (text(),'Meninjau Permintaan')]"))

                If elementList.Count > 0 Then
                    statusPost.Status = False
                    statusPost.Message = "Peninjauan Belum disejui Facebook"

                    Return statusPost
                End If
                WaitHandle.WaitAny({suspendEvent})
                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains (@href,'/marketplace/create/vehicle/')]")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                    Return statusPost
                End Try
                WaitHandle.WaitAny({suspendEvent})
                elementList =
    Driver.FindElements(By.XPath("//a[contains (@href,'/marketplace/create/vehicle/')]"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    elementList(0).Click()
                Else
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                    Return statusPost
                End If
                Thread.Sleep(jedarandom * 3)
                WaitHandle.WaitAny({suspendEvent})
                '======================================


                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 1 / 5 / endRow))


#Region "Validasi Halaman sebelum proses input"
                WaitHandle.WaitAny({suspendEvent})
                elementList =
    Driver.FindElements(By.XPath("//span[text()='Maaf, ada masalah']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count > 0 Then
                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If

                Thread.Sleep(jedarandom)
                WaitHandle.WaitAny({suspendEvent})

                elementList =
    Driver.FindElements(By.XPath("//input[@type='file']"))
                ' Cek apakah elemen ada atau tidak ada
                If elementList.Count = 0 Then
                    Thread.Sleep(jedarandom)

                    statusPost.Status = False
                    statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                    Return statusPost
                End If
#End Region

                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 2 / 5 / endRow))
                WaitHandle.WaitAny({suspendEvent})
                If Not String.IsNullOrEmpty(fields(0)) Then
#Region "proses input"
                    model = (fields(0))
                    jarakTempuh = CInt(fields(1))
                    harga = CInt(fields(2))
                    alamat = fields(3)
                    keterangan = fields(4)
                    Foto1 = fields(5)
                    Foto2 = fields(6)
                    Foto3 = fields(7)
                    Foto4 = fields(8)
                    Foto5 = fields(9)
                    Foto6 = fields(10)
                    Foto7 = fields(11)
                    Foto8 = fields(12)
                    Foto9 = fields(13)
                    Foto10 = fields(14)
                    JedaPost = Convert.ToInt64(fields(15))
                    JedaDraf = Convert.ToInt64(fields(16))

                    Thread.Sleep(jedarandom)

                    WaitHandle.WaitAny({suspendEvent})
                    elementList =
    Driver.FindElements(By.XPath("//span[text()='Batas tercapai']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        Thread.Sleep(jedarandom)

                        statusPost.Status = False
                        statusPost.Message = "Batas Posting di marketplace sudah tercapai"

                        Return statusPost
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    Try
                        waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//input[@type='text']")))
                    Catch ex As WebDriverTimeoutException
                        statusPost.Status = False
                        statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                        Return statusPost
                    End Try

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                    If elementList.Count = 0 Then
                        statusPost.Status = False
                        statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                        Return statusPost
                    End If
                    Thread.Sleep(jedarandom * 3)
                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Jenis kendaraan')]"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 1 Then
                            elementList(1).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    Try
                        ' INPUT FOTO PRODUK POST FBMP
                        Dim fotoList As New List(Of String)()
                        For x As Integer = 1 To 10
                            Dim fotoField As String = fields(x + 4)
                            If Not String.IsNullOrEmpty(fotoField) Then
                                If Not File.Exists(fotoField) Then
                                    statusPost.Status = False
                                    statusPost.Message = "File FOTO " & x & " Tidak ditemukan (Saran simpan foto di folder document)"

                                    Return statusPost
                                End If

                                fotoList.Add(fotoField)
                            End If
                        Next
                        If fotoList.Count > 0 Then
                            Dim fotoString As String = String.Join(vbNewLine, fotoList)
                            Dim inputFoto As IWebElement = Driver.FindElement(By.XPath("//input[@type='file']"))
                            inputFoto.SendKeys(fotoString)
                            Thread.Sleep(fotoList.Count)
                        End If
                    Catch ex As Exception
                    End Try

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Tahun')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(tahun).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Merek')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            For Each element In elementList
                                Try
                                    Dim elementOptn As IReadOnlyCollection(Of IWebElement) = element.FindElements(By.XPath("div/div/div/span"))
                                    If elementOptn.Count > 0 AndAlso elementOptn(0).Text = merek Then
                                        element.Click()
                                        Exit For
                                    End If
                                Catch ex As Exception
                                End Try
                            Next
                        End If
                        Thread.Sleep(jedarandom)
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    '// JML KMR MODEL
                    elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 1 Then
                        elementList(1).SendKeys(model) ' Kirim karakter ke elemen input
                        Thread.Sleep(jedarandom) ' Tunggu sebentar
                    End If

                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 3 / 5 / endRow))

                    WaitHandle.WaitAny({suspendEvent})
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 2 Then
                        elementList(2).SendKeys(jarakTempuh) ' Kirim karakter ke elemen input
                        Thread.Sleep(jedarandom) ' Tunggu sebentar
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    '// HARGA
                    If elementList.Count > 3 Then
                        elementList(3).SendKeys(harga) ' Kirim karakter ke elemen input
                        Thread.Sleep(jedarandom) ' Tunggu sebentar
                    End If


                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Warna eksterior')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(warna).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Warna eksterior')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(warna).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If


                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Jenis bahan bakar')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(fuel).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                       By.XPath("//label[contains (@aria-label,'Transmisi')][@role='combobox']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                        elementList = Driver.FindElements(
                        By.XPath("//div[contains (@role,'option')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(transmission).Click()
                        End If
                        Thread.Sleep(jedarandom)
                    End If
                    WaitHandle.WaitAny({suspendEvent})
                    '// KETERANGAN PRODUK
                    elementList = Driver.FindElements(
                            By.XPath("//label[@aria-label='Keterangan']/div/div/textarea"))
                    ' Cek apakah elemen ada atau tidak ada
                    'If elementList.Count > 0 Then
                    '    For Each karakter As Char In keterangan
                    '        elementList(0).SendKeys(karakter) ' Kirim karakter ke elemen input
                    '        Thread.Sleep(delayInterval) ' Tunggu sebentar
                    '    Next
                    'End If
                    If elementList.Count > 0 Then
                        form.Invoke(Sub() Clipboard.SetText(keterangan))
                        elementList(0).Click()
                        elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                    End If

                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 4 / 5 / endRow))
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    'Thread.Sleep(jedarandom)

                    Thread.Sleep(jedarandom * 2)

                    WaitHandle.WaitAny({suspendEvent})
                    '// KLIK SIMPAN DRAF
                    elementList = Driver.FindElements(
                                                  By.XPath("//div[contains(@aria-label,'Simpan draf')]"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        Try
                            elementList(0).Click()
                        Catch ex As Exception

                        End Try
                    End If

                    Thread.Sleep(jedarandom)

                    WaitHandle.WaitAny({suspendEvent})
                    elementList = Driver.FindElements(
                                By.XPath("//div[@aria-label='Tutup']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 AndAlso elementList(0).Enabled Then
                        Dim ariaDisabledValue As String = elementList(0).GetAttribute("aria-disabled")
                        If ariaDisabledValue IsNot Nothing AndAlso
                            ariaDisabledValue.ToLower() <> "true" Then
                            elementList(0).Click()
                        End If
                    End If


                    Thread.Sleep(jedarandom * JedaDraf)
                    WaitHandle.WaitAny({suspendEvent})

#End Region
                End If

                statusPost.Status = True
                statusPost.Message = String.Empty
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = "kolom pada CSV tidak memenuhi kriteria"
                Return statusPost
            End If
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try

        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function

    '//Function untuk edit Draft dan Posting
    Public Function EditMotorFromDraftFB(jedarandom As Integer, fields As String(), waitforelement As WebDriverWait, tahun As Integer, merek As String,
                                        warna As Integer, fuel As Integer, transmission As Integer, form As DataGridView, userId As String, startRow As Integer,
                                         endRow As Integer, ByRef suspendEvent As ManualResetEvent) As StatusPost
        Dim statusPost As New StatusPost()
        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing
        Dim delayInterval As Integer = 10 ' Penundaan dalam milidetik (10 ms)

        Dim prevProgress As Integer = CInt((startRow - 1) * 100 / endRow)
        Try
            If fields.Length >= 17 Then
                Dim LokasiProdukku As String = fields(3)
                Dim JedaPost As Integer = Convert.ToInt64(fields(15))
                Dim UrlPost As String = "https://www.facebook.com"
                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 1 / 5 / endRow))

                Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                Thread.Sleep(jedarandom)
                WaitHandle.WaitAny({suspendEvent})
                Driver.Navigate.Refresh()
                Thread.Sleep(jedarandom * 3)
                WaitHandle.WaitAny({suspendEvent})

                elementList =
    Driver.FindElements(By.XPath("//span[contains (text(),'Meninjau Permintaan')]"))

                If elementList.Count > 0 Then
                    statusPost.Status = False
                    statusPost.Message = "Peninjauan Belum disejui Facebook"

                    Return statusPost
                End If
                WaitHandle.WaitAny({suspendEvent})

                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains (@href,'/marketplace/create/item/')]")))
                Catch ex As WebDriverTimeoutException
                    statusPost.Status = False
                    statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                    Return statusPost
                End Try
                WaitHandle.WaitAny({suspendEvent})
                '// KLIK PILIH DRAFT
                elementList = Driver.FindElements(
                                              By.XPath("//a[contains(@href,'/marketplace/edit/?listing_id')]"))

                If elementList.Count > 0 Then
                    '//Pilih Draft jika tersedia
                    elementList(0).Click()
                    UrlPost = Driver.Url
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})
                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 2 / 5 / endRow))

                    'Proses Posting Dari Draft
                    statusPost = postMotorFromDraft(jedarandom, LokasiProdukku, JedaPost, UrlPost, suspendEvent)
                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 4 / 5 / endRow))
                    Return statusPost
                Else
                    '//Buat Draft Baru jika tidak menemukan Draft
                    statusPost = PostMotorOnlyDraftFB(jedarandom, fields, waitforelement, tahun, merek,
                                                      warna, fuel, transmission, form, userId, startRow, endRow, suspendEvent)

                    If statusPost.Status Then
                        Driver.GoToUrl("https://www.facebook.com/marketplace/create")
                        WaitHandle.WaitAny({suspendEvent})
                        Thread.Sleep(jedarandom)
                        Driver.Navigate.Refresh()
                        Thread.Sleep(jedarandom * 3)
                        WaitHandle.WaitAny({suspendEvent})

                        Try
                            waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains(@href,'/marketplace/edit/?listing_id')]")))
                        Catch ex As WebDriverTimeoutException
                            statusPost.Status = False
                            statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                            Return statusPost
                        End Try

                        WaitHandle.WaitAny({suspendEvent})
                        '// KLIK PILIH DRAFT
                        elementList = Driver.FindElements(
                                                      By.XPath("//a[contains(@href,'/marketplace/edit/?listing_id')]"))

                        If elementList.Count > 0 Then
                            elementList(0).Click()
                            UrlPost = Driver.Url
                            Thread.Sleep(jedarandom)
                            WaitHandle.WaitAny({suspendEvent})

                            postMotorFromDraft(jedarandom, LokasiProdukku, JedaPost, UrlPost, suspendEvent)
                        End If
                    Else
                        Return statusPost
                    End If
                End If
            Else
                statusPost.Status = False
                statusPost.Message = "kolom pada CSV tidak memenuhi kriteria"
                Return statusPost
            End If
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try

        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function

    Public Function postMotorFromDraft(jedarandom As Integer, LokasiProdukku As String, JedaPost As Integer, UrlPost As String, ByRef suspendEvent As ManualResetEvent) As StatusPost
        Dim statusPost As New StatusPost()
        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing
        Dim waitforelement As WebDriverWait = New WebDriverWait(Driver, TimeSpan.FromSeconds(waitElement))

        Try
            Driver.GoToUrl(UrlPost)
            Thread.Sleep(jedarandom * 2)
            WaitHandle.WaitAny({suspendEvent})
            Driver.Navigate.Refresh()
            Thread.Sleep(jedarandom * 2)
            WaitHandle.WaitAny({suspendEvent})

            elementList =
    Driver.FindElements(By.XPath("//span[text()='Maaf, ada masalah']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 Then
                Thread.Sleep(jedarandom)
                WaitHandle.WaitAny({suspendEvent})

                statusPost.Status = False
                statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                Return statusPost
            End If

            WaitHandle.WaitAny({suspendEvent})
            Try
                waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//input[@type='file']")))
            Catch ex As WebDriverTimeoutException
                statusPost.Status = False
                statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                Return statusPost
            End Try

            WaitHandle.WaitAny({suspendEvent})
            elementList =
    Driver.FindElements(By.XPath("//input[@type='file']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count = 0 Then
                Thread.Sleep(jedarandom)

                statusPost.Status = False
                statusPost.Message = "Terdapat Masalah ketika mengakses halaman create item di marketplace"

                Return statusPost
            End If

            WaitHandle.WaitAny({suspendEvent})
            '// LOKASI
            Dim ulangi As Boolean = True
            Dim alamatStr = LokasiProdukku
            Do While ulangi
                elementList = Driver.FindElements(
                       By.XPath("//input[contains (@aria-label,'Lokasi')][@role='combobox'][@type='text']"))
                If elementList.Count > 0 Then

                    Thread.Sleep(jedarandom)
                    elementList(0).SendKeys(Keys.Control & "a") ' Memilih semua teks dalam elemen
                    Thread.Sleep(jedarandom)
                    elementList(0).SendKeys(Keys.Delete)
                    Thread.Sleep(jedarandom)
                    elementList(0).SendKeys(alamatStr)
                    Thread.Sleep(jedarandom)
                    elementList = Driver.FindElements(
                          By.XPath("//ul[contains (@aria-label,'disarankan')]/li"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        ulangi = False
                        Thread.Sleep(jedarandom)
                    Else
                        If alamatStr.Length <> 0 Then
                            alamatStr = alamatStr.Substring(0, alamatStr.Length - 1)
                        Else
                            ulangi = False
                        End If
                    End If

                End If
            Loop
            Thread.Sleep(jedarandom)

            WaitHandle.WaitAny({suspendEvent})
            elementList = Driver.FindElements(
                    By.XPath("//div[@aria-label='Berikutnya']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 Then
                If elementList(0).Enabled Then
                    elementList(0).Click()
                End If
            End If

            Thread.Sleep(jedarandom)

            WaitHandle.WaitAny({suspendEvent})
            Try
                waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//div[@aria-label='Publikasikan']")))
            Catch ex As WebDriverTimeoutException
                statusPost.Status = False
                statusPost.Message = "tidak bisa mempublikasikan"
                Return statusPost
            End Try

            WaitHandle.WaitAny({suspendEvent})
            elementList = Driver.FindElements(
                    By.XPath("//div[@aria-label='Publikasikan']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 Then
                elementList(0).Click()
            Else
                statusPost.Status = False
                statusPost.Message = "Gagal Saat Posting"

                Return statusPost
            End If

            Thread.Sleep(jedarandom)

            WaitHandle.WaitAny({suspendEvent})
            elementList = Driver.FindElements(
                    By.XPath("//div[@aria-label='Tutup']"))
            ' Cek apakah elemen ada atau tidak ada
            If elementList.Count > 0 AndAlso elementList(0).Enabled Then
                Dim ariaDisabledValue As String = elementList(0).GetAttribute("aria-disabled")
                If ariaDisabledValue IsNot Nothing AndAlso
                            ariaDisabledValue.ToLower() <> "true" Then
                    elementList(0).Click()
                End If
            End If


            Thread.Sleep(jedarandom * JedaPost)
            WaitHandle.WaitAny({suspendEvent})
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try

        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function

#End Region

#Region "Kumpulan Fungsi Posting FB Lite"
    Public Function PostFBLite(jedarandom As Integer, fields As String(), waitforelement As WebDriverWait, categoryProd As Integer,
                               form As DataGridView, userId As String, startRow As Integer, endRow As Integer, ByRef suspendEvent As ManualResetEvent) As StatusPost
        Dim statusPost As New StatusPost()

        Dim KataKunciProduk As String = String.Empty
        Dim HargaProduknya As String = String.Empty
        Dim KeteranganProduk As String = String.Empty
        Dim LokasiProdukku As String = String.Empty
        Dim Foto1 As String = String.Empty
        Dim Foto2 As String = String.Empty
        Dim Foto3 As String = String.Empty
        Dim Foto4 As String = String.Empty
        Dim Foto5 As String = String.Empty
        Dim JedaPost As String = String.Empty
        '// MENDETEK KODINGAN JIKA TIDAK ADA MP NYA 
        Dim random As New Random()
        Dim waktuRandom As Integer = random.Next(5, 7) * 1000 'dalam milidetik


        Dim delayInterval As Integer = 10 ' Penundaan dalam milidetik (10 ms)

        Dim prevProgress As Integer = CInt((startRow - 1) * 100 / endRow)
        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing

        Try
            If fields.Length >= 10 Then
                WaitHandle.WaitAny({suspendEvent})
                updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 1 / 5 / endRow))

                If Not String.IsNullOrEmpty(fields(0)) Then

                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

#Region "Proses Input dan Inject ke elment berdasarkan CSV"
                    KataKunciProduk = fields(0)
                    HargaProduknya = fields(1)
                    KeteranganProduk = fields(2)
                    LokasiProdukku = fields(3)
                    Foto1 = fields(4)
                    Foto2 = fields(5)
                    Foto3 = fields(6)
                    Foto4 = fields(7)
                    Foto5 = fields(8)
                    JedaPost = fields(9)
                    '// KODINGAN INI JALAN HANYA 1   KALI 
                    Driver.GoToUrl("https://m.facebook.com/marketplace/create/item")
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})
                    Driver.Navigate.Refresh()
                    Thread.Sleep(waktuRandom)
                    WaitHandle.WaitAny({suspendEvent})
                    Try

                        updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 2 / 5 / endRow))
                        WaitHandle.WaitAny({suspendEvent})
                        Try
                            waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//input[@type='text']")))
                        Catch ex As WebDriverTimeoutException
                            statusPost.Status = False
                            statusPost.Message = "Halaman Buat Marketplace tidak tersedia"
                            Return statusPost
                        End Try

                        WaitHandle.WaitAny({suspendEvent})

                        ' FOTO PRODUK VERSI LITE
                        elementList = Driver.FindElements(By.XPath("//input[@type='text']"))
                        If elementList.Count = 0 Then
                            statusPost.Status = False
                            statusPost.Message = "Halaman Buat Marketplace tidak tersedia"

                            Return statusPost
                        End If
                        Thread.Sleep(jedarandom * 2)
                        WaitHandle.WaitAny({suspendEvent})
                        Driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(Foto1)
                        Thread.Sleep(jedarandom)
                        WaitHandle.WaitAny({suspendEvent})
                        Driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(Foto2)
                        Thread.Sleep(jedarandom)
                        WaitHandle.WaitAny({suspendEvent})
                        Driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(Foto3)
                        Thread.Sleep(jedarandom)
                        WaitHandle.WaitAny({suspendEvent})
                        Driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(Foto4)
                        Thread.Sleep(jedarandom)
                        WaitHandle.WaitAny({suspendEvent})
                        Driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(Foto5)
                        Thread.Sleep(waktuRandom)
                        Thread.Sleep(waktuRandom)
                        Thread.Sleep(jedarandom)
                        Thread.Sleep(waktuRandom)
                        Thread.Sleep(jedarandom)
                        WaitHandle.WaitAny({suspendEvent})
                    Catch ex As Exception
                    End Try
                    Thread.Sleep(waktuRandom)
                    '// KATA KUNCI PRODK 
                    WaitHandle.WaitAny({suspendEvent})
                    Driver.FindElement(By.XPath("//input[@type='text']")).SendKeys(KataKunciProduk)
                    Thread.Sleep(jedarandom)
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})
                    '// HARGA PRODUK
                    Driver.FindElement(By.XPath("//input[@name='price']")).SendKeys(HargaProduknya)
                    Thread.Sleep(jedarandom)
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})
                    '// KATEGORI PRODUK VERSI LITE 
                    Driver.FindElement(By.XPath("/html/body/div[1]/div/div[4]/div/div[1]/div/form/div[2]/div/div/div[4]/div/div[1]")).Click()
                    Try
                        waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//*[contains(text(),'Kategori')]")))
                    Catch ex As WebDriverTimeoutException
                        statusPost.Status = False
                        statusPost.Message = "Kategory tidak tersedia"
                        Return statusPost
                    End Try
                    Thread.Sleep(jedarandom)
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})
                    Dim PilihKategoriBro = categoryProd

                    Dim cateSelect As Integer = 6

                    If categoryProd <= 1 Then
                        cateSelect += categoryProd
                    ElseIf categoryProd > 1 AndAlso categoryProd <= 6 Then
                        cateSelect += categoryProd + 1
                    ElseIf categoryProd > 6 AndAlso categoryProd <= 8 Then
                        cateSelect += categoryProd + 2
                    ElseIf categoryProd > 8 AndAlso categoryProd <= 9 Then
                        cateSelect += categoryProd + 3
                    ElseIf categoryProd > 9 AndAlso categoryProd <= 15 Then
                        cateSelect += categoryProd + 5
                    ElseIf categoryProd > 15 AndAlso categoryProd <= 19 Then
                        cateSelect += categoryProd + 6
                    ElseIf categoryProd > 19 AndAlso categoryProd <= 23 Then
                        cateSelect += categoryProd + 7
                    ElseIf categoryProd > 23 AndAlso categoryProd <= 25 Then
                        cateSelect += categoryProd + 8
                    ElseIf categoryProd = 26 Then
                        cateSelect = 19
                    End If

                    'Driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div[" & cateSelect & "]/div[1]")).Click()
                    elementList = Driver.FindElements(By.XPath("//div[@id = 'modalDialogView']/div/div/div[" & cateSelect & "]/div[1]"))
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                    End If


                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 3 / 5 / endRow))

                    Thread.Sleep(jedarandom)
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})
                    '// LOKASI PRODUK
                    Thread.Sleep(jedarandom)
                    Driver.FindElement(By.XPath("/html/body/div[1]/div/div[4]/div/div[1]/div/form/div[2]/div/div/div[5]/div/div[1]")).Click()
                    Thread.Sleep(jedarandom)
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})
                    '// LOKASI ENGGAK WORK 
                    Try
                        Dim lokasiinputData = Driver.FindElement(By.XPath("//*[@placeholder='Cari berdasarkan Kota, Lingkungan, atau Kode Pos']"))
                        lokasiinputData.SendKeys(LokasiProdukku)
                        Thread.Sleep(jedarandom)
                        Thread.Sleep(jedarandom)
                    Catch ex As Exception
                    End Try
                    WaitHandle.WaitAny({suspendEvent})

                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 4 / 5 / endRow))

                    Driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div[2]/div/div[1]/div[1]/div/div[1]")).Click()
                    Thread.Sleep(jedarandom)
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})
                    '// KETERANGAN PRODUK
                    ' driver.FindElement(By.XPath("//textarea[@name='description']")).SendKeys(KeteranganProduk)
                    Dim keteranganproduknya As String = KeteranganProduk
                    Dim keteranganhapus As IWebElement = Nothing

                    Try
                        keteranganhapus = waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//textarea[@name='description']")))
                    Catch ex As WebDriverTimeoutException
                        statusPost.Status = False
                        statusPost.Message = "deskripsi tidak tersedia"
                        Return statusPost
                    End Try

                    WaitHandle.WaitAny({suspendEvent})
                    keteranganhapus.Click()
                    'For Each karakter As Char In keteranganproduknya
                    '    keteranganhapus.SendKeys(karakter) ' Kirim karakter ke elemen input
                    '    Thread.Sleep(delayInterval) ' Tunggu sebentar
                    'Next
                    form.Invoke(Sub() Clipboard.SetText(keteranganproduknya))
                    keteranganhapus.Click()
                    keteranganhapus.SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                    WaitHandle.WaitAny({suspendEvent})

                    Thread.Sleep(waktuRandom)
                    Driver.ExecuteScript("window.scrollBy(0, 1000)")
                    Thread.Sleep(waktuRandom)
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})
                    '// POSTING PRODUK 
                    Driver.FindElement(By.XPath("/html/body/div[1]/div/div[4]/div/div[1]/div/form/div[2]/div/div/div[7]/div[3]/div")).Click()
                    sleep(1)
                    Try
                        Dim klikbuttonbaru As IWebElement = Driver.FindElement(By.XPath("//div[contains(text(),'Posting')]"))
                        klikbuttonbaru.Click()
                    Catch ex As Exception
                    End Try
                    Thread.Sleep(waktuRandom)
                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})
                    '// INFO HASIL POSTING 
                    sleep(JedaPost)

#End Region

                End If

                statusPost.Status = True
                statusPost.Message = String.Empty
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = "kolom pada CSV tidak memenuhi kriteria"
                Return statusPost
            End If
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try

        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function

#End Region

#Region "Fungsi Send Message member group"
    Public Function SendMessageMemberGroup(jedarandom As Integer, fields As String(), waitforelement As WebDriverWait, form As DataGridView,
                                           userId As String, messageTxt As String, startRow As Integer, endRow As Integer, ByRef suspendEvent As ManualResetEvent) As StatusPost
        Dim statusPost As New StatusPost()
        Dim urlMemberGroup As String = String.Empty
        Dim gambar As String = String.Empty

        Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing

        Dim prevProgress As Integer = CInt((startRow - 1) * 100 / endRow)
        Try
            If fields.Length >= 2 Then
                If Not String.IsNullOrEmpty(fields(0)) Then

                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})
                    urlMemberGroup = fields(0)
                    gambar = fields(1)

                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 1 / 5 / endRow))

                    Driver.GoToUrl(urlMemberGroup)

                    Thread.Sleep(jedarandom * 2)
                    WaitHandle.WaitAny({suspendEvent})

                    Try
                        waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//div[contains (@aria-label,'Kirim pesan')][@role='button']")))
                    Catch ex As WebDriverTimeoutException
                        statusPost.Status = False
                        statusPost.Message = "Halaman tidak tersedia"
                        Return statusPost
                    End Try

                    elementList =
    Driver.FindElements(By.XPath("//div[contains (@aria-label,'Kirim pesan')][@role='button']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                        Thread.Sleep(jedarandom)
                    Else
                        statusPost.Status = False
                        statusPost.Message = "Tidak Bisa mengirim pesan ke akun ini"
                        Return statusPost
                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    If Not String.IsNullOrEmpty(gambar) Then

                        Try
                            ' INPUT GAMBAR
                            If Not String.IsNullOrEmpty(gambar) Then
                                If Not File.Exists(gambar) Then
                                    statusPost.Status = False
                                    statusPost.Message = "File GAMBAR Tidak ditemukan"

                                    Return statusPost
                                End If
                                Dim fotoString As String = String.Join(vbNewLine, gambar)
                                Dim inputFoto As IWebElement = Driver.FindElement(By.XPath("//input[@type = 'file']"))
                                inputFoto.SendKeys(fotoString)
                                Thread.Sleep(jedarandom)
                            End If
                        Catch ex As Exception
                        End Try

                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 2 / 5 / endRow))
                    elementList =
                       Driver.FindElements(By.XPath("//div[contains(@data-lexical-editor, 'true')][contains(@aria-label, 'Pesan')]/p"))
                    If elementList.Count > 0 Then
                        form.Invoke(Sub() Clipboard.SetText(messageTxt))
                        elementList(0).Click()
                        elementList(0).SendKeys(Keys.Control & "v") ' Memilih seluruh teks
                    End If

                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 3 / 5 / endRow))
                    elementList =
               Driver.FindElements(By.XPath("//div[contains(@aria-label, 'Tekan Enter untuk mengirim')]"))
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                    End If

                    Thread.Sleep(jedarandom)
                    WaitHandle.WaitAny({suspendEvent})

                    updateProgress(form, userId, prevProgress + CInt(startRow * 100 * 4 / 5 / endRow))
                    elementList =
           Driver.FindElements(By.XPath("//div[contains(@aria-label, 'Tutup obrolan')]"))
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                    End If

                    Thread.Sleep(jedarandom * 3)
                    WaitHandle.WaitAny({suspendEvent})
                End If

                statusPost.Status = True
                statusPost.Message = String.Empty
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = "kolom pada CSV tidak memenuhi kriteria"
                Return statusPost
            End If
        Catch ex As Exception
            If Not CheckInternetConnection() Then
                statusPost.Status = False
                statusPost.Message = "Koneksi Internet terputus"
                Return statusPost
            Else
                statusPost.Status = False
                statusPost.Message = ex.Message
                Return statusPost
            End If
        End Try

        statusPost.Status = True
        statusPost.Message = String.Empty
        Return statusPost
    End Function
#End Region

    '==============================================================

End Class

Public Class StatusPost
    Public Property Status As Boolean
    Public Property Message As String
End Class

Public Class LicensePackage
    Public Property IsTrial As Boolean
    Public Property ModulRegistered As List(Of String)
    Public Sub New()

    End Sub
    Public Sub New(_isTrial As Boolean, _modulRegistered As List(Of String))
        IsTrial = _isTrial
        ModulRegistered = _modulRegistered
    End Sub
End Class