Imports System.Windows.Forms
Imports Util

Public Class classReporte
    Inherits classComun

#Region "Constructor"

    Public Sub New(ByVal po_form As Form)
        MyBase.New(po_form)
    End Sub

#End Region

#Region "Inicializacion"

    Public Overrides Sub sub_cargarForm()
        Try

            ' Se asigna el dataTable recibido por el formulario al control de usuario Grid
            sub_asignarDataTable()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_asignarDataTable()
        Try

            ' Se obtiene el dataTable del formulario
            Dim lo_dtb As DataTable = CType(o_form, frmReporte).DataTableReporte

            ' Se verifica si se obtuvo el dataTable
            If lo_dtb Is Nothing Then
                MsgBox("No se asigno una origen de datos al reporte.")
                Exit Sub
            End If

            ' Se obtiene el control de usuario Grid
            Dim lo_grid As uct_gridConBusqueda = ctr_obtenerControl("datos", o_form.Controls)

            ' Se verifica si se obtuvo el grid
            If lo_grid Is Nothing Then
                MsgBox("Error al obtener la grilla de datos.")
                Exit Sub
            End If

            ' Se asigna el dataTable al grid
            lo_grid.DataSource = lo_dtb
            lo_grid.sub_inicializar()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

End Class
