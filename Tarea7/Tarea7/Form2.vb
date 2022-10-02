Imports System.Data.SqlClient

Public Class Form2
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        usr_id = ""
        usr_nombre = ""
        usr_username = ""
        usr_password = ""
        usr_tipo = ""
        usr_foto = ""

        Form1.Visible = True
        Me.Visible = False
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs)
        Dim rutaimagen As String = "C:\Users\Jorge\Desktop\IM\car.png"
        PictureBox1.Image = Image.FromFile(rutaimagen)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim passwordIngresada = TextBox1.Text
        Dim passEncriptada = Encriptar(passwordIngresada)

        If (passEncriptada = usr_password) Then
            MsgBox("Login exitoso")
            Form4.Visible = True
            Me.Visible = False
            Form4.Label1.Text = usr_username
        Else
            MsgBox("Contraseña Incorrecta")
        End If
        Form1.TextBox1.Text = ""



    End Sub
End Class