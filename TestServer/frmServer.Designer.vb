<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServer
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
        Me.cmdIniciar = New System.Windows.Forms.Button()
        Me.cmdParar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmdIniciar
        '
        Me.cmdIniciar.Location = New System.Drawing.Point(92, 37)
        Me.cmdIniciar.Name = "cmdIniciar"
        Me.cmdIniciar.Size = New System.Drawing.Size(75, 23)
        Me.cmdIniciar.TabIndex = 0
        Me.cmdIniciar.Text = "Iniciar"
        Me.cmdIniciar.UseVisualStyleBackColor = True
        '
        'cmdParar
        '
        Me.cmdParar.Location = New System.Drawing.Point(92, 66)
        Me.cmdParar.Name = "cmdParar"
        Me.cmdParar.Size = New System.Drawing.Size(75, 23)
        Me.cmdParar.TabIndex = 1
        Me.cmdParar.Text = "Parar"
        Me.cmdParar.UseVisualStyleBackColor = True
        '
        'frmServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(281, 136)
        Me.Controls.Add(Me.cmdParar)
        Me.Controls.Add(Me.cmdIniciar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmServer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Server"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmdIniciar As Button
    Friend WithEvents cmdParar As Button
End Class
