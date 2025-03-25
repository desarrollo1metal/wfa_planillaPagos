Imports System.Attribute
<AttributeUsageAttribute(AttributeTargets.Class)> _
Public Class annForm
    Inherits Attribute

    ' Atributos
    Private s_entidad As String

    ' Propiedades
    Public Overridable ReadOnly Property entidad() As Object
        Get
            Return s_entidad
        End Get
    End Property

    Public Sub New(ByVal ps_entidad As Object)
        s_entidad = ps_entidad
    End Sub

End Class
