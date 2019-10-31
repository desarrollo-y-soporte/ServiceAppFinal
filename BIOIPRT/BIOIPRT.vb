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
            AxBIO_IP_RT1.DataHora(_Reloj, pFecha)
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
            AxBIO_IP_RT1.InicializarComponente(16, BIO_IP_RealTime.TipoVerificador.Sem_Dígito_Verificador, BIO_IP_RealTime.TipoCriptografia.Normal)
            AxBIO_IP_RT1.AdicionaRelogio(_Reloj, _IP, 20, 15, 8, 0, 1, 1)
            AxBIO_IP_RT1.ConectarRelogio(_Reloj)
            AxBIO_IP_RT1.Status(_Reloj)

        Catch ex As Exception
            Throw New Exception(ex.Message)
            _IsConnected = False
        End Try
    End Sub

    Public Sub Desconectar() Implements IBackEnd.Desconectar
        Try
            If _IsConnected Then
                AxBIO_IP_RT1.DesconectarRelogio(_Reloj)
                _IsConnected = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
            _IsConnected = False
        End Try
    End Sub

    Private Sub AxBIO_IP_RT1_ConcluidoStatus(sender As Object, e As __BIO_IP_RT_ConcluidoStatusEvent) Handles AxBIO_IP_RT1.ConcluidoStatus
        _CantidadFichadas = e.quantidade_Registros
        _Fecha = e.data
        _Hora = e.hora
    End Sub

    Private Sub AxBIO_IP_RT1_ConcluidoDataHora(sender As Object, e As __BIO_IP_RT_ConcluidoDataHoraEvent) Handles AxBIO_IP_RT1.ConcluidoDataHora
        _Log.WriteLog("Se cambio el dia y hora", TraceEventType.Information)
    End Sub

    Private Sub AxBIO_IP_RT1_Error(sender As Object, e As __BIO_IP_RT_ErrorEvent) Handles AxBIO_IP_RT1.[Error]
        _Log.WriteLog("Se produjo un error: " + e.descricao, TraceEventType.Information)
    End Sub

    Private Sub AxBIO_IP_RT1_RegistroRecolhido(sender As Object, e As __BIO_IP_RT_RegistroRecolhidoEvent) Handles AxBIO_IP_RT1.RegistroRecolhido
        Try
            Dim pFecha As New Date(CInt(e.data.Substring(4, 2)), CInt(e.data.Substring(2, 2)), CInt(e.data.Substring(0, 2)), CInt(e.hora.Substring(0, 2)), CInt(e.hora.Substring(2, 2)), 0)
            Dim pFechaHora As Date

            'pFecha = CDate(e.data)
            'pHora = CDate(e.hora)
            pFechaHora = _Fecha.AddHours(_Hora.Hour).AddMinutes(_Hora.Minute).AddSeconds(_Hora.Second)

            pFichadas.Add(pFecha.ToString("yyyyMMddhhmmss") + "," + e.matricula.ToString)

            AxBIO_IP_RT1.RegistroGravado(_Reloj)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub AxBIO_IP_RT1_Conectado(sender As Object, e As __BIO_IP_RT_ConectadoEvent) Handles AxBIO_IP_RT1.Conectado
        _Log.WriteLog("Se ha Conectado a: " + e.numero_Relogio.ToString, TraceEventType.Information)
        _IsConnected = True
        AxBIO_IP_RT1.StatusComunicacao(_Reloj)
    End Sub

    Private Sub AxBIO_IP_RT1_ConcluidoStatusComunicacao(sender As Object, e As __BIO_IP_RT_ConcluidoStatusComunicacaoEvent) Handles AxBIO_IP_RT1.ConcluidoStatusComunicacao
        AxBIO_IP_RT1.Status(_Reloj)
    End Sub

    Public Function CantidadFichadas() As Integer Implements IBackEnd.CantidadFichadas
        Return _CantidadFichadas
    End Function

    Public Function FechaHoraUltInicializacion() As Date Implements IBackEnd.FechaHoraUltInicializacion
        Dim pFechaHora As Date
        Try
            pFechaHora = _Fecha.AddHours(_Hora.Hour).AddMinutes(_Hora.Minute).AddSeconds(_Hora.Second)
        Catch ex As Exception
            pFechaHora = Date.Now
        End Try
        Return pFechaHora
    End Function

    Public Function Lectura() As String Implements IBackEnd.Lectura
        Return ""
    End Function

    Public Function PrepararLectura() As Boolean Implements IBackEnd.PrepararLectura
        Throw New NotImplementedException()
    End Function
End Class

