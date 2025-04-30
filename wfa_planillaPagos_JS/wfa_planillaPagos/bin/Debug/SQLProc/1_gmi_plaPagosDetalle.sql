/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/table/*</tipoSQL>*/ 
/*<nombre>*/gmi_plaPagosDetalle/*</nombre>*/
(

/*<campo>*/id /*</campo>*/int
/*<campo>*/, lineaNumAsg /*</campo>*/int
/*<campo>*/, Codigo /*</campo>*/nvarchar(30)
/*<campo>*/, Nombre /*</campo>*/nvarchar(200)
/*<campo>*/, Id_Doc /*</campo>*/int
/*<campo>*/, Referencia /*</campo>*/nvarchar(100)
/*<campo>*/, Tipo_Doc /*</campo>*/nvarchar(20)
/*<campo>*/, DocLine /*</campo>*/int 
/*<campo>*/, FechaDoc /*</campo>*/datetime
/*<campo>*/, Comentario /*</campo>*/nvarchar(150)
/*<campo>*/, MonedaDoc /*</campo>*/nvarchar(3)
/*<campo>*/, TipoCambioDoc /*</campo>*/numeric(19, 6)
/*<campo>*/, Total /*</campo>*/numeric(19, 6)
/*<campo>*/, Saldo /*</campo>*/numeric(19, 6)
/*<campo>*/, Imp_Aplicado /*</campo>*/numeric(19, 6)
/*<campo>*/, Imp_AplicadoME /*</campo>*/numeric(19, 6)
/*<campo>*/, SaldoFavor /*</campo>*/numeric(19, 6)
/*<campo>*/, SaldoFavorME /*</campo>*/numeric(19, 6)
/*<campo>*/, MontoOp /*</campo>*/numeric(19, 6)
/*<campo>*/, MonedaPag /*</campo>*/nvarchar(3)
/*<campo>*/, Tipo_Cambio /*</campo>*/numeric(19, 6) 
/*<campo>*/, Cuenta /*</campo>*/nvarchar(20)
/*<campo>*/, FechaPago /*</campo>*/datetime
/*<campo>*/, Nro_Operacion /*</campo>*/nvarchar(40)
/*<campo>*/, idEC /*</campo>*/int 
/*<campo>*/, ComentarioPl /*</campo>*/nvarchar(150)
/*<campo>*/, tipoAsg /*</campo>*/int
/*<campo>*/, DocNumSAP /*</campo>*/int 
/*<campo>*/, DocEntrySAP /*</campo>*/int 
/*<campo>*/, tipoEC /*</campo>*/char(2)
/*<campo>*/, difTC /*</campo>*/char(1)

)