Imports System
Imports System.Threading
Imports System.Net.Sockets
Imports System.IO
Imports System.Text
Imports zkemkeeper
Imports ServiceApp.Definiciones

Public Class WSServer

#Region "ESTRUCTURAS"
    Private Structure InfoDeUnCliente
        'Esta estructura permite guardar la información sobre un cliente
        Public Socket As Socket 'Socket utilizado para mantener la conexion con el cliente
        Public Thread As Thread 'Thread utilizado para escuchar al cliente
        Public UltimosDatosRecibidos As String 'Ultimos datos enviados por el cliente
    End Structure
#End Region

#Region "VARIABLES"
    'Private _ZKTeco As CZKEMClass
    Private _TcpListener As TcpListener
    Private Clientes As New Hashtable() 'Aqui se guarda la informacion de todos los clientes conectados
    Private _Thread As Thread
    Private IDClienteActual As Net.IPEndPoint 'Ultimo cliente conectado
    Private _Port As String
    Private _Equipo As BackendEnum
#End Region

#Region "EVENTOS"
    Public Event NuevaConexion(ByVal IDTerminal As Net.IPEndPoint)
    Public Event DatosRecibidos(ByVal IDTerminal As Net.IPEndPoint)
    Public Event ConexionTerminada(ByVal IDTerminal As Net.IPEndPoint)
#End Region

#Region "PROPIEDADES"
    Property Port() As String
        Get
            Port = _Port
        End Get
        Set(ByVal Value As String)
            _Port = Value
        End Set
    End Property

    Public Property Equipo As BackendEnum
        Get
            Return _Equipo
        End Get
        Set(value As BackendEnum)
            _Equipo = value
        End Set
    End Property
#End Region

#Region "METODOS"
    Public Sub Escuchar()
        _TcpListener = New TcpListener(_Port)
        'Inicio la escucha
        _TcpListener.Start()
        'Creo un thread para que se quede escuchando la llegada de un cliente
        _Thread = New Thread(AddressOf EsperarCliente)
        _Thread.Start()
    End Sub

    Public Function ObtenerDatos(ByVal IDCliente As Net.IPEndPoint) As String
        Dim InfoClienteSolicitado As InfoDeUnCliente

        'Obtengo la informacion del cliente solicitado
        InfoClienteSolicitado = Clientes(IDCliente)
        ObtenerDatos = InfoClienteSolicitado.UltimosDatosRecibidos
    End Function

    Public Sub Cerrar(ByVal IDCliente As Net.IPEndPoint)
        Dim InfoClienteActual As InfoDeUnCliente

        'Obtengo la informacion del cliente solicitado
        InfoClienteActual = Clientes(IDCliente)
        'Cierro la conexion con el cliente
        InfoClienteActual.Socket.Close()
    End Sub

    Public Sub Cerrar()
        Dim InfoClienteActual As InfoDeUnCliente

        'Recorro todos los clientes y voy cerrando las conexiones
        For Each InfoClienteActual In Clientes.Values
            Call Cerrar(InfoClienteActual.Socket.RemoteEndPoint)
        Next
    End Sub

    Public Sub EnviarDatos(ByVal IDCliente As Net.IPEndPoint, ByVal Datos As String)
        Dim Cliente As InfoDeUnCliente

        'Obtengo la informacion del cliente al que se le quiere enviar el mensaje
        Cliente = Clientes(IDCliente)
        'Le envio el mensaje
        Cliente.Socket.Send(Encoding.ASCII.GetBytes(Datos))
    End Sub

    Public Sub EnviarDatos(ByVal Datos As String)
        Dim Cliente As InfoDeUnCliente

        'Recorro todos los clientes conectados, y les envio el mensaje recibido
        'en el parametro Datos
        For Each Cliente In Clientes.Values
            EnviarDatos(Cliente.Socket.RemoteEndPoint, Datos)
        Next
    End Sub

#End Region

#Region "FUNCIONES PRIVADAS"
    Private Sub EsperarCliente()
        Dim InfoClienteActual As InfoDeUnCliente

        With InfoClienteActual
            While True
                'Cuando se recibe la conexion, guardo la informacion del cliente
                'Guardo el Socket que utilizo para mantener la conexion con el cliente
                .Socket = _TcpListener.AcceptSocket() 'Se queda esperando la conexion de un cliente
                'Guardo el el RemoteEndPoint, que utilizo para identificar al cliente
                IDClienteActual = .Socket.RemoteEndPoint
                'Creo un Thread para que se encargue de escuchar los mensaje del cliente
                .Thread = New Thread(AddressOf LeerSocket)
                'Agrego la informacion del cliente al HashArray Clientes, donde esta la
                'informacion de todos estos
                SyncLock Me
                    Clientes.Add(IDClienteActual, InfoClienteActual)
                End SyncLock
                'Genero el evento Nueva conexion
                RaiseEvent NuevaConexion(IDClienteActual)
                'Inicio el thread encargado de escuchar los mensajes del cliente
                .Thread.Start()
            End While
        End With
    End Sub

    Private Sub LeerSocket()
        Dim IDReal As Net.IPEndPoint 'ID del cliente que se va a escuchar
        Dim Recibir() As Byte 'Array utilizado para recibir los datos que llegan
        Dim InfoClienteActual As InfoDeUnCliente 'Informacion del cliente que se va escuchar
        Dim Ret As Integer = 0

        IDReal = IDClienteActual
        InfoClienteActual = Clientes(IDReal)

        With InfoClienteActual
            While True
                If .Socket.Connected Then
                    Recibir = New Byte(100) {}
                    Try
                        'Me quedo esperando a que llegue un mensaje desde el cliente
                        Ret = .Socket.Receive(Recibir, Recibir.Length, SocketFlags.None)

                        If Ret > 0 Then
                            'Guardo el mensaje recibido
                            .UltimosDatosRecibidos = Encoding.ASCII.GetString(Recibir)
                            Clientes(IDReal) = InfoClienteActual
                            'Genero el evento de la recepcion del mensaje
                            RaiseEvent DatosRecibidos(IDReal)
                        Else
                            'Genero el evento de la finalizacion de la conexion
                            RaiseEvent ConexionTerminada(IDReal)
                            Exit While
                        End If
                    Catch e As Exception
                        If Not .Socket.Connected Then
                            'Genero el evento de la finalizacion de la conexion
                            RaiseEvent ConexionTerminada(IDReal)
                            Exit While
                        End If
                    End Try
                End If
            End While
            Call CerrarThread(IDReal)
        End With
    End Sub

    Private Sub CerrarThread(ByVal IDCliente As Net.IPEndPoint)
        Dim InfoClienteActual As InfoDeUnCliente

        'Cierro el thread que se encargaba de escuchar al cliente especificado
        InfoClienteActual = Clientes(IDCliente)
        Try
            InfoClienteActual.Thread.Abort()
        Catch e As Exception
            SyncLock Me
                'Elimino el cliente del HashArray que guarda la informacion de los clientes
                Clientes.Remove(IDCliente)
            End SyncLock
        End Try
    End Sub
#End Region

End Class
