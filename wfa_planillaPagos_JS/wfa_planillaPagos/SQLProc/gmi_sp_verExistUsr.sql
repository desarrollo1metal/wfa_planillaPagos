/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_verExistUsr/*</nombre>*/

@usr as nvarchar(100)

as

select count('A') from gmi_sysUsr where codigo = @usr