Imports System.ComponentModel

<Serializable()>
Public Class ClRecurso
    Implements INotifyPropertyChanged 'Para poder usar el Binding

    Private _codigo As String
    Private _descripcion As String
    Private _und As String
    Private _tipo As String
    Private _cantidad As Double
    Private _precio As Double

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

    Public Property Tipo As String
        Get
            Return _tipo
        End Get
        Set(value As String)
            If value <> _tipo Then
                _tipo = value
                onPropertyChanged("Tipo")
            End If
        End Set
    End Property

    Public Property Cantidad As Double
        Get
            Return _cantidad
        End Get
        Set(value As Double)
            If value <> _cantidad Then
                _cantidad = value
                onPropertyChanged("Cantidad")
            End If
        End Set
    End Property

    Public Property Precio As Double
        Get
            Return _precio
        End Get
        Set(value As Double)
            If value <> _precio Then
                _precio = value
                onPropertyChanged("Precio")
            End If
        End Set
    End Property

    Public ReadOnly Property Parcial As Double
        Get
            If Left(_und, 1) = "%" Then
                Return _cantidad * _precio / 100
            Else
                Return _cantidad * _precio
            End If
        End Get
    End Property

    <NonSerialized()>
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub onPropertyChanged(ByVal Nombre As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(Nombre))
    End Sub

    Public Sub New(ByVal CodRec As String, ByVal Desc As String, ByVal und As String, ByVal Pre As Double,
                   ByVal Cant As Double, ByVal Tip As String)
        _codigo = CodRec
        _descripcion = Desc
        _und = und
        _tipo = Tip
        _precio = Pre
        _cantidad = Cant

    End Sub

    Public Sub New()

    End Sub
End Class
