Imports Util
Imports System.Windows.Forms
Imports cl_Entidad

Public Class classMenu
    Inherits classComun

#Region "Constructor"

    Public Sub New(ByVal po_form As Form)
        MyBase.New(po_form)
    End Sub

#End Region

#Region "Ini_formulario"

    Public Overrides Sub sub_cargarForm()
        Try

            ' Se inicia el combo de menu
            sub_iniComboTipMenu()

            ' Se inicia el combo de menus tipo
            sub_iniComboMnuTitulo()

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub sub_iniComboTipMenu()
        Try

            ' Se indica el filtro correspondiente al listado de seleccion del parametro de clientes
            Dim lo_cboTipMenu As ComboBox = ctr_obtenerControl("tipoMenu", o_form.Controls)

            ' Se verifica si se obtuvo el comboBox
            If Not lo_cboTipMenu Is Nothing Then

                ' Se declara un dataTable para el estado
                Dim lo_dtb As New DataTable

                ' Se crea las columnas del datatable
                lo_dtb.Columns.Add("val")
                lo_dtb.Columns.Add("dscr")

                ' Se añade los estados al dataTable
                Dim lo_row As DataRow = lo_dtb.NewRow
                lo_row.BeginEdit()
                lo_row("val") = "0"
                lo_row("dscr") = "Titulo"
                lo_row.EndEdit()

                ' Se añade la fila al dataTable
                lo_dtb.Rows.Add(lo_row)

                ' Se limpia el objeto row
                lo_row = Nothing

                ' Se inicializa una nueva fila
                lo_row = lo_dtb.NewRow
                lo_row.BeginEdit()
                lo_row("val") = "1"
                lo_row("dscr") = "Funcion"
                lo_row.EndEdit()

                ' Se añade la fila al dataTable
                lo_dtb.Rows.Add(lo_row)

                ' Se limpia el objeto row
                lo_row = Nothing

                ' Se asigna el dataTable al Combo de Estado
                lo_cboTipMenu.DataSource = lo_dtb
                lo_cboTipMenu.ValueMember = "val"
                lo_cboTipMenu.DisplayMember = "dscr"

            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub sub_iniComboMnuTitulo()
        Try

            ' Se indica el filtro correspondiente al listado de seleccion del parametro de clientes
            Dim lo_cboTipMenu As ComboBox = ctr_obtenerControl("idMenuPadre", o_form.Controls)

            ' Se verifica si se obtuvo el comboBox
            If Not lo_cboTipMenu Is Nothing Then

                ' Se obtiene los menus de tipo Título
                Dim lo_dtb As DataTable = entMenu.dtb_obtMenuTitulo

                ' Se asigna el resultado de la consulta al combo
                If Not lo_dtb Is Nothing Then
                    lo_cboTipMenu.DataSource = lo_dtb
                    lo_cboTipMenu.ValueMember = "id"
                    lo_cboTipMenu.DisplayMember = "nomMenu"
                End If

            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Protected Overrides Function bol_validarModoAnadir(ByVal pb_antesAccion As Boolean) As Boolean
        Try

            ' Se verifica si la validacion se realiza luego de la accion
            If pb_antesAccion = False Then
                sub_iniComboMnuTitulo()
            End If

            ' Se retorna TRUE
            Return True

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return False
        End Try
    End Function

#End Region

#Region "val_accionPrin"

    Protected Overrides Function bol_valAlAnadir(ByVal pb_antesAccion As Boolean) As Boolean
        Try

            ' Se realiza las validaciones antes de añadir
            If pb_antesAccion = True Then
                Return bol_valCampoFuncion()
            Else
                sub_expDatosMenu()
            End If

            ' Si las validaciones fueron correctas, se retorna TRUE
            Return True

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return False
        End Try
    End Function

    Protected Overrides Function bol_valAlActualizar(ByVal pb_antesAccion As Boolean) As Boolean
        Try

            ' Se realiza las validaciones antes de añadir
            If pb_antesAccion = True Then
                Return bol_valCamposFuncionYMenuPadre()
            Else
                sub_expDatosMenu()
            End If

            ' Si las validaciones fueron correctas, se retorna TRUE
            Return True

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return False
        End Try
    End Function

    Private Function bol_valCamposFuncionYMenuPadre() As Boolean
        Try

            ' Se declara una variable para el resultado de las validaciones
            Dim lb_res As Boolean = True

            ' Se valida el campo funcion
            lb_res = bol_valCampoFuncion()

            ' Se verifica el resultado de la validacion
            If lb_res = False Then
                Return lb_res
            End If

            ' Se valida el campo menuPadre
            lb_res = bol_valMenuPadre()

            ' Se verifica el resultado de la validacion
            If lb_res = False Then
                Return lb_res
            End If

            ' Si las validaciones son correctas
            Return True

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return False
        End Try
    End Function

    Private Function bol_valCampoFuncion() As Boolean
        Try

            ' Se obtiene el control del campo Funcion u del campo tipo de menú
            Dim lo_txtFuncion As Control = ctr_obtControl("funcion")
            Dim lo_txtProyecto As Control = ctr_obtControl("proyecto")
            Dim lo_txtClase As Control = ctr_obtControl("clase")
            Dim lo_cboTipMenu As Control = ctr_obtControl("tipoMenu")

            ' Se verifica si se obtuvo el control
            If lo_txtFuncion Is Nothing Or lo_cboTipMenu Is Nothing Or lo_txtClase Is Nothing Or lo_txtProyecto Is Nothing Then
                Return False
            End If

            ' Se obtiene los valores de los controles
            Dim ls_funcion As String = obj_obtValorControl(lo_txtFuncion).ToString
            Dim ls_proyecto As String = obj_obtValorControl(lo_txtProyecto).ToString
            Dim ls_clase As String = obj_obtValorControl(lo_txtClase).ToString
            Dim ls_tipoMenu As String = obj_obtValorControl(lo_cboTipMenu)

            ' Se verifica si se selecciono algun valor en el tipoMenu
            If ls_tipoMenu Is Nothing Or ls_tipoMenu.Trim = "" Then
                MsgBox("Debe seleccionar un valor en el campo Tipo de Menú")
                Return False
            End If

            ' Se convierte el valor obtenido en entero
            Dim li_tipoMenu As Integer = CInt(obj_obtValorControl(lo_cboTipMenu))

            ' Se verifica el tipo de menu obtenido
            If li_tipoMenu = enm_tipoMenu.Funcion Then

                ' Se verifica si se especificó algún valor en el control
                If ls_funcion.Trim = "" Then
                    MsgBox("Debe especificar la función asociada al menú.")
                    Return False
                End If
                If ls_proyecto.Trim = "" Then
                    MsgBox("Debe ingresar el proyecto en donde se encuentra ubicada la clase donde se buscará la función del menú.")
                    Return False
                End If
                If ls_clase.Trim = "" Then
                    MsgBox("Debe ingresar la clase en donde se encuentra la función del menú.")
                    Return False
                End If

            Else

                ' Se verifica si se asignó algun valor al campo funcion
                If ls_funcion.Trim <> "" Then
                    sub_asigValorControl("funcion", "")
                    sub_asigValorControl("proyecto", "")
                    sub_asigValorControl("clase", "")
                End If

            End If

            ' Si la validación fue correcta, se retorna TRUE
            Return True

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return False
        End Try
    End Function

    Private Function bol_valMenuPadre() As Boolean
        Try

            ' Se verifica el modo del formulario
            If o_form.Modo = enm_modoForm.ACTUALIZAR Or o_form.Modo = enm_modoForm.VER Then

                ' Se obtiene el control de menu padre
                Dim lo_cboMenuPadre As Control = ctr_obtControl("idMenuPadre")

                ' Se obtiene el control Id
                Dim lo_txtId As Control = ctr_obtControl("id")

                ' Se verifica si se obtuvo el control
                If lo_cboMenuPadre Is Nothing Or lo_txtId Is Nothing Then
                    Return False
                End If

                ' Se verifica si ambos valores son iguales
                If CInt(obj_obtValorControl(lo_cboMenuPadre)) = CInt(obj_obtValorControl(lo_txtId)) Then

                    ' Se indica que un menu titulo no puede contar con el mismo id de menu padre
                    MsgBox("Un menú Título no puede tener como menú padre al mismo Id de menú.")

                    ' Se retorna el resultado
                    Return False

                End If

            End If

            ' Si la validacion es correcta, se retorna TRUE
            Return True

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return False
        End Try
    End Function

#End Region

#Region "Export_Menu"

    Public Sub sub_expDatosMenu()
        Try

            ' Se verifica que la acción se ejecute solo cuando el modo del formulario no sea BUSCAR
            If o_form.Modo = enm_modoForm.BUSCAR Then
                Exit Sub
            End If

            ' Se obtiene los datos de menu existentes en la base de datos
            Dim lo_dtb As DataTable = entMenu.dtb_obtDatosTabla(New entMenu)
            lo_dtb.TableName = "menu"

            ' Se guarda el dataTable en un archivo txt
            Dim lb_guardo As Boolean = bol_guardarDtbEnXML(lo_dtb, My.Application.Info.DirectoryPath & "\Menu\menu.txt")

            ' Se verifica si se guardo el archivo de manera correcta
            If lb_guardo = True Then
                MsgBox("Se exportó los datos de menú a XML de manera correcta.")
            Else
                MsgBox("No se pudo exportar los datos de menú.")
            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

End Class