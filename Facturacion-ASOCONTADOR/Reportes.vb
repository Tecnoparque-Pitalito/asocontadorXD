Imports System.Globalization

Public Class Reportes
    Dim PDFReportes As New Clases.PDFController
    Dim Reportes As New Clases.ReportesController
    Private Sub Reportes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RedondearEsquinasFormulario(Me, 20)
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy"
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy/MMM"
        ComboBox1.SelectedIndex = 0
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CerrarFormulario(Me)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub TabPage3_Click(sender As Object, e As EventArgs) Handles TabPage3.Click
        Dim anio As Integer = DateTime.Now.Year
        ' Reportes.ReportesPeriodo(ComboBox2, DataGridView2)
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy/MMM"
        Dim mes As Integer = DateTimePicker1.Value.Month
        Dim anyo As Integer = DateTimePicker1.Value.Year
        Reportes.RealizarReportesMes(DataGridView1, mes, anyo)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim mes As Integer = DateTimePicker1.Value.Month
        Dim anyo As Integer = DateTimePicker1.Value.Year
        Dim Filter As Integer = ComboBox1.SelectedIndex
        If ComboBox1.SelectedIndex = 0 Then
            Reportes.RealizarReportesMes(DataGridView1, mes, anyo)
        ElseIf ComboBox1.SelectedIndex = 1 Then
            Reportes.ReporteMesMorosos(DataGridView1, mes, anyo, Filter)
        ElseIf ComboBox1.SelectedIndex = 2 Then
            Reportes.ReporteMesMorosos(DataGridView1, mes, anyo, Filter)
        ElseIf ComboBox1.SelectedIndex = 3 Then
            Reportes.ReporteMesMorosos(DataGridView1, mes, anyo, Filter)
        End If
    End Sub

    Private hasPainted As Boolean = False ' Variable para controlar si ya se ha pintado

    Private Sub TabPage3_Paint(sender As Object, e As PaintEventArgs) Handles TabPage3.Paint
        If Not hasPainted Then
            ComboBox2.SelectedIndex = 0
            hasPainted = True ' Cambia a True para evitar que se muestre de nuevo
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim filtroEstado As Integer = ComboBox2.SelectedIndex + 1
        Reportes.ReportesPeriodo(filtroEstado, DataGridView2)
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Dim anyo As Integer = DateTimePicker1.Value.Year
        Dim mes As Integer = DateTimePicker1.Value.Month
        PDFReportes.reportesInternos(DataGridView1, ComboBox1, anyo, mes)
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        PDFReportes.reportesInternosPeriodo(DataGridView2, ComboBox2)
    End Sub
End Class