Imports Excel = Microsoft.Office.Interop.Excel,
    System.Collections.Specialized,
    System.Collections.ObjectModel,
    System.IO


Public Class ClExcel
    Private xlApp As Excel.Application
    Property xlwbook As Excel.Workbook

    Private Function LeeCelda(ByRef Hoja As Excel.Worksheet, ByVal Col As String, ByVal Fil As Integer) As String
        Dim celda As String

        celda = Col + Fil.ToString
        Return Convert.ToString(Hoja.Range(celda).Value)

    End Function

    Private Function SeleccionaHoja(ByVal NomHoja As String) As Excel.Worksheet
        Dim i As Integer
        Dim xlsheet As Excel.Worksheet

        For i = 0 To xlwbook.Sheets.Count - 1
            xlsheet = CType(xlwbook.Sheets(i + 1), Excel.Worksheet)
            If xlsheet.Name = NomHoja Then
                Return xlsheet
                Exit Function
            End If
        Next
    End Function

    Private Function ConvDouble(ByRef xls As Excel.Worksheet, ByVal ColCamp As String, ByVal Fila As Integer) As Double
        Dim tmp As String
        Dim camp As Double

        tmp = LeeCelda(xls, ColCamp, Fila)
        If tmp = "" Then
            Camp = 0.0
        Else
            Camp = Convert.ToDouble(tmp)
        End If
        Return Camp
    End Function

    Public Function DameHojas(ByVal fName As String) As StringCollection
        Dim i As Integer
        Dim xlsheet As Excel.Worksheet
        Dim temp As New StringCollection

        xlApp.Workbooks.Open(fName)
        xlwbook = xlApp.Workbooks(Path.GetFileName(fName))
        For i = 0 To xlwbook.Sheets.Count - 1
            xlsheet = CType(xlwbook.Sheets(i + 1), Excel.Worksheet)
            temp.Add(xlsheet.Name)
        Next

        Return temp

    End Function

    Public Sub New()
        xlApp = New Excel.Application
        xlApp.Visible = True
        If xlApp Is Nothing Then
            MessageBox.Show("Excel no está instalado en su Sistema")
            Application.Current.Shutdown()
        End If
    End Sub

    Public Sub CerrarExcel()
        xlApp.Quit()
        xlApp = Nothing
    End Sub

    Private Function EvalEncab(Dato As String) As Boolean
        Return (UCase(Dato) = UCase(TMat)) OrElse (UCase(Dato) = UCase(TMO)) OrElse (UCase(Dato) = UCase(TEQ)) _
            OrElse (UCase(Dato) = UCase(TSP)) OrElse (UCase(Dato) = UCase(TSC))
    End Function

    Public Sub ImportaPresup(ByVal NomHoja As String, ByVal ColCod As String, ByVal ColDes As String,
                             ByVal ColUnd As String, ByVal ColMet As String, ByVal ColPu As String,
                             ByVal FilIni As Integer, ByRef Espec As ClEspecialidad, ByVal NomEspec As String)

        Dim Cod As String, Descrip As String, Und As String, Metrado As Double, PU As Double
        Dim xlsheet As New Excel.Worksheet
        Dim i As Integer, Part As ClPartida

        Cod = ""
        Descrip = " "
        Und = ""
        Metrado = 0.0
        PU = 0.0
        i = FilIni

        Espec.Nombre = NomEspec
        xlsheet = SeleccionaHoja(NomHoja)

        Do Until Descrip = ""
            Descrip = LeeCelda(xlsheet, ColDes, i)
            Cod = LeeCelda(xlsheet, ColCod, i)
            Und = LeeCelda(xlsheet, ColUnd, i)

            Metrado = ConvDouble(xlsheet, ColMet, i)
            PU = ConvDouble(xlsheet, ColPu, i)

            Part = New ClPartida(Cod, Descrip, Und, Metrado, PU)
            Espec.lsPartidas.Add(Part)
            i += 1
        Loop

    End Sub

    Public Sub ImportaPU(ByVal NomHoja As String, ByVal ColCod As String, ByVal ColDes As String,
                         ByVal ColRMo As String, ByVal ColREq As String, ByVal ColDesRec As String,
                         ByVal ColUnd As String, ByVal ColCuad As String, ByVal ColCant As String,
                         ByVal ColPre As String, ByVal FIni As Integer, ByVal ColIni As String,
                         ByRef Espec As ClEspecialidad)

        Dim Cod As String, DescPart As String, RMo As Double, REq As Double, DescRec As String
        Dim Und As String, Cuadrilla As Double, Cantidad As Double, Precio As Double, i As Integer, j As Integer
        Dim FilBlanc As Integer
        Dim xlsheet As New Excel.Worksheet, apu As ClApu, Recurso As ClRecApu, Sp As ClSp
        Dim ListaSP As ObservableCollection(Of ClSp)
        Dim NuevaPartida As String, TipoRec As String

        'FilBlanc, es un contador de filas blancas para la lectura de los APU
        'FilBlanc tiene que ser menor que el máximo de filas blancas en la columna descripción de recursos 
        'de la hoja de APU del Presupuesto que se quiere leer.
        'Usualmente las filas blancas no exceden a 3 en los libros exportados con S10
        'El sistema deja de buscar análisis para importar, cuando FilBlanc es mayor que MaxFil 
        '(constante definida en el módulo TGlo)
        'NApu es una constante definida en TGlo

        FilBlanc = 0
        Cod = ""
        DescPart = ""
        RMo = 0.0
        REq = 0.0
        DescRec = ""
        Und = ""
        Cuadrilla = 0.0
        Cantidad = 0.0
        Precio = 0.0
        i = FIni
        j = 0
        TipoRec = ""

        xlsheet = SeleccionaHoja(NomHoja)

        Do Until FilBlanc >= MaxFil
            NuevaPartida = LeeCelda(xlsheet, ColIni, i)
            If UCase(NuevaPartida) = UCase(NApu) Then
                DescPart = LeeCelda(xlsheet, ColDes, i)
                Cod = LeeCelda(xlsheet, ColCod, i)
                i += 1
                DescRec = ""
                RMo = ConvDouble(xlsheet, ColRMo, i)
                REq = ConvDouble(xlsheet, ColREq, i)

                Do Until (FilBlanc >= MaxFil) Or EvalEncab(DescRec)
                    i += 1
                    DescRec = LeeCelda(xlsheet, ColDesRec, i)
                    If DescRec = "" OrElse DescRec = EncabS10 Then
                        FilBlanc += 1
                    End If
                Loop
                i += 1
                TipoRec = DescRec
                DescRec = " "
            End If
            NuevaPartida = ""
            FilBlanc = 0
            apu = New ClApu
            ListaSP = New ObservableCollection(Of ClSp)

            Do Until (FilBlanc >= MaxFil) Or (UCase(NuevaPartida) = UCase(NApu))
                NuevaPartida = LeeCelda(xlsheet, ColIni, i)
                DescRec = LeeCelda(xlsheet, ColDesRec, i)
                If EvalEncab(DescRec) Then
                    TipoRec = DescRec
                End If
                If NuevaPartida = "" Then
                    FilBlanc += 1
                Else
                    FilBlanc = 0
                End If
                Und = LeeCelda(xlsheet, ColUnd, i)
                If Und <> "" Then
                    Cuadrilla = ConvDouble(xlsheet, ColCuad, i)
                    Cantidad = ConvDouble(xlsheet, ColCant, i)
                    Precio = ConvDouble(xlsheet, ColPre, i)
                End If
                i += 1
                If TipoRec <> TSP And Und <> "" Then
                    Recurso = New ClRecApu(Cod, DescRec, Und, Cantidad, Cuadrilla, Precio, TipoRec)
                    apu.Add(Recurso)
                End If
                If TipoRec = TSP And Und <> "" Then
                    Sp = New ClSp("", DescRec, Und, Cantidad, Precio, 0, 0)
                    ListaSP.Add(Sp)
                End If
                If Und = "" Then
                    NuevaPartida = LeeCelda(xlsheet, ColIni, i)
                End If
            Loop

            j = Espec.BuscaPartxCod(Cod)
            If j <> -1 Then
                Espec.lsPartidas(j).Lrec = apu
                Espec.lsPartidas(j).Rdeq = REq
                Espec.lsPartidas(j).Rdmo = RMo
                Espec.lsPartidas(j).Lsp = ListaSP
            End If

        Loop
    End Sub
End Class
