Imports Util
Public Class sqlPlanilla

#Region "Consultas"

    Public Shared Function dtb_obtCtasPorCobrar(ByVal ps_cardCode As String) As DataTable
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_obtCtasPorCobrar '" & ps_cardCode & "'"

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function dtb_obtCtasBanco() As DataTable
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_obtCtasBanco"

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function dtb_obtNroPosBanco(ByVal sCuenta As String) As DataTable
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "EXEC [BYR_BF_GETPOSBANC] 3,'" + sCuenta + "'"

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function int_obtPlanillasAbiertas() As Integer
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_obtPlanillasAbiertas"

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)(0)(0)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function int_obtCantLineasAsgPll(ByVal pi_id As Integer) As Integer
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_obtCantLineasAsgPll " & pi_id.ToString

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)(0)(0)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function str_actualizarNrosSAPPlaDet(ByVal pi_id As Integer) As String
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_actualizarNrosSAPPlaDet " & pi_id.ToString

            ' Se ejecuta la consulta
            Return ModuleSQLComun.str_ejecutarSQL_NET(ls_sql)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message
        End Try
    End Function

    Public Shared Function dtb_obtECenPlanillas() As DataTable
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_obtECenPlanillas"

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function int_verIdECenPlanillas(ByVal pi_id As Integer, ByVal pi_idEC As Integer) As Integer
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_verIdECenPlanillas " & pi_id.ToString & ", " & pi_idEC.ToString

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)(0)(0)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function int_verIdDocEnPlanillas(ByVal pi_id As Integer, ByVal pi_idDoc As Integer) As Integer
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_verIdDocEnPlanillas " & pi_id.ToString & ", " & pi_idDoc.ToString

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)(0)(0)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function str_obtCtaBancoNacion() As String
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_obtCtaBancoNacion"

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)(0)(0)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return ""
        End Try
    End Function

    Public Shared Function dtb_Reporte() As DataTable
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_ReporteDepositos"

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function dtb_ObtenerBanco(ByVal intId As Integer) As String
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_ObtenerBanco " & intId

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)(0)(0)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function str_verTipoPlanilla(ByVal pi_id As Integer) As String
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_verTipoPlanilla " & pi_id

            ' Se verifica si se obtuvo registros
            Dim lo_dtb As DataTable = ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)
            If lo_dtb.Rows.Count = 0 Then
                Return "-"
            Else
                Return lo_dtb(0)(0)
            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return ""
        End Try
    End Function

    Public Shared Function int_verExitDocEnPllAbierta(ByVal pi_idDoc As Integer, ByVal pi_transType As Integer, pi_docLine As Integer, pi_idPll As Integer) As Integer
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_verExitDocEnPllAbierta " & pi_idDoc.ToString & ", " & pi_transType.ToString & ", " & pi_docLine.ToString & ", " & pi_idPll.ToString

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)(0)(0)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function int_verExitDocEnPllAbierta2(ByVal pi_idDoc As Integer, ByVal pi_transType As Integer, pi_docLine As Integer, pi_idPll As Integer, asientoAjusteT As String) As Integer
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_verExitDocEnPllAbierta2 " & pi_idDoc.ToString & ", " & pi_transType.ToString & ", " & pi_docLine.ToString & ", " & pi_idPll.ToString & ", " & "'" & asientoAjusteT.Replace("'", "''") & "'"

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)(0)(0)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function int_verExitECEnPllAbierta(ByVal pi_idEC As Integer, pi_idPll As Integer) As Integer
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_verExitECEnPllAbierta " & pi_idEC.ToString & ", " & pi_idPll.ToString

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)(0)(0)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

#End Region

End Class
