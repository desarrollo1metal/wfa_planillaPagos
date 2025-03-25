Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraTab
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Reflection
Imports DevExpress.Utils.Menu
Imports System.Windows.Forms

Public Class frmBuscar

    ' Se declara los atributos de la clase
    Private o_dvwDatos As DataView
    Private o_dtbDatos As DataTable
    Private s_buscarPor1 As String
    Private s_buscarPor2 As String
    Private o_dtbSelec As New DataTable
    Private o_dtbOcultarCol As New DataTable
    Private o_dtbCabCol As New DataTable
    Private o_dtbDetalle As New DataTable
    Private s_tblDet As String
    Private s_tblId As String

    ' Variables para el subReporte
    Private o_lstObjSR As New List(Of Object)
    Private o_dtbParamSR As New DataTable
    Private o_lstColLlavesSR As New List(Of List(Of String))
    Private i_ancho As Integer
    Private i_altura As Integer
    Private b_tieneDet As Boolean = False

    ' Datos del detalle
    Private o_objetoDet As Object
    Private s_metodoDet As String
    Private s_colLlaveDet As String
    Private o_lstColLlavesDet As New List(Of List(Of String))

    ' Listado de orden de seleccion
    Private o_lstOrdenSelec As New List(Of Integer)
    Private b_indShift As Boolean = False

    Private i_contador As Integer = 0

    ' Propiedades
    Public Property objetoDet() As Object
        Get
            Return o_objetoDet
        End Get
        Set(ByVal value As Object)
            o_objetoDet = value
        End Set
    End Property

    Public Property metodoDet() As String
        Get
            Return s_metodoDet
        End Get
        Set(ByVal value As String)
            s_metodoDet = value
        End Set
    End Property

    Public Property colLlaveDet() As String
        Get
            Return s_colLlaveDet
        End Get
        Set(ByVal value As String)
            s_colLlaveDet = value
        End Set
    End Property

    Public Property dtbSelec() As DataTable
        Get
            Return o_dtbSelec
        End Get
        Set(ByVal value As DataTable)
            o_dtbSelec = value
        End Set
    End Property

    Public Property dtbCabCol() As DataTable
        Get
            Return o_dtbCabCol
        End Get
        Set(ByVal value As DataTable)
            o_dtbCabCol = value
        End Set
    End Property

    Public Property dtbOcultarCol() As DataTable
        Get
            Return o_dtbOcultarCol
        End Get
        Set(ByVal value As DataTable)
            o_dtbOcultarCol = value
        End Set
    End Property

    Public Property detalle() As DataTable
        Get
            Return o_dtbDetalle
        End Get
        Set(ByVal value As DataTable)
            o_dtbDetalle = value
        End Set
    End Property

    Public Property tblDet() As String
        Get
            Return s_tblDet
        End Get
        Set(ByVal value As String)
            s_tblDet = value
        End Set
    End Property

    Public Property tblId() As String
        Get
            Return s_tblId
        End Get
        Set(ByVal value As String)
            s_tblId = value
        End Set
    End Property

    ' Constructor
    Public Sub New(ByVal po_dtbDatos As DataTable, ByVal ps_buscarPor1 As String, ByVal ps_valorBusq As String, Optional ByVal ps_tblDet As String = "", Optional ByVal ps_tblId As String = "")
        Try
            ' Se inicializa los componentes
            InitializeComponent()

            ' Se obtiene el dataView del DataTable
            o_dtbDatos = po_dtbDatos
            o_dvwDatos = po_dtbDatos.DefaultView

            ' Se ajusta el tamaño de la ventana de acuerdo al número de columnas
            sub_ajustarTamano()

            ' Se obtiene los campos de condición para el filtro del GridControl
            s_buscarPor1 = ps_buscarPor1
            's_buscarPor2 = ps_buscarPor2
            s_tblDet = ps_tblDet
            s_tblId = ps_tblId

            ' Se asigna el DataTable como DataSource del GridControl
            Me.gctDatos.DataSource = o_dvwDatos
            o_dvwDatos.RowFilter = String.Empty
            txtBuscar.Text = ps_valorBusq
            sub_establecerFiltroGctlPrincipal()

            ' Se inicializa el dataTable de seleccion
            sub_iniDtbSelec()

            ' Se inicializa el dataTable de columnas a ocultar
            sub_iniDtbColOcultar()

            ' Se inicializa el dataTable de cabeceras de columnas
            sub_iniDtbCabCol()

            ' Se inicializa el dataTable de datos de opciones de subreporte
            sub_iniDtbParamSR()

            ' Se asigna el ancho de columnas adecuado
            sub_asignarAnchoColGridView(GridView1)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_iniDtbColOcultar()
        Try

            ' Se inicializa las columnas del DataTable de columnas del GridView a mostrar
            o_dtbOcultarCol.TableName = "tblOcultar"
            o_dtbOcultarCol.Columns.Add("nomCol")

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_iniDtbCabCol()
        Try

            ' Se inicializa las columnas del DataTable de columnas del GridView a mostrar
            o_dtbCabCol.TableName = "tblCabCol"
            o_dtbCabCol.Columns.Add("nomCol")
            o_dtbCabCol.Columns.Add("cabCol")

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_iniDtbParamSR()
        Try

            ' Se inicializa las columnas del DataTable de columnas del GridView a mostrar
            o_dtbParamSR.TableName = "tblParamSR"
            o_dtbParamSR.Columns.Add("idOpc")
            o_dtbParamSR.Columns.Add("nomOpc")
            o_dtbParamSR.Columns.Add("nomMetod")
            o_dtbParamSR.Columns.Add("colLlave")
            o_dtbParamSR.Columns.Add("nomMetodDet")
            o_dtbParamSR.Columns.Add("colLlaveDet")

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub sub_obtenerSeleccion()
        Try

            ' Se obtiene el dataTable del grid
            'Dim lo_dtb As DataTable = CType(gctDatos.DataSource, DataView).Table
            Dim lo_dvw As DataView = CType(gctDatos.DataSource, DataView)
            'CType(gctDatos.FocusedView.DataSource, DataView)
            Dim lo_grid As GridView = gctDatos.FocusedView

            ' Se recorre las filas seleccionadas
            Dim li_selcs As Integer() = lo_grid.GetSelectedRows

            ' Se declara un arreglo para obtener los indices seleccionados de la lista de orden de seleccion
            Dim li_ordnSel As Integer() = o_lstOrdenSelec.ToArray

            ' Se verifica si existe objetos en el arreglo de orden de seleccion
            If li_ordnSel.Length = 0 Then
                li_ordnSel = li_selcs
            End If

            ' Se recorre los indices
            For Each i As Integer In li_ordnSel
                'For Each i As Integer In li_selcs

                ' Se verifica que el indicce actual existe en el arreglo de indices seleccionados
                Dim lb_existe As Boolean = Array.Exists(li_selcs, Function(element)
                                                                      Return element.Equals(i)
                                                                  End Function)

                If lb_existe = False Then
                    Continue For
                End If

                ' Se crea una nueva linea
                Dim lo_row As DataRow = o_dtbSelec.NewRow
                lo_row.BeginEdit()

                ' Se obtiene el rowHandle de las filas seleccionadas
                'Dim li_rowHdl As Integer = GridView1.GetRowHandle(i)

                ' Se recorre las columnas del dataTable de seleccion
                For Each lo_col As DataColumn In o_dtbSelec.Columns

                    ' Se asigna el valor a la columna correspondiente                   
                    lo_row(lo_col.ColumnName) = GridView1.GetRow(i)(lo_col.ColumnName)

                Next
                lo_row.EndEdit()

                ' Se añade la linea al dataTable
                o_dtbSelec.Rows.Add(lo_row)
            Next

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub sub_iniDtbSelec()
        Try

            ' Se inicializa el dataTable de seleccion
            o_dtbSelec.TableName = "dtbSelec"

            ' Se recorre las columnas de dataTable del GridControl para inicializar el dataTable de selección
            For Each lo_col As DataColumn In o_dtbDatos.Columns

                ' Se crea las columnas del dataTable de seleccion
                o_dtbSelec.Columns.Add(lo_col.ColumnName)

            Next

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Function obj_obtenerValorSel(ByVal ps_nomCampo As String) As Object
        Try

            ' Se retorna el valor del campo indicado
            If o_dtbSelec.Rows.Count > 0 Then
                If Not o_dtbSelec.Columns.Item(ps_nomCampo) Is Nothing Then
                    Return ModuleGlobalDatos.obj_esNuloBD(o_dtbSelec.Rows(0)(ps_nomCampo))
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function obj_obtenerValorSel(ByVal ps_nomCampo As String, ByVal pi_indice As Integer) As Object
        Try

            ' Se retorna el valor del campo indicado
            If o_dtbSelec.Rows.Count > 0 Then
                Return ModuleGlobalDatos.obj_esNuloBD(o_dtbSelec.Rows(pi_indice)(ps_nomCampo))
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

#Region "EventosCtrlForm"

    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        Try

            ' Se obtiene los datos seleccionados
            sub_obtenerSeleccion()

            ' Se obtiene el detalle de la fila seleccionada
            sub_obtenerDatosDetalle()

            ' Se cierra el formulario
            Me.Close()

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub GridView1_PopupMenuShowing(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView1.PopupMenuShowing
        sub_mostrarMenu(e)
    End Sub

    Private Sub GridView1_RowCellClick(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles GridView1.RowCellClick
        ' Eventos para guardar el orden de la seleccion
        'sub_guardarOrdenSelec(e)
    End Sub

    Private Sub GridView1_RowClick(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        ' Eventos para guardar el orden de la seleccion
        sub_guardarOrdenSelec(e)
    End Sub

    Private Sub GridView1_BeforeLeaveRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles GridView1.BeforeLeaveRow
        'sub_actDeselec(e)
    End Sub

    Private Sub GridView1_ShowingEditor(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GridView1.ShowingEditor
        Try

            ' Se cancela la opción para editar la columna
            e.Cancel = True

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try
            ' Se cierra el formulario
            Me.Close()
        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub gctDatos_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gctDatos.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                e.Handled = True
                btnSelec_Click(sender, e)
            End If
            If e.KeyCode = Keys.ShiftKey Then
                b_indShift = True
            End If
        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub gctDatos_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gctDatos.KeyUp
        If e.KeyCode = Keys.ShiftKey Then
            b_indShift = False
        End If
    End Sub

    Private Sub btnSelec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelec.Click
        Try

            ' Se obtiene los datos seleccionados
            sub_obtenerSeleccion()

            ' Se obtiene el detalle de la fila seleccionada
            sub_obtenerDatosDetalle()

            ' Se cierra el formulario
            Me.Close()

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub txtBuscar_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBuscar.EditValueChanged
        sub_establecerFiltroGctlPrincipal()
    End Sub

    Private Sub txtBuscar_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBuscar.KeyDown
        sub_establecerFocusGridControlPrinc(e)
    End Sub

#End Region

#Region "manejoColumnasGrid"

    Public Sub sub_adcColumnOcultar(ByVal ps_nomCol As String)
        Try

            ' Se adiciona al dataTable las columnas a mostrar
            Dim lo_row As DataRow = o_dtbOcultarCol.NewRow
            lo_row.BeginEdit()
            lo_row(0) = ps_nomCol
            lo_row.EndEdit()
            o_dtbOcultarCol.Rows.Add(lo_row)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_ocultarColumnas()
        Try

            ' Se recorre el dataTable de columnas a mostrar
            For Each lo_row As DataRow In o_dtbOcultarCol.Rows

                ' Se oculta la columna
                GridView1.Columns(lo_row(0).ToString).Visible = False

            Next

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_adcCabColumn(ByVal ps_nomCol As String, ByVal ps_cabCol As String)
        Try

            ' Se adiciona al dataTable las columnas a mostrar
            Dim lo_row As DataRow = o_dtbCabCol.NewRow
            lo_row.BeginEdit()
            lo_row(0) = ps_nomCol
            lo_row(1) = ps_cabCol
            lo_row.EndEdit()
            o_dtbCabCol.Rows.Add(lo_row)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_asigCabColumnas()
        Try

            ' Se recorre el dataTable de columnas a mostrar
            For Each lo_row As DataRow In o_dtbCabCol.Rows

                ' Se asigna las cabeceras correspondientes
                ModuleGlobalForm.sub_asignarCabecerasGridView(GridView1, lo_row(0).ToString, lo_row(1).ToString)

            Next

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_ocultColumnSinCabecera()
        Try

            ' Se recorre las columnas del GridView para ver cuales no cuentan con una cabecera asignada
            For Each lo_col As GridColumn In GridView1.Columns

                ' Se verifica la cabecera asignada de la columna
                If lo_col.Caption = "" Then

                    ' Se oculta la columna
                    lo_col.Visible = False

                End If

            Next

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub sub_ajustarTamano()
        Try

            ' Se obtiene el número de columnas del dataTable
            Dim li_numCols As Integer = o_dtbDatos.Columns.Count

            ' Se ajusta el tamaño de acuerdo al numero de columnas
            Dim li_widht As Integer = 100 * li_numCols

            ' Se verifica si el tamaño calculado excede el maximo permitido para este add-on
            If li_widht >= 1320 Then
                Me.Width = 1320
            ElseIf li_widht < Me.Width Then
                Me.Width = Me.Width
            Else
                Me.Width = li_widht
            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

#Region "asigDatosForm"

    Private Sub sub_obtenerDatosDetalle()
        Try

            ' Se verifica si se especifico los datos del detalle
            'If Not s_tblDet.Trim = "" And Not s_tblId.Trim = "" Then

            '    ' Se obtiene los datos del detalle desde la base de datos
            '    o_dtbDetalle = CNBusqueda.dtb_obtenerDetalleObj(s_tblDet, obj_obtenerValorSel(s_tblId))

            'End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Function bol_asignarDatosForm(ByVal po_controles As System.Windows.Forms.Control.ControlCollection) As Boolean
        Try

            ' Se verifica que se haya seleccionado un registro
            If o_dtbSelec.Rows.Count > 0 Then

                ' Se recorre las columnas de la fila del DataTable con los datos seleccionados
                For Each lo_control As Control In po_controles

                    ' Se verifica que se haya ingresado un valor en la propiedad Tag del control
                    If Not lo_control.Tag Is Nothing Then

                        ' Se recorre las columnas del DataTable del registro seleccionado
                        For Each lo_col As DataColumn In o_dtbSelec.Columns

                            ' Se verifica que el nombre del control sea el mismo que el nombre de la columna actual del dataTable
                            If lo_control.Tag.ToString.ToLower = lo_col.ColumnName.ToLower Then

                                ' Se asigna el valor correspondiente al control de acuerdo al tipo
                                If TypeOf (lo_control) Is DateEdit Then

                                    CType(lo_control, DateEdit).Text = ModuleGlobalDatos.obj_esNuloBD(dtbSelec.Rows(0)(lo_col.ColumnName), Now)

                                ElseIf TypeOf (lo_control) Is CalcEdit Then

                                    CType(lo_control, CalcEdit).Value = ModuleGlobalDatos.obj_esNuloBD(dtbSelec.Rows(0)(lo_col.ColumnName), 0.0)
                                ElseIf TypeOf (lo_control) Is TextEdit Then

                                    CType(lo_control, TextEdit).Text = ModuleGlobalDatos.obj_esNuloBD(dtbSelec.Rows(0)(lo_col.ColumnName).ToString, "")

                                ElseIf TypeOf (lo_control) Is Label Then

                                    CType(lo_control, Label).Text = ModuleGlobalDatos.obj_esNuloBD(dtbSelec.Rows(0)(lo_col.ColumnName), "")

                                ElseIf TypeOf (lo_control) Is System.Windows.Forms.ComboBox Then

                                    CType(lo_control, System.Windows.Forms.ComboBox).SelectedValue = ModuleGlobalDatos.obj_esNuloBD(dtbSelec.Rows(0)(lo_col.ColumnName), "")

                                End If

                            End If

                            ' Se verifica si el control tiene el mismo nombre que la tabla asignada como Detalle
                            If lo_control.Tag.ToString.ToLower = s_tblDet.ToLower Then

                                ' Se asigna el DataTable del detalle si el control es GridControl
                                If TypeOf (lo_control) Is GridControl Then

                                    CType(lo_control, GridControl).DataSource = o_dtbDetalle

                                End If

                            End If

                        Next

                    End If

                    ' Se verifica si el control es XtraTabControl
                    If TypeOf (lo_control) Is XtraTabControl Then

                        ' Se recorre los tabs
                        For j As Integer = 0 To CType(lo_control, XtraTabControl).TabPages.Count - 1

                            ' Se limpia el control
                            bol_asignarDatosForm(CType(lo_control, XtraTabControl).TabPages.Item(j).Controls)

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
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return False
        End Try
    End Function

#End Region

    Public Function obj_obtValorColSel(ByVal ps_nomColum As String)
        Try

            ' Se verifica que se haya seleccionado un registro
            If o_dtbSelec.Rows.Count > 0 Then

                ' Se retorna el valor
                Return ModuleGlobalDatos.obj_esNuloBD(dtbSelec.Rows(0)(ps_nomColum), New Object)

            Else
                Return New Object
            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function int_contarSelecc() As Integer
        Try

            ' Se retorna la cantidad de filas seleccionadas
            Return o_dtbSelec.Rows.Count

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return -1
        End Try
    End Function

    Public Sub sub_habilitarMultiSelec(ByVal pb_habl As Boolean)
        Try

            ' Se habilita o deshabilita la multiseleccion del grid
            GridView1.OptionsSelection.MultiSelect = pb_habl

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#Region "MenuClickDerecho"

    Public Sub sub_anadirOpcionSR(ByVal ps_idOpcion As String, ByVal ps_nomOpcion As String, ByVal po_objetoSR As Object, ByVal ps_nomMetodo As String, ByVal ps_colLlave As String, Optional ByVal ps_nomMetodoDet As String = "", Optional ByVal ps_colLlaveDet As String = "")
        Try

            ' Se añade los datos de la opción a la Lista y al DataTable
            o_lstObjSR.Add(po_objetoSR)
            Dim lo_row As DataRow = o_dtbParamSR.NewRow
            lo_row.BeginEdit()
            lo_row("idOpc") = ps_idOpcion
            lo_row("nomOpc") = ps_nomOpcion
            lo_row("nomMetod") = ps_nomMetodo
            lo_row("colLlave") = ps_colLlave
            o_lstColLlavesSR.Add(Nothing)
            lo_row("nomMetodDet") = ps_nomMetodoDet
            lo_row("colLlaveDet") = ps_colLlaveDet
            ' Se llena la posicion en el listado de parametros por metodo
            o_lstColLlavesDet.Add(Nothing)
            lo_row.EndEdit()
            o_dtbParamSR.Rows.Add(lo_row)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_anadirOpcionSR(ByVal ps_idOpcion As String, ByVal ps_nomOpcion As String, ByVal po_objetoSR As Object, ByVal ps_nomMetodo As String, ByVal ps_colLlave As List(Of String), Optional ByVal ps_nomMetodoDet As String = "", Optional ByVal ps_colLlaveDet As List(Of String) = Nothing)
        Try

            ' Se añade los datos de la opción a la Lista y al DataTable
            o_lstObjSR.Add(po_objetoSR)
            Dim lo_row As DataRow = o_dtbParamSR.NewRow
            lo_row.BeginEdit()
            lo_row("idOpc") = ps_idOpcion
            lo_row("nomOpc") = ps_nomOpcion
            lo_row("nomMetod") = ps_nomMetodo
            o_lstColLlavesSR.Add(ps_colLlave)
            'lo_row("colLlave") = ps_colLlave
            lo_row("nomMetodDet") = ps_nomMetodoDet
            o_lstColLlavesDet.Add(ps_colLlaveDet)
            'lo_row("colLlaveDet") = ps_colLlaveDet
            lo_row.EndEdit()
            o_dtbParamSR.Rows.Add(lo_row)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub sub_mostrarMenu(ByVal e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs)
        Try
            ' Se verifica si se realizo click derecho en una fila
            If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
                Dim rowHandle As Integer = e.HitInfo.RowHandle

                ' Se limpia las opciones del menu
                e.Menu.Items.Clear()

                If o_dtbParamSR.Rows.Count > 0 Then

                    ' Se recorre las opciones de SubReporte para crear el menu
                    For i As Integer = 0 To o_dtbParamSR.Rows.Count - 1

                        Dim item As DXMenuItem = sub_crearMenu(GridView1, rowHandle, i, o_dtbParamSR.Rows(i)("nomOpc"))
                        item.BeginGroup = True
                        e.Menu.Items.Add(item)

                    Next

                End If

            End If
        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    ' Create a check menu item that triggers the Boolean AllowCellMerge option.
    Function sub_crearMenu(ByVal view As GridView, ByVal rowHandle As Integer, ByVal pi_indice As Integer, ByVal ps_nomOpc As String) As DXMenuCheckItem
        Dim checkItem As New DXMenuCheckItem(ps_nomOpc, False, Nothing, AddressOf sub_mostrarReporte)
        checkItem.Tag = pi_indice
        Return checkItem
    End Function

    'The handler for the MergingEnabled menu item
    Private Sub sub_mostrarReporte(ByVal sender As Object, ByVal e As EventArgs)
        'Try
        '    ' Se obtiene la opcion seleccionada
        '    Dim lo_item As DXMenuCheckItem = CType(sender, DXMenuItem)

        '    ' Se obtiene el ID de la opcion seleccionada
        '    Dim li_indice As Integer = CInt(lo_item.Tag)

        '    ' Se obtiene el objeto de la lista para realizar la operacion especificada
        '    For i As Integer = 0 To o_dtbParamSR.Rows.Count - 1

        '        ' Se verifica el indice
        '        If i = li_indice Then

        '            ' Se obtiene el objeto que contiene el metodo de donde se generara el reporte
        '            Dim lo_objeto As Object = o_lstObjSR.Item(i)

        '            ' Se obtiene el metodo del reporte
        '            Dim lo_method As MethodInfo = lo_objeto.GetType.GetMethod(o_dtbParamSR.Rows(i)("nomMetod").ToString)

        '            ' Se ejecuta el metodo y se obtiene los datos del reporte
        '            Dim lo_dtb As DataTable
        '            ' Se verifica si se recibió una sola columna de enlace al subreporte o mas de una
        '            If Not o_lstColLlavesSR.Item(i) Is Nothing Then
        '                ' Se obtiene la lista de columnas llave del subreporte actual
        '                Dim lo_lstColsLlaveSR As List(Of String) = CType(o_lstColLlavesSR.Item(i), List(Of String))

        '                ' Se construye el arreglo de objetos que contendrá los parametros del metodo a invocar
        '                Dim lo_params(lo_lstColsLlaveSR.Count - 1) As Object
        '                For j As Integer = 0 To lo_lstColsLlaveSR.Count - 1
        '                    lo_params(j) = GridView1.GetFocusedRowCellValue(lo_lstColsLlaveSR.Item(j).ToString).ToString 'GridView1.GetFocusedRowCellValue(o_dtbParamSR.Rows(i)(lo_lstColsLlaveSR.Item(j).ToString).ToString).ToString
        '                Next

        '                ' Se invoca al metodo
        '                lo_dtb = CType(lo_method.Invoke(lo_objeto, lo_params), DataTable)

        '            Else
        '                lo_dtb = CType(lo_method.Invoke(lo_objeto, New Object() {GridView1.GetFocusedRowCellValue(o_dtbParamSR.Rows(i)("colLlave").ToString).ToString}), DataTable)
        '            End If

        '            ' Se verifica si se le asigno metodo y enlace para la grilla de detalle
        '            Dim lb_tieneDet As Boolean = False
        '            If o_dtbParamSR.Rows(i)("nomMetodDet").ToString.Trim <> "" And o_dtbParamSR.Rows(i)("colLlaveDet").ToString.Trim <> "" Then
        '                lb_tieneDet = True
        '            ElseIf o_dtbParamSR.Rows(i)("nomMetodDet").ToString.Trim <> "" And o_lstColLlavesDet.Count > 0 Then
        '                lb_tieneDet = True
        '            End If

        '            ' Se declara una instancia del formulario de reporte
        '            Dim lo_frmRep As New frmReporte(lo_dtb, "", "", lb_tieneDet)
        '            lo_frmRep.Text = o_dtbParamSR.Rows(i)("nomOpc").ToString

        '            ' Se obtiene el ancho y la altura del formulario
        '            i_ancho = Me.Width
        '            i_altura = Me.Height

        '            ' Se asigna al nuevo formulario la mitad del tamaño que el formulario actual
        '            If lb_tieneDet = False Then
        '                lo_frmRep.Width = i_ancho / 2
        '                lo_frmRep.Height = i_altura / 2
        '                lo_frmRep.gctlPrinc.Width = ((Me.gctDatos.Width - 50) / 2)
        '                lo_frmRep.gctlPrinc.Height = (Me.gctDatos.Height - 100) / 2
        '            Else
        '                If Not o_lstColLlavesDet.Item(i) Is Nothing Then

        '                    ' Se asigna el detalle del reporte
        '                    lo_frmRep.objetoDet = lo_objeto
        '                    lo_frmRep.metodoDet = o_dtbParamSR.Rows(i)("nomMetodDet").ToString
        '                    lo_frmRep.ColLlavesDet = o_lstColLlavesDet.Item(i)
        '                    lo_frmRep.sub_asignarDatosDetalle()

        '                Else

        '                    ' Se asigna el detalle del reporte
        '                    lo_frmRep.objetoDet = lo_objeto
        '                    lo_frmRep.metodoDet = o_dtbParamSR.Rows(i)("nomMetodDet").ToString
        '                    lo_frmRep.colLlaveDet = o_dtbParamSR.Rows(i)("colLlaveDet").ToString
        '                    lo_frmRep.sub_asignarDatosDetalle()

        '                End If
        '            End If

        '            ' Se muestra el formulario 
        '            lo_frmRep.Show()

        '            ' Se finaliza el for
        '            Exit For

        '        End If

        '    Next

        'Catch ex As Exception
        '    MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        'End Try
        ''Dim item As DXMenuCheckItem = CType(sender, DXMenuItem)
        ''Dim info As RowInfo = CType(item.Tag, RowInfo)
        ''info.View.OptionsView.AllowCellMerge = item.Checked
    End Sub

#End Region

#Region "FiltrarPor"

    Private Sub sub_establecerFocusGridControlPrinc(ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Down Then
                e.Handled = True
                Me.gctDatos.Focus()
            End If
        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub sub_establecerFiltroGctlPrincipal()
        Try
            ' Se asigna los campos de filtro del GridControl
            If Me.txtBuscar.Text = "" Then
                o_dvwDatos.RowFilter = String.Empty
            End If
            o_dvwDatos.RowFilter = str_obtNomColumnFiltro()
        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Function str_obtNomColumnFiltro() As String
        Try

            ' Se declara una variable para el filtro
            Dim ls_rowFilter As String = ""

            ' Se verifica si hay columnas ordenadas
            If GridView1.SortedColumns.Count > 0 Then

                ' Se obtiene la columna de orden
                Dim lo_gridColumn As GridColumn = GridView1.SortedColumns.Item(0)

                ' Se verifica si la columna actual fue ordenada
                s_buscarPor1 = lo_gridColumn.FieldName

                ' Se verifica si se indico el filtro especial
                Dim ls_filtroEsp As String = ""
                If Me.txtBuscar.Text.Trim <> "" And Strings.Left(Me.txtBuscar.Text.Trim, 1) = "*" Then
                    ls_filtroEsp = "%"
                End If

                ' Se verifica el tipo de datos de la columna
                If (lo_gridColumn.ColumnType) Is GetType(String) Then
                    ls_rowFilter = s_buscarPor1 & " LIKE '" & ls_filtroEsp & Me.txtBuscar.Text.Trim & "%'"
                Else
                    ls_rowFilter = "convert(" & s_buscarPor1 & ", System.String)" & " like '" & ls_filtroEsp & Me.txtBuscar.Text.Trim & "%'"
                End If

            Else

                ' Se verifica si existe la columna indicada como filtro
                If Not GridView1.Columns(s_buscarPor1) Is Nothing Then

                    ' Se obtiene la columna de orden
                    Dim lo_gridColumn As GridColumn = GridView1.Columns(s_buscarPor1)

                    ' Se verifica si se indico el filtro especial
                    Dim ls_filtroEsp As String = ""
                    If Me.txtBuscar.Text.Trim <> "" And Strings.Left(Me.txtBuscar.Text.Trim, 1) = "*" Then
                        ls_filtroEsp = "%"
                    End If

                    ' Se verifica el tipo de datos de la columna
                    If (lo_gridColumn.ColumnType) Is GetType(String) Then
                        ls_rowFilter = s_buscarPor1 & " LIKE '" & ls_filtroEsp & Me.txtBuscar.Text.Trim & "%'"
                    Else
                        ls_rowFilter = "convert(" & s_buscarPor1 & ", System.String)" & " like '" & ls_filtroEsp & Me.txtBuscar.Text.Trim & "%'"
                    End If

                End If

            End If

            ' Se retorna la cadena de filtro para los datos del gridview
            Return ls_rowFilter

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return ""
        End Try
    End Function

#End Region

#Region "GuardarOrdenSelec"

    Private Sub sub_guardarOrdenSelec(ByVal e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs)
        Try
            'i_contador = i_contador + 1
            'Label1.Text = i_contador
            ' Se verifica si la seleccion se realizo con la tecla shift presionada
            If b_indShift = True Then

                ' Se limpie el listado de indices ordenados
                o_lstOrdenSelec.Clear()

                ' Se obtiene los indices de las celdas seleccionadas
                Dim lo_grid As GridView = gctDatos.FocusedView
                Dim li_selcs As Integer() = lo_grid.GetSelectedRows

                ' Se ingresa los indices obtenidos en la lista de indices ordenados
                o_lstOrdenSelec = li_selcs.ToList

            Else

                ' Se obtiene el indice de la fila seleccionada
                Dim li_iSelec As Integer = e.RowHandle

                ' Se obtiene un arreglo de la lista de orden para hacer las verificaciones correspondientes
                Dim li_orden As Integer() = o_lstOrdenSelec.ToArray

                ' Se obtiene las celdas seleccionadas
                Dim lo_grid As GridView = gctDatos.FocusedView
                Dim li_selcs As Integer() = lo_grid.GetSelectedRows

                ' Se verifica si solo hay una fila seleccionada
                If li_selcs.Length = 1 Then

                    ' Se limpia el listado de orden de seleccion
                    o_lstOrdenSelec.Clear()

                    ' Se añade el indice seleccionado al listado
                    o_lstOrdenSelec.Add(li_selcs(0))

                ElseIf li_selcs.Length = 2 Then ' Si hay dos filas seleccionadas

                    ' Se limpia el listado de orden de seleccion
                    o_lstOrdenSelec.Clear()

                    ' Se ingresa los indices al listado
                    If li_selcs(0) = li_iSelec Then
                        o_lstOrdenSelec.Add(li_selcs(1))
                        o_lstOrdenSelec.Add(li_iSelec)
                    Else
                        o_lstOrdenSelec.Add(li_selcs(0))
                        o_lstOrdenSelec.Add(li_iSelec)
                    End If

                ElseIf li_selcs.Length = 0 Then ' Si no hay filas seleccionadas

                    ' Se limpia el listado de orden de seleccion
                    o_lstOrdenSelec.Clear()

                Else ' Si hay mas de una fila seleccionada

                    ' Se verifica si la celda esta seleccionada
                    If GridView1.IsRowSelected(li_iSelec) = True Then

                        ' Si la celda esta seleccionada, se verifica el indice se encuentra almacenado en la lista de orden
                        Dim lb_existe As Boolean = Array.Exists(li_orden, Function(element)
                                                                              Return element.Equals(li_iSelec)
                                                                          End Function)
                        ' Si no se encuentra almacenado en la lista de orden, se almacena en la misma
                        If lb_existe = False Then
                            o_lstOrdenSelec.Add(li_iSelec)
                            'Else
                            '    MsgBox("AQUI PASO ALGO RARO: LSTORD= " & o_lstOrdenSelec.Count & " ; SELC= " & li_selcs.Length & " ItemCode= " & GridView1.GetRow(li_iSelec)("ItemCode"))
                        End If

                        'If o_lstOrdenSelec.Count <> li_selcs.Length Then
                        '    MsgBox("ERROR (NO AÑADIO A LA LISTA): LSTORD= " & o_lstOrdenSelec.Count & " ; SELC= " & li_selcs.Length & " ItemCode= " & GridView1.GetRow(li_iSelec)("ItemCode"))
                        'End If

                    Else

                        ' Si la celda no esta seleccionada, se verifica el indice se encuentra almacenado en la lista de orden
                        Dim lb_existe As Boolean = Array.Exists(li_orden, Function(element)
                                                                              Return element.Equals(li_iSelec)
                                                                          End Function)

                        ' Si se encuentra almacenado en la lista de orden, se elimina el indice de la misma
                        If lb_existe = True Then
                            o_lstOrdenSelec.Remove(li_iSelec)
                            'Else
                            '    MsgBox("AQUI PASO ALGO RARO: LSTORD= " & o_lstOrdenSelec.Count & " ; SELC= " & li_selcs.Length & " ItemCode= " & GridView1.GetRow(li_iSelec)("ItemCode"))
                        End If

                        'If o_lstOrdenSelec.Count <> li_selcs.Length Then
                        '    MsgBox("ERROR (NO QUITO DE LA LISTA): LSTORD= " & o_lstOrdenSelec.Count & " ; SELC= " & li_selcs.Length & " ItemCode= " & GridView1.GetRow(li_iSelec)("ItemCode"))
                        'End If

                    End If

                End If

            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub sub_actDeselec(ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs)
        Try

            ' Se obtiene el arreglo de las filas seleccionadas
            'Dim lo_grid As GridView = gctDatos.FocusedView
            'Dim li_selcs As Integer() = lo_grid.GetSelectedRows

            ' Se obtiene el indice de la fila deseleccionada
            Dim li_iSelec As Integer = e.RowHandle

            ' Se crea una variable para el indice que se desea eliminar. Dicho indice, representa a la fila deseleccionada
            Dim li_iDselec As Integer = -1

            If GridView1.IsRowSelected(li_iSelec) = False Then

                ' Se recorre el listado de indices seleccionados
                For Each i As Integer In o_lstOrdenSelec

                    ' Si el indice obtenido es igual al indice actual del recorrido
                    If i = li_iSelec Then
                        li_iDselec = i
                    End If

                Next

                ' Se elimina el indice deseleccionado de la lista
                o_lstOrdenSelec.Remove(li_iDselec)

            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

    Private Sub gctDatos_Click(sender As System.Object, e As System.EventArgs) Handles gctDatos.Click

    End Sub
End Class