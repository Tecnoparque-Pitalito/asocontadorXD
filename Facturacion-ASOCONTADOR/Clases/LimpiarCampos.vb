Namespace Clases
    Friend Class LimpiarCampos
        Public Sub LimpiarTextBoxes(ParamArray textBoxes() As TextBox)
            For Each txt In textBoxes
                txt.Clear() ' Limpia el contenido del TextBox
            Next
        End Sub

        Public Sub LimpiarComboBoxes(ParamArray comboBoxes() As ComboBox)
            For Each cmb In comboBoxes
                cmb.SelectedIndex = -1 ' Deselecciona cualquier valor
                'cmb.Text = "" ' Limpia el texto mostrado
            Next
        End Sub

        Public Sub LimpiarNumericUpDowns(ParamArray numericUpDowns() As NumericUpDown)
            For Each nud In numericUpDowns
                nud.Value = 0 ' Establece el valor en 0
            Next
        End Sub

        Public Sub LimpiarControles(ParamArray controles() As Control)
            For Each ctrl In controles
                If TypeOf ctrl Is TextBox Then
                    CType(ctrl, TextBox).Clear() ' Limpia el contenido del TextBox
                ElseIf TypeOf ctrl Is MaskedTextBox Then
                    CType(ctrl, MaskedTextBox).Clear() ' Limpia el contenido del MaskedTextBox
                End If
            Next
        End Sub



    End Class


End Namespace
