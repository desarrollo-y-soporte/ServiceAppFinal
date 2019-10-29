Imports ServiceApp.Configuracion.EntidadesDAL
Imports ServiceApp.Configuracion.EntidadesMDL

Public MustInherit Class ServicioBase
    Inherits ServicioMDL

    Private _ServicioDAL As ServicioDAL

#Region "New and Finalize"
    Public Sub New()
        MyBase.New
        Init()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Protected Sub Init()
        If _ServicioDAL IsNot Nothing Then
            _ServicioDAL = Nothing
        End If
        _ServicioDAL = New ServicioDAL(My.Application.Info.DirectoryPath + "\appconfig.xml")
    End Sub
#End Region

#Region "property"
    Public Property PathName() As String
        Get
            Return _ServicioDAL.PathName
        End Get
        Set(ByVal value As String)
            If _ServicioDAL IsNot Nothing Then
                _ServicioDAL = Nothing
            End If

            _ServicioDAL = New ServicioDAL(value)
        End Set
    End Property
#End Region

#Region "Metodos"
    Public Sub LoadFile()
        Try
            _ServicioDAL.LoadFile(Me)
        Catch ex As Exception
            Throw New Exception("Error en ServicioBase.LoadFile - " + ex.Message.ToString)
        End Try

    End Sub

    Public Overridable Sub SaveFile()
        Try
            _ServicioDAL.SaveFile(Me)
        Catch ex As Exception
            Throw New Exception("Error en ServicioBase.SaveFile - " + ex.Message.ToString)
        End Try


    End Sub
#End Region

End Class

Public Class Servicio
    Inherits ServicioBase

#Region "New and Finalize"
    Public Sub New()

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
#End Region

#Region "Metodos"
    Public Overrides Sub SaveFile()
        MyBase.SaveFile()
    End Sub
#End Region

End Class
