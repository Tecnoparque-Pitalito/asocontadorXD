Public Class UsuarioSis
    Dim admin As New Clases.AdminController

    Private Sub UsuarioSis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Clear()
        RedondearEsquinasFormulario(Me, 20)
        ComboBox1.Items.Add("Administrador")
        ComboBox1.Items.Add("Estándar")

        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CerrarFormulario(Me)
    End Sub
    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub ButsaveUsuario_Click(sender As Object, e As EventArgs) Handles ButsaveUsuario.Click
        Dim nombreAdmin As String = txtUsuario.Text
        Dim contrasena As String = txtContrasena.Text
        Dim rol As String = ComboBox1.Text
        If Not String.IsNullOrWhiteSpace(nombreAdmin) AndAlso
           Not String.IsNullOrWhiteSpace(contrasena) Then
            Dim filasAfectadas As Integer = admin.CrearAdministrador(nombreAdmin, contrasena, rol)
            If filasAfectadas > 0 Then
                MsgBox($"El usuario {rol} se creó exitosamente")
            ElseIf filasAfectadas = 0 Then
                MsgBox($"El usuario {rol} con la cédula proporcionada ya existe.")
            Else
                MsgBox($"Error al guardar el {rol}")
            End If
        Else
            MsgBox("Todos los campos son necesarios")
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim contrasena As String = txtContrasena.Text
        If Not String.IsNullOrWhiteSpace(contrasena) Then
            Dim filasAfectadas As Integer = admin.ElminarAdministrador(contrasena)
            If filasAfectadas > 0 Then
                MsgBox("El usuario se eliminó exitosamente")
            ElseIf filasAfectadas = 0 Then
                MsgBox("El usuario proporcionado no existe")
            Else
                MsgBox("Error al eliminar el usuario")
            End If
        Else
            MsgBox("Es necesario indicar el usuario.")
        End If
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Dim cedulaBuscar As String = InputBox("Ingrese el nombre del usuario a buscar:", "Buscar Usuario")

        If String.IsNullOrEmpty(cedulaBuscar) Then
            MsgBox("Debe ingresar un nombre.")
            Return
        Else
            Dim NumUsuarios As Integer = admin.BuscarUsuario(cedulaBuscar, txtUsuario, txtContrasena, ComboBox1)
        End If
    End Sub

    Private Sub ButmodUsuario_Click(sender As Object, e As EventArgs) Handles ButmodUsuario.Click
        Dim nombreAdmin As String = txtUsuario.Text
        Dim contrasena As String = txtContrasena.Text
        Dim rol As String = ComboBox1.Text
        If Not String.IsNullOrWhiteSpace(nombreAdmin) AndAlso
           Not String.IsNullOrWhiteSpace(contrasena) Then
            Dim filasAfectadas As Integer = admin.ActualizarAdministrador(nombreAdmin, contrasena, rol)
            'If filasAfectadas > 0 Then
            '    MsgBox($"El usuario {rol} se actualizó correctamente")
            'ElseIf filasAfectadas = 0 Then
            '    MsgBox($"El usuario {rol} con la cédula proporcionada ya existe.")
            'Else
            '    MsgBox($"Error al guardar el {rol}")
            'End If
        Else
            MsgBox("Todos los campos son necesarios")
            MsgBox(contrasena)
            MsgBox(rol)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
End Class