Imports Util

<annEntidad("gmi_plaPagosDetalle", "id", True, True, True, "lineaNumAsg")> _
Public Class entPlanilla_Lineas
    Inherits entComunDetalle

    ' Atributos
    <annAtributo(True, False, "id", "Nro.Planilla", False)>
    Private i_id As Integer
    <annAtributo(True, False, "lineaNumAsg", "Nro.Linea Asig", False)>
    Private i_lineaNumAsg As Integer
    <annAtributo(True, False, "Codigo", "Codigo Cliente", False)>
    Private s_Codigo As String
    <annAtributo(True, False, "Nombre", "Nombre Cliente", False)>
    Private s_Nombre As String
    <annAtributo(True, False, "Id_Doc", "Id. Documento", False)>
    Private i_Id_Doc As Integer
    <annAtributo(True, False, "Referencia", "Referencia", False)>
    Private s_Referencia As String
    <annAtributo(True, False, "Tipo_Doc", "Tipo Documento", False)>
    Private s_Tipo_Doc As String
    <annAtributo(True, False, "DocLine", "ID Linea Asiento", False)>
    Private i_DocLine As Integer
    <annAtributo(True, False, "FechaDoc", "Fecha Documento", False)>
    Private d_FechaDoc As Date
    <annAtributo(True, False, "Comentario", "Comentario", False)>
    Private s_Comentario As String
    <annAtributo(True, False, "MonedaDoc", "Moneda Documento", False)>
    Private s_MonedaDoc As String
    <annAtributo(True, False, "TipoCambioDoc", "Tipo Cambio Documento", True)>
    Private d_TipoCambioDoc As Double
    <annAtributo(True, False, "Total", "Total Documento", False)>
    Private d_Total As Double
    <annAtributo(True, False, "Saldo", "Saldo Documento", True)>
    Private d_Saldo As Double
    <annAtributo(True, False, "Imp_Aplicado", "Importe Aplicado", True)>
    Private d_Imp_Aplicado As Double
    <annAtributo(True, False, "Imp_AplicadoME", "Importe Aplicado ME", True)>
    Private d_Imp_AplicadoME As Double
    <annAtributo(True, False, "SaldoFavor", "Saldo a Favor", False)>
    Private d_SaldoFavor As Double
    <annAtributo(True, False, "SaldoFavorME", "Saldo a Favor ME", False)>
    Private d_SaldoFavorME As Double
    <annAtributo(True, False, "MontoOp", "Monto Operacion", False)>
    Private d_MontoOp As Double
    <annAtributo(True, False, "MonedaPag", "Moneda Pago", False)>
    Private s_MonedaPag As String
    <annAtributo(True, False, "Tipo_Cambio", "Tipo Cambio Pago", True)>
    Private d_Tipo_Cambio As Double
    <annAtributo(True, False, "Cuenta", "Cuenta Contable", False)>
    Private s_Cuenta As String
    <annAtributo(True, False, "FechaPago", "Fecha Pago", False)>
    Private d_FechaPago As Date
    <annAtributo(True, False, "FechaDeposito", "Fecha Deposito", False)>
    Private d_FechaDeposito As Date
    <annAtributo(True, False, "Nro_Operacion", "Nro. Operacion", False)>
    Private s_Nro_Operacion As String
    <annAtributo(True, False, "idEC", "Id Estado Cuenta", False)>
    Private i_idEC As Integer
    <annAtributo(True, False, "ComentarioPl", "Comentario Planilla", False)>
    Private s_ComentarioPl As String
    <annAtributo(True, False, "tipoAsg", "Tipo asignacion", False)>
    Private i_tipoAsg As Integer
    <annAtributo(True, False, "DocNumSAP", "DocNum SAP", False)>
    Private i_DocNumSAP As Integer
    <annAtributo(True, False, "DocEntrySAP", "DocEntry SAP", False)>
    Private i_DocEntrySAP As Integer
    <annAtributo(True, False, "tipoEC", "Tipo de Registro EC", False)>
    Private s_tipoEC As String
    <annAtributo(True, False, "difTC", "Diferencia por Tipo de Cambio", True)>
    Private s_difTC As String
    <annAtributo(True, False, "numPos", "Número Posición", False)>
    Private s_numPos As String
    <annAtributo(True, False, "FechaCrea", "Fecha_Creacion", False)>
    Private d_FechaCrea As Date
    <annAtributo(True, False, "Ruc", "Ruc", False)>
    Private s_Ruc As String
    <annAtributo(True, False, "CodBien", "Codigo Bien", False)>
    Private s_cod As String

    <annAtributo(True, False, "asientoajustoT", "asientoajustoT", False)>
    Private s_asientoajustoT As String

    Public Property asientoajustoT() As String
        Get
            Return s_asientoajustoT
        End Get
        Set(ByVal value As String)
            s_asientoajustoT = value
        End Set
    End Property


    ' Propiedades
    Public Property NumPosicion() As String
        Get
            Return s_numPos
        End Get
        Set(ByVal value As String)
            s_numPos = value
        End Set
    End Property

    Public Property Nro_Operacion() As String
        Get
            Return s_Nro_Operacion
        End Get
        Set(ByVal value As String)
            s_Nro_Operacion = value
        End Set
    End Property

    Public Property FechaPago() As Date
        Get
            Return d_FechaPago
        End Get
        Set(ByVal value As Date)
            d_FechaPago = value
        End Set
    End Property

    Public Property FechaDeposito() As Date
        Get
            Return d_FechaDeposito
        End Get
        Set(ByVal value As Date)
            d_FechaDeposito = value
        End Set
    End Property

    Public Property Cuenta() As String
        Get
            Return s_Cuenta
        End Get
        Set(ByVal value As String)
            s_Cuenta = value
        End Set
    End Property

    Public Property Tipo_Cambio() As Double
        Get
            Return d_Tipo_Cambio
        End Get
        Set(ByVal value As Double)
            d_Tipo_Cambio = value
        End Set
    End Property

    Public Property MonedaPag() As String
        Get
            Return s_MonedaPag
        End Get
        Set(ByVal value As String)
            s_MonedaPag = value
        End Set
    End Property

    Public Property Imp_Aplicado() As Double
        Get
            Return d_Imp_Aplicado
        End Get
        Set(ByVal value As Double)
            d_Imp_Aplicado = value
        End Set
    End Property

    Public Property Imp_AplicadoME() As Double
        Get
            Return d_Imp_AplicadoME
        End Get
        Set(ByVal value As Double)
            d_Imp_AplicadoME = value
        End Set
    End Property

    Public Property SaldoFavor() As Double
        Get
            Return d_SaldoFavor
        End Get
        Set(ByVal value As Double)
            d_SaldoFavor = value
        End Set
    End Property

    Public Property SaldoFavorME() As Double
        Get
            Return d_SaldoFavorME
        End Get
        Set(ByVal value As Double)
            d_SaldoFavorME = value
        End Set
    End Property

    Public Property MontoOp() As Double
        Get
            Return d_MontoOp
        End Get
        Set(ByVal value As Double)
            d_MontoOp = value
        End Set
    End Property

    Public Property Saldo() As Double
        Get
            Return d_Saldo
        End Get
        Set(ByVal value As Double)
            d_Saldo = value
        End Set
    End Property

    Public Property Total() As Double
        Get
            Return d_Total
        End Get
        Set(ByVal value As Double)
            d_Total = value
        End Set
    End Property

    Public Property MonedaDoc() As String
        Get
            Return s_MonedaDoc
        End Get
        Set(ByVal value As String)
            s_MonedaDoc = value
        End Set
    End Property

    Public Property TipoCambioDoc() As Double
        Get
            Return d_TipoCambioDoc
        End Get
        Set(ByVal value As Double)
            d_TipoCambioDoc = value
        End Set
    End Property

    Public Property Comentario() As String
        Get
            Return s_Comentario
        End Get
        Set(ByVal value As String)
            s_Comentario = value
        End Set
    End Property

    Public Property FechaDoc() As Date
        Get
            Return d_FechaDoc
        End Get
        Set(ByVal value As Date)
            d_FechaDoc = value
        End Set
    End Property

    Public Property Tipo_Doc() As String
        Get
            Return s_Tipo_Doc
        End Get
        Set(ByVal value As String)
            s_Tipo_Doc = value
        End Set
    End Property

    Public Property DocLine() As Integer
        Get
            Return i_DocLine
        End Get
        Set(ByVal value As Integer)
            i_DocLine = value
        End Set
    End Property

    Public Property Referencia() As String
        Get
            Return s_Referencia
        End Get
        Set(ByVal value As String)
            s_Referencia = value
        End Set
    End Property


    Public Property Id_Doc() As Integer
        Get
            Return i_Id_Doc
        End Get
        Set(ByVal value As Integer)
            i_Id_Doc = value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return s_Nombre
        End Get
        Set(ByVal value As String)
            s_Nombre = value
        End Set
    End Property

    Public Property Codigo() As String
        Get
            Return s_Codigo
        End Get
        Set(ByVal value As String)
            s_Codigo = value
        End Set
    End Property

    Public Property id() As Integer
        Get
            Return i_id
        End Get
        Set(ByVal value As Integer)
            i_id = value
        End Set
    End Property

    Public Property LineaNumAsg() As Integer
        Get
            Return i_lineaNumAsg
        End Get
        Set(ByVal value As Integer)
            i_lineaNumAsg = value
        End Set
    End Property

    Public Property idEC() As Integer
        Get
            Return i_idEC
        End Get
        Set(ByVal value As Integer)
            i_idEC = value
        End Set
    End Property

    Public Property ComentarioPl() As String
        Get
            Return s_ComentarioPl
        End Get
        Set(ByVal value As String)
            s_ComentarioPl = value
        End Set
    End Property

    Public Property tipoAsg() As Integer
        Get
            Return i_tipoAsg
        End Get
        Set(ByVal value As Integer)
            i_tipoAsg = value
        End Set
    End Property

    Public Property DocNumSAP() As Integer
        Get
            Return i_DocNumSAP
        End Get
        Set(ByVal value As Integer)
            i_DocNumSAP = value
        End Set
    End Property

    Public Property DocEntrySAP() As Integer
        Get
            Return i_DocEntrySAP
        End Get
        Set(ByVal value As Integer)
            i_DocEntrySAP = value
        End Set
    End Property

    Public Property tipoEC() As String
        Get
            Return s_tipoEC
        End Get
        Set(ByVal value As String)
            s_tipoEC = value
        End Set
    End Property

    Public Property DifTC() As String
        Get
            Return s_difTC
        End Get
        Set(ByVal value As String)
            s_difTC = value
        End Set
    End Property

    Public Property FechaCreacion() As Date
        Get
            Return d_FechaCrea
        End Get
        Set(ByVal value As Date)
            d_FechaCrea = value
        End Set
    End Property

    Public Property Ruc() As String
        Get
            Return s_Ruc
        End Get
        Set(ByVal value As String)
            s_Ruc = value
        End Set
    End Property

    Public Property Cod() As String
        Get
            Return s_cod
        End Get
        Set(ByVal value As String)
            s_cod = value
        End Set
    End Property

End Class
