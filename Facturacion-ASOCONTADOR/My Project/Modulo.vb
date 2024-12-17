Imports System.Runtime.InteropServices

Module Modulo
    ' Función para cerrar un formulario con mensaje de confirmación
    Public Function CerrarFormulario(ByVal formulario As Form) As Boolean
        ' Mostrar mensaje de confirmación
        Dim respuesta As DialogResult = MessageBox.Show($"¿Estás seguro de que deseas cerrar el formulario '{formulario.Name}'?", "Confirmar Cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        ' Verificar la respuesta del usuario
        If respuesta = DialogResult.Yes Then
            ' Cerrar el formulario si el usuario elige "Sí"
            formulario.Close()
            Return True ' Se cerró el formulario
        Else
            Return False ' No se cerró el formulario
        End If
    End Function
    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")>
    Private Function CreateRoundRectRgn(ByVal left As Integer, ByVal top As Integer, ByVal right As Integer, ByVal bottom As Integer, ByVal width As Integer, ByVal height As Integer) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Function SetWindowRgn(ByVal hWnd As IntPtr, ByVal hRgn As IntPtr, ByVal bRedraw As Boolean) As Integer
    End Function


    Public Sub RedondearEsquinasFormulario(ByVal form As Form, ByVal cornerRadius As Integer)
        Dim regionHandle As IntPtr = CreateRoundRectRgn(0, 0, form.Width, form.Height, cornerRadius, cornerRadius)
        SetWindowRgn(form.Handle, regionHandle, True)
    End Sub
End Module
