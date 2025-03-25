/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_actualizarNrosSAPPlaDet/*</nombre>*/

@id as int

as

update 
T1
set
T1.DocEntrySAP = T0.DocEntrySAP
, T1.DocNumSAP = T2.DocNum
from 
gmi_plaPagosDetalle2 T0
inner join gmi_plaPagosDetalle T1 on T0.id = T1.id and T0.lineaNumAsg = T1.lineaNumAsg and T0.idEC = T1.idEC
inner join ORCT T2 on T0.DocEntrySAP = T2.DocEntry
where 
T0.Id = @id