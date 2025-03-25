Imports System.Windows.Forms
Imports Util
Imports System.Reflection
Imports System.Runtime.Remoting
Imports System.Drawing
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraTab
Imports DevExpress.XtraEditors.Repository
Imports System.IO
Imports System.Globalization

Public Class classComun

    ' Variables Globales de la clase
    Public o_entConn As entConn

    ' Atributos de la clase
    Protected o_form As frmComun

    ' Variables de uso comun
    Protected i_decimalesImp As Integer

#Region "Constructor"

    Public Sub New(ByVal po_form As Form)

        ' Se obtiene el formulario que invoca a esta clase del negocio
        o_form = po_form

    End Sub

#End Region

#Region "Fn_Comun"

#Region "Inicializar"

    ' Metodo de carga inicial del formulario
    Public Sub sub_cargarFrm()
        Try

            ' Se inicializa el formulario con las acciones comunes
            sub_cargarFormSys()

            ' Se inicializa el formulario con las acciones propias del objeto de negocio asociado
            sub_cargarForm()

            ' Se asignan los eventos a los controles
            sub_asigEventosControlActualizacion(o_form.Controls)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_cargarFormSys()
        Try

            ' Se inicializa los objetos de detalle que existan en el formulario
            sub_inicializarObjDetalle()

            ' Se inicializa las variables de cantidad de decimales
            sub_iniCantDecimales()

            ' Se inicializa el combo de Estado
            sub_iniComboEstado()

            ' Se inicializa el id automatico de la entidad
            sub_asigNumAutoEntidad()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Overridable Sub sub_cargarForm()
        Try

            ' <<< La logica de este metodo debe ser definida en las clases derivadas >>>

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    ' Asignar un valor al control
    Protected Sub sub_asigValorControl(ByVal ps_nomCampo As String, ByVal po_valor As Object)
        Try

            ' Se obtiene el control por nombre
            Dim lo_control As Control = ctr_obtenerControl(ps_nomCampo, CType(o_form, Form).Controls)

            ' Se asigna el valor al control
            ModuleGlobalForm.sub_asigValorControl(lo_control, po_valor)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    ' Se invoca un formulario a partir del nombre
    Protected Sub sub_invocarFormulario(ByVal ps_nomForm As String)
        Try

            ' Se ejecuta el metodo para invocar un formulario en el objeto Form asociado a esta clase de negocio
            Dim lo_method As MethodInfo = o_form.GetType.GetMethod("sub_invocarFormulario")

            ' Se invoca el metodo obtenido
            lo_method.Invoke(o_form, New Object() {ps_nomForm})

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    ' Inicializa los grids corespondientes a los objetos de detalle
    Protected Sub sub_inicializarObjDetalle()
        Try

            ' Se obtiene los controles de usuario Grid que cuenten con una referencia hacia una tabla
            For Each lo_control As Control In o_form.Controls

                ' Se verifica si el control obtenido es de tipo uct_uct_gridConBusqueda
                If TypeOf (lo_control) Is uct_gridConBusqueda Then

                    ' Se verifica si cuenta con algun valor en la propiedad Tabla
                    If Not CType(lo_control, uct_gridConBusqueda).Tabla Is Nothing Then
                        If Not CType(lo_control, uct_gridConBusqueda).Tabla = "" Then

                            ' Se obtiene el valor de la propiedad tabla
                            Dim ls_tabla As String = CType(lo_control, uct_gridConBusqueda).Tabla

                            ' Se obtiene la estructura de la tabla asociada al control
                            CType(lo_control, uct_gridConBusqueda).DataSource = entComun.dtb_obtEstructuraTabla(ls_tabla)

                        End If
                    End If

                End If

            Next

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    ' Se obtiene la cantidad de decimales para los importes, precios, porcentajes o cantidades de acuerdo a la configuración realizada en la compañia SAP
    Protected Sub sub_iniCantDecimales()
        Try

            ' Se obtiene la cantidad de decimales para los importes
            i_decimalesImp = entComun.int_obtDecimalesImp

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    ' Se inicializa el combo de Estado: si es que hubiera
    Protected Sub sub_iniComboEstado()
        Try

            ' Se obtiene el comboBox de Estado 
            Dim lo_combo As System.Windows.Forms.ComboBox = ctr_obtenerControl("estado", o_form.Controls)

            ' Se verifica si se obtuvo el comboBox
            If Not lo_combo Is Nothing Then

                ' Se declara un dataTable para el estado
                Dim lo_dtb As New DataTable

                ' Se crea las columnas del datatable
                lo_dtb.Columns.Add("val")
                lo_dtb.Columns.Add("dscr")

                ' Se añade los estados al dataTable
                Dim lo_row As DataRow = lo_dtb.NewRow
                lo_row.BeginEdit()
                lo_row("val") = "O"
                lo_row("dscr") = "Abierto"
                lo_row.EndEdit()

                ' Se añade la fila al dataTable
                lo_dtb.Rows.Add(lo_row)

                ' Se limpia el objeto row
                lo_row = Nothing

                ' Se inicializa una nueva fila
                lo_row = lo_dtb.NewRow
                lo_row.BeginEdit()
                lo_row("val") = "C"
                lo_row("dscr") = "Cerrado"
                lo_row.EndEdit()

                ' Se añade la fila al dataTable
                lo_dtb.Rows.Add(lo_row)

                ' Se limpia el objeto row
                lo_row = Nothing

                ' Se inicializa una nueva fila
                lo_row = lo_dtb.NewRow
                lo_row.BeginEdit()
                lo_row("val") = "A"
                lo_row("dscr") = "Cancelado"
                lo_row.EndEdit()

                ' Se añade la fila al dataTable
                lo_dtb.Rows.Add(lo_row)

                ' Se limpia el objeto row
                lo_row = Nothing

                ' Se asigna el dataTable al Combo de Estado
                lo_combo.DataSource = lo_dtb
                lo_combo.ValueMember = "val"
                lo_combo.DisplayMember = "dscr"

            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    ' Se asigna la numeracion automatica del objeto
    Private Sub sub_asigNumAutoEntidad()
        Try

            ' Se verifica el modo del formulario
            If o_form.Modo <> enm_modoForm.ANADIR Then
                Exit Sub
            End If

            ' Se verifica si el formulario cuenta con la propiedad TAG
            If o_form.Tag Is Nothing Then
                Exit Sub
            End If

            ' Se verifica si el campo de Id de la entidad es un entero automatico
            ' - Se obtiene la entidad
            Dim lo_entidad As Object = obj_obtenerEntidad()

            ' - Se verifica si se obtuvo el objeto
            If lo_entidad Is Nothing Then
                Exit Sub
            End If

            ' - Se obtiene el campo id de la entidad
            ' - - Se verifica si la entidad contiene
            Dim lo_annEnt As annEntidad = ann_obtenerAnotacion(lo_entidad, GetType(annEntidad))

            ' - - Se verifica si se obtuvo la anotacion
            If lo_annEnt Is Nothing Then
                Exit Sub
            End If

            ' - - Se obtiene el id de la tabla asociada a la entidad
            Dim ls_idTabla As String = lo_annEnt.IdTabla

            ' Se verifica si el Id corresponde a un entero automatico
            If lo_annEnt.EsIdIntAuto = False Then
                Exit Sub
            End If

            ' - Se obtiene el control asociado al id de la tabla
            Dim lo_control As Control = ctr_obtenerControl(ls_idTabla, o_form.Controls)

            ' - Se verifica si se obtuvo el control
            If lo_control Is Nothing Then
                Exit Sub
            End If

            ' Se autogenera el nuevo id para el campo
            sub_obtIdIntAuto(lo_annEnt.Tabla, ls_idTabla)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_obtIdIntAuto(ByVal ps_tabla As String, ByVal ps_idTabla As String)
        Try

            ' Se obtiene el último ID en una variable int
            Dim li_ultimoId As Integer = entComun.int_autoIntId(ps_tabla, ps_idTabla)

            ' Se asigna el id al control
            sub_asigValorControl(ps_idTabla, li_ultimoId.ToString)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#Region "Accion_Comun"

    Public Sub sub_accionPrincipal()
        Try

            ' Se obtiene la entidad asociada al formulario
            Dim lo_objeto As Object = obj_obtenerEntidad()

            ' Se obtiene los datos de la entidad desde el formulario
            sub_obtEntidadDesdeForm(lo_objeto)

            ' Se declara una variable para el resultado de la operacion
            Dim ls_resultado As String = ""

            ' Se verifica el modo del formulario
            If o_form.Modo = enm_modoForm.ANADIR Then

                ' Se realiza la adicion de acuerdo a los datos obtenidos del formulario
                sub_anadir(lo_objeto)

            ElseIf o_form.Modo = enm_modoForm.ACTUALIZAR Then

                ' Se realiza la actualizacion de acuerdo a los datos obtenidos del formulario
                sub_actualizar(lo_objeto)

            ElseIf o_form.Modo = enm_modoForm.BUSCAR Then

                ' Se realiza la busqueda de acuerdo a los datos obtenidos desde el formulario
                sub_buscar(lo_objeto)

            ElseIf o_form.Modo = enm_modoForm.VER Then

                ' Se cierra el formulario
                o_form.Close()

            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Protected Sub sub_anadir(ByVal po_objeto As Object)
        Try

            ' Se realida las validaciones para la accion: Antes de realizar la accion
            If bol_valAlAnadir(True) = False Then
                Exit Sub
            End If

            ' Se declara una variable para el resultado de la operacion
            Dim ls_resultado As String = ""

            ' Se obtiene el metodo de la entidad relacionado a la accion asociada al modo del formulario
            Dim lo_method As MethodInfo = po_objeto.GetType.GetMethod("str_anadir")

            ' Se ejecuta el metodo
            ls_resultado = lo_method.Invoke(po_objeto, New Object() {})

            ' Se verifica el resultado de la operacion
            If ls_resultado.Trim = "" Then ' Si la operacion fue correcta

                ' Se muestra un mensaje de exito
                MsgBox("La operación se realizó con exito")

                ' Se asigna el modo del formulario
                sub_asignarModo(enm_modoForm.ANADIR)

                ' Se asigna el nuevo numero de id de ser necesario
                sub_asigNumAutoEntidad()

            End If

            ' Se muestra un mensaje con el resultado de la operacion 
            If ls_resultado.Trim <> "" Then
                MsgBox(ls_resultado)
            End If

            ' Se realida las validaciones para la accion: Antes de realizar la accion
            If bol_valAlAnadir(False) = False Then
                Exit Sub
            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Protected Sub sub_actualizar(ByVal po_objeto As Object)
        Try

            ' Se realida las validaciones para la accion: Antes de realizar la accion
            If bol_valAlActualizar(True) = False Then
                Exit Sub
            End If

            ' Se declara una variable para el resultado de la operacion
            Dim ls_resultado As String = ""

            ' Se obtiene el metodo de la entidad relacionado a la accion asociada al modo del formulario
            Dim lo_method As MethodInfo = po_objeto.GetType.GetMethod("str_actualizar")

            ' Se ejecuta el metodo
            ls_resultado = lo_method.Invoke(po_objeto, New Object() {})

            ' Se verifica el resultado de la operacion
            If ls_resultado.Trim = "" Then ' Si la operacion fue correcta

                ' Se muestra un mensaje de exito
                MsgBox("La operación se realizó con exito")

                ' Se asigna el modo del formulario
                sub_asignarModo(enm_modoForm.VER)

            End If

            ' Se muestra un mensaje con el resultado de la operacion 
            If ls_resultado.Trim <> "" Then
                MsgBox(ls_resultado)
            End If

            ' Se realida las validaciones para la accion: Antes de realizar la accion
            If bol_valAlActualizar(False) = False Then
                Exit Sub
            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Protected Sub sub_buscar(ByVal po_objeto As Object)
        Try

            ' Se obtiene el metodo de la entidad relacionado a la accion asociada al modo del formulario
            Dim lo_method As MethodInfo = po_objeto.GetType.GetMethod("dtb_buscar")

            ' Se ejecuta el metodo
            Dim lo_dtb As DataTable = lo_method.Invoke(po_objeto, New Object() {})

            ' Se verifica si se obtuvo resultados del dataTable
            If lo_dtb Is Nothing Then
                MsgBox("Ocurrio un error al realiza la busqueda")
            End If

            ' Se declara una variable para obtener la tabla de id de la entidad
            Dim lo_annEnt As annEntidad = ann_obtenerAnotacion(po_objeto, GetType(annEntidad))

            ' Se verifica si se obtuvo la anotacion de entidad
            If lo_annEnt Is Nothing Then
                Exit Sub
            End If

            ' Se verifica cuantos registros se obtuvo de la consulta
            If lo_dtb.Rows.Count = 0 Then
                MsgBox("No se obtuvo resultados de la busqueda")
            ElseIf lo_dtb.Rows.Count = 1 Then

                ' Se obtiene el objeto por codigo
                sub_obtEntidadPorCodigo(po_objeto, lo_dtb.Rows(0)(lo_annEnt.IdTabla))

                ' Se asigna el modo del formulario
                sub_asignarModo(enm_modoForm.VER)

            Else

                ' Se declara un formulario de busqueda
                Dim lo_frmBuscar As New frmBuscar(lo_dtb, "", "")

                ' Se muestra el formulario de busqueda con los resultados obtenidos
                lo_frmBuscar.ShowDialog()

                ' Se verifica si se selecciono algun valor del formulario de busqueda
                If lo_frmBuscar.int_contarSelecc > 0 Then

                    ' Se obtiene el objeto por codigo
                    sub_obtEntidadPorCodigo(po_objeto, lo_frmBuscar.obj_obtenerValorSel(lo_annEnt.IdTabla))

                    ' Se asigna el modo del formulario
                    sub_asignarModo(enm_modoForm.VER)

                End If

            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Public Sub sub_obtEntidadDesdeForm(ByVal po_entidad As Object)
        Try

            ' Se recorre los atributos de la entidad
            For Each lo_field As FieldInfo In po_entidad.GetType.GetFields(BindingFlags.NonPublic Or BindingFlags.Public Or Reflection.BindingFlags.Instance)

                ' Se obtiene la anotacion del atributo
                Dim lo_annAtr As annAtributo = lo_field.GetCustomAttributes(True)(0)

                ' Se verifica si se obtuvo el atributo
                If Not lo_annAtr Is Nothing Then

                    ' Se verifica si el atributo es un campo de base de datos
                    If lo_annAtr.esCampoBD = True Then

                        ' Se verifica si el atributo es un Detalle
                        If lo_annAtr.esDetalle = True Then

                            ' Se asume que el control asociado al objeto de detalle sera de tipo uct_gridConBusqueda
                            Dim lo_uctGridDetalle As uct_gridConBusqueda = ctr_obtenerControl(lo_annAtr.campoBD, o_form.Controls)

                            ' Se verifica si se obtuvo el control
                            If Not lo_uctGridDetalle Is Nothing Then

                                ' Se obtiene el objeto de detalle
                                Dim lo_objDetalle As Object = lo_field.GetValue(po_entidad)

                                ' Se obtiene el metodo de asignacion de detalle con dataTable desde el objeto de detalle obtenido
                                Dim lo_method As MethodInfo = lo_objDetalle.GetType.GetMethod("sub_obtDesdeDataTable")

                                ' Se invoca al metodo
                                lo_method.Invoke(lo_objDetalle, New Object() {obj_obtValorControl(lo_uctGridDetalle)})

                            Else

                                ' Se obtiene el objeto de detalle
                                Dim lo_objDetalle As Object = lo_field.GetValue(po_entidad)

                                ' Se obtiene el atributo del objeto de detalle que indicará que este detalle no se actualizará desde este formulario
                                Dim lo_fieldSeAct As FieldInfo = lo_objDetalle.GetType.GetField("o_seActualiza", BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.Instance)

                                ' Se indica que el objeto de detalle no se actualizará desde este formulario
                                lo_fieldSeAct.SetValue(lo_objDetalle, False)

                            End If

                        Else

                            ' Se obtiene el control con el nombre del campo de base de datos
                            Dim lo_control As Control = ctr_obtenerControl(lo_annAtr.campoBD, o_form.Controls)

                            ' Se verifica si se obtuvo el control
                            If Not lo_control Is Nothing Then

                                ' Se obtiene el valor del control
                                Dim lo_valor As Object = obj_obtValorControl(lo_control)

                                ' Se asigna el valor al campo
                                lo_field.SetValue(po_entidad, obj_convertirATipo(lo_valor, lo_field.FieldType))

                            End If

                        End If

                    End If

                End If

            Next

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Protected Sub sub_asignarEntidadAForm(ByVal po_entidad As Object, ByVal po_valId As Object)
        Try

            ' Se obtiene el atributo de la entidad para obtener el id de la tabl
            Dim lo_annEnt As annEntidad = ann_obtenerAnotacion(po_entidad, GetType(annEntidad))

            ' Se verifica si se obtuvo la anotacion de entidad
            If lo_annEnt Is Nothing Then
                Exit Sub
            End If

            ' Se recorre los atributos de la entidad
            For Each lo_field As FieldInfo In po_entidad.GetType.GetFields(BindingFlags.NonPublic Or BindingFlags.Public Or Reflection.BindingFlags.Instance)

                ' Se obtiene la anotacion del atributo
                Dim lo_annAtr As annAtributo = lo_field.GetCustomAttributes(True)(0)

                ' Se verifica si se obtuvo el atributo
                If Not lo_annAtr Is Nothing Then

                    ' Se verifica si el atributo es un campo de base de datos
                    If lo_annAtr.esCampoBD = True Then

                        ' Se verifica si el atributo es un Detalle
                        If lo_annAtr.esDetalle = True Then

                            ' Se asume que el control asociado al objeto de detalle sera de tipo uct_gridConBusqueda
                            Dim lo_uctGridDetalle As uct_gridConBusqueda = ctr_obtenerControl(lo_annAtr.campoBD, o_form.Controls)

                            ' Se verifica si se obtuvo el control
                            If Not lo_uctGridDetalle Is Nothing Then

                                ' Se obtiene el objeto de detalle
                                Dim lo_objDetalle As Object = lo_field.GetValue(po_entidad)

                                ' Se obtiene la anotacion de entidad asociada al objeto de detalle
                                Dim lo_annEntDetalle As annEntidad = ann_obtenerAnotacion(lo_objDetalle, GetType(annEntidad))

                                ' Se verifica si se obtuvo el objeto de detalle
                                If lo_annEntDetalle Is Nothing Then
                                    Continue For
                                End If

                                ' Se obtiene el metodo de asignacion de detalle con dataTable desde el objeto de detalle obtenido
                                Dim lo_method As MethodInfo = lo_objDetalle.GetType.GetMethod("dtb_obtEnDataTable")

                                ' Se invoca al metodo <<<< FALTA!"!!!!!!!!!!!!! >>>>
                                Dim lo_dtb As DataTable = lo_method.Invoke(lo_objDetalle, New Object() {po_valId})

                                ' Se asigna el detalle al control
                                sub_asigValorControl(lo_annEntDetalle.Tabla, lo_dtb)

                            End If

                        Else

                            ' Se obtiene el control con el nombre del campo de base de datos
                            Dim lo_control As Control = ctr_obtenerControl(lo_annAtr.campoBD, o_form.Controls)

                            ' Se verifica si se obtuvo el control
                            If Not lo_control Is Nothing Then

                                ' Se asigna el valor al control
                                sub_asigValorControl(lo_annAtr.campoBD, lo_field.GetValue(po_entidad))

                            End If

                        End If

                    End If

                End If

            Next

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Protected Function dbl_redondearImporte(ByVal pd_importe As Double) As Double
        Try

            ' Se redondea de acuerdo a la cantidad de decimales asignada a los importes
            Return Math.Round(pd_importe, i_decimalesImp)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return pd_importe
        End Try
    End Function

    Protected Function dbl_redondearImporCzDet(ByVal pd_importe As Double) As Double
        Try

            ' Se redondea de acuerdo a la cantidad de decimales asignada a los importes
            Return Math.Round(pd_importe, 0)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return pd_importe
        End Try
    End Function

    Protected Function obj_obtenerEntidad() As Object
        Try

            ' Se obtiene la entidad asociada al formulario
            Dim ls_entidad As String = ""

            ' Se verifica si el formulario tiene Tag
            If Not o_form.Tag Is Nothing Then
                If o_form.Tag.ToString.Trim <> "" Then
                    ls_entidad = o_form.Tag.ToString
                Else
                    Return Nothing
                End If
            Else
                MsgBox("No se asocio al formulario una entidad (Propiedad TAG). ")
                Return Nothing
            End If

            ' Se crea una nueva instancia de la entidad con el nombre obtenido
            Dim lo_objHandle As ObjectHandle = Activator.CreateInstance("cl_Entidad", "cl_Entidad." & ls_entidad)
            Dim lo_objeto As Object = lo_objHandle.Unwrap

            ' Se retorna el objeto obtenido
            Return lo_objeto

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Private Sub sub_obtEntidadPorCodigo(ByVal po_objeto As Object, ByVal po_valId As Object)
        Try

            ' Se obtiene el metodo 
            Dim lo_method As MethodInfo = po_objeto.GetType.GetMethod("obj_obtPorCodigo")

            ' Se ejecuta el metodo
            Dim lo_entidad As Object = lo_method.Invoke(po_objeto, New Object() {po_valId})

            ' Se asigna la entidad al formulario
            sub_asignarEntidadAForm(lo_entidad, po_valId)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Protected Function str_obtEstadoObjeto() As String
        Try

            ' Se obtiene el combo de estado
            Dim lo_combo As System.Windows.Forms.ComboBox = ctr_obtenerControl("estado", o_form.Controls)

            ' Se verifica si se obtuvo el control
            If lo_combo Is Nothing Then
                Return ""
            End If

            ' Se obtiene el valor del control
            Return obj_obtValorControl(lo_combo)

        Catch ex As Exception
            Return ""
        End Try
    End Function

    Protected Sub sub_asignarEstadoObjeto(ByVal ps_estado As String)
        Try

            ' Se obtiene el combo de estado
            Dim lo_combo As System.Windows.Forms.ComboBox = ctr_obtenerControl("estado", o_form.Controls)

            ' Se verifica si se obtuvo el control
            If lo_combo Is Nothing Then
                Exit Sub
            End If

            ' Se asigna el valor al combo de Estado
            sub_asigValorControl("estado", ps_estado)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Overridable Sub sub_accionCombo(ByVal po_cbo As System.Windows.Forms.ComboBox)
        Try

            ' <<< Este metodo se sobreescribira en la clase del negocio si la misma requiere realizar alguna accion con un comboBox >>>

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Protected Overridable Function bol_valAlAnadir(ByVal pb_antesAccion As Boolean) As Boolean
        Return True ' < La función se puede sobreescribir >
    End Function

    Protected Overridable Function bol_valAlActualizar(ByVal pb_antesAccion As Boolean) As Boolean
        Return True ' < La función se puede sobreescribir >
    End Function

#End Region

#Region "Acciones_BarraHerr"

    Public Sub sub_preparaAnadir()
        Try

            ' Se prepara el formulario para el modo añadir
            sub_asignarModo(enm_modoForm.ANADIR)

            ' Se obtiene el ultimo numero Id para la entidad
            sub_asigNumAutoEntidad()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Public Sub sub_preparaBuscar()
        Try

            ' Se prepara el formulario para el modo añadir
            sub_asignarModo(enm_modoForm.BUSCAR)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Public Sub sub_recuperarForm()
        Try

            ' Se recupera la información del formulario 
            sub_recuperarAutoguardados()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Public Sub sub_cancelar()
        Try

            ' Se cancelar o elimina el objeto
            sub_cancelarObjeto()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#Region "Autoguardado"

    Protected Sub sub_autoGuardar()
        Try
            If o_form.Modo = enm_modoForm.ANADIR Or o_form.Modo = enm_modoForm.ACTUALIZAR Then
                sub_grabarEnXML()
            End If
        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_grabarEnXML()
        Try

            ' Se obtiene el directorio donde se ubicara las carpetas de los documentos autoguardados
            Dim ls_rutaPrinc As String = "C:\Users\" & Environment.UserName & "\Documents\App_PlanillaPagosR"
            Dim lo_directory As New DirectoryInfo(ls_rutaPrinc)

            ' Se crea una carpeta para el documento autoguardado
            If lo_directory.Exists = False Then ' Si no existe la carpeta especificada, se realiza la creacion de la misma
                lo_directory.Create()
            End If

            ' Se crea un directorio de autoguardado para la base de datos actual
            Dim ls_rutaDirBD As String = ls_rutaPrinc & "\" & s_SAPComp
            Dim lo_dirBD As New DirectoryInfo(ls_rutaDirBD)

            ' Si no existe la carpeta especificada, se realiza la creacion de la misma
            If lo_dirBD.Exists = False Then
                lo_dirBD.Create()
            End If

            ' Se crea un indicador que servira como nombre para la carpeta del documento autoguardado
            Dim ls_indicador As String = ""

            ' Se verifica si ya existe un valor para el indicador de autoguardado
            'If lblAGInd.Text = "AGInd" Or lblAGInd.Text.Trim = "" Then
            '    ls_indicador = Now.Date.ToString("yyyyMMdd", CultureInfo.InvariantCulture) & "_" & Now.Hour.ToString & Now.Minute.ToString & "_" & txtCliente.Text
            '    lblAGInd.Text = ls_indicador
            'Else
            '    ls_indicador = lblAGInd.Text
            'End If

            ' Se asigna el nombre de la carpeta de autoguardado
            ls_indicador = Now.Date.ToString("yyyyMMdd", CultureInfo.InvariantCulture) & "_" & Now.Hour.ToString & Now.Minute.ToString & "_" & o_form.Tag

            ' Se verifica si existe un directorio con el nombre del indicador obtenido
            Dim ls_dirDoc As String = ls_rutaDirBD & "\" & ls_indicador
            Dim lo_dirDoc As New DirectoryInfo(ls_dirDoc)

            ' Se crea un directorio para el documento a guardar con la fecha, la hora y el codigo de socio de negocio
            If lo_dirDoc.Exists = False Then
                lo_dirDoc.Create()
            End If

            ' Se crea o sobreescribe los archivos XML
            sub_grabarEnXML(ls_dirDoc)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_grabarEnXML(ByVal ps_ruta As String)
        Try

            ' Se obtiene los datos de la cabecera del formulario
            Dim lo_dtb As DataTable = dtb_obtDatosCabecera()

            ' Se genera un texto xml a partir del dataTable
            Dim lo_dsCab As New DataSet
            lo_dsCab.Merge(lo_dtb)
            lo_dsCab.WriteXml(ps_ruta & "\cab.txt")

            ' Se recorre los controles del formulario para obtener los controles de usuario grid
            For Each lo_grid As Control In o_form.Controls

                ' Se verifica si el control cuenta con tag
                If lo_grid.Tag Is Nothing Then
                    Continue For
                End If

                If lo_grid.Tag.ToString.Trim = "" Then
                    Continue For
                End If

                ' Se verifica el tipo de control
                If TypeOf (lo_grid) Is uct_gridConBusqueda Then

                    ' Se obtiene el dataSource
                    Dim lo_dtbGrid As DataTable = CType(lo_grid, uct_gridConBusqueda).DataSource

                    ' Se verifica si se obtuvo el dataTable
                    If Not lo_dtbGrid Is Nothing Then

                        ' Se asigna el nombre de tabla al grid con el tag del control
                        lo_dtbGrid.TableName = lo_grid.Tag

                        ' Se declara un dataSet para el guardado en XML
                        Dim lo_dsGrid As New DataSet
                        lo_dsGrid.Merge(lo_dtbGrid)
                        lo_dsGrid.WriteXml(ps_ruta & "\" & lo_grid.Tag & ".txt")

                    End If

                End If

            Next

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Function dtb_obtDatosCabecera() As DataTable
        Try

            ' Se declara un dataTable para obtener los datos del formulario
            Dim lo_dtb As New DataTable

            ' Se asigna el nobmre al DataTable
            lo_dtb.TableName = "Obj_Cab"

            ' Se declara un dataTable para obtener los datos del formulario
            lo_dtb = dtb_crearDtbParaDatosForm(o_form.Controls, lo_dtb)

            ' Se crea una nueva fila
            Dim lo_newRow As DataRow = lo_dtb.NewRow
            lo_newRow.BeginEdit()

            ' Se recorre las columnas del dataTable para almacenar los datos de la cabecera del formulario
            For Each lo_column As DataColumn In lo_dtb.Columns

                ' Se obtiene el control de acuerdo al nombre del tag, el cual es el mismo que el nombre de la columna
                Dim lo_control As Control = ModuleGlobalForm.ctr_obtenerControl(lo_column.ColumnName, o_form.Controls)

                ' Se asigna el valor a la columna
                lo_newRow(lo_column.ColumnName) = ModuleGlobalForm.obj_obtValorControl(lo_control)

            Next
            lo_newRow.EndEdit()

            ' Se añade la fila al dataTable
            lo_dtb.Rows.Add(lo_newRow)

            ' Se retorna el dataTable
            Return lo_dtb

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Private Function dtb_crearDtbParaDatosForm(ByVal po_controles As System.Windows.Forms.Control.ControlCollection, ByVal po_dtb As DataTable) As DataTable
        Try

            ' Se crea las columnas de acuerdo a los controles que cuenten con Tag
            For Each lo_control As Control In po_controles

                ' Se verifica si el control es de tipo XtraTabControl (Tab o pestañas)
                If TypeOf (lo_control) Is XtraTabControl Then

                    ' Se recorre los tabs
                    For j As Integer = 0 To CType(lo_control, XtraTabControl).TabPages.Count - 1

                        ' Se limpia el control
                        dtb_crearDtbParaDatosForm(CType(lo_control, XtraTabControl).TabPages.Item(j).Controls, po_dtb)

                    Next

                End If

                ' Se verifica si el control cuenta con Tag
                If (Not lo_control.Tag Is Nothing) Then
                    If lo_control.Tag.ToString <> "" Then

                        ' Se verifica que el control no sea de tipo GridControl
                        If Not TypeOf (lo_control) Is GridControl And Not TypeOf (lo_control) Is uct_gridConBusqueda Then

                            ' Se verifica si existe una columna con el nombre del control
                            If po_dtb.Columns(lo_control.Tag.ToString) Is Nothing Then

                                ' Se crea una columna con el nombre del control que cuenta con Tag
                                po_dtb.Columns.Add(lo_control.Tag.ToString)

                            End If

                        End If

                    End If

                End If

            Next

            ' Se retorna el dataTable
            Return po_dtb

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

#End Region

#Region "Recuperacion"

    Private Sub sub_recuperarAutoguardados()
        Try

            ' Se realiza acciones antes de realizar la acción de Recuperación
            If bol_validarRecuperar(True) = False Then
                Exit Sub
            End If

            ' Se realiza las acciones para la recuperacion de datos autoguardados
            Dim lb_ver As Boolean = bol_recuperar()

            ' Se verifica si la acción de recuperacion se realizó con exito
            If lb_ver = False Then
                Exit Sub
            End If

            ' Se realiza acciones luego de realizar la acción de Recuperación
            If bol_validarRecuperar(False) = False Then
                Exit Sub
            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Function bol_recuperar() As Boolean
        Try

            ' Se obtiene el directorio donde se ubicara las carpetas de los documentos autoguardados
            Dim ls_rutaPrinc As String = "C:\Users\" & Environment.UserName & "\Documents\App_PlanillaPagosR\" & s_SAPComp
            Dim lo_directory As New DirectoryInfo(ls_rutaPrinc)

            ' Se verifica si existe el directorio
            If lo_directory.Exists = False Then
                MsgBox("El directorio de autoguardado no existe o aun no se ha realizado ninguna accion de autoguardado")
                Return False
            Else

                ' Se obtienen los directorios de documentos autoguardados
                Dim lo_dirs As DirectoryInfo() = lo_directory.GetDirectories

                ' Se recorre los directorios obtenidos para llenar un dataTable con la informacion
                Dim lo_dtbDirDocs As New DataTable

                ' Se crea las columnas del dataTable
                lo_dtbDirDocs.Columns.Add("nomDoc")
                lo_dtbDirDocs.Columns.Add("fecCrea")
                lo_dtbDirDocs.Columns.Add("fecModf")
                lo_dtbDirDocs.Columns.Add("ruta")

                ' Se recorre el directorio
                For Each lo_dir In lo_dirs

                    ' Se crea una nueva fila del dataTable
                    Dim lo_row As DataRow = lo_dtbDirDocs.NewRow
                    lo_row.BeginEdit()
                    lo_row("nomDoc") = lo_dir.Name
                    lo_row("fecCrea") = lo_dir.CreationTime
                    lo_row("fecModf") = lo_dir.LastWriteTime
                    lo_row("ruta") = lo_dir.FullName
                    lo_row.EndEdit()
                    lo_dtbDirDocs.Rows.Add(lo_row)

                Next

                ' Se muestra el listado de documentos autoguardados en una lista de seleccion de busqueda
                Dim lo_frmBuscar As New frmBuscar(lo_dtbDirDocs, "nomDoc", "")

                ' Se asigna los nombres a las columnas
                lo_frmBuscar.sub_adcCabColumn("nomDoc", "Nombre")
                lo_frmBuscar.sub_adcCabColumn("fecCrea", "Fecha de Creacion")
                lo_frmBuscar.sub_adcCabColumn("fecModf", "Fecha de Modificacion")
                lo_frmBuscar.sub_adcCabColumn("ruta", "Ruta")

                ' Se asigna el ancho correspondiente a las columnas del gridView
                lo_frmBuscar.sub_asigCabColumnas()

                ' Se muestra el listado
                lo_frmBuscar.ShowDialog()

                ' Se verifica si se seleccionó algun registro del listado
                Dim li_cont As Integer = lo_frmBuscar.int_contarSelecc
                If li_cont > 0 Then

                    ' Se recupera los datos del documento seleccionado
                    sub_recuperar(lo_frmBuscar.obj_obtenerValorSel("ruta"))

                    ' Si la recuperacion se realizó con exito
                    Return True

                Else
                    Return False
                End If

            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Sub sub_recuperar(ByVal ps_nomDoc As String)
        Try

            ' Se prepara el formulario para el modo añadir de manera temporal
            'sub_asignarModo(enm_modoForm.ANADIR)

            ' Se crea un dataTable para los datos de cabecera
            Dim lo_dtb As New DataTable
            Dim lo_dsCab As New DataSet

            ' Se asigna los nobmres correspondientes a los dataTable
            lo_dtb.TableName = "Obj_Cab"

            ' Se asigna los datos obtenidos al formulario
            lo_dsCab.ReadXml(ps_nomDoc & "\cab.txt")

            ' Se obtiene los dataTables desde los dataSet
            If lo_dsCab.Tables.Count > 0 Then
                lo_dtb = lo_dsCab.Tables(0)
            End If

            ' Se cambio el modo del formulario a RECUPERAR
            'sub_asigModoForm(enm_modoForm.RECUPERAR)

            ' Se obtiene el Modo del formulario
            Dim li_modo As Integer = lo_dtb.Rows(0)("Modo")

            ' Se asigna el modo obtenido de la recuperacion
            'Dim lo_control As Control = ctr_obtenerControl("Modo", o_form.Controls)
            sub_asignarModo(li_modo)

            ' Se carga la información obtenida a los controles del formulario
            If lo_dtb.Rows.Count > 0 Then
                ModuleGlobalForm.bol_asignarDatosForm(o_form.Controls, lo_dtb)
            End If

            ' Se asigna el modo del formulario recuperado
            'If o_form.Modo <> enm_modoForm.ANADIR Then
            '    sub_prepararFormPorModo()
            'End If

            ' Se recorre los controles de usuario grid
            ' Se recorre los controles del formulario para obtener los controles de usuario grid
            For Each lo_grid As Control In o_form.Controls

                ' Se verifica si el control cuenta con tag
                If lo_grid.Tag Is Nothing Then
                    Continue For
                End If

                If lo_grid.Tag.ToString.Trim = "" Then
                    Continue For
                End If

                ' Se verifica el tipo de control
                If TypeOf (lo_grid) Is uct_gridConBusqueda Then

                    ' Se declara un dataSet para los detalles
                    Dim lo_dsDet As New DataSet

                    ' Se declara un dataSet para el guardado en XML
                    lo_dsDet.ReadXml(ps_nomDoc & "\" & lo_grid.Tag & ".txt")

                    If lo_dsDet.Tables.Count > 0 Then

                        ' Se declara un dataTable para asignarlo al control
                        Dim lo_dtbDet As DataTable

                        ' Se obtiene el dataTable desde el DataSet
                        lo_dtbDet = lo_dsDet.Tables(0)

                        ' Se asigna el valor al control
                        sub_asigValorControl(lo_grid.Tag, lo_dtbDet)

                    End If

                End If

            Next

            ' Se verifica el modo obtenido para asegurar que el modo actual del formulario sea el correcto
            If li_modo = enm_modoForm.ANADIR Then

                ' Se prepara los controles para el modo Añadir
                sub_prepControlModoAnadir()

                ' Se asigna el texto al boton
                sub_asigTxtBotonPrincipal(enm_modoForm.ANADIR)

                ' Se asigna el modo al formulario
                o_form.Modo = enm_modoForm.ANADIR

            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#Region "CancelarObj"

    Private Sub sub_cancelarObjeto()
        Try

            ' Se verifica el modo del formulario
            If Not o_form.Modo = enm_modoForm.ACTUALIZAR And Not o_form.Modo = enm_modoForm.VER Then
                MsgBox("Debe buscar y mostrar un registro antes de realizar esta accion.")
                Exit Sub
            End If

            ' Se ejecuta las validaciones antes de la accion
            If bol_valCancelar(True) = False Then
                Exit Sub
            End If

            ' Se ejecuta validaciones del propio objeto definidos en la clase asociada al formulario
            sub_cancelarOEliminar()

            ' Se ejecuta las validaciones luego de la accion
            If bol_valCancelar(False) = False Then
                Exit Sub
            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Protected Overridable Function bol_valCancelar(ByVal pb_antesAccion As Boolean) As Boolean
        Try

            ' <<< El codigo de esta validacion sera definido en la clase asociada al formulario >>>>
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Sub sub_cancelarOEliminar()
        Try

            ' Se obtiene el estado
            Dim ls_estado As String = str_obtEstadoObjeto()

            ' Se verifica si se obtuvo el estado
            If ls_estado Is Nothing Then
                Exit Sub
            End If
            If ls_estado.Trim = "" Then
                Exit Sub
            End If

            ' Se muestra un mensaje de confirmacion
            Dim li_confirm As Integer = MessageBox.Show("Esta seguro que desea cancelar el registro.", "caption", MessageBoxButtons.YesNoCancel)

            ' Se verifica el resultado del mensaje de confirmacion
            If Not li_confirm = DialogResult.Yes Then
                Exit Sub
            End If

            ' Se asigna al documento el estado Cancelado
            sub_asignarEstadoObjeto("A")

            ' Se actualiza el objeto
            sub_accionPrincipal()

            ' En caso de que el objeto sea un Dato Maestro se debe eliminar o anular el registro
            ' <<< PENDIENTE: en otros desarrollos >>>

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#End Region

#Region "Modo_Form"

    Protected Sub sub_asignarModo(ByVal pi_modo As Integer)
        Try

            ' Se verifica el modo recibido
            sub_prepararFormPorModo(pi_modo)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Protected Sub sub_prepararFormPorModo(ByVal pi_modo As Integer)
        Try

            ' Se verifica el modo recibido
            If pi_modo = enm_modoForm.ANADIR Then
                sub_prepFormAnadir()
            ElseIf pi_modo = enm_modoForm.ACTUALIZAR Then
                sub_prepFormActualizar()
            ElseIf pi_modo = enm_modoForm.BUSCAR Then
                sub_prepFormBuscar()
            ElseIf pi_modo = enm_modoForm.VER Then
                sub_prepFormVer()
            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_asigTxtBotonPrincipal(ByVal pi_modo As Integer)
        Try

            ' Se obtiene el boton principal del formulario
            Dim lo_btnPrincipal As Button = ctr_obtenerControl("1", o_form.Controls)

            ' Se verifica si se obtuvo el botón
            If Not lo_btnPrincipal Is Nothing Then

                ' Se verifica el modo recibido
                If pi_modo = enm_modoForm.ANADIR Then
                    lo_btnPrincipal.Text = "Añadir"
                ElseIf pi_modo = enm_modoForm.ACTUALIZAR Then
                    lo_btnPrincipal.Text = "Actualizar"
                ElseIf pi_modo = enm_modoForm.BUSCAR Then
                    lo_btnPrincipal.Text = "Buscar"
                ElseIf pi_modo = enm_modoForm.VER Then
                    lo_btnPrincipal.Text = "Aceptar"
                End If

            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Protected Overridable Function bol_validarModoAnadir(ByVal pb_antesAccion As Boolean) As Boolean
        Try

            ' <<< El metodo puede ser sobreescrito en la clase derivada >>>
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Protected Overridable Function bol_validarModoActualizar(ByVal pb_antesAccion As Boolean) As Boolean
        Try

            ' <<< El metodo puede ser sobreescrito en la clase derivada >>>
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Protected Overridable Function bol_validarModoVer(ByVal pb_antesAccion As Boolean) As Boolean
        Try

            ' <<< El metodo puede ser sobreescrito en la clase derivada >>>
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Protected Overridable Function bol_validarRecuperar(ByVal pb_antesAccion As Boolean) As Boolean
        Try

            ' <<< El metodo puede ser sobreescrito en la clase derivada >>>
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

#Region "ModoAnadir"

    Private Sub sub_prepFormAnadir()
        Try

            ' Se realiza acciones antes de realizar el cambio de modo
            If bol_validarModoAnadir(True) = False Then
                Exit Sub
            End If

            ' Se limpia el formulario
            sub_limpiarControlesForm(o_form.Controls)

            ' Se prepara los controles para el modo añadir de acuerdo a la entidad asociada al formulario
            sub_prepControlModoAnadir()

            ' Se asigna el estado por defecto al formulario
            sub_prepararComboEstado()

            ' Se asigna el texto al boton
            sub_asigTxtBotonPrincipal(enm_modoForm.ANADIR)

            ' Se asigna el modo al formulario
            o_form.Modo = enm_modoForm.ANADIR

            ' Se realiza acciones antes de realizar el cambio de modo
            If bol_validarModoAnadir(False) = False Then
                Exit Sub
            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_prepControlModoAnadir()
        Try

            ' Se recorre los atributos de la entidad
            Dim lo_entidad As Object = obj_obtenerEntidad()

            ' Se recorre los atributos de la entidad
            For Each lo_field As FieldInfo In lo_entidad.GetType.GetFields(BindingFlags.NonPublic Or BindingFlags.Public Or Reflection.BindingFlags.Instance)

                ' Se obtiene la anotacion de atributo del campo
                Dim lo_annAtr As annAtributo = ann_obtenerAnotacion(lo_field, GetType(annAtributo))

                ' Se verifica si se obtuvo el atributo
                If lo_annAtr Is Nothing Then
                    Continue For
                End If

                ' Se verifica si el atributo corresponde a un campo de base de datos, si no corresponde a un detalle y si es un campo de busqueda
                If lo_annAtr.esCampoBD = True And lo_annAtr.esDetalle = False Then

                    ' Se busca el control asociado al atributo
                    Dim lo_control As Control = ctr_obtenerControl(lo_annAtr.campoBD, o_form.Controls)

                    ' Se verifica si se obtuvo el control
                    If lo_control Is Nothing Then
                        Continue For
                    End If

                    ' Se verifica si el campo es editable por el usuario
                    If lo_annAtr.usrEditable = True Then

                        ' Se habilita el control y se le asigna un color para diferenciar el modo busqueda
                        lo_control.Enabled = True
                        lo_control.BackColor = Color.White

                    Else

                        ' Se habilita el control y se le asigna un color para diferenciar el modo busqueda
                        lo_control.Enabled = False
                        lo_control.BackColor = Color.White

                    End If

                End If

            Next

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_prepararComboEstado()
        Try

            ' Se obtiene el combo de estado
            Dim lo_combo As System.Windows.Forms.ComboBox = ctr_obtenerControl("estado", o_form.Controls)

            ' Se verifica si se obtuvo el combo
            If lo_combo Is Nothing Then
                Exit Sub
            End If

            ' Se asigna el valor por defecto al combo
            sub_asigValorControl("estado", "O")

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#Region "ModoActualizar"

    Private Sub sub_prepFormActualizar()
        Try

            ' Se realiza acciones antes de realizar el cambio de modo
            If bol_validarModoActualizar(True) = False Then
                Exit Sub
            End If

            If o_form.Modo = enm_modoForm.VER Then

                ' Se asigna el texto al boton
                sub_asigTxtBotonPrincipal(enm_modoForm.ACTUALIZAR)

                ' Se asigna el modo al formulario
                o_form.Modo = enm_modoForm.ACTUALIZAR

            End If

            ' Se realiza acciones luego de realizar el cambio de modo
            If bol_validarModoActualizar(False) = False Then
                Exit Sub
            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_asigEventosControlActualizacion(ByVal po_controls As System.Windows.Forms.Control.ControlCollection)
        Try

            ' Se recorre los controles TextBox del formulario
            For Each lo_control As Control In po_controls

                If TypeOf (lo_control) Is DateEdit Then

                    ' Se le asigna un evento
                    AddHandler (CType(lo_control, TextEdit)).TextChanged, AddressOf ctr_modificacion

                ElseIf TypeOf (lo_control) Is TextEdit Then

                    ' Se le asigna un evento
                    AddHandler (CType(lo_control, TextEdit)).TextChanged, AddressOf ctr_modificacion

                ElseIf TypeOf (lo_control) Is uct_lstSeleccion Then

                    ' Se le asigna un evento
                    AddHandler (CType(lo_control, uct_lstSeleccion)).evt_modificacionTxt, AddressOf ctr_modificacion

                ElseIf TypeOf (lo_control) Is TextBox Then

                    ' Se le asigna un evento
                    AddHandler (CType(lo_control, TextBox)).TextChanged, AddressOf ctr_modificacion

                ElseIf TypeOf (lo_control) Is CheckBox Then

                    ' Se le asigna un evento
                    AddHandler (CType(lo_control, CheckBox)).CheckedChanged, AddressOf ctr_modificacion

                ElseIf TypeOf (lo_control) Is System.Windows.Forms.ComboBox Then

                    ' Se le asigna un evento
                    AddHandler (CType(lo_control, System.Windows.Forms.ComboBox)).SelectedIndexChanged, AddressOf ctr_modificacion

                ElseIf TypeOf (lo_control) Is GridControl Then

                    ' Se le asigna un evento
                    AddHandler (CType(lo_control, GridControl)).TextChanged, AddressOf ctr_modificacion
                    sub_asigEventosCambioControlsFormGrid(CType(lo_control, GridControl).RepositoryItems)

                ElseIf TypeOf (lo_control) Is uct_gridConBusqueda Then

                    ' Se verifica si el control esta asociado a una tabla
                    'If Not CType(lo_control, uct_gridConBusqueda).Tabla Is Nothing Then
                    '    ' Se le asigna un evento
                    '    AddHandler (CType(lo_control, uct_gridConBusqueda)).DataSource.RowDeleted, AddressOf ctr_modificacion
                    '    AddHandler (CType(lo_control, uct_gridConBusqueda)).DataSource.TableNewRow, AddressOf ctr_modificacion
                    'End If

                    AddHandler (CType(lo_control, uct_gridConBusqueda)).evt_modificacionGrid, AddressOf ctr_modificacion

                ElseIf TypeOf (lo_control) Is XtraTabControl Then

                    ' Se recorre los tabs
                    For j As Integer = 0 To CType(lo_control, XtraTabControl).TabPages.Count - 1

                        ' Se le asigna un evento
                        sub_asigEventosControlActualizacion(CType(lo_control, XtraTabControl).TabPages.Item(j).Controls)

                    Next

                ElseIf TypeOf (lo_control) Is Panel Then

                    sub_asigEventosControlActualizacion(CType(lo_control, Panel).Controls)

                End If

            Next

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Public Sub sub_asigEventosCambioControlsFormGrid(ByVal po_controles As RepositoryItemCollection)
        Try

            ' Se recorre los controles TextBox del formulario
            For Each lo_control As RepositoryItem In po_controles

                If TypeOf (lo_control) Is RepositoryItemDateEdit Then

                    ' Se le asigna un evento
                    AddHandler (CType(lo_control, RepositoryItemDateEdit)).EditValueChanging, AddressOf ctr_modificacion

                ElseIf TypeOf (lo_control) Is RepositoryItemTextEdit Then

                    ' Se le asigna un evento
                    AddHandler (CType(lo_control, RepositoryItemTextEdit)).EditValueChanging, AddressOf ctr_modificacion

                ElseIf TypeOf (lo_control) Is RepositoryItemComboBox Then

                    ' Se le asigna un evento
                    AddHandler (CType(lo_control, RepositoryItemComboBox)).SelectedIndexChanged, AddressOf ctr_modificacion

                End If

            Next

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub ctr_modificacion(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            ' Se cambia el modo del formulario a Actualizar si el modo actual es Ver
            sub_asignarModo(enm_modoForm.ACTUALIZAR)
        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#Region "ModoBusqueda"

    Private Sub sub_prepFormBuscar()
        Try

            ' Se limpia el formulario
            sub_limpiarControlesForm(o_form.Controls)

            ' Se prepara los controles para el modo busqueda de acuerdo a la entidad asociada al formulario
            sub_prepControlModoBuscar()

            ' Se asigna el texto al boton
            sub_asigTxtBotonPrincipal(enm_modoForm.BUSCAR)

            ' Se asigna el modo al formulario
            o_form.Modo = enm_modoForm.BUSCAR

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_prepControlModoBuscar()
        Try

            ' Se recorre los atributos de la entidad
            Dim lo_entidad As Object = obj_obtenerEntidad()

            ' Se recorre los atributos de la entidad
            For Each lo_field As FieldInfo In lo_entidad.GetType.GetFields(BindingFlags.NonPublic Or BindingFlags.Public Or Reflection.BindingFlags.Instance)

                ' Se obtiene la anotacion de atributo del campo
                Dim lo_annAtr As annAtributo = ann_obtenerAnotacion(lo_field, GetType(annAtributo))

                ' Se verifica si se obtuvo el atributo
                If lo_annAtr Is Nothing Then
                    Continue For
                End If

                ' Se verifica si el atributo corresponde a un campo de base de datos, si no corresponde a un detalle y si es un campo de busqueda
                If lo_annAtr.esCampoBD = True And lo_annAtr.esDetalle = False And lo_annAtr.deBusqueda = True Then

                    ' Se busca el control asociado al atributo
                    Dim lo_control As Control = ctr_obtenerControl(lo_annAtr.campoBD, o_form.Controls)

                    ' Se verifica si se obtuvo el control
                    If lo_control Is Nothing Then
                        Continue For
                    End If

                    ' Se habilita el control y se le asigna un color para diferenciar el modo busqueda
                    lo_control.Enabled = True
                    lo_control.BackColor = Color.LightYellow

                End If

            Next

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#Region "ModoVer"

    Private Sub sub_prepFormVer()
        Try

            ' Se realiza acciones antes de realizar el cambio de modo
            If bol_validarModoVer(True) = False Then
                Exit Sub
            End If

            ' Se prepara los controles de acuerdo al modo
            sub_prepControlModoVer()

            ' Se asigna el texto al boton
            sub_asigTxtBotonPrincipal(enm_modoForm.VER)

            ' Se asigna el modo al formulario
            o_form.Modo = enm_modoForm.VER

            ' Se realiza acciones luego de realizar el cambio de modo
            If bol_validarModoVer(False) = False Then
                Exit Sub
            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_prepControlModoVer()
        Try

            ' Se recorre los atributos de la entidad
            Dim lo_entidad As Object = obj_obtenerEntidad()

            ' Se recorre los atributos de la entidad
            For Each lo_field As FieldInfo In lo_entidad.GetType.GetFields(BindingFlags.NonPublic Or BindingFlags.Public Or Reflection.BindingFlags.Instance)

                ' Se obtiene la anotacion de atributo del campo
                Dim lo_annAtr As annAtributo = ann_obtenerAnotacion(lo_field, GetType(annAtributo))

                ' Se verifica si se obtuvo el atributo
                If lo_annAtr Is Nothing Then
                    Continue For
                End If

                ' Se verifica si el atributo corresponde a un campo de base de datos, si no corresponde a un detalle y si es un campo de busqueda
                If lo_annAtr.esCampoBD = True Then

                    ' Se busca el control asociado al atributo
                    Dim lo_control As Control = ctr_obtenerControl(lo_annAtr.campoBD, o_form.Controls)

                    ' Se verifica si se obtuvo el control
                    If lo_control Is Nothing Then
                        Continue For
                    End If

                    ' Se obtiene el estado 
                    Dim ls_estado As String = str_obtEstadoObjeto()

                    ' Se verifica si el campo es editable por el usuario
                    If lo_annAtr.usrEditable = True Then

                        ' Se verifica si el campo permite actualización
                        If lo_annAtr.seActualiza = True Then

                            ' Se verifica el estado: solo se puede actualizar campos en estado abierto
                            If Not ls_estado.Trim = "C" And Not ls_estado.Trim = "A" Then
                                sub_habilitacionControl(lo_control, True)
                            Else
                                sub_habilitacionControl(lo_control, False)
                            End If

                        Else
                            sub_habilitacionControl(lo_control, False)
                        End If

                        ' Se asigna el color de fondo al control
                        lo_control.BackColor = Color.White

                    Else

                        ' Se habilita el control y se le asigna un color para diferenciar el modo busqueda
                        sub_habilitacionControl(lo_control, False)
                        lo_control.BackColor = Color.White

                    End If

                End If

            Next

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#Region "ModoRecuperar"



#End Region

#End Region

#Region "Controles"

    Protected Function ctr_obtControl(ByVal ps_tag As String) As Control
        Try
            ' Se obtiene el control por nombre
            Dim lo_control As Control = ctr_obtenerControl(ps_tag, o_form.Controls)

            ' Se verifica si se obtuvo el control
            If lo_control Is Nothing Then
                MsgBox("No se pudo obtener el control <" & ps_tag & ">.")
                Return Nothing
            End If

            ' Se retorna el control obtenido
            Return lo_control

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region

#End Region

#Region "Consultas_Comun"


    Protected Function dbl_obtCuentaGanancia_pc() As String
        Try
            Dim ld_tipoCambio As String
            ld_tipoCambio = String.Empty
            ' Se obtiene el tipo de cambio para la fecha y la moneda recibida
            ld_tipoCambio = entComun.dbl_obtCuentaGanancia_pc_()
            'Dim ld_tipoCambio As Double = entComun.dbl_obtenerTipoCambio(ps_moneda, ps_fecha)

            Return ld_tipoCambio


        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return ""
        End Try
    End Function

    Protected Function dbl_obtTipoCambio(ByVal ps_moneda As String, ByVal ps_fecha As String) As Double
        Try
            If ps_moneda <> "SOL" Then
                ' Se obtiene el tipo de cambio para la fecha y la moneda recibida
                Dim ld_tipoCambio As Double = entComun.dbl_obtenerTipoCambio(ps_moneda, ps_fecha)

                ' Se verifica si se obtuvo algun valor del tipo de cambio

                If ld_tipoCambio = 0.0 Or ld_tipoCambio = -1 Then

                    ' Se muestra un mensaje que indique que no se pudo obtener el tipo de cambio para la fecha especificada
                    MsgBox("No se pudo obtener el tipo de cambio para la moneda " & ps_moneda & " en la fecha " & ps_fecha)

                    ' Se retorna 1
                    Return 0

                Else

                    ' Se retorna el valor obtenido
                    Return ld_tipoCambio

                End If
            Else

                ' Si la moneda es local, se retorna 1
                Return 1

            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return -1
        End Try
    End Function

    Protected Function dtb_obtDatosReporteConParams(ByVal ps_nomProc As String, Optional ByVal po_lstCtrParams As List(Of Control) = Nothing) As DataTable
        Try

            ' Se obtiene los parametros del procedure que obtendrá los datos del reporte
            Dim lo_dtb As DataTable = entComun.dtb_obtParamProcedure(ps_nomProc)

            ' Se muestra el reporte con una ventana de parametros
            Dim lo_frmRepParam As New frmParamReporte(ps_nomProc, lo_dtb)

            ' Se crea los parametros del reporte
            Dim lb_resCreaParam As Boolean = lo_frmRepParam.bol_crearControlesParam(po_lstCtrParams)

            ' Se verifica si se creó los parametros del reporte
            If lb_resCreaParam = True Then
                lo_frmRepParam.ShowDialog()
            End If

            ' Se verifica si se obtuvo resultados del reporte
            Dim lo_dtbResultado As DataTable = lo_frmRepParam.Resultado
            If lo_dtbResultado Is Nothing Then
                MsgBox("No se ejecuto la consulta. No es obtuvo resultados.")
                Return Nothing
            End If
            If lo_dtbResultado.Rows.Count = 0 Then
                MsgBox("No es obtuvo resultados con los criterios de selección ingresados.")
            End If

            ' Se retorna el dataTable obtenido
            Return lo_dtbResultado

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

#End Region

#Region "SAP"

    Protected Function sbo_conectar(ByVal ps_sboUsr As String, ByVal ps_sboPass As String) As SAPbobsCOM.Company
        Try

            ' Se obtiene el objeto company de SAP
            Return entComun.sbo_conectar(ps_sboUsr, ps_sboPass)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Protected Function bol_iniciarTransSBO(ByVal po_SBOCompany As SAPbobsCOM.Company) As Boolean
        Try
            ' Se verifica si existe una transaccion abierta
            If po_SBOCompany.InTransaction = True Then
                po_SBOCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
            End If


            ' Se inicia la transaccion
            po_SBOCompany.StartTransaction()

            ' Se retorna TRUE para indicar que la operacion se realizó con exito
            Return True

        Catch ex As Exception
            po_SBOCompany.Disconnect()
            Return False
        End Try
    End Function

    Protected Function bol_RollBackTransSBO(ByVal po_SBOCompany As SAPbobsCOM.Company) As Boolean
        Try
            ' Se verifica si existe una transaccion abierta
            If po_SBOCompany.InTransaction = True Then
                po_SBOCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
            Else
                Return False
            End If

            ' Se retorna TRUE para indicar que la operacion se realizó con exito
            Return True

        Catch ex As Exception
            po_SBOCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
            Return False
        End Try
    End Function

    Protected Function str_CommitTransSBO(ByVal po_SBOCompany As SAPbobsCOM.Company) As String
        Try
            ' Se verifica si existe una transaccion abierta
            If po_SBOCompany.InTransaction = True Then
                po_SBOCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
            Else
                Return "Error en la transaccion"
            End If

            ' Se retorna TRUE para indicar que la operacion se realizó con exito
            Return ""

        Catch ex As Exception
            If po_SBOCompany.InTransaction = True Then po_SBOCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
            Return ex.Message
        End Try
    End Function

    Protected Sub sub_errorRegistroPlanilla(ByVal po_SBOCompany As SAPbobsCOM.Company)
        Try

            ' Hubo un error al momento de generar el detalle de la planilla
            'MsgBox("Hubo un error al momento de generar el detalle de la planilla. No es posible realizar el proceso")
            sub_mostrarMensaje("Hubo un error al momento de generar el detalle de la planilla. No es posible realizar el proceso", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Protected Sub sub_errorProcesoSAP(ByVal po_SBOCompany As SAPbobsCOM.Company)
        Try

            ' Se declara dos variables para obtener el codigo y la descripcion del error
            Dim li_res As Integer = 0
            Dim ls_res As String = ""

            ' Hubo un error al momento de generar el detalle de la planilla
            po_SBOCompany.GetLastError(li_res, ls_res)
            sub_mostrarMensaje("Ocurrio un error al realizar la operacion en SAP: " & li_res.ToString & " - " & ls_res, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sap)
            'MsgBox("Ocurrio un error al realizar la operacion en SAP: " & li_res.ToString & " - " & ls_res)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

End Class
