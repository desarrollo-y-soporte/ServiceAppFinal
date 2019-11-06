Imports System.ComponentModel
Imports AxBIO_IP_RealTime
Imports ServiceApp.BIOIPRT
Imports ServiceApp.Interface
Imports ServiceApp.Logueos

Public Class BIOIPRT
    Implements IBackEnd

    Private _Reloj As Integer
    Private _IsConnected As Boolean
    Private _Log As New Log
    Private _IP As String
    Private _Port As Integer
    Private _CantidadFichadas As Integer
    Private _Fecha As Date

    Public Event FichadaOnlineEvent As IBackEnd.FichadaOnlineEventEventHandler Implements IBackEnd.FichadaOnlineEvent

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Call AxBIO_IP_RT2.InicializarComponente(5, BIO_IP_RealTime.TipoVerificador.Módulo_11, BIO_IP_RealTime.TipoCriptografia.Normal)
        _Reloj = 1
    End Sub

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
            Return _IsConnected
        End Get
        Set(value As Boolean)
            _IsConnected = value
        End Set
    End Property

    Public Property IsConnected As Boolean
        Get
            Return _IsConnected
        End Get
        Set(value As Boolean)
            _IsConnected = value
        End Set
    End Property

    Public Property Reloj As Integer
        Get
            Return _Reloj
        End Get
        Set(value As Integer)
            _Reloj = value
        End Set
    End Property

    Public Sub CambioFechaHora(pFecha As Date) Implements IBackEnd.CambioFechaHora
        Try
            'AxBIO_IP_RT2.DataHora(pBioiprtClass.Reloj, pFecha)
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
        '_Fecha = e.data
        '_Hora = e.hora
        _Fecha = e.data.AddHours(e.hora.Hour).AddMinutes(e.hora.Minute).AddSeconds(e.hora.Second)
    End Sub

    Private Sub AxBIO_IP_RT2_Error(sender As Object, e As __BIO_IP_RT_ErrorEvent) Handles AxBIO_IP_RT2.[Error]
        _Log.WriteLog("Se produjo un error: " + e.descricao, TraceEventType.Information)
    End Sub

    Public Sub Status()
        Call AxBIO_IP_RT2.Status(_Reloj)
    End Sub

    Public Function CantidadFichadas() As Integer Implements IBackEnd.CantidadFichadas
        Return _CantidadFichadas
    End Function

    Public Function FechaHoraUltInicializacion() As Date Implements IBackEnd.FechaHoraUltInicializacion
        Return _Fecha
    End Function

    Public Function Lectura() As String Implements IBackEnd.Lectura
        Throw New NotImplementedException()
    End Function

    Public Function PrepararLectura() As Boolean Implements IBackEnd.PrepararLectura
        Throw New NotImplementedException()
    End Function

    Private Sub cmdConectar_Click(sender As Object, e As EventArgs) Handles cmdConectar.Click
        If txtIP.Text <> "" Then
            Conectar()
        End If
    End Sub

    Private Sub AxBIO_IP_RT2_Conectado(sender As Object, e As __BIO_IP_RT_ConectadoEvent) Handles AxBIO_IP_RT2.Conectado
        _IsConnected = True
        Status()
    End Sub

    Private Sub cmdListado_Click(sender As Object, e As EventArgs) Handles cmdListado.Click
        'Call AxBIO_IP_RT2.Status(pBioiprtClass.Reloj)
    End Sub

    Private Sub BIOIPRT_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If _IsConnected Then
            Desconectar()
        End If
    End Sub

    Private Sub BIOIPRT_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtIP.Text = _IP

        Call AxBIO_IP_RT2.AdicionaRelogio(_Reloj, _IP, 10, 0, 8, 0, 0, 0)
        'AddHandler pBioIpRtClassInterop.ConectarEvent, New eve(AddressOf axCZKEM1_OnFinger)
    End Sub

    Private Sub cmdTest_Click(sender As Object, e As EventArgs) Handles cmdTest.Click
        'MsgBox(pBioIpRtClassInterop.GetValor)
        Status()
    End Sub

End Class

