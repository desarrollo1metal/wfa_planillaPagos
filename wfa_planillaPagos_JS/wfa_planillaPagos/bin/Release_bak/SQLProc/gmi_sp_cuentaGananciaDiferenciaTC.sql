/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_cuentaGananciaDiferenciaTC/*</nombre>*/

as

select TOP 1 ISNULL( AcctCode,'') AS AcctCode
from OACT
where AcctName LIKE  '%GANANCIA DIFERENCIA%'