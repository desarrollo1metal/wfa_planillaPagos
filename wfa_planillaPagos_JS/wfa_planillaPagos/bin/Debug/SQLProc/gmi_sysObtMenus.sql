/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sysObtMenus/*</nombre>*/

as 

select 
id
, nomMenu
, tipoMenu
, idMenuPadre
, ordenMenu
, funcion
, activo 
from 
gmi_sysMenu where isnull(activo, 'N') = 'Y'