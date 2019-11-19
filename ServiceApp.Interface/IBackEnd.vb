Imports ServiceApp
Public Interface IBackEnd

    Event FichadaOnlineEvent(ByVal pFecha As String, ByVal pId As String, ByVal pTpMovimiento As String)

    Sub CambioFechaHora(ByVal pFecha As Date)
    Sub Borrado()
    Sub InhabilitacionTotal()
    Sub AltaTarjeta(ByVal pId As String)
    Sub BajaTarjeta(ByVal pId As String)

    Sub Conectar()
    Sub Desconectar()

    Property IP As String
    Property Port As Integer
    Property Identificador As Integer
    Property Conectado As Boolean

    Function CantidadFichadas() As Integer
    Function FechaHoraUltInicializacion() As Date
    Function Lectura() As String
    Function PrepararLectura() As Boolean

End Interface
