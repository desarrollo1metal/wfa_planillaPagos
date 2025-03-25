Imports cl_AccesoDatos
Imports Util

<annEntidad("GMI_TmpOprBnc", "id")> _
Public Class entCargarEC
    Inherits entComun

    ' Atributos
    <annAtributo(True, False, "Fecha", "Fecha", False, True)>
    Private d_fecha As Date
    <annAtributo(True, False, "Descripcion", "Descripcion", False, True)>
    Private s_descripcion As String
    <annAtributo(True, False, "Monto", "Monto", False, True)>
    Private d_monto As Double
    <annAtributo(True, False, "Operacion", "Operacion", False, True)>
    Private s_operacion As String
    <annAtributo(True, False, "Cuenta", "Cuenta", False, True)>
    Private s_cuenta As String
    <annAtributo(True, False, "id", "id", False, True)>
    Private i_id As String
    <annAtributo(True, False, "ruc", "ruc", False, True)>
    Private s_ruc As String
    <annAtributo(True, False, "esPll", "Es de Planillas", False, True)>
    Private s_esPll As String

    ' Propiedades
    Public Property Id() As String
        Get
            Return i_id
        End Get
        Set(ByVal value As String)
            i_id = value
        End Set
    End Property

    Public Property Ruc() As String
        Get
            Return s_ruc
        End Get
        Set(ByVal value As String)
            s_ruc = value
        End Set
    End Property

    Public Property esDePlanillas() As String
        Get
            Return s_esPll
        End Get
        Set(ByVal value As String)
            s_esPll = value
        End Set
    End Property

    Public Property Cuenta() As String
        Get
            Return s_cuenta
        End Get
        Set(ByVal value As String)
            s_cuenta = value
        End Set
    End Property

    Public Property Operacion() As String
        Get
            Return s_operacion
        End Get
        Set(ByVal value As String)
            s_operacion = value
        End Set
    End Property

    Public Property Monto() As Double
        Get
            Return d_monto
        End Get
        Set(ByVal value As Double)
            d_monto = value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return s_descripcion
        End Get
        Set(ByVal value As String)
            s_descripcion = value
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

#Region "ProcesosNegocio"

    Public Shared Function dtb_importarEC(ByVal ps_ruta As String) As DataTable
        Try

            ' Se importa los datos del Estado de Cuenta hacia la tabla correspondiente en la base de datos
            Return sqlCargarEC.dtb_importarEC(ps_ruta)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entCargarEC", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function str_importarEC(ByVal po_dtb As DataTable) As String
        Try

            ' Se importa los datos del Estado de Cuenta hacia la tabla correspondiente en la base de datos
            Return sqlCargarEC.str_importarEC(po_dtb, New entCargarEC)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entCargarEC", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message
        End Try
    End Function

#End Region

#Region "Consultas"

    Public Shared Function dtb_obtEstadoCuentaPPR(ByVal ps_ctaBanc As String) As DataTable
        Try

            ' Se obtiene el estado de cuenta filtrado por cuenta bancaria
            Return sqlCargarEC.dtb_obtEstadoCuentaPPR(ps_ctaBanc)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entCargarEC", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function dtb_repEstadoCuenta() As DataTable
        Try

            ' Se obtiene el estado de cuenta filtrado por cuenta bancaria
            Return sqlCargarEC.dtb_repEstadoCuenta()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entCargarEC", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function int_verNroOperEC(ByVal pd_fecha As Date, ByVal ps_cuenta As String, ByVal ps_nroOper As String, ByVal pd_monto As Double) As Integer
        Try

            ' Se verifica si el numero de operacion existe en la tabla de Estado de Cuenta
            Return sqlCargarEC.int_verNroOperEC(pd_fecha, ps_cuenta, ps_nroOper, pd_monto)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entCargarEC", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return -1
        End Try
    End Function

    Public Shared Function int_verNroOperECenPll(ByVal pd_fecha As Date, ByVal ps_cuenta As String, ByVal ps_nroOper As String, ByVal pd_monto As Double) As Integer
        Try

            ' Se verifica si el numero de operacion existe en alguna planilla
            Return sqlCargarEC.int_verNroOperECenPll(pd_fecha, ps_cuenta, ps_nroOper, pd_monto)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entCargarEC", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return -1
        End Try
    End Function

    Public Shared Function str_verTipoRegEC(ByVal pi_id As Integer) As String
        Try

            ' Se obtiene el tipo de registro de Estado de Cuenta
            Return sqlCargarEC.str_verTipoRegEC(pi_id)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entCargarEC", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return "x"
        End Try
    End Function

#End Region

End Class
