Imports System.ComponentModel

'El campo _prioridad, establece la prioridad que tiene la fase de control asignada al recurso. Esta puede tomar
'los valores 1, 2 ó 3. Siendo 3 la de mayor peso. La fase de control se puede establecer a nivel de partida (con
'una prioridad de 1), a nivel de subpartida (con una prioridad 2) y a nivel de recurso (con prioridad 3). Esto
'permitirá asignar un recurso a una fase (p=3) y cuando la partida que lo contiene se asigne a otra fase (p=1)
'prevalecerá la asignación al recurso sobre la partida

<Serializable()>
Public Class ClRecurso
    Implements INotifyPropertyChanged 'Para poder usar el Binding

    Private _codigo As String
    Private _codFase As String
    Private _prioridad As Integer 'Prioridad de la asignación de la fase de control (1,2 o 3)
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

    Public Property CodFase As String
        Get
            Return _codFase
        End Get
        Set(value As String)
            If value <> _codFase Then
                _codFase = value
                onPropertyChanged("CodFase")
            End If
        End Set
    End Property

    Public Property Prioridad As Integer
        Get
            Return _prioridad
        End Get
        Set(value As Integer)
            If value <> _prioridad Then
                _prioridad = value
                onPropertyChanged("Prioridad")
            End If
        End Set
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
        _prioridad = 0 'Prioridad no asignada
    End Sub

    Public Sub New()
        _prioridad = 0 'Prioridad no asignada
    End Sub
End Class
