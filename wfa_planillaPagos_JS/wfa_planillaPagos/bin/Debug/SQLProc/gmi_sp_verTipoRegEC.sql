/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_verTipoRegEC/*</nombre>*/

@id as int

as

declare @lc_esPll char(1)

select top 1 @lc_esPll = esPll from GMI_TmpOprBnc where id = @id

select isnull(@lc_esPll, '-')