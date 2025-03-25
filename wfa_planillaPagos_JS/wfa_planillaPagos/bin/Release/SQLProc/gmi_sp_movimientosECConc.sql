/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_movimientosECConc/*</nombre>*/

@visualizar as char(1)

as

set @visualizar = isnull(@visualizar, 'T')

if @visualizar = 'P' -- Pendientes
begin

select 
T0.*
, T2.*

from
GMI_TmpOprBnc T0
inner join DSC1 T1 on T0.Cuenta = T1.Account
left join gmi_vw_obtenerPagosNroOper T2 on dbo.udf_SupCerosYComilSim( T2.Nro_Operacion ) <> ''
										and dbo.udf_SupCerosYComilSim( T0.Operacion ) = dbo.udf_SupCerosYComilSim( T2.Nro_Operacion ) 
										and (T2.Cta_transfer = T1.GLAccount or T2.Cta_Cash = T1.GLAccount)
where
isnull(T2.Nro_SAP, '') = ''

end

if @visualizar = 'A' -- Asignados
begin

select 
T0.*
, T2.*
from
GMI_TmpOprBnc T0
inner join DSC1 T1 on T0.Cuenta = T1.Account
left join gmi_vw_obtenerPagosNroOper T2 on dbo.udf_SupCerosYComilSim( T2.Nro_Operacion ) <> ''
										and dbo.udf_SupCerosYComilSim( T0.Operacion ) = dbo.udf_SupCerosYComilSim( T2.Nro_Operacion ) 
										and (T2.Cta_transfer = T1.GLAccount or T2.Cta_Cash = T1.GLAccount)
where
isnull(T2.Nro_SAP, '') <> ''

end

if @visualizar = 'T' -- Todos
begin

select 
T0.*
, T2.*
from
GMI_TmpOprBnc T0
inner join DSC1 T1 on T0.Cuenta = T1.Account
left join gmi_vw_obtenerPagosNroOper T2 on dbo.udf_SupCerosYComilSim( T2.Nro_Operacion ) <> ''
										and dbo.udf_SupCerosYComilSim( T0.Operacion ) = dbo.udf_SupCerosYComilSim( T2.Nro_Operacion ) 
										and (T2.Cta_transfer = T1.GLAccount or T2.Cta_Cash = T1.GLAccount)

end

