Imports System.Data.SqlClient
Public Class Form5


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        reproductorMP3.close()
        Form4.Visible = True
        Me.Visible = False

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim nombrePlaylist As String = ComboBox1.SelectedItem.ToString()
        Dim MiCmd As SqlCommand
        Dim MiLector As SqlDataReader

        conexion.Open()

        Try
            MiCmd = conexion.CreateCommand
            Dim consulta As String = "SELECT * FROM playlist WHERE usuario_id=" + usr_id + " AND nombre='" + nombrePlaylist + "'"
            MiCmd.CommandText = consulta
            MiLector = MiCmd.ExecuteReader()

            Do While MiLector.Read()
                '0-id;  1= nombre;  2=fecha; 3=foto; 4=usr id
                Label2.Text = MiLector.GetString(1)
                Label3.Text = MiLector.GetValue(2).ToString()
                PictureBox1.Image = Image.FromFile(MiLector.GetString(3))
            Loop

        Catch ex As Exception
            MsgBox("Error al consultar playlist")
        End Try

        conexion.Close()


        ActualizarCanciones()


    End Sub

    Sub ActualizarPlaylists()
        ComboBox1.Items.Clear()
        Dim MiCmd As SqlCommand
        Dim MiLector As SqlDataReader

        conexion.Open()

        Try
            MiCmd = conexion.CreateCommand
            Dim consulta As String = "SELECT * FROM playlist WHERE usuario_id=" + usr_id + ""
            MiCmd.CommandText = consulta
            MiLector = MiCmd.ExecuteReader()

            Do While MiLector.Read()
                '0-id;  1= nombre;  2=fecha; 3=foto; 4=usr id
                Dim nombrePlaylist As String = MiLector.GetString(1)
                ComboBox1.Items.Add(nombrePlaylist)
            Loop

        Catch ex As Exception
            MsgBox("Error al consultar playlist")
        End Try

        conexion.Close()
    End Sub

    Sub ActualizarCanciones()
        ListBox1.Items.Clear()
        Dim MiCmd As SqlCommand
        Dim MiLector As SqlDataReader

        Dim nombrePlaylist1 As String = ComboBox1.SelectedItem.ToString()
        Dim idPlaylist = ObtenerIDPlaylist(nombrePlaylist1)

        conexion.Open()

        Try
            MiCmd = conexion.CreateCommand
            Dim consulta As String = "
                SELECT d.playlist_id,d.cancion_id, c.nombre
                FROM detalle_playlist d,cancion c
                WHERE (d.cancion_id=c.id) AND (d.playlist_id=" + idPlaylist + ");
            "
            MiCmd.CommandText = consulta
            MiLector = MiCmd.ExecuteReader()

            Do While MiLector.Read()
                '0-id playlist;  1= id cancion;  2=nombre cancion
                Dim nombreCancion As String = MiLector.GetString(2)
                ListBox1.Items.Add(nombreCancion)
            Loop

        Catch ex As Exception
            MsgBox("Error al consultar playlist")
        End Try

        conexion.Close()
    End Sub

    Function ObtenerIDPlaylist(nombrePlaylist As String)
        Dim resultadoID = ""

        Dim MiCmd As SqlCommand
        Dim MiLector As SqlDataReader
        conexion.Open()

        Try
            MiCmd = conexion.CreateCommand
            Dim consulta As String = "SELECT * FROM playlist WHERE nombre='" + nombrePlaylist + "' AND usuario_id=" + usr_id + ""
            MiCmd.CommandText = consulta
            MiLector = MiCmd.ExecuteReader()

            Do While MiLector.Read()
                '0-id;  1= nombre;  2=fecha; 3=foto; 4=usuario id
                resultadoID = MiLector.GetInt32(0).ToString()
            Loop

        Catch ex As Exception
            MsgBox("Error al consultar playlist")
        End Try

        conexion.Close()

        Return resultadoID
    End Function

    Function ObtenerRutaCancion(nombreCancion As String)
        Dim resultadoURL = ""

        Dim MiCmd As SqlCommand
        Dim MiLector As SqlDataReader
        conexion.Open()

        Try
            MiCmd = conexion.CreateCommand
            Dim consulta As String = "SELECT * FROM cancion WHERE nombre='" + nombreCancion + "'"
            MiCmd.CommandText = consulta
            MiLector = MiCmd.ExecuteReader()

            Do While MiLector.Read()
                '0-id;  1= nombre;  2=genero; 3=ruta; 4=artista id
                resultadoURL = MiLector.GetString(3)
            Loop

        Catch ex As Exception
            MsgBox("Error al consultar playlist")
        End Try

        conexion.Close()

        Return resultadoURL
    End Function

    Private Sub Button3_Click(sender As Object, e As EventArgs) 
        ListBox1.Items.Clear()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim cancionSeleccionada As String = ListBox1.SelectedItem()
        Dim rutaCancion As String = ObtenerRutaCancion(cancionSeleccionada)

        reproductorMP3.URL = rutaCancion

    End Sub
End Class