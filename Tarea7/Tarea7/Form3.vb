Imports System.Data.SqlClient
Public Class Form3
    Dim rutaarchivo As String = ""
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.ShowDialog()
        rutaarchivo = OpenFileDialog1.FileName()
        If Not (rutaarchivo = "") Then
            Label6.Text = rutaarchivo
        End If

        PictureBox1.Image = Image.FromFile(rutaarchivo)

    End Sub
    Function longitudPassword(texto As String) As Boolean
        Dim resultado As Boolean = False
        If (texto.Length >= 8) Then
            resultado = True
        End If
        Return resultado
    End Function
    Function contieneNumero(texto As String) As Boolean
        Dim resultado As Boolean = False
        If (texto.Contains("0") Or texto.Contains("1") Or texto.Contains("2") Or texto.Contains("3") Or texto.Contains("4") Or texto.Contains("5") Or texto.Contains("6") Or texto.Contains("7") Or texto.Contains("8") Or texto.Contains("9")) Then
            resultado = True
        End If
        Return resultado
    End Function

    Function contieneMayusculas(texto As String) As Boolean
        Dim resultado As Boolean = False
        If (texto.Contains("A") Or texto.Contains("B") Or texto.Contains("C") Or texto.Contains("D") Or texto.Contains("E") Or texto.Contains("F") Or texto.Contains("G") Or texto.Contains("H") Or texto.Contains("I") Or texto.Contains("J") Or
            texto.Contains("K") Or texto.Contains("L") Or texto.Contains("M") Or texto.Contains("N") Or texto.Contains("O") Or texto.Contains("P") Or texto.Contains("Q") Or texto.Contains("R") Or texto.Contains("S") Or texto.Contains("T") Or
            texto.Contains("U") Or texto.Contains("V") Or texto.Contains("W") Or texto.Contains("X") Or texto.Contains("Y") Or texto.Contains("Z")) Then
            resultado = True
        End If
        Return resultado
    End Function
    Function contieneCaracteres(texto As String) As Boolean
        Dim resultado As Boolean = False
        If (texto.Contains("!") Or texto.Contains("#") Or texto.Contains("$") Or texto.Contains("%") Or texto.Contains("&") Or texto.Contains("/") Or texto.Contains("(") Or texto.Contains(")") Or texto.Contains("=") Or texto.Contains("?") Or
            texto.Contains("¿") Or texto.Contains("¡") Or texto.Contains("´") Or texto.Contains("+") Or texto.Contains("*") Or texto.Contains("-") Or texto.Contains("{") Or texto.Contains("}") Or texto.Contains("]") Or texto.Contains("[") Or
            texto.Contains("_") Or texto.Contains("-") Or texto.Contains(".") Or texto.Contains(":") Or texto.Contains(",") Or texto.Contains(";")) Then
            resultado = True
        End If
        Return resultado
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim nuevoUsr_nombre As String = TextBox1.Text
        Dim nuevoUsr_edad As String = TextBox5.Text
        Dim nuevoUsr_username As String = TextBox2.Text
        Dim nuevoUsr_pass1 As String = TextBox3.Text
        Dim nuevoUsr_pass2 As String = TextBox4.Text
        If Not (nuevoUsr_nombre = "") And Not (nuevoUsr_edad = "") And Not (nuevoUsr_username = "") And Not (nuevoUsr_pass1 = "") And Not (nuevoUsr_pass2 = "") Then
            If (nuevoUsr_pass1 = nuevoUsr_pass2) Then
                'MsgBox("Contraseña Iguales")
                If (longitudPassword(nuevoUsr_pass1) And contieneNumero(nuevoUsr_pass1) And contieneMayusculas(nuevoUsr_pass1) And contieneCaracteres(nuevoUsr_pass1)) Then
                    'MsgBox("Password Aceptada")
                    'Registrar el usuario
                    If Not (rutaarchivo = "") Then
                        MsgBox("Datos Aceptados")
                        Dim nuevoUsr_PassEncrip As String = Encriptar(nuevoUsr_pass1)
                        conexion.Open()
                        Try
                            Dim consulta As String = "INSERT INTO USUARIO(NOMBRE,EDAD,USERNAME,PASSWORD,TIPO,FOTO_PERFIL) VALUES ('" + nuevoUsr_nombre + "','" + nuevoUsr_edad + "','" + nuevoUsr_username + "','" + nuevoUsr_PassEncrip + "','Cliente','" + "','" + rutaarchivo + "')"
                            Dim comando As SqlCommand
                            comando = New SqlCommand(consulta, conexion)
                            comando.ExecuteNonQuery()
                            MessageBox.Show("Los datos se guardaron correctamente")

                        Catch ex As Exception
                            MsgBox("Error al ingresar Datos a la DB")
                        End Try

                        conexion.Close()

                    Else
                        MsgBox("Seleccione una foto")
                    End If

                Else
                    MsgBox("Password Rechazada")
                End If
            Else
                MsgBox("Contraseñas no coinciden")
            End If

        Else
            MsgBox("Ingrese todos los Campos")
        End If

        Form1.TextBox1.Text = ""
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form1.Visible = True
        Me.Visible = False

    End Sub

    Private Sub EventLog1_EntryWritten(sender As Object, e As EntryWrittenEventArgs) Handles EventLog1.EntryWritten

    End Sub
End Class