Imports Org.BouncyCastle.Math.EC
Imports System.Drawing.Drawing2D

Public Class Facturas
    Dim FacturasGeneratorController As New Clases.FacturasGeneratorController
    Dim limpiar As New Clases.LimpiarCampos
    Dim DescargarPDFactura As New Clases.PDFController
    Dim facturasMasivas As New Clases.FacturasMasivasGeneratorController

    Dim usuarioControler As New Clases.UsuariosController

    Dim TelFontanero As Long = 0

    Dim Anyo As Integer
    Dim Mes As Integer
    Dim VistaPrevia As Boolean = False


    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        ' Verificar si el carácter presionado no es un número ni la tecla de retroceso
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True ' Cancela el evento KeyPress si no es un número
        End If
    End Sub


    Private Async Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            Button7.Enabled = False
            Button2.Visible = False
            Button4.Visible = False
            Button6.Visible = False

            Dim listaCedulas As List(Of Cedulas) = usuarioControler.ListarCedulasUsuarios()

            If listaCedulas IsNot Nothing AndAlso listaCedulas.Count > 0 Then
                For Each cedula As Cedulas In listaCedulas
                    TextBox2.Text = $"{cedula.cedula}"
                    PictureBox2_Click(Nothing, EventArgs.Empty)
                    Dim listaPredios As Integer = ComboBox2.Items.Count
                    If listaPredios > 0 Then
                        For i As Integer = 0 To listaPredios - 1
                            ComboBox2.SelectedIndex = i
                            Button2_Click(Nothing, EventArgs.Empty)
                        Next
                    End If
                Next
                MsgBox("Las Facturas estan lsitas para descargarse")
            Else
                MsgBox("No se encontraron cédulas.")
            End If

            ' Rehabilitar los botones y volver a habilitar Button7 después de 10 segundos

            Button7.Enabled = False
            Button2.Visible = True
            Button4.Visible = True
            Button6.Visible = True

            Await Task.Delay(10000) ' Espera 10 segundos
            Button7.Enabled = True
        Catch ex As Exception
            MsgBox("ocurrio un errror")
        End Try
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Dim estado As Boolean = FacturasGeneratorController.ListarPredios(TextBox2, ComboBox2, TextBox1)

        If estado Then
            ComboBox2.SelectedIndex = 0
            If estado AndAlso ComboBox2.SelectedItem IsNot Nothing Then
                ' Obtiene el texto seleccionado y lo separa en ID y Nombre
                Dim selectedText As String = ComboBox2.SelectedItem.ToString()
                Dim parts As String() = selectedText.Split("-"c)

                ' Asegura que el ID se convierta correctamente
                Dim idPredio As Integer
                If Integer.TryParse(parts(0).Trim(), idPredio) Then
                    Dim anyo As Integer = DateTime.Now.Year
                    FacturasGeneratorController.prepareDate(TextBox2, idPredio)
                Else
                    MsgBox("El ID del predio no es válido.")
                End If
            Else
                MsgBox("No se ha podido obtener el estado o no hay un elemento seleccionado en ComboBox2.")
            End If
        End If
    End Sub

    ' Evento al cambiar el ítem seleccionado en ComboBox2 (vacío, puedes agregar lógica si se necesita)
    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim selectedText As String = ComboBox2.SelectedItem.ToString()
        ' Separar el ID del Nombre (suponiendo que están separados por " - ")
        Dim parts As String() = selectedText.Split("-"c)
        ' Convertir el primer elemento (el ID) a Integer
        Dim idPredio As Integer = Convert.ToInt32(parts(0))
        Dim NOmPredio As String = Convert.ToString(parts(1))
        NombrePredio.Text = $"Predio:{NOmPredio}"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ComboBox2.SelectedIndex <> -1 Then

            Dim selectedText As String = ComboBox2.SelectedItem.ToString()

            ' Separar el ID del Nombre (suponiendo que están separados por " - ")
            Dim parts As String() = selectedText.Split("-"c)

            ' Convertir el primer elemento (el ID) a Integer
            Dim idPredio As Integer = Convert.ToInt32(parts(0))
            Dim Fecha As DateTime = DateTimePicker1.Value

            VistaPrevia = True
            Dim vistaFactura As Boolean = FacturasGeneratorController.VistaDeFactura(IdFactura1, IdFactura2, NombreCliente, NombrePredio, TelefonoCliente, PeriodoCobro, TotalPagar, FechaFin, CostoPeriodo, reconexion, multas, atrasos, recargoMora, otros, ajustePeso, valor, ClienteColilla, PredioColilla, totalColilla, TextBox2, idPredio, Fecha, PeriodoColilla)
            'FacturasGeneratorController.VistaDeFactura(NombreCliente, CedulaCliente, TelefonoCliente, NombrePredio, VeredaPredio, NumeroMatricula, FacturaID, PeriodoCobro, FechaFin, DataGridView1, DataGridView2, DataGridView3, observaciones, TextBox2, NomPredio, TextBox3, anyo, mes, TextBox1)
            'FacturasGeneratorController.GenerarVistaFactura(TextBox3, TextBox1, DataGridView1, DataGridView2, observaciones)
        Else
            MsgBox("Seleccione un usuario")
        End If

    End Sub

    ' Lista para acumular las facturas a imprimir
    Private facturasPendientes As New List(Of Dictionary(Of String, Object))

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Comprobamos si TelFontanero no tiene un valor definido
        If TelFontanero = 0 Then
            Dim input As String = InputBox("Por favor, ingrese el número del fontanero:", "Solicitud de Número")
            Dim numero As Long

            If Long.TryParse(input, numero) Then
                TelFontanero = numero
            Else
                MessageBox.Show("El valor ingresado no es un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
        End If

        If VistaPrevia Then

            Dim RecorrerListaDePersonas As Boolean = FacturasGeneratorController.recorerPersonas()

        Else
            MsgBox("Primero, genere una vista previa.")
        End If
    End Sub




    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CerrarFormulario(Me)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Facturas_Load(sender As Object, e As EventArgs)

    End Sub

    Private Sub Facturas_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        RedondearPanel(Panel5, 30)
        RedondearPanel(Panel6, 30)
        RedondearPanel(Panel7, 30)
        RedondearPanel(Panel8, 30)
        RedondearLabel(valor, 2)
        RedondearEsquinasFormulario(Me, 20)
        Me.AcceptButton = Nothing
        Dim fecha As DateTime = DateTime.Now
        DateTimePicker1.Value = fecha
        Anyo = fecha.Year
        Mes = fecha.Month
        Dim estadoDelPeriodo As Boolean = FacturasGeneratorController.VerificarEstado(Anyo, Mes)
        If estadoDelPeriodo Then
            MsgBox("Primero es necesario generar un periodo Nuevo")
            ' En lugar de cerrar aquí, esperamos al evento Shown
            AddHandler Me.Shown, AddressOf CerrarFormularioForce
            Return
        End If
        'Dim Usuario As Boolean = usuarioControler.ListarPrimerUsuario(TextBox2)
        'If Usuario Then
        '    PictureBox2_Click(Nothing, EventArgs.Empty)
        'End If
    End Sub

    'Método para redondear el Label
    Private Sub RedondearLabel(label As Label, radio As Integer)
        Dim path As New GraphicsPath()
        path.StartFigure()
        path.AddArc(New Rectangle(0, 0, radio, radio), 180, 90)
        path.AddArc(New Rectangle(label.Width - radio, 0, radio, radio), 270, 90)
        path.AddArc(New Rectangle(label.Width - radio, label.Height - radio, radio, radio), 0, 90)
        path.AddArc(New Rectangle(0, label.Height - radio, radio, radio), 90, 90)
        path.CloseFigure()
        label.Region = New Region(path)
    End Sub

    Private Sub RedondearPanel(panel As Panel, radio As Integer)
        Dim path As New GraphicsPath()
        path.StartFigure()
        path.AddArc(New Rectangle(0, 0, radio, radio), 180, 90)
        path.AddArc(New Rectangle(panel.Width - radio, 0, radio, radio), 270, 90)
        path.AddArc(New Rectangle(panel.Width - radio, panel.Height - radio, radio, radio), 0, 90)
        path.AddArc(New Rectangle(0, panel.Height - radio, radio, radio), 90, 90)
        path.CloseFigure()
        panel.Region = New Region(path)
    End Sub

    Private Sub CerrarFormularioForce(sender As Object, e As EventArgs)
        ' Cierra el formulario aquí después de que se haya mostrado
        Me.Close()
    End Sub



    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        limpiar.LimpiarTextBoxes(TextBox2)
        limpiar.LimpiarComboBoxes(ComboBox2)
        FacturasGeneratorController.limpiarListas()
        VistaPrevia = False
    End Sub

    Private Sub TableLayoutPanel2_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel2.Paint

    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label37_Click(sender As Object, e As EventArgs) Handles Label37.Click

    End Sub

    Private Sub TableLayoutPanel15_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel15.Paint
        Dim radio As Integer = 20 ' Ajusta el radio según necesites
        Dim path As New GraphicsPath()
        path.AddArc(New Rectangle(0, 0, radio, radio), 180, 90)
        path.AddArc(New Rectangle(TableLayoutPanel15.Width - radio, 0, radio, radio), 270, 90)
        path.AddArc(New Rectangle(TableLayoutPanel15.Width - radio, TableLayoutPanel15.Height - radio, radio, radio), 0, 90)
        path.AddArc(New Rectangle(0, TableLayoutPanel15.Height - radio, radio, radio), 90, 90)
        path.CloseFigure()

        ' Dibuja el borde redondeado
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        e.Graphics.DrawPath(New Pen(Color.Black, 2), path) ' Cambia el color y grosor del borde según prefieras

    End Sub

    Private Sub TableLayoutPanel6_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel6.Paint
        Dim radio As Integer = 20 ' Ajusta el radio según necesites
        Dim path As New GraphicsPath()
        path.AddArc(New Rectangle(0, 0, radio, radio), 180, 90)
        path.AddArc(New Rectangle(TableLayoutPanel15.Width - radio, 0, radio, radio), 270, 90)
        path.AddArc(New Rectangle(TableLayoutPanel15.Width - radio, TableLayoutPanel15.Height - radio, radio, radio), 0, 90)
        path.AddArc(New Rectangle(0, TableLayoutPanel15.Height - radio, radio, radio), 90, 90)
        path.CloseFigure()

        ' Dibuja el borde redondeado
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        e.Graphics.DrawPath(New Pen(Color.Black, 2), path) ' Cambia el color y grosor del borde según prefieras

    End Sub

    Private Sub TableLayoutPanel11_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel11.Paint
        Dim radio As Integer = 20 ' Ajusta el radio según necesites
        Dim path As New GraphicsPath()
        path.AddArc(New Rectangle(0, 0, radio, radio), 180, 90)
        path.AddArc(New Rectangle(TableLayoutPanel15.Width - radio, 0, radio, radio), 270, 90)
        path.AddArc(New Rectangle(TableLayoutPanel15.Width - radio, TableLayoutPanel15.Height - radio, radio, radio), 0, 90)
        path.AddArc(New Rectangle(0, TableLayoutPanel15.Height - radio, radio, radio), 90, 90)
        path.CloseFigure()

        ' Dibuja el borde redondeado
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        e.Graphics.DrawPath(New Pen(Color.Black, 2), path) ' Cambia el color y grosor del borde según prefieras

    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles reconexion.Click

    End Sub

    Private Sub ajustePeso_Click(sender As Object, e As EventArgs) Handles ajustePeso.Click

    End Sub
End Class

Friend Class Cedulas
    Public Property cedula As String

    Public Sub New(cedula As String)
        Me.cedula = cedula
    End Sub
    Public Overrides Function ToString() As String
        Return $"Cedula: {cedula}"
    End Function
End Class
