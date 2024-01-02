Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports SeleniumExtras.WaitHelpers
Imports SeleniumUndetectedChromeDriver
Imports System.IO
Imports System.Threading
Imports System.Configuration


Public Class LoginFacebook

    Dim baseForm As FormBase = DirectCast(Parent, FormBase)
    Dim isNewDataUsers As Boolean = False
    Dim indexCheckAcc As Integer = 1
    Dim indexLogin As Integer = 1
    Private doubleClickOccurred As Boolean = False
    Private stopEvent As New ManualResetEvent(False)
    Private suspendEvent As New ManualResetEvent(True)

    Private Sub LoginFacebook_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        baseForm = DirectCast(Parent, FormBase)
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
            loadUserDataXML(dataSet)
        End If

        If baseForm.licensePackage IsNot Nothing AndAlso baseForm.licensePackage.IsTrial AndAlso cbxDataProfile.Items.Count > 0 Then
            ButtonLoadCSV.Enabled = False
            btnAddProfile.Enabled = False
        End If

        btnAddProfile.Enabled = False
        btnAddProfile.BackColor = Color.Gainsboro
        baseForm.isLoad = False
    End Sub

    Function loadUserDataXML(dataSet As DataSet)
        If dataSet.Tables.Count > 0 Then
            Dim dataTable As DataTable = dataSet.Tables(0)

            ' Menerapkan filter ke DataView
            Dim dataView As New DataView(dataSet.Tables("UserData"))
            dataView.RowFilter = String.Concat("ProfileName = '", cbxDataProfile.Text, "'")

            ' Hapus semua baris yang ada di DataGridView
            gridUserFacebook.Rows.Clear()

            ' Isi DataGridView dengan data dari DataTable
            For Each rowView As DataRowView In dataView
                Dim Status As String = String.Empty

                Select Case CInt(rowView("IsLogin"))
                    Case 0
                        Status = "BELUM DICEK"
                    Case 1
                        Status = "BERHASIL"
                    Case 2
                        Status = "DIBANNED"
                    Case 3
                        Status = "GAGAL"

                End Select

                gridUserFacebook.Rows.Add(rowView("UserId").ToString(), rowView("Password").ToString(), rowView("VerifyCode").ToString(), rowView("IsLogin"), Status)
            Next
        End If
        Return True
    End Function

    Private Sub ButtonLoadCSV_Click(sender As Object, e As EventArgs) Handles ButtonLoadCSV.Click
        ' Buat dialog untuk memilih file CSV
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "CSV Files|*.csv"
        openFileDialog.Title = "Select a CSV File"
        openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            ' Baca file CSV yang dipilih
            Dim filePath As String = openFileDialog.FileName
            Try
                Dim csvData As String() = File.ReadAllLines(filePath)
                Dim profileName As String = ""
                ' Isi dropdown dengan data dari file CSV
                For i As Integer = 1 To 1
                    Dim rowValues As String() = csvData(i).Split(ConfigurationManager.AppSettings("delimeter"))
                    profileName = rowValues(0)
                Next

                cbxDataProfile.Text = profileName
                'gridUserFacebook.Rows.Clear()

                Dim dataRowList As New List(Of String())()
                ' Isi DataTable dengan data dari file CSV
                For i As Integer = 4 To csvData.Length - 1
                    Dim rowValues As String() = csvData(i).Split(ConfigurationManager.AppSettings("delimeter"))
                    Dim isUserIdExists As Boolean = False

                    ' Periksa apakah user ID sudah ada dalam DataGridView
                    For Each row As DataGridViewRow In gridUserFacebook.Rows
                        If row.Cells("UserIdCol").Value IsNot Nothing AndAlso
                            row.Cells("UserIdCol").Value.ToString() = rowValues(0).ToString() Then
                            isUserIdExists = True
                            Exit For
                        End If
                    Next
                    If Not isUserIdExists Then
                        dataRowList.Add({rowValues(0), rowValues(1), rowValues(2), "0", "BELUM DICEK"})
                        'gridUserFacebook.Rows.Add(rowValues(0), rowValues(1), False)
                    End If
                Next

                If dataRowList.Count + gridUserFacebook.Rows.Count > 10 Then
                    MessageBox.Show("Data User yang diizinkan hanya 10 akun!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Else
                    For Each dataRow In dataRowList
                        gridUserFacebook.Rows.Add(dataRow)
                    Next
                End If

                If gridUserFacebook.Rows.Count > 0 Then
                    changeButtonStatusLogin(False)
                End If

            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub changeButtonStatusLogin(enable As Boolean)
        If enable Then
            btnDelete.Text = "Delete Profile"
            btnDelete.Image = Global.Facebook_Onetobot_All_Fitur.My.Resources.Resources.trash_16
            btnLogin.Enabled = True
            btnLogin.BackColor = Color.Cyan
            cbxDataProfile.Enabled = True
            btnRefreshData.Enabled = True
            btnRefreshData.BackColor = Color.FromArgb(15, 102, 139)
            btnLoginManual.Enabled = True
            btnLoginManual.BackColor = Color.FromArgb(15, 102, 139)
            btnProductPost.Enabled = True
            btnProductPost.BackColor = Color.FromArgb(15, 102, 139)

            btnAddProfile.Enabled = False
            btnAddProfile.BackColor = Color.Gainsboro

            isNewDataUsers = False
        Else
            btnDelete.Text = "Batal Tambah"
            btnDelete.Image = Global.Facebook_Onetobot_All_Fitur.My.Resources.Resources.rotate_16
            btnLogin.Enabled = False
            btnLogin.BackColor = Color.Gainsboro
            cbxDataProfile.Enabled = False
            btnRefreshData.Enabled = False
            btnRefreshData.BackColor = Color.Gainsboro
            btnLoginManual.Enabled = False
            btnLoginManual.BackColor = Color.Gainsboro
            btnProductPost.Enabled = False
            btnProductPost.BackColor = Color.Gainsboro

            btnAddProfile.Enabled = True
            btnAddProfile.BackColor = Color.FromArgb(15, 102, 139)

            isNewDataUsers = True
        End If
    End Sub

    Private Sub btnAddProfile_Click(sender As Object, e As EventArgs) Handles btnAddProfile.Click
        Try

            If cbxDataProfile.Text = "" Then
                MsgBox("Nama Profile Harus Diisi!")
                Exit Sub
            ElseIf gridUserFacebook.Rows.Count = 0 Then
                MsgBox("Data Akun Facebook Harus Diisi!")
                Exit Sub
            Else
                Dim dataSet As New DataSet

                Dim profileExists As Boolean = False
                Dim addNewProfile As Boolean = False
                If File.Exists(ChromeProfile.dataUser) Then
                    dataSet.ReadXml(ChromeProfile.dataUser)

                    ' Periksa apakah targetUserId sudah ada dalam data
                    Dim profileTable As DataTable = dataSet.Tables("ProfileData")

                    If profileTable Is Nothing Then
                        profileTable = New DataTable("ProfileData")
                        profileTable.Columns.Add("ProfileName", GetType(String))
                        addNewProfile = True
                    End If

                    For Each rowtable As DataRow In profileTable.Rows
                        If rowtable("ProfileName").ToString() = cbxDataProfile.Text Then
                            profileExists = True
                            Exit For
                        End If
                    Next

                    If Not profileExists Then
                        profileTable.Rows.Add(cbxDataProfile.Text)
                    End If


                    ' Periksa apakah targetUserId sudah ada dalam data
                    Dim userTable As DataTable = dataSet.Tables("UserData")
                    Dim userExists As Boolean = False
                    Dim addNewUser As Boolean = False

                    If userTable Is Nothing Then
                        userTable = New DataTable("UserData")
                        ' Tambahkan kolom ke DataTable
                        userTable.Columns.Add("UserId", GetType(String))
                        userTable.Columns.Add("Password", GetType(String))
                        userTable.Columns.Add("VerifyCode", GetType(String))
                        userTable.Columns.Add("IsLogin", GetType(Integer))
                        userTable.Columns.Add("ProfileName", GetType(String))
                        addNewUser = True
                    End If

                    For Each row As DataGridViewRow In gridUserFacebook.Rows

                        For Each rowtable As DataRow In userTable.Rows
                            Dim userId As String = row.Cells("UserIdCol").Value.ToString()
                            If rowtable("UserId").ToString() = userId Then
                                userExists = True
                                If rowtable("ProfileName").ToString() IsNot cbxDataProfile.Text Then
                                    'Dim result As DialogResult = MessageBox.Show(String.Concat("User ID '", userId,
                                    '                              "' sudah ada dalam Data Profile '", rowtable("ProfileName").ToString(),
                                    '                              "', Apakah Akan tetap dilanjutkan?"),
                                    '                          "Peringatan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

                                    'If result = Windows.Forms.DialogResult.No Then
                                    '    Return
                                    'End If
                                End If
                                Exit For
                            End If
                        Next

                        ' Jika user tidak ditemukan, tambahkan data baru
                        If Not userExists Then
                            Dim userId As String = row.Cells("UserIdCol").Value.ToString()
                            Dim password As String = row.Cells("PasswordCol").Value.ToString()
                            Dim verifyCode As String = row.Cells("verifyCodeCol").Value.ToString()
                            Dim isLogin As Integer = row.Cells("IsLoginCol").Value
                            userTable.Rows.Add(userId, password, verifyCode, isLogin, cbxDataProfile.Text)
                        End If

                        userExists = False
                    Next


                    If addNewUser Then
                        dataSet.Tables.Add(userTable)
                    End If
                    If addNewProfile Then
                        dataSet.Tables.Add(profileTable)
                    End If
                Else

                    Dim dataTable As New DataTable("UserData")
                    ' Tambahkan kolom ke DataTable
                    dataTable.Columns.Add("UserId", GetType(String))
                    dataTable.Columns.Add("Password", GetType(String))
                    dataTable.Columns.Add("VerifyCode", GetType(String))
                    dataTable.Columns.Add("IsLogin", GetType(Integer))
                    dataTable.Columns.Add("ProfileName", GetType(String))

                    ' Tambahkan data ke DataTable
                    For Each row As DataGridViewRow In gridUserFacebook.Rows
                        If Not row.IsNewRow Then
                            Dim userId As String = row.Cells("UserIdCol").Value.ToString()
                            Dim password As String = row.Cells("PasswordCol").Value.ToString()
                            Dim verifyCode As String = row.Cells("verifyCodeCol").Value.ToString()
                            Dim isLogin As Integer = row.Cells("IsLoginCol").Value

                            dataTable.Rows.Add(userId, password, verifyCode, isLogin, cbxDataProfile.Text)
                        End If
                    Next

                    ' Tambahkan kolom ke DataTable profile data
                    Dim profileTable As New DataTable("ProfileData")
                    profileTable.Columns.Add("ProfileName", GetType(String))
                    ' Tambahkan data ke DataTable profile data
                    profileTable.Rows.Add(cbxDataProfile.Text)


                    dataSet.Tables.Add(dataTable)
                    dataSet.Tables.Add(profileTable)
                End If

                ' Simpan data ke file XML
                dataSet.WriteXml(ChromeProfile.dataUser)
                If Not profileExists Then
                    cbxDataProfile.Items.Add(cbxDataProfile.Text)
                End If

                For i = 0 To cbxDataProfile.Items.Count - 1
                    If cbxDataProfile.Text = cbxDataProfile.Items(i) Then
                        cbxDataProfile.SelectedIndex = i
                    End If
                Next

                changeButtonStatusLogin(True)

                MessageBox.Show("Berhasil disimpan!", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information)

                If baseForm.licensePackage IsNot Nothing AndAlso baseForm.licensePackage.IsTrial AndAlso cbxDataProfile.Items.Count > 0 Then
                    ButtonLoadCSV.Enabled = False
                    btnAddProfile.Enabled = False
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Not isNewDataUsers Then

                Dim result As DialogResult = MessageBox.Show(String.Concat("Apakah Anda yakin ingin menghapus Profile ", cbxDataProfile.Text, " ?"), "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If result = Windows.Forms.DialogResult.No Then
                    Return
                End If

                deleteAllDataUserAndProfile(cbxDataProfile.Text)
            Else
                Dim dataSet As New DataSet
                If File.Exists(ChromeProfile.dataUser) Then
                    ' Muat data dari file XML
                    dataSet.ReadXml(ChromeProfile.dataUser)
                    loadUserDataXML(dataSet)
                Else
                    gridUserFacebook.Rows.Clear()
                    cbxDataProfile.Text = String.Empty
                End If

                changeButtonStatusLogin(True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub deleteAllDataUserAndProfile(profileName As String)

        Dim dataSet As New DataSet()
        dataSet.ReadXml(ChromeProfile.dataUser)

        ' Membaca data dari UserData.xml ke dalam DataSet
        ' Mendapatkan tabel "UserData" dari DataSet
        Dim userTable As DataTable = dataSet.Tables("UserData")
        Dim profileTable As DataTable = dataSet.Tables("ProfileData")

        For Each dataRow In gridUserFacebook.Rows
            ' Ambil UserID dari baris yang akan dihapus
            Dim userIdToDelete As String = dataRow.Cells("UserIdCol").Value.ToString()

            ' Mencari userID yang akan dihapus
            Dim rowToDelete As DataRow = Nothing
            For Each row As DataRow In userTable.Rows
                If row("UserId").ToString() = userIdToDelete Then
                    rowToDelete = row
                    Exit For
                End If
            Next


            If rowToDelete IsNot Nothing Then
                ' Hapus row dari tabel
                userTable.Rows.Remove(rowToDelete)

            Else
                MessageBox.Show(
                String.Concat("UserID ", userIdToDelete, " tidak ditemukan dalam data UserData.xml."),
                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Next


        ' Mencari userID yang akan dihapus
        Dim rowProfileToDelete As DataRow = Nothing
        For Each row As DataRow In profileTable.Rows
            If row("ProfileName").ToString() = profileName Then
                rowProfileToDelete = row
                Exit For
            End If
        Next

        If rowProfileToDelete IsNot Nothing Then
            ' Hapus row dari tabel
            profileTable.Rows.Remove(rowProfileToDelete)

            ' Path ke folder yang ingin diperiksa atau dihapus
            Dim folderPath As String = Path.Combine(ChromeProfile.MyDoumentProfile, profileName)

            ' Memeriksa apakah folder ada
            If Directory.Exists(folderPath) Then
                ' Folder ada, Anda dapat menghapusnya jika diperlukan
                Try
                    Directory.Delete(folderPath, True)
                Catch ex As Exception
                    MessageBox.Show("Gagal menghapus folder: " & ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

        Else
            MessageBox.Show(
            String.Concat("Profile ", profileName, " tidak ditemukan dalam data UserData.xml."),
            "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        ' Simpan perubahan ke UserData.xml
        dataSet.WriteXml(ChromeProfile.dataUser)

        gridUserFacebook.Rows.Clear()
        cbxDataProfile.Items.Remove(profileName)
        If cbxDataProfile.Items.Count > 0 Then
            cbxDataProfile.SelectedIndex = 0
        End If
    End Sub

    Private Sub cbxDataProfile_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxDataProfile.SelectedIndexChanged
        If Not baseForm.isLoad Then
            Dim dataSet As New DataSet

            ' Muat data dari file XML
            dataSet.ReadXml(ChromeProfile.dataUser)

            loadUserDataXML(dataSet)

            changeButtonStatusLogin(True)
        End If
    End Sub

    Private Sub cbxDataProfile_TextChanged(sender As Object, e As EventArgs) Handles cbxDataProfile.TextChanged
        gridUserFacebook.Rows.Clear()
    End Sub

    Private Sub gridUserFacebook_KeyDown(sender As Object, e As KeyEventArgs) Handles gridUserFacebook.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Delete Then

            If gridUserFacebook.SelectedRows.Count > 0 Then
                Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin menghapus baris ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                For Each userDel In gridUserFacebook.SelectedRows
                    ' Ambil UserID dari baris yang akan dihapus
                    Dim userIdToDelete As String = userDel.Cells("UserIdCol").Value.ToString()

                    ' Pastikan ada baris yang dipilih dalam DataGridView

                    If result = Windows.Forms.DialogResult.No Then
                        Return
                    End If

                    deleteUserData(userIdToDelete)
                Next

            Else
                MessageBox.Show("Pilih baris yang ingin dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub deleteUserData(userIdToDelete As String)

        ' Membaca data dari UserData.xml ke dalam DataSet
        Dim dataSet As New DataSet()
        dataSet.ReadXml(ChromeProfile.dataUser)

        ' Mendapatkan tabel "UserData" dari DataSet
        Dim userTable As DataTable = dataSet.Tables("UserData")

        ' Mencari userID yang akan dihapus
        Dim rowToDelete As DataRow = Nothing
        For Each row As DataRow In userTable.Rows
            If row("UserId").ToString() = userIdToDelete Then
                rowToDelete = row
                Exit For
            End If
        Next

        If rowToDelete IsNot Nothing Then
            ' Hapus row dari tabel
            userTable.Rows.Remove(rowToDelete)

            ' Simpan perubahan ke UserData.xml
            dataSet.WriteXml(ChromeProfile.dataUser)

            ' Hapus baris dari DataGridView
            gridUserFacebook.Rows.Remove(gridUserFacebook.SelectedRows(0))

            If Directory.Exists(ChromeProfile.MyDoumentProfile & "\" & userIdToDelete) Then
                Directory.Delete(ChromeProfile.MyDoumentProfile & "\" & userIdToDelete, True)
            End If
        Else
            MessageBox.Show(
            String.Concat("UserID ", userIdToDelete, " tidak ditemukan dalam data UserData.xml."),
            "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If Not doubleClickOccurred Then
            doubleClickOccurred = True

            Try
                runAutoLogin(chkRunAllProfile.Checked)
            Catch ex As Exception
                MessageBox.Show(ex.Message,
                               "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try

            doubleClickOccurred = False
        End If
    End Sub

    Private Sub btnLoginManual_Click(sender As Object, e As EventArgs) Handles btnLoginManual.Click
        If Not doubleClickOccurred Then
            doubleClickOccurred = True

            Try
                runManualLogin()
            Catch ex As Exception
                MessageBox.Show(ex.Message,
                               "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try

            doubleClickOccurred = False
        End If
    End Sub

    Private Sub runAutoLogin(isRunAll As Boolean)
        If gridUserFacebook.Rows.Count > 0 Then
            'stopEvent.Reset()
            Dim rowViews As New List(Of DataRowView)
            For Each row As DataGridViewRow In gridUserFacebook.Rows
                If Not row.IsNewRow Then

                    'Dim profileName = row.Cells(0).Value
                    If File.Exists(ChromeProfile.dataUser) Then
                        Dim dataSet As New DataSet
                        dataSet.ReadXml(ChromeProfile.dataUser)
                        ' Menerapkan filter ke DataView
                        Dim dataView As New DataView(dataSet.Tables("UserData"))
                        dataView.RowFilter = String.Concat("UserId = '", row.Cells(0).Value, "'")

                        ' Menyalin data dari DataView ke DataTable
                        'Membuat Thread baru

                        rowViews.Add(dataView(0))
                    End If

                End If
            Next

            Dim profileName = cbxDataProfile.Text
            Dim existingProfile = baseForm.Profiles.Find(Function(p) p.ProfileName = profileName And p.IsOnProcess = True)
            If existingProfile Is Nothing Then

                Dim newThread As Thread = New Thread(Sub() LoginToFacebookWork(rowViews, profileName, isRunAll))

                'Menambahkannya ke daftar thread
                baseForm.threads.Add(newThread)

                ' Memulai thread
                newThread.Start()
            Else
                MessageBox.Show(String.Concat("Harapa tunggu beberapa saat sampai proses selesai untuk akun ", profileName),
                                       "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Else
                MessageBox.Show("Tidak ada Data User yang akan login ",
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub runManualLogin()
        If gridUserFacebook.SelectedRows.Count > 0 Then
            For Each row As DataGridViewRow In gridUserFacebook.SelectedRows
                If Not row.IsNewRow Then
                    Dim dataTable As DataTable = Nothing
                    Dim accountCode = row.Cells(0).Value
                    Dim profileName = cbxDataProfile.Text
                    If File.Exists(ChromeProfile.dataUser) Then
                        Dim dataSet As New DataSet
                        dataSet.ReadXml(ChromeProfile.dataUser)
                        ' Menerapkan filter ke DataView
                        Dim dataView As New DataView(dataSet.Tables("UserData"))
                        dataView.RowFilter = String.Concat("UserId = '", row.Cells(0).Value, "'")


                        dataTable = dataView.Table.Clone()

                        ' Menyalin data dari DataView ke DataTable
                        For Each rowView As DataRowView In dataView
                            ' Menambahkan baris baru ke DataTable dan menyalin nilai kolom dari DataView
                            Dim DataRow As DataRow = dataTable.NewRow()
                            DataRow.ItemArray = rowView.Row.ItemArray
                            'If DataRow(3) = 1 Then
                            '    Continue For
                            'End If
                            Dim existingProfile = baseForm.Profiles.Find(Function(p) p.ProfileName = profileName And p.IsOnProcess = True)
                            If existingProfile Is Nothing Then
                                existingProfile = baseForm.runChromeDriver(profileName, accountCode)

                                If existingProfile IsNot Nothing Then
                                    existingProfile.IsOnProcess = True
                                End If

                                ' Membuat thread baru
                                Dim newThread As Thread = New Thread(Sub() LoginToFacebookManual(profileName, indexLogin, existingProfile))

                                ' Menambahkannya ke daftar thread
                                baseForm.threads.Add(newThread)

                                ' Memulai thread
                                newThread.Start()
                                indexLogin += 1

                                Exit For
                            Else
                                MessageBox.Show(String.Concat("Harapa tunggu beberapa saat sampai proses selesai untuk akun ", profileName),
                                       "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            End If

                        Next
                    End If
                End If
            Next
        Else
            MessageBox.Show("Pilih User Untuk Login ",
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub LoginToFacebookWork(dataView As List(Of DataRowView), profileName As String, isRunAll As Boolean)
        Dim countdownEvent As CountdownEvent = New CountdownEvent(dataView.Count)
        Dim workerTasks As List(Of Task) = New List(Of Task)()
        Dim datarows As New List(Of DataRow)
        For Each rowView As DataRowView In dataView
            Dim dataTable As DataTable = Nothing
            dataTable = rowView.DataView.Table.Clone()
            ' Menambahkan baris baru ke DataTable dan menyalin nilai kolom dari DataView
            Dim DataRow As DataRow = dataTable.NewRow()
            DataRow.ItemArray = rowView.Row.ItemArray
            'If DataRow(3) = 1 Then
            '    Continue For
            'End If
            Dim existingProfile = baseForm.Profiles.Find(Function(p) p.AccountCode = DataRow(0) And p.IsOnProcess = True)
            If existingProfile Is Nothing Then
                existingProfile = baseForm.runChromeDriver(profileName, DataRow(0))

                If existingProfile IsNot Nothing Then
                    existingProfile.IsOnProcess = True
                End If

                If isRunAll Then
                    Dim workerTask As Task = Task.Run(Sub() LoginToFacebook(DataRow, existingProfile, countdownEvent, datarows))
                    Thread.Sleep(1000)
                    ' Menyimpan referensi task ke dalam list untuk keperluan pengelolaan
                    workerTasks.Add(workerTask)
                Else
                    LoginToFacebook(DataRow, existingProfile, countdownEvent, datarows)
                End If

                'Exit For
            Else
                MessageBox.Show(String.Concat("Harapa tunggu beberapa saat sampai proses selesai untuk akun ", DataRow(0)),
                       "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Next

        countdownEvent.Wait()

        If File.Exists(ChromeProfile.dataUser) Then
            Dim dataSet As New DataSet
            dataSet.ReadXml(ChromeProfile.dataUser)
            Dim userTable As DataTable = dataSet.Tables("UserData")
            For Each dataProfile In datarows
                ' Periksa apakah targetUserId sudah ada dalam data
                Dim userId As String = dataProfile(0)
                Dim isLogin As Integer = dataProfile(3)
                For Each rowtable As DataRow In userTable.Rows
                    If rowtable("UserId").ToString() = userId Then
                        rowtable("IsLogin") = isLogin
                        Exit For
                    End If
                Next
            Next
            dataSet.WriteXml(ChromeProfile.dataUser)

            MessageBox.Show("Auto Login facebook selesai",
                      "informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub LoginToFacebook(dataProfile As DataRow, existingProfile As ChromeProfile, ByRef countdownEvent As CountdownEvent, ByRef datarows As List(Of DataRow))
        ' Logika untuk menginject elemen ke Chrome
        Try
            Dim driver As UndetectedChromeDriver = Nothing

            Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing
            Dim statusPost As New StatusPost()

            If existingProfile IsNot Nothing Then
                driver = existingProfile.Driver
            End If

            Dim userId As String = dataProfile(0)
            Dim password As String = dataProfile(1)
            Dim verifyCode As String = dataProfile(2)
            Dim twoFACode As String = String.Empty

            baseForm.sleep(1)
            'Dim waitforelement As WebDriverWait = New WebDriverWait(driver, TimeSpan.FromSeconds(100))

            '// MENDETEK KODINGAN JIKA TIDAK ADA MP NYA 
            Dim random As New Random()
            Dim waktuRandom As Integer = random.Next(5, 7) * 1000 'dalam milidetik
            Dim jedarandom As Integer = random.Next(2, 4) * 1000 'dalam milidetik

            Try
                driver.GoToUrl("https://www.facebook.com/")

                'driver.Navigate.Refresh()
                Thread.Sleep(jedarandom * 2)

                Dim isLogin = True
                Try
                    elementList =
                               driver.FindElements(By.XPath("//form[contains(@action, '/logout.php')]"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        Thread.Sleep(jedarandom)
                        Dim elementList1 As IReadOnlyCollection(Of IWebElement) =
                                  driver.FindElements(By.XPath("//div[contains(@aria-label, 'Messenger')][contains(@role, 'button')]"))

                        ' Cek apakah elemen ada atau tidak ada
                        If elementList1.Count = 0 Then
                            dataProfile(3) = 2
                        Else
                            dataProfile(3) = 1
                        End If
                    Else
                        dataProfile(3) = 0
                    End If
                Catch ex As Exception
                    dataProfile(3) = 3
                End Try

                WaitHandle.WaitAny({suspendEvent})

                ' Cek apakah elemen ada atau tidak ada
                If dataProfile(3) Is Nothing Or dataProfile(3) = 0 Or dataProfile(3) = 3 Then
                    If Not String.IsNullOrEmpty(verifyCode) Then
                        driver.GoToUrl("https://2fa.live/")

                        Thread.Sleep(jedarandom)

                        elementList =
                                driver.FindElements(By.Id("listToken"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(0).SendKeys(verifyCode)
                        End If

                        Thread.Sleep(jedarandom)

                        elementList =
                                driver.FindElements(By.Id("submit"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(0).Click()
                        End If

                        Thread.Sleep(jedarandom)

                        elementList =
                                driver.FindElements(By.Id("output"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            Dim valAttr As String = elementList(0).GetAttribute("value")
                            twoFACode = valAttr.Substring(Math.Max(0, valAttr.Length - 6))
                        End If

                        Thread.Sleep(jedarandom)
                    End If
                    driver.GoToUrl("https://www.facebook.com/")
                    WaitHandle.WaitAny({suspendEvent})
                    Thread.Sleep(waktuRandom * 2)

                    statusPost = existingProfile.LoginProcess(userId, password, jedarandom)

                    If Not statusPost.Status Then
                        If statusPost.Message <> "Belum Verifikasi" Then
                            Throw New Exception(statusPost.Message)
                        End If
                    End If
                    WaitHandle.WaitAny({suspendEvent})
                    Thread.Sleep(jedarandom)

                    elementList =
                            driver.FindElements(By.XPath("//input[@name='approvals_code']"))
                    ' Cek apakah elemen ada atau tidak ada
                    If elementList.Count > 0 Then
                        If Not String.IsNullOrEmpty(twoFACode) Then
                            elementList(0).SendKeys(twoFACode)

                            Thread.Sleep(jedarandom)

                            elementList =
        driver.FindElements(By.XPath("//button[@type='submit']"))
                            ' Cek apakah elemen ada atau tidak ada
                            If elementList.Count > 0 Then
                                elementList(0).Click()

                                Thread.Sleep(jedarandom)

                                elementList =
        driver.FindElements(By.XPath("//button[@type='submit']"))
                                ' Cek apakah elemen ada atau tidak ada
                                If elementList.Count > 0 Then
                                    elementList(0).Click()
                                End If
                            End If
                        End If
                    End If
                    WaitHandle.WaitAny({suspendEvent})
                    Thread.Sleep(jedarandom)

                    Dim elementList1 As IReadOnlyCollection(Of IWebElement) =
                                  driver.FindElements(By.XPath("//div[contains(@aria-label, 'Messenger')][contains(@role, 'button')]"))

                    ' Cek apakah elemen ada atau tidak ada
                    If elementList1.Count = 0 Then
                        dataProfile(3) = 2
                    Else
                        elementList =
                            driver.FindElements(By.XPath("//div[contains(@aria-label, 'Ikuti Tur Privasi')]"))
                        ' Cek apakah elemen ada atau tidak ada
                        If elementList.Count > 0 Then
                            elementList(0).Click()

                            Thread.Sleep(jedarandom)

                            elementList =
                                driver.FindElements(By.XPath("//div[contains(@aria-label, 'Berikutnya')]"))
                            ' Cek apakah elemen ada atau tidak ada
                            If elementList.Count > 0 Then
                                elementList(0).Click()

                                Thread.Sleep(jedarandom)

                                elementList =
                                    driver.FindElements(By.XPath("//div[contains(@aria-label, 'Berikutnya')]"))
                                ' Cek apakah elemen ada atau tidak ada
                                If elementList.Count > 1 Then
                                    elementList(1).Click()

                                    Thread.Sleep(jedarandom)

                                    elementList =
                                        driver.FindElements(By.XPath("//div[contains(@aria-label, 'Selesai')]"))
                                    ' Cek apakah elemen ada atau tidak ada
                                    If elementList.Count > 0 Then
                                        elementList(0).Click()
                                    End If
                                End If
                            End If
                        End If
                        driver.GoToUrl("https://www.facebook.com/settings/?tab=your_facebook_information")
                        Thread.Sleep(jedarandom * 2)
                        Try
                            elementList =
                           driver.FindElements(By.XPath("////*[@aria-label='OK']"))

                            If elementList.Count > 0 Then
                                elementList(0).Click()
                                Thread.Sleep(jedarandom)
                            End If
                        Catch ex As Exception
                        End Try
                        Thread.Sleep(jedarandom * 2)
                        WaitHandle.WaitAny({suspendEvent})
                        driver.GoToUrl("https://www.facebook.com/settings?tab=language")

                        Thread.Sleep(jedarandom * 2)

                        elementList =
                            driver.FindElements(By.XPath("//div[contains(@role, 'main')]/div/div/div/div/div[2]//div[@class = 'xdppsyt x1l90r2v']/div[2]/div/div[3]/div[contains(@aria-label, 'Edit')][@role = 'button']"))

                        If elementList.Count > 0 Then
                            elementList(0).Click()
                            Thread.Sleep(jedarandom)
                        End If

                        elementList =
                            driver.FindElements(By.XPath("//div[@aria-haspopup ='listbox'][@role ='combobox']"))

                        If elementList.Count > 0 Then
                            elementList(0).Click()
                            Thread.Sleep(jedarandom)
                        End If
                        WaitHandle.WaitAny({suspendEvent})
                        elementList =
                            driver.FindElements(By.XPath("//div[@role ='option']"))

                        If elementList.Count > 0 Then
                            For Each element In elementList
                                Dim eleOption As IWebElement = element.FindElement(By.XPath("//span[text() ='Bahasa Indonesia']"))
                                If eleOption IsNot Nothing Then
                                    element.Click()
                                    Thread.Sleep(jedarandom)
                                    Exit For
                                End If
                            Next
                        End If

                        Try
                            elementList =
                                driver.FindElements(By.XPath("//div/div[2]/div[@aria-label ='Simpan perubahan'][@role ='button']"))

                            If elementList.Count > 0 AndAlso elementList(0).Enabled Then
                                elementList(0).Click()
                                Thread.Sleep(jedarandom)
                            End If
                        Catch ex As Exception
                        End Try
                        WaitHandle.WaitAny({suspendEvent})
                        Try
                            elementList =
                                driver.FindElements(By.XPath("//div/div[2]/div[@aria-label ='Save changes'][@role ='button']"))
                            If elementList.Count > 0 AndAlso elementList(0).Enabled Then
                                elementList(0).Click()
                                Thread.Sleep(jedarandom)
                            End If

                        Catch ex As Exception
                        End Try

                        dataProfile(3) = 1
                    End If
                    Thread.Sleep(jedarandom * 3)
                End If
            Catch ex As Exception
                Throw ex
            End Try

        Catch ex As Exception
            dataProfile(3) = 3
        Finally
        End Try
        'existingProfile.updateData(dataProfile)
        datarows.Add(dataProfile)

        If existingProfile IsNot Nothing Then
            baseForm.Invoke(Sub()
                                existingProfile.IsOnProcess = False
                                If existingProfile.Driver IsNot Nothing Then
                                    existingProfile.Driver.Quit()
                                End If

                                baseForm.Profiles.Remove(existingProfile)
                            End Sub)

        End If

        countdownEvent.Signal()
    End Sub



    Private Sub gridUserFacebook_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles gridUserFacebook.CellFormatting
        ' Pastikan kita hanya memproses kolom "status"
        If e.ColumnIndex = gridUserFacebook.Columns("StatusStrCol").Index AndAlso e.RowIndex >= 0 AndAlso Not gridUserFacebook.Rows(e.RowIndex).IsNewRow Then
            Dim cellValue As String = gridUserFacebook(e.ColumnIndex, e.RowIndex).Value.ToString()

            ' Tentukan kondisi, misalnya jika status adalah "berhasil"
            If cellValue = "BERHASIL" Then
                gridUserFacebook.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Green
                gridUserFacebook.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            ElseIf cellValue = "DIBANNED" Then
                gridUserFacebook.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Red
                gridUserFacebook.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            ElseIf cellValue = "BELUM DICEK" Then
                gridUserFacebook.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Orange
                gridUserFacebook.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            ElseIf cellValue = "GAGAL" Then
                gridUserFacebook.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Brown
                gridUserFacebook.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            End If
        End If
    End Sub

    Private Sub btnRefreshData_Click(sender As Object, e As EventArgs) Handles btnRefreshData.Click
        RefreshData()
    End Sub

    Private Sub RefreshData()
        Dim dataSet As New DataSet

        ' Muat data dari file XML
        dataSet.ReadXml(ChromeProfile.dataUser)

        loadUserDataXML(dataSet)

        changeButtonStatusLogin(True)
    End Sub


    Private Sub LoginToFacebookManual(profileName As String, workerIndex As Integer,
                            existingProfile As ChromeProfile)
        ' Logika untuk menginject elemen ke Chrome
        Try
            Dim driver As UndetectedChromeDriver = Nothing

            Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing
            Dim statusPost As New StatusPost()

            If existingProfile IsNot Nothing Then
                driver = existingProfile.Driver
            End If

            If existingProfile IsNot Nothing Then
                baseForm.Invoke(Sub() existingProfile.IsOnProcess = False)
            End If

            '// MENDETEK KODINGAN JIKA TIDAK ADA MP NYA 
            Dim random As New Random()
            Dim waktuRandom As Integer = random.Next(5, 7) * 1000 'dalam milidetik
            Dim jedarandom As Integer = random.Next(2, 4) * 1000 'dalam milidetik

            Try
                driver.GoToUrl("https://www.facebook.com/")

            Catch ex As Exception
            End Try

            'driver.Quit()
        Catch
            If existingProfile IsNot Nothing Then
                baseForm.Invoke(Sub() existingProfile.IsOnProcess = False)
            End If
        Finally
            If existingProfile IsNot Nothing Then
                baseForm.Invoke(Sub() existingProfile.IsOnProcess = False)
            End If
        End Try
    End Sub

    Private Sub gridUserFacebook_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles gridUserFacebook.RowPostPaint
        ' Menggambar nomor urut di setiap baris
        Using b As New SolidBrush(gridUserFacebook.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4)
        End Using

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

            RefreshData()
        Else
            MessageBox.Show("tidak ada Browser yang terbuka", "informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnProductPost_Click(sender As Object, e As EventArgs) Handles btnProductPost.Click
        If Not doubleClickOccurred Then
            doubleClickOccurred = True

            If gridUserFacebook.SelectedRows.Count = 0 Then
                MessageBox.Show("harap Pilih Salahsatu akun", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                doubleClickOccurred = False
                Return
            End If
            Try
                runProductPost()
            Catch ex As Exception
                MessageBox.Show(ex.Message,
                               "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try

            doubleClickOccurred = False
        End If
    End Sub

    Private Sub runProductPost()
        If gridUserFacebook.SelectedRows.Count > 0 Then
            For Each row As DataGridViewRow In gridUserFacebook.SelectedRows
                If Not row.IsNewRow Then
                    Dim dataTable As DataTable = Nothing
                    Dim accountCode = row.Cells(0).Value
                    Dim profileName = cbxDataProfile.Text
                    If File.Exists(ChromeProfile.dataUser) Then
                        Dim dataSet As New DataSet
                        dataSet.ReadXml(ChromeProfile.dataUser)
                        ' Menerapkan filter ke DataView
                        Dim dataView As New DataView(dataSet.Tables("UserData"))
                        dataView.RowFilter = String.Concat("UserId = '", row.Cells(0).Value, "'")


                        dataTable = dataView.Table.Clone()

                        ' Menyalin data dari DataView ke DataTable
                        For Each rowView As DataRowView In dataView
                            ' Menambahkan baris baru ke DataTable dan menyalin nilai kolom dari DataView
                            Dim DataRow As DataRow = dataTable.NewRow()
                            DataRow.ItemArray = rowView.Row.ItemArray
                            'If DataRow(3) = 1 Then
                            '    Continue For
                            'End If
                            Dim existingProfile = baseForm.Profiles.Find(Function(p) p.ProfileName = profileName And p.IsOnProcess = True)
                            If existingProfile Is Nothing Then
                                existingProfile = baseForm.runChromeDriver(profileName, accountCode)

                                If existingProfile IsNot Nothing Then
                                    existingProfile.IsOnProcess = True
                                End If

                                ' Membuat thread baru
                                Dim newThread As Thread = New Thread(Sub() FacebookProductPost(DataRow, profileName, indexLogin, existingProfile))

                                ' Menambahkannya ke daftar thread
                                baseForm.threads.Add(newThread)

                                ' Memulai thread
                                newThread.Start()
                                indexLogin += 1

                                Exit For
                            Else
                                MessageBox.Show(String.Concat("Harapa tunggu beberapa saat sampai proses selesai untuk akun ", profileName),
                                       "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            End If

                        Next
                    End If
                End If
            Next
        Else
            MessageBox.Show("User Belum dipilih ",
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub FacebookProductPost(dataProfile As DataRow, profileName As String, workerIndex As Integer,
                            existingProfile As ChromeProfile)
        ' Logika untuk menginject elemen ke Chrome
        Try
            Dim driver As UndetectedChromeDriver = Nothing

            Dim elementList As IReadOnlyCollection(Of IWebElement) = Nothing
            Dim statusPost As New StatusPost()

            If existingProfile IsNot Nothing Then
                driver = existingProfile.Driver
            End If

            If existingProfile IsNot Nothing Then
                baseForm.Invoke(Sub() existingProfile.IsOnProcess = False)
            End If

            '// MENDETEK KODINGAN JIKA TIDAK ADA MP NYA 
            Dim random As New Random()
            Dim waktuRandom As Integer = random.Next(5, 7) * 1000 'dalam milidetik
            Dim jedarandom As Integer = random.Next(2, 4) * 1000 'dalam milidetik

            Try
                driver.GoToUrl("https://www.facebook.com/")

                'driver.Navigate.Refresh()
                Thread.Sleep(jedarandom * 2)

                Dim userId As String = dataProfile(0)
                Dim password As String = dataProfile(1)
                Dim verifyCode As String = dataProfile(2)
                Dim isLogin As Integer = dataProfile(3)
                Dim twoFACode As String = String.Empty

                baseForm.sleep(1)
                'Dim waitforelement As WebDriverWait = New WebDriverWait(driver, TimeSpan.FromSeconds(100))

                Try
                    driver.GoToUrl("https://www.facebook.com/")

                    Try
                        Thread.Sleep(jedarandom * 2)
                        Thread.Sleep(jedarandom)
                        statusPost = existingProfile.CheckLoginProcess(dataProfile)


                        driver.GoToUrl("https://www.facebook.com/marketplace/you/selling")

                        'MessageBox.Show("Scan User Login Berhasil", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Catch ex As Exception
                        driver.Quit()
                        dataProfile(3) = 3
                        Throw ex
                    End Try

                Catch ex As Exception
                    'MessageBox.Show("Akun " & userId & " DIBANNED, Sistem akan melanjutkan login ke akun berikutnya!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    dataProfile(3) = 3
                End Try

            Catch ex As Exception
            End Try

            'driver.Quit()
        Catch
            If existingProfile IsNot Nothing Then
                baseForm.Invoke(Sub() existingProfile.IsOnProcess = False)
            End If
        Finally
            If existingProfile IsNot Nothing Then
                baseForm.Invoke(Sub() existingProfile.IsOnProcess = False)
            End If
        End Try
    End Sub

    Dim isPause = False

    Private Sub SuspendProcess(ByVal process As Process)
        For Each t As ProcessThread In process.Threads
            Dim th As IntPtr
            'th = OpenThread(ThreadAccess)
        Next
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnPause.Click
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

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim webAddress As String = "https://drive.google.com/file/d/1N9yAH6YRYb2a2KVibiOJx4gAVC05qWw1/view?usp=sharing"
        Process.Start(webAddress)
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim webAddress As String = "https://youtu.be/bKd_JFcn4Lk"
        Process.Start(webAddress)
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Dim webAddress As String = "https://youtu.be/1AVnA0BW7_A"
        Process.Start(webAddress)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        For Each profile In baseForm.Profiles
            If profile.IsOnProcess Then
                MessageBox.Show("Tidak Bisa menutup Halaman, Masih terdapat proses yang belum selesai", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next
        Dim tabName = "LoginFB"
        baseForm.CloseAndRemoveTabPage(tabName)

    End Sub
End Class
