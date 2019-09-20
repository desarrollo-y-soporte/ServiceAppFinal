Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports ServiceApp.WinSocket
Imports ServiceApp.Definiciones
Imports ServiceApp.Logueos
Imports System.Net
Imports ServiceApp.Interface

Public Class EmuladorCronos
    Implements IDisposable

#Region "Declaraion Private"
    Private _WSInfoCliente As New WSInfoCliente
    Private _Log As New Log
    Private _InicioLectura As Boolean
    Private _FinLectura As Boolean
    Private _Operacion As OperacionesEnum
    Private _Sistema As BackendEnum
    Private _IPCliente As String
    Private _PortCliente As Integer
    Private _Traductor As Traductor
#End Region

#Region "Declaracion BackEnd"
    WithEvents _EmuladorCronos As IBackEnd
#End Region

#Region "Constantes"
    Private Const ACK As Byte = &H6
    Private Const NACK As Byte = &H5
#End Region

#Region "Declaracion Eventos"
    Public Event NuevaConexion()
    Public Event DatosRecibidos()
    Public Event ConexionTerminada()
#End Region

#Region "New and Finalize"
    Public Sub New()
        _Operacion = OperacionesEnum.Ninguna
        _Sistema = BackendEnum.Ninguno
        Dim pEmuladorCronosFactory As EmuladorCronosFactory = New EmuladorCronosFactory()
        _EmuladorCronos = pEmuladorCronosFactory.CreateInstance(_Sistema)
        _Traductor = New Traductor
    End Sub

    Public Sub New(ByVal pSistema As BackendEnum, ByVal pIP As String, ByVal pPort As Integer)
        _Operacion = OperacionesEnum.Ninguna
        _Sistema = pSistema
        _IPCliente = pIP
        _PortCliente = pPort
        Dim pEmuladorCronosFactory As EmuladorCronosFactory = New EmuladorCronosFactory()
        _EmuladorCronos = pEmuladorCronosFactory.CreateInstance(_Sistema)
        _Traductor = New Traductor(CInt(_Sistema))
    End Sub

    Protected Overrides Sub Finalize()
        If _EmuladorCronos IsNot Nothing Then
            _EmuladorCronos.Desconectar()
            _EmuladorCronos = Nothing
        End If
        MyBase.Finalize()
    End Sub
#End Region

#Region "Properties"
    Public Property InfoDeUnCliente As WSInfoCliente
        Get
            Return _WSInfoCliente
        End Get
        Set(value As WSInfoCliente)
            _WSInfoCliente = value
        End Set
    End Property

    Public Property Sistema As BackendEnum
        Get
            Return _Sistema
        End Get
        Set(value As BackendEnum)
            _Sistema = value
        End Set
    End Property

    Public Property IPCliente As String
        Get
            Return _IPCliente
        End Get
        Set(value As String)
            _IPCliente = value
        End Set
    End Property

    Public Property PortCliente As Integer
        Get
            Return _PortCliente
        End Get
        Set(value As Integer)
            _PortCliente = value
        End Set
    End Property
#End Region

#Region "Metodos"
    Private Sub LeerSocket()
        Dim Recibir() As Byte 'Array utilizado para recibir los datos que llegan
        Dim Ret As Integer = 0

        With _WSInfoCliente
            While True
                If .Socket.Connected Then
                    Recibir = New Byte(100) {}

                    Try
                        'Me quedo esperando a que llegue un mensaje desde el cliente
                        Ret = .Socket.Receive(Recibir, Recibir.Length, SocketFlags.None)
                        _Log.WriteLog("recibiendo info de: ", TraceEventType.Information)
                        If Ret > 0 Then
                            Dim j As Integer
                            j = -1
                            For i = 0 To Ret

                                If Chr(Recibir(i)) = ChrW(2) And Not _InicioLectura Then
                                    _InicioLectura = True
                                End If
                                If Chr(Recibir(i)) = ChrW(82) And _InicioLectura Then
                                    j = i + 2
                                    '_Operacion = OperacionesEnum.CambioFechaHora
                                End If
                                If i = j Then
                                    Dim pAccion() As Byte = {Recibir(i)}
                                    _Operacion = Encoding.ASCII.GetString(pAccion)
                                End If
                                If Chr(Recibir(i)) = ChrW(3) And _InicioLectura Then
                                    _FinLectura = True
                                End If
                                If _InicioLectura And _FinLectura Then
                                    .UltimosDatosRecibidos = Encoding.ASCII.GetString(Recibir)

                                    RaiseEvent DatosRecibidos()

                                    _InicioLectura = False
                                    _FinLectura = False
                                End If
                            Next
                        Else
                            'Genero el evento de la finalizacion de la conexion
                            RaiseEvent ConexionTerminada()
                            Exit While
                        End If
                    Catch e As Exception
                        If Not .Socket.Connected Then
                            'Genero el evento de la finalizacion de la conexion
                            RaiseEvent ConexionTerminada()
                            Exit While
                        End If
                    End Try
                End If
            End While

            Call CerrarThread()

        End With
    End Sub

    Public Sub Start()
        With _WSInfoCliente
            'Cuando se recibe la conexion, guardo la informacion del cliente
            'Guardo el Socket que utilizo para mantener la conexion con el cliente
            .Thread = New Thread(AddressOf LeerSocket)
            'Genero el evento Nueva conexion
            RaiseEvent NuevaConexion()
            'Inicio el thread encargado de escuchar los mensajes del cliente
            .Thread.Start()
            'End While
        End With
    End Sub

    Private Sub CerrarThread()
        Try
            _WSInfoCliente.Thread.Abort()
        Catch e As Exception
        End Try
    End Sub

    Public Sub Cerrar(ByVal IDCliente As Net.IPEndPoint)
        'Cierro la conexion con el cliente
        _WSInfoCliente.Socket.Close()
    End Sub

    Public Sub Cerrar()
        Call Cerrar(_WSInfoCliente.Socket.RemoteEndPoint)
    End Sub

    Public Sub EnviarDatos(ByVal IDCliente As Net.IPEndPoint, ByVal Datos As String)
        _Log.WriteLog("Enviando alguna info: " + Datos.ToString, TraceEventType.Information)
        _WSInfoCliente.Socket.Send(Encoding.ASCII.GetBytes(Datos))
    End Sub

    Public Sub EnviarACK()
        _Log.WriteLog("Enviando ACK: ", TraceEventType.Information)
        _WSInfoCliente.Socket.Send(Encoding.ASCII.GetBytes(ACK))
    End Sub

    Public Sub EnviarNACK()
        _Log.WriteLog("Enviando NACK: ", TraceEventType.Information)
        _WSInfoCliente.Socket.Send(Encoding.ASCII.GetBytes(NACK))
    End Sub

    Public Sub Accion(ByVal pDato As String)
        Select Case _Operacion
            Case OperacionesEnum.Lectura
                _EmuladorCronos.Lectura()
            Case OperacionesEnum.CambioFechaHora
                _EmuladorCronos.CambioFechaHora(CDate(pDato))
            Case OperacionesEnum.Borrado
                _EmuladorCronos.Borrado()
            Case OperacionesEnum.InhabilitacionTotal
                _EmuladorCronos.InhabilitacionTotal()
            Case OperacionesEnum.AltaTarjeta
                _EmuladorCronos.AltaTarjeta(pDato)
            Case OperacionesEnum.BajaTarjeta
                _EmuladorCronos.BajaTarjeta(pDato)
            Case OperacionesEnum.Ninguna
                _Log.WriteLog("No se reconocio operacion a ejecutar.", TraceEventType.Information)
        End Select
    End Sub
#End Region

#Region "Ejecucion Eventos"
    Private Sub EmuladorCronos_NuevaConexion() Handles Me.NuevaConexion
        _EmuladorCronos.IP = _IPCliente
        _EmuladorCronos.Port = _PortCliente
        If _EmuladorCronos.Conectado Then
            _EmuladorCronos.Desconectar()
        End If
        _EmuladorCronos.Conectar()
    End Sub

    Private Sub EmuladorCronos_ConexionTerminada() Handles Me.ConexionTerminada

    End Sub

    Private Sub EmuladorCronos_DatosRecibidos() Handles Me.DatosRecibidos
        Dim pMessage As String = _WSInfoCliente.UltimosDatosRecibidos
        Dim pInfo As String = ""

        _Log.WriteLog("Ultimos datos recibidos: " + pMessage.ToString, TraceEventType.Information)

        Accion(pInfo)

        EnviarACK()
    End Sub

    Private Sub _EmuladorCronos_FichadaOnlineEvent(pFecha As String, pId As String) Handles _EmuladorCronos.FichadaOnlineEvent
        Dim pTarjeta As String = _Traductor.Buscar(pId)

        _WSInfoCliente.Socket.Send(Encoding.ASCII.GetBytes(pFecha + pTarjeta))
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


#End Region

End Class
