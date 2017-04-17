Module TGlo
    'Máximo número de filas blancas entre APUs en la hoja Excel
    Public Const MaxFil As Integer = 5

    'Error de redondeo al desacoplar los APU
    Public Const ErrorRed As Double = 0.01

    'Máximo número de iteraciones para llegar al Error de Redondeo
    Public Const MaxIteraciones As Integer = 10

    'Es el encabezado para los Recursos en los APUs que genera S10 al exportar a Excel
    Public Const EncabS10 As String = "Descripción Recurso"

    'Titulos en los Análisis de PU de S10
    Public Const TMat As String = "Materiales"
    Public Const TMO As String = "Mano de Obra"
    Public Const TEQ As String = "Equipos"
    Public Const TSC As String = "Subcontratos"
    Public Const TSP As String = "Subpartidas"

    'Indicador de Nuevo Apu la palabra "Partida" de la primera columna con datos de la hoja Excel de APUs
    Public Const NApu As String = "Partida"

End Module
