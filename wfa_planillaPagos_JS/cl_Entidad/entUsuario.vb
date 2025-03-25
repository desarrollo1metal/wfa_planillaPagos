Imports Util

<annEntidad("gmi_sysUsr", "codigo", False)> _
Public Class entUsuario
    Inherits entComun

    ' Atributos 
    <annAtributo(True, False, "codigo", "Codigo del usuario", True, True, False, False, True)>
    Private s_codigo As String
    <annAtributo(True, False, "nombre", "Nombre del usuario", True, True, True, False, True)>
    Private s_nombre As String
    <annAtributo(True, False, "activo", "Activo", True, False, True)>
    Private s_activo As String
    <annAtributo(True, False, "admin", "Usuario Administrador", True, False, True)>
    Private s_admin As String
    <annAtributo(True, False, "password", "Password", True, False, True, False, True)>
    Private s_password As String

    ' Propiedades
    Public Property Codigo() As String
        Get
            Return s_codigo
        End Get
        Set(ByVal value As String)
            s_codigo = value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return s_nombre
        End Get
        Set(ByVal value As String)
            s_nombre = value
        End Set
    End Property

    Public Property Activo() As String
        Get
            Return s_activo
        End Get
        Set(ByVal value As String)
            s_activo = value
        End Set
    End Property

    Public Property Admin() As String
        Get
            Return s_admin
        End Get
        Set(ByVal value As String)
            s_admin = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return s_password
        End Get
        Set(ByVal value As String)
            s_password = value
        End Set
    End Property

End Class
