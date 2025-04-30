/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/table/*</tipoSQL>*/ 
/*<nombre>*/gmi_sysMenu/*</nombre>*/
(

/*<campo>*/ id /*</campo>*/int 
/*<campo>*/, nomMenu /*</campo>*/nvarchar(75)
/*<campo>*/, tipoMenu /*</campo>*/int
/*<campo>*/, idMenuPadre /*</campo>*/int
/*<campo>*/, ordenMenu /*</campo>*/int
/*<campo>*/, funcion /*</campo>*/nvarchar(75)
/*<campo>*/, FechaCrea /*</campo>*/datetime
/*<campo>*/, FechaAct /*</campo>*/datetime
/*<campo>*/, winUsrCrea /*</campo>*/nvarchar(50)
/*<campo>*/, nomPCCrea /*</campo>*/nvarchar(50)
/*<campo>*/, ipPCCrea /*</campo>*/nvarchar(50)

)