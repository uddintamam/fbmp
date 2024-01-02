Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports SeleniumExtras.WaitHelpers
Imports SeleniumUndetectedChromeDriver
Imports System.IO
Imports System.Threading

Public Class InteractionFBControl

    Dim baseForm As FormBase = DirectCast(Parent, FormBase)
    Private doubleClickOccurred As Boolean = False

    Private profileDataSet As New DataSet()
    Private Checkboxheader As CheckBox = New CheckBox()
    Private stopEvent As New ManualResetEvent(False)
    Private suspendEvent As New ManualResetEvent(True)

    Public Sub New()
        InitializeComponent()

        ' Misalkan Anda memiliki DataGridView bernama dgvProgress
        Dim progressBarColumn As New ProgressBarColumn()
        progressBarColumn.HeaderText = "PROGRESS"
        progressBarColumn.Name = "ProgressCol"
        progressBarColumn.Width = 150
        gridProfile.Columns.Add(progressBarColumn)
    End Sub

    Private Sub PostHomeAndGroupControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

            Dim groupId As String = txtGroupId.Text
            If String.IsNullOrEmpty(groupId) Then
                MessageBox.Show("Id Group Wajib di isi!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                doubleClickOccurred = False
                Return
            End If

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

#End Region
            Try
                runRobot(cbxDataProfile.Text, groupId, chkRunAllProfile.Checked)
            Catch ex As Exception
                MessageBox.Show(ex.Message,
                                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            doubleClickOccurred = False
        End If
    End Sub

    Private Sub runRobot(profileName As String, groupId As String, isRunAll As Boolean)
        Dim foundRows As DataRow() = profileDataSet.Tables(0).Select($"profileChromeCol = '{profileName}' AND digunakanCol = True")

        If foundRows.Count > 0 Then
            Dim existingProfile = baseForm.Profiles.Find(Function(p) p.ProfileName = profileName And p.IsOnProcess = True)
            If existingProfile Is Nothing Then
                Dim newThread As Thread =
                              New Thread(Sub() runRobotWork(foundRows, groupId, isRunAll))
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

    Private Sub runRobotWork(foundRows As DataRow(), groupId As String, isRunAll As Boolean)

#Region "Proses membuka browser dan inject element ke dalam threads"

        For Each foundRow In foundRows
            ' //cek apakah Profile yang di pilih sedang digunakan
            Dim existingProfile = baseForm.Profiles.Find(Function(p) p.ProfileName = foundRow(2) And p.IsOnProcess = True)
            If existingProfile Is Nothing Then
                '========================================
                'Masukkan proses kedalam antrian threads
                'proses dilanjutkan ke function PostingLiteFb
                If isRunAll Then
                    Task.Run(Sub() InteractionFb(foundRow, groupId))
                    Thread.Sleep(1000)
                Else
                    InteractionFb(foundRow, groupId)
                End If

            Else
                MessageBox.Show(
                    String.Concat("Harapa tunggu beberapa saat sampai proses selesai untuk Akun ", foundRow(2)),
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Next
#End Region
    End Sub


    Private Sub InteractionFb(dataProfile As DataRow, groupId As String)

        Dim runningProfile = ""
        Dim userId As String = ""
        Dim existingProfile As ChromeProfile = Nothing
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
            baseForm.sleep(1)
            WaitHandle.WaitAny({suspendEvent})
            Dim waitforelement As WebDriverWait = New WebDriverWait(driver, TimeSpan.FromSeconds(ChromeProfile.waitElement))
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
            Dim waktuRandom As Integer = random.Next(4, 6) * 1000 'dalam milidetik
            Dim jedarandom As Integer = random.Next(1, 2) * 1000 'dalam milidetik

            Thread.Sleep(jedarandom * 2)
            WaitHandle.WaitAny({suspendEvent})
            Try
                statusPost = existingProfile.CheckLoginProcess(dataProfile)
                existingProfile.updateProgress(Me, userId, CInt(1 * 100 / 5))

                If Not statusPost.Status Then
                    Throw New Exception(statusPost.Message)
                End If

                Thread.Sleep(jedarandom)
                WaitHandle.WaitAny({suspendEvent})

                driver.GoToUrl("https://www.facebook.com/")

                Thread.Sleep(jedarandom)
                WaitHandle.WaitAny({suspendEvent})

                existingProfile.updateProgress(Me, userId, CInt(2 * 100 / 5))

                Thread.Sleep(jedarandom)
                WaitHandle.WaitAny({suspendEvent})

                Dim sukaBeranda As Integer = 0

                Try
                    waitforelement.Until(ExpectedConditions.ElementExists(By.XPath("//div[contains(@class, 'xq8finb x16n37ib')]//div[contains(@aria-label, 'Suka')][contains(@role, 'button')]")))
                Catch ex As WebDriverTimeoutException
                    Throw New Exception("Halaman tidak tersedia")
                End Try

                For a = 1 To 2
                    For i = 1 To 5
                        elementList =
driver.FindElements(By.XPath("//div[contains(@class, 'xq8finb x16n37ib')]//div[contains(@aria-label, 'Suka')][contains(@role, 'button')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            WaitHandle.WaitAny({suspendEvent})

                            Dim berhasil As Boolean = False
                            Dim percobaan As Integer = 0
                            Dim mulai As Integer = 1
                            If elementList.Count - 5 < 1 Then
                                mulai = 1
                            Else
                                mulai = elementList.Count - 5
                            End If
                            ' Do While loop untuk mengulangi tindakan jika gagal
                            Do While Not berhasil AndAlso percobaan < 3
                                Try
                                    Dim angkaAcak As Integer = random.Next(mulai, elementList.Count) - 1

                                    If elementList(angkaAcak).Enabled Then
                                        elementList(angkaAcak).Click()

                                        sukaBeranda += 1
                                    End If
                                    berhasil = True
                                Catch ex As Exception
                                    berhasil = False
                                    percobaan += 1
                                End Try
                            Loop

                            driver.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);")
                            Thread.Sleep(jedarandom) ' Tunggu beberapa detik untuk memuat
                            WaitHandle.WaitAny({suspendEvent})

                            driver.ExecuteScript("window.scrollBy(0, -100);")
                            ' Mendapatkan elemen scroll pada halaman web
                            Thread.Sleep(jedarandom * 4) ' Tunggu beberapa detik untuk memuat
                            WaitHandle.WaitAny({suspendEvent})

                            existingProfile.updateProgress(Me.gridProfile, userId, (CInt(2 * 100 / 5) + CInt(sukaBeranda * 10 / 10)))

                        End If
                    Next

                    If a < 2 Then
                        driver.GoToUrl(groupId)
                        Thread.Sleep(jedarandom) ' Tunggu beberapa detik untuk memuat
                        WaitHandle.WaitAny({suspendEvent})
                        driver.ExecuteScript("window.scrollTo(0, 300);")
                        Thread.Sleep(jedarandom * 2) ' Tunggu beberapa detik untuk memuat
                    End If
                Next

                existingProfile.updateProgress(Me.gridProfile, userId, CInt(3 * 100 / 5))

                driver.GoToUrl("https://www.facebook.com/reel/")
                For i = 1 To 5
                    Thread.Sleep(jedarandom * 2) ' Tunggu beberapa detik untuk memuat
                    WaitHandle.WaitAny({suspendEvent})

                    elementList =
driver.FindElements(By.XPath("//div[contains(@aria-label, 'Suka')][contains(@role, 'button')]"))
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                    End If

                    Thread.Sleep(jedarandom * 2) ' Tunggu beberapa detik untuk memuat
                    WaitHandle.WaitAny({suspendEvent})

                    elementList =
                    driver.FindElements(By.XPath("//div[contains(@aria-label, 'Kartu Berikutnya')]"))
                    If elementList.Count > 0 Then
                        elementList(0).Click()
                    End If
                    WaitHandle.WaitAny({suspendEvent})

                    existingProfile.updateProgress(Me.gridProfile, userId, (CInt(3 * 100 / 5) + CInt(i * 10 / 10)))
                Next

                Thread.Sleep(jedarandom) ' Tunggu beberapa detik untuk memuat

                driver.GoToUrl("https://www.facebook.com/watch")

                For i = 1 To 5
                    elementList =
driver.FindElements(By.XPath("//div[contains(@class, 'x78zum5 x1iyjqo2')]/div[contains(@class, 'x6s0dn4 xzsf02u x78zum5 x1nxh6w3 x1u2d2a2')]/div[contains(@aria-label, 'Suka')][contains(@role, 'button')]"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then

                        Dim berhasil As Boolean = False
                        Dim percobaan As Integer = 0
                        Dim mulai As Integer = 1
                        If elementList.Count - 5 < 1 Then
                            mulai = 1
                        Else
                            mulai = elementList.Count - 5
                        End If
                        ' Do While loop untuk mengulangi tindakan jika gagal
                        Do While Not berhasil AndAlso percobaan < 3
                            Try
                                Dim angkaAcak As Integer = random.Next(mulai, elementList.Count) - 1

                                If elementList(angkaAcak).Enabled Then
                                    elementList(angkaAcak).Click()
                                    Thread.Sleep(jedarandom * 5) ' Tunggu beberapa detik untuk memuat
                                    sukaBeranda += 1
                                End If
                                berhasil = True
                            Catch ex As Exception
                                berhasil = False
                                percobaan += 1
                            End Try
                        Loop

                        driver.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);")
                        Thread.Sleep(jedarandom) ' Tunggu beberapa detik untuk memuat

                        driver.ExecuteScript("window.scrollBy(0, -100);")
                        ' Mendapatkan elemen scroll pada halaman web
                        Thread.Sleep(jedarandom * 4) ' Tunggu beberapa detik untuk memuat

                        ' Mendapatkan elemen scroll pada halaman web

                        existingProfile.updateProgress(Me.gridProfile, userId, (CInt(3 * 100 / 5) + CInt((5 + i) * 10 / 10)))
                        WaitHandle.WaitAny({suspendEvent})

                    End If
                Next

                existingProfile.updateProgress(Me.gridProfile, userId, CInt(4 * 100 / 5))

                Thread.Sleep(jedarandom)
                WaitHandle.WaitAny({suspendEvent})

                existingProfile.updateProgress(Me.gridProfile, userId, CInt(5 * 100 / 5))
                success = "SELESAI"
            Catch ex As Exception
                messageError = ex.Message
                success = "GAGAL"
            End Try
            WaitHandle.WaitAny({suspendEvent})

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
        BeginInvoke(Sub() MessageBox.Show("Profil " & runningProfile & " Selesai dijalankan!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information))
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

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim webAddress As String = "https://youtu.be/i5cAViwYAi4"
        Process.Start(webAddress)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        For Each profile In baseForm.Profiles
            If profile.IsOnProcess Then
                MessageBox.Show("Tidak Bisa menutup Halaman, Masih terdapat proses yang belum selesai", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        Dim tabName = "InteractionFB"
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
