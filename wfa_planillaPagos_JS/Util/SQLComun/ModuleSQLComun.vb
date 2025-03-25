Imports System.Data.SqlClient
Imports System.Reflection
Imports Util

Public Module ModuleSQLComun

    ' Se declara las variables globales
    Dim SBOCompany As SAPbobsCOM.Company

    ' Variables del modulo
    Public S_SQLPassword As String
    Public S_DbUserName As String
    Public S_CompanyDB As String
    Public S_Server As String

    ' Constante privada
    Private S_NOMMODULO As String = "ModuleSQLComun"

#Region "EjecucionSQL"

    Public Sub LiberarObjeto(ByVal objeto As Object)
        Try
            If objeto IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objeto)
            End If
        Catch
            ' Ignorar errores
        End Try

        Try
            objeto = Nothing
        Catch
            ' Ignorar errores
        End Try

        GC.Collect()
    End Sub



    Public Function dtb_ejecutarSQL_doquery(ByVal ps_sql As String) As DataTable
        Try

            ' Se declara un objeto RecordSet
            Dim lo_recordSet As SAPbobsCOM.Recordset

            ' Se inicializa el objeto
            lo_recordSet = SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            ' Se ejecuta la consulta
            lo_recordSet.DoQuery(ps_sql)

            ' Se retorna el recordSet
            Dim resultTable As DataTable = dtb_convertRecordSetADataTable(lo_recordSet)
            LiberarObjeto(lo_recordSet)
            Return resultTable

        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Function dtb_ejecutarSQL_NET(ByVal ps_sql As String) As DataTable
        Dim ls_query As String = ps_sql ' Se declara una variable para obtener el query que se pretende ejecutar en caso de que ocurra un error
        Try
            Dim cn As New SqlConnection(sqlConexion.Conectar(S_Server, S_CompanyDB, S_DbUserName, S_SQLPassword))
            Dim cmd As New SqlCommand(ps_sql, cn)
            cmd.CommandTimeout = 300
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            da.Dispose()
            cn.Close()
            cn.Dispose()
            Return (dt)
            'vo

        Catch ex As Exception
            'MsgBox(ex.Message & " - Query: <" & ls_query & ">")
            sub_mostrarMensaje(ex.Message & " - Query: <" & ls_query & ">", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, S_NOMMODULO, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Function str_ejecutarSQL_NET(ByVal ps_sql As String) As String
        Dim ls_query As String = ps_sql ' Se declara una variable para obtener el query que se pretende ejecutar en caso de que ocurra un error
        Try

            Dim cn As New SqlConnection(sqlConexion.Conectar(S_Server, S_CompanyDB, S_DbUserName, S_SQLPassword))
            Dim cmd As New SqlCommand(ps_sql, cn)
            cmd.CommandTimeout = 300
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            da.Dispose()
            cn.Close()
            cn.Dispose()

            ' Si no ocurrio errores, se retorna una cadena vacia
            Return ""

        Catch ex As Exception
            Return ex.Message & " - Query: <" & ls_query & ">"
        End Try
    End Function

    Public Function str_ejecutarSQL_NET(ByVal ps_sql As String, ByVal po_sqlConn As SqlConnection, ByVal po_sqlTran As SqlTransaction) As String
        Dim ls_query As String = ps_sql ' Se declara una variable para obtener el query que se pretende ejecutar en caso de que ocurra un error
        Try

            Dim cmd As New SqlCommand(ps_sql, po_sqlConn)
            cmd.CommandTimeout = 300
            cmd.Transaction = po_sqlTran
            cmd.ExecuteNonQuery()

            ' Si no ocurrio errores, se retorna una cadena vacia
            Return ""

        Catch ex As Exception
            Return ex.Message & " - Query: <" & ls_query & ">"
        End Try
    End Function

    Public Function con_obtSQLConnection() As SqlConnection
        Try
            Dim cn As New SqlConnection(sqlConexion.Conectar(S_Server, S_CompanyDB, S_DbUserName, S_SQLPassword))
            cn.Open()
            Return cn
        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function trn_obtSQLTransaction(ByVal po_conn As SqlConnection, ByVal ps_nomTran As String) As SqlTransaction
        Try
            Dim lo_sqlTran As SqlTransaction = po_conn.BeginTransaction(IsolationLevel.ReadCommitted, ps_nomTran)
            Return lo_sqlTran
        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Sub sub_rollBackTransaction(ByVal po_sqlTransaction As SqlTransaction, ByVal po_sqlConnection As SqlConnection)
        Try
            po_sqlTransaction.Rollback()
            po_sqlTransaction.Dispose()
            po_sqlConnection.Close()
            po_sqlConnection.Dispose()
        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_commitTransaction(ByVal po_sqlTransaction As SqlTransaction, ByVal po_sqlConnection As SqlConnection)
        Try
            po_sqlTransaction.Commit()
            po_sqlTransaction.Dispose()
            po_sqlConnection.Close()
            po_sqlConnection.Dispose()
        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

#Region "Conexion"

    Public Function sbo_conectar(ByVal ps_SBOUsrName As String _
                           , ByVal ps_SBOPass As String) As SAPbobsCOM.Company
        Try

            ' Se declara las variables del metodo
            Dim li_resultado As Integer = 0

            ' Se declara las variables que obtendran los mensajess de error que puedan ocurrir
            Dim li_error As Integer = 0
            Dim ls_error As String = ""

            ' Se asigna las propiedades al objeto Company
            SBOCompany = New SAPbobsCOM.Company
            SBOCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2017
            SBOCompany.DbUserName = S_DbUserName
            SBOCompany.DbPassword = S_SQLPassword
            SBOCompany.Server = "192.168.1.2"
            SBOCompany.LicenseServer = "192.168.1.2:30000"
            SBOCompany.CompanyDB = S_CompanyDB
            SBOCompany.UserName = ps_SBOUsrName
            SBOCompany.Password = ps_SBOPass
            SBOCompany.UseTrusted = False

            ' Se conecta la compañia
            li_resultado = SBOCompany.Connect()

            If li_resultado = 0 Then
                'Se retorna el objeto Company
                Return SBOCompany
            Else
                SBOCompany.GetLastError(li_error, ls_error)
                'MsgBox("Error al conectar a la compañia SAP: " & li_error.ToString & " - " & ls_error)
                sub_mostrarMensaje("Error al conectar a la compañia SAP: " & li_error.ToString & " - " & ls_error, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "ModuleSQLComun", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sap)
                Return Nothing
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Function bol_comprobarConexion(ByVal ps_SQLUsrName As String _
                                        , ByVal ps_SQLPass As String _
                                        , ByVal ps_DBServer As String _
                                        , ByVal ps_SBOCompany As String) As Boolean
        Try

            ' Se realiza la conexion para comprobar los datos recibidos
            Dim cn As New SqlConnection(sqlConexion.Conectar(ps_DBServer, ps_SBOCompany, ps_SQLUsrName, ps_SQLPass))

            ' Si no ocurrio ningun error, se almacenan los datos recibidos
            S_DbUserName = ps_SQLUsrName
            S_SQLPassword = ps_SQLPass
            S_CompanyDB = ps_SBOCompany
            S_Server = ps_DBServer

            ' Si no ocurrio ningun error, se retorna TRUE
            Return True

        Catch ex As Exception
            MsgBox("Error de Conexion: " & ex.ToString)
            Return False
        End Try
    End Function

#End Region

#Region "DataTable"

    Public Function dtb_convertRecordSetADataTable(ByVal po_recordSet As SAPbobsCOM.Recordset)
        Try

            ' Se declara las variables del metodo
            Dim lo_dataTable As New DataTable

            ' Se asigna el nombre al DataTable
            lo_dataTable.TableName = "dtRecordSet"

            ' Se crea las columnas del DataTable de acuerdo a las columnas del recordSet
            For i As Integer = 0 To po_recordSet.Fields.Count - 1

                ' Se crea las columnas del DataTable de acuerdo a los campos del recordSet
                lo_dataTable.Columns.Add(po_recordSet.Fields.Item(i).Name)

            Next

            ' Se ingresa los datos al DataTable
            While Not (po_recordSet.EoF)

                ' Se añade una linea al DataTable
                Dim lo_dtRow As DataRow = lo_dataTable.NewRow

                ' Se recorre las columnas del DataTable
                For Each lo_dtCol As DataColumn In lo_dataTable.Columns

                    ' Se añade el valor al registro nuevo del DataTable
                    lo_dtRow(lo_dtCol.ColumnName) = po_recordSet.Fields.Item(lo_dtCol.ColumnName).Value

                Next

                ' Se añade la fila al DataTable
                lo_dataTable.Rows.Add(lo_dtRow)

                ' Se recorre el siguiente registro del RecordsSet
                po_recordSet.MoveNext()

            End While

            ' Se retorna el dataTable
            Return lo_dataTable

        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

#End Region

#Region "Consultas_Comunes"

    Public Function dtb_buscar(ByVal ps_sql As String) As DataTable
        Try

            ' Se declara una variable para la cadena SQL
            Dim ls_sql As String = ps_sql

            ' Se obtiene las ofertas de venta
            Return dtb_ejecutarSQL_NET(ls_sql)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try

    End Function

    Public Function dtb_obtCampo(ByVal ps_tabla As String, ByVal ps_campo As String)
        Try

            ' Se declara una variable para la cadena SQL
            Dim ls_sql As String = "exec gmi_sp_obtColumnasTabla '" & ps_tabla & "', '" & ps_campo & "'"

            ' Se obtiene las ofertas de venta
            Return dtb_ejecutarSQL_NET(ls_sql)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function int_autoIntId(ByVal ps_tabla As String, ByVal ps_campoId As String) As Integer
        Try

            ' Se declara una variable para la cadena SQL
            Dim ls_sql As String = "select isnull(max(" & ps_campoId & ") + 1, 1) from " & ps_tabla

            ' Se obtiene las ofertas de venta
            Return dtb_ejecutarSQL_NET(ls_sql).Rows(0)(0)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function str_obtMonLocal() As String
        Try

            ' Se declara una variable para la cadena SQL
            Dim ls_sql As String = "exec gmi_sp_obtMonLocal"

            ' Se realiza la consulta
            Return dtb_ejecutarSQL_NET(ls_sql).Rows(0)(0)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return ""
        End Try
    End Function

    Public Function dtb_obtEstructuraTabla(ByVal ps_tabla As String) As DataTable
        Try

            ' Se declara una variable para la cadena SQL
            Dim ls_sql As String = "select * from " & ps_tabla & " where 1 = 0 "

            ' Se obtiene las ofertas de venta
            Return dtb_ejecutarSQL_NET(ls_sql)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function dbl_obtenerTipoCambio(ByVal ps_moneda As String, ByVal ps_fecha As String) As Double
        Try

            ' Se declara una variable para la cadena SQL
            Dim ls_sql As String = "execute gmi_sp_obtenerTipoCambioPLL '" & ps_moneda & "', '" & ps_fecha & "', '" & S_CompanyDB & "'"

            ' Se ejecuta la consulta
            Dim lo_dtb As DataTable = ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)

            ' Se verifica si se obtuvo algun resultado
            If lo_dtb.Rows.Count > 0 Then

                ' Se retorna el tipo de cambio para la fecha especificada
                Return Double.Parse(lo_dtb.Rows(0).Item(0).ToString)

            Else

                ' Se retorna 0
                Return 0.0

            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return 0.0
        End Try
    End Function

    Public Function str_obtenerParamSQLPorTipo(ByVal po_param As Object) As String
        Try

            ' De acuerdo al tipo de objeto se asigna la sintaxis del parametro
            If po_param Is Nothing Then
                Return "null"
            ElseIf TypeOf (po_param) Is String Then
                Return "'" & CStr(po_param).Replace("'", "''") & "'"
            ElseIf TypeOf (po_param) Is Date Then
                If CDate(po_param).ToString("yyyyMMdd") = "00010101" Then
                    Return "null"
                Else
                    Return "'" & CDate(po_param).ToString("yyyyMMdd") & "'"
                End If

            ElseIf TypeOf (po_param) Is Integer Or TypeOf (po_param) Is Long Or TypeOf (po_param) Is Double Then
                Return CStr(po_param).Replace(",", ".")
            Else
                Return "null"
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
            Return "null"
        End Try
    End Function

    Public Function str_obtenerParamSQLPorTipo(ByVal po_param As Object, ByVal po_type As Type) As String
        Try

            ' De acuerdo al tipo de objeto se asigna la sintaxis del parametro
            If po_param Is Nothing Or IsDBNull(po_param) = True Then
                Return "null"
            ElseIf po_type = GetType(String) Then
                Return "'" & CStr(po_param).Replace("'", "''") & "'"
            ElseIf po_type = GetType(Date) Then
                If CDate(po_param).ToString("yyyyMMdd") = "00010101" Then
                    Return "null"
                Else
                    Return "'" & CDate(po_param).ToString("yyyyMMdd") & "'"
                End If

            ElseIf po_type = GetType(Integer) Or po_type = GetType(Long) Or po_type = GetType(Double) Then
                Return CStr(po_param).Replace(",", ".")
            Else
                Return "null"
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
            Return "null"
        End Try
    End Function

    Public Function str_obtenerParamSQLPorTipoConLike(ByVal po_param As Object) As String
        Try

            ' De acuerdo al tipo de objeto se asigna la sintaxis del parametro
            If po_param Is Nothing Then
                Return "null"
            ElseIf TypeOf (po_param) Is String Then
                Return "'%" & CStr(po_param) & "%'"
            ElseIf TypeOf (po_param) Is Date Then
                If CDate(po_param).ToString("yyyyMMdd") = "00010101" Then
                    Return "null"
                Else
                    Return "'" & CDate(po_param).ToString("yyyyMMdd") & "'"
                End If

            ElseIf TypeOf (po_param) Is Integer Or TypeOf (po_param) Is Long Or TypeOf (po_param) Is Double Then
                Return CStr(po_param).Replace(",", ".")
            Else
                Return "null"
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
            Return "null"
        End Try
    End Function

    Public Function int_obtDecimalesImp() As Integer
        Try

            ' Se declara una variable para la cadena SQL
            Dim ls_sql As String = "exec gmi_sp_obtDecimalesImp"

            ' Se realiza la consulta
            Return dtb_ejecutarSQL_NET(ls_sql).Rows(0)(0)

        Catch ex As Exception
            Return 6
        End Try
    End Function

    Public Function str_verExisteCuentaBancaria(ByVal ps_cuenta As String) As String
        Try

            ' Se declara una variable para la cadena SQL
            Dim ls_sql As String = "exec gmi_sp_verExisteCuentaBancaria '" & ps_cuenta & "'"

            ' Se realiza la consulta
            Return dtb_ejecutarSQL_NET(ls_sql).Rows(0)(0)

        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function dtb_obtParamProcedure(ByVal ps_nomProcedure As String) As DataTable
        Try

            ' Se declara una variable para la cadena SQL
            Dim ls_sql As String = "exec gmi_sp_obtParamProcedure '" & ps_nomProcedure & "'"

            ' Se obtiene las ofertas de venta
            Return dtb_ejecutarSQL_NET(ls_sql)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function lon_obtVersion(ps_compania As String) As Long
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_obtVersion '" & ps_compania & "'"

            ' Se ejecuta la consulta
            Return CType(ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql).Rows(0).Item(0), Long)

        Catch ex As Exception
            Return -1
        End Try
    End Function

    Public Function int_verExistUsr(ps_usr As String) As Integer
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_verExistUsr '" & ps_usr & "'"

            ' Se ejecuta la consulta
            Return CType(ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql).Rows(0).Item(0), Integer)

        Catch ex As Exception
            Return -1
        End Try
    End Function

    Public Function str_verUsrAdmin()
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_verUsrAdmin"

            ' Se ejecuta la transaccion
            Return str_ejecutarSQL_NET(ls_sql)

        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function str_verEstadoPeriodo(pd_fecha As Date) As String
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_verEstadoPeriodo '" & pd_fecha.ToString("yyyyMMdd") & "'"

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)(0)(0)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "ModuleSQLComun", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return ""
        End Try
    End Function

    Public Function str_verTransId_PagoRecibido(DocentryT As Integer) As Integer
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_verTransId_PagoRecibido '" & Convert.ToString(DocentryT) & "'"

            ' Se ejecuta la consulta
            Return ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)(0)(0)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "ModuleSQLComun", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return ""
        End Try
    End Function



    Public Function dbl_obtCuentaGanancia_pl() As String

        Dim result1 As String
            '' Se declara una variable para la cadena SQL
            'Dim ls_sql As String = "execute gmi_sp_cuentaGananciaDiferenciaTC "

            Try

            ' Se declara una variable para la cadena SQL
            Dim ls_sql As String = "exec gmi_sp_cuentaGananciaDiferenciaTC"
            'Dim ls_sql As String = "exec gmi_sp_obtMonLocal"


            Dim lo_dtb As DataTable = ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)

            ' Se verifica si se obtuvo algun resultado
            If lo_dtb.Rows.Count > 0 Then

                    ' Se retorna el tipo de cambio para la fecha especificada
                    Return lo_dtb.Rows(0).Item(0).ToString

                Else

                    ' Se retorna 0
                    Return ""

                End If
            'FIN


            ' Se realiza la consulta




            '' Se ejecuta la consulta
            'Dim lo_dtb As DataTable = ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql)

            '' Se verifica si se obtuvo algun resultado
            'If lo_dtb.Rows.Count > 0 Then

            '    ' Se retorna el tipo de cambio para la fecha especificada
            '    Return (lo_dtb.Rows(0).Item(0).ToString)

            'Else

            '    ' Se retorna 0
            '    Return ""

            'End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return ""
        End Try
    End Function

#End Region

#Region "TransaccionesComunes"

    Public Function str_anadir(ByVal po_objeto As Object, Optional ByVal po_sqlConn As SqlConnection = Nothing, Optional ByVal po_sqlTran As SqlTransaction = Nothing, Optional ByVal po_id As Object = Nothing) As String
        Try

            ' Se declara una variable para la sentencia Insert
            Dim ls_insert As String = ""

            ' Se declara una variable para el armado de la sentencia SQL Insert
            Dim ls_camposIns As String = ""
            Dim ls_valoresIns As String = ""

            ' Se declara una variable para el separador de los campos y valores
            Dim ls_sep As String = ""

            ' Se declara una variable para que contenga el Id del objeto cabecera en caso de que se deba ingresar un objeto de detalle
            Dim lo_id As Object = Nothing

            ' Se obtiene la tabla asociada desde la anotacion del objeto
            Dim ls_tabla As String = "" ' Se declara una variable para obtener la tabla asociada al objeto
            Dim ls_idTabla As String = "" ' Se declara una variable para obtener el id de la tabla asociada al objeto
            Dim lb_idIntAuto As Boolean = True ' Se declara una variable para obtener el id de la tabla asociada al objeto
            Dim lb_esDetalle As Boolean = False ' Se declara una variable para verificar si el objeto actual corresponde a un objeto de detalle
            Dim lo_annEntidad As annEntidad = ann_obtenerAnotacion(po_objeto, GetType(annEntidad))
            If Not lo_annEntidad Is Nothing Then
                ls_tabla = lo_annEntidad.Tabla
                lb_esDetalle = lo_annEntidad.esDetalle
                ls_idTabla = lo_annEntidad.IdTabla
                lb_idIntAuto = lo_annEntidad.EsIdIntAuto
            Else
                Return "El objeto no tiene una anotacion de Entidad. Por tal motivo, no se puede hacer la adicion del registro a la base de datos."
            End If

            ' Se declara una lista de objetos que contendrá los objetos de detalle
            Dim lo_objsDetalle As New List(Of Object)

            ' Se recorre los atributos del objeto
            For Each lo_field As FieldInfo In po_objeto.GetType.GetFields(BindingFlags.NonPublic Or BindingFlags.Public Or Reflection.BindingFlags.Instance)

                ' Se obtiene el atributo y se verifica si es de tipo annAtributo
                Dim lo_annAtributo As annAtributo = ann_obtenerAnotacion(lo_field, GetType(annAtributo))

                ' Se verifica si se obtuvo el atributo
                If Not lo_annAtributo Is Nothing Then

                    ' Se verifica si es un atributo que corresponde a un campo de base de datos
                    If lo_annAtributo.esCampoBD = True Then

                        ' Se verifica si es un atributo que correspnde a un detalle
                        If lo_annAtributo.esDetalle = True Then

                            ' Se indica que el objeto cuenta con un detalle para realizar la adición del mismo luego de la inserción de la cabecera
                            lo_objsDetalle.Add(lo_field.GetValue(po_objeto))

                        Else

                            ' Se verifica si el campo actual es un campo de sistema
                            If lo_annAtributo.deSistema = True Then

                                ' Se verifica si el campo existe en la tabla
                                Dim li_indExistCampo As Integer = int_verExistCampoDeTable(lo_annAtributo.campoBD, lo_annEntidad.Tabla)
                                If li_indExistCampo < 1 Then
                                    GoTo etq_sigCampo
                                End If

                            End If

                            ' Se declara una variable para obtener el valor del campo 
                            Dim lo_valor As Object

                            ' Se verifica si el campo corresponde al campo de id de la tabla
                            If lo_annAtributo.campoBD.ToLower = ls_idTabla.ToLower Then

                                ' Se verifica si se recibio un id de un objeto cabecera que indique que el objeto actual es de detalle
                                If po_id Is Nothing Then

                                    ' Se obtiene el valor correspondiente al Id
                                    If lb_idIntAuto = True Then
                                        ' Se autogenera el id de acuerdo a la tabla y al nombre del campo
                                        lo_valor = int_autoIntId(ls_tabla, ls_idTabla)
                                    Else
                                        lo_valor = lo_field.GetValue(po_objeto)
                                    End If

                                    ' Se almacena el valor del campo id de la tabla
                                    lo_id = lo_valor

                                Else ' Si el objeto actual es de detalle
                                    lo_valor = po_id
                                End If

                            Else
                                lo_valor = lo_field.GetValue(po_objeto)
                            End If

                            ' Se verifica si el atributo es obligatorio
                            If lo_annAtributo.Obligatorio = True Then

                                ' Se verifica el valor obtenido para el insert del SQL
                                If str_obtenerParamSQLPorTipo(lo_valor).ToLower.Replace(" ", "") = "null" Or str_obtenerParamSQLPorTipo(lo_valor).Replace(" ", "") = "-1" Or str_obtenerParamSQLPorTipo(lo_valor).Replace(" ", "") = "''" Then
                                    Return "Debe ingresar el valor correspondiente en el campo <" & lo_annAtributo.campoBD & ">."
                                End If

                            End If

                            ' Se construye la sentencia de inserción en la base de datos
                            ' - Se añade los valores al insert
                            ls_camposIns = ls_camposIns & ls_sep & " " & lo_annAtributo.campoBD
                            ls_valoresIns = ls_valoresIns & ls_sep & " " & str_obtenerParamSQLPorTipo(lo_valor)

                            ' Se asigna el valor de una coma(,) al separador
                            ls_sep = ","

                        End If

                    End If

                End If
etq_sigCampo:
            Next

            ' Se construye la sentencia insert
            If ls_tabla.Trim <> "" And ls_camposIns.Trim <> "" And ls_valoresIns.Trim <> "" Then ' Se verifique que el insert tenga valores y campos

                ' Se construye la sentencia insert
                ls_insert = "insert into " & ls_tabla & "(" & ls_camposIns & ") values (" & ls_valoresIns & ")"

            End If

            ' Se declara una variable para obtener el resultado de la insercion
            Dim ls_resultInsert As String = ""

            ' Se especifica el inicio de la transaccion. 
            ' <<< BEGIN TRANSACTION >>>
            Dim lo_sqlConnection As SqlConnection
            Dim lo_sqlTran As SqlTransaction

            ' Solo se utilizara el objeto SQL Transaction cuando el objeto no sea de detalle
            If lb_esDetalle = False Then
                lo_sqlConnection = con_obtSQLConnection()
                lo_sqlTran = trn_obtSQLTransaction(lo_sqlConnection, ls_tabla)
            End If

            ' Se realiza la insercion
            If lb_esDetalle = False Then
                ls_resultInsert = str_ejecutarSQL_NET(ls_insert, lo_sqlConnection, lo_sqlTran)
            Else
                ls_resultInsert = str_ejecutarSQL_NET(ls_insert, po_sqlConn, po_sqlTran)
            End If

            ' Se verifica el resultado del insert
            If ls_resultInsert.Trim = "" Then

                ' Se realiza la insercion de los objetos de detalle
                ' - Se recorre los objetos del listado de objetos de detalle
                For Each lo_objDet As Object In lo_objsDetalle

                    ' Se llama al mismo metodo para realizar la inserción
                    If lb_esDetalle = False Then
                        ls_resultInsert = str_anadirDetalle(lo_objDet, lo_sqlConnection, lo_sqlTran, lo_id)
                    Else
                        ls_resultInsert = str_anadirDetalle(lo_objDet, po_sqlConn, po_sqlTran, lo_id)
                    End If

                    ' Se verifica el resultado de la insercion
                    If ls_resultInsert.Trim <> "" Then

                        ' Se culmina la transaccion con rollback. Solo se utilizara el objeto SQL Transaction cuando el objeto no sea de detalle
                        ' <<< ROLLBACK TRANSACTION >>>
                        If lb_esDetalle = False Then sub_rollBackTransaction(lo_sqlTran, lo_sqlConnection)

                        ' Se finaliza el recorrido
                        Exit For

                    End If

                Next

                ' Se verifica el resultado de la insercion del detalle
                If ls_resultInsert.Trim = "" Then

                    ' Se especifica el final de la transaccion. Solo se utilizara el objeto SQL Transaction cuando el objeto no sea de detalle
                    ' <<< COMMIT TRANSACTION >>>
                    If lb_esDetalle = False Then sub_commitTransaction(lo_sqlTran, lo_sqlConnection)

                End If

                ' Se retorna el resultado de la operacion
                Return ls_resultInsert

            Else

                ' Se culmina la transaccion con rollback. Solo se utilizara el objeto SQL Transaction cuando el objeto no sea de detalle
                ' <<< ROLLBACK TRANSACTION >>>
                If lb_esDetalle = False Then sub_rollBackTransaction(lo_sqlTran, lo_sqlConnection)

                ' Se retorna el mensaje de error
                Return ls_resultInsert

            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message
        End Try
    End Function

    Private Function str_anadirDetalle(ByVal po_objeto As Object, Optional ByVal po_sqlConn As SqlConnection = Nothing, Optional ByVal po_sqlTran As SqlTransaction = Nothing, Optional ByVal po_id As Object = Nothing)
        Try

            ' Se declara una variable para el resultado de la insercion
            Dim ls_resultado As String = ""

            ' Se obtiene el id del objeto cabecera
            Dim lo_id As Object = po_id

            ' Se obtiene el listado de objetos de detalle dentro del objeto recibido
            Dim lo_lstObjDet As New List(Of Object)

            ' Se obtiene el atributo correspondiente al listado de objetos de detalle
            Dim lo_fieldDet As FieldInfo = po_objeto.GetType.GetField("o_lstObjs", BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance)

            ' Si se obtuvo el atributo que contiene el listado mencionado, se obtiene el mismo en la variable declarada
            If Not lo_fieldDet Is Nothing Then
                lo_lstObjDet = lo_fieldDet.GetValue(po_objeto)
            Else
                Return "No se obtuvo el listado de objetos de detalle desde la entidad comun de detalle."
            End If

            ' Se recorre el listado de objetos de detalle
            For Each lo_objDet As Object In lo_lstObjDet

                ' Se añade la informacion de dicho objeto a la base de datos
                ls_resultado = str_anadir(lo_objDet, po_sqlConn, po_sqlTran, lo_id)

                ' Se verifica el resultado de la insercion
                If ls_resultado.Trim <> "" Then ' Si ocurrio algun error
                    Return ls_resultado
                End If

            Next

            ' Si no ocurrio errores
            Return ls_resultado

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message
        End Try
    End Function

    Public Function str_actualizar(ByVal po_objeto As Object, Optional ByVal po_sqlConn As SqlConnection = Nothing, Optional ByVal po_sqlTran As SqlTransaction = Nothing, Optional ByVal po_id As Object = Nothing) As String
        Try

            ' Se declara una variable para la sentencia Insert
            Dim ls_update As String = ""

            ' Se declara una variable para el armado de la sentencia SQL Insert
            Dim ls_camposAct As String = ""
            Dim ls_condicion As String = " where "

            ' Se declara una variable para el separador de los campos y valores
            Dim ls_sep As String = ""

            ' Se declara una variable para que contenga el Id del objeto cabecera en caso de que se deba ingresar un objeto de detalle
            Dim lo_id As Object = Nothing

            ' Se obtiene la tabla asociada desde la anotacion del objeto
            Dim ls_tabla As String = "" ' Se declara una variable para obtener la tabla asociada al objeto
            Dim ls_idTabla As String = "" ' Se declara una variable para obtener el id de la tabla asociada al objeto
            Dim lb_idIntAuto As Boolean = True ' Se declara una variable para obtener el id de la tabla asociada al objeto
            Dim lb_esDetalle As Boolean = False ' Se declara una variable para verificar si el objeto actual corresponde a un objeto de detalle
            Dim lo_annEntidad As annEntidad = ann_obtenerAnotacion(po_objeto, GetType(annEntidad))
            If Not lo_annEntidad Is Nothing Then
                ls_tabla = lo_annEntidad.Tabla
                lb_esDetalle = lo_annEntidad.esDetalle
                ls_idTabla = lo_annEntidad.IdTabla
                lb_idIntAuto = lo_annEntidad.EsIdIntAuto
            Else
                Return "El objeto no tiene una anotacion de Entidad. Por tal motivo, no se puede hacer la adicion del registro a la base de datos."
            End If

            ' Se declara una lista de objetos que contendrá los objetos de detalle
            Dim lo_objsDetalle As New List(Of Object)

            ' Se recorre los atributos del objeto
            For Each lo_field As FieldInfo In po_objeto.GetType.GetFields(BindingFlags.NonPublic Or BindingFlags.Public Or Reflection.BindingFlags.Instance)

                ' Se obtiene el atributo y se verifica si es de tipo annAtributo
                Dim lo_annAtributo As annAtributo = ann_obtenerAnotacion(lo_field, GetType(annAtributo))

                ' Se verifica si se obtuvo el atributo
                If Not lo_annAtributo Is Nothing Then

                    ' Se verifica si es un atributo que corresponde a un campo de base de datos
                    If lo_annAtributo.esCampoBD = True Then

                        ' Se verifica si es un atributo que correspnde a un detalle
                        If lo_annAtributo.esDetalle = True Then

                            ' Se indica que el objeto cuenta con un detalle para realizar la adición del mismo luego de la inserción de la cabecera
                            lo_objsDetalle.Add(lo_field.GetValue(po_objeto))

                        Else

                            ' Se declara una variable para obtener el valor del campo 
                            Dim lo_valor As Object

                            ' Se verifica si el campo corresponde al campo de id de la tabla
                            If lo_annAtributo.campoBD.ToLower = ls_idTabla.ToLower Then

                                ' Se verifica si se recibio un id de un objeto cabecera que indique que el objeto actual es de detalle
                                If po_id Is Nothing Then

                                    ' Se obtiene el valor correspondiente al Id
                                    lo_valor = lo_field.GetValue(po_objeto)

                                    ' Se almacena el valor del campo id de la tabla
                                    lo_id = lo_valor

                                    ' Se construye la condicion del UPDATE
                                    ls_condicion = ls_condicion & " " & lo_annAtributo.campoBD & " = " & str_obtenerParamSQLPorTipo(lo_id)

                                Else ' Si el objeto actual es de detalle
                                    lo_valor = po_id
                                End If

                            Else

                                ' Se verifica si el campo es actualizable
                                If lo_annAtributo.seActualiza = True Then

                                    ' Se obtiene el valor del objeto
                                    lo_valor = lo_field.GetValue(po_objeto)

                                    ' Se verifica si el atributo es obligatorio
                                    If lo_annAtributo.Obligatorio = True Then

                                        ' Se verifica el valor obtenido para el update del SQL
                                        If str_obtenerParamSQLPorTipo(lo_valor).ToLower.Replace(" ", "") = "null" Or str_obtenerParamSQLPorTipo(lo_valor).Replace(" ", "") = "-1" Or str_obtenerParamSQLPorTipo(lo_valor).Replace(" ", "") = "''" Then
                                            Return "Debe ingresar el valor correspondiente en el campo <" & lo_annAtributo.campoBD & ">."
                                        End If

                                    End If

                                    ' Se construye la asignacion de campos de la sentencia update
                                    ls_camposAct = ls_camposAct & ls_sep & " " & lo_annAtributo.campoBD & " = " & str_obtenerParamSQLPorTipo(lo_valor)

                                    ' Se asigna el valor de una coma(,) al separador
                                    ls_sep = ","

                                End If

                            End If

                        End If

                    End If

                End If

            Next

            ' Se construye la sentencia insert
            If ls_tabla.Trim <> "" And ls_camposAct.Trim <> "" And ls_condicion.Trim <> "" Then ' Se verifique que el insert tenga valores y campos

                ' Se construye la sentencia insert
                ls_update = "update " & ls_tabla.Trim & " set " & ls_camposAct.Trim & " " & ls_condicion.Trim

            Else

                Return "No se formo la sentencia UPDATE de manera correcta: " & ls_update

            End If

            ' Se declara una variable para obtener el resultado de la insercion
            Dim ls_resultUpdate As String = ""

            ' Se especifica el inicio de la transaccion. 
            ' <<< BEGIN TRANSACTION >>>
            Dim lo_sqlConnection As SqlConnection
            Dim lo_sqlTran As SqlTransaction

            ' Solo se utilizara el objeto SQL Transaction cuando el objeto no sea de detalle
            If lb_esDetalle = False Then
                lo_sqlConnection = con_obtSQLConnection()
                lo_sqlTran = trn_obtSQLTransaction(lo_sqlConnection, ls_tabla)
            End If

            ' Se realiza la insercion
            If lb_esDetalle = False Then
                ls_resultUpdate = str_ejecutarSQL_NET(ls_update, lo_sqlConnection, lo_sqlTran)
            End If

            ' Se verifica el resultado del insert
            If ls_resultUpdate.Trim = "" Then

                ' Se realiza la actualizacion de los objetos de detalle
                ' - Se recorre los objetos del listado de objetos de detalle
                For Each lo_objDet As Object In lo_objsDetalle

                    ' Se realiza la actualizacion de los objetos de detalle
                    If lb_esDetalle = False Then
                        ls_resultUpdate = str_actualizarObjetosDetalle(lo_objDet, lo_sqlConnection, lo_sqlTran, lo_id)
                    Else
                        ls_resultUpdate = str_actualizarObjetosDetalle(lo_objDet, po_sqlConn, po_sqlTran, lo_id)
                    End If

                    ' Se verifica el resultado de la insercion
                    If ls_resultUpdate.Trim <> "" Then

                        ' Se culmina la transaccion con rollback. Solo se utilizara el objeto SQL Transaction cuando el objeto no sea de detalle
                        ' <<< ROLLBACK TRANSACTION >>>
                        If lb_esDetalle = False Then sub_rollBackTransaction(lo_sqlTran, lo_sqlConnection)

                        ' Se finaliza el recorrido
                        Exit For

                    End If

                Next

                ' Se verifica el resultado de la insercion del detalle
                If ls_resultUpdate.Trim = "" Then

                    ' Se especifica el final de la transaccion. Solo se utilizara el objeto SQL Transaction cuando el objeto no sea de detalle
                    ' <<< COMMIT TRANSACTION >>>
                    If lb_esDetalle = False Then sub_commitTransaction(lo_sqlTran, lo_sqlConnection)

                End If

                ' Se retorna el resultado de la operacion
                Return ls_resultUpdate

            Else

                ' Se culmina la transaccion con rollback. Solo se utilizara el objeto SQL Transaction cuando el objeto no sea de detalle
                ' <<< ROLLBACK TRANSACTION >>>
                If lb_esDetalle = False Then sub_rollBackTransaction(lo_sqlTran, lo_sqlConnection)

                ' Se retorna el mensaje de error
                Return ls_resultUpdate

            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message
        End Try
    End Function

    Private Function str_actualizarObjetosDetalle(ByVal po_objeto As Object, Optional ByVal po_sqlConn As SqlConnection = Nothing, Optional ByVal po_sqlTran As SqlTransaction = Nothing, Optional ByVal po_id As Object = Nothing)
        Try

            ' Se declara una variable para el resultado de las transacciones SQL
            Dim ls_resultado As String = ""

            ' Se declara una variable para la sentencia Delete que limpiara el objeto de detalle
            Dim ls_delete As String = ""

            ' Se obtiene la tabla asociada desde la anotacion del objeto
            Dim ls_tabla As String = "" ' Se declara una variable para obtener la tabla asociada al objeto
            Dim ls_idTabla As String = "" ' Se declara una variable para obtener el id de la tabla asociada al objeto
            Dim lb_idIntAuto As Boolean = True ' Se declara una variable para obtener el id de la tabla asociada al objeto
            Dim lb_esDetalle As Boolean = False ' Se declara una variable para verificar si el objeto actual corresponde a un objeto de detalle
            Dim lo_annEntidad As annEntidad = ann_obtenerAnotacion(po_objeto, GetType(annEntidad))
            If Not lo_annEntidad Is Nothing Then
                ls_tabla = lo_annEntidad.Tabla
                lb_esDetalle = lo_annEntidad.esDetalle
                ls_idTabla = lo_annEntidad.IdTabla
                lb_idIntAuto = lo_annEntidad.EsIdIntAuto
            Else
                Return "El objeto no tiene una anotacion de Entidad. Por tal motivo, no se puede hacer la adicion del registro a la base de datos."
            End If

            ' Se verifica si el objeto corresponde a un objeto de detalle
            If lb_esDetalle = False Then
                Return "Ocurrio un error. La anotación de la entidad indica que no es un detalle."
            End If

            ' Se obtiene el campo que verifica la actualizacion del detalle
            Dim lo_fieldSeAct As FieldInfo = po_objeto.GetType.GetField("o_seActualiza", BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.Instance)

            ' Se verifica si se obtuvo el campo que indica la actualizacion del detalle
            If lo_fieldSeAct = Nothing Then
                Return "No se pudo obtener el campo que indica la actualización del detalle."
            End If

            ' Se indica que el objeto de detalle no se actualizará desde este formulario
            Dim lb_seActualiza As Boolean = lo_fieldSeAct.GetValue(po_objeto)

            ' Se verifica si el objeto se actualiza
            If lb_seActualiza = False Then
                Return ""
            End If

            ' Se construye la sentencia Delete
            ls_delete = "delete " & ls_tabla & " where " & ls_idTabla & " = " & str_obtenerParamSQLPorTipo(po_id)

            ' Se limpia los registros correspondientes al objeto de detalle
            ls_resultado = str_ejecutarSQL_NET(ls_delete, po_sqlConn, po_sqlTran)

            ' Se verifica el resultado de la transaccion
            If ls_resultado.Trim = "" Then

                ' Se añade el objeto de detalle a la base de datos 
                ls_resultado = str_anadirDetalle(po_objeto, po_sqlConn, po_sqlTran, po_id)

            End If

            ' Se retorna el resultado de la(s) transaccion(es)
            Return ls_resultado

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message
        End Try
    End Function

    Public Function str_ejecDelete(ByVal ps_tabla As String, ByVal ps_campo As String, ByVal po_valor As Object, ByVal po_sqlConn As SqlConnection, ByVal po_sqlTran As SqlTransaction)
        Try

            ' Se declara una variable para la sentencia SQL
            Dim ls_sql As String = "delete " & ps_tabla & " where " & ps_campo & " = " & str_obtenerParamSQLPorTipo(po_valor)

            ' Se ejecuta la sentencia
            Return str_ejecutarSQL_NET(ls_sql, po_sqlConn, po_sqlTran)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message
        End Try
    End Function

    Public Function str_ejecInsertDtbRow(ByVal po_objeto As Object, ByVal po_row As DataRow, ByVal po_sqlConn As SqlConnection, ByVal po_sqlTran As SqlTransaction, Optional ByVal pi_id As Integer = Nothing) As String
        Try

            ' Se declara una variable para la sentencia Insert
            Dim ls_insert As String = ""

            ' Se declara una variable para el armado de la sentencia SQL Insert
            Dim ls_camposIns As String = ""
            Dim ls_valoresIns As String = ""

            ' Se declara una variable para el separador de los campos y valores
            Dim ls_sep As String = ""

            ' Se obtiene la anotacion de la entidad
            Dim lo_annEntidad As annEntidad = ann_obtenerAnotacion(po_objeto, GetType(annEntidad))

            ' Se verifica si se obtuvo la entidad
            If lo_annEntidad Is Nothing Then
                Return "El objeto no cuenta con una anotacion de Entidad. No es posible realizar la insercion de los datos del DataRow recibido."
            End If

            ' Se recorre los atributos del objeto
            For Each lo_field As FieldInfo In po_objeto.GetType.GetFields(BindingFlags.NonPublic Or BindingFlags.Public Or Reflection.BindingFlags.Instance)

                ' Se obtiene la anotacion de atributo
                Dim lo_annAtr As annAtributo = ann_obtenerAnotacion(lo_field, GetType(annAtributo))

                ' Se verifica si se obtuvo la entidad
                If lo_annAtr Is Nothing Then
                    Continue For
                End If

                ' Se verifica si el campo corresponde al campo de id de la tabla
                If lo_annAtr.campoBD.ToLower = lo_annEntidad.IdTabla.ToLower Then

                    ' Se declara una variable para el id entero autogenerado
                    Dim li_id As Integer = -1

                    ' Se obtiene el valor correspondiente al Id
                    If lo_annEntidad.EsIdIntAuto = True Then

                        ' Se verifica si se recibido el primer nuevo id autogenerado entero
                        If pi_id = Nothing Then
                            Return "La insercion de datos masiva desde un dataTable debe recibir como parametro el primer id entero autogenerado."
                        End If

                        ' Se autogenera el id de acuerdo a la tabla y al nombre del campo
                        'li_id = int_autoIntId(lo_annEntidad.Tabla, lo_annEntidad.IdTabla)
                        li_id = pi_id

                    Else

                        GoTo Continuar

                    End If

                    ' Se construye la sentencia de inserción en la base de datos
                    ' - Se añade los valores al insert
                    ls_camposIns = ls_camposIns & ls_sep & " " & lo_annAtr.campoBD
                    ls_valoresIns = ls_valoresIns & ls_sep & " " & str_obtenerParamSQLPorTipo(li_id)

                    ' Se asigna el valor de una coma(,) al separador
                    ls_sep = ","

                    ' Se sigue con el siguiente atributo del objeto
                    GoTo etq_sigCampo

                End If

Continuar:

                ' Se verifica si el campo actual corresponde a un campo de sistema
                If lo_annAtr.deSistema = True Then

                    ' Se verifica si el campo existe en la tabla
                    Dim li_indExistCampo As Integer = int_verExistCampoDeTable(lo_annAtr.campoBD, lo_annEntidad.Tabla)
                    If li_indExistCampo > 0 Then

                        ' Se declara una variable para el valor a ingresar
                        Dim lo_valor As Object = Nothing

                        ' Se verifica el nombre del campo del sistema
                        If lo_annAtr.campoBD = "FechaCrea" Then
                            lo_valor = dte_obtFechaActual()
                        ElseIf lo_annAtr.campoBD = "winUsrCrea" Then
                            lo_valor = str_obtWinUsr()
                        ElseIf lo_annAtr.campoBD = "nomPCCrea" Then
                            lo_valor = str_obtNomPC()
                        ElseIf lo_annAtr.campoBD = "ipPCCrea" Then
                            lo_valor = str_obtIpPC()
                        End If

                        ' Se verifica si se obtuvo algun valor
                        If Not lo_valor Is Nothing Then

                            ' Se construye la sentencia de inserción en la base de datos
                            ' - Se añade los valores al insert
                            ls_camposIns = ls_camposIns & ls_sep & " " & lo_annAtr.campoBD
                            ls_valoresIns = ls_valoresIns & ls_sep & " " & str_obtenerParamSQLPorTipo(lo_valor)

                            ' Se asigna el valor de una coma(,) al separador
                            ls_sep = ","

                        End If

                    End If

                    ' Se sigue con el siguiente atributo del objeto
                    GoTo etq_sigCampo

                End If

                ' Se recorre las columnas del dataTable 
                For Each lo_column As DataColumn In po_row.Table.Columns

                    ' Se verifica si el nombre de la columna es el mismo al nombre del atributo actual
                    If lo_column.ColumnName.Trim.ToLower = lo_annAtr.campoBD.Trim.ToLower Then

                        ' Se declara una variable para obtener el valor del campo 
                        Dim lo_valor As Object

                        ' Se verifica si el campo corresponde al campo de id de la tabla
                        If lo_annAtr.campoBD.ToLower = lo_annEntidad.IdTabla.ToLower Then

                            ' Se obtiene el valor correspondiente al Id
                            If lo_annEntidad.EsIdIntAuto = True Then

                                ' Se verifica si se recibido el primer nuevo id autogenerado entero
                                If pi_id = Nothing Then
                                    Return "La insercion de datos masiva desde un dataTable debe recibir como parametro el primer id entero autogenerado."
                                End If

                                ' Se autogenera el id de acuerdo a la tabla y al nombre del campo
                                'li_id = int_autoIntId(lo_annEntidad.Tabla, lo_annEntidad.IdTabla)
                                lo_valor = pi_id

                            Else
                                'lo_valor = lo_field.GetValue(po_objeto)
                                lo_valor = po_row(lo_column.ColumnName)
                            End If

                        Else
                            'lo_valor = lo_field.GetValue(po_objeto)
                            lo_valor = po_row(lo_column.ColumnName)
                        End If

                        ' Se construye la sentencia de inserción en la base de datos
                        ' - Se añade los valores al insert
                        ls_camposIns = ls_camposIns & ls_sep & " " & lo_annAtr.campoBD
                        ls_valoresIns = ls_valoresIns & ls_sep & " " & str_obtenerParamSQLPorTipo(lo_valor, lo_field.FieldType)

                        ' Se asigna el valor de una coma(,) al separador
                        ls_sep = ","

                        ' Se sigue con el siguiente atributo del objeto
                        GoTo etq_sigCampo

                    End If

                Next

etq_sigCampo:

            Next

            ' Se construye la sentencia insert
            If lo_annEntidad.Tabla.Trim <> "" And ls_camposIns.Trim <> "" And ls_valoresIns.Trim <> "" Then ' Se verifique que el insert tenga valores y campos

                ' Se construye la sentencia insert
                ls_insert = "insert into " & lo_annEntidad.Tabla & "(" & ls_camposIns & ") values (" & ls_valoresIns & ")"

            End If

            ' Se ejecuta la sentencia
            Return str_ejecutarSQL_NET(ls_insert, po_sqlConn, po_sqlTran)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message
        End Try
    End Function

#End Region

#Region "BusquedaObj"

    Public Function dtb_buscar(ByVal po_objeto As Object) As DataTable
        Try

            ' Se declara una variable para la sentencia Insert
            Dim ls_select As String = ""

            ' Se declara una variable para el armado de la sentencia SQL Insert
            Dim ls_condicion As String = " where 1 = 1 "

            ' Se declara una variable para el separador de los campos y valores
            Dim ls_sep As String = ""

            ' Se declara una variable para que contenga el Id del objeto cabecera en caso de que se deba ingresar un objeto de detalle
            Dim lo_id As Object = Nothing

            ' Se obtiene la tabla asociada desde la anotacion del objeto
            Dim ls_tabla As String = "" ' Se declara una variable para obtener la tabla asociada al objeto
            Dim ls_idTabla As String = "" ' Se declara una variable para obtener el id de la tabla asociada al objeto
            Dim lb_idIntAuto As Boolean = True ' Se declara una variable para obtener el id de la tabla asociada al objeto
            Dim lb_esDetalle As Boolean = False ' Se declara una variable para verificar si el objeto actual corresponde a un objeto de detalle
            Dim lo_annEntidad As annEntidad = ann_obtenerAnotacion(po_objeto, GetType(annEntidad))
            If Not lo_annEntidad Is Nothing Then
                ls_tabla = lo_annEntidad.Tabla
                lb_esDetalle = lo_annEntidad.esDetalle
                ls_idTabla = lo_annEntidad.IdTabla
                lb_idIntAuto = lo_annEntidad.EsIdIntAuto
            Else
                Return Nothing
            End If

            ' Se recorre los atributos del objeto
            For Each lo_field As FieldInfo In po_objeto.GetType.GetFields(BindingFlags.NonPublic Or BindingFlags.Public Or Reflection.BindingFlags.Instance)

                ' Se verifica la anotacion del campo
                For Each lo_ann As Object In lo_field.GetCustomAttributes(True)

                    ' Se obtiene el atributo y se verifica si es de tipo annAtributo
                    Dim lo_annAtributo As annAtributo = CType(lo_ann, annAtributo)

                    ' Se verifica si se obtuvo el atributo
                    If Not lo_annAtributo Is Nothing Then

                        ' Se verifica si es un atributo que corresponde a un campo de base de datos
                        If lo_annAtributo.esCampoBD = True Then

                            ' Se verifica si es un atributo que correspnde a un detalle
                            If lo_annAtributo.deBusqueda = True Then

                                ' Se declara una variable para obtener el valor del campo 
                                Dim lo_valor As Object

                                ' Se obtiene el valor del objeto
                                lo_valor = lo_field.GetValue(po_objeto)

                                ' Se verifica si se obtuvo algun valor
                                If Not lo_valor Is Nothing Then

                                    ' Se asigna el valor de una coma(,) al separador
                                    ls_sep = " and "

                                    ' Se obtiene el valor de la condicion
                                    Dim ls_valor As String = str_obtenerParamSQLPorTipoConLike(lo_valor)

                                    ' Se construye la condicion de la sentencia SELECT
                                    If Not ls_valor.Trim = "''" And Not ls_valor.Trim = "null" And Not ls_valor.Trim = "-1" And Not ls_valor.Trim = "00010101" Then

                                        ' Se verifica el tipo del campo de la entidad
                                        If lo_field.FieldType = GetType(String) Then
                                            ls_condicion = ls_condicion & ls_sep & " isnull(" & lo_annAtributo.campoBD & ", '') like " & ls_valor & " "
                                        Else
                                            ls_condicion = ls_condicion & ls_sep & " " & lo_annAtributo.campoBD & " = " & ls_valor
                                        End If


                                    End If

                                End If

                            End If

                        End If

                    End If

                Next

            Next

            ' Se construye la sentencia SELECT de acuerdo al tipo de objeto
            If lo_annEntidad.esDetalle = False Then
                ls_select = "select * from " & ls_tabla & " " & ls_condicion & " order by " & lo_annEntidad.IdTabla & " desc"
            Else
                ' Se verifica si al objeto de detalle se le ha especificado un campo de enumeracion
                If lo_annEntidad.DetCampoEnum.Trim <> "" Then
                    ls_select = "select * from " & ls_tabla & " " & ls_condicion & " order by " & lo_annEntidad.DetCampoEnum & " asc"
                Else
                    ls_select = "select * from " & ls_tabla & " " & ls_condicion
                End If

            End If

            ' Se realiza la consulta
            Return dtb_ejecutarSQL_NET(ls_select)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

#End Region

#Region "ObjetosSQL"

    Public Function int_indExistObjSQL(ByVal ps_nomObjSQL As String) As Integer
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "select count(name) from sys.objects where name like '" & ps_nomObjSQL & "'"

            ' Se ejecuta la consulta
            Return CInt(ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql).Rows(0).Item(0))

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function int_verExistCampoDeTable(ByVal ps_campo As String, ByVal ps_tabla As String) As Integer
        Try

            ' Se declara una variable para la consulta SQL
            Dim ls_sql As String = "exec gmi_sp_verExistCampoDeTable '" & ps_campo & "', '" & ps_tabla & "'"

            ' Se ejecuta la consulta
            Return CInt(ModuleSQLComun.dtb_ejecutarSQL_NET(ls_sql).Rows(0).Item(0))

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
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
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
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
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

#End Region

End Module