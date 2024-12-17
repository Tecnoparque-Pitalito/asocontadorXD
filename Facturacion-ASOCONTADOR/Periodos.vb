Imports System.Data.SQLite
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Periodos

    Dim PeriodosCostos As New Clases.PeriodoCostosController
    Private Sub Periodos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            RedondearEsquinasFormulario(Me, 20)
            DateTimePicker1.Value = New DateTime(DateTime.Now.Year, 1, 1)
            DateTimePicker2.Value = New DateTime(DateTimePicker1.Value.Year, 12, 31)
            PeriodosCostos.FormatoDateTimePicker(DateTimePicker1)
            PeriodosCostos.FormatoDateTimePicker(DateTimePicker2)
            Dim calcularDiferencias As Integer = PeriodosCostos.CalcularDiferencias(DateTimePicker1, DateTimePicker2, lbdiferencia, GroupBox2, GroupBox4)
            PeriodosCostos.ListarClasificacionUso(ComboBox1)
            PeriodosCostos.ListarOtrosCostos(ComboBox2)
            PeriodosCostos.PeriodosRegistrados(DataGridView1)

        Catch ex As Exception
            MsgBox("Error al cargar esta ventana")
        End Try
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        PeriodosCostos.CalcularDiferencias(DateTimePicker1, DateTimePicker2, lbdiferencia, GroupBox2, GroupBox4)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Solicitar al usuario que ingrese el tipo de uso
        Dim TipoDeUso As String = InputBox("Ingrese el tipo de Clasificación de Uso:", "Nueva Clasificación de Uso")
        PeriodosCostos.AddTipoUso(TipoDeUso)
        PeriodosCostos.ListarClasificacionUso(ComboBox1)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Verificar si se ha seleccionado un elemento en el ComboBox
        If ComboBox1.SelectedIndex <> -1 Then
            ' Obtener el valor seleccionado del ComboBox
            Dim selectedItem As String = ComboBox1.SelectedItem.ToString()

            ' Separar el valor del id y el TipoDeUso
            Dim parts() As String = selectedItem.Split({" - "}, StringSplitOptions.None)
            Dim id As Integer = Convert.ToInt32(parts(0))
            Dim tipoUso As String = parts(1)

            ' Obtener el valor del TextBox
            Dim valor As String = TextBox1.Text

            ' Verificar si el TextBox está vacío
            If Not String.IsNullOrWhiteSpace(valor) Then
                ' Verificar si el id del tipo de uso ya está presente en algún elemento del ListBox
                Dim idDuplicado As Boolean = False
                For Each item As String In ListBox1.Items
                    If item.Contains("ID: " & id) Then
                        idDuplicado = True
                        Exit For
                    End If
                Next

                ' Si el id del tipo de uso no está duplicado, agregar el elemento al ListBox
                If Not idDuplicado Then
                    ListBox1.Items.Add("ID: " & id & "|  Clasificación de Uso: " & tipoUso & "|  Valor: " & valor)
                Else
                    MessageBox.Show("Esta Clasificación de Uso ya está en la lista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("Por favor, ingrese una nueva clasificación de uso", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Por favor, seleccione una nueva clasificación de uso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        ' Verificar si la tecla presionada es un número o una tecla de control (como retroceso)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            ' Si la tecla presionada no es un número ni una tecla de control, cancelar la entrada
            e.Handled = True
        End If
    End Sub
    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        ' Verificar si la tecla presionada es un número o una tecla de control (como retroceso)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            ' Si la tecla presionada no es un número ni una tecla de control, cancelar la entrada
            e.Handled = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CerrarFormulario(Me)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Minimizar el formulario actual
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub AgregarDatosDesdeListBox2()

        For Each item As String In ListBox2.Items
            ' Separar el item en sus componentes
            Dim parts() As String = item.Split({"|"}, StringSplitOptions.None)
            Dim Otrocosto As String = parts(1).Split(":")(1).Trim()
            Dim valor2 As String = parts(2).Split(":")(1).Trim()
            Dim periodoID As String = Label7.ToString()

            ' Llamar a la función para agregar el costo a la base de datos
            AgregarCosto2(Otrocosto, valor2, periodoID) ' Ajusta PeriodoID si es necesario
        Next
    End Sub

    Public Sub AgregarCosto2(Otrocosto As String, valor2 As String, periodoID As String)

        Using connection As New SQLiteConnection(connectionString)
            connection.Open()

            ' Comando SQL para insertar un nuevo registro en la tabla Costos
            Dim query As String = "INSERT INTO ValOtrosCostos (CostoAsociado, Valor,PeriodoID) VALUES (@CostoAsociado, @Valor,@PeriodoID)"

            Using command As New SQLiteCommand(query, connection)
                ' Añadir parámetros a la consulta SQL
                command.Parameters.AddWithValue("@CostoAsociado", If(String.IsNullOrEmpty(Otrocosto), DBNull.Value, Otrocosto))
                command.Parameters.AddWithValue("@Valor", If(String.IsNullOrEmpty(valor2), DBNull.Value, valor2))
                command.Parameters.AddWithValue("@PeriodoID", periodoID)

                Try
                    ' Ejecutar la consulta SQL
                    command.ExecuteNonQuery()
                    '  MessageBox.Show("Costos agregados correctamente.")
                Catch ex As Exception
                    ' Manejar errores, por ejemplo, si el valor de ClasifiCosto no es único
                    MessageBox.Show("Error al agregar el costo: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Public Sub AgregarCosto(valor As String, tipoDeUso As String, periodoID As String)

        Using connection As New SQLiteConnection(connectionString)
            connection.Open()

            ' Comando SQL para insertar un nuevo registro en la tabla Costos
            Dim query As String = "INSERT INTO costosfijos (valor, ClasificacionUso_idClasificacionUso, Periodos_idPeriodos) VALUES (@TipoDeUso, @Valor,@PeriodoID)"

            Using command As New SQLiteCommand(query, connection)
                ' Añadir parámetros a la consulta SQL
                command.Parameters.AddWithValue("@TipoDeUso", If(String.IsNullOrEmpty(tipoDeUso), DBNull.Value, tipoDeUso))
                command.Parameters.AddWithValue("@Valor", If(String.IsNullOrEmpty(valor), DBNull.Value, valor))
                command.Parameters.AddWithValue("@PeriodoID", periodoID)

                Try
                    ' Ejecutar la consulta SQL
                    command.ExecuteNonQuery()
                    '  MessageBox.Show("Costos agregados correctamente.")
                Catch ex As Exception
                    ' Manejar errores, por ejemplo, si el valor de ClasifiCosto no es único
                    MessageBox.Show("Error al agregar el costo: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ' Solicitar al usuario que ingrese el tipo de uso
        Dim CostoAsociado As String = InputBox("Ingrese el Nuevos costos asociados:", "Nuevo costo asociado")
        PeriodosCostos.AddCostoAsociado(CostoAsociado)
        PeriodosCostos.ListarOtrosCostos(ComboBox2)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' Verificar si se ha seleccionado un elemento en el ComboBox
        If ComboBox2.SelectedIndex <> -1 Then
            ' Obtener el valor seleccionado del ComboBox
            Dim selectedItem As String = ComboBox2.SelectedItem.ToString()

            ' Separar el valor del id y el TipoDeUso
            Dim parts() As String = selectedItem.Split({" - "}, StringSplitOptions.None)
            Dim id As Integer = Convert.ToInt32(parts(0))
            Dim tipoUso As String = parts(1)

            ' Obtener el valor del TextBox
            Dim valor As String = TextBox2.Text

            ' Verificar si el TextBox está vacío
            If Not String.IsNullOrWhiteSpace(valor) Then
                ' Verificar si el id del tipo de uso ya está presente en algún elemento del ListBox
                Dim idDuplicado As Boolean = False
                For Each item As String In ListBox2.Items
                    If item.Contains("ID: " & id) Then
                        idDuplicado = True
                        Exit For
                    End If
                Next

                ' Si el id del tipo de uso no está duplicado, agregar el elemento al ListBox
                If Not idDuplicado Then
                    ListBox2.Items.Add("ID: " & id & "|  Costo Asociado: " & tipoUso & "|  Valor: " & valor)
                Else
                    MessageBox.Show("Este costo asociado ya está en la lista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("Por favor, ingrese un nuevo costo asociado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Por favor, seleccione un nuevo costo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub btnSaveTransacsion_Click(sender As Object, e As EventArgs) Handles btnSaveTransacsion.Click

        Dim value As Double

        If Not Double.TryParse(MaskedTextBox1.Text, value) Then
            MsgBox("Ingrese un valor válido por mora.")
            Return
        End If


        ' Verificar que todos los campos y listas requeridos tengan datos
        If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse
       String.IsNullOrWhiteSpace(TextBox2.Text) OrElse
       ListBox1.Items.Count = 0 OrElse
       ListBox2.Items.Count = 0 Then

            MessageBox.Show("Por favor, complete todos los campos y agregue elementos a las listas antes de continuar.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If


        ' Llamadas a métodos de PeriodosCostos para agregar y listar transacciones
        PeriodosCostos.AgregarPeriodoTransacciones(DateTimePicker1, DateTimePicker2, MaskedTextBox1, IdPeriodo, ListBox1, ListBox2)
        PeriodosCostos.PeriodosRegistrados(DataGridView1)
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        PeriodosCostos.CalcularDiferencias(DateTimePicker1, DateTimePicker2, lbdiferencia, GroupBox2, GroupBox4)
        Dim anyo As Integer = DateTimePicker1.Value.Year
        IdPeriodo.Text = anyo
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ' Mostrar mensaje de confirmación
        Dim result As DialogResult = MessageBox.Show("¿Estás seguro de que deseas eliminar un periodo? Es posible que se pierdan datos.",
                                                 "Confirmación de eliminación",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning)

        ' Verificar la respuesta del usuario
        If result = DialogResult.Yes Then
            ' Solicitar el periodo que desea eliminar
            Dim periodo As String = InputBox("Por favor, introduce el periodo que desea eliminar:",
                                         "Seleccionar Periodo")


            If Not String.IsNullOrEmpty(periodo) Then

                Dim EliminarPeriodos As Boolean = PeriodosCostos.EliminarPeriodo(periodo)
                If EliminarPeriodos Then
                    MessageBox.Show("El periodo '" & periodo & "' ha sido eliminado correctamente.",
              "Información",
              MessageBoxButtons.OK,
              MessageBoxIcon.Information)
                    PeriodosCostos.PeriodosRegistrados(DataGridView1)
                Else
                    MsgBox("Error, periodo no eliminado")
                End If
            Else
                ' Si no se ingresa un periodo
                MessageBox.Show("No se ingresó ningún periodo. Operación cancelada.",
                            "Información",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            End If
        Else
            ' Cancelar la operación
            MessageBox.Show("Eliminación cancelada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim periodo As String = InputBox("Por favor, introduce el periodo que desea modificar:",
                                         "Seleccionar Periodo")


        If Not String.IsNullOrEmpty(periodo) Then
            Dim actualizar As New ActualizarPeriodo()
            actualizar.ComboBox1.Items.Add("Costos fijos")
            actualizar.ComboBox1.Items.Add("Costos asociados")
            actualizar.ComboBox1.SelectedIndex = 0
            actualizar.anyo = periodo
            'Dim BuscarFijos As Boolean = PeriodosCostos.listarijos(actualizar.ComboBox4)
            actualizar.ShowDialog()
        End If


    End Sub
End Class


