/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_verIdECenPlanillas/*</nombre>*/

@id as int
, @idEC as int

AS

declare @li_idPla int

set @li_idPla =( select
					top 1 T1.id
					from
					gmi_plaPagosDetalle T0
					inner join gmi_plaPagos T1 on T0.id = T1.id
					left join ORCT T2 on T0.DocEntrySAP = T2.DocEntry and T1.estado = 'C'
					where 
					T0.id <> @id
					and T0.idEC = @idEC
					and T1.estado in ('O', 'C') 
					and case isnull(T2.DocEntry, -1) when -1 then 'A' else isnull(T2.Canceled, 'N') end = case isnull(T2.DocEntry, -1) when -1 then 'A' else 'N' end)

set @li_idPla = isnull(@li_idPla, 0)

select @li_idPla