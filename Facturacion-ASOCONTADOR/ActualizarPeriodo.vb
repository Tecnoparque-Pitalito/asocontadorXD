Imports System.Data.SQLite
Imports com.itextpdf.text.pdf

Public Class ActualizarPeriodo
    Private tabla As String
    Private columna As String
    Public anyo As String
    Dim identificador As String

    Dim periodos As New Clases.PeriodoCostosController

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim update As Boolean = periodos.actualizarPeriodos(tabla, identificador, TextBox1.Text, columna)
    End Sub

    Private Sub ActualizarPeriodo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RedondearEsquinasFormulario(Me, 20)
        periodos.listarijos(ComboBox4, anyo)
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        If ComboBox4.SelectedItem IsNot Nothing Then
            Try
                ' Obtiene el valor del KeyValuePair seleccionado.
                Dim selectedItem As KeyValuePair(Of String, Integer) = CType(ComboBox4.SelectedItem, KeyValuePair(Of String, Integer))
                Dim idCosto As Integer = selectedItem.Value
                identificador = idCosto.ToString()

                ' Verifica que los valores de tabla y columna estén configurados.
                If String.IsNullOrEmpty(tabla) OrElse String.IsNullOrEmpty(columna) Then
                    MsgBox("Por favor, selecciona un tipo de tabla válido en el ComboBox1.")
                    Return
                End If

                ' Realiza la consulta y actualiza la lista.
                Dim consultaExitosa As Boolean = realizarConsulta(TextBox1, idCosto, tabla, columna)
                If Not consultaExitosa Then
                    MsgBox("La consulta no devolvió resultados.")
                End If
            Catch ex As Exception
                MsgBox("Error al manejar la selección: " & ex.Message)
            End Try
        End If
    End Sub

    Private Function realizarConsulta(textBox1 As TextBox, idCosto As Integer, tabla As String, columna As String) As Boolean
        Try
            ' Validar que los nombres de la tabla y la columna no estén vacíos
            If String.IsNullOrWhiteSpace(tabla) OrElse String.IsNullOrWhiteSpace(columna) Then
                MsgBox("El nombre de la tabla o columna no puede estar vacío.")
                Return False
            End If

            Using conn As New SQLiteConnection(connectionString)
                conn.Open()

                ' Construir la consulta SQL
                Dim query As String = $"SELECT valor FROM {tabla} WHERE {columna} = @IdCosto and Periodos_idPeriodos = @anyo"
                Using cmd As New SQLiteCommand(query, conn)
                    ' Agregar parámetros para evitar inyección SQL
                    cmd.Parameters.AddWithValue("@IdCosto", idCosto)
                    cmd.Parameters.AddWithValue("@anyo", anyo)
                    Using reader As SQLiteDataReader = cmd.ExecuteReader()
                        If reader.HasRows Then
                            textBox1.Clear() ' Limpiar el contenido actual del TextBox
                            While reader.Read()
                                ' Asignar los resultados al TextBox, concatenando los valores encontrados
                                textBox1.AppendText(reader("valor").ToString() & Environment.NewLine)
                            End While
                            Return True
                        Else
                            ' Mostrar mensaje si no hay resultados
                            MsgBox("No se encontraron resultados para el ID especificado.")
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Manejo de excepciones
            MsgBox("Error al realizar la consulta: " & ex.Message)
        End Try

        Return False
    End Function



    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 0 Then
            tabla = "costosfijos"
            columna = "idCostos"
            periodos.listarijos(ComboBox4, anyo)
        ElseIf ComboBox1.SelectedIndex = 1 Then
            tabla = "valorotroscostos"
            columna = "idValorOtrosCostos"
            periodos.listarAsociados(ComboBox4, anyo)
        Else
            MsgBox("Selecciona un valor válido en el ComboBox.")
            tabla = String.Empty
            columna = String.Empty
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Try
            ' Obtener el texto del TextBox
            Dim currentText As String = TextBox1.Text

            ' Verificar si el texto contiene solo caracteres numéricos
            If Not IsNumeric(currentText) AndAlso currentText <> "" Then
                ' Si no es numérico, mostrar un mensaje o manejar el caso
                MsgBox("El texto ingresado no es numérico.")
                ' Opcional: eliminar el último carácter ingresado
                TextBox1.Text = currentText.Substring(0, currentText.Length - 1)
                TextBox1.SelectionStart = TextBox1.Text.Length ' Mantener el cursor al final
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try
    End Sub

End Class
