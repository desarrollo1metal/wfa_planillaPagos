/*<accion>*/create/*</accion>*/ 
/*<tipoSQL>*/procedure/*</tipoSQL>*/ 
/*<nombre>*/gmi_sp_obtenerInfoEstadoCuentaBancoTXT_PLPR/*</nombre>*/
 
@pv_ruta as nvarchar(max)

as

-- Se crea una tabla temporal para almacenar los datos obtenidos desde el archivo de texto
create table #TEMP  (

Fecha datetime
, Descripcion nvarchar(75)
--, Monto decimal(19, 6)
, Monto nvarchar(20)
, Operacion nvarchar(20)
, Cuenta nvarchar(50)
, ruc nvarchar(20)

)

-- Se declara una variable para la sentencia SQL
declare @sql nvarchar(max)

-- Se realiza la inserción de los datos del archivo de texto en la tabla temporal
set @sql = 'BULK INSERT #TEMP FROM ''' + @pv_ruta + ''' WITH (ROWTERMINATOR =''\n'' ,FIELDTERMINATOR = ''\t'' ,FIRSTROW=1) '

-- Se ejecuta la sentencia SQL
exec (@sql)

-- Se declara las variables locales necesarias para los calculos del procedimiento almacenado
declare @pv_cuenta nvarchar(20) -- FormatCode de la cuenta contable
declare @pd_fecDesde datetime -- Fecha Inicial para la consulta
declare @pd_fecHasta datetime -- Fecha Final para la consulta

-- Se obtiene los valores
set @pv_cuenta = ''
set @pd_fecDesde = (select min(Fecha) from #TEMP)
set @pd_fecHasta = (select max(Fecha) from #TEMP)

-- Se inserta la información filtrada mediante los parametros recibidos por el Stored Procedure en una nueva tabla temporal
select 
ROW_NUMBER() over (order by Fecha) as 'Fila' -- Se añade un codigo único a cada registro de la información importada mediante la enumación de los registros
, Fecha
, Descripcion
, cast(replace(Monto, ',', '.') as numeric(19,6)) as Monto
, Operacion
, Cuenta
, ruc
into #TMP_2 -- Se inserta la información en otra tabla temporal
from 
#TEMP 
where 
REPLACE(Cuenta, '-', '') like '%' + REPLACE(@pv_cuenta, '-', '') + '%'
and Fecha between @pd_fecDesde and @pd_fecHasta

-- Se genera un codigo para cada registro que no cuente con numero de operacion con el formato iyyyyMMddccccnn: donde cccc son los cuatro ultimos dígitos de la cuenta bancaria y nn es un contador de registros de la tabla temporal #TMP2 
select 
'i' + CONVERT(VARCHAR(10), [Fecha], 112) + RIGHT(REPLACE([Cuenta], '-', ''), 4) + CAST([Indicador] as nvarchar(11)) as 'N_Operacion' 
, * 
into
#TMP_3 -- Se inserta la información en una nueva tabla temporal: #TMP_3
from 
(
select 
ROW_NUMBER() over (partition by Fecha, Cuenta order by Fecha asc) as 'Indicador' -- Se enumera los registros de la tabla temporal #TMP_2
, * 
from 
#TMP_2 
where
(replace(Operacion, ' ', '') like '00' or ISNULL(Operacion, '') = '')
) S0

-- Se realiza la actualizacion en la tabla
UPDATE T0
SET T0.[Operacion] = T1.[N_Operacion]
FROM 
#TMP_2 T0
inner join #TMP_3 T1 on T0.[Fila] = T1.[Fila]
WHERE
(replace(T0.[Operacion], ' ', '') like '00' or ISNULL(T0.[Operacion], '') = '')

-- Se inserta los datos en la tabla de Estado de Cuenta
-- Se verifica que la consulta a ingresar tenga registros
declare @li_contador int
set @li_contador = (select COUNT('A') from #TMP_2)

-- Se verifica si hay registros a importar
IF @li_contador > 0
begin

	-- Antes de ingresar los registros, se valida que los registros a sobreescribir, si los hubiera, no se encuentren registrados en el detalle de una planilla Abierta o Cancelada
	declare @li_verECenPla int
	set @li_verECenPla = ( select
							COUNT('A')
							from
							GMI_TmpOprBnc T0
							inner join gmi_plaPagosDetalle T1 on T0.id  = T1.idEC 
							inner join gmi_plaPagos T2 on T1.id = T2.id 
							where 
							REPLACE(T0.Cuenta COLLATE DATABASE_DEFAULT, '-', '') in (select REPLACE(S0.Cuenta COLLATE DATABASE_DEFAULT, '-', '') from #TMP_2 S0 where S0.Fecha = T0.Fecha)
							and T2.estado in ('C', 'O') )

	-- Si el verificador es mayor a 0, quiere decir que existen registros del estado de cuenta, para el mismo dia y para la misma cuenta, en el detalle de alguna planilla abierta o cerrada
	-- Por tal motivo no se puede proceder a realizar la sustitucion de dichos registros 
	if @li_verECenPla = 0
	begin

		-- Se obtiene el último ID para los registros que serán insertados
		declare @nId int = (select max(id) from GMI_TmpOprBnc)
		set @nId = isnull(@nId, 0)

		-- Se crea una tabla temporal en donde se encuentren los registros del estado de cuenta que coinciden con los datos recibidos en Cuenta, Fecha y Nro. de Operacion
		select 
		T1.id 
		into #TMP_4
		from 
		#TMP_2 T0
		inner join GMI_TmpOprBnc T1 on T0.Fecha = T1.Fecha
									and REPLACE(T0.Cuenta COLLATE DATABASE_DEFAULT, '-', '') = REPLACE(T1.Cuenta COLLATE DATABASE_DEFAULT, '-', '') 
									and dbo.udf_SupCerosYComilSim( T0.Operacion COLLATE DATABASE_DEFAULT ) = dbo.udf_SupCerosYComilSim( T1.Operacion COLLATE DATABASE_DEFAULT)
		
		-- Se crea una tabla temporal en donde se encuentren los registros del estado de cuentan que ya están registrados en planillas
		select 
		T2.id
		, T2.Operacion
		, T2.Cuenta
		, T0.id as idPll
		, T2.Fecha
		into #TMP_5
		from 
		gmi_plaPagosDetalle T0 
		inner join gmi_plaPagos T1 on T0.id = T1.id 
		inner join gmi_tmpOPrBnc T2 on T0.idEC = T2.id
		where T1.estado in ('C', 'O')

		-- Se crea una tabla temporal que contenga los datos que se ingresarán a la table de Estado de Cuenta
		select 
		T0.Fecha
		, T0.Descripcion
		, T0.Monto
		, T0.Operacion
		, T0.Cuenta
		, @nId + ROW_NUMBER() over (order by T0.Fecha asc) as Id
		, T0.ruc
		into #TMP_6
		from 
		#TMP_2 T0 
		left join #TMP_5 T1 on T0.Fecha = T1.Fecha
								and REPLACE(T0.Cuenta COLLATE DATABASE_DEFAULT, '-', '') = REPLACE(T1.Cuenta COLLATE DATABASE_DEFAULT, '-', '') 
								and dbo.udf_SupCerosYComilSim( T0.Operacion COLLATE DATABASE_DEFAULT ) = dbo.udf_SupCerosYComilSim( T1.Operacion COLLATE DATABASE_DEFAULT)
		where
		isnull(T1.idPll, -1) = -1
		order by 
		T0.Cuenta
		, T0.Fecha
		, T0.Operacion

		-- Se borra los registros de la tabla con el mismo numero de cuenta y que se encuentren dentro del rango de fechas ingresado como parametro
		delete 
		GMI_TmpOprBnc 
		where 
		id in (select id from #TMP_4)
		and id not in (select id from #TMP_5)		

		-- Se inserta los datos en la tabla
		insert into GMI_TmpOprBnc 
		select 
		*
		from 
		#TMP_6 
		order by 
		Id asc

		-- Se muestra los datos recien importados
		select 
		Fecha as fecha
		, Descripcion as descripcion
		, Monto as monto
		, Operacion as operacion
		, Cuenta as cuenta
		, Id as id
		, ruc
		from 
		#TMP_6
		--where
		--REPLACE(Cuenta COLLATE DATABASE_DEFAULT, '-', '') in (select REPLACE(S0.Cuenta COLLATE DATABASE_DEFAULT, '-', '') from #TMP_2 S0 where S0.Fecha = Fecha)
		--and Fecha in (select S0.Fecha from #TMP_2 S0 where REPLACE(S0.Cuenta COLLATE DATABASE_DEFAULT, '-', '') like REPLACE(Cuenta COLLATE DATABASE_DEFAULT, '-', ''))
		order by Cuenta, Fecha, Operacion

	end
	else
	begin

		select 'En los registros a importar existen movimientos que cuentan con la misma fecha y la misma cuenta bancaria que algunos registros del detalle de una planilla abierta o cerrada. Sobreescribir dichos registros podria ocasionar una inconsistencia en los datos de las planillas registradas. Para sobreescribir los registros existentes en la tabla del Estado de Cuenta, cancele las planillas en donde existan registros que se pretende sobreescribir, elimine las lineas de detalle en donde se hace referencia a operaciones de dicha cuenta bancaria en dicha fecha o modifique el archivo Excel que desea importar.'

	end

end
ELSE
begin

	select 'No se obtuvo registros para la importacion'

end