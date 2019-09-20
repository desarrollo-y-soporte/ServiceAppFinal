Imports ServiceApp.Definiciones
Imports ServiceApp.Interface

Public Class Traductor
    Implements IDisposable

    Private _PathName As String
    Private _DataSet As DataSet
    Private _DataTable As DataTable
    Private _Sistemas As BackendEnum
    Private _TraductorMDL As New Dictionary(Of Integer, TraductorMDL)


#Region "New and Finalize"
    Public Sub New()
        _PathName = My.Application.Info.DirectoryPath + "\traductor.xml"
        LoadFile()
    End Sub

    Public Sub New(ByVal pSistemas As Integer)
        _Sistemas = pSistemas
        _PathName = My.Application.Info.DirectoryPath + "\traductor.xml"
        LoadFile()
    End Sub

    Public Sub New(ByVal pPath As String)
        If pPath <> "" Then
            _PathName = pPath
            LoadFile()
        End If

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
#End Region

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

#Region "Property"
    Public Property PathName As String
        Get
            Return _PathName
        End Get
        Set(value As String)
            _PathName = value
        End Set
    End Property

    Public Property DataSet As DataSet
        Get
            Return _DataSet
        End Get
        Set(value As DataSet)
            _DataSet = value
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

    Public Property Sistemas As Integer
        Get
            Return _Sistemas
        End Get
        Set(value As Integer)
            _Sistemas = value
        End Set
    End Property

    Public Property TraductorMDL As Dictionary(Of Integer, TraductorMDL)
        Get
            Return _TraductorMDL
        End Get
        Set(value As Dictionary(Of Integer, TraductorMDL))
            _TraductorMDL = value
        End Set
    End Property



#End Region

#Region "Metodos"
    Private Sub LoadFile()
        Dim pFileStream As System.IO.FileStream = Nothing

        Try
            pFileStream = New System.IO.FileStream(_PathName, System.IO.FileMode.OpenOrCreate)

            _DataSet = New DataSet
            _DataSet.ReadXml(pFileStream)
            _DataTable = _DataSet.Tables("Tarjeta")

            Dim iKey As Integer

            iKey = 0
            For Each pRow As DataRow In _DataTable.Rows
                Try
                    _TraductorMDL.Add(iKey, New TraductorMDL With {.Original = _DataTable.Rows(iKey).Item("Original").ToString, .Dimmep = _DataTable.Rows(iKey).Item("Dimmep").ToString,
                                      .Dummy = _DataTable.Rows(iKey).Item("Dummy").ToString, .ZKTeco = _DataTable.Rows(iKey).Item("ZKTeco").ToString})
                Catch ex As Exception
                    _TraductorMDL.Add(iKey, New TraductorMDL With {.Original = String.Empty, .Dimmep = String.Empty, .Dummy = String.Empty, .ZKTeco = String.Empty})
                End Try
                iKey = iKey + 1
            Next

        Catch ex As Xml.XmlException
            Throw ex
        Finally
            If Not pFileStream Is Nothing Then
                pFileStream.Close()
                pFileStream = Nothing
            End If
        End Try
    End Sub

    Public Function Buscar(ByVal pBuscar As String) As String
        Dim pValor As String
        Dim pTraductorMDL As New TraductorMDL

        pValor = ""
        Select Case _Sistemas
            Case BackendEnum.Dimmep
                If _TraductorMDL.Where(Function(x) x.Value.Dimmep.Equals(pBuscar)).Count > 0 Then
                    pTraductorMDL = _TraductorMDL.Where(Function(x) x.Value.Dimmep.Equals(pBuscar))
                    pValor = pTraductorMDL.Original
                Else
                    pValor = pBuscar
                End If

            Case BackendEnum.Dummy
                If _TraductorMDL.Where(Function(x) x.Value.Dummy.Equals(pBuscar)).Count > 0 Then
                    pTraductorMDL = _TraductorMDL.Where(Function(x) x.Value.Dummy.Equals(pBuscar))
                    pValor = pTraductorMDL.Original
                Else
                    pValor = pBuscar
                End If

            Case BackendEnum.ZKTeco
                If _TraductorMDL.Where(Function(x) x.Value.ZKTeco.Equals(pBuscar)).Count > 0 Then
                    pTraductorMDL = _TraductorMDL.Where(Function(x) x.Value.ZKTeco.Equals(pBuscar))
                    pValor = pTraductorMDL.Original
                Else
                    pValor = pBuscar
                End If
            Case BackendEnum.SistemaCentral
                If _TraductorMDL.Where(Function(x) x.Value.Original.Equals(pBuscar)).Count > 0 Then
                    pTraductorMDL = _TraductorMDL.Where(Function(x) x.Value.Original.Equals(pBuscar))
                    pValor = pTraductorMDL.Original
                Else
                    pValor = pBuscar
                End If

        End Select

        Return pValor
    End Function
#End Region
End Class


Public Class TraductorMDL
    Implements IDisposable

    Private _Original As String
    Private _Dimmep As String
    Private _Dummy As String
    Private _ZKTeco As String


#Region "New and Finalize"
    Public Sub New()
        _Original = String.Empty
        _Dimmep = String.Empty
        _Dummy = String.Empty
        _ZKTeco = String.Empty
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
#End Region

#Region "property"
    Public Property Original As String
        Get
            Return _Original
        End Get
        Set(value As String)
            _Original = value
        End Set
    End Property

    Public Property Dimmep As String
        Get
            Return _Dimmep
        End Get
        Set(value As String)
            _Dimmep = value
        End Set
    End Property

    Public Property Dummy As String
        Get
            Return _Dummy
        End Get
        Set(value As String)
            _Dummy = value
        End Set
    End Property

    Public Property ZKTeco As String
        Get
            Return _ZKTeco
        End Get
        Set(value As String)
            _ZKTeco = value
        End Set
    End Property
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub

#End Region
End Class
