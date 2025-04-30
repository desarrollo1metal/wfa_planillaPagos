/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_verIdDocEnPlanillas/*</nombre>*/

@id as int
, @id_Doc as int

AS

declare @li_idPla int

set @li_idPla =( select
					top 1 T1.id
					from
					gmi_plaPagosDetalle T0
					inner join gmi_plaPagos T1 on T0.id = T1.id
					where 
					T0.id <> @id
					and T0.Id_Doc = @id_Doc
					and T1.estado in ('O') )

set @li_idPla = isnull(@li_idPla, 0)

select @li_idPla