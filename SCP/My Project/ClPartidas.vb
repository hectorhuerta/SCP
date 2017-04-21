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

End Class
