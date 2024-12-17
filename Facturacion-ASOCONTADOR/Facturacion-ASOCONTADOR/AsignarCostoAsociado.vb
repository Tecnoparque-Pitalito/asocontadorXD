Public Class AsignarCostoAsociado

    Dim predios As New Clases.PrediosController
    Dim costos As New Clases.FacturasGeneratorController
    Dim costosAsociados As New Clases.CostosAsociados
    Private Sub AsignarCostoAsociado_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RedondearEsquinasFormulario(Me, 20)
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = ("dd 'de' MMM 'de' yyyy")
        Dim anyo As Integer = DateTime.Now.Year
        costos.ConstosAdicionales(ComboBox2, anyo)
        If DataGridView1.Columns.Count = 0 Then ' Solo se definen si no hay columnas existentes
            With DataGridView1
                .Columns.Add("Usuario", "Usuario")
                .Columns.Add("Predio", "Predio")
                .Columns.Add("Valor", "Valor")
                .Columns.Add("Fecha", "Fecha")
                .CellBorderStyle = DataGridViewCellBorderStyle.None
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .AllowUserToAddRows = False
                .RowHeadersVisible = False
            End With
        End If
    End Sub
    Private Sub TextBox10_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox10.KeyPress
        ' Verificar si el carácter presionado no es un número ni la tecla de retroceso
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True ' Cancela el evento KeyPress si no es un número
        End If
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If String.IsNullOrWhiteSpace(TextBox10.Text) Then
            MessageBox.Show("La identificación no puede estar vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Else
            DataGridView1.Rows.Clear()
            predios.listarIDNombre(ComboBox1, TextBox10)
            ComboBox1.Enabled = True

        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If String.IsNullOrWhiteSpace(ComboBox1.Text) Then
            MessageBox.Show("Busque un usuario y seleccione un predio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim usuario As String = TextBox10.Text
            Dim predio As String = ComboBox1.Text
            Dim valor As String = ComboBox2.Text
            Dim fecha As String = DateTimePicker1.Value.ToString("dd/MM/yyyy")

            costosAsociados.AgregarCosto(DataGridView1, usuario, predio, valor, fecha)
            ComboBox1.Enabled = False
        End If
    End Sub

    Private Sub RealizarPago_Click(sender As Object, e As EventArgs) Handles RealizarPago.Click
        If DataGridView1.Rows.Count <= 0 Then
            MsgBox("no hay datos que agregar")
            Return
        End If
        costosAsociados.ConfirmarCosto(DataGridView1)
        DataGridView1.Rows.Clear()
        costosAsociados.ListarCostos(DataGridView1, ComboBox1.SelectedItem.ToString, DateTimePicker1)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CerrarFormulario(Me)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub ComboBox1_SeListarCostoslectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        costosAsociados.ListarCostos(DataGridView1, ComboBox1.SelectedItem.ToString, DateTimePicker1)
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        costosAsociados.ListarCostos(DataGridView1, ComboBox1.SelectedItem.ToString, DateTimePicker1)
    End Sub
End Class