Imports System.Data.SQLite
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Namespace Clases
    Friend Class FacturasMasivasGeneratorController






        Friend Function GenerarFacturas(anyo As Integer, mes As Integer) As Boolean
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()
                Using transaction = conn.BeginTransaction
                    Try
                        ' Variables para almacenar los datos
                        Dim Cedula As Integer
                        Dim Nombre As String
                        Dim Telefono As String
                        Dim NombrePredio As String
                        Dim idPredio As Integer
                        Dim saldo As Integer
                        Dim estado As Integer
                        Dim idfactura As Integer
                        Dim fechaPago As String
                        Dim generarPDF As Boolean
                        Dim periodoCobro As String = $"{mes}/{anyo}"
                        Dim facturasParaImprimir As New List(Of DocumentData)

                        ' Consulta para obtener los usuarios
                        Using ListUsuarios As New SQLiteCommand("SELECT NumeroCedula, Nombre, telefono FROM usuarios", conn)
                            Using ListUsuariosReader As SQLiteDataReader = ListUsuarios.ExecuteReader
                                If ListUsuariosReader.HasRows Then
                                    While ListUsuariosReader.Read()
                                        Cedula = Convert.ToInt32(ListUsuariosReader("NumeroCedula"))
                                        Nombre = ListUsuariosReader("Nombre").ToString()
                                        Telefono = ListUsuariosReader("telefono").ToString()

                                        ' Consulta para obtener los predios del usuario
                                        Using ListarPredios As New SQLiteCommand("SELECT idPredios, NombrePredio FROM predios WHERE Usuarios_NumeroCedula = @cedula", conn)
                                            ListarPredios.Parameters.AddWithValue("@cedula", Cedula)
                                            Using ListarPrediosReader As SQLiteDataReader = ListarPredios.ExecuteReader
                                                If ListarPrediosReader.HasRows Then
                                                    While ListarPrediosReader.Read()
                                                        NombrePredio = ListarPrediosReader("NombrePredio").ToString()
                                                        idPredio = Convert.ToInt32(ListarPrediosReader("idPredios"))

                                                        ' Consulta para obtener la factura del predio en el mes y año especificados
                                                        Using listarFactura As New SQLiteCommand("SELECT saldo, estado, idFactura, FechaPago FROM factura WHERE mes <= @mes AND Periodos_idPeriodos <= @anyo AND Predios_idPredios = @IdPredio", conn)
                                                            listarFactura.Parameters.AddWithValue("@mes", mes)
                                                            listarFactura.Parameters.AddWithValue("@anyo", anyo)
                                                            listarFactura.Parameters.AddWithValue("@IdPredio", idPredio)
                                                            Using listarfacturaReader As SQLiteDataReader = listarFactura.ExecuteReader
                                                                If listarfacturaReader.HasRows Then
                                                                    While listarfacturaReader.Read()
                                                                        saldo = Convert.ToInt32(listarfacturaReader("saldo"))
                                                                        estado = Convert.ToInt32(listarfacturaReader("estado"))
                                                                        idfactura = Convert.ToInt32(listarfacturaReader("idFactura"))
                                                                        fechaPago = listarfacturaReader("FechaPago").ToString()
                                                                        If saldo = 0 Then
                                                                            If estado <> 5 AndAlso estado <> 2 Then
                                                                                facturasParaImprimir.Add(New DocumentData(Nombre, NombrePredio, idfactura, periodoCobro, Telefono, fechaPago, saldo))
                                                                            End If
                                                                        Else

                                                                        End If

                                                                    End While
                                                                End If
                                                            End Using
                                                        End Using
                                                    End While
                                                End If
                                            End Using
                                        End Using
                                    End While
                                End If
                            End Using
                        End Using

                        ' Generación de PDFs
                        If facturasParaImprimir.Count > 0 Then
                            generarPDF = GenerarPDFMasivo(facturasParaImprimir)
                            If Not generarPDF Then
                                transaction.Rollback()
                                MsgBox("Error en la generación de las facturas.")
                                Return False
                            End If
                        End If

                        ' Confirmar la transacción
                        transaction.Commit()
                        MsgBox("Facturas generadas correctamente.", MsgBoxStyle.Information, "Éxito")
                        Return True
                    Catch ex As Exception
                        transaction.Rollback()
                        MsgBox("Fallo al generar todas las facturas: " & ex.Message, MsgBoxStyle.Critical, "Error")
                        Return False
                    End Try
                End Using
            End Using
        End Function




        Private Function GenerarPDFMasivo(facturas As List(Of DocumentData)) As Boolean
            Try
                Dim rutaDocumentos As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                Dim rutaFacturas As String = Path.Combine(rutaDocumentos, "asocontadorFacturas")
                If Not Directory.Exists(rutaFacturas) Then Directory.CreateDirectory(rutaFacturas)

                Dim nombreArchivo As String = $"Facturas_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                Dim rutaArchivo As String = Path.Combine(rutaFacturas, nombreArchivo)

                Dim documento As New Document(PageSize.HALFLETTER, 36, 36, 36, 36)
                Dim escritorPdf As PdfWriter = PdfWriter.GetInstance(documento, New FileStream(rutaArchivo, FileMode.Create))

                ' Añadir el encabezado y pie de página
                escritorPdf.PageEvent = New EncabezadoPieDePagina()
                documento.Open()

                Dim contador As Integer = 0
                For Each factura In facturas
                    ' Verifica si debe agregar una nueva página o si coloca dos facturas por página
                    If contador > 0 AndAlso contador Mod 2 = 0 Then
                        documento.NewPage()
                    End If
                    documento.Add(New Paragraph($"Factura de {factura.Nombre}", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)))
                    documento.Add(New Paragraph($"Predio: {factura.NombrePredio}", FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                    documento.Add(New Paragraph($"ID Factura: {factura.IdFactura}", FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                    documento.Add(New Paragraph($"Periodo de Cobro: {factura.PeriodoCobro}", FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                    documento.Add(New Paragraph($"Teléfono: {factura.Telefono}", FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                    documento.Add(New Paragraph($"Fecha de Pago: {factura.FechaPago}", FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                    documento.Add(New Paragraph($"Saldo: {factura.Saldo}", FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                    documento.Add(New Paragraph(""))

                    contador += 1
                Next

                documento.Close()
                escritorPdf.Close()
                MsgBox($"Archivo generado en: {rutaArchivo}")
                Return True
            Catch ex As Exception
                MsgBox("Error al crear el archivo PDF: " & ex.Message, MsgBoxStyle.Critical, "Error")
                Return False
            End Try
        End Function

        Friend Class DocumentData
            Public Sub New(nombre As String, nombrePredio As String, idFactura As Integer, periodoCobro As String, telefono As String, fechaPago As String, saldo As Integer)
                Me.Nombre = nombre
                Me.NombrePredio = nombrePredio
                Me.IdFactura = idFactura
                Me.PeriodoCobro = periodoCobro
                Me.Telefono = telefono
                Me.FechaPago = fechaPago
                Me.Saldo = saldo
            End Sub

            Public Property Nombre As String
            Public Property NombrePredio As String
            Public Property IdFactura As Integer
            Public Property PeriodoCobro As String
            Public Property Telefono As String
            Public Property FechaPago As String
            Public Property Saldo As Integer
        End Class

        Friend Class EncabezadoPieDePagina
            Inherits PdfPageEventHelper

            Public Overrides Sub OnEndPage(writer As PdfWriter, document As Document)
                Dim canvas As PdfContentByte = writer.DirectContent
                Dim fuenteEncabezado As Font = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD, BaseColor.BLACK)
                Dim encabezado As String = "Encabezado personalizado"
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_CENTER, New Phrase(encabezado, fuenteEncabezado), document.PageSize.Width / 2, document.PageSize.Height - 30, 0)

                Dim fuentePie As Font = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL, BaseColor.BLACK)
                Dim piePagina As String = $"Página {writer.PageNumber}"
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_CENTER, New Phrase(piePagina, fuentePie), document.PageSize.Width / 2, 15, 0)
            End Sub
        End Class
    End Class
End Namespace
