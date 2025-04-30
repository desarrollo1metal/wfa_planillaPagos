/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_obtMontoacuentaReconciliacion/*</nombre>*/

@DocEntry integer

as

select NoDocSum
from ORCT
WHERE DocEntry = @DocEntry

