Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraTab
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid
Imports DevExpress.XtraEditors

Public Module ModuleGlobalForm

    ' Variables de la clase
    WithEvents o_timer As New Timer
    Private o_pnlMsj As Panel
    Private i_seg As Integer

    ' * Modulo para metodos comunes de un formulario

    Public Sub sub_cargarCombo(ByVal po_comboBox As System.Windows.Forms.ComboBox, ByVal po_dt As DataTable, ByVal ps_displayMember As String, ByVal ps_valueMember As String)
        Try

            ' Se asigna el DataSource al ComboBox
            po_comboBox.DataSource = po_dt
            po_comboBox.DisplayMember = ps_displayMember
            po_comboBox.ValueMember = ps_valueMember
            po_comboBox.SelectedIndex = -1

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try

    End Sub

    Public Function bol_selecValorComboBox(ByVal po_comboBox As System.Windows.Forms.ComboBox, ByVal po_valor As Object) As Boolean
        Try

            ' Se recorre los elementos del comboBox
            For i As Integer = 0 To po_comboBox.Items.Count - 1

                ' Se selecciona el item actual
                po_comboBox.SelectedIndex = i

                ' Se verifica el valor seleccionado
                If po_comboBox.SelectedValue = po_valor Then
                    Return True
                End If

            Next

            ' Si no se encontro el valor
            Return False

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return False
        End Try
    End Function

    Public Sub sub_limpiarGridViewX(ByVal po_gridView As GridView)
        Try

            ' Se obtiene el DataTable del GridView
            Dim lo_dtb As DataTable = po_gridView.DataSource

            ' Se recorre las filas del DataTable
            For i As Integer = 0 To lo_dtb.Rows.Count - 1

                ' Se borra la fila de la posicion actual
                lo_dtb.Rows(i).Delete()

            Next

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_limpiarDataTable(ByVal po_dataTable As DataTable)
        Try

            ' Se borra las lineas del dataTable
            po_dataTable.Rows.Clear()

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Enum enm_modoForm
        ANADIR = 0
        ACTUALIZAR = 1
        VER = 2
        BUSCAR = 3
        RECUPERAR = 4
    End Enum

    Public Sub sub_limpiarControlesForm(ByVal po_controles As System.Windows.Forms.Control.ControlCollection)
        Try

            ' Se recorre los controles del formulario
            For i As Integer = 0 To po_controles.Count - 1

                ' Se obtiene el control
                Dim lo_Ctl = po_controles.Item(i)

                ' Se verifica el tipo de control
                If TypeOf (lo_Ctl) Is DateEdit Then

                    ' Se limpia el control
                    CType(lo_Ctl, DateEdit).Text = ""

                ElseIf TypeOf (lo_Ctl) Is CalcEdit Then

                    ' Se limpia el control
                    CType(lo_Ctl, CalcEdit).Value = 0.0

                ElseIf TypeOf (lo_Ctl) Is MemoEdit Then

                    ' Se limpia el control
                    CType(lo_Ctl, MemoEdit).Text = ""

                ElseIf TypeOf (lo_Ctl) Is CheckBox Then

                    ' Se limpia el control
                    CType(lo_Ctl, CheckBox).Checked = False

                ElseIf TypeOf (lo_Ctl) Is TextEdit Then

                    ' Se limpia el control
                    If Integer.TryParse((CType(lo_Ctl, TextEdit).Text), New Integer) Then
                        CType(lo_Ctl, TextEdit).Text = -1
                    ElseIf Double.TryParse((CType(lo_Ctl, TextEdit).Text), New Double) Then
                        CType(lo_Ctl, TextEdit).Text = 0.0
                    Else
                        CType(lo_Ctl, TextEdit).Text = ""
                    End If

                ElseIf TypeOf (lo_Ctl) Is TextBox Then

                    CType(lo_Ctl, TextBox).Text = ""

                ElseIf TypeOf (lo_Ctl) Is System.Windows.Forms.ComboBox Then

                    ' Se verifica la cantidad de elementos que tiene el combo
                    CType(lo_Ctl, System.Windows.Forms.ComboBox).SelectedIndex = -1

                ElseIf TypeOf (lo_Ctl) Is GridControl Then

                    ' Se limpia el control
                    Dim lo_dtb As DataTable = CType(lo_Ctl, GridControl).DataSource
                    lo_dtb.Rows.Clear()

                ElseIf TypeOf (lo_Ctl) Is uct_gridConBusqueda Then

                    CType(lo_Ctl, uct_gridConBusqueda).sub_limpiar()

                ElseIf TypeOf (lo_Ctl) Is uct_lstSeleccion Then

                    CType(lo_Ctl, uct_lstSeleccion).Value = ""
                    CType(lo_Ctl, uct_lstSeleccion).Texto = ""

                ElseIf TypeOf (lo_Ctl) Is XtraTabControl Then

                    ' Se recorre los tabs
                    For j As Integer = 0 To CType(lo_Ctl, XtraTabControl).TabPages.Count - 1

                        ' Se limpia el control
                        sub_limpiarControlesForm(CType(lo_Ctl, XtraTabControl).TabPages.Item(j).Controls)

                    Next

                End If

            Next


        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_manejarColorControlesPorModo(ByVal po_controles As System.Windows.Forms.Control.ControlCollection, ByVal pi_modo As Integer)
        Try

            ' Se recorre los controles del formulario
            For i As Integer = 0 To po_controles.Count - 1

                ' Se obtiene el control
                Dim lo_Ctl = po_controles.Item(i)

                If TypeOf (lo_Ctl) Is XtraTabControl Then

                    ' Se recorre los tabs
                    For j As Integer = 0 To CType(lo_Ctl, XtraTabControl).TabPages.Count - 1

                        ' Se limpia el control
                        sub_manejarColorControlesPorModo(CType(lo_Ctl, XtraTabControl).TabPages.Item(j).Controls, pi_modo)

                    Next

                Else

                    If TypeOf (lo_Ctl) Is TextBox Or TypeOf (lo_Ctl) Is TextEdit Or TypeOf (lo_Ctl) Is System.Windows.Forms.ComboBox Or TypeOf (lo_Ctl) Is MemoEdit Then

                        ' Se asigna el color al control de acuerdo al modo
                        If Not lo_Ctl.Tag Is Nothing Then

                            ' Se verifica que se haya ingresado una descripcion en la propiedad Tag
                            If lo_Ctl.Tag.ToString <> "" Then
                                If pi_modo = enm_modoForm.BUSCAR Then
                                    ' Se limpia los controles DateEdit
                                    If TypeOf (lo_Ctl) Is DateEdit Then
                                        CType(lo_Ctl, DateEdit).Text = ""
                                    End If

                                    lo_Ctl.BackColor = Color.LightYellow
                                    lo_Ctl.Enabled = True
                                Else
                                    lo_Ctl.BackColor = Color.White
                                End If

                            End If

                        End If

                    End If

                End If

            Next

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Function dtb_obtenerValoresBusqueda(ByVal po_controles As System.Windows.Forms.Control.ControlCollection)
        Try

            ' Se crea un dataTable que contendra los parametros de busqueda
            Dim lo_dtb As New DataTable

            ' Se crea las columnas del dataTable
            lo_dtb.Columns.Add("nom")
            lo_dtb.Columns.Add("val")

            ' Se ingresan los parametros de busqueda en el dataTable
            sub_obtenerValoresBusqueda(po_controles, lo_dtb)

            ' Se retorna el dataTable
            Return lo_dtb

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Private Sub sub_obtenerValoresBusqueda(ByVal po_controles As System.Windows.Forms.Control.ControlCollection, ByVal po_dtb As DataTable)
        Try

            ' Se recorre los controles del formulario
            For i As Integer = 0 To po_controles.Count - 1

                ' Se obtiene el control
                Dim lo_Ctl = po_controles.Item(i)

                If TypeOf (lo_Ctl) Is XtraTabControl Then

                    ' Se recorre los tabs
                    For j As Integer = 0 To CType(lo_Ctl, XtraTabControl).TabPages.Count - 1

                        ' Se limpia el control
                        sub_obtenerValoresBusqueda(CType(lo_Ctl, XtraTabControl).TabPages.Item(j).Controls, po_dtb)

                    Next

                Else

                    If TypeOf (lo_Ctl) Is TextBox Or TypeOf (lo_Ctl) Is TextEdit Or TypeOf (lo_Ctl) Is System.Windows.Forms.ComboBox Or TypeOf (lo_Ctl) Is MemoEdit Then

                        ' Se asigna el color al control de acuerdo al modo
                        If Not lo_Ctl.Tag Is Nothing Then

                            ' Se verifica que se haya ingresado una descripcion en la propiedad Tag
                            If lo_Ctl.Tag.ToString <> "" Then

                                ' Se verifica el valor obtenido del control
                                Dim lo_valor As Object = obj_obtValorControl(lo_Ctl)
                                If Not lo_valor Is Nothing Then
                                    If CStr(lo_valor).Trim <> "" And CStr(lo_valor).Trim <> "-1" And CStr(lo_valor).Trim <> "0" And CStr(lo_valor).Trim <> "0.00" Then

                                        ' Se añade una nueva fila al dataTable
                                        Dim lo_row As DataRow = po_dtb.NewRow
                                        lo_row.BeginEdit()
                                        lo_row("nom") = lo_Ctl.Tag.ToString
                                        lo_row("val") = lo_valor
                                        lo_row.EndEdit()
                                        po_dtb.Rows.Add(lo_row)

                                    End If
                                End If

                            End If

                        End If

                    End If

                End If

            Next

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Function int_obtenerNuevoNumFilaDetalle(ByVal po_dataTable As DataTable, ByVal ps_nomCampoLinea As String) As Integer
        Try

            ' Se verifica el numero de filas del dataTable
            If po_dataTable.Rows.Count = 0 Then
                Return 1
            Else
                Return po_dataTable.Rows(po_dataTable.Rows.Count - 1)(ps_nomCampoLinea) + 1
            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return -1
        End Try
    End Function

    Public Function bol_validarTipoCambio(ByVal pd_tipoCambio As Double) As Boolean
        Try

            ' Se verifica si el tipo de cambio es 0.00
            If pd_tipoCambio = 0.0 Then
                ' Debe ingresar el tipo de cambio para la fecha actual
                MsgBox("Debe ingresar el tipo de cambio para la fecha actual")
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return False
        End Try
    End Function

    Public Sub sub_ocultarColumnaGridView(ByVal po_gridView As GridView, ByVal ps_nomCol As String)
        Try

            ' Se oculta la columna del gridView
            po_gridView.Columns(ps_nomCol).Visible = False

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_asignarCabecerasGridView(ByVal po_gridView As GridView, ByVal ps_nomCol As String, ByVal ps_nuevaCabecera As String)
        Try

            If Not po_gridView.Columns(ps_nomCol) Is Nothing Then

                ' Se asigna la nueva cabecera a la columna
                po_gridView.Columns(ps_nomCol).Caption = ps_nuevaCabecera
                If ps_nomCol.ToLower.Contains("name") Or ps_nomCol.ToLower.Contains("nom") Then po_gridView.Columns(ps_nomCol).Width = 600 Else po_gridView.Columns(ps_nomCol).Width = 100

            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_asignarAnchoColGridView(ByVal po_gridView As GridView)
        Try

            ' Se recorre las columnas del gridView
            For i As Integer = 0 To po_gridView.Columns.Count - 1

                ' Se asigna el tamaño a las columnas
                If po_gridView.Columns(i).Name.ToLower.Contains("name") Or po_gridView.Columns(i).Name.ToLower.Contains("name") Or po_gridView.Columns(i).Name.ToLower.Contains("descr") Or po_gridView.Columns(i).Name.ToLower.Contains("cliente") Or po_gridView.Columns(i).Name.ToLower.Contains("nombre") Then
                    po_gridView.Columns(i).Width = 400
                Else
                    po_gridView.Columns(i).Width = 100
                End If


            Next

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_dsblOrderColumnasGrid(ByVal po_gridView As GridView)
        Try

            ' Se recorre las columnas del GridView
            For i As Integer = 0 To po_gridView.Columns.Count - 1

                ' Se deshabilita la opcion de Orden
                po_gridView.Columns(i).OptionsColumn.AllowSort = False

            Next

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_mostrarMsj(ByVal po_pnl As Panel, ByVal ps_mensaje As String, ByVal pi_tipoMsj As Integer)
        Try

            ' Se verifica si se recibio un mensaje
            If ps_mensaje.Trim <> "" Then

                ' Se crea un control Label para el mensaje
                Dim lo_lblMsj As New Label

                ' Se asigna las propiedades al Label y se añade al panel de mensaje
                po_pnl.Controls.Add(lo_lblMsj)
                lo_lblMsj.Location = New Point(1, 1)
                lo_lblMsj.Size = New Size(po_pnl.Width, 13)

                ' Se asigna el texto al Label
                lo_lblMsj.Text = ps_mensaje

                ' Se asigna los colores de acuerdo al tipo de mensaje
                If pi_tipoMsj = enm_tipoMsj.exito Then
                    lo_lblMsj.ForeColor = Color.Black
                    po_pnl.BackColor = Color.DarkSeaGreen
                    lo_lblMsj.Text = lo_lblMsj.Text
                ElseIf pi_tipoMsj = enm_tipoMsj.error_sap Then
                    lo_lblMsj.ForeColor = Color.White
                    po_pnl.BackColor = Color.Red
                    lo_lblMsj.Text = "sap: " & lo_lblMsj.Text
                ElseIf pi_tipoMsj = enm_tipoMsj.error_sis Then
                    lo_lblMsj.ForeColor = Color.White
                    po_pnl.BackColor = Color.Red
                    lo_lblMsj.Text = "sis: " & lo_lblMsj.Text
                ElseIf pi_tipoMsj = enm_tipoMsj.error_exc Then
                    lo_lblMsj.ForeColor = Color.White
                    po_pnl.BackColor = Color.Red
                    lo_lblMsj.Text = "exc: " & lo_lblMsj.Text
                ElseIf pi_tipoMsj = enm_tipoMsj.info Then
                    lo_lblMsj.ForeColor = Color.Black
                    po_pnl.BackColor = Color.LightSteelBlue
                End If

                ' Se obtiene el panel de mensaje
                o_pnlMsj = po_pnl

                ' Se asigna el tiempo que durara el mensaje
                i_seg = 5

                ' Se inicia el conteo de tiempo
                o_timer.Interval = 1000
                o_timer.Enabled = True

            End If

        Catch ex As Exception
        End Try
    End Sub

    Public Enum enm_tipoMsj
        exito = 0
        error_sap = 1
        error_sis = 2
        error_exc = 3
        info = 4
        error_val = 5
    End Enum

    Private Sub o_timer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_timer.Tick
        Try

            If i_seg > 0 Then

                ' Se hace visible el mensaje
                o_pnlMsj.Visible = True

                ' Se disminuye en 1 el contador de segundos
                i_seg = i_seg - 1

            Else

                ' Se oculta el mensaje
                o_pnlMsj.Visible = False

                ' Se detiene el Timer
                o_timer.Enabled = False

                ' Se resetea el panel
                o_pnlMsj.Controls.Clear()
                o_pnlMsj = Nothing

            End If

        Catch ex As Exception
        End Try
    End Sub

    Public Sub sub_asigValorControl(ByVal po_control As Control, ByVal po_valor As Object)
        Try

            Try

                ' Se verifica el tipo de control
                If TypeOf (po_control) Is DateEdit Then

                    ' Se asigna el valor al control
                    CType(po_control, DateEdit).Text = po_valor

                ElseIf TypeOf (po_control) Is CalcEdit Then

                    ' Se asigna el valor al control
                    CType(po_control, CalcEdit).Value = po_valor

                ElseIf TypeOf (po_control) Is TextEdit Then

                    ' Se asigna el valor al control
                    CType(po_control, TextEdit).Text = po_valor

                ElseIf TypeOf (po_control) Is TextBox Then

                    ' Se asigna el valor al control
                    CType(po_control, TextBox).Text = po_valor

                ElseIf TypeOf (po_control) Is Label Then

                    ' Se asigna el valor al control
                    CType(po_control, Label).Text = po_valor

                ElseIf TypeOf (po_control) Is MemoEdit Then

                    ' Se asigna el valor al control
                    CType(po_control, MemoEdit).Text = po_valor

                ElseIf TypeOf (po_control) Is CheckBox Then

                    ' Se verifica el valor recibido
                    If CStr(po_valor).ToLower = "y" Then
                        CType(po_control, CheckBox).Checked = True
                    Else
                        CType(po_control, CheckBox).Checked = False
                    End If

                ElseIf TypeOf (po_control) Is System.Windows.Forms.ComboBox Then

                    ' Se asigna el valor al control
                    CType(po_control, System.Windows.Forms.ComboBox).SelectedValue = po_valor

                ElseIf TypeOf (po_control) Is uct_gridConBusqueda Then

                    ' Se asigna el valor al control
                    CType(po_control, uct_gridConBusqueda).DataSource = po_valor
                    CType(po_control, uct_gridConBusqueda).sub_inicializar()

                ElseIf TypeOf (po_control) Is uct_lstSeleccion Then

                    ' Se asigna el valor al control
                    CType(po_control, uct_lstSeleccion).Value = po_valor
                    CType(po_control, uct_lstSeleccion).Texto = po_valor

                ElseIf TypeOf (po_control) Is GridControl Then

                    ' Se asigna el valor al control
                    CType(po_control, GridControl).DataSource = po_valor

                End If

            Catch ex As Exception
                MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            End Try

        Catch ex As Exception
        End Try
    End Sub

    Public Function obj_obtValorControl(ByVal po_control As Control) As Object
        Try
            ' Se verifica el tipo de control
            If TypeOf (po_control) Is DateEdit Then

                ' Se asigna el valor al control
                Return CType(po_control, DateEdit).Text

            ElseIf TypeOf (po_control) Is CalcEdit Then

                ' Se asigna el valor al control
                Return CType(po_control, CalcEdit).Value

            ElseIf TypeOf (po_control) Is TextEdit Then

                ' Se asigna el valor al control
                Return CType(po_control, TextEdit).Text

            ElseIf TypeOf (po_control) Is TextBox Then

                ' Se asigna el valor al control
                Return CType(po_control, TextBox).Text

            ElseIf TypeOf (po_control) Is MemoEdit Then

                ' Se asigna el valor al control
                Return CType(po_control, MemoEdit).Text

            ElseIf TypeOf (po_control) Is CheckBox Then

                ' Se verifica el valor del control
                If CType(po_control, CheckBox).Checked = True Then
                    Return "Y"
                Else
                    Return "N"
                End If

            ElseIf TypeOf (po_control) Is Label Then

                ' Se asigna el valor al control
                Return CType(po_control, Label).Text

            ElseIf TypeOf (po_control) Is System.Windows.Forms.ComboBox Then

                ' Se asigna el valor al control
                Dim lo_valor As Object = CType(po_control, System.Windows.Forms.ComboBox).SelectedValue

                ' Se se obtuvo valor del combo, se obtiene el texto
                If lo_valor Is Nothing Then
                    lo_valor = CType(po_control, System.Windows.Forms.ComboBox).Text
                End If

                ' Se retorna el valor
                Return lo_valor

            ElseIf TypeOf (po_control) Is GridControl Then

                ' Se asigna el valor al control
                Return CType(po_control, GridControl).DataSource

            ElseIf TypeOf (po_control) Is uct_gridConBusqueda Then

                ' Se obtiene el valor
                Return CType(po_control, uct_gridConBusqueda).DataSource

            ElseIf TypeOf (po_control) Is uct_lstSeleccion Then

                ' Se obtiene el valor
                Return CType(po_control, uct_lstSeleccion).Value

            Else

                Return Nothing

            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ctr_obtenerControl(ByVal ps_tag As String, ByVal po_controls As System.Windows.Forms.Control.ControlCollection) As Control
        Try
            ' Se recorre los controles para obtener aquel que tengo el mismo nombre en la propiedad Tag
            For Each lo_ctrl As Control In po_controls

                If TypeOf (lo_ctrl) Is XtraTabControl Then

                    ' Se recorre los tabs
                    For j As Integer = 0 To CType(lo_ctrl, XtraTabControl).TabPages.Count - 1

                        ' Se limpia el control
                        Dim lo_ctrlResult As Control
                        lo_ctrlResult = ctr_obtenerControl(ps_tag, CType(lo_ctrl, XtraTabControl).TabPages.Item(j).Controls)

                        ' Se verifica si se obtuvo algun control
                        If Not lo_ctrlResult Is Nothing Then
                            Return lo_ctrlResult
                        End If

                    Next

                End If

                If TypeOf (lo_ctrl) Is Panel Then

                    ' Se limpia el control
                    Dim lo_ctrlResult As Control
                    lo_ctrlResult = ctr_obtenerControl(ps_tag, CType(lo_ctrl, Panel).Controls)

                    ' Se verifica si se obtuvo algun control
                    If Not lo_ctrlResult Is Nothing Then
                        Return lo_ctrlResult
                    End If

                End If

                ' Se verifica si la propiedad Tag del control es igual al parametro recibido
                If Not lo_ctrl.Tag Is Nothing Then
                    If lo_ctrl.Tag.ToString = ps_tag Then
                        Return lo_ctrl
                    End If
                End If

            Next

            ' Si no se encontro el control
            Return Nothing

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub sub_asigAnchoCol(ByVal po_gridView As GridView)
        Try

            ' Se recorre las columnas del GridView
            For i As Integer = 0 To po_gridView.Columns.Count - 1

                ' Se deshabilita la opcion de Orden
                If po_gridView.Columns(i).Name.ToLower.Contains("descripcion") Then
                    po_gridView.Columns(i).Width = 200
                Else
                    po_gridView.Columns(i).Width = 100
                End If


            Next

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Function bol_asignarDatosForm(ByVal po_controles As System.Windows.Forms.Control.ControlCollection, ByVal po_dtbSelec As DataTable, Optional ByVal ps_tblDet As String = "", Optional ByVal po_dtbDet As DataTable = Nothing) As Boolean
        Try

            ' Se verifica que se haya seleccionado un registro
            If po_dtbSelec.Rows.Count > 0 Then

                ' Se recorre las columnas de la fila del DataTable con los datos seleccionados
                For Each lo_control As Control In po_controles

                    ' Se verifica que se haya ingresado un valor en la propiedad Tag del control
                    If Not lo_control.Tag Is Nothing Then

                        ' Se recorre las columnas del DataTable del registro seleccionado
                        For Each lo_col As DataColumn In po_dtbSelec.Columns

                            ' Se verifica que el nombre del control sea el mismo que el nombre de la columna actual del dataTable
                            If lo_control.Tag.ToString.ToLower = lo_col.ColumnName.ToLower Then

                                ' Se asigna el valor correspondiente al control de acuerdo al tipo
                                If TypeOf (lo_control) Is DateEdit Then

                                    CType(lo_control, DateEdit).Text = ModuleGlobalDatos.obj_esNuloBD(po_dtbSelec.Rows(0)(lo_col.ColumnName), Now)

                                ElseIf TypeOf (lo_control) Is CalcEdit Then

                                    CType(lo_control, CalcEdit).Value = ModuleGlobalDatos.obj_esNuloBD(po_dtbSelec.Rows(0)(lo_col.ColumnName), 0.0)
                                ElseIf TypeOf (lo_control) Is TextEdit Then

                                    CType(lo_control, TextEdit).Text = ModuleGlobalDatos.obj_esNuloBD(po_dtbSelec.Rows(0)(lo_col.ColumnName).ToString, "")

                                ElseIf TypeOf (lo_control) Is Label Then

                                    CType(lo_control, Label).Text = ModuleGlobalDatos.obj_esNuloBD(po_dtbSelec.Rows(0)(lo_col.ColumnName), "")

                                ElseIf TypeOf (lo_control) Is System.Windows.Forms.ComboBox Then

                                    CType(lo_control, System.Windows.Forms.ComboBox).SelectedValue = ModuleGlobalDatos.obj_esNuloBD(po_dtbSelec.Rows(0)(lo_col.ColumnName), "")

                                ElseIf TypeOf (lo_control) Is System.Windows.Forms.Panel Then

                                    bol_asignarDatosForm(CType(lo_control, System.Windows.Forms.Panel).Controls, po_dtbSelec, ps_tblDet, po_dtbDet)

                                End If

                            End If

                            ' Se verifica si el control tiene el mismo nombre que la tabla asignada como Detalle
                            If lo_control.Tag.ToString.ToLower = ps_tblDet.ToLower Then

                                ' Se asigna el DataTable del detalle si el control es GridControl
                                If TypeOf (lo_control) Is GridControl Then
                                    'If CType(lo_control, GridControl).DataSource Is Nothing And Not po_dtbDet Is Nothing Then
                                    CType(lo_control, GridControl).DataSource = po_dtbDet
                                    'End If
                                End If

                            End If

                        Next

                    End If

                    ' Se verifica si el control es XtraTabControl
                    If TypeOf (lo_control) Is XtraTabControl Then

                        ' Se recorre los tabs
                        For j As Integer = 0 To CType(lo_control, XtraTabControl).TabPages.Count - 1

                            ' Se limpia el control
                            bol_asignarDatosForm(CType(lo_control, XtraTabControl).TabPages.Item(j).Controls, po_dtbSelec, ps_tblDet, po_dtbDet)

                        Next

                    End If

                Next

                ' Se retorna un indicador para señalar que se selecciono una fila del listado
                Return True

            Else

                ' Si no se selecciono ningun valor del listado
                Return False

            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return False
        End Try
    End Function

    Public Sub sub_inicializarUgc(ByVal po_ugc As uct_gridConBusqueda)
        Try

            ' Se inicializa el control
            po_ugc.sub_inicializar()

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_incrementarProgressBar(ByVal po_progressBar As System.Windows.Forms.ProgressBar)
        Try

            ' Se icrementa el progressBar
            If Not po_progressBar Is Nothing Then
                po_progressBar.Value = po_progressBar.Value + 1
            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_resetProgressBar(ByVal po_progressBar As System.Windows.Forms.ProgressBar)
        Try

            ' Se reinicia el progressBar
            If Not po_progressBar Is Nothing Then po_progressBar.Value = 0

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_habilitacionControl(ByVal po_control As Control, ByVal pb_habilitar As Boolean)
        Try

            ' Se verifica el tipo de control
            If TypeOf (po_control) Is uct_gridConBusqueda Then
                CType(po_control, uct_gridConBusqueda).conMenu = pb_habilitar
            Else
                po_control.Enabled = pb_habilitar
            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Enum enm_tipoControlRptryItem
        CHECKBOX = 0
        DATEEDIT = 1
    End Enum

End Module
