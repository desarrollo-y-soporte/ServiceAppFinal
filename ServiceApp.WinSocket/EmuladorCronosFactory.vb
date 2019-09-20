Imports ServiceApp.Interface
Imports ServiceApp.BIOIPRT
Imports ServiceApp.Dummy
Imports ServiceApp.ZKTeco
Imports ServiceApp.Definiciones

Public Class EmuladorCronosFactory
    Public Function CreateInstance(ByVal TipoEmulador As BackendEnum) As IBackEnd
        Dim pResultado As IBackEnd
        Select Case TipoEmulador
            Case BackendEnum.Dummy
                pResultado = New ServiceApp.Dummy.Dummy
            Case BackendEnum.Dimmep
                pResultado = New ServiceApp.BIOIPRT.BIOIPRT()
            Case BackendEnum.ZKTeco
                pResultado = New ServiceApp.ZKTeco.ZKTeco()
            Case Else
                pResultado = Nothing
        End Select

        Return pResultado
    End Function
End Class
