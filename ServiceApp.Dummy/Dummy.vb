Imports System.Text
Imports ServiceApp.Interface
Imports ServiceApp.Logueos

Public Class Dummy
    Implements IDisposable, IBackEnd

    Private _Log As New Log
    Private _Port As Integer
    Private _IP As String
    Private _Conectado As Boolean

    WithEvents _WSCliente As New WSCliente
    Public Event FichadaOnlineEvent As IBackEnd.FichadaOnlineEventEventHandler Implements IBackEnd.FichadaOnlineEvent

#Region "New and Finalize"
    Public Sub New()
        _Conectado = False
    End Sub

    Public Sub New(ByVal pIP As String, ByVal pPort As Integer)
        _IP = pIP
        _Port = pPort
        _Conectado = False
    End Sub
#End Region

#Region "IBackEnd"
    Public Sub Lectura() Implements IBackEnd.Lectura
        _WSCliente.EnviarDatos("Dummy - Lectura")
        _Log.WriteLog("Dummy - Lectura", TraceEventType.Information)
    End Sub

    Public Sub CambioFechaHora(pFecha As Date) Implements IBackEnd.CambioFechaHora
        _WSCliente.EnviarDatos("Dummy - Cambio Fecha Hora")
        _Log.WriteLog("Dummy - Cambio Fecha Hora", TraceEventType.Information)
    End Sub

    Public Sub Borrado() Implements IBackEnd.Borrado
        _WSCliente.EnviarDatos("Dummy - Borrado")
        _Log.WriteLog("Dummy - Borrado", TraceEventType.Information)
    End Sub

    Public Sub InhabilitacionTotal() Implements IBackEnd.InhabilitacionTotal
        _WSCliente.EnviarDatos("Dummy - Inhabilitacion Total")
        _Log.WriteLog("Dummy - Inhabilitacion Total", TraceEventType.Information)
    End Sub

    Public Sub AltaTarjeta(pId As String) Implements IBackEnd.AltaTarjeta
        _WSCliente.EnviarDatos("Dummy - Alta de Tarjeta")
        _Log.WriteLog("Dummy - Alta de Tarjeta", TraceEventType.Information)
    End Sub

    Public Sub BajaTarjeta(pId As String) Implements IBackEnd.BajaTarjeta
        _WSCliente.EnviarDatos("Dummy - Baja de Tarjeta")
        _Log.WriteLog("Baja de Tarjeta", TraceEventType.Information)
    End Sub

    Public Sub PedidoInicializacion() Implements IBackEnd.PedidoInicializacion
        Throw New NotImplementedException()
    End Sub

    Public Function CantidadFichadas() As Integer Implements IBackEnd.CantidadFichadas
        Throw New NotImplementedException()
    End Function

    Public Sub IBackEnd_Conectar() Implements IBackEnd.Conectar
        With _WSCliente
            .IP = _IP
            .Port = _Port
            .Conectar()
        End With
        _Conectado = True
    End Sub

    Public Sub IBackEnd_Desconectar() Implements IBackEnd.Desconectar
        _Conectado = False
        _WSCliente = Nothing
        _Log.WriteLog("Desconectar", TraceEventType.Information)
    End Sub

#Region "Properties"
    Public Property IP As String Implements IBackEnd.IP
        Get
            Return _IP
        End Get
        Set(value As String)
            _IP = value
        End Set
    End Property

    Public Property Port As Integer Implements IBackEnd.Port
        Get
            Return _Port
        End Get
        Set(value As Integer)
            _Port = value
        End Set
    End Property

    Public Property Conectado As Boolean Implements IBackEnd.Conectado
        Get
            Return _Conectado
        End Get
        Set(value As Boolean)
            _Conectado = value
        End Set
    End Property
#End Region
#End Region

#Region "WSClient"
    Private Sub _WSCliente_ConexionTerminada() Handles _WSCliente.ConexionTerminada
        IBackEnd_Desconectar()
    End Sub

    Private Sub _WSCliente_DatosRecibidos(datos As String) Handles _WSCliente.DatosRecibidos
        _Log.WriteLog("Accion: " + datos, TraceEventType.Information)
        Dim pSpliteo() As String
        datos = datos.Replace(vbNullChar, "")
        Dim pMensaje As String = RTrim(LTrim(datos))
        pSpliteo = pMensaje.Split(",")

        Select Case pSpliteo(0)
            Case "1"
                _Log.WriteLog("Fecha y Hora: " + pSpliteo(1), TraceEventType.Information)
                _Log.WriteLog("Tarjeta: " + pSpliteo(2), TraceEventType.Information)
                RaiseEvent FichadaOnlineEvent(pSpliteo(1), pSpliteo(2))
        End Select

    End Sub
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
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

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

#End Region

End Class
