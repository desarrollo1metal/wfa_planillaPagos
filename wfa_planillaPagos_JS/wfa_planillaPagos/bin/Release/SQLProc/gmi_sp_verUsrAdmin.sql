/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_verUsrAdmin/*</nombre>*/

AS

-- Se verifica si existe el usuario administrador
declare @cont int = ( select COUNT('A') from gmi_sysusr where codigo like 'administrador' )

-- Se ingresa el usuario si no existe en la base de datos
if @cont < 1
begin
	insert into gmi_sysusr(codigo, nombre, activo, admin, FechaCrea, FechaAct, winUsrCrea, password) values ('administrador', 'Administrador del sistema', 'Y', 'Y', GETDATE(), GETDATE(), 'administrador', 'hiVfxyl8rjwwrg1pi1MWFMMF2w1Xq1B7ns7ju3jIi1Y=')
end