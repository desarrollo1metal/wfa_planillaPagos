/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_obtPlanillasAbiertas/*</nombre>*/

as 

select count('A') from gmi_plaPagos where estado = 'O'