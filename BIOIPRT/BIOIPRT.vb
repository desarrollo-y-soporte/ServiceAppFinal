Imports ServiceApp.Interface

Public Class BIOIPRT
    Implements IBackEnd

    Public Property IP As String Implements IBackEnd.IP
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Port As Integer Implements IBackEnd.Port
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Conectado As Boolean Implements IBackEnd.Conectado
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Boolean)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Event FichadaOnlineEvent As IBackEnd.FichadaOnlineEventEventHandler Implements IBackEnd.FichadaOnlineEvent

    Public Sub Lectura() Implements IBackEnd.Lectura
        Throw New NotImplementedException()
    End Sub

    Public Sub CambioFechaHora(pFecha As Date) Implements IBackEnd.CambioFechaHora
        Throw New NotImplementedException()
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

    Public Sub PedidoInicializacion() Implements IBackEnd.PedidoInicializacion
        Throw New NotImplementedException()
    End Sub

    Public Sub Conectar() Implements IBackEnd.Conectar
        Throw New NotImplementedException()
    End Sub

    Public Sub Desconectar() Implements IBackEnd.Desconectar
        Throw New NotImplementedException()
    End Sub

    Public Function CantidadFichadas() As Integer Implements IBackEnd.CantidadFichadas
        Throw New NotImplementedException()
    End Function
End Class
