Imports System.Attribute
<AttributeUsageAttribute(AttributeTargets.Field)> _
Public Class annAtributo
    Inherits Attribute

    Private b_esCampoBD As Boolean
    Private b_esDetalle As Boolean
    Private s_campoBD As String
    Private s_descrAtr As String
    Private b_usrEditable As Boolean
    Private b_deBusqueda As Boolean
    Private b_seActualiza As Boolean
    Private b_esDeSistem As Boolean
    Private b_obligatorio As Boolean
    Private s_propiedad As String

    Public Property esCampoBD() As Boolean
        Get
            Return b_esCampoBD
        End Get
        Set(ByVal value As Boolean)
            b_esCampoBD = value
        End Set
    End Property

    Public Overridable ReadOnly Property usrEditable() As Boolean
        Get
            Return b_usrEditable
        End Get
    End Property

    Public Overridable ReadOnly Property deBusqueda() As Boolean
        Get
            Return b_deBusqueda
        End Get
    End Property

    Public Overridable ReadOnly Property descrAtr() As String
        Get
            Return s_descrAtr
        End Get
    End Property

    Public Overridable ReadOnly Property campoBD() As String
        Get
            Return s_campoBD
        End Get
    End Property

    Public Property esDetalle() As Boolean
        Get
            Return b_esDetalle
        End Get
        Set(ByVal value As Boolean)
            b_esDetalle = value
        End Set
    End Property

    Public Overridable ReadOnly Property seActualiza() As Boolean
        Get
            Return b_seActualiza
        End Get
    End Property

    Public ReadOnly Property deSistema() As Boolean
        Get
            Return b_esDeSistem
        End Get
    End Property

    Public Property Obligatorio() As Boolean
        Get
            Return b_obligatorio
        End Get
        Set(ByVal value As Boolean)
            b_obligatorio = value
        End Set
    End Property

    Public Property Propiedad() As String
        Get
            Return s_propiedad
        End Get
        Set(ByVal value As String)
            s_propiedad = value
        End Set
    End Property

    Sub New(ByVal pb_esCampoBD As Boolean, _
            Optional ByVal pb_esDetalle As Boolean = False, _
            Optional ByVal ps_campoBD As String = "", _
            Optional ByVal ps_descrAtr As String = "", _
            Optional ByVal pb_usrEditable As Boolean = True, _
            Optional ByVal pb_deBusqueda As Boolean = True, _
            Optional ByVal pb_seActualiza As Boolean = True, _
            Optional ByVal pb_deSistema As Boolean = False, _
            Optional ByVal pb_obligatorio As Boolean = False, _
            Optional ByVal ps_propiedad As String = "")
        b_esCampoBD = pb_esCampoBD
        b_esDetalle = pb_esDetalle
        s_campoBD = ps_campoBD
        s_descrAtr = ps_descrAtr
        b_usrEditable = pb_usrEditable
        b_deBusqueda = pb_deBusqueda
        b_seActualiza = pb_seActualiza
        b_esDeSistem = pb_deSistema
        b_obligatorio = pb_obligatorio
        s_propiedad = ps_propiedad
    End Sub

End Class

