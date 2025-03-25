Imports Util
Imports DevExpress.XtraGrid.Columns
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing
Imports DevExpress.Utils.Menu
Imports System.Reflection
Imports System.IO
Imports DevExpress.XtraEditors.Repository

Public Class uct_gridConBusqueda

    ' Eventos del control
    Public Event evt_modificacionGrid As eventHandler
    Public Event evt_eliminacionFilas As eventHandler
    Public Event evt_mostrandoEditorColumna As eventHandler
    Public Event evt_alModificarCeldaGrid As EventHandler
    Public Event evt_trasModificarCeldaGrid As EventHandler
    Public Event evt_CargarPosicionBancoCombo As EventHandler

    ' Se declara los atributos de la clase
    ' Datos del Grid
    Private o_dvwDatos As DataView
    Private WithEvents o_dtbDatos As DataTable

    ' Filtros de busqueda
    Private s_buscarPor1 As String
    Private s_buscarPor2 As String

    ' Filas seleccionadas
    Private o_dtbSelec As New DataTable

    ' Columnas a ocultar
    Private o_dtbOcultarCol As New DataTable

    ' Cabeceras de las columnas
    Private o_dtbCabCol As New DataTable
    Private o_dtbDetalle As New DataTable

    ' <<< Variables sin utilizar y que debe ser analizada su depuracion >>>
    Private s_tblDet As String

    ' Campo llave de los datos mostrados en el grid
    Private s_tblId As String
    Private s_tblId2 As String

    ' Tabla de detalle asociada al control
    Private s_tabla As String

    ' Indica si es la primera vez que se carga el grid
    Private b_primeraEjec As Boolean = True

    ' Indica si el grid permite filtro de columnas nativo de devExpress
    Private b_conFiltro As Boolean = True

    ' Variables para la alternancia de color
    Private s_colAlternaColor As String = ""
    Private b_alternarColor As Boolean = True

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
    Private o_dataSource As DataTable
    Private s_buscarPor As String = ""
    Private s_valorBusq As String = ""
    Private b_conMenu As Boolean = False
    Private s_colElm As String
    Private b_permitirOrden As Boolean = False
    Private o_objDetalle As Object

    Public Property valorBusq() As String
        Get
            Return s_valorBusq
        End Get
        Set(ByVal value As String)
            s_valorBusq = value
        End Set
    End Property

    Public Property buscarPor() As String
        Get
            Return s_buscarPor
        End Get
        Set(ByVal value As String)
            s_buscarPor = value
        End Set
    End Property

    Public Property DataSource() As DataTable
        Get
            Return o_dataSource
        End Get
        Set(ByVal value As DataTable)
            o_dataSource = value
        End Set
    End Property

    Public Property conFiltro() As Boolean
        Get
            Return b_conFiltro
        End Get
        Set(ByVal value As Boolean)
            b_conFiltro = value
        End Set
    End Property

    Public Property Tabla() As String
        Get
            Return s_tabla
        End Get
        Set(ByVal value As String)
            s_tabla = value
        End Set
    End Property

    Public Property TablaId() As String
        Get
            Return s_tblId
        End Get
        Set(ByVal value As String)
            s_tblId = value
        End Set
    End Property

    Public Property TablaId2() As String
        Get
            Return s_tblId2
        End Get
        Set(ByVal value As String)
            s_tblId2 = value
        End Set
    End Property

    Public Property ColAlternaColor() As String
        Get
            Return s_colAlternaColor
        End Get
        Set(ByVal value As String)
            s_colAlternaColor = value
        End Set
    End Property

    Public Property conMenu() As Boolean
        Get
            Return b_conMenu
        End Get
        Set(ByVal value As Boolean)
            b_conMenu = value
        End Set
    End Property

    Public Property ColElm() As String
        Get
            Return s_colElm
        End Get
        Set(ByVal value As String)
            s_colElm = value
        End Set
    End Property

    Public Property PermitirOrden() As Boolean
        Get
            Return b_permitirOrden
        End Get
        Set(ByVal value As Boolean)
            b_permitirOrden = value
        End Set
    End Property

    Public Property ObjDetalle() As Object
        Get
            Return o_objDetalle
        End Get
        Set(ByVal value As Object)
            o_objDetalle = value
        End Set
    End Property

#Region "Inicializacion"

    Private Sub uct_gridConBusqueda_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sub_asigPropiedadesIniciales()
    End Sub

    Public Sub sub_asigPropiedadesIniciales()
        Try

            ' Se verifica si se indico que el control debe tener filtro de busqueda
            Label1.Visible = b_conFiltro
            txtBuscar.Visible = b_conFiltro

            ' Se indica si el grid permite el orden de los registros por columna
            gvwDatos.OptionsCustomization.AllowSort = b_permitirOrden

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_inicializar()
        Try

            ' Se inicializa el control
            sub_inicializar(o_dataSource, s_buscarPor, s_valorBusq, s_tblDet, s_tblId)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub sub_inicializar(ByVal po_dtbDatos As DataTable, ByVal ps_buscarPor1 As String, ByVal ps_valorBusq As String, Optional ByVal ps_tblDet As String = "", Optional ByVal ps_tblId As String = "")
        Try

            ' Se obtiene el dataView del DataTable
            o_dtbDatos = po_dtbDatos
            o_dvwDatos = po_dtbDatos.DefaultView

            ' Se ajusta el tamaño de la ventana de acuerdo al número de columnas
            'sub_ajustarTamano()

            ' Se obtiene los campos de condición para el filtro del GridControl
            s_buscarPor1 = ps_buscarPor1
            s_tblDet = ps_tblDet
            s_tblId = ps_tblId

            ' Se asigna el DataTable como DataSource del GridControl
            Me.gctDatos.DataSource = o_dvwDatos
            o_dvwDatos.RowFilter = String.Empty
            txtBuscar.Text = ps_valorBusq
            sub_establecerFiltroGctlPrincipal()

            ' Solo se inicializa los componentes la primera ejecucion
            If b_primeraEjec = True Then

                ' Se inicializa el dataTable de seleccion
                sub_iniDtbSelec()

                ' Se inicializa el dataTable de columnas a ocultar
                sub_iniDtbColOcultar()

                ' Se inicializa el dataTable de cabeceras de columnas
                sub_iniDtbCabCol()

                ' Se inicializa el dataTable de datos de opciones de subreporte
                sub_iniDtbParamSR()

            End If

            ' Se asigna el ancho de columnas adecuado
            sub_asignarAnchoColGridView(gvwDatos)

            ' Se indica que la siguiente vez que se entre a este metodo, ya no será la primera ejecucion
            b_primeraEjec = False

            ' Se inicializa los eventos del control
            sub_inicializarEventos()

            ' Se inicializa las opciones del menu
            sub_iniOpcionesMenuPorDefecto()

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub
    'casta
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

    Public Sub sub_inicializarEventos()
        Try

            ' Se asigna los eventos al dataTable
            AddHandler o_dtbDatos.TableNewRow, New DataTableNewRowEventHandler(AddressOf Table_NewRow)
            AddHandler o_dtbDatos.RowDeleted, New DataRowChangeEventHandler(AddressOf Row_Deleted)
            AddHandler o_dtbDatos.ColumnChanging, New DataColumnChangeEventHandler(AddressOf Column_Changing)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

#Region "EventosControl"

    Private Sub txtBuscar_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBuscar.EditValueChanged
        sub_establecerFiltroGctlPrincipal()
    End Sub

    Private Sub txtBuscar_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBuscar.KeyDown
        sub_establecerFocusGridControlPrinc(e)
    End Sub

    Private Sub gvwDatos_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gvwDatos.CellValueChanged
        RaiseEvent evt_trasModificarCeldaGrid(sender, e)
    End Sub

    Private Sub gvwDatos_CellValueChanging(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gvwDatos.CellValueChanging
        RaiseEvent evt_alModificarCeldaGrid(sender, e)
        RaiseEvent evt_modificacionGrid(sender, e)
    End Sub

    Private Sub gvwDatos_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvwDatos.LostFocus
        'b_alternarColor = True
    End Sub

    Private Sub gvwDatos_RowClick(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles gvwDatos.RowClick
        sub_guardarOrdenSelec(e)
        'b_alternarColor = False
    End Sub

    Private Sub gvwDatos_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gvwDatos.RowStyle
        sub_alternarColorPorValorCol(sender, e)
    End Sub

    Private Sub gctDatos_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gctDatos.KeyDown
        Try
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

    Private Sub gvwDatos_PopupMenuShowing(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles gvwDatos.PopupMenuShowing
        sub_mostrarMenu(e)
    End Sub

    Private Sub gvwDatos_ShowingEditor(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles gvwDatos.ShowingEditor
        ' Se maneja la edicion de columnas de acuerdo al objeto asociado
        sub_manejoAlMostrarEditorColumn(sender, e)

        ' Se invoca el evento en la clase donde se encuentra el control
        RaiseEvent evt_mostrandoEditorColumna(sender, e)
    End Sub

#End Region

#Region "ManejoGUI"

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

    Public Sub sub_habilitarMultiSelec(ByVal pb_habl As Boolean)
        Try

            ' Se habilita o deshabilita la multiseleccion del grid
            gvwDatos.OptionsSelection.MultiSelect = pb_habl

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_habilitarOrdenCol(ByVal pb_habl As Boolean)
        Try

            ' Se habilita o deshabilita la opción de orden de columna
            gvwDatos.OptionsCustomization.AllowSort = pb_habl

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_alternarColorPorValorCol(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs)
        Try
            If b_alternarColor = False Then
                Exit Sub
            End If

            ' Se verifica si la propiedad de Alternar Color esta activa para este control
            If s_colAlternaColor <> "" Then
                ' Se verifica que el indice sea mayor a cero
                If e.RowHandle >= 0 Then

                    ' Se obtiene el gridView
                    Dim lo_View As GridView = sender

                    ' Se obtiene el valor que determinara la alternancia de los colores
                    Dim li_valor As Integer = lo_View.GetRowCellDisplayText(e.RowHandle, lo_View.Columns(s_colAlternaColor))

                    If li_valor = 0 Then

                        ' Se asigna el color a la fila
                        e.Appearance.BackColor = Color.WhiteSmoke

                    ElseIf li_valor Mod 2 = 0 Then

                        ' Se asigna el color a la fila                      
                        e.Appearance.BackColor = Color.WhiteSmoke

                    ElseIf li_valor Mod 2 <> 0 Then

                        ' Se asigna el color a la fila
                        e.Appearance.BackColor = Color.LightBlue

                    End If

                End If

            End If
        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_limpiar()
        Try

            ' Se limpia los elementos seleccionados
            sub_limpiarSeleccion()

            ' Se limpia el dataTable de datos
            If Not o_dtbDatos Is Nothing Then ' Se verifica si el grid cuenta con un dataTable asignado
                o_dtbDatos.Rows.Clear()
            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

#Region "seleccion"

    Public Function int_contarSelecc() As Integer
        Try

            ' Se retorna la cantidad de filas seleccionadas
            Return o_dtbSelec.Rows.Count

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return -1
        End Try
    End Function

    Public Function dtb_obtenerSeleccion() As DataTable
        Try

            ' Antes de obtener una nueva seleccion, se limpia el dataTable de seleccion
            o_dtbSelec.Rows.Clear()

            ' Se obtiene el dataTable del grid
            Dim lo_dvw As DataView = CType(gctDatos.DataSource, DataView)
            Dim lo_grid As GridView = gctDatos.FocusedView

            ' Se recorre las filas seleccionadas
            Dim li_selcs As Integer() = lo_grid.GetSelectedRows

            ' Se declara un arreglo para obtener los indices seleccionados de la lista de orden de seleccion
            Dim li_ordnSel As Integer() = o_lstOrdenSelec.ToArray

            ' Se verifica si existe objetos en el arreglo de orden de seleccion
            If li_ordnSel.Length = 0 Then
                li_ordnSel = li_selcs
            End If

            ' Se verifica que la lista de orden tenga igual o mas cantidad de indices que la seleccion del grid
            If li_selcs.Length > o_lstOrdenSelec.Count Then

                ' Se muestra un mensaje de error que indique que se realice de nuevo la seleccion
                MsgBox("Ocurrio un error al ordenar los registros seleccionados. Por favor, realice la seleccion de nuevo. (" & Me.Name & ")")

                ' Se recorre los indices seleccionados para realizar la deseleccion
                For Each d As Integer In li_selcs
                    gvwDatos.UnselectRow(d)
                Next

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

                ' Se recorre las columnas del dataTable de seleccion
                For Each lo_col As DataColumn In o_dtbSelec.Columns

                    ' Se asigna el valor a la columna correspondiente                   
                    lo_row(lo_col.ColumnName) = gvwDatos.GetRow(i)(lo_col.ColumnName)

                Next
                lo_row.EndEdit()

                ' Se añade la linea al dataTable
                o_dtbSelec.Rows.Add(lo_row)

            Next

            ' Se retorna el dataTable
            Return o_dtbSelec

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return o_dtbSelec
        End Try
    End Function

    Private Sub sub_guardarOrdenSelec(ByVal e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs)
        Try
            'i_contador = i_contador + 1


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

                    ' Se verifica si la fila actual esta seleccionada
                    If gvwDatos.IsRowSelected(li_iSelec) = True Then

                        ' Se ingresa los indices al listado
                        If li_selcs(0) = li_iSelec Then
                            o_lstOrdenSelec.Add(li_selcs(1))
                            o_lstOrdenSelec.Add(li_iSelec)
                        Else
                            o_lstOrdenSelec.Add(li_selcs(0))
                            o_lstOrdenSelec.Add(li_iSelec)
                        End If
                    Else

                        ' Se asigna a la lista de orden los mismos valores que el arreglo de indices seleccionados
                        o_lstOrdenSelec = li_selcs.ToList

                    End If


                ElseIf li_selcs.Length = 0 Then ' Si no hay filas seleccionadas

                    ' Se limpia el listado de orden de seleccion
                    o_lstOrdenSelec.Clear()

                Else ' Si hay mas de una fila seleccionada

                    ' Se verifica si la celda esta seleccionada
                    If gvwDatos.IsRowSelected(li_iSelec) = True Then

                        ' Si la celda esta seleccionada, se verifica el indice se encuentra almacenado en la lista de orden
                        Dim lb_existe As Boolean = Array.Exists(li_orden, Function(element)
                                                                              Return element.Equals(li_iSelec)
                                                                          End Function)
                        ' Si no se encuentra almacenado en la lista de orden, se almacena en la misma
                        If lb_existe = False Then
                            o_lstOrdenSelec.Add(li_iSelec)
                        End If

                    Else

                        ' Si la celda no esta seleccionada, se verifica el indice se encuentra almacenado en la lista de orden
                        Dim lb_existe As Boolean = Array.Exists(li_orden, Function(element)
                                                                              Return element.Equals(li_iSelec)
                                                                          End Function)

                        ' Si se encuentra almacenado en la lista de orden, se elimina el indice de la misma
                        If lb_existe = True Then
                            o_lstOrdenSelec.Remove(li_iSelec)
                        End If

                    End If

                End If

                Label2.Text = li_selcs.Length
                Label3.Text = o_lstOrdenSelec.Count

            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_eliminarFilasSelec()
        Try

            ' Se obtiene las filas seleccionadas del grid
            Dim lo_grid As GridView = gctDatos.FocusedView

            ' Se recorre las filas seleccionadas
            Dim li_selcs As Integer() = lo_grid.GetSelectedRows

            ' Se declara una lista para obtener los ids de las columnas a eliminar
            Dim lo_lstIds As New List(Of DataRow)

            ' Se recorrre las filas seleccionadas 
            For Each i As Integer In li_selcs

                ' Se obtiene el valor del id de los datos del grid actual
                Dim lo_tblId As Object = gvwDatos.GetRow(i)(s_tblId)

                ' Se declara una variable para el valor del segundo id
                Dim lo_tblId2 As Object

                ' Se obtiene el valor del segundo id de los datos del grid actual
                If Not s_tblId2 Is Nothing Then
                    If s_tblId2.ToString.Trim <> "" Then
                        lo_tblId2 = gvwDatos.GetRow(i)(s_tblId2)
                    End If
                End If

                ' Se obtiene la fila 
                Dim lo_row As DataRow = row_obtFila(lo_tblId, lo_tblId2)

                ' Se verifica si se obtuvo la fila
                If Not lo_row Is Nothing Then
                    lo_lstIds.Add(lo_row)
                End If

                ' Se obtiene los ids a eliminar
                'lo_lstIds.Add(lo_tblId)

            Next

            ' Se recorre el listado para realizar la eliminacion de las filas
            For Each lo_row As DataRow In lo_lstIds

                ' Se elimina la fila del grid
                sub_elimFila(lo_row)

            Next

            ' Se limpia el dataTable de filas seleccionados
            o_dtbSelec.Rows.Clear()

            ' Se limpia la lista de orden
            o_lstOrdenSelec.Clear()

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Function row_obtFila(ByVal po_id As Object, Optional po_id2 As Object = Nothing) As DataRow
        Try

            ' Se declara una variable para el filtro
            Dim ls_filtro As String = s_tblId & "='" & po_id & "'"

            ' Se verifica si se recibiio como parametro otro valor adicional
            If Not po_id2 Is Nothing Then
                If po_id2.ToString.Trim <> "" Then
                    ' Se asigna un filtro adicional a la cadena
                    ls_filtro = ls_filtro & " and " & s_tblId2 & " = '" & po_id2 & "'"
                End If
            End If

            ' Se obtiene la fila seleccionada
            Dim dr As DataRow = o_dtbDatos.Select(ls_filtro)(0)

            ' Se verifica si se obtuvo el dataRow
            If dr Is Nothing Then
                MsgBox("No se obtuvo ninguna fila para la eliminacion.")
                Return Nothing
            End If

            ' Se retorna el dataRow obtenido
            Return dr

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Private Sub sub_elimFila(po_row As DataRow)
        Try

            ' Se remueve la fila 
            o_dtbDatos.Rows.Remove(po_row)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub sub_elimFila(ByVal po_id As Object, Optional po_id2 As Object = Nothing)
        Try

            ' Se declara una variable para el filtro
            Dim ls_filtro As String = s_tblId & "='" & po_id & "'"

            ' Se verifica si se recibiio como parametro otro valor adicional
            If Not po_id2 Is Nothing Then
                If po_id2.ToString.Trim <> "" Then
                    ' Se asigna un filtro adicional a la cadena
                    ls_filtro = ls_filtro & " and " & s_tblId2 & " = '" & po_id2 & "'"
                End If
            End If

            ' Se obtiene la fila seleccionada
            Dim dr As DataRow = o_dtbDatos.Select(ls_filtro)(0)

            ' Se verifica si se obtuvo el dataRow
            If dr Is Nothing Then
                MsgBox("No se obtuvo ninguna fila para la eliminacion.")
                Exit Sub
            End If

            ' Se remueve la fila 
            o_dtbDatos.Rows.Remove(dr)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub sub_elimFilas(ByVal po_id As Object)
        Try

            ' Se obtiene la fila seleccionada
            Dim lo_rows As DataRow() = o_dtbDatos.Select(s_colElm & "='" & po_id & "'")

            ' Se recorre las filas obtenidas de la seleccion
            For Each lo_row In lo_rows

                ' Se remueve la fila 
                o_dtbDatos.Rows.Remove(lo_row)

            Next

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_limpiarSeleccion()
        Try

            ' Se limpia el dataTable de filas seleccionados
            o_dtbSelec.Rows.Clear()

            ' Se limpia la lista de orden
            o_lstOrdenSelec.Clear()

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Function dtr_obtDataRowFilaSeleccionada() As DataRow
        Try

            ' Se retorna la fila seleccionada
            Return gvwDatos.GetFocusedDataRow

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function dtr_obtFilaSeleccionada() As Object
        Try

            ' Se retorna la fila seleccionada
            Return gvwDatos.GetFocusedRow

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function col_obtColumnaSeleccionada() As GridColumn
        Try

            ' Se retorna la columna seleccionada
            Return gvwDatos.FocusedColumn

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

#End Region

#Region "Filtro"

    Private Function str_obtNomColumnFiltro() As String
        Try

            ' Se declara una variable para el filtro
            Dim ls_rowFilter As String = ""

            ' Se verifica si hay columnas ordenadas
            If gvwDatos.SortedColumns.Count > 0 Then

                ' Se obtiene la columna de orden
                Dim lo_gridColumn As GridColumn = gvwDatos.SortedColumns.Item(0)

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
                If Not gvwDatos.Columns(s_buscarPor1) Is Nothing Then

                    ' Se obtiene la columna de orden
                    Dim lo_gridColumn As GridColumn = gvwDatos.Columns(s_buscarPor1)

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

#End Region

#Region "EventosDataTable"

    Public Sub Table_NewRow(ByVal sender As Object, ByVal e As DataTableNewRowEventArgs) Handles o_dtbDatos.TableNewRow
        RaiseEvent evt_modificacionGrid(sender, e)
    End Sub

    Public Sub Row_Deleted(ByVal sender As Object, ByVal e As DataRowChangeEventArgs) Handles o_dtbDatos.RowDeleted
        RaiseEvent evt_modificacionGrid(sender, e)
        RaiseEvent evt_eliminacionFilas(sender, e)
    End Sub

    Private Sub Column_Changing(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) Handles o_dtbDatos.ColumnChanging
        ' RaiseEvent evt_modificacionGrid(sender, e)
    End Sub

#End Region

#Region "menuClickDerecho"

    Private Sub sub_mostrarMenu(ByVal e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs)
        Try

            ' Se verifica si la propiedad que indica que el control debe mostrar menu esta activa (true)
            If conMenu = False Then
                Exit Sub
            End If

            ' Se verifica si se realizo click derecho en una fila
            If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
                Dim rowHandle As Integer = e.HitInfo.RowHandle

                ' Se limpia las opciones del menu
                e.Menu.Items.Clear()

                If o_dtbParamSR.Rows.Count > 0 Then

                    ' Se recorre las opciones de SubReporte para crear el menu
                    For i As Integer = 0 To o_dtbParamSR.Rows.Count - 1

                        Dim item As DXMenuItem = sub_crearMenu(gvwDatos, rowHandle, i, o_dtbParamSR.Rows(i)("nomOpc"))
                        item.BeginGroup = True
                        e.Menu.Items.Add(item)

                    Next

                End If

            End If
        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Function sub_crearMenu(ByVal view As GridView, ByVal rowHandle As Integer, ByVal pi_indice As Integer, ByVal ps_nomOpc As String) As DXMenuCheckItem
        Dim checkItem As New DXMenuCheckItem(ps_nomOpc, False, Nothing, AddressOf sub_accionMenu)
        checkItem.Tag = pi_indice
        Return checkItem
    End Function

    Private Sub sub_accionMenu(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ' Se obtiene la opcion seleccionada
            Dim lo_item As DXMenuCheckItem = CType(sender, DXMenuItem)

            ' Se obtiene el ID de la opcion seleccionada
            Dim li_indice As Integer = CInt(lo_item.Tag)

            ' Se obtiene el objeto de la lista para realizar la operacion especificada
            For i As Integer = 0 To o_dtbParamSR.Rows.Count - 1

                ' Se verifica el indice
                If i = li_indice Then

                    ' Se obtiene el metodo de la accion
                    Dim lo_method As MethodInfo = Me.GetType.GetMethod(o_dtbParamSR.Rows(i)("idOpc").ToString, BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.Instance)

                    ' Se ejecuta el metodo
                    lo_method.Invoke(Me, New Object() {})

                End If

            Next

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub sub_iniOpcionesMenuPorDefecto()
        Try

            ' Se limpia las filas 
            o_dtbParamSR.Rows.Clear()

            ' Se añade un registro al dataTable de opciones
            Dim lo_row As DataRow = o_dtbParamSR.NewRow
            lo_row.BeginEdit()
            lo_row("idOpc") = "sub_elmFila"
            lo_row("nomOpc") = "Eliminar Fila(s)"
            lo_row.EndEdit()

            ' Se añade la opcion al dataTable
            o_dtbParamSR.Rows.Add(lo_row)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

#Region "eliminarFila(s)"

    Private Sub sub_elmFila()
        Try

            ' Se obtiene el valor de la fila seleccionada correspondiente al campo que señalara la seleccion para la eliminacion de las filas
            Dim lo_valor As Object = gvwDatos.GetFocusedRowCellValue(ColElm)

            ' Se invoca el metodo de eliminacion de filas
            sub_elimFilas(lo_valor)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

#Region "manejoColumn"

    Private Sub sub_manejoAlMostrarEditorColumn(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try

            ' Se verifica si el control tiene TAG y objeto de detalle asociado, lo que indicaria que el grid corresponde a una tabla de detalle
            If Me.Tag Is Nothing Or Me.ObjDetalle Is Nothing Then
                e.Cancel = True
                Exit Sub
            End If
            If Not Me.ObjDetalle Is Nothing And (Me.Tag Is Nothing Or Me.Tag.ToString.Trim = "") Then
                MsgBox("El grid tiene un objeto de detalle asignado, pero no tiene el nombre de la tabla de detalle asociada al objeto asignado.")
                e.Cancel = True
                Exit Sub
            End If

            ' Se obtiene la anotacion de entidad del objeto recibido
            Dim lo_annEnt As annEntidad = ann_obtenerAnotacion(Me.ObjDetalle, GetType(annEntidad))

            ' Se verifica si el objeto tiene entidad de anotacion
            If lo_annEnt Is Nothing Then
                MsgBox("No se asigno una anotación de entidad al objeto asignado al control de usuario grid.")
                Exit Sub
            End If

            ' Se verifica si la entidad corresponde a un detalle
            If lo_annEnt.esDetalle = False Then
                MsgBox("El objeto asignado no corresponde a una entidad de detalle.")
                Exit Sub
            End If

            ' Se verifica si el TAG del control es igual a la tabla asociada al objeto de detalle asignado
            If lo_annEnt.Tabla.ToLower = Me.Tag.ToString.Trim.ToLower Then

                ' Se recorre los atributos del objeto
                For Each lo_field As FieldInfo In Me.ObjDetalle.GetType.GetFields(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance)

                    ' Se obtiene la anotacion de atributo 
                    Dim lo_annAtr As annAtributo = ann_obtenerAnotacion(lo_field, GetType(annAtributo))

                    ' Se verifica si se obtuvo la anotacion
                    If lo_annAtr Is Nothing Then
                        Continue For
                    End If

                    ' Se obtiene el gridView
                    Dim lo_view As GridView = sender

                    ' Se verifica si la columna corresponde al campo actual
                    If lo_view.FocusedColumn.Name.ToLower.Contains(lo_annAtr.campoBD.ToLower) Then

                        ' Se verifica si el atributo corresponde a un campo que puede ser modificado por el usuario
                        If lo_annAtr.usrEditable = False Then
                            e.Cancel = True
                        End If

                    End If

                Next

            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Public Sub sub_asigControlColumna(ByVal ps_columna As String, ByVal pi_tipoControl As Integer)
        Try

            ' Se obtiene la columna
            Dim lo_column As GridColumn = gvwDatos.Columns(ps_columna)

            ' Se verifica si se obtuvo la columna
            If lo_column Is Nothing Then
                MsgBox("No se obtuvo ninguna columna con el nombre <" & ps_columna & "> en el grid <" & Me.Tag & ">")
            End If

            ' Se declara un control RepositoryItem
            Dim lo_rptryItem As RepositoryItem

            ' Se verifica que tipo de repositoryItem se recibe
            If pi_tipoControl = enm_tipoControlRptryItem.CHECKBOX Then

                ' Se asigna el tipo de repositoryItem
                lo_rptryItem = New RepositoryItemCheckEdit
                CType(lo_rptryItem, RepositoryItemCheckEdit).ValueChecked = "Y"
                CType(lo_rptryItem, RepositoryItemCheckEdit).ValueUnchecked = "N"

            ElseIf pi_tipoControl = enm_tipoControlRptryItem.DATEEDIT Then

                ' Se asigna el tipo de repositoryItem
                lo_rptryItem = New RepositoryItemDateEdit

            Else
                MsgBox("El tipo de RepositoryItem enviado no ha sido contemplado por el desarrollo de la aplicacion.")
                Exit Sub
            End If

            ' Se añade los items al GridControl
            gctDatos.RepositoryItems.Add(lo_rptryItem)

            ' Se asigna el repositoryItem a la columna
            lo_column.ColumnEdit = lo_rptryItem

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

#Region "GridView"

    Public Function gvw_obtGridView() As GridView
        Try

            ' Se retorna el gridView
            Return gvwDatos

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

#End Region

End Class

