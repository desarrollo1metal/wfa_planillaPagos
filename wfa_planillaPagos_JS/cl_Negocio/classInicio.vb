Imports System.IO
Imports System.Windows.Forms
Imports Util
Imports System.Drawing
Imports System.Reflection
Imports cl_Entidad
Imports OfficeOpenXml
Public Class classInicio
    Inherits classComun

#Region "Constructor"

    Public Sub New(ByVal po_form As Form)
        MyBase.New(po_form)

        ' Se realiza la conexion
        sub_conectar()

    End Sub

#End Region

#Region "Menu"

    Public Function bol_construirMenu() As Boolean
        Try

            ' Se obtiene el panel de menu
            Dim lo_panel As Control = ctr_obtControl("menu")

            ' Se verifica si se obtuvo el panel
            If lo_panel Is Nothing Then
                Return False
            End If

            ' Se obtienen los menus desde XML
            sub_construirMenu(lo_panel)

            ' Se retorna TRUE si todo fue correcto
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Function bol_obtMenusDeXML() As Boolean
        Try

            ' Se declara una variable para el resultado de las operaciones en la base de datos
            Dim ls_res As String = ""

            ' Se obtiene el dataTable desde el archivo XML de los Menus
            Dim lo_dtb As DataTable = dtb_obtenerDtbDeXML(My.Application.Info.DirectoryPath & "\Menu\menu.txt")

            ' Se verifica si se obtuvo el dataTable de los menus
            If lo_dtb Is Nothing Then
                MsgBox("No se pudo obtener los menús desde el archivo XML.")
                Return False
            End If

            ' Se ingresa los menus obtenidos en el dataTable
            ls_res = entMenu.str_insertMenuDesdeDTB(New entMenu, lo_dtb)

            ' Se verifica si la accion se realizo de manera correcta
            If ls_res.Trim <> "" Then
                MsgBox("OCurrio un error al actualizar los menús desde el archivo XML: " & ls_res)
                Return False
            End If

            ' Se retorna TRUE si todo fue correcto
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

    Private Sub sub_construirMenu(ByVal po_panel As Panel)
        Try

            ' Se obtiene los menus que no tienen menu padre
            Dim lo_dtb As DataTable = entMenu.dtb_sysObtMenusPorMnuPadre(-1)

            ' Se verifica si se obtuvo resultados en la busqueda
            If lo_dtb Is Nothing Then
                MsgBox("Ocurrió un error al obtener los menús.")
                Exit Sub
            End If

            ' Se verifica si se obtuvo registros de la consulta
            If lo_dtb.Rows.Count < 1 Then
                Exit Sub
            End If

            ' Se declara un objeto de tipo Menu
            Dim lo_entMenu As New entMenu

            ' Se obtiene un listado de objetos a partir de los resultados de la consulta
            Dim lo_lstMenus As List(Of Object) = lo_entMenu.obj_obtListaObjectos(lo_entMenu, lo_dtb)

            ' Se verifica si se obtuvo la lista de objetos
            If lo_lstMenus Is Nothing Then
                MsgBox("Ocurrio un error al obtener el listado de objetos de menú.")
                Exit Sub
            End If

            ' Se declara una variable local para las posiciones X e Y de los menus princiapales
            Dim li_x As Integer = 1
            Dim li_y As Integer = 1

            ' Se declara una variable para el nivel
            Dim li_nivel As Integer = 1

            ' Se crea un control de menú por cada objeto de la lista
            For Each lo_menu As entMenu In lo_lstMenus

                ' Se crea un nuevo control de tipo botonMenu
                Dim lo_uctBtnMenu As New uctBotonMenu

                ' Se asigna las propieades correspondientes al control
                lo_uctBtnMenu.Id = lo_menu.Id
                lo_uctBtnMenu.TipoMenu = lo_menu.TipoMenu
                lo_uctBtnMenu.Funcion = lo_menu.Funcion
                lo_uctBtnMenu.MenuPadre = lo_menu.IdMenuPadre
                lo_uctBtnMenu.NombreMenu = lo_menu.NombreMenu '.Insert(0, StrDup(li_nivel, "          "))
                lo_uctBtnMenu.Nivel = li_nivel
                lo_uctBtnMenu.Width = po_panel.Width
                lo_uctBtnMenu.Location = New Point(li_x, li_y)
                lo_uctBtnMenu.Clase = lo_menu.Clase
                lo_uctBtnMenu.Proyecto = lo_menu.Proyecto
                lo_uctBtnMenu.EsFormulario = lo_menu.EsFormulario
                lo_uctBtnMenu.configSis = lo_menu.ConfigSis

                ' Se incrementa la pocision Y
                li_y = li_y + 30

                ' Se añade el boton al manel
                po_panel.Controls.Add(lo_uctBtnMenu)

                ' Se asigna los eventos al boton
                sub_asigEventosBtnMenu(lo_uctBtnMenu)

                ' Se invoca al mismo metodo para verificar si el menu actual cuenta con subMenus
                int_construirSubMenu(po_panel, lo_menu.Id, li_x, li_y, li_nivel + 1)

            Next

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Function int_construirSubMenu(ByVal po_panel As Panel, Optional ByVal pi_mnuPadre As Integer = -1, Optional ByVal pi_x As Integer = 1, Optional ByVal pi_y As Integer = 1, Optional ByVal pi_nivel As Integer = 1) As Integer
        Try

            ' Se obtiene los menus que no tienen menu padre
            Dim lo_dtb As DataTable = entMenu.dtb_sysObtMenusPorMnuPadre(pi_mnuPadre)

            ' Se verifica si se obtuvo resultados en la busqueda
            If lo_dtb Is Nothing Then
                MsgBox("Ocurrió un error al obtener los menús.")
                Return pi_y
            End If

            ' Se verifica si se obtuvo registros de la consulta
            If lo_dtb.Rows.Count < 1 Then
                Return pi_y
            End If

            ' Se declara un objeto de tipo Menu
            Dim lo_entMenu As New entMenu

            ' Se obtiene un listado de objetos a partir de los resultados de la consulta
            Dim lo_lstMenus As List(Of Object) = lo_entMenu.obj_obtListaObjectos(lo_entMenu, lo_dtb)

            ' Se verifica si se obtuvo la lista de objetos
            If lo_lstMenus Is Nothing Then
                MsgBox("Ocurrio un error al obtener el listado de objetos de menú.")
                Return pi_y
            End If

            ' Se declara una variable local para la posicion Y de los menus princiapales
            Dim li_y As Integer = pi_y

            ' Se crea un control de menú por cada objeto de la lista
            For Each lo_menu As entMenu In lo_lstMenus

                ' Se crea un nuevo control de tipo botonMenu
                Dim lo_uctBtnMenu As New uctBotonMenu

                ' Se asigna las propieades correspondientes al control
                lo_uctBtnMenu.Id = lo_menu.Id
                lo_uctBtnMenu.TipoMenu = lo_menu.TipoMenu
                lo_uctBtnMenu.Funcion = lo_menu.Funcion
                lo_uctBtnMenu.MenuPadre = lo_menu.IdMenuPadre
                lo_uctBtnMenu.NombreMenu = lo_menu.NombreMenu '.Insert(0, StrDup(pi_nivel, "          "))
                lo_uctBtnMenu.Clase = lo_menu.Clase
                lo_uctBtnMenu.Proyecto = lo_menu.Proyecto
                lo_uctBtnMenu.EsFormulario = lo_menu.EsFormulario
                lo_uctBtnMenu.Nivel = pi_nivel
                lo_uctBtnMenu.Width = po_panel.Width
                lo_uctBtnMenu.Location = New Point(pi_x, li_y)
                lo_uctBtnMenu.Visible = False
                lo_uctBtnMenu.configSis = lo_menu.ConfigSis
                'lo_uctBtnMenu.PropiedadesBoton.Image.FromFile(

                ' Se incrementa la posición del menu
                'li_y = li_y + 30

                ' Se añade el boton al manel
                po_panel.Controls.Add(lo_uctBtnMenu)

                ' Se asigna los eventos al boton
                sub_asigEventosBtnMenu(lo_uctBtnMenu)

                ' Se invoca al mismo metodo para verificar si el menu actual cuenta con subMenus
                li_y = int_construirSubMenu(po_panel, lo_menu.Id, pi_x, li_y, pi_nivel + 1)

            Next

            ' Se retorna la posicion Y para continuar con la adicion de botones en la parte inferior
            Return pi_y

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return pi_y
        End Try
    End Function

    Private Sub sub_asigEventosBtnMenu(ByVal po_btnMenu As uctBotonMenu)
        Try

            ' Se asigna el evento al boton recibido
            AddHandler po_btnMenu.evt_btnMenuClick, AddressOf botonMenuClick

        Catch ex As Exception
            sub_registrarLog(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.info)
        End Try
    End Sub

    Private Sub botonMenuClick(ByVal sender As Object, ByVal e As EventArgs)
        Try

            ' Se obtiene el boton de menú desde el objeto sender
            Dim lo_btnMenu As uctBotonMenu = CType(sender, uctBotonMenu)

            ' Se ejecuta los eventos del botón
            sub_accionBotonMenu(lo_btnMenu)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_accionBotonMenu(ByVal po_btnMenu As uctBotonMenu)
        Try

            ' Se obtiene el panel de menu
            Dim lo_panel As Control = ctr_obtControl("menu")

            ' Se verifica si se obtuvo el panel
            If lo_panel Is Nothing Then
                Exit Sub
            End If

            ' Se obtiene el nivel del boton actual
            Dim li_nivelAct As Integer = po_btnMenu.Nivel

            ' Se declara una variable para obtener la cantidad incrementos de posiciones Y
            Dim li_incrPosY As Integer = 0

            ' Se declara un contador de subMenu
            Dim li_contSubMnu As Integer = 0

            ' Se verifica el tipo de menu asociado al boton
            If po_btnMenu.TipoMenu = enm_tipoMenu.Titulo Then

                ' Se recorre los controles dentro del panel de menu
                For Each lo_control As Control In lo_panel.Controls

                    ' Se verifica el tipo de control
                    If Not TypeOf (lo_control) Is uctBotonMenu Then
                        Continue For
                    End If

                    ' Se oculta o se muestra todos los botones que tengan como menu padre al boton actual
                    If CType(lo_control, uctBotonMenu).MenuPadre = po_btnMenu.Id Then

                        ' Se verifica si el control está visible
                        If lo_control.Visible = True Then
                            ' Se hace visible el boton de menu
                            lo_control.Visible = False
                            li_incrPosY = li_incrPosY - 1
                            lo_control.Location = New Point(lo_control.Location.X, po_btnMenu.Location.Y)
                        Else
                            ' Se hace visible el boton de menu
                            lo_control.Visible = True
                            li_contSubMnu = li_contSubMnu + 1
                            li_incrPosY = li_incrPosY + 1
                            lo_control.Location = New Point(lo_control.Location.X, po_btnMenu.Location.Y + 30 * li_contSubMnu)
                        End If

                    Else

                        ' Se oculta los botones que tengan un menu inferior al menu recibido
                        If CType(lo_control, uctBotonMenu).Nivel > po_btnMenu.Nivel Then
                            If lo_control.Visible = True Then
                                lo_control.Visible = False
                                li_incrPosY = li_incrPosY - 1
                            End If
                        Else
                            ' Se ajusta la posicion de los botones inferiores
                            lo_control.Location = New Point(lo_control.Location.X, lo_control.Location.Y + 30 * li_incrPosY)
                        End If

                    End If

                Next

            Else

                ' Se ejecuta la funcion asociada al menu
                sub_ejecMetodoMenu(po_btnMenu)

            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Private Sub sub_ejecMetodoMenu(ByVal po_btnMenu As uctBotonMenu)
        Try

            ' Se verifica si es un menu de configuracion del sistema
            If po_btnMenu.configSis.ToLower = "y" Then

                ' Se verifica si existe el usuario actual en el sistema
                Dim li_resultVer As Integer = int_verExistUsr(s_sysUsr)

                ' Se verifica si se obtuvo algun resultado
                If li_resultVer = -1 Then
                    MsgBox("Ocurrió un error al verificar la existencia del usuario en la base de datos.")
                    Exit Sub
                End If

                ' Se verifica si el usuario existe en la base de datos
                If li_resultVer = 0 Then
                    MsgBox("Esta opción solo está disponible para los administradores del sistema (El usuario no está registrado en el sistema).")
                    Exit Sub
                End If

                ' Se obtiene el objeto correspondiente al usuario actual
                Dim lo_usuario As New entUsuario
                lo_usuario = lo_usuario.obj_obtPorCodigo(s_sysUsr)

                ' Se verifica si se obtuvo el objeto
                If lo_usuario Is Nothing Then
                    MsgBox("Esta opción solo está disponible para los administradores del sistema (No se obtuvo el objeto por codigo).")
                    Exit Sub
                End If

                ' Se verifica si el usuario actual es un administrador del sistema
                If lo_usuario.Admin.ToLower <> "y" Then
                    MsgBox("Esta opción solo está disponible para los administradores del sistema.")
                    Exit Sub
                End If

            End If

            ' Se verifica si el menu asociado al boton invoca a un formulario
            If po_btnMenu.EsFormulario.ToLower = "y" Then

                ' Se invoca el formulario asociado al menu
                sub_invocarFormulario(po_btnMenu.Clase)

            Else

                ' Se obtiene la clase desde el assembly recibido
                Dim lo_objeto As Object = obj_obtObjeto(po_btnMenu.Proyecto, po_btnMenu.Clase, o_form)

                ' Se verifica si se obtuvo el objeto
                If lo_objeto Is Nothing Then
                    Exit Sub
                End If

                ' Se obtiene la funcion del menu
                Dim ls_funcion As String = po_btnMenu.Funcion

                ' Se declara un objeto de tipo metodo
                Dim lo_method As MethodInfo = met_obtenerMetodo(lo_objeto, ls_funcion)

                ' Se verifica si se obtuvo el metodo
                If lo_method Is Nothing Then
                    Exit Sub
                End If

                ' Se ejecuta el metodo
                lo_method.Invoke(lo_objeto, New Object() {})

            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

#End Region

#Region "Conexion"

    ' Se realiza la conexion
    Private Sub sub_conectar()
        Try

            ' Se declara un formulario de seleccion de compañia
            Dim lo_frmComp As New frmComp

            ' Se muestra un dialogro del formulario declarado
            lo_frmComp.ShowDialog()

            ' Se verifica si se obtuvo el valor de una compañia
            Dim ls_iniCom As String = lo_frmComp.s_comp
            If ls_iniCom.Trim = "" Then
                MsgBox("Debe seleccionar una compañia para poder acceder al aplicativo. Se cerrara la aplicacion.")
                Application.Exit()
                Exit Sub
            End If

            ' Se declara una entidad de tipo Conexion
            Dim lo_entConn As New entConn

            ' Se inicializa los componenetes
            lo_entConn = str_iniComponentes(ls_iniCom)

            ' Se verifica si ocurrio algun error
            If Not lo_entConn Is Nothing Then
                Dim lo_control As Control = ctr_obtenerControl("conexion", o_form.Controls)
                Dim ls_valor As String = obj_obtValorControl(lo_control)
                sub_asigValorControl("conexion", lo_entConn.SAPComp)
                o_entConn = lo_entConn
                sub_obtDatosConexion(lo_entConn)

                ' Se actualiza los datos de inicializacion que corresponden a la nueva version
                bol_iniComponentesVersion()

                ' Se verifica el usuario ingresado
                If bol_verUsuarioSistema() = False Then
                    sub_conectar()
                    Exit Sub
                End If

                ' Se construye el menu
                bol_construirMenu()

                ' Se registra el Log de ingreso al sistema
                sub_registrarLog("Ingreso a la aplicacion", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.info)

                ' Se verifica si existe el usuario administrador
                sub_verExistAdmin()

            End If

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    ' Se obtiene los datos de conexion
    Public Function str_iniComponentes(ByVal ps_iniCom As String) As Object
        Try

            ' Se declara una variables para el resultado de las operaciones
            Dim ls_res As String = ""

            ' Se declara una instancia de la clase que contiene los datos de la conexion
            Dim lo_entConn As New entConn

            ' Se asigna la compañia
            lo_entConn.company = ps_iniCom

            ' Se obtiene los datos de conexion
            Dim ls_result As String = lo_entConn.str_obtDatosConn()

            ' Se verifica si se obtuvo todos los datos de conexión necesarios
            If ls_result.Trim = "" Then

                If bol_comprobarConexion(lo_entConn.dbUser, lo_entConn.dbPass, lo_entConn.dbServ, lo_entConn.SAPComp) = True Then

                    ' Se crea los Stored Procedures utilizados por el add-on
                    ls_result = str_crearSQLProc()

                End If

            End If

            ' Se verifica si ocurrio algun error
            If ls_result.Trim <> "" Then
                MsgBox(ls_result)
                Return Nothing
            Else
                Return lo_entConn
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ' Se comprueba la conexion
    Public Function bol_comprobarConexion(ByVal ps_SQLUsrName As String _
                                        , ByVal ps_SQLPass As String _
                                        , ByVal ps_DBServer As String _
                                        , ByVal ps_SBOCompany As String) As Boolean
        Return ModuleSQLComun.bol_comprobarConexion(ps_SQLUsrName, ps_SQLPass, ps_DBServer, ps_SBOCompany)
    End Function

#End Region

#Region "Inicio_Componentes"

    ' Se crea los objetos SQL ubicados en el directorio de archivos SQL de la aplicacion
    Private Function str_crearSQLProc() As String
        Try

            ' Se declara una variables para el resultado de las operaciones
            Dim ls_res As String = ""
            Dim ls_errObjSQL As String = ""

            ' Se obtiene la ruta de la aplicacion
            Dim ls_ruta As String = My.Application.Info.DirectoryPath
            ls_ruta = ls_ruta & "\SQLProc"

            ' Se obtiene el directorio donde se encuentran los procedimientos SQL
            Dim lo_directory As New DirectoryInfo(ls_ruta)

            ' Se obtiene los archivos del directorio 
            Dim lo_files As FileInfo() = lo_directory.GetFiles

            ' Se recorre los archivos del directorio
            For Each lo_file As FileInfo In lo_files

                ' Se verifica que el archivo tenga extension SQL
                If lo_file.Extension.ToLower = ".sql" Then

                    ' Se obtiene el texto del archivo
                    Dim ls_texto As String = My.Computer.FileSystem.ReadAllText(lo_file.FullName)
                    'Dim ls_etqProc As String = "/*<tipoSQL>*/procedure/*</tipoSQL>*/ "
                    Dim ls_etqProc As String = "/*<tipoSQL>*"

                    ' Se verifica que el archivo corresponda a un procedure SQL
                    If ls_texto.ToLower.Contains(ls_etqProc.ToLower) Then

                        ' Se obtiene el nombre del procedure
                        Dim ls_nombreObj As String = ls_texto.Substring(ls_texto.ToLower.IndexOf("<nombre>") + "<nombre>".Length, ls_texto.ToLower.IndexOf("</nombre>") - (ls_texto.ToLower.IndexOf("<nombre>") + "<nombre>".Length))
                        Dim ls_tipoSQL As String = ls_texto.Substring(ls_texto.ToLower.IndexOf("<tiposql>") + "<tiposql>".Length, ls_texto.ToLower.IndexOf("</tiposql>") - (ls_texto.ToLower.IndexOf("<tiposql>") + "<tiposql>".Length))

                        ' Se elimina los caracteres sobrantes del archivo SQL en el nombre
                        ls_nombreObj = ls_nombreObj.Replace("/", "").Replace("*", "")
                        ls_tipoSQL = ls_tipoSQL.Replace("/", "").Replace("*", "")

                        ' Se verifica si existe el objeto SQL en la base de datos
                        Dim li_indExist As Integer = int_indExistObjSQL(ls_nombreObj)

                        ' Si el objeto SQL existe, se actualiza el objeto
                        If li_indExist > 0 Then

                            ' Se verifica el tipo de objeto SQL
                            If ls_tipoSQL.ToLower.Contains("table") Then ' Si es una tabla

                                ' Se recorre los campos de la tabla en el texto
                                ls_res = str_crearCamposTablaEnArchivo(lo_file.FullName, ls_nombreObj)

                                ' Se guarda el nombre del objeto SQL que devolvió un error
                                If ls_res.Trim <> "" Then
                                    ls_errObjSQL = ls_res & " - " & ls_nombreObj
                                    MsgBox("Ocurrio un error al crear el objeto SQL <" & ls_tipoSQL & "> con nombre <" & ls_nombreObj & ">: " & ls_res)
                                End If

                                ' Se continua el FOR
                                Continue For

                            End If

                            ' Si no es una tabla
                            ' Se asigna al query la accion ALTER
                            ls_texto = ls_texto.Replace("/*<accion>*/create/*</accion>*/ ".ToLower, "/*<accion>*/alter/*</accion>*/".ToLower)

                            ' Se ejecuta la sentencia SQL
                            ls_res = str_ejecutarQuery(ls_texto)

                            ' Se guarda el nombre del objeto SQL que devolvió un error
                            If ls_res.Trim <> "" Then
                                ls_errObjSQL = ls_res & " - " & ls_nombreObj
                                MsgBox("Ocurrio un error al crear el objeto SQL <" & ls_tipoSQL & "> con nombre <" & ls_nombreObj & ">: " & ls_res)
                            End If

                        Else ' Si no existe, se crea el objeto

                            ' Se asigna al query la accion CREATE
                            ls_texto = ls_texto.Replace("/*<accion>*/alter/*</accion>*/ ".ToLower, "/*<accion>*/create/*</accion>*/".ToLower)

                            ' Se ejecuta la sentencia SQL
                            ls_res = str_ejecutarQuery(ls_texto)

                            ' Se guarda el nombre del objeto SQL que devolvió un error
                            If ls_res.Trim <> "" Then
                                ls_errObjSQL = ls_res & " - " & ls_nombreObj
                                MsgBox("Ocurrio un error al crear el objeto SQL <" & ls_tipoSQL & "> con nombre <" & ls_nombreObj & ">: " & ls_res)
                            End If

                        End If

                    End If

                End If

            Next

            ' Se verifica si ocurrio errores
            If ls_res.Trim <> "" Then
                ls_res = "Ocurrió errores al intentar crear o actualizar los siguientes objetos: " & ls_errObjSQL
            End If

            ' Se retorna el resultado
            Return ls_res

        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Private Function str_crearCamposTablaEnArchivo(ByVal ps_ruta As String, ByVal ps_nomTabla As String) As String
        Try

            ' Se declara una variable para el resultado de la sentencia SQL
            Dim ls_res As String = ""

            ' Se declara una variable para la sentencia SQL que añadira los campos a la tabla
            Dim ls_sql As String = ""
            Dim ls_separador As String = ""

            ' Se declara una variable para leer cada linea del archivo
            Dim ls_linea As String = ""

            ' Se obtiene el archivo de la ruta recibida
            Using lo_sr As New StreamReader(ps_ruta)
                While Not lo_sr.EndOfStream

                    ' Se obtiene la linea
                    ls_linea = lo_sr.ReadLine

                    ' Se verifica si la linea actual corresponde a un campo
                    If ls_linea.ToLower.Trim.Contains("<campo>") Then

                        ' Se verifica si el campo se encuentra comentado
                        If Mid(ls_linea.ToLower.Trim, 1, 2) <> "--" Then

                            ' Se declara una variable para obtener el nombre del campo
                            Dim ls_nomCampo As String = ls_linea.Substring(ls_linea.ToLower.IndexOf("<campo>") + "<campo>".Length, ls_linea.ToLower.IndexOf("</campo>") - (ls_linea.ToLower.IndexOf("<campo>") + "<campo>".Length))

                            ' Se limpia nombre del campo
                            ls_nomCampo = ls_nomCampo.Replace(",", "")
                            ls_nomCampo = ls_nomCampo.Replace("/", "").Replace("*", "")
                            ls_nomCampo = ls_nomCampo.Trim

                            ' Se verifica si el campo existe en la base de datos
                            Dim li_verExistCampo As Integer = int_verExistCampoDeTable(ls_nomCampo, ps_nomTabla)
                            If li_verExistCampo = 0 Then

                                ' Se inicializa la sentencia SQL para la adicion de campos
                                If ls_sql.Trim = "" Then
                                    ls_sql = "alter table " & ps_nomTabla & " add "
                                End If

                                ' Se añade la sintaxis de adicion del campo
                                ls_sql = ls_sql & ls_separador & " " & ls_linea.Replace(",", "")

                                ' Se indica el separador de campos
                                ls_separador = ", "

                            End If

                        End If

                    End If

                End While

            End Using

            ' Se verifica si se construyo la sentencia 
            If ls_sql.Trim <> "" Then

                ' Se ejecuta la sentencia 
                ls_res = str_ejecutarQuery(ls_sql)

            End If

            ' Se recorre las lineas del archivo de texto
            Return ls_res

        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Private Function bol_iniComponentesVersion() As Boolean
        Try

            ' Se verifica la versión del aplicativo
            Dim ll_versionApp As Long = lon_obtVersionAPP()

            ' Se obtiene la version del aplicativo registrada en la base de datos
            Dim ll_versionAppBD As Long = lon_obtVersion(o_entConn.SAPComp)

            ' Se verifica si se obtuvo las versiones de la aplicacion a comparar
            If ll_versionApp = -1 Then
                MsgBox("No se obtuvo la versión de configuración registrada en el archivo TXT de  la versión.")
                Return False
            End If
            If ll_versionAppBD = -1 Then

                ' Se muestra un mensaje que indica que no se encontro la versión en la base de datos
                MsgBox("No se obtuvo la versión de configuración registrada en la base de datos.")

                ' Se construye el menu desde el archiv XML y se ingresa a la base de datos los valores correspondientes
                bol_obtMenusDeXML()

                ' Se retorna FALSE para no continuar con el proces
                Return False

            End If

            ' Se declara una variable para el resultado de las operaciones en la base de datos
            Dim ls_res As String = ""

            ' Se verifica si la versión si la versión de la aplicacion es mayor a la versión registrada en la base de datos
            If ll_versionApp > ll_versionAppBD Then

                ' Se obtiene los menus desde el archivo XML correspondiente a la version actual
                Dim lb_res As Boolean = bol_obtMenusDeXML()

                ' Se verifica si la operacion se realizó de manera correcta
                If lb_res = False Then
                    Return False
                End If

                ' Se obtiene el objeto de configuracion
                Dim lo_config As New entConfig
                lo_config = lo_config.cfg_obtConfiguracionApp

                ' Se verifica si se obtuvo el objeto
                If lo_config Is Nothing Then
                    MsgBox("No se encontro un registro de configuracion para esta base de datos. Inicie la opción de Configuracion para crear un registro para esta base de datos")
                    Return False
                End If

                ' Se actualiza la version de la aplicacion en la base de datos
                lo_config.Version = ll_versionApp
                ls_res = lo_config.str_actualizar()

                ' Se verifica si se actualizo la version de la aplicacion 
                If ls_res.Trim <> "" Then
                    MsgBox("Ocurrio un error al actualizar la version de la aplicacion: " & ls_res)
                End If

            End If

            ' Se retorna TRUE si todas las operaciones fueron correcta
            Return True

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return False
        End Try
    End Function

#End Region

#Region "VerificacionUsuariosSys"

    Private Sub sub_verExistAdmin()
        Try

            ' Se verifica si existe el usuario administrador
            Dim ls_resVer As String = str_verUsrAdmin()

            ' Se verifica si la operacion se realizó de manera correcta
            If ls_resVer.Trim <> "" Then
                sub_mostrarMensaje("Ocurrió un error al verificar el usuario administrador: " & ls_resVer, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.info)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function bol_verUsuarioSistema() As Boolean
        Try

            ' Se verifica si el usuario ingresado es igual al usuario de windows que ha ingresado
            If s_sysUsr.Trim = str_obtWinUsr.Trim Then
                Return True
            End If

            ' Se verifica si el usuario existe en la base de datos
            Dim li_ver As Integer = int_verExistUsr(s_sysUsr)
            If li_ver < 1 Then
                sub_mostrarMensaje("El usuario " & s_sysUsr & " es incorrecto o no existe. Por favor, ingrese con un usuario válido o utilice el usuario por defecto.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_val)
                Return False
            End If

            ' Se encripta el password obtenido
            Dim ls_sysPassEncript As String = str_encriptar(s_sysPass)

            ' Se verifica si se obtuvo el texto encriptado
            If ls_sysPassEncript.Trim = "" Then
                sub_mostrarMensaje("Ocurrió un error al encriptar el password.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_sis)
                Return False
            End If

            ' Se verifica el password del usuario
            li_ver = int_verPassUsr(s_sysUsr, ls_sysPassEncript)

            ' Se verifica si existe el usuario
            If li_ver < 1 Then
                sub_mostrarMensaje("La constraseña del usuario ingresado es incorrecta.", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_val)
                Return False
            End If

            ' Si todas las validaciones fueron correctas, se retorna TRUE
            Return True

        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

#End Region

End Class
