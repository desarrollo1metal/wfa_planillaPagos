/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_obtCantLineasAsgPll/*</nombre>*/

@pi_id int

as

select distinct lineaNumAsg into #LineaNumAsg from gmi_plaPagosDetalle where id = @pi_id

select count(lineaNumAsg) from #LineaNumAsg