/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_verTipoPlanilla/*</nombre>*/

@pi_id as int

as

select 
estado
from
gmi_plaPagos
where 
1 = 1
and id = @pi_id
