Imports System.Collections.ObjectModel

<Serializable>
Public Class ClEspecialidades
    Inherits ObservableCollection(Of ClEspecialidad)

    Public ReadOnly Property ResumenEspec As IEnumerable(Of ClDataEspec)
        Get
            Return ResumEspec()
        End Get
    End Property

    Private Function ResumEspec() As IEnumerable(Of ClDataEspec)
        Dim query = From espec In Items Select New ClDataEspec With {.Nombre = espec.Nombre,
                                            .Moneda = espec.Moneda, .CDirecto = espec.Cdirecto}

        Return query

    End Function

End Class
