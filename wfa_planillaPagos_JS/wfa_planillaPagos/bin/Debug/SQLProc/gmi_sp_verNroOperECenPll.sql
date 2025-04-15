/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_verNroOperECenPll/*</nombre>*/

@fecha as datetime
, @cuenta as nvarchar(100)
, @operacion as nvarchar(50)
, @monto as decimal(19, 6)

as

declare @id as int

select
top 1 @id = T2.id
from
GMI_TmpOprBnc T0 
inner join gmi_plaPagosDetalle T1 on T0.id = T1.idEC
inner join gmi_plaPagos T2 on T1.id = T2.id
where
T0.Fecha = @fecha
and REPLACE( T0.Cuenta, '-', '' ) = REPLACE( @cuenta, '-', '' ) 
and dbo.udf_SupCerosYComilSim( T0.Operacion ) = dbo.udf_SupCerosYComilSim( @operacion )
and T0.Monto = @monto
and T2.estado in ('O', 'C')

select isnull(@id, -1)