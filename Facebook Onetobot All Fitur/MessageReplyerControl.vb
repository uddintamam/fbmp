Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports SeleniumUndetectedChromeDriver
Imports System.IO
Imports System.Threading
Imports SeleniumExtras.WaitHelpers

Public Class MessageReplyerControl

    Dim baseForm As FormBase = DirectCast(Parent, FormBase)
    Private doubleClickOccurred As Boolean = False

    Dim fieldList As New List(Of String())()

    Dim totalRow As Integer = 1
    Private profileDataSet As New DataSet()
    Private stopEvent As New ManualResetEvent(False)
    Private suspendEvent As New ManualResetEvent(True)

    Private Checkboxheader As CheckBox = New CheckBox()
    Public Sub New()
        InitializeComponent()

        ' Tambahkan Progress ke GridView
        Dim progressBarColumn As New ProgressBarColumn()
        progressBarColumn.HeaderText = "PROGRESS"
        progressBarColumn.Name = "ProgressCol"
        progressBarColumn.Width = 150
        gridProfile.Columns.Add(progressBarColumn)
    End Sub

    Private Sub MessageReplyerControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                MessageBox.Show("Tidak ada Data Profile dan akun Facebook yang akan di jalankan, Pilih Profil dan akun yang akan dijalankan", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                doubleClickOccurred = False
                Return
            End If

            If String.IsNullOrEmpty(txtCsv.Text) Then
                MessageBox.Show("File CSV Belum dipilih", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                doubleClickOccurred = False
                Return
            End If
#End Region
            Try
                runRobot(cbxDataProfile.Text, chkRunAllProfile.Checked)
            Catch ex As Exception
                MessageBox.Show(ex.Message,
                                           "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            doubleClickOccurred = False
        End If
    End Sub

    Private Sub runRobot(profileName As String, isRunAll As Boolean)
        Dim foundRows As DataRow() = profileDataSet.Tables(0).Select($"profileChromeCol = '{profileName}' AND digunakanCol = True")

        If foundRows.Count > 0 Then
            Dim existingProfile = baseForm.Profiles.Find(Function(p) p.ProfileName = profileName And p.IsOnProcess = True)
            If existingProfile Is Nothing Then

                Dim newThread As Thread =
                    New Thread(Sub() runRobotWork(foundRows, isRunAll))
                '========================================

                ' Menambahkannya ke daftar thread
                baseForm.threads.Add(newThread)

                ' Memulai thread
                newThread.Start()
            Else
                MessageBox.Show(String.Concat("Harapa tunggu beberapa saat sampai proses selesai untuk akun ", profileName),
                                       "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub runRobotWork(foundRows As DataRow(), isRunAll As Boolean)

        For Each foundRow In foundRows
#Region "Proses membuka browser dan inject element ke dalam threads"
            ' //cek apakah Profile yang di pilih sedang digunakan
            Dim existingProfile = baseForm.Profiles.Find(Function(p) p.ProfileName = foundRow(2) And p.IsOnProcess = True)
            If existingProfile Is Nothing Then
                '========================================
                'Masukkan proses kedalam antrian threads
                'proses dilanjutkan ke function PostingLiteFb

                If isRunAll Then
                    Task.Run(Sub() MessageReplyerFb(foundRow, fieldList))
                    Thread.Sleep(1000)
                Else
                    MessageReplyerFb(foundRow, fieldList)
                End If
            Else
                MessageBox.Show(
                    String.Concat("Harapa tunggu beberapa saat sampai proses selesai untuk Akun ", foundRow(2)),
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

#End Region
        Next
    End Sub

    Private Sub MessageReplyerFb(dataProfile As DataRow, fieldList As List(Of String()))

        Dim runningProfile = ""
        Dim userId As String = ""
        Dim existingProfile As ChromeProfile = Nothing
        '// MENDETEK KODINGAN JIKA TIDAK ADA MP NYA 
        Dim random As New Random()
        Dim waktuRandom As Integer = random.Next(4, 6) * 1000 'dalam milidetik
        Dim jedarandom As Integer = random.Next(1, 2) * 1000 'dalam milidetik

        Dim statusPost As New StatusPost()
        Try

            Dim countUser = 0

            Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing

            Dim success As String = ""
            Dim totalRenew As Integer = 0
            Dim profileName = dataProfile(1)
            userId = dataProfile(2)
            Dim password As String = dataProfile(3)

            Dim driver As UndetectedChromeDriver = Nothing

            '========================================
            '//membuka Browser chrome dan menyimpan ke object Profiles di FormBase
            existingProfile = baseForm.runChromeDriver(profileName, userId, 0)
            '========================================
            WaitHandle.WaitAny({suspendEvent})

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
            WaitHandle.WaitAny({suspendEvent})

            baseForm.sleep(1)
            Try
                driver.GoToUrl("https://www.facebook.com")
                Dim title = driver.Title
            Catch ex As Exception
                If driver IsNot Nothing Then
                    driver.Quit() 'menutup browser
                End If
            End Try

            Dim messageError As String = String.Empty

            Dim waitforelement As WebDriverWait = New WebDriverWait(driver, TimeSpan.FromSeconds(ChromeProfile.waitElement))

            Thread.Sleep(jedarandom * 2)
            WaitHandle.WaitAny({suspendEvent})

            driver.Navigate.Refresh()

            Thread.Sleep(waktuRandom)
            Try

                statusPost = existingProfile.CheckLoginProcess(dataProfile)

                If Not statusPost.Status Then
                    Throw New Exception(statusPost.Message)
                End If
                WaitHandle.WaitAny({suspendEvent})

                Thread.Sleep(jedarandom)

                driver.GoToUrl("https://www.facebook.com/marketplace/inbox")
                WaitHandle.WaitAny({suspendEvent})

                existingProfile.updateProgress(Me.gridProfile, userId, CInt(2 * 100 / 5))

                Thread.Sleep(jedarandom)
                WaitHandle.WaitAny({suspendEvent})

                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//div[contains(@aria-label, 'Koleksi item Marketplace')][contains(@role, 'main')]/div/div/div/div/div[2]/div/div/div/div/div/div")))
                Catch ex As WebDriverTimeoutException
                    Throw New Exception("Halaman tidak tersedia")
                End Try
                WaitHandle.WaitAny({suspendEvent})

                For a As Integer = 1 To 25
                    driver.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);")
                    Thread.Sleep(jedarandom) ' Tunggu beberapa detik untuk memuat

                    ' Mendapatkan elemen scroll pada halaman web
                    Dim scrollElement As IWebElement = driver.FindElement(By.TagName("html"))

                    Dim isScrollAtBottom As Boolean =
                        CBool(driver.ExecuteScript("return arguments[0].scrollTop + arguments[0].clientHeight >= arguments[0].scrollHeight;", scrollElement))
                    WaitHandle.WaitAny({suspendEvent})

                    ' Menampilkan hasil
                    If isScrollAtBottom Then
                        Exit For
                    End If
                Next

                Dim elementList2 As IReadOnlyCollection(Of IWebElement) =
                    driver.FindElements(By.XPath("//div[contains(@aria-label, 'Koleksi item Marketplace')][contains(@role, 'main')]/div/div/div/div/div[2]/div/div/div/div/div/div"))

                existingProfile.updateProgress(Me.gridProfile, userId, CInt(3 * 100 / 5))
                WaitHandle.WaitAny({suspendEvent})

                ' Cek apakah elemen ada atau tidak ada
                If elementList2.Count > 0 Then
                    Dim ab As Integer = 1
                    For Each elementItem In elementList2

                        existingProfile.updateProgress(Me.gridProfile, userId, CInt(3 * 100 / 5) + CInt(4 * 100 * ab / elementList2.Count / 5))
                        ab += 1

                        Dim elementMssg As IWebElement = Nothing
                        Try
                            elementMssg =
                                elementItem.FindElement(By.XPath("div[contains(@role, 'button')]"))
                        Catch ex As Exception
                        End Try
                        WaitHandle.WaitAny({suspendEvent})

                        If elementMssg IsNot Nothing Then
                            Dim elementPoint =
                                elementMssg.FindElement(By.XPath("div/div[1]/div"))
                            If elementPoint IsNot Nothing Then
                                ' Gunakan JavaScript Executor untuk mendapatkan warna latar belakang
                                Dim jsExecutor As IJavaScriptExecutor = CType(driver, IJavaScriptExecutor)
                                Dim backgroundColor As String = jsExecutor.ExecuteScript("return getComputedStyle(arguments[0]).backgroundColor;", elementPoint).ToString()
                                'backgroundColor = "rgb(8, 102, 255)" AndAlso
                                Try
                                    If elementMssg.Enabled Then

                                        elementMssg.Click()

                                        Thread.Sleep(jedarandom)
                                        WaitHandle.WaitAny({suspendEvent})

                                        Try
                                            waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//div[contains(@class, 'x1gslohp x11i5rnm x12nagc x1mh8g0r x1yc453h x126k92a')][contains(@dir, 'auto')]")))
                                        Catch ex As WebDriverTimeoutException
                                            Throw New Exception("Halaman tidak tersedia")
                                        End Try

                                        elementList =
                    driver.FindElements(By.XPath("//div[contains(@class, 'x1gslohp x11i5rnm x12nagc x1mh8g0r x1yc453h x126k92a')][contains(@dir, 'auto')]"))

                                        Dim elementListQuest As IReadOnlyCollection(Of IWebElement) =
                        driver.FindElements(By.XPath("//span[contains(@class, 'xvq8zen xo1l8bm xzsf02u')]/div[contains(@class, 'x1gslohp x11i5rnm x12nagc x1mh8g0r x1yc453h x126k92a')][contains(@dir, 'auto')]"))

                                        Dim willAsw As Boolean = True
                                        If elementList.Count > 0 AndAlso elementListQuest.Count > 0 Then
                                            Dim questi As String = elementListQuest(elementListQuest.Count - 1).GetAttribute("innerHTML")
                                            Dim lastEle As String = elementList(elementList.Count - 1).GetAttribute("innerHTML")
                                            If String.Compare(lastEle, questi) <> 0 Then
                                                willAsw = False
                                            End If
                                        End If

                                        If elementList.Count > 0 AndAlso elementListQuest.Count > 0 AndAlso willAsw Then

                                            'elementMssg = elementList(1).FindElement(By.XPath("div[1]/div[contains(@data-scope, 'messages_table')]/div/div[2]/div[1]/div[1]/div[1]/span/div[2]/div[1]/div/span/div"))

                                            Dim mssg = elementList(elementList.Count - 1).GetAttribute("innerHTML")

                                            Dim messageSend As String = fieldList(1)(1)
                                            For i As Integer = 1 To fieldList.Count - 1
                                                Dim isFound As Boolean = False
                                                Dim field = fieldList(i)
                                                Dim quests As String() = field(0).Split("/")
                                                For Each quest In quests
                                                    If mssg.ToLower().Contains(quest.ToLower()) Then
                                                        isFound = True
                                                        messageSend = field(1).ToString()
                                                        Exit For
                                                    End If
                                                Next

                                                If isFound Then
                                                    Exit For
                                                End If
                                            Next

                                            elementList =
                driver.FindElements(By.XPath("//div[contains(@data-lexical-editor, 'true')][contains(@aria-label, 'Pesan')]/p"))
                                            If elementList.Count > 0 Then
                                                elementList(0).SendKeys(messageSend)
                                            End If

                                            Thread.Sleep(jedarandom)
                                            WaitHandle.WaitAny({suspendEvent})

                                            elementList =
        driver.FindElements(By.XPath("//div[contains(@aria-label, 'Tekan Enter untuk mengirim')]"))
                                            If elementList.Count > 0 Then
                                                elementList(0).Click()

                                                totalRenew += 1
                                            End If

                                        End If

                                        Thread.Sleep(jedarandom)
                                        WaitHandle.WaitAny({suspendEvent})

                                        elementList =
    driver.FindElements(By.XPath("//div[contains(@aria-label, 'Tutup obrolan')]"))
                                        If elementList.Count > 0 Then
                                            elementList(0).Click()
                                        End If

                                    End If
                                Catch ex As Exception
                                End Try

                            End If

                        End If

                    Next
                End If

                Thread.Sleep(jedarandom)

                existingProfile.updateProgress(Me.gridProfile, userId, 100)
                WaitHandle.WaitAny({suspendEvent})

                success = "SELESAI"
                Catch ex As Exception
                    messageError = ex.Message
                    success = "GAGAL"
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
    End Sub

    Private Sub btnImportCsv_Click(sender As Object, e As EventArgs) Handles btnImportCsv.Click
        '//data Csv akan di buka dan di tampung pada object fieldList
        Dim OpenFileDialog1 As New OpenFileDialog
        OpenFileDialog1.Filter = "CSV File | *.csv|Text files (*.txt)|*.txt"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            txtCsv.Text = OpenFileDialog1.FileName
            totalRow = 1
            fieldList.Clear()
            Using csvFile As New FileIO.TextFieldParser(txtCsv.Text)
                csvFile.TextFieldType = FileIO.FieldType.Delimited
                csvFile.SetDelimiters(",")
                ' Loop melalui baris-baris CSV hingga mencapai baris 'startRow'
                While Not csvFile.EndOfData
                    Dim fields As String() = csvFile.ReadFields()
                    fieldList.Add(fields)
                    totalRow += 1
                End While
            End Using
        End If
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

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim webAddress As String = "https://drive.google.com/file/d/1yjVxXr9DBSete1DW-RZRXIGqYw14B5CP/view?usp=sharing"
        Process.Start(webAddress)
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim webAddress As String = "https://youtu.be/NbnMn8UQb3A"
        Process.Start(webAddress)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        For Each profile In baseForm.Profiles
            If profile.IsOnProcess Then
                MessageBox.Show("Tidak Bisa menutup Halaman, Masih terdapat proses yang belum selesai", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next
        Dim tabName = "MessageReplyer"
        baseForm.CloseAndRemoveTabPage(tabName)

    End Sub

    Dim isPause = False
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
End Class
