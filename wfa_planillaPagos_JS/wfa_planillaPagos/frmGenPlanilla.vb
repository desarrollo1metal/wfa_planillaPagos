Imports cl_Negocio
Imports Util

Public Class frmGenPlanilla
    Inherits frmComun

#Region "Inicializacion"

    ' Se asigna el objeto de negocio asociado al formulario
    Overrides Sub sub_iniObjNegocio()
        Try

            ' Se asigna el objeto de negocio asociado a este formulario
            o_objNegocio = New ClassGenPlanilla(Me)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

    Private Sub btnExportar_Click(sender As System.Object, e As System.EventArgs) Handles btnExportar.Click

    End Sub

    Private Sub btnGenPlanilla_Click(sender As System.Object, e As System.EventArgs) Handles btnGenPlanilla.Click

    End Sub

    Private Sub agregar2_Click(sender As Object, e As EventArgs) Handles agregar2.Click

    End Sub

    Private Sub frmGenPlanilla_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ulsClientes_Load(sender As Object, e As EventArgs) Handles ulsClientes.Load

    End Sub

    Private Sub btnBuscarClts_Click(sender As Object, e As EventArgs) Handles btnBuscarClts.Click

    End Sub

    Private Sub btnAdcRegistro_Click(sender As Object, e As EventArgs) Handles btnAdcRegistro.Click

    End Sub
End Class