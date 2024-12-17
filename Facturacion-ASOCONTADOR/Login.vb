Imports System.Data.SQLite
Imports System.Speech.Synthesis
Imports Facturacion_ASOCONTADOR.Clases

Public Class Login

    Dim periodo As New Clases.PeriodoCostosController


    Private rolUsuario As String = ""
    Dim synth As New SpeechSynthesizer()
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RedondearEsquinasFormulario(Me, 20)
        Principal.MenuStrip1.Enabled = False
        periodo.verificarPeriodo(DateTime.Now)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CerrarFormulario(Me)
        Principal.Close()
    End Sub

    Private Function ValidarUsuario(usuario As String, contrasena As String) As Boolean
        Try
            Dim query As String = "SELECT COUNT(*) FROM administrador WHERE usuario=@usuario AND contrasena=@contrasena"

            Using connection As New SQLiteConnection(connectionString)
                Using command As New SQLiteCommand(query, connection)
                    command.Parameters.AddWithValue("@usuario", usuario)
                    command.Parameters.AddWithValue("@contrasena", contrasena)
                    connection.Open()
                    Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
                    If count > 0 Then
                        ' Si la validación es exitosa, obtener el rol del usuario
                        Dim rolQuery As String = "SELECT rol FROM administrador WHERE Usuario=@usuario AND Contrasena=@contrasena"
                        command.CommandText = rolQuery
                        Dim rol As String = Convert.ToString(command.ExecuteScalar())
                        rolUsuario = rol ' Guardar el rol en la variable de clase
                        UsuarioLogin = usuario
                    End If
                    Return count > 0
                End Using
            End Using
        Catch ex As Exception
            synth.Speak("Ocurrió un error en la base de datos")
        End Try
    End Function

    Private Sub ButsaveUsuario_Click(sender As Object, e As EventArgs) Handles ButsaveUsuario.Click
        Dim usuario As String = txtUsuario.Text
        Dim contrasena As String = txtContrasena.Text


        If ValidarUsuario(usuario, contrasena) Then
            'MessageBox.Show("Inicio de sesión exitoso")
            Principal.MenuStrip1.Enabled = True
            Me.Close()
            Principal.LB_User.Text = UsuarioLogin
            Principal.LB_Rol.Text = rolUsuario

            ' Síntesis de voz del mensaje de error.

            synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Teen)
            synth.Volume = 100
            synth.Rate = 0
            synth.Speak("Inicio de sesión exitoso")
            'synth.Speak("Azange le gusta el gey tore")

            ' Libera los recursos de SpeechSynthesizer.
            synth.Dispose()

        Else
            synth.Speak("Usuario o contraseña incorrectos")
            Principal.MenuStrip1.Enabled = False
        End If
    End Sub

    Private Sub ButmodUsuario_Click(sender As Object, e As EventArgs) Handles ButmodUsuario.Click
        txtUsuario.Text = ""
        txtContrasena.Text = ""
        Principal.MenuStrip1.Enabled = False
    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub
End Class
