
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
    Private _ListaFichadas As New List(Of String)
    Private _SiguienteFichada As Integer

    Public Sub New()
        MyBase.New()
        _Reloj = 1
        _IP = ""
        _Port = -1
        _IdProcess = -1
        _IsConnected = False
        _ListaFichadas.Clear()
        _SiguienteFichada = 0
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
        _ListaFichadas.Clear()
        _SiguienteFichada = 0
        AddHandler _Form.EnviarLectura, AddressOf LecutraHandler
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

    Public Property Form As BIOIPRT.BIOIPRT
        Get
            Return _Form
        End Get
        Set(value As BIOIPRT.BIOIPRT)
            _Form = value
        End Set
    End Property

    Public Property Identificador As Integer Implements IBackEnd.Identificador
        Get
            Return _Reloj
        End Get
        Set(value As Integer)
            _Reloj = value
            _Form.Identificador = _Reloj
        End Set
    End Property

    Public Event FichadaOnlineEvent As IBackEnd.FichadaOnlineEventEventHandler Implements IBackEnd.FichadaOnlineEvent

    Public Sub CambioFechaHora(pFecha As Date) Implements IBackEnd.CambioFechaHora
        If _Form.IsConnected Then
            _Form.CambioFechaHora(pFecha)
        End If
    End Sub

    Public Sub Borrado() Implements IBackEnd.Borrado
        If _Form.IsConnected Then
            _Form.Borrado()
        End If
    End Sub

    Public Sub InhabilitacionTotal() Implements IBackEnd.InhabilitacionTotal
        If _Form.IsConnected Then
            _Form.InhabilitacionTotal()
        End If
    End Sub

    Public Sub AltaTarjeta(pId As String) Implements IBackEnd.AltaTarjeta
        If _Form.IsConnected Then
            _Form.AltaTarjeta(pId)
        End If
    End Sub

    Public Sub BajaTarjeta(pId As String) Implements IBackEnd.BajaTarjeta
        If _Form.IsConnected Then
            _Form.BajaTarjeta(pId)
        End If
    End Sub

    Public Sub Conectar() Implements IBackEnd.Conectar
        Preparar()
        _Form.Conectar()
    End Sub

    Public Sub Desconectar() Implements IBackEnd.Desconectar
        If _Form.IsConnected Then
            '_Form.Desconectar()
            _Form = Nothing
        End If
    End Sub

    Public Function CantidadFichadas() As Integer Implements IBackEnd.CantidadFichadas
        Return _Form.CantidadFichadas
    End Function

    Public Function FechaHoraUltInicializacion() As Date Implements IBackEnd.FechaHoraUltInicializacion
        Return _Form.FechaHoraUltInicializacion
    End Function

    Public Function Lectura() As String Implements IBackEnd.Lectura
        Dim pFichadas As String
        Dim pSplit() As String
        Dim sFechaCompleta As String

        pFichadas = ""
        If _Form.IsConnected Then
            If _Form.Linea <> "" Then
                If _SiguienteFichada < _ListaFichadas.Count - 1 Then
                    pSplit = _ListaFichadas(_SiguienteFichada).Split(",")
                    sFechaCompleta = Date.Now.Year.ToString + pSplit(1).Substring(2, 2) + pSplit(1).Substring(0, 2) + pSplit(2).ToString
                    pFichadas = sFechaCompleta + ", " + pSplit(0).ToString.Substring(6, 10) + "," + Split(3).ToString.Substring(0, 1).ToUpper

                    _SiguienteFichada = _SiguienteFichada + 1
                End If
            End If
        End If

        Return pFichadas

    End Function

    Sub LecutraHandler()
        _ListaFichadas.Add(_Form.Linea)
    End Sub

    Public Function PrepararLectura() As Boolean Implements IBackEnd.PrepararLectura
        If _Form.IsConnected Then
            _Form.IniciarLecutar()
        End If
        Return True
    End Function

    Public Sub Preparar()
        _Form.Identificador = _Reloj
    End Sub

    Public Sub Status()
        _Form.Status()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

End Class
