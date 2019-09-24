Imports System.ComponentModel
Imports System.Net
Imports ServiceApp.Dummy
Imports ServiceApp.Definiciones

Public Class TestDummy

    WithEvents WinSockServer As New WSServer
    WithEvents WinSockCliente As New WSCliente
    WithEvents pDummy As New Dummy.Dummy

    Private _CantidadFichadas As Integer
    Private _Accion As OperacionesEnum

#Region "Cliente"
    Private Sub PrepararCliente()
        With WinSockCliente
            'Determino a donde se quiere conectar el usuario

            .IP = txtIP.Text
            .Port = txtPort.Text

            'Me conecto
            .Conectar()

            'Deshabilito la posibilidad de conexion
            txtIP.Enabled = False
            txtPort.Enabled = False
            cmdConectar.Enabled = False

            'Habilito la posibilidad de enviar mensajes
            cmdClienteEnviar.Enabled = True
            txtClienteMensaje.Enabled = True
        End With
    End Sub

    Private Sub WinSockCliente_ConexionTerminada() Handles WinSockCliente.ConexionTerminada
        MsgBox("Finalizo la conexion")

        'Habilito la posibilidad de una reconexion
        txtIP.Enabled = True
        txtPort.Enabled = True
        cmdConectar.Enabled = True

        'Deshabilito la posibilidad de enviar mensajes
        cmdClienteEnviar.Enabled = False
        txtClienteMensaje.Enabled = False
    End Sub

    Private Sub WinSockCliente_DatosRecibidos(datos As String) Handles WinSockCliente.DatosRecibidos
        'MsgBox("El servidor envio el siguiente mensaje: " & datos)
    End Sub

    Private Sub cmdClienteEnviar_Click(sender As Object, e As EventArgs) Handles cmdClienteEnviar.Click
        'Envio lo que esta escrito en la caja de texto del mensaje
        WinSockCliente.EnviarDatos(txtClienteMensaje.Text)
    End Sub

    Private Sub cmdConectar_Click(sender As Object, e As EventArgs) Handles cmdConectar.Click
        PrepararCliente()
    End Sub
#End Region

#Region "Metodos"
    Private Sub TestDummy_Load(sender As Object, e As EventArgs) Handles Me.Load
        CargarComboEquipo()
        CargarComboAcciones()
        SetearDefault()
    End Sub

    Private Sub TestDummy_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        End
    End Sub

    Private Sub CargarComboEquipo()
        'Ninguno = -1
        'ZKTeco = 1
        'Dimmep = 2
        'SistemaCentral = 3
        'Dummy = 4
        Dim pDictionary As New System.Collections.Generic.Dictionary(Of Integer, String)
        pDictionary.Add(1, "ZKTeco")
        pDictionary.Add(2, "Dimmep")
        pDictionary.Add(4, "Dummy")
        pDictionary.Add(3, "SistemaCentral")
        cboEquipo.DisplayMember = "Value"
        cboEquipo.ValueMember = "Key"
        cboEquipo.DataSource = pDictionary.ToArray
    End Sub

    Private Sub CargarComboAcciones()
        Dim pDictionary As New System.Collections.Generic.Dictionary(Of Integer, String)
        pDictionary.Add(1, "Enviar Fichada On Line")
        'pDictionary.Add(2, "CambioFechaHora")
        'pDictionary.Add(3, "Borrado")
        'pDictionary.Add(5, "InhabilitacionTotal")
        'pDictionary.Add(6, "AltaTarjeta")

        cboAcciones.DisplayMember = "Value"
        cboAcciones.ValueMember = "Key"
        cboAcciones.DataSource = pDictionary.ToArray
    End Sub

    Private Sub SetearDefault()
        lblFechaHora.Text = Now
        _CantidadFichadas = Me.GenRandomInt()
        lblFichadasEnMemoria.Text = "Fichadas en Memoria: " + _CantidadFichadas.ToString
    End Sub
#End Region

#Region "Servidor"
    Private Sub cmdSimularServer_Click(sender As Object, e As EventArgs) Handles cmdSimularServer.Click
        PrepararServer()
        cmdSimularServer.Enabled = False
        txtServerPort.Enabled = False
        cboEquipo.Enabled = False
    End Sub

    Private Sub cmdAccion_Click(sender As Object, e As EventArgs) Handles cmdAccion.Click
        'Envio el texto escrito en el textbox txtMensaje a todos los clientes
        Select Case cboAcciones.SelectedValue
            Case 1
                WinSockServer.EnviarDatos(EnviarFichada)
        End Select

    End Sub

    Private Sub WinSockServer_ConexionTerminada(IDTerminal As IPEndPoint) Handles WinSockServer.ConexionTerminada
        'Muestro con quien se termino la conexion
        'MsgBox("Se ha desconectado el cliente desde la IP= " & IDTerminal.Address.ToString & ",Puerto = " & IDTerminal.Port)
    End Sub

    Private Sub WinSockServer_DatosRecibidos(IDTerminal As IPEndPoint) Handles WinSockServer.DatosRecibidos
        'Muestro quien envio el mensaje
        'MsgBox("Nuevo mensaje desde el cliente de la IP= " & IDTerminal.Address.ToString & ",Puerto = " & IDTerminal.Port)
        'Muestro el mensaje recibido
        Dim pMensaje As String = WinSockServer.ObtenerDatos(IDTerminal)
        Dim pDiaYHora As String = Now.ToString("yyMMddhhmmss")

        If IsNumeric(pMensaje) Then
            _Accion = CInt(pMensaje)
            Select Case _Accion
                Case OperacionesEnum.CambioFechaHora
                    lblFechaHora.Text = Now()
                Case OperacionesEnum.AltaTarjeta
                Case OperacionesEnum.BajaTarjeta
                Case OperacionesEnum.Borrado
                Case OperacionesEnum.InhabilitacionTotal
                Case OperacionesEnum.Inicializacion
                    WinSockServer.EnviarDatos("66," + pDiaYHora + "," + _CantidadFichadas.ToString)
                Case OperacionesEnum.Lectura
                    For i As Integer = 0 To _CantidadFichadas - 1
                        WinSockServer.EnviarDatos(EnviarFichada)
                        Threading.Thread.Sleep(1000)
                    Next
                Case OperacionesEnum.SalidaRelay
                Case Else
                    MsgBox(pMensaje)
            End Select
        Else
            MsgBox(pMensaje)
        End If

    End Sub

    Private Sub WinSockServer_NuevaConexion(IDTerminal As IPEndPoint) Handles WinSockServer.NuevaConexion
        'Muestro quien se conecto
        'MsgBox("Se ha conectado un nuevo cliente desde la IP= " & IDTerminal.Address.ToString & ",Puerto = " & IDTerminal.Port)
    End Sub

    Public Function EnviarFichada() As String
        Dim pDiaYHora As String
        Dim pFichada As String
        Dim pMensaje As String
        pDiaYHora = Now.ToString("yyMMddhhmmss")
        pFichada = GenRandomInt.ToString

        pMensaje = "0," + pDiaYHora + "," + pFichada

        Return pMensaje
    End Function

    Public Sub PrepararServer()
        With WinSockServer
            .Equipo = cboEquipo.SelectedValue
            'Establezco el puerto donde escuchar
            .Port = CInt(txtServerPort.Text)
            'Comienzo la escucha
            .Escuchar()
        End With
    End Sub

    Private Sub cmdServerEnviar_Click(sender As Object, e As EventArgs) Handles cmdServerEnviar.Click
        'Envio el texto escrito en el textbox txtMensaje a todos los clientes
        WinSockServer.EnviarDatos(txtServerMensaje.Text)
    End Sub

    Private Sub cmdDesconectarServer_Click(sender As Object, e As EventArgs) Handles cmdDesconectarServer.Click
        cmdSimularServer.Enabled = True
        txtServerPort.Enabled = True
        cboEquipo.Enabled = True
        WinSockServer = Nothing
    End Sub

    Private Function GenRandomInt() As Int32
        Static staticRandomGenerator As New System.Random
        Return staticRandomGenerator.Next(1, 10000)
    End Function

#End Region
End Class
