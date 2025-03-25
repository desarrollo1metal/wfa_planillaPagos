<annEntidad("gmi_sysLog", "id")> _
Public Class entLog
    Inherits entComun

    ' Atributos
    <annAtributo(True, False, "id", "Id. del registro", False, True, False)>
    Private i_id As Integer
    <annAtributo(True, False, "proyecto", "Pryecto", True, True, True, False, True)>
    Private s_proyecto As String
    <annAtributo(True, False, "clase", "Clase", True, True, True, False, True)>
    Private s_clase As String
    <annAtributo(True, False, "metodo", "Metodo", True, True, True, False, True)>
    Private s_metodo As String
    <annAtributo(True, False, "tipo", "Tipo de mensaje", True, True, True, False, True)>
    Private i_tipo As Integer
    <annAtributo(True, False, "mensaje", "Mensaje del Log", True, True, True, False, True)>
    Private s_mensaje As String
    <annAtributo(True, False, "fecha", "Fecha del mensaje", True, True, True, False, True)>
    Private d_fecha As Date
    <annAtributo(True, False, "hora", "Fecha del mensaje", True, True, True, False, True)>
    Private i_hora As Integer

    ' Propiedades
    Public Property Id() As Integer
        Get
            Return i_id
        End Get
        Set(ByVal value As Integer)
            i_id = value
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

    Public Property Clase() As String
        Get
            Return s_clase
        End Get
        Set(ByVal value As String)
            s_clase = value
        End Set
    End Property

    Public Property Metodo() As String
        Get
            Return s_metodo
        End Get
        Set(ByVal value As String)
            s_metodo = value
        End Set
    End Property

    Public Property Tipo() As Integer
        Get
            Return i_tipo
        End Get
        Set(ByVal value As Integer)
            i_tipo = value
        End Set
    End Property

    Public Property Mensaje() As String
        Get
            Return s_mensaje
        End Get
        Set(ByVal value As String)
            s_mensaje = value
        End Set
    End Property

    Public Property Fecha() As Date
        Get
            Return d_fecha
        End Get
        Set(ByVal value As Date)
            d_fecha = value
        End Set
    End Property

    Public Property Hora() As Integer
        Get
            Return i_hora
        End Get
        Set(ByVal value As Integer)
            i_hora = value
        End Set
    End Property

End Class
