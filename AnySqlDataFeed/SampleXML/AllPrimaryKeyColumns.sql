
SELECT 
	 pkc.TABLE_SCHEMA
	,pkc.TABLE_NAME
	,pkc.COLUMN_NAME
	,pkc.CONSTRAINT_NAME
	,pkc.ORDINAL_POSITION
	,isc.DATA_TYPE
FROM V_DELDATA_PrimaryKeyColumns AS pkc

LEFT JOIN INFORMATION_SCHEMA.COLUMNS AS isc
	ON isc.TABLE_NAME = pkc.TABLE_NAME
	AND isc.COLUMN_NAME = pkc.COLUMN_NAME 

WHERE isc.DATA_TYPE NOT IN 
(
	'uniqueidentifier', 'int', 'bigint', 'varchar', 'nvarchar' --,'datetime'
)

AND table_name NOT IN ('sysdiagrams', 'dtproperties') 
