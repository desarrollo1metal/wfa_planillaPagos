/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/view/*</tipoSQL>*/ 
/*<nombre>*/gmi_DepositosPendientes/*</nombre>*/

AS

-- Se obtiene todos los registros de Estados de Cuenta existentes en la tabla de detalle de planilla
with 
ECenPll as (
select 
T0.id
, T2.estado
from 
GMI_TmpOprBnc T0 
inner join gmi_plaPagosDetalle T1 on T0.id = T1.idEC
inner join gmi_plaPagos T2 on T1.id = T2.id)
, ECenPR as (
select 
T2.Operacion
, T5.DocNum
, T2.id
, T5.DocEntry
from
GMI_TmpOprBnc T2 
inner join DSC1 T3 on T2.Cuenta = T3.Account
--inner join OACT T4 on T3.GLAccount = T4.AcctCode	
inner join ORCT T5 on dbo.udf_SupCerosYComilSim( isnull(T2.Operacion, '') ) = dbo.udf_SupCerosYComilSim( isnull(T5.U_GMI_RENDICION, '') ) and (T5.TrsfrAcct = T3.GLAccount or T5.CashAcct = T3.GLAccount) and ISNULL(T5.Canceled, 'N') = 'N'
where
T2.id not in (select idEC from gmi_plaPagosDetalle)
)


-- Se realiza la consulta del reporte

-- - Registros del estado de cuenta en planillas abiertas y cerradas
	select distinct
			T2.Fecha,
			T2.Descripcion,
			T2.monto,
			T2.Operacion,
			T2.Cuenta,
			T2.Id,
			Estado = case T0.estado When 'C' Then /*'Procesado'--*/case isnull(T3.Canceled, 'N') when 'N' then 'Procesado' else 'No Procesado' end
									When 'O' Then 'Por Procesar'
									else '-' end
			, '1' as QryOrigen
			, case when T0.id <= 0 then null else T0.id end as Numero
			, T0.FechaCrea as Fecha_Creacion
			, T0.FechaPrcs as Fecha_Proceso
			, case T0.estado when 'C' then 'Cerrado' when 'O' then 'Abierto' when 'A' then 'Cancelado' else '-' end as Estado_Pll
			, T1.DocNumSAP as Nro_SAP_PR
	from 
	gmi_plaPagos T0 
	inner join gmi_plaPagosDetalle T1 on T0.id = T1.id
	inner join GMI_TmpOprBnc T2 on T1.idEC = T2.id
	left join ORCT T3 on T1.DocEntrySAP = T3.DocEntry
	where 
	T0.estado in ('C', 'O')
	and T2.Monto > 0.000000	
	--and (select count('A') from ECenPR E0 where E0.DocEntry = T1.DocEntrySAP) > 0

	UNION ALL

-- - Registros del estado de cuenta en planillas anuladas, y no existentes en otras planillas abiertas o cerradas
	select distinct 
			T2.Fecha,
			T2.Descripcion,
			T2.monto,
			T2.Operacion,
			T2.Cuenta,
			T2.Id,
			Estado = case T0.estado When 'A' Then 'No Procesado*'																		
									else '-' end
			, '2' as QryOrigen
			--, T0.id as Numero
			--, T0.FechaCrea as Fecha_Creacion
			--, T0.FechaPrcs as Fecha_Proceso
			--, case T0.estado when 'C' then 'Cerrado' when 'O' then 'Abierto' when 'A' then 'Cancelado' else '-' end as Estado_Pll
			--, T1.DocNumSAP as Nro_SAP_PR
			, null as Numero
			, null as Fecha_Creacion
			, null as Fecha_Proceso
			, null as Estado_Pll
			, null as Nro_SAP_PR
	from 
	gmi_plaPagos T0 
	inner join gmi_plaPagosDetalle T1 on T0.id = T1.id
	inner join GMI_TmpOprBnc T2 on T1.idEC = T2.id
	left join ECenPR T3 on T2.id = T3.id
	where 
	T0.estado in ('A')
	and T2.id not in (select E0.id from ECenPll E0 where E0.estado in ('O', 'C'))
	--and T2.id not in (select E0.id from ECenPR E0)
	and T2.Monto > 0.000000
	and isnull(T3.id, -1) = -1
	and isnull(T2.esPll, 'N') = 'Y'

	UNION ALL

-- - Registros del estado de cuenta que no existen en planillas
	select T0.Fecha,
			T0.Descripcion,
			T0.monto,
			T0.Operacion,
			T0.Cuenta,
			T0.Id,
			'No Procesado' as Estado
			, '3' as QryOrigen
			, null as Numero
			, null as Fecha_Creacion
			, null as Fecha_Proceso
			, null as Estado_Pll
			, null as Nro_SAP_PR
	from GMI_TmpOprBnc T0
	left join DSC1 T1 on T0.Cuenta = T1.Account
	left join OACT T2 on T1.GLAccount = T2.AcctCode	
	--left join ECenPR T3 on dbo.udf_SupCerosYComilSim( T0.Operacion ) = dbo.udf_SupCerosYComilSim( T3.Operacion )
	left join ECenPR T3 on T0.id = T3.id
	where 
	T0.id not in (select idEC from gmi_plaPagosDetalle)
	and T0.Monto > 0.000000
	and isnull(T3.Operacion, '') = ''
	and isnull(T0.esPll, 'N') = 'Y'

	UNION ALL

	select 
	T0.Fecha,
			T0.Descripcion,
			T0.monto,
			T0.Operacion,
			T0.Cuenta,
			T0.Id,
			'Procesado Manual' as Estado
			, '4' as QryOrigen
			, null as Numero
			, null as Fecha_Creacion
			, null as Fecha_Proceso
			, null as Estado_Pll
			, T3.DocNum as Nro_SAP_PR
	from 
	GMI_TmpOprBnc T0 
	left join DSC1 T1 on T0.Cuenta = T1.Account
	left join OACT T2 on T1.GLAccount = T2.AcctCode	
	--left join ECenPR T3 on dbo.udf_SupCerosYComilSim( T0.Operacion ) = dbo.udf_SupCerosYComilSim( T3.Operacion )
	left join ECenPR T3 on T0.id = T3.id
	where
	isnull(T3.Operacion, '') <> ''	
	and T0.Monto > 0.000000
	and T0.id not in (select idEC from gmi_plaPagosDetalle)
	and isnull(T0.esPll, 'N') = 'Y'