Imports System.Reflection
Imports System.Windows.Forms
Imports DevExpress.XtraTab
Imports DevExpress.XtraGrid
Imports Util
Imports System.Runtime.Remoting
Imports DevExpress.XtraEditors

Public Class frmComun

    ' Atributos comunes del formulario
    Public o_objNegocio As Object ' Se declara una variable que contendra el objeto del negocio
    Public i_modo As Integer ' Se declara una variable para el modo del formulario
    Public b_ocultarBarra As Boolean = False

#Region "Propiedades"

    Public Property Modo() As Integer
        Get
            Return i_modo
        End Get
        Set(ByVal value As Integer)
            i_modo = value
            lblModo.Text = i_modo.ToString
        End Set
    End Property

    Public Property OcultarBarra() As Boolean
        Get
            Return b_ocultarBarra
        End Get
        Set(ByVal value As Boolean)
            b_ocultarBarra = value
        End Set
    End Property

#End Region

#Region "CargaForm"
    Private Sub frmComun_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sub_iniForm()
    End Sub
#End Region

#Region "Inicializacion"

    ' Se inicializa el formulario heredado
    Private Sub sub_iniForm()
        Try

            ' Se inicializa el objeto del negocio
            sub_iniObjNegocio()

            ' Se asigna los eventos de boton al formulario
            sub_asigEvtBotones(Me.Controls)

            ' Se ejecuta el metodo de carga de formulario propio de la clase de negocio asociada al formulario heredado
            sub_cargarForm()

            ' Si el formulario esta relacionado a una entidad, se muestra la barra de herramientas
            sub_mostrarBarraHerr()

            ' Se asigna los eventos de combo al formulario
            sub_asigEvtCombo(Me.Controls)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    ' Se obtiene el objeto de negocio a partir del nombre de la clase asignada en la propiedad Tag del formulario
    Overridable Sub sub_iniObjNegocio()
        Try

            ' <<< La logica de este metodo debe ser definida en las clases derivadas >>>

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    ' Se asigna un evento en comun a todos los botones del formulario
    Public Sub sub_asigEvtBotones(ByVal po_controls As System.Windows.Forms.Control.ControlCollection)
        Try

            ' Se recorre los botones del formulario
            For Each lo_control As Control In po_controls

                ' Se verifica si el control actual es un botón
                If TypeOf (lo_control) Is Button Then

                    ' Se asigna el evento al boton
                    AddHandler (CType(lo_control, Button)).Click, AddressOf btn_click

                ElseIf TypeOf (lo_control) Is SimpleButton Then

                    ' Se asigna el evento al boton
                    AddHandler (CType(lo_control, SimpleButton)).Click, AddressOf btn_click

                ElseIf TypeOf (lo_control) Is Panel Then

                    ' Se realiza esta accion para los controles que se encuentran dentro del panel
                    sub_asigEvtBotones(CType(lo_control, Panel).Controls)

                End If

            Next

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    ' Se asigna un evento de seleccion de combo para los combos existentes en el formulario
    Public Sub sub_asigEvtCombo(ByVal po_controls As System.Windows.Forms.Control.ControlCollection)
        Try

            ' Se recorre los botones del formulario
            For Each lo_control As Control In po_controls

                ' Se verifica si el control actual es un botón
                If TypeOf (lo_control) Is System.Windows.Forms.ComboBox Then

                    ' Se asigna el evento al boton
                    AddHandler (CType(lo_control, System.Windows.Forms.ComboBox)).SelectedIndexChanged, AddressOf cbo_selectedIndexChanged

                ElseIf TypeOf (lo_control) Is Panel Then

                    ' Se realiza esta accion para los controles que se encuentran dentro del panel
                    sub_asigEvtCombo(CType(lo_control, Panel).Controls)

                End If

            Next

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    ' Se muestra la barra de herramientas segun la asociacion del formulario con una entidad
    Private Sub sub_mostrarBarraHerr()
        Try

            ' Se verifica si el formulario esta asociado a una entidad
            If Not Tag Is Nothing Then
                If Tag.ToString.Trim <> "" Then
                    If OcultarBarra = False Then
                        pnlBarraHerr.Visible = True
                    Else
                        pnlBarraHerr.Visible = False
                    End If
                Else
                    pnlBarraHerr.Visible = False
                End If
            Else
                pnlBarraHerr.Visible = False
            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#Region "EventoBoton"

    ' Evento comun para los botones del formulario
    Public Sub btn_click(ByVal sender As Object, ByVal e As EventArgs)
        sub_accionBoton(sender)
    End Sub

    ' Evento comun para los System.Windows.Forms.ComboBox del formulario
    Public Sub cbo_selectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        sub_accionCombo(sender)
    End Sub

#End Region

#Region "FuncionalidadComun"

    Public Sub sub_accionBoton(ByVal sender As Object)
        Try

            ' Se verifica si el TAG indica alguna palabra reservada para este desarrollo
            If sender.Tag.ToString.ToLower = "cancel" Then

                ' Se cierra el formulario
                Me.Close()

            ElseIf sender.Tag.ToString = "1" Then

                ' Se obtiene el nombre del metodo asociado 
                Dim lo_method As MethodInfo = o_objNegocio.GetType.GetMethod("sub_accionPrincipal")

                ' Se ejecuta el metodo
                lo_method.Invoke(o_objNegocio, New Object() {})

            Else

                ' Se obtiene el nombre del metodo asociado 
                Dim lo_method As MethodInfo = o_objNegocio.GetType.GetMethod(sender.Tag.ToString)

                ' Se ejecuta el metodo
                lo_method.Invoke(o_objNegocio, New Object() {})

            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Public Sub sub_accionCombo(ByVal sender As Object)
        Try

            ' Se obtiene el tag del control
            Dim lo_cbo As Control = CType(sender, System.Windows.Forms.ComboBox)

            ' Se verifica si el control cuenta con la propiedad TAG
            If Not lo_cbo.Tag Is Nothing Then

                ' Se obtiene el metodo asociado a esta acción en la clase del negocio comun
                Dim lo_method As MethodInfo = o_objNegocio.GetType.GetMethod("sub_accionCombo")

                ' Se ejecuta el metodo
                lo_method.Invoke(o_objNegocio, New Object() {lo_cbo})

            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Public Sub sub_cargarForm()
        Try

            If o_objNegocio Is Nothing Then
                Exit Sub
            End If

            ' Se obtiene el nombre del metodo asociado 
            Dim lo_method As MethodInfo = o_objNegocio.GetType.GetMethod("sub_cargarFrm")

            ' Se ejecuta el metodo
            lo_method.Invoke(o_objNegocio, New Object() {})

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    ' Se obtiene la información del formulario en un dataTable
    Public Function dtb_crearDtbParaDatosForm(ByVal po_controles As System.Windows.Forms.Control.ControlCollection, ByVal po_dtb As DataTable) As DataTable
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
                        If Not TypeOf (lo_control) Is GridControl Then

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

    Public Function dtb_obtDatosCabecera() As DataTable
        Try

            ' Se declara un dataTable para obtener los datos del formulario
            Dim lo_dtb As New DataTable

            ' Se asigna el nobmre al DataTable
            lo_dtb.TableName = "Obj_Cab"

            ' Se declara un dataTable para obtener los datos del formulario
            lo_dtb = dtb_crearDtbParaDatosForm(Me.Controls, lo_dtb)

            ' Se crea una nueva fila
            Dim lo_newRow As DataRow = lo_dtb.NewRow
            lo_newRow.BeginEdit()

            ' Se recorre las columnas del dataTable para almacenar los datos de la cabecera del formulario
            For Each lo_column As DataColumn In lo_dtb.Columns

                ' Se obtiene el control de acuerdo al nombre del tag, el cual es el mismo que el nombre de la columna
                Dim lo_control As Control = ModuleGlobalForm.ctr_obtenerControl(lo_column.ColumnName, Me.Controls)

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

    Public Sub sub_invocarFormulario(ByVal ps_nomForm As String)
        Try

            ' Se obtiene el formulario a mostrar por nombre
            Dim lo_objHandle As ObjectHandle = Activator.CreateInstance(My.Application.Info.AssemblyName, My.Application.Info.AssemblyName & "." & ps_nomForm)
            Dim lo_frm As Object = lo_objHandle.Unwrap

            ' Se muestra el formulario de Carga de Estado de Cuenta
            CType(lo_frm, Form).Show()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

    Private Sub btnBuscarOF_Click(sender As System.Object, e As System.EventArgs) Handles btnBuscarOF.Click

    End Sub
End Class