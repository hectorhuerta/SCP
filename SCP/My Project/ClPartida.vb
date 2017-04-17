Imports System.ComponentModel, System.Collections.ObjectModel
Imports SCP

<Serializable()>
Public Class ClPartida
    Implements INotifyPropertyChanged 'Es necesario para usar el Binding

    Private _codigo As String 'Código interno de la partida
    Private _codcli As String 'Código de la partida dado por el Cliente
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
            If value <> _codigo Then
                _codigo = value
                onPropertyChanged("Codigo")
            End If
        End Set
    End Property

    Public Property CodCli As String
        Get
            Return _codcli
        End Get
        Set(value As String)
            If value <> _codcli Then
                _codcli = value
                onPropertyChanged("CodCli")
            End If
        End Set
    End Property

    Public Property Descripcion As String
        Get
            Return _descripcion
        End Get
        Set(value As String)
            If value <> _descripcion Then
                _descripcion = value
                onPropertyChanged("Descripcion")
            End If
        End Set
    End Property

    Public Property Und As String
        Get
            Return _und
        End Get
        Set(value As String)
            If value <> _und Then
                _und = value
                onPropertyChanged("Und")
            End If
        End Set
    End Property

    Public Property Metrado As Double
        Get
            Return _metrado
        End Get
        Set(value As Double)
            If value <> _metrado Then
                _metrado = value
                onPropertyChanged("Metrado")
            End If
        End Set
    End Property

    Public Property Pu As Double
        Get
            Return _pu
        End Get
        Set(value As Double)
            If value <> _pu Then
                _pu = value
                onPropertyChanged("Pu")
            End If
        End Set
    End Property

    Public Property Rdmo As Double
        Get
            Return _rdmo
        End Get
        Set(value As Double)
            If value <> _rdmo Then
                _rdmo = value
                onPropertyChanged("Rdmo")
            End If
        End Set
    End Property

    Public Property Rdeq As Double
        Get
            Return _rdeq
        End Get
        Set(value As Double)
            If value <> _rdeq Then
                _rdeq = value
                onPropertyChanged("Rdeq")
            End If
        End Set
    End Property

    Public ReadOnly Property Parcial As Double
        Get
            Return _metrado * _pu
        End Get
    End Property

    Public ReadOnly Property ParcialCalc As Double
        Get
            Return CalculaParcial()
        End Get
    End Property

    Public ReadOnly Property NumSP As Integer
        Get
            Return Lsp.Count
        End Get
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

    <NonSerialized()>
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub onPropertyChanged(ByVal Nombre As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(Nombre))
    End Sub

    Public Sub New(ByVal cod As String, ByVal desc As String, ByVal und As String, ByVal metrado As Double,
                   ByVal pu As Double)
        _codcli = cod 'cuando el cliente brinda código, el código interno es igual al codigo cliente
        _codigo = cod
        _descripcion = desc
        _und = und
        _metrado = metrado
        _pu = pu
        Lrec = New ClApu
        Lsp = New ObservableCollection(Of ClSp)
    End Sub

    Public Sub AgregaRecApu(ByVal CodRec As String, ByVal Desc As String, und As String, ByVal Cant As Double,
                            ByVal Cuad As Double, ByVal Prec As Double, ByVal Tip As String)
        Dim Rec As ClRecApu

        Rec = New ClRecApu(CodRec, Desc, und, Cant, Cuad, Prec, Tip)
        Lrec.Add(Rec)
    End Sub

    Public Function CalculaParcial() As Double
        Dim rec As New ClRecApu()
        Dim sp As ClSp

        Dim suma As Double

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


End Class
