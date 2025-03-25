/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_obtParamProcedure/*</nombre>*/

@nomProcedure as nvarchar(100)

AS

select 
T0.name as nomParam
, T2.name as tipoParam
from 
sys.parameters T0 
inner join sys.procedures T1 on T0.object_id = T1.object_id
inner join sys.types T2 on T0.system_type_id = T2.system_type_id
where
T1.name = @nomProcedure
and T2.name not like 'sysname'