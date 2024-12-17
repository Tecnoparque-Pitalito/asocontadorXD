Imports System.Data.SQLite
Imports System.IO
Imports System.Security.Cryptography
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports SQLitePCL

Namespace Clases
    Friend Class FacturasGeneratorController

        Dim valorPeriodo As Decimal
        Dim listaDePersonas As New List(Of Persona)()


        Friend Sub ConstosAdicionales(comboBox1 As ComboBox, anyo As Integer)
            Try
                ' Limpiar los items del ComboBox antes de agregar los nuevos
                comboBox1.Items.Clear()

                ' Crear la conexión a la base de datos
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()

                    ' Crear el comando SQL para obtener los valores de otros costos
                    Using cmd As New SQLiteCommand("SELECT VOC.valor, OC.CostoAsociado FROM valorotroscostos VOC JOIN otroscostos OC ON VOC.OtrosCostos_idOtrosCostos = OC.idOtrosCostos WHERE Periodos_idPeriodos = @periodo", conn)
                        ' Asignar el valor del año al parámetro @periodo
                        cmd.Parameters.AddWithValue("@periodo", anyo)

                        ' Ejecutar el comando y leer los resultados
                        Using reader As SQLiteDataReader = cmd.ExecuteReader()
                            ' Verificar si hay resultados
                            If reader.HasRows Then
                                ' Recorrer los resultados y agregarlos al ComboBox
                                While reader.Read()
                                    ' Concatenar valor y costo asociado para mostrar en el ComboBox
                                    Dim item As String = $"{reader("valor")} - {reader("CostoAsociado")}"
                                    comboBox1.Items.Add(item)
                                End While

                                ' Seleccionar el primer ítem por defecto si hay elementos
                                If comboBox1.Items.Count > 0 Then
                                    comboBox1.SelectedIndex = 0
                                End If
                            Else
                                ' Si no hay resultados, mostrar un mensaje de advertencia
                                MsgBox($"No se encontraron costos adicionales para el período {anyo}")
                            End If
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                ' Mostrar el error si ocurre algún problema
                MsgBox($"Error al obtener costos adicionales: {ex.Message}")
            End Try
        End Sub

        Friend Sub VistaDeFacturaXD(nombreCliente As Label, cedulaCliente As Label, telefonoCliente As Label, nombrePredio As Label, veredaPredio As Label, numeroMatricula As Label, facturaID As Label, periodoCobro As Label, fechaFin As Label, dataGridView1 As DataGridView, datagridview2 As DataGridView, dataGridView3 As DataGridView, observaciones As Label, textBox2 As TextBox, Nomperiodo As Integer, textbox3 As TextBox, anyo As Integer, mes As Integer, textbox1 As TextBox)
            MsgBox(mes)
            Try
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()
                    Using cmd As New SQLiteCommand("SELECT 
                 fac.estado,
                 pre.NombrePredio, 
                 pre.Vereda, 
                 cu.TipoDeUso,
                 pre.NumeroMatricula, 
                 us.Nombre,
                 us.telefono,
                 cf.valor AS ValorCostoFijo, 
                 fac.idFactura
             FROM
                 predios pre
             JOIN
                 usuarios us ON pre.Usuarios_NumeroCedula = us.NumeroCedula
             JOIN
                 factura fac ON fac.Predios_idPredios = pre.idPredios 
             JOIN
                 clasificacionuso cu ON cu.idClasificacionUso = pre.ClasificacionUso_idClasificacionUso   
             JOIN
                 periodos per ON fac.Periodos_idPeriodos = per.idPeriodos 
             JOIN
                 costosfijos cf ON cf.ClasificacionUso_idClasificacionUso = pre.ClasificacionUso_idClasificacionUso   
                 AND cf.Periodos_idPeriodos = per.idPeriodos 
             WHERE 
                 fac.mes = @mes 
                 AND per.idPeriodos = @anyo 
                 AND us.NumeroCedula = @cedula
                 AND pre.idPredios = @predio;", conn)

                        ' Asignar parámetros
                        cmd.Parameters.AddWithValue("@mes", mes)
                        cmd.Parameters.AddWithValue("@anyo", anyo)
                        cmd.Parameters.AddWithValue("@cedula", textBox2.Text)
                        cmd.Parameters.AddWithValue("@predio", Nomperiodo)

                        Using reader As SQLiteDataReader = cmd.ExecuteReader()
                            If reader.Read() Then
                                ' Verificar el valor de estado
                                Dim estado As Integer = Convert.ToInt32(reader("estado"))

                                If estado = 4 Then
                                    MessageBox.Show("Predio Inactivo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    Exit Sub ' Detener la ejecución del código
                                ElseIf estado = 2 Then
                                    MessageBox.Show("Pagos al día", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub ' Detener la ejecución del código
                                End If

                                ' Asignar valores a los labels si el estado no interrumpe la ejecución
                                nombreCliente.Text = "Cliente: " & reader("Nombre").ToString()
                                cedulaCliente.Text = "Identificación: " & textBox2.Text
                                telefonoCliente.Text = "Teléfono: " & reader("Telefono").ToString()
                                nombrePredio.Text = "Nombre del Predio: " & reader("NombrePredio").ToString()
                                veredaPredio.Text = "Vereda: " & reader("Vereda").ToString()
                                numeroMatricula.Text = "Número de Matrícula: " & reader("NumeroMatricula").ToString()
                                facturaID.Text = "ID de Factura: " & reader("idFactura").ToString()
                                periodoCobro.Text = "Periodo de Cobro: " & mes.ToString() & "/" & anyo.ToString()
                                fechaFin.Text = "Fecha Fin: " & DateTime.Now.ToString("dd/MM/yyyy")

                                ' Configuración del DataGridView1
                                With dataGridView1
                                    .Rows.Clear()
                                    .Columns.Clear()
                                    .Columns.Add("Descripcion", "Descripción")
                                    .Columns.Add("ValorTipoUso", "Valor Tipo de Uso")
                                    .AllowUserToAddRows = False
                                    .RowHeadersVisible = False
                                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                                    ' Agregar la fila con los datos del predio y el tipo de uso
                                    .Rows.Add(reader("NombrePredio").ToString() & " - " & reader("TipoDeUso").ToString(), reader("ValorCostoFijo").ToString())
                                End With
                            Else
                                MessageBox.Show("No se encontró la factura para los parámetros proporcionados.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            End If
                        End Using
                    End Using
                End Using

                ' Procesar las líneas del TextBox3 y agregarlas a datagridview2
                Dim texto As String = textbox3.Text
                Dim lineas() As String = texto.Split(New String() {Environment.NewLine}, StringSplitOptions.None)

                ' Limpiar y configurar las columnas del DataGridView2
                With datagridview2
                    .Rows.Clear()
                    .Columns.Clear()
                    .Columns.Add("descripcion", "Descripción")
                    .Columns.Add("valor", "Valor")
                    .AllowUserToAddRows = False
                    .RowHeadersVisible = False
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                End With

                Dim tieneDatos As Boolean = False ' Indicador para saber si se agregaron valores válidos

                ' Procesar cada línea del TextBox
                For Each linea As String In lineas
                    If Not String.IsNullOrWhiteSpace(linea) Then
                        Dim partes() As String = linea.Split("-"c)

                        If partes.Length = 2 Then
                            ' Quitar posibles espacios en blanco alrededor de las partes
                            Dim valor As String = If(String.IsNullOrWhiteSpace(partes(0).Trim()), "NULL", partes(0).Trim())
                            Dim descripcion As String = If(String.IsNullOrWhiteSpace(partes(1).Trim()), "NULL", partes(1).Trim())

                            ' Agregar la fila solo si hay datos en alguna de las partes
                            If valor <> "NULL" Or descripcion <> "NULL" Then
                                datagridview2.Rows.Add(descripcion, valor)
                                tieneDatos = True ' Se agregaron datos válidos
                            End If
                        End If
                    End If
                Next

                ' Si no se ingresaron valores válidos, agregar una fila con NULLs
                If Not tieneDatos Then
                    datagridview2.Rows.Add("NULL", 0)
                End If

                ' Calcular el total de los DataGridView1 y DataGridView2
                Dim total As Decimal = 0

                ' Sumar valores del DataGridView1
                For Each row As DataGridViewRow In dataGridView1.Rows
                    If row.Cells("ValorTipoUso").Value IsNot Nothing Then
                        total += Convert.ToDecimal(row.Cells("ValorTipoUso").Value)
                    End If
                Next

                ' Sumar valores del DataGridView2
                For Each row As DataGridViewRow In datagridview2.Rows
                    If row.Cells("valor").Value IsNot Nothing Then
                        total += Convert.ToDecimal(row.Cells("valor").Value)
                    End If
                Next

                ' Mostrar el total en DataGridView3
                With dataGridView3
                    ' Limpiar el DataGridView
                    .Rows.Clear()
                    .Columns.Clear()

                    ' Agregar una sola columna para mostrar "Total: valor"
                    .Columns.Add("Total", "") ' La columna no necesita encabezado
                    .Columns.Add("Descripcion", "") ' La columna no necesita encabezado

                    ' Deshabilitar agregar filas y encabezados
                    .AllowUserToAddRows = False
                    .RowHeadersVisible = False
                    .ColumnHeadersVisible = False ' Ocultar encabezados de columna

                    ' Ajustar el ancho de columna para que ocupe todo el DataGridView
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                    ' Agregar la fila con el total formateado
                    .Rows.Add("Total: ", total.ToString())
                End With

                observaciones.Text = textbox1.Text
            Catch ex As Exception
                MessageBox.Show("Error al obtener los datos de la factura: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Friend Sub prepareDate(textBox2 As TextBox, idPredio As Integer)
            Try
                Using conn As New SQLiteConnection(connectionString)
                    Dim fechaActual As DateTime = DateTime.Now
                    Dim mes As Integer
                    Dim anyo As Integer
                    If fechaActual.Month = 1 Then
                        mes = 12
                        anyo = fechaActual.Year - 1
                    Else
                        mes = fechaActual.Month - 1
                        anyo = fechaActual.Year
                    End If
                    conn.Open()
                    Using cmd As New SQLiteCommand("SELECT 
                    cf.valor AS ValorCostoFijo
                FROM 
                    predios p
                JOIN 
                    usuarios us ON p.Usuarios_NumeroCedula = us.NumeroCedula  
                JOIN 
                    factura fac ON fac.Predios_idPredios = p.idPredios        
                JOIN
                    periodos per ON fac.Periodos_idPeriodos = per.idPeriodos  
                JOIN
                    costosfijos cf ON cf.ClasificacionUso_idClasificacionUso = p.ClasificacionUso_idClasificacionUso 
                    AND cf.Periodos_idPeriodos = per.idPeriodos               
                WHERE 
                    fac.estado != 2 AND us.NumeroCedula = @cedula AND p.idPredios = @idPredio AND fac.Periodos_idPeriodos = @anyo and mes = @mes
                ORDER BY
                    fac.Periodos_idPeriodos", conn)

                        ' Extraer los valores del TextBox y del parámetro idPredio
                        cmd.Parameters.AddWithValue("@cedula", textBox2.Text)
                        cmd.Parameters.AddWithValue("@idPredio", idPredio)
                        cmd.Parameters.AddWithValue("@anyo", anyo)
                        cmd.Parameters.AddWithValue("@mes", mes)

                        Using reader As SQLiteDataReader = cmd.ExecuteReader
                            If reader.HasRows Then
                                While reader.Read
                                    valorPeriodo = reader.GetDecimal(reader.GetOrdinal("ValorCostoFijo"))
                                End While
                            End If
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                MsgBox("Error al obtener datos: " & ex.Message)
            End Try
        End Sub

        Friend Sub GenerarVistaFactura(textBox3 As TextBox, textBox1 As TextBox, dataGridView1 As DataGridView, dataGridView2 As DataGridView, observaciones As Label)
            Try
                ' Procesar las líneas del TextBox3 y agregarlas a datagridview2
                Dim texto As String = textBox3.Text
                Dim lineas() As String = texto.Split(New String() {Environment.NewLine}, StringSplitOptions.None)

                ' Limpiar y configurar las columnas del DataGridView2
                With dataGridView1
                    .Rows.Clear()
                    .Columns.Clear()
                    .Columns.Add("descripcion", "Descripción")
                    .Columns.Add("valor", "Valor")
                    .AllowUserToAddRows = False
                    .RowHeadersVisible = False
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                End With

                Dim tieneDatos As Boolean = False ' Indicador para saber si se agregaron valores válidos

                ' Procesar cada línea del TextBox
                For Each linea As String In lineas
                    If Not String.IsNullOrWhiteSpace(linea) Then
                        Dim partes() As String = linea.Split("-"c)

                        If partes.Length = 2 Then
                            ' Quitar posibles espacios en blanco alrededor de las partes
                            Dim valor As String = If(String.IsNullOrWhiteSpace(partes(0).Trim()), "NULL", partes(0).Trim())
                            Dim descripcion As String = If(String.IsNullOrWhiteSpace(partes(1).Trim()), "NULL", partes(1).Trim())

                            ' Agregar la fila solo si hay datos en alguna de las partes
                            If valor <> "NULL" Or descripcion <> "NULL" Then
                                dataGridView1.Rows.Add(descripcion, valor)
                                tieneDatos = True ' Se agregaron datos válidos
                            End If
                        End If
                    End If
                Next

                ' Si no se ingresaron valores válidos, agregar una fila con NULLs
                If Not tieneDatos Then
                    dataGridView1.Rows.Add("NULL", 0)
                End If

                Dim total As Decimal = 0

                For Each row As DataGridViewRow In dataGridView1.Rows
                    If row.Cells("Valor").Value IsNot Nothing Then
                        total += Convert.ToDecimal(row.Cells("valor").Value)
                    End If
                Next

                With dataGridView2
                    ' Limpiar el DataGridView
                    .Rows.Clear()
                    .Columns.Clear()

                    ' Agregar una sola columna para mostrar "Total: valor"
                    .Columns.Add("Total", "") ' La columna no necesita encabezado
                    .Columns.Add("Descripcion", "") ' La columna no necesita encabezado

                    ' Deshabilitar agregar filas y encabezados
                    .AllowUserToAddRows = False
                    .RowHeadersVisible = False
                    .ColumnHeadersVisible = False ' Ocultar encabezados de columna

                    ' Ajustar el ancho de columna para que ocupe todo el DataGridView
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                    ' Agregar la fila con el total formateado
                    .Rows.Add("Total: ", total.ToString())
                End With

                observaciones.Text = textBox1.Text

            Catch ex As Exception
                MsgBox("Error al generar la vista de la factura" & ex.Message)
            End Try
        End Sub



        Friend Function AñadirdatosFactura(nombreCliente As Label, nombrePredio As Label, facturaID As Label, telefonoCliente As Label, periodoCobro As Label, fechaFin As Label, text As String, idPredio As Integer) As Boolean
            Try
                Dim fechaActual As DateTime = DateTime.Now
                Dim mes As Integer
                Dim anyo As Integer
                If fechaActual.Month = 1 Then
                    mes = 12
                    anyo = fechaActual.Year - 1
                Else
                    mes = fechaActual.Month - 1
                    anyo = fechaActual.Year
                End If
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()
                    Using cmd As New SQLiteCommand("
                SELECT 
                    us.Nombre AS NombreUsuario,
                    pre.NombrePredio,
                    fac.idFactura,
                    us.telefono,
                    fac.FechaPago
                FROM
                    predios pre
                JOIN
                    usuarios us ON pre.Usuarios_NumeroCedula = us.NumeroCedula
                JOIN
                    factura fac ON fac.Predios_idPredios = pre.idPredios 
                JOIN
                    clasificacionuso cu ON cu.idClasificacionUso = pre.ClasificacionUso_idClasificacionUso   
                JOIN
                    periodos per ON fac.Periodos_idPeriodos = per.idPeriodos 
                JOIN
                    costosfijos cf ON cf.ClasificacionUso_idClasificacionUso = pre.ClasificacionUso_idClasificacionUso   
                    AND cf.Periodos_idPeriodos = per.idPeriodos 
                WHERE 
                    us.NumeroCedula = @NumeroCedula AND
                    pre.idPredios = @idPredios AND 
                    fac.mes = @mes AND
                    fac.Periodos_idPeriodos = @anyo
            ", conn)
                        cmd.Parameters.AddWithValue("@NumeroCedula", text)
                        cmd.Parameters.AddWithValue("@idPredios", idPredio)
                        cmd.Parameters.AddWithValue("@mes", mes)
                        cmd.Parameters.AddWithValue("@anyo", anyo)

                        Using reader As SQLiteDataReader = cmd.ExecuteReader()
                            If reader.HasRows Then
                                reader.Read() ' Mover a la primera fila
                                nombreCliente.Text = "Cliente: " & reader("NombreUsuario").ToString()
                                nombrePredio.Text = "Predio: " & reader("NombrePredio").ToString()
                                facturaID.Text = "ID de Factura: " & reader("idFactura").ToString()
                                telefonoCliente.Text = "Teléfono: " & reader("telefono").ToString()
                                periodoCobro.Text = "Periodo de Cobro: " & mes & "/" & anyo
                                fechaFin.Text = $"Fecha de Pago: 10/{mes}/{anyo}"
                            End If
                        End Using
                    End Using
                End Using
                Return True
            Catch ex As Exception
                MsgBox("Error al extraer datos: " & ex.Message)
                Return False
            End Try
        End Function

        Friend Function ListarPredios(textBox2 As TextBox, comboBox2 As ComboBox, textbox1 As TextBox) As Boolean
            Try
                ' Limpiar los items del ComboBox antes de agregar los nuevos
                comboBox2.Items.Clear()

                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()
                    Using cmd As New SQLiteCommand("SELECT idPredios, NombrePredio FROM predios WHERE Usuarios_NumeroCedula = @cedula", conn)
                        ' Asignar el valor de la cédula del TextBox al parámetro de la consulta
                        cmd.Parameters.AddWithValue("@cedula", textBox2.Text)

                        ' Ejecutar la consulta
                        Dim reader = cmd.ExecuteReader()

                        ' Verificar si se obtienen filas
                        If reader.HasRows Then
                            ' Recorrer las filas del resultado y añadir cada predio al ComboBox
                            While reader.Read()
                                Dim idPredios As String = reader("idPredios").ToString()
                                Dim nombrePredio As String = reader("NombrePredio").ToString()

                                ' Concatenar el idPredios y nombrePredio en el ComboBox
                                comboBox2.Items.Add($"{idPredios}-{nombrePredio}")
                            End While
                            ' Si hay elementos en el ComboBox, seleccionar el primer ítem por defecto
                            If comboBox2.Items.Count > 0 Then

                                Return True
                            End If
                        Else

                            textbox1.Text &= $"No se encontraron predios para la cédula: {textBox2.Text}" & vbCrLf

                            Return False
                        End If
                    End Using
                End Using
            Catch ex As Exception
                ' Control de errores para cualquier excepción
                MsgBox($"Error al buscar predios para la factura: {ex.Message}")
                Return False
            End Try
        End Function

        Friend Function VerificarEstado(anyo As Integer, mes As Integer) As Boolean
            Try
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()
                    Using cmd As New SQLiteCommand("SELECT FinPeriodo from periodos WHERE idPeriodos = @anyo", conn)
                        cmd.Parameters.AddWithValue("@anyo", anyo)
                        Using reader As SQLiteDataReader = cmd.ExecuteReader
                            If reader.HasRows Then
                                ' Leer la fecha FinPeriodo
                                reader.Read()
                                Dim finPeriodo As String = reader("FinPeriodo").ToString()

                                ' Extraer año y mes de FinPeriodo
                                Dim finPeriodoDate As DateTime = DateTime.Parse(finPeriodo)
                                Dim finPeriodoAnyo As Integer = finPeriodoDate.Year
                                Dim finPeriodoMes As Integer = finPeriodoDate.Month

                                ' Comparar año y mes
                                If finPeriodoAnyo = anyo AndAlso finPeriodoMes = mes Then
                                    Using cdm As New SQLiteCommand("SELECT FinPeriodo from periodos WHERE idPeriodos = @anyo", conn)
                                        cdm.Parameters.AddWithValue("@anyo", anyo + 1)
                                        Using reader2 As SQLiteDataReader = cdm.ExecuteReader
                                            If reader2.HasRows Then
                                                Return False
                                            Else
                                                Return True
                                            End If
                                        End Using
                                    End Using
                                Else
                                    Return False
                                End If
                            End If
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                MsgBox("Error al consultar la base de datos: " & ex.Message)
            End Try

            Return False ' Si no se encuentra ningún registro, retornar False
        End Function


        Private Function FacturarInnactivoSinSaldo(textBox3 As TextBox, textBox1 As TextBox, text As String, idPredio As Integer, conn As SQLiteConnection) As Boolean
            Try
                Dim fechaActual As DateTime = DateTime.Now
                Dim mes As Integer
                Dim anyo As Integer
                If fechaActual.Month = 1 Then
                    mes = 12
                    anyo = fechaActual.Year - 1
                Else
                    mes = fechaActual.Month
                    anyo = fechaActual.Year
                End If
                Using cmd As New SQLiteCommand("
                    SELECT mes, 
                           Periodos_idPeriodos, 
                           CASE  
                               WHEN estado = 4 THEN 'Pendiente'
                               WHEN estado = 6 THEN 'Pago con mora'
                           END AS estado,
                           CASE 
	                       	   WHEN addCostosAsociados IS NOT NULL THEN addCostosAsociados
	                       	   ELSE 'no aplica'
	                       END AS costoAdicional
                    FROM factura 
                    WHERE estado > 3 AND estado != 5 AND Periodos_idPeriodos <= @anyo AND mes <= @mes AND Predios_idPredios = @idpredio;
                ", conn)
                    cmd.Parameters.AddWithValue("@anyo", anyo)
                    cmd.Parameters.AddWithValue("@mes", mes)
                    cmd.Parameters.AddWithValue("@idpredio", idPredio)

                    Using reader As SQLiteDataReader = cmd.ExecuteReader()
                        Dim contador As Integer = 0
                        If reader.HasRows Then
                            While reader.Read()
                                Dim periodoAnyo As Integer = reader.GetInt32(reader.GetOrdinal("Periodos_idPeriodos"))
                                Dim periodoMes As Integer = reader.GetInt32(reader.GetOrdinal("mes"))
                                Dim estado As String = reader.GetString(reader.GetOrdinal("estado"))
                                Dim valor As Integer = 5000
                                Dim costoAdicional As String = reader("costoAdicional").ToString()
                                If estado <> "Pendiente" Then
                                    contador += 1
                                End If
                                textBox3.AppendText($"{valor} - Factura {estado}: {periodoAnyo}/{periodoMes}" & Environment.NewLine)
                                If costoAdicional <> "no aplica" Then
                                    ' Separar los diferentes costos adicionales
                                    Dim costos() As String = costoAdicional.Split("|"c)

                                    ' Recorrer cada costo adicional y formatearlo
                                    For Each costo As String In costos
                                        ' Eliminar "Valor:" y espacios adicionales al principio
                                        Dim formateado As String = costo.Replace("Valor:", "").Trim()

                                        ' Agregar a textBox3 solo si no está vacío
                                        If Not String.IsNullOrEmpty(formateado) Then
                                            textBox3.AppendText(formateado & Environment.NewLine)
                                        End If
                                    Next
                                End If
                            End While
                            textBox1.Text = $"Total de facturas vencidas: {contador}"
                        Else
                            textBox1.Text = "Facturas al día"
                        End If
                    End Using
                End Using
                Return True
            Catch ex As Exception
                MsgBox("Error en la base de datos: " & ex.Message)
                Return False
            End Try
        End Function


        Private Function FacturaSinSaldo(textBox3 As TextBox, textBox1 As TextBox, cedula As String, year As Integer, conn As SQLiteConnection) As Boolean
            Try
                Dim fechaActual As DateTime = DateTime.Now
                Using cmd As New SQLiteCommand("
            SELECT 
                cf.valor AS ValorCostoFijo,
                fac.mes,
                fac.Periodos_idPeriodos,
                fac.estado,
                CASE 
	            	WHEN addCostosAsociados IS NOT NULL THEN addCostosAsociados
	            	ELSE 'no aplica'
	            END AS costoAdicional
            FROM 
                predios p
            JOIN 
                usuarios us ON p.Usuarios_NumeroCedula = us.NumeroCedula  
            JOIN 
                factura fac ON fac.Predios_idPredios = p.idPredios        
            JOIN
                periodos per ON fac.Periodos_idPeriodos = per.idPeriodos  
            JOIN
                costosfijos cf ON cf.ClasificacionUso_idClasificacionUso = p.ClasificacionUso_idClasificacionUso 
                AND cf.Periodos_idPeriodos = per.idPeriodos               
            WHERE 
                fac.estado != 2 AND us.NumeroCedula = @cedula AND p.idPredios = @idPredio
            ORDER BY
                fac.Periodos_idPeriodos;", conn)

                    cmd.Parameters.AddWithValue("@cedula", cedula)
                    cmd.Parameters.AddWithValue("@idPredio", year)

                    Using reader As SQLiteDataReader = cmd.ExecuteReader()
                        Dim count As Integer = 0
                        textBox3.Clear() ' Limpiar el TextBox antes de agregar texto nuevo

                        If reader.HasRows Then
                            While reader.Read()
                                ' Obtener los valores de los campos
                                Dim periodoAnyo As Integer = reader.GetInt32(reader.GetOrdinal("Periodos_idPeriodos"))
                                Dim periodoMes As Integer = reader.GetInt32(reader.GetOrdinal("mes"))
                                Dim valor As Decimal = reader.GetDecimal(reader.GetOrdinal("ValorCostoFijo"))
                                Dim costoAdicional As String = reader("costoAdicional").ToString()
                                Dim estado As Integer = Convert.ToInt32(reader("estado"))
                                ' Comparar con el año y mes actuales

                                If periodoAnyo <= fechaActual.Year Then
                                    If estado = 1 Then
                                        textBox3.AppendText($"{valor} - Factura actual, Periodo: {periodoAnyo}/{periodoMes}" & Environment.NewLine)
                                        If costoAdicional <> "no aplica" Then
                                            ' Separar los diferentes costos adicionales
                                            Dim costos() As String = costoAdicional.Split("|"c)

                                            ' Recorrer cada costo adicional y formatearlo
                                            For Each costo As String In costos
                                                ' Eliminar "Valor:" y espacios adicionales al principio
                                                Dim formateado As String = costo.Replace("Valor:", "").Trim()

                                                ' Agregar a textBox3 solo si no está vacío
                                                If Not String.IsNullOrEmpty(formateado) Then
                                                    textBox3.AppendText(formateado & Environment.NewLine)
                                                End If
                                            Next
                                        End If
                                    Else
                                        Using cmd2 As New SQLiteCommand("SELECT interesMora FROM periodos WHERE idPeriodos = @anyo", conn)
                                            cmd2.Parameters.AddWithValue("@anyo", periodoAnyo)

                                            Using reader2 As SQLiteDataReader = cmd2.ExecuteReader()
                                                If reader2.Read Then
                                                    Dim getMora As String = reader2("interesMora").ToString()

                                                    Dim mora As Decimal = Convert.ToDecimal(getMora)
                                                    Dim valorConMora As Decimal = valor + (valor * mora / 100)
                                                    textBox3.AppendText($"{valorConMora} - Factura vencida en: {periodoAnyo}/{periodoMes}" & Environment.NewLine)
                                                    If costoAdicional <> "no aplica" Then
                                                        ' Separar los diferentes costos adicionales
                                                        Dim costos() As String = costoAdicional.Split("|"c)

                                                        ' Recorrer cada costo adicional y formatearlo
                                                        For Each costo As String In costos
                                                            ' Eliminar "Valor:" y espacios adicionales al principio
                                                            Dim formateado As String = costo.Replace("Valor:", "").Trim()

                                                            ' Agregar a textBox3 solo si no está vacío
                                                            If Not String.IsNullOrEmpty(formateado) Then
                                                                textBox3.AppendText(formateado & Environment.NewLine)
                                                            End If
                                                        Next
                                                    End If
                                                    count += 1
                                                End If
                                            End Using
                                        End Using
                                    End If
                                End If
                            End While

                            ' Mostrar el total de facturas vencidas en el segundo TextBox
                            textBox1.Text = $"Total de facturas vencidas: {count}"
                        Else
                            ' No se encontraron facturas, limpiar TextBox
                            textBox1.Text = "Facturas al día"
                        End If
                    End Using
                End Using
            Catch ex As Exception
                ' Manejar cualquier excepción que ocurra durante la ejecución
                MessageBox.Show("Error: " & ex.Message)
            End Try

            Return True
        End Function



        Private Function PagarDescontarConSaldo(estado As Integer, saldo As Decimal, valorPeriodo As Decimal, text As String, idPredio As Integer, anyo As Integer, mes As Integer, conn As SQLiteConnection, textBox3 As TextBox, textBox1 As TextBox) As Boolean
            Try
                Using transaction = conn.BeginTransaction()
                    Dim mesUpdateSaldo As Integer
                    Dim anyoUpdateSaldo As Integer

                    If saldo >= valorPeriodo Then
                        Dim restante As Decimal = saldo - valorPeriodo

                        Try
                            ' Primera actualización de la factura
                            Using cmd As New SQLiteCommand("UPDATE factura 
                        SET estado = @estado, FechaPago = strftime('%Y-%m-%d', 'now')
                        WHERE Periodos_idPeriodos = @anyo
                        AND mes = @mes
                        AND Predios_idPredios = (
                            SELECT pre.idPredios 
                            FROM predios pre
                            JOIN usuarios us ON pre.Usuarios_NumeroCedula = us.NumeroCedula
                            WHERE us.NumeroCedula = @cedula
                            AND pre.idPredios = @idpredio
                        );", conn)

                                cmd.Parameters.AddWithValue("@estado", estado)
                                cmd.Parameters.AddWithValue("@anyo", anyo)
                                cmd.Parameters.AddWithValue("@mes", mes)
                                cmd.Parameters.AddWithValue("@cedula", text)
                                cmd.Parameters.AddWithValue("@idpredio", idPredio)

                                Dim filasAfectadas As Integer = cmd.ExecuteNonQuery()
                                If filasAfectadas = 0 Then
                                    MsgBox("No se encontró la factura para actualizar.")
                                    transaction.Rollback()
                                    Return False
                                End If
                            End Using

                            ' Verificación de fecha y cálculo de nuevo periodo
                            Using cmd As New SQLiteCommand("SELECT FinPeriodo FROM periodos WHERE idPeriodos = @anyo", conn)
                                cmd.Parameters.AddWithValue("@anyo", anyo)
                                Using reader As SQLiteDataReader = cmd.ExecuteReader()
                                    If reader.Read() Then
                                        Dim optenerFecha As DateTime
                                        If DateTime.TryParse(reader("FinPeriodo").ToString(), optenerFecha) Then
                                            Dim anyoOptenido As Integer = optenerFecha.Year
                                            Dim mesOptenido As Integer = optenerFecha.Month

                                            If mesOptenido = mes AndAlso anyoOptenido = anyo Then
                                                ' Obtener el siguiente periodo
                                                Using getNewPeriodo As New SQLiteCommand("SELECT InicioPeriodo FROM periodos WHERE idPeriodos = @anyo", conn)
                                                    getNewPeriodo.Parameters.AddWithValue("@anyo", anyo + 1)
                                                    Using reader2 As SQLiteDataReader = getNewPeriodo.ExecuteReader()
                                                        If reader2.Read() Then
                                                            Dim nuevaFechaOptenida As DateTime
                                                            If DateTime.TryParse(reader2("InicioPeriodo").ToString(), nuevaFechaOptenida) Then
                                                                mesUpdateSaldo = nuevaFechaOptenida.Month
                                                                anyoUpdateSaldo = nuevaFechaOptenida.Year
                                                            Else
                                                                MsgBox("Error al parsear la fecha de inicio del nuevo periodo.")
                                                                transaction.Rollback()
                                                                Return False
                                                            End If
                                                        Else
                                                            MsgBox("No se encontró el siguiente periodo.")
                                                            transaction.Rollback()
                                                            Return False
                                                        End If
                                                    End Using
                                                End Using
                                            Else
                                                mesUpdateSaldo = mes + 1
                                                anyoUpdateSaldo = anyo
                                            End If
                                        Else
                                            MsgBox("Error al parsear la fecha de fin del periodo.")
                                            transaction.Rollback()
                                            Return False
                                        End If
                                    Else
                                        MsgBox("No se encontró el periodo actual.")
                                        transaction.Rollback()
                                        Return False
                                    End If
                                End Using
                            End Using

                            ' Segunda actualización de saldo
                            MsgBox($"Intentando actualizar saldo. Año: {anyoUpdateSaldo}, Mes: {mesUpdateSaldo}, ID Predio: {idPredio}, Cedula: {text}, Saldo restante: {restante}")
                            Using cmd As New SQLiteCommand("UPDATE factura 
                        SET saldo = @saldo
                        WHERE Periodos_idPeriodos = @anyo
                        AND mes = @mes
                        AND Predios_idPredios = (
                            SELECT pre.idPredios 
                            FROM predios pre
                            JOIN usuarios us ON pre.Usuarios_NumeroCedula = us.NumeroCedula
                            WHERE us.NumeroCedula = @cedula
                            AND pre.idPredios = @idpredio
                        );", conn)

                                cmd.Parameters.AddWithValue("@saldo", restante)
                                cmd.Parameters.AddWithValue("@anyo", anyoUpdateSaldo)
                                cmd.Parameters.AddWithValue("@mes", mesUpdateSaldo)
                                cmd.Parameters.AddWithValue("@cedula", text)
                                cmd.Parameters.AddWithValue("@idpredio", idPredio)

                                Dim filasAfectadas As Integer = cmd.ExecuteNonQuery()
                                If filasAfectadas = 0 Then
                                    MsgBox("No se pudo actualizar el saldo. Verifique que la combinación de Año, Mes, ID Predio y Cedula es correcta.")
                                    transaction.Rollback()
                                    Return False
                                End If
                            End Using

                            textBox3.AppendText("No hay facturas pendientes" & Environment.NewLine)
                            textBox1.AppendText($"Factura pagada. Saldo restante: {restante}" & Environment.NewLine)
                            transaction.Commit()
                            MsgBox("Factura pagada con éxito.")
                            Return True

                        Catch ex As Exception
                            transaction.Rollback()
                            MsgBox("Error en la base de datos: " & ex.Message)
                            Return False
                        End Try

                    Else
                        ' Caso cuando saldo < valorPeriodo
                        Dim pendiente As Decimal = valorPeriodo - saldo
                        Dim dataXD As String = $"Valor pendiente: {pendiente}"

                        Using cmd As New SQLiteCommand("UPDATE factura
                    SET saldo = 0, Observaciones = @data
                    WHERE Periodos_idPeriodos = @anyo
                    AND mes = @mes
                    AND Predios_idPredios = (
                        SELECT pre.idPredios 
                        FROM predios pre
                        JOIN usuarios us ON pre.Usuarios_NumeroCedula = us.NumeroCedula
                        WHERE us.NumeroCedula = @cedula
                        AND pre.idPredios = @idpredio
                    );", conn)

                            cmd.Parameters.AddWithValue("@data", dataXD)
                            cmd.Parameters.AddWithValue("@anyo", anyo)
                            cmd.Parameters.AddWithValue("@mes", mes)
                            cmd.Parameters.AddWithValue("@cedula", text)
                            cmd.Parameters.AddWithValue("@idpredio", idPredio)

                            Dim filasAfectadas As Integer = cmd.ExecuteNonQuery()
                            If filasAfectadas = 0 Then
                                MsgBox("No se pudo actualizar el saldo.")
                                transaction.Rollback()
                                Return False
                            End If
                        End Using

                        textBox3.AppendText($"Saldo pendiente de {pendiente} para la factura: {anyo}/{mes}" & Environment.NewLine)
                        textBox1.AppendText("Saldo actual: 0")
                        transaction.Commit()
                        Return True
                    End If
                End Using

            Catch ex As Exception
                MsgBox("Error al procesar el pago: " & ex.Message)
                Return False
            End Try
        End Function

        Friend Function VistaDeFactura(idFactura1 As Label, idFactura2 As Label, nombreCliente As Label, nombrePredio As Label, telefonoCliente As Label, periodoCobro As Label, totalPagar As Label, fechaFin As Label, costoPeriodo As Label, reconexion As Label, multas As Label, atrasos As Label, recargoMora As Label, otros As Label, ajustePeso As Label, valor As Label, clienteColilla As Label, predioColilla As Label, totalColilla As Label, textBox2 As TextBox, idPredio As Integer, fechaEnviada As DateTime, PeriodoColilla As Label) As Boolean
            Try
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()
                    Dim query As String = "
                SELECT 
                   MAX(fac.idFactura) AS idFactura,
          p.NombrePredio,
          us.Nombre,
          us.telefono,
          SUM(CASE WHEN fac.estado = 3 THEN cf.valor  
			WHEN fac.estado = 4 THEN '5000' 	
			ELSE 0 END) AS valor,
          GROUP_CONCAT(fac.addCostosAsociados, ' | ') AS addCostosAsociados,
          SUM(CASE WHEN fac.estado = 1 THEN cf.valor 
			WHEN fac.estado = 6 THEN '5000' ELSE 0 END) AS atrasos,
          CASE 
              WHEN SUM(CASE WHEN fac.estado = 1 THEN cf.valor ELSE 0 END) > 0 THEN 
                  (SUM(CASE WHEN fac.estado = 1 THEN cf.valor ELSE 0 END) * CAST(REPLACE(per.interesMora, ',', '.') AS REAL)) / 100
              WHEN SUM(CASE WHEN fac.estado = 6 THEN '5000' ELSE 0 END) > 0 THEN 
                  (SUM(CASE WHEN fac.estado = 6 THEN '5000' ELSE 0 END) * CAST(REPLACE(per.interesMora, ',', '.') AS REAL)) / 100
              ELSE 0
          END AS RecargoMora,
          MAX(fac.mes) AS mes,
          MAX(fac.Periodos_idPeriodos) AS Periodos_idPeriodos,
          MAX(fac.FechaPago) AS FechaPago,
          fac.saldo AS saldo
                FROM 
                    predios p
                JOIN usuarios us ON p.Usuarios_NumeroCedula = us.NumeroCedula
                JOIN factura fac ON fac.Predios_idPredios = p.idPredios
                JOIN periodos per ON fac.Periodos_idPeriodos = per.idPeriodos
                JOIN costosfijos cf ON cf.ClasificacionUso_idClasificacionUso = p.ClasificacionUso_idClasificacionUso 
                    AND cf.Periodos_idPeriodos = per.idPeriodos
                JOIN clasificacionuso cu ON cu.idClasificacionUso = p.ClasificacionUso_idClasificacionUso
                WHERE 
                    fac.estado NOT IN (2, 5)
                    AND us.NumeroCedula = @cedula
                    AND p.idPredios = @idpredios
                    AND (CAST(fac.Periodos_idPeriodos AS TEXT) || '/' || 
                         SUBSTR('00' || CAST(fac.mes AS TEXT), -2) || '/01' <= @datetime)
                GROUP BY 
                    p.NombrePredio, us.Nombre, us.telefono;
            "

                    Using cmd As New SQLiteCommand(query, conn)
                        ' Formatear la fecha
                        Dim fechaFormateada As String = fechaEnviada.ToString("yyyy/MM/dd")
                        cmd.Parameters.AddWithValue("@cedula", textBox2.Text.Trim())
                        cmd.Parameters.AddWithValue("@idpredios", idPredio)
                        cmd.Parameters.AddWithValue("@datetime", fechaFormateada)

                        Using reader As SQLiteDataReader = cmd.ExecuteReader()
                            If reader.HasRows AndAlso reader.Read() Then
                                ' Inicializar variables
                                Dim valorPeriodo As Double = If(IsDBNull(reader("valor")), 0, Convert.ToDouble(reader("valor")))
                                Dim valorReconexion As Double = 0
                                Dim valorMultas As Double = 0
                                Dim valorOtros As Double = 0
                                Dim valorAtrasos As Double = If(IsDBNull(reader("atrasos")), 0, Convert.ToDouble(reader("atrasos")))
                                Dim valorRecargoMora As Double = If(IsDBNull(reader("RecargoMora")), 0, Convert.ToDouble(reader("RecargoMora")))
                                Dim ajustesdelPeso As Double = 0

                                ' Asignar datos al formulario
                                idFactura1.Text = reader("idFactura").ToString()
                                idFactura2.Text = reader("idFactura").ToString()
                                nombreCliente.Text = $"Nombre: {reader("Nombre").ToString()}"
                                clienteColilla.Text = $"Nombre: {reader("Nombre").ToString()}"
                                nombrePredio.Text = $"Predio: {reader("NombrePredio").ToString()}"
                                predioColilla.Text = $"Predio: {reader("NombrePredio").ToString()}"
                                telefonoCliente.Text = $"Teléfono: {reader("telefono").ToString()}"

                                ' Mes en español
                                Dim mesNumero As Integer = Integer.Parse(reader("mes").ToString())
                                Dim nombreMes As String = New DateTime(1, mesNumero, 1).ToString("MMMM", Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
                                Dim valorPerdoCobro As String = $"{nombreMes}/{reader("Periodos_idPeriodos")}"
                                Dim FechaPago As DateTime = DateTime.Parse(reader("FechaPago").ToString()).ToString("dd/MMMM/yyyy", Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
                                periodoCobro.Text = $"Periodo facturado: {nombreMes}/{reader("Periodos_idPeriodos")}"
                                PeriodoColilla.Text = $"{nombreMes}/{reader("Periodos_idPeriodos")}"

                                ' Fecha de pago
                                If Not IsDBNull(reader("FechaPago")) Then
                                    fechaFin.Text = $"Pagar antes de: {DateTime.Parse(reader("FechaPago").ToString()).ToString("dd/MMMM/yyyy", Globalization.CultureInfo.CreateSpecificCulture("es-ES"))}"
                                Else
                                    fechaFin.Text = "Pagar antes de: fecha Indicada"
                                End If

                                costoPeriodo.Text = $"${valorPeriodo:N2}"

                                ' Procesar costos asociados
                                If Not IsDBNull(reader("addCostosAsociados")) Then
                                    Dim costos As String = reader("addCostosAsociados").ToString()
                                    Dim partes() As String = costos.Split("|"c)
                                    For Each parte As String In partes
                                        If parte.Contains("Reconexión") Then
                                            valorReconexion += ExtraerValor(parte)
                                        ElseIf parte.Contains("Multas") Then
                                            valorMultas += ExtraerValor(parte)
                                        Else
                                            valorOtros += ExtraerValor(parte)
                                        End If
                                    Next
                                End If

                                ' Calcular el total a pagar
                                Dim totalDePago As Double = valorPeriodo + valorReconexion + valorMultas + valorOtros + valorAtrasos + valorRecargoMora

                                Dim residuo As Double = totalDePago Mod 50
                                If residuo <> 0 Then
                                    If residuo <= 25 Then
                                        ajustesdelPeso = -residuo ' Ajustar hacia abajo
                                    Else
                                        ajustesdelPeso = 50 - residuo ' Ajustar hacia arriba
                                    End If
                                End If

                                totalDePago += ajustesdelPeso


                                totalPagar.Text = $"${totalDePago:N2}"
                                valor.Text = $"${totalDePago:N2}"
                                totalColilla.Text = $"${totalDePago:N2}"
                                reconexion.Text = $"${valorReconexion:N2}"
                                multas.Text = $"${valorMultas:N2}"
                                otros.Text = $"${valorOtros:N2}"
                                atrasos.Text = $"${valorAtrasos:N2}"
                                recargoMora.Text = $"${valorRecargoMora:N2}"
                                ajustePeso.Text = $"${ajustesdelPeso:N2}"



                                Dim avisos As String

                                Dim saldo As Double = Convert.ToDouble(reader("saldo"))
                                Dim restante As Double
                                If saldo > 0 Then
                                    If saldo > totalDePago Then
                                        restante = saldo - totalDePago
                                        Dim PagarDescontarConSaldo As Boolean = PagarConSaldo(conn, textBox2, idPredio, fechaFormateada, 2, restante)
                                        If PagarDescontarConSaldo Then
                                            avisos = $"LA FACTURA SE PAGÓ CON SU SALDO." & vbCrLf & $" SALDO RESTANTE: {restante.ToString("N2").ToUpper()}"
                                        Else
                                            avisos = "ERROR AL GENERAR LA PRESENTE FACTURA."
                                            MsgBox($"ERROR EN LA FACTURA DEL USUARIO: {textBox2.Text.ToUpper()}", MsgBoxStyle.Critical, "ERROR")
                                        End If
                                    Else
                                        restante = totalDePago - saldo
                                        Dim data As String = $"Valor pendiente: {restante}"
                                        Dim descontar As Boolean = descantarFactura(reader("idFactura").ToString, conn, data)
                                        If descontar Then
                                            avisos = $"SALDO INSUFICIENTE:{vbCrLf} Disponible: ${saldo},{vbCrLf} A PAGAR: ${restante.ToString("N2")}"
                                        Else
                                            avisos = "ERROR AL GENERAR LA PRESENTE FACTURA."
                                            MsgBox($"ERROR EN LA FACTURA DEL USUARIO: {textBox2.Text.ToUpper()}", MsgBoxStyle.Critical, "ERROR")
                                        End If
                                    End If
                                End If

                                If valorRecargoMora > 0 Then
                                    avisos = "AVISO DE SUSPENSIÓN"
                                End If


                                listaDePersonas.Add(New Persona(
                                                    reader("idFactura").ToString(),
                                                    reader("Nombre").ToString(),
                                                    reader("NombrePredio").ToString(),
                                                    reader("telefono").ToString(),
                                                    valorPerdoCobro,
                                                    valorPeriodo.ToString("N2"),
                                                    totalDePago.ToString("N2"),
                                                    valorReconexion.ToString("N2"),
                                                    valorMultas.ToString("N2"),
                                                    valorOtros.ToString("N2"),
                                                    valorAtrasos.ToString("N2"),
                                                    valorRecargoMora.ToString("N2"),
                                                    ajustesdelPeso.ToString("N2"),
                                                    avisos,
                                                    FechaPago.ToString("dd/MMMM/yyyy", Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
                                                ))
                            Else
                                MsgBox("No se encontraron resultados.")
                                Return False
                            End If
                        End Using
                    End Using
                End Using
                Return True
            Catch ex As Exception
                MsgBox($"Error: {ex.Message}")
                Return False
            End Try
        End Function

        Private Function descantarFactura(toString As String, conn As SQLiteConnection, data As String) As Boolean
            Try
                Using cmd As New SQLiteCommand("UPDATE factura
                    SET saldo = 0, Observaciones = @data
                    WHERE idFactura = @idFactura", conn)
                    cmd.Parameters.AddWithValue("@idFactura", toString)
                    cmd.Parameters.AddWithValue("@data", data)
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            Catch ex As Exception
                MsgBox($"Error al procesar las facturas: {ex.Message}", MsgBoxStyle.Critical, "Error")
                Return False
            End Try
        End Function

        Private Function PagarConSaldo(conn As SQLiteConnection, textBox2 As TextBox, idPredio As Integer, fechaFormateada As String, estado As Integer, restante As Double) As Boolean
            Try
                Using transaction As SQLiteTransaction = conn.BeginTransaction()
                    Try
                        ' Consulta principal para obtener la factura y la siguiente
                        Using cmd As New SQLiteCommand("
                    SELECT 
                        idFactura, 
                        (SELECT MIN(idFactura)
                         FROM factura
                         WHERE estado NOT IN (2, 5)
                           AND Predios_idPredios = @idPredio
                           AND (CAST(Periodos_idPeriodos AS TEXT) || '/' || 
                                SUBSTR('00' || CAST(mes AS TEXT), -2) || '/01') > @Fecha) AS siguiente
                    FROM factura
                    WHERE estado NOT IN (2, 5)
                      AND Predios_idPredios = @idPredio
                      AND (CAST(Periodos_idPeriodos AS TEXT) || '/' || 
                           SUBSTR('00' || CAST(mes AS TEXT), -2) || '/01') <= @Fecha;", conn)
                            cmd.Parameters.AddWithValue("@idPredio", idPredio)
                            cmd.Parameters.AddWithValue("@Fecha", fechaFormateada)

                            Using reader As SQLiteDataReader = cmd.ExecuteReader()
                                If reader.Read() Then ' Mover al primer registro
                                    ' Procesar filas individuales
                                    Dim idFacturaActual As Integer = Convert.ToInt32(reader("idFactura"))

                                    ' Actualizar estado de la factura actual
                                    Using updateCmd As New SQLiteCommand("UPDATE factura 
                                                                  SET estado = 2 
                                                                  WHERE idFactura = @idFactura", conn, transaction)
                                        updateCmd.Parameters.AddWithValue("@idFactura", idFacturaActual)
                                        updateCmd.ExecuteNonQuery()
                                    End Using

                                    ' Verificar si hay una siguiente factura
                                    If reader.IsDBNull(reader.GetOrdinal("siguiente")) Then
                                        ' No hay siguiente factura
                                        transaction.Rollback()
                                        MsgBox("Cree el siguiente periodo.", MsgBoxStyle.Critical, "Error")
                                        Return False
                                    Else
                                        Dim siguienteId As Integer = Convert.ToInt32(reader("siguiente"))
                                        ' Actualizar saldo de la siguiente factura
                                        Using updateSaldo As New SQLiteCommand("UPDATE factura 
                                                                        SET saldo = @restante 
                                                                        WHERE idFactura = @idFactura", conn, transaction)
                                            updateSaldo.Parameters.AddWithValue("@restante", restante)
                                            updateSaldo.Parameters.AddWithValue("@idFactura", siguienteId)
                                            updateSaldo.ExecuteNonQuery()
                                        End Using
                                    End If
                                Else
                                    ' No hay filas en el lector
                                    transaction.Rollback()
                                    MsgBox("No se encontraron facturas para procesar.", MsgBoxStyle.Critical, "Error")
                                    Return False
                                End If
                            End Using
                        End Using

                        ' Confirmar la transacción
                        transaction.Commit()
                        Return True
                    Catch ex As Exception
                        ' Revertir la transacción en caso de error
                        transaction.Rollback()
                        MsgBox($"Error al procesar las facturas: {ex.Message}", MsgBoxStyle.Critical, "Error")
                        Return False
                    End Try
                End Using
            Catch ex As Exception
                MsgBox($"Error en la conexión: {ex.Message}", MsgBoxStyle.Critical, "Error")
                Return False
            End Try
        End Function


        Private Function ExtraerValor(texto As String) As Double
            ' Buscar la palabra "Valor: " y extraer el número que sigue
            Dim valor As Double = 0
            Dim inicio As Integer = texto.IndexOf("Valor: ")
            If inicio >= 0 Then
                Dim subTexto As String = texto.Substring(inicio + 7).Trim() ' Saltar "Valor: "
                Dim fin As Integer = subTexto.IndexOf(" "c) ' Encontrar el siguiente espacio
                If fin >= 0 Then
                    Double.TryParse(subTexto.Substring(0, fin), valor)
                Else
                    Double.TryParse(subTexto, valor)
                End If
            End If
            Return valor
        End Function

        Friend Function recorerPersonas() As Boolean
            Try
                If listaDePersonas Is Nothing OrElse listaDePersonas.Count = 0 Then
                    MsgBox("No hay personas en la lista.")
                    Return False
                End If

                pdfFormtoHorizontal(listaDePersonas) ' Pasa la lista de personas al método
                Return True
            Catch ex As Exception
                MsgBox($"No se pudo generar los PDF. Error: {ex.Message}")
                Return False
            End Try
        End Function


        Private Sub pdfFormtoHorizontal(listaDePersonas As List(Of Persona))
            Try
                If listaDePersonas Is Nothing OrElse listaDePersonas.Count = 0 Then
                    MsgBox("No hay personas en la lista para generar el PDF.")
                    Return
                End If

                ' Ruta de la carpeta y archivo PDF
                Dim folderPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "facturasAsocontador")
                If Not Directory.Exists(folderPath) Then
                    Directory.CreateDirectory(folderPath)
                End If

                Dim filePath As String = Path.Combine(folderPath, "FacturaAsocontador.pdf")

                ' Configuración del documento en tamaño media carta horizontal
                Dim document As New Document(New Rectangle(792, 520), 20, 20, 20, 20) ' Media carta en horizontal (11 x 5.5 pulgadas)
                Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream(filePath, FileMode.Create))
                document.Open()

                ' Crear una tabla principal con dos columnas
                Dim mainTable As New PdfPTable(2)
                mainTable.WidthPercentage = 100
                mainTable.SetWidths({1, 1})

                ' Crear una tabla para cada factura
                For Each persona As Persona In listaDePersonas
                    Dim facturaConColilla As New PdfPTable(1)
                    facturaConColilla.WidthPercentage = 100

                    ' Crear y agregar la tabla de factura
                    Dim invoiceTable As PdfPTable = CreateInvoice(persona)
                    If invoiceTable IsNot Nothing AndAlso invoiceTable.Rows.Count > 0 Then
                        facturaConColilla.AddCell(New PdfPCell(invoiceTable) With {
                    .Border = Rectangle.NO_BORDER,
                    .Padding = 10,
                    .FixedHeight = 400,
                    .VerticalAlignment = Element.ALIGN_TOP
                })
                    Else
                        facturaConColilla.AddCell(New PdfPCell(New Phrase("Factura vacía")) With {
                    .Border = Rectangle.NO_BORDER,
                    .Padding = 10,
                    .FixedHeight = 400,
                    .VerticalAlignment = Element.ALIGN_TOP
                })
                    End If

                    ' Crear y agregar la tabla de colilla
                    Dim footerTable As PdfPTable = CreateFooterTable(persona)
                    If footerTable IsNot Nothing AndAlso footerTable.Rows.Count > 0 Then
                        facturaConColilla.AddCell(New PdfPCell(footerTable) With {
                    .Border = Rectangle.NO_BORDER,
                    .FixedHeight = 60,
                    .VerticalAlignment = Element.ALIGN_BOTTOM
                })
                    Else
                        facturaConColilla.AddCell(New PdfPCell(New Phrase("Colilla vacía")) With {
                    .Border = Rectangle.NO_BORDER,
                    .FixedHeight = 60,
                    .VerticalAlignment = Element.ALIGN_BOTTOM
                })
                    End If

                    mainTable.AddCell(New PdfPCell(facturaConColilla) With {
                .Border = Rectangle.NO_BORDER,
                .PaddingRight = 20
            })
                Next

                ' Si el número de celdas es impar, agrega una celda vacía
                If listaDePersonas.Count Mod 2 <> 0 Then
                    mainTable.AddCell(New PdfPCell(New Phrase("")) With {
                .Border = Rectangle.NO_BORDER
            })
                End If

                ' Agregar la tabla principal al documento
                document.Add(mainTable)

                ' Cerrar el documento
                document.Close()

                MsgBox("PDF generado exitosamente en: " & filePath)
            Catch ex As Exception
                MsgBox($"No se generó el PDF: {ex.Message}")
            End Try
        End Sub



        Private Function CreateFooterTable(persona As Persona) As PdfPTable
            ' Configuración de fuentes
            Dim boldFont As Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10)
            Dim titleFont As Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.BLACK)

            ' Tabla para la colilla de pago
            Dim footerTable As New PdfPTable(3)
            footerTable.WidthPercentage = 100
            footerTable.SetWidths({30, 40, 30})

            ' Agregar las celdas de la colilla de pago con bordes
            footerTable.AddCell(New PdfPCell(New Phrase($"Factura N°:", boldFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER, .Border = Rectangle.BOX})
            footerTable.AddCell(New PdfPCell(New Phrase("Período facturado", titleFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT, .Border = Rectangle.BOX})
            footerTable.AddCell(New PdfPCell(New Phrase($"{persona.PeriodoCobro}", boldFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER, .Border = Rectangle.BOX})

            footerTable.AddCell(New PdfPCell(New Phrase($"{persona.IdFactura}", boldFont)) With {.Rowspan = 2, .HorizontalAlignment = Element.ALIGN_CENTER, .VerticalAlignment = Element.ALIGN_CENTER, .Border = Rectangle.BOX})
            footerTable.AddCell(New PdfPCell(New Phrase($"Nombre: {persona.NombreCliente}", boldFont)) With {.HorizontalAlignment = Element.ALIGN_LEFT, .Border = Rectangle.BOX})
            footerTable.AddCell(New PdfPCell(New Phrase("Total a pagar", boldFont)) With {.BackgroundColor = BaseColor.YELLOW, .HorizontalAlignment = Element.ALIGN_LEFT, .Border = Rectangle.BOX})

            footerTable.AddCell(New PdfPCell(New Phrase($"Predio: {persona.NombrePredio}", boldFont)) With {.HorizontalAlignment = Element.ALIGN_LEFT, .Border = Rectangle.BOX})
            footerTable.AddCell(New PdfPCell(New Phrase($"${persona.TotalPagar}", titleFont)) With {.BackgroundColor = BaseColor.YELLOW, .HorizontalAlignment = Element.ALIGN_LEFT, .Border = Rectangle.BOX})

            Return footerTable
        End Function

        Private Function CreateInvoice(persona As Persona) As PdfPTable
            ' Configuración de fuentes
            Dim boldFont As Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10)
            Dim regularFont As Font = FontFactory.GetFont(FontFactory.HELVETICA, 8)
            Dim titleFont As Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK)

            ' Tabla principal para la factura (sin borde)
            Dim table As New PdfPTable(1)
            table.WidthPercentage = 100

            ' Título de la factura
            Dim titleTable As New PdfPTable(3)
            titleTable.WidthPercentage = 100
            titleTable.SetWidths({20, 80, 20})

            titleTable.AddCell(New PdfPCell(New Phrase(" ", regularFont)) With {.Rowspan = 2, .Border = Rectangle.NO_BORDER})
            titleTable.AddCell(New PdfPCell(New Phrase("ASOCIACIÓN DE USUARIOS DEL DISTRITO DE ADECUACIÓN DE TIERRAS DE PEQUEÑA ESCALA CONTADOR ""ASOCONTADOR""", regularFont)) With {.Border = Rectangle.NO_BORDER})
            titleTable.AddCell(New PdfPCell(New Phrase($"Factura N°", boldFont)) With {.BackgroundColor = BaseColor.YELLOW, .HorizontalAlignment = Element.ALIGN_CENTER, .Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER})
            titleTable.AddCell(New PdfPCell(New Phrase("NIT. 900.544.992-0", regularFont)) With {.Border = Rectangle.NO_BORDER})
            titleTable.AddCell(New PdfPCell(New Phrase($"{persona.IdFactura}", regularFont)) With {.BackgroundColor = BaseColor.YELLOW, .HorizontalAlignment = Element.ALIGN_CENTER, .Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER})
            table.AddCell(New PdfPCell(titleTable) With {.Border = Rectangle.NO_BORDER})

            ' Sección de datos del cliente
            Dim clientInfoTable As New PdfPTable(2)
            clientInfoTable.WidthPercentage = 100
            clientInfoTable.SpacingBefore = 10
            clientInfoTable.SetWidths({30, 70})

            clientInfoTable.AddCell(New PdfPCell(New Phrase("Nombre:", boldFont)) With {.Border = Rectangle.BOX})
            clientInfoTable.AddCell(New PdfPCell(New Phrase($"{persona.NombreCliente}", regularFont)) With {.Border = Rectangle.BOX})
            clientInfoTable.AddCell(New PdfPCell(New Phrase("Teléfono:", boldFont)) With {.Border = Rectangle.BOX})
            clientInfoTable.AddCell(New PdfPCell(New Phrase($"{persona.TelefonoCliente}", regularFont)) With {.Border = Rectangle.BOX})
            clientInfoTable.AddCell(New PdfPCell(New Phrase("Predio:", boldFont)) With {.Border = Rectangle.BOX})
            clientInfoTable.AddCell(New PdfPCell(New Phrase($"{persona.NombrePredio}", regularFont)) With {.Border = Rectangle.BOX})
            clientInfoTable.AddCell(New PdfPCell(New Phrase("Período facturado:", boldFont)) With {.Border = Rectangle.BOX})
            clientInfoTable.AddCell(New PdfPCell(New Phrase($"{persona.PeriodoCobro}", regularFont)) With {.Border = Rectangle.BOX})
            table.AddCell(New PdfPCell(clientInfoTable) With {.Border = Rectangle.NO_BORDER, .PaddingBottom = 10}) ' Espacio inferior

            ' Sección de total a pagar en amarillo (con borde)
            Dim totalTable As New PdfPTable(1)
            totalTable.WidthPercentage = 100
            totalTable.AddCell(New PdfPCell(New Phrase("Total, a pagar:", boldFont)) With {.BackgroundColor = BaseColor.YELLOW, .HorizontalAlignment = Element.ALIGN_CENTER, .Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER})
            totalTable.AddCell(New PdfPCell(New Phrase($"${persona.TotalPagar}", titleFont)) With {.BackgroundColor = BaseColor.YELLOW, .HorizontalAlignment = Element.ALIGN_CENTER, .Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER})
            totalTable.AddCell(New PdfPCell(New Phrase("Pagar antes de: ", boldFont)) With {.Border = Rectangle.NO_BORDER, .HorizontalAlignment = Element.ALIGN_RIGHT})
            totalTable.AddCell(New PdfPCell(New Phrase($"{persona.FechaPago}", boldFont)) With {.Border = Rectangle.NO_BORDER, .HorizontalAlignment = Element.ALIGN_RIGHT})
            table.AddCell(New PdfPCell(totalTable) With {.Border = Rectangle.NO_BORDER, .PaddingBottom = 10}) ' Espacio inferior entre total y conceptos



            ' Conceptos de factura con bordes
            Dim conceptsTable As New PdfPTable(2)
            conceptsTable.WidthPercentage = 100
            conceptsTable.SetWidths({80, 20})

            conceptsTable.AddCell(New PdfPCell(New Phrase("CONCEPTOS DE FACTURA", boldFont)) With {.Colspan = 2, .HorizontalAlignment = Element.ALIGN_CENTER, .Border = Rectangle.NO_BORDER})
            conceptsTable.AddCell(New PdfPCell(New Phrase("Costo del período", regularFont)) With {.Border = Rectangle.BOX})
            conceptsTable.AddCell(New PdfPCell(New Phrase($"${persona.CostoPeriodo}", regularFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT, .Border = Rectangle.BOX})
            conceptsTable.AddCell(New PdfPCell(New Phrase("Reconexión", regularFont)) With {.Border = Rectangle.BOX})
            conceptsTable.AddCell(New PdfPCell(New Phrase($"${persona.Reconexion}", regularFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT, .Border = Rectangle.BOX})
            conceptsTable.AddCell(New PdfPCell(New Phrase("Multas", regularFont)) With {.Border = Rectangle.BOX})
            conceptsTable.AddCell(New PdfPCell(New Phrase($"${persona.Multas}", regularFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT, .Border = Rectangle.BOX})
            conceptsTable.AddCell(New PdfPCell(New Phrase("Atrasos", regularFont)) With {.Border = Rectangle.BOX})
            conceptsTable.AddCell(New PdfPCell(New Phrase($"${persona.atrasos}", regularFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT, .Border = Rectangle.BOX})
            conceptsTable.AddCell(New PdfPCell(New Phrase("Recargo por mora", regularFont)) With {.Border = Rectangle.BOX})
            conceptsTable.AddCell(New PdfPCell(New Phrase($"${persona.RecargoMora}", regularFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT, .Border = Rectangle.BOX})
            conceptsTable.AddCell(New PdfPCell(New Phrase("Otros", regularFont)) With {.Border = Rectangle.BOX})
            conceptsTable.AddCell(New PdfPCell(New Phrase($"${persona.otros}", regularFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT, .Border = Rectangle.BOX})
            conceptsTable.AddCell(New PdfPCell(New Phrase("Ajustes al peso", regularFont)) With {.Border = Rectangle.BOX})
            conceptsTable.AddCell(New PdfPCell(New Phrase($"${persona.AjustePeso}", regularFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT, .Border = Rectangle.BOX})
            conceptsTable.AddCell(New PdfPCell(New Phrase("Valor", boldFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT, .Border = Rectangle.BOX})
            conceptsTable.AddCell(New PdfPCell(New Phrase($"${persona.TotalPagar}", boldFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT, .Border = Rectangle.BOX})


            ' Agregar la tabla de conceptos a la tabla principal
            table.AddCell(New PdfPCell(conceptsTable) With {.Border = Rectangle.NO_BORDER})

            ' Footer con mensaje (sin borde)
            table.AddCell(New PdfPCell(New Phrase("‘Un mundo con agua depende de todos, ¡Cuidémosla!’", regularFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER, .Border = Rectangle.NO_BORDER})
            table.AddCell(New PdfPCell(New Phrase($"{persona.Avisos}", titleFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER, .Border = Rectangle.NO_BORDER, .PaddingTop = 2})

            Return table
        End Function

        Friend Sub limpiarListas()
            For Each persona As Persona In listaDePersonas
                persona.Limpiar()
            Next
        End Sub

    End Class

    Public Class Persona
        Public Property IdFactura As String
        Public Property NombreCliente As String
        Public Property NombrePredio As String
        Public Property TelefonoCliente As String
        Public Property PeriodoCobro As String
        Public Property CostoPeriodo As String
        Public Property TotalPagar As String
        Public Property Reconexion As String
        Public Property Multas As String
        Public Property otros As String
        Public Property atrasos As String
        Public Property RecargoMora As String
        Public Property AjustePeso As String
        Public Property Avisos As String
        Public Property FechaPago As String

        'constructor
        Public Sub New(IdFactura As String, NombreCliente As String, NombrePredio As String, TelefonoCliente As String, PeriodoCobro As String, CostoPeriodo As String, TotalPagar As String, Reconexion As String, Multas As String, otros As String, atrasos As String, RecargoMora As String, AjustePeso As String, avisos As String, FechaPago As String)
            Me.IdFactura = IdFactura
            Me.NombreCliente = NombreCliente
            Me.NombrePredio = NombrePredio
            Me.TelefonoCliente = TelefonoCliente
            Me.PeriodoCobro = PeriodoCobro
            Me.CostoPeriodo = CostoPeriodo
            Me.TotalPagar = TotalPagar
            Me.Reconexion = Reconexion
            Me.Multas = Multas
            Me.otros = otros
            Me.atrasos = atrasos
            Me.RecargoMora = RecargoMora
            Me.AjustePeso = AjustePeso
            Me.Avisos = avisos
            Me.FechaPago = FechaPago
        End Sub

        'Public Overrides Function ToString() As String
        '    Return $"Factura: {IdFactura}" & vbCrLf &
        '   $"Cliente: {NombreCliente}" & vbCrLf &
        '   $"Predio: {NombrePredio}" & vbCrLf &
        '   $"Teléfono: {TelefonoCliente}" & vbCrLf &
        '   $"Periodo: {PeriodoCobro}" & vbCrLf &
        '   $"Costo: {CostoPeriodo}" & vbCrLf &
        '   $"Total: {TotalPagar}" & vbCrLf &
        '   $"Reconexión: {Reconexion}" & vbCrLf &
        '   $"Multas: {Multas}" & vbCrLf &
        '   $"Otros: {otros}" & vbCrLf &
        '   $"Atrasos: {atrasos}" & vbCrLf &
        '   $"Recargo Mora: {RecargoMora}" & vbCrLf &
        '   $"Ajuste Peso: {AjustePeso}"
        'End Function

        Public Sub Limpiar()
            Me.IdFactura = Nothing
            Me.NombreCliente = Nothing
            Me.NombrePredio = Nothing
            Me.TelefonoCliente = Nothing
            Me.PeriodoCobro = Nothing
            Me.CostoPeriodo = Nothing
            Me.TotalPagar = Nothing
            Me.Reconexion = Nothing
            Me.Multas = Nothing
            Me.otros = Nothing
            Me.atrasos = Nothing
            Me.RecargoMora = Nothing
            Me.AjustePeso = Nothing
            Me.Avisos = Nothing
            Me.FechaPago = Nothing
        End Sub
    End Class

End Namespace
