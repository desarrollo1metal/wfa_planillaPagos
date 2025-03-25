Imports Util
Imports cl_Entidad
Imports System.Windows.Forms

Public Class classConfig
    Inherits classComun

#Region "Constructor"

    Public Sub New(ByVal po_form As frmComun)
        MyBase.New(po_form)
    End Sub

#End Region

#Region "Ini_formulario"

    Public Overrides Sub sub_cargarForm()
        Try

            ' Se obtiene la compañia a la cual se está conectado
            Dim ls_company As String = S_CompanyDB

            ' Se asigna al Label el nombre de la compañia
            Dim lo_lblCompany As Control = ctr_obtenerControl("company", o_form.Controls)

            ' Se verifica si se obtuvo el control
            If lo_lblCompany Is Nothing Then
                MsgBox("No se obtuvo el control para el nombre de la compañia.")
                o_form.Close() ' Se cierra el formulario
                Exit Sub
            End If

            ' Se asigna el nombre de la compañia al control 
            sub_asigValorControl("company", ls_company)

            ' Se obtiene la version del aplicativo en el archivo TXT
            Dim ll_version As Long = lon_obtVersionAPP()

            ' Se verifica si se obtuvo un valor de version
            If ll_version = -1 Then
                MsgBox("No se pudo obtener la version desde el archivo TXT de la aplicacion.")
                Exit Sub
            End If

            ' Se asigna el nombre de la compañia al control 
            sub_asigValorControl("appVersion", ll_version)

            ' Se obtiene la configuración para la compañia conectada
            Dim lo_entConfig As New entConfig
            lo_entConfig = lo_entConfig.obj_obtPorCodigo(ls_company)

            ' Se verifica si se obtuvo los datos del objeto
            If lo_entConfig.Company Is Nothing Then

                ' Se añade la configuración a la base de datos
                sub_accionPrincipal()

                ' Se muestra un mensaje que indique que se ingreso en la base de datos un registro de configuracion para la compañia actual
                MsgBox("Se ingreso en la base de datos un registro de configuracion para la compañia actual. Para configurar la aplicación vuelva a ingresar a esta ventana.")

                ' Se cierra el formulario
                o_form.Close()

            Else

                ' Se asigna los datos del objeto al formulario
                sub_asignarEntidadAForm(lo_entConfig, ls_company)

                ' Se asigna el modo del formulario a VER
                sub_asignarModo(enm_modoForm.VER)

                ' Se inicializa los controles del formulario
                sub_iniControl()

            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub sub_iniControl()
        Try

            ' Se obtiene los controles a inicializar
            Dim lo_ulsCtaPerdidaTC As Control = ctr_obtenerControl("ctaPerdidaTC", o_form.Controls)
            Dim lo_ulsCtaGanaciaTC As Control = ctr_obtenerControl("ctaGanaciaTC", o_form.Controls)

            ' Se asigna los filtros a ambos listados
            CType(lo_ulsCtaPerdidaTC, uct_lstSeleccion).sub_anadirCondicion("Postable", "Y")
            CType(lo_ulsCtaPerdidaTC, uct_lstSeleccion).sub_anadirCondicion("LocManTran", "N")
            CType(lo_ulsCtaGanaciaTC, uct_lstSeleccion).sub_anadirCondicion("Postable", "Y")
            CType(lo_ulsCtaGanaciaTC, uct_lstSeleccion).sub_anadirCondicion("LocManTran", "N")

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

#Region "Validaciones"

    Protected Overrides Function bol_valAlAnadir(ByVal pb_antesAccion As Boolean) As Boolean
        Try

            ' Se realiza las validaciones antes de añadir
            If pb_antesAccion = False Then
                Return bol_guardarVersionEnTXT()
            End If

            ' Si las validaciones fueron correctas, se retorna TRUE
            Return True

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return False
        End Try
    End Function

    Protected Overrides Function bol_valAlActualizar(ByVal pb_antesAccion As Boolean) As Boolean
        Try

            ' Se realiza las validaciones antes de añadir
            If pb_antesAccion = False Then
                Return bol_guardarVersionEnTXT()
            End If

            ' Si las validaciones fueron correctas, se retorna TRUE
            Return True

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return False
        End Try
    End Function

    Private Function bol_guardarVersionEnTXT() As Boolean
        Try

            ' Se obtiene el campo de la version
            Dim lo_version As Control = ctr_obtControl("appVersion")

            ' Se verifica si se obtuvo el control
            If lo_version Is Nothing Then
                Return False
            End If

            ' Se obtiene el valor de la versión 
            Dim ls_version As String = obj_obtValorControl(lo_version)

            ' Se verifica si se obtuvo un valor para la version
            If ls_version.Trim = "" Then
                MsgBox("No se obtuvo un valor para guardarlo como versión desde el campo correspondiente.")
                Return False
            End If

            ' Se guarda la version en el archivo txt
            Return bol_grabarValorEnTXT(ls_version, My.Application.Info.DirectoryPath & "\Version", "version.txt")

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return False
        End Try
    End Function

#End Region

End Class
