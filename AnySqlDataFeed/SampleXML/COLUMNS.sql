
SELECT * FROM information_schema.columns 
WHERE (1=1) 
AND table_name LIKE 'T[_]%' 
AND data_type NOT IN 
( 
	 'uniqueidentifier', 'tinyint', 'smallint', 'int', 'bigint' 
	,'varchar', 'nvarchar', 'text', 'ntext', 'char', 'nchar' 
	,'bit', 'float', 'decimal' 
	,'datetime' --, 'date' 
	,'image', 'varbinary' 
) 
