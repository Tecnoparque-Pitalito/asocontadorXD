Imports System.Data.SQLite
Imports System.IO

Public Class Backup

    Private sourceDatabasePath As String = Application.StartupPath & "\DataBAse_Asocontador.s3db"
    Private backupFolderPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Backup_Facturacion")
    Private backupFilePath As String = ""

    Private backupTimer As Timer

    Public Sub New()
        InitializeComponent()

        ' Configurar el temporizador para que se ejecute todos los días a la misma hora
        backupTimer = New Timer()
        backupTimer.Interval = GetMillisecondsUntilNextBackup()
        AddHandler backupTimer.Tick, AddressOf BackupTimer_Tick
        backupTimer.Start()
    End Sub

    Private Function GetMillisecondsUntilNextBackup() As Integer
        ' Calcular la cantidad de milisegundos hasta la próxima ejecución de la copia de seguridad
        Dim now As DateTime = DateTime.Now
        Dim desiredTime As DateTime = New DateTime(now.Year, now.Month, now.Day, 12, 0, 0) ' Hora deseada (12:00 PM)

        If now > desiredTime Then
            desiredTime = desiredTime.AddDays(1) ' Si ya pasó la hora deseada hoy, programarla para mañana
        End If

        Dim millisecondsUntilNextBackup As Integer = CInt((desiredTime - now).TotalMilliseconds)
        Return millisecondsUntilNextBackup
    End Function

    Private Sub BackupTimer_Tick(sender As Object, e As EventArgs)
        ' Ejecutar la copia de seguridad cuando se dispare el temporizador
        CreateBackup()
    End Sub

    Private Sub CreateBackup()
        Try
            ' Generar un nombre de archivo de respaldo con la fecha y hora actual
            Dim backupFileName As String = $"Backup_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.db"
            Dim backupFilePath As String = Path.Combine(backupFolderPath, backupFileName)

            ' Verificar si el directorio de respaldo existe; si no, crearlo
            If Not Directory.Exists(backupFolderPath) Then
                Directory.CreateDirectory(backupFolderPath)
            End If

            ' Copiar el archivo de la base de datos al directorio de respaldo
            File.Copy(sourceDatabasePath, backupFilePath, True)

            MessageBox.Show("Copia de seguridad creada con éxito en: " & backupFilePath)
        Catch ex As Exception
            MessageBox.Show("Error al crear la copia de seguridad: " & ex.Message)
        End Try

    End Sub

    Private Sub Backup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RedondearEsquinasFormulario(Me, 20)
    End Sub

    Private Sub BackupButton_Click(sender As Object, e As EventArgs) Handles BackupButton.Click
        ' Esta es la función que se ejecuta al hacer clic en el botón de respaldo.

        CreateBackup()
    End Sub

    Private Sub RestoreButton_Click(sender As Object, e As EventArgs) Handles RestoreButton.Click
        Try
            ' Abrir un OpenFileDialog para que el usuario seleccione el archivo de respaldo
            Dim openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "Archivos de Base de Datos SQLite (*.db)|*.db"
            openFileDialog.InitialDirectory = backupFolderPath

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                ' Obtener la ruta del archivo de respaldo seleccionado
                Dim backupFilePath As String = openFileDialog.FileName

                ' Restaurar la base de datos desde el archivo de respaldo seleccionado
                Dim databasePath As String = Path.Combine(Application.StartupPath, "DataBAse_Asocontador.s3db")
                File.Copy(backupFilePath, databasePath, True)

                MessageBox.Show("Base de datos restaurada con éxito desde: " & backupFilePath)
            End If
        Catch ex As Exception
            MessageBox.Show("Error al restaurar la base de datos: " & ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CerrarFormulario(Me)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Minimizar el formulario actual
        Me.WindowState = FormWindowState.Minimized
    End Sub
End Class
