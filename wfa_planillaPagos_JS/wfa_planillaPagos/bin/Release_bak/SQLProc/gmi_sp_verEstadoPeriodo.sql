/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_verEstadoPeriodo/*</nombre>*/

@fecha as datetime

as

select PeriodStat from OFPR where @fecha between F_RefDate and T_RefDate