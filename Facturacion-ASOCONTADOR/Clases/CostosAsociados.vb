Imports System.Data.SQLite
Imports System.Text.RegularExpressions

Namespace Clases
    Friend Class CostosAsociados
        Friend Sub AgregarCosto(dataGridView As DataGridView, usuario As String, predio As String, valor As String, fecha As String)
            Try
                ' Verifica si ya existe el valor en la columna "Valor"
                Dim exists As Boolean = dataGridView.Rows.Cast(Of DataGridViewRow)().Any(Function(row) row.Cells("Valor").Value?.ToString() = valor)

                If Not exists Then
                    ' Agregar una nueva fila al DataGridView
                    Dim newRow As String() = {usuario, predio, valor, fecha}
                    dataGridView.Rows.Add(newRow)
                Else
                    ' Muestra un mensaje de alerta si el valor ya existe
                    MessageBox.Show("El valor ya existe en la lista.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Sub

        Friend Sub ConfirmarCosto(dataGridView1 As DataGridView)
            Try
                Dim datosToInsert As String = String.Empty
                Dim mesExtracted As Integer
                Dim anyoExtracted As Integer
                Dim predio As String = String.Empty ' Inicializar predio como vacío
                ' Recorre cada fila en el DataGridView
                For Each row As DataGridViewRow In dataGridView1.Rows
                    ' Ignora la fila nueva si está vacía
                    If Not row.IsNewRow Then
                        Dim usuario As String = If(row.Cells("Usuario").Value IsNot Nothing, row.Cells("Usuario").Value.ToString(), "N/A")
                        predio = If(row.Cells("Predio").Value IsNot Nothing, row.Cells("Predio").Value.ToString(), "N/A")
                        Dim valor As String = If(row.Cells("Valor").Value IsNot Nothing, row.Cells("Valor").Value.ToString(), "N/A")
                        Dim fecha As String = If(row.Cells("Fecha").Value IsNot Nothing, row.Cells("Fecha").Value.ToString(), "N/A")
                        Dim parsedDate As DateTime

                        If DateTime.TryParse(fecha, parsedDate) Then
                            mesExtracted = parsedDate.Month
                            anyoExtracted = parsedDate.Year
                        Else
                            MsgBox("Error en interpretar las fechas.")
                            Return ' Salir de la función si hay un error en la fecha
                        End If

                        ' Concatenar los valores en un formato legible
                        datosToInsert &= String.Format("Valor: {0}, Fecha: {1} |{2}", valor, fecha, Environment.NewLine)

                    End If
                Next

                ' Extraer solo el número del predio
                Dim idPredio As Integer
                Dim match As Match = Regex.Match(predio, "^\d+")
                If match.Success Then
                    idPredio = Integer.Parse(match.Value) ' Convertir el valor extraído a entero
                Else
                    MessageBox.Show("Formato de predio inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return ' Salir de la función si el formato es inválido
                End If

                ' Mostrar los datos en un MessageBox
                If String.IsNullOrEmpty(datosToInsert) Then
                    MessageBox.Show("No hay datos para guardar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    Using conn As New SQLiteConnection(connectionString)
                        conn.Open()
                        Using cdm As New SQLiteCommand("UPDATE factura SET addCostosAsociados = @texto WHERE mes = @mes AND Periodos_idPeriodos = @anyo AND Predios_idPredios = @idPredio", conn)
                            cdm.Parameters.AddWithValue("@texto", datosToInsert)
                            cdm.Parameters.AddWithValue("@mes", mesExtracted)
                            cdm.Parameters.AddWithValue("@anyo", anyoExtracted)
                            cdm.Parameters.AddWithValue("@idPredio", idPredio)

                            ' Ejecutar la consulta
                            Dim rowsAffected As Integer = cdm.ExecuteNonQuery()
                            If rowsAffected = 0 Then
                                MessageBox.Show("No se encontró ningún registro para actualizar con los criterios proporcionados.")
                            Else
                                MsgBox("Actualizado correctamente")
                            End If
                        End Using
                    End Using
                End If
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Friend Sub ListarCostos(dataGridView1 As DataGridView, comboBox1 As String, dateTimePicker1 As DateTimePicker)
            Try
                dataGridView1.Rows.Clear()

                ' Extraer el idPredio del comboBox1 en formato "1 - texto"
                Dim predioText As String = comboBox1
                Dim idPredio As Integer = Integer.Parse(predioText.Split("-"c)(0).Trim())

                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()
                    Using cdm As New SQLiteCommand("SELECT us.NumeroCedula, pre.idPredios, fac.addCostosAsociados, pre.NombrePredio FROM factura fac JOIN predios pre ON fac.Predios_idPredios = pre.idPredios JOIN usuarios us ON pre.Usuarios_NumeroCedula = us.NumeroCedula WHERE mes = @mes AND Periodos_idPeriodos = @anyo AND Predios_idPredios = @idPredio", conn)
                        cdm.Parameters.AddWithValue("@mes", dateTimePicker1.Value.Month)
                        cdm.Parameters.AddWithValue("@anyo", dateTimePicker1.Value.Year)
                        cdm.Parameters.AddWithValue("@idPredio", idPredio)

                        ' Ejecutar la consulta y manejar los resultados
                        Using reader As SQLiteDataReader = cdm.ExecuteReader()
                            If reader.HasRows Then
                                While reader.Read()
                                    ' Obtener los valores necesarios
                                    Dim numeroCedula As String = reader("NumeroCedula").ToString()
                                    Dim idPredioValue As String = reader("idPredios").ToString() & " - " & reader("NombrePredio").ToString()
                                    Dim addCostos As String = If(reader("addCostosAsociados") IsNot DBNull.Value, reader("addCostosAsociados").ToString(), "")

                                    ' Ignorar si addCostosAsociados está vacío o no contiene datos relevantes
                                    If String.IsNullOrWhiteSpace(addCostos) Then
                                        Continue While
                                    End If

                                    ' Dividir addCostos en registros individuales usando el delimitador "|"
                                    Dim registros As String() = addCostos.Split(New String() {"|"}, StringSplitOptions.RemoveEmptyEntries)

                                    For Each registro As String In registros
                                        ' Extraer el valor y la fecha de cada registro
                                        Dim valor As String = ""
                                        Dim fecha As String = ""

                                        ' Separar el texto por comas y extraer el valor y la fecha
                                        Dim partes As String() = registro.Split(","c)
                                        If partes.Length > 0 Then
                                            valor = partes(0).Replace("Valor: ", "").Trim()
                                        End If
                                        If partes.Length > 1 Then
                                            Dim fechaPart As String = partes(1).Trim() ' Ejemplo: Fecha: 29/10/2024
                                            fecha = fechaPart.Split(":"c)(1).Trim() ' Extraer solo la fecha
                                        End If

                                        ' Verificar si tanto valor como fecha no están vacíos antes de agregar al DataGridView
                                        If Not String.IsNullOrEmpty(valor) AndAlso Not String.IsNullOrEmpty(fecha) Then
                                            dataGridView1.Rows.Add(numeroCedula, idPredioValue, valor, fecha)
                                        End If
                                    Next
                                End While
                            Else
                                MessageBox.Show("No se encontraron registros.")
                            End If
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error en llamar los datos: " & ex.Message)
            End Try
        End Sub

        Friend Sub LimpiarCostoAsociado(datetime As DateTimePicker, comboBox As ComboBox)

            Dim datosToInsert As Object = DBNull.Value ' Variable vacía para la base de datos
            Dim mesExtracted As Integer = datetime.Value.Month ' Extraer el mes del DateTimePicker
            Dim anyoExtracted As Integer = datetime.Value.Year ' Extraer el año del DateTimePicker
            Dim idPredio As String = String.Empty ' Inicializar ID del predio como vacío

            Try
                ' Validar si hay una selección en el ComboBox
                If comboBox.SelectedIndex <> -1 Then
                    ' Extraer el ID del predio antes del guion
                    idPredio = comboBox.SelectedItem.ToString().Split("-"c)(0).Trim()
                Else
                    MessageBox.Show("Por favor, selecciona un predio válido en el ComboBox.")
                    Return
                End If

                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()
                    Using cdm As New SQLiteCommand("UPDATE factura SET addCostosAsociados = @texto WHERE mes = @mes AND Periodos_idPeriodos = @anyo AND Predios_idPredios = @idPredio", conn)
                        ' Asignar parámetros al comando
                        cdm.Parameters.AddWithValue("@texto", datosToInsert)
                        cdm.Parameters.AddWithValue("@mes", mesExtracted)
                        cdm.Parameters.AddWithValue("@anyo", anyoExtracted)
                        cdm.Parameters.AddWithValue("@idPredio", idPredio)

                        ' Ejecutar el comando
                        Dim rowsAffected As Integer = cdm.ExecuteNonQuery()
                        If rowsAffected = 0 Then
                            MessageBox.Show("No se encontró ningún registro para actualizar con los criterios proporcionados.")
                        Else
                            MessageBox.Show("Actualizado correctamente.")
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show($"Error al actualizar la base de datos: {ex.Message}")
                Return
            End Try
        End Sub

    End Class
End Namespace
