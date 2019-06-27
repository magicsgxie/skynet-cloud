using System.Collections.Generic;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Schema.Loader
{
    class MySqlSchemaLoader : SchemaLoader
    {
        string dbName = null;
        protected override void InitConnection(System.Data.Common.DbConnection conn)
        {
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            dbName = conn.Database;
        }


        protected override string AllColumnsSql
        {
            get
            {
                return @"
                            SELECT 
                                    table_name AS TableName,
                                    CASE WHEN (SELECT   COUNT(*) 
                                                FROM    INFORMATION_SCHEMA.VIEWS AS c 
                                                WHERE   c.table_name = a.table_name AND a.TABLE_SCHEMA = c.TABLE_SCHEMA) = 1 
                                         THEN  1 
                                         ELSE  0 END 
                                    AS IsView,
                                    ordinal_position AS `Order`,
                                    column_Name AS ColumnName,
                                    data_type AS ColumnType,
                                    CASE WHEN column_type LIKE '%blob%' OR column_type LIKE  '%text%'  THEN 0 ELSE character_octet_length END AS `Length`,
                                    NUMERIC_PRECISION AS `Precision`,
                                    NUMERIC_SCALE AS Scale,
                                    CASE WHEN extra = 'auto_increment' THEN 1 ELSE 0 END AS IsGenerated,
                                    CASE WHEN is_nullable = 'YES' THEN 1 ELSE 0 END AS `IsNullable`,
                                    column_default AS DefaultValue,
                                    column_comment AS `Comment`
                             FROM INFORMATION_SCHEMA.COLUMNS a
                             WHERE a.TABLE_SCHEMA = '" + dbName + "'";
            }
        }

        protected override string AllConstraintsSql
        {
            get
            {
                return @"
                            SELECT 
                                    a.table_name AS TableName
                                    ,a.constraint_name AS `Name`
                                    ,b.COLUMN_NAME AS ColumnName
                                    ,CASE WHEN CONSTRAINT_TYPE = 'PRIMARY KEY'  
                                            THEN 1 
                                            WHEN CONSTRAINT_TYPE ='UNIQUE' 
                                            THEN 2
                                            WHEN CONSTRAINT_TYPE ='CHECK'
                                            THEN 3
                                            END AS `TYPE`
                            FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS a 
                                 INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS b 
                                 ON a.CONSTRAINT_NAME = b.CONSTRAINT_NAME AND a.TABLE_SCHEMA = b.TABLE_SCHEMA 
                                 AND a.table_name=b.Table_Name
                            WHERE   (NOT (a.CONSTRAINT_NAME LIKE 'FK%')) 
                                    AND  a.TABLE_SCHEMA = '" + dbName + "'";
            }
        }

        protected override string AllFKsSql
        {
            get
            {
                return @"
                            SELECT 
                                    constraint_name AS `Name`
                                    , table_name AS ThisTableName
                                    ,column_name AS ThisKey
                                    ,REFERENCED_TABLE_NAME AS OtherTableName
                                    ,referenced_column_name AS OtherKey
                            FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
                            WHERE CONSTRAINT_NAME LIKE 'FK%'
                            AND  TABLE_SCHEMA = '" + dbName + "'";
            }
        }

        const string types = @"TypeName:BLOB,ProviderDbType:252,DataType:System.Byte[]
                                TypeName:TINYBLOB,ProviderDbType:249,DataType:System.Byte[]
                                TypeName:MEDIUMBLOB,ProviderDbType:250,DataType:System.Byte[]
                                TypeName:LONGBLOB,ProviderDbType:251,DataType:System.Byte[]
                                TypeName:BINARY,ProviderDbType:600,DataType:System.Byte[]
                                TypeName:VARBINARY,ProviderDbType:601,DataType:System.Byte[]

                                TypeName:DATE,ProviderDbType:10,DataType:System.DateTime
                                TypeName:DATETIME,ProviderDbType:12,DataType:System.DateTime
                                TypeName:TIMESTAMP,ProviderDbType:7,DataType:System.DateTime
                                TypeName:TIME,ProviderDbType:11,DataType:System.TimeSpan

                                TypeName:CHAR,ProviderDbType:254,DataType:System.String
                                TypeName:NCHAR,ProviderDbType:254,DataType:System.String
                                TypeName:VARCHAR,ProviderDbType:253,DataType:System.String
                                TypeName:NVARCHAR,ProviderDbType:253,DataType:System.String
                                TypeName:SET,ProviderDbType:248,DataType:System.String
                                TypeName:ENUM,ProviderDbType:247,DataType:System.String
                                TypeName:TINYTEXT,ProviderDbType:749,DataType:System.String
                                TypeName:TEXT,ProviderDbType:752,DataType:System.String
                                TypeName:MEDIUMTEXT,ProviderDbType:750,DataType:System.String
                                TypeName:LONGTEXT,ProviderDbType:751,DataType:System.String

                                TypeName:DOUBLE,ProviderDbType:5,DataType:System.Double
                                TypeName:FLOAT,ProviderDbType:4,DataType:System.Single
                                TypeName:TINYINT,ProviderDbType:1,DataType:System.SByte
                                TypeName:SMALLINT,ProviderDbType:2,DataType:System.Int16
                                TypeName:INT,ProviderDbType:3,DataType:System.Int32
                                TypeName:YEAR,ProviderDbType:13,DataType:System.Int32
                                TypeName:MEDIUMINT,ProviderDbType:9,DataType:System.Int32
                                TypeName:BIGINT,ProviderDbType:8,DataType:System.Int64
                                TypeName:DECIMAL,ProviderDbType:246,DataType:System.Decimal
                                TypeName:TINY INT,ProviderDbType:501,DataType:System.Byte
                                TypeName:SMALLINT,ProviderDbType:502,DataType:System.UInt16
                                TypeName:MEDIUMINT,ProviderDbType:509,DataType:System.UInt32
                                TypeName:INT,ProviderDbType:503,DataType:System.UInt32
                                TypeName:BIGINT,ProviderDbType:508,DataType:System.UInt64";

        protected override Dictionary<string, Common.TypeMappingInfo> TypeMappings
        {
            get
            {
                return TypeMapping.MySQLDbMap;
            }
        }
    }
}
