Imports System.Collections.ObjectModel
Imports SCP

Public Class ClEspecialidad
    Private _nombre As String
    Private _moneda As String
    Private _cdirecto As Double 'registra el costo directo
    Private _lsPartidas As ObservableCollection(Of ClPartida) 'Partidas y metrados que forman el Presupuesto
    Private _lsSP As ObservableCollection(Of ClSp) 'Sub partidas por especialidad

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

    Public Property lsPartidas As ObservableCollection(Of ClPartida)
        Get
            Return _lsPartidas
        End Get
        Set(value As ObservableCollection(Of ClPartida))
            _lsPartidas = value
        End Set
    End Property

    Public Property LsSP As ObservableCollection(Of ClSp)
        Get
            Return _lsSP
        End Get
        Set(value As ObservableCollection(Of ClSp))
            _lsSP = value
        End Set
    End Property

    Public Sub New()
        lsPartidas = New ObservableCollection(Of ClPartida)
        LsSP = New ObservableCollection(Of ClSp)
    End Sub

    Public Function BuscaPartxCod(ByVal cod As String) As Integer
        Dim query = (From partida In _lsPartidas Where partida.Codigo = cod
                     Select partida)
        If query.Any Then
            Return _lsPartidas.IndexOf(query.First)
        Else Return -1
        End If
    End Function

    Public Function BuscaSPxNom(ByVal NomSP As String) As Integer
        Dim query = (From sp In _lsSP Where sp.Descripcion = NomSP
                     Select sp)
        If query.Any Then
            Return _lsSP.IndexOf(query.First)
        Else Return -1
        End If
    End Function

End Class
