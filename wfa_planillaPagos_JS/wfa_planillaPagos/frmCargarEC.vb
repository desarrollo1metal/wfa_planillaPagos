Imports cl_Negocio
Imports Util

Public Class frmCargarEC
    Inherits frmComun

#Region "Inicializacion"

    ' Se asigna el objeto de negocio asociado al formulario
    Overrides Sub sub_iniObjNegocio()
        Try

            ' Se asigna el objeto de negocio asociado a este formulario
            o_objNegocio = New classCargarEC(Me)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

    Private Sub ugcEC_Load(sender As System.Object, e As System.EventArgs) Handles ugcEC.Load

    End Sub
End Class
