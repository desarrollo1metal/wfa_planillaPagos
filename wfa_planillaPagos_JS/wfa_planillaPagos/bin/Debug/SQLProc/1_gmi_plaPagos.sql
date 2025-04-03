/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/table/*</tipoSQL>*/ 
/*<nombre>*/gmi_plaPagos/*</nombre>*/
(

/*<campo>*/ id /*</campo>*/int 
/*<campo>*/, FechaCrea /*</campo>*/datetime
/*<campo>*/, FechaAct /*</campo>*/datetime
/*<campo>*/, FechaPrcs /*</campo>*/datetime
/*<campo>*/, estado /*</campo>*/char(1)
/*<campo>*/, comentario /*</campo>*/nvarchar(max)
/*<campo>*/, tipoPla /*</campo>*/char(1)
/*<campo>*/, winUsrCrea /*</campo>*/nvarchar(50)
/*<campo>*/, nomPCCrea /*</campo>*/nvarchar(50)
/*<campo>*/, ipPCCrea /*</campo>*/nvarchar(50)

)