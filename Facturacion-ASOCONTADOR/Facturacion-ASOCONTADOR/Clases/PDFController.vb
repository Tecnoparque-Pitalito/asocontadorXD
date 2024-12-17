Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Namespace Clases
    Friend Class PDFController

        Friend Sub reportesInternosPeriodo(dataGridView2 As DataGridView, combobox As ComboBox)
            Try
                ' Obtener la ruta de la carpeta Documentos del usuario
                Dim rutaDocumentos As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                ' Definir la ruta de la carpeta asocontadorFacturas
                Dim rutaFacturas As String = Path.Combine(rutaDocumentos, "AsocontadorReportes")

                ' Verificar si la carpeta no existe, si no, crearla
                If Not Directory.Exists(rutaFacturas) Then
                    Directory.CreateDirectory(rutaFacturas)
                End If

                ' Nombre del archivo PDF
                Dim nombreArchivo As String = "Factura_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".pdf"
                Dim rutaArchivo As String = Path.Combine(rutaFacturas, nombreArchivo)

                ' Crear el documento PDF
                Dim documento As New Document(PageSize.A4, 50, 50, 25, 50) ' Espacio inferior aumentado
                Dim escritorPdf As PdfWriter = PdfWriter.GetInstance(documento, New FileStream(rutaArchivo, FileMode.Create))

                ' Establecer el evento de página para el encabezado y el pie de página
                escritorPdf.PageEvent = New EncabezadoPieDePagina()

                ' Abrir el documento para escritura
                documento.Open()
                documento.Add(New Paragraph(" "))
                documento.Add(New Paragraph(" "))
                documento.Add(New Paragraph(" "))
                documento.Add(New Paragraph($"Reportes de Usuarios en estado: {combobox.SelectedItem} por periodos."))
                documento.Add(New Paragraph(" "))
                documento.Add(New Paragraph(" "))
                documento.Add(New Paragraph(" "))

                Dim reportesOtros As New PdfPTable(7)

                reportesOtros.WidthPercentage = 100
                reportesOtros.DefaultCell.Border = PdfPCell.NO_BORDER

                Dim colorFondoCeldas As BaseColor = New BaseColor(117, 197, 227, 100)
                Dim EspcioTablas As Single = 10.0F

                ' Agregar encabezados
                reportesOtros.AddCell(New PdfPCell(New Phrase("Nombre Usuario")) With {.Border = PdfPCell.BOTTOM_BORDER, .BackgroundColor = colorFondoCeldas, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                reportesOtros.AddCell(New PdfPCell(New Phrase("Número de Cédula")) With {.Border = PdfPCell.BOTTOM_BORDER Or PdfPCell.LEFT_BORDER, .BackgroundColor = colorFondoCeldas, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                reportesOtros.AddCell(New PdfPCell(New Phrase("Nombre del Predio")) With {.Border = PdfPCell.BOTTOM_BORDER Or PdfPCell.LEFT_BORDER, .BackgroundColor = colorFondoCeldas, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                reportesOtros.AddCell(New PdfPCell(New Phrase("Tipo de Uso")) With {.Border = PdfPCell.BOTTOM_BORDER Or PdfPCell.LEFT_BORDER, .BackgroundColor = colorFondoCeldas, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                reportesOtros.AddCell(New PdfPCell(New Phrase("Periodo")) With {.Border = PdfPCell.BOTTOM_BORDER Or PdfPCell.LEFT_BORDER, .BackgroundColor = colorFondoCeldas, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                reportesOtros.AddCell(New PdfPCell(New Phrase("Numero de facturas")) With {.Border = PdfPCell.BOTTOM_BORDER Or PdfPCell.LEFT_BORDER, .BackgroundColor = colorFondoCeldas, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                reportesOtros.AddCell(New PdfPCell(New Phrase("Valor Total")) With {.Border = PdfPCell.BOTTOM_BORDER Or PdfPCell.LEFT_BORDER, .BackgroundColor = colorFondoCeldas, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})

                ' Agregar las filas del DataGridView
                For Each row As DataGridViewRow In dataGridView2.Rows
                    If Not row.IsNewRow Then
                        reportesOtros.AddCell(New PdfPCell(New Phrase(row.Cells(0).Value.ToString())) With {.Border = PdfPCell.TOP_BORDER, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                        reportesOtros.AddCell(New PdfPCell(New Phrase(row.Cells(1).Value.ToString())) With {.Border = PdfPCell.LEFT_BORDER Or PdfPCell.TOP_BORDER, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                        reportesOtros.AddCell(New PdfPCell(New Phrase(row.Cells(2).Value.ToString())) With {.Border = PdfPCell.LEFT_BORDER Or PdfPCell.TOP_BORDER, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                        reportesOtros.AddCell(New PdfPCell(New Phrase(row.Cells(3).Value.ToString())) With {.Border = PdfPCell.LEFT_BORDER Or PdfPCell.TOP_BORDER, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                        reportesOtros.AddCell(New PdfPCell(New Phrase(row.Cells(4).Value.ToString())) With {.Border = PdfPCell.LEFT_BORDER Or PdfPCell.TOP_BORDER, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                        reportesOtros.AddCell(New PdfPCell(New Phrase(row.Cells(5).Value.ToString())) With {.Border = PdfPCell.LEFT_BORDER Or PdfPCell.TOP_BORDER, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                        reportesOtros.AddCell(New PdfPCell(New Phrase(row.Cells(6).Value.ToString())) With {.Border = PdfPCell.LEFT_BORDER Or PdfPCell.TOP_BORDER, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                    End If
                Next

                documento.Add(reportesOtros)

                ' Cerrar el documento después de agregar todo
                documento.Close()

                ' Cerrar el escritor después de cerrar el documento
                escritorPdf.Close()

                ' Notificar al usuario que el PDF ha sido creado
                MsgBox("El archivo PDF ha sido creado en la ruta: " & rutaArchivo, MsgBoxStyle.Information, "Factura Creada")

            Catch ex As Exception
                ' Manejar excepciones si algo sale mal
                MsgBox("Error al crear el archivo PDF: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End Try
        End Sub



        Friend Sub reportesInternos(dataGridView2 As DataGridView, ComboBox1 As ComboBox, anyo As Integer, mes As Integer)
            Try
                ' Obtener la ruta de la carpeta Documentos del usuario
                Dim rutaDocumentos As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                ' Definir la ruta de la carpeta asocontadorFacturas
                Dim rutaFacturas As String = Path.Combine(rutaDocumentos, "AsocontadorReportes")

                ' Verificar si la carpeta no existe, si no, crearla
                If Not Directory.Exists(rutaFacturas) Then
                    Directory.CreateDirectory(rutaFacturas)
                End If

                ' Nombre del archivo PDF
                Dim nombreArchivo As String = "Factura_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".pdf"
                Dim rutaArchivo As String = Path.Combine(rutaFacturas, nombreArchivo)

                ' Crear el documento PDF
                Dim documento As New Document(PageSize.A4, 50, 50, 25, 50) ' Espacio inferior aumentado
                Dim escritorPdf As PdfWriter = PdfWriter.GetInstance(documento, New FileStream(rutaArchivo, FileMode.Create))

                ' Establecer el evento de página para el encabezado y el pie de página
                escritorPdf.PageEvent = New EncabezadoPieDePagina()

                ' Abrir el documento para escritura
                documento.Open()
                documento.Add(New Paragraph(" "))
                documento.Add(New Paragraph(" "))
                documento.Add(New Paragraph(" "))
                documento.Add(New Paragraph($"Reportes del período {anyo} en el mes {mes} de los usuarios con estado: {ComboBox1.SelectedItem}"))
                documento.Add(New Paragraph(" "))
                documento.Add(New Paragraph(" "))

                Dim reportesOtros As New PdfPTable(9)

                reportesOtros.WidthPercentage = 100
                reportesOtros.DefaultCell.Border = PdfPCell.NO_BORDER

                Dim colorFondoCeldas As BaseColor = New BaseColor(117, 197, 227, 100)
                Dim EspcioTablas As Single = 10.0F

                ' Agregar encabezados
                reportesOtros.AddCell(New PdfPCell(New Phrase("Nombre predio")) With {.Border = PdfPCell.BOTTOM_BORDER, .BackgroundColor = colorFondoCeldas, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                reportesOtros.AddCell(New PdfPCell(New Phrase("Nombre Usuario")) With {.Border = PdfPCell.BOTTOM_BORDER Or PdfPCell.LEFT_BORDER, .BackgroundColor = colorFondoCeldas, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                reportesOtros.AddCell(New PdfPCell(New Phrase("Numero de cedula")) With {.Border = PdfPCell.BOTTOM_BORDER Or PdfPCell.LEFT_BORDER, .BackgroundColor = colorFondoCeldas, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                reportesOtros.AddCell(New PdfPCell(New Phrase("Numero de telefono")) With {.Border = PdfPCell.BOTTOM_BORDER Or PdfPCell.LEFT_BORDER, .BackgroundColor = colorFondoCeldas, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                reportesOtros.AddCell(New PdfPCell(New Phrase("Estado")) With {.Border = PdfPCell.BOTTOM_BORDER Or PdfPCell.LEFT_BORDER, .BackgroundColor = colorFondoCeldas, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                reportesOtros.AddCell(New PdfPCell(New Phrase("Mes")) With {.Border = PdfPCell.BOTTOM_BORDER Or PdfPCell.LEFT_BORDER, .BackgroundColor = colorFondoCeldas, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                reportesOtros.AddCell(New PdfPCell(New Phrase("Periodo")) With {.Border = PdfPCell.BOTTOM_BORDER Or PdfPCell.LEFT_BORDER, .BackgroundColor = colorFondoCeldas, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                reportesOtros.AddCell(New PdfPCell(New Phrase("Valor")) With {.Border = PdfPCell.BOTTOM_BORDER Or PdfPCell.LEFT_BORDER, .BackgroundColor = colorFondoCeldas, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                reportesOtros.AddCell(New PdfPCell(New Phrase("Tipo de uso")) With {.Border = PdfPCell.BOTTOM_BORDER Or PdfPCell.LEFT_BORDER, .BackgroundColor = colorFondoCeldas, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})

                ' Agregar las filas del DataGridView
                For Each row As DataGridViewRow In dataGridView2.Rows
                    If Not row.IsNewRow Then
                        reportesOtros.AddCell(New PdfPCell(New Phrase(row.Cells(0).Value.ToString())) With {.Border = PdfPCell.TOP_BORDER, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                        reportesOtros.AddCell(New PdfPCell(New Phrase(row.Cells(1).Value.ToString())) With {.Border = PdfPCell.LEFT_BORDER Or PdfPCell.TOP_BORDER, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                        reportesOtros.AddCell(New PdfPCell(New Phrase(row.Cells(2).Value.ToString())) With {.Border = PdfPCell.LEFT_BORDER Or PdfPCell.TOP_BORDER, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                        reportesOtros.AddCell(New PdfPCell(New Phrase(row.Cells(3).Value.ToString())) With {.Border = PdfPCell.LEFT_BORDER Or PdfPCell.TOP_BORDER, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                        reportesOtros.AddCell(New PdfPCell(New Phrase(row.Cells(4).Value.ToString())) With {.Border = PdfPCell.LEFT_BORDER Or PdfPCell.TOP_BORDER, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                        reportesOtros.AddCell(New PdfPCell(New Phrase(row.Cells(5).Value.ToString())) With {.Border = PdfPCell.LEFT_BORDER Or PdfPCell.TOP_BORDER, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                        reportesOtros.AddCell(New PdfPCell(New Phrase(row.Cells(6).Value.ToString())) With {.Border = PdfPCell.LEFT_BORDER Or PdfPCell.TOP_BORDER, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                        reportesOtros.AddCell(New PdfPCell(New Phrase(row.Cells(7).Value.ToString())) With {.Border = PdfPCell.LEFT_BORDER Or PdfPCell.TOP_BORDER, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                        reportesOtros.AddCell(New PdfPCell(New Phrase(row.Cells(8).Value.ToString())) With {.Border = PdfPCell.LEFT_BORDER Or PdfPCell.TOP_BORDER, .PaddingBottom = EspcioTablas, .PaddingTop = EspcioTablas})
                    End If
                Next

                documento.Add(reportesOtros)

                ' Cerrar el documento después de agregar todo
                documento.Close()

                ' Cerrar el escritor después de cerrar el documento
                escritorPdf.Close()

                ' Notificar al usuario que el PDF ha sido creado
                MsgBox("El archivo PDF ha sido creado en la ruta: " & rutaArchivo, MsgBoxStyle.Information, "Factura Creada")

            Catch ex As Exception
                ' Manejar excepciones si algo sale mal
                MsgBox("Error al crear el archivo PDF: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End Try
        End Sub


        Friend Sub NuevaFactura(facturas As List(Of Dictionary(Of String, Object)))
            Try
                ' Configuración del archivo PDF
                Dim rutaDocumentos As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                Dim rutaFacturas As String = Path.Combine(rutaDocumentos, "asocontadorFacturas")
                If Not Directory.Exists(rutaFacturas) Then Directory.CreateDirectory(rutaFacturas)

                Dim nombreArchivo As String = $"Factura_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                Dim rutaArchivo As String = Path.Combine(rutaFacturas, nombreArchivo)
                Dim documento As New Document(PageSize.A4, 50, 50, 25, 50)
                Dim escritorPdf As PdfWriter = PdfWriter.GetInstance(documento, New FileStream(rutaArchivo, FileMode.Create))

                documento.Open()

                ' Generar cada factura en la lista
                For i As Integer = 0 To facturas.Count - 1 Step 2
                    Dim tablaPrincipal As New PdfPTable(1) With {.WidthPercentage = 100}

                    ' Primera factura
                    Dim factura1 As Dictionary(Of String, Object) = facturas(i)
                    Dim tablaFactura1 As New PdfPTable(3) With {.WidthPercentage = 100}
                    GenerarContenidoFactura(tablaFactura1, factura1)
                    tablaPrincipal.AddCell(New PdfPCell(tablaFactura1) With {.Border = PdfPCell.NO_BORDER})

                    ' Segunda factura, si existe
                    If i + 1 < facturas.Count Then
                        Dim factura2 As Dictionary(Of String, Object) = facturas(i + 1)
                        Dim tablaFactura2 As New PdfPTable(3) With {.WidthPercentage = 100}
                        GenerarContenidoFactura(tablaFactura2, factura2)
                        tablaPrincipal.AddCell(New PdfPCell(tablaFactura2) With {.Border = PdfPCell.NO_BORDER})
                    End If

                    ' Agregar tabla principal al documento
                    documento.Add(tablaPrincipal)

                    ' Agregar nueva página si hay más facturas
                    If i + 2 < facturas.Count Then
                        documento.NewPage()
                    End If
                Next

                documento.Close()
                escritorPdf.Close()
                MsgBox("El archivo PDF ha sido creado en la ruta: " & rutaArchivo, MsgBoxStyle.Information, "Factura Creada")

            Catch ex As Exception
                MsgBox("Error al crear el archivo PDF: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End Try
        End Sub

        ' Función auxiliar para generar el contenido de cada factura en su tabla
        Private Sub GenerarContenidoFactura(tablaFactura As PdfPTable, facturaDatos As Dictionary(Of String, Object))
            Dim nombreCliente As String = facturaDatos("NombreCliente").ToString()
            Dim telefonoCliente As String = facturaDatos("TelefonoCliente").ToString()
            Dim nombrePredio As String = facturaDatos("NombrePredio").ToString()
            Dim facturaID As String = facturaDatos("FacturaID").ToString()
            Dim periodoCobro As String = facturaDatos("PeriodoCobro").ToString()
            Dim fechaFin As String = facturaDatos("FechaFin").ToString()
            Dim observaciones As String = facturaDatos("Observaciones").ToString()
            Dim dgv1 As List(Of String) = CType(facturaDatos("DataGridView1"), List(Of String))
            Dim dgv2 As List(Of String) = CType(facturaDatos("DataGridView2"), List(Of String))
            Dim fontanero As String = facturaDatos("Fontanero").ToString()

            ' Crear tabla de imagen y detalles
            Dim tablaImagen As New PdfPTable(2) With {.WidthPercentage = 100}
            tablaImagen.SetWidths(New Single() {0.8F, 0.2F})
            Dim phrase As New Phrase()
            phrase.Add("ASOCIACIÓN DE USUARIOS DEL DISTRITO DE ADECUACIÓN DE TIERRAS, DE PEQUEÑA ESCALA CONTADOR 'ASOCONTADOR'")
            phrase.Add(Chunk.NEWLINE)
            phrase.Add("NIT. 900.544.992-0")
            phrase.Add(Chunk.NEWLINE)
            phrase.Add("Dirección oficina: Carrera 10a este # 3-93 barrio Villas de San Gabriel")
            phrase.Add(Chunk.NEWLINE)
            phrase.Add("Teléfono Oficina: 3202363996")
            phrase.Add(Chunk.NEWLINE)
            phrase.Add($"Fontanero: {fontanero}")
            phrase.Add(Chunk.NEWLINE)
            phrase.Add("Pagos: Cuenta de Ahorros de Bancolombia N° 453868061-48 Asocontador")

            tablaImagen.AddCell(New PdfPCell(phrase) With {.Border = PdfPCell.NO_BORDER})

            ' Insertar la imagen
            Dim imagenRuta As String = Path.Combine(Environment.CurrentDirectory, "Imagenes", "Icono.png")
            If File.Exists(imagenRuta) Then
                Dim img As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imagenRuta)
                img.ScaleToFit(50, 50)
                tablaImagen.AddCell(New PdfPCell(img) With {.HorizontalAlignment = Element.ALIGN_RIGHT, .VerticalAlignment = Element.ALIGN_MIDDLE, .Border = PdfPCell.NO_BORDER})
            End If

            tablaFactura.AddCell(New PdfPCell(tablaImagen) With {.Colspan = 5, .Border = PdfPCell.NO_BORDER})
            tablaFactura.AddCell(New PdfPCell(New Phrase("  ")) With {.Colspan = 5, .Border = PdfPCell.NO_BORDER})

            ' Información del cliente
            Dim tabladatos As New PdfPTable(2) With {.WidthPercentage = 100}
            Dim padding As Single = 5.0F
            tabladatos.AddCell(New PdfPCell(New Phrase($"{nombreCliente}")) With {.PaddingTop = padding, .PaddingBottom = padding, .Border = PdfPCell.NO_BORDER})
            tabladatos.AddCell(New PdfPCell(New Phrase($"{nombrePredio}")) With {.PaddingTop = padding, .PaddingBottom = padding, .Border = PdfPCell.NO_BORDER})
            tabladatos.AddCell(New PdfPCell(New Phrase($"{facturaID}")) With {.PaddingTop = padding, .PaddingBottom = padding, .Border = PdfPCell.NO_BORDER})
            tabladatos.AddCell(New PdfPCell(New Phrase($"{periodoCobro}")) With {.PaddingTop = padding, .PaddingBottom = padding, .Border = PdfPCell.NO_BORDER})
            tabladatos.AddCell(New PdfPCell(New Phrase($"{telefonoCliente}")) With {.PaddingTop = padding, .PaddingBottom = padding, .Border = PdfPCell.NO_BORDER})
            tabladatos.AddCell(New PdfPCell(New Phrase($"{fechaFin}")) With {.PaddingTop = padding, .PaddingBottom = padding, .Border = PdfPCell.NO_BORDER})
            tablaFactura.AddCell(New PdfPCell(tabladatos) With {.Colspan = 5, .Border = PdfPCell.NO_BORDER, .PaddingBottom = padding + 5.0F})

            ' Agregar DataGridView1 (detalles)
            Dim tablaDetalles1 As New PdfPTable(2) With {.WidthPercentage = 100}
            tablaDetalles1.AddCell(New PdfPCell(New Phrase("Descripción")) With {.BackgroundColor = BaseColor.LIGHT_GRAY})
            tablaDetalles1.AddCell(New PdfPCell(New Phrase("Valor tipo de uso")) With {.BackgroundColor = BaseColor.LIGHT_GRAY})
            For Each item As String In dgv1
                Dim datos As String() = item.Split(","c)
                tablaDetalles1.AddCell(datos(0).Trim())
                tablaDetalles1.AddCell(datos(1).Trim())
            Next
            tablaFactura.AddCell(New PdfPCell(tablaDetalles1) With {.Colspan = 5, .Border = PdfPCell.NO_BORDER})

            ' Agregar DataGridView2 (total)
            Dim tablaDetalles2 As New PdfPTable(2) With {.WidthPercentage = 100}
            tablaDetalles2.AddCell(New PdfPCell(New Phrase("Descripción")) With {.BackgroundColor = BaseColor.LIGHT_GRAY, .PaddingTop = padding, .PaddingBottom = padding})
            tablaDetalles2.AddCell(New PdfPCell(New Phrase("Total")) With {.BackgroundColor = BaseColor.LIGHT_GRAY, .PaddingTop = padding, .PaddingBottom = padding})
            For Each item As String In dgv2
                Dim datos As String() = item.Split(","c)
                tablaDetalles2.AddCell(New PdfPCell(New Phrase(datos(0).Trim())) With {.PaddingTop = padding, .PaddingBottom = padding})
                tablaDetalles2.AddCell(New PdfPCell(New Phrase(datos(1).Trim())) With {.PaddingTop = padding, .PaddingBottom = padding})
            Next
            tablaFactura.AddCell(New PdfPCell(tablaDetalles2) With {.Colspan = 5, .Border = PdfPCell.NO_BORDER})

            ' Observaciones
            tablaFactura.AddCell(New PdfPCell(New Phrase($"Observaciones: {observaciones}")) With {.Colspan = 5, .Border = PdfPCell.NO_BORDER})
            tablaFactura.AddCell(New PdfPCell(New Phrase("  ")) With {.Colspan = 5, .Border = PdfPCell.NO_BORDER})
            tablaFactura.AddCell(New PdfPCell(New Phrase("  ")) With {.Colspan = 5, .Border = PdfPCell.NO_BORDER})
        End Sub



    End Class

    ' Clase para manejar el encabezado y el pie de página
    Friend Class EncabezadoPieDePagina
        Inherits PdfPageEventHelper

        Public Overrides Sub OnEndPage(writer As PdfWriter, document As Document)
            Dim canvas As PdfContentByte = writer.DirectContent

            ' Ruta de la imagen
            Dim imagenRuta As String = Path.Combine(Environment.CurrentDirectory, "Imagenes", "Icono.png")
            Dim imagen As Image = Image.GetInstance(imagenRuta)

            ' Ajustar el tamaño de la imagen (puedes modificar estos valores)
            Dim anchoImagen As Single = 60.0F ' Ancho de la imagen
            Dim altoImagen As Single = 60.0F ' Alto de la imagen
            imagen.ScaleToFit(anchoImagen, altoImagen)

            ' Agregar imagen al encabezado
            imagen.SetAbsolutePosition(document.Right - document.RightMargin - -40.0F - anchoImagen, document.PageSize.Height - document.TopMargin - 40.0F)
            canvas.AddImage(imagen)

            ' Agregar texto al encabezado
            Dim encabezadoTexto As String = "Asocontador.INC Calle 3 #3-37 Pitalito Huila"
            Dim encabezado As New Phrase(encabezadoTexto, New Font(Font.FontFamily.HELVETICA, 12, Font.BOLD))
            Dim posicionTextoX As Single = document.LeftMargin
            Dim posicionTextoY As Single = document.PageSize.Height - document.TopMargin - altoImagen - -50.0F
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, encabezado, posicionTextoX, posicionTextoY, 0)

            ' Agregar el pie de página
            Dim piePagina As New Paragraph("'Un mundo con agua depende de todos. ¡Cuidémosla!'", New Font(Font.FontFamily.HELVETICA, 10, Font.ITALIC))
            piePagina.Alignment = Element.ALIGN_CENTER
            piePagina.SetLeading(0, 1.5F) ' Ajustar el espaciado entre líneas

            ' Calcular el centro de la página y colocar el texto en el margen inferior
            Dim centerX As Single = (document.PageSize.Width - document.LeftMargin - document.RightMargin) / 2 + document.LeftMargin
            Dim posY As Single = document.BottomMargin - 20

            ColumnText.ShowTextAligned(canvas, Element.ALIGN_CENTER, New Phrase(piePagina), centerX, posY, 0)

        End Sub
    End Class



End Namespace
