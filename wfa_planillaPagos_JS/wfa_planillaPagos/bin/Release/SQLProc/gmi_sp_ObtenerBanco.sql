/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_ObtenerBanco/*</nombre>*/
	@Id	int
AS
SELECT ISNULL(T2.BankName,'') AS BankName
FROM GMI_TmpOprBnc T0
	INNER JOIN dsc1 T1
		ON T0.Cuenta = T1.Account
	INNER JOIN odsc T2
		ON T2.BankCode = T1.BankCode
WHERE T0.Id = @Id
