﻿Imports System.ComponentModel,
    System.Collections.ObjectModel
Imports SCP

<Serializable()>
Public Class ClProy
    Implements INotifyPropertyChanged 'Es necesario para usar el Binding
    Private _codigo As String
    Private _nombre As String
    Private _ubicacion As String
    Private _contratista As String
    Private _supervisor As String
    Private _cliente As String
    Private _numContrat As String
    Private _monContrat As Double
    Private _finicio As Date
    Private _plazo As Single
    Private _especialidades As Boolean 'Indica si tiene o nó especialidades
    Private _lespec As ObservableCollection(Of ClEspecialidad)

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

    Public Property Ubicacion As String
        Get
            Return _ubicacion
        End Get
        Set(value As String)
            If value <> _ubicacion Then
                _ubicacion = value
                onPropertyChanged("Ubicacion")
            End If
        End Set
    End Property

    Public Property Contratista As String
        Get
            Return _contratista
        End Get
        Set(value As String)
            If value <> _contratista Then
                _contratista = value
                onPropertyChanged("Contratista")
            End If
        End Set
    End Property

    Public Property Supervisor As String
        Get
            Return _supervisor
        End Get
        Set(value As String)
            If value <> _supervisor Then
                _supervisor = value
                onPropertyChanged("Supervisor")
            End If
        End Set
    End Property

    Public Property Cliente As String
        Get
            Return _cliente
        End Get
        Set(value As String)
            If value <> _cliente Then
                _cliente = value
                onPropertyChanged("Cliente")
            End If
        End Set
    End Property

    Public Property NumContrat As String
        Get
            Return _numContrat
        End Get
        Set(value As String)
            If value <> _numContrat Then
                _numContrat = value
                onPropertyChanged("NumContrat")
            End If
        End Set
    End Property

    Public Property MonContrat As Double
        Get
            Return _monContrat
        End Get
        Set(value As Double)
            If value <> _monContrat Then
                _monContrat = value
                onPropertyChanged("MonContrat")
            End If
        End Set
    End Property

    Public Property FInicio As Date
        Get
            Return _finicio
        End Get
        Set(value As Date)
            If value <> _finicio Then
                _finicio = value
                onPropertyChanged("FInicio")
            End If
        End Set
    End Property

    Public Property Plazo As Single
        Get
            Return _plazo
        End Get
        Set(value As Single)
            If value <> _plazo Then
                _plazo = value
                onPropertyChanged("Plazo")
            End If
        End Set
    End Property
    Public ReadOnly Property FFin As Date
        Get
            Return FInicio.AddDays(_plazo - 1)
        End Get

    End Property

    Public Property Especialidades As Boolean
        Get
            Return _especialidades
        End Get
        Set(value As Boolean)
            If value <> _especialidades Then
                _especialidades = value
                onPropertyChanged("Especialidades")
            End If
        End Set
    End Property

    Public Property Lespec As ObservableCollection(Of ClEspecialidad)
        Get
            Return _lespec
        End Get
        Set(value As ObservableCollection(Of ClEspecialidad))
            _lespec = value
        End Set
    End Property

    <NonSerialized()>
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub onPropertyChanged(ByVal Nombre As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(Nombre))
    End Sub
End Class
