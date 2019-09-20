Imports System.ComponentModel
Imports ServiceApp.Service

Public Class frmServer
    Private _ServiceApp As ServiceApp.Service.ServiceApp

    Private Sub frmServer_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        PararServicio()
        Finalizar()
    End Sub

    Private Sub Finalizar()
        End
    End Sub

    Private Sub cmdIniciar_Click(sender As Object, e As EventArgs) Handles cmdIniciar.Click
        _ServiceApp = New ServiceApp.Service.ServiceApp
        cmdIniciar.Enabled = False
    End Sub

    Private Sub PararServicio()
        If _ServiceApp IsNot Nothing Then
            _ServiceApp.Dispose()
            _ServiceApp = Nothing
        End If
    End Sub

    Private Sub cmdParar_Click(sender As Object, e As EventArgs) Handles cmdParar.Click
        cmdIniciar.Enabled = True
        PararServicio()
    End Sub
End Class
