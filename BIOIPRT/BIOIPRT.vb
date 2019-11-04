
Imports AxBIO_IP_RealTime
Imports ServiceApp.Interface
Imports ServiceApp.Logueos

Public Class BIOIPRT
    Implements IBackEnd

    Private _IP As String
    Private _Port As Integer
    Private _Log As New Log
    Private _IsConnected As Boolean
    Private _Reloj As Integer
    Private _CantidadFichadas As Integer
    Private _Fecha As Date
    Private _Hora As Date
    Dim pFichadas As New List(Of String)

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        Call AxBIO_IP_RT2.InicializarComponente(5, BIO_IP_RealTime.TipoVerificador.Módulo_11, BIO_IP_RealTime.TipoCriptografia.Normal)
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

        pFichadas = Nothing
        _Reloj = 1
    End Sub

    Public Property IP As String Implements IBackEnd.IP
        Get
            Return _IP
        End Get
        Set(value As String)
            _IP = value
            Call AxBIO_IP_RT2.AdicionaRelogio(_Reloj, _IP, 10, 0, 8, 0, 0, 0)
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
            Return _IsConnected
        End Get
        Set(value As Boolean)
            _IsConnected = value
        End Set
    End Property

    Public Event FichadaOnlineEvent As IBackEnd.FichadaOnlineEventEventHandler Implements IBackEnd.FichadaOnlineEvent

    Public Sub CambioFechaHora(pFecha As Date) Implements IBackEnd.CambioFechaHora
        Try
            AxBIO_IP_RT2.DataHora(_Reloj, pFecha)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub Borrado() Implements IBackEnd.Borrado
        Throw New NotImplementedException()
    End Sub

    Public Sub InhabilitacionTotal() Implements IBackEnd.InhabilitacionTotal
        Throw New NotImplementedException()
    End Sub

    Public Sub AltaTarjeta(pId As String) Implements IBackEnd.AltaTarjeta
        Throw New NotImplementedException()
    End Sub

    Public Sub BajaTarjeta(pId As String) Implements IBackEnd.BajaTarjeta
        Throw New NotImplementedException()
    End Sub

    Public Sub Conectar() Implements IBackEnd.Conectar
        Try
            Desconectar()
            Call AxBIO_IP_RT2.ConectarRelogios()
            _IsConnected = True
            'Threading.Thread.Sleep(10000)
        Catch ex As Exception
            Throw New Exception(ex.Message)
            _IsConnected = False
        End Try
    End Sub

    Public Sub Desconectar() Implements IBackEnd.Desconectar
        Try
            If _IsConnected Then
                Call AxBIO_IP_RT2.DesconectarRelogio(_Reloj)
                _IsConnected = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
            _IsConnected = False
        End Try
    End Sub

    Private Sub AxBIO_IP_RT2_ConcluidoStatus(sender As Object, e As __BIO_IP_RT_ConcluidoStatusEvent) Handles AxBIO_IP_RT2.ConcluidoStatus
        _CantidadFichadas = e.quantidade_Registros
        _Fecha = e.data
        _Hora = e.hora
    End Sub

    Private Sub AxBIO_IP_RT2_Error(sender As Object, e As __BIO_IP_RT_ErrorEvent) Handles AxBIO_IP_RT2.[Error]
        _Log.WriteLog("Se produjo un error: " + e.descricao, TraceEventType.Information)
    End Sub

    Public Function CantidadFichadas() As Integer Implements IBackEnd.CantidadFichadas
        Call AxBIO_IP_RT2.Status(_Reloj)
        Throw New NotImplementedException()
    End Function

    Public Function FechaHoraUltInicializacion() As Date Implements IBackEnd.FechaHoraUltInicializacion
        Throw New NotImplementedException()
    End Function

    Public Function Lectura() As String Implements IBackEnd.Lectura
        Throw New NotImplementedException()
    End Function

    Public Function PrepararLectura() As Boolean Implements IBackEnd.PrepararLectura
        Throw New NotImplementedException()
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Conectar()
    End Sub

    Private Sub AxBIO_IP_RT2_Conectado(sender As Object, e As __BIO_IP_RT_ConectadoEvent) Handles AxBIO_IP_RT2.Conectado
        'Call AxBIO_IP_RT2.Status(_Reloj)
    End Sub


End Class

