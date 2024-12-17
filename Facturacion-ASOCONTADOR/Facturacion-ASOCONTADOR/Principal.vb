Imports System.Drawing.Printing


Public Class Principal

    Dim facturasPagos As New Clases.FacturasPagosController

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LbFechaHora.Text = Now


    End Sub

    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim ctl As Control
        Dim ctlMDI As MdiClient
        For Each ctl In Me.Controls
            Try
                ctlMDI = CType(ctl, MdiClient)
                ctlMDI.BackColor = Me.BackColor
            Catch exc As InvalidCastException
            End Try
        Next
        Login.MdiParent = Me
        Login.Show()
    End Sub

    Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
        Me.Close()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Minimizar el formulario actual
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CerrarFormulario(Me)

    End Sub



    Private Sub CrearUsuarioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CrearUsuarioToolStripMenuItem.Click
        Usuarios.MdiParent = Me
        Usuarios.Show()
    End Sub

    Private Sub CopiasDeSeguridadYRespaldoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopiasDeSeguridadYRespaldoToolStripMenuItem.Click
        Backup.MdiParent = Me
        Backup.Show()
    End Sub

    Private Sub GestiónDePrediosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GestiónDePrediosToolStripMenuItem.Click
        Predios.MdiParent = Me
        Predios.Show()
    End Sub

    Private Sub GestionDePeriodosYCostosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GestionDePeriodosYCostosToolStripMenuItem.Click
        Periodos.MdiParent = Me
        Periodos.Show()
    End Sub

    Private Sub GenerarFacturaciónToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GenerarFacturaciónToolStripMenuItem.Click
        'Imprimir.MdiParent = Me
        'Imprimir.Show()
        Pagos.MdiParent = Me
        Pagos.Show()
    End Sub

    Private Sub UsuariosDelSistemaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UsuariosDelSistemaToolStripMenuItem.Click
        UsuarioSis.MdiParent = Me
        UsuarioSis.Show()
    End Sub

    Private Sub PuntosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PuntosToolStripMenuItem.Click

    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click

    End Sub

    Private Sub IrAReportesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IrAReportesToolStripMenuItem.Click
        Reportes.MdiParent = Me
        Reportes.Show()
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click

    End Sub

    Private Sub GenerarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GenerarToolStripMenuItem.Click
        Facturas.MdiParent = Me
        Facturas.Show()

        facturasPagos.ValidarFechaPago()
    End Sub

    Private Sub AsignarCostosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsignarCostosToolStripMenuItem.Click
        AsignarCostoAsociado.MdiParent = Me
        AsignarCostoAsociado.Show()
    End Sub

    'Private Sub AdministradorToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    UsuarioSis.MdiParent = Me
    '    UsuarioSis.Show()
    'End Sub

    'Private Sub EstandarToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    Usuarios.MdiParent = Me
    '    Usuarios.Show()
    'End Sub
End Class
