Imports Microsoft.Win32,
      System.Collections.Specialized
Imports SCP

Public Class ImportPresup
    Private _xlap As ClExcel

    Public Property Xlap As ClExcel
        Get
            Return _xlap
        End Get
        Set(value As ClExcel)
            _xlap = value
        End Set
    End Property

    Private Sub btnCancel_Click(sender As Object, e As RoutedEventArgs) Handles btnCancel.Click
        Me.Close()
        Xlap.CerrarExcel()
    End Sub

    Private Sub btnOd_Click(sender As Object, e As RoutedEventArgs) Handles btnOd.Click
        Dim od As New OpenFileDialog
        Dim str As New StringCollection

        With od
            .Title = "Seleccione archivo Excel que contiene el Presupuesto"
            .Filter = "Todos los archivos|*.*|Archivos Excel(*.xls,*.xlsx)|*.xls;*.xlsx"
            If .ShowDialog Then
                txbFileName.Text = .FileName
                Xlap = New ClExcel
                str = Xlap.DameHojas(.FileName)
                cbPresup.ItemsSource = str
                cbApu.ItemsSource = str
                cbSP.ItemsSource = str
            End If
        End With
    End Sub

    Private Sub btnAcep_Click(sender As Object, e As RoutedEventArgs) Handles btnAcep.Click
        DialogResult = New Boolean?(True)
    End Sub
End Class
