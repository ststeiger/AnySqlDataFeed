
-- DECLARE @__in_table_name nvarchar(128)
-- SET @__in_table_name = N'___AllTypes' 


SELECT 
	--  table_schema
	-- ,table_name
	 column_name
	-- ,ordinal_position
	-- ,column_default
	-- ,is_nullable
	,data_type
	
	,CASE WHEN is_nullable = 'YES'
		THEN NULL 
		ELSE 'false' 
	END AS EntityNullable
	
	,CASE 
		WHEN DATA_TYPE IN ('bigint', 'bigserial') THEN 'Edm.Int64'
		WHEN DATA_TYPE = 'binary' THEN 'Edm.Binary'
		WHEN DATA_TYPE IN ('bit', 'boolean') THEN 'Edm.Boolean'
		WHEN data_type IN ('char', 'nchar','varchar', 'nvarchar','text', 'ntext', 'character varying', 'national character varying') THEN 'Edm.String' 
		WHEN DATA_TYPE = 'date' THEN 'Edm.DateTime'
		WHEN DATA_TYPE = 'smalldatetime' THEN 'Edm.DateTime'
		WHEN DATA_TYPE IN ('datetime') THEN 'Edm.DateTime'
		WHEN DATA_TYPE IN ('datetime2', 'timestamp without time zone') THEN 'Edm.DateTime'
		WHEN DATA_TYPE IN ('datetimeoffset', 'timestamp with time zone') THEN 'Edm.DateTimeOffset'
		WHEN DATA_TYPE = 'decimal' THEN 'Edm.Decimal'
		WHEN DATA_TYPE IN ('float', 'double', 'double precision') THEN 'Edm.Double'
		WHEN DATA_TYPE = 'image' THEN 'Edm.Binary'
		WHEN DATA_TYPE IN ('int', 'integer', 'serial') THEN 'Edm.Int32'
		WHEN DATA_TYPE = 'money' THEN 'Edm.Decimal'
		WHEN DATA_TYPE = 'numeric' THEN 'Edm.Decimal'
		WHEN DATA_TYPE = 'real' THEN 'Edm.Single'
		WHEN DATA_TYPE = 'smallint' THEN 'Edm.Int16'
		WHEN DATA_TYPE = 'smallmoney' THEN 'Edm.Decimal'
		WHEN DATA_TYPE IN ('time', 'time with timezone', 'time without timezone') THEN 'Edm.Time'
		WHEN DATA_TYPE = 'timestamp' THEN 'Edm.Binary'
		WHEN DATA_TYPE = 'tinyint' THEN 'Edm.Byte'
		WHEN DATA_TYPE IN ('uniqueidentifier', 'uuid') THEN 'Edm.Guid'
		WHEN DATA_TYPE IN ('varbinary', 'bytea') THEN 'Edm.Binary'
		WHEN DATA_TYPE = 'xml' THEN 'Edm.String'
		
		ELSE 'n.def.'
	END AS EntityType 
	
	,CASE 
		WHEN data_type IN ('image', 'xml') THEN 'Max' 
		WHEN data_type IN ('char', 'nchar','varchar', 'nvarchar','text', 'ntext', 'character varying', 'national character varying')
			 AND (character_maximum_length > 100000 OR character_maximum_length < 0 OR DATA_TYPE IN ('text', 'ntext'))
				THEN 'Max' 
		WHEN data_type IN ('char', 'nchar','varchar', 'nvarchar','text', 'ntext', 'character varying', 'national character varying')
			THEN CAST(character_maximum_length AS varchar(30)) 
		WHEN data_type IN ('binary', 'varbinary', 'bytea')
			THEN 
				CASE 
					WHEN character_octet_length < 0 THEN 'Max' 
					ELSE CAST(character_octet_length AS varchar(30)) 
				END 
		ELSE NULL 
	END AS EntityMaxLength
	
	,
	CASE 
		WHEN data_type IN ('image', 'xml') THEN 'false' 
		WHEN data_type IN ('varchar', 'nvarchar','text', 'ntext', 'varbinary', 'bytea', 'xml', 'character varying', 'national character varying') THEN 'false' 
		WHEN data_type IN ('char', 'nchar', 'binary') THEN 'true' 
		ELSE NULL 
	END AS EntityFixedLength
	
	,CASE 
		
		WHEN EXISTS(SELECT * FROM information_schema.tables WHERE table_schema = 'pg_catalog') AND data_type IN ('character varying', 'national character varying', 'text') THEN 'true' -- PostGre uses UTF-8 always
		WHEN data_type IN ('nchar', 'nvarchar', 'ntext', 'xml') THEN 'true' 
		WHEN data_type IN ('char', 'varchar', 'text') THEN 'false'
		ELSE NULL 
	END AS EntityUnicode 
	
	,
	CASE 
		WHEN data_type IN ('bigint', 'biginteger', 'bigserial', 'int', 'integer', 'serial', 'smallint', 'tinyint') THEN NULL 
		WHEN datetime_precision IS NOT NULL THEN datetime_precision 
		ELSE numeric_precision 
	END AS EntityPrecision 
	
	,
	CASE 
		WHEN data_type IN ('bigint', 'biginteger', 'bigseial', 'int', 'integer', 'serial', 'smallint', 'tinyint') THEN NULL 
		ELSE numeric_scale 
	END AS EntityScale
	
	-- ,character_maximum_length
	-- ,character_octet_length
	-- ,numeric_precision
	-- ,numeric_precision_radix

	-- ,character_set_catalog
	-- ,character_set_schema
	-- ,character_set_name
	-- ,collation_catalog
	-- ,collation_schema
	-- ,collation_name
	-- ,*
FROM INFORMATION_SCHEMA.COLUMNS 

WHERE (1=1) 
-- AND TABLE_NAME = @__in_table_name 
--AND TABLE_NAME = 't_blogpost' 
AND table_name NOT IN ('sysdiagrams', 'dtproperties') 
AND table_schema NOT IN ('pg_catalog', 'information_schema') 
ORDER BY ORDINAL_POSITION 
