Imports System.Collections.Specialized


'La clase ClElemFase (Elemento de Fase) nos servirá para guardar en la fase, el código del elemento 
'(recurso, subpartida o partida) que será asignado a la fase así como las excepciones que tendrá esta asignación.
'Por ejemplo, se puede asignar un recurso a la fase, pero decir que para la subpartida XXX no aplica esta regla
'por lo que el elemento quedaría asignado a la subpartida XXX y no a la fase. Del mismo modo, se puede establecer
'la excepción para una partida.

Public Class ClElemFase
    Private _codElem As String 'se registra el código/descripción del recurso, subpartida o partida que va a la fase
    Private _Excepto As StringCollection 'se registran los códigos o descripciones de la partida o subpartida

    Public Property CodElem As String
        Get
            Return _codElem
        End Get
        Set(value As String)
            _codElem = value
        End Set
    End Property

    Public Property Excepto As StringCollection
        Get
            Return _Excepto
        End Get
        Set(value As StringCollection)
            _Excepto = value
        End Set
    End Property

    Public Sub New(ByVal Cod As String)
        CodElem = Cod
        Excepto = New StringCollection
    End Sub
End Class
