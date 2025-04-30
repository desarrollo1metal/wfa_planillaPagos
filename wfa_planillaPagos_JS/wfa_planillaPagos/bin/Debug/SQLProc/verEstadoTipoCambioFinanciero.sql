/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/verEstadoTipoCambioFinanciero/*</nombre>*/

@fecha as varchar(8)
as
SELECT ISNULL(SUM(U_GMI_TC_USD), 1) AS Valor
FROM [@GMI_TC_DETALLE]
WHERE U_GMI_TC_DIA = @fecha

