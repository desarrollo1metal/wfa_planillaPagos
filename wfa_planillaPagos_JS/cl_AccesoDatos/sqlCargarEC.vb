Imports Util
Imports System.Data.SqlClient

Public Class sqlCargarEC

#Region "ProcesosNegocio"

    Public Shared Function dtb_importarEC(ByVal ps_ruta As String) As DataTable
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_obtenerInfoEstadoCuentaBancoTXT_PLPR '" & ps_ruta & "'"

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlCargarEC", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function str_importarEC(ByVal po_dtb As DataTable, ByVal po_entEC As Object) As String
        Try

            ' Se verifica si el dataTable recibido es valido
            If po_dtb Is Nothing Then
                Return "No se obtuvo un dataTable para la importacion."
            End If

            ' Se verifica si el dataTable tiene registros
            If po_dtb.Rows.Count = 0 Then
                Return "No hay registros para importar en el dataTable recibido."
            End If

            ' Se obtiene el id entero autogenerado siguiente
            Dim li_nuevoId As Integer = int_autoIntId("GMI_TmpOprBnc", "id")

            ' Se inicia la transaccion SQL
            Dim lo_sqlConn As SqlConnection = con_obtSQLConnection()
            Dim lo_sqlTran As SqlTransaction = trn_obtSQLTransaction(lo_sqlConn, "cargarEC")

            ' Se declara una variable para el resultado de las transacciones SQL
            Dim ls_result As String = ""

            ' Se recorre el dataTable
            For Each lo_row As DataRow In po_dtb.Rows

                ' Se verifica si el registro ya existe en la base de datos
                If lo_row("existeEnEC").ToString.ToLower = "si" Then

                    ' Se elimina el registro por id
                    ls_result = str_ejecDelete("GMI_TmpOprBnc", "id", CInt(lo_row("idEC")), lo_sqlConn, lo_sqlTran)

                    ' Se verifica si la transaccion fue correcta
                    If ls_result.Trim <> "" Then
                        sub_rollBackTransaction(lo_sqlTran, lo_sqlConn)
                        Return "Ocurrio un error en el proceso de importacion a la base de datos: " & ls_result
                    End If

                End If

                ' Se inserta el registro en la tabla
                ls_result = str_ejecInsertDtbRow(po_entEC, lo_row, lo_sqlConn, lo_sqlTran, li_nuevoId)

                ' Se verifica si la transaccion fue correcta
                If ls_result.Trim <> "" Then
                    sub_rollBackTransaction(lo_sqlTran, lo_sqlConn)
                    Return "Ocurrio un error en el proceso de importacion a la base de datos: " & ls_result
                End If

                ' Se incrementa el id 
                li_nuevoId = li_nuevoId + 1

            Next

            ' Se verifica si la transaccion fue correcta
            If ls_result.Trim <> "" Then
                sub_rollBackTransaction(lo_sqlTran, lo_sqlConn)
                Return "Ocurrio un error en el proceso de importacion a la base de datos: " & ls_result
            End If

            ' Si todas las transacciones se ejecutaron de manera correcta, se confirma la transaccion
            sub_commitTransaction(lo_sqlTran, lo_sqlConn)

            ' Se retorna una cadena vacia para indicar que el proceso fue exitoso
            Return ""

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlCargarEC", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message
        End Try
    End Function

    Public Shared Function dtb_obtEstadoCuentaPPR(ByVal ps_ctaBanc As String) As DataTable
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_obtEstadoCuentaPPR '" & ps_ctaBanc & "'"

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlCargarEC", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function dtb_repEstadoCuenta() As DataTable
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_repEstadoCuenta"

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlCargarEC", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function int_verNroOperEC(ByVal pd_fecha As Date, ByVal ps_cuenta As String, ByVal ps_nroOper As String, ByVal pd_monto As Double) As Integer
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_verNroOperEC '" & pd_fecha.ToString("yyyyMMdd") & "', '" & ps_cuenta & "', '" & ps_nroOper.Replace("'", "''") & "', " & pd_monto

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql).Rows(0)(0)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlCargarEC", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return -1
        End Try
    End Function

    Public Shared Function int_verNroOperECenPll(ByVal pd_fecha As Date, ByVal ps_cuenta As String, ByVal ps_nroOper As String, ByVal pd_monto As Double) As Integer
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_verNroOperECenPll '" & pd_fecha.ToString("yyyyMMdd") & "', '" & ps_cuenta & "', '" & ps_nroOper.Replace("'", "''") & "', " & pd_monto

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql).Rows(0)(0)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlCargarEC", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return -1
        End Try
    End Function

    Public Shared Function str_verTipoRegEC(ByVal pi_id As Integer) As String
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_verTipoRegEC " & pi_id.ToString

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql).Rows(0)(0)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "sqlCargarEC", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return "x"
        End Try
    End Function

#End Region

End Class
