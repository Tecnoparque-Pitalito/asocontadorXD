Imports System.Data.SQLite

Namespace Clases
    Friend Class AdminController
        Public Sub New()
        End Sub

        Public Function CrearAdministrador(nombre As String, contrasena As String, rol As String)
            Try
                Using conexion As New SQLiteConnection(connectionString)
                    conexion.Open()
                    Dim query As String = " INSERT INTO administrador (usuario, contrasena, rol) VALUES (@nombre, @contrasena, @rol);"
                    Using cmd As New SQLiteCommand(query, conexion)
                        cmd.Parameters.AddWithValue("@nombre", nombre)
                        cmd.Parameters.AddWithValue("@contrasena", contrasena)
                        cmd.Parameters.AddWithValue("@rol", rol)
                        Dim filasAfectadas As Integer = cmd.ExecuteNonQuery()
                        Return filasAfectadas
                    End Using
                    conexion.Close()
                End Using
            Catch ex As Exception
                MsgBox($"Error al crear el {rol}: " & ex.Message)
            End Try
        End Function

        Public Function ElminarAdministrador(Identificacion As String)
            Try
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()
                    Dim query As String = ("DELETE FROM administrador WHERE usuario = @usuario")
                    Using cmd As New SQLiteCommand(query, conn)
                        cmd.Parameters.AddWithValue("@usuario", cedulaBuscar)
                        Dim filasAfectadas As Integer = cmd.ExecuteNonQuery()
                        Return filasAfectadas
                    End Using
                    conn.Close()
                End Using
            Catch ex As Exception
                MsgBox("Error al eliminar el usuario: " & ex.Message)
            End Try
        End Function

        Friend Function BuscarUsuario(cedulaBuscar As String, Nombre As TextBox, Contrasena As TextBox, rol As ComboBox) As Integer
            Try
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()
                    Dim query As String = ("select * from administrador  where usuario = @usuario")
                    Using cmd As New SQLiteCommand(query, conn)
                        cmd.Parameters.AddWithValue("@usuario", cedulaBuscar)
                        Using reader As SQLiteDataReader = cmd.ExecuteReader()
                            If reader.Read() Then
                                Nombre.Text = reader("usuario").ToString()
                                Contrasena.Text = reader("contrasena").ToString()
                                rol.SelectedItem = reader("rol").ToString()
                            End If
                        End Using
                    End Using
                    conn.Close()
                End Using
            Catch ex As Exception
                MsgBox("Error al buscar y mostrar los datos del usuario: " & ex.Message)
            End Try
        End Function

        Friend Function ActualizarAdministrador(nombreAdmin As Object, contrasena As String, rol As String) As Integer
            Try
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()
                    Dim query As String = "UPDATE administrador SET contrasena = @contrasena, rol = @rol WHERE usuario = @usuario;"
                    Using cmd As New SQLiteCommand(query, conn)
                        cmd.Parameters.AddWithValue("@contrasena", contrasena)
                        cmd.Parameters.AddWithValue("@rol", rol)
                        cmd.Parameters.AddWithValue("@usuario", nombreAdmin)
                        Dim filasAfectadas As Integer = cmd.ExecuteNonQuery()
                        If filasAfectadas > 0 Then
                            MsgBox("Se Actualizo Correctamente")
                        ElseIf filasAfectadas = 0 Then
                            MsgBox($"No se actulizo el usuario {rol}")
                        Else
                            MsgBox("Error el en proceso")
                        End If
                    End Using
                    conn.Close()
                End Using
            Catch ex As Exception
                MsgBox("Error al buscar los datos del usuario: " & ex.Message)
            End Try
        End Function
    End Class
End Namespace
