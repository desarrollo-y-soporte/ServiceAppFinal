Imports System.Net.Sockets
Imports System.Threading

Public Class WSInfoCliente
    Implements IDisposable

    'Esta estructura permite guardar la información sobre un cliente
    Private _Socket As Socket 'Socket utilizado para mantener la conexion con el cliente
    Private _Thread As Thread 'Thread utilizado para escuchar al cliente
    Private _UltimosDatosRecibidos As String 'Ultimos datos enviados por el cliente

    Public Sub New()

    End Sub

    Public Property Socket As Socket
        Get
            Return _Socket
        End Get
        Set(value As Socket)
            _Socket = value
        End Set
    End Property

    Public Property Thread As Thread
        Get
            Return _Thread
        End Get
        Set(value As Thread)
            _Thread = value
        End Set
    End Property

    Public Property UltimosDatosRecibidos As String
        Get
            Return _UltimosDatosRecibidos
        End Get
        Set(value As String)
            _UltimosDatosRecibidos = value
        End Set
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If

            ' TODO: libere los recursos no administrados (objetos no administrados) y reemplace Finalize() a continuación.
            ' TODO: configure los campos grandes en nulos.
        End If
        disposedValue = True
    End Sub

    ' TODO: reemplace Finalize() solo si el anterior Dispose(disposing As Boolean) tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
        Dispose(True)
        ' TODO: quite la marca de comentario de la siguiente línea si Finalize() se ha reemplazado antes.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
