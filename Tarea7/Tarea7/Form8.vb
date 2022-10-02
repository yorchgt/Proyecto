Imports System.Data.SqlClient

Public Class Form8
    Dim rutaCancion As String = ""

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form4.Visible = True
        Me.Visible = False

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OpenFileDialog1.ShowDialog()
        rutaCancion = OpenFileDialog1.FileName()
        If Not (rutaCancion = "") Then
            Label5.Text = rutaCancion
        End If
    End Sub

    Sub ActualizarArtistas()
        ComboBox1.Items.Clear()
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
                ComboBox1.Items.Add(nombreArtista)
            Loop

        Catch ex As Exception
            MsgBox("Error al consultar artistas")
        End Try

        conexion.Close()
    End Sub
    Sub ActualizarCanciones()
        ListBox1.Items.Clear()
        Dim MiCmd As SqlCommand
        Dim MiLector As SqlDataReader

        conexion.Open()

        Try
            MiCmd = conexion.CreateCommand
            Dim consulta As String = "SELECT * FROM cancion"
            MiCmd.CommandText = consulta
            MiLector = MiCmd.ExecuteReader()

            Do While MiLector.Read()
                '0-id;  1= nombre;  2=genero; 3=ubicacion; 4=artista
                Dim nombreArtista As String = MiLector.GetString(1)
                ListBox1.Items.Add(nombreArtista)
            Loop

        Catch ex As Exception
            MsgBox("Error al consultar artistas")
        End Try

        conexion.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim nombreCancion As String = TextBox1.Text
        Dim generoCancion As String = TextBox2.Text
        Dim artistaSeleccionado As String = ComboBox1.SelectedItem().ToString()
        Dim idArtista As String = ObtenerIdArtista(artistaSeleccionado)

        If (nombreCancion = "" Or generoCancion = "" Or rutaCancion = "") Then
            MsgBox("Introduzca todos los campos")
        Else
            'Abrir la conexion
            conexion.Open()

            Try
                Dim consulta As String = "INSERT INTO cancion(nombre,genero,ubicacion,artista_id) VALUES ('" + nombreCancion + "','" + generoCancion + "','" + rutaCancion + "'," + idArtista + ")"
                Dim comando As SqlCommand
                comando = New SqlCommand(consulta, conexion)
                comando.ExecuteNonQuery()
                MessageBox.Show("Los datos de la cancion guardaron correctamente")
            Catch ex As Exception
                MsgBox("Error al ingresar datos a la DB")
            End Try

            'Cerrar la conexion
            conexion.Close()
        End If

        ActualizarCanciones()
        TextBox1.Text = ""
        TextBox2.Text = ""
        ComboBox1.Text = ""

    End Sub

    Function ObtenerIdArtista(nombreArtista As String) As String
        Dim resultadoID = ""

        Dim MiCmd As SqlCommand
        Dim MiLector As SqlDataReader
        conexion.Open()

        Try
            MiCmd = conexion.CreateCommand
            Dim consulta As String = "SELECT * FROM artista WHERE nombre='" + nombreArtista + "'"
            MiCmd.CommandText = consulta
            MiLector = MiCmd.ExecuteReader()

            Do While MiLector.Read()
                '0-id;  1= nombre;  2=nacionalidad
                resultadoID = MiLector.GetInt32(0).ToString()
            Loop

        Catch ex As Exception
            MsgBox("Error al consultar artistas")
        End Try

        conexion.Close()

        Return resultadoID
    End Function

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim nombreCancion As String = ListBox1.SelectedItem.ToString()
        If MsgBox("Desea Eliminar el Artista?", vbQuestion + vbYesNo, "Pregunta") = vbYes Then
            conexion.Open()
            Try
                Dim consulta As String = "DELETE FROM cancion WHERE nombre='" + nombreCancion + "'"
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
        ActualizarCanciones()
    End Sub
End Class