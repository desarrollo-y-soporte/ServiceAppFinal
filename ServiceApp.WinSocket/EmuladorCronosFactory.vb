Imports ServiceApp.Interface
Imports ServiceApp.BIOIPRT
Imports ServiceApp.Dummy
Imports ServiceApp.ZKTeco
Imports ServiceApp.Interop
Imports ServiceApp.Definiciones

Public Class EmuladorCronosFactory
    Public Function CreateInstance(ByVal TipoEmulador As BackendEnum, ByVal Form As BIOIPRT.BIOIPRT) As IBackEnd
        Dim pResultado As IBackEnd
        Select Case TipoEmulador
            Case BackendEnum.Dummy
                pResultado = New ServiceApp.Dummy.Dummy
            Case BackendEnum.Dimmep
                pResultado = New ServiceApp.Interop.BIOIPRTClass(Form)
            Case BackendEnum.ZKTeco
                pResultado = New ServiceApp.ZKTeco.ZKTeco()
            Case Else
                pResultado = Nothing
        End Select

        Return pResultado
    End Function
End Class
