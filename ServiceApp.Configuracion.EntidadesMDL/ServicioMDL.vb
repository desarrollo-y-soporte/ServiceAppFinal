Imports ServiceApp.Configuracion.EntidadesMDL

Public Class ServicioMDL
    Private _DatosServiciosMDL As DatosServicioMDL
    Private _DatosOtrosSistemasDic As Dictionary(Of Integer, DatosServicioMDL)

#Region "New and Finalize"
    Public Sub New()
        MyBase.New
        _DatosServiciosMDL = New DatosServicioMDL
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
#End Region

#Region "Property"
    Public Property DatosServiciosMDL As DatosServicioMDL
        Get
            Return _DatosServiciosMDL
        End Get
        Set(value As DatosServicioMDL)
            _DatosServiciosMDL = value
        End Set
    End Property

    Public Property DatosOtrosSistemasDic As Dictionary(Of Integer, DatosServicioMDL)
        Get
            Return _DatosOtrosSistemasDic
        End Get
        Set(value As Dictionary(Of Integer, DatosServicioMDL))
            _DatosOtrosSistemasDic = value
        End Set
    End Property
#End Region
End Class
