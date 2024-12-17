Imports System.Drawing.Printing

Public Class Imprimir

    Private pd As PrintDocument

    Private Sub Imprimir_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        RedondearEsquinasFormulario(Me, 20)
        ' Inicializar el objeto PrintDocument
        pd = New PrintDocument()

        ' Asociar el evento PrintPage con el controlador de eventos
        AddHandler pd.PrintPage, AddressOf ImprimirTicket

        ' Configurar el PrintPreviewControl para mostrar la vista previa
        PrintPreviewControl1.Document = pd

    End Sub



    Private Sub ImprimirTicket(sender As Object, e As PrintPageEventArgs)
        ' Definir el contenido del ticket a imprimir
        Dim contenido As String = "Tienda de Ejemplo" & vbCrLf &
                                   "------------------" & vbCrLf &
                                   "Producto A: $10.00" & vbCrLf &
                                   "Producto B: $20.00" & vbCrLf &
                                   "------------------" & vbCrLf &
                                   "Total: $30.00" & vbCrLf &
                                   "Gracias por su compra"

        ' Definir la fuente y el pincel para la impresión
        Using fuente As New Font("Arial", 10)
            Using pincel As New SolidBrush(Color.Black)
                ' Especificar la posición y el tamaño del área de impresión
                Dim areaImpresion As New RectangleF(100, 100, 300, 300)

                ' Dibujar el contenido en el área de impresión
                e.Graphics.DrawString(contenido, fuente, pincel, areaImpresion)
            End Using
        End Using
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Mostrar la vista previa del documento
        Dim previewDialog As New PrintPreviewDialog()
        previewDialog.Document = pd
        previewDialog.ShowDialog()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' Mostrar el cuadro de diálogo de impresión
        Dim dialogoImpresion As New PrintDialog()
        dialogoImpresion.Document = pd

        If dialogoImpresion.ShowDialog() = DialogResult.OK Then
            ' Iniciar la impresión
            pd.Print()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CerrarFormulario(Me)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Minimizar el formulario actual
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class