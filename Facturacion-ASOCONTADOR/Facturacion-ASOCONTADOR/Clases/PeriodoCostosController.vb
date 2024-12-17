Imports System.Data.SQLite
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports SQLitePCL

Namespace Clases
    Friend Class PeriodoCostosController
        Friend Sub ListarClasificacionUso(comboBox1 As ComboBox)
            Try
                ' Limpiar ComboBox antes de llenarlo
                comboBox1.Items.Clear()
                ' Consultar la base de datos para obtener los tipos de uso
                Dim query As String = "SELECT idClasificacionUso, TipoDeUso FROM ClasificacionUso"
                Using connection As New SQLiteConnection(connectionString)
                    connection.Open()
                    Using command As New SQLiteCommand(query, connection)
                        Using reader As SQLiteDataReader = command.ExecuteReader()
                            While reader.Read()
                                ' Agregar cada tipo de uso al ComboBox
                                comboBox1.Items.Add(reader("idClasificacionUso").ToString() & " - " & reader("TipoDeUso").ToString())
                            End While
                        End Using
                    End Using
                End Using
                ' Seleccionar el primer elemento del ComboBox si hay elementos disponibles
                If comboBox1.Items.Count > 0 Then
                    comboBox1.SelectedIndex = 0
                    comboBox1.DropDownStyle = ComboBoxStyle.DropDownList
                End If
            Catch ex As Exception
                MsgBox($"Error al Listar Clasificaciones de uso: {ex}")
            End Try
        End Sub

        Friend Sub ListarOtrosCostos(comboBox2 As ComboBox)
            Try
                ' Limpiar ComboBox antes de llenarlo
                comboBox2.Items.Clear()
                ' Consultar la base de datos para obtener los tipos de uso
                Dim query As String = "SELECT idOtrosCostos, CostoAsociado FROM OtrosCostos"
                Using connection As New SQLiteConnection(connectionString)
                    connection.Open()
                    Using command As New SQLiteCommand(query, connection)
                        Using reader As SQLiteDataReader = command.ExecuteReader()
                            While reader.Read()
                                ' Agregar cada tipo de uso al ComboBox
                                comboBox2.Items.Add(reader("idOtrosCostos").ToString() & " - " & reader("CostoAsociado").ToString())
                            End While
                        End Using
                    End Using
                End Using
                ' Seleccionar el primer elemento del ComboBox si hay elementos disponibles
                If comboBox2.Items.Count > 0 Then
                    comboBox2.SelectedIndex = 0
                    comboBox2.DropDownStyle = ComboBoxStyle.DropDownList
                End If
            Catch ex As Exception
                MsgBox($"Error al Listar Otros costos: {ex}")
            End Try
        End Sub

        Friend Sub PeriodosRegistrados(dataGridView1 As DataGridView)
            ' Crear una conexión a la base de datos SQLite
            Using connection As New SQLiteConnection(connectionString)
                Try
                    ' Abrir la conexión
                    connection.Open()

                    ' Crear el comando SQL para seleccionar los datos
                    Dim query As String = " SELECT  p.idPeriodos as Periodo,   p.InicioPeriodo as Inicio, p.FinPeriodo as fin, p.FechaCreacion, cu.TipoDeUso, cf.valor as valor
                        FROM costosfijos cf 
                        JOIN periodos p ON p.idPeriodos = cf.Periodos_idPeriodos
                        JOIN clasificacionuso cu ON cf.ClasificacionUso_idClasificacionUso = cu.idClasificacionUso;
                        "
                    Using command As New SQLiteCommand(query, connection)
                        ' Crear un adaptador para llenar el DataTable
                        Dim adapter As New SQLiteDataAdapter(command)
                        Dim dataTable As New DataTable()

                        ' Llenar el DataTable con los datos de la base de datos
                        adapter.Fill(dataTable)

                        ' Asignar el DataTable como origen de datos para el DataGridView
                        dataGridView1.DataSource = dataTable
                    End Using
                Catch ex As Exception
                    ' Manejar cualquier error que pueda ocurrir
                    MessageBox.Show("Error al cargar los datos: " & ex.Message)
                End Try
            End Using
        End Sub

        Friend Sub AddTipoUso(tipoDeUso As String)
            Try
                ' Verificar si se ingresó un valor
                If Not String.IsNullOrWhiteSpace(tipoDeUso) Then
                    ' Ejecutar la sentencia SQL para insertar el nuevo tipo de uso en la tabla
                    Using connection As New SQLiteConnection(connectionString)
                        connection.Open()
                        Dim sql As String = "INSERT INTO ClasificacionUso (TipoDeUso) VALUES (@TipoDeUso)"
                        Using command As New SQLiteCommand(sql, connection)
                            command.Parameters.AddWithValue("@TipoDeUso", tipoDeUso)
                            command.ExecuteNonQuery()
                        End Using
                    End Using
                    MessageBox.Show("Clasificación de Uso guardado correctamente.")
                Else
                    MessageBox.Show("Debe ingresar una Clasificación de Uso.")
                End If
            Catch ex As Exception
                MsgBox($"Error en la base de datos: {ex}")
            End Try
        End Sub

        Friend Sub AddCostoAsociado(costoAsociado As String)
            Try
                ' Verificar si se ingresó un valor
                If Not String.IsNullOrWhiteSpace(costoAsociado) Then
                    ' Ejecutar la sentencia SQL para insertar el nuevo tipo de uso en la tabla
                    Using connection As New SQLiteConnection(connectionString)
                        connection.Open()
                        Dim sql As String = "INSERT INTO OtrosCostos (CostoAsociado) VALUES (@TipoCostoAsociado)"
                        Using command As New SQLiteCommand(sql, connection)
                            command.Parameters.AddWithValue("@TipoCostoAsociado", costoAsociado)
                            command.ExecuteNonQuery()
                        End Using
                    End Using

                    MessageBox.Show("Nuevos Costos guardado correctamente.")
                Else
                    MessageBox.Show("Debe ingresar un Costo.")
                End If
            Catch ex As Exception

            End Try
        End Sub

        Friend Sub AgregarPeriodoTransacciones(dateTimePicker1 As DateTimePicker, dateTimePicker2 As DateTimePicker, MaskedTextBox1 As MaskedTextBox, idPeriodo As Label, listBox1 As ListBox, listBox2 As ListBox)
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()

                ' Formatear fechas usando el Value del DateTimePicker
                Dim fecha As DateTime = DateTime.Now
                Dim fechaStrin As String = fecha.ToString("yyyy/MM/dd")
                Dim inicio As DateTime = dateTimePicker1.Value
                Dim fin As DateTime = dateTimePicker2.Value

                ' Comienza la transacción
                Using transaction = conn.BeginTransaction()
                    Try
                        ' Insertar el periodo en la tabla periodos
                        Using cmd As New SQLiteCommand("INSERT INTO periodos (idPeriodos, InicioPeriodo, FinPeriodo, FechaCreacion, interesMora) VALUES (@id, @InicioPeriodo, @FinPeriodo, @FechaCreacionPeriodo, @interesMora)", conn)
                            cmd.Parameters.AddWithValue("@id", If(String.IsNullOrEmpty(idPeriodo.Text), DBNull.Value, Convert.ToInt32(idPeriodo.Text)))
                            cmd.Parameters.AddWithValue("@InicioPeriodo", inicio.ToString("yyyy/MM/dd"))
                            cmd.Parameters.AddWithValue("@FinPeriodo", fin.ToString("yyyy/MM/dd"))
                            cmd.Parameters.AddWithValue("@FechaCreacionPeriodo", fechaStrin)
                            cmd.Parameters.AddWithValue("@interesMora", MaskedTextBox1.Text)
                            cmd.ExecuteNonQuery()
                        End Using

                        ' Obtener el ID del periodo recién creado
                        Dim idPeriodoGet As Long = conn.LastInsertRowId

                        ' Procesar los elementos seleccionados de listBox1 para la tabla costosfijos
                        For Each item As String In listBox1.Items
                            ' Dividir el ítem para extraer los valores
                            Dim parts() As String = item.Split("|"c)
                            Dim idPart As String = parts(0).Trim() ' ID: 1
                            Dim valorPart As String = parts(2).Trim() ' valor: 2000
                            Dim idClasificacionUso As Integer = Convert.ToInt32(idPart.Split(":"c)(1).Trim())
                            Dim valor As Decimal = Convert.ToDecimal(valorPart.Split(":"c)(1).Trim())

                            ' Insertar los datos en la tabla costosfijos
                            Using cmd As New SQLiteCommand("INSERT INTO costosfijos (valor, ClasificacionUso_idClasificacionUso, Periodos_idPeriodos) VALUES (@valor, @Clasificacion, @anyo)", conn)
                                cmd.Parameters.AddWithValue("@valor", valor)
                                cmd.Parameters.AddWithValue("@Clasificacion", idClasificacionUso)
                                cmd.Parameters.AddWithValue("@anyo", idPeriodoGet)
                                cmd.ExecuteNonQuery()
                            End Using
                        Next

                        ' Procesar los elementos seleccionados de listBox2 para la tabla valorotroscostos
                        For Each item As String In listBox2.Items
                            ' Dividir el ítem para extraer los valores
                            Dim parts() As String = item.Split("|"c)
                            Dim idPart As String = parts(0).Trim() ' ID: 1
                            Dim valorPart As String = parts(2).Trim() ' valor: 2000
                            Dim idOtrosCostos As Integer = Convert.ToInt32(idPart.Split(":"c)(1).Trim())
                            Dim valor As Decimal = Convert.ToDecimal(valorPart.Split(":"c)(1).Trim())

                            ' Insertar los datos en la tabla valorotroscostos
                            Using cmd As New SQLiteCommand("INSERT INTO valorotroscostos (valor, OtrosCostos_idOtrosCostos, Periodos_idPeriodos) VALUES (@valor, @Clasificacion, @anyo)", conn)
                                cmd.Parameters.AddWithValue("@valor", valor)
                                cmd.Parameters.AddWithValue("@Clasificacion", idOtrosCostos)
                                cmd.Parameters.AddWithValue("@anyo", idPeriodoGet)
                                cmd.ExecuteNonQuery()
                            End Using
                        Next

                        ' Calcular la cantidad de meses entre las fechas de inicio y fin
                        Dim mesesDiff As Integer = ((fin.Year - inicio.Year) * 12) + fin.Month - inicio.Month

                        ' Obtener todos los idPredios de la tabla predios
                        Dim query As String = "SELECT idPredios FROM predios WHERE ServicioActivo != 0"
                        Using cmdSelect As New SQLiteCommand(query, conn)
                            Using reader As SQLiteDataReader = cmdSelect.ExecuteReader()
                                While reader.Read()
                                    ' Obtener el idPredios actual
                                    Dim idpredio As Integer = Convert.ToInt32(reader("idPredios"))

                                    ' Insertar una factura por cada mes en el rango
                                    For mesOffset As Integer = 0 To mesesDiff
                                        ' Calcular el mes y la fecha de pago
                                        Dim mesActual As DateTime = inicio.AddMonths(mesOffset)
                                        Dim mesNumero As Integer = mesActual.Month

                                        Dim FechaPago As Date = New Date(idPeriodoGet, mesNumero, 12)

                                        ' Insertar la factura para cada idPredio y cada mes
                                        Using cmdInsert As New SQLiteCommand("INSERT INTO factura (Predios_idPredios, Periodos_idPeriodos, mes, FechaPago) VALUES (@predio, @periodo, @mes,  @FechaPago)", conn)
                                            cmdInsert.Parameters.AddWithValue("@predio", idpredio)
                                            cmdInsert.Parameters.AddWithValue("@periodo", idPeriodoGet)
                                            cmdInsert.Parameters.AddWithValue("@mes", mesNumero)
                                            cmdInsert.Parameters.AddWithValue("@FechaPago", FechaPago)
                                            cmdInsert.ExecuteNonQuery()
                                        End Using
                                    Next
                                End While
                            End Using
                        End Using

                        ' Confirmar la transacción
                        transaction.Commit()
                        MsgBox("Registros Completados")

                    Catch ex As Exception
                        ' Si hay un error, revertir la transacción
                        transaction.Rollback()
                        MsgBox("Error al registrar los datos: " & ex.Message)
                    End Try
                End Using
            End Using
        End Sub

        Friend Sub FormatoDateTimePicker(dateTimePicker As DateTimePicker)
            With dateTimePicker
                .Format = DateTimePickerFormat.Custom
                .CustomFormat = "dd ' de' MMMM 'de ' yyyy"
            End With
        End Sub

        Friend Sub verificarPeriodo(now As Date)
            Dim anyo As Integer = now.Year
            Dim mes As Integer = now.Month
            Try
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()

                    ' Primero intentamos buscar el periodo del año siguiente
                    Using cmd As New SQLiteCommand("SELECT FinPeriodo FROM periodos WHERE idPeriodos = @anyo", conn)
                        cmd.Parameters.AddWithValue("@anyo", anyo + 1)
                        Using reader As SQLiteDataReader = cmd.ExecuteReader()
                            If reader.Read() AndAlso Not IsDBNull(reader("FinPeriodo")) Then
                                ' Si encontramos el periodo del año siguiente, salimos del método
                                Return
                            End If
                        End Using
                    End Using

                    ' Si no existe un periodo del año siguiente, buscamos el del año actual
                    Using command As New SQLiteCommand("SELECT FinPeriodo FROM periodos WHERE idPeriodos = @anyo", conn)
                        command.Parameters.AddWithValue("@anyo", anyo)
                        Using leader As SQLiteDataReader = command.ExecuteReader()
                            If leader.Read() AndAlso Not IsDBNull(leader("FinPeriodo")) Then
                                Dim fechaFinPeriodo As DateTime
                                If DateTime.TryParse(leader("FinPeriodo").ToString(), fechaFinPeriodo) Then
                                    Dim mesPeriodo As Integer = fechaFinPeriodo.Month
                                    If mesPeriodo >= mes Then
                                        MsgBox("Se recomienda crear un nuevo Periodo.")
                                    End If
                                Else
                                    MsgBox("El formato de 'FinPeriodo' no es válido.")
                                End If
                            Else
                                MsgBox("No se encontró un periodo para el año actual.")
                            End If
                        End Using
                    End Using

                End Using
            Catch ex As Exception
                MsgBox("Error al ejecutar la base de datos: " & ex.Message)
            End Try
        End Sub



        Friend Function CalcularDiferencias(dateTimePicker1 As DateTimePicker, dateTimePicker2 As DateTimePicker, lbdiferencia As Label, groupBox2 As Windows.Forms.GroupBox, groupBox4 As Windows.Forms.GroupBox) As Integer
            Try
                ' Verifica si los DateTimePicker tienen valores válidos
                If dateTimePicker1 Is Nothing OrElse dateTimePicker2 Is Nothing Then
                    MessageBox.Show("Por favor, asegúrate de que ambos DateTimePickers estén configurados correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return -1
                End If

                ' Obtén el año del primer DateTimePicker
                Dim yearPicker1 As Integer = dateTimePicker1.Value.Year

                ' Establece el año de dateTimePicker2 al año de dateTimePicker1, manteniendo mes y día actuales
                dateTimePicker2.Value = New DateTime(yearPicker1, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day)

                ' Compara las fechas
                If dateTimePicker2.Value >= dateTimePicker1.Value Then
                    ' Calcula la diferencia entre las dos fechas en días
                    Dim diferencia As TimeSpan = dateTimePicker2.Value - dateTimePicker1.Value
                    Dim dias As Integer = CInt(diferencia.TotalDays)

                    ' Muestra la diferencia en días en el Label
                    lbdiferencia.Text = "El periodo actual comprende: " & dias.ToString() & " días"
                    groupBox2.Enabled = True
                    groupBox4.Enabled = True
                    Return 1
                Else
                    ' Si la fecha final es menor que la inicial, muestra un mensaje de error
                    MessageBox.Show("La fecha final no puede ser anterior a la fecha inicial", "Error de fecha", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    dateTimePicker2.Value = New DateTime(dateTimePicker1.Value.Year, 12, 31)
                    ' Establece el texto del Label en "0"
                    Dim diferencia As TimeSpan = dateTimePicker2.Value - dateTimePicker1.Value
                    Dim dias As Integer = CInt(diferencia.TotalDays)

                    ' Muestra la diferencia en días en el Label
                    lbdiferencia.Text = "El periodo actual comprende: " & dias.ToString() & " días"
                    Return -1
                End If

            Catch ex As Exception
                ' Captura excepciones inesperadas
                MessageBox.Show("Ocurrió un error al calcular la diferencia de fechas: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                lbdiferencia.Text = "El periodo actual comprende: 0 días"
                Return -3
            End Try
        End Function


    End Class
End Namespace
