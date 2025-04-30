/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_verEstadoPeriodoValidacion/*</nombre>*/

@fecha as varchar(8)
as

BEGIN
    SET NOCOUNT ON;

    SELECT COALESCE(
        (SELECT PeriodStat FROM OFPR WHERE @fecha BETWEEN F_RefDate AND T_RefDate), 
        'C'
    ) AS PeriodStat;
END
