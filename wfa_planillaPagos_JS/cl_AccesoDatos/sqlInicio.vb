Imports Util
Public Class sqlInicio

    Public Function int_indExistObjSQL(ByVal ps_nomObjSQL As String) As Integer
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "select count(name) from sys.objects where name like '" & ps_nomObjSQL & "'"

            ' Se ejecuta la consulta
            Return CInt(ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql).Rows(0).Item(0))

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Function str_ejecutarQuery(ByVal ps_query As String) As String
        Try

            ' Se ejecuta el query
            Dim ls_res As String = ModuleSQLComun.str_ejecutarSQL_NET(ps_query)

            ' Si no ocurrio errores, se retorna una cadena vacia
            Return ls_res

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return ex.Message
        End Try
    End Function

    Public Function dtb_obtCUSAP(ByVal ps_tabla As String, ByVal ps_idFrmSAP As String, ByVal pi_ufCatSAP As Integer) As DataTable
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_obtCamposUsrSAP '" & ps_tabla & "', '" & ps_idFrmSAP & "', " & pi_ufCatSAP.ToString & ""

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Function dtb_obtValCUSAP(ByVal ps_tabla As String, ByVal pi_idCUSAP As Integer) As DataTable
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_obtValoresCUSAP '" & ps_tabla & "', " & pi_idCUSAP.ToString & ""

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

End Class
