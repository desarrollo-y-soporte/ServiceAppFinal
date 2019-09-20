<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestDummy
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdClienteEnviar = New System.Windows.Forms.Button()
        Me.txtClienteMensaje = New System.Windows.Forms.TextBox()
        Me.cmdConectar = New System.Windows.Forms.Button()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.lblPuerto = New System.Windows.Forms.Label()
        Me.txtIP = New System.Windows.Forms.TextBox()
        Me.lblIP = New System.Windows.Forms.Label()
        Me.txtServerMensaje = New System.Windows.Forms.TextBox()
        Me.cmdServerEnviar = New System.Windows.Forms.Button()
        Me.cboEquipo = New System.Windows.Forms.ComboBox()
        Me.cmdSimularServer = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblFechaHora = New System.Windows.Forms.Label()
        Me.cmdDesconectarServer = New System.Windows.Forms.Button()
        Me.cmdAccion = New System.Windows.Forms.Button()
        Me.cboAcciones = New System.Windows.Forms.ComboBox()
        Me.lblPuertoServer = New System.Windows.Forms.Label()
        Me.lblEquipo = New System.Windows.Forms.Label()
        Me.txtServerPort = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdClienteEnviar)
        Me.GroupBox1.Controls.Add(Me.txtClienteMensaje)
        Me.GroupBox1.Controls.Add(Me.cmdConectar)
        Me.GroupBox1.Controls.Add(Me.txtPort)
        Me.GroupBox1.Controls.Add(Me.lblPuerto)
        Me.GroupBox1.Controls.Add(Me.txtIP)
        Me.GroupBox1.Controls.Add(Me.lblIP)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(440, 286)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Client State"
        '
        'cmdClienteEnviar
        '
        Me.cmdClienteEnviar.Location = New System.Drawing.Point(272, 107)
        Me.cmdClienteEnviar.Name = "cmdClienteEnviar"
        Me.cmdClienteEnviar.Size = New System.Drawing.Size(75, 23)
        Me.cmdClienteEnviar.TabIndex = 6
        Me.cmdClienteEnviar.Text = "Enviar"
        Me.cmdClienteEnviar.UseVisualStyleBackColor = True
        '
        'txtClienteMensaje
        '
        Me.txtClienteMensaje.Location = New System.Drawing.Point(12, 109)
        Me.txtClienteMensaje.Name = "txtClienteMensaje"
        Me.txtClienteMensaje.Size = New System.Drawing.Size(234, 20)
        Me.txtClienteMensaje.TabIndex = 5
        '
        'cmdConectar
        '
        Me.cmdConectar.Location = New System.Drawing.Point(272, 50)
        Me.cmdConectar.Name = "cmdConectar"
        Me.cmdConectar.Size = New System.Drawing.Size(75, 23)
        Me.cmdConectar.TabIndex = 4
        Me.cmdConectar.Text = "Conectar"
        Me.cmdConectar.UseVisualStyleBackColor = True
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(90, 52)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(100, 20)
        Me.txtPort.TabIndex = 3
        '
        'lblPuerto
        '
        Me.lblPuerto.AutoSize = True
        Me.lblPuerto.Location = New System.Drawing.Point(9, 55)
        Me.lblPuerto.Name = "lblPuerto"
        Me.lblPuerto.Size = New System.Drawing.Size(38, 13)
        Me.lblPuerto.TabIndex = 2
        Me.lblPuerto.Text = "Puerto"
        '
        'txtIP
        '
        Me.txtIP.Location = New System.Drawing.Point(90, 17)
        Me.txtIP.Name = "txtIP"
        Me.txtIP.Size = New System.Drawing.Size(156, 20)
        Me.txtIP.TabIndex = 1
        '
        'lblIP
        '
        Me.lblIP.AutoSize = True
        Me.lblIP.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblIP.Location = New System.Drawing.Point(6, 23)
        Me.lblIP.Name = "lblIP"
        Me.lblIP.Size = New System.Drawing.Size(17, 13)
        Me.lblIP.TabIndex = 0
        Me.lblIP.Text = "IP"
        '
        'txtServerMensaje
        '
        Me.txtServerMensaje.Location = New System.Drawing.Point(6, 91)
        Me.txtServerMensaje.Name = "txtServerMensaje"
        Me.txtServerMensaje.Size = New System.Drawing.Size(281, 20)
        Me.txtServerMensaje.TabIndex = 0
        '
        'cmdServerEnviar
        '
        Me.cmdServerEnviar.Location = New System.Drawing.Point(293, 89)
        Me.cmdServerEnviar.Name = "cmdServerEnviar"
        Me.cmdServerEnviar.Size = New System.Drawing.Size(75, 23)
        Me.cmdServerEnviar.TabIndex = 1
        Me.cmdServerEnviar.Text = "Enviar Mensaje"
        Me.cmdServerEnviar.UseVisualStyleBackColor = True
        '
        'cboEquipo
        '
        Me.cboEquipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEquipo.FormattingEnabled = True
        Me.cboEquipo.Location = New System.Drawing.Point(6, 37)
        Me.cboEquipo.Name = "cboEquipo"
        Me.cboEquipo.Size = New System.Drawing.Size(175, 21)
        Me.cboEquipo.TabIndex = 2
        '
        'cmdSimularServer
        '
        Me.cmdSimularServer.Location = New System.Drawing.Point(293, 35)
        Me.cmdSimularServer.Name = "cmdSimularServer"
        Me.cmdSimularServer.Size = New System.Drawing.Size(75, 23)
        Me.cmdSimularServer.TabIndex = 3
        Me.cmdSimularServer.Text = "Simular"
        Me.cmdSimularServer.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblFechaHora)
        Me.GroupBox2.Controls.Add(Me.cmdDesconectarServer)
        Me.GroupBox2.Controls.Add(Me.cmdAccion)
        Me.GroupBox2.Controls.Add(Me.cboAcciones)
        Me.GroupBox2.Controls.Add(Me.lblPuertoServer)
        Me.GroupBox2.Controls.Add(Me.lblEquipo)
        Me.GroupBox2.Controls.Add(Me.txtServerPort)
        Me.GroupBox2.Controls.Add(Me.cmdSimularServer)
        Me.GroupBox2.Controls.Add(Me.cboEquipo)
        Me.GroupBox2.Controls.Add(Me.cmdServerEnviar)
        Me.GroupBox2.Controls.Add(Me.txtServerMensaje)
        Me.GroupBox2.Location = New System.Drawing.Point(501, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(440, 287)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Server State"
        '
        'lblFechaHora
        '
        Me.lblFechaHora.Location = New System.Drawing.Point(6, 254)
        Me.lblFechaHora.Name = "lblFechaHora"
        Me.lblFechaHora.Size = New System.Drawing.Size(132, 30)
        Me.lblFechaHora.TabIndex = 10
        Me.lblFechaHora.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmdDesconectarServer
        '
        Me.cmdDesconectarServer.Location = New System.Drawing.Point(293, 63)
        Me.cmdDesconectarServer.Name = "cmdDesconectarServer"
        Me.cmdDesconectarServer.Size = New System.Drawing.Size(75, 23)
        Me.cmdDesconectarServer.TabIndex = 9
        Me.cmdDesconectarServer.Text = "Desconectar"
        Me.cmdDesconectarServer.UseVisualStyleBackColor = True
        '
        'cmdAccion
        '
        Me.cmdAccion.Location = New System.Drawing.Point(293, 118)
        Me.cmdAccion.Name = "cmdAccion"
        Me.cmdAccion.Size = New System.Drawing.Size(75, 23)
        Me.cmdAccion.TabIndex = 8
        Me.cmdAccion.Text = "Accion"
        Me.cmdAccion.UseVisualStyleBackColor = True
        '
        'cboAcciones
        '
        Me.cboAcciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAcciones.FormattingEnabled = True
        Me.cboAcciones.Location = New System.Drawing.Point(6, 120)
        Me.cboAcciones.Name = "cboAcciones"
        Me.cboAcciones.Size = New System.Drawing.Size(281, 21)
        Me.cboAcciones.TabIndex = 7
        '
        'lblPuertoServer
        '
        Me.lblPuertoServer.AutoSize = True
        Me.lblPuertoServer.Location = New System.Drawing.Point(187, 21)
        Me.lblPuertoServer.Name = "lblPuertoServer"
        Me.lblPuertoServer.Size = New System.Drawing.Size(38, 13)
        Me.lblPuertoServer.TabIndex = 6
        Me.lblPuertoServer.Text = "Puerto"
        '
        'lblEquipo
        '
        Me.lblEquipo.AutoSize = True
        Me.lblEquipo.Location = New System.Drawing.Point(6, 21)
        Me.lblEquipo.Name = "lblEquipo"
        Me.lblEquipo.Size = New System.Drawing.Size(86, 13)
        Me.lblEquipo.TabIndex = 5
        Me.lblEquipo.Text = "Equipo Backend"
        '
        'txtServerPort
        '
        Me.txtServerPort.Location = New System.Drawing.Point(187, 37)
        Me.txtServerPort.Name = "txtServerPort"
        Me.txtServerPort.Size = New System.Drawing.Size(100, 20)
        Me.txtServerPort.TabIndex = 4
        '
        'TestDummy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(953, 311)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "TestDummy"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dummy - Backend"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtIP As TextBox
    Public WithEvents lblIP As Label
    Friend WithEvents cmdConectar As Button
    Friend WithEvents txtPort As TextBox
    Friend WithEvents lblPuerto As Label
    Friend WithEvents cmdClienteEnviar As Button
    Friend WithEvents txtClienteMensaje As TextBox
    Friend WithEvents txtServerMensaje As TextBox
    Friend WithEvents cmdServerEnviar As Button
    Friend WithEvents cboEquipo As ComboBox
    Friend WithEvents cmdSimularServer As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents lblEquipo As Label
    Friend WithEvents txtServerPort As TextBox
    Friend WithEvents lblPuertoServer As Label
    Friend WithEvents cmdAccion As Button
    Friend WithEvents cboAcciones As ComboBox
    Friend WithEvents cmdDesconectarServer As Button
    Friend WithEvents lblFechaHora As Label
End Class
