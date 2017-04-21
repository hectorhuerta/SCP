Imports System.ComponentModel, System.Collections.ObjectModel
Imports SCP

<Serializable()>
Public Class ClPartida
    Implements INotifyPropertyChanged 'Es necesario para usar el Binding

    ' Private _espec As ClEspecialidad 'Se registra la Especialidad a la que pertenece
    Private _codigo As String 'Código interno de la partida
    Private _codcli As String 'Código de la partida dado por el Cliente
    Private _descripcion As String
    Private _und As String
    Private _metrado As Double
    Private _pu As Double
    Private _rdmo As Double
    Private _rdeq As Double
    Private _lrecApu As ClApu  'Listado de recursos del apu, incluye el campo cuadrilla
    Private _lsp As ClPartidas  'Listado de subpartidas de la partida

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

    Public Property LrecApu As ClApu
        Get
            Return _lrecApu
        End Get
        Set(value As ClApu)
            _lrecApu = value
        End Set
    End Property

    Public Property Lsp As ClPartidas
        Get
            Return _lsp
        End Get
        Set(value As ClPartidas)
            _lsp = value
        End Set
    End Property

    '   Public Property Espec As ClEspecialidad
    '   Get
    '  Return _espec
    ' End Get
    'Set(value As ClEspecialidad)
    '       _espec = value
    'End Set
    'End Property

    <NonSerialized()>
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub onPropertyChanged(ByVal Nombre As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(Nombre))
    End Sub

    Public Sub New(ByVal cod As String, ByVal desc As String, ByVal und As String, ByVal metrado As Double,
                   ByVal pu As Double, ByRef Espec As ClEspecialidad)
        _codcli = cod 'cuando el cliente brinda código, el código interno es igual al codigo cliente
        _codigo = cod
        _descripcion = desc
        _und = und
        _metrado = metrado
        _pu = pu
        '  Me.Espec = Espec
        LrecApu = New ClApu
        Lsp = New ClPartidas
    End Sub

    Public Sub New(ByVal cod As String, ByVal desc As String, ByVal und As String, ByVal metrado As Double,
                   ByVal pu As Double, ByRef Espec As ClEspecialidad, ByVal RdMo As Double, RdEq As Double)
        Me.New(cod, desc, und, metrado, pu, Espec)
        Me.Rdmo = RdMo
        Me.Rdeq = RdEq
    End Sub

    Private Shared Function CalcularRecursos(ByRef Part As ClPartida, ByRef BDSP As ClPartidas) As ClRecursos
        Dim lista As New ClRecursos
        Dim rec As ClRecurso, i As Integer
        Dim TemPu As ClApu = ClClonarObj.Clonar(Part.LrecApu)
        Dim BdTemp As New ClPartidas


        'For i = 0 To Part.LrecApu.Count - 1
        ' TemPu.Add(Part.LrecApu(i))
        ' Next

        For Each recApu As ClRecApu In TemPu
            recApu.Cantidad *= Part.Metrado
            rec = recApu
            lista.Add(rec)
        Next

        If Part.Lsp.Any Then
            For Each sp As ClPartida In Part.Lsp
                i = BDSP.BuscaPartxNom(sp.Descripcion)
                Dim spclone As ClPartida = ClClonarObj.Clonar(BDSP(i))
                BdTemp.Add(spclone)
                BdTemp.Last.Metrado = sp.Metrado * Part.Metrado
            Next
        End If

        If Part.Lsp.Any Then
            For Each sp As ClPartida In BdTemp
                lista.AnexaLista(CalcularRecursos(sp, BDSP))
            Next
        End If

        TemPu.Clear()
        TemPu = Nothing

        BdTemp.Clear()
        BdTemp = Nothing

        Return lista

    End Function

    Public Shared Function ListarRecursos(ByRef Part As ClPartida, ByRef BDSP As ClPartidas) As ClRecursos
        Dim listaRec As New ClRecursos, i As Integer, j As Integer
        Dim Correcion As Double, parcial As Double

        listaRec = CalcularRecursos(Part, BDSP)
        parcial = listaRec.SubTotalRec
        i = 0
        Do Until (Math.Abs(Part.Parcial - parcial) <= ErrorRed) Or (i >= MaxIteraciones)
            Correcion = Part.Parcial / parcial
            For Each rec As ClRecurso In listaRec
                rec.Cantidad *= Correcion
            Next
            parcial = listaRec.SubTotalRec
            i += 1
        Loop

        Return listaRec

    End Function

End Class
