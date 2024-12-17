Imports System.Data.SQLite
Imports System.Threading

Namespace Clases
    Friend Class PrediosController

        Friend Function RegistrarPredios(documento As String, nombrePredio As String, extensionPredio As String, veredaPredio As String, matriculaPredio As String, registroPredio As String, clasificacionUso As String, diamTuveria As Decimal, areasContruir As String, areasContruidas As String, servicioActivo As Boolean, observaciones As String, anyo As Integer, idPredio As String) As Integer
            Try
                Using connection As New SQLiteConnection(connectionString)
                    connection.Open()

                    ' Iniciar transacción
                    Using transaction = connection.BeginTransaction()
                        Try
                            ' Primera consulta: Insertar en Predios
                            Dim queryInsertPredios As String = "INSERT INTO Predios (Vereda, ServicioActivo, AreasContruidas, AreasPorConstruir, DiametroTuveria, NombrePredio, ExtencionTotal, Observaciones, NumeroMatricula, NumeroRegistroCatastral, Usuarios_NumeroCedula, ClasificacionUso_idClasificacionUso) 
                                           VALUES (@Vereda, @ServicioActivo, @AreaConstruida, @AreaPorConstruir, @DiametroTuberia, @NombrePredio, @ExtensionTotalPredio, @Observaciones, @NumeroMatricula, @NumeroRegistroCatastral, @Documento, @ClasifUso)"

                            Using command As New SQLiteCommand(queryInsertPredios, connection, transaction)
                                ' Asignar valores a los parámetros
                                command.Parameters.AddWithValue("@Vereda", veredaPredio)
                                command.Parameters.AddWithValue("@ServicioActivo", servicioActivo)
                                command.Parameters.AddWithValue("@AreaConstruida", areasContruidas)
                                command.Parameters.AddWithValue("@AreaPorConstruir", areasContruir)
                                command.Parameters.AddWithValue("@DiametroTuberia", diamTuveria)
                                command.Parameters.AddWithValue("@NombrePredio", nombrePredio)
                                command.Parameters.AddWithValue("@ExtensionTotalPredio", extensionPredio)
                                command.Parameters.AddWithValue("@Observaciones", observaciones)
                                command.Parameters.AddWithValue("@NumeroMatricula", matriculaPredio)
                                command.Parameters.AddWithValue("@NumeroRegistroCatastral", registroPredio)
                                command.Parameters.AddWithValue("@Documento", documento)
                                command.Parameters.AddWithValue("@ClasifUso", clasificacionUso)

                                Dim filasAfectadas As Integer = command.ExecuteNonQuery()

                                If filasAfectadas = 0 Then
                                    MsgBox("Ocurrió un error al insertar los datos.")
                                    transaction.Rollback()
                                    Return -1
                                End If
                            End Using

                            ' Segunda consulta: Seleccionar FinPeriodo
                            Dim queryPeriodo As String = "SELECT FinPeriodo FROM periodos WHERE idPeriodos = @anyo"
                            Dim continuar As Boolean = True

                            While continuar
                                Using cmd As New SQLiteCommand(queryPeriodo, connection, transaction)
                                    cmd.Parameters.Clear()
                                    cmd.Parameters.AddWithValue("@anyo", anyo)

                                    Using reader As SQLiteDataReader = cmd.ExecuteReader()
                                        If reader.Read() Then
                                            ' Verificar si FinPeriodo no es DBNull antes de convertir a DateTime
                                            If Not IsDBNull(reader("FinPeriodo")) Then
                                                Dim finPeriodoStr As String = reader("FinPeriodo").ToString()
                                                Dim finPeriodo As DateTime

                                                Dim inicio As DateTime = DateTime.Now

                                                ' Intentar convertir el valor a DateTime
                                                If DateTime.TryParse(finPeriodoStr, finPeriodo) Then
                                                    Dim mesesDiff As Integer = ((finPeriodo.Year - inicio.Year) * 12) + finPeriodo.Month - inicio.Month

                                                    For mesOffset As Integer = 0 To mesesDiff
                                                        ' Calcular el mes y la fecha de pago
                                                        Dim mesActual As DateTime = inicio.AddMonths(mesOffset)
                                                        Dim anyoActual As Integer = mesActual.Year
                                                        Dim mesNumero As Integer = mesActual.Month  ' Mes correspondiente de `mesActual`

                                                        ' Configurar la fecha de pago en el día 12 del siguiente mes
                                                        Dim fechaPago As DateTime
                                                        If mesNumero = 12 Then
                                                            fechaPago = New DateTime(anyoActual + 1, 1, 12)  ' Enero del próximo año
                                                        Else
                                                            fechaPago = New DateTime(anyoActual, mesNumero + 1, 12)  ' Mes siguiente dentro del mismo año
                                                        End If

                                                        ' Insertar la factura para cada idPredio y cada mes
                                                        Dim queryInsertFactura As String = "INSERT INTO factura (Predios_idPredios, Periodos_idPeriodos, mes, FechaPago) VALUES (@predio, @periodo, @mes, @FechaPago)"
                                                        Using cmdInsert As New SQLiteCommand(queryInsertFactura, connection, transaction)
                                                            cmdInsert.Parameters.AddWithValue("@predio", idPredio)
                                                            cmdInsert.Parameters.AddWithValue("@periodo", anyoActual)
                                                            cmdInsert.Parameters.AddWithValue("@mes", mesNumero)
                                                            cmdInsert.Parameters.AddWithValue("@FechaPago", fechaPago)
                                                            cmdInsert.ExecuteNonQuery()
                                                        End Using
                                                    Next

                                                    ' Avanzar al próximo año si hay uno en la base de datos
                                                    anyo += 1
                                                    queryPeriodo = "SELECT FinPeriodo FROM periodos WHERE idPeriodos = @anyo"
                                                Else
                                                    MsgBox("El valor de FinPeriodo no es una fecha válida.")
                                                    transaction.Rollback()
                                                    Return -1
                                                End If
                                            Else
                                                MsgBox("El campo FinPeriodo está vacío.")
                                                transaction.Rollback()
                                                Return -1
                                            End If
                                        Else
                                            ' Si no hay más periodos para el próximo año, detener el ciclo
                                            continuar = False
                                        End If
                                    End Using
                                End Using
                            End While



                            ' Confirmar la transacción
                            transaction.Commit()
                            MsgBox("Datos insertados con éxito.")
                            Return 1
                        Catch ex As Exception
                            ' Si ocurre un error, deshacer la transacción
                            transaction.Rollback()
                            MsgBox("Error al registrar predios: " & ex.Message)
                            Return -1
                        End Try
                    End Using
                End Using

            Catch ex As SQLiteException
                MsgBox("Error de base de datos: " & ex.Message)
                Return -1
            Catch ex As Exception
                MsgBox("Error al registrar predios: " & ex.Message)
                Return -1
            End Try
        End Function


        Friend Function BuscarPredios(txtNumeroCedula As TextBox, dgvPredios As DataGridView) As Integer
            Try
                ' Validar que el campo de cédula no esté vacío
                Dim cedulaUsuarioABuscar As String = txtNumeroCedula.Text.Trim()
                If String.IsNullOrEmpty(cedulaUsuarioABuscar) Then
                    MsgBox("Por favor ingrese una cédula válida.")
                    Return -1
                End If

                ' Establecer la conexión y realizar la consulta
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()
                    ' Modificar la consulta para mostrar "Activo" o "Inactivo"
                    Dim queryPredios As String = "SELECT idPredios AS código, 
                                                  Vereda, 
                                                  CASE 
                                                      WHEN ServicioActivo = 1 THEN 'Activo' 
                                                      ELSE 'Inactivo' 
                                                  END AS ServicioActivo, 
                                                  AreasContruidas as ÁreasConstruidas, 
                                                  AreasPorConstruir as ÁreasContruir, 
                                                  DiametroTuveria as DiametroTubería, 
                                                  NombrePredio, 
                                                  ExtencionTotal as Extensión, 
                                                  Observaciones, 
                                                  NumeroMatricula as NúmeroMatrícula,  
                                                  NumeroRegistroCatastral as NúmeroCatastral, 
                                                  Usuarios_NumeroCedula as NúmeroCedula
                                           FROM predios 
                                           WHERE Usuarios_NumeroCedula = @Cedula"
                    Using commandPredios As New SQLiteCommand(queryPredios, conn)
                        commandPredios.Parameters.AddWithValue("@Cedula", cedulaUsuarioABuscar)

                        ' Llenar el DataGridView con los datos obtenidos
                        Using adapter As New SQLiteDataAdapter(commandPredios)
                            Dim dataTable As New DataTable()
                            adapter.Fill(dataTable)

                            If dataTable.Rows.Count > 0 Then
                                dgvPredios.DataSource = dataTable
                                ' Establecer el color del texto de las celdas del DataGridView
                                dgvPredios.DefaultCellStyle.ForeColor = Color.Black

                                ' Establecer el color del texto para los encabezados de columna, si es necesario
                                dgvPredios.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black

                            Else
                                MsgBox("No se encontraron predios para el documento especificado.")
                                Return 0 ' No se encontraron registros
                            End If
                        End Using
                    End Using
                End Using

                ' Retornar éxito (1) si se completó la operación sin errores
                Return 1
            Catch ex As SQLiteException
                MsgBox("Error en la carga de Predios SQLERROR : " & ex.Message)
                Return -2 ' Código de error para problemas de base de datos
            Catch ex As Exception
                MsgBox("Ocurrió un error al buscar los predios: " & ex.Message)
                Return -3 ' Código de error para otros tipos de excepciones
            End Try
        End Function


        Friend Function SeleccionarPredio(cedula As TextBox, label24 As Label, txt_nomPredio As TextBox, textBox1 As TextBox, textBox2 As TextBox, textBox3 As TextBox, textBox4 As TextBox, comboBox1 As ComboBox, numericUpDown1 As NumericUpDown, textBox6 As TextBox, textBox7 As TextBox, radioButton1 As RadioButton, radioButton2 As RadioButton, textBox8 As TextBox, codigoPredioBuscar As String) As Integer
            Try
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()

                    Dim query As String = "SELECT idPredios, Vereda, ServicioActivo, AreasContruidas, AreasPorConstruir, DiametroTuveria, NombrePredio, ExtencionTotal, Observaciones, NumeroMatricula, NumeroRegistroCatastral, Usuarios_NumeroCedula, TipoDeUso FROM predios p JOIN clasificacionuso cu ON cu.idClasificacionUso = p.ClasificacionUso_idClasificacionUso WHERE Usuarios_NumeroCedula = @Cedula AND idPredios = @codigoPredioBuscar"

                    Using cmd As New SQLiteCommand(query, conn)
                        ' Asignar parámetros de consulta
                        cmd.Parameters.AddWithValue("@Cedula", cedula.Text)
                        cmd.Parameters.AddWithValue("@codigoPredioBuscar", codigoPredioBuscar)

                        Using reader As SQLiteDataReader = cmd.ExecuteReader()
                            If reader.Read() Then
                                ' Verificación y asignación de valores
                                label24.Text = If(IsDBNull(reader("idPredios")), "", reader("idPredios").ToString())
                                txt_nomPredio.Text = If(IsDBNull(reader("NombrePredio")), "", reader("NombrePredio").ToString())
                                textBox1.Text = If(IsDBNull(reader("ExtencionTotal")), "", reader("ExtencionTotal").ToString())
                                textBox2.Text = If(IsDBNull(reader("Vereda")), "", reader("Vereda").ToString())
                                textBox3.Text = If(IsDBNull(reader("NumeroMatricula")), "", reader("NumeroMatricula").ToString())
                                textBox4.Text = If(IsDBNull(reader("NumeroRegistroCatastral")), "", reader("NumeroRegistroCatastral").ToString())
                                comboBox1.SelectedItem = If(IsDBNull(reader("TipoDeUso")), "", reader("TipoDeUso").ToString())

                                ' Manejar valores numéricos
                                Dim diametroTuberia As Decimal
                                If Decimal.TryParse(reader("DiametroTuveria").ToString(), diametroTuberia) Then
                                    numericUpDown1.Value = diametroTuberia
                                Else
                                    numericUpDown1.Value = 0
                                End If
                                textBox6.Text = If(IsDBNull(reader("AreasPorConstruir")), "", reader("AreasPorConstruir").ToString())
                                textBox7.Text = If(IsDBNull(reader("AreasContruidas")), "", reader("AreasContruidas").ToString())
                                textBox8.Text = If(IsDBNull(reader("Observaciones")), "", reader("Observaciones").ToString())
                                Dim servicioActivo As Boolean = Convert.ToBoolean(If(IsDBNull(reader("ServicioActivo")), False, reader("ServicioActivo")))
                                radioButton1.Checked = servicioActivo
                                radioButton2.Checked = Not servicioActivo
                            End If
                        End Using
                    End Using
                    conn.Close()
                    Return 1
                End Using
            Catch ex As SQLiteException
                MsgBox("Error de base de datos al buscar los predios: " & ex.Message)
                Return -2 ' Código de error para problemas de base de datos
            Catch ex As Exception
                MsgBox("Ocurrió un error al buscar el predio: " & ex.Message)
                Return -1
            End Try
        End Function

        Friend Function ActualizarPredios(codigo As String, numCedula As String, nombrePredio As String, extensionPredio As String, veredaPredio As String, matriculaPredio As String, registroPredio As String, clasificacionUso As String, diamTuveria As Decimal, areasContruir As String, areasContruidas As String, servicioActivo As Boolean, observaciones As String, anyo As String) As Integer
            Try
                Using connection As New SQLiteConnection(connectionString)
                    connection.Open()

                    MsgBox(clasificacionUso)

                    ' Iniciar una transacción
                    Using transaction = connection.BeginTransaction()
                        Try
                            ' Consulta SQL para actualizar el predio
                            Dim query As String = "UPDATE predios 
                                           SET Vereda = @Vereda, 
                                               ServicioActivo = @ServicioActivo, 
                                               AreasContruidas = @AreaConstruida, 
                                               AreasPorConstruir = @AreaPorConstruir, 
                                               DiametroTuveria = @DiametroTuberia, 
                                               NombrePredio = @NombrePredio, 
                                               ExtencionTotal = @ExtensionTotalPredio, 
                                               Observaciones = @Observaciones, 
                                               NumeroMatricula = @NumeroMatricula, 
                                               NumeroRegistroCatastral = @NumeroRegistroCatastral, 
                                               ClasificacionUso_idClasificacionUso = @ClasifUso 
                                           WHERE idPredios = @Id AND Usuarios_NumeroCedula = @Cedula"

                            Using command As New SQLiteCommand(query, connection, transaction)
                                ' Asignar valores a los parámetros
                                command.Parameters.AddWithValue("@Id", codigo)
                                command.Parameters.AddWithValue("@Cedula", numCedula)
                                command.Parameters.AddWithValue("@Vereda", veredaPredio)
                                command.Parameters.AddWithValue("@ServicioActivo", servicioActivo)
                                command.Parameters.AddWithValue("@AreaConstruida", areasContruidas)
                                command.Parameters.AddWithValue("@AreaPorConstruir", areasContruir)
                                command.Parameters.AddWithValue("@DiametroTuberia", diamTuveria)
                                command.Parameters.AddWithValue("@NombrePredio", nombrePredio)
                                command.Parameters.AddWithValue("@ExtensionTotalPredio", extensionPredio)
                                command.Parameters.AddWithValue("@Observaciones", observaciones)
                                command.Parameters.AddWithValue("@NumeroMatricula", matriculaPredio)
                                command.Parameters.AddWithValue("@NumeroRegistroCatastral", registroPredio)
                                command.Parameters.AddWithValue("@ClasifUso", clasificacionUso)

                                ' Ejecutar la consulta y verificar si se afectaron filas
                                Dim filasAfectadas As Integer = command.ExecuteNonQuery()

                                If filasAfectadas = 0 Then
                                    MsgBox("No se encontró el predio con los datos proporcionados.")
                                    transaction.Rollback()
                                    Return -1
                                End If
                            End Using

                            ' Si el servicio está inactivo, actualizar el estado de las facturas
                            If servicioActivo = False Then
                                Dim queryFactura As String = "UPDATE factura 
                                                      SET estado = 4 
                                                      WHERE estado = 3 AND Predios_idPredios = @IdPredio"
                                Using cmd As New SQLiteCommand(queryFactura, connection, transaction)
                                    cmd.Parameters.AddWithValue("@IdPredio", codigo)

                                    Dim filasAfectadasFactura As Integer = cmd.ExecuteNonQuery()
                                    If filasAfectadasFactura = 0 Then
                                        MsgBox("No se encontraron facturas activas para actualizar.")
                                        transaction.Rollback()
                                    End If
                                End Using
                            Else
                                ' Obtener la fecha de fin de periodo
                                'Dim queryPeriodo As String = "SELECT FinPeriodo FROM periodos WHERE idPeriodos = @anyo"
                                Dim queryPeriodo As String = "SELECT max(FinPeriodo) FROM periodos"
                                Using cmd As New SQLiteCommand(queryPeriodo, connection)
                                    'cmd.Parameters.AddWithValue("@anyo", anyo)
                                    Using reader As SQLiteDataReader = cmd.ExecuteReader
                                        If reader.Read Then
                                            If Not IsDBNull(reader("max(FinPeriodo)")) Then
                                                Dim finPeriodo As DateTime = DateTime.Parse(reader("max(FinPeriodo)").ToString)
                                                Dim inicio As DateTime = DateTime.Now

                                                ' Ciclo desde la fecha actual hasta el fin del periodo
                                                While inicio <= finPeriodo
                                                    ' Actualizar facturas mes a mes
                                                    Dim queryUpdateTrue As String = "UPDATE factura 
                                                                              SET estado = 3 
                                                                              WHERE estado = 4 AND Predios_idPredios = @IdPredio AND mes = @mes"
                                                    Using cmdUpdate As New SQLiteCommand(queryUpdateTrue, connection, transaction)
                                                        cmdUpdate.Parameters.AddWithValue("@IdPredio", codigo)
                                                        cmdUpdate.Parameters.AddWithValue("@mes", inicio.Month)
                                                        cmdUpdate.ExecuteNonQuery()
                                                    End Using

                                                    ' Pasar al siguiente mes
                                                    inicio = inicio.AddMonths(1)
                                                End While
                                            End If
                                        End If
                                    End Using
                                End Using
                            End If

                            ' Confirmar la transacción
                            transaction.Commit()
                            MsgBox("Predio y facturas actualizados con éxito.")
                            Return 1
                        Catch ex As Exception
                            ' Si ocurre un error, deshacer la transacción
                            transaction.Rollback()
                            MsgBox("Ocurrió un error al actualizar el predio: " & ex.Message)
                            Return -1
                        End Try
                    End Using
                End Using
            Catch ex As SQLiteException
                MsgBox("Error de base de datos al actualizar el predio: " & ex.Message)
                Return -2 ' Código de error para problemas de base de datos
            Catch ex As Exception
                MsgBox("Ocurrió un error al actualizar el predio: " & ex.Message)
                Return -1
            End Try
        End Function



        Friend Function NuevoCodigo(label24 As Label) As Integer
            Try
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()
                    Dim siguienteCodigo As Integer = 1
                    Dim query As String = "SELECT MAX(idPredios) FROM Predios"
                    Using cmd As New SQLiteCommand(query, conn)
                        Dim codigo = cmd.ExecuteScalar()
                        If codigo IsNot DBNull.Value AndAlso codigo IsNot Nothing Then
                            siguienteCodigo = Convert.ToInt32(codigo) + 1
                            If label24.Text = siguienteCodigo.ToString Then
                                Return 2
                            End If
                            label24.Text = siguienteCodigo.ToString()
                            Return 1
                        End If
                    End Using
                    conn.Close()
                End Using
            Catch ex As SQLiteException
                MsgBox("Error de base de datos al buscar los predios: " & ex.Message)
                Return -2 ' Código de error para problemas de base de datos
            Catch ex As Exception
                MsgBox("Ocurrió un error al buscar el predio: " & ex.Message)
                Return -1
            End Try
        End Function

        Friend Sub EliminarPredios(label24 As TextBox, idPredio As String)
            Try
                Using connection As New SQLiteConnection(connectionString)
                    connection.Open()


                    'MsgBox($"doc {label24.Text} y el id {idPredio}")
                    ' Consulta SQL para eliminar el predio
                    Dim query As String = "DELETE FROM predios WHERE idPredios = @id AND Usuarios_NumeroCedula = @Documento"

                    Using cdm As New SQLiteCommand(query, connection)
                        ' Asignar los valores correctos a los parámetros
                        cdm.Parameters.AddWithValue("@id", idPredio) ' ID del predio
                        cdm.Parameters.AddWithValue("@Documento", label24.Text) ' Número de cédula del usuario

                        ' Ejecutar la consulta y verificar cuántas filas se vieron afectadas
                        Dim filasAfectadas As Integer = cdm.ExecuteNonQuery()

                        ' Verificar si se eliminó algún registro
                        If filasAfectadas > 0 Then
                            MsgBox("Eliminado exitosamente")
                        Else
                            MsgBox("No se eliminó ningún registro")
                        End If
                    End Using
                End Using
            Catch ex As SQLiteException
                MsgBox("Error de base de datos al eliminar el predio: " & ex.Message)
            Catch ex As Exception
                MsgBox("Ocurrió un error al eliminar el predio: " & ex.Message)
            End Try
        End Sub

        Friend Sub listarIDNombre(comboBox1 As ComboBox, textBox10 As TextBox, label As Label)
            Try
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()
                    Using cmd As New SQLiteCommand("SELECT pre.idPredios, pre.NombrePredio, us.Nombre FROM predios pre JOIN usuarios us ON us.NumeroCedula = Usuarios_NumeroCedula WHERE Usuarios_NumeroCedula = @cedula", conn)
                        cmd.Parameters.AddWithValue("@cedula", textBox10.Text)

                        ' Ejecutar la consulta
                        Using reader As SQLiteDataReader = cmd.ExecuteReader()
                            comboBox1.Items.Clear() ' Limpiar elementos anteriores del ComboBox
                            If reader.HasRows Then
                                While reader.Read()
                                    label.Text = reader.GetString(2)
                                    Dim id As Integer = reader.GetInt32(0)  ' Obtener el idPredios
                                    Dim nombre As String = reader.GetString(1)  ' Obtener el NombrePredio
                                    comboBox1.Items.Add($"{id} - {nombre}")  ' Añadir al ComboBox en el formato deseado
                                End While
                                comboBox1.SelectedIndex = 0 ' Seleccionar el primer elemento por defecto
                            Else
                                MessageBox.Show("No se encontraron predios para la identificación proporcionada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Ocurrió un error: " & ex.Message)
            End Try
        End Sub


    End Class
End Namespace
