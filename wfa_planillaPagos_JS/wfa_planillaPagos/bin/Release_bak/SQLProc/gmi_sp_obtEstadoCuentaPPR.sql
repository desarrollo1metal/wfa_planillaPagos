/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_obtEstadoCuentaPPR/*</nombre>*/

@pv_cuenta nvarchar(25) 

as

with tvw_orct as
(
select U_GMI_RENDICION, TrsfrAcct, CashAcct, Canceled, TaxDate from ORCT where isnull(U_GMI_RENDICION, '') <> '' and ISNULL(Canceled, 'N') <> 'Y'
)


select 
T0.id
, T0.Fecha
, T0.Descripcion + isnull(': ' + T5.CardCode + ' - ' + T5.CardName, '') as Descripcion
, T0.Monto
, T0.Operacion
, T0.Cuenta
, T2.ActCurr as Moneda
, T3.BankName as Banco
, T2.AcctCode as Cta_Contable
, T0.Ruc
, 'OP' as TipoOp
, case isnull(T0.esPll, 'N') when 'Y' then 'Si' else 'No' end as EC_dePlanilla
from 
GMI_TmpOprBnc T0 
left join DSC1 T1 on T0.Cuenta = T1.Account
left join OACT T2 on T1.GLAccount = T2.AcctCode
left join ODSC T3 on T1.BankCode = T3.BankCode
left join tvw_orct/*ORCT*/ T4 on dbo.udf_SupCerosYComilSim( T0.Operacion ) = dbo.udf_SupCerosYComilSim( T4.U_GMI_RENDICION ) 
							 and (T4.TrsfrAcct = T2.AcctCode or T4.CashAcct = T2.AcctCode) 
							 and ISNULL(T4.Canceled, 'N') <> 'Y'
							 and T4.TaxDate = T0.Fecha
left join OCRD T5 on isnull(T0.Ruc, '') = isnull(T5.LicTradNum, '') and T5.CardType = 'C'
where 
--T0.Cuenta like '%'+ isnull(@pv_cuenta, '') +'%'
T0.Cuenta like '%'+ isnull(@pv_cuenta, '') +'%'
--and T0.id not in (select idEC from IdECPla)
and not exists(
		select 1
		from	gmi_plaPagosDetalle ppd
		inner	join gmi_plaPagos	pp	on ppd.id = pp.id
		left	join ORCT			p	on ppd.DocEntrySAP = p.DocEntry and pp.estado = 'C'
		where
			ppd.idEC = T0.id
		and pp.estado in ('O', 'C') 
		and case when p.DocEntry is null then 'A' else p.Canceled end = case when p.DocEntry is null then 'A' else 'N' end
)
and isnull(T0.Monto, 0.000000) > 0.000000
and isnull(T4.U_GMI_RENDICION, '') = ''
and isnull(T0.esPll, 'N') = 'Y'

--union all

--select 
--T0.TransId as id
--, T0.RefDate as Fecha
--, 'Saldo a Favor: ' + T1.CardCode + ' - ' + T1.CardName as Descripcion
--, case isnull(T0.FCCurrency, '') when '' then T0.BalDueCred else T0.BalFcCred end as Monto
--, isnull(T2.U_GMI_RENDICION, '') as Operacion
--, T3.Account as Cuenta
--, case isnull(T0.FCCurrency, '') when '' then 'SOL' else T0.FCCurrency end as Moneda
--, T4.BankName as Banco
--, T3.GLAccount as Cta_Contable
--, '' as Ruc
--, 'SF' as TipoOp
--from 
--JDT1 T0
--inner join OCRD T1 on T0.ShortName = T1.CardCode
--inner join ORCT T2 on T0.TransId = T2.TransId
--inner join DSC1 T3 on T2.TrsfrAcct = T3.GLAccount
--inner join ODSC T4 on T3.BankCode = T4.BankCode
--where 
--T0.TransType = 24
--and T3.Account like '%'+ isnull(@pv_cuenta, '') +'%'
--and isnull(T0.BalDueCred, 0.000000) > 0.000000

order by 
T0.Fecha 
desc 
OPTION(RECOMPILE)