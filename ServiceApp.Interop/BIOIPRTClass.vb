
Imports ServiceApp.BIOIPRT
Imports ServiceApp.Interface
Imports ServiceApp.Logueos

Public Class BIOIPRTClass
    Implements IBackEnd

    Private _IP As String
    Private _Port As Integer
    Private _IsConnected As Boolean
    Private _Reloj As Integer
    Private _IdProcess As Integer
    Private sPath As String
    Private _Form As BIOIPRT.BIOIPRT

    Public Sub New()
        MyBase.New()
        _Reloj = 1
        _IP = ""
        _Port = -1
        _IdProcess = -1
        _IsConnected = False
        'StartProcess()
    End Sub

    Public Sub New(ByVal pForm As BIOIPRT.BIOIPRT)
        MyBase.New()
        _Reloj = 1
        _IP = ""
        _Port = -1
        _IdProcess = -1
        _IsConnected = False
        _Form = pForm
        'StartProcess()
    End Sub

    Public Property IP As String Implements IBackEnd.IP
        Get
            Return _IP
        End Get
        Set(value As String)
            _IP = value
            _Form.IP = _IP
        End Set
    End Property

    Public Property Port As Integer Implements IBackEnd.Port
        Get
            Return _Port
        End Get
        Set(value As Integer)
            _Port = value
            _Form.Port = _Port
        End Set
    End Property

    Public Property Conectado As Boolean Implements IBackEnd.Conectado
        Get
            Return _IsConnected
        End Get
        Set(value As Boolean)
            _IsConnected = value
            _Form.IsConnected = _IsConnected
        End Set
    End Property

    Public Property Reloj As Integer
        Get
            Return _Reloj
        End Get
        Set(value As Integer)
            _Reloj = value
            _Form.Reloj = _Reloj
        End Set
    End Property

    Public Property Form As BIOIPRT.BIOIPRT
        Get
            Return _Form
        End Get
        Set(value As BIOIPRT.BIOIPRT)
            _Form = value
        End Set
    End Property

    Public Event FichadaOnlineEvent As IBackEnd.FichadaOnlineEventEventHandler Implements IBackEnd.FichadaOnlineEvent

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

    Public Sub Conectar() Implements IBackEnd.Conectar
        Preparar()
        _Form.Conectar()
    End Sub

    Public Sub Desconectar() Implements IBackEnd.Desconectar
    End Sub

    Public Function CantidadFichadas() As Integer Implements IBackEnd.CantidadFichadas
        Return _Form.CantidadFichadas
    End Function

    Public Function FechaHoraUltInicializacion() As Date Implements IBackEnd.FechaHoraUltInicializacion
        Return _Form.FechaHoraUltInicializacion
    End Function

    Public Function Lectura() As String Implements IBackEnd.Lectura
        Throw New NotImplementedException()
    End Function

    Public Function PrepararLectura() As Boolean Implements IBackEnd.PrepararLectura
        Throw New NotImplementedException()
    End Function

    Public Sub Preparar()
        _Form.Reloj = _Reloj

    End Sub

    Public Sub Status()
        _Form.Status()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

End Class
