Imports ServiceApp.Configuracion.EntidadesMDL

Public Class DatosServicioDAL
    Private _DataTable As DataTable
    Private _DatosServicioMDL As New DatosServicioMDL
    Private _DatosSistemas As New Dictionary(Of Integer, DatosServicioMDL)

#Region "New and Finalize"
    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByRef _DataSet As DataSet)
        MyBase.New()
        Try
            _DataTable = _DataSet.Tables("DatosServicio")
            If _DataTable Is Nothing Then
                InicializarTable(_DataSet)
            End If
        Catch ex As Exception
            InicializarTable(_DataSet)
        End Try
    End Sub

    Public Sub New(ByRef _DataSet As DataSet, ByRef pDataTable As DataTable)
        MyBase.New()
        _DataTable = pDataTable
        _DatosServicioMDL.IPServicio = _DataTable.Rows(0).Item("IPServicio").ToString
        _DatosServicioMDL.PuertoServicio = CInt(_DataTable.Rows(0).Item("PuertoServicio"))
        _DatosServicioMDL.IPRemota = _DataTable.Rows(0).Item("IPOtroSistema").ToString
        _DatosServicioMDL.PuertoRemoto = _DataTable.Rows(0).Item("PuertoOtroSistema").ToString
        _DatosServicioMDL.Sistema = CInt(_DataTable.Rows(0).Item("EquipoBackEnd"))
        _DatosServicioMDL.Identificador = CInt(_DataTable.Rows(0).Item("Identificador"))
        _DatosServicioMDL.UltFechaBorrado = _DataTable.Rows(0).Item("UltFechaBorrado")
        _DatosServicioMDL.CarpetaLog = _DataTable.Rows(0).Item("CarpetaLog").ToString
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
#End Region

#Region "Metodos"
    Private Sub InicializarTable(ByRef _DataSet As DataSet)
        _DataTable = New DataTable("DatosServicio")
        _DataTable.Columns.Add("IPServicio")
        _DataTable.Columns.Add("PuertoServicio")
        _DataTable.Columns.Add("IPOtroSistema")
        _DataTable.Columns.Add("PuertoOtroSistema")
        _DataTable.Columns.Add("EquipoBackEnd")
        _DataTable.Columns.Add("Identificador")
        _DataTable.Columns.Add("UltFechaBorrado")
        _DataTable.Rows.Add(_DataTable.NewRow)
        _DataTable.Rows(0).Item("IPServicio") = String.Empty
        _DataTable.Rows(0).Item("PuertoServicio") = -1
        _DataTable.Rows(0).Item("IPOtroSistema") = String.Empty
        _DataTable.Rows(0).Item("PuertoOtroSistema") = -1
        _DataTable.Rows(0).Item("EquipoBackEnd") = -1
        _DataTable.Rows(0).Item("Identificador") = -1
        _DataTable.Rows(0).Item("UltFechaBorrado") = Date.Now
        Try
            _DataTable.Rows(0).Item("CarpetaLog") = My.Application.Info.DirectoryPath + "\log"
        Catch ex As ArgumentException
            _DataTable.Columns.Add("CarpetaLog")
            _DataTable.Rows(0).Item("CarpetaLog") = My.Application.Info.DirectoryPath + "\log"
        End Try

        _DataSet.Tables.Add(_DataTable)
    End Sub

    Private Sub Actualizar()

        If _DatosSistemas.Count > 0 Then
            _DataTable.Rows.Clear()

            For Each iKey As Integer In _DatosSistemas.Keys
                Dim pDatosOtrosSistemasMDL As DatosServicioMDL = _DatosSistemas(iKey)
                _DataTable.Rows.Add(_DataTable.NewRow)

                _DataTable.Rows(iKey).Item("IPServicio") = pDatosOtrosSistemasMDL.IPServicio.ToString
                _DataTable.Rows(iKey).Item("PuertoServicio") = pDatosOtrosSistemasMDL.PuertoServicio
                _DataTable.Rows(iKey).Item("IPOtroSistema") = pDatosOtrosSistemasMDL.IPRemota.ToString
                _DataTable.Rows(iKey).Item("PuertoOtroSistema") = pDatosOtrosSistemasMDL.PuertoRemoto.ToString
                _DataTable.Rows(iKey).Item("EquipoBackEnd") = pDatosOtrosSistemasMDL.Sistema.ToString
                _DataTable.Rows(iKey).Item("Identificador") = pDatosOtrosSistemasMDL.Identificador.ToString
                _DataTable.Rows(iKey).Item("UltFechaBorrado") = pDatosOtrosSistemasMDL.UltFechaBorrado.ToString
            Next
        End If

        Try
            _DataTable.Rows(0).Item("CarpetaLog") = _DatosServicioMDL.CarpetaLog
        Catch ex As ArgumentException
            _DataTable.Columns.Add("CarpetaLog")
            _DataTable.Rows(0).Item("CarpetaLog") = _DatosServicioMDL.CarpetaLog

        End Try
    End Sub
#End Region

#Region "Property"
    Public Property DatosOtrosSistemas() As IDictionary(Of Integer, DatosServicioMDL)
        Get
            Dim iKey As Integer

            iKey = 0
            For Each pRow As DataRow In _DataTable.Rows
                Try
                    _DatosSistemas.Add(iKey, New DatosServicioMDL With {.IPServicio = _DataTable.Rows(iKey).Item("IPServicio").ToString, .PuertoServicio = CInt(_DataTable.Rows(iKey).Item("PuertoServicio").ToString), .IPRemota = _DataTable.Rows(iKey).Item("IPOtroSistema").ToString, .PuertoRemoto = CInt(_DataTable.Rows(iKey).Item("PuertoOtroSistema").ToString), .Sistema = CInt(_DataTable.Rows(iKey).Item("EquipoBackEnd").ToString), .Identificador = CInt(_DataTable.Rows(iKey).Item("Identificador").ToString), .UltFechaBorrado = IIf(_DataTable.Rows(iKey).Item("UltFechaBorrado").ToString = "", .UltFechaBorrado, _DataTable.Rows(iKey).Item("UltFechaBorrado")), .CarpetaLog = _DataTable.Rows(iKey).Item("CarpetaLog").ToString})
                Catch ex As Exception
                    _DatosSistemas.Add(iKey, New DatosServicioMDL With {.IPServicio = String.Empty, .PuertoServicio = -1, .IPRemota = String.Empty, .PuertoRemoto = -1, .Sistema = -1, .Identificador = -1, .UltFechaBorrado = Date.Now, .CarpetaLog = String.Empty})
                End Try
                iKey = iKey + 1
            Next

            Return _DatosSistemas
        End Get
        Set(ByVal value As IDictionary(Of Integer, DatosServicioMDL))
            _DatosSistemas = value
            Actualizar()
        End Set
    End Property

    Public Property DataTable As DataTable
        Get
            Return _DataTable
        End Get
        Set(value As DataTable)
            _DataTable = value
        End Set
    End Property

    Public Property DatosServicio() As DatosServicioMDL
        Get
            Try
                _DatosServicioMDL.IPServicio = _DataTable.Rows(0).Item("IPServicio").ToString
            Catch ex As Exception
                _DatosServicioMDL.IPServicio = String.Empty
            End Try

            Try
                _DatosServicioMDL.PuertoServicio = CInt(_DataTable.Rows(0).Item("PuertoServicio").ToString)
            Catch ex As Exception
                _DatosServicioMDL.PuertoServicio = -1
            End Try

            Try
                _DatosServicioMDL.IPRemota = _DataTable.Rows(0).Item("IPOtroSistema").ToString
            Catch ex As Exception
                _DatosServicioMDL.IPRemota = String.Empty
            End Try

            Try
                _DatosServicioMDL.PuertoRemoto = CInt(_DataTable.Rows(0).Item("PuertoOtroSistema").ToString)
            Catch ex As Exception
                _DatosServicioMDL.PuertoRemoto = -1
            End Try

            Try
                _DatosServicioMDL.Sistema = CInt(_DataTable.Rows(0).Item("EquipoBackEnd").ToString)
            Catch ex As Exception
                _DatosServicioMDL.Sistema = -1
            End Try

            Try
                _DatosServicioMDL.Identificador = CInt(_DataTable.Rows(0).Item("Identificador").ToString)
            Catch ex As Exception
                _DatosServicioMDL.Identificador = -1
            End Try

            Try
                _DatosServicioMDL.UltFechaBorrado = IIf(_DataTable.Rows(0).Item("UltFechaBorrado").ToString = "", _DatosServicioMDL.UltFechaBorrado, _DataTable.Rows(0).Item("UltFechaBorrado"))
            Catch ex As Exception
                _DatosServicioMDL.UltFechaBorrado = Date.Now
            End Try

            Try
                _DatosServicioMDL.CarpetaLog = _DataTable.Rows(0).Item("CarpetaLog").ToString
            Catch ex As Exception
                _DatosServicioMDL.CarpetaLog = My.Application.Info.DirectoryPath + "\log"
            End Try

            Return _DatosServicioMDL
        End Get
        Set(ByVal value As DatosServicioMDL)
            _DatosServicioMDL = value
            Actualizar()
        End Set
    End Property
#End Region

End Class
