Imports ServiceApp.Logueos
Imports ServiceApp.Configuracion.Entidades
Imports ServiceApp.Configuracion.EntidadesMDL

Public Class ServiceApp
    Implements IDisposable

    Private _WinSocket() As WinSocket.WSServer
    Private _Log As New Log
    Private _Timer As System.Timers.Timer
    Private _Servicio As Servicio
    Private _DatosServicio As New Dictionary(Of Integer, DatosServicioMDL)

    Public Sub New()
        Try
            _Servicio = New Servicio
            _Servicio.LoadFile()
            _DatosServicio = _Servicio.DatosOtrosSistemasDic


            If _DatosServicio.Count > 0 Then
                ReDim Preserve _WinSocket(_DatosServicio.Count - 1)
                For Each iKey As Integer In _DatosServicio.Keys
                    Dim pDatosOtrosSistemasMDL As DatosServicioMDL = _DatosServicio(iKey)
                    _WinSocket(iKey) = New WinSocket.WSServer
                    _WinSocket(iKey).IpAddressConnect = System.Net.IPAddress.Parse(pDatosOtrosSistemasMDL.IPServicio)
                    _WinSocket(iKey).Port = pDatosOtrosSistemasMDL.PuertoServicio
                    _WinSocket(iKey).IPRemoto = pDatosOtrosSistemasMDL.IPRemota
                    _WinSocket(iKey).PuertoRemoto = pDatosOtrosSistemasMDL.PuertoRemoto
                    _WinSocket(iKey).Sistema = pDatosOtrosSistemasMDL.Sistema
                    If _WinSocket(iKey).Sistema = Definiciones.BackendEnum.Dimmep Then
                        Dim pForm As New BIOIPRT.BIOIPRT
                        pForm.IP = _WinSocket(iKey).IPRemoto.ToString
                        pForm.Show()
                        _WinSocket(iKey).Form = pForm
                    Else
                        _WinSocket(iKey).Form = Nothing
                    End If
                    _Log.WriteLog("Iniciando Escucha para - " + _WinSocket(iKey).IpAddressConnect.ToString + ":" + _WinSocket(iKey).Port.ToString, TraceEventType.Information)
                    _WinSocket(iKey).Escuchar()
                Next
            End If
        Catch ex As Exception
            _Log.WriteLog("Error en ServiceApp.New - " + ex.Message.ToString, TraceEventType.Information)
        End Try


    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
                _WinSocket(0) = Nothing
                _Log = Nothing
            End If

            ' TODO: libere los recursos no administrados (objetos no administrados) y reemplace Finalize() a continuación.
            ' TODO: configure los campos grandes en nulos.
        End If
        disposedValue = True
    End Sub

    ' TODO: reemplace Finalize() solo si el anterior Dispose(disposing As Boolean) tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
        Dispose(True)
        ' TODO: quite la marca de comentario de la siguiente línea si Finalize() se ha reemplazado antes.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
