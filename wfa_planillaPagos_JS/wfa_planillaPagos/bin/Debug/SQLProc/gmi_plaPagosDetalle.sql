create table gmi_plaPagosDetalle(

id int
, lineaNumAsg int
, Codigo nvarchar(30)
, Nombre nvarchar(200)
, Id_Doc int
, Referencia nvarchar(100)
, Tipo_Doc nvarchar(20)
, DocLine int 
--, Tiene_Dtr nvarchar(10)
--, Porcentaje_Dtr numeric(19, 6)
, FechaDoc datetime
, Comentario nvarchar(150)
, MonedaDoc nvarchar(3)
, TipoCambioDoc numeric(19, 6)
, Total numeric(19, 6)
, Saldo numeric(19, 6)
, Imp_Aplicado numeric(19, 6)
, Imp_AplicadoME numeric(19, 6)
, SaldoFavor numeric(19, 6)
, SaldoFavorME numeric(19, 6)
, MontoOp numeric(19, 6)
, MonedaPag nvarchar(3)
, Tipo_Cambio numeric(19, 6) 
, Cuenta nvarchar(20)
, FechaPago datetime
, Nro_Operacion nvarchar(40)
, idEC int 
, ComentarioPl nvarchar(150)
, tipoAsg int
, DocNumSAP int 
, DocEntrySAP int 
, tipoEC char(2)
, difTC char(1)

)