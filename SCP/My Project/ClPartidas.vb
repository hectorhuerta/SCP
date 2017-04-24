Imports System.Collections.ObjectModel

<Serializable>
Public Class ClPartidas
    Inherits ObservableCollection(Of ClPartida)

    Public Function BuscaPartxNom(ByVal Nom As String) As Integer
        Dim query = (From part In Items Where part.Descripcion = Nom Select part)
        If query.Any Then
            Return Items.IndexOf(query.First)
        Else
            Return -1
        End If
    End Function

    Public Function BuscaPartxCod(ByVal Cod As String) As Integer
        Dim query = (From part In Items Where part.Codigo = Cod Select part)
        If query.Any Then
            Return Items.IndexOf(query.First)
        Else
            Return -1
        End If
    End Function

    Public Sub AnexarLista(ByRef LPartidas As ClPartidas)
        Dim i As Integer
        For Each part As ClPartida In LPartidas
            i = BuscaPartxNom(part.Descripcion)
            If i <> -1 Then
                Items(i).Metrado += part.Metrado
            Else
                Items.Add(part)
            End If
        Next
    End Sub

End Class
