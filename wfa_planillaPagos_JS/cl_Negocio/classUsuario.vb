Imports Util
Imports System.Windows.Forms
Imports cl_Entidad

Public Class classUsuario
    Inherits classComun

    ' Variables de la clase
    Private s_pass As String = ""

#Region "Constructor"

    Public Sub New(ByVal po_form As Form)
        MyBase.New(po_form)
    End Sub

#End Region

#Region "Validacion"

    Protected Overrides Function bol_valAlAnadir(pb_antesAccion As Boolean) As Boolean
        Try

            ' Se ejecuta la validacion antes de que se ejecute la accion
            If pb_antesAccion = True Then
                Return bol_valAdicion()
            Else
                sub_actualizarPassEncript()
            End If

            ' Se retorna TRUE si la validacion fue correcta
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Protected Overrides Function bol_valAlActualizar(pb_antesAccion As Boolean) As Boolean
        Try

            ' Se ejecuta la validacion antes de que se ejecute la accion
            If pb_antesAccion = True Then
                Return bol_valAdicion()
            Else
                sub_actualizarPassEncript()
            End If

            ' Se retorna TRUE si la validacion fue correcta
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Function bol_valAdicion() As Boolean
        Try

            ' Se obtiene el control asociado al password
            Dim lo_pass1 As Control = ctr_obtControl("password")
            Dim lo_pass2 As Control = ctr_obtControl("password2")

            ' Se verifica si se obtuvo el control de password
            If lo_pass1 Is Nothing Then
                sub_mostrarMensaje("No se pudo obtener el control <password>.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Return False
            End If

            If lo_pass2 Is Nothing Then
                sub_mostrarMensaje("No se pudo obtener el control <password2>.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Return False
            End If

            ' Se obtiene el valor del control
            Dim ls_pass1 As String = obj_obtValorControl(lo_pass1)

            ' Se verifica si se obtuvo el valor del control
            If ls_pass1 Is Nothing Then
                sub_mostrarMensaje("Debe ingresar el password.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Return False
            End If

            ' Se valida la similitud de las cajas de texto de password
            Dim lb_res As Boolean = bol_valPassword(lo_pass1, ls_pass1)

            ' Se verifica el resultado de la validacion
            If lb_res = False Then
                Return False
            End If

            ' Se verifica el modo del formulario
            If o_form.Modo = enm_modoForm.ANADIR Then

                ' Se obtiene el password
                s_pass = ls_pass1

                ' Se asigna el valor encriptado a las cajas de texto
                sub_asigValorControl("password", str_encriptar(ls_pass1))
                sub_asigValorControl("password2", str_encriptar(ls_pass1))

            ElseIf o_form.Modo = enm_modoForm.ACTUALIZAR Then

                ' Se verifica si los controles de password está habilitados
                If lo_pass1.Enabled = True Then

                    ' Se obtiene el password
                    s_pass = ls_pass1

                    ' Se asigna el valor encriptado a las cajas de texto
                    sub_asigValorControl("password", str_encriptar(ls_pass1))
                    sub_asigValorControl("password2", str_encriptar(ls_pass1))

                End If

            End If

            ' Si las validaciones fueron correctas
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Function bol_valPassword(po_pass As TextBox, ps_pass As String) As Boolean
        Try

            ' Se obtiene los controles asociados al password
            Dim lo_pass2 As Control = ctr_obtControl("password2")

            ' Se verifica si se obtuvo los controles de password
            If lo_pass2 Is Nothing Then
                sub_mostrarMensaje("No se pudo obtener el control <password2>.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Return False
            End If

            ' Se obtiene los valores ingresados en los controles
            Dim ls_pass2 As String = obj_obtValorControl(lo_pass2)

            ' Se verifica si se obtuvo los valores de los controles
            If ls_pass2 Is Nothing Then
                sub_mostrarMensaje("Debe ingresar el password en ambas cajas de texto.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Return False
            End If

            ' Se verifica si los valores recibidos como password estan vacios
            If ps_pass.Trim = "" Or ls_pass2.Trim = "" Then
                sub_mostrarMensaje("Debe ingresar el password del usuario y confirmar el mismo.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Return False
            End If

            ' Se verifica si los valores en ambos controles son iguales
            If ps_pass.Trim.Replace(" ", "") <> ls_pass2.Trim.Replace(" ", "") Then
                sub_mostrarMensaje("Los password no coinciden. Asegurese de que sean iguales.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Return False
            End If

            ' Si las validaciones fueron correctas
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

#End Region

#Region "Encriptacion"

    Private Sub sub_actualizarPassEncript()
        Try

            ' Se verifica si se recibio un password para actualizar
            If s_pass.Trim = "" Then
                Exit Sub
            End If

            ' Se verifica si se obtuvo el valor del control
            If s_pass Is Nothing Then
                sub_mostrarMensaje("Debe ingresar el password.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            ' Se encripta el texto del password
            Dim ls_passEncr As String = str_encriptar(s_pass)

            ' Se verifica si se realizó la encriptacion demanera correcta
            If ls_passEncr.Trim = "" Then
                s_pass = ""
                Exit Sub
            End If

            ' Se obtiene el control que corresponde al codigo del usuario
            Dim lo_codigo As Control = ctr_obtControl("codigo")

            ' Se verifica si se obtuvo el control
            If lo_codigo Is Nothing Then
                sub_mostrarMensaje("No se pudo obtener el control <codigo>.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                s_pass = ""
                Exit Sub
            End If

            ' Se obtiene el valor del control
            Dim ls_codigo As String = obj_obtValorControl(lo_codigo)

            ' Se verifica si se obtuvo el valor del control
            If ls_codigo Is Nothing Then
                sub_mostrarMensaje("Ocurrió un error al obtener el código del usuario.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                s_pass = ""
                Exit Sub
            End If
            If ls_codigo Is Nothing Then
                sub_mostrarMensaje("Debe ingresar el código del usuario.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                s_pass = ""
                Exit Sub
            End If

            ' Se obtiene el objeto desde el codigo
            Dim ls_res As String = str_actPasswordUsr(ls_codigo, ls_passEncr)

            ' Se verifica el resultado de la actualizacion del usuario
            If ls_res.Trim <> "" Then
                sub_mostrarMensaje("No se pudo actualizar el password: " & ls_res, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                s_pass = ""
            End If

            ' Se limpia la variable para el próximo uso
            s_pass = ""

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#Region "EventosBoton"

    Public Sub sub_habilPass()
        Try

            ' Se obtiene los controles de password
            Dim lo_pass1 As Control = ctr_obtControl("password")
            Dim lo_pass2 As Control = ctr_obtControl("password2")

            ' Se verifica si se obtuvo los controles de password
            If lo_pass1 Is Nothing Then
                sub_mostrarMensaje("No se pudo obtener el control <password>.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            If lo_pass2 Is Nothing Then
                sub_mostrarMensaje("No se pudo obtener el control <password2>.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            ' Se verifica si los controles estan habilitados o deshabilitados
            If lo_pass1.Enabled = True Then

                ' Se deshabilita los controles de password
                lo_pass1.Enabled = False
                lo_pass2.Enabled = False

            Else

                ' Se habilita los controles de password
                lo_pass1.Enabled = True
                lo_pass2.Enabled = True

                ' Se limpia los controles
                sub_asigValorControl("password", "")
                sub_asigValorControl("password2", "")

            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#Region "ManejoForm"

    Protected Overrides Function bol_validarModoAnadir(pb_antesAccion As Boolean) As Boolean
        Try

            ' Se verifica que la accion sea realizada luego del cambio de modo
            If pb_antesAccion = False Then
                sub_deshabilitarCtrPass()
            End If

            ' Se retorna TRUE para finalizar
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Protected Overrides Function bol_validarModoVer(pb_antesAccion As Boolean) As Boolean
        Try

            ' Se verifica que la accion sea realizada luego del cambio de modo
            If pb_antesAccion = False Then
                sub_deshabilitarCtrPass()
            End If

            ' Se retorna TRUE para finalizar
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Sub sub_deshabilitarCtrPass()
        Try

            ' Se obtiene los controles de password
            Dim lo_pass1 As Control = ctr_obtControl("password")
            Dim lo_pass2 As Control = ctr_obtControl("password2")

            ' Se verifica si se obtuvo los controles de password
            If lo_pass1 Is Nothing Then
                sub_mostrarMensaje("No se pudo obtener el control <password>.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            If lo_pass2 Is Nothing Then
                sub_mostrarMensaje("No se pudo obtener el control <password2>.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            ' Se deshabilita los controles de password
            lo_pass1.Enabled = False
            lo_pass2.Enabled = False

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

End Class
