/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_ReporteDepositos/*</nombre>*/

@fDesde as datetime
, @fHasta as datetime
, @estado as nvarchar(20)

AS

--declare @ld_fDesde datetime = @fDesde
--declare @ld_fHasta datetime = @fHasta
--declare @lv_estado nvarchar(20) = @estado

SELECT FECHA,
		CONVERT(VARCHAR(10),FECHA,103) AS FECHA2,
		Descripcion,
		CONVERT(NUMERIC(19,2),Monto) AS Monto,
		Operacion,
		Cuenta,
		Id,
		Estado
		, Numero
		, CONVERT(VARCHAR(10),Fecha_Creacion,103)
		, CONVERT(VARCHAR(10),Fecha_Proceso,103)
		, Estado_Pll
		, Nro_SAP_PR
FROM gmi_DepositosPendientes
where
Estado like replace(@estado, 'Todos', '') + '%'
and FECHA between @fDesde and @fHasta
order by 1 desc
OPTION (RECOMPILE)