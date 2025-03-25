-- Asientos en donde estén presentes loas cuentas de diferencia de cambio - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
select 
T0.TransId
, T0.RefDate
, T0.Memo
, T1.Account
, T1.ShortName
, T1.Debit 
, T1.Credit
, T1.FCDebit 
, T1.FCCredit
, T1.LineMemo
, T0.DataSource
, T0.TransType
, T0.TransRate
, T0.TransCurr
from 
OJDT T0 
inner join JDT1 T1 on T0.TransId = T1.TransId
where 
1 = 1
and T0.DataSource = 'I' 
and T0.TransType = 30
and ( T1.Account in ('776001', '977601') or T1.ContraAct in ('776001', '977601') )
and year(T0.RefDate) = 2016
--and T1.ShortName = 'C001396'
--and T0.TransId = 679817
order by 
T0.TransId desc
-- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

-- Pagos Recibidos que tengan Pago a Cuenta - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
 select 
 CardCode
 , CardName
 , DocNum
 , DocDate
 , NoDocSum
 , NoDocSumFC
 , TransId 
 , DataSource
 from 
 ORCT 
 where 
 1 = 1
 --and CardCode = 'C001869' 
 and NoDocSum <> 0.000000 
 and DocType <> 'A'
 order by 
 DocDate desc
 -- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

 -- Asientos de Pagos recibidos que tienen pago a cuenta - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

  select 
 T0.CardCode
 , T0.CardName
 , T0.DocNum
 , T0.DocDate
 , T0.NoDocSum
 , T0.NoDocSumFC
 , T0.TransId 
 , T0.DataSource
 , T1.BalDueCred
 , T1.BalDueDeb
 , T0.Canceled
 from 
 ORCT T0
 inner join JDT1 T1 on T0.TransId = T1.TransId and T0.CardCode = T1.ShortName
 where 
 1 = 1
 --and CardCode = 'C001869' 
 and T0.NoDocSum <> 0.000000 
 and T0.DocType <> 'A'
 and T0.DocCurr = 'SOL'
 order by 
 T0.DocDate desc

 -- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

 select TransId, Debit, Credit, FCDebit, FCCredit, BalDueDeb, BalDueCred, BalFcDeb, BalFcCred from JDT1 where TransId = 658650

 -- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

 select 
T0.ReconNum, T1.TransId, T1.Account, T1.ReconSum, T1.ReconSumFC
from 
OITR T0 
inner join ITR1 T1 on T0.ReconNum = T1.ReconNum
where
1 = 1
--and T1.Account = '103002'
and TransId in (658650)

-- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

