Imports System.Data.SqlClient
Public Class Form10
    Dim MiCmd As SqlCommand
    Dim adaptador As SqlDataAdapter
    Dim setDatos As DataSet

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form4.Visible = True
        Me.Visible = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim consulta1 As String = "
            SELECT a.nombre AS 'Nombre artista',COUNT(c.nombre) AS 'Cantidad canciones'
            FROM cancion c
            INNER JOIN artista a
            ON c.artista_id=a.id
            GROUP BY a.nombre
            ORDER BY 'Nombre artista'
            ;
        "
        MostrarReporte(consulta1)
    End Sub


    Sub MostrarReporte(entrada As String)
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()

        conexion.Open()

        Try
            MiCmd = conexion.CreateCommand
            Dim consulta As String = entrada

            MiCmd.CommandText = consulta
            MiCmd.CommandType = CommandType.Text
            MiCmd.Connection = conexion
            adaptador = New SqlDataAdapter(MiCmd)
            setDatos = New DataSet()
            adaptador.Fill(setDatos)

            DataGridView1.DataSource = setDatos.Tables(0).DefaultView

        Catch ex As Exception
            MsgBox("Error al realizar consulta")
        End Try

        conexion.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim consulta1 As String = "
        SELECT c.nombre AS 'Nombre cancion', COUNT(d.playlist_id) AS 'Cant. veces agregadas'
        FROM detalle_playlist d
        INNER JOIN cancion c
        ON d.cancion_id=c.id
        GROUP BY c.nombre
        ;
        "
        MostrarReporte(consulta1)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim consulta1 As String = "SELECT * FROM artista"
        MostrarReporte(consulta1)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim consulta1 As String = "
            SELECT TOP 3 a.nombre AS 'Nombre artista', COUNT(c.nombre) AS 'Cant. canciones agregadas'
            FROM detalle_playlist d
            INNER JOIN cancion c
            ON d.cancion_id=c.id
            INNER JOIN artista a
            ON c.artista_id=a.id
            GROUP BY a.nombre
            ORDER BY 'Cant. canciones agregadas' DESC
            ;
           "
        MostrarReporte(consulta1)
    End Sub
End Class