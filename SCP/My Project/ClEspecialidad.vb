Imports System.Collections.ObjectModel
Imports SCP
<Serializable>
Public Class ClEspecialidad
    Private _nombre As String
    Private _moneda As String
    Private _cdirecto As Double 'registra el costo directo
    Private _lsPartidas As ClPartidas  'Partidas y metrados de la especialidad
    Private _lsSP As ClPartidas  'Sub partidas de la especialidad
    Private _lRec As ClRecursos 'Listado de recursos de la especialidad

    Public Property Nombre As String
        Get
            Return _nombre
        End Get
        Set(value As String)
            _nombre = value
        End Set
    End Property

    Public Property Moneda As String
        Get
            Return _moneda
        End Get
        Set(value As String)
            _moneda = value
        End Set
    End Property

    Public Property Cdirecto As Double
        Get
            Return _cdirecto
        End Get
        Set(value As Double)
            _cdirecto = value
        End Set
    End Property

    Public Property lsPartidas As ClPartidas
        Get
            Return _lsPartidas
        End Get
        Set(value As ClPartidas)
            _lsPartidas = value
        End Set
    End Property

    Public Property LsSP As ClPartidas
        Get
            Return _lsSP
        End Get
        Set(value As ClPartidas)
            _lsSP = value
        End Set
    End Property

    Public Property LRec As ClRecursos
        Get
            Return _lRec
        End Get
        Set(value As ClRecursos)
            _lRec = value
        End Set
    End Property

    Public Sub New()
        lsPartidas = New ClPartidas
        LsSP = New ClPartidas
        LRec = New ClRecursos
    End Sub


End Class
