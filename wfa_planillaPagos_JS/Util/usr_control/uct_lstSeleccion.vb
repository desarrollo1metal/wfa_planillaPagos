Imports System.Globalization
Imports System.Runtime.Remoting
Imports System.Reflection

Public Class uct_lstSeleccion

    ' Atributos
    Private o_value As Object
    Private s_text As String = ""
    Private s_tabla As String = ""
    Private s_campoVal As String = ""
    Private s_campoTxt As String = ""
    Private s_mostrarCampo As String = ""
    Private o_dtbCond As New DataTable
    Private o_objBusqueda As Object

    ' Eventos
    Public Event evt_modificacionTxt As EventHandler

    ' Propiedades
    Public Property Value() As Object
        Get
            Return o_value
        End Get
        Set(ByVal value As Object)
            o_value = value
        End Set
    End Property

    Public Property Texto() As String
        Get
            Return s_text
        End Get
        Set(ByVal value As String)
            s_text = value
            txtVal.Text = value
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

    Public Property campoVal() As String
        Get
            Return s_campoVal
        End Get
        Set(ByVal value As String)
            s_campoVal = value
        End Set
    End Property

    Public Property CampoTxt() As String
        Get
            Return s_campoTxt
        End Get
        Set(ByVal value As String)
            s_campoTxt = value
            txtVal.Tag = s_campoTxt
        End Set
    End Property

    Public Property mostrarCampo() As String
        Get
            Return s_mostrarCampo
        End Get
        Set(ByVal value As String)
            s_mostrarCampo = value
        End Set
    End Property

    ' Constructor
    Public Sub New()

        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        sub_iniDtbCondiciones()

        ' Se inicializa el objeto asociado al control de usuario
        sub_inicializarObjetoComun()

    End Sub

#Region "Inicializacion"

    Private Sub sub_inicializarObjetoComun()
        Try

            '' Se obtiene el objeto comun desde donde se realizan las consultas
            'Dim lo_objHandle As ObjectHandle = Activator.CreateInstance("cl_Entidad", "cl_Entidad.entComun")
            'Dim lo_entidad As Object = lo_objHandle.Unwrap

            '' Se asigna el objeto obtenido a la variable del control
            'o_objBusqueda = lo_entidad
            o_objBusqueda = New entComun

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

#Region "Eventos"

    ' Eventos
    Private Sub btnMostrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMostrar.Click
        sub_mostrarListaSelec()
    End Sub

    Private Sub txtVal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVal.TextChanged
        sub_evtCambioTexto()
        RaiseEvent evt_modificacionTxt(sender, e)
    End Sub
    '
#End Region

#Region "MetodosFunciones"

#Region "Condicion"

    ' Inicializacion del dataTable de las condiciones
    Private Sub sub_iniDtbCondiciones()
        Try

            ' Se añade las columnas del dataTable de condiciones
            o_dtbCond.Columns.Add("nom")
            o_dtbCond.Columns.Add("val")

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    ' Adicion de condiciones para los datos del listado de seleccion
    Public Sub sub_anadirCondicion(ByVal ps_campo As String, ByVal po_valor As Object)
        Try

            ' Se crea una nueva fila
            Dim lo_row As DataRow = o_dtbCond.NewRow
            lo_row.BeginEdit()
            lo_row("nom") = ps_campo
            lo_row("val") = po_valor
            lo_row.EndEdit()

            ' Se añade la fila al dataTable
            o_dtbCond.Rows.Add(lo_row)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    ' Se construye la condicion para la consulta del listado
    Private Function str_construirCondicion() As String
        Try

            ' Se recorre el dataTable de condiciones
            Dim ls_condicion As String = str_genCondicionBusqueda(o_dtbCond, s_tabla)

            ' Se retorna la condicion
            Return ls_condicion

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return ""
        End Try
    End Function

#End Region

#Region "Busqueda"

    ' Se construye la consulta para busqueda
    Private Function dtb_construirConsulta() As DataTable
        Try

            ' Se construye la consulta a partir de la tabla y los campos señalados como valor, texto y las condiciones
            Dim ls_sql As String = "select " & s_campoVal & ", " & s_campoTxt & " from " & s_tabla & " where 1 = 1 " & str_construirCondicion()

            ' Como el metodo que construye la condicion esta elaborado para enviar la condicion sql como parametro, se debe reemplazar las dos comillas simples juntas por una sola
            ls_sql = ls_sql.Replace("''", "'")

            ' Se ejecuta la consulta y se obtiene el resultado en un dataTable
            ' - Se obtiene el metodo que obtendra el DataTable con el resultado de la busqueda
            Dim lo_method As MethodInfo = o_objBusqueda.GetType.GetMethod("dtb_ejcConsultaSQL")
            Dim lo_dtb As DataTable = CType(lo_method.Invoke(o_objBusqueda, New Object() {ls_sql}), DataTable)
            'Dim lo_dtb As DataTable = CNBusqueda.dtb_buscar(ls_sql)

            ' Se retorna el dataTable
            Return lo_dtb

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

    Private Sub sub_mostrarVentanaSelec(ByVal po_dtb As DataTable)
        Try

            ' Se crea el formulario de busqueda
            Dim lo_frmBuscar As New frmBuscar(po_dtb, s_mostrarCampo, txtVal.Text)

            ' Se muestra el formulario de busqueda
            lo_frmBuscar.ShowDialog()

            ' Se obtiene los datos seleccionados
            If lo_frmBuscar.int_contarSelecc = 1 Then

                ' Se asigna los valores obtenidos a las propiedades
                Me.Value = lo_frmBuscar.obj_obtenerValorSel(s_campoVal)
                Me.Texto = lo_frmBuscar.obj_obtenerValorSel(s_mostrarCampo)

            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub sub_mostrarListaSelec()
        Try

            ' Se muestra la ventana de seleccion
            sub_mostrarVentanaSelec(dtb_construirConsulta)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Function str_genCondicionBusqueda(ByVal po_dtb As DataTable, ByVal ps_tabla As String) As String
        Try

            ' Se declara una variable para la condicion de la consulta
            Dim ls_condicion As String = ""

            ' Se recorre el dataTable con los nombres de los campos de parametro y su respectivo valor
            For Each lo_row In po_dtb.Rows

                ' Se verifica el tipo de dato del campo recibido
                ' - Se obtiene el metodo que obtendra el DataTable con el campo
                Dim lo_method As MethodInfo = o_objBusqueda.GetType.GetMethod("dtb_obtCampo")
                Dim lo_dtb As DataTable = CType(lo_method.Invoke(o_objBusqueda, New Object() {ps_tabla, lo_row("nom")}), DataTable)
                'Dim lo_dtb As DataTable = dtb_obtCampo(ps_tabla, lo_row("nom"))

                ' Se asume que la consulta solo devolvera un campo
                If Not lo_dtb Is Nothing And lo_dtb.Rows.Count = 1 Then

                    ' Se verifica el tipo de dato y, de acuerdo a eso, se arma la condicion where de la consulta de busqueda
                    If lo_dtb.Rows(0)("Tipo").ToString.ToLower.Contains("char") Then
                        If Not lo_row("val") Is Nothing Then
                            If Microsoft.VisualBasic.Left(lo_row("val").ToString, 1) = "*" Then
                                ls_condicion = ls_condicion & " and " & lo_row("nom") & " like ''%" & lo_row("val").ToString.Replace("*", "") & "%'' "
                            Else
                                ls_condicion = ls_condicion & " and " & lo_row("nom") & " like ''" & lo_row("val") & "%'' "
                            End If
                        End If

                    ElseIf lo_dtb.Rows(0)("Tipo").ToString.ToLower.Contains("int") Then
                        ls_condicion = ls_condicion & " and " & lo_row("nom") & " = " & CStr(lo_row("val")) & " "
                    ElseIf lo_dtb.Rows(0)("Tipo").ToString.ToLower.Contains("date") Then
                        ls_condicion = ls_condicion & " and " & lo_row("nom") & " = ''" & CDate(lo_row("val")).ToString("yyyyMMdd", CultureInfo.InvariantCulture) & "'' "
                    End If

                End If

            Next

            ' Se retorna la cadena que contiene la condicion
            Return ls_condicion

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

#End Region

#Region "CajaTexto"

    ' Se maneja los eventos realizados al modificar el texto de la caja de texto principal
    Private Sub sub_evtCambioTexto()
        Try

            ' Se verifica si la caja de texto se encuentra vacia
            If txtVal.Text.Trim = "" Then

                ' Se limpia los valores del control
                Value = ""
                Texto = ""
            Else

                ' Se verifica si el texto de la caja de texto es diferente a la propiedad texto
                If txtVal.Text.Trim <> Texto.Trim Then

                    ' Se muestra el listado de seleccion con el valor de la caja de texto como parametro
                    sub_mostrarListaSelec()

                End If

            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

#Region "AsigValor"



#End Region

#End Region

    
End Class