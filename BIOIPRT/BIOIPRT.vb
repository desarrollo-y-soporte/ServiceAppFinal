Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports AxBIO_IP_RealTime
Imports ServiceApp.BIOIPRT
Imports ServiceApp.Interface
Imports ServiceApp.Logueos
Imports ServiceApp.Configuracion.Entidades
Imports ServiceApp.Configuracion.EntidadesMDL

Public Class BIOIPRT
    Implements IBackEnd

    Private _Reloj As Integer
    Private _IsConnected As Boolean
    Private _Log As New Log
    Private _IP As String
    Private _Port As Integer
    Private _CantidadFichadas As Integer
    Private _Fecha As Date
    Private _Path As String
    Private _NombreArchivo As String
    Private _StreamWriter As StreamWriter
    Private _ModoManual As Boolean
    Private _Linea As String
    Private _EsVisible As Boolean

    Private _Servicio As New Servicio
    Private _DatosServicio As New Dictionary(Of Integer, DatosServicioMDL)

    Public Event FichadaOnlineEvent As IBackEnd.FichadaOnlineEventEventHandler Implements IBackEnd.FichadaOnlineEvent
    Public Event EnviarLectura()

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Call AxBIO_IP_RT2.InicializarComponente(10, BIO_IP_RealTime.TipoVerificador.Módulo_11, BIO_IP_RealTime.TipoCriptografia.Normal)
        '_Reloj = 1
        _Port = 3000
        _ModoManual = True
        _EsVisible = True
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

    Public Property ModoManual As Boolean
        Get
            Return _ModoManual
        End Get
        Set(value As Boolean)
            _ModoManual = value
        End Set
    End Property

    Public Property Linea As String
        Get
            Return _Linea
        End Get
        Set(value As String)
            _Linea = value
        End Set
    End Property

    Public Property Identificador As Integer Implements IBackEnd.Identificador
        Get
            Return _Reloj
        End Get
        Set(value As Integer)
            _Reloj = value
        End Set
    End Property

    Public Property EsVisible As Boolean
        Get
            Return _EsVisible
        End Get
        Set(value As Boolean)
            _EsVisible = value
        End Set
    End Property

    Public Sub CambioFechaHora(pFecha As Date) Implements IBackEnd.CambioFechaHora
        If _IsConnected Then
            Try
                AxBIO_IP_RT2.DataHora(_Reloj, pFecha)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
    End Sub

    Public Sub Borrado() Implements IBackEnd.Borrado
        If _IsConnected Then
            Try
                GuardarConfig()
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
    End Sub

    Public Sub InhabilitacionTotal() Implements IBackEnd.InhabilitacionTotal
        If _IsConnected Then
            Try
                AxBIO_IP_RT2.Desativar(_Reloj, True)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
    End Sub

    Public Sub AltaTarjeta(pId As String) Implements IBackEnd.AltaTarjeta
        AxBIO_IP_RT2.EnviarCrachaIndividual(_Reloj, pId)
    End Sub

    Public Sub BajaTarjeta(pId As String) Implements IBackEnd.BajaTarjeta
        AxBIO_IP_RT2.ExcluirCrachaIndividual(_Reloj, pId)
    End Sub

    Public Sub Conectar() Implements IBackEnd.Conectar
        Try
            Desconectar()

            Call AxBIO_IP_RT2.ConectarRelogio(_Reloj)

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
        lblError.Text = e.descricao
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
        If _IP <> "" And _Reloj <> -1 Then
            Conectar()
        End If
    End Sub

    Private Sub AxBIO_IP_RT2_Conectado(sender As Object, e As __BIO_IP_RT_ConectadoEvent) Handles AxBIO_IP_RT2.Conectado
        _IsConnected = True
        cmdListado.Enabled = True
        If Not ModoManual Then
            'Status()
        End If

    End Sub

    Private Sub cmdListado_Click(sender As Object, e As EventArgs) Handles cmdListado.Click
        If _IsConnected Then
            Try
                If _ModoManual Then
                    _NombreArchivo = Date.Now().ToString("yyyymmddhhmmss") + ".txt"
                    lblArchivo.Text = _Path + _NombreArchivo
                    _StreamWriter = File.AppendText(_Path + _NombreArchivo)
                    _StreamWriter.Flush()
                    _StreamWriter.Close()
                End If
                IniciarLecutar()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If
    End Sub

    Public Sub IniciarLecutar()
        Call AxBIO_IP_RT2.IniciarColetaBackup(_Reloj, _Fecha.ToShortDateString)
    End Sub

    Private Sub BIOIPRT_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If _IsConnected Then
            Desconectar()
        End If
    End Sub

    Private Sub BIOIPRT_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            _Path = Path.GetTempPath()
            cmdConectar.Enabled = False
            cmdListado.Enabled = False
            If _IP <> "" And _Reloj <> -1 Then
                txtIP.Text = _IP
                txtIdentificador.Text = CStr(_Reloj)

                AdicionarReloj()
                'AddHandler pBioIpRtClassInterop.ConectarEvent, New eve(AddressOf axCZKEM1_OnFinger)
            End If
            CargarConfig()
        Catch ex As Exception
            _Log.WriteLog("Se produjo un error: " + ex.Message, TraceEventType.Information)
        End Try

    End Sub

    Private Sub AdicionarReloj()
        If _IP <> "" And _Reloj <> -1 Then
            Call AxBIO_IP_RT2.AdicionaRelogio(_Reloj, _IP, 10, 0, 8, 0, 0, 0, BIO_IP_RealTime.TipoAck.Ack1, BIO_IP_RealTime.ModeloRelogio.Sensor_2, BIO_IP_RealTime.TipoAcessoDigital.Remoto_1_N, _Port)
        End If
    End Sub

    Private Sub cmdAdicionar_Click(sender As Object, e As EventArgs) Handles cmdAdicionar.Click
        If txtIP.Text <> "" And txtIdentificador.Text <> "" Then
            If ValidarIP(txtIP.Text) Then
                cmdConectar.Enabled = True
                _IP = txtIP.Text
                _Reloj = CInt(txtIdentificador.Text)
                AdicionarReloj()
            End If
        End If
    End Sub

    Private Sub AxBIO_IP_RT2_ConcluidoColetaBackup(sender As Object, e As __BIO_IP_RT_ConcluidoColetaBackupEvent) Handles AxBIO_IP_RT2.ConcluidoColetaBackup

    End Sub

    Private Function ValidarIP(ByVal pIP As String) As Boolean
        Dim pReturn As Boolean
        Try
            Dim IP As IPAddress = IPAddress.Parse(pIP)
            pReturn = True
        Catch ex As Exception
            pReturn = False
        End Try
        Return pReturn
    End Function

    Private Sub AxBIO_IP_RT2_RegistroRecolhido(sender As Object, e As __BIO_IP_RT_RegistroRecolhidoEvent) Handles AxBIO_IP_RT2.RegistroRecolhido
        Dim pLinea As String = e.matricula.ToString + "," + e.data.ToString + "," + e.hora.ToString + "," + e.status.ToString + "," + e.funcao.ToString
        'MsgBox(e.matricula)
        If _ModoManual Then
            Dim pStreamWriter As New System.IO.StreamWriter(_Path + _NombreArchivo, True)
            pStreamWriter.WriteLine(pLinea)
            pStreamWriter.Flush()
            pStreamWriter.Close()
        Else
            _Linea = pLinea
            RaiseEvent EnviarLectura()
        End If
        Call AxBIO_IP_RT2.RegistroGravado(_Reloj)
    End Sub

    Private Sub lblArchivo_Click(sender As Object, e As EventArgs) Handles lblArchivo.Click
        Process.Start("explorer.exe", _Path)
    End Sub

    Private Sub BIOIPRT_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        If _Log IsNot Nothing Then
            _Log = Nothing
        End If

        If _StreamWriter IsNot Nothing Then
            _StreamWriter = Nothing
        End If

        If _Servicio IsNot Nothing Then
            _Servicio = Nothing
        End If
    End Sub

    Private Sub BIOIPRT_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.Visible = _EsVisible
    End Sub

    Private Sub CargarConfig()
        Try
            _Servicio.LoadFile()
            _DatosServicio = _Servicio.DatosOtrosSistemasDic
            If _DatosServicio.Count > 0 Then
                For Each iKey As Integer In _DatosServicio.Keys
                    Dim pDatosOtrosSistemasMDL As DatosServicioMDL = _DatosServicio(iKey)
                    If pDatosOtrosSistemasMDL.IPRemota = _IP And pDatosOtrosSistemasMDL.PuertoRemoto = _Port And pDatosOtrosSistemasMDL.Identificador = _Reloj Then
                        _Fecha = pDatosOtrosSistemasMDL.UltFechaBorrado
                    End If
                Next
            End If
        Catch ex As Exception
            _Log.WriteLog("Se produjo un error: " + ex.Message, TraceEventType.Information)
        End Try
    End Sub

    Private Sub GuardarConfig()
        Dim pDatosServicio As New Dictionary(Of Integer, DatosServicioMDL)

        If _DatosServicio.Count > 0 Then
            For Each iKey As Integer In _DatosServicio.Keys
                Dim pDatosOtrosSistemasMDL As DatosServicioMDL = _DatosServicio(iKey)
                If pDatosOtrosSistemasMDL.IPRemota = _IP And pDatosOtrosSistemasMDL.PuertoRemoto = _Port And pDatosOtrosSistemasMDL.Identificador = _Reloj Then
                    _Fecha = Date.Now
                    pDatosOtrosSistemasMDL.UltFechaBorrado = _Fecha
                End If
                pDatosServicio.Add(iKey, pDatosOtrosSistemasMDL)
            Next
            _Servicio.DatosOtrosSistemasDic = pDatosServicio
            _Servicio.SaveFile()
        End If

    End Sub
End Class

