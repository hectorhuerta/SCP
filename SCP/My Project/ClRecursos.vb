Imports System.Collections.ObjectModel,
    System.ComponentModel


<Serializable>
Public Class ClRecursos
    Inherits ObservableCollection(Of ClRecurso)

    Public Function BuscaRecxCod(ByVal Cod As String) As Integer
        Dim query = (From rec In Items Where rec.Codigo = Cod Select rec)
        If query.Any Then
            Return Items.IndexOf(query.First)
        Else
            Return -1
        End If

    End Function

    Public Sub AnexaLista(ByRef Lista As ClRecursos)
        Dim i As Integer

        For Each rec As ClRecurso In Lista
            i = BuscaRecxCod(rec.Codigo)
            If i <> -1 Then
                If Left(Items(i).Und, 1) = "%" Then
                    Items(i).Precio = (Items(i).Cantidad * Items(i).Precio + rec.Cantidad * rec.Precio) /
                        (Items(i).Cantidad + rec.Cantidad)
                End If
                Items(i).Cantidad += rec.Cantidad 'Primero modificamos el precio del recurso und=% y luego la cantidad
            Else
                    Items.Add(rec)
            End If
        Next
    End Sub

    Public Function SubTotalRec() As Double
        Dim suma As Double = 0.0

        For Each rec As ClRecurso In Items
            suma += rec.Parcial
        Next
        Return suma
    End Function
End Class
