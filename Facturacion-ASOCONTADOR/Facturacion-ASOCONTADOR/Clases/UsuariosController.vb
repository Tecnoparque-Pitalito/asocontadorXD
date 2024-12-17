Imports System.Data.SQLite

Namespace Clases
    Friend Class UsuariosController
        Friend Sub ListarUsuarios(dataGridViewUsuarios As DataGridView)
            ' Consultar todos los usuarios y cargar los datos en el DataGridView
            Dim query As String = "SELECT * FROM Usuarios"
            Dim dataTable As New DataTable()

            Try
                Using connection As New SQLiteConnection(connectionString)
                    connection.Open()

                    Using adapter As New SQLiteDataAdapter(query, connection)
                        adapter.Fill(dataTable)
                    End Using
                End Using

                ' Asignar la tabla de datos al DataGridView
                dataGridViewUsuarios.DataSource = dataTable
            Catch ex As Exception
                ' Manejar cualquier excepción que pueda ocurrir
                MessageBox.Show("Error al actualizar el DataGridView: " & ex.Message)
            End Try
        End Sub



        Friend Function CrearUsuario(nombreUsuario As String, numeroCedula As String, lugarExpedicion As String, fechaExpedicion As String, telefono As String, fechaNacimiento As String, lugarNacimiento As String, email As String) As Integer
            Try
                Using connexion As New SQLiteConnection(connectionString)
                    connexion.Open()

                    ' Consulta con los nombres de columnas corregidos
                    Dim query As String = "INSERT INTO Usuarios (Nombre, NumeroCedula, LugarExpedicionCedula, FechaEpedicion, telefono, FechaNacimiento, LugarNacimiento, email) " &
                                          "VALUES (@Nombre, @NumeroCedula, @LugarExpedicion, @FechaExpedicion, @Telefono, @FechaNacimiento, @LugarNacimiento, @Email)"

                    Using cmd As New SQLiteCommand(query, connexion)
                        ' Asignación de parámetros con nombres corregidos
                        cmd.Parameters.AddWithValue("@Nombre", nombreUsuario)
                        cmd.Parameters.AddWithValue("@NumeroCedula", numeroCedula)
                        cmd.Parameters.AddWithValue("@LugarExpedicion", lugarExpedicion)
                        cmd.Parameters.AddWithValue("@FechaExpedicion", fechaExpedicion)
                        cmd.Parameters.AddWithValue("@Telefono", telefono)
                        cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento)
                        cmd.Parameters.AddWithValue("@LugarNacimiento", lugarNacimiento)
                        cmd.Parameters.AddWithValue("@Email", email)

                        ' Ejecutar consulta
                        Dim filasAfectadas As Integer = cmd.ExecuteNonQuery()

                        ' Verificación del resultado
                        If filasAfectadas > 0 Then
                            MsgBox("Usuario creado exitosamente")
                            Return 1
                        Else
                            MsgBox("Error al crear el Usuario")
                            Return -1
                        End If
                    End Using
                End Using

            Catch ex As Exception
                ' Manejo del error con detalle
                MsgBox($"Error al crear el usuario: {ex.Message}", MsgBoxStyle.Critical)
                Return -1
            End Try

        End Function

        Friend Function ActualizarUsuarios(nombreUsuario As String, numeroCedula As String, lugarExpedicion As String, fechaExpedicion As String, telefono As String, fechaNacimiento As String, lugarNacimiento As String, email As String) As Integer
            Try
                ' Validar que los campos no estén vacíos antes de proceder
                If String.IsNullOrWhiteSpace(nombreUsuario) OrElse String.IsNullOrWhiteSpace(numeroCedula) Then
                    MsgBox("El campo 'Nombre' o 'Cédula' no puede estar vacío", MsgBoxStyle.Exclamation)
                    Return -1
                End If

                Using connexion As New SQLiteConnection(connectionString)
                    connexion.Open()

                    ' Sentencia UPDATE para actualizar los datos del usuario donde la cédula coincida
                    Dim query As String = "UPDATE Usuarios SET Nombre = @Nombre, LugarExpedicionCedula = @LugarExpedicion, FechaEpedicion = @FechaExpedicion, telefono = @Telefono, FechaNacimiento = @FechaNacimiento, LugarNacimiento = @LugarNacimiento, email = @Email WHERE NumeroCedula = @NumeroCedula"

                    Using cmd As New SQLiteCommand(query, connexion)
                        ' Asignación de parámetros
                        cmd.Parameters.AddWithValue("@Nombre", nombreUsuario)
                        cmd.Parameters.AddWithValue("@NumeroCedula", numeroCedula)  ' El WHERE busca por la cédula
                        cmd.Parameters.AddWithValue("@LugarExpedicion", lugarExpedicion)
                        cmd.Parameters.AddWithValue("@FechaExpedicion", fechaExpedicion)
                        cmd.Parameters.AddWithValue("@Telefono", telefono)
                        cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento)
                        cmd.Parameters.AddWithValue("@LugarNacimiento", lugarNacimiento)
                        cmd.Parameters.AddWithValue("@Email", email)

                        Dim filasAfectadas As Integer = cmd.ExecuteNonQuery()

                        If filasAfectadas > 0 Then
                            MsgBox("Usuario actualizado exitosamente")
                            Return 1
                        Else
                            MsgBox("No se encontró ningún usuario con la cédula proporcionada o no se realizaron cambios")
                            Return -1
                        End If
                    End Using
                End Using

            Catch ex As Exception
                MsgBox($"Error al actualizar el usuario: {ex.Message}", MsgBoxStyle.Critical)
                Return -1
            End Try

        End Function

        Friend Function ListarPrimerUsuario(textBox2 As TextBox) As Boolean
            Try
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()
                    Using cmd As New SQLiteCommand("SELECT NumeroCedula FROM usuarios LIMIT 1", conn)
                        Using reader As SQLiteDataReader = cmd.ExecuteReader()
                            If reader.Read() Then ' Cambiado HasRows por Read para obtener el valor de NumeroCedula
                                textBox2.Text = reader("NumeroCedula").ToString()
                                Return True
                            Else
                                MsgBox("No se encontró un usuario")
                                Return False
                            End If
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                MsgBox("Error en la consulta de base de datos: " & ex.Message) ' Mensaje específico en MsgBox
            End Try
        End Function

        Friend Function BuscarUsuarios(txtNumeroCedula As TextBox, txtLugarExpedicion As TextBox, txtFechaExpedicion As MaskedTextBox, txtNombreUsuario As TextBox, txtTelefono As TextBox, txtFechaNacimiento As MaskedTextBox, txtLugarNacimiento As TextBox, txtEmail As TextBox, cedulaUsuarioABuscar As String) As Boolean
            Try
                Dim queryUsuario As String = "SELECT Nombre as Nombre, NumeroCedula as NumeroCedula, LugarExpedicionCedula as LugarExpedicionCedula, FechaEpedicion as FechaExpedicion, telefono as Telefono, FechaNacimiento as FechaNacimiento, LugarNacimiento as LugarNacimiento, email as Email FROM usuarios where NumeroCedula = @Cedula"
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()
                    Using commandUsuario As New SQLiteCommand(queryUsuario, conn)
                        commandUsuario.Parameters.AddWithValue("@Cedula", cedulaUsuarioABuscar)
                        Using reader As SQLiteDataReader = commandUsuario.ExecuteReader()
                            ' Verificar si se encontró el usuario
                            If reader.Read() Then
                                ' Obtener los datos del usuario
                                Dim nombreUsuario As String = reader("Nombre").ToString()
                                Dim numeroCedula As String = reader("NumeroCedula").ToString()
                                Dim lugarExpedicion As String = reader("LugarExpedicionCedula").ToString()
                                Dim fechaExpedicion As String = reader("FechaExpedicion").ToString()
                                Dim telefono As String = reader("Telefono").ToString()
                                Dim fechaNacimiento As String = reader("FechaNacimiento").ToString()
                                Dim lugarNacimiento As String = reader("LugarNacimiento").ToString()
                                Dim email As String = reader("Email").ToString()

                                ' Mostrar los datos en los componentes de texto
                                txtNombreUsuario.Text = nombreUsuario
                                txtNumeroCedula.Text = numeroCedula
                                txtLugarExpedicion.Text = lugarExpedicion
                                txtFechaExpedicion.Text = fechaExpedicion
                                txtTelefono.Text = telefono
                                txtFechaNacimiento.Text = fechaNacimiento
                                txtLugarNacimiento.Text = lugarNacimiento
                                txtEmail.Text = email

                                Return True
                            Else
                                ' Mostrar un mensaje de error si no se encontró el usuario
                                MsgBox("No se encontró ningún usuario con la cédula especificada.")
                                Return False
                            End If
                        End Using
                    End Using
                    conn.Close()
                End Using
            Catch ex As Exception
                MsgBox("Ocurrió un error durante la consulta: " & ex.Message)
                Return False
            End Try
        End Function

        Friend Function ListarCedulasUsuarios() As List(Of Cedulas)
            Dim listaCedulas As New List(Of Cedulas)

            Try
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()

                    Using cmd As New SQLiteCommand("SELECT NumeroCedula FROM usuarios", conn)
                        Using reader As SQLiteDataReader = cmd.ExecuteReader()
                            If reader.HasRows Then
                                While reader.Read()
                                    ' Verifica si el valor no es nulo y lo convierte a String
                                    Dim numeroCedula As String = If(Not reader.IsDBNull(0), reader.GetValue(0).ToString(), String.Empty)

                                    ' Crea una instancia de Cedulas solo si el valor no es vacío
                                    If Not String.IsNullOrEmpty(numeroCedula) Then
                                        Dim cedula As New Cedulas(numeroCedula)
                                        listaCedulas.Add(cedula)
                                    End If
                                End While
                            End If
                        End Using
                    End Using
                End Using

            Catch ex As Exception
                MsgBox("No se pudo listar los Usuarios: " & ex.Message)
            End Try

            Return listaCedulas
        End Function


    End Class
End Namespace
