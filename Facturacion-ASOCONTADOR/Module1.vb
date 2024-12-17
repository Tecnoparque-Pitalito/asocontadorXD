Imports System.Data.SQLite

Module Module1
    Public rolUsuario As String = ""
    Public UsuarioLogin As String = ""

    Public connectionString As String = "Data Source=" & Application.StartupPath & "\DataBAse_Asocontador.s3db;"

    Public Function CrearUsuario(nombre As String, cedula As String, expedicion As String, fechaExpedicion As String, telefono As String, fechaNacimiento As String, lugarNacimiento As String, email As String) As Integer
        Using conn As New SQLiteConnection(connectionString)
            conn.Open()
            Dim query As String = "INSERT INTO Usuario (NombreUsuario, NumeroCedula, LugarExpedicion, FechaExpedicion, Telefono, FechaNacimiento, LugarNacimiento, Email) VALUES (@NombreUsuario, @NumeroCedula, @LugarExpedicion, @FechaExpedicion, @Telefono, @FechaNacimiento, @LugarNacimiento, @Email)"
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@NombreUsuario", nombre)
                cmd.Parameters.AddWithValue("@NumeroCedula", cedula)
                cmd.Parameters.AddWithValue("@LugarExpedicion", expedicion)
                cmd.Parameters.AddWithValue("@FechaExpedicion", fechaExpedicion)
                cmd.Parameters.AddWithValue("@Telefono", telefono)
                cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento)
                cmd.Parameters.AddWithValue("@LugarNacimiento", lugarNacimiento)
                cmd.Parameters.AddWithValue("@Email", email)
                Return cmd.ExecuteNonQuery()
            End Using
        End Using
    End Function

    Public Function ObtenerUsuarios() As DataTable
        Using conn As New SQLiteConnection(connectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM Usuario"
            Using cmd As New SQLiteCommand(query, conn)
                Dim dt As New DataTable()
                dt.Load(cmd.ExecuteReader())
                Return dt
            End Using
        End Using
    End Function

    Public Function ActualizarUsuario(id As Integer, nombre As String, cedula As String, expedicion As String, fechaExpedicion As String, telefono As String, fechaNacimiento As String, lugarNacimiento As String, email As String) As Integer
        Using conn As New SQLiteConnection(connectionString)
            conn.Open()
            Dim query As String = "UPDATE Usuario SET NombreUsuario=@NombreUsuario, NumeroCedula=@NumeroCedula, LugarExpedicion=@LugarExpedicion, FechaExpedicion=@FechaExpedicion, Telefono=@Telefono, FechaNacimiento=@FechaNacimiento, LugarNacimiento=@LugarNacimiento, Email=@Email WHERE ID=@ID"
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@NombreUsuario", nombre)
                cmd.Parameters.AddWithValue("@NumeroCedula", cedula)
                cmd.Parameters.AddWithValue("@LugarExpedicion", expedicion)
                cmd.Parameters.AddWithValue("@FechaExpedicion", fechaExpedicion)
                cmd.Parameters.AddWithValue("@Telefono", telefono)
                cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento)
                cmd.Parameters.AddWithValue("@LugarNacimiento", lugarNacimiento)
                cmd.Parameters.AddWithValue("@Email", email)
                cmd.Parameters.AddWithValue("@ID", id)
                Return cmd.ExecuteNonQuery()
            End Using
        End Using
    End Function

    Public Function EliminarUsuarioPorCedula(cedula As String) As Integer
        ' Definir la consulta para eliminar el usuario por cédula
        Dim query As String = "DELETE FROM Usuario WHERE NumeroCedula = @Cedula"

        ' Contador para almacenar el número de filas afectadas
        Dim filasAfectadas As Integer = 0

        Using conn As New SQLiteConnection(connectionString)
            conn.Open()
            Using cmd As New SQLiteCommand(query, conn)
                ' Asignar el valor de la cédula al parámetro
                cmd.Parameters.AddWithValue("@Cedula", cedula)
                ' Ejecutar la consulta y obtener el número de filas afectadas
                filasAfectadas = cmd.ExecuteNonQuery()
            End Using
        End Using

        ' Retornar el número de filas afectadas
        Return filasAfectadas
    End Function

End Module
