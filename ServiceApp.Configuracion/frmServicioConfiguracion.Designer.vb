<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServicioConfiguracion
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmServicioConfiguracion))
        Me.txtIPServicio = New System.Windows.Forms.TextBox()
        Me.lblIPServicio = New System.Windows.Forms.Label()
        Me.cmdBuscar = New System.Windows.Forms.Button()
        Me.txtCarpetaLog = New System.Windows.Forms.TextBox()
        Me.lblCarpetaLog = New System.Windows.Forms.Label()
        Me.cmdAplicar = New System.Windows.Forms.Button()
        Me.cmdCerrar = New System.Windows.Forms.Button()
        Me.BuscaDir = New System.Windows.Forms.FolderBrowserDialog()
        Me.txtPuertoServicio = New System.Windows.Forms.TextBox()
        Me.lblPuertoServicio = New System.Windows.Forms.Label()
        Me.txtPuertoOtrosSistemas = New System.Windows.Forms.TextBox()
        Me.lblPuertoOtrosSistemas = New System.Windows.Forms.Label()
        Me.txtIPOtrosSistemas = New System.Windows.Forms.TextBox()
        Me.lblIpOtrosSistemas = New System.Windows.Forms.Label()
        Me.cboSistemas = New System.Windows.Forms.ComboBox()
        Me.lblOrigen = New System.Windows.Forms.Label()
        Me.cmdQuitar = New System.Windows.Forms.Button()
        Me.cmdAgregar = New System.Windows.Forms.Button()
        Me.lvwOtrosSistemas = New System.Windows.Forms.ListView()
        Me.txtIdentificador = New System.Windows.Forms.TextBox()
        Me.lblIdentificador = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtIPServicio
        '
        Me.txtIPServicio.Location = New System.Drawing.Point(12, 25)
        Me.txtIPServicio.Name = "txtIPServicio"
        Me.txtIPServicio.Size = New System.Drawing.Size(130, 20)
        Me.txtIPServicio.TabIndex = 0
        '
        'lblIPServicio
        '
        Me.lblIPServicio.AutoSize = True
        Me.lblIPServicio.Location = New System.Drawing.Point(12, 9)
        Me.lblIPServicio.Name = "lblIPServicio"
        Me.lblIPServicio.Size = New System.Drawing.Size(58, 13)
        Me.lblIPServicio.TabIndex = 2
        Me.lblIPServicio.Text = "IP Servicio"
        '
        'cmdBuscar
        '
        Me.cmdBuscar.Location = New System.Drawing.Point(277, 367)
        Me.cmdBuscar.Name = "cmdBuscar"
        Me.cmdBuscar.Size = New System.Drawing.Size(75, 23)
        Me.cmdBuscar.TabIndex = 8
        Me.cmdBuscar.Text = "Buscar"
        Me.cmdBuscar.UseVisualStyleBackColor = True
        '
        'txtCarpetaLog
        '
        Me.txtCarpetaLog.Location = New System.Drawing.Point(12, 369)
        Me.txtCarpetaLog.Name = "txtCarpetaLog"
        Me.txtCarpetaLog.Size = New System.Drawing.Size(259, 20)
        Me.txtCarpetaLog.TabIndex = 7
        '
        'lblCarpetaLog
        '
        Me.lblCarpetaLog.AutoSize = True
        Me.lblCarpetaLog.Location = New System.Drawing.Point(12, 352)
        Me.lblCarpetaLog.Name = "lblCarpetaLog"
        Me.lblCarpetaLog.Size = New System.Drawing.Size(65, 13)
        Me.lblCarpetaLog.TabIndex = 7
        Me.lblCarpetaLog.Text = "Carpeta Log"
        '
        'cmdAplicar
        '
        Me.cmdAplicar.Location = New System.Drawing.Point(411, 398)
        Me.cmdAplicar.Name = "cmdAplicar"
        Me.cmdAplicar.Size = New System.Drawing.Size(75, 23)
        Me.cmdAplicar.TabIndex = 10
        Me.cmdAplicar.Text = "Aplicar"
        Me.cmdAplicar.UseVisualStyleBackColor = True
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Location = New System.Drawing.Point(492, 398)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(75, 23)
        Me.cmdCerrar.TabIndex = 11
        Me.cmdCerrar.Text = "Cerrar"
        Me.cmdCerrar.UseVisualStyleBackColor = True
        '
        'txtPuertoServicio
        '
        Me.txtPuertoServicio.Location = New System.Drawing.Point(145, 25)
        Me.txtPuertoServicio.Name = "txtPuertoServicio"
        Me.txtPuertoServicio.Size = New System.Drawing.Size(100, 20)
        Me.txtPuertoServicio.TabIndex = 1
        '
        'lblPuertoServicio
        '
        Me.lblPuertoServicio.AutoSize = True
        Me.lblPuertoServicio.Location = New System.Drawing.Point(145, 8)
        Me.lblPuertoServicio.Name = "lblPuertoServicio"
        Me.lblPuertoServicio.Size = New System.Drawing.Size(79, 13)
        Me.lblPuertoServicio.TabIndex = 16
        Me.lblPuertoServicio.Text = "Puerto Servicio"
        '
        'txtPuertoOtrosSistemas
        '
        Me.txtPuertoOtrosSistemas.Location = New System.Drawing.Point(148, 70)
        Me.txtPuertoOtrosSistemas.Name = "txtPuertoOtrosSistemas"
        Me.txtPuertoOtrosSistemas.Size = New System.Drawing.Size(100, 20)
        Me.txtPuertoOtrosSistemas.TabIndex = 4
        '
        'lblPuertoOtrosSistemas
        '
        Me.lblPuertoOtrosSistemas.AutoSize = True
        Me.lblPuertoOtrosSistemas.Location = New System.Drawing.Point(145, 54)
        Me.lblPuertoOtrosSistemas.Name = "lblPuertoOtrosSistemas"
        Me.lblPuertoOtrosSistemas.Size = New System.Drawing.Size(111, 13)
        Me.lblPuertoOtrosSistemas.TabIndex = 20
        Me.lblPuertoOtrosSistemas.Text = "Puerto Otros Sistemas"
        '
        'txtIPOtrosSistemas
        '
        Me.txtIPOtrosSistemas.Location = New System.Drawing.Point(12, 71)
        Me.txtIPOtrosSistemas.Name = "txtIPOtrosSistemas"
        Me.txtIPOtrosSistemas.Size = New System.Drawing.Size(130, 20)
        Me.txtIPOtrosSistemas.TabIndex = 3
        '
        'lblIpOtrosSistemas
        '
        Me.lblIpOtrosSistemas.AutoSize = True
        Me.lblIpOtrosSistemas.Location = New System.Drawing.Point(12, 55)
        Me.lblIpOtrosSistemas.Name = "lblIpOtrosSistemas"
        Me.lblIpOtrosSistemas.Size = New System.Drawing.Size(90, 13)
        Me.lblIpOtrosSistemas.TabIndex = 19
        Me.lblIpOtrosSistemas.Text = "IP Otros Sistemas"
        '
        'cboSistemas
        '
        Me.cboSistemas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSistemas.FormattingEnabled = True
        Me.cboSistemas.Location = New System.Drawing.Point(254, 70)
        Me.cboSistemas.Name = "cboSistemas"
        Me.cboSistemas.Size = New System.Drawing.Size(159, 21)
        Me.cboSistemas.TabIndex = 5
        '
        'lblOrigen
        '
        Me.lblOrigen.AutoSize = True
        Me.lblOrigen.Location = New System.Drawing.Point(254, 54)
        Me.lblOrigen.Name = "lblOrigen"
        Me.lblOrigen.Size = New System.Drawing.Size(87, 13)
        Me.lblOrigen.TabIndex = 22
        Me.lblOrigen.Text = "Equipo BackEnd"
        '
        'cmdQuitar
        '
        Me.cmdQuitar.Location = New System.Drawing.Point(492, 347)
        Me.cmdQuitar.Name = "cmdQuitar"
        Me.cmdQuitar.Size = New System.Drawing.Size(75, 23)
        Me.cmdQuitar.TabIndex = 9
        Me.cmdQuitar.Text = "Quitar"
        Me.cmdQuitar.UseVisualStyleBackColor = True
        '
        'cmdAgregar
        '
        Me.cmdAgregar.Location = New System.Drawing.Point(419, 69)
        Me.cmdAgregar.Name = "cmdAgregar"
        Me.cmdAgregar.Size = New System.Drawing.Size(75, 23)
        Me.cmdAgregar.TabIndex = 6
        Me.cmdAgregar.Text = "Agregar"
        Me.cmdAgregar.UseVisualStyleBackColor = True
        '
        'lvwOtrosSistemas
        '
        Me.lvwOtrosSistemas.FullRowSelect = True
        Me.lvwOtrosSistemas.HideSelection = False
        Me.lvwOtrosSistemas.Location = New System.Drawing.Point(12, 98)
        Me.lvwOtrosSistemas.Name = "lvwOtrosSistemas"
        Me.lvwOtrosSistemas.Size = New System.Drawing.Size(555, 243)
        Me.lvwOtrosSistemas.TabIndex = 8
        Me.lvwOtrosSistemas.UseCompatibleStateImageBehavior = False
        Me.lvwOtrosSistemas.View = System.Windows.Forms.View.Details
        '
        'txtIdentificador
        '
        Me.txtIdentificador.Location = New System.Drawing.Point(251, 26)
        Me.txtIdentificador.Name = "txtIdentificador"
        Me.txtIdentificador.Size = New System.Drawing.Size(100, 20)
        Me.txtIdentificador.TabIndex = 2
        '
        'lblIdentificador
        '
        Me.lblIdentificador.AutoSize = True
        Me.lblIdentificador.Location = New System.Drawing.Point(251, 9)
        Me.lblIdentificador.Name = "lblIdentificador"
        Me.lblIdentificador.Size = New System.Drawing.Size(65, 13)
        Me.lblIdentificador.TabIndex = 24
        Me.lblIdentificador.Text = "Identificador"
        '
        'frmServicioConfiguracion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(579, 433)
        Me.Controls.Add(Me.txtIdentificador)
        Me.Controls.Add(Me.lblIdentificador)
        Me.Controls.Add(Me.lvwOtrosSistemas)
        Me.Controls.Add(Me.cmdQuitar)
        Me.Controls.Add(Me.cmdAgregar)
        Me.Controls.Add(Me.cboSistemas)
        Me.Controls.Add(Me.lblOrigen)
        Me.Controls.Add(Me.txtPuertoOtrosSistemas)
        Me.Controls.Add(Me.lblPuertoOtrosSistemas)
        Me.Controls.Add(Me.txtIPOtrosSistemas)
        Me.Controls.Add(Me.lblIpOtrosSistemas)
        Me.Controls.Add(Me.txtPuertoServicio)
        Me.Controls.Add(Me.lblPuertoServicio)
        Me.Controls.Add(Me.cmdAplicar)
        Me.Controls.Add(Me.cmdCerrar)
        Me.Controls.Add(Me.cmdBuscar)
        Me.Controls.Add(Me.txtCarpetaLog)
        Me.Controls.Add(Me.lblCarpetaLog)
        Me.Controls.Add(Me.txtIPServicio)
        Me.Controls.Add(Me.lblIPServicio)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmServicioConfiguracion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ServiceApp - Soporte y Desarrollo de Sistemas"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtIPServicio As TextBox
    Friend WithEvents lblIPServicio As Label
    Friend WithEvents cmdBuscar As Button
    Friend WithEvents txtCarpetaLog As TextBox
    Friend WithEvents lblCarpetaLog As Label
    Friend WithEvents cmdAplicar As Button
    Friend WithEvents cmdCerrar As Button
    Friend WithEvents BuscaDir As FolderBrowserDialog
    Friend WithEvents txtPuertoServicio As TextBox
    Friend WithEvents lblPuertoServicio As Label
    Friend WithEvents txtPuertoOtrosSistemas As TextBox
    Friend WithEvents lblPuertoOtrosSistemas As Label
    Friend WithEvents txtIPOtrosSistemas As TextBox
    Friend WithEvents lblIpOtrosSistemas As Label
    Friend WithEvents cboSistemas As ComboBox
    Friend WithEvents lblOrigen As Label
    Friend WithEvents cmdQuitar As Button
    Friend WithEvents cmdAgregar As Button
    Friend WithEvents lvwOtrosSistemas As ListView
    Friend WithEvents txtIdentificador As TextBox
    Friend WithEvents lblIdentificador As Label
End Class
