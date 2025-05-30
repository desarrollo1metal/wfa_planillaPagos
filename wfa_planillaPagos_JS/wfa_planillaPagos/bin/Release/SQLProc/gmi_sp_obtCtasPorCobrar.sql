/*<accion>*/ALTER/*</accion>*/
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/[dbo].[gmi_sp_obtCtasPorCobrar]/*</nombre>*/

@cardCode as nvarchar(25)

as

select

'X' AS 'Detraccion',

T1.CardCode as Codigo
, T1.CardName as Nombre
, case T0.TransType
	when '13' then
		'Factura/Boleta'
	when '203' then
		'Anticipo'
	else
		T0.TransType
  end as Tipo_Doc
, isnull(T0.Ref2, '') as Referencia
, T0.RefDate as FechaDoc
, isnull(T0.LineMemo, '') as Comentario
, CASE ISNULL(T0.FCCurrency, '') when '' then 'SOL' else T0.FCCurrency end MonedaDoc
, T2.DocRate as TipoCambioDoc
, CASE ISNULL(T0.FCCurrency, '') when '' then T0.Debit else T0.FCDebit end as Total
, CASE ISNULL(T0.FCCurrency, '') when '' then T0.BalDueDeb else T0.BalFcDeb end as Saldo
, T0.BaseRef as Ref_SAP
, T0.CreatedBy as Id_Doc
, T0.TransType
, T0.Line_ID
, T1.LicTradNum Ruc
,(select top 1 LEft(i.ItemCode,4) from OINV o INNER JOIN INV1 i on o.Docentry=i.DocEntry inner join JDT1 j on o.TransId=j.TransId where j.TransId=T0.TransId Group by i.ItemCode) CodBien
from 
JDT1 T0 
inner join OCRD T1 on T0.ShortName = T1.CardCode
inner join gmi_vw_ventas T2 on T0.TransType = T2.ObjType and T0.CreatedBy = T2.DocEntry
where
	CASE ISNULL(T0.FCCurrency, '') when '' then T0.BalDueDeb else T0.BalFcDeb end > 0.000000
--and not exists(select 1 
--				from		gmi_plaPagosDetalle ppd 
--				inner join	gmi_plaPagos pp on ppd.id = pp.id 
--				where ppd.Tipo_Doc = T0.TransType and ppd.Id_Doc = T0.CreatedBy and isnull(pp.estado, '') = 'O')
and not exists(
		select 1
		from	gmi_plaPagosDetalle ppd
		inner	join gmi_plaPagos	pp	on ppd.id = pp.id
		left	join ORCT			p	on ppd.DocEntrySAP = p.DocEntry and pp.estado = 'C'
		where
			ppd.Tipo_Doc = T0.TransType 
		and ppd.Id_Doc = T0.CreatedBy
		and pp.estado in ('O') 
		--and pp.estado in ('O', 'C') 
		and case when p.DocEntry is null then 'A' else p.Canceled end = case when p.DocEntry is null then 'A' else 'N' end
)
and T1.CardType = 'C'
and T1.CardCode like '%' + @cardCode + '%'
and T0.TransType in ('13', '203')
and isnull( (select Count('A')
			 from 
			 [@VS_LTR_ORIGENCNJ] L0 
			 inner join [@VS_LTR_INFOCNJ] L1 on L0.U_VS_CANJE = L1.Code
			 inner join ORCT L2 on L1.U_VS_PAGOINTERNO = L2.DocEntry and isnull(L2.Canceled, 'X') <> 'Y'
			 where 
			 L0.U_VS_NUMEROSAP = T0.CreatedBy 
			 and L0.U_VS_INVTYPE = T0.TransType 
			 and isnull(L0.U_VS_ESTADO, '') <> 'R' 
			 and L0.U_VS_MONTO = case L0.U_VS_MONEDA when 'SOL' then T0.BalDueDeb else T0.BalFcDeb end), 0 )  =  0
union all
select

'Y' AS 'Detraccion',
T1.CardCode as Codigo
, T1.CardName as Nombre
, case T0.TransType
	when '30' then
		'Letra'
	else
		T0.TransType
  end as Tipo_Doc
, isnull(T0.LineMemo, '') as Referencia
, T0.RefDate as FechaDoc
, isnull(T0.Ref2, '') as Comentario
, CASE ISNULL(T0.FCCurrency, '') when '' then 'SOL' else T0.FCCurrency end MonedaDoc
, CASE ISNULL(T0.FCCurrency, '') when '' then 1.000000 else (select R0.Rate from ORTT R0 where R0.RateDate = T0.RefDate and R0.Currency = ISNULL(T0.FCCurrency, '')) end TipoCambioDoc
, CASE ISNULL(T0.FCCurrency, '') when '' then T0.Debit else T0.FCDebit end as Total
, CASE ISNULL(T0.FCCurrency, '') when '' then T0.BalDueDeb else T0.BalFcDeb end as Saldo
, T0.BaseRef as Ref_SAP
, T0.TransId as Id_Doc
, T0.TransType
, T0.Line_ID
, T1.LicTradNum Ruc
,(select top 1 LEft(i.ItemCode,4) from OINV o INNER JOIN INV1 i on o.Docentry=i.DocEntry inner join JDT1 j on o.TransId=j.TransId where j.TransId=T0.TransId Group by i.ItemCode) CodBien
from 
JDT1 T0 
inner join OCRD T1 on T0.ShortName = T1.CardCode
LEFT join "@VS_LTR_LETRACNJ" T2 on T0.TransId = T2.U_VS_ASIENTO and T0.Line_ID = T2.U_VS_ASIENTOLIN
where
	CASE ISNULL(T0.FCCurrency, '') when '' then T0.BalDueDeb else T0.BalFcDeb end > 0.000000
--and not exists(select 1 
--				from		gmi_plaPagosDetalle ppd 
--				inner join	gmi_plaPagos pp on ppd.id = pp.id 
--				where ppd.Tipo_Doc = T0.TransType and ppd.Id_Doc = T0.TransId and isnull(pp.estado, '') = 'O')
and not exists(
		select 1
		from	gmi_plaPagosDetalle ppd
		inner	join gmi_plaPagos	pp	on ppd.id = pp.id
		left	join ORCT			p	on ppd.DocEntrySAP = p.DocEntry and pp.estado = 'C'
		where
			ppd.Tipo_Doc = T0.TransType 
		and ppd.Id_Doc = T0.TransId
		and pp.estado in ('O') 
		--and pp.estado in ('O', 'C') 
		and case when p.DocEntry is null then 'A' else p.Canceled end = case when p.DocEntry is null then 'A' else 'N' end
)
and T1.CardType = 'C'
and T1.CardCode like '%' + @cardCode + '%'
and T0.TransType in ('30')
--and T0.U_VS_ESLETRA like 's%'
--and T2.U_VS_SITUACION = '01'

order by 
T0.RefDate desc