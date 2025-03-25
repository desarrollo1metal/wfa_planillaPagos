/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_obtMenuTitulo/*</nombre>*/

as

select -1 as id, 'Ninguno' as nomMenu

UNION all

select id, nomMenu from gmi_sysMenu where tipoMenu = 0

order by 1