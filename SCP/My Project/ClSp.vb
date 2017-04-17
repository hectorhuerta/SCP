Imports System.Collections.ObjectModel
Imports SCP

Public Class ClSp
    Private _codigo As String
    Private _descripcion As String
    Private _und As String
    Private _metrado As Double
    Private _pu As Double
    Private _rdmo As Double
    Private _rdeq As Double
    Private _lrec As ClApu
    Private _lsp As ObservableCollection(Of ClSp)

    Public Property Codigo As String
        Get
            Return _codigo
        End Get
        Set(value As String)
            _codigo = value
        End Set
    End Property

    Public Property Descripcion As String
        Get
            Return _descripcion
        End Get
        Set(value As String)
            _descripcion = value
        End Set
    End Property

    Public Property Und As String
        Get
            Return _und
        End Get
        Set(value As String)
            _und = value
        End Set
    End Property

    Public Property Metrado As Double
        Get
            Return _metrado
        End Get
        Set(value As Double)
            _metrado = value
        End Set
    End Property

    Public Property Pu As Double
        Get
            Return _pu
        End Get
        Set(value As Double)
            _pu = value
        End Set
    End Property

    Public Property Rdmo As Double
        Get
            Return _rdmo
        End Get
        Set(value As Double)
            _rdmo = value
        End Set
    End Property

    Public Property Rdeq As Double
        Get
            Return _rdeq
        End Get
        Set(value As Double)
            _rdeq = value
        End Set
    End Property

    Public Property Lrec As ClApu
        Get
            Return _lrec
        End Get
        Set(value As ClApu)
            _lrec = value
        End Set
    End Property

    Public Property Lsp As ObservableCollection(Of ClSp)
        Get
            Return _lsp
        End Get
        Set(value As ObservableCollection(Of ClSp))
            _lsp = value
        End Set
    End Property

    Public ReadOnly Property NumSP As Integer
        Get
            Return Lsp.Count
        End Get
    End Property

    Public Function CalculaParcial() As Double
        Dim suma As Double
        Dim rec As New ClRecApu
        Dim sp As ClSp

        suma = 0.0
        For Each rec In Lrec
            suma += rec.Parcial
        Next
        If NumSP <> 0 Then
            For Each sp In Lsp
                suma += sp.CalculaParcial
            Next
        End If
        Return suma
    End Function

    Public Function DameListaRecursos() As ClRecursos
        Dim rec As New ClRecurso
        Dim _ltemp As New ClRecursos
        Dim sp As ClSp

        For Each rec In Lrec
            rec.Cantidad = Metrado * rec.Cantidad
            _ltemp.Add(rec)
        Next

        Return _ltemp

        If NumSP <> 0 Then
            For Each sp In Lsp


            Next


        End If

    End Function

    Public Sub New(ByVal Cod As String, ByVal Descrip As String, ByVal Und As String, ByVal Cant As Double,
                   ByVal Pu As Double, ByVal RMo As Double, ByVal REq As Double)
        Codigo = Cod
        Descripcion = Descrip
        Me.Und = Und
        Metrado = Cant
        Me.Pu = Pu
        Rdmo = RMo
        Rdeq = REq

        Lrec = New ClApu
        Lsp = New ObservableCollection(Of ClSp)
    End Sub
End Class
