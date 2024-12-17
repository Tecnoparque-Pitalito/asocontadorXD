Imports System.Data.SQLite

Public Class Predios

    Dim Predios As New Clases.PrediosController
    Dim Usuarios As New Clases.UsuariosController
    Dim Limpiar As New Clases.LimpiarCampos
    Private Sub Predios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            RedondearEsquinasFormulario(Me, 20)

            Predios.NuevoCodigo(Label24)



            CargarIdsEnComboBox(ComboBox2)


            ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
            ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList
            ComboBox2.SelectedIndex = 0

            ComboTipoDeUso(ComboBox1)

        Catch ex As Exception
            'MsgBox("Error al momento de cargar el formulario")
        End Try
    End Sub

    Public Sub CargarIdsEnComboBox(comboBox As ComboBox)
        Dim fechaActual As DateTime = DateTime.Now
        comboBox.Items.Add(fechaActual.Year.ToString)
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CerrarFormulario(Me)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Mostrar cuadro de entrada para ingresar la cédula del usuario a buscar

        dgvPredios.DataSource = Nothing
        Dim cedulaUsuarioABuscar As String = txtNumeroCedula.Text
        ' Verificar si se ingresó una cédula
        If Not String.IsNullOrEmpty(cedulaUsuarioABuscar) Then
            Dim BuscarUsuario As Boolean = Usuarios.BuscarUsuarios(txtNumeroCedula, txtLugarExpedicion, txtFechaExpedicion, txtNombreUsuario, txtTelefono, txtFechaNacimiento, txtLugarNacimiento, txtEmail, cedulaUsuarioABuscar)
            If BuscarUsuario = True Then
                Dim BuscarPredios As Integer = Predios.BuscarPredios(txtNumeroCedula, dgvPredios)
                ComboTipoDeUso(ComboBox1)
                GroupBox5.Enabled = True
                GroupBox6.Enabled = True
                GroupBox7.Enabled = True
            End If
        Else
            MsgBox("Debe ingresar una cédula.")
            Return
        End If
    End Sub

    Private Sub ComboTipoDeUso(ByVal comboBox As ComboBox)
        Dim query As String = "Select idClasificacionUso as id, TipoDeUso from clasificacionuso"

        Using connection As New SQLiteConnection(connectionString)
            Try
                connection.Open()
                Using command As New SQLiteCommand(query, connection)
                    Using reader As SQLiteDataReader = command.ExecuteReader()
                        comboBox.Items.Clear()
                        While reader.Read()
                            ' Obtener id y TipoDeUso
                            Dim id As Integer = reader("id")
                            Dim tipoDeUso As String = reader("TipoDeUso").ToString()

                            ' Formatear el texto a "ID | TIPO DE USO"
                            Dim itemText As String = id & " | " & tipoDeUso

                            ' Agregar el texto formateado al ComboBox
                            comboBox.Items.Add(itemText)
                            comboBox.SelectedIndex = 0
                        End While
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            Finally
                connection.Close()
            End Try
        End Using
    End Sub

    Private Sub ButsaveUsuario_Click(sender As Object, e As EventArgs) Handles ButsaveUsuario.Click
        Dim NumCedula As String = txtNumeroCedula.Text
        Dim NombrePredio As String = txt_nomPredio.Text
        Dim ExtensionPredio As String = TextBox1.Text
        Dim VeredaPredio As String = TextBox2.Text
        Dim MatriculaPredio As String = TextBox3.Text
        Dim RegistroPredio As String = TextBox4.Text
        Dim ClasificacionUso As String = ComboBox1.Text.Split("|"c)(0).Trim()
        Dim DiamTuveria As Decimal = NumericUpDown1.Value
        Dim AreasContruir As String = TextBox6.Text
        Dim AreasContruidas As String = TextBox7.Text
        Dim ServicioActivo As Boolean = RadioButton1.Checked
        Dim Observacones As String = TextBox8.Text
        Dim Codigo As String = Label24.Text


        If Not String.IsNullOrWhiteSpace(NumCedula) AndAlso
  Not String.IsNullOrWhiteSpace(NombrePredio) AndAlso
  Not String.IsNullOrWhiteSpace(ExtensionPredio) AndAlso
  Not String.IsNullOrWhiteSpace(VeredaPredio) AndAlso
  Not String.IsNullOrWhiteSpace(MatriculaPredio) AndAlso
  Not String.IsNullOrWhiteSpace(RegistroPredio) AndAlso
  Not String.IsNullOrWhiteSpace(ClasificacionUso) AndAlso
  Not String.IsNullOrWhiteSpace(DiamTuveria) AndAlso
  Not String.IsNullOrWhiteSpace(AreasContruir) AndAlso
  Not String.IsNullOrWhiteSpace(AreasContruidas) AndAlso
  Not String.IsNullOrWhiteSpace(Observacones) Then
            Dim RegisterPredios As Integer = Predios.RegistrarPredios(
            txtNumeroCedula.Text.Trim(),
            txt_nomPredio.Text.Trim(),
            TextBox1.Text.Trim(),
            TextBox2.Text.Trim(),
            TextBox3.Text.Trim(),
            TextBox4.Text.Trim(),
            If(String.IsNullOrEmpty(ComboBox1.Text), String.Empty, ComboBox1.Text.Split("|"c)(0).Trim()),
            NumericUpDown1.Value,
            TextBox6.Text.Trim(),
            TextBox7.Text.Trim(),
            RadioButton1.Checked,
            TextBox8.Text.Trim(),
            If(ComboBox2.SelectedItem IsNot Nothing, Convert.ToInt32(ComboBox2.SelectedItem), 0),
            Label24.Text.Trim()
        )

            If RegisterPredios = 1 Then
                Limpiar.LimpiarTextBoxes(txt_nomPredio, TextBox1, TextBox2, TextBox3, TextBox4, TextBox6, TextBox7, TextBox7, TextBox8)
                Limpiar.LimpiarComboBoxes(ComboBox1)
                Limpiar.LimpiarNumericUpDowns(NumericUpDown1)
                Predios.NuevoCodigo(Label24)
                ComboTipoDeUso(ComboBox1)
            End If
            Dim BuscarPredios As Integer = Predios.BuscarPredios(txtNumeroCedula, dgvPredios)
            Predios.NuevoCodigo(Label24)
        Else
            MsgBox("LLene todos los campos")
        End If



    End Sub

    Private Sub ButbuscarUsuario_Click(sender As Object, e As EventArgs) Handles ButbuscarUsuario.Click
        Dim codigoPredioBuscar As String = InputBox("Ingrese el código del predio a buscar:", "Buscar Predio")

        If String.IsNullOrEmpty(codigoPredioBuscar) Then
            MsgBox("Debe ingresar un código de predio.")
            Return
        Else
            RadioButton1.Enabled = True
            RadioButton2.Enabled = True
            ButsaveUsuario.Visible = False
            Predios.SeleccionarPredio(txtNumeroCedula, Label24, txt_nomPredio, TextBox1, TextBox2, TextBox3, TextBox4, ComboBox1, NumericUpDown1, TextBox6, TextBox7, RadioButton1, RadioButton2, TextBox8, codigoPredioBuscar)
        End If
    End Sub

    Private Sub ButmodUsuario_Click(sender As Object, e As EventArgs) Handles ButmodUsuario.Click
        Dim NumCedula As String = txtNumeroCedula.Text
        Dim NombrePredio As String = txt_nomPredio.Text
        Dim ExtensionPredio As String = TextBox1.Text
        Dim VeredaPredio As String = TextBox2.Text
        Dim MatriculaPredio As String = TextBox3.Text
        Dim RegistroPredio As String = TextBox4.Text
        Dim ClasificacionUso As String = ComboBox1.Text.Split("|"c)(0).Trim()
        Dim DiamTuveria As Decimal = NumericUpDown1.Value
        Dim AreasContruir As String = TextBox6.Text
        Dim AreasContruidas As String = TextBox7.Text
        Dim ServicioActivo As Boolean = RadioButton1.Checked
        Dim Observacones As String = TextBox8.Text
        Dim Codigo As String = Label24.Text


        If Not String.IsNullOrWhiteSpace(NumCedula) AndAlso
         Not String.IsNullOrWhiteSpace(NombrePredio) AndAlso
         Not String.IsNullOrWhiteSpace(ExtensionPredio) AndAlso
         Not String.IsNullOrWhiteSpace(VeredaPredio) AndAlso
         Not String.IsNullOrWhiteSpace(MatriculaPredio) AndAlso
         Not String.IsNullOrWhiteSpace(RegistroPredio) AndAlso
         Not String.IsNullOrWhiteSpace(ClasificacionUso) AndAlso
         Not String.IsNullOrWhiteSpace(DiamTuveria) AndAlso
         Not String.IsNullOrWhiteSpace(AreasContruir) AndAlso
         Not String.IsNullOrWhiteSpace(AreasContruidas) AndAlso
         Not String.IsNullOrWhiteSpace(Observacones) Then
            Dim filasAfectadas As Integer = Predios.ActualizarPredios(Codigo, NumCedula, NombrePredio, ExtensionPredio, VeredaPredio, MatriculaPredio, RegistroPredio, ClasificacionUso, DiamTuveria, AreasContruir, AreasContruidas, ServicioActivo, Observacones, ComboBox2.Text)
            If filasAfectadas Then

                RadioButton1.Enabled = False
                RadioButton2.Enabled = False
                ButsaveUsuario.Visible = True
                Limpiar.LimpiarTextBoxes(txt_nomPredio, TextBox1, TextBox2, TextBox3, TextBox4, TextBox6, TextBox7, TextBox7, TextBox8)
                Limpiar.LimpiarComboBoxes(ComboBox1)
                Limpiar.LimpiarNumericUpDowns(NumericUpDown1)
                Predios.NuevoCodigo(Label24)
                ComboTipoDeUso(ComboBox1)
            End If
            Dim BuscarPredios As Integer = Predios.BuscarPredios(txtNumeroCedula, dgvPredios)
            Predios.NuevoCodigo(Label24)
        Else
            MsgBox("LLene todos los campos")
        End If
    End Sub

    Private Sub ButeliminarUsuario_Click(sender As Object, e As EventArgs) Handles ButeliminarUsuario.Click
        If String.IsNullOrWhiteSpace(txtNumeroCedula.Text) Then
            MsgBox("Primero ingrese un Usuario.")
            Return
        End If
        Dim codigoPredioBuscar As String = InputBox("Ingrese el código del predio a Eliminar:", "Seleccionar Predio")

        If String.IsNullOrEmpty(codigoPredioBuscar) Then
            MsgBox("Debe ingresar un código de predio.")
            Return
        Else
            Predios.EliminarPredios(txtNumeroCedula, codigoPredioBuscar)
        End If
        ComboTipoDeUso(ComboBox1)
        Predios.BuscarPredios(txtNumeroCedula, dgvPredios)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Limpiar.LimpiarTextBoxes(txt_nomPredio, TextBox1, TextBox2, TextBox3, TextBox4, TextBox6, TextBox7, TextBox7, TextBox8)
        Limpiar.LimpiarComboBoxes(ComboBox1)
        Limpiar.LimpiarNumericUpDowns(NumericUpDown1)
        Predios.NuevoCodigo(Label24)
        ComboTipoDeUso(ComboBox1)
        ButsaveUsuario.Visible = True
    End Sub

    Private Sub txtNumeroCedula_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNumeroCedula.KeyPress
        ' Verificar si el carácter presionado no es un número ni la tecla de retroceso
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True ' Cancela el evento KeyPress si no es un número
        End If
    End Sub


    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        ' Verificar si el carácter presionado no es un número ni la tecla de retroceso
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True ' Cancela el evento KeyPress si no es un número
        End If
    End Sub


    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        ' Verificar si el carácter presionado no es un número ni la tecla de retroceso
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True ' Cancela el evento KeyPress si no es un número
        End If
    End Sub


    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        ' Verificar si el carácter presionado no es un número ni la tecla de retroceso
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True ' Cancela el evento KeyPress si no es un número
        End If
    End Sub
    Private Sub TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress
        ' Verificar si el carácter presionado no es un número ni la tecla de retroceso
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True ' Cancela el evento KeyPress si no es un número
        End If
    End Sub
    Private Sub TextBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox7.KeyPress
        ' Verificar si el carácter presionado no es un número ni la tecla de retroceso
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True ' Cancela el evento KeyPress si no es un número
        End If
    End Sub

    'Private Sub txt_nomPredio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_nomPredio.KeyPress
    '    ' Verificar si el carácter presionado no es una letra ni una tecla de control
    '    If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
    '        e.Handled = True ' Cancela el evento KeyPress si no es una letra
    '    End If
    'End Sub


    Private Sub Panel7_Paint(sender As Object, e As PaintEventArgs) Handles Panel7.Paint

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub dgvPredios_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPredios.CellContentClick

    End Sub
End Class