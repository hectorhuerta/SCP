Imports System.ComponentModel, System.Collections.ObjectModel
Imports SCP

<Serializable()>
Public Class ClPartida
    Implements INotifyPropertyChanged 'Es necesario para usar el Binding

    Private _codigo As String 'Código interno de la partida
    Private _codcli As String 'Código de la partida dado por el Cliente
    Private _codFase As String 'Código de la Fase de Control o Centro de Costo
    Private _descripcion As String
    Private _und As String
    Private _metrado As Double
    Private _pu As Double
    Private _rdmo As Double
    Private _rdeq As Double
    Private _estaDetallada As Boolean 'Indica si se le ha aplicado la función DetallaPartida
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

    Public Property EstaDetallada As Boolean
        Get
            Return _estaDetallada
        End Get
        Set(value As Boolean)
            If value <> _estaDetallada Then
                _estaDetallada = value
                onPropertyChanged("EstaDetallada")
            End If
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
        EstaDetallada = False
        LrecApu = New ClApu
        Lsp = New ClPartidas
    End Sub

    Public Sub New(ByVal cod As String, ByVal desc As String, ByVal und As String, ByVal metrado As Double,
                   ByVal pu As Double, ByVal RdMo As Double, RdEq As Double)
        Me.New(cod, desc, und, metrado, pu)
        Me.Rdmo = RdMo
        Me.Rdeq = RdEq
        Me.EstaDetallada = False
    End Sub
    '<<< CalcularRecursos >>>
    'Función recursiva que se encarga de generar la lista de los recursos de la partida, desanidando
    'las subpartidas que contiene

    Private Shared Function CalcularRecursos(ByRef Part As ClPartida, ByRef BDSP As ClPartidas) As ClRecursos
        Dim lista As New ClRecursos
        Dim rec As ClRecurso, i As Integer
        Dim TemPu As ClApu = ClClonarObj.Clonar(Part.LrecApu)
        Dim BdTemp As New ClPartidas

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
    '<<< ListarSPs >>>
    'Función recursiva que genera un listado de las subpartidas de la partida, considerando el metrado que 
    'corresponda. No corrige los metrados calculados de las subpartidas

    Public Shared Function ListarSPs(ByRef Part As ClPartida, ByRef BDSP As ClPartidas) As ClPartidas
        Dim lista As New ClPartidas
        Dim BDTemp As New ClPartidas, i As Integer
        If Part.Lsp.Any Then
            For Each sp As ClPartida In Part.Lsp
                i = BDSP.BuscaPartxNom(sp.Descripcion)
                Dim spclone As ClPartida = ClClonarObj.Clonar(BDSP(i))
                BDTemp.Add(spclone)
                BDTemp.Last.Metrado = Part.Metrado * sp.Metrado
                BDTemp.Last.Pu = sp.Pu
            Next
            'Anexar lista de subpartidas
            lista.AnexarLista(BDTemp)
            For Each sp As ClPartida In BDTemp
                'Anexar lista de subpartidas
                lista.AnexarLista(ListarSPs(sp, BDSP))
            Next
        End If

        BDTemp.Clear()
        BDTemp = Nothing

        Return lista

    End Function
    '<<< ListarRecursos >>>
    'Función que genera la lista de los recursos de una partida, desanidando las subpartidas que pudiera
    'contener. Además corrige el error en las cantidades de recursos originado por las sucesivas operaciones de
    'recursividad. Usa la función recursiva CalcularRecursos

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
    '<<< DetallaPartida >>>
    'Función recursiva que acopla a la partida, todo el árbol de subpartidas con sus correspondientes
    'recursos y cantidades según los APU, con la finalidad de poder asignarles una Fase de Control o 
    'Centro de costo

    Public Shared Function DetallaPartida(ByRef Part As ClPartida, ByRef BDSP As ClPartidas) As ClPartida
        Dim i As Integer, BDTemp As New ClPartidas
        Dim PartDet As ClPartida = ClClonarObj.Clonar(Part)

        PartDet.Lsp.Clear()

        For Each sp As ClPartida In Part.Lsp
            i = BDSP.BuscaPartxNom(sp.Descripcion)
            Dim spClone As ClPartida = ClClonarObj.Clonar(BDSP(i))
            spClone.Metrado = sp.Metrado
            spClone.Pu = sp.Pu
            BDTemp.Add(spClone)
        Next

        If BDTemp.Any Then
            For Each sp As ClPartida In BDTemp
                PartDet.Lsp.Add(ClPartida.DetallaPartida(sp, BDSP))
            Next
        End If

        BDTemp.Clear()
        BDTemp = Nothing

        Part.EstaDetallada = True

        Return PartDet

    End Function
    '<<< AsignaFase >>>
    'Procedimiento recursivo que asigna la fase a la partida y propaga la asignación a todos los recursos y
    'subpartidas de su árbol. Se requiere que el parámetro Part, sea la partida detallada que se obtiene 
    'mediante la función DetallaPartida. El parámetro Prioridad puede ser 1 ó 2 dependiendo de si lo que se
    'fasea es una partida (p=1) o una subpartida (p=2)

    Public Shared Sub AsignaFase(ByVal CodFase As String, ByVal Prioridad As Integer, Part As ClPartida,
                                      ByRef BDSP As ClPartidas)

        Part.CodFase = CodFase

        For Each rec As ClRecApu In Part.LrecApu
            If Prioridad > rec.Prioridad Then
                rec.CodFase = CodFase
                rec.Prioridad = Prioridad
            End If
        Next

        For Each sp As ClPartida In Part.Lsp
            AsignaFase(CodFase, Prioridad, sp, BDSP)
        Next


    End Sub

End Class

