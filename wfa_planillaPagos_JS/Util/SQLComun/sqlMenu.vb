Imports System.Data.SqlClient

Public Class sqlMenu

#Region "Consultas"

    Public Shared Function dtb_obtMenuTitulo() As DataTable
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_obtMenuTitulo"

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Shared Function dtb_sysObtMenusPorMnuPadre(ByVal pi_id As Integer) As DataTable
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sysObtMenusPorMnuPadre " & pi_id.ToString

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Shared Function str_limpiarMenuSis(ByVal po_sqlConn As SqlConnection, ByVal po_sqlTran As SqlTransaction) As String
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_limpiarMenuSis"

            ' Se ejecuta la consulta
            Return ModuleSQLComun.str_ejecutarSQL_NET(ls_sql, po_sqlConn, po_sqlTran)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return ex.Message
        End Try
    End Function

#End Region

#Region "Transacciones"

    Public Shared Function str_insertMenuDesdeDTB(ByVal po_entMenu As Object, ByVal po_dtb As DataTable) As String
        Try

            ' Se verifica si el dataTable tiene registros
            If po_dtb.Rows.Count < 1 Then
                Return "No hay registros en el dataTable a ingresar."
            End If

            ' Se inicia la transaccion SQL
            Dim lo_sqlConn As SqlConnection = con_obtSQLConnection()
            Dim lo_sqlTran As SqlTransaction = trn_obtSQLTransaction(lo_sqlConn, "cargarEC")

            ' Se declara una variable para el resultado de las transacciones SQL
            Dim ls_result As String = ""

            ' Se limpia los menus del sistema
            ls_result = str_limpiarMenuSis(lo_sqlConn, lo_sqlTran)

            ' Se verifica si la transaccion fue correcta
            If ls_result.Trim <> "" Then
                sub_rollBackTransaction(lo_sqlTran, lo_sqlConn)
                Return "Ocurrio un error en el proceso de eliminacion de los menús de la base de datos: " & ls_result
            End If

            For Each lo_row As DataRow In po_dtb.Rows

                ' Se obtiene el id del registro de menu
                Dim li_id As Integer = CInt(lo_row("id"))

                ' Se verifica si se obtuvo el valor de id
                If li_id = -1 Then
                    sub_rollBackTransaction(lo_sqlTran, lo_sqlConn)
                    Return "El valor de Id obtenido para el menú es incorrecto."
                End If

                ' Se inserta el registro en la tabla
                ls_result = str_ejecInsertDtbRow(po_entMenu, lo_row, lo_sqlConn, lo_sqlTran, li_id)

                ' Se verifica si la transaccion fue correcta
                If ls_result.Trim <> "" Then
                    sub_rollBackTransaction(lo_sqlTran, lo_sqlConn)
                    Return "Ocurrio un error en el proceso de insercion a la base de datos: " & ls_result
                End If

            Next

            ' Se verifica si la transaccion fue correcta
            If ls_result.Trim <> "" Then
                sub_rollBackTransaction(lo_sqlTran, lo_sqlConn)
                Return "Ocurrio un error en el proceso de insercion a la base de datos: " & ls_result
            End If

            ' Si todas las transacciones se ejecutaron de manera correcta, se confirma la transaccion
            sub_commitTransaction(lo_sqlTran, lo_sqlConn)

            ' Se retorna una cadena vacia para indicar que el proceso fue exitoso
            Return ""

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return ex.Message
        End Try
    End Function

#End Region

End Class
