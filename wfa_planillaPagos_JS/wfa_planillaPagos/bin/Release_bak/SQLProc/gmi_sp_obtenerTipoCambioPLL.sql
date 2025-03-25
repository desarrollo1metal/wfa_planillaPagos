/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_obtenerTipoCambioPLL/*</nombre>*/

@pv_moneda nvarchar(3)
, @pv_fecha nvarchar(8)
, @pv_bd nvarchar(50)

as 


if ISNULL(@pv_bd, '') = ''
begin

	select
	Rate
	from 
	ORTT
	where
	RateDate = @pv_fecha
	and Currency = @pv_moneda

end
else
begin

	select
	Rate
	from 
	ORTT
	where
	RateDate = @pv_fecha
	and Currency = @pv_moneda

end