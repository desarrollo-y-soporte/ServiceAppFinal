<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.AxBIO_IP_RT2 = New AxBIO_IP_RealTime.AxBIO_IP_RT()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.AxBIO_IP_RT2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(197, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'BIOIPRT
        '
        Me.ClientSize = New System.Drawing.Size(284, 54)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.AxBIO_IP_RT2)
        Me.Name = "BIOIPRT"
        CType(Me.AxBIO_IP_RT2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents AxBIO_IP_RT2 As AxBIO_IP_RealTime.AxBIO_IP_RT
    Friend WithEvents Button1 As Button

    'Friend WithEvents AxBIO_IP_RT1 As AxBIO_IP_RealTime.AxBIO_IP_RT
End Class
