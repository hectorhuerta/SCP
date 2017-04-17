Public Class ClRecApu
    Inherits ClRecurso
    Private _cuadrilla As Double

    Property Cuadrilla As Double
        Get
            Return _cuadrilla
        End Get
        Set(value As Double)
            If value <> _cuadrilla Then
                _cuadrilla = value
                onPropertyChanged("Cuadrilla")
            End If
        End Set
    End Property

    Public Sub New(ByVal cod As String, ByVal descr As String, ByVal und As String, ByVal cant As Double,
                   ByVal cuad As Double, ByVal prec As Double, ByVal tip As String)
        MyBase.New(cod, descr, und, prec, cant, tip)
        _cuadrilla = cuad
    End Sub

    Public Sub New()
        MyBase.New

    End Sub
End Class
