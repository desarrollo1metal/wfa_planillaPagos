/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/table/*</tipoSQL>*/ 
/*<nombre>*/gmi_sysLog/*</nombre>*/
(

/*<campo>*/ id /*</campo>*/int 
/*<campo>*/, proyecto /*</campo>*/nvarchar(150)
/*<campo>*/, clase /*</campo>*/nvarchar(150)
/*<campo>*/, metodo /*</campo>*/nvarchar(150)
/*<campo>*/, tipo /*</campo>*/int
/*<campo>*/, mensaje /*</campo>*/ntext
/*<campo>*/, fecha /*</campo>*/datetime
/*<campo>*/, hora /*</campo>*/int 
/*<campo>*/, winUsrCrea /*</campo>*/nvarchar(50)
/*<campo>*/, nomPCCrea /*</campo>*/nvarchar(50)
/*<campo>*/, ipPCCrea /*</campo>*/nvarchar(50)

)