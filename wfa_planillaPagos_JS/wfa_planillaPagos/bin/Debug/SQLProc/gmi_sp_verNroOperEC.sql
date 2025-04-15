/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_verNroOperEC/*</nombre>*/

@fecha as datetime
, @cuenta as nvarchar(100)
, @operacion as nvarchar(50)
, @monto as decimal(19, 6)

as

declare @id as int

select
top 1 @id = T0.id
from
GMI_TmpOprBnc T0 
where
T0.Fecha = @fecha
and REPLACE( T0.Cuenta, '-', '' ) = REPLACE( @cuenta, '-', '' ) 
and dbo.udf_SupCerosYComilSim( T0.Operacion ) = dbo.udf_SupCerosYComilSim( @operacion )
and T0.Monto = @monto

select isnull(@id, -1)