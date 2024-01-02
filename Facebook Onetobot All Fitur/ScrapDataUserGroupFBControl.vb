Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports SeleniumUndetectedChromeDriver
Imports System.IO
Imports System.Threading
Imports System.Configuration
Imports OpenQA.Selenium.Interactions

Public Class ScrapDataUserGroupFBControl

    Dim baseForm As FormBase = DirectCast(Parent, FormBase)
    Dim fieldList As New List(Of String())()
    Private doubleClickOccurred As Boolean = False

    Private profileDataSet As New DataSet()

    Private Checkboxheader As CheckBox = New CheckBox()
    Dim totalRow As Integer = 1
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub ScrapDataUserGroupFBControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Periksa apakah file UserData.xml sudah ada
        baseForm = DirectCast(Parent, FormBase)

        Dim HeadercellLocation As Point = Me.gridProfile.GetCellDisplayRectangle(0, -1, True).Location
        Checkboxheader.Location = New Point(HeadercellLocation.X + 8, HeadercellLocation.Y + 2)
        Checkboxheader.Size = New Size(18, 18)
        Checkboxheader.BackColor = Color.White

        Checkboxheader.Checked = False

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
                table.Columns.Add("StartCsvCol", GetType(Integer))
                table.Columns.Add("sampaiCsvCol", GetType(Integer))
                'table.Columns.Add("ProgressCol", GetType(Integer))


                ' Menerapkan filter ke DataView
                Dim dataView As New DataView(dataSet.Tables("UserData"))
                dataView.RowFilter = String.Concat("IsLogin = '1'")

                '//Dim rowCount As Integer = My.Computer.FileSystem.GetDirectories(FixCustom).Count

                ' Inisialisasi nilai awal untuk kolom StartCsvCol
                Dim defaultStartValue As Integer = Convert.ToInt16(ConfigurationManager.AppSettings("startNum"))
                Dim startValue As Integer = defaultStartValue
                Dim rangeValue As Integer = Convert.ToInt16(ConfigurationManager.AppSettings("rangeNum"))

                ' Inisialisasi nilai awal untuk kolom sampaiCsvCol
                Dim endValue As Integer = startValue + rangeValue
                Dim profileSelect As String = String.Empty

                For Each rowView As DataRowView In dataView
                    Dim profileName As String = rowView("ProfileName").ToString()
                    Dim userId As String = rowView("UserId").ToString()
                    Dim password As String = rowView("Password").ToString()

                    If String.Compare(profileSelect, profileName) <> 0 Then
                        startValue = defaultStartValue
                        endValue = startValue + rangeValue
                        profileSelect = profileName
                    End If

                    ' Menambahkan baris ke DataGridView pertama
                    table.Rows.Add(True, profileName, userId, password, startValue, endValue)

                    startValue = endValue + 1
                    endValue = startValue + rangeValue
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

            For Each rowView As DataRowView In dataView
                Dim isUsed As Boolean = CBool(rowView("digunakanCol"))
                Dim profileName As String = rowView("profileChromeCol").ToString()
                Dim userId As String = rowView("userIdforPostCol").ToString()
                Dim password As String = rowView("passwordforPostCol").ToString()

                Dim startValue As Integer = CInt(rowView("StartCsvCol"))
                Dim endValue As Integer = CInt(rowView("sampaiCsvCol"))
                'Dim progress As Integer = CInt(rowView("ProgressCol"))

                ' Menambahkan baris ke DataGridView pertama
                gridProfile.Rows.Add(isUsed, profileName, userId, password, startValue, endValue)
            Next

        End If
        Return True
    End Function

    Private Sub Button225_Click(sender As Object, e As EventArgs) Handles btnImportCsv.Click
        Dim OpenFileDialog1 As New OpenFileDialog
        OpenFileDialog1.Filter = "CSV File | *.csv|Text files (*.txt)|*.txt"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            txtCsv.Text = OpenFileDialog1.FileName
            totalRow = 1
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

        If (e.ColumnIndex = sampaiCsvCol.Index AndAlso e.RowIndex >= 0) Or (e.ColumnIndex = StartCsvCol.Index) Then
            Dim editingControlBounds As Rectangle =
                gridProfile.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, False)

            ' Buat dan konfigurasi NumericUpDown control
            Dim numericUpDown As New NumericUpDown()
            numericUpDown.Minimum = 0
            numericUpDown.Maximum = 99999
            numericUpDown.Value = CInt(gridProfile(e.ColumnIndex, e.RowIndex).Value)

            ' Atur posisi NumericUpDown control
            numericUpDown.Bounds = editingControlBounds

            ' Tambahkan NumericUpDown control ke DataGridView
            gridProfile.Controls.Add(numericUpDown)

            ' Atur nilai saat kontrol kehilangan fokus
            AddHandler numericUpDown.Leave,
                Sub(senderControl As Object, eControl As EventArgs)
                    gridProfile(e.ColumnIndex, e.RowIndex).Value = numericUpDown.Value
                    gridProfile.Controls.Remove(numericUpDown)
                End Sub

            ' Atur fokus pada NumericUpDown control
            numericUpDown.Focus()
        End If
    End Sub

    Private Sub gridProfile_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles gridProfile.CellValidating
        If e.ColumnIndex = sampaiCsvCol.Index AndAlso e.RowIndex >= 0 Then
            Dim newValue As Integer

            If Not Integer.TryParse(e.FormattedValue.ToString(), newValue) Then
                ' Jika nilai yang dimasukkan tidak berupa angka, tampilkan pesan kesalahan
                e.Cancel = True
                MessageBox.Show("Masukkan angka yang valid.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                ' Periksa apakah nilai melebihi nilai di sampaiCsvCol baris berikutnya
                If e.RowIndex < gridProfile.Rows.Count - 1 Then
                    Dim startValue As Integer = CInt(gridProfile("StartCsvCol", e.RowIndex).Value)
                    Dim endValue As Integer = CInt(gridProfile("sampaiCsvCol", e.RowIndex).Value)
                    If endValue < startValue Then
                        ' Jika nilai melebihi atau sama dengan nilai di baris berikutnya, batalkan perubahan
                        e.Cancel = True
                        gridProfile("sampaiCsvCol", e.RowIndex).Value = startValue
                        MessageBox.Show("Nilai sampai tidak boleh kurang dari nilai mulai.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub gridProfile_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles gridProfile.CellValueChanged
        ' Cek apakah perubahan terjadi di kolom sampaiCsvCol
        If e.ColumnIndex = sampaiCsvCol.Index AndAlso e.RowIndex >= 0 Then
            ' Ambil nilai yang baru di kolom sampaiCsvCol
            Dim newValue As Integer = CInt(gridProfile("sampaiCsvCol", e.RowIndex).Value)

            ' Hitung ulang nilai di kolom StartCsvCol
            If e.RowIndex < gridProfile.Rows.Count - 1 Then
                gridProfile("StartCsvCol", e.RowIndex + 1).Value = newValue + 1
            End If
        End If
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

            If String.IsNullOrEmpty(txtCsv.Text) Then
                MessageBox.Show("File CSV Belum dipilih", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
                doubleClickOccurred = False
                Return
            End If
#End Region

            Try
                If File.Exists(ChromeProfile.dataUser) Then
                    ' Muat data dari file XML
                    Dim dataSet As New DataSet

                    ' Muat data dari file XML
                    dataSet.ReadXml(ChromeProfile.dataUser)

                    If dataSet.Tables.Count > 1 Then
                        Dim dataProfile As DataTable = dataSet.Tables(1)

                        If Not chkRunAllProfile.Checked Then
                            runRobot(cbxDataProfile.Text)
                        Else
                            For Each rowProfile As DataRow In dataProfile.Rows
                                Dim profileName = rowProfile("ProfileName")
                                runRobot(profileName)
                            Next
                        End If
                    End If
                Else
                    MessageBox.Show(String.Concat("UserData.xml Tidak Ditemukan"),
                                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message,
                                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            doubleClickOccurred = False
        End If
    End Sub

    Private Sub runRobot(profileName As String)
        ' //cek apakah Profile yang di pilih sedang digunakan
        Dim existingProfile = baseForm.Profiles.Find(Function(p) p.ProfileName = profileName And p.IsOnProcess = True)
        If existingProfile Is Nothing Then

            Dim foundRows As DataRow() = profileDataSet.Tables(0).Select($"profileChromeCol = '{profileName}' AND digunakanCol = True")

            If foundRows.Count > 0 Then
                Dim windowsSize As Integer

#Region "Proses membuka browser dan inject element ke dalam threads"

                '========================================
                '//membuka Browser chrome dan menyimpan ke object Profiles di FormBase
                existingProfile = baseForm.runChromeDriver(profileName, "", windowsSize)
                '========================================

                '//mendefinisikan jika profile yang dipilih sedang digunakan
                If existingProfile IsNot Nothing Then
                    existingProfile.IsOnProcess = True
                End If

                '========================================
                'Masukkan proses kedalam antrian threads
                'proses dilanjutkan ke function PostingLiteFb
                Dim newThread As Thread =
                    New Thread(Sub() PostingLiteFb(foundRows, fieldList,
                                                   existingProfile))
                '========================================

                ' Menambahkannya ke daftar thread
                baseForm.threads.Add(newThread)

                ' Memulai thread
                newThread.Start()

#End Region
            End If
        Else
            MessageBox.Show(
                String.Concat("Harapa tunggu beberapa saat sampai proses selesai untuk profile ", profileName),
                            "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub PostingLiteFb(dataProfileAccount As DataRow(), fieldList As List(Of String()), existingProfile As ChromeProfile)

        Dim runningProfile = ""
        Dim userId As String = ""

        Dim statusPost As New StatusPost()
        Try
            Dim driver As UndetectedChromeDriver = Nothing

            If existingProfile IsNot Nothing Then
                driver = existingProfile.Driver
                runningProfile = existingProfile.ProfileName

                If driver Is Nothing Then
                    Throw New Exception("Terjadi Konfik, Harap tutup Browser")
                End If
            End If
            Dim countUser = 0

            Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing

            For Each dataProfile As DataRow In dataProfileAccount
                userId = dataProfile(2)
                Dim password As String = dataProfile(3)

                Dim startRow As Integer = dataProfile(4)
                Dim endRow As Integer = dataProfile(5)

                baseForm.sleep(1)
                Dim waitforelement As WebDriverWait = New WebDriverWait(driver, TimeSpan.FromSeconds(60 * 60))
                Try
                    Dim title = driver.Title
                Catch ex As Exception
                    If driver IsNot Nothing Then
                        driver.Quit() 'menutup browser
                    End If
                End Try

                If countUser > 0 Then
                    ' Simulasikan pembukaan tab baru (Ctrl+T)
                    Dim actions As Actions = New Actions(driver)
                    actions.KeyDown(Keys.Control).SendKeys("t").KeyUp(Keys.Control).Build().Perform()

                    ' Beralih ke tab baru yang telah dibuka
                    driver.SwitchTo().Window(driver.WindowHandles.Last())

                End If

                driver.GoToUrl("https://www.facebook.com")
                Dim messageError As String = String.Empty

                driver.Navigate.Refresh()
                '// MENDETEK KODINGAN JIKA TIDAK ADA MP NYA 
                Dim random As New Random()
                Dim waktuRandom As Integer = random.Next(5, 7) * 1000 'dalam milidetik
                Dim jedarandom As Integer = random.Next(2, 4) * 1000 'dalam milidetik

                Thread.Sleep(waktuRandom * 2)
                Try
                    statusPost = existingProfile.LoginProcess(userId, password, jedarandom)

                    If Not statusPost.Status Then
                        Throw New Exception(statusPost.Message)
                    End If
                    If fieldList.Count < endRow Then
                        messageError = "Jumlah Baris di File CSV tidak mencukupi"
                        countUser += 1

                        Thread.Sleep(jedarandom)

                        elementList =
                                    driver.FindElements(By.XPath("//form[contains(@action, '/logout.php')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(0).Submit()
                        End If

                        MessageBox.Show("User Id " & userId & " : " & messageError, "Peringatan", MessageBoxButtons.OK)
                        Continue For
                    End If

                    For i As Integer = startRow - 1 To endRow - 1
                        Dim fields As String() = fieldList(i)
                        Dim postStatus As StatusPost = Nothing

                        If fields.Length >= 2 Then
                            If Not String.IsNullOrEmpty(fields(0)) Then
                                Dim urlGroup = fields(0) & "/members"

                                Thread.Sleep(jedarandom)

                                driver.GoToUrl(urlGroup)

                                Thread.Sleep(jedarandom)

                                elementList =
driver.FindElements(By.XPath("//span[text()='Anggota']/span/strong"))
                                ' Cek apakah elemen ada atau tidak ada
                                If elementList.Count > 0 Then

                                    ' Hapus semua elemen <span> dalam elemen <strong> menggunakan JavaScript
                                    Dim script As String = "var strongElement = arguments[0];" &
                                                          "var spans = strongElement.getElementsByTagName('span');" &
                                                          "for (var i = 0; i < spans.length; i++) {" &
                                                          "    strongElement.removeChild(spans[i]);" &
                                                          "}"
                                    CType(driver, IJavaScriptExecutor).ExecuteScript(script, elementList(0))

                                    Dim totalMember As Integer = Convert.ToInt32(elementList(0).GetAttribute("innerHTML").Replace(".", ""))
                                    Dim limit As Integer = Convert.ToInt32(fields(1))

                                    ' Gulir halaman untuk memuat lebih banyak anggota grup (gunakan perulangan jika perlu)
                                    Dim jumlahGuliran As Integer = 1 ' Ubah sesuai kebutuhan Anda
                                    Dim limitUser As Integer = 1

                                    If totalMember > limit Then
                                        jumlahGuliran = limit / 10
                                        limitUser = limit
                                    Else
                                        jumlahGuliran = totalMember / 10
                                        limitUser = totalMember
                                    End If

                                    For a As Integer = 1 To jumlahGuliran
                                        driver.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);")
                                        Thread.Sleep(jedarandom) ' Tunggu beberapa detik untuk memuat
                                    Next

                                    ' Cari elemen-elemen yang mengandung user ID (ini mungkin perlu disesuaikan dengan tampilan halaman Facebook)
                                    Dim elemenUserIDs As IList(Of IWebElement) = driver.FindElements(By.XPath("//div[contains(@class, '1lq5wgf xgqcy7u x30kzoy')]/div/div[2]/div[1]//span[contains(@class, 'xt0psk2')]/a[contains(@href, '/groups/')]"))

                                    ' Ambil user ID dari setiap elemen
                                    Dim userIdMembers As New List(Of String)
                                    For a As Integer = 0 To limitUser - 1
                                        Dim elemen As IWebElement = elemenUserIDs(a)
                                        Dim href As String = elemen.GetAttribute("href")
                                        ' Temukan elemen <a> berdasarkan atribut href
                                        ' Eksekusi JavaScript untuk mengambil teks dari elemen yang berada di bawah elemen <a>
                                        Dim javascriptExecutor As IJavaScriptExecutor = CType(driver, IJavaScriptExecutor)
                                        Dim NamaUser As String = javascriptExecutor.ExecuteScript("return arguments[0].textContent;", elemen).ToString()
                                        gridReport.Invoke(
                                    Sub() gridReport.Rows.Add(href, NamaUser))

                                    Next

                                End If
                            End If
                        End If


                    Next

                    Thread.Sleep(jedarandom)

                    elementList =
                       driver.FindElements(By.XPath("//form[contains(@action, '/logout.php')]"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        elementList(0).Submit()
                    End If

                    countUser += 1
                Catch ex As Exception
                    messageError = ex.Message
                End Try
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
        If existingProfile IsNot Nothing Then
            baseForm.Invoke(Sub() existingProfile.IsOnProcess = False)
        End If
    End Sub

    Private Sub gridReport_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles gridReport.CellFormatting
        ' Pastikan kita hanya memproses kolom "status"

    End Sub

    Private Sub btnRefreshData_Click(sender As Object, e As EventArgs) Handles btnRefreshData.Click
        Dim dataSet As New DataSet

        ' Muat data dari file XML
        dataSet.ReadXml(ChromeProfile.dataUser)
        profileDataSet.Tables.Clear()
        loadDataFromBaseProfile(dataSet)
        loadDataForPOSTgridXML()
    End Sub

    Private Sub btnSimpanCsv_Click(sender As Object, e As EventArgs) Handles btnSimpanCsv.Click
        ' Buat string untuk menyimpan data
        Dim csvContent As New List(Of String)()

        ' Buat dialog untuk menyimpan file
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Filter = "File CSV (*.csv)|*.csv"
        saveFileDialog.Title = "Simpan File CSV"

        ' Jika pengguna menekan OK pada dialog
        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Dim fileName As String = saveFileDialog.FileName

            ' Header CSV (nama kolom)
            csvContent.Add("USER URL, NAMA") ' Ganti dengan nama-nama kolom yang sesuai

            ' Loop melalui setiap baris dalam DataGridView
            For Each row As DataGridViewRow In gridReport.Rows
                ' Pastikan baris tidak kosong
                If Not row.IsNewRow Then
                    ' Ambil data dari dua kolom yang Anda inginkan
                    Dim kolom1 As String = row.Cells(0).Value.ToString() ' Ganti dengan nama kolom yang sesuai
                    Dim kolom2 As String = row.Cells(1).Value.ToString() ' Ganti dengan nama kolom yang sesuai

                    ' Gabungkan data ke dalam satu baris CSV
                    Dim csvRow As String = kolom1 & "," & kolom2
                    csvContent.Add(csvRow)
                End If
            Next

            ' Simpan data CSV ke file
            File.WriteAllLines(fileName, csvContent)

            ' Buka folder di mana file CSV disimpan
            Dim folderPath As String = Path.GetDirectoryName(fileName)
            Process.Start("explorer.exe", folderPath)
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
End Class
