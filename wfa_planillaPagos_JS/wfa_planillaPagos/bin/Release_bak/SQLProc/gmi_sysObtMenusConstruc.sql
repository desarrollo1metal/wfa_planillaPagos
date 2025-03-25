/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sysObtMenusPorMnuPadre/*</nombre>*/

@idMenuPadre as int

as 

select 
*
from 
gmi_sysMenu 
where 
isnull(activo, 'N') = 'Y'
and ISNULL(idMenuPadre, -1) = @idMenuPadre
order by
ordenMenu