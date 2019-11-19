Imports ServiceApp.Configuracion.Entidades
Imports ServiceApp.Configuracion.EntidadesMDL

Public Class frmServicioConfiguracion
    Private _Servicio As New Servicio
    Private _DatosServicio As New Dictionary(Of Integer, DatosServicioMDL)

    Private Sub frmServicioConfiguracion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigurarControles()
        _Servicio.LoadFile()
        CargarDatos()
    End Sub

    Private Sub cmdBuscar_Click(sender As Object, e As EventArgs) Handles cmdBuscar.Click
        If BuscaDir.ShowDialog() = DialogResult.OK Then
            txtCarpetaLog.Text = BuscaDir.SelectedPath
        End If
    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        If MessageBox.Show("¿Acepta la configuracion?", My.Application.Info.Title, MessageBoxButtons.OKCancel) = DialogResult.OK Then
            DatosObligatorios()
            AceptarConfiguracion()
        End If
        End
    End Sub

    Private Sub CargarDatos()
        CargarComboEquipo()
        ConfigurarControles()

        txtCarpetaLog.Text = _Servicio.DatosServiciosMDL.CarpetaLog
        '-- Lista de Objetos de Otros Sistemas
        _DatosServicio = _Servicio.DatosOtrosSistemasDic
        If _DatosServicio.Count > 0 Then
            For Each iKey As Integer In _DatosServicio.Keys
                Dim pDatosOtrosSistemasMDL As DatosServicioMDL = _DatosServicio(iKey)
                AgregarRegistro(pDatosOtrosSistemasMDL.IPServicio, pDatosOtrosSistemasMDL.PuertoServicio, pDatosOtrosSistemasMDL.IPRemota, pDatosOtrosSistemasMDL.PuertoRemoto, pDatosOtrosSistemasMDL.Sistema, pDatosOtrosSistemasMDL.Identificador)
            Next
        End If
    End Sub

    Private Sub cmdAplicar_Click(sender As Object, e As EventArgs) Handles cmdAplicar.Click
        If MessageBox.Show("Se realizará la modificación de la configuración del servicio. ¿Desea continuar?", My.Application.Info.Title, MessageBoxButtons.OKCancel) = DialogResult.OK Then
            DatosObligatorios()
            AceptarConfiguracion(True)
        End If

    End Sub

    Private Sub AceptarConfiguracion(ByVal Optional pRecargarDatos As Boolean = False)
        Dim pDatosServicio As New Dictionary(Of Integer, DatosServicioMDL)
        Dim iKey As Integer

        iKey = 0
        ''---- Datos del Servicio
        '_Servicio.DatosServicios = txtIPServicio.Text
        '_Servicio.DatosServicio.Puerto = CInt(txtPuertoServicio.Text)
        _Servicio.DatosServiciosMDL.CarpetaLog = txtCarpetaLog.Text

        ''---- Datos Del Sistema Central
        '_Servicio.DatosSistemaCentral.DireccionIP = txtIPSistemaCentral.Text
        '_Servicio.DatosSistemaCentral.Puerto = CInt(txtPuertoSistemaCentral.Text)


        For Each item As ListViewItem In lvwOtrosSistemas.Items
            Try
                '.IPServicio = _DataTable.Rows(iKey).Item("IPServicio").ToString, .PuertoServicio = CInt(_DataTable.Rows(iKey).Item("PuertoServicio").ToString), .IPRemota = _DataTable.Rows(iKey).Item("IPOtroSistema").ToString, .PuertoRemoto = CInt(_DataTable.Rows(iKey).Item("PuertoOtroSistema").ToString), .Sistema = CInt(_DataTable.Rows(iKey).Item("EquipoBackEnd").ToString), .CarpetaLog = _DataTable.Rows(iKey).Item("CarpetaLog").ToString
                pDatosServicio.Add(iKey, New DatosServicioMDL With {.IPServicio = item.SubItems(0).Text, .PuertoServicio = CInt(item.SubItems(1).Text), .IPRemota = item.SubItems(2).Text, .PuertoRemoto = CInt(item.SubItems(3).Text), .Sistema = CInt(item.SubItems(4).Text), .Identificador = CInt(item.SubItems(5).Text)})
            Catch ex As Exception
                pDatosServicio.Add(iKey, New DatosServicioMDL With {.IPServicio = String.Empty, .PuertoServicio = -1, .IPRemota = String.Empty, .PuertoRemoto = String.Empty, .Sistema = -1, .Identificador = -1, .CarpetaLog = String.Empty})
            End Try
            iKey = iKey + 1
        Next

        _Servicio.DatosOtrosSistemasDic = pDatosServicio
        _Servicio.SaveFile()
        If pRecargarDatos Then
            CargarDatos()
        End If
    End Sub

    Private Sub ConfigurarControles()
        lvwOtrosSistemas.Columns.Clear()
        lvwOtrosSistemas.Columns.Add("IP Servidor", 120, HorizontalAlignment.Right)
        lvwOtrosSistemas.Columns.Add("Puerto Servidor", 70, HorizontalAlignment.Right)
        lvwOtrosSistemas.Columns.Add("IP Remota", 120, HorizontalAlignment.Right)
        lvwOtrosSistemas.Columns.Add("Puerto Remoto", 70, HorizontalAlignment.Right)
        lvwOtrosSistemas.Columns.Add("Sistema", 100, HorizontalAlignment.Left)
        lvwOtrosSistemas.Columns.Add("Identificador", 70, HorizontalAlignment.Left)
        lvwOtrosSistemas.Items.Clear()
        txtIPOtrosSistemas.Text = ""
        txtPuertoOtrosSistemas.Text = ""

    End Sub

    Private Sub cmdQuitar_Click(sender As Object, e As EventArgs) Handles cmdQuitar.Click
        If lvwOtrosSistemas.Items.Count > 0 Then
            If MessageBox.Show("Se va a quitar un registro. ¿Desea continuar?", My.Application.Info.Title, MessageBoxButtons.OKCancel) = DialogResult.OK Then
                lvwOtrosSistemas.Items.Remove(lvwOtrosSistemas.SelectedItems(0))
            End If
        End If
    End Sub

    Private Sub cmdAgregar_Click(sender As Object, e As EventArgs) Handles cmdAgregar.Click
        If (txtIPServicio.Text <> "" And txtPuertoServicio.Text <> "" And
            txtIPOtrosSistemas.Text <> "" And txtPuertoOtrosSistemas.Text <> "" And txtIdentificador.Text <> "") Then
            AgregarRegistro(txtIPServicio.Text, CInt(txtPuertoServicio.Text), txtIPOtrosSistemas.Text, CInt(txtPuertoOtrosSistemas.Text), cboSistemas.SelectedValue, CInt(txtIdentificador.Text))
        End If
    End Sub

    Private Sub AgregarRegistro(ByVal pDireccionIP As String, ByVal pPuerto As Integer, ByVal pIPRemoto As String, ByVal pPuertoRemoto As Integer, ByVal pOrigen As String, ByVal pIdentificador As Integer)
        Dim item As New ListViewItem()
        item.Text = pDireccionIP
        item.SubItems.Add(pPuerto)
        item.SubItems.Add(pIPRemoto)
        item.SubItems.Add(pPuertoRemoto)
        item.SubItems.Add(pOrigen)
        item.SubItems.Add(pIdentificador)
        lvwOtrosSistemas.Items.Add(item)
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
        cboSistemas.DisplayMember = "Value"
        cboSistemas.ValueMember = "Key"
        cboSistemas.DataSource = pDictionary.ToArray
    End Sub

    Private Sub DatosObligatorios()
        If txtCarpetaLog.Text = "" Then
            Throw New System.Exception("La direccion de la carpeta log se encuentra vacia")
        End If
    End Sub

End Class
