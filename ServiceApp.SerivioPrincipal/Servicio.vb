
Public Class Servicio
    Private _ServiceApp As ServiceApp.Service.ServiceApp

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Agregue el código aquí para iniciar el servicio. Este método debería poner
        ' en movimiento los elementos para que el servicio pueda funcionar.

        _ServiceApp = New ServiceApp.Service.ServiceApp
    End Sub

    Protected Overrides Sub OnStop()
        ' Agregue el código aquí para realizar cualquier anulación necesaria para detener el servicio.
        If _ServiceApp IsNot Nothing Then
            _ServiceApp.Dispose()
            _ServiceApp = Nothing
        End If
    End Sub

End Class
