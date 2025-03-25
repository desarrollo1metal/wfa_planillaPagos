Imports System
Imports System.IO
Imports System.Windows.Forms
Imports Util
Imports System.Data.OleDb
Imports cl_Entidad
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class classCargarEC
    Inherits classComun

    ' Atributos
    Private o_fileInfo As FileInfo
    Private o_dtbInfoEC As DataTable
    Private i_contOpAuto As Integer = 1

#Region "Constructor"

    Public Sub New(ByVal po_form As Form)
        MyBase.New(po_form)
    End Sub

#End Region

#Region "Ini_form"

    Public Overrides Sub sub_cargarForm()
        Try

            ' Se asigna los eventos a los controles necesarios
            sub_asigEventosChk()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_asigEventosChk()
        Try

            ' Se obtiene el checkBox indicador de registros para Planilla
            Dim lo_chk As Control = ctr_obtenerControl("regPll", o_form.Controls)

            ' Se verifica si se obtuvo el control
            If lo_chk Is Nothing Then
                sub_mostrarMensaje("Ocurrió un error al obtener el checkBox <regPll>.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
                Exit Sub
            End If

            ' Se asigna el evento al control
            AddHandler CType(lo_chk, CheckBox).CheckedChanged, AddressOf chkBoxChecked

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#Region "Eventos_Control"

    Private Sub chkBoxChecked(ByVal sender As Object, ByVal e As EventArgs)
        Try

            ' Se asigna el valor correspondiente a los registros del grid
            sub_asigTipoRegistroEC(sender)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#Region "ProcesoNegocio"

    ' Se obtiene la ruta del archivo excel que contiene el EC
    Public Sub sub_mostrarRuta()
        Try

            ' Se declara una variable para obtener la ruta del archivo
            Dim ls_ruta As String = ""

            ' Se muestra un cuadro de dialogo para seleccionar archivos Excel
            Dim lo_openFD As New OpenFileDialog()

            ' Se asigna las propiedades al cuadro de dialogo
            With lo_openFD
                .Title = "Seleccionar archivo"
                .Filter = "Archivos Excel(*.xls;*.xlsx)|*.xls;*xlsx|Archivo de Texto(*.txt)|*.txt|Todos los archivos(*.*)|*.*"
                .Multiselect = False
                .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop

                ' Si se selecciona OK
                If .ShowDialog = Windows.Forms.DialogResult.OK Then

                    ' Se obtiene el nombre del archivo
                    ls_ruta = .FileName

                    ' Se asigna el dato al control correspondiente
                    sub_asigValorControl("rutaEC", ls_ruta)

                End If

            End With

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    ' Se carga los datos del archivo Excel en la grilla
    Public Sub sub_mostrarEC()
        Try

            ' Se declara una variable para obtener la ruta del archivo seleccionado
            Dim ls_ruta As String = ""

            ' Se obtiene la caja de texto donde se encuentra la ruta del archivo
            Dim lo_control As Control = ctr_obtenerControl("rutaEC", o_form.Controls)

            ' Se obtiene la ruta desde la caja de texto
            ls_ruta = obj_obtValorControl(lo_control)

            ' Se verifica si se obtuvo la ruta del archivo
            If ls_ruta Is Nothing Then
                MsgBox("No se seleccionó ningun archivo")
                Exit Sub
            End If

            If ls_ruta.Trim = "" Then
                MsgBox("No se seleccionó ningun archivo")
                Exit Sub
            End If

            ' Se obtiene el archivo de la ruta obtenida
            Dim lo_fileInfo As FileInfo = My.Computer.FileSystem.GetFileInfo(ls_ruta)

            ' Se valida la informacion obtenida desde el excel
            If bol_valMostrarEC(lo_fileInfo) = False Then

                ' Se muestra un mensaje para indicar que el archivo especificado no cumple con el formato del Estado de Cuenta
                MsgBox("La información o la estructura del archivo """ & lo_fileInfo.Name & """ no corresponde al formato esperado del Estado de Cuenta.")

                ' Se finaliza el metodo
                Exit Sub

            End If

            ' Se obtiene la información del archivo excel
            Dim lo_dtb As DataTable = dtb_obtDatosDesdeExcel(lo_fileInfo)

            ' Se obtiene el grid de la información del Estado de Cuenta
            Dim lo_gridControl As Control = ctr_obtenerControl("infoEC", o_form.Controls)

            ' Se valida la informacion obtenida en el grid
            lo_dtb = dtb_valRegistrosEC(lo_dtb)

            ' Se verifica si se obtuvo el dataTable
            If lo_dtb Is Nothing Then
                CType(lo_gridControl, uct_gridConBusqueda).sub_limpiar()
                Exit Sub
            End If

            ' Se asigna el dataTable obtenido como dataSource del Grid
            CType(lo_gridControl, uct_gridConBusqueda).DataSource = Nothing
            sub_asigValorControl("infoEC", lo_dtb)

            ' Se obtiene asigna el objeto local FileInfo del metodo al atributo correspondiente de la clase
            o_fileInfo = lo_fileInfo

            ' Se obtiene el dataTable adquirido desde el Excel
            o_dtbInfoEC = lo_dtb

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    ' Se importa los datos hacia una tabla de base de datos
    Public Sub sub_importarEC()
        Try

            ' Se valida la información de la columna asociada a SAP: Numero de Cuenta Bancaria
            If bol_valCuentaBancaria() = False Then
                Exit Sub
            End If

            ' Se declara una variable para los mensajes de confirmacion
            Dim li_confirm As Integer = -1

            ' Se declara dos variables que determinaran si existe registros existentes en la tabla de estado de cuenta y en la tabla de planilla
            Dim lb_existEC As Boolean = False
            Dim lb_existPll As Boolean = False

            ' Se declara una variable para indicar si se deseea sobreescribir los registros del Estado de Cuenta que ya están registrados en el sistema
            Dim lb_sobreescrEC As Boolean = False

            ' Se obtiene el grid de la información del Estado de Cuenta
            Dim lo_gridControl As Control = ctr_obtenerControl("infoEC", o_form.Controls)

            ' Se verifica si se obtuvo el control
            If lo_gridControl Is Nothing Then
                sub_mostrarMensaje("Ocurrio un error al obtener el control <infoEC>.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            ' Se obtiene el dataTable del control grid
            Dim lo_dtb As DataTable = CType(lo_gridControl, uct_gridConBusqueda).DataSource

            ' Se verifica si se obtuvo el dataTable desde el control
            If lo_dtb Is Nothing Then
                sub_mostrarMensaje("La grilla <infoEC> no tiene fuente de datos.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            ' Se verifica si el dataTable tiene registros
            If lo_dtb.Rows.Count = 0 Then
                MsgBox("No hay registros para importar en la grilla.")
                Exit Sub
            End If

            ' Se declara un dataTable donde se encontrará la información a importar
            Dim lo_dtbImport As New DataTable

            ' Se indica que el nuevo dataTable tendra las mismas columnas
            lo_dtbImport = lo_dtb.Clone

            ' Se limpia las filas del dataTable
            lo_dtbImport.Rows.Clear()

            ' Se declara una variable para verificar si el registro existe en alguna planilla
            Dim lb_regExistPll As Boolean = False

            ' Se declara una variable para verificar si el registro existe en la tabla de Estado de Cuenta
            Dim lb_regExistEC As Boolean = False

            ' Se obtiene el checkBox indicador de registros para Planilla
            Dim lo_chk As Control = ctr_obtenerControl("regPll", o_form.Controls)

            ' Se verifica si se obtuvo el control
            If lo_chk Is Nothing Then
                sub_mostrarMensaje("Ocurrió un error al obtener el checkBox <regPll>.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            ' Se obtiene el valor del check de tipo de registro
            Dim ls_check As String = obj_obtValorControl(lo_chk)

            ' Se recorre la grilla para obtener los registros que se importaran
            For Each lo_row As DataRow In lo_dtb.Rows

                ' Se reinicia las variables que verifican la existencia de los registros en las tablas del sistema
                lb_regExistPll = False
                lb_regExistEC = False

                ' Se verifica si el registro ya existe en la tabla de estado de cuenta
                If lo_row("existeEnEC").ToString.ToLower = "si" Then

                    ' Se indica que existen registros en la tabla de Estado de Cuenta
                    If lb_existEC = False Then

                        ' Se asigna TRUE al indicador de registros existentes en la tabla Estado de Cuenta
                        lb_existEC = True

                        ' Se reinicia la variable de confirmación para volver a usarla en el siguiente mensaje
                        li_confirm = -1

                        ' Se muestra un mensaje de confirmacion para determinar si se desea sobreescribir los registros existentes
                        li_confirm = MessageBox.Show("Existen registros que ya se encuentran registrados en la tabla de Estado de Cuenta con el mismo Número de Operación, la misma cuenta, el mismo monto y en la misma fecha. ¿Desea sobreescribir dichos registros?", "caption", MessageBoxButtons.YesNoCancel)

                        ' Se verifica el resultado del mensaje de confirmacion
                        If li_confirm = DialogResult.Cancel Then
                            Exit Sub
                        End If

                        If li_confirm = DialogResult.Yes Then
                            lb_sobreescrEC = True
                        End If

                        ' Se verifica si se selecciono que se desea sobreescribir
                        If lb_sobreescrEC = True Then

                            ' Se reinicia la variable de confirmación para volver a usarla en el siguiente mensaje
                            li_confirm = -1

                            ' Se verifica el tipo de registro de EC a importar
                            If ls_check.Trim.ToLower = "y" Then

                                ' Se informa que el proceso sobreescribira solo los registros del mismo tipo seleccionado
                                li_confirm = MessageBox.Show("Solo se sobreescribirá los registros que Si se haya indicado como Registros para Planillas de Pagos Recibidos ( columna <esRegistroPll> ). ¿Desea continuar?", "caption", MessageBoxButtons.YesNoCancel)

                            Else

                                ' Se informa que el proceso sobreescribira solo los registros del mismo tipo
                                li_confirm = MessageBox.Show("Solo se sobreescribirá los registros que No se haya indicado como Registros para Planillas de Pagos Recibidos ( columna <esRegistroPll> ). ¿Desea continuar?", "caption", MessageBoxButtons.YesNoCancel)

                            End If

                            ' Se verifica el resultado del mensaje de confirmacion
                            If li_confirm <> DialogResult.Yes Then
                                Exit Sub
                            End If

                        End If

                    End If

                    ' Se indica que el registro existe en la tabla de Estado de Cuenta
                    lb_regExistEC = True

                End If

                ' Se verifica si el registro ya existe en alguna planilla
                If lo_row("existeEnPll").ToString.ToLower = "si" Then

                    ' Se indica que existen registros en la tabla de Estado de Cuenta
                    If lb_existPll = False Then

                        ' Se asigna TRUE al indicador de registros existentes en la tabla Estado de Cuenta
                        lb_existPll = True

                        ' Se reinicia la variable de confirmación para volver a usarla en el siguiente mensaje
                        li_confirm = -1

                        ' Se muestra un mensaje de confirmacion para determinar si se desea sobreescribir los registros existentes
                        li_confirm = MessageBox.Show("Existen registros con el mismo Número de Operación, la misma Fecha, el mismo monto y la misma Cuenta que ya han sido utilizados en alguna Planilla de Pagos Recibidos. Dichos registros no serán sobreescritos por el proceso de importación. ¿Desea continuar?", "caption", MessageBoxButtons.YesNoCancel)

                        ' Se verifica el resultado del mensaje de confirmacion
                        If li_confirm <> DialogResult.Yes Then
                            Exit Sub
                        End If

                    End If

                    ' Se indica que el registro actual existe en alguna planilla
                    lb_regExistPll = True

                End If

                ' Se verifica si existen registros en
                If lb_regExistPll = True Then
                    Continue For
                End If

                ' Se verifica si se indico que se debe sobreescribir los registro existentes en la tabla de Estado de Cuenta
                If lb_sobreescrEC = False Then

                    ' Se verifica si el registro actual existe en la tabla de Estado de Cuenta
                    If lb_regExistEC = True Then
                        Continue For
                    End If

                End If

                ' Se verifica si el registro actual corresponde al mismo tipo de registro señalado en el check
                If lb_regExistEC = True Then

                    If lo_row("esRegistroPll").ToString.ToLower <> str_obtValDeYesNoStr(ls_check).ToLower Then
                        Continue For
                    End If

                End If

                ' Se añade la fila actual al nuevo dataTable                
                lo_dtbImport.Rows.Add(lo_row.ItemArray)

            Next

            ' Se reinicia la variable de confirmación para volver a usarla en el siguiente mensaje
            li_confirm = -1

            ' Se muestra un mensaje de confirmacíon que indique si se desea visualizar los datos que serán importados
            li_confirm = MessageBox.Show("Antes de continuar, ¿desea visualizar los registros que serán importados?", "caption", MessageBoxButtons.YesNoCancel)

            ' Se verifica el resultado del mensaje de confirmacion
            If li_confirm = DialogResult.Yes Then

                ' Se declara un formulario de tipo Reporte para mostrar los registros que serán importados
                Dim lo_frmRpt As New frmReporte(lo_dtbImport, "Datos del Estado de Cuenta a importar")

                ' Se muestra el formulario como cuadro de dialogo
                lo_frmRpt.ShowDialog()

            End If

            ' Se reinicia la variable de confirmación para volver a usarla en el siguiente mensaje
            li_confirm = -1

            ' Se muestra otro mensaje de confirmación que preguntará al usuario si desea continuar
            li_confirm = MessageBox.Show("¿Desea continuar?", "caption", MessageBoxButtons.YesNoCancel)

            ' Se verifica el resultado del mensaje de confirmacion
            If li_confirm <> DialogResult.Yes Then
                Exit Sub
            End If

            ' Se realiza la adición de los registros en la base de datos
            Dim ls_resultado As String = entCargarEC.str_importarEC(lo_dtbImport)

            ' Se verifica el resultado del proceso
            If ls_resultado.Trim <> "" Then
                MsgBox(ls_resultado)
            Else
                MsgBox("El proceso de importación se realizó de manera exitosa.")
            End If

            ' Se asigna el dataTable obtenido como dataSource del Grid
            CType(lo_gridControl, uct_gridConBusqueda).sub_limpiar()

            ' Se muestra un reporte del Estado de Cuenta en el sistema
            sub_consultarEC()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#Region "Vald_procesosNegocio"

#Region "sub_mostrarEC"

    Private Function bol_valMostrarEC(ByVal po_fileInfo As FileInfo) As Boolean
        Try

            ' Se verifica que el parametro recibido no sea Nothing
            If po_fileInfo Is Nothing Then
                Return False
            End If

            ' Se valida que la extension del archivo sea Excel
            If Not po_fileInfo.Extension.ToString.ToLower.Contains("xls") Then
                MsgBox("El archivo seleccionado debe tener la extension <xls> o <xlsx>.")
                Return False
            End If

            ' Se valida que las columnas del excel correspondan a las columnas del Estado de Cuenta
            If sub_valColumnasEC(po_fileInfo) = False Then
                Return False
            End If

            ' Si todas las validaciones fueron correctos
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Function sub_valColumnasEC(ByVal po_fileInfo As FileInfo) As Boolean
        Try

            ' Se obtiene los datos del excel
            Dim lo_dtb As DataTable = dtb_obtDatosDesdeExcel(po_fileInfo)

            ' Se verifica si se obtuvo resultados 
            If lo_dtb Is Nothing Then
                sub_mostrarMensaje("No se pudo obtener datos desde el archivo Excel.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Return False
            End If

            ' Se verifica si se obtuvo mas columnas de las necesarias
            If lo_dtb.Columns.Count <> 6 Then
                MsgBox("Se encontro " & lo_dtb.Columns.Count & " columnas en el archivo excel; cuando solo debe existir seis (fecha, descripcion, monto, operacion, cuenta y ruc)")
                Return False
            End If

            ' Se verifica si existen las columnas minimas del EC
            If bol_existColumnaDtb(lo_dtb, "fecha") = False Then
                MsgBox("No se encontro la columna <fecha> en el excel del Estado de Cuenta. Los nombres de las columnas del Excel deben contener los siguientes nombres en letras minusculas, sin tildes y sin espacios: fecha, descripcion, monto, operacion y cuenta.")
                Return False
            End If

            If bol_existColumnaDtb(lo_dtb, "descripcion") = False Then
                MsgBox("No se encontro la columna <descripcion> en el excel del Estado de Cuenta. Los nombres de las columnas del Excel deben contener los siguientes nombres en letras minusculas, sin tildes y sin espacios: fecha, descripcion, monto, operacion y cuenta.")
                Return False
            End If

            If bol_existColumnaDtb(lo_dtb, "monto") = False Then
                MsgBox("No se encontro la columna <monto> en el excel del Estado de Cuenta. Los nombres de las columnas del Excel deben contener los siguientes nombres en letras minusculas, sin tildes y sin espacios: fecha, descripcion, monto, operacion y cuenta.")
                Return False
            End If

            If bol_existColumnaDtb(lo_dtb, "operacion") = False Then
                MsgBox("No se encontro la columna <operacion> en el excel del Estado de Cuenta. Los nombres de las columnas del Excel deben contener los siguientes nombres en letras minusculas, sin tildes y sin espacios: fecha, descripcion, monto, operacion y cuenta.")
                Return False
            End If

            If bol_existColumnaDtb(lo_dtb, "cuenta") = False Then
                MsgBox("No se encontro la columna <cuenta> en el excel del Estado de Cuenta. Los nombres de las columnas del Excel deben contener los siguientes nombres en letras minusculas, sin tildes y sin espacios: fecha, descripcion, monto, operacion y cuenta.")
                Return False
            End If

            If bol_existColumnaDtb(lo_dtb, "ruc") = False Then
                MsgBox("No se encontro la columna <ruc> en el excel del Estado de Cuenta. Los nombres de las columnas del Excel deben contener los siguientes nombres en letras minusculas, sin tildes y sin espacios: fecha, descripcion, monto, operacion y cuenta.")
                Return False
            End If

            ' Si la validacion fue correcta
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Function dtb_valRegistrosEC(ByVal po_dtb As DataTable) As DataTable
        Try

            ' Se declara dos variables para obtener el numero de registros en las tablas del estado de cuenta y en las tablas de planilla
            Dim li_existEC As Integer = 0
            Dim li_existPll As Integer = 0

            ' Se añade dos columnas al dataTable
            po_dtb.Columns.Add("existeEnEC")
            po_dtb.Columns.Add("idEC")
            po_dtb.Columns.Add("esRegistroPll")
            po_dtb.Columns.Add("existeEnPll")
            po_dtb.Columns.Add("idPll")
            po_dtb.Columns.Add("banco")
            po_dtb.Columns.Add("esPll")

            ' Se obtiene el checkBox indicador de registros para Planilla
            Dim lo_chk As Control = ctr_obtenerControl("regPll", o_form.Controls)

            ' Se verifica si se obtuvo el control
            If lo_chk Is Nothing Then
                sub_mostrarMensaje("Ocurrió un error al obtener el checkBox <regPll>.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Return Nothing
            End If

            ' Se declara un contador para las filas recorridas
            Dim li_contador As Integer = 1

            ' Se recorre los registros del dataTable
            For Each lo_row As DataRow In po_dtb.Rows

                ' Se verifica si se puede obtener los valores con el formato requerido
                If obj_intentarCast(lo_row("fecha"), GetType(Date)) = Nothing Then
                    MsgBox("No se pudo obtener el valor de la columna <fecha> en la fila " & li_contador + 1 & " del archivo Excel. Asegurese que la columna cuente con formato Fecha.")
                    Return Nothing
                End If

                If obj_intentarCast(lo_row("monto"), GetType(Double)) = Nothing Then
                    MsgBox("No se pudo obtener el valor de la columna <monto> en la fila " & li_contador + 1 & " del archivo Excel. Asegurese que la columna cuente con formato Numérico.")
                    Return Nothing
                End If

                ' Se verifica si en la base de datos existen registros con el mismo numero de operacion para la misma cuenta y la misma fecha
                Dim li_idEC As Integer = entCargarEC.int_verNroOperEC(CDate(lo_row("fecha")), obj_esNuloBD(lo_row("cuenta"), ""), obj_esNuloBD(lo_row("operacion"), ""), CDbl(lo_row("monto")))

                ' Se verifica si se encontro registros en la tabla de Estado de Cuenta
                If li_idEC > 0 Then

                    ' Se indica que el registro existe en la tabla de Estado de Cuenta
                    lo_row("existeEnEC") = "Si"
                    lo_row("idEC") = li_idEC

                    ' Se incrementa el contador
                    li_existEC = li_existEC + 1

                Else

                    ' Se indica que el registro existe en la tabla de Estado de Cuenta
                    lo_row("existeEnEC") = "No"

                End If


                ' Se verifica si en la base de datos existen registros con el mismo numero de operacion para la misma cuenta y la misma fecha que ya han sido registrados en planillas
                Dim li_idPll As Integer = entCargarEC.int_verNroOperECenPll(CDate(lo_row("fecha")), obj_esNuloBD(lo_row("cuenta"), ""), obj_esNuloBD(lo_row("operacion"), ""), CDbl(lo_row("monto")))

                ' Se verifica si se encontro registros en la tabla de Planillas
                If li_idPll > 0 Then

                    ' Se indica que el registro existe en la tabla de Planillas
                    lo_row("existeEnPll") = "Si"
                    lo_row("idPll") = li_idPll

                    ' Se incrementa el contador
                    li_existPll = li_existPll + 1

                Else

                    ' Se indica que el registro existe en la tabla de Planillas
                    lo_row("existeEnPll") = "No"

                End If

                ' Se obtiene el tipo de registro
                Dim ls_esRegPll As String = entCargarEC.str_verTipoRegEC(li_idEC)

                ' Se asigna el valor a la columna correspondiente
                lo_row("esRegistroPll") = str_obtValDeYesNoStr(ls_esRegPll)

                ' Se incrementa el valor de las filas recorridas
                li_contador = li_contador + 1

                ' Se verifica si el checkBox está marcado
                Dim ls_check As String = obj_obtValorControl(lo_chk)
                If ls_check.Trim.ToLower = "y" Then

                    ' Se asigna el valor a la columna
                    lo_row("esPll") = "Y"

                Else

                    ' Se asigna el valor a la columna
                    lo_row("esPll") = "N"

                End If

            Next

            ' Se asigna el dataTable obtenido como dataSource del Grid
            Dim lo_gridControl As Control = ctr_obtenerControl("infoEC", o_form.Controls)

            ' Se actualiza el dataSource del grid
            CType(lo_gridControl, uct_gridConBusqueda).sub_limpiar()
            sub_asigValorControl("infoEC", po_dtb)

            ' Se verifica si existe registros en las tablas de estado de cuenta y planillas
            If li_existEC > 0 Then
                If li_existPll > 0 Then
                    MsgBox("Hay " & li_existEC & " registro(s) que ya existe(n) en el sistema, de los cuales " & li_existPll & " ya está(n) registrado(s) en alguna planilla.")
                Else
                    MsgBox("Hay " & li_existEC & " registro(s) que ya existe(n) en el sistema.")
                End If
            End If

            ' Se retorna el dataTable
            Return po_dtb

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

#End Region

#Region "sub_importarEC"

    Private Function bol_valimportarEC(ByVal po_dtb As DataTable) As Boolean
        Try

            ' Se verifica si obttuvo alguna respuesta del proceso de importacion en SQL
            If po_dtb Is Nothing Then

                ' Se muestra un mensaje que indica que ocurrió un error al guardar la información en la base de datos
                sub_mostrarMensaje("Ocurrió un error al ejecutar la transacción.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)

                ' Se finaliza el metodo
                Return False

            End If

            ' Se verifica si se realizó la importacion
            If po_dtb.Columns.Count = 1 Then

                ' Se muestra un mensaje que indica que no se obtuvo datos para la importacion
                sub_mostrarMensaje("Ocurrió un error al intentar registrar la información en la base de datos. " & po_dtb.Rows(0)(0).ToString, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)

                ' Se finaliza el metodo
                Return False

            End If

            ' Si la validacion fue correcta
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Function bol_valCuentaBancaria() As Boolean
        Try

            ' Se obtiene el grid de detalle
            Dim lo_grid As uct_gridConBusqueda = ctr_obtenerControl("infoEC", o_form.Controls)

            ' Se verifica si se obtuvo el control
            If lo_grid Is Nothing Then
                sub_mostrarMensaje("No se pudo obtener la grilla de detalle.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Return False
            End If

            ' Se obtiene el dataTable del grid
            Dim lo_dtb As DataTable = lo_grid.DataSource

            ' Se verifica si se obtuvo el dataSource del control
            If lo_dtb Is Nothing Then
                MsgBox("No se pudo obtener los datos para la importación. Asegurese de haber seleccionado y mostrado un archivo Excel con el formato correcto del Estado de Cuenta.")
                Return False
            End If

            ' Se recorre las columnas del dataTable del Grid
            For Each lo_row As DataRow In lo_dtb.Rows

                ' Se obtiene el valor de la columna Cuenta
                Dim ls_cuenta As String = lo_row("Cuenta")

                ' Se verifica si la cuenta bancaria existe en SAP Business One
                Dim ls_codBanco As String = entComun.str_verExisteCuentaBancaria(ls_cuenta.Trim) ' Si existe el valor sera mayor a cero

                ' Se verifica el resultado de la busqueda
                If ls_codBanco = "" Then
                    MsgBox("No se pudo encontrar la cuenta " & ls_cuenta & " en SAP Business One.")
                    Return False
                End If

                ' Se asigna un numero de operacion automatico a los registro que no cuenten con este dato
                sub_asigNroOperAuto(lo_row, ls_codBanco)

            Next

            ' Se resetea el contador de numeros de operacion autogenerados
            i_contOpAuto = 1

            ' Se la validacion fue correcta, se retorna TRUE
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Sub sub_asigNroOperAuto(ByVal po_row As DataRow, ByVal ps_codBanco As String)
        Try

            ' Se asigna el codigo del banco a la columna correspondiente
            po_row("banco") = ps_codBanco

            ' Se verifica si el registro cuenta con numero de operacion
            Dim ls_nroOper As String = obj_esNuloBD(po_row("operacion"), "")

            ' Se verifica el valor obtenido
            If ls_nroOper.Trim.Replace(" ", "") <> "00" And ls_nroOper.Trim <> "" Then
                Exit Sub
            End If

            ' Se genera el nuevo numero de operacion
            ls_nroOper = po_row("banco").ToString & CDate(po_row("fecha")).ToString("yyyyMMdd") & "_" & i_contOpAuto.ToString

            ' Se asigna el valor a la column
            po_row("operacion") = ls_nroOper

            ' Se incrementa el contador de numeros de operacion autogenerados
            i_contOpAuto = i_contOpAuto + 1

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#End Region

#Region "Consultas"

    Public Sub sub_consultarEC()
        Try

            ' Se realiza la consulta hacia la tabla de Estado de Cuenta
            Dim lo_dtb As DataTable = entCargarEC.dtb_repEstadoCuenta

            ' Se declara una variable de tipo reporte
            Dim lo_frmReporte As New frmReporte(lo_dtb, "Estado de Cuenta")

            ' Se muestra el formulario de reporte
            lo_frmReporte.Show()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Public Sub sub_ECenPlanillas()
        Try

            ' Se realiza la consulta hacia la tabla de Estado de Cuenta
            Dim lo_dtb As DataTable = entPlanilla.dtb_obtECenPlanillas

            ' Se declara una variable de tipo reporte
            Dim lo_frmReporte As New frmReporte(lo_dtb, "Registros del Estado de Cuenta en las Planillas")

            ' Se muestra el formulario de reporte
            lo_frmReporte.Show()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Public Sub sub_repPagosNroOprInex()
        Try

            ' Se obtiene el resultado del reporte
            Dim lo_dtb As DataTable = dtb_obtDatosReporteConParams("gmi_sp_repPagosSinNroOper")

            ' Se verifica si se ejecuto la consulta para el reporte
            If lo_dtb Is Nothing Then
                Exit Sub
            End If
            If lo_dtb.Rows.Count = 0 Then
                Exit Sub
            End If

            ' Se genera un reporte Excel de los Pagos con Numero de Operacion Inexistentes
            sub_genExcelDesdeDataTable(lo_dtb, "Reporte de Pagos con Nros. de Operación Inexistentes")

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#Region "CheckPll"

    Private Sub sub_asigTipoRegistroEC(ByVal po_chk As CheckBox)
        Try

            ' Se obtiene el valor del checkBox
            Dim ls_chk As String = obj_obtValorControl(po_chk)

            ' Se obtiene el grid de la información del Estado de Cuenta
            Dim lo_gridControl As Control = ctr_obtenerControl("infoEC", o_form.Controls)

            ' Se verifica si se obtuvo el control
            If lo_gridControl Is Nothing Then
                sub_mostrarMensaje("No se pudo obtener el control <infoEC>.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Exit Sub
            End If

            ' Se obtiene el dataTable del control
            Dim lo_dtb As DataTable = CType(lo_gridControl, uct_gridConBusqueda).DataSource

            ' Se verifica si se obtuvo el dataTable
            If lo_dtb Is Nothing Then
                Exit Sub
            End If

            ' Se verifica el dataTable tiene registros
            If lo_dtb.Rows.Count = 0 Then
                Exit Sub
            End If

            ' Se recorre las filas del dataTable
            For Each lo_row As DataRow In lo_dtb.Rows

                ' Se asigna el valor a la columna correspondiente
                lo_row("esPll") = ls_chk

            Next

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

End Class
