﻿Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports SeleniumUndetectedChromeDriver
Imports System.IO
Imports System.Threading
Imports System.Configuration

Public Class PostFBGeneralPostControl

#Region "properti"
    Dim baseForm As FormBase = DirectCast(Parent, FormBase)
    Dim _fiturPostFB As FiturPostFBEnum
    Dim fieldList As New List(Of String())()
    Dim totalRow As Integer = 1
    Private doubleClickOccurred As Boolean = False

    Private profileDataSet As New DataSet()
    Private suspendEvent As New ManualResetEvent(True)

    Private Checkboxheader As CheckBox = New CheckBox()
#End Region

#Region "Constructor"
    '//Constructor ini yang di inisialisaskan pada FormBase di event load

    '//Constructor tanpa parameter (saat ini belum digunakan)
    Public Sub New()
        InitializeComponent()

        ' Tambahkan Progress ke GridView
        Dim progressBarColumn As New ProgressBarColumn()
        progressBarColumn.HeaderText = "PROGRESS"
        progressBarColumn.Name = "ProgressCol"
        progressBarColumn.Width = 150
        gridProfile.Columns.Add(progressBarColumn)
    End Sub

    '//Constructor dengan parameter Enum (saat ini di gunakan untuk inisialisasi di FormBase)
    Public Sub New(fiturPostFB As FiturPostFBEnum)
        InitializeComponent()
        _fiturPostFB = fiturPostFB

        'mengubah Label Judul tombol start berdasarkan enum
        Select Case _fiturPostFB
            Case FiturPostFBEnum.General
                lblHeader.Text = "FITUR AUTO POST FBMP PRODUK UMUM BIASA"
                'btnStartProcess.Text = "          START ROBOT POST PRODUK UMUM BIASA"
            Case FiturPostFBEnum.OnlyDraft
                lblHeader.Text = "FITUR AUTO POST FBMP PRODUK UMUM DRAF"
                'btnStartProcess.Text = "          START ROBOT POST PRODUK UMUM DRAF"
            Case FiturPostFBEnum.DraftAndPost
                lblHeader.Text = "FITUR AUTO POST FBMP PRODUK UMUM ANTI DUPLIKAT"
                'btnStartProcess.Text = "          START ROBOT POST PRODUK UMUM ANTI DUPLIKAT"
        End Select

        '=================================
        ' inisialisasi Tambahkan Progress ke GridView
        Dim progressBarColumn As New ProgressBarColumn()
        progressBarColumn.HeaderText = "PROGRESS"
        progressBarColumn.Name = "ProgressCol"
        progressBarColumn.Width = 150
        gridProfile.Columns.Add(progressBarColumn)
        '=================================


    End Sub

#End Region



#Region "Event Control"

    Private Sub PostFBGeneralDraftAndPostControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'inisialisasi bahawa BaseForm adalah induk dari halaman ini
        baseForm = DirectCast(Parent, FormBase)

        '=================================
        'inisialisasi checkbox select/unselect all pada header gridProfile
        Dim HeadercellLocation As Point = Me.gridProfile.GetCellDisplayRectangle(0, -1, True).Location
        Checkboxheader.Location = New Point(HeadercellLocation.X + 8, HeadercellLocation.Y + 2)
        Checkboxheader.Size = New Size(18, 18)
        Checkboxheader.BackColor = Color.White

        Checkboxheader.Checked = True

        gridProfile.Controls.Add(Checkboxheader)

        AddHandler Checkboxheader.Click, AddressOf headerCheckbox_Click
        '=================================

        '=================================
        'Binding Data dari UserData.xml
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
        '=================================

        baseForm.isLoad = False
    End Sub

    Private Sub headerCheckbox_Click(ByVal sender As Object, ByVal e As EventArgs)
        gridProfile.EndEdit()
        'fungi select/uselect all
        For Each row As DataGridViewRow In gridProfile.Rows
            Dim chk As DataGridViewCheckBoxCell = TryCast(row.Cells("digunakanCol"), DataGridViewCheckBoxCell)
            chk.Value = Checkboxheader.Checked
        Next
    End Sub

    Private Sub Button225_Click(sender As Object, e As EventArgs) Handles btnImportCsv.Click
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


    '//event Tombol Start Posting Umum/Draft/posting dan Draft
    Private Sub btnStartProcess_Click(sender As Object, e As EventArgs) Handles btnStartProcess.Click
        'handle untuk double click
        If Not doubleClickOccurred Then
            doubleClickOccurred = True
            Dim category As Integer = cbxCategory.SelectedIndex

#Region "Validasi sebelum menjalankan proses"
            If category < 0 Then
                MessageBox.Show("Kategori Produk belum dipilih.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                doubleClickOccurred = False
                Return
            End If

            Dim conditionProd As Integer = cbxConditionProd.SelectedIndex

            If conditionProd < 0 Then
                MessageBox.Show("Kondisi Produk belum dipilih.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
                runRobot(cbxDataProfile.Text, category, conditionProd, chkRunAllProfile.Checked)
            Catch ex As Exception
                MessageBox.Show(ex.Message,
                                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            doubleClickOccurred = False
        End If
    End Sub

    Private Sub runRobot(profileName As String, category As String, conditionProd As String, isRunAll As Boolean)
        ' //cek apakah Profile yang di pilih sedang digunakan
        Dim foundRows As DataRow() = profileDataSet.Tables(0).Select($"profileChromeCol = '{profileName}' AND digunakanCol = True")

        If foundRows.Count > 0 Then

            Dim validToContinue As Boolean = False
            For Each dataProfile As DataRow In foundRows
                If dataProfile(0) Then
                    validToContinue = True
                    Exit For
                End If
            Next

            If Not validToContinue Then
                Throw New Exception("Tidak ada Data Profile dan akun Facebook yang akan di jalankan, Pilih Profil dan akun yang akan dijalankan")
            End If

            Dim existingProfile = baseForm.Profiles.Find(Function(p) p.ProfileName = profileName And p.IsOnProcess = True)
            If existingProfile Is Nothing Then

                Dim newThread As Thread =
                    New Thread(Sub() runRobotWork(foundRows,
                                                    category, conditionProd, isRunAll))
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

    Private Sub runRobotWork(foundRows As DataRow(), category As String, conditionProd As String, isRunAll As Boolean)
#Region "Proses membuka browser dan inject element ke dalam threads"

        For Each foundRow In foundRows
            Dim existingProfile = baseForm.Profiles.Find(Function(p) p.ProfileName = foundRow(2) And p.IsOnProcess = True)
            If existingProfile Is Nothing Then

                If isRunAll Then
                    Task.Run(Sub() PostingLiteFb(foundRow,
                                                category, conditionProd, fieldList))
                    Thread.Sleep(1000)
                Else
                    PostingLiteFb(foundRow,
                                                category, conditionProd, fieldList)
                End If
            Else
                MessageBox.Show(
                    String.Concat("Harapa tunggu beberapa saat sampai proses selesai untuk Akun ", foundRow(2)),
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Next

#End Region
    End Sub

    Private Sub gridReport_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles gridReport.CellFormatting
        ' Pastikan kita hanya memproses kolom "status"
        If e.ColumnIndex = gridReport.Columns("statusCol").Index AndAlso e.RowIndex >= 0 AndAlso Not gridReport.Rows(e.RowIndex).IsNewRow Then
            Dim cellValue As String = gridReport(e.ColumnIndex, e.RowIndex).Value.ToString()

            ' Tentukan kondisi, misalnya jika status adalah "berhasil"
            If cellValue = "BERHASIL" Then
                gridReport.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Green
                gridReport.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            ElseIf cellValue = "GAGAL" Then
                gridReport.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Red
                gridReport.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            End If
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

#End Region

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
                table.Columns.Add("ProgressCol", GetType(Integer))


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
                    table.Rows.Add(True, profileName, userId, password, startValue, endValue, 0)

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

            Dim allChecked = True
            For Each rowView As DataRowView In dataView
                Dim isUsed As Boolean = CBool(rowView("digunakanCol"))
                Dim profileName As String = rowView("profileChromeCol").ToString()
                Dim userId As String = rowView("userIdforPostCol").ToString()
                Dim password As String = rowView("passwordforPostCol").ToString()

                Dim startValue As Integer = CInt(rowView("StartCsvCol"))
                Dim endValue As Integer = CInt(rowView("sampaiCsvCol"))
                Dim progress As Integer = CInt(rowView("ProgressCol"))

                If allChecked AndAlso Not isUsed Then
                    allChecked = False
                End If

                ' Menambahkan baris ke DataGridView pertama
                gridProfile.Rows.Add(isUsed, profileName, userId, password, startValue, endValue, progress)
            Next

            Checkboxheader.Checked = allChecked

        End If
        Return True
    End Function

    'Function Proses Posting umum/ Draft/ Posting & Draft
    Private Sub PostingLiteFb(dataProfile As DataRow, categoryProd As Integer, conditionProd As Integer, fieldList As List(Of String()))

        Dim runningProfile = ""
        Dim userId As String = ""
        Dim existingProfile = Nothing

        Dim statusPost As New StatusPost()
        Try
            Dim countUser = 0

            Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing
            Dim profileName = dataProfile(1)
            userId = dataProfile(2)
            Dim password As String = dataProfile(3)

            Dim startRow As Integer = dataProfile(4)
            Dim endRow As Integer = dataProfile(5)


            Dim driver As UndetectedChromeDriver = Nothing

            '=============================
            'inisialisasi ukuran browser berdasarkan enum
            'Dim windowsSize = 0
            'Select Case _fiturPostFB
            '    Case FiturPostFBEnum.General
            '        windowsSize = 0
            '    Case FiturPostFBEnum.OnlyDraft
            '        windowsSize = 1
            '    Case FiturPostFBEnum.DraftAndPost
            '        windowsSize = 1
            'End Select
            '=============================

            '========================================
            '//membuka Browser chrome dan menyimpan ke object Profiles di FormBase
            existingProfile = baseForm.runChromeDriver(profileName, userId, 1)
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
            Dim waitforelement As WebDriverWait = New WebDriverWait(driver, TimeSpan.FromSeconds(ChromeProfile.waitElement))
            Try
                Dim title = driver.Title
            Catch ex As Exception
                If driver IsNot Nothing Then
                    driver.Quit() 'menutup browser
                End If
            End Try

#Region "Mulai membuka halaman facebook.com"
            driver.GoToUrl("https://www.facebook.com")
            Dim messageError As String = String.Empty
            WaitHandle.WaitAny({suspendEvent})

            driver.Navigate.Refresh()
            '// MENDETEK KODINGAN JIKA TIDAK ADA MP NYA 
            Dim random As New Random()
            Dim waktuRandom As Integer = random.Next(5, 7) * 1000 'dalam milidetik
            Dim jedarandom As Integer = random.Next(2, 4) * 1000 'dalam milidetik

            Thread.Sleep(waktuRandom * 2)
            Try


                '=============================================
                '//melanjutkan Check Login ke class ChromeProfile.vb
                statusPost = existingProfile.CheckLoginProcess(dataProfile)
                '=============================================
                WaitHandle.WaitAny({suspendEvent})

                If Not statusPost.Status Then
                    Throw New Exception(statusPost.Message)
                End If

                Thread.Sleep(jedarandom)
                If fieldList.Count < endRow Then
                    messageError = "Jumlah Baris di File CSV tidak mencukupi"


                    Throw New Exception(messageError)
                End If
                WaitHandle.WaitAny({suspendEvent})

                Dim nilaiAwal As Integer = startRow - 1
                Dim nilaiAkhir As Integer = endRow - 1

                Dim jmlBaris = nilaiAkhir - nilaiAwal + 1
                Dim current = 1
                For i As Integer = nilaiAwal To nilaiAkhir
                    Dim fields As String() = fieldList(i)
                    Dim postStatus As StatusPost = Nothing
#Region "Proses input Data berdasarkan CSV di teruskan ke Class ChromeProfile"
                    '//proses dijalankan berdasarkan enum
                    Select Case _fiturPostFB
                        Case FiturPostFBEnum.General
                            postStatus =
                                    existingProfile.PostGeneralFB(jedarandom, fields, waitforelement,
                                                             categoryProd, conditionProd, Me.gridProfile, userId,
                                                             current, jmlBaris, suspendEvent)
                        Case FiturPostFBEnum.OnlyDraft
                            postStatus =
                                    existingProfile.PostGeneralOnlyDraftFB(
                                                            jedarandom, fields, waitforelement,
                                                            categoryProd, conditionProd, Me.gridProfile, userId,
                                                            current, jmlBaris, suspendEvent)
                        Case FiturPostFBEnum.DraftAndPost
                            postStatus =
                                existingProfile.EditGeneraFromDraftFB(
                                                            jedarandom, fields, waitforelement,
                                                            categoryProd, conditionProd, Me.gridProfile, userId,
                                                            current, jmlBaris, suspendEvent)
                            'End If
                    End Select
#End Region

                    existingProfile.updateProgress(Me.gridProfile, userId, CInt(current * 100 / jmlBaris))

                    WaitHandle.WaitAny({suspendEvent})
                    If postStatus IsNot Nothing Then
                        If postStatus.Status Then
                            gridReport.Invoke(
                                Sub() gridReport.Rows.Add("BERHASIL", fields(0),
                                                            runningProfile, userId, ""))
                        Else
                            gridReport.Invoke(
                                Sub() gridReport.Rows.Add("GAGAL", fields(0),
                                                            runningProfile, userId, postStatus.Message))
                        End If
                    End If

                    current += 1
                Next

                Thread.Sleep(jedarandom)

            Catch ex As Exception
                If Not existingProfile.CheckInternetConnection() Then
                    gridReport.Invoke(
                        Sub() gridReport.Rows.Add("GAGAL", "", runningProfile, userId, "Koneksi Internet terputus"))
                Else
                    messageError = ex.Message
                    gridReport.Invoke(
                        Sub() gridReport.Rows.Add("GAGAL", "", runningProfile, userId, ex.Message))
                End If

            End Try
#End Region
            existingProfile.updateProgress(Me.gridProfile, userId, 100)
        Catch ex As Exception
            If Not existingProfile.CheckInternetConnection() Then
                gridReport.Invoke(
                           Sub() gridReport.Rows.Add("GAGAL", "", runningProfile, userId, "Koneksi Internet terputus"))
            Else
                gridReport.Invoke(
                Sub() gridReport.Rows.Add("GAGAL", "", runningProfile, userId, ex.Message))
            End If
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

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        For Each profile In baseForm.Profiles
            If profile.IsOnProcess Then
                MessageBox.Show("Tidak Bisa menutup Halaman, Masih terdapat proses yang belum selesai", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next
        Select Case _fiturPostFB
            Case FiturPostFBEnum.General
                Dim tabName = "PostGeneral"
                baseForm.CloseAndRemoveTabPage(tabName)

            Case FiturPostFBEnum.OnlyDraft
                Dim tabName = "GeneralDraft"
                baseForm.CloseAndRemoveTabPage(tabName)

            Case FiturPostFBEnum.DraftAndPost
                Dim tabName = "PostAntiDuplicate"
                baseForm.CloseAndRemoveTabPage(tabName)

        End Select

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim webAddress As String = "https://www.youtube.com/watch?v=ekZrUrGXvNE"
        Process.Start(webAddress)
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Dim webAddress As String = "https://www.youtube.com/watch?v=OdxJLhBWAFM"
        Process.Start(webAddress)
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim webAddress As String = "https://drive.google.com/file/d/1xumaCDlEzMyPQ4riM6rEf4ojRSHeQS2w/view?usp=sharing"
        Process.Start(webAddress)
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
End Class
