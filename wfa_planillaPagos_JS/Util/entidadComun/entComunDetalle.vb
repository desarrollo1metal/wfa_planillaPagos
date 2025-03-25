Imports System.Reflection
Imports Util
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO

Public Class entComunDetalle

    ' Atributos comunes
    <annAtributo(False)> _
    Protected o_lstObjs As New List(Of Object)
    <annAtributo(False)> _
    Protected o_seActualiza As Boolean = True

    ' Propiedades
    Public Property lstObjs() As List(Of Object)
        Get
            Return o_lstObjs
        End Get
        Set(ByVal value As List(Of Object))
            o_lstObjs = value
        End Set
    End Property

    Public Property seActualiza() As Boolean
        Get
            Return o_seActualiza
        End Get
        Set(ByVal value As Boolean)
            o_seActualiza = value
        End Set
    End Property


#Region "acciones_comunes"

    Public Sub sub_anadir()
        Try

            ' Se obtiene un objeto nuevo a partir de los datos de las propiedades del actual
            Dim lo_detalle As entComunDetalle = obj_crearObjDetalle(Me)

            ' Se duplica el objeto actual para introducirlo a la lista de objetos de detalle
            lstObjs.Add(lo_detalle)

            ' Se limpia las propiedades del objeto actual
            sub_limpiarPropiedadesActual(Me)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Public Function int_contar() As Integer
        Try
            Return o_lstObjs.Count
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Sub sub_limpiar()
        Try

            ' Se limpia el listado de objetos de detalle
            o_lstObjs.Clear()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Public Sub sub_obtLineaDetalle(ByVal pi_lineaNum As Integer)
        Try

            ' Se obtiene el detalle
            Dim lo_objDet As Object = o_lstObjs(pi_lineaNum)

            ' Se recorre los campos del objeto obtenido
            For Each lo_field As FieldInfo In lo_objDet.GetType.GetFields(BindingFlags.NonPublic Or BindingFlags.Public Or Reflection.BindingFlags.Instance)

                ' Se obtiene la anotacion del campo
                Dim lo_annAtr As annAtributo = ann_obtenerAnotacion(lo_field, GetType(annAtributo))

                ' Se verifica si se obtuvo la anotacion
                If lo_annAtr Is Nothing Then
                    Continue For
                End If

                ' Se verifica si es un atributo que corresponde a un campo de base de datos
                If lo_annAtr.esCampoBD = True Then

                    ' Se verifica si es un atributo que correspnde a un detalle
                    If lo_annAtr.esDetalle = False Then

                        ' Se obtiene el valor del campo
                        Dim lo_valor As Object = lo_field.GetValue(lo_objDet)

                        ' Se obtiene el campo del objeto actual
                        Dim lo_fieldActual As FieldInfo = Me.GetType.GetField(lo_field.Name)

                        ' Se asigna el valor al mismo campo del objeto actual
                        lo_fieldActual.SetValue(Me, lo_valor)

                    End If

                End If

            Next

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Public Sub sub_obtDesdeDataTable(ByVal po_dtb As DataTable)
        Try

            ' Se recorre las filas del dataTable
            For Each lo_row As DataRow In po_dtb.Rows

                ' Se declara un nuevo objeto de detalle
                Dim lo_objDetalle As Object = Activator.CreateInstance(Me.GetType)

                ' Se obtiene los atributos del objeto de detalle
                For Each lo_field As FieldInfo In Me.GetType.GetFields(BindingFlags.NonPublic Or BindingFlags.Public Or Reflection.BindingFlags.Instance)

                    ' Se obtiene la anotacion de atributo
                    Dim lo_annAtr As annAtributo = lo_field.GetCustomAttributes(True)(0)

                    ' Se verifica si el atributo corresponde a un campo de base de datos
                    If Not lo_annAtr Is Nothing Then
                        If lo_annAtr.esCampoBD = True Then

                            ' Se verifica si existe una columna en el dataTable que tenga el mismo nombre que el atributo actual
                            If Not po_dtb.Columns(lo_annAtr.campoBD) Is Nothing Then

                                ' Se asigna el valor de la columna al objeto
                                lo_field.SetValue(lo_objDetalle, obj_convertirATipo(lo_row(lo_annAtr.campoBD), lo_field.FieldType))

                            End If
                        End If
                    End If

                Next

                ' Se añade el objeto al listado de detalle
                o_lstObjs.Add(lo_objDetalle)

            Next

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Public Function dtb_obtEnDataTable(ByVal po_id As Object) As DataTable
        Try

            ' Se obtiene la anotacion de la entidad
            Dim lo_annEnt As annEntidad = ann_obtenerAnotacion(Me, GetType(annEntidad))

            ' Se verifica si se obtuvo la anotacion
            If lo_annEnt Is Nothing Then
                Return Nothing
            End If

            ' Se obtiene el nombre de la tabla
            Dim ls_tabla As String = lo_annEnt.Tabla
            Dim ls_idTabla As String = lo_annEnt.IdTabla

            ' Se declara una variable para la sentencia SQL
            Dim ls_sql As String = ""

            ' Se verifica si el objeto es un detalle
            If lo_annEnt.esDetalle = True Then
                ' Se verifica si se especifico un campo de orden enumeracion para el detalle
                If lo_annEnt.DetCampoEnum.Trim <> "" Then
                    ' Se obtiene los datos de la tabla
                    ls_sql = "select * from " & ls_tabla & " where " & ls_idTabla & " = " & str_obtenerParamSQLPorTipo(po_id) & " order by " & lo_annEnt.DetCampoEnum & " asc"
                Else
                    ' Se obtiene los datos de la tabla
                    ls_sql = "select * from " & ls_tabla & " where " & ls_idTabla & " = " & str_obtenerParamSQLPorTipo(po_id)
                End If
            Else
                ' Se obtiene los datos de la tabla
                ls_sql = "select * from " & ls_tabla & " where " & ls_idTabla & " = " & str_obtenerParamSQLPorTipo(po_id)
            End If



            ' Se ejecuta la consulta 
            Dim lo_dtb As DataTable = dtb_ejecutarSQL_NET(ls_sql)

            ' Se retorna el dataTable
            Return lo_dtb

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Private Sub sub_limpiarPropiedadesActual(ByVal po_objeto As Object)
        Try

            ' Se recorre los campos del objeto obtenido
            For Each lo_field As FieldInfo In po_objeto.GetType.GetFields(BindingFlags.NonPublic Or BindingFlags.Public Or Reflection.BindingFlags.Instance)

                ' Se obtiene la anotacion del campo
                Dim lo_annAtr As annAtributo = ann_obtenerAnotacion(lo_field, GetType(annAtributo))

                ' Se verifica si se obtuvo la anotacion
                If lo_annAtr Is Nothing Then
                    Continue For
                End If

                ' Se verifica si es un atributo que corresponde a un campo de base de datos
                If lo_annAtr.esCampoBD = True Then

                    ' Se verifica si es un atributo que correspnde a un detalle
                    If lo_annAtr.esDetalle = False Then

                        ' Se limpia la propiedad
                        lo_field.SetValue(po_objeto, Nothing)

                    End If

                End If

            Next

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
        End Try
    End Sub

    Public Function obj_crearObjDetalle(ByVal po_objeto As Object) As Object
        Try


            ' Se declara un nuevo objeto de detalle
            Dim lo_detalle As Object = Activator.CreateInstance(po_objeto.GetType)

            ' Se recorre los campos del objeto obtenido
            For Each lo_field As FieldInfo In po_objeto.GetType.GetFields(BindingFlags.NonPublic Or BindingFlags.Public Or Reflection.BindingFlags.Instance)

                ' Se obtiene la anotacion del campo
                Dim lo_annAtr As annAtributo = ann_obtenerAnotacion(lo_field, GetType(annAtributo))

                ' Se verifica si se obtuvo la anotacion
                If lo_annAtr Is Nothing Then
                    Continue For
                End If

                ' Se verifica si es un atributo que corresponde a un campo de base de datos
                If lo_annAtr.esCampoBD = True Then

                    ' Se verifica si es un atributo que correspnde a un detalle
                    If lo_annAtr.esDetalle = False Then

                        ' Se obtiene el valor del campo
                        Dim lo_valor As Object = lo_field.GetValue(po_objeto)

                        ' Se obtiene el campo del objeto actual
                        'Dim lo_fieldActual As FieldInfo = po_objeto.GetType.GetField(lo_field.Name)

                        ' Se asigna el valor al mismo campo del objeto actual
                        lo_field.SetValue(lo_detalle, lo_valor)

                    End If

                End If

            Next

            ' Se retorna el objeto
            Return lo_detalle

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

#End Region

End Class
