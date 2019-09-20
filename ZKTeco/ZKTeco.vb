Imports ServiceApp.Interface
Imports zkemkeeper

Public Class ZKTeco
    Implements IBackEnd

    WithEvents axCZKEM1 As New CZKEMClass

    Private _IsConnected As Boolean
    Public Event FichadaOnlineEvent As IBackEnd.FichadaOnlineEventEventHandler Implements IBackEnd.FichadaOnlineEvent

    Public Property IsConnected As Boolean
        Get
            Return _IsConnected
        End Get
        Set(value As Boolean)
            _IsConnected = value
        End Set
    End Property

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

    Public Function Connect(ByVal pIP As String, ByVal pPort As Integer, ByVal pCommKey As Integer) As Integer

        If pIP = vbNullString Or pPort = 0 Or pCommKey = vbNullString Then
            Return -1
        End If

        If (pPort <= 0) Or pPort > 65535 Then
            Return -1
        End If

        If (pCommKey < 0) Or (pCommKey > 999999) Then
            Return -1
        End If

        Dim pErrorCode As Integer = 0

        axCZKEM1.SetCommPassword(pCommKey)

        If _IsConnected Then
            axCZKEM1.Disconnect()
            _IsConnected = False
            Return -2
        End If

        If axCZKEM1.Connect_Net(pIP, pPort) Then
            _IsConnected = True
            Return 1
        End If

        Return pErrorCode
    End Function

    Public Sub Disconnect()
        If _IsConnected Then
            axCZKEM1.Disconnect()
        End If
    End Sub

    Private Sub axCZKEM1_OnFinger() Handles axCZKEM1.OnFinger

    End Sub

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

    Public Function CantidadFichadas() As Integer Implements IBackEnd.CantidadFichadas
        Throw New NotImplementedException()
    End Function

    Public Sub Conectar() Implements IBackEnd.Conectar
        Throw New NotImplementedException()
    End Sub

    Public Sub Desconectar() Implements IBackEnd.Desconectar
        Throw New NotImplementedException()
    End Sub
End Class
