Imports System.Windows.Forms
Imports Util
Imports cl_Entidad
Imports System.Drawing
Imports System.IO
Imports OfficeOpenXml
Imports DevExpress.XtraGrid.Columns

Public Class ClassGenPlanilla
    Inherits classComun

    ' Variables de la clase
    Private i_lineaNumAsg As Integer = 0
    Private o_dtbDocsCliente As DataTable
    Private o_dtbEC As DataTable
    Private b_difTC As Boolean = False
    Private numPos As String

#Region "Constructor"

    Public Sub New(ByVal po_form As frmComun)
        MyBase.New(po_form)
    End Sub

#End Region

#Region "Ini_formulario"

    Public Overrides Sub sub_cargarForm()
        Try

            ' Se inicializa el control de usuario GridControl para las Cuentas Por Cobrar del Cliente
            sub_iniULSClientes()

            ' Se inicializa el comboBox de cuentas bancarias
            sub_iniCboEC()

            ' Se indica que los grids de Clientes y de Estado de cuenta permitirán la multiselección de registros
            sub_habilitarMultiSelecGrids()

            ' Se inicializa el grid de planilla
            sub_iniGridPlanilla()

            ' Se asigna los eventos requeridos al grid de planilla
            sub_iniEventosGridPlanilla()

            ' Se inicializa el combo de Tipos de Planilla
            ini_comboTipoPla()

            'Cargar Evento Combo
          

            ' Se valida las planillas abiertas
            'bol_valPlanillasAbiertas()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_iniULSClientes()
        Try

            ' Se indica el filtro correspondiente al listado de seleccion del parametro de clientes
            Dim lo_ulsClientes As Control = ctr_obtenerControl("ulsClientes", o_form.Controls)
            CType(lo_ulsClientes, uct_lstSeleccion).sub_anadirCondicion("CardType", "C")

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_iniCboEC()
        Try

            ' Se indica el filtro correspondiente al listado de seleccion del parametro de clientes
            Dim lo_cboCtasBanco As Control = ctr_obtenerControl("CtasBanco", o_form.Controls)

            ' Se obtiene el dataTable con los resultados de la consulta de las cuentas bancarias
            Dim lo_dtb As DataTable = entPlanilla.dtb_obtCtasBanco

            ' Se asigna el dataTable como DataSource del comboBox
            CType(lo_cboCtasBanco, ComboBox).DataSource = lo_dtb
            CType(lo_cboCtasBanco, ComboBox).ValueMember = "Account"
            CType(lo_cboCtasBanco, ComboBox).DisplayMember = "BankName"

            AddHandler CType(lo_cboCtasBanco, ComboBox).SelectedIndexChanged, AddressOf seCargarPosBancoCombo

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_habilitarMultiSelecGrids()
        Try

            ' Se obtiene los Grids de Clientes y del Estado de Cuenta del banco
            Dim lo_ugcClientes As Control = ctr_obtenerControl("ECClientes", o_form.Controls)
            Dim lo_ugcEC As Control = ctr_obtenerControl("ECBanco", o_form.Controls)

            ' Se indica que ambos controles permitiran la multiselección
            CType(lo_ugcClientes, uct_gridConBusqueda).sub_habilitarMultiSelec(True)
            CType(lo_ugcEC, uct_gridConBusqueda).sub_habilitarMultiSelec(True)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_iniGridPlanilla()
        Try

            ' Se inicializa el control de usuario GridControl para el Estado de Cuenta
            Dim lo_ugc As Control = ctr_obtenerControl("gmi_plaPagosDetalle", o_form.Controls)

            ' Se inicializa el control
            CType(lo_ugc, uct_gridConBusqueda).sub_inicializar()

            ' Se indica que no permitira reordenar los registros al hacer click en la cabecera de las columnas
            CType(lo_ugc, uct_gridConBusqueda).sub_habilitarOrdenCol(False)

            ' Se indica el objeto de detalle asociado al grid
            CType(lo_ugc, uct_gridConBusqueda).ObjDetalle = New entPlanilla_Lineas

            ' Se indica un control especifico para la columna DifTC
            CType(lo_ugc, uct_gridConBusqueda).sub_asigControlColumna("difTC", enm_tipoControlRptryItem.CHECKBOX)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_iniEventosGridPlanilla()
        Try

            ' Se obtiene el grid de la planilla
            Dim lo_ugc As uct_gridConBusqueda = ctr_obtenerControl("gmi_plaPagosDetalle", o_form.Controls)

            ' Se le asigna un evento para obtener el evento de eliminar
            AddHandler lo_ugc.evt_eliminacionFilas, AddressOf seEliminoFilas
            AddHandler lo_ugc.evt_alModificarCeldaGrid, AddressOf seModificaCeldaGrid
            AddHandler lo_ugc.evt_trasModificarCeldaGrid, AddressOf seModificoCeldaGrid
            AddHandler lo_ugc.evt_mostrandoEditorColumna, AddressOf seMuestraEditorColumna

            'evt_CargarPosicionBancoCombo 

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub seEliminoFilas(ByVal sender As Object, ByVal e As EventArgs)
        Try

            ' Se ejecuta la accion de carga de ambos grids superiores
            sub_recargarGridsSuperiores()

            ' Se reenumera el numero de linea por asignacion
            sub_reenumerarLineaNumAsg()

            ' Se ejecuta la accion de autoguardado al eliminar las filas
            sub_autoGuardar()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub seModificaCeldaGrid(ByVal sender As Object, ByVal e As EventArgs)
        Try

            ' Se valida la seleccion del check de Diferencia de Cambio
            sub_valSelChkDifTC(e)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub seModificoCeldaGrid(ByVal sender As Object, ByVal e As EventArgs)
        Try

            ' Se verifica si se debe asignar el check de diferencia de cambio a la fila actual
            sub_asigCheckDifTC()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub seCargarPosBancoCombo(ByVal sender As Object, ByVal e As EventArgs)
        Try

            ' Se realiza los eventos correspondientes al mostrar el editor de la columna del grid
            sub_manejarCargaPosBanco(sender, e)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub seMuestraEditorColumna(ByVal sender As Object, ByVal e As EventArgs)
        Try

            ' Se realiza los eventos correspondientes al mostrar el editor de la columna del grid
            sub_manejarColImpApl(sender, e)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Protected Overrides Function bol_validarModoAnadir(ByVal pb_antesAccion As Boolean) As Boolean
        Try

            ' Se verifica si la validacion se ejecuta antes de realizar el cambio de modo
            If pb_antesAccion = True Then
                'Return bol_valPlanillasAbiertas()
            End If

            ' Si la validacion fue correcta
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Protected Overrides Function bol_validarRecuperar(ByVal pb_antesAccion As Boolean) As Boolean
        Try

            ' Se verifica si la validacion se ejecuta antes de realizar el cambio de modo
            If pb_antesAccion = False Then
                Return bol_valIdPlanillaAbiertaRecp()
            End If

            ' Si la validacion fue correcta
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Function bol_valPlanillasAbiertas() As Boolean
        Try

            ' Se verifica si existen planillas abiertas en la base de datos
            Dim li_planAbiertas As Integer = entPlanilla.int_obtPlanillasAbiertas

            ' Se verifica si hay planillas abiertas
            If li_planAbiertas > 0 Then

                ' Se muestra un mensaje que indica que hay planillas abiertas pendientes por procesar
                MsgBox("Existe(n) " & li_planAbiertas.ToString & " planilla(s) abierta(s). Debe procesar o cancelar dicha(s) planilla(s) antes de registrar una nueva.")

                ' Se verifica si el modo actual es añadir
                sub_asignarModo(enm_modoForm.BUSCAR)

                ' No se realiza el cambio de modo
                Return False

            End If

            ' Se retorna TRUE
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Function bol_valIdPlanillaAbiertaRecp() As Boolean
        Try

            ' Se obtiene el control con el id de la planilla
            Dim lo_control As Control = ctr_obtenerControl("id", o_form.Controls)

            ' Se verifica si se obtuvo el control
            If lo_control Is Nothing Then
                sub_mostrarMensaje("No se obtuvo el control <id>.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                sub_asignarModo(enm_modoForm.BUSCAR)
                Return False
            End If

            ' Se obtiene el campo Id
            Dim li_id As Integer = CInt(obj_obtValorControl(lo_control))

            ' Se verifica que el id obtenido corresponda a una planilla abierta
            Dim ls_estado As String = entPlanilla.str_verTipoPlanilla(li_id)

            ' Si ocurrio un error al obtener el estado de la planilla
            If ls_estado = "" Then
                sub_mostrarMensaje("Ocurrio un error al obtener el estado de la planilla.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                sub_asignarModo(enm_modoForm.BUSCAR)
                Return False
            End If

            ' Se verifica si el estado obtenido corresponde a una planilla cerrada o anulada
            If ls_estado.Trim = "C" Or ls_estado.Trim = "A" Then
                MsgBox("Los datos obtenidos corresponden a una planilla que se encuentra Cerrada o Cancelada.")
                sub_asignarModo(enm_modoForm.BUSCAR)
                Return False
            End If

            ' Si el estado obtenido corresponde a una planilla abierta o si señala que la planilla no existe, se muestra los datos recuperados
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Sub ini_comboTipoPla()
        Try

            ' Se indica el filtro correspondiente al listado de seleccion del parametro de clientes
            Dim lo_cboCtasBanco As ComboBox = ctr_obtenerControl("tipoPla", o_form.Controls)

            ' Se verifica si se obtuvo el comboBox
            If Not lo_cboCtasBanco Is Nothing Then

                ' Se declara un dataTable para el estado
                Dim lo_dtb As New DataTable

                ' Se crea las columnas del datatable
                lo_dtb.Columns.Add("val")
                lo_dtb.Columns.Add("dscr")

                ' Se añade los estados al dataTable
                Dim lo_row As DataRow = lo_dtb.NewRow
                lo_row.BeginEdit()
                lo_row("val") = "C"
                lo_row("dscr") = "Comercial"
                lo_row.EndEdit()

                ' Se añade la fila al dataTable
                lo_dtb.Rows.Add(lo_row)

                ' Se limpia el objeto row
                lo_row = Nothing

                ' Se inicializa una nueva fila
                lo_row = lo_dtb.NewRow
                lo_row.BeginEdit()
                lo_row("val") = "D"
                lo_row("dscr") = "Detraccion"
                lo_row.EndEdit()

                ' Se añade la fila al dataTable
                lo_dtb.Rows.Add(lo_row)

                ' Se limpia el objeto row
                lo_row = Nothing

                ' Se inicializa una nueva fila
                lo_row = lo_dtb.NewRow
                lo_row.BeginEdit()
                lo_row("val") = "A"
                lo_row("dscr") = "Auto-detraccion"
                lo_row.EndEdit()

                ' Se añade la fila al dataTable
                lo_dtb.Rows.Add(lo_row)

                ' Se limpia el objeto row
                lo_row = Nothing

                ' Se asigna el dataTable al Combo de Estado
                lo_cboCtasBanco.DataSource = lo_dtb
                lo_cboCtasBanco.ValueMember = "val"
                lo_cboCtasBanco.DisplayMember = "dscr"

            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#Region "ProcesoNegocio"

    ' Se valida la accion de cancelar
    Protected Overrides Function bol_valCancelar(ByVal pb_antesAccion As Boolean) As Boolean
        Try

            ' Se verifica si la validación se realiza antes o despues de la acción
            If pb_antesAccion = True Then

                ' Se verifica el estado de la planilla
                Dim ls_estado As String = str_obtEstadoObjeto()

                ' Se verifica si se obtuvo el estado
                If ls_estado Is Nothing Then
                    sub_mostrarMensaje("No se pudo obtener el estado de la planilla.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                    Return False
                End If
                If ls_estado.Trim = "" Then
                    sub_mostrarMensaje("No se pudo obtener el estado de la planilla.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                    Return False
                End If

                ' Se verifica si la planilla esta Cancelada
                If ls_estado.Trim = "A" Then
                    MsgBox("La planilla ya se encuentra Cancelada.")
                    Return False
                End If

                ' Se verfica si el estado no es abierto
                If ls_estado.Trim <> "O" Then
                    MsgBox("Desde esta ventana, solo puede cancelar una planilla Abierta. Para cancelar una planilla Cerrada, debe dirigirse a la ventana <Procesar Planilla>.")
                    Return False
                End If

                ' Se retorna true si todo fue correcto
                Return True

            End If

            ' Se retorna true
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

#End Region

#Region "accionesForm"

    Public Sub distribuir()

        Dim operacion As String
        Dim id As Integer
        Dim monto As Double
        Dim lo_ugcEC As Control = ctr_obtenerControl("ECBanco", o_form.Controls)

        ' Se obtiene los registros seleccionados de la grilla
        Dim lo_dtbSelsEC As DataTable = CType(lo_ugcEC, uct_gridConBusqueda).dtb_obtenerSeleccion


        If lo_dtbSelsEC.Rows.Count = 1 Then
            operacion = lo_dtbSelsEC.Rows(0)("Operacion")
            id = lo_dtbSelsEC.Rows(0)("id")
            monto = lo_dtbSelsEC.Rows(0)("Monto")

            Dim Distribucion As New FrmDistribucion_Prs
            Distribucion.Operacion = operacion
            Distribucion.ID = id
            Distribucion.Monto = monto
            Distribucion.ShowDialog()

        End If
    End Sub



    ' Se adiciona un registro en la grilla de la planilla a partir de los registros seleccionados en las grillas superiores
    Public Sub sub_adicionarRegistro()
        Try

            Dim li_confirm As Integer = -1

            ' Se obtiene los controles de usuario Grid
            Dim lo_ugcClientes As Control = ctr_obtenerControl("ECClientes", o_form.Controls)
            Dim lo_ugcEC As Control = ctr_obtenerControl("ECBanco", o_form.Controls)

            ' Se verifica si se obtuvo los controles
            If lo_ugcClientes Is Nothing Then

                ' Se concluye con el metodo
                sub_mostrarMensaje("No se pudo obtener el control <ECClientes>.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub

            End If

            If lo_ugcClientes Is Nothing Then

                ' Se concluye con el metodo
                sub_mostrarMensaje("No se pudo obtener el control <ECBanco>.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub

            End If

            ' Se obtiene los registros seleccionados de ambos Grids
            Dim lo_dtbSelsClient As DataTable = CType(lo_ugcClientes, uct_gridConBusqueda).dtb_obtenerSeleccion
            Dim lo_dtbSelsEC As DataTable = CType(lo_ugcEC, uct_gridConBusqueda).dtb_obtenerSeleccion

            ' Se verifica que haya registros seleccionados en las grillas superiores
            If lo_dtbSelsClient.Rows.Count = 0 Or lo_dtbSelsEC.Rows.Count = 0 Then

                ' Se muestra un mensaje que indique que se debe seleccionar registros en ambas grillas
                MsgBox("Debe seleccionar al menos un registro en cada grilla para añadir un registro a la planilla.")

                ' Se concluye con el metodo
                Exit Sub

            End If

            ' Se verifica si ambass grillas cuentan con mas de un registro seleccionado
            If lo_dtbSelsClient.Rows.Count > 1 And lo_dtbSelsEC.Rows.Count > 1 Then

                ' Se muestra un mensaje que indique que se debe seleccionar registros en ambas grillas
                MsgBox("Ambas grillas no pueden contar con mas de un registro seleccionado.")

                ' Se concluye con el metodo
                Exit Sub

            End If

            ' Se verifica si entre los documentos o depósitos seleccionados existe alguno que ya esté registrado en una planilla abierta
            If bol_valRegEnPllAbiertas(lo_dtbSelsClient, lo_dtbSelsEC) = False Then
                Exit Sub
            End If

            ' Se valida la posible selección de Saldos a Favor
            'If bol_valSaldosAFavor(lo_dtbSelsEC) = False Then
            '    Exit Sub
            'End If

            'FOOD PARA AJUSTE
            Dim aplicaAjuste As Boolean
            aplicaAjuste = True

            ' Se verifica si alguna de las grillas cuenta con mas de un registro seleccionado
            If lo_dtbSelsClient.Rows.Count > 1 Or lo_dtbSelsEC.Rows.Count > 1 Then

                ' Se obtiene el tipo de planilla
                Dim ls_tipoPla As String = str_obtTipoPlanilla()

                ' Se verifica el tipo de planilla
                If ls_tipoPla.Trim = "D" Then
                    MsgBox("En las planillas de tipo Detraccion solo está permitida la asignación de un documento hacia un deposito.")
                    Exit Sub
                End If

                ' Si la grilla de clientes tiene mas de un registro, se distrubye el monto de la operacion del EC seleccionada entrer los documentos del cliente seleccionados
                If lo_dtbSelsClient.Rows.Count > 1 Then
                    sub_adicionMuchosAUno(lo_dtbSelsClient, lo_dtbSelsEC)

                    aplicaAjuste = False


                End If

                ' Si la grilla del EC tiene mas de un registro, se distribuye el monto del documento del cliente entre el número de operaciones de pago del EC
                If lo_dtbSelsEC.Rows.Count > 1 Then
                    sub_adicionUnoAMuchos(lo_dtbSelsClient, lo_dtbSelsEC)

                    aplicaAjuste = False

                End If

            End If





            Dim booleanaj As String
            booleanaj = String.Empty
            If aplicaAjuste Then

                '' agregar la respuesta si o no
                li_confirm = MessageBox.Show("¿Desea que se genere un asiento de ajuste?", "caption", MessageBoxButtons.YesNoCancel)

                Dim asientoAjustoF As String
                'Se verifica el resultado del mensaje de confirmacion
                If Not li_confirm = DialogResult.Yes Then

                    'lo_dtbSelsEC.Rows(0)("asientoajustoT") = "N"
                    booleanaj = "N"

                Else
                    ''lo_dtbSelsEC.Rows(0)("asientoajustoT") = "Y"
                    ''lo_dtbSelsEC.Rows(0)("asientoajustoT") = "Y"
                    'lo_dtbSelsEC.Rows.Add()
                    'lo_dtbSelsEC.Rows(0)("asientoajustoT") = "Y"
                    booleanaj = "Y"

                End If

            End If

            ' Se verifica si ambas grillas cuentan con un registro
            If lo_dtbSelsClient.Rows.Count = 1 And lo_dtbSelsEC.Rows.Count = 1 Then

                ' Se añade añade el registro en la grilla de la planilla
                sub_adicionUnoAUnov2(lo_dtbSelsClient, lo_dtbSelsEC, booleanaj)

            End If



            'If lo_dtbSelsClient.Rows.Count = 0 Or lo_dtbSelsEC.Rows.Count = 0 Then

            '    ' Se muestra un mensaje que indique que se debe seleccionar registros en ambas grillas
            '    MsgBox("Debe seleccionar al menos un registro en cada grilla para añadir un registro a la planilla.")

            '    ' Se concluye con el metodo
            '    Exit Sub

            'End If





        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_adicionUnoAUno(ByVal po_dtbDocsClien As DataTable, ByVal po_dtbEC As DataTable)
        Try

            ' Se obtiene el dataTable del grid de planilla
            Dim lo_control As Control = ctr_obtenerControl("gmi_plaPagosDetalle", o_form.Controls)
            Dim lo_uctPlanilla As uct_gridConBusqueda = CType(lo_control, uct_gridConBusqueda)
            Dim lo_dtb As DataTable = CType(obj_obtValorControl(lo_uctPlanilla), DataTable)

            ' Se obtiene el tipo de planilla
            Dim ls_tipoPla As String = str_obtTipoPlanilla()

            ' Se verifica si se obtuvo el tipo de planilla
            If ls_tipoPla.Trim = "" Then
                sub_mostrarMensaje("No se obtuvo el tipo de planilla.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            ' Se declara una variable para obtener la moneda local de la compañia SAP
            Dim ls_monedaLocal As String = entComun.str_obtMonLocal

            ' Se declara una variable para obtener el saldo del documento
            Dim ld_saldoDoc As Double = CDbl(po_dtbDocsClien.Rows(0)("Saldo"))

            ' Se obtiene el saldo del documento en ambas monedas: local y extrangera
            Dim ld_saldoDocLocal As Double = dbl_obtImportesPorMoneda(ld_saldoDoc, po_dtbDocsClien.Rows(0)("MonedaDoc"), ls_monedaLocal, po_dtbDocsClien.Rows(0)("FechaDoc"))
            Dim ld_saldoDocME As Double = dbl_obtImportesPorMoneda(ld_saldoDoc, po_dtbDocsClien.Rows(0)("MonedaDoc"), "USD", po_dtbDocsClien.Rows(0)("FechaDoc"))

            ' Se añade el registro a la grilla de PLanilla
            Dim lo_row As DataRow = lo_dtb.NewRow
            lo_row.BeginEdit()
            lo_row("id") = obj_obtValorControl(ctr_obtenerControl("id", o_form.Controls))
            lo_row("lineaNumAsg") = int_obtLineaNumAsg()
            lo_row("Codigo") = po_dtbDocsClien.Rows(0)("Codigo")
            lo_row("Nombre") = po_dtbDocsClien.Rows(0)("Nombre")
            lo_row("Id_Doc") = po_dtbDocsClien.Rows(0)("Id_Doc")
            lo_row("Referencia") = po_dtbDocsClien.Rows(0)("Referencia")
            lo_row("Tipo_Doc") = po_dtbDocsClien.Rows(0)("TransType")
            lo_row("DocLine") = po_dtbDocsClien.Rows(0)("Line_ID")
            lo_row("FechaDoc") = po_dtbDocsClien.Rows(0)("FechaDoc")
            lo_row("Comentario") = po_dtbDocsClien.Rows(0)("Comentario")
            lo_row("MonedaDoc") = po_dtbDocsClien.Rows(0)("MonedaDoc")
            lo_row("TipoCambioDoc") = po_dtbDocsClien.Rows(0)("TipoCambioDoc")
            lo_row("Total") = po_dtbDocsClien.Rows(0)("Total")
            lo_row("Saldo") = po_dtbDocsClien.Rows(0)("Saldo")


            ' Se verifica el tipo de planilla
            If ls_tipoPla = "D" Then
                lo_row("Imp_Aplicado") = dbl_obtImportesPorMoneda(po_dtbEC.Rows(0)("Monto"), po_dtbEC.Rows(0)("Moneda"), "SOL", po_dtbDocsClien.Rows(0)("FechaDoc"))
                lo_row("Imp_AplicadoME") = dbl_obtImportesPorMoneda(po_dtbEC.Rows(0)("Monto"), po_dtbEC.Rows(0)("Moneda"), "USD", po_dtbDocsClien.Rows(0)("FechaDoc"))
            Else
                If ls_tipoPla = "A" Then
                    lo_row("Imp_Aplicado") = dbl_obtImportesPorMoneda(po_dtbEC.Rows(0)("Monto"), po_dtbEC.Rows(0)("Moneda"), "SOL", po_dtbDocsClien.Rows(0)("FechaDoc"))
                    lo_row("Imp_AplicadoME") = dbl_obtImportesPorMoneda(po_dtbEC.Rows(0)("Monto"), po_dtbEC.Rows(0)("Moneda"), "USD", po_dtbDocsClien.Rows(0)("FechaDoc"))
                Else
                    lo_row("Imp_Aplicado") = dbl_obtImportesPorMoneda(po_dtbEC.Rows(0)("Monto"), po_dtbEC.Rows(0)("Moneda"), "SOL", po_dtbDocsClien.Rows(0)("FechaDoc"))
                    lo_row("Imp_AplicadoME") = dbl_obtImportesPorMoneda(po_dtbEC.Rows(0)("Monto"), po_dtbEC.Rows(0)("Moneda"), "USD", po_dtbDocsClien.Rows(0)("FechaDoc"))
                End If

            End If

            lo_row("MonedaPag") = po_dtbEC.Rows(0)("Moneda")
            lo_row("Tipo_Cambio") = dbl_obtTipoCambio("USD", CDate(po_dtbDocsClien.Rows(0)("FechaDoc")).ToString("yyyyMMdd"))
            lo_row("Cuenta") = po_dtbEC.Rows(0)("Cta_Contable")

            Dim FechaDocumento As DevExpress.XtraEditors.DateEdit = ctr_obtenerControl("FechaDocumento", o_form.Controls)
            lo_row("FechaPago") = If(FechaDocumento.EditValue = Nothing, po_dtbEC.Rows(0)("Fecha"), FechaDocumento.EditValue)
            lo_row("FechaDeposito") = po_dtbEC.Rows(0)("Fecha")
            lo_row("Nro_Operacion") = po_dtbEC.Rows(0)("Operacion")
            lo_row("idEC") = po_dtbEC.Rows(0)("id")
            lo_row("ComentarioPl") = str_obtComentOper(lo_row("MonedaDoc"), lo_row("Saldo"), lo_row("Imp_Aplicado"), lo_row("Imp_AplicadoME"))
            lo_row("SaldoFavor") = str_obtSaldoAFavor(ls_monedaLocal, ld_saldoDocLocal, ld_saldoDocME, lo_row("Imp_Aplicado"), lo_row("Imp_AplicadoME"))
            lo_row("SaldoFavorME") = str_obtSaldoAFavor("USD", ld_saldoDocLocal, ld_saldoDocME, lo_row("Imp_Aplicado"), lo_row("Imp_AplicadoME"))
            lo_row("MontoOp") = po_dtbEC.Rows(0)("Monto")
            lo_row("tipoAsg") = 0
            lo_row("tipoEC") = po_dtbEC.Rows(0)("TipoOp")

            lo_row("asientoajustoT") = po_dtbEC.Rows(0)("asientoajustoT")

            lo_row("difTC") = "N"
            lo_row("FechaCrea") = Now.Date
            lo_row("Ruc") = po_dtbDocsClien.Rows(0)("Ruc").ToString
            lo_row("CodBien") = po_dtbDocsClien.Rows(0)("CodBien").ToString

            Dim lo_cboPosBanco As Control = ctr_obtenerControl("PosBanco", o_form.Controls)
            lo_row("numPos") = lo_cboPosBanco.Text
            lo_row.EndEdit()

            ' Seobtienen las grillas superiores
            Dim lo_uctDocsClient As uct_gridConBusqueda = ctr_obtenerControl("ECClientes", o_form.Controls)
            Dim lo_uctEC As uct_gridConBusqueda = ctr_obtenerControl("ECBanco", o_form.Controls)

            ' Se valida la adicion del registro
            If bol_valUnoAUno(po_dtbDocsClien.Rows(0), po_dtbEC.Rows(0)) = True Then

                ' Se añade la fila al grid Planilla
                sub_adcFilaUnoAUno(lo_row, lo_dtb, lo_uctDocsClient, lo_uctEC, lo_uctPlanilla)

            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub


    Private Sub sub_adicionUnoAUnov2(ByVal po_dtbDocsClien As DataTable, ByVal po_dtbEC As DataTable, ByVal valor1AJ As String)
        Try

            ' Se obtiene el dataTable del grid de planilla
            Dim lo_control As Control = ctr_obtenerControl("gmi_plaPagosDetalle", o_form.Controls)
            Dim lo_uctPlanilla As uct_gridConBusqueda = CType(lo_control, uct_gridConBusqueda)
            Dim lo_dtb As DataTable = CType(obj_obtValorControl(lo_uctPlanilla), DataTable)

            ' Se obtiene el tipo de planilla
            Dim ls_tipoPla As String = str_obtTipoPlanilla()

            ' Se verifica si se obtuvo el tipo de planilla
            If ls_tipoPla.Trim = "" Then
                sub_mostrarMensaje("No se obtuvo el tipo de planilla.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            ' Se declara una variable para obtener la moneda local de la compañia SAP
            Dim ls_monedaLocal As String = entComun.str_obtMonLocal

            ' Se declara una variable para obtener el saldo del documento
            Dim ld_saldoDoc As Double = CDbl(po_dtbDocsClien.Rows(0)("Saldo"))

            ' Se obtiene el saldo del documento en ambas monedas: local y extrangera
            Dim ld_saldoDocLocal As Double = dbl_obtImportesPorMoneda(ld_saldoDoc, po_dtbDocsClien.Rows(0)("MonedaDoc"), ls_monedaLocal, po_dtbDocsClien.Rows(0)("FechaDoc"))
            Dim ld_saldoDocME As Double = dbl_obtImportesPorMoneda(ld_saldoDoc, po_dtbDocsClien.Rows(0)("MonedaDoc"), "USD", po_dtbDocsClien.Rows(0)("FechaDoc"))

            ' Se añade el registro a la grilla de PLanilla
            Dim lo_row As DataRow = lo_dtb.NewRow
            lo_row.BeginEdit()
            lo_row("id") = obj_obtValorControl(ctr_obtenerControl("id", o_form.Controls))
            lo_row("lineaNumAsg") = int_obtLineaNumAsg()
            lo_row("Codigo") = po_dtbDocsClien.Rows(0)("Codigo")
            lo_row("Nombre") = po_dtbDocsClien.Rows(0)("Nombre")
            lo_row("Id_Doc") = po_dtbDocsClien.Rows(0)("Id_Doc")
            lo_row("Referencia") = po_dtbDocsClien.Rows(0)("Referencia")
            lo_row("Tipo_Doc") = po_dtbDocsClien.Rows(0)("TransType")
            lo_row("DocLine") = po_dtbDocsClien.Rows(0)("Line_ID")
            lo_row("FechaDoc") = po_dtbDocsClien.Rows(0)("FechaDoc")
            lo_row("Comentario") = po_dtbDocsClien.Rows(0)("Comentario")
            lo_row("MonedaDoc") = po_dtbDocsClien.Rows(0)("MonedaDoc")
            lo_row("TipoCambioDoc") = po_dtbDocsClien.Rows(0)("TipoCambioDoc")
            lo_row("Total") = po_dtbDocsClien.Rows(0)("Total")
            lo_row("Saldo") = po_dtbDocsClien.Rows(0)("Saldo")


            ' Se verifica el tipo de planilla
            If ls_tipoPla = "D" Then
                lo_row("Imp_Aplicado") = dbl_obtImportesPorMoneda(po_dtbEC.Rows(0)("Monto"), po_dtbEC.Rows(0)("Moneda"), "SOL", po_dtbDocsClien.Rows(0)("FechaDoc"))
                lo_row("Imp_AplicadoME") = dbl_obtImportesPorMoneda(po_dtbEC.Rows(0)("Monto"), po_dtbEC.Rows(0)("Moneda"), "USD", po_dtbDocsClien.Rows(0)("FechaDoc"))
            Else
                If ls_tipoPla = "A" Then
                    lo_row("Imp_Aplicado") = dbl_obtImportesPorMoneda(po_dtbEC.Rows(0)("Monto"), po_dtbEC.Rows(0)("Moneda"), "SOL", po_dtbDocsClien.Rows(0)("FechaDoc"))
                    lo_row("Imp_AplicadoME") = dbl_obtImportesPorMoneda(po_dtbEC.Rows(0)("Monto"), po_dtbEC.Rows(0)("Moneda"), "USD", po_dtbDocsClien.Rows(0)("FechaDoc"))
                Else
                    lo_row("Imp_Aplicado") = dbl_obtImportesPorMoneda(po_dtbEC.Rows(0)("Monto"), po_dtbEC.Rows(0)("Moneda"), "SOL", po_dtbDocsClien.Rows(0)("FechaDoc"))
                    lo_row("Imp_AplicadoME") = dbl_obtImportesPorMoneda(po_dtbEC.Rows(0)("Monto"), po_dtbEC.Rows(0)("Moneda"), "USD", po_dtbDocsClien.Rows(0)("FechaDoc"))
                End If

            End If

            lo_row("MonedaPag") = po_dtbEC.Rows(0)("Moneda")
            lo_row("Tipo_Cambio") = dbl_obtTipoCambio("USD", CDate(po_dtbDocsClien.Rows(0)("FechaDoc")).ToString("yyyyMMdd"))
            lo_row("Cuenta") = po_dtbEC.Rows(0)("Cta_Contable")

            Dim FechaDocumento As DevExpress.XtraEditors.DateEdit = ctr_obtenerControl("FechaDocumento", o_form.Controls)
            lo_row("FechaPago") = If(FechaDocumento.EditValue = Nothing, po_dtbEC.Rows(0)("Fecha"), FechaDocumento.EditValue)
            lo_row("FechaDeposito") = po_dtbEC.Rows(0)("Fecha")
            lo_row("Nro_Operacion") = po_dtbEC.Rows(0)("Operacion")
            lo_row("idEC") = po_dtbEC.Rows(0)("id")
            lo_row("ComentarioPl") = str_obtComentOper(lo_row("MonedaDoc"), lo_row("Saldo"), lo_row("Imp_Aplicado"), lo_row("Imp_AplicadoME"))
            lo_row("SaldoFavor") = str_obtSaldoAFavor(ls_monedaLocal, ld_saldoDocLocal, ld_saldoDocME, lo_row("Imp_Aplicado"), lo_row("Imp_AplicadoME"))
            lo_row("SaldoFavorME") = str_obtSaldoAFavor("USD", ld_saldoDocLocal, ld_saldoDocME, lo_row("Imp_Aplicado"), lo_row("Imp_AplicadoME"))
            lo_row("MontoOp") = po_dtbEC.Rows(0)("Monto")
            lo_row("tipoAsg") = 0
            lo_row("tipoEC") = po_dtbEC.Rows(0)("TipoOp")

            lo_row("asientoajustoT") = valor1AJ

            lo_row("difTC") = "N"
            lo_row("FechaCrea") = Now.Date
            lo_row("Ruc") = po_dtbDocsClien.Rows(0)("Ruc").ToString
            lo_row("CodBien") = po_dtbDocsClien.Rows(0)("CodBien").ToString

            Dim lo_cboPosBanco As Control = ctr_obtenerControl("PosBanco", o_form.Controls)
            lo_row("numPos") = lo_cboPosBanco.Text
            lo_row.EndEdit()

            ' Seobtienen las grillas superiores
            Dim lo_uctDocsClient As uct_gridConBusqueda = ctr_obtenerControl("ECClientes", o_form.Controls)
            Dim lo_uctEC As uct_gridConBusqueda = ctr_obtenerControl("ECBanco", o_form.Controls)

            ' Se valida la adicion del registro
            If bol_valUnoAUno(po_dtbDocsClien.Rows(0), po_dtbEC.Rows(0)) = True Then

                ' Se añade la fila al grid Planilla
                sub_adcFilaUnoAUno(lo_row, lo_dtb, lo_uctDocsClient, lo_uctEC, lo_uctPlanilla)

            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub


    Private Sub sub_adicionUnoAMuchos(ByVal po_dtbDocsClien As DataTable, ByVal po_dtbEC As DataTable)
        Try

            ' Seobtienen las grillas superiores
            Dim lo_uctDocsClient As uct_gridConBusqueda = ctr_obtenerControl("ECClientes", o_form.Controls)
            Dim lo_uctEC As uct_gridConBusqueda = ctr_obtenerControl("ECBanco", o_form.Controls)

            ' Se obtiene la grilla de planilla
            Dim lo_uctPlanilla As uct_gridConBusqueda = ctr_obtenerControl("gmi_plaPagosDetalle", o_form.Controls)
            Dim lo_dtb As DataTable = CType(obj_obtValorControl(lo_uctPlanilla), DataTable)

            ' Se obtiene el tipo de planilla
            Dim ls_tipoPla As String = str_obtTipoPlanilla()

            ' Se verifica si se obtuvo el tipo de planilla
            If ls_tipoPla.Trim = "" Then
                sub_mostrarMensaje("No se obtuvo el tipo de planilla.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            ' Se verifica si se selecciono algun registro de saldo a favor
            Dim lb_tieneSF As Boolean = bol_tieneSF(po_dtbEC)

            ' Se declara una variable para el tipo de asignacion: UNO a MUCHOS simple o UNO a MUCHOS con Saldo a Favor
            Dim li_tipoAsg As Integer = -1

            ' Se verifica si la asignacion actual tiene Saldo a Favor
            If lb_tieneSF = False Then
                li_tipoAsg = 1 ' UNO A MUCHOS simple
            Else
                li_tipoAsg = 3 ' UNO A MUCHOS con Saldo a Favor
            End If

            ' Se valida, si se seleccionó algun saldo a favor, que dicho registro sea del mismo cliente del documento seleccionado
            If lb_tieneSF = True Then

                ' Se verifica si hay algun saldo a favor que no corresponda al cliente del documento seleccionado
                If bol_valClienteSF(po_dtbDocsClien.Rows(0), po_dtbEC) = False Then

                    ' Se finaliza el metodo
                    Exit Sub

                End If

            End If

            ' Se declara una variable para obtener la moneda local de la compañia SAP
            Dim ls_monedaLocal As String = entComun.str_obtMonLocal

            ' Se declara dos variables para los importes aplicados en moneda local y extranjera
            Dim ld_impApl As Double = 0.0
            Dim ld_impAplME As Double = 0.0

            ' Se declara dos variables que acumulen la suma de los importes en moneda local y extranjera 
            Dim ld_sumImpApl As Double = 0.0
            Dim ld_sumIimpAplME As Double = 0.0

            ' Se declara una variable para obtener el saldo del documento
            Dim ld_saldoDoc As Double = CDbl(po_dtbDocsClien.Rows(0)("Saldo"))

            ' Se obtiene el saldo del documento en ambas monedas: local y extrangera
            Dim ld_saldoDocLocal As Double
            Dim ld_saldoDocME As Double

            ' Se verifica el tipo de planilla           
            ld_saldoDocLocal = dbl_obtImportesPorMoneda(ld_saldoDoc, po_dtbDocsClien.Rows(0)("MonedaDoc"), ls_monedaLocal, po_dtbDocsClien.Rows(0)("FechaDoc"))
            ld_saldoDocME = dbl_obtImportesPorMoneda(ld_saldoDoc, po_dtbDocsClien.Rows(0)("MonedaDoc"), "USD", po_dtbDocsClien.Rows(0)("FechaDoc"))

            ' Se obtiene la moneda del documento
            Dim ls_monedaDoc As String = CStr(po_dtbDocsClien.Rows(0)("MonedaDoc"))

            ' Se obtiene la fila del dataTable del documento de clientes
            Dim lo_rowDoc As DataRow = po_dtbDocsClien.Rows(0)

            ' Se obtiene el total de los registros del Estado de Cuenta seleccionados
            For Each lo_rowEC As DataRow In po_dtbEC.Rows

                ' Se obtiene los importes en moneda local y extranjera              
                ld_impApl = dbl_obtImportesPorMoneda(lo_rowEC("Monto"), lo_rowEC("Moneda"), "SOL", po_dtbDocsClien.Rows(0)("FechaDoc"))
                ld_impAplME = dbl_obtImportesPorMoneda(lo_rowEC("Monto"), lo_rowEC("Moneda"), "USD", po_dtbDocsClien.Rows(0)("FechaDoc"))

                ' Se acumula los importes aplicados
                ld_sumImpApl = ld_sumImpApl + ld_impApl
                ld_sumIimpAplME = ld_sumIimpAplME + ld_impAplME

            Next

            ' Se declara dos variables para los importes aplicados en moneda local y extranjera
            Dim ld_impApl2 As Double = 0.0
            Dim ld_impAplME2 As Double = 0.0

            ' Se declara dos variables que acumulen la suma de los importes en moneda local y extranjera 
            Dim ld_sumImpApl2 As Double = 0.0
            Dim ld_sumIimpAplME2 As Double = 0.0

            ' Se declara una lista para las filas a ingresar en el dataTable de la planilla
            Dim lo_lstRows As New List(Of DataRow)

            ' Se declara una variable entera para contar los registros del dataTable de documentos del cliente
            Dim li_contDocs As Integer = 0

            ' Se recorre las filas seleccionadas del grid del Estado de Cuenta
            For Each lo_rowEC As DataRow In po_dtbEC.Rows

                ' Se incrementa el contador
                li_contDocs = li_contDocs + 1

                ' Se verifica si se excede el saldo total del documento
                If ls_monedaDoc = entComun.str_obtMonLocal Then
                    If ld_saldoDoc <= ld_sumImpApl2 Then
                        MsgBox("La seleccion de registros del Estado de Cuenta excede la cantidad de Pagos Recibidos que es posible realizar en SAP al documento del cliente seleccionado.")

                        Exit Sub
                    End If
                Else
                    If ld_saldoDoc <= ld_sumIimpAplME2 Then
                        MsgBox("La seleccion de registros del Estado de Cuenta excede la cantidad de Pagos Recibidos que es posible realizar en SAP al documento del cliente seleccionado.")

                        ' Se finaliza el metodo
                        Exit Sub

                    End If
                End If

                ' Se valida la adicion de las filas de la planilla
                If bol_valUnoAUno(po_dtbDocsClien.Rows(0), lo_rowEC) = False Then

                    ' Se finaliza el metodo
                    Exit Sub

                End If

                ' Se obtiene los importes en moneda local y extranjera              
                ld_impApl2 = dbl_obtImportesPorMoneda(lo_rowEC("Monto"), lo_rowEC("Moneda"), "SOL", po_dtbDocsClien.Rows(0)("FechaDoc"))
                ld_impAplME2 = dbl_obtImportesPorMoneda(lo_rowEC("Monto"), lo_rowEC("Moneda"), "USD", po_dtbDocsClien.Rows(0)("FechaDoc"))

                ' Se añade el registro a la grilla de PLanilla
                Dim lo_row As DataRow = lo_dtb.NewRow
                lo_row.BeginEdit()
                lo_row("id") = obj_obtValorControl(ctr_obtenerControl("id", o_form.Controls))
                lo_row("lineaNumAsg") = int_obtLineaNumAsg()
                lo_row("Codigo") = po_dtbDocsClien.Rows(0)("Codigo")
                lo_row("Nombre") = po_dtbDocsClien.Rows(0)("Nombre")
                lo_row("Id_Doc") = po_dtbDocsClien.Rows(0)("Id_Doc")
                lo_row("Referencia") = po_dtbDocsClien.Rows(0)("Referencia")
                lo_row("Tipo_Doc") = po_dtbDocsClien.Rows(0)("TransType")
                lo_row("DocLine") = po_dtbDocsClien.Rows(0)("Line_ID")
                lo_row("FechaDoc") = po_dtbDocsClien.Rows(0)("FechaDoc")
                lo_row("Comentario") = po_dtbDocsClien.Rows(0)("Comentario")
                lo_row("MonedaDoc") = po_dtbDocsClien.Rows(0)("MonedaDoc")
                lo_row("TipoCambioDoc") = po_dtbDocsClien.Rows(0)("TipoCambioDoc")

                lo_row("asientoajustoT") = "N"

                lo_row("Total") = po_dtbDocsClien.Rows(0)("Total")
                lo_row("Saldo") = po_dtbDocsClien.Rows(0)("Saldo")
                lo_row("Imp_Aplicado") = ld_impApl2
                lo_row("Imp_AplicadoME") = ld_impAplME2
                lo_row("MonedaPag") = lo_rowEC("Moneda")
                lo_row("MontoOp") = lo_rowEC("Monto")
                lo_row("Tipo_Cambio") = dbl_obtTipoCambio("USD", CDate(po_dtbDocsClien.Rows(0)("FechaDoc")).ToString("yyyyMMdd"))
                lo_row("Cuenta") = lo_rowEC("Cta_Contable")
                Dim FechaDocumento As DevExpress.XtraEditors.DateEdit = ctr_obtenerControl("FechaDocumento", o_form.Controls)
                lo_row("FechaPago") = If(FechaDocumento.EditValue = Nothing, lo_rowEC("Fecha"), FechaDocumento.EditValue)
                lo_row("FechaDeposito") = po_dtbEC.Rows(0)("Fecha")
                lo_row("Nro_Operacion") = lo_rowEC("Operacion")
                lo_row("idEC") = lo_rowEC("id")
                lo_row("tipoAsg") = li_tipoAsg
                lo_row("tipoEC") = lo_rowEC("TipoOp")
                lo_row("difTC") = "N"
                lo_row("FechaCrea") = Now.Date
                lo_row("Ruc") = po_dtbDocsClien.Rows(0)("Ruc").ToString
                lo_row("CodBien") = po_dtbDocsClien.Rows(0)("CodBien").ToString

                ' Se asigna el comentario de la linea de la planilla de acuerdo a la comparación del saldo del documento con la suma total de registros del Estado de Cuenta
                lo_row("ComentarioPl") = str_obtComentOper(ls_monedaDoc, ld_saldoDoc, ld_sumImpApl, ld_sumIimpAplME)

                ' Se verifica si es el ultimo registro seleccionado
                If li_contDocs = po_dtbEC.Rows.Count Then

                    ' Se asigna los saldos a favor del cliente
                    lo_row("SaldoFavor") = str_obtSaldoAFavor(ls_monedaLocal, ld_saldoDocLocal, ld_saldoDocME, ld_sumImpApl, ld_sumIimpAplME)
                    lo_row("SaldoFavorME") = str_obtSaldoAFavor("USD", ld_saldoDocLocal, ld_saldoDocME, ld_sumImpApl, ld_sumIimpAplME)

                Else

                    ' Se asigna los saldos a favor del cliente
                    lo_row("SaldoFavor") = 0.0
                    lo_row("SaldoFavorME") = 0.0

                End If
                lo_row.EndEdit()

                ' Se acumula los importes aplicados
                ld_sumImpApl2 = ld_sumImpApl2 + ld_impApl2
                ld_sumIimpAplME2 = ld_sumIimpAplME2 + ld_impAplME2

                ' Se añade la fila a la lista
                lo_lstRows.Add(lo_row)

            Next

            ' Se añade las filas de la lista al dataTable de la planilla
            sub_adcFilaMuchosAMuchos(lo_lstRows, lo_dtb, lo_uctDocsClient, lo_uctEC, lo_uctPlanilla)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    'Cezar
    Private Sub sub_adicionMuchosAUno(ByVal po_dtbDocsClien As DataTable, ByVal po_dtbEC As DataTable)
        Try

            ' Seobtienen las grillas superiores
            Dim lo_uctDocsClient As uct_gridConBusqueda = ctr_obtenerControl("ECClientes", o_form.Controls)
            Dim lo_uctEC As uct_gridConBusqueda = ctr_obtenerControl("ECBanco", o_form.Controls)

            ' Se obtiene la grilla de planilla
            Dim lo_uctPlanilla As uct_gridConBusqueda = ctr_obtenerControl("gmi_plaPagosDetalle", o_form.Controls)
            Dim lo_dtb As DataTable = CType(obj_obtValorControl(lo_uctPlanilla), DataTable)

            ' Se obtiene el tipo de planilla
            Dim ls_tipoPla As String = str_obtTipoPlanilla()

            ' Se declara dos variables para los saldos en moneda local y extranjera
            Dim ld_saldo As Double = 0.0
            Dim ld_saldoME As Double = 0.0

            ' Se declara dos variables que acumulen la suma de los importes en moneda local y extranjera 
            Dim ld_sumSaldo As Double = 0.0
            Dim ld_sumSaldoME As Double = 0.0

            ' Se obtiene la moneda del documento
            Dim ls_monedaPag As String = CStr(po_dtbEC.Rows(0)("Moneda"))

            ' Se declara una variable para obtener la moneda local de la compañia SAP
            Dim ls_monedaLocal As String = entComun.str_obtMonLocal

            ' Se obtiene la fila del dataTable del Estado de cuenta
            'Dim lo_rowEC As DataRow = po_dtbEC.Rows(0)
            Dim lo_rowEC As DataRow = po_dtbEC.Rows(0)
            Dim lo_rowCD As DataRow = po_dtbDocsClien.Rows(0)

            ' Se declara una variable para obtener el Monto de la operación del Estado de Cuenta
            Dim ld_montoEC As Double
            Dim ld_montoECME As Double

            ' Se verifica el tipo de planilla           
            ld_montoEC = dbl_obtImportesPorMoneda(CDbl(lo_rowEC("Monto")), lo_rowEC("Moneda"), "SOL", lo_rowCD("FechaDoc"))
            ld_montoECME = dbl_obtImportesPorMoneda(CDbl(lo_rowEC("Monto")), lo_rowEC("Moneda"), "USD", lo_rowCD("FechaDoc"))

            'ld_montoEC = dbl_obtImportesPorMoneda(CDbl(lo_rowEC("Monto")), lo_rowEC("Moneda"), "SOL", lo_rowEC("FechaDoc"))
            'ld_montoECME = dbl_obtImportesPorMoneda(CDbl(lo_rowEC("Monto")), lo_rowEC("Moneda"), "USD", lo_rowEC("FechaDoc"))

            ' Se obtiene el total de los documentos seleccionados
            For Each lo_rowDoc As DataRow In po_dtbDocsClien.Rows

                ' Se obtiene los importes en moneda local y extranjera
                ld_saldo = dbl_obtImportesPorMoneda(lo_rowDoc("Saldo"), lo_rowDoc("MonedaDoc"), "SOL", lo_rowCD("FechaDoc"))
                ld_saldoME = dbl_obtImportesPorMoneda(lo_rowDoc("Saldo"), lo_rowDoc("MonedaDoc"), "USD", lo_rowCD("FechaDoc"))

                ' Se acumula los importes aplicados
                ld_sumSaldo = ld_sumSaldo + ld_saldo
                ld_sumSaldoME = ld_sumSaldoME + ld_saldoME

            Next

            ' Se declara dos variables para los saldos en moneda local y extranjera
            Dim ld_saldo2 As Double = 0.0
            Dim ld_saldoME2 As Double = 0.0

            ' Se declara dos variables que acumulen la suma de los saldos en moneda local y extranjera 
            Dim ld_sumSaldo2 As Double = 0.0
            Dim ld_sumSaldoME2 As Double = 0.0

            ' Se declara una lista para las filas a ingresar en el dataTable de la planilla
            Dim lo_lstRows As New List(Of DataRow)

            ' Se declara una variable entera para contar los registros del dataTable de documentos del cliente
            Dim li_contDocs As Integer = 0

            ' Se recorre las filas seleccionadas del grid de documentos del cliente
            For Each lo_rowDoc As DataRow In po_dtbDocsClien.Rows

                ' Se incrementa el contador
                li_contDocs = li_contDocs + 1

                ' Se verifica si se excede el monto de la operacion
                If ls_monedaPag = entComun.str_obtMonLocal Then
                    If (ld_montoEC - ld_sumSaldo2) <= 0.0 Then
                        MsgBox("La cantidad de documentos seleccionados excede la cantidad posible de documentos a los que se les puede aplicar el monto de la operación del Estado de Cuenta seleccionada.")
                        ' Se finaliza el metodo
                        Exit Sub
                    End If
                Else
                    If (ld_montoECME - ld_sumSaldoME2) <= 0.0 Then
                        MsgBox("La cantidad de documentos seleccionados excede la cantidad posible de documentos a los que se les puede aplicar el monto de la operación del Estado de Cuenta seleccionada.")
                        ' Se finaliza el metodo
                        Exit Sub

                    End If
                End If

                ' Se valida la seleccion de las dos grillas superiores
                If bol_valMuchosAUno(po_dtbDocsClien, po_dtbEC) = False Then

                    ' Se finaliza el metodo
                    Exit Sub

                End If

                ' Se obtiene los saldos en moneda local y extranjera
                ld_saldo2 = dbl_obtImportesPorMoneda(lo_rowDoc("Saldo"), lo_rowDoc("MonedaDoc"), "SOL", lo_rowCD("FechaDoc"))
                ld_saldoME2 = dbl_obtImportesPorMoneda(lo_rowDoc("Saldo"), lo_rowDoc("MonedaDoc"), "USD", lo_rowCD("FechaDoc"))

                ' Se añade el registro a la grilla de PLanilla
                Dim lo_row As DataRow = lo_dtb.NewRow
                lo_row.BeginEdit()

                ' Se asigna el importe aplicado de acuerdo a la moneda 
                If ls_monedaPag = entComun.str_obtMonLocal Then
                    If ld_montoEC - ld_sumSaldo2 - ld_saldo2 < 0.0 Then
                        lo_row("Imp_Aplicado") = ld_montoEC - ld_sumSaldo2
                        lo_row("Imp_AplicadoME") = ld_montoECME - ld_sumSaldoME2
                        'lo_row("ComentarioPl") = "Amortizacion"
                    Else
                        If po_dtbDocsClien.Rows.Count = li_contDocs Then
                            lo_row("Imp_Aplicado") = ld_montoEC - ld_sumSaldo2
                            lo_row("Imp_AplicadoME") = ld_montoECME - ld_sumSaldoME2
                            'lo_row("ComentarioPl") = "Cancelacion"
                        Else
                            lo_row("Imp_Aplicado") = ld_saldo2
                            lo_row("Imp_AplicadoME") = ld_saldoME2
                            'lo_row("ComentarioPl") = "Cancelacion"
                        End If
                    End If
                Else
                    If ld_montoECME - ld_sumSaldoME2 - ld_saldoME2 < 0.0 Then
                        lo_row("Imp_Aplicado") = ld_montoEC - ld_sumSaldo2
                        lo_row("Imp_AplicadoME") = ld_montoECME - ld_sumSaldoME2
                        'lo_row("ComentarioPl") = "Amortizacion"
                    Else
                        If po_dtbDocsClien.Rows.Count = li_contDocs Then
                            lo_row("Imp_Aplicado") = ld_montoEC - ld_sumSaldo2
                            lo_row("Imp_AplicadoME") = ld_montoECME - ld_sumSaldoME2
                            'lo_row("ComentarioPl") = "Cancelacion"
                        Else
                            lo_row("Imp_Aplicado") = ld_saldo2
                            lo_row("Imp_AplicadoME") = ld_saldoME2
                            'lo_row("ComentarioPl") = "Cancelacion"
                        End If
                    End If
                End If

                ' Se redondea los importes a la cantidad de decimales especificada en la configuracion de la base de datos de SAP Business One
                lo_row("Imp_Aplicado") = dbl_redondearImporte(lo_row("Imp_Aplicado"))
                lo_row("Imp_AplicadoME") = dbl_redondearImporte(lo_row("Imp_AplicadoME"))

                lo_row("id") = obj_obtValorControl(ctr_obtenerControl("id", o_form.Controls))
                lo_row("lineaNumAsg") = int_obtLineaNumAsg()
                lo_row("Codigo") = lo_rowDoc("Codigo")
                lo_row("Nombre") = lo_rowDoc("Nombre")
                lo_row("Id_Doc") = lo_rowDoc("Id_Doc")
                lo_row("Referencia") = lo_rowDoc("Referencia")
                lo_row("Tipo_Doc") = lo_rowDoc("TransType")
                lo_row("DocLine") = lo_rowDoc("Line_ID")
                lo_row("FechaDoc") = lo_rowDoc("FechaDoc")
                lo_row("Comentario") = lo_rowDoc("Comentario")
                lo_row("MonedaDoc") = lo_rowDoc("MonedaDoc")
                lo_row("TipoCambioDoc") = lo_rowDoc("TipoCambioDoc")
                lo_row("Total") = lo_rowDoc("Total")
                lo_row("Saldo") = lo_rowDoc("Saldo")
                lo_row("MonedaPag") = lo_rowEC("Moneda")
                lo_row("MontoOp") = lo_rowEC("Monto")

                ''AGREGADO JSOLIS
                'lo_dtbSelsEC.Rows(0)("asientoajustoT") = "N"
                lo_row("asientoajustoT") = "N"

                'lo_row("Tipo_Cambio") = dbl_obtTipoCambio("USD", CDate(lo_rowEC("Fecha")).ToString("yyyyMMdd"))
                lo_row("Tipo_Cambio") = dbl_obtTipoCambio("USD", CDate(lo_rowCD("FechaDoc")).ToString("yyyyMMdd"))
                lo_row("Cuenta") = lo_rowEC("Cta_Contable")
                Dim FechaDocumento As DevExpress.XtraEditors.DateEdit = ctr_obtenerControl("FechaDocumento", o_form.Controls)
                lo_row("FechaPago") = If(FechaDocumento.EditValue = Nothing, lo_rowEC("Fecha"), FechaDocumento.EditValue)
                lo_row("FechaDeposito") = po_dtbEC.Rows(0)("Fecha")
                lo_row("Nro_Operacion") = lo_rowEC("Operacion")
                lo_row("idEC") = lo_rowEC("id")
                lo_row("tipoAsg") = 2
                lo_row("tipoEC") = lo_rowEC("TipoOp")
                lo_row("difTC") = "N"
                lo_row("FechaCrea") = Now.Date
                lo_row("Ruc") = po_dtbDocsClien.Rows(0)("Ruc").ToString
                lo_row("CodBien") = po_dtbDocsClien.Rows(0)("CodBien").ToString

                ' Se asigna el comentario de la linea de la planilla de acuerdo a la comparación del saldo del documento con la suma total de registros del Estado de Cuenta
                lo_row("ComentarioPl") = str_obtComentOper(lo_row("MonedaDoc"), lo_row("Saldo"), lo_row("Imp_Aplicado"), lo_row("Imp_AplicadoME"))

                ' Se asigna los saldos a favor del cliente
                lo_row("SaldoFavor") = str_obtSaldoAFavor(ls_monedaLocal, ld_saldo2, ld_saldoME2, lo_row("Imp_Aplicado"), lo_row("Imp_AplicadoME"))
                lo_row("SaldoFavorME") = str_obtSaldoAFavor("USD", ld_saldo2, ld_saldoME2, lo_row("Imp_Aplicado"), lo_row("Imp_AplicadoME"))




                lo_row.EndEdit()

                ' Se acumula los importes aplicados
                ld_sumSaldo2 = ld_sumSaldo2 + ld_saldo2
                ld_sumSaldoME2 = ld_sumSaldoME2 + ld_saldoME2

                ' Se añade la fila a la lista
                lo_lstRows.Add(lo_row)

            Next

            ' Se añade las filas de la lista al dataTable de la planilla
            sub_adcFilaMuchosAMuchos(lo_lstRows, lo_dtb, lo_uctDocsClient, lo_uctEC, lo_uctPlanilla)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    ' Se obtiene el importe aplicado en ambas monedas
    Private Function dbl_obtImportesPorMoneda(ByVal pd_importe As Double, ByVal ps_monImporte As String, ByVal ps_monConver As String, ByVal pd_fechaTC As Date) As Double
        Try

            ' Se obtiene la moneda local
            Dim ls_monLocal As String = entComun.str_obtMonLocal

            ' Se obtiene la cantidad de decimales configurados para el importe en la base de datos actual de SAP Business One
            Dim li_decimales As Integer = int_obtDecimalesImp()

            ' Se declara una variable para el tipo de cambio
            Dim ld_TCMonImpor As Double = dbl_obtTipoCambio(ps_monImporte, pd_fechaTC.ToString("yyyyMMdd"))
            Dim ld_TCMonConver As Double = dbl_obtTipoCambio(ps_monConver, pd_fechaTC.ToString("yyyyMMdd"))

            ' Se declara una variable para obtener el importe
            Dim ld_importe As Double = 0.0

            ' Se verificalas monedas
            If ps_monImporte = ps_monConver Then
                Return Math.Round(pd_importe, li_decimales)
            ElseIf ps_monImporte = ls_monLocal Then
                Return Math.Round(pd_importe / ld_TCMonConver, li_decimales)
            ElseIf ps_monImporte <> ls_monLocal Then
                Return Math.Round(pd_importe * ld_TCMonImpor, li_decimales)
            Else
                Return 0.0
            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return -1
        End Try
    End Function

    ' Se obtiene el comentario sobre Cancelacion o Amortizacion del registro
    Private Function str_obtComentOper(ByVal ps_monDoc As String, ByVal pd_impDoc As Double, ByVal pd_impPago As Double, ByVal pd_impPagoME As Double) As String
        Try

            ' Se verifica la moneda del documento
            If ps_monDoc = entComun.str_obtMonLocal Then
                If pd_impDoc > pd_impPago Then
                    Return "Amortizacion"
                Else
                    Return "Cancelacion"
                End If
            Else
                If pd_impDoc > pd_impPagoME Then
                    Return "Amortizacion"
                Else
                    Return "Cancelacion"
                End If
            End If

            ' Si ocurrio un evento inesperado
            Return ""

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return ""
        End Try
    End Function

    ' Se obtiene el saldo a favor del cliente
    Private Function str_obtSaldoAFavor(ByVal ps_moneda As String, ByVal pd_impDoc As Double, ByVal pd_impDocME As Double, ByVal pd_impPago As Double, ByVal pd_impPagoME As Double) As String
        Try

            ' Se verifica la moneda del documento
            If ps_moneda = entComun.str_obtMonLocal Then
                If pd_impPago - pd_impDoc > 0.0 Then
                    Return dbl_redondearImporte(pd_impPago - pd_impDoc)
                Else
                    Return 0.0
                End If
            Else
                If pd_impPagoME - pd_impDocME > 0.0 Then
                    Return dbl_redondearImporte(pd_impPagoME - pd_impDocME)
                Else
                    Return 0.0
                End If
            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return ""
        End Try
    End Function

    ' Se añade la fila al grid de planilla
    Private Sub sub_adcFilaUnoAUno(ByVal po_row As DataRow, ByVal po_dtb As DataTable, ByVal po_uctDocs As uct_gridConBusqueda, ByVal po_uctEC As uct_gridConBusqueda, ByVal po_uctPla As uct_gridConBusqueda)
        Try

            ' Se añade la fila al grid Planilla
            po_dtb.Rows.Add(po_row)

            ' Se eliminan los registros seleccionados de las grillas superiores                
            po_uctDocs.sub_eliminarFilasSelec()
            po_uctEC.sub_eliminarFilasSelec()

            ' Se incrementa el enumerador de lineas por asignacion
            i_lineaNumAsg = i_lineaNumAsg + 1

            ' Se ejecuta la accion de autoguardado al añadir las filas
            sub_autoGuardar()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_adcFilaMuchosAMuchos(ByVal po_lstRows As List(Of DataRow), ByVal po_dtb As DataTable, ByVal po_uctDocs As uct_gridConBusqueda, ByVal po_uctEC As uct_gridConBusqueda, ByVal po_uctPla As uct_gridConBusqueda)
        Try

            ' Se añade las filas de la lista al dataTable de la planilla
            For Each lo_dRow As DataRow In po_lstRows
                po_dtb.Rows.Add(lo_dRow)
            Next

            ' Se eliminan los registros seleccionados de las grillas superiores                
            po_uctDocs.sub_eliminarFilasSelec()
            po_uctEC.sub_eliminarFilasSelec()

            ' Se incrementa el enumerador de lineas por asignacion
            i_lineaNumAsg = i_lineaNumAsg + 1

            ' Se ejecuta la accion de autoguardado al añadir las filas
            sub_autoGuardar()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    ' Se obtiene un nuevo Número de Asignación
    Private Function int_obtLineaNumAsg() As Integer
        Try

            ' Se declara una variable para obtener el Id de la linea
            Dim li_LineaNumAsig As Integer = -1

            ' Se obtiene la grilla de planilla
            Dim lo_uctPlanilla As uct_gridConBusqueda = ctr_obtenerControl("gmi_plaPagosDetalle", o_form.Controls)
            Dim lo_dtb As DataTable = CType(obj_obtValorControl(lo_uctPlanilla), DataTable)

            ' Se recorre las filas del dataTable de planilla
            For Each lo_row As DataRow In lo_dtb.Rows
                ' Se obtiene el numero mayor de la columna 
                If li_LineaNumAsig < lo_row("lineaNumAsg") Then
                    li_LineaNumAsig = lo_row("lineaNumAsg")
                End If
            Next

            ' Se retorna la variable entera
            Return li_LineaNumAsig + 1

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return 0
        End Try
    End Function

    ' Se reenumera los Numeros de Asignacion del detalle
    Private Sub sub_reenumerarLineaNumAsg()
        Try

            ' Se obtiene la grilla de planilla
            Dim lo_uctPlanilla As uct_gridConBusqueda = ctr_obtenerControl("gmi_plaPagosDetalle", o_form.Controls)
            Dim lo_dtb As DataTable = CType(obj_obtValorControl(lo_uctPlanilla), DataTable)

            ' Se declara una variable para la nueva enumeracion
            Dim li_numero As Integer = -1

            ' Se declara una variable para obtener el valor actual de la columna+
            Dim li_lineaNumAsg As Integer = -1

            ' Se recorre las filas del dataTable de planilla
            For Each lo_row As DataRow In lo_dtb.Rows

                ' Se verifica si la fila no fue borrada
                If lo_row.RowState <> DataRowState.Deleted Then

                    ' Se verifica si la variable y el valor de la fila son diferentes
                    If li_lineaNumAsg <> lo_row("lineaNumAsg") Then

                        ' Se incrementa en uno el nuevo enumerador
                        li_numero = li_numero + 1

                        ' Se obtiene el valor de la columna
                        li_lineaNumAsg = lo_row("lineaNumAsg")

                        ' Se asigna el nuevo enumerador
                        lo_row("lineaNumAsg") = li_numero

                    Else

                        ' Se asigna el nuevo enumerador
                        lo_row("lineaNumAsg") = li_numero

                    End If

                End If

            Next

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    ' Acciones de los ComboBox del formulario asociado a esta clase de negocio
    Public Overrides Sub sub_accionCombo(ByVal po_cbo As System.Windows.Forms.ComboBox)
        Try

            ' Se limpia el grid de detalle de la planilla y se vuelve a cargar el grid de operaciones bancarias de acuerdo al tipo de planilla
            sub_limpiarDetYCargarEC(po_cbo)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    ' Se limpia el grid de detalle de la planilla y se vuelve a cargar el grid de operaciones bancarias de acuerdo al tipo de planilla
    Private Sub sub_limpiarDetYCargarEC(ByVal po_cbo As System.Windows.Forms.ComboBox)
        Try

            ' Se verifica el modo del formulario
            If o_form.Modo <> enm_modoForm.ANADIR Then
                Exit Sub
            End If

            ' Se verifica el tag del combo
            If po_cbo.Tag = "tipoPla" Then

                ' Se obtiene el control del detalle de la planilla
                Dim lo_uctPlanilla As Control = ctr_obtenerControl("gmi_plaPagosDetalle", o_form.Controls)

                ' Se verifica si se obtuvo el control
                If lo_uctPlanilla Is Nothing Then

                    ' No se pudo obtener el control con el TAG especificado
                    sub_mostrarMensaje("No se pudo obtener el control con el TAG <gmi_plaPagosDetalle>", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                    'MsgBox("No se pudo obtener el control con el TAG <gmi_plaPagosDetalle>")

                    ' Se finaliza el metodo
                    Exit Sub

                End If

                ' Se limpia el grid del detalle de la planilla
                CType(lo_uctPlanilla, uct_gridConBusqueda).sub_limpiar()

                ' Se recarga el grid de operaciones bancarias
                sub_cargarUGCEstadoCuenta()
                sub_cargarUGCClientes()

            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub
    Private Sub sub_manejarCargaPosBanco(ByVal sender As Object, ByVal e As EventArgs)
        Try

            ' Se indica el filtro correspondiente al listado de seleccion del parametro de clientes
            Dim lo_cboPosBanco As Control = ctr_obtenerControl("PosBanco", o_form.Controls)
            Dim lo_cboCtasBanco As Control = ctr_obtenerControl("CtasBanco", o_form.Controls)

            Dim lo_dtb As DataTable = entPlanilla.dtb_obtNroPosBanco(CType(lo_cboCtasBanco, ComboBox).SelectedValue)
            ' Se asigna el dataTable como DataSource del comboBox
            CType(lo_cboPosBanco, ComboBox).DataSource = lo_dtb
            CType(lo_cboPosBanco, ComboBox).ValueMember = "Concepto"
            CType(lo_cboPosBanco, ComboBox).DisplayMember = "NroSap"

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub
    ' Se maneja la habilitacion de la columna Imp_aplicado de acuerdo a la moneda del depósito
    Private Sub sub_manejarColImpApl(ByVal sender As Object, ByVal e As EventArgs)
        Try

            ' Se obtiene la moneda del depósito
            ' - Se obtiene el gridView de la planilla
            Dim lo_gridPll As Control = ctr_obtControl("gmi_plaPagosDetalle")

            ' - Se verifica si se obtuvo el control
            If lo_gridPll Is Nothing Then
                Exit Sub
            End If

            ' - Se obtiene la columna seleccionada
            Dim lo_gridCol As GridColumn = CType(lo_gridPll, uct_gridConBusqueda).col_obtColumnaSeleccionada

            ' - Se verifica si se obtuvo la columna
            If lo_gridCol Is Nothing Then
                sub_mostrarMensaje("Ocurrio un error al obtener la columna seleccionada.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            ' Se verifica si la columna del evento corresponde a los importes aplicados
            If Not lo_gridCol.Name.ToLower.Contains("imp_aplicado") Then
                Exit Sub
            End If

            ' - Se obtiene la fila seleccionada
            Dim lo_row As DataRow = CType(lo_gridPll, uct_gridConBusqueda).dtr_obtDataRowFilaSeleccionada

            ' - Se verifica si se obtuvo el dataRow
            If lo_row Is Nothing Then
                sub_mostrarMensaje("Ocurrio un error al obtener la fila seleccionada.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            ' - Se verifica si la asignacion actual es de muchos a uno
            Dim li_tipoAsg As Integer = lo_row("tipoAsg")
            If li_tipoAsg <> 2 Then

                ' Se deshabilita la columna
                CType(e, System.ComponentModel.CancelEventArgs).Cancel = True

                ' Se finaliza el metodo
                Exit Sub

            End If

            ' - Se obtiene la moneda del depósito
            Dim ls_monedaDep As String = lo_row("MonedaPag")

            ' - Se verifica si se obtuvo la moneda del deposito
            If ls_monedaDep Is Nothing Then
                sub_mostrarMensaje("Ocurrio un error al obtener la moneda del depósito.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            ElseIf ls_monedaDep.Trim = "" Then
                sub_mostrarMensaje("Ocurrio un error al obtener la moneda del depósito.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            ' Se habilita o deshabilita la columna "Imp_Aplicado" de acuerdo a la moneda obtenida
            If ls_monedaDep = str_obtMonLocal() Then

                ' Se verifica si el nombre de la columna corresponde al importe en moneda extrangera
                If lo_gridCol.Name.ToLower.Contains("imp_aplicadome") Then

                    ' Se deshabilita la columna que corresponde a la moneda extrangera
                    CType(e, System.ComponentModel.CancelEventArgs).Cancel = True

                End If

            Else

                ' Se verifica si el nombre de la columna corresponde al importe en moneda local
                If Not lo_gridCol.Name.ToLower.Contains("imp_aplicadome") Then

                    ' Se deshabilita la columna que corresponde a la moneda local
                    CType(e, System.ComponentModel.CancelEventArgs).Cancel = True

                End If

            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#Region "Validaciones"

#Region "val_general"

    Private Overloads Function bol_valAlAnadir() As Boolean
        Try

            ' Se declara una variable para el resultado
            Dim lb_resultado As Boolean = True

            ' Se valida los documentos repetidos en otras planillas abiertas
            lb_resultado = bol_valDocsECEnPllAbiertasAlAnadir()

            ' Se verifica si la validacion fue correcta
            If lb_resultado = False Then
                Return False
            End If

            ' Se valida los importes aplicados de las asignaciones Muchos a Uno
            lb_resultado = bol_valMuchosAUnoAlGrabar()

            ' Se verifica si la validacion fue correcta
            If lb_resultado = False Then
                Return False
            End If

            ' Si todas las validaciones fueron correctas, se retorna TRUE
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Protected Overrides Function bol_valAlAnadir(pb_antesAccion As Boolean) As Boolean
        Try

            ' Se realiza las validaciones correspondientes antes de añadir
            If pb_antesAccion = True Then
                Return bol_valAlAnadir()
            End If

            ' Si las validaciones fueron correctas, se retorna TRUE
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Protected Overrides Function bol_valAlActualizar(pb_antesAccion As Boolean) As Boolean
        Try

            ' Se realiza las validaciones correspondientes antes de añadir
            If pb_antesAccion = True Then
                Return bol_valAlAnadir()
            End If

            ' Si las validaciones fueron correctas, se retorna TRUE
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

#End Region

#Region "val_planillasAbiertas"

    Private Function bol_validarIdsEnPlanillas(ByVal pi_idDoc As Integer, ByVal pi_idEC As Integer) As Boolean
        Try

            ' Se obtiene el control con el campo de id
            Dim lo_control As Control = ctr_obtenerControl("id", o_form.Controls)

            ' Se verifica si se obtuvo el control
            If lo_control Is Nothing Then
                sub_mostrarMensaje("No se pudo obtener el control en donde se encuentra el Id de la planilla. No se puede realizar la validacion.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Return False
            End If

            ' Se obtiene el valor del id
            Dim li_id As Integer = obj_obtValorControl(lo_control)

            ' Se verifica si el id del Estado de Cuenta existe en otras planillas que se encuentran abiertas o cerradas
            Dim li_indicador As Integer = entPlanilla.int_verIdECenPlanillas(li_id, pi_idEC)

            ' Se verifica el resultado de la busqueda
            If li_indicador = -1 Then
                sub_mostrarMensaje("Ocurrio un error al realizar la busqueda del registro del Estado de Cuenta en otras planillas. No se puede realizar la validacion.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Return False
            End If

            ' Se verifica si el id del Estado de Cuenta existe en otras planillas abiertas o cerradas
            If li_indicador > 0 Then
                MsgBox("El registro " & pi_idEC & " del Estado de Cuenta ya existe en la planilla " & li_indicador & ", la cual se encuentra Abierta o ya fue procesada (Cerrada).")
                Return False
            End If

            ' Se verifica si el Id del documento existe en otra planilla que se encuentre Abierta
            li_indicador = entPlanilla.int_verIdDocEnPlanillas(li_id, pi_idDoc)

            ' Se verifica el resultado de la busqueda
            If li_indicador = -1 Then
                sub_mostrarMensaje("Ocurrio un error al realizar la busqueda del documento en otras planillas. No se puede realizar la validacion.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Return False
            End If

            ' Se verifica si el id del Estado de Cuenta existe en otras planillas abiertas o cerradas
            If li_indicador > 0 Then
                MsgBox("El documento con Id " & pi_idDoc & " ya existe en la planilla " & li_indicador & ", la cual se encuentra Abierta.")
                Return False
            End If

            ' Si la validacion fue correcta, se retorna TRUE
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Function bol_valRegEnPllAbiertas(po_dtbSelsClient As DataTable, po_dtbSelsEC As DataTable) As Boolean
        Try

            ' Se verifica el modo del formulario para identiicar si se crea una nueva planilla o si se actualiza una existente
            Dim li_idPll As Integer = -1
            If o_form.Modo <> enm_modoForm.ANADIR Then

                ' Se obtiene el control del id de la planilla
                Dim lo_idPll As Control = ctr_obtControl("id")

                ' Se verifica si se obtuvo el control
                If lo_idPll Is Nothing Then
                    Return False
                End If

                ' Se obtiene el id de la planilla
                li_idPll = obj_obtValorControl(lo_idPll)

                ' Se verifica si se obtuvo el id de la planilla 
                If li_idPll < 1 Then
                    sub_mostrarMensaje("Ocurrio un error al obtener el id de la planilla.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                    Return False
                End If

            End If

            ' Se recorre los registros del dataTable de los clientes seleccionados
            For Each lo_rowDocs As DataRow In po_dtbSelsClient.Rows

                ' Se obtiene el id del documento
                Dim li_idDoc As Integer = lo_rowDocs("Id_Doc")
                Dim li_TransType As Integer = lo_rowDocs("TransType")
                Dim li_docLine As Integer = lo_rowDocs("Line_ID")

                'Dim li_asientoajustoT As Integer = lo_rowDocs("asientoajustoT")

                '' asientoajustot 
                'Dim asientoajustot As String = lo_row("asientoajustoT")

                ' Se verifica si el registro existe en otra planilla abierta
                'Dim li_verifPll As Integer = entPlanilla.int_verExitDocEnPllAbierta2(li_idDoc, li_TransType, li_docLine, li_idPll, li_asientoajustoT)
                Dim li_verifPll As Integer = entPlanilla.int_verExitDocEnPllAbierta(li_idDoc, li_TransType, li_docLine, li_idPll)
                If li_verifPll > 0 Then
                    sub_mostrarMensaje("El documento " & li_idDoc.ToString & " de tipo " & li_TransType & " está registrado en la planilla " & li_verifPll.ToString & ", la cual se encuentra abierta.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_val)
                    Return False
                End If

            Next

            For Each lo_rowEC As DataRow In po_dtbSelsEC.Rows

                ' Se obtiene el id del registro del estado de cuenta
                Dim li_idEC As Integer = lo_rowEC("id")

                ' Se verifica si el registro existe en otra planilla abierta
                Dim li_verifPll As Integer = entPlanilla.int_verExitECEnPllAbierta(li_idEC, li_idPll)

                If li_verifPll > 0 Then
                    sub_mostrarMensaje("El depósito " & li_idEC.ToString & " está registrado en la planilla " & li_verifPll.ToString & ", la cual se encuentra abierta.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_val)
                    Return False
                End If

            Next

            ' Si todas las validaciones fueron correctas, se retorna TRUE
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Function bol_valDocsECEnPllAbiertasAlAnadir() As Boolean
        Try

            ' Se obtiene el dataTable del detalle de la planilla
            Dim lo_pllDet As Control = ctr_obtControl("gmi_plaPagosDetalle")

            ' Se verifica si se obtuvo el control
            If lo_pllDet Is Nothing Then
                Return False
            End If

            ' Se obtiene el dataTable desde la grilla obtenida
            Dim lo_dtb As DataTable = CType(lo_pllDet, uct_gridConBusqueda).DataSource

            ' Se verifica si se obtuvo el dataTable
            If lo_dtb Is Nothing Then
                sub_mostrarMensaje("No se pudo obtener el dataTable del detalle de la planilla", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Return False
            End If

            ' Se verifica el modo del formulario para identiicar si se crea una nueva planilla o si se actualiza una existente
            Dim li_idPll As Integer = -1
            If o_form.Modo <> enm_modoForm.ANADIR Then

                ' Se obtiene el control del id de la planilla
                Dim lo_idPll As Control = ctr_obtControl("id")

                ' Se verifica si se obtuvo el control
                If lo_idPll Is Nothing Then
                    Return False
                End If

                ' Se obtiene el id de la planilla
                li_idPll = obj_obtValorControl(lo_idPll)

                ' Se verifica si se obtuvo el id de la planilla 
                If li_idPll < 1 Then
                    sub_mostrarMensaje("Ocurrio un error al obtener el id de la planilla.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                    Return False
                End If

            End If

            ' Se recorre las filas del dataTable
            For Each lo_row As DataRow In lo_dtb.Rows

                ' Se obtiene el id del documento
                Dim li_idDoc As Integer = lo_row("Id_Doc")

                ' Se obtiene el tipo de documento
                Dim li_tipoDoc As Integer = lo_row("Tipo_Doc")

                ' Se obtiene la linea del asiento en donde se encuentra el saldo del documento
                Dim li_docLine As Integer = lo_row("DocLine")

                ' Se obtiene el id del registro del estado de cuenta
                Dim li_idEC As Integer = lo_row("idEC")

                ' Se obtiene el numero de asignacion actual
                Dim li_nroAsig As Integer = lo_row("lineaNumAsg")

                ' asientoajustot 
                Dim asientoajustot As String = lo_row("asientoajustoT")


                ' Se verifica si el documento existe en otra planilla abierta
                Dim li_verifPll As Integer = entPlanilla.int_verExitDocEnPllAbierta2(li_idDoc, li_tipoDoc, li_docLine, li_idPll, asientoajustot)
                If li_verifPll > 0 Then
                    sub_mostrarMensaje("El documento " & li_idDoc.ToString & " de tipo " & li_tipoDoc & " está registrado en la planilla " & li_verifPll.ToString & ", la cual se encuentra abierta. (Nro. Asig: " & li_nroAsig.ToString & ")", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_val)
                    Return False
                End If

                ' Se verifica si el registro del estado de cuenta existe en otra planilla abierta
                li_verifPll = entPlanilla.int_verExitECEnPllAbierta(li_idEC, li_idPll)
                If li_verifPll > 0 Then
                    sub_mostrarMensaje("El depósito " & li_idEC.ToString & " está registrado en la planilla " & li_verifPll.ToString & ", la cual se encuentra abierta. (Nro. Asig: " & li_nroAsig.ToString & ")", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_val)
                    Return False
                End If

            Next

            ' Si la validación fue correcta, se retorna TRUE
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

#End Region

#Region "val_SaldoFavor"

    Private Function bol_tieneSF(ByVal po_dtbEC As DataTable) As Boolean
        Try

            ' Se recorre las filas del dataTable para verificar si hay saldos a favor
            For Each lo_row As DataRow In po_dtbEC.Rows

                ' Se verifica si la fila corresponde a un registro de Saldo a Favor
                If lo_row("TipoOp") = "SF" Then
                    Return True
                End If

            Next

            ' Si no se encontro registros de tipo Saldo a Favor
            Return False

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Function bol_valClienteSF(ByVal po_rowDoc As DataRow, ByVal po_dtbEC As DataTable) As Boolean
        Try

            ' Se recorre el dataTable del estado de cuenta
            For Each lo_row As DataRow In po_dtbEC.Rows

                ' Se verifica si la fila corresponde a un registro de Saldo a Favor
                If lo_row("TipoOp") = "SF" Then

                    ' Se verifica si la descripcion contiene el código del cliente
                    If lo_row("Descripcion").ToString.ToLower.Contains(po_rowDoc("Codigo").ToString.ToLower) = False Then

                        ' Se muestra un mensaje de error
                        MsgBox("Solo se puede seleccionar saldos a favor que estén relacionados al código del cliente del documento seleccionado.")
                        Return False

                    End If

                End If

            Next

            ' Si la validación fue correcta, se retorna TRUE
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Function bol_valSaldosAFavor(ByVal po_dtbEC As DataTable) As Boolean
        Try

            ' Se verifica si el dataTable de Estado de Cuenta tiene mas de un registro
            If po_dtbEC.Rows.Count = 1 Then

                ' Se verifica si la unica fila del dataTable corresponde a un Saldo a Favor
                Dim ls_sf As String = po_dtbEC.Rows(0)("TipoOp")
                If ls_sf.Trim = "SF" Then
                    ' Se muestra un mensaje de error que indique que no puede existir un saldo a favor en una asignacion UNO a UNO o MUCHOS a UNO
                    MsgBox("Solo se puede asignar un Saldo a Favor si este va acompañado de uno o mas depositos comunes.")
                    Return False
                End If

            Else

                ' Se recorre las filas del dataTable para verificar si hay saldos a favor, para verificar cuantos depositos comunes existen y para verificar la moneda
                Dim lb_tieneSF As Boolean = False
                Dim li_depCom As String = 0
                Dim ls_moneda As String = ""
                For Each lo_row As DataRow In po_dtbEC.Rows

                    ' Se obtiene la moneda del pago
                    If ls_moneda.Trim = "" Then
                        ls_moneda = lo_row("Moneda")
                    End If

                    ' Se verifica si las monedas son diferentes
                    If ls_moneda <> lo_row("Moneda") Then
                        MsgBox("Los saldos a favor deben tener la misma moneda que los depositos seleccionados")
                        Return False
                    End If

                    ' Se verifica si la fila corresponde a un registro de Saldo a Favor
                    If lo_row("TipoOp") = "SF" Then
                        lb_tieneSF = True
                    Else
                        li_depCom = li_depCom + 1
                    End If

                Next

                ' Se verifica si solo hay registros de tipo saldo a favor
                If lb_tieneSF = True And li_depCom = 0 Then
                    MsgBox("Solo se puede asignar un Saldo a Favor si este va acompañado de uno o mas depositos comunes.")
                    Return False
                End If

                ' Se verifica si se seleccionó mas de un deposito
                If li_depCom > 1 Then
                    MsgBox("Cuando se selecciona Saldos a Favor solo se puede seleccionar un solo deposito.")
                    Return False
                End If

            End If

            ' Si la validación fue correcta, se retorna TRUE
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

#End Region

#Region "val_asignaciones"

    Private Function bol_validarIdsDetalle(ByVal po_rowDocsClien As DataRow, ByVal po_rowEC As DataRow) As Boolean
        Try

            ' Se obtiene la grilla de planilla
            Dim lo_uctPlanilla As uct_gridConBusqueda = ctr_obtenerControl("gmi_plaPagosDetalle", o_form.Controls)
            Dim lo_dtb As DataTable = CType(obj_obtValorControl(lo_uctPlanilla), DataTable)

            ' Se recorre el dataTable de la planilla
            For Each lo_row As DataRow In lo_dtb.Rows

                ' Se verifica si el id del documento existe en la fila actual
                If lo_row("Id_Doc") = po_rowDocsClien("Id_Doc") And lo_row("DocLine") = po_rowDocsClien("Line_ID") Then
                    MsgBox("El Id del documento " & po_rowDocsClien("Id_Doc") & " ya existe en el detalle de la planilla en la asignacion número " & lo_row("lineaNumAsg") & ".")
                    Return False
                End If

                ' Se verifica si el id del registro del estado de cuenta existe en la fila actual
                If lo_row("idEC") = po_rowEC("id") Then
                    MsgBox("El Id  " & lo_row("idEC") & " del registro del estado de cuenta seleccionado ya existe en el detalle de la planilla en la asignacion número " & lo_row("lineaNumAsg") & ".")
                    Return False
                End If

            Next

            ' Si no se encontro los valores buscados 
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Function bol_valUnoAUno(ByVal po_rowDocsClien As DataRow, ByVal po_rowEC As DataRow) As Boolean
        Try
            'Fernando: Ana Herrera dice que la fecha del pago si puede ser menor a la fecha del documento
            'La fecha del pago no puede ser menor a la fecha del documento
            'If CDate(po_rowDocsClien("FechaDoc")).CompareTo(CDate(po_rowEC("Fecha"))) > 0 Then
            '    MsgBox("La fecha del pago no puede ser menor a la fecha del documento.")
            '    Return False
            'End If

            ' Se verifica que el Id del documento y el Id del EC no estne registrados en el detalle
            If bol_validarIdsDetalle(po_rowDocsClien, po_rowEC) = False Then
                Return False
            End If

            ' Se valida que Id del Estado de Cuenta no exista en otra planilla abierta o cerrada
            If bol_validarIdsEnPlanillas(po_rowDocsClien("Id_Doc"), po_rowEC("id")) = False Then
                Return False
            End If

            ' Si la validacion fue correcta se retorna TRUE
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Function bol_fechaPagoMenor(ByVal po_rowDocsClien As DataRow, ByVal po_rowEC As DataRow) As Boolean
        Try

            ' Se valida si la fecha del pago es menor a la fecha del documento
            If CDate(po_rowDocsClien("FechaDoc")).CompareTo(CDate(po_rowEC("Fecha"))) > 0 Then
                Return True
            End If

            ' Si la validacion fue correcta se retorna TRUE
            Return False

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Function bol_valMuchosAUno(ByVal po_dtbDocs As DataTable, ByVal po_dtbEC As DataTable)
        Try

            ' Se declara una variable para obtener el codigo del cliente
            Dim ls_codCliente As String = ""

            ' Se recorre las filas del DataTable de documentos
            For Each lo_row As DataRow In po_dtbDocs.Rows

                ' Se obtiene el código del cliente
                If ls_codCliente.Trim = "" Then ls_codCliente = lo_row("Codigo")

                ' Se verifica si los códigos de cliente entre las filas seleccionadas son iguales
                If ls_codCliente <> lo_row("Codigo") Then

                    ' Se muestra un mensaje que indicará que se intenta aplicar un pago a facturas de diferente cliente
                    MsgBox("Un pago solo puede aplicarse facturas de un mismo cliente")
                    Return False

                End If

                ' Se realiza la validación de uno a uno
                If bol_valUnoAUno(lo_row, po_dtbEC.Rows(0)) = False Then
                    Return False
                End If

            Next

            ' Si la validacion fue correcta 
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Function bol_valMuchosAUnoAlGrabar()
        Try

            ' Se obtiene el grid del detalle de la planilla
            Dim lo_gridPll As Control = ctr_obtControl("gmi_plaPagosDetalle")

            ' Se verifica si se obtuvo el grid
            If lo_gridPll Is Nothing Then
                Return False
            End If

            ' Se obtiene el dataTable del grid
            Dim lo_dtb As DataTable = CType(lo_gridPll, uct_gridConBusqueda).DataSource

            ' Se verifica si se obtuvo el dataTable
            If lo_dtb Is Nothing Then
                sub_mostrarMensaje("No se pudo obtener la fuente de datos del detalle de planilla.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Return False
            End If

            ' Se verifica si se obtuvo registros
            If lo_dtb.Rows.Count < 1 Then
                sub_mostrarMensaje("El detalle de la planilla no tiene registros.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_val)
                Return False
            End If

            ' Se declara una variable para obtener el tipo de asignacion
            Dim li_tipoAsg As Integer = -1

            ' Se declara una variable para el numero de asignacion
            Dim li_nroAsg As Integer = -1

            ' Se declara una variable para obtener el monto del depósito
            Dim ld_montoDep As Double = 0.0

            ' Se declara una variable para acumular los importes a aplicar de una asignacion
            Dim ld_impAplAsg As Double = 0.0

            ' Se declara una lista para los montos a acumular por asignacion
            Dim lo_lstImpApl As New List(Of Double)

            ' Se recorre el dataTable para validar las asignaciones de Muchos a Uno
            For Each lo_row As DataRow In lo_dtb.Rows

                ' Se obtiene el tipo de asignacion
                li_tipoAsg = lo_row("tipoAsg")

                ' Se verifica si el tipo de asignacion corresponde a Muchos a uno
                If li_tipoAsg <> 2 Then
                    Continue For
                End If

                ' Se obtiene la moneda del depósito
                Dim ls_monedaDep As String = lo_row("MonedaPag")

                ' Se verifica si se obtuvo la moneda del depósito
                If ls_monedaDep.Trim = "" Then
                    sub_mostrarMensaje("No se puedo obtener la moneda del depósito en la asignación " & li_nroAsg.ToString & ".", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                    Return False
                End If

                ' Se verifica si el importe aplicado es mayor a cero
                Dim ld_impApl As Double = dbl_obtImpAplPorMoneda(lo_row, ls_monedaDep)

                ' Se verifica si se obtuvo el importe aplicado
                If ld_impApl = -1 Then
                    sub_mostrarMensaje("Ocurrió un error al obtener el importe aplicado por moneda en la asignación " & li_nroAsg.ToString & ".", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                    Return False
                ElseIf Not ld_impApl > 0.0 Then
                    sub_mostrarMensaje("El importe aplicado debe ser mayor a 0.000000. (Nro. Asignacion: " & li_nroAsg.ToString & ")", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                    Return False
                End If

                ' Se verifica si es un número de asignacion nuevo
                If li_nroAsg <> CInt(lo_row("lineaNumAsg")) Then

                    ' Se verifica si es el primer registro recorrido
                    If ld_montoDep = 0.0 Then

                        ' Se obtiene el monto del depósito
                        ld_montoDep = CDbl(lo_row("MontoOp"))

                        ' Se añade a la lista el importe a aplicar
                        sub_anadirImpAplALista(lo_row, ls_monedaDep, lo_lstImpApl)

                        ' Se obtiene el numero de asignacion
                        li_nroAsg = lo_row("lineaNumAsg")

                    Else

                        ' Se obtiene la suma de los importes a aplicar de la lista obtenida de la asignacion anterior
                        Dim ld_sumImpApl As Double = dbl_sumImpAplicado(lo_lstImpApl)

                        ' Se verifica si se obtuvo la suma de manera correcta
                        If ld_sumImpApl = -1 Then
                            sub_mostrarMensaje("Ocurrió un error al obtener la suma de los importes aplicados en la asignación " & li_nroAsg.ToString & ".", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                            Return False
                        End If
                        'ld_sumImpApl
                        ' Se verifica si la suma de los importes a aplicar de la asignacion son diferentes al monto del depósito
                        If TruncateDecimal(ld_sumImpApl, 2) <> ld_montoDep Then
                            sub_mostrarMensaje("La suma de los importes aplicados a los documentos debe ser igual al monto del depósito en la asignación " & li_nroAsg.ToString & ".", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_val)
                            Return False
                        End If

                        ' Se limpia la lista
                        lo_lstImpApl.Clear()

                        ' Se obtiene el numero de asignacion
                        li_nroAsg = lo_row("lineaNumAsg")

                    End If


                Else

                    ' Se añade a la lista el importe a aplicar
                    sub_anadirImpAplALista(lo_row, ls_monedaDep, lo_lstImpApl)

                End If

            Next

            ' Se verifica si quedo alguna comparacion pendiente segun el listado de importes aplicados
            If lo_lstImpApl.Count > 0 Then

                ' Se obtiene la suma de los importes a aplicar de la lista obtenida de la asignacion anterior
                Dim ld_sumImpApl As Double = dbl_sumImpAplicado(lo_lstImpApl)

                ' Se verifica si se obtuvo la suma de manera correcta
                If ld_sumImpApl = -1 Then
                    sub_mostrarMensaje("Ocurrió un error al obtener la suma de los importes aplicados en la asignación " & li_nroAsg.ToString & ".", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                    Return False
                End If

                ' Se verifica si la suma de los importes a aplicar de la asignacion son diferentes al monto del depósito

                If TruncateDecimal(ld_sumImpApl, 2) <> ld_montoDep Then
                    sub_mostrarMensaje("La suma de los importes aplicados a los documentos debe ser igual al monto del depósito en la asignación " & li_nroAsg.ToString & ".", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_val)
                    Return False
                End If

                ' Se limpia la lista
                lo_lstImpApl.Clear()

            End If

            ' Si todas las validaciones fueron correctas, se retorna TRUE
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Function TruncateDecimal(value As Decimal, precision As Integer) As Decimal
        Dim stepper As Decimal = Math.Pow(10, precision)
        Dim tmp As Decimal = Math.Truncate(stepper * value)
        Return tmp / stepper
    End Function

    Private Function dbl_sumImpAplicado(po_lstImpApl As List(Of Double)) As Double
        Try

            ' Se declara una variable para la suma
            Dim ld_sumaImpApl As Double = 0.0

            ' Se recorre la lista
            For Each ld_impApl As Double In po_lstImpApl

                ' Se acumula los importes en la variable
                ld_sumaImpApl = ld_sumaImpApl + ld_impApl

            Next

            ' Se retorna la suma del importe aplicado
            Return ld_sumaImpApl

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return -1
        End Try
    End Function

    Private Sub sub_anadirImpAplALista(po_row As DataRow, ps_monedaDep As String, po_lstImpApl As List(Of Double))
        Try

            ' Se obtiene el valor de la columna Imp_aplicado por moneda
            Dim ld_impApl As Double = dbl_obtImpAplPorMoneda(po_row, ps_monedaDep)

            ' Se añade a la lista el importe a plicar
            po_lstImpApl.Add(ld_impApl)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Function dbl_obtImpAplPorMoneda(po_row As DataRow, ps_monedaDep As String)
        Try

            ' Se declara una variable para obtener el importe aplicado
            Dim ld_impApl As Double

            ' Se verifica la moneda del depósito
            If ps_monedaDep = str_obtMonLocal() Then

                ' Se obtiene el valor de la columna Imp_aplicado
                ld_impApl = po_row("Imp_Aplicado")

            Else

                ' Se obtiene el valor de la columna Imp_aplicado
                ld_impApl = po_row("Imp_AplicadoME")

            End If

            ' Se retorna el importe aplicado
            Return ld_impApl

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return -1
        End Try
    End Function

#End Region

#Region "val_DifTC"

    Private Sub sub_valSelChkDifTC(ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs)
        Try

            ' Se verifica si el valor recibido es nulo
            If e.Value Is Nothing Then
                Exit Sub
            End If

            ' Se obtiene el grid del detalle de la planilla
            Dim lo_grid As Control = ctr_obtenerControl("gmi_plaPagosDetalle", o_form.Controls)

            ' Se verifica si se obtuvo el grid
            If lo_grid Is Nothing Then
                sub_mostrarMensaje("No se obtuvo el control <gmi_plaPagosDetalle> para la validacion del check de diferencia de cambio.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            ' Se verifica que la columna seleccionada corresponde al check de Diferencia de tipo de cambio
            ' - Se obtiene la columna seleccionada
            Dim lo_gridCol As GridColumn = CType(lo_grid, uct_gridConBusqueda).col_obtColumnaSeleccionada

            ' - Se verifica si se obtuvo la columna seleccionada
            If lo_gridCol Is Nothing Then
                sub_mostrarMensaje("Ocurrió un error al obtener la columna seleccionada.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            ' Se verifica el nombre de la columna seleccionada
            If Not lo_gridCol.Name.ToLower.Contains("diftc") Then
                Exit Sub
            End If

            ' Se verifica si el valor recibido indica que se selecciono el check
            If e.Value.ToString.ToLower = "n" Then
                Exit Sub
            End If

            ' Se obtiene el dataRow de la fila seleccionada
            Dim lo_row As DataRow = CType(lo_grid, uct_gridConBusqueda).dtr_obtDataRowFilaSeleccionada

            ' Se verifica si las moneda del documento y la moneda del pago son iguales
            If lo_row("MonedaDoc").ToString.ToLower = lo_row("MonedaPag").ToString.ToLower Then
                MsgBox("No es posible indicar Diferencia de Cambio cuando la moneda del documento y la moneda del depósito son iguales.")

                ' Se asigna el indicador para el checkbox de la diferencia de cambio
                b_difTC = True

                Exit Sub
            End If

            ' Se verifica los valores que indican que no se ha realizado el pago completo del saldo del documento seleccionado
            If lo_row("SaldoFavor") = 0.0 And lo_row("SaldoFavorME") = 0.0 And lo_row("ComentarioPl").ToString.ToLower = "cancelacion" Then
                MsgBox("La cancelación del documento se realizó con un monto exacto. No es posible indicar Diferencia de Cambio en este registro.")

                ' Se asigna el indicador para el checkbox de la diferencia de cambio
                b_difTC = True

                Exit Sub
            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_asigCheckDifTC()
        Try

            ' Se verifica el indicador del check de diferencia de cambio
            If b_difTC = False Then
                Exit Sub
            End If

            ' Se obtiene el grid del detalle de la planilla
            Dim lo_grid As Control = ctr_obtenerControl("gmi_plaPagosDetalle", o_form.Controls)

            ' Se verifica si se obtuvo el grid
            If lo_grid Is Nothing Then
                sub_mostrarMensaje("No se obtuvo el control <gmi_plaPagosDetalle> para la validacion del check de diferencia de cambio.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            ' Se obtiene el dataRow de la fila seleccionada
            Dim lo_row As DataRow = CType(lo_grid, uct_gridConBusqueda).dtr_obtDataRowFilaSeleccionada

            ' Se asigna el valor a la columna difTC
            lo_row("difTC") = "N"

            ' Se reinicia el indicador
            b_difTC = False

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#End Region

#Region "Busquedas"

    Public Sub sub_cargarUGCEstadoCuenta()
        Try

            ' Se verifica el modo del formulario para que solo no se pueda cargar la grilla en el modo busqueda
            If o_form.Modo = enm_modoForm.BUSCAR Or str_obtEstadoObjeto() <> "O" Then
                Exit Sub
            End If

            ' Se obtiene el valor del control que contendra el valor de filtro para la busqueda
            Dim lo_ulsClientes As Control = ctr_obtenerControl("CtasBanco", o_form.Controls)
            Dim ls_filtro As String = obj_obtValorControl(lo_ulsClientes)

            ' Se asigna un valor por defecto en caso de que el valor obtenido sea nulo
            If ls_filtro Is Nothing Then
                ls_filtro = ""
            End If

            ' Se obtiene el combo del tipo de planilla
            Dim lo_cboTipoPla As Control = ctr_obtenerControl("tipoPla", o_form.Controls)

            ' Se verifica si se obtuvo el combo
            If lo_cboTipoPla Is Nothing Then
                sub_mostrarMensaje("Ocurrio un error al obtener el control Tipo de Planilla.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            ' Se obtiene el tipo de planilla
            Dim ls_tipoPla As String = obj_obtValorControl(lo_cboTipoPla)

            ' Se verifica si se obtuvo el valor
            If ls_tipoPla Is Nothing Then
                'MsgBox("No se pudo obtener el Tipo de Planilla. Seleccione un Tipo de Planilla antes de realizar esta acción.")
                Exit Sub
            End If

            ' Se declara un DataTable para obtener los datos de la busqueda
            Dim lo_dtb As DataTable

            ' Se verifica el tipo de planilla
            If ls_tipoPla.Trim = "D" Then

                ' Se muestra un mensaje que indique que en las planillas de tipo Detracción solo puede utilizar depositos realizados en el banco de la nacion
                MsgBox("las planillas de tipo Detracción solo puede utilizar depositos realizados en el Banco de la Nacion.")

                ' Se obtiene la cuenta del banco de la nación
                ls_filtro = entPlanilla.str_obtCtaBancoNacion

                ' Se obtiene los datos del Estado de Cuenta

                'o_dtbEC.Clear()
                lo_dtb = entCargarEC.dtb_obtEstadoCuentaPPR_Detraccion(ls_filtro)

            Else

                ''o_dtbEC.Clear()

                ' Se obtiene los datos del Estado de Cuenta
                lo_dtb = entCargarEC.dtb_obtEstadoCuentaPPR(ls_filtro)



            End If

            ' Se verifica si se ejecuto de manera correcta la busqueda
            If lo_dtb Is Nothing Then
                sub_mostrarMensaje("Ocurrio un error al obtener los documentos pendientes por cobrar.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            ' Se obtiene el dataTable en una variable de la clase para la ejecucion del metodo de recarga
            o_dtbEC = lo_dtb.Clone

            ' Se asigna las propiedades necesarias a la grilla del Estado de Cuenta
            sub_asigValorControl("ECBanco", lo_dtb)

            ' Se inicializa el control de usuario GridControl para el Estado de Cuenta""
            Dim lo_ugc As Control = ctr_obtenerControl("ECBanco", o_form.Controls)
            CType(lo_ugc, uct_gridConBusqueda).sub_inicializar()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Public Sub sub_cargarUGCClientes()
        Try

            ' Se verifica el modo del formulario para que solo no se pueda cargar la grilla en el modo busqueda
            If o_form.Modo = enm_modoForm.BUSCAR Or str_obtEstadoObjeto() <> "O" Then
                Exit Sub
            End If

            ' Se obtiene el valor del control que contendra el valor de filtro para la busqueda
            Dim lo_ulsClientes As Control = ctr_obtenerControl("ulsClientes", o_form.Controls)
            Dim ls_filtro As String = obj_obtValorControl(lo_ulsClientes)

            ' Se asigna un valor por defecto en caso de que el valor obtenido sea nulo
            If ls_filtro Is Nothing Then
                ls_filtro = ""
            End If

            ' Se obtiene los datos del Estado de Cuenta
            Dim lo_dtb As DataTable = entPlanilla.dtb_obtCtasPorCobrar(ls_filtro)

            ' Se verifica si se ejecuto de manera correcta la busqueda
            If lo_dtb Is Nothing Then
                sub_mostrarMensaje("Ocurrio un error al obtener los documentos pendientes por cobrar.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            ' Se obtiene el dataTable en una variable de la clase para la ejecucion del metodo de recarga
            o_dtbDocsCliente = lo_dtb.Clone

            ' Se asigna las propiedades necesarias a la grilla del Estado de Cuenta
            sub_asigValorControl("ECClientes", lo_dtb)

            ' Se inicializa el control de usuario GridControl para el Estado de Cuenta
            Dim lo_ugc As Control = ctr_obtenerControl("ECClientes", o_form.Controls)
            CType(lo_ugc, uct_gridConBusqueda).sub_inicializar()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_recargarGridsSuperiores()
        Try

            ' Se verifica si los dataTables de la clase contienen datos
            If o_dtbDocsCliente Is Nothing Then
                sub_cargarUGCClientes()
            Else

                ' Se verifica si el dataTable tiene filas
                If o_dtbDocsCliente.Rows.Count = 0 Then
                    sub_cargarUGCClientes()
                Else

                    ' Se asigna las propiedades necesarias a la grilla del Estado de Cuenta
                    sub_asigValorControl("ECClientes", o_dtbDocsCliente)

                    ' Se inicializa el control de usuario GridControl para el Estado de Cuenta
                    Dim lo_ugc As Control = ctr_obtenerControl("ECClientes", o_form.Controls)
                    CType(lo_ugc, uct_gridConBusqueda).sub_inicializar()

                End If

            End If

            If o_dtbEC Is Nothing Then
                sub_cargarUGCEstadoCuenta()
            Else

                ' Se verifica si el dataTable tiene filas
                If o_dtbEC.Rows.Count = 0 Then
                    sub_cargarUGCEstadoCuenta()
                Else

                    ' Se asigna las propiedades necesarias a la grilla del Estado de Cuenta
                    sub_asigValorControl("ECBanco", o_dtbEC)

                    ' Se inicializa el control de usuario GridControl para el Estado de Cuenta
                    Dim lo_ugc As Control = ctr_obtenerControl("ECBanco", o_form.Controls)
                    CType(lo_ugc, uct_gridConBusqueda).sub_inicializar()

                End If

            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Function str_obtTipoPlanilla() As String
        Try

            ' Se obtiene el combo del tipo de planilla
            Dim lo_cboTipoPla As Control = ctr_obtenerControl("tipoPla", o_form.Controls)

            ' Se verifica si se obtuvo el combo
            If lo_cboTipoPla Is Nothing Then
                sub_mostrarMensaje("Ocurrio un error al obtener el control Tipo de Planilla.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Return ""
            End If

            ' Se obtiene el tipo de planilla
            Dim ls_tipoPla As String = obj_obtValorControl(lo_cboTipoPla)

            ' Se verifica si se obtuvo el valor
            If ls_tipoPla Is Nothing Then
                MsgBox("No se seleccionó el tipo de planilla.")
                Return ""
            End If

            ' Se retorna el tipo de planilla
            Return ls_tipoPla

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return ""
        End Try
    End Function

#End Region

#Region "Exportar Excel"

    Public Sub sub_ExportarExcelDespositoCz()
        Try
            ' Se obtiene el grid del detalle de la planilla
            'Dim gc As Control = ctr_obtenerControl("gmi_plaPagosDetalle", o_form.Controls)
            Dim gc As Control = ctr_obtenerControl("ECBanco", o_form.Controls)

            ' Se obtiene el dataSource del grid
            Dim ds As DataTable = CType(gc, uct_gridConBusqueda).DataSource


            ' Se verifica si se obtuvo el dataTable
            If ds Is Nothing Then
                MsgBox("No hay datos en la grilla del detalle de la planilla.")
                Exit Sub
            End If

            ' Se verifica si el dataTable tiene registros
            If ds.Rows.Count < 1 Then
                MsgBox("No hay registros en la grilla del detalle de la planilla.")
                Exit Sub
            End If

            ' Se genera el reporte
            GenegarReporteDepo(CrearTablaDepo(ds))

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub


    Public Sub sub_ExportarExcel()
        Try
            ' Se obtiene el grid del detalle de la planilla
            Dim gc As Control = ctr_obtenerControl("gmi_plaPagosDetalle", o_form.Controls)

            ' Se obtiene el dataSource del grid
            Dim ds As DataTable = CType(gc, uct_gridConBusqueda).DataSource

            ' Se verifica si se obtuvo el dataTable
            If ds Is Nothing Then
                MsgBox("No hay datos en la grilla del detalle de la planilla.")
                Exit Sub
            End If

            ' Se verifica si el dataTable tiene registros
            If ds.Rows.Count < 1 Then
                MsgBox("No hay registros en la grilla del detalle de la planilla.")
                Exit Sub
            End If


            Dim ls_tipoPla As String = str_obtTipoPlanilla()
            If ls_tipoPla = "A" Then
                GenerarReporteAutoDetraccionCz(CreateTableAutoDetraccionCz(ds))
            End If

            ' Se genera el reporte
            GenegarReporte(CrearTabla(ds))

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Function CrearTablaDepo(ByVal idtbDatos As DataTable) As DataTable
        Try
            Dim dtbExport As New DataTable
            Dim strTipMon As String = ""
            Dim col As DataColumn
            col = New DataColumn("id Planilla", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("Fecha", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("Desc", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("Monto", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("Operacion", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("Cuenta", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("Moneda", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("Banco", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("Contable", GetType(System.String)) : dtbExport.Columns.Add(col)

            Dim dr As DataRow
            For i As Integer = 0 To idtbDatos.Rows.Count - 1
                dr = dtbExport.NewRow()
                dr("id Planilla") = idtbDatos.Rows(i)("id").ToString
                dr("Fecha") = idtbDatos.Rows(i)("Fecha").ToString
                dr("Desc") = idtbDatos.Rows(i)("Descripcion").ToString
                dr("Operacion") = idtbDatos.Rows(i)("Operacion").ToString
                dr("Cuenta") = idtbDatos.Rows(i)("Cuenta").ToString
                dr("Moneda") = strTipMon & idtbDatos.Rows(i)("Moneda").ToString
                If (idtbDatos.Rows(i)("Moneda").ToString.Equals("SOL")) Then
                    strTipMon = "S/. "
                Else
                    strTipMon = "$ "
                End If
                dr("Monto") = strTipMon & dbl_redondearImporte(idtbDatos.Rows(i)("Monto")).ToString
                dr("Contable") = idtbDatos.Rows(i)("Cta_Contable").ToString
                dr("Banco") = idtbDatos.Rows(i)("Banco").ToString
                dtbExport.Rows.Add(dr)
            Next
            Return dtbExport
        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Private Sub GenegarReporteDepo(ByVal idtbDatos As DataTable)
        Dim strRutaFormato As String = Application.StartupPath & "\FormatosExcelReportes\PlanillaDepositos.xlsx"
        Dim strRutaTemp As String = Path.GetTempPath & "Planilla" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".xlsx"
        Dim fNewFile As New FileInfo(strRutaTemp)
        Dim fexistingFile As New FileInfo(strRutaFormato)
        Try
            Using MyExcel As New ExcelPackage(fexistingFile)
                Dim iExcelWS As ExcelWorksheet
                iExcelWS = MyExcel.Workbook.Worksheets("hoja1")
                iExcelWS.Cells("B4").LoadFromDataTable(idtbDatos, False)
                iExcelWS.Cells("B4:B" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("D4:D" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("E4:E" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("F4:F" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("H4:H" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("J4:J" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("L4:L" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                For i As Integer = 4 To idtbDatos.Rows.Count + 3 Step 2
                    iExcelWS.Cells("B" & i & ":L" & i).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                Next
                iExcelWS.Cells("B" & idtbDatos.Rows.Count + 3 & ":L" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("B4:B" & idtbDatos.Rows.Count + 3).AutoFitColumns()
                MyExcel.SaveAs(fNewFile)
            End Using
            System.Diagnostics.Process.Start(strRutaTemp)
        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub


    Private Function CrearTabla(ByVal idtbDatos As DataTable) As DataTable
        Try
            Dim dtbExport As New DataTable
            Dim strTipMon As String = ""
            Dim col As DataColumn
            col = New DataColumn("Fecha Crea", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("id Planilla", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("Cliente", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("NroDoc", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("Fecha", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("ImpOrig", GetType(System.String)) : dtbExport.Columns.Add(col)

            col = New DataColumn("Saldo", GetType(System.String)) : dtbExport.Columns.Add(col) 'adiciono

            col = New DataColumn("ImpApli", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("SaldoAF", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("TC", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("Monto", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("Banco", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("OP", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("Fecha2", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("Comend", GetType(System.String)) : dtbExport.Columns.Add(col)

            Dim dr As DataRow
            For i As Integer = 0 To idtbDatos.Rows.Count - 1
                dr = dtbExport.NewRow()
                dr("Fecha Crea") = idtbDatos.Rows(i)("FechaCrea").ToString.Substring(0, 10)
                dr("id Planilla") = idtbDatos.Rows(i)("id").ToString
                dr("Cliente") = idtbDatos.Rows(i)("Nombre").ToString
                dr("NroDoc") = idtbDatos.Rows(i)("Referencia").ToString
                dr("Fecha") = idtbDatos.Rows(i)("FechaDoc").ToString.Substring(0, 10)
                If (idtbDatos.Rows(i)("monedaDoc").ToString.Equals("SOL")) Then
                    strTipMon = "S/. "
                    If idtbDatos.Rows(i)("total") > idtbDatos.Rows(i)("imp_aplicadoME") Then
                        dr("ImpApli") = strTipMon & dbl_redondearImporte(idtbDatos.Rows(i)("imp_aplicado")).ToString
                    Else
                        dr("ImpApli") = strTipMon & dbl_redondearImporte(idtbDatos.Rows(i)("total")).ToString
                    End If
                Else
                    strTipMon = "$ "
                    If idtbDatos.Rows(i)("total") > idtbDatos.Rows(i)("imp_aplicadoME") Then
                        dr("ImpApli") = strTipMon & dbl_redondearImporte(idtbDatos.Rows(i)("imp_aplicadoME")).ToString
                    Else
                        dr("ImpApli") = strTipMon & dbl_redondearImporte(idtbDatos.Rows(i)("total")).ToString
                    End If

                End If
                dr("Saldo") = strTipMon & dbl_redondearImporte(idtbDatos.Rows(i)("Saldo")).ToString 'agregado
                dr("ImpOrig") = strTipMon & dbl_redondearImporte(idtbDatos.Rows(i)("total")).ToString


                If (idtbDatos.Rows(i)("MonedaPag").ToString.Equals("SOL")) Then
                    strTipMon = "S/. "
                    dr("SaldoAF") = strTipMon & dbl_redondearImporte(idtbDatos.Rows(i)("SaldoFavor").ToString)
                Else
                    strTipMon = "$ "
                    dr("SaldoAF") = strTipMon & dbl_redondearImporte(idtbDatos.Rows(i)("SaldoFavorME").ToString)
                End If

                dr("TC") = idtbDatos.Rows(i)("tipo_cambio").ToString
                dr("Monto") = strTipMon & dbl_redondearImporte(idtbDatos.Rows(i)("montoop")).ToString
                dr("Banco") = entPlanilla.dtb_ObtenerBanco(CInt(idtbDatos.Rows(i)("idEc").ToString))
                dr("OP") = idtbDatos.Rows(i)("nro_operacion").ToString
                dr("Fecha2") = idtbDatos.Rows(i)("fechapago").ToString.Substring(0, 10)
                dr("Comend") = idtbDatos.Rows(i)("ComentarioPl").ToString
                dtbExport.Rows.Add(dr)
            Next
            Return dtbExport
        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Private Function CreateTableAutoDetraccionCz(ByVal idtbDatos As DataTable) As DataTable

        Try
            Dim dtbExport As New DataTable
            Dim strTipMon As String = ""
            Dim col As DataColumn
            col = New DataColumn("Ruc", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("Cliente", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("CodBien", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("Monto", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("MporTC", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("TC", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("CXTC", GetType(System.Int32)) : dtbExport.Columns.Add(col)
            col = New DataColumn("Fecha", GetType(System.String)) : dtbExport.Columns.Add(col)
            col = New DataColumn("NroDoc", GetType(System.String)) : dtbExport.Columns.Add(col)

            Dim dr As DataRow
            For i As Integer = 0 To idtbDatos.Rows.Count - 1
                dr = dtbExport.NewRow()
                dr("Ruc") = idtbDatos.Rows(i)("Ruc").ToString.Trim()
                dr("Cliente") = idtbDatos.Rows(i)("Nombre").ToString.Trim()

                If idtbDatos.Rows(i)("CodBien").ToString.Trim = "0900" Then
                    dr("CodBien") = "010"
                Else
                    dr("CodBien") = "037"
                End If

                If (idtbDatos.Rows(i)("MonedaDoc").ToString.Equals("SOL")) Then
                    strTipMon = "S/. "
                Else
                    strTipMon = "$ "
                End If
                dr("Monto") = strTipMon & dbl_redondearImporte(idtbDatos.Rows(i)("total")).ToString.Trim()
                dr("TC") = idtbDatos.Rows(i)("tipo_cambio").ToString
                If idtbDatos.Rows(i)("CodBien").ToString.Trim = "0900" Then
                    dr("MporTC") = strTipMon & dbl_redondearImporte(idtbDatos.Rows(i)("total") * 0.15).ToString.Trim
                    dr("CXTC") = dbl_redondearImporCzDet(dbl_redondearImporte(idtbDatos.Rows(i)("total") * 0.15) * idtbDatos.Rows(i)("tipo_cambio")).ToString.Trim
                Else
                    dr("MporTC") = strTipMon & dbl_redondearImporte(idtbDatos.Rows(i)("total") * 0.12).ToString.Trim
                    dr("CXTC") = dbl_redondearImporCzDet(dbl_redondearImporte(idtbDatos.Rows(i)("total") * 0.12) * idtbDatos.Rows(i)("tipo_cambio")).ToString.Trim
                End If
                dr("Fecha") = idtbDatos.Rows(i)("FechaDoc").ToString.Substring(0, 10).Trim
                dr("NroDoc") = idtbDatos.Rows(i)("Referencia").ToString.Trim
                dtbExport.Rows.Add(dr)
            Next
            Return dtbExport
        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try


    End Function


    Private Sub GenegarReporte(ByVal idtbDatos As DataTable)
        Dim strRutaFormato As String = Application.StartupPath & "\FormatosExcelReportes\Planilla.xlsx"
        Dim strRutaTemp As String = Path.GetTempPath & "Planilla" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".xlsx"
        Dim fNewFile As New FileInfo(strRutaTemp)
        Dim fexistingFile As New FileInfo(strRutaFormato)
        Try
            Using MyExcel As New ExcelPackage(fexistingFile)
                Dim iExcelWS As ExcelWorksheet
                iExcelWS = MyExcel.Workbook.Worksheets("hoja1")
                iExcelWS.Cells("A4").LoadFromDataTable(idtbDatos, False)
                iExcelWS.Cells("A4:A" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("B4:B" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("D4:D" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("F4:F" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("H4:H" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("J4:J" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("L4:L" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("N4:N" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("O4:O" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin) 'AGREGADO
                For i As Integer = 4 To idtbDatos.Rows.Count + 3 Step 2
                    iExcelWS.Cells("A" & i & ":N" & i).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                Next
                iExcelWS.Cells("A" & idtbDatos.Rows.Count + 3 & ":O" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("A4:A" & idtbDatos.Rows.Count + 3).AutoFitColumns()
                MyExcel.SaveAs(fNewFile)
            End Using
            System.Diagnostics.Process.Start(strRutaTemp)
        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub GenerarReporteAutoDetraccionCz(ByVal idtbDatos As DataTable)
        Dim strRutaFormato As String = Application.StartupPath & "\FormatosExcelReportes\PlanillaAutoDetraccionz.xlsx"
        Dim strRutaTemp As String = Path.GetTempPath & "Planilla" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".xlsx"
        Dim fNewFile As New FileInfo(strRutaTemp)
        Dim fexistingFile As New FileInfo(strRutaFormato)
        Try
            Using MyExcel As New ExcelPackage(fexistingFile)
                Dim iExcelWS As ExcelWorksheet
                iExcelWS = MyExcel.Workbook.Worksheets("hoja1")
                iExcelWS.Cells("B4").LoadFromDataTable(idtbDatos, False)
                iExcelWS.Cells("B4:B" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("B4:B" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("D4:D" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("F4:F" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("H4:H" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("J4:J" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                For i As Integer = 4 To idtbDatos.Rows.Count + 3 Step 2
                    iExcelWS.Cells("B" & i & ":J" & i).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                Next
                iExcelWS.Cells("B" & idtbDatos.Rows.Count + 3 & ":J" & idtbDatos.Rows.Count + 3).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("B4:B" & idtbDatos.Rows.Count + 3).AutoFitColumns()
                MyExcel.SaveAs(fNewFile)
            End Using
            System.Diagnostics.Process.Start(strRutaTemp)
        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub


    Private Sub sub_repDepositosPendientes(ByVal idtbDatos As DataTable)
        Dim strRutaFormato As String = Application.StartupPath & "\FormatosExcelReportes\DepesitosPendientes.xlsx"
        Dim strRutaTemp As String = Path.GetTempPath & "Reporte_de_Depositos_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".xlsx"
        Dim fNewFile As New FileInfo(strRutaTemp)
        Dim fexistingFile As New FileInfo(strRutaFormato)
        Try
            Using MyExcel As New ExcelPackage(fexistingFile)
                Dim iExcelWS As ExcelWorksheet
                iExcelWS = MyExcel.Workbook.Worksheets("hoja1")
                idtbDatos.Columns.RemoveAt(0) : idtbDatos.AcceptChanges()
                iExcelWS.Cells("B5").LoadFromDataTable(idtbDatos, False)
                iExcelWS.Cells("B5:B" & idtbDatos.Rows.Count + 4).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("D5:D" & idtbDatos.Rows.Count + 4).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("F5:F" & idtbDatos.Rows.Count + 4).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("H5:H" & idtbDatos.Rows.Count + 4).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("J5:J" & idtbDatos.Rows.Count + 4).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("L5:L" & idtbDatos.Rows.Count + 4).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                iExcelWS.Cells("M5:M" & idtbDatos.Rows.Count + 4).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                For i As Integer = 6 To idtbDatos.Rows.Count + 4 Step 2
                    iExcelWS.Cells("B" & i & ":M" & i).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                Next
                MyExcel.SaveAs(fNewFile)
            End Using
            System.Diagnostics.Process.Start(strRutaTemp)
        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#Region "Reportes"

    Public Sub sub_mostrarReporte()
        Try

            ' Se crea un comboBox para el parametro Estado del reporte
            Dim lo_cboEstado As New System.Windows.Forms.ComboBox

            ' Se asigna la propiedad TAG con el nombre parametro 
            lo_cboEstado.Tag = "@estado"
            lo_cboEstado.DropDownStyle = ComboBoxStyle.DropDownList

            ' Se añade los items al comboBox
            lo_cboEstado.Items.Add("Todos")
            lo_cboEstado.Items.Add("Procesado")
            lo_cboEstado.Items.Add("Por Procesar")
            lo_cboEstado.Items.Add("No Procesado")
            lo_cboEstado.Items.Add("Procesado Manual")

            ' Se crea una lista para enviar el control como parametro
            Dim lo_lstParam As New List(Of Control)

            ' Se añade el control del parametro
            lo_lstParam.Add(lo_cboEstado)

            ' Se obtiene el resultado del reporte
            Dim lo_dtb As DataTable = dtb_obtDatosReporteConParams("gmi_sp_ReporteDepositos", lo_lstParam)

            ' Se verifica si se ejecuto la consulta para el reporte
            If lo_dtb Is Nothing Then
                Exit Sub
            End If
            If lo_dtb.Rows.Count = 0 Then
                Exit Sub
            End If

            'Metodo para Generar el Reporte de Depesitos Pendientes
            sub_repDepositosPendientes(lo_dtb)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

End Class

