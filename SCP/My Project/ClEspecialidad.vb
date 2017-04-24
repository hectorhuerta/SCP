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

    Public ReadOnly Property LRec As ClRecursos
        Get
            If _lRec.Any Then
                Return _lRec
            Else
                Return ListarRecursos()
            End If
        End Get
    End Property

    Public ReadOnly Property TotMo As Double
        Get
            Dim query = (From rec In LRec Where rec.Tipo = TMO Select rec)
            Return query.Sum(Function(rec) rec.Parcial)
        End Get
    End Property

    Public ReadOnly Property TotMa As Double
        Get
            Dim query = (From rec In LRec Where rec.Tipo = TMat Select rec)
            Return query.Sum(Function(rec) rec.Parcial)
        End Get
    End Property

    Public ReadOnly Property TotEq As Double
        Get
            Dim query = (From rec In LRec Where rec.Tipo = TEQ Select rec)
            Return query.Sum(Function(rec) rec.Parcial)
        End Get
    End Property

    Public ReadOnly Property TotSc As Double
        Get
            Dim query = (From rec In LRec Where rec.Tipo = TSC Select rec)
            Return query.Sum(Function(rec) rec.Parcial)
        End Get
    End Property

    Public Sub New()
        lsPartidas = New ClPartidas
        LsSP = New ClPartidas
        _lRec = New ClRecursos
    End Sub

    Private Function ListarRecursos() As ClRecursos
        Dim temp As New ClRecursos
        For Each part As ClPartida In lsPartidas
            temp.AnexaLista(ClPartida.ListarRecursos(part, LsSP))
        Next
        _lRec = temp
        Return temp
    End Function

    Private Function ListarSP() As ClPartidas
        Dim temp As New ClPartidas
        For Each part As ClPartida In lsPartidas
            temp.AnexarLista(ClPartida.ListarSPs(part, LsSP))
        Next
        Return temp

    End Function

    Public ReadOnly Property ListarSubPartidas As ClPartidas
        Get
            Return ListarSP()
        End Get
    End Property

End Class
