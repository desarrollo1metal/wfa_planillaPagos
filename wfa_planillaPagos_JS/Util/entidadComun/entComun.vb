Imports Util
Imports System.Reflection
Imports System.Runtime.Remoting

Public Class entComun

    ' Atributos comunes

    <annAtributo(True, False, "FechaCrea", "Fecha de Creacion", False, True, False, True)>
    Protected d_FechaCrea As Date
    <annAtributo(True, False, "FechaAct", "Fecha de Actualizacion", False, True, True, True)>
    Protected d_FechaAct As Date
    <annAtributo(True, False, "winUsrCrea", "Usuario Windows Creador", False, True, False, True)>
    Protected s_winUsrCrea As String
    <annAtributo(True, False, "nomPCCrea", "Nombre PC Creador", False, True, False, True)>
    Protected s_nomPCCrea As String
    <annAtributo(True, False, "ipPCCrea", "IP PC Creador", False, True, False, True)>
    Protected s_ipPCCrea As String

    Public Property FechaCrea() As Date
        Get
            Return d_FechaCrea
        End Get
        Set(ByVal value As Date)
            d_FechaCrea = value
        End Set
    End Property

    Public Property FechaAct() As Date
        Get
            Return d_FechaAct
        End Get
        Set(ByVal value As Date)
            d_FechaAct = value
        End Set
    End Property

    Public Property WinUsrCrea() As String
        Get
            Return s_winUsrCrea
        End Get
        Set(ByVal value As String)
            s_winUsrCrea = value
        End Set
    End Property

    Public Property NombrePC() As String
        Get
            Return s_nomPCCrea
        End Get
        Set(ByVal value As String)
            s_nomPCCrea = value
        End Set
    End Property

    Public Property IpPCCrea() As String
        Get
            Return s_ipPCCrea
        End Get
        Set(ByVal value As String)
            s_ipPCCrea = value
        End Set
    End Property

#Region "ProcesoNegocio_Comun"

    Public Function str_anadir() As String
        Try

            ' Se inicializa los atributos comunes
            FechaCrea = Now.Date
            FechaAct = Now.Date
            WinUsrCrea = str_obtWinUsr()
            NombrePC = str_obtNomPC()
            IpPCCrea = str_obtIpPC()

            ' Se realiza la insercion de los datos del objeto en la base de datos
            Return ModuleSQLComun.str_anadir(Me)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message
        End Try
    End Function

    Public Function str_actualizar() As String
        Try

            ' Se inicializa los atributos comunes
            FechaAct = Now.Date

            ' Se realiza la insercion de los datos del objeto en la base de datos
            Return ModuleSQLComun.str_actualizar(Me)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message
        End Try
    End Function

    Public Function dtb_buscar() As DataTable
        Try

            ' Se obtiene el resultado de la busqueda
            Return ModuleSQLComun.dtb_buscar(Me)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Function obj_obtPorCodigo(ByVal po_id As Object) As Object
        Try

            ' Se obtiene el objeto por codigo
            Return obj_obtObjetoPorCodigo(Me, po_id)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Private Function obj_obtObjetoPorCodigo(ByVal po_objeto As Object, ByVal po_id As Object) As Object
        Try

            ' Se obtiene la anotacion de la entidad
            Dim lo_annEnt As annEntidad = ann_obtenerAnotacion(po_objeto, GetType(annEntidad))

            ' Se verifica si se obtuvo la anotacion
            If lo_annEnt Is Nothing Then
                Return Nothing
            End If

            ' Se obtiene el nombre de la tabla
            Dim ls_tabla As String = lo_annEnt.Tabla
            Dim ls_idTabla As String = lo_annEnt.IdTabla

            ' Se declara una variable para la sentencia SQL
            Dim ls_sql As String = ""

            If lo_annEnt.esDetalle = True Then
                If lo_annEnt.DetCampoEnum.Trim = "" Then
                    ' Se obtiene los datos de la tabla
                    ls_sql = "select * from " & ls_tabla & " where " & ls_idTabla & " = " & str_obtenerParamSQLPorTipo(po_id)
                Else
                    ' Se obtiene los datos de la tabla
                    ls_sql = "select * from " & ls_tabla & " where " & ls_idTabla & " = " & str_obtenerParamSQLPorTipo(po_id) & " order by " & lo_annEnt.DetCampoEnum & " asc"
                End If
            Else
                ' Se obtiene los datos de la tabla
                ls_sql = "select * from " & ls_tabla & " where " & ls_idTabla & " = " & str_obtenerParamSQLPorTipo(po_id)
            End If

            ' Se ejecuta la consulta 
            Dim lo_dtb As DataTable = dtb_ejecutarSQL_NET(ls_sql)

            ' Se verifica si se obtuvo resultados
            If lo_dtb Is Nothing Then
                MsgBox("Ocurrio un error al obtener los datos de la entidad.")
            End If

            ' Se verifica si se obtuvo algun registro de la busqueda
            If lo_dtb.Rows.Count = 0 Then
                If lo_annEnt.esDetalle = True Then
                    If lo_annEnt.detObligatorio = True Then
                        MsgBox("La busqueda en la tabla " & ls_tabla & " para el id " & po_id.ToString & " no arrojo registros.")
                    End If
                Else
                    MsgBox("La busqueda en la tabla " & ls_tabla & " para el id " & po_id.ToString & " no arrojo registros.")
                End If
            End If

            ' Se recorre las filas del DataTable
            For Each lo_row As DataRow In lo_dtb.Rows

                ' Se asigna los datos del dataTable al objeto
                For Each lo_field As FieldInfo In po_objeto.GetType.GetFields(BindingFlags.NonPublic Or BindingFlags.Public Or Reflection.BindingFlags.Instance)

                    ' Se obtiene la anotacion del campo
                    Dim lo_annAtr As annAtributo = ann_obtenerAnotacion(lo_field, GetType(annAtributo))

                    ' Se verifica si se obtuvo la anotacion
                    If lo_annAtr Is Nothing Then
                        Continue For
                    End If

                    ' Se verifica si el atributo corresponde a un campo de base de datos
                    If lo_annAtr.esCampoBD = True Then

                        ' Se verifica si el atributo actual corresponde a un detalle
                        If lo_annAtr.esDetalle = False Then

                            ' Se asigna el valor de la columna correspondiente del dataTable a la entidad
                            lo_field.SetValue(po_objeto, obj_convertirATipo(lo_row(lo_annAtr.campoBD), lo_field.FieldType))

                        Else

                            ' Se ejecuta esta accion para el objeto de detalle
                            lo_field.SetValue(po_objeto, obj_obtObjetoPorCodigo(lo_field.GetValue(po_objeto), po_id))

                        End If

                    End If

                Next

                ' Se verifica si el objeto actual corresponde a una entidad de detalle
                If lo_annEnt.esDetalle = True Then

                    ' Se obtiene el metodo de adicion al detalle
                    Dim lo_method As MethodInfo = po_objeto.GetType.GetMethod("sub_anadir", BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance)

                    ' Se invoca el metodo de adicion al detalle
                    lo_method.Invoke(po_objeto, New Object() {})

                End If

            Next

            ' Se retorna el objeto 
            Return po_objeto

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Function obj_obtListaObjectos(ByVal po_objeto As Object, ByVal po_dtb As DataTable) As List(Of Object)
        Try

            ' Se obtiene la anotacion de la entidad
            Dim lo_annEnt As annEntidad = ann_obtenerAnotacion(po_objeto, GetType(annEntidad))

            ' Se verifica si se obtuvo la anotacion
            If lo_annEnt Is Nothing Then
                Return Nothing
            End If

            ' Se declara una lista para los objetos 
            Dim lo_lstObj As New List(Of Object)

            ' Se recorre las filas del DataTable
            For Each lo_row As DataRow In po_dtb.Rows

                ' Se declara un nuevo objeto a partir del actual
                Dim lo_objHdl As ObjectHandle = Activator.CreateInstance(po_objeto.GetType.Assembly.GetName.Name.ToString, po_objeto.GetType.Assembly.GetName.Name.ToString & "." & po_objeto.GetType.Name.ToString)
                Dim lo_objeto As Object = lo_objHdl.Unwrap

                ' Se asigna los datos del dataTable al objeto
                For Each lo_field As FieldInfo In lo_objeto.GetType.GetFields(BindingFlags.NonPublic Or BindingFlags.Public Or Reflection.BindingFlags.Instance)

                    ' Se obtiene la anotacion del campo
                    Dim lo_annAtr As annAtributo = ann_obtenerAnotacion(lo_field, GetType(annAtributo))

                    ' Se verifica si se obtuvo la anotacion
                    If lo_annAtr Is Nothing Then
                        Continue For
                    End If

                    ' Se obtiene el valor de la columna
                    If po_dtb.Columns(lo_annAtr.campoBD) Is Nothing Then
                        Continue For
                    End If

                    ' Se verifica si el atributo corresponde a un campo de base de datos
                    If lo_annAtr.esCampoBD = True Then

                        ' Se verifica si el atributo actual corresponde a un detalle
                        If lo_annAtr.esDetalle = False Then

                            ' Se asigna el valor de la columna correspondiente del dataTable a la entidad
                            lo_field.SetValue(lo_objeto, obj_convertirATipo(lo_row(lo_annAtr.campoBD), lo_field.FieldType))

                        Else

                            ' Se ejecuta esta accion para el objeto de detalle
                            lo_field.SetValue(lo_objeto, obj_obtObjetoPorCodigo(lo_field.GetValue(po_objeto), lo_row(lo_annEnt.IdTabla)))

                        End If

                    End If

                Next

                ' Se añade el objeto a la lista
                lo_lstObj.Add(lo_objeto)

            Next

            ' Se retorna la lista de objetos
            Return lo_lstObj

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

#End Region

#Region "Consultas"
    Public Shared Function dbl_obtCuentaGanancia_pc_() As String
        Return ModuleSQLComun.dbl_obtCuentaGanancia_pl()
    End Function
    Public Shared Function dtb_buscar(ByVal ps_sql As String) As DataTable
        Return ModuleSQLComun.dtb_buscar(ps_sql)
    End Function

    Public Shared Function dtb_obtCampo(ByVal ps_tabla As String, ByVal ps_campo As String)
        Return ModuleSQLComun.dtb_obtCampo(ps_tabla, ps_campo)
    End Function

    Public Shared Function dtb_obtEstructuraTabla(ByVal ps_tabla As String) As DataTable
        Return ModuleSQLComun.dtb_obtEstructuraTabla(ps_tabla)
    End Function

    Public Shared Function dbl_obtenerTipoCambio(ByVal ps_moneda As String, ByVal ps_fecha As String) As Double
        Return ModuleSQLComun.dbl_obtenerTipoCambio(ps_moneda, ps_fecha)
    End Function

    Public Shared Function str_obtMonLocal() As String
        Return ModuleSQLComun.str_obtMonLocal()
    End Function

    Public Shared Function int_obtDecimalesImp() As Integer
        Return ModuleSQLComun.int_obtDecimalesImp()
    End Function

    Public Shared Function int_autoIntId(ByVal ps_tabla As String, ByVal ps_idTabla As String) As Integer
        Return ModuleSQLComun.int_autoIntId(ps_tabla, ps_idTabla)
    End Function

    Public Shared Function str_verExisteCuentaBancaria(ByVal ps_cuenta As String) As String
        Return ModuleSQLComun.str_verExisteCuentaBancaria(ps_cuenta)
    End Function

    Public Shared Function dtb_obtDatosTabla(ByVal ps_tabla As String) As DataTable
        Try

            ' Se verifica si se envió el nombre de la tabla
            If ps_tabla.Trim = "" Then

                ' No se envió un nombre de tabla
                MsgBox("No se envió un nombre de tabla")
                Return Nothing

            End If

            ' Se obtiene la sentencia para consultar la tabla
            Dim ls_sql As String = "select * from " & ps_tabla

            ' Se retorna el resultado de la consulta
            Return ModuleSQLComun.dtb_buscar(ls_sql)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entComun", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function dtb_obtDatosTabla(ByVal po_objeto As Object) As DataTable
        Try

            ' Se obtiene la anotacion de entidad
            Dim lo_annEnt As annEntidad = ann_obtenerAnotacion(po_objeto, GetType(annEntidad))

            ' Se verifica si se obtuvo la anotacion
            If lo_annEnt Is Nothing Then
                MsgBox("La clase no cuenta con una anotacion de entidad.")
                Return Nothing
            End If

            ' Se obtiene la sentencia para consultar la tabla
            Dim ls_sql As String = "select * from " & lo_annEnt.Tabla

            ' Se retorna el resultado de la consulta
            Return ModuleSQLComun.dtb_buscar(ls_sql)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entComun", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function dtb_ejcConsultaSQL(ByVal ps_sql As String)
        Return ModuleSQLComun.dtb_buscar(ps_sql)
    End Function

    Public Shared Function dtb_obtParamProcedure(ByVal ps_nomProcedure As String) As DataTable
        Return ModuleSQLComun.dtb_obtParamProcedure(ps_nomProcedure)
    End Function

#End Region

#Region "SAP"

    Public Shared Function sbo_conectar(ByVal ps_SBOUsrName As String, ByVal ps_SBOPass As String) As SAPbobsCOM.Company
        Return ModuleSQLComun.sbo_conectar(ps_SBOUsrName, ps_SBOPass)
    End Function

#End Region

End Class
