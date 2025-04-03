/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_verExistCampoDeTable/*</nombre>*/

@campo as nvarchar(75)
, @tabla as nvarchar(75)

as

select COUNT('A') from sys.columns T0 inner join sys.objects T1 on T0.object_id = T1.object_id where T0.name like @campo and T1.name like @tabla