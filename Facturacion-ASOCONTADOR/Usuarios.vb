Imports System.Data.SQLite
Imports System.Text.RegularExpressions

Public Class Usuarios

    Dim Usuarios As New Clases.UsuariosController
    Dim limpiar As New Clases.LimpiarCampos
    Private connectionString As String = "Data Source=" & Application.StartupPath & "\DataBAse_Asocontador.s3db;"

    Private Sub Usuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RedondearEsquinasFormulario(Me, 20)
        ConvertirTextBoxAMayusculas(txtLugarExpedicion)
        ConvertirTextBoxAMayusculas(txtNombreUsuario)
        ConvertirTextBoxAMayusculas(txtLugarNacimiento)
        Usuarios.ListarUsuarios(DataGridViewUsuarios)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CerrarFormulario(Me)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub ButbuscarUsuario_Click(sender As Object, e As EventArgs) Handles ButbuscarUsuario.Click
        ' Mostrar cuadro de entrada para ingresar la cédula del usuario a buscar
        Dim cedulaUsuarioABuscar As String = InputBox("Ingrese la cédula del usuario a buscar:", "Buscar Usuario")

        ' Verificar si se ingresó una cédula
        If String.IsNullOrEmpty(cedulaUsuarioABuscar) Then

            MsgBox("Debe ingresar una cédula.")
            Return
        End If

        Dim BuscarUsuario As Boolean = Usuarios.BuscarUsuarios(txtNumeroCedula, txtLugarExpedicion, txtFechaExpedicion, txtNombreUsuario, txtTelefono, txtFechaNacimiento, txtLugarNacimiento, txtEmail, cedulaUsuarioABuscar)
    End Sub

    Private Function UsuarioExistePorCedula(cedula As String) As Boolean
        ' Consulta para verificar si existe un usuario con la cédula proporcionada
        Dim query As String = "SELECT COUNT(*) FROM Usuarios WHERE NumeroCedula = @Cedula"

        Using conn As New SQLiteConnection(connectionString)
            conn.Open()
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@Cedula", cedula)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return count > 0 ' Retorna True si existe al menos un usuario con la cédula proporcionada
            End Using
        End Using
    End Function

    Public Function CrearUsuario(nombre As String, cedula As String, expedicion As String, fechaExpedicion As String, telefono As String, fechaNacimiento As String, lugarNacimiento As String, email As String) As Integer
        ' Verificar si el usuario ya existe con el número de cédula proporcionado
        If UsuarioExistePorCedula(cedula) Then
            Return 0 ' Indicar que el usuario ya existe
        End If

        ' Si el usuario no existe, proceder con la inserción
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
                Dim filasAfectadas As Integer = cmd.ExecuteNonQuery()

                Usuarios.ListarUsuarios(DataGridViewUsuarios)

                Return filasAfectadas
            End Using
        End Using
    End Function

    Private Function EliminarUsuarioPorCedula(cedula As String) As Integer
        ' Consulta para eliminar un usuario por cédula
        Dim query As String = "DELETE FROM Usuarios WHERE NumeroCedula = @Cedula"

        Using conn As New SQLiteConnection(connectionString)
            conn.Open()
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@Cedula", cedula)
                Dim filasAfectadas As Integer = cmd.ExecuteNonQuery()

                ' Después de eliminar el usuario, actualizar el DataGridView
                Usuarios.ListarUsuarios(DataGridViewUsuarios)

                Return filasAfectadas
            End Using
        End Using
    End Function

    Private Sub ConvertirTextBoxAMayusculas(ByVal textBox As TextBox)
        ' Suscribir el evento KeyPress del TextBox
        AddHandler textBox.KeyPress, AddressOf TextBoxAMayusculas_KeyPress
    End Sub

    Private Sub TextBoxAMayusculas_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        ' Verificar si la tecla presionada es una letra minúscula
        If Char.IsLower(e.KeyChar) Then
            ' Convertir la letra a mayúscula
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub

    Private Sub txtEmail_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged
        ' Obtener el texto ingresado en el TextBox
        Dim correo As String = txtEmail.Text

        ' Definir el patrón de expresión regular para validar el formato del correo electrónico
        Dim patron As String = "^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"

        ' Verificar si el correo coincide con el patrón
        If Regex.IsMatch(correo, patron) Then
            ' El correo electrónico tiene un formato válido
            txtEmail.ForeColor = Color.Black
        Else
            ' El correo electrónico tiene un formato inválido
            txtEmail.ForeColor = Color.Red
        End If
    End Sub



    Private Sub ButeliminarUsuario_Click(sender As Object, e As EventArgs) Handles ButeliminarUsuario.Click
        ' Solicitar al usuario que ingrese la cédula del usuario a eliminar
        Dim cedula As String = InputBox("Ingrese la cédula del usuario a eliminar:", "Eliminar Usuario")

        ' Verificar si se ingresó una cédula
        If Not String.IsNullOrEmpty(cedula) Then
            ' Llamar a la función para eliminar el usuario por cédula
            Dim filasAfectadas As Integer = EliminarUsuarioPorCedula(cedula)

            ' Verificar si se eliminó el usuario correctamente
            If filasAfectadas > 0 Then

                MsgBox("El usuario con la cédula " & cedula & " ha sido eliminado correctamente.")

            Else

                MsgBox("No se encontró ningún usuario con la cédula " & cedula)
            End If
        End If
    End Sub

    Private Sub ButsaveUsuario_Click(sender As Object, e As EventArgs) Handles ButsaveUsuario.Click
        ' Obtener los valores ingresados por el usuario
        Dim nombreUsuario As String = txtNombreUsuario.Text
        Dim numeroCedula As String = txtNumeroCedula.Text
        Dim lugarExpedicion As String = txtLugarExpedicion.Text
        Dim fechaExpedicion As String = txtFechaExpedicion.Text
        Dim telefono As String = txtTelefono.Text
        Dim fechaNacimiento As String = txtFechaNacimiento.Text
        Dim lugarNacimiento As String = txtLugarNacimiento.Text
        Dim email As String = txtEmail.Text

        ' Asignar valor predeterminado si el campo de correo está en blanco
        If String.IsNullOrWhiteSpace(email) Then
            email = "No@plica.null"
        End If

        ' Verificar que se hayan ingresado todos los datos requeridos
        If Not String.IsNullOrWhiteSpace(nombreUsuario) AndAlso
       Not String.IsNullOrWhiteSpace(numeroCedula) AndAlso
       Not String.IsNullOrWhiteSpace(lugarExpedicion) AndAlso
       Not String.IsNullOrWhiteSpace(fechaExpedicion) AndAlso
       Not String.IsNullOrWhiteSpace(telefono) AndAlso
       Not String.IsNullOrWhiteSpace(fechaNacimiento) AndAlso
       Not String.IsNullOrWhiteSpace(lugarNacimiento) Then

            ' Verificar el formato del correo electrónico
            Dim patronCorreo As String = "^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
            If Not Regex.IsMatch(email, patronCorreo) Then
                ' El correo electrónico tiene un formato inválido
                MsgBox("El correo electrónico ingresado no tiene un formato válido.")
                Return
            End If

            ' Intentar guardar el nuevo usuario en la base de datos
            Dim filasAfectadas As Integer = Usuarios.CrearUsuario(nombreUsuario, numeroCedula, lugarExpedicion, fechaExpedicion, telefono, fechaNacimiento, lugarNacimiento, email)
            Usuarios.ListarUsuarios(DataGridViewUsuarios)
            limpiar.LimpiarTextBoxes(txtNombreUsuario, txtNumeroCedula, txtLugarExpedicion, txtTelefono, txtLugarNacimiento, txtEmail)
            limpiar.LimpiarControles(txtFechaNacimiento, txtFechaExpedicion)
        Else
            MsgBox("Por favor complete todos los campos.")
        End If
    End Sub



    Private Sub LimpiarCampos()
        ' Limpiar todos los campos de entrada de texto
        txtNombreUsuario.Text = ""
        txtNumeroCedula.Text = ""
        txtLugarExpedicion.Text = ""
        txtFechaExpedicion.Text = ""
        txtTelefono.Text = ""
        txtFechaNacimiento.Text = ""
        txtLugarNacimiento.Text = ""
        txtEmail.Text = ""
    End Sub

    Private Sub ButmodUsuario_Click(sender As Object, e As EventArgs) Handles ButmodUsuario.Click

        ' Obtener los nuevos valores ingresados por el usuario
        Dim numeroCedula As String = txtNumeroCedula.Text
        Dim nombreUsuario As String = txtNombreUsuario.Text
        Dim lugarExpedicion As String = txtLugarExpedicion.Text
        Dim fechaExpedicion As String = txtFechaExpedicion.Text
        Dim telefono As String = txtTelefono.Text
        Dim fechaNacimiento As String = txtFechaNacimiento.Text
        Dim lugarNacimiento As String = txtLugarNacimiento.Text
        Dim email As String = txtEmail.Text

        ' Verificar que se hayan ingresado todos los datos requeridos
        If Not String.IsNullOrWhiteSpace(nombreUsuario) AndAlso
           Not String.IsNullOrWhiteSpace(lugarExpedicion) AndAlso
           Not String.IsNullOrWhiteSpace(fechaExpedicion) AndAlso
           Not String.IsNullOrWhiteSpace(telefono) AndAlso
           Not String.IsNullOrWhiteSpace(fechaNacimiento) AndAlso
           Not String.IsNullOrWhiteSpace(lugarNacimiento) AndAlso
           Not String.IsNullOrWhiteSpace(email) Then

            ' Verificar el formato del correo electrónico
            Dim patronCorreo As String = "^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
            If Not Regex.IsMatch(email, patronCorreo) Then
                ' El correo electrónico tiene un formato inválido
                MsgBox("El correo electrónico ingresado no tiene un formato válido.")
                Return
            End If

            Dim filasAfectadas As Integer = Usuarios.ActualizarUsuarios(nombreUsuario, numeroCedula, lugarExpedicion, fechaExpedicion, telefono, fechaNacimiento, lugarNacimiento, email)
            Usuarios.ListarUsuarios(DataGridViewUsuarios)
            limpiar.LimpiarTextBoxes(txtNombreUsuario, txtNumeroCedula, txtLugarExpedicion, txtTelefono, txtLugarNacimiento, txtEmail)
            limpiar.LimpiarControles(txtFechaNacimiento, txtFechaExpedicion)

        Else
            MsgBox("Por favor complete todos los campos.")
        End If

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub txtFechaExpedicion_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles txtFechaExpedicion.MaskInputRejected

    End Sub
End Class
