Public Class HomeControl
    Dim baseForm As FormBase = DirectCast(Parent, FormBase)
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim text As String = Label2.Text ' Gantilabel1 dengan nama kontrol Anda

        ' Pindahkan karakter pertama ke akhir
        text = text.Substring(1) & text(0)

        ' Update teks di label atau textbox
        Label2.Text = text ' Gantilabel1 dengan nama kontrol Anda

        ' Pindahkan posisi horizontal teks sedikit ke kiri
        Label2.Left -= 2 ' Ganti 2 dengan nilai yang sesuai untuk kecepatan animasi yang Anda inginkan

        ' Jika teks melewati batas kiri form, reset posisi
        If Label2.Right < 0 Then
            Label2.Left = Me.ClientSize.Width
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Label5.Text = DateTime.Now.ToString("HH:mm:ss")
    End Sub

    Private Sub HomeControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        baseForm = DirectCast(Parent, FormBase)
        Timer1.Interval = 100 ' Interval waktu dalam milidetik
        Timer1.Enabled = True
        '// ICON UNTUK WAKTU 
        Timer2.Interval = 1000 ' Interval waktu dalam milidetik (1 detik)
        Timer2.Enabled = True
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim tabName = "LoginFB"
        Dim tabIndex = baseForm.findTab(tabName)
        If tabIndex > -1 Then
            baseForm.TabControl.SelectedIndex = tabIndex
        Else
            Dim loginControl As New LoginFacebook()
            baseForm.createNewTab(tabName, loginControl)
        End If
    End Sub

    Private Sub AutoPostProduk_Click(sender As Object, e As EventArgs) Handles AutoPostProduk.Click
        Dim tabName = "PostGeneral"
        Dim tabIndex = baseForm.findTab(tabName)
        If tabIndex > -1 Then
            baseForm.TabControl.SelectedIndex = tabIndex
        Else
            Dim postControl As New PostFBGeneralPostControl(FiturPostFBEnum.General)
            baseForm.createNewTab(tabName, postControl)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim tabName = "PostFBMotor"
        Dim tabIndex = baseForm.findTab(tabName)
        If tabIndex > -1 Then
            baseForm.TabControl.SelectedIndex = tabIndex
        Else
            Dim postFBMotorControl As New PostFBMotorControl(FiturPostFBEnum.General)
            baseForm.createNewTab(tabName, postFBMotorControl)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim tabName = "PostFBMobil"
        Dim tabIndex = baseForm.findTab(tabName)
        If tabIndex > -1 Then
            baseForm.TabControl.SelectedIndex = tabIndex
        Else
            Dim postFBMobilControl As New PostFBMobilControl(FiturPostFBEnum.General)
            baseForm.createNewTab(tabName, postFBMobilControl)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim tabName = "PostFBProperti"
        Dim tabIndex = baseForm.findTab(tabName)
        If tabIndex > -1 Then
            baseForm.TabControl.SelectedIndex = tabIndex
        Else
            Dim postFBPropertiControl As New PostFBPropertiControl(FiturPostFBEnum.General)
            baseForm.createNewTab(tabName, postFBPropertiControl)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim tabName = "PostFBLite"
        Dim tabIndex = baseForm.findTab(tabName)
        If tabIndex > -1 Then
            baseForm.TabControl.SelectedIndex = tabIndex
        Else
            Dim postFBLiteControl As New PostFBLiteControl()
            baseForm.createNewTab(tabName, postFBLiteControl)
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim tabName = "PostGroupFB"
        Dim tabIndex = baseForm.findTab(tabName)
        If tabIndex > -1 Then
            baseForm.TabControl.SelectedIndex = tabIndex
        Else
            Dim postGroupFBControl As New PostGroupFBControl()
            baseForm.createNewTab(tabName, postGroupFBControl)
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim tabName = "SenderMessage"
        Dim tabIndex = baseForm.findTab(tabName)
        If tabIndex > -1 Then
            baseForm.TabControl.SelectedIndex = tabIndex
        Else
            Dim control As New MessageSenderControl()
            baseForm.createNewTab(tabName, control)
        End If
    End Sub
End Class
