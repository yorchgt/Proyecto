Imports System.Data.SqlClient

Public Class Form1
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form3.Visible = True
        Me.Visible = False

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim MiCmd As SqlCommand
        Dim MiLector As SqlDataReader
        Dim nombreUsuario As String = TextBox1.Text

        Try
            MiCmd = conexion.CreateCommand
            Dim consulta As String = "SELECT * FROM usuario WHERE username ='" + nombreUsuario + "'"
            MiCmd.CommandText = consulta

            conexion.Open()
            MiLector = MiCmd.ExecuteReader()

            Do While MiLector.Read()
                'reuslts = reuslts + MiLector.GetInt32(0).ToString() + MiLector.GetString(1) +MiLector.GetInt32(2).ToString()+ MiLector.GetString(3) + MiLector.GetString(4) + MiLector.GetString(5) + MiLector.GetString(6) + vbLf
                '0-id;1=nombre;2=edad;3=username;4=password;5=tipo;6=foto
                usr_id = MiLector.GetInt32(0).ToString()
                usr_nombre = MiLector.GetString(1)
                usr_edad = MiLector.GetInt32(2).ToString()
                usr_username = MiLector.GetString(3)
                usr_password = MiLector.GetString(4)
                usr_tipo = MiLector.GetString(5)
                usr_foto = MiLector.GetString(6)
            Loop
            'Console.WriteLine(usr_nombre)
            If Not (usr_username = "") Then
                'MsgBox("bienvenido " + usr_username)
                Form2.PictureBox1.Image = Image.FromFile(usr_foto)
                Form2.Label2.Text = usr_username
                Form2.Visible = True
                Me.Visible = False
            Else
                MsgBox("Usuario no encontrado")
            End If
            conexion.Close()
        Catch ex As Exception

        End Try





    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            conexion.Open()
            MsgBox("conexion con la DB exitosa")
            Button2.Enabled = True
            Button3.Enabled = True
            Label1.Text = "Conectado"
            conexion.Close()
        Catch ex As Exception
            MsgBox("Error al conectar con la DB")
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If MsgBox("Desea Salir?", vbQuestion + vbYesNo, "Pregunta") = vbYes Then
            End
        End If
    End Sub
End Class
