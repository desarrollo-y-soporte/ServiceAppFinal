Imports ServiceApp.Configuracion.EntidadesMDL

Public Class ServicioDAL
    Private _DatosServicioDAL As DatosServicioDAL
    Private _DataSet As DataSet
    Private _PathName As String
    Private _DataTable As DataTable

#Region "New and Finalize"
    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal pPath As String)
        MyBase.New()
        _PathName = pPath
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
#End Region

#Region "Property"
    Public Property PathName() As String
        Get
            Return _PathName
        End Get
        Set(ByVal value As String)
            _PathName = value
        End Set
    End Property
#End Region

#Region "Metodos"
    Public Sub SaveFile(ByVal pServicioMDL As ServicioMDL)
        Dim pFileStream As System.IO.FileStream = Nothing

        Try

            _DatosServicioDAL.DatosOtrosSistemas = pServicioMDL.DatosOtrosSistemasDic

            pFileStream = New System.IO.FileStream(_PathName, System.IO.FileMode.OpenOrCreate)
            pFileStream.SetLength(0) 'trunca el archivo y lo prepara para guardar los nuevos datos
            _DataSet.WriteXml(pFileStream)
        Finally
            If Not pFileStream Is Nothing Then
                pFileStream.Close()
                pFileStream = Nothing
            End If
        End Try
    End Sub

    Public Sub LoadFile(ByVal pServicioMDL As ServicioMDL)
        Dim pFileStream As System.IO.FileStream = Nothing

        Try
            pFileStream = New System.IO.FileStream(_PathName, System.IO.FileMode.OpenOrCreate)

            _DataSet = New DataSet
            _DataSet.ReadXml(pFileStream)


            _DatosServicioDAL = New DatosServicioDAL(_DataSet)
            pServicioMDL.DatosServiciosMDL = _DatosServicioDAL.DatosServicio
            pServicioMDL.DatosOtrosSistemasDic = _DatosServicioDAL.DatosOtrosSistemas

        Catch ex As Xml.XmlException
            _DataSet.DataSetName = "root"
            _DatosServicioDAL = New DatosServicioDAL(_DataSet)

            pServicioMDL.DatosServiciosMDL = _DatosServicioDAL.DatosServicio
            pServicioMDL.DatosOtrosSistemasDic = _DatosServicioDAL.DatosOtrosSistemas
            'pServicioMDL.DatosOtrosSistemasDic = _DatosServicioDAL.
            Throw ex
        Finally
            If Not pFileStream Is Nothing Then
                pFileStream.Close()
                pFileStream = Nothing
            End If
        End Try
    End Sub
#End Region

End Class
