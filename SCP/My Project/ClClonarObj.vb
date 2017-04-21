Imports System.Runtime.Serialization,
    System.Runtime.Serialization.Formatters.Binary,
    System.IO


Public Class ClClonarObj
    Public Shared Function Clonar(Of T)(ObjToClone As T) As T
        If Object.ReferenceEquals(ObjToClone, Nothing) Then
            Return ObjToClone
        End If

        Dim formatter As New BinaryFormatter(Nothing, New StreamingContext(StreamingContextStates.Clone))
        Dim ms As New MemoryStream
        Using ms
            formatter.Serialize(ms, ObjToClone)
            ms.Seek(0, SeekOrigin.Begin)
            Return CType(formatter.Deserialize(ms), T)
        End Using

    End Function
End Class
