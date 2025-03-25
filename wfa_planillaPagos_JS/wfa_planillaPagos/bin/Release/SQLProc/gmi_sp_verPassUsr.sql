/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_verPassUsr/*</nombre>*/

@codigo as nvarchar(50)
, @password as nvarchar(150)

as

select count('A') from gmi_sysUsr where codigo = @codigo and password = @password