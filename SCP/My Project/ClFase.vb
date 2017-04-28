Imports System.ComponentModel
Imports SCP

Public Class ClFase
    Implements INotifyPropertyChanged

    Private _idfase As String
    Private _nombre As String
    Private _alcance As String
    Private _metrado As Double
    Private _und As String
    Private _lrec As ClElemsFase

    Public Property Idfase As String
        Get
            Return _idfase
        End Get
        Set(value As String)
            If value <> _idfase Then
                _idfase = value
                onPropertyChanged("IdFase")
            End If
        End Set
    End Property

    Public Property Nombre As String
        Get
            Return _nombre
        End Get
        Set(value As String)
            If value <> _nombre Then
                _nombre = value
                onPropertyChanged("Nombre")
            End If
        End Set
    End Property

    Public Property Alcance As String
        Get
            Return _alcance
        End Get
        Set(value As String)
            If value <> _alcance Then
                _alcance = value
                onPropertyChanged("Alcance")
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

    Public Property Lrec As ClElemsFase
        Get
            Return _lrec
        End Get
        Set(value As ClElemsFase)
            _lrec = value
        End Set
    End Property

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub onPropertyChanged(ByVal Nombre As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(Nombre))
    End Sub

    Public Sub New(ByVal CodFase As String, ByVal Descrip As String, ByVal Alc As String, ByVal Met As Double,
                   ByVal Und As String)
        Idfase = CodFase
        Nombre = Descrip
        Alcance = Alc
        Metrado = Met
        Me.Und = Und
        Lrec = New ClElemsFase

    End Sub


End Class
