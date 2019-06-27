
namespace UWay.Skynet.Cloud.Data.Schema.Loader
{
    class SqlServerSchemaLoader : SchemaLoader
    {
        protected override string AllColumnsSql
        {
            get
            {
                return @"SELECT
                                DISTINCT TableName=d.Name,
                                DataBaseName=table_catalog,
                                [Schema] = TABLE_SCHEMA,
                                IsView = case when (d.xtype='V') then 1 else 0 end,
	                            [Order]=a.colorder,
	                            ColumnName=a.Name,
	                            ColumnType=b.Name,
	                            Length=a.Length,
	                            [Precision]=COLUMNPROPERTY(a.id,a.name,'PRECISION'),
	                            Scale=isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0),
	                            IsGenerated=case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then 1 else 0 end,
	                            [IsNullable]=a.isnullable,
	                            DefaultValue=isnull(e.text,'')
                                --,Comment=isnull(g.[value],'')
                        FROM 
	                            INFORMATION_SCHEMA.TABLE_CONSTRAINTS ,SYSCOLUMNS a
	                            LEFT JOIN SYSTYPES b ON a.XUSERTYPE=b.XUSERTYPE 
	                            INNER JOIN SYSOBJECTS d ON a.ID=d.ID AND (d.XTYPE='U' OR d.XTYPE='V') AND  d.NAME<>'dtproperties' 
	                            LEFT JOIN SYSCOMMENTS e ON a.CDEFAULT=e.ID 
                                --left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id 
                        WHERE d.Name <>'syssegments' and d.Name <>'sysconstraints' and d.name <>'sysdiagrams'
                                "
                        ;
            }
        }

        protected override string AllConstraintsSql
        {
            get
            {
                return @"SELECT 
                                [Schema] = a.CONSTRAINT_SCHEMA, 
                                TableName = a.TABLE_NAME,
                                ColumnName = b.COLUMN_NAME,
                                Name = a.CONSTRAINT_NAME
                                ,Type = CASE WHEN CONSTRAINT_TYPE = 'PRIMARY KEY'  
                                            THEN 1 
                                            WHEN CONSTRAINT_TYPE ='UNIQUE' 
                                            THEN 2
                                            WHEN CONSTRAINT_TYPE ='CHECK'
                                            THEN 3
                                            END
                        FROM 
                                INFORMATION_SCHEMA.TABLE_CONSTRAINTS a 
                                INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE b 
                                        ON a.CONSTRAINT_NAME = b.CONSTRAINT_NAME
                        where CONSTRAINT_TYPE <>'FOREIGN KEY'"
                       ;
            }
        }

        protected override string AllFKsSql
        {
            get
            {
                return @"SELECT
                                Name = CU.CONSTRAINT_NAME,
                                ThisSchema = FK.table_schema,
                                ThisTableName  = FK.TABLE_NAME,
                                ThisKey = CU.COLUMN_NAME,
                                OtherSchema = pk.table_schema,
                                OtherTableName  = PK.TABLE_NAME,
                                OtherKey = PT.COLUMN_NAME
                        FROM 
                                INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C
                                INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK 
                                        ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME
                                INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK 
                                        ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME
                                INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU 
                                        ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME
                                INNER JOIN
                                        (	
                                            SELECT i1.TABLE_NAME, i2.COLUMN_NAME
                                            FROM  INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1
                                            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME
                                            WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY'
                                        ) 
                                PT ON PT.TABLE_NAME = PK.TABLE_NAME"
                        ;
            }
        }


    }
}
