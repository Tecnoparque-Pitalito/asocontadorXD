Public Class Adelanto_Abonos
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        InputValue = TextBox1.Text ' Almacenar el valor ingresado
        Me.DialogResult = DialogResult.OK ' Establecer el resultado del diálogo
        Me.Close() ' Cerrar el formulario
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel ' Establecer el resultado del diálogo
        Me.Close() ' Cerrar el formulario
    End Sub

    Private Sub Adelanto_Abonos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RedondearEsquinasFormulario(Me, 20)
    End Sub
End Class