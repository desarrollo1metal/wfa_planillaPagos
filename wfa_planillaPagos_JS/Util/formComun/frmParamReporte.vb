Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraEditors

Public Class frmParamReporte

    ' Se declara los atributos del formulario
    Private o_dtbParams As DataTable
    Private s_nomProc As String = ""
    Private o_lstCtrParams As New List(Of Control)
    Private o_dtbResultadoRep As DataTable

    ' Propiedades
    Public ReadOnly Property Resultado() As DataTable
        Get
            Return o_dtbResultadoRep
        End Get
    End Property

#Region "Constructor"

    Public Sub New(ByVal ps_nomProc As String, ByVal po_dtbParams As DataTable)

        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        s_nomProc = ps_nomProc
        o_dtbParams = po_dtbParams

    End Sub

#End Region

#Region "Controles"

    ' Se añade los controles correspondientes para los parametros del reporte
    Public Function bol_crearControlesParam(ByVal po_lstCtrParams As List(Of Control)) As Boolean
        Try

            ' Se verifica si se obtuvo el dataTable de parametros
            If o_dtbParams Is Nothing Then
                MsgBox("No se envió un <DataTable> parametros para el reporte.")
                Return False
            End If

            If o_dtbParams.Rows.Count = 0 Then
                MsgBox("No se obtuvo parametros para el reporte.")
                Return False
            End If

            ' Se obtiene el listado de controles para los parametros especificos
            ' - Primero, se verifica si se recibio un listado de parametro
            If Not po_lstCtrParams Is Nothing Then
                o_lstCtrParams = po_lstCtrParams
            End If

            ' Se declara variables para la ubicacion de los controles
            Dim li_x As Integer = 5
            Dim li_y As Integer = 5

            ' Se declara un indicador para verificar si se asigno un control propio para determinado parametro
            Dim lb_tieneControl As Boolean = False

            ' Se recorre el listado de parametros
            For Each lo_row As DataRow In o_dtbParams.Rows

                ' Se crea un control Label para la descripcion
                Dim lo_label As New Label
                lo_label.Tag = "lbl_" & lo_row("nomParam")
                lo_label.Location = New Point(li_x, li_y)
                lo_label.Size = New Size(100, 20)
                lo_label.Text = lo_row("nomParam")
                Me.Controls.Add(lo_label)

                ' Se recorre el listado de controles para los parametros para asignar un control especifico al parametro
                For Each lo_ctr As Control In o_lstCtrParams

                    ' Se verifica si los controles coinciden en el nombre del parametro (TAG)
                    If lo_row("nomParam").ToString.Trim.ToLower = lo_ctr.Tag.ToString.Trim.ToLower Then

                        ' Se indica que el parametro tiene un control propio
                        lb_tieneControl = True

                        ' Se añade el control al formulario
                        lo_ctr.Location = New Point(li_x + 120, li_y)
                        lo_ctr.Size = New Size(150, 20)
                        Me.Controls.Add(lo_ctr)

                        ' Se continua el FOR
                        Exit For

                    End If

                Next

                ' Se verifica si el parametro tiene un control propio
                If lb_tieneControl = True Then
                    Continue For
                End If

                ' Se declara un control para el parametro
                Dim lo_ctrParam As New Control

                ' Se añade un control al parametro de acuerdo al tipo de dato
                If lo_row("tipoParam").ToString.ToLower.Contains("char") Then
                    lo_ctrParam = New TextEdit
                ElseIf lo_row("tipoParam").ToString.ToLower.Contains("int") Then
                    lo_ctrParam = New TextEdit
                ElseIf lo_row("tipoParam").ToString.ToLower.Contains("date") Then
                    lo_ctrParam = New DateEdit
                End If

                ' Se asigna las propiedades al control
                lo_ctrParam.Tag = lo_row("nomParam")
                lo_ctrParam.Location = New Point(li_x + 120, li_y)
                lo_ctrParam.Size = New Size(150, 20)
                Me.Controls.Add(lo_ctrParam)

                ' Se incrementa las variables de ubicacion
                li_y = li_y + 25

            Next

            ' Se asigna la altura al formulario
            Me.Height = Me.Height + li_y + 20

            ' Se finaliza el metodo con TRUE si todo fue correcto
            Return True

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return False
        End Try
    End Function

    ' Se añade un control para un parametro en especial
    Public Sub sub_anadirControlParam(ByVal po_control As Control)
        Try

            ' Se verifica si el control tiene TAG
            If po_control.Tag Is Nothing Then
                MsgBox("El control enviado como parametro debe contar con el nombre del parametro del Stored Procedure de SQL en la propiedad TAG. No se puede añadir el control a listado de parametros")
            End If

            ' Se añade el control al listado
            o_lstCtrParams.Add(po_control)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    ' Se añade las descripciones de usuario a los nombres de los parametros


#End Region

#Region "Eventos"

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        sub_obtDatosReporte()
    End Sub

#End Region

#Region "Reporte"

    ' Se obtiene los datos del reporte desde el procedure recibido
    Public Sub sub_obtDatosReporte()
        Try

            ' Se limpia el dataTable de resultado
            o_dtbResultadoRep = Nothing

            ' Se declara una variable que contendra la sentencia de ejecucion del procedimiento almacenado
            Dim ls_sqlProc As String = "execute " & s_nomProc & " "
            Dim ls_separador As String = ""

            ' Se recorre los controles de los parametros 
            For Each lo_row As DataRow In o_dtbParams.Rows

                ' Se obtiene el control que cuenta con el tag del nombre del parametro
                Dim lo_control As Control = ctr_obtenerControl(lo_row("nomParam"), Me.Controls)

                ' Se obtiene el valor del control
                Dim lo_valor As Object = obj_obtValorControl(lo_control)

                ' Se verifica el tipo de dato del parametro
                If lo_row("tipoParam").ToString.ToLower.Contains("char") Then
                    ls_sqlProc = ls_sqlProc & ls_separador & "'" & CStr(lo_valor) & "'"
                ElseIf lo_row("tipoParam").ToString.ToLower.Contains("int") Then
                    ls_sqlProc = ls_sqlProc & ls_separador & "" & CInt(lo_valor) & ""
                ElseIf lo_row("tipoParam").ToString.ToLower.Contains("date") Then
                    ls_sqlProc = ls_sqlProc & ls_separador & "'" & CDate(lo_valor).ToString("yyyyMMdd") & "'"
                End If

                ' Se asigna el separador para los parametros
                ls_separador = ", "

            Next

            ' Se obtiene el resultado
            o_dtbResultadoRep = dtb_ejecutarSQL_NET(ls_sqlProc)

            ' Se cierra el formulario
            Me.Close()

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region
   
End Class