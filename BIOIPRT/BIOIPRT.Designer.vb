﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BIOIPRT
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BIOIPRT))
        Me.cmdConectar = New System.Windows.Forms.Button()
        Me.txtIP = New System.Windows.Forms.TextBox()
        Me.cmdListado = New System.Windows.Forms.Button()
        Me.AxBIO_IP_RT2 = New AxBIO_IP_RealTime.AxBIO_IP_RT()
        Me.cmdAdicionar = New System.Windows.Forms.Button()
        Me.lblArchivo = New System.Windows.Forms.Label()
        Me.lblError = New System.Windows.Forms.Label()
        Me.txtIdentificador = New System.Windows.Forms.TextBox()
        CType(Me.AxBIO_IP_RT2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdConectar
        '
        Me.cmdConectar.Location = New System.Drawing.Point(197, 39)
        Me.cmdConectar.Name = "cmdConectar"
        Me.cmdConectar.Size = New System.Drawing.Size(75, 23)
        Me.cmdConectar.TabIndex = 1
        Me.cmdConectar.Text = "Conectar"
        Me.cmdConectar.UseVisualStyleBackColor = True
        '
        'txtIP
        '
        Me.txtIP.Location = New System.Drawing.Point(68, 12)
        Me.txtIP.Name = "txtIP"
        Me.txtIP.Size = New System.Drawing.Size(123, 20)
        Me.txtIP.TabIndex = 2
        '
        'cmdListado
        '
        Me.cmdListado.Location = New System.Drawing.Point(197, 68)
        Me.cmdListado.Name = "cmdListado"
        Me.cmdListado.Size = New System.Drawing.Size(75, 23)
        Me.cmdListado.TabIndex = 3
        Me.cmdListado.Text = "Listado"
        Me.cmdListado.UseVisualStyleBackColor = True
        '
        'AxBIO_IP_RT2
        '
        Me.AxBIO_IP_RT2.Enabled = True
        Me.AxBIO_IP_RT2.Location = New System.Drawing.Point(12, 12)
        Me.AxBIO_IP_RT2.Name = "AxBIO_IP_RT2"
        Me.AxBIO_IP_RT2.OcxState = CType(resources.GetObject("AxBIO_IP_RT2.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxBIO_IP_RT2.Size = New System.Drawing.Size(32, 32)
        Me.AxBIO_IP_RT2.TabIndex = 0
        '
        'cmdAdicionar
        '
        Me.cmdAdicionar.Location = New System.Drawing.Point(116, 39)
        Me.cmdAdicionar.Name = "cmdAdicionar"
        Me.cmdAdicionar.Size = New System.Drawing.Size(75, 23)
        Me.cmdAdicionar.TabIndex = 4
        Me.cmdAdicionar.Text = "Adicionar"
        Me.cmdAdicionar.UseVisualStyleBackColor = True
        '
        'lblArchivo
        '
        Me.lblArchivo.Location = New System.Drawing.Point(12, 122)
        Me.lblArchivo.Name = "lblArchivo"
        Me.lblArchivo.Size = New System.Drawing.Size(260, 16)
        Me.lblArchivo.TabIndex = 7
        '
        'lblError
        '
        Me.lblError.Location = New System.Drawing.Point(7, 68)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(184, 42)
        Me.lblError.TabIndex = 8
        '
        'txtIdentificador
        '
        Me.txtIdentificador.Location = New System.Drawing.Point(197, 12)
        Me.txtIdentificador.Name = "txtIdentificador"
        Me.txtIdentificador.Size = New System.Drawing.Size(75, 20)
        Me.txtIdentificador.TabIndex = 9
        '
        'BIOIPRT
        '
        Me.ClientSize = New System.Drawing.Size(284, 145)
        Me.Controls.Add(Me.txtIdentificador)
        Me.Controls.Add(Me.lblError)
        Me.Controls.Add(Me.lblArchivo)
        Me.Controls.Add(Me.cmdAdicionar)
        Me.Controls.Add(Me.cmdListado)
        Me.Controls.Add(Me.txtIP)
        Me.Controls.Add(Me.cmdConectar)
        Me.Controls.Add(Me.AxBIO_IP_RT2)
        Me.Name = "BIOIPRT"
        CType(Me.AxBIO_IP_RT2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents AxBIO_IP_RT2 As AxBIO_IP_RealTime.AxBIO_IP_RT
    Friend WithEvents cmdConectar As Button
    Friend WithEvents txtIP As TextBox
    Friend WithEvents cmdListado As Button
    Friend WithEvents cmdAdicionar As Button
    Friend WithEvents lblArchivo As Label
    Friend WithEvents lblError As Label
    Friend WithEvents txtIdentificador As TextBox

    'Friend WithEvents AxBIO_IP_RT1 As AxBIO_IP_RealTime.AxBIO_IP_RT
End Class
