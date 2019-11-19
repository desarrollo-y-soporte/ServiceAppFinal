Imports System.Text
Imports ServiceApp.Interface
Imports ServiceApp.Logueos
Imports ServiceApp.Definiciones
Imports System.Threading

Public Class Dummy
    Implements IDisposable, IBackEnd

    Private _Log As New Log
    Private _Port As Integer
    Private _IP As String
    Private _Conectado As Boolean
    Private _Accion As BackendEnum
    Private _Mensaje As String
    Private _CantidadFichadas As Integer
    Private _Identificador As Integer

    WithEvents _WSCliente As New WSCliente
    Public Event FichadaOnlineEvent As IBackEnd.FichadaOnlineEventEventHandler Implements IBackEnd.FichadaOnlineEvent

#Region "New and Finalize"
    Public Sub New()
        _Conectado = False
        _CantidadFichadas = 20
    End Sub

    Public Sub New(ByVal pIP As String, ByVal pPort As Integer)
        Me.New()
        _IP = pIP
        _Port = pPort
        _Conectado = False
    End Sub
#End Region

#Region "IBackEnd"
    Public Function Lectura() As String Implements IBackEnd.Lectura
        IBackEnd_Conectar()
        Dim pFichadas As New List(Of String)
        pFichadas.Add("20190929000100" + ", 1")
        pFichadas.Add("20190929000200" + ", 2")
        pFichadas.Add("20190929000300" + ", 3")
        pFichadas.Add("20190929000400" + ", 4")
        pFichadas.Add("20190929000500" + ", 5")
        pFichadas.Add("20190929000600" + ", 6")
        pFichadas.Add("20190929000700" + ", 7")
        pFichadas.Add("20190929000800" + ", 8")
        pFichadas.Add("20190929000900" + ", 9")
        pFichadas.Add("20190929001000" + ", 10")
        pFichadas.Add("20190929001100" + ", 11")
        pFichadas.Add("20190929001200" + ", 12")
        pFichadas.Add("20190929001300" + ", 13")
        pFichadas.Add("20190929001400" + ", 14")
        pFichadas.Add("20190929001500" + ", 15")
        pFichadas.Add("20190929001600" + ", 16")
        pFichadas.Add("20190929001700" + ", 17")
        pFichadas.Add("20190929001800" + ", 18")
        pFichadas.Add("20190929001900" + ", 19")
        pFichadas.Add("20190929002000" + ", 20")
        _Log.WriteLog("Dummy - Lectura", TraceEventType.Information)
        Return ""
    End Function

    Public Sub CambioFechaHora(pFecha As Date) Implements IBackEnd.CambioFechaHora
        IBackEnd_Conectar()
        _WSCliente.EnviarDatos(CInt(OperacionesEnum.CambioFechaHora).ToString)
        _Log.WriteLog("Dummy - Cambio Fecha Hora", TraceEventType.Information)
    End Sub

    Public Sub Borrado() Implements IBackEnd.Borrado
        IBackEnd_Conectar()
        _WSCliente.EnviarDatos(CInt(OperacionesEnum.Borrado).ToString)
        _Log.WriteLog("Dummy - Borrado", TraceEventType.Information)
    End Sub

    Public Sub InhabilitacionTotal() Implements IBackEnd.InhabilitacionTotal
        IBackEnd_Conectar()
        _WSCliente.EnviarDatos(CInt(OperacionesEnum.InhabilitacionTotal).ToString)
        _Log.WriteLog("Dummy - Inhabilitacion Total", TraceEventType.Information)
    End Sub

    Public Sub AltaTarjeta(pId As String) Implements IBackEnd.AltaTarjeta
        IBackEnd_Conectar()
        _WSCliente.EnviarDatos(CInt(OperacionesEnum.AltaTarjeta).ToString)
        _Log.WriteLog("Dummy - Alta de Tarjeta", TraceEventType.Information)
    End Sub

    Public Sub BajaTarjeta(pId As String) Implements IBackEnd.BajaTarjeta
        IBackEnd_Conectar()
        _WSCliente.EnviarDatos(CInt(OperacionesEnum.BajaTarjeta).ToString)
        _Log.WriteLog("Baja de Tarjeta", TraceEventType.Information)
    End Sub

    'Public Sub PedidoInicializacion() Implements IBackEnd.PedidoInicializacion
    '    _WSCliente.EnviarDatos(CInt(OperacionesEnum.Inicializacion).ToString)
    '    _Log.WriteLog("Baja de Tarjeta", TraceEventType.Information)
    'End Sub

    Public Function CantidadFichadas() As Integer Implements IBackEnd.CantidadFichadas
        IBackEnd_Conectar()
        Return _CantidadFichadas
    End Function

    Public Function FechaHoraUltInicializacion() As Date Implements IBackEnd.FechaHoraUltInicializacion
        IBackEnd_Conectar()
        Return Date.Now
    End Function

    Public Sub IBackEnd_Conectar() Implements IBackEnd.Conectar
        If Not _Conectado Then
            With _WSCliente
                .IP = _IP
                .Port = _Port
                .Conectar()
            End With
            _Conectado = True
        End If
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

    Public Property Identificador As Integer Implements IBackEnd.Identificador
        Get
            Return _Identificador
        End Get
        Set(value As Integer)
            _Identificador = value
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
        Dim pOperacion As OperacionesEnum

        pOperacion = CInt(pSpliteo(0))
        Select Case pOperacion
            Case OperacionesEnum.FichadaOnLine
                _Log.WriteLog("Fecha y Hora: " + pSpliteo(1), TraceEventType.Information)
                _Log.WriteLog("Tarjeta: " + pSpliteo(2), TraceEventType.Information)
                RaiseEvent FichadaOnlineEvent(CDate(pSpliteo(1)), pSpliteo(2), "E")
            Case OperacionesEnum.Inicializacion
                'RaiseEvent PedidoInicializacionEvent(datos)
            Case OperacionesEnum.Lectura
                'RaiseEvent LecturaFichadaEvent(datos)
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

    Public Function PrepararLectura() As Boolean Implements IBackEnd.PrepararLectura
        Throw New NotImplementedException()
    End Function

#End Region

End Class
