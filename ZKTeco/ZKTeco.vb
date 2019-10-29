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
            DesregistrarEventosOnLine()
            axCZKEM1.Disconnect()
            _IsConnected = False
            Return -2
        End If

        If axCZKEM1.Connect_Net(pIP, pPort) Then
            'axCZKEM1.MachineNumber = 1
            RegistrarEventosOnLine()
            _IsConnected = True
            Return 1
        End If

        Return pErrorCode
    End Function

    Public Sub Disconnect()
        If _IsConnected Then
            RegistrarEventosOnLine()
            axCZKEM1.Disconnect()
            _IsConnected = False
        End If
    End Sub

    Public Sub CambioFechaHora(pFecha As Date) Implements IBackEnd.CambioFechaHora
        If _IsConnected Then
            Dim idwYear As Integer = pFecha.Year
            Dim idwMonth As Integer = pFecha.Month
            Dim idwDay As Integer = pFecha.Day
            Dim idwHour As Integer = pFecha.Hour
            Dim idwMinute As Integer = pFecha.Minute
            Dim idwSecond As Integer = pFecha.Second

            If (axCZKEM1.SetDeviceTime2(axCZKEM1.MachineNumber, idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond)) Then
                axCZKEM1.RefreshData(axCZKEM1.MachineNumber)
                axCZKEM1.GetDeviceTime(axCZKEM1.MachineNumber, idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond)
                Debug.Print("stop")
            Else
                axCZKEM1.GetLastError(idwErrorCode)
                Throw New Exception("Error" + idwErrorCode.ToString)
            End If

        End If

    End Sub

    Public Sub Borrado() Implements IBackEnd.Borrado
        If _IsConnected Then
            axCZKEM1.ClearData(axCZKEM1.MachineNumber, 1)
        End If
    End Sub

    Public Sub InhabilitacionTotal() Implements IBackEnd.InhabilitacionTotal
        If _IsConnected Then
            axCZKEM1.EnableDevice(axCZKEM1.MachineNumber, False)
        End If
    End Sub

    Public Sub AltaTarjeta(pId As String) Implements IBackEnd.AltaTarjeta
        If _IsConnected Then
            Dim idwErrorCode As Integer = Nothing
            Dim sdwEnrollNumber As String = Nothing
            Dim sName As String = Nothing
            Dim sPassword As String = Nothing
            Dim iPrivilege As Integer = Nothing
            Dim bEnabled As Boolean = Nothing

            axCZKEM1.EnableDevice(axCZKEM1.MachineNumber, False)
            axCZKEM1.SetStrCardNumber(pId)

            If axCZKEM1.SSR_SetUserInfo(axCZKEM1.MachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled) Then
                'lblOutputInfo.Items.Add("Set user information successfully")
                _Log.WriteLog("Informacion enviada con exito", TraceEventType.Information)
            Else

                axCZKEM1.GetLastError(idwErrorCode)
                'lblOutputInfo.Items.Add("*Operation failed,ErrorCode=" & idwErrorCode.ToString())
                _Log.WriteLog("*Operacion Erronea,ErrorCode=" & idwErrorCode.ToString(), TraceEventType.Information)
            End If

            axCZKEM1.RefreshData(axCZKEM1.MachineNumber)
            axCZKEM1.EnableDevice(axCZKEM1.MachineNumber, True)
        End If
    End Sub

    Public Sub BajaTarjeta(pId As String) Implements IBackEnd.BajaTarjeta
        If _IsConnected Then

        End If
    End Sub

    Public Function CantidadFichadas() As Integer Implements IBackEnd.CantidadFichadas
        Dim pCantidad As Integer = 0
        'Agregar contador de fichadas
        Disconnect()
        Conectar()
        axCZKEM1.GetDeviceStatus(axCZKEM1.MachineNumber, 6, pCantidad)

        Return pCantidad
    End Function

    Public Sub Conectar() Implements IBackEnd.Conectar
        Connect(_IP, _Port, _CommKey)
    End Sub

    Public Sub Desconectar() Implements IBackEnd.Desconectar
        Disconnect()
    End Sub

    Public Function FechaHoraUltInicializacion() As Date Implements IBackEnd.FechaHoraUltInicializacion
        If _IsConnected Then
            Return Date.Now
        End If
        Return Date.Now
    End Function

    Public Function Lectura() As String Implements IBackEnd.Lectura
        Dim dwTMachineNumber As Integer = 0
        Dim dwEMachineNumber As Integer = 0
        Dim dwEnrollNumber As Integer = 0
        Dim idwVerifyMode As Integer = 0
        Dim idwInOutMode As Integer = 0
        Dim idwYear As Integer = 0
        Dim idwMonth As Integer = 0
        Dim idwDay As Integer = 0
        Dim idwHour As Integer = 0
        Dim idwMinute As Integer = 0
        Dim idwSecond As Integer = 0
        Dim idwWorkcode As Integer = 0
        Dim sFechaCompleta As String
        Dim pFichadas As String
        pFichadas = ""

        If _IsConnected Then
            'axCZKEM1.EnableDevice(axCZKEM1.MachineNumber, False)
            'Dim dt_log As DataTable

            'If axCZKEM1.GetGeneralLogData(axCZKEM1.MachineNumber, dwTMachineNumber, dwEnrollNumber, dwEMachineNumber, idwVerifyMode, idwInOutMode, idwYear, idwMonth, idwDay, idwHour, idwMinute) Then
            If axCZKEM1.SSR_GetGeneralLogData(axCZKEM1.MachineNumber, dwEnrollNumber, idwVerifyMode, idwInOutMode, idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond, idwWorkcode) Then
                'Dim dr As DataRow = dt_log.NewRow()
                'sdwEnrollNumber
                sFechaCompleta = idwYear.ToString + idwMonth.ToString("00") + idwDay.ToString("00") + idwHour.ToString("00") + idwMinute.ToString("00") + idwSecond.ToString("00")
                'dr("Verify Type") = idwVerifyMode
                'dr("Verify State") = idwInOutMode
                'dr("WorkCode") = idwWorkcode
                pFichadas = sFechaCompleta + ", " + dwEnrollNumber.ToString
            Else
                pFichadas = ""
            End If
            'axCZKEM1.EnableDevice(axCZKEM1.MachineNumber, True)
        End If
        Return pFichadas
    End Function

    Private Sub axCZKEM1_OnVerify(UserID As Integer) Handles axCZKEM1.OnVerify


    End Sub

    Private Sub RegistrarEventosOnLine()
        If axCZKEM1.RegEvent(axCZKEM1.MachineNumber, 65535) Then

            'common interface
            AddHandler axCZKEM1.OnFinger, New zkemkeeper._IZKEMEvents_OnFingerEventHandler(AddressOf axCZKEM1_OnFinger)
            AddHandler axCZKEM1.OnVerify, New zkemkeeper._IZKEMEvents_OnVerifyEventHandler(AddressOf axCZKEM1_OnVerify)
            AddHandler axCZKEM1.OnFingerFeature, New zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(AddressOf axCZKEM1_OnFingerFeature)
            AddHandler axCZKEM1.OnDeleteTemplate, New zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(AddressOf axCZKEM1_OnDeleteTemplate)
            AddHandler axCZKEM1.OnNewUser, New zkemkeeper._IZKEMEvents_OnNewUserEventHandler(AddressOf axCZKEM1_OnNewUser)
            AddHandler axCZKEM1.OnHIDNum, New zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(AddressOf axCZKEM1_OnHIDNum)
            AddHandler axCZKEM1.OnAlarm, New zkemkeeper._IZKEMEvents_OnAlarmEventHandler(AddressOf axCZKEM1_OnAlarm)
            AddHandler axCZKEM1.OnDoor, New zkemkeeper._IZKEMEvents_OnDoorEventHandler(AddressOf axCZKEM1_OnDoor)

            'only for color device
            AddHandler axCZKEM1.OnAttTransactionEx, New zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(AddressOf axCZKEM1_OnAttTransactionEx)
            AddHandler axCZKEM1.OnEnrollFingerEx, New zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(AddressOf axCZKEM1_OnEnrollFingerEx)

            'only for black&white device
            AddHandler axCZKEM1.OnAttTransaction, New zkemkeeper._IZKEMEvents_OnAttTransactionEventHandler(AddressOf axCZKEM1_OnAttTransaction)
            AddHandler axCZKEM1.OnWriteCard, New zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(AddressOf axCZKEM1_OnWriteCard)
            AddHandler axCZKEM1.OnEmptyCard, New zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(AddressOf axCZKEM1_OnEmptyCard)
            AddHandler axCZKEM1.OnKeyPress, New zkemkeeper._IZKEMEvents_OnKeyPressEventHandler(AddressOf axCZKEM1_OnKeyPress)
            AddHandler axCZKEM1.OnEnrollFinger, New zkemkeeper._IZKEMEvents_OnEnrollFingerEventHandler(AddressOf axCZKEM1_OnEnrollFinger)

        End If
    End Sub

    Private Sub DesregistrarEventosOnLine()
        'common interface
        RemoveHandler axCZKEM1.OnFinger, New zkemkeeper._IZKEMEvents_OnFingerEventHandler(AddressOf axCZKEM1_OnFinger)
        RemoveHandler axCZKEM1.OnVerify, New zkemkeeper._IZKEMEvents_OnVerifyEventHandler(AddressOf axCZKEM1_OnVerify)
        RemoveHandler axCZKEM1.OnFingerFeature, New zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(AddressOf axCZKEM1_OnFingerFeature)
        RemoveHandler axCZKEM1.OnDeleteTemplate, New zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(AddressOf axCZKEM1_OnDeleteTemplate)
        RemoveHandler axCZKEM1.OnNewUser, New zkemkeeper._IZKEMEvents_OnNewUserEventHandler(AddressOf axCZKEM1_OnNewUser)
        RemoveHandler axCZKEM1.OnHIDNum, New zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(AddressOf axCZKEM1_OnHIDNum)
        RemoveHandler axCZKEM1.OnAlarm, New zkemkeeper._IZKEMEvents_OnAlarmEventHandler(AddressOf axCZKEM1_OnAlarm)
        RemoveHandler axCZKEM1.OnDoor, New zkemkeeper._IZKEMEvents_OnDoorEventHandler(AddressOf axCZKEM1_OnDoor)

        'only for color device
        RemoveHandler axCZKEM1.OnAttTransactionEx, New zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(AddressOf axCZKEM1_OnAttTransactionEx)
        RemoveHandler axCZKEM1.OnEnrollFingerEx, New zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(AddressOf axCZKEM1_OnEnrollFingerEx)

        'only for black&white device
        RemoveHandler axCZKEM1.OnAttTransaction, New zkemkeeper._IZKEMEvents_OnAttTransactionEventHandler(AddressOf axCZKEM1_OnAttTransaction)
        RemoveHandler axCZKEM1.OnWriteCard, New zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(AddressOf axCZKEM1_OnWriteCard)
        RemoveHandler axCZKEM1.OnEmptyCard, New zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(AddressOf axCZKEM1_OnEmptyCard)
        RemoveHandler axCZKEM1.OnKeyPress, New zkemkeeper._IZKEMEvents_OnKeyPressEventHandler(AddressOf axCZKEM1_OnKeyPress)
        RemoveHandler axCZKEM1.OnEnrollFinger, New zkemkeeper._IZKEMEvents_OnEnrollFingerEventHandler(AddressOf axCZKEM1_OnEnrollFinger)
    End Sub

    Private Sub axCZKEM1_OnFinger() Handles axCZKEM1.OnFinger

    End Sub

    Private Sub axCZKEM1_OnFingerFeature(Score As Integer) Handles axCZKEM1.OnFingerFeature

    End Sub

    Private Sub axCZKEM1_OnDeleteTemplate(EnrollNumber As Integer, FingerIndex As Integer) Handles axCZKEM1.OnDeleteTemplate

    End Sub

    Private Sub axCZKEM1_OnNewUser(EnrollNumber As Integer) Handles axCZKEM1.OnNewUser

    End Sub

    Private Sub axCZKEM1_OnHIDNum(CardNumber As Integer) Handles axCZKEM1.OnHIDNum

    End Sub

    Private Sub axCZKEM1_OnAlarm(AlarmType As Integer, EnrollNumber As Integer, Verified As Integer) Handles axCZKEM1.OnAlarm

    End Sub

    Private Sub axCZKEM1_OnDoor(EventType As Integer) Handles axCZKEM1.OnDoor

    End Sub

    Private Sub axCZKEM1_OnAttTransaction(EnrollNumber As Integer, IsInValid As Integer, AttState As Integer, VerifyMethod As Integer, Year As Integer, Month As Integer, Day As Integer, Hour As Integer, Minute As Integer, Second As Integer) Handles axCZKEM1.OnAttTransaction
        Dim sFechaCompleta As String

        sFechaCompleta = Year.ToString + "-" + Month.ToString + "-" + Day.ToString + " " + Hour.ToString + ":" + Minute.ToString + ":" + Second.ToString
        RaiseEvent FichadaOnlineEvent(sFechaCompleta, EnrollNumber.ToString, "E")
    End Sub

    Private Sub axCZKEM1_OnAttTransactionEx(EnrollNumber As String, IsInValid As Integer, AttState As Integer, VerifyMethod As Integer, Year As Integer, Month As Integer, Day As Integer, Hour As Integer, Minute As Integer, Second As Integer, WorkCode As Integer) Handles axCZKEM1.OnAttTransactionEx

    End Sub

    Private Sub axCZKEM1_OnEnrollFingerEx(EnrollNumber As String, FingerIndex As Integer, ActionResult As Integer, TemplateLength As Integer) Handles axCZKEM1.OnEnrollFingerEx

    End Sub

    Private Sub axCZKEM1_OnWriteCard(EnrollNumber As Integer, ActionResult As Integer, Length As Integer) Handles axCZKEM1.OnWriteCard

    End Sub

    Private Sub axCZKEM1_OnEmptyCard(ActionResult As Integer) Handles axCZKEM1.OnEmptyCard

    End Sub

    Private Sub axCZKEM1_OnKeyPress(Key As Integer) Handles axCZKEM1.OnKeyPress

    End Sub

    Private Sub axCZKEM1_OnEnrollFinger(EnrollNumber As Integer, FingerIndex As Integer, ActionResult As Integer, TemplateLength As Integer) Handles axCZKEM1.OnEnrollFinger

    End Sub

    Private Function IBackEnd_PrepararLectura() As Boolean Implements IBackEnd.PrepararLectura
        Dim oReturn As Boolean
        oReturn = False
        If _IsConnected Then
            'axCZKEM1.EnableDevice(axCZKEM1.MachineNumber, False)
            If axCZKEM1.ReadGeneralLogData(axCZKEM1.MachineNumber) Then
                oReturn = True
            Else
                oReturn = False
            End If
            'axCZKEM1.EnableDevice(axCZKEM1.MachineNumber, True)
        End If
        Return oReturn
    End Function
End Class
