Imports System.Attribute
<AttributeUsageAttribute(AttributeTargets.Class)> _
Public Class annEntidad
    Inherits Attribute

    ' Atributos
    Private s_tabla As String
    Private s_idTabla As String
    Private b_esIdIntAuto As Boolean
    Private b_esDetalle As Boolean
    Private b_detObligatorio As Boolean
    Private s_detCampoEnum As String

    ' Propiedades
    Public Overridable ReadOnly Property Tabla() As String
        Get
            Return s_tabla
        End Get
    End Property

    Public Overridable ReadOnly Property IdTabla() As String
        Get
            Return s_idTabla
        End Get
    End Property

    Public Overridable ReadOnly Property EsIdIntAuto() As Boolean
        Get
            Return b_esIdIntAuto
        End Get
    End Property

    Public Overridable ReadOnly Property esDetalle() As Boolean
        Get
            Return b_esDetalle
        End Get
    End Property

    Public Overridable ReadOnly Property detObligatorio() As Boolean
        Get
            Return b_detObligatorio
        End Get
    End Property

    Public Overridable ReadOnly Property DetCampoEnum() As String
        Get
            Return s_detCampoEnum
        End Get
    End Property

    Public Sub New(ByVal ps_tabla As String, ByVal ps_idTabla As String, Optional ByVal pb_esIdIntAuto As Boolean = True, Optional ByVal pb_esDetalle As Boolean = False, Optional ByVal pb_detObligatorio As Boolean = False, Optional ByVal ps_detCampoEnum As String = "")
        s_tabla = ps_tabla
        s_idTabla = ps_idTabla
        b_esDetalle = pb_esDetalle
        b_esIdIntAuto = pb_esIdIntAuto
        b_detObligatorio = pb_detObligatorio
        s_detCampoEnum = ps_detCampoEnum
    End Sub

End Class
