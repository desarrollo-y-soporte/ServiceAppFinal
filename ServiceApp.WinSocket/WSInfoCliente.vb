Imports System.Net.Sockets
Imports System.Threading

Public Class WSInfoCliente
    'Esta estructura permite guardar la información sobre un cliente
    Private _Socket As Socket 'Socket utilizado para mantener la conexion con el cliente
    Private _Thread As Thread 'Thread utilizado para escuchar al cliente
    Private _UltimosDatosRecibidos As String 'Ultimos datos enviados por el cliente

    Public Sub New()

    End Sub

    Public Property Socket As Socket
        Get
            Return _Socket
        End Get
        Set(value As Socket)
            _Socket = value
        End Set
    End Property

    Public Property Thread As Thread
        Get
            Return _Thread
        End Get
        Set(value As Thread)
            _Thread = value
        End Set
    End Property

    Public Property UltimosDatosRecibidos As String
        Get
            Return _UltimosDatosRecibidos
        End Get
        Set(value As String)
            _UltimosDatosRecibidos = value
        End Set
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
