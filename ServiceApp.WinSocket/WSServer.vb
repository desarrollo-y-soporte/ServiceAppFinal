Imports System.Threading
Imports System.Net.Sockets
Imports System.Net
Imports ServiceApp.Logueos
Imports ServiceApp.Definiciones

Public Class WSServer

#Region "VARIABLES"
    Private _TcpListener As TcpListener
    Private _Thread As Thread
    Private _Port As Integer
    Private _IpAddressConnect As IPAddress
    Private _Log As New Log
    Private _Sistema As BackendEnum
    Private _WSInfoCliente As New WSInfoCliente
    Private _IPRemoto As String
    Private _PuertoRemoto As Integer
#End Region

#Region "EVENTOS"
    Public Event NuevaConexion()
    Public Event DatosRecibidos()
    Public Event ConexionTerminada()
#End Region


#Region "PROPIEDADES"
    Property Port() As Integer
        Get
            Port = _Port
        End Get
        Set(ByVal Value As Integer)
            _Port = Value
        End Set
    End Property

    Public Property IpAddressConnect As IPAddress
        Get
            Return _IpAddressConnect
        End Get
        Set(value As IPAddress)
            _IpAddressConnect = value
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

    Public Property IPRemoto As String
        Get
            Return _IPRemoto
        End Get
        Set(value As String)
            _IPRemoto = value
        End Set
    End Property

    Public Property PuertoRemoto As Integer
        Get
            Return _PuertoRemoto
        End Get
        Set(value As Integer)
            _PuertoRemoto = value
        End Set
    End Property
#End Region

#Region "METODOS"
    Public Sub Escuchar()
        Try
            _TcpListener = New TcpListener(_IpAddressConnect, _Port)
            'Inicio la escucha
            _TcpListener.Start()
            'Creo un thread para que se quede escuchando la llegada de un cliente
            _Thread = New Thread(AddressOf EsperarCliente)
            If _Sistema = BackendEnum.Dimmep Then
                _Thread.SetApartmentState(ApartmentState.STA)
            End If

            _Thread.Start()
        Catch ex As Exception
            Throw New Exception("Error en WSServer.Escuchar - " + ex.Message.ToString)
        End Try

    End Sub

    Private Sub EsperarCliente()
        Try
            With _WSInfoCliente
                While True
                    'Cuando se recibe la conexion, guardo la informacion del cliente
                    'Guardo el Socket que utilizo para mantener la conexion con el cliente
                    .Socket = _TcpListener.AcceptSocket() 'Se queda esperando la conexion de un cliente
                    'Guardo el el RemoteEndPoint, que utilizo para identificar al cliente
                    'IDClienteActual = .Socket.RemoteEndPoint
                    'Creo un Thread para que se encargue de escuchar los mensaje del cliente
                    '.Thread = New Thread(AddressOf LeerSocket)
                    'Agrego la informacion del cliente al HashArray Clientes, donde esta la
                    'informacion de todos estos
                    'SyncLock Me
                    'Clientes.Add(IDClienteActual, InfoClienteActual)
                    'End SyncLock
                    'Genero el evento Nueva conexion
                    RaiseEvent NuevaConexion()
                    'Inicio el thread encargado de escuchar los mensajes del cliente
                    '.Thread.Start()
                End While
            End With
        Catch ex As Exception
            Throw New Exception("Error en WSServer.EsperarCliente - " + ex.Message.ToString)
        End Try
    End Sub


    Private Sub WSServer_ConexionTerminada() Handles Me.ConexionTerminada
        _Log.WriteLog("Se termino la conexion con el cliente: ", TraceEventType.Information)
    End Sub

    Private Sub WSServer_DatosRecibidos() Handles Me.DatosRecibidos

    End Sub

    Private Sub WSServer_NuevaConexion() Handles Me.NuevaConexion
        Try
            _Log.WriteLog("Se conecto un nuevo cliente.", TraceEventType.Information)

            _Log.WriteLog("Escucha Iniciada. Esperando Informacion", TraceEventType.Information)
            Dim pEmuladorCronos As New EmuladorCronos(_Sistema, _IPRemoto, _PuertoRemoto)
            pEmuladorCronos.InfoDeUnCliente.Socket = _WSInfoCliente.Socket
            pEmuladorCronos.Start()
        Catch ex As Exception
            Throw New Exception("Error en WSServer_NuevaConexion.NuevaConexion - " + ex.Message.ToString)
        End Try

    End Sub

#End Region

End Class
