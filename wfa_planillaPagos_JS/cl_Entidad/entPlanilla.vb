Imports cl_AccesoDatos
Imports Util
Imports System.Attribute
Imports System.ComponentModel.DataAnnotations

<annEntidad("gmi_plaPagos", "id")> _
Public Class entPlanilla
    Inherits entComun

    ' Atributos
    <annAtributo(True, False, "id", "Nro.Planilla", False, True)>
    Private i_id As Integer
    <annAtributo(True, False, "FechaPrcs", "Fecha de Proceso", False, True)>
    Private d_FechaPrcs As Date
    <annAtributo(True, False, "estado", "Estado", False, True)>
    Private s_estado As String
    <annAtributo(True, False, "comentario", "Comentario", True, True)>
    Private s_comentario As String
    <annAtributo(True, False, "tipoPla", "Tipo de Planilla", True, True, False)>
    Private s_tipoPla As String
    <annAtributo(True, True, "gmi_plaPagosDetalle")>
    Private o_pll_lineas As New entPlanilla_Lineas
    <annAtributo(True, True, "gmi_plaPagosDetalle2")>
    Private o_pll_pagosR As New entPlanilla_PagosR

    ' Propiedades
    Public Property id() As Integer
        Get
            Return i_id
        End Get
        Set(ByVal value As Integer)
            i_id = value
        End Set
    End Property

    Public Property FechaPrcs() As Date
        Get
            Return d_FechaPrcs
        End Get
        Set(ByVal value As Date)
            d_FechaPrcs = value
        End Set
    End Property

    Public Property Estado() As String
        Get
            Return s_estado
        End Get
        Set(ByVal value As String)
            s_estado = value
        End Set
    End Property

    Public Property Comentario() As String
        Get
            Return s_comentario
        End Get
        Set(ByVal value As String)
            s_comentario = value
        End Set
    End Property

    Public Property TipoPla() As String
        Get
            Return s_tipoPla
        End Get
        Set(ByVal value As String)
            s_tipoPla = value
        End Set
    End Property

    Public Property Lineas() As entPlanilla_Lineas
        Get
            Return o_pll_lineas
        End Get
        Set(ByVal value As entPlanilla_Lineas)
            o_pll_lineas = value
        End Set
    End Property

    Public Property PagosR() As entPlanilla_PagosR
        Get
            Return o_pll_pagosR
        End Get
        Set(ByVal value As entPlanilla_PagosR)
            o_pll_pagosR = value
        End Set
    End Property

#Region "ProcesosNegocio"

    Public Shared Function str_actualizarNrosSAPPlaDet(ByVal pi_id As Integer) As String
        Try

            ' Se actualiza los numeros SAP en la tabla de detalle 
            Return sqlPlanilla.str_actualizarNrosSAPPlaDet(pi_id)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message
        End Try
    End Function

#End Region

#Region "Consultas"

    Public Shared Function dtb_obtNroPosBanco(ByVal ps_Cuenta As String) As DataTable
        Try

            ' Se obtiene el estado de cuenta del cliente desde SAP
            Return sqlPlanilla.dtb_obtNroPosBanco(ps_Cuenta)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function dtb_obtCtasPorCobrar(ByVal ps_cardCode As String) As DataTable
        Try

            ' Se obtiene el estado de cuenta del cliente desde SAP
            Return sqlPlanilla.dtb_obtCtasPorCobrar(ps_cardCode)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function dtb_obtCtasBanco() As DataTable
        Try

            ' Se obtiene el estado de cuenta filtrado por cuenta bancaria
            Return sqlPlanilla.dtb_obtCtasBanco()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function int_obtPlanillasAbiertas() As Integer
        Try

            ' Se obtiene el numero de planillas abiertas
            Return sqlPlanilla.int_obtPlanillasAbiertas()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return -1
        End Try
    End Function

    Public Shared Function int_obtCantLineasAsgPll(ByVal pi_id As Integer) As Integer
        Try

            ' Se obtiene el numero de planillas abiertas
            Return sqlPlanilla.int_obtCantLineasAsgPll(pi_id)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return -1
        End Try
    End Function

    Public Shared Function dtb_obtECenPlanillas() As DataTable
        Try

            ' Se obtiene el numero de planillas abiertas
            Return sqlPlanilla.dtb_obtECenPlanillas()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function int_verIdECenPlanillas(ByVal pi_id As Integer, ByVal pi_idEC As Integer) As Integer
        Try

            ' Se obtiene el numero de planillas abiertas
            Return sqlPlanilla.int_verIdECenPlanillas(pi_id, pi_idEC)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return -1
        End Try
    End Function

    Public Shared Function int_verIdDocEnPlanillas(ByVal pi_id As Integer, ByVal pi_idDoc As Integer) As Integer
        Try

            ' Se obtiene el numero de planillas abiertas
            Return sqlPlanilla.int_verIdDocEnPlanillas(pi_id, pi_idDoc)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return -1
        End Try
    End Function

    Public Shared Function str_obtCtaBancoNacion() As String
        Try

            ' Se obtiene el codigo de cuenta bancaria del banco de la nacion
            Return sqlPlanilla.str_obtCtaBancoNacion

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return ""
        End Try
    End Function

    Public Shared Function dtb_Reporte() As DataTable
        Try

            ' Se obtiene el numero de planillas abiertas
            Return sqlPlanilla.dtb_Reporte()

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return Nothing
        End Try
    End Function

    Public Shared Function dtb_ObtenerBanco(ByVal intID As Integer) As String
        Try

            ' Se obtiene el codigo de cuenta bancaria del banco de la nacion
            Return sqlPlanilla.dtb_ObtenerBanco(intID)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return ""
        End Try
    End Function

    Public Shared Function str_verTipoPlanilla(ByVal pi_id As Integer) As String
        Try

            ' Se obtiene el entero que indicara si el id recibido corresponde a una planilla abierta
            Return sqlPlanilla.str_verTipoPlanilla(pi_id)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return ""
        End Try
    End Function

    Public Shared Function int_verExitDocEnPllAbierta(ByVal pi_idDoc As Integer, ByVal pi_transType As Integer, pi_docLine As Integer, pi_idPll As Integer) As Integer
        Try

            ' Se obtiene el entero que indicara si el id recibido corresponde a una planilla abierta
            Return sqlPlanilla.int_verExitDocEnPllAbierta(pi_idDoc, pi_transType, pi_docLine, pi_idPll)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return -1
        End Try
    End Function

    Public Shared Function int_verExitDocEnPllAbierta2(ByVal pi_idDoc As Integer, ByVal pi_transType As Integer, pi_docLine As Integer, pi_idPll As Integer, asientoajusteT As String) As Integer
        Try

            ' Se obtiene el entero que indicara si el id recibido corresponde a una planilla abierta
            Return sqlPlanilla.int_verExitDocEnPllAbierta2(pi_idDoc, pi_transType, pi_docLine, pi_idPll, asientoajusteT)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return -1
        End Try
    End Function


    Public Shared Function int_verExitECEnPllAbierta(ByVal pi_idEC As Integer, pi_idPll As Integer) As Integer
        Try

            ' Se obtiene el entero que indicara si el id recibido corresponde a una planilla abierta
            Return sqlPlanilla.int_verExitECEnPllAbierta(pi_idEC, pi_idPll)

        Catch ex As Exception
            sub_mostrarMensaje(ex.Message, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, "entPlanilla", System.Reflection.MethodInfo.GetCurrentMethod.Name, enm_tipoMsj.error_exc)
            Return -1
        End Try
    End Function

#End Region

End Class
