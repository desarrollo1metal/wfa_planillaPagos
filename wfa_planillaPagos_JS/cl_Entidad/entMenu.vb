Imports Util

<annEntidad("gmi_sysMenu", "id")> _
Public Class entMenu
    Inherits entComun

    ' Atributos
    <annAtributo(True, False, "id", "Id. del Menu", False, True, False)>
    Private i_id As Integer
    <annAtributo(True, False, "nomMenu", "Nombre del Menu", True, True, True, False, True)>
    Private s_nomMenu As String
    <annAtributo(True, False, "tipoMenu", "Tipo de Menu", True, True, True, False, True)>
    Private i_tipoMenu As Integer
    <annAtributo(True, False, "idMenuPadre", "Id. Menu Padre", True, True)>
    Private i_idMenuPadre As Integer
    <annAtributo(True, False, "ordenMenu", "Posicion Menu", True, True)>
    Private i_ordenMenu As Integer
    <annAtributo(True, False, "funcion", "Funcion del Menu", True, True, True, False, False)>
    Private s_funcion As String
    <annAtributo(True, False, "activo", "Activo", True, False, True)>
    Private s_activo As String
    <annAtributo(True, False, "clase", "Clase", True, True, True)>
    Private s_clase As String
    <annAtributo(True, False, "proyecto", "Proyecto", True, True, True)>
    Private s_proyecto As String
    <annAtributo(True, False, "form", "Formulario", True, False, True)>
    Private s_form As String
    <annAtributo(True, False, "configSis", "Menu de Configuracion del Sistema", True, False, True)>
    Private s_configSis As String

    ' Propiedades
    Public Property Id() As Integer
        Get
            Return i_id
        End Get
        Set(ByVal value As Integer)
            i_id = value
        End Set
    End Property

    Public Property NombreMenu() As String
        Get
            Return s_nomMenu
        End Get
        Set(ByVal value As String)
            s_nomMenu = value
        End Set
    End Property

    Public Property TipoMenu() As Integer
        Get
            Return i_tipoMenu
        End Get
        Set(ByVal value As Integer)
            i_tipoMenu = value
        End Set
    End Property

    Public Property IdMenuPadre() As Integer
        Get
            Return i_idMenuPadre
        End Get
        Set(ByVal value As Integer)
            i_idMenuPadre = value
        End Set
    End Property

    Public Property OrdenMenu() As Integer
        Get
            Return i_ordenMenu
        End Get
        Set(ByVal value As Integer)
            i_ordenMenu = value
        End Set
    End Property

    Public Property Funcion() As String
        Get
            Return s_funcion
        End Get
        Set(ByVal value As String)
            s_funcion = value
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

    Public Property Clase() As String
        Get
            Return s_clase
        End Get
        Set(ByVal value As String)
            s_clase = value
        End Set
    End Property

    Public Property Proyecto() As String
        Get
            Return s_proyecto
        End Get
        Set(ByVal value As String)
            s_proyecto = value
        End Set
    End Property

    Public Property EsFormulario() As String
        Get
            Return s_form
        End Get
        Set(ByVal value As String)
            s_form = value
        End Set
    End Property

    Public Property ConfigSis() As String
        Get
            Return s_configSis
        End Get
        Set(ByVal value As String)
            s_configSis = value
        End Set
    End Property

#Region "Consultas"

    Public Shared Function dtb_obtMenuTitulo() As DataTable
        Return sqlMenu.dtb_obtMenuTitulo
    End Function

    Public Shared Function dtb_sysObtMenusPorMnuPadre(ByVal pi_id As Integer) As DataTable
        Return sqlMenu.dtb_sysObtMenusPorMnuPadre(pi_id)
    End Function

#End Region

#Region "Transacciones"

    Public Shared Function str_insertMenuDesdeDTB(ByVal po_entMenu As Object, ByVal po_dtb As DataTable) As String
        Return sqlMenu.str_insertMenuDesdeDTB(po_entMenu, po_dtb)
    End Function

#End Region

End Class
