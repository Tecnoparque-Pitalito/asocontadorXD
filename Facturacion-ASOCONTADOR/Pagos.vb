Imports Facturacion_ASOCONTADOR.Clases

Public Class Pagos
    Dim FacturasPagos As New Clases.FacturasPagosController
    Dim predios As New Clases.PrediosController
    Private Sub Tarifas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RedondearEsquinasFormulario(Me, 20)
    End Sub

    Private Sub TextBox10_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox10.KeyPress
        ' Verificar si el carácter presionado no es un número ni la tecla de retroceso
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True ' Cancela el evento KeyPress si no es un número
        End If
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        predios.listarIDNombre(ComboBox1, TextBox10, Label3)

        ' Asegúrate de que el ComboBox tiene un elemento seleccionado
        If ComboBox1.SelectedItem IsNot Nothing Then
            ' Obtener el texto seleccionado en el ComboBox
            Dim selectedText As String = ComboBox1.SelectedItem.ToString()

            ' Dividir el texto para obtener el ID (parte antes del guion)
            Dim idPart As String = selectedText.Split("-"c)(0).Trim()

            ' Convertir a entero
            Dim idpredio As Integer
            If Integer.TryParse(idPart, idpredio) Then
                FacturasPagos.ListarFacturasPagos(TextBox10, NombrePropietario, Telefono, Predio, matricula, DataGridView1, idpredio)
            Else
                ' Manejar el caso en que no se puede convertir a entero
                MessageBox.Show("No se pudo obtener un ID válido.")
            End If
        Else
            MessageBox.Show("Por favor, seleccione un predio válido.")
        End If


    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem IsNot Nothing Then
            ' Obtener el texto seleccionado en el ComboBox
            Dim selectedText As String = ComboBox1.SelectedItem.ToString()

            ' Dividir el texto para obtener el ID (parte antes del guion)
            Dim idPart As String = selectedText.Split("-"c)(0).Trim()

            ' Convertir a entero
            Dim idpredio As Integer
            If Integer.TryParse(idPart, idpredio) Then
                FacturasPagos.ListarFacturasPagos(TextBox10, NombrePropietario, Telefono, Predio, matricula, DataGridView1, idpredio)
            Else
                ' Manejar el caso en que no se puede convertir a entero
                MessageBox.Show("No se pudo obtener un ID válido.")
            End If
        Else
            MessageBox.Show("Por favor, seleccione un predio válido.")
        End If
    End Sub

    Private Sub RealizarPago_Click(sender As Object, e As EventArgs) Handles RealizarPago.Click
        If ComboBox1.SelectedItem IsNot Nothing Then
            ' Obtener el texto seleccionado en el ComboBox
            Dim selectedText As String = ComboBox1.SelectedItem.ToString()

            ' Dividir el texto para obtener el ID (parte antes del guion)
            Dim idPart As String = selectedText.Split("-"c)(0).Trim()

            ' Convertir a entero
            Dim idpredio As Integer
            If Integer.TryParse(idPart, idpredio) Then
                FacturasPagos.generarPago(DataGridView1, idpredio, TextBox10.Text)
                ' FacturasPagos.generarPago(DataGridView1)
                FacturasPagos.ListarFacturasPagos(TextBox10, NombrePropietario, Telefono, Predio, matricula, DataGridView1, idpredio)
            Else
                ' Manejar el caso en que no se puede convertir a entero
                MessageBox.Show("No se pudo obtener un ID válido.")
            End If
        Else
            MessageBox.Show("Por favor, seleccione un predio válido.")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CerrarFormulario(Me)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If String.IsNullOrWhiteSpace(TextBox10.Text) AndAlso
                String.IsNullOrWhiteSpace(ComboBox1.SelectedValue) Then
            MsgBox("Seleccione un Usuario.")
            Return
        End If

        Dim pagosPendientes As Boolean = False

        For Each row As DataGridViewRow In DataGridView1.Rows
            ' Supongamos que la columna "Saldo" está en el índice 2 del DataGridView
            Dim saldo As Object = row.Cells(2).Value

            ' Si la celda de saldo no está vacía o tiene un valor mayor a 0
            If saldo IsNot Nothing AndAlso IsNumeric(saldo) AndAlso Convert.ToDecimal(saldo) > 0 Then
                pagosPendientes = True
                Exit For
            End If
        Next

        If pagosPendientes Then
            MsgBox("No se puede adelantar hasta que el usuario esté al día con los pagos.")
            Return
        End If


        Dim inputDialog As New Adelanto_Abonos()
        If inputDialog.ShowDialog() = DialogResult.OK Then
            Dim UserInput As String = inputDialog.TextBox1.Text

            If ComboBox1.SelectedItem IsNot Nothing Then
                ' Obtener el texto seleccionado en el ComboBox
                Dim selectedText As String = ComboBox1.SelectedItem.ToString()

                ' Dividir el texto para obtener el ID (parte antes del guion)
                Dim idPart As String = selectedText.Split("-"c)(0).Trim()

                ' Convertir a entero
                Dim idpredio As Integer
                If Integer.TryParse(idPart, idpredio) Then
                    FacturasPagos.AgregarSaldo(UserInput, idpredio)
                Else
                    ' Manejar el caso en que no se puede convertir a entero
                    MessageBox.Show("No se pudo obtener un ID válido.")
                End If
            Else
                MessageBox.Show("Por favor, seleccione un predio válido.")
            End If
        Else
            MessageBox.Show("Operación cancelada.")
        End If
    End Sub
End Class