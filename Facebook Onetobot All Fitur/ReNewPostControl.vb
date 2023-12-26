Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports SeleniumUndetectedChromeDriver
Imports System.IO
Imports System.Threading
Imports SeleniumExtras.WaitHelpers

Public Class ReNewPostControl

    Dim baseForm As FormBase = DirectCast(Parent, FormBase)
    Private doubleClickOccurred As Boolean = False

    Private profileDataSet As New DataSet()
    Private Checkboxheader As CheckBox = New CheckBox()
    Private suspendEvent As New ManualResetEvent(True)

    Private _fiturManagePost As FiturManagePostEnum
    Public Sub New(fiturManagePost As FiturManagePostEnum)
        InitializeComponent()

        _fiturManagePost = fiturManagePost
        Select Case _fiturManagePost
            Case FiturManagePostEnum.Renew
                lblHeader.Text = "FITUR AUTO PERBAHARUI POSTINGAN"
                btnStartProcess.Text = "          START ROBOT PERBAHARUI"
                LinkLabel1.Text = "Video Tutorial Jalankan Perbaharui Postingan FBMP"
                gbChoice.Visible = False
                btnStartProcess.Width = 315
            Case FiturManagePostEnum.Delete
                lblHeader.Text = "FITUR AUTO HAPUS POSTINGAN"
                btnStartProcess.Text = "          START ROBOT HAPUS"
                LinkLabel1.Text = "Video Tutorial Jalankan Hapus Postingan FBMP"
                gbChoice.Visible = True
                btnStartProcess.Width = 275
        End Select

        ' Misalkan Anda memiliki DataGridView bernama dgvProgress
        Dim progressBarColumn As New ProgressBarColumn()
        progressBarColumn.HeaderText = "PROGRESS"
        progressBarColumn.Name = "ProgressCol"
        progressBarColumn.Width = 150
        gridProfile.Columns.Add(progressBarColumn)
    End Sub

    Private Sub ReNewPostControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Periksa apakah file UserData.xml sudah ada
        baseForm = DirectCast(Parent, FormBase)

        Dim HeadercellLocation As Point = Me.gridProfile.GetCellDisplayRectangle(0, -1, True).Location
        Checkboxheader.Location = New Point(HeadercellLocation.X + 8, HeadercellLocation.Y + 2)
        Checkboxheader.Size = New Size(18, 18)
        Checkboxheader.BackColor = Color.White

        Checkboxheader.Checked = True

        gridProfile.Controls.Add(Checkboxheader)

        AddHandler Checkboxheader.Click, AddressOf headerCheckbox_Click

        If File.Exists(ChromeProfile.dataUser) Then
            ' Muat data dari file XML
            baseForm.isLoad = True
            Dim dataSet As New DataSet

            ' Muat data dari file XML
            dataSet.ReadXml(ChromeProfile.dataUser)

            If dataSet.Tables.Count > 1 Then
                Dim dataProfile As DataTable = dataSet.Tables(1)

                cbxDataProfile.Items.Clear()
                For Each row As DataRow In dataProfile.Rows
                    cbxDataProfile.Items.Add(row("ProfileName"))
                Next

                If cbxDataProfile.Items.Count > 0 Then
                    cbxDataProfile.SelectedIndex = 0
                End If
            End If
            loadDataFromBaseProfile(dataSet)
            loadDataForPOSTgridXML()
        End If
        baseForm.isLoad = False
    End Sub

    Private Sub headerCheckbox_Click(ByVal sender As Object, ByVal e As EventArgs)
        gridProfile.EndEdit()

        For Each row As DataGridViewRow In gridProfile.Rows
            Dim chk As DataGridViewCheckBoxCell = TryCast(row.Cells("digunakanCol"), DataGridViewCheckBoxCell)
            chk.Value = Checkboxheader.Checked
        Next
    End Sub

    Function loadDataFromBaseProfile(dataSet As DataSet)
        If dataSet.Tables.Count > 0 Then
            Dim dataTable As DataTable = dataSet.Tables(0)

            If profileDataSet.Tables.Count = 0 Then
                Dim table As New DataTable("ProfileDataTable")
                table.Columns.Add("digunakanCol", GetType(Boolean))
                table.Columns.Add("profileChromeCol", GetType(String))
                table.Columns.Add("userIdforPostCol", GetType(String))
                table.Columns.Add("passwordforPostCol", GetType(String))
                table.Columns.Add("totalRenewCol", GetType(Integer))
                table.Columns.Add("ProgressCol", GetType(Integer))

                ' Menerapkan filter ke DataView
                Dim dataView As New DataView(dataSet.Tables("UserData"))
                dataView.RowFilter = String.Concat("IsLogin = '1'")

                For Each rowView As DataRowView In dataView
                    Dim profileName As String = rowView("ProfileName").ToString()
                    Dim userId As String = rowView("UserId").ToString()
                    Dim password As String = rowView("Password").ToString()

                    ' Menambahkan baris ke DataGridView pertama
                    table.Rows.Add(True, profileName, userId, password, 0, 0)

                Next

                profileDataSet.Tables.Add(table)
            End If

        End If
        Return True
    End Function

    Function loadDataForPOSTgridXML()
        If profileDataSet.Tables.Count > 0 Then
            ' Menerapkan filter ke DataView
            Dim dataView As New DataView(profileDataSet.Tables("ProfileDataTable"))
            dataView.RowFilter = String.Concat("profileChromeCol = '" & cbxDataProfile.Text & "'")

            ' Hapus semua baris yang ada di DataGridView
            gridProfile.Rows.Clear()

            Dim allChecked = True
            For Each rowView As DataRowView In dataView
                Dim isUsed As Boolean = CBool(rowView("digunakanCol"))
                Dim profileName As String = rowView("profileChromeCol").ToString()
                Dim userId As String = rowView("userIdforPostCol").ToString()
                Dim password As String = rowView("passwordforPostCol").ToString()

                Dim totalRenew As Integer = CInt(rowView("totalRenewCol"))
                Dim progress As Integer = CInt(rowView("ProgressCol"))

                If allChecked AndAlso Not isUsed Then
                    allChecked = False
                End If
                ' Menambahkan baris ke DataGridView pertama
                gridProfile.Rows.Add(isUsed, profileName, userId, password, totalRenew, progress)
            Next
            Checkboxheader.Checked = allChecked

        End If
        Return True
    End Function

    Private Sub btnStartProcess_Click(sender As Object, e As EventArgs) Handles btnStartProcess.Click
        'handle untuk double click
        If Not doubleClickOccurred Then
            doubleClickOccurred = True
#Region "Validasi sebelum menjalankan proses"

            Dim validToContinue As Boolean = False
            For Each row As DataGridViewRow In gridProfile.Rows
                If row.Cells("digunakanCol").Value Then
                    validToContinue = True
                    Exit For
                End If
            Next

            If Not validToContinue Then
                MessageBox.Show("Tidak ada Data Profile dan akun Facebook yang akan di jalankan, Pilih Profil dan akun yang akan dijalankan", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
                doubleClickOccurred = False
                Return
            End If

            Select Case _fiturManagePost
                Case FiturManagePostEnum.Delete
                    If Not chkOldest.Checked AndAlso Not chkAgainst.Checked AndAlso Not chkDraft.Checked Then
                        MessageBox.Show("Harap pilih Hapus berdasarkan Terlama/Melanggar/Draft", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        doubleClickOccurred = False
                        Return
                    End If

                    If chkOldest.Checked AndAlso numMaxOldest.Value = 0 Then
                        MessageBox.Show("Harap tentukan maksimal hapus postingan terlama", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        doubleClickOccurred = False
                        Return
                    End If
            End Select


#End Region
            Try
                runRobot(cbxDataProfile.Text, chkRunAllProfile.Checked, chkOldest.Checked, chkAgainst.Checked, chkDraft.Checked, numMaxOldest.Value)
            Catch ex As Exception
                MessageBox.Show(ex.Message,
                                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            doubleClickOccurred = False
        End If
    End Sub

    Private Sub runRobot(profileName As String, isRunAll As Boolean, isOldest As Boolean, isAgainst As Boolean, isDraft As Boolean, maxOldest As Integer)
        Dim foundRows As DataRow() = profileDataSet.Tables(0).Select($"profileChromeCol = '{profileName}' AND digunakanCol = True")

        If foundRows.Count > 0 Then
            '========================================
            'Masukkan proses kedalam antrian threads
            'proses dilanjutkan ke function PostingLiteFb
            Dim newThread As Thread =
                New Thread(Sub() runRobotWork(foundRows, isRunAll, isOldest, isAgainst, isDraft, maxOldest))
            '========================================

            ' Menambahkannya ke daftar thread
            baseForm.threads.Add(newThread)

            ' Memulai thread
            newThread.Start()

        End If
    End Sub

    Private Sub runRobotWork(foundRows As DataRow(), isRunAll As Boolean, isOldest As Boolean, isAgainst As Boolean, isDraft As Boolean, maxOldest As Integer)
        For Each foundRow In foundRows
#Region "Proses membuka browser dan inject element ke dalam threads"
            ' //cek apakah Profile yang di pilih sedang digunakan
            Dim existingProfile = baseForm.Profiles.Find(Function(p) p.ProfileName = foundRow(2) And p.IsOnProcess = True)
            If existingProfile Is Nothing Then

                If isRunAll Then
                    Task.Run(Sub() RenewPostFb(foundRow, isOldest, isAgainst, isDraft, maxOldest))
                    Thread.Sleep(1000)
                Else
                    RenewPostFb(foundRow, isOldest, isAgainst, isDraft, maxOldest)
                End If
            Else
                MessageBox.Show(
                String.Concat("Harapa tunggu beberapa saat sampai proses selesai untuk Akun ", foundRow(2)),
                            "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
#End Region
        Next
    End Sub

    Private Sub RenewPostFb(dataProfile As DataRow, isOldest As Boolean, isAgainst As Boolean, isDraft As Boolean, maxOldest As Integer)

        Dim runningProfile = ""
        Dim userId As String = ""
        Dim existingProfile As ChromeProfile = Nothing
        Dim statusPost As New StatusPost()
        Try
            Dim countUser = 0

            Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing

            Dim success As String = ""
            Dim totalRenew As Integer = 0
            Dim profileName = dataProfile(2)
            userId = dataProfile(2)
            Dim password As String = dataProfile(3)

            Dim driver As UndetectedChromeDriver = Nothing

            '========================================
            '//membuka Browser chrome dan menyimpan ke object Profiles di FormBase
            Dim windowsSize = 0
            Select Case _fiturManagePost
                Case FiturManagePostEnum.Renew
                    windowsSize = 0
                Case FiturManagePostEnum.Delete
                    windowsSize = 1
            End Select
            existingProfile = baseForm.runChromeDriver(userId, windowsSize)
            '========================================

            '//mendefinisikan jika profile yang dipilih sedang digunakan
            If existingProfile IsNot Nothing Then
                existingProfile.IsOnProcess = True
            End If


            If existingProfile IsNot Nothing Then
                driver = existingProfile.Driver
                runningProfile = existingProfile.ProfileName

                If driver Is Nothing Then
                    Throw New Exception("Terjadi Konfik, Harap tutup Browser")
                End If
            End If

            existingProfile.updateProgress(Me.gridProfile, userId, CInt(1 * 100 / 5))
            baseForm.sleep(1)
            Dim waitforelement As WebDriverWait = New WebDriverWait(driver, TimeSpan.FromSeconds(60 * 60))
            Try
                Dim title = driver.Title
            Catch ex As Exception
                If driver IsNot Nothing Then
                    driver.Quit() 'menutup browser
                End If
            End Try

            driver.GoToUrl("https://www.facebook.com")
            Dim messageError As String = String.Empty
            WaitHandle.WaitAny({suspendEvent})

            driver.Navigate.Refresh()
            '// MENDETEK KODINGAN JIKA TIDAK ADA MP NYA 
            Dim random As New Random()
            Dim waktuRandom As Integer = random.Next(5, 7) * 1000 'dalam milidetik
            Dim jedarandom As Integer = random.Next(2, 4) * 1000 'dalam milidetik

            Thread.Sleep(waktuRandom * 2)
            WaitHandle.WaitAny({suspendEvent})
            Try

                existingProfile.updateProgress(Me.gridProfile, userId, CInt(2 * 100 / 5))
                statusPost = existingProfile.CheckLoginProcess(dataProfile)

                WaitHandle.WaitAny({suspendEvent})
                If Not statusPost.Status Then
                    Throw New Exception(statusPost.Message)
                End If

                Thread.Sleep(jedarandom)


                WaitHandle.WaitAny({suspendEvent})
                Select Case _fiturManagePost
                    Case FiturManagePostEnum.Renew
                        existingProfile.updateProgress(Me.gridProfile, userId, CInt(3 * 100 / 5))
                        Dim ulangi As Boolean = True

                        Do While ulangi
                            driver.GoToUrl("https://www.facebook.com/marketplace/selling/renew_listings/?is_routable_dialog=true")
                            Thread.Sleep(jedarandom)
                            WaitHandle.WaitAny({suspendEvent})
                            driver.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);")
                            Thread.Sleep(jedarandom * 3) ' Tunggu beberapa detik untuk memuat
                            WaitHandle.WaitAny({suspendEvent})

                            elementList =
                            driver.FindElements(By.XPath("//div[contains(@aria-label, 'Perbarui')][contains(@role, 'button')]"))
                            ' Cek apakah elemen ada atau tidak ada
                            If elementList.Count > 0 Then
                                Dim i As Integer = 1
                                For Each elementItem In elementList
                                    If elementItem.Enabled Then
                                        elementItem.Click()
                                        totalRenew += 1
                                        If i <> elementList.Count Then
                                            existingProfile.updateProgress(Me.gridProfile, userId, CInt(3 * 100 / 5) + CInt(4 * 100 * i / elementList.Count / 5))
                                            WaitHandle.WaitAny({suspendEvent})
                                        End If
                                        i += 1
                                    End If
                                Next

                                existingProfile.updateProgress(Me.gridProfile, userId, CInt(4 * 100 / 5))
                                WaitHandle.WaitAny({suspendEvent})
                            Else
                                ulangi = False
                            End If
                        Loop
                    Case FiturManagePostEnum.Delete

                        If isOldest Then
                            Dim deletedNum As Integer = 0
                            Dim isMax = False

                            Do While maxOldest > deletedNum

                                driver.GoToUrl("https://www.facebook.com/marketplace/you/selling?order=CREATION_TIMESTAMP")

                                Thread.Sleep(jedarandom * 2)

                                WaitHandle.WaitAny({suspendEvent})

                                elementList =
                                    driver.FindElements(By.XPath("//div[contains(@class, 'x9f619 x1n2onr6 x1ja2u2z x78zum5 xdt5ytf x2lah0s x193iq5w x1k70j0n xzueoph xzboxd6 x14l7nz5')]"))

                                Dim elementTawaranList As IReadOnlyCollection(Of IWebElement) = Nothing
                                If elementList.Count > 0 Then

                                    For Each element In elementList

                                        If maxOldest <= deletedNum Then
                                            isMax = True
                                            Exit For
                                        End If

                                        '//div[contains(@aria-label , 'Tawarkan Ulang')]//span[contains(text() , 'Tawarkan Ulang')]
                                        Dim elementLainnya As IWebElement = element.FindElement(By.XPath("div/div/div/div[2]//div[contains(@aria-label , 'Lainnya')][@role = 'button']"))
                                        If elementLainnya IsNot Nothing Then
                                            elementLainnya.Click()
                                            Thread.Sleep(jedarandom)
                                            WaitHandle.WaitAny({suspendEvent})
                                            Dim elementHapus As IReadOnlyCollection(Of IWebElement) = driver.FindElements(By.XPath("//*[@role = 'menuitem']"))
                                            If elementHapus.Count > 0 Then
                                                elementHapus(elementHapus.Count - 1).Click()
                                                Thread.Sleep(jedarandom)
                                                WaitHandle.WaitAny({suspendEvent})
                                                elementHapus = driver.FindElements(By.XPath("//div[@role = 'dialog']/div[3]/div[2]/div/div[2]/div[@role = 'button'][contains(@aria-label , 'Hapus')]"))
                                                If elementHapus.Count = 0 Then
                                                    elementHapus = driver.FindElements(By.XPath("//div[@role = 'dialog']/div/div/div/div[3]//div[@role = 'button'][contains(@aria-label , 'Hapus')]"))
                                                End If
                                                If elementHapus.Count > 0 Then
                                                    elementHapus(0).Click()

                                                    totalRenew += 1
                                                    Thread.Sleep(jedarandom)
                                                    WaitHandle.WaitAny({suspendEvent})
                                                    elementHapus = driver.FindElements(By.XPath("//div[@role = 'dialog']/div/div/div/div[1]//div[@role = 'button'][contains(@aria-label , 'Tutup')]"))
                                                    If elementHapus.Count > 0 Then
                                                        elementHapus(0).Click()
                                                    End If

                                                    deletedNum += 1


                                                    If deletedNum <> elementList.Count Then
                                                        existingProfile.updateProgress(Me.gridProfile, userId, CInt(2 * 100 / 5) + CInt(3 * 100 * deletedNum / elementList.Count / 5))
                                                        WaitHandle.WaitAny({suspendEvent})
                                                    End If
                                                End If
                                            End If
                                        End If
                                    Next

                                End If
                                existingProfile.updateProgress(Me.gridProfile, userId, CInt(3 * 100 / 5))
                                WaitHandle.WaitAny({suspendEvent})

                                If isMax Then
                                    Exit Do
                                End If

                                If elementList.Count < 4 Then
                                    Exit Do
                                End If

                            Loop
                        End If

                        If isAgainst Then
                            Dim isFound = True
                            For i = 1 To 10
                                driver.GoToUrl("https://www.facebook.com/marketplace/you/selling")

                                Thread.Sleep(jedarandom * 2)

                                WaitHandle.WaitAny({suspendEvent})

                                elementList =
                                    driver.FindElements(By.XPath("//div[contains(@class, 'x9f619 x1n2onr6 x1ja2u2z x78zum5 xdt5ytf x2lah0s x193iq5w x1k70j0n xzueoph xzboxd6 x14l7nz5')]"))
                                Dim elementTawaranList As IReadOnlyCollection(Of IWebElement) = Nothing
                                If elementList.Count > 0 Then
                                    For Each element In elementList
                                        '//div[contains(@aria-label , 'Tawarkan Ulang')]//span[contains(text() , 'Tawarkan Ulang')]
                                        Try
                                            elementTawaranList = element.FindElements(By.XPath("div/div/div/div[2]//div[@class = 'x193iq5w']/div[@class = 'x1daaz14']/div/div/div[2]//div[@class = 'x1f6kntn x117nqv4 x1a1m0xk']"))
                                        Catch
                                        End Try
                                        If elementTawaranList.Count > 0 Then
                                            Dim elementLainnya As IWebElement = element.FindElement(By.XPath("div/div/div/div[2]//div[contains(@aria-label , 'Lainnya')][@role = 'button']"))
                                            If elementLainnya IsNot Nothing Then
                                                elementLainnya.Click()
                                                Thread.Sleep(jedarandom)
                                                WaitHandle.WaitAny({suspendEvent})
                                                Dim elementHapus As IReadOnlyCollection(Of IWebElement) = driver.FindElements(By.XPath("//*[@role = 'menuitem']"))
                                                If elementHapus.Count > 0 Then
                                                    elementHapus(elementHapus.Count - 1).Click()
                                                    Thread.Sleep(jedarandom)
                                                    WaitHandle.WaitAny({suspendEvent})
                                                    elementHapus = driver.FindElements(By.XPath("//div[@role = 'dialog']/div[3]/div[2]/div/div[2]/div[@role = 'button'][contains(@aria-label , 'Hapus')]"))
                                                    If elementHapus.Count = 0 Then
                                                        elementHapus = driver.FindElements(By.XPath("//div[@role = 'dialog']/div/div/div/div[3]//div[@role = 'button'][contains(@aria-label , 'Hapus')]"))
                                                    End If
                                                    If elementHapus.Count > 0 Then
                                                        elementHapus(0).Click()

                                                        totalRenew += 1
                                                        Thread.Sleep(jedarandom * 2)
                                                        WaitHandle.WaitAny({suspendEvent})
                                                        elementHapus = driver.FindElements(By.XPath("//div[@role = 'dialog']/div/div/div/div[1]//div[@role = 'button'][contains(@aria-label , 'Tutup')]"))
                                                        If elementHapus.Count > 0 Then
                                                            elementHapus(0).Click()
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        Else
                                            isFound = False
                                            Exit For
                                        End If

                                        If i <> elementList.Count Then
                                            existingProfile.updateProgress(Me.gridProfile, userId, CInt(3 * 100 / 5) + CInt(4 * 100 * i / elementList.Count / 5))
                                            WaitHandle.WaitAny({suspendEvent})
                                        End If

                                    Next
                                End If

                                If Not isFound Then
                                    Exit For
                                End If
                            Next
                            existingProfile.updateProgress(Me.gridProfile, userId, CInt(4 * 100 / 5))
                        End If

                        If isDraft Then
                            Dim isFound = True
                            For i = 1 To 10
                                driver.GoToUrl("https://www.facebook.com/marketplace/you/selling?state=DRAFT")

                                Thread.Sleep(jedarandom * 2)

                                WaitHandle.WaitAny({suspendEvent})

                                elementList =
                                    driver.FindElements(By.XPath("//div[contains(@class, 'x9f619 x1n2onr6 x1ja2u2z x78zum5 xdt5ytf x2lah0s x193iq5w x1k70j0n xzueoph xzboxd6 x14l7nz5')]"))
                                Dim elementTawaranList As IReadOnlyCollection(Of IWebElement) = Nothing
                                If elementList.Count > 0 Then
                                    For Each element In elementList
                                        Dim elementLainnya As IWebElement = element.FindElement(By.XPath("div/div/div/div[2]//div[contains(@aria-label , 'Hapus Draf')][@role = 'button']"))
                                        If elementLainnya IsNot Nothing Then
                                            elementLainnya.Click()
                                            Thread.Sleep(jedarandom)

                                            WaitHandle.WaitAny({suspendEvent})
                                            Dim elementHapus As IReadOnlyCollection(Of IWebElement) = driver.FindElements(By.XPath("//div[@role = 'dialog']/div[3]/div[2]/div/div[2]/div[@role = 'button'][contains(@aria-label , 'Hapus')]"))
                                            If elementHapus.Count = 0 Then
                                                elementHapus = driver.FindElements(By.XPath("//div[@role = 'dialog']/div/div/div/div[3]//div[@role = 'button'][contains(@aria-label , 'Hapus')]"))
                                            End If
                                            If elementHapus.Count > 0 Then
                                                elementHapus(0).Click()

                                                totalRenew += 1
                                                Thread.Sleep(jedarandom)
                                                WaitHandle.WaitAny({suspendEvent})
                                                elementHapus = driver.FindElements(By.XPath("//div[@role = 'dialog']/div/div/div/div[1]//div[@role = 'button'][contains(@aria-label , 'Tutup')]"))
                                                If elementHapus.Count > 0 Then
                                                    elementHapus(0).Click()
                                                End If
                                            End If
                                        End If

                                        If i <> elementList.Count Then
                                            existingProfile.updateProgress(Me.gridProfile, userId, CInt(3 * 100 / 5) + CInt(4 * 100 * i / elementList.Count / 5))
                                            WaitHandle.WaitAny({suspendEvent})
                                        End If

                                    Next
                                End If

                                If elementList.Count < 4 Then
                                    Exit For
                                End If
                            Next
                            existingProfile.updateProgress(Me.gridProfile, userId, CInt(4 * 100 / 5))
                        End If
                        Thread.Sleep(jedarandom * 3)
                        WaitHandle.WaitAny({suspendEvent})
                End Select


                Thread.Sleep(jedarandom)

                WaitHandle.WaitAny({suspendEvent})
                'existingProfile.updateProgress(Me.gridProfile, userId, CInt(5 * 100 / 5))
                success = "SELESAI"
            Catch ex As Exception
                If Not existingProfile.CheckInternetConnection() Then
                    messageError = "Koneksi Internet terputus"
                    success = "GAGAL"
                Else
                    messageError = ex.Message
                    success = "GAGAL"
                End If
            End Try

            For Each row In gridProfile.Rows
                If row.Cells("userIdforPostCol").Value IsNot Nothing Then
                    Dim targetUserId As String = row.Cells("userIdforPostCol").Value.ToString()

                    If userId = targetUserId Then
                        ' Update nilai-nilai kolom sesuai kebutuhan
                        Invoke(Sub() row.Cells("totalRenewCol").Value = totalRenew)
                        Exit For
                    End If
                End If
            Next
            existingProfile.updateProgress(Me.gridProfile, userId, 100)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try

        If existingProfile IsNot Nothing Then
            baseForm.Invoke(Sub()
                                If existingProfile IsNot Nothing Then
                                    existingProfile.IsOnProcess = False
                                    If existingProfile.Driver IsNot Nothing Then
                                        existingProfile.Driver.Quit()
                                    End If
                                End If
                            End Sub)
        End If
        BeginInvoke(Sub() MessageBox.Show("Akun " & runningProfile & " Selesai dijalankan!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information))
    End Sub

    Private Sub btnRefreshData_Click(sender As Object, e As EventArgs) Handles btnRefreshData.Click
        Dim dataSet As New DataSet

        ' Muat data dari file XML
        dataSet.ReadXml(ChromeProfile.dataUser)
        profileDataSet.Tables.Clear()
        loadDataFromBaseProfile(dataSet)
        loadDataForPOSTgridXML()
    End Sub

    Private Sub gridProfile_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles gridProfile.CellClick
        If e.RowIndex >= 0 Then
            Dim isChecked As Boolean = True
            For Each row As DataGridViewRow In gridProfile.Rows
                If Not Convert.ToBoolean(row.Cells("digunakanCol").Value) Then
                    isChecked = False
                    Exit For
                End If
            Next

            Checkboxheader.Checked = isChecked
        End If
    End Sub

    Private Sub cbxDataProfile_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxDataProfile.SelectedIndexChanged
        If Not baseForm.isLoad Then
            loadDataForPOSTgridXML()
        End If
    End Sub

    Private Sub gridProfile_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles gridProfile.RowPostPaint
        ' Menggambar nomor urut di setiap baris
        Using b As New SolidBrush(gridProfile.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub gridProfile_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles gridProfile.CellValueChanged
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            ' Mendapatkan nilai yang diubah
            Dim newValue As Object = gridProfile.Rows(e.RowIndex).Cells(e.ColumnIndex).Value

            Dim userId As Object = gridProfile.Rows(e.RowIndex).Cells("userIdforPostCol").Value

            ' Mendapatkan nama kolom
            Dim columnName As String = gridProfile.Columns(e.ColumnIndex).Name

            ' Mengubah nilai di dalam dataset
            Dim foundRows As DataRow() = profileDataSet.Tables(0).Select($"profileChromeCol = '{cbxDataProfile.Text}' AND userIdforPostCol = '{userId}'")
            foundRows(0)(columnName) = newValue
        End If
    End Sub

    Private Sub btnForceClose_Click(sender As Object, e As EventArgs) Handles btnForceClose.Click
        If baseForm.Profiles.Count > 0 Then
            If MessageBox.Show("apakah anda yakin akan menutup seluruh Browser?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = DialogResult.OK Then
                For Each profile In baseForm.Profiles
                    If profile.Driver IsNot Nothing Then
                        profile.Driver.Quit()
                    End If
                Next
                baseForm.Profiles.Clear()
            End If

            'RefreshData()
        Else
            MessageBox.Show("tidak ada Browser yang terbuka", "informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Select Case _fiturManagePost
            Case FiturManagePostEnum.Renew
                Dim webAddress As String = "https://youtu.be/hLU-4VLDgFA"
                Process.Start(webAddress)
            Case FiturManagePostEnum.Delete
                Dim webAddress As String = "https://www.youtube.com/watch?v=_zN0xnAcZoQ"
                Process.Start(webAddress)
        End Select
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        For Each profile In baseForm.Profiles
            If profile.IsOnProcess Then
                MessageBox.Show("Tidak Bisa menutup Halaman, Masih terdapat proses yang belum selesai", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next
        Select Case _fiturManagePost
            Case FiturManagePostEnum.Renew
                Dim tabName = "ReNewPost"
                baseForm.CloseAndRemoveTabPage(tabName)
            Case FiturManagePostEnum.Delete
                Dim tabName = "DeletePost"
                baseForm.CloseAndRemoveTabPage(tabName)
        End Select
    End Sub


    Dim isPause As Boolean = False
    Private Sub btnPause_Click(sender As Object, e As EventArgs) Handles btnPause.Click
        If Not isPause Then
            'For Each threadObj In baseForm.threads
            '    threadObj.Suspend()
            'Next
            suspendEvent.Reset()
            btnPause.Text = "Lanjut"
            btnPause.Image = Global.Facebook_Onetobot_All_Fitur.My.Resources.Resources.right_arrow_16
            isPause = True
        Else
            'For Each threadObj In baseForm.threads
            '    threadObj.Resume()
            'Next
            suspendEvent.Set()
            isPause = False
            btnPause.Text = "Pause"
            btnPause.Image = Global.Facebook_Onetobot_All_Fitur.My.Resources.Resources.pause_16
        End If
    End Sub

    Private Sub chkOldest_CheckedChanged(sender As Object, e As EventArgs) Handles chkOldest.CheckedChanged
        numMaxOldest.Enabled = chkOldest.Checked
    End Sub
End Class
