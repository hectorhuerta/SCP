Imports Microsoft.Win32,
    System.Collections.Specialized


Class MainWindow
    Private Sub mSalir_Click(sender As Object, e As RoutedEventArgs) Handles mSalir.Click
        Application.Current.Shutdown()

    End Sub

    Private Sub mImportar_Click(sender As Object, e As RoutedEventArgs) Handles mImportar.Click
        Dim dlg As New ImportPresup
        Dim Espec As New ClEspecialidad

        If dlg.ShowDialog() Then
            dlg.Xlap.ImportaPresup("PRESUPUESTO", "C", "E", "F", "G", "H", 2, Espec, "")
            dlg.Xlap.ImportaPU("PARTIDAS", "D", "G", "G", "I", "B", "D", "K", "L", "M", "N", 10, "B", Espec, False)
            dlg.Xlap.ImportaPU("SUB PARTIDAS", "D", "G", "G", "I", "B", "D", "K", "L", "M", "N", 10, "B", Espec, True)
        End If


    End Sub
End Class
