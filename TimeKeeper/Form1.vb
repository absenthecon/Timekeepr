Imports System.Net.Mail

Public Class Form1
    Dim WithEvents aTimer As New System.Windows.Forms.Timer 'could have been done in the Designer

    Private Sub aTimer_Tick(ByVal sender As Object,
                            ByVal e As System.EventArgs) Handles aTimer.Tick
        Label1.Text = DateTime.Now.ToString("dd MMMM, yyyy h:mm:ss tt")
    End Sub

    Private Sub Form1_Shown(ByVal sender As Object,
                            ByVal e As System.EventArgs) Handles Me.Shown
        aTimer.Interval = 250
        aTimer.Start()
        TextBox1.Select()
        If Date.Now.Hour > 12 Then
            Button1.Text = "CLOCK OUT"
            RadioButton1.Text = "CLOCK IN"
        Else
            Button1.Text = "CLOCK IN"
            RadioButton1.Text = "CLOCK OUT"
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click



        Try
            Dim Smtp_Server As New SmtpClient
            Dim e_mail As New MailMessage()
            Smtp_Server.UseDefaultCredentials = False
            Smtp_Server.Credentials = New Net.NetworkCredential("timekeepr@test.internal", "password")
            Smtp_Server.Port = 25
            Smtp_Server.EnableSsl = False
            Smtp_Server.Host = "127.0.0.1"

            e_mail = New MailMessage()
            e_mail.From = New MailAddress("TimeKeepr@test.internal")
            e_mail.To.Add("timekeepr@test.internal")
            If Button1.Text = "CLOCK IN" Then
                e_mail.Subject = "User " & TextBox1.Text & " clocked in"
                e_mail.IsBodyHtml = False
                e_mail.Body = TextBox1.Text & " > " & DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss") & " > " & TextBox2.Text & " > " & RichTextBox1.Text
                Smtp_Server.Send(e_mail)
                MsgBox("Clocked in")
            Else
                e_mail.Subject = "User " & TextBox1.Text & " clocked out"
                e_mail.IsBodyHtml = False
                e_mail.Body = TextBox1.Text & " > " & DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss") & " > " & TextBox2.Text & " > " & RichTextBox1.Text
                Smtp_Server.Send(e_mail)
                MsgBox("Clocked out")
            End If
        Catch error_t As Exception
            MsgBox(error_t.ToString)
        End Try

        TextBox1.Text = ""
        TextBox2.Text = ""
        RichTextBox1.Text = ""
        RadioButton1.Checked = False
        Button1.Text = "CLOCK IN"
        TextBox1.Select()
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If Button1.Text = "CLOCK OUT" Then
            Button1.Text = "CLOCK IN"
        Else
            Button1.Text = "CLOCK OUT"
        End If
    End Sub
End Class
