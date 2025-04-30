/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_obtenerLineaPR/*</nombre>*/

@transId as nvarchar(50),
@cardcode as NVARCHAR(50)
as

BEGIN
    SET NOCOUNT ON;

    SELECT COALESCE(
        (SELECT TOP 1 Line_ID FROM JDT1 WHERE TransId = @transId AND ShortName = @cardcode), 
        1
    ) AS Line_ID;
END
