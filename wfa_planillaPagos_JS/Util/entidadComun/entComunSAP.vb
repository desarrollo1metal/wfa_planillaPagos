Imports System.Reflection
Imports System.Runtime.Remoting

Public Class entComunSAP

    ' Atributos
    Protected o_objetoSAP As Object
    Protected o_SBOCompany As SAPbobsCOM.Company
    Private i_businessObjectType As Integer

    ' Propiedades
    Public Property ObjetoSAP() As Object
        Get
            Return o_objetoSAP
        End Get
        Set(ByVal value As Object)
            o_objetoSAP = value
        End Set
    End Property

    Public ReadOnly Property SBOCompany() As SAPbobsCOM.Company
        Get
            Return o_SBOCompany
        End Get
    End Property

    Public Property businessObjectType() As Integer
        Get
            Return i_businessObjectType
        End Get
        Set(ByVal value As Integer)
            i_businessObjectType = value
        End Set
    End Property

#Region "Constructor"

    Public Sub New(ByVal po_SBOCompany As SAPbobsCOM.Company, ByVal po_objetoSAP As Object, ByVal pi_businessObjectType As Integer)
        Try

            ' Se inicializa las variables de la clase
            o_SBOCompany = po_SBOCompany
            o_objetoSAP = po_objetoSAP
            i_businessObjectType = pi_businessObjectType

            ' Se inicializa el objeto
            sub_iniObjetoSAP(po_SBOCompany, pi_businessObjectType)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

#Region "Inicializacion_Objeto"

    Private Sub sub_iniObjetoSAP(ByVal po_SBOCompany As SAPbobsCOM.Company, ByVal pi_businessObjectType As Integer)
        Try

            ' Se obtiene el tipo de objeto SAP
            o_objetoSAP = po_SBOCompany.GetBusinessObject(pi_businessObjectType)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

#Region "ProcesoNegocio_Comun"

    ' Añadir
    Protected Function str_anadir() As String
        Try

            ' Se obtiene el metodo añadir del objeto SAP
            Dim lo_method As MethodInfo = met_obtenerMetodo(o_objetoSAP, "Add")

            ' Se verifica si se obtuvo el metodo
            If lo_method Is Nothing Then
                Return "No se obtuvo el metodo <Add> del objeto."
            End If

            ' Se ejecuta el metodo y se obtiene el valor resultante
            Dim li_res As Integer = lo_method.Invoke(o_objetoSAP, New Object() {})

            ' Se verifica el resultado de la operacion
            If li_res <> 0 Then
                Return "Ocurrio un error al añadir el objeto SAP: " & o_SBOCompany.GetLastErrorCode.ToString & " - " & o_SBOCompany.GetLastErrorDescription
            End If

            ' Se retorna una cadena vacia si la operacion se realizó con exito
            Return ""

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message
        End Try
    End Function

    ' Actualizar
    Protected Function str_actualizar() As String
        Try

            ' Se obtiene el metodo actualizar del objeto SAP
            Dim lo_method As MethodInfo = met_obtenerMetodo(o_objetoSAP, "Update")

            ' Se verifica si se obtuvo el metodo
            If lo_method Is Nothing Then
                Return "No se obtuvo el metodo <Update> del objeto."
            End If

            ' Se ejecuta el metodo y se obtiene el valor resultante
            Dim li_res As Integer = lo_method.Invoke(o_objetoSAP, New Object() {})

            ' Se verifica el resultado de la operacion
            If li_res <> 0 Then
                Return "Ocurrio un error al actualizar el objeto SAP: " & o_SBOCompany.GetLastErrorCode.ToString & " - " & o_SBOCompany.GetLastErrorDescription
            End If

            ' Se retorna una cadena vacia si la operacion se realizó con exito
            Return ""

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message
        End Try
    End Function

    ' Cancelar
    Protected Function str_cancelar() As String
        Try

            ' Se obtiene el metodo cancelar del objeto SAP
            Dim lo_method As MethodInfo = met_obtenerMetodo(o_objetoSAP, "Cancel")

            ' Se verifica si se obtuvo el metodo
            If lo_method Is Nothing Then
                Return "No se obtuvo el metodo <Cancel> del objeto."
            End If

            ' Se ejecuta el metodo y se obtiene el valor resultante
            Dim li_res As Integer = lo_method.Invoke(o_objetoSAP, New Object() {})

            ' Se verifica el resultado de la operacion
            If li_res <> 0 Then
                Return "Ocurrio un error al cancelar el objeto SAP: " & o_SBOCompany.GetLastErrorCode.ToString & " - " & o_SBOCompany.GetLastErrorDescription
            End If

            ' Se retorna una cadena vacia si la operacion se realizó con exito
            Return ""

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message
        End Try
    End Function

#End Region

#Region "Busqueda"

    Public Function obj_obtPorCodigo(ByVal po_id As Object) As Object
        Try

            ' Se obtiene el metodo cancelar del objeto SAP
            Dim lo_method As MethodInfo = met_obtenerMetodo(o_objetoSAP, "getByKey")

            ' Se verifica si se obtuvo el metodo
            If lo_method Is Nothing Then
                Return "No se obtuvo el metodo <getByKey> del objeto."
            End If

            ' Se obtiene el objeto
            Dim lo_ver As Boolean = lo_method.Invoke(o_objetoSAP, New Object() {po_id})

            ' Se verifica si se obtuvo el objeto
            If lo_ver = False Then
                MsgBox("No se obtuvo un objeto para el id <" & po_id.ToString & ">")
            End If

            ' Se recorre los atributos del objeto actual
            For Each lo_field As FieldInfo In Me.GetType.GetFields(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance)

                ' Se obtiene el atributo SAP
                Dim lo_annAtr As annAtributo = ann_obtenerAnotacion(lo_field, GetType(annAtributo))

                ' Se verifica si se obtuvo la anotacion
                If lo_annAtr Is Nothing Then
                    Continue For
                End If

                ' Se verifica si el campo corresponde a un campo de base de datos
                If lo_annAtr.campoBD = False Then
                    Continue For
                End If

                ' Se obtiene la propiedad asociada al atributo actual desde el objeto SAP
                Dim lo_propInfo As PropertyInfo = o_objetoSAP.GetType.GetProperty(lo_annAtr.Propiedad)

                ' Se verifica si se obtuvo la propiedad
                If lo_propInfo Is Nothing Then
                    MsgBox("No se obtuvo la propiedad <" & lo_annAtr.Propiedad & ">.")
                    Continue For
                End If

                ' Se obtiene el valor de la propiedad
                Dim lo_valor As Object = lo_propInfo.GetValue(o_objetoSAP, Nothing)

                ' Se asigna el valor al campo
                lo_field.SetValue(Me, lo_valor)

            Next

            ' Se retorna el objeto
            Return Me

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
            Return Nothing
        End Try
    End Function

#End Region

End Class
