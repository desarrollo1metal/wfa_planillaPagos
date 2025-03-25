Imports Util
Imports cl_Negocio

Public Class frmConfigMenu
    Inherits frmComun

#Region "Inicializacion"

    ' Se asigna el objeto de negocio asociado al formulario
    Overrides Sub sub_iniObjNegocio()
        Try

            ' Se asigna el objeto de negocio asociado a este formulario
            o_objNegocio = New classMenu(Me)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

End Class