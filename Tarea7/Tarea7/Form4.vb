Imports System.Data.SqlClient

Public Class Form4
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        usr_id = ""
        usr_nombre = ""
        usr_username = ""
        usr_password = ""
        usr_tipo = ""
        usr_foto = ""

        Form1.TextBox1.Text = ""
        Form1.Visible = True
        Me.Visible = False

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form9.ActualizarCanciones()

        Form9.Visible = True
        Me.Visible = False

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form7.ActualizarLista()

        Form7.Visible = True
        Me.Visible = False

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Form8.ActualizarArtistas()
        Form8.ActualizarCanciones()
        Form8.Visible = True
        Me.Visible = False

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Form5.ActualizarPlaylists()
        Form5.Visible = True
        Me.Visible = False
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Form10.Visible = True
        Me.Visible = False

    End Sub
End Class