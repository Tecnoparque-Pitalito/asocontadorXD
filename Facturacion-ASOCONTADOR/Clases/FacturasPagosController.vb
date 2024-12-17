Imports System.Data.SQLite
Imports iTextSharp.text.xml.simpleparser.handler

Namespace Clases
    Friend Class FacturasPagosController
        Friend Sub ValidarFechaPago()
            Try
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()

                    ' Consulta para seleccionar facturas con FechaPago vencida y estado = 3
                    Dim query As String = "SELECT idFactura
                                                FROM factura
                                                WHERE strftime('%Y-%m', FechaPago) <= strftime('%Y-%m', 'now', '-1 month') AND estado = 3;"
                    Using cmd As New SQLiteCommand(query, conn)
                        ' Usar ExecuteReader ya que estamos recuperando datos
                        Using reader As SQLiteDataReader = cmd.ExecuteReader()
                            If reader.HasRows Then
                                While reader.Read()
                                    ' Obtener el idFactura de cada factura con FechaPago vencida
                                    Dim idFactura As Integer = reader("idFactura")

                                    ' Actualizar el estado a 2 para cada idFactura recuperada
                                    Using updateCmd As New SQLiteCommand("UPDATE factura SET estado = 1, FechaPago = date('now'),  Observaciones = 'No se pagó a tiempo la factura' WHERE idFactura = @idFactura", conn)
                                        updateCmd.Parameters.AddWithValue("@idFactura", idFactura)
                                        updateCmd.ExecuteNonQuery()
                                    End Using
                                End While
                            End If
                        End Using
                    End Using
                End Using
            Catch ex As SQLiteException
                ' Manejo de excepciones de SQLite
                MsgBox("Error de base de datos: " & ex.Message)
            Catch ex As Exception
                ' Manejo de otras excepciones generales
                MsgBox("Ocurrió un error: " & ex.Message)
            End Try
        End Sub

        Friend Sub ListarFacturasPagos(textBox10 As TextBox, nombrePropietario As TextBox, telefono As TextBox, predio As TextBox, matricula As TextBox, dataGridView1 As DataGridView, idpredio As Integer)
            Try
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()
                    Using cmd As New SQLiteCommand("
                SELECT 
                    pre.NombrePredio, 
                    pre.NumeroMatricula,
                    cu.TipoDeUso,
                    us.Nombre AS NombreUsuario,
                    us.telefono,
                    CASE
                        WHEN fac.estado = 1  THEN cf.valor + (cf.valor * per.interesMora / 100)
                        WHEN fac.estado = 6 THEN 5000 + (5000 * per.interesMora / 100)
                        ELSE cf.valor
                    END AS ValorCostoFijo,
                    CASE
                        WHEN fac.estado = 3 AND strftime('%Y-%m', fac.FechaPago) = strftime('%Y-%m', 'now') THEN 'Cobro actual'
                        WHEN fac.estado = 1 THEN 'Cobro por mora'
                        WHEN fac.estado = 4 THEN 'Costo Fijo'
                        WHEN fac.estado = 6 THEN 'Costo fijo con mora'
                        ELSE 'No Aplica'
                    END AS CostoFijo,
                    fac.mes,
                    fac.Periodos_idPeriodos,
                    per.interesMora,
                    fac.Periodos_idPeriodos || ' - ' || fac.mes AS PeriodoCompleto,
                    CASE
                        WHEN fac.Observaciones IS NOT NULL THEN fac.Observaciones
                        ELSE 'no aplica'
                    END AS Observaciones
                FROM
                    predios pre
                JOIN
                    usuarios us ON pre.Usuarios_NumeroCedula = us.NumeroCedula
                JOIN
                    clasificacionuso cu ON cu.idClasificacionUso = pre.ClasificacionUso_idClasificacionUso 
                JOIN
                    costosfijos cf ON cf.ClasificacionUso_idClasificacionUso = pre.ClasificacionUso_idClasificacionUso 
                    AND cf.Periodos_idPeriodos = per.idPeriodos
                JOIN 
                    factura fac ON fac.Predios_idPredios = pre.idPredios        
                JOIN
                    periodos per ON fac.Periodos_idPeriodos = per.idPeriodos 
                WHERE
                    us.NumeroCedula = @cedula 
                    AND fac.estado != 2 
                    AND fac.estado != 5
                    AND strftime('%Y-%m', fac.FechaPago) <= strftime('%Y-%m', 'now')
                    AND pre.idPredios = @predio
                ORDER BY
                    fac.mes;", conn)

                        cmd.Parameters.AddWithValue("@cedula", textBox10.Text)
                        cmd.Parameters.AddWithValue("@predio", idpredio)

                        Using reader As SQLiteDataReader = cmd.ExecuteReader()
                            If reader.HasRows Then
                                Dim dt As New DataTable()
                                dt.Load(reader)
                                If dt.Rows.Count > 0 Then
                                    Dim firstRow As DataRow = dt.Rows(0)
                                    nombrePropietario.Text = firstRow("NombreUsuario").ToString()
                                    telefono.Text = firstRow("telefono").ToString()
                                    predio.Text = firstRow("NombrePredio").ToString()
                                    matricula.Text = firstRow("NumeroMatricula").ToString()
                                End If

                                ' Agregar columna calculada para "Valor"
                                dt.Columns.Add("Valor", GetType(Decimal))

                                For Each row As DataRow In dt.Rows
                                    ' Primero, manejamos el CostoFijo
                                    Select Case row("CostoFijo").ToString()
                                        Case "Cobro actual", "Cobro por mora"
                                            row("Valor") = Convert.ToDecimal(row("ValorCostoFijo"))
                                        Case "Costo Fijo"
                                            row("Valor") = 5000D
                                        Case Else
                                            row("Valor") = Convert.ToDecimal(row("ValorCostoFijo"))
                                    End Select

                                    ' Manejo de Observaciones para extraer el valor numérico
                                    Dim observaciones As String = row("Observaciones").ToString()
                                    If observaciones <> "no aplica" Then
                                        Dim match As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(observaciones, "\d+(\.\d+)?")
                                        If match.Success Then
                                            row("Valor") = Convert.ToDecimal(match.Value)
                                        End If
                                    End If
                                Next

                                ' Configurar columnas en el DataGridView
                                dataGridView1.Columns.Clear()
                                dataGridView1.AutoGenerateColumns = False
                                dataGridView1.AllowUserToAddRows = False
                                dataGridView1.RowHeadersVisible = False
                                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                                ' Agregar columnas al DataGridView
                                Dim colDescripcion As New DataGridViewTextBoxColumn()
                                colDescripcion.HeaderText = "Descripción"
                                colDescripcion.DataPropertyName = "CostoFijo"
                                dataGridView1.Columns.Add(colDescripcion)

                                Dim colPeriodo As New DataGridViewTextBoxColumn()
                                colPeriodo.HeaderText = "Periodo"
                                colPeriodo.DataPropertyName = "PeriodoCompleto"
                                dataGridView1.Columns.Add(colPeriodo)

                                Dim colValor As New DataGridViewTextBoxColumn()
                                colValor.HeaderText = "Valor"
                                colValor.DataPropertyName = "Valor"
                                dataGridView1.Columns.Add(colValor)

                                Dim colNombreYUso As New DataGridViewTextBoxColumn()
                                colNombreYUso.HeaderText = "Uso del Predio"
                                colNombreYUso.DataPropertyName = "TipoDeUso"
                                dataGridView1.Columns.Add(colNombreYUso)

                                ' Asignar el DataTable al DataGridView
                                dataGridView1.DataSource = dt
                            Else
                                MessageBox.Show("Al parecer todo está al día")
                                dataGridView1.Columns.Clear()
                            End If
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Ocurrió un error: " & ex.Message)
            End Try
        End Sub

        Friend Sub generarPago(dataGridView1 As DataGridView, idpredio As Integer, cedula As String)
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()

                ' Iniciar una transacción
                Dim transaction As SQLiteTransaction = conn.BeginTransaction()

                Try
                    For Each row As DataGridViewRow In dataGridView1.Rows
                        ' Ignorar la fila nueva (si no es necesario)
                        If Not row.IsNewRow Then
                            ' Acceder al valor de la columna 2 (índice 1)
                            Dim valorColumna2 As String = row.Cells(1).Value.ToString()

                            ' Dividir el valor en año y mes
                            Dim partes() As String = valorColumna2.Split("-"c)
                            If partes.Length = 2 Then
                                Dim anyo As Integer = Integer.Parse(partes(0).Trim()) ' Año
                                Dim mes As Integer = Integer.Parse(partes(1).Trim()) ' Mes

                                Using cmd As New SQLiteCommand("UPDATE factura 
                            SET estado = 2, FechaPago = strftime('%Y-%m-%d', 'now')
                            WHERE Periodos_idPeriodos = @anyo
                            AND mes = @mes
                            AND Predios_idPredios = (
                                SELECT pre.idPredios 
                                FROM predios pre
                                JOIN usuarios us ON pre.Usuarios_NumeroCedula = us.NumeroCedula
                                WHERE us.NumeroCedula = @cedula
                                AND pre.idPredios = @idpredio
                            );", conn)

                                    cmd.Parameters.AddWithValue("@anyo", anyo)
                                    cmd.Parameters.AddWithValue("@mes", mes)
                                    cmd.Parameters.AddWithValue("@cedula", cedula)
                                    cmd.Parameters.AddWithValue("@idpredio", idpredio)

                                    cmd.ExecuteNonQuery() ' Ejecutar la actualización
                                End Using
                            Else
                                MessageBox.Show("El formato no es válido: " & valorColumna2, "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            End If
                        End If
                    Next

                    ' Confirmar la transacción si todo ha ido bien
                    transaction.Commit()
                    MessageBox.Show("Los pagos se han actualizado correctamente.", "Actualización Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    ' Deshacer la transacción si ocurre un error
                    transaction.Rollback()
                    ' Manejar la excepción aquí (por ejemplo, mostrar un mensaje de error)
                    MessageBox.Show("Error: " & ex.Message, "Error de Actualización", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Sub

        Friend Sub AgregarSaldo(userInput As String, idPredio As Integer)
            Try
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()

                    ' Inicia la transacción
                    Using transaction As SQLiteTransaction = conn.BeginTransaction()
                        Dim fechaActual As DateTime = DateTime.Now
                        Dim IfFactura As Integer = 0
                        Dim saldoNuevo As Double

                        ' Obtener la factura mínima que cumple las condiciones
                        Using minFact As New SQLiteCommand("SELECT MIN(idFactura) as MinFactura 
                                                    FROM factura 
                                                    WHERE Predios_idPredios = @IdPredio 
                                                    AND estado NOT IN (2, 5, 1, 6)", conn, transaction)
                            minFact.Parameters.AddWithValue("@IdPredio", idPredio)
                            Using reader As SQLiteDataReader = minFact.ExecuteReader()
                                If reader.Read() AndAlso Not reader.IsDBNull(reader.GetOrdinal("MinFactura")) Then
                                    IfFactura = Convert.ToInt32(reader("MinFactura"))
                                End If
                            End Using
                        End Using

                        ' Si no se encontró factura válida, salir
                        If IfFactura = 0 Then
                            MsgBox("No se encontró ninguna factura válida para este predio.")
                            Return
                        End If

                        ' Obtener el saldo actual de la factura seleccionada
                        Using saldo As New SQLiteCommand("SELECT saldo 
                                                  FROM factura 
                                                  WHERE idFactura = @IdFactura", conn, transaction)
                            saldo.Parameters.AddWithValue("@IdFactura", IfFactura)
                            Using reader As SQLiteDataReader = saldo.ExecuteReader()
                                If reader.Read() AndAlso Not reader.IsDBNull(reader.GetOrdinal("saldo")) Then
                                    Dim saldoConsulta As Double = Convert.ToDouble(reader("saldo"))
                                    saldoNuevo = saldoConsulta + Convert.ToDouble(userInput)
                                Else
                                    MsgBox("No se pudo obtener el saldo actual de la factura.")
                                    Return
                                End If
                            End Using
                        End Using

                        ' Actualizar el saldo en la factura
                        Using cmd As New SQLiteCommand("UPDATE factura 
                                                SET saldo = @saldo 
                                                WHERE idFactura = @IdFactura", conn, transaction)
                            cmd.Parameters.AddWithValue("@saldo", saldoNuevo)
                            cmd.Parameters.AddWithValue("@IdFactura", IfFactura)
                            Dim result As Integer = cmd.ExecuteNonQuery()
                            If result > 0 Then
                                MsgBox("Adelanto con éxito.")
                            Else
                                MsgBox("Error durante la operación: no se encontraron registros que actualizar.")
                            End If
                        End Using

                        ' Confirmar la transacción
                        transaction.Commit()
                    End Using
                End Using
            Catch ex As Exception
                MsgBox("Ocurrió un error: " & ex.Message) ' Mensaje de error para depuración
            End Try
        End Sub


    End Class
End Namespace
