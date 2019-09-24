Imports ServiceApp.Interface
Imports zkemkeeper
Imports ServiceApp.Logueos

Public Class ZKTeco
    Implements IBackEnd

    WithEvents axCZKEM1 As New CZKEMClass

    Private _IsConnected As Boolean
    Private _IP As String
    Private _Port As Integer
    Private _CommKey As Integer
    Private _Log As New Log
    Private idwErrorCode As Integer

    Public Sub New()
        _CommKey = 0
    End Sub

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
            Return _IP
        End Get
        Set(value As String)
            _IP = value
        End Set
    End Property

    Public Property Port As Integer Implements IBackEnd.Port
        Get
            Return _Port
        End Get
        Set(value As Integer)
            _Port = value
        End Set
    End Property

    Public Property Conectado As Boolean Implements IBackEnd.Conectado
        Get
            Return _IsConnected
        End Get
        Set(value As Boolean)
            _IsConnected = value
        End Set
    End Property

    Public Function Connect(ByVal pIP As String, ByVal pPort As Integer, ByVal pCommKey As Integer) As Integer

        If pIP = vbNullString Or pPort = 0 Then
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
            'axCZKEM1.MachineNumber = 1
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
        If _IsConnected Then

        End If
    End Sub

    Public Sub CambioFechaHora(pFecha As Date) Implements IBackEnd.CambioFechaHora
        If _IsConnected Then
            Dim idwYear As Integer = Convert.ToInt32(pFecha.Year.ToString())
            Dim idwMonth As Integer = Convert.ToInt32(pFecha.Month.ToString())
            Dim idwDay As Integer = Convert.ToInt32(pFecha.Day.ToString())
            Dim idwHour As Integer = Convert.ToInt32(pFecha.Hour.ToString())
            Dim idwMinute As Integer = Convert.ToInt32(pFecha.Minute.ToString())
            Dim idwSecond As Integer = Convert.ToInt32(pFecha.Second.ToString())
            If (axCZKEM1.SetDeviceTime2(axCZKEM1.MachineNumber, idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond)) Then
                axCZKEM1.RefreshData(axCZKEM1.MachineNumber)
            Else
                axCZKEM1.GetLastError(idwErrorCode)
                Throw New Exception("Error" + idwErrorCode.ToString)
            End If

        End If

    End Sub

    Public Sub Borrado() Implements IBackEnd.Borrado
        If _IsConnected Then

        End If
    End Sub

    Public Sub InhabilitacionTotal() Implements IBackEnd.InhabilitacionTotal
        If _IsConnected Then

        End If
    End Sub

    Public Sub AltaTarjeta(pId As String) Implements IBackEnd.AltaTarjeta
        If _IsConnected Then
            'Dim idwErrorCode As Integer = Nothing
            'Dim sdwEnrollNumber As String = Nothing
            'Dim sName As String = Nothing
            'Dim sPassword As String = Nothing
            'Dim iPrivilege As Integer = Nothing
            'Dim bEnabled As Boolean = Nothing

            'axCZKEM1.EnableDevice(axCZKEM1.MachineNumber, False)
            'axCZKEM1.SetStrCardNumber(pId)

            'If axCZKEM1.SSR_SetUserInfo(axCZKEM1.MachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled) Then
            '    'lblOutputInfo.Items.Add("Set user information successfully")
            '    _Log.WriteLog("Informacion enviada con exito", TraceEventType.Information)
            'Else

            '    axCZKEM1.GetLastError(idwErrorCode)
            '    'lblOutputInfo.Items.Add("*Operation failed,ErrorCode=" & idwErrorCode.ToString())
            '    _Log.WriteLog("*Operacion Erronea,ErrorCode=" & idwErrorCode.ToString(), TraceEventType.Information)
            'End If

            'axCZKEM1.RefreshData(axCZKEM1.MachineNumber)
            'axCZKEM1.EnableDevice(axCZKEM1.MachineNumber, True)
        End If
    End Sub

    Public Sub BajaTarjeta(pId As String) Implements IBackEnd.BajaTarjeta
        If _IsConnected Then

        End If
    End Sub

    Public Function CantidadFichadas() As Integer Implements IBackEnd.CantidadFichadas
        Dim pCantidad As Integer = 0
        If _IsConnected Then

            axCZKEM1.GetDeviceStatus(axCZKEM1.MachineNumber, 5, pCantidad)
            Return pCantidad
        End If
        Return pCantidad
    End Function

    Public Sub Conectar() Implements IBackEnd.Conectar
        Connect(_IP, _Port, _CommKey)
    End Sub

    Public Sub Desconectar() Implements IBackEnd.Desconectar
        Desconectar()
    End Sub

    Public Function FechaHoraUltInicializacion() As Date Implements IBackEnd.FechaHoraUltInicializacion
        If _IsConnected Then
            Return Date.Now
        End If
        Return Date.Now
    End Function
End Class
