Imports System.Data.SQLite

Namespace Clases
    Friend Class ReportesController
        Friend Sub ReportesPeriodo(estado As Integer, dataGridView2 As DataGridView)
            Try
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()
                    Using cmd As New SQLiteCommand("SELECT 
                us.Nombre AS NombreUsuario,
                us.NumeroCedula,
                p.NombrePredio,             
                cu.TipoDeUso,               
                per.idPeriodos AS Periodo, 
                COUNT(fac.idFactura) AS ContadorEstados, 
                SUM(cf.valor) AS ValorTotalEstados 
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
            JOIN
                clasificacionuso cu ON cu.idClasificacionUso = p.ClasificacionUso_idClasificacionUso 
            WHERE 
                fac.estado = @estado
            GROUP BY 
                us.Nombre, us.NumeroCedula, p.NombrePredio, cu.TipoDeUso, per.idPeriodos
            ORDER BY 
                us.Nombre, per.idPeriodos, p.NombrePredio;", conn)

                        ' Añadir el parámetro para "estado"
                        cmd.Parameters.AddWithValue("@estado", estado)

                        ' Ejecutar el comando
                        Using reader As SQLiteDataReader = cmd.ExecuteReader()
                            ' Limpiar las columnas y filas existentes en el DataGridView
                            dataGridView2.Rows.Clear()
                            dataGridView2.Columns.Clear()

                            ' Agregar columnas al DataGridView
                            dataGridView2.Columns.Add("NombreUsuario", "Nombre del Usuario")
                            dataGridView2.Columns.Add("NumeroCedula", "Número de Cédula")
                            dataGridView2.Columns.Add("NombrePredio", "Nombre del Predio")
                            dataGridView2.Columns.Add("TipoDeUso", "Tipo de Uso")
                            dataGridView2.Columns.Add("Periodo", "Periodo")
                            dataGridView2.Columns.Add("ContadorEstados", "Número de Facturas")
                            dataGridView2.Columns.Add("ValorTotalEstados", "Valor Total")

                            ' Ajustar el tamaño de las columnas para que ocupen todo el ancho del DataGridView
                            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                            ' Ajustar el tamaño de las filas
                            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

                            ' Leer los datos y agregarlos al DataGridView
                            While reader.Read()
                                dataGridView2.Rows.Add(reader("NombreUsuario").ToString(),
                                               reader("NumeroCedula").ToString(),
                                               reader("NombrePredio").ToString(),
                                               reader("TipoDeUso").ToString(),
                                               reader("Periodo").ToString(),
                                               reader("ContadorEstados").ToString(),
                                               String.Format("{0:C}", reader("ValorTotalEstados"))) ' Formato de moneda
                            End While
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error al cargar los reportes: " & ex.Message)
            End Try
        End Sub


        Friend Sub RealizarReportesMes(dataGridView1 As DataGridView, mes As Integer, anyo As Integer)
            Try
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()
                    Using cmd As New SQLiteCommand("
                    SELECT 
                        p.NombrePredio,         
                        us.Nombre,
                        us.NumeroCedula,
                        us.telefono,
                        CASE 
                            WHEN fac.estado = 3 THEN 'Pendiente'
                            WHEN fac.estado = 2 THEN 'Pago'
                            WHEN fac.estado = 1 THEN 'Vencido'
                            ELSE 'Estado desconocido'
                        END AS EstadoFactura, 
                        fac.mes,              
                        per.idPeriodos,       
                        per.InicioPeriodo,    
                        per.FinPeriodo,       
                        cf.valor AS ValorCostoFijo, 
                        cu.TipoDeUso            
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
                    JOIN
                        clasificacionuso cu ON cu.idClasificacionUso = p.ClasificacionUso_idClasificacionUso 
                    WHERE 
                        fac.mes = @mes AND per.idPeriodos = @anyo
                    ORDER BY
                        fac.estado, fac.mes;
                    ", conn)

                        ' Agregar el parámetro del mes
                        cmd.Parameters.AddWithValue("@mes", mes)
                        cmd.Parameters.AddWithValue("@anyo", anyo)

                        ' Ejecutar el lector de datos
                        Using reader As SQLiteDataReader = cmd.ExecuteReader()
                            ' Limpiar el DataGridView antes de llenarlo
                            dataGridView1.Rows.Clear()
                            dataGridView1.Columns.Clear()

                            ' Agregar las columnas al DataGridView
                            dataGridView1.Columns.Add("NombrePredio", "Nombre Predio")
                            dataGridView1.Columns.Add("Nombre", "Nombre")
                            dataGridView1.Columns.Add("NumeroCedula", "Número de Cédula")
                            dataGridView1.Columns.Add("telefono", "Número de Teléfono")
                            dataGridView1.Columns.Add("EstadoFactura", "Estado")  ' Cambiado a EstadoFactura
                            dataGridView1.Columns.Add("Mes", "Mes")
                            dataGridView1.Columns.Add("idPeriodos", "ID Periodos")
                            dataGridView1.Columns.Add("ValorCostoFijo", "Valor")  ' Cambiado a ValorCostoFijo
                            dataGridView1.Columns.Add("TipoDeUso", "Tipo de Uso")

                            ' Ajustar el tamaño de las columnas para que ocupen todo el ancho del DataGridView
                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                            ' Ajustar el tamaño de las filas
                            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

                            ' Leer los datos y agregarlos al DataGridView
                            While reader.Read()
                                dataGridView1.Rows.Add(reader("NombrePredio"), reader("Nombre"), reader("NumeroCedula"), reader("telefono"),
                                                       reader("EstadoFactura"), reader("mes"), reader("idPeriodos"),
                                                       reader("ValorCostoFijo"), reader("TipoDeUso"))
                            End While
                        End Using

                    End Using
                End Using
            Catch ex As Exception
                ' Manejar la excepción (puedes mostrar un mensaje de error si lo deseas)
                MessageBox.Show("Error en la consulta a la base de datos: Error Filtro mes: " & ex.Message)
            End Try
        End Sub

        Friend Sub ReporteMesMorosos(dataGridView3 As DataGridView, mes As Integer, anyo As Integer, filter As Integer)
            Try
                Using conn As New SQLiteConnection(connectionString)
                    conn.Open()
                    Using cmd As New SQLiteCommand("
                    SELECT 
                        p.NombrePredio,         
                        us.Nombre,
                        us.NumeroCedula,
                        us.telefono,
                        CASE 
                            WHEN fac.estado = 3 THEN 'Pendiente'
                            WHEN fac.estado = 2 THEN 'Pago'
                            WHEN fac.estado = 1 THEN 'Vencido'
                            ELSE 'Estado desconocido'
                        END AS EstadoFactura, 
                        fac.mes,              
                        per.idPeriodos,       
                        per.InicioPeriodo,    
                        per.FinPeriodo,       
                        cf.valor AS ValorCostoFijo, 
                        cu.TipoDeUso            
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
                    JOIN
                        clasificacionuso cu ON cu.idClasificacionUso = p.ClasificacionUso_idClasificacionUso 
                    WHERE 
                        fac.mes = @mes AND per.idPeriodos = @anyo AND fac.estado = @filter
                    ORDER BY
                        fac.estado, fac.mes;
                    ", conn)

                        ' Agregar el parámetro del mes
                        cmd.Parameters.AddWithValue("@mes", mes)
                        cmd.Parameters.AddWithValue("@anyo", anyo)
                        cmd.Parameters.AddWithValue("@filter", filter)

                        ' Ejecutar el lector de datos
                        Using reader As SQLiteDataReader = cmd.ExecuteReader()
                            ' Limpiar el DataGridView antes de llenarlo
                            dataGridView3.Rows.Clear()
                            dataGridView3.Columns.Clear()

                            ' Agregar las columnas al DataGridView
                            dataGridView3.Columns.Add("NombrePredio", "Nombre Predio")
                            dataGridView3.Columns.Add("Nombre", "Nombre")
                            dataGridView3.Columns.Add("NumeroCedula", "Número de Cédula")
                            dataGridView3.Columns.Add("telefono", "Número de Teléfono")
                            dataGridView3.Columns.Add("EstadoFactura", "Estado")  ' Cambiado a EstadoFactura
                            dataGridView3.Columns.Add("Mes", "Mes")
                            dataGridView3.Columns.Add("idPeriodos", "ID Periodos")
                            dataGridView3.Columns.Add("ValorCostoFijo", "Valor")  ' Cambiado a ValorCostoFijo
                            dataGridView3.Columns.Add("TipoDeUso", "Tipo de Uso")

                            ' Ajustar el tamaño de las columnas para que ocupen todo el ancho del DataGridView
                            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                            ' Ajustar el tamaño de las filas
                            dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

                            ' Leer los datos y agregarlos al DataGridView
                            While reader.Read()
                                dataGridView3.Rows.Add(reader("NombrePredio"), reader("Nombre"), reader("NumeroCedula"), reader("telefono"),
                                                       reader("EstadoFactura"), reader("mes"), reader("idPeriodos"),
                                                       reader("ValorCostoFijo"), reader("TipoDeUso"))
                            End While
                        End Using

                    End Using
                End Using
            Catch ex As Exception
                ' Manejar la excepción (puedes mostrar un mensaje de error si lo deseas
                MessageBox.Show("Error en la consulta a la base de datos: Error Filtro mes morosos: " & ex.Message)
            End Try
        End Sub
    End Class
End Namespace



'SELECT 
'	p.InicioPeriodo,
'	p.FinPeriodo,
'    CASE 
'      WHEN fac.estado = 3 THEN 'Pendiente'
'      WHEN fac.estado = 2 THEN 'Pago'
'      WHEN fac.estado = 1 THEN 'Vencido'
'      ELSE 'Estado desconocido'
'	END AS EstadoFactura,
'	cf.valor as costosfijos,
'	cu.TipoDeUso,
'	voc.valor as valorotroscostos,
'	oc.CostoAsociado
'
'from periodos p
'JOIN factura fac ON fac.Periodos_idPeriodos = p.idPeriodos
'JOIN costosfijos cf ON cf.Periodos_idPeriodos = idPeriodos
'JOIN clasificacionuso cu ON cf.ClasificacionUso_idClasificacionUso = idClasificacionUso
'JOIN valorotroscostos voc ON voc.Periodos_idPeriodos = p.idPeriodos
'JOIN otroscostos oc ON voc.OtrosCostos_idOtrosCostos = oc.idOtrosCostos



