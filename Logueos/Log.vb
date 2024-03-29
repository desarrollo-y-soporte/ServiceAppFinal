﻿Public Class Log
    Implements IDisposable

    Private _CarpetaLog As String

    Public Sub New()
        _CarpetaLog = "C:\temp\ServiceLog"
    End Sub

    Public Sub WriteLog(ByVal pMessage As String, ByVal severity As System.Diagnostics.TraceEventType)
        Dim pEncabezado As String
        Dim pTraceFile As String

        pTraceFile = _CarpetaLog + "\LOG_" + Now.ToString("yyyyMMdd") + ".log"
        pEncabezado = ""

        If Not System.IO.Directory.Exists(_CarpetaLog) Then
            System.IO.Directory.CreateDirectory(_CarpetaLog)
        End If
        If Not System.IO.File.Exists(pTraceFile) Then
            pEncabezado = pEncabezado + "Nombre Maquina: " + My.Computer.Name + vbCrLf
            pEncabezado = pEncabezado + "Sistema Operativo: " + My.Computer.Info.OSFullName + "Versión: " + My.Computer.Info.OSVersion.ToString + vbCrLf
        End If
        Using pStream As System.IO.Stream = System.IO.File.Open(pTraceFile, IO.FileMode.Append, IO.FileAccess.Write, IO.FileShare.Write)
            Using pTrace As New TextWriterTraceListener(pStream, Me.GetType.Name)
                If pEncabezado.ToString <> String.Empty Then
                    pTrace.Write(pEncabezado)
                End If
                pTrace.TraceData(New TraceEventCache, Now.ToString("yyyyMMdd hh:mm ss"), severity, 1, pMessage)
                pTrace.Flush()
                pTrace.Close()
            End Using
        End Using
    End Sub

    Public Sub WriteLog(ByVal ex As System.Exception, ByVal severity As System.Diagnostics.TraceEventType, ByVal additionalInfo As String)
        Dim pMensaje As String
        Dim pEncabezado As String

        pMensaje = Date.Now.ToString("yyyyMMdd hh:mm s") + vbTab + additionalInfo + vbTab + ex.ToString + vbCrLf + ex.StackTrace.ToString
        Dim pTraceFile As String
        pTraceFile = _CarpetaLog + "\LOG_" + Now.ToString("yyyyMMdd") + ".log"
        pEncabezado = ""

        If Not System.IO.Directory.Exists(_CarpetaLog) Then
            System.IO.Directory.CreateDirectory(_CarpetaLog)
        End If

        If Not System.IO.File.Exists(pTraceFile) Then
            pEncabezado = pEncabezado + "Nombre Maquina: " + My.Computer.Name + vbCrLf
            pEncabezado = pEncabezado + "Sistema Operativo: " + My.Computer.Info.OSFullName + "Versión: " + My.Computer.Info.OSVersion.ToString + vbCrLf
        End If
        Using pStream As System.IO.Stream = System.IO.File.Open(pTraceFile, IO.FileMode.Append, IO.FileAccess.Write, IO.FileShare.Write)
            Using pTrace As New TextWriterTraceListener(pStream, Me.GetType.Name)
                pTrace.TraceEvent(New TraceEventCache, Now.ToString("yyyyMMdd hh:mm ss"), TraceEventType.Error, 1, pMensaje)
                'Now.ToString("yyyyMMdd hh:mm s") + vbTab + AppDomain.CurrentDomain.FriendlyName, TraceEventType.Error, 1, _UserAudit + vbTab + Header + ": " + Message)
                pTrace.Flush()
                pTrace.Close()
            End Using
        End Using
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
