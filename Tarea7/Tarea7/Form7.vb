Imports System.Data.SqlClient
Public Class Form7
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim nombreArtista As String = TextBox1.Text
        Dim nacionArtista As String = TextBox2.Text
        If (nombreArtista = "" Or nacionArtista = "") Then
            MsgBox("Introduzca todos los campos")
        Else
            'Abrir la conexion
            conexion.Open()

            Try
                Dim consulta As String = "INSERT INTO artista(nombre,nacionalidad) VALUES ('" + nombreArtista + "','" + nacionArtista + "')"
                Dim comando As SqlCommand
                comando = New SqlCommand(consulta, conexion)
                comando.ExecuteNonQuery()
                MessageBox.Show("Los datos del artista guardaron correctamente")
            Catch ex As Exception
                MsgBox("Error al ingresar datos a la DB")
            End Try

            'Cerrar la conexion
            conexion.Close()
        End If

        'Metodo para actualizar la lista
        ActualizarLista()
    End Sub

    Sub ActualizarLista()
        ListBox1.Items.Clear()
        Dim MiCmd As SqlCommand
        Dim MiLector As SqlDataReader

        conexion.Open()

        Try
            MiCmd = conexion.CreateCommand
            Dim consulta As String = "SELECT * FROM artista"
            MiCmd.CommandText = consulta
            MiLector = MiCmd.ExecuteReader()

            Do While MiLector.Read()
                '0-id;  1= nombre;  2= nacionalidad
                Dim nombreArtista As String = MiLector.GetString(1)
                ListBox1.Items.Add(nombreArtista)
            Loop

        Catch ex As Exception
            MsgBox("Error al consultar artistas")
        End Try

        conexion.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form4.Visible = True
        Me.Visible = False

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim nombreArtista As String = ListBox1.SelectedItem.ToString()
        If MsgBox("Desea Eliminar el Artista?", vbQuestion + vbYesNo, "Pregunta") = vbYes Then


            conexion.Open()
            Try
                Dim consulta As String = "DELETE FROM artista WHERE nombre='" + nombreArtista + "'"
                Dim comando As SqlCommand
                comando = New SqlCommand(consulta, conexion)
                comando.ExecuteNonQuery()
                MessageBox.Show("Los datos se borraron correctamente")
            Catch ex As Exception
                MsgBox("Error borrar datos de la DB")
            End Try
            conexion.Close()


        End If
        'Actualizar la lista
        ActualizarLista()
    End Sub


End Class