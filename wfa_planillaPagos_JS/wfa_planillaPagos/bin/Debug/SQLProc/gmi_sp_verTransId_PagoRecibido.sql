/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_verTransId_PagoRecibido/*</nombre>*/

@DocEntry as integer
as
select  ISNULL(TransId,'') from ORCT where DocEntry =@DocEntry
