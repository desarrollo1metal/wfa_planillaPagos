/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_obtenerPagosNroOper/*</nombre>*/

@fDesde as datetime
, @fHasta as datetime
, @conNroOper as char(1)

as

if @conNroOper = 'Y'
begin

select
*
from
gmi_vw_obtenerPagosNroOper
where 
dbo.udf_SupCerosYComilSim( isnull(Nro_Operacion, '') ) <> ''
and Fecha_Contab between @fDesde and @fHasta

end

if @conNroOper = 'N'
begin

select
*
from
gmi_vw_obtenerPagosNroOper
where 
dbo.udf_SupCerosYComilSim( isnull(Nro_Operacion, '') ) = ''
and Fecha_Contab between @fDesde and @fHasta

end

if @conNroOper = 'T'
begin

select
*
from
gmi_vw_obtenerPagosNroOper
where 
Fecha_Contab between @fDesde and @fHasta

end

