﻿Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports ServiceApp.WinSocket
Imports ServiceApp.Definiciones
Imports ServiceApp.Logueos
Imports System.Net
Imports ServiceApp.Interface
Imports System.Globalization
Imports System.Windows.Forms
Imports ServiceApp.BIOIPRT

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
    Private _FichadasEnMemoria As Integer
    Private _ModoLecturaFichadas As Boolean
    Private _ModoFinLecturaFichadas As Boolean
    Private _Identificador As Integer
    Private _Form As BIOIPRT.BIOIPRT

#End Region

#Region "Declaracion BackEnd"
    WithEvents _IBackEnd As IBackEnd
#End Region

#Region "Constantes"
    Private ACK As Byte() = {&H6}
    Private NACK As Byte() = {&H5}
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
        _FichadasEnMemoria = 0
        _ModoLecturaFichadas = False
        _ModoFinLecturaFichadas = False
    End Sub

    Public Sub New(ByVal pSistema As BackendEnum, ByVal pIP As String, ByVal pPort As Integer, ByVal pForm As BIOIPRT.BIOIPRT, ByVal pIdentificador As Integer)
        Me.New()
        _Sistema = pSistema
        _IPCliente = pIP
        _PortCliente = pPort
        _Form = pForm
        _Identificador = pIdentificador
        Dim pEmuladorCronosFactory As EmuladorCronosFactory = New EmuladorCronosFactory()
        _IBackEnd = pEmuladorCronosFactory.CreateInstance(_Sistema, pForm)
        _Traductor = New Traductor(CInt(_Sistema))
    End Sub

    Protected Overrides Sub Finalize()
        If _IBackEnd IsNot Nothing Then
            _IBackEnd.Desconectar()
            _IBackEnd = Nothing
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

    Public Property Form As BIOIPRT.BIOIPRT
        Get
            Return _Form
        End Get
        Set(value As BIOIPRT.BIOIPRT)
            _Form = value
        End Set
    End Property

    Public Property Identificador As Integer
        Get
            Return _Identificador
        End Get
        Set(value As Integer)
            _Identificador = value
        End Set
    End Property
#End Region

#Region "Metodos"
    Private Sub LeerSocket()
        Dim Recibir() As Byte 'Array utilizado para recibir los datos que llegan
        Dim Ret As Integer = 0
        Try
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
                                For i = 0 To Ret - 1

                                    If _ModoLecturaFichadas Then
                                        If Recibir(i) = ACK(0) Then
                                            If _ModoLecturaFichadas Then
                                                _Operacion = OperacionesEnum.Lectura
                                                _InicioLectura = True
                                                _FinLectura = True
                                            End If
                                        Else
                                            _ModoLecturaFichadas = False
                                            _InicioLectura = False
                                            _FinLectura = False
                                        End If
                                    End If

                                    If Chr(Recibir(i)) = ChrW(2) And Not _InicioLectura Then
                                        _InicioLectura = True
                                    End If

                                    If Chr(Recibir(i)) = ChrW(82) And _InicioLectura Then
                                        j = i + 2
                                        '_Operacion = OperacionesEnum.CambioFechaHora
                                    End If

                                    If i = j Then
                                        Dim pAccion() As Byte = {Recibir(i)}
                                        If Encoding.ASCII.GetString(pAccion) = "B" Then
                                            _Operacion = 66
                                        Else
                                            _Operacion = Encoding.ASCII.GetString(pAccion)
                                        End If
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
                            _Log.WriteLog("Error en EmuladorCronos.LeerSocket - " + e.Message.ToString, TraceEventType.Information)
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
        Catch ex As Exception
            _Log.WriteLog("Error en EmuladorCronos.LeerSocket - " + ex.Message.ToString, TraceEventType.Information)
        End Try

    End Sub

    Public Sub Start()
        Try
            With _WSInfoCliente
                'Cuando se recibe la conexion, guardo la informacion del cliente
                'Guardo el Socket que utilizo para mantener la conexion con el cliente
                .Thread = New Thread(AddressOf LeerSocket)
                If _Sistema = BackendEnum.Dimmep Then
                    .Thread.SetApartmentState(ApartmentState.STA)
                End If
                'Genero el evento Nueva conexion
                RaiseEvent NuevaConexion()
                'Inicio el thread encargado de escuchar los mensajes del cliente
                .Thread.Start()
                'End While
            End With
        Catch ex As Exception
            Throw New Exception("Error en EmuladorCronos.Start - " + ex.Message.ToString)
        End Try

    End Sub

    Private Sub CerrarThread()
        Try
            _WSInfoCliente.Thread.Abort()
        Catch ex As Exception
            Throw New Exception("Error en EmuladorCronos.CerrarThread - " + ex.Message.ToString)
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
        _WSInfoCliente.Socket.Send(ACK)
    End Sub

    Public Sub EnviarNACK()
        _Log.WriteLog("Enviando NACK: ", TraceEventType.Information)
        _WSInfoCliente.Socket.Send(NACK)
    End Sub

    Public Sub Accion(ByVal pDato As String)
        Try
            Select Case _Operacion
                Case OperacionesEnum.Inicializacion

                    PedidoInicializacion()
                Case OperacionesEnum.Lectura
                    If Not _ModoLecturaFichadas Then
                        LecturaConfirmacion()
                        EnviarLectura()
                    Else
                        EnviarLectura()
                    End If
                Case OperacionesEnum.CambioFechaHora
                    CambioFechaHora(pDato)
                Case OperacionesEnum.Borrado
                    _IBackEnd.Borrado()
                    EnviarACK()
                Case OperacionesEnum.InhabilitacionTotal
                    _IBackEnd.InhabilitacionTotal()
                    EnviarACK()
                Case OperacionesEnum.AltaTarjeta
                    _IBackEnd.AltaTarjeta(pDato)
                    EnviarACK()
                Case OperacionesEnum.BajaTarjeta
                    _IBackEnd.BajaTarjeta(pDato)
                    EnviarACK()
                Case OperacionesEnum.ACK
                    If _ModoLecturaFichadas Then
                        EnviarLectura()
                    End If
                Case OperacionesEnum.NACK
                    If _ModoLecturaFichadas Then
                        _ModoLecturaFichadas = False
                    End If
                Case OperacionesEnum.Ninguna
                    _Log.WriteLog("No se reconocio operacion a ejecutar.", TraceEventType.Information)
            End Select
        Catch ex As Exception
            EnviarNACK()
            Throw New Exception("Error en EmuladorCronos.Accion - " + ex.Message.ToString)
        End Try

    End Sub

    Private Sub CambioFechaHora(ByVal pDato As String)
        Try
            Dim pFecha As New Date(2000 + CInt(pDato.Substring(0, 2)), CInt(pDato.Substring(2, 2)), CInt(pDato.Substring(4, 2)), CInt(pDato.Substring(7, 2)), CInt(pDato.Substring(9, 2)), 0)

            _IBackEnd.CambioFechaHora(pFecha)

            EnviarACK()
        Catch ex As Exception
            Throw New Exception("Error en EmuladorCronos.CambioFechaHora - " + ex.Message.ToString)
            EnviarNACK()
        End Try
    End Sub

    Private Sub PedidoInicializacion()
        Dim pMensaje(17) As Byte

        Dim pCantidad As Integer = _IBackEnd.CantidadFichadas()
        Dim pFecha As Date = _IBackEnd.FechaHoraUltInicializacion()

        pMensaje(0) = 2
        Array.Copy(Encoding.ASCII.GetBytes(pFecha.ToString("yyMMddHHmm")), 0, pMensaje, 1, pFecha.ToString("yyMMddHHmm").Length)
        Array.Copy(Encoding.ASCII.GetBytes(pCantidad.ToString("00000")), 0, pMensaje, 11, pFecha.ToString("00000").Length)
        pMensaje(16) = 3
        pMensaje(17) = CalcularXOR(pMensaje, 17)
        _WSInfoCliente.Socket.Send(pMensaje)
    End Sub

    Private Sub LecturaConfirmacion()
        Try
            If _IBackEnd.PrepararLectura Then
                EnviarACK()
                _ModoLecturaFichadas = True
            Else
                EnviarNACK()
            End If
        Catch ex As Exception
            Throw New Exception("Error en EmuladorCronos.LecturaConfirmacion - " + ex.Message.ToString)
            EnviarNACK()
        End Try
    End Sub

    Private Sub EnviarLectura()
        Dim pMensaje(22) As Byte
        Dim pListaFichadas As String = _IBackEnd.Lectura()

        If pListaFichadas <> "" Then
            Dim pSplit() As String = pListaFichadas.Split(",")
            Dim pDiaHora As New Date(CInt(pSplit(0).Substring(0, 4)), CInt(pSplit(0).Substring(4, 2)), CInt(pSplit(0).Substring(6, 2)), CInt(pSplit(0).Substring(8, 2)), CInt(pSplit(0).Substring(10, 2)), 0)
            'Dim pDiaHora As Date = DateTime.ParseExact(pSplit(0), "yyyyMMddHHmmss", CultureInfo.InvariantCulture)

            _IBackEnd_FichadaOnlineEvent(pDiaHora, pSplit(1), IIf(_Sistema = BackendEnum.ZKTeco, "E", pSplit(2)))

            _FichadasEnMemoria = _FichadasEnMemoria + 1
        Else
            _ModoFinLecturaFichadas = False
        End If

    End Sub

    Public Function CalcularXOR(ByVal pMensaje As Byte(), ByVal pLargo As Integer) As Integer
        Dim pResultado As Integer

        pResultado = pMensaje(0)
        For i As Integer = 0 To pLargo - 1
            pResultado = pResultado Xor pMensaje(i)
        Next

        Return pResultado
    End Function
#End Region

#Region "Ejecucion Eventos"
    Private Sub EmuladorCronos_NuevaConexion() Handles Me.NuevaConexion
        Try
            _ModoLecturaFichadas = False
            _IBackEnd.IP = _IPCliente
            _IBackEnd.Port = _PortCliente
            _IBackEnd.Identificador = _Identificador
            If _IBackEnd.Conectado Then
                _IBackEnd.Desconectar()
            End If

            _IBackEnd.Conectar()
        Catch ex As Exception
            _IBackEnd = Nothing
            Throw New Exception("Error en EmuladorCronos_NuevaConexion.NuevaConexion - " + ex.Message.ToString)
        End Try
    End Sub

    Private Sub EmuladorCronos_ConexionTerminada() Handles Me.ConexionTerminada
        If _IBackEnd IsNot Nothing Then
            _IBackEnd.Desconectar()
            _IBackEnd = Nothing
        End If
    End Sub

    Private Sub EmuladorCronos_DatosRecibidos() Handles Me.DatosRecibidos
        Dim pMessage As String = _WSInfoCliente.UltimosDatosRecibidos
        Dim pInfo As String = ""

        Try
            If Not _ModoLecturaFichadas Then
                pInfo = pMessage
                pInfo = pInfo.Replace(ChrW(2), "")
                pInfo = pInfo.Replace(ChrW(3), "")
                pInfo = pInfo.Replace(vbNullChar, "")
                pInfo = pInfo.Trim()
                pInfo = pInfo.Substring(3)
            End If
            _Log.WriteLog("Ultimos datos recibidos: " + pMessage.ToString, TraceEventType.Information)

            Accion(pInfo)

            'EnviarACK()
        Catch ex As Exception
            Throw New Exception("Error en EmuladorCronos_DatosRecibidos.DatosRecibidos - " + ex.Message.ToString)
        End Try

    End Sub

    Private Sub _IBackEnd_FichadaOnlineEvent(pFecha As Date, pId As String, ByVal pTpMovimiento As String) Handles _IBackEnd.FichadaOnlineEvent
        Dim pMensaje(22) As Byte

        pMensaje(0) = 2
        pMensaje(1) = 49
        'pasar parametro del IBackEnd
        ' zkteco siempre viene E
        Select Case pTpMovimiento
            Case "E"
                pMensaje(2) = 48
            Case "S"
                pMensaje(2) = 49
        End Select

        ' E seria pMensaje(2) = 48
        ' S seria pMensaje(2) = 49
        ' pMensaje(2) = 30


        Array.Copy(Encoding.ASCII.GetBytes(pFecha.ToString("MMddHHmm")), 0, pMensaje, 3, pFecha.ToString("MMddHHmm").Length)
        pId = _Traductor.Buscar(pId)
        Array.Copy(Encoding.ASCII.GetBytes(String.Format("{0000000000}", pId)), 0, pMensaje, 11, String.Format("{0000000000}", pId).Length)
        'Array.Copy(Encoding.ASCII.GetBytes(pCantidad.ToString("00000")), 0, pMensaje, 11, pFecha.ToString("00000").Length)

        pMensaje(21) = 3
        pMensaje(22) = CalcularXOR(pMensaje, 22)
        _WSInfoCliente.Socket.Send(pMensaje)
    End Sub

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
                If _WSInfoCliente IsNot Nothing Then
                    _WSInfoCliente.Dispose()
                    _WSInfoCliente = Nothing
                End If

                '_Log
                If _Log IsNot Nothing Then
                    _Log.Dispose()
                    _Log = Nothing
                End If
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
