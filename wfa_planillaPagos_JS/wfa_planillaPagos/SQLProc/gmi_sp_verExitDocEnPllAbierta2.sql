/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_verExitDocEnPllAbierta2/*</nombre>*/


@idDoc as int
, @transType as int
, @docLine as int
, @idPll as int
, @asientoajustoT as nvarchar(1)


as

-- Se declara una variable para obtener el id de la planilla
declare @id int

if isnull( @idPll, -1 ) = -1
begin
	
	set @id = (select 
				top 1 T1.id
				from 
				gmi_plaPagosDetalle T0 
				inner join gmi_plaPagos T1 on T0.id = T1.id 
				where
				1 = 1
				and T1.estado like 'O'
				and T0.Id_Doc = @idDoc
				and T0.DocLine = @docLine
				and T0.Tipo_Doc = @transType
				and T0.asientoajustoT = @asientoajustoT
				)

end
else
begin

	set @id = (select 
				top 1 T1.id
				from 
				gmi_plaPagosDetalle T0 
				inner join gmi_plaPagos T1 on T0.id = T1.id 
				where
				1 = 1
				and T1.estado like 'O'
				and T0.Id_Doc = @idDoc
				and T0.Tipo_Doc = @transType
				and T0.DocLine = @docLine
				and T1.id <> @idPll
				and T0.asientoajustoT = @asientoajustoT
				)
	
end
select isnull( @id, 0 )
