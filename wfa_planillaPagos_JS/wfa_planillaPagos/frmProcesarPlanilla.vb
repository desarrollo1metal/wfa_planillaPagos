Imports cl_Negocio
Imports Util

Public Class frmProcesarPlanilla
    Inherits frmComun

#Region "Inicializacion"

    ' Se asigna el objeto de negocio asociado al formulario
    Overrides Sub sub_iniObjNegocio()
        Try

            ' Se asigna el objeto de negocio asociado a este formulario
            o_objNegocio = New classProcesarPlanilla(Me)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

    Private Sub btnBuscar_Click(sender As System.Object, e As System.EventArgs) Handles btnBuscar.Click

    End Sub

    Private Sub btnProcesar_Click(sender As System.Object, e As System.EventArgs) Handles btnProcesar.Click

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class