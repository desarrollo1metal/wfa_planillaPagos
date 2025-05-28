/*<accion>*/create/*</accion>*/
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/[dbo].[gmi_sp_verEstadoTipoCambioFinanciero]/*</nombre>*/
@fecha as varchar(8)
as
SELECT ISNULL(SUM(U_GMI_TC_USD), 1) AS Valor
FROM [@GMI_TC_DETALLE] gmi_sp_verExitDocEnPllAbierta2
WHERE U_GMI_TC_DIA = @fecha
