Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.IO

Public Class WSCliente

#Region "VARIABLES"
    Private _Stream As Stream 'Utilizado para enviar datos al Servidor y recibir datos del mismo
    Private _IP As String 'Direccion del objeto de la clase Servidor
    Private _Port As Integer 'Puerto donde escucha el objeto de la clase Servidor
#End Region

#Region "EVENTOS"
    Public Event ConexionTerminada()
    Public Event DatosRecibidos(ByVal datos As String)
#End Region

#Region "PROPIEDADES"
    Public Property Port() As Integer
        Get
            Port = _Port
        End Get
        Set(ByVal Value As Integer)
            _Port = Value
        End Set
    End Property

    Public Property IP() As String
        Get
            IP = _IP
        End Get
        Set(ByVal Value As String)
            _IP = Value
        End Set
    End Property
#End Region

#Region "METODOS"
    Public Sub Conectar()
        Dim pTcpClient As TcpClient
        Dim pThread As Thread 'Se encarga de escuchar mensajes enviados por el Servidor

        pTcpClient = New TcpClient()
        'Me conecto al objeto de la clase Servidor,
        '  determinado por las propiedades IPDelHost y PuertoDelHost
        pTcpClient.Connect(_IP, _Port)
        _Stream = pTcpClient.GetStream()
        'Creo e inicio un thread para que escuche los mensajes enviados por el Servidor
        pThread = New Thread(AddressOf LeerSocket)
        pThread.Start()
    End Sub

    Public Sub EnviarDatos(ByVal Datos As String)
        Dim BufferDeEscritura() As Byte

        BufferDeEscritura = Encoding.ASCII.GetBytes(Datos)
        If Not (_Stream Is Nothing) Then
            'Envio los datos al Servidor
            _Stream.Write(BufferDeEscritura, 0, BufferDeEscritura.Length)
        End If
    End Sub
#End Region

#Region "FUNCIONES PRIVADAS"
    Private Sub LeerSocket()
        Dim BufferDeLectura() As Byte
        While True
            Try
                BufferDeLectura = New Byte(100) {}
                'Me quedo esperando a que llegue algun mensaje
                _Stream.Read(BufferDeLectura, 0, BufferDeLectura.Length)
                'Genero el evento DatosRecibidos, ya que se han recibido datos desde el Servidor
                RaiseEvent DatosRecibidos(Encoding.ASCII.GetString(BufferDeLectura))
            Catch e As Exception
                Exit While
            End Try
        End While
        'Finalizo la conexion, por lo tanto genero el evento correspondiente
        RaiseEvent ConexionTerminada()
    End Sub
#End Region
End Class
