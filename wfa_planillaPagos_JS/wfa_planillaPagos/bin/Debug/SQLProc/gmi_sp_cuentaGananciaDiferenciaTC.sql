/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_cuentaGananciaDiferenciaTC/*</nombre>*/

as


BEGIN
	--select '776001' AS 'AcctCode'
	DECLARE @EnbSgmnAct CHAR(1);
    -- Obtener el valor del campo (se asume que CINF tiene solo una fila)
    SELECT @EnbSgmnAct = ISNULL(EnbSgmnAct,'N') FROM CINF;

    -- Evaluar el valor
    IF @EnbSgmnAct = 'Y'
    BEGIN
        -- BLOQUE SI ES 'Y'
        select '_SYS00000001306' as AcctCode;

        -- Aqu� puedes poner otra consulta, por ejemplo:
        -- SELECT * FROM OtraTabla;
    END
    ELSE
    BEGIN
        SELECT '776001' AS AcctCode;
    END

END
