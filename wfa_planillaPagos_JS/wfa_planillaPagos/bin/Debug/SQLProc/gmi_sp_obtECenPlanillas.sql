/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_obtECenPlanillas/*</nombre>*/

as

select
T0.*
, T2.id as Nro_Planilla
, T1.lineaNumAsg as Nro_Asignacion
, T2.comentario as Comentario_Planilla
, case T2.estado when 'O' then 'Abierto' when 'C' then 'Cerrado' when 'A' then 'Cancelado' end as Estado_Planilla
from
GMI_TmpOprBnc T0
inner join gmi_plaPagosDetalle T1 on T0.id  = T1.idEC 
inner join gmi_plaPagos T2 on T1.id = T2.id 