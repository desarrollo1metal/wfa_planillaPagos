/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_obtUltNroPlanilla/*</nombre>*/

as

-- Se obtiene el ultimo Id de la planilla
declare @ultimoId int
set @ultimoId = (select max(id) from gmi_plaPagos)
set @ultimoId = isnull(@ultimoId, 0) + 1

select @ultimoId as id