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
        Me.AxBIO_IP_RT1 = New AxBIO_IP_RealTime.AxBIO_IP_RT()
        CType(Me.AxBIO_IP_RT1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxBIO_IP_RT1
        '
        Me.AxBIO_IP_RT1.Enabled = True
        Me.AxBIO_IP_RT1.Location = New System.Drawing.Point(12, 12)
        Me.AxBIO_IP_RT1.Name = "AxBIO_IP_RT1"
        Me.AxBIO_IP_RT1.OcxState = CType(resources.GetObject("AxBIO_IP_RT1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxBIO_IP_RT1.Size = New System.Drawing.Size(32, 32)
        Me.AxBIO_IP_RT1.TabIndex = 0
        '
        'BIOIPRT
        '
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.AxBIO_IP_RT1)
        Me.Name = "BIOIPRT"
        CType(Me.AxBIO_IP_RT1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents AxBIO_IP_RT1 As AxBIO_IP_RealTime.AxBIO_IP_RT

    'Friend WithEvents AxBIO_IP_RT1 As AxBIO_IP_RealTime.AxBIO_IP_RT
End Class
