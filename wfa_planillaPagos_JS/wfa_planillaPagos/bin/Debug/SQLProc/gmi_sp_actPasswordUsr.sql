/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_actPasswordUsr/*</nombre>*/

@codigo as nvarchar(50)
, @password as nvarchar(150)

AS

update gmi_sysUsr set password = @password where codigo = @codigo