Imports ServiceApp.Definiciones

Public Class DatosServicioMDL
    Private _IPServicio As String
    Private _PuertoServicio As Integer
    Private _CarpetaLog As String
    Private _IPRemota As String
    Private _PuertoRemoto As Integer
    Private _Sistema As Integer

#Region "New and Finalize"
    Public Sub New()
        _IPServicio = ""
        _PuertoServicio = -1
        _CarpetaLog = ""
        _IPRemota = ""
        _PuertoRemoto = -1
        _Sistema = BackendEnum.Ninguno
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
#End Region

#Region "Properties"
    Public Property IPServicio As String
        Get
            Return _IPServicio
        End Get
        Set(value As String)
            _IPServicio = value
        End Set
    End Property

    Public Property PuertoServicio As Integer
        Get
            Return _PuertoServicio
        End Get
        Set(value As Integer)
            _PuertoServicio = value
        End Set
    End Property

    Public Property CarpetaLog As String
        Get
            Return _CarpetaLog
        End Get
        Set(value As String)
            _CarpetaLog = value
        End Set
    End Property

    Public Property IPRemota As String
        Get
            Return _IPRemota
        End Get
        Set(value As String)
            _IPRemota = value
        End Set
    End Property

    Public Property PuertoRemoto As Integer
        Get
            Return _PuertoRemoto
        End Get
        Set(value As Integer)
            _PuertoRemoto = value
        End Set
    End Property

    Public Property Sistema As Integer
        Get
            Return _Sistema
        End Get
        Set(value As Integer)
            _Sistema = value
        End Set
    End Property
#End Region
End Class
