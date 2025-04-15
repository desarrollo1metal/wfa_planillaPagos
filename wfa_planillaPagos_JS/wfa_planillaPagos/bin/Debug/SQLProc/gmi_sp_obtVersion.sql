/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_obtVersion/*</nombre>*/

@baseDatos as nvarchar(150)

as

select appVersion from gmi_appPllConfig where company = @baseDatos