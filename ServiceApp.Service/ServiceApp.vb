Imports ServiceApp.Logueos
Imports ServiceApp.Configuracion.Entidades
Imports ServiceApp.Configuracion.EntidadesMDL
Imports System.Windows.Forms

Public Class ServiceApp
    Implements IDisposable

    Private _WinSocket() As WinSocket.WSServer
    Private _Log As New Log
    Private _Timer As System.Timers.Timer
    Private _Servicio As Servicio
    Private _DatosServicio As New Dictionary(Of Integer, DatosServicioMDL)
    Private pForm As BIOIPRT.BIOIPRT

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
                    _WinSocket(iKey).Identificador = pDatosOtrosSistemasMDL.Identificador
                    If _WinSocket(iKey).Sistema = Definiciones.BackendEnum.Dimmep Then
                        '_Log.WriteLog("Error en ServiceApp.New - Ingreso a Dimmep", TraceEventType.Information)
                        pForm = New BIOIPRT.BIOIPRT
                        pForm.IP = _WinSocket(iKey).IPRemoto.ToString
                        pForm.Port = _WinSocket(iKey).PuertoRemoto
                        pForm.Identificador = _WinSocket(iKey).Identificador
                        pForm.ModoManual = False
                        pForm.EsVisible = False
                        '_Log.WriteLog("Error en ServiceApp.New - Antes de Show", TraceEventType.Information)
                        pForm.Show()
                        '_Log.WriteLog("Error en ServiceApp.New - Despues de Show", TraceEventType.Information)
                        '_Log.WriteLog("Error en ServiceApp.New - Envio pForm", TraceEventType.Information)
                        _WinSocket(iKey).Form = pForm
                        '_Log.WriteLog("Error en ServiceApp.New - Termino pForm", TraceEventType.Information)
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
                If _WinSocket(0) IsNot Nothing Then
                    _WinSocket(0).Dispose()
                    _WinSocket(0) = Nothing
                End If
                If _Log IsNot Nothing Then
                    _Log = Nothing
                End If

                If Not pForm Is Nothing Then
                    pForm.Dispose()
                    pForm.Close()
                End If
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
