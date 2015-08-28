
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[___AllTypes]') AND type in (N'U'))
DROP TABLE [dbo].[___AllTypes]
GO



CREATE TABLE dbo.___AllTypes
(
	 mybigint bigint NOT NULL 
	,myfixedbinary binary(50) NULL 
	,mybit bit NULL 
	,myfixedchar char(10) NULL 
	,mydate date NULL 
	,mydatetime datetime NULL 
	,mydatetime2_7 datetime2(7) NULL 
	,mydatetimeoffset_7 datetimeoffset(7) NULL 
	,mydecimal_18_3 decimal(18, 3) NULL 
	,myfloat float NULL 
	,mygeography geography NULL 
	,mygeometry geometry NULL 
	,myhierarchyid hierarchyid NULL 
	,myimage image NULL 
	,myint int NULL 
	,mymoney money NULL 
	,myfixednchar nchar(10) NULL 
	,myntext ntext NULL 
	,mynumeric_18_3 numeric(18, 3) NULL 
	,mynvarchar nvarchar(50) NULL 
	,mynvarcharMAX nvarchar(max) NULL 
	,myreal real NULL 
	,mysmalldatetime smalldatetime NULL 
	,mysmallint smallint NULL 
	,mysmallmoney smallmoney NULL 
	,mysql_variant sql_variant NULL 
	,mytext text NULL 
	,mytime_7 time(7) NULL 
	,mytimestamp timestamp NULL 
	,mytinyint tinyint NULL 
	,myuniqueidentifier uniqueidentifier NULL 
	,myvarbinary varbinary(50) NULL 
	,myvarbinaryMAX varbinary(max) NULL 
	,myvarchar varchar(50) NULL 
	,myvarcharMAX varchar(max) NULL 
	,myxml xml NULL 
	 CONSTRAINT PK____AllTypes PRIMARY KEY ( mybigint )
); 
