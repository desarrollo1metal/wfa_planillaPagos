/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_obtColumnasTabla/*</nombre>*/

@pv_tabla as nvarchar(50)
, @pv_campo as nvarchar(50)

as

if isnull(@pv_campo, '') = ''
begin

	set @pv_campo = '%%'

end

select 
T0.name as Campo
, T2.name as Tipo
, T1.name as Tabla
from 
sys.columns T0
inner join sys.tables T1 on T0.object_id = T1.object_id
inner join sys.types T2 on T0.system_type_id = T2.system_type_id
where
1 = 1
and T1.name like @pv_tabla
and T0.name like @pv_campo
and (T2.name like '%char%' or T2.name like '%int%' or T2.name like '%date%')