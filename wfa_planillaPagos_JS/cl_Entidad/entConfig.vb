Imports cl_AccesoDatos
Imports Util
Imports System.Attribute
Imports System.ComponentModel.DataAnnotations

<annEntidad("gmi_appPllConfig", "company", False)> _
Public Class entConfig
    Inherits entComun

    ' Atrbitos
    <annAtributo(True, False, "company", "Compania", False, False)>
    Private s_company As String
    <annAtributo(True, False, "editTCPagML", "Editar TC. Pagos en ML", True, False)>
    Private s_editTCPagML As String
    <annAtributo(True, False, "prctDetrac", "Porcentaje Detraccion", True, False)>
    Private d_prctDetrac As Double
    <annAtributo(True, False, "appVersion", "Version de la aplicacion", True, False)>
    Private l_appVersion As String
    <annAtributo(True, False, "creaAsTC", "Crear Asiento de Tipo de Cambio", True, False)>
    Private s_creaAsTC As String
    <annAtributo(True, False, "ctaPerdidaTC", "Cuenta de perdida por Dif.Cambio", True, False)>
    Private s_ctaPerdidaTC As String
    <annAtributo(True, False, "ctaGanaciaTC", "Cuenta de ganancia por Dif.Cambio", True, False)>
    Private s_ctaGanaciaTC As String

    ' Porpiedades
    Public Property PorcentajeDtr() As Double
        Get
            Return d_prctDetrac
        End Get
        Set(ByVal value As Double)
            d_prctDetrac = value
        End Set
    End Property

    Public Property EditarTCPagosML() As String
        Get
            Return s_editTCPagML
        End Get
        Set(ByVal value As String)
            s_editTCPagML = value
        End Set
    End Property

    Public Property Company() As String
        Get
            Return s_company
        End Get
        Set(ByVal value As String)
            s_company = value
        End Set
    End Property

    Public Property Version() As String
        Get
            Return l_appVersion
        End Get
        Set(ByVal value As String)
            l_appVersion = value
        End Set
    End Property

    Public Property CreaAsTC() As String
        Get
            Return s_creaAsTC
        End Get
        Set(ByVal value As String)
            s_creaAsTC = value
        End Set
    End Property

    Public Property CtaPerdidaTC() As String
        Get
            Return s_ctaPerdidaTC
        End Get
        Set(ByVal value As String)
            s_ctaPerdidaTC = value
        End Set
    End Property

    Public Property CtaGanaciaTC() As String
        Get
            Return s_ctaGanaciaTC
        End Get
        Set(ByVal value As String)
            s_ctaGanaciaTC = value
        End Set
    End Property

#Region "Consultas"

    Public Shared Function lon_obtVersion(ps_compania As String) As Long
        Return ModuleSQLComun.lon_obtVersion(ps_compania)
    End Function

    Public Function cfg_obtConfiguracionApp() As entConfig
        Try

            ' Se obtiene el objeto de configuracion por codigo: el unico codigo de configuracion para la base de datos es el mismo nombre de la base de datos
            Dim lo_objeto As Object = obj_obtPorCodigo(s_SAPComp)

            ' Se verifica si se obtuvo la configuracion
            If lo_objeto Is Nothing Then
                MsgBox(" Ocurrio un error al obtener el objeto de Configuracion.")
                Return Nothing
            End If

            ' Se retorna el objeto
            Return lo_objeto

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, Me.GetType.Name.ToString, System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

#End Region

End Class
