
namespace UWay.Skynet.Cloud.Data.Schema.Loader
{
    class SqlCeSchemaLoader : SchemaLoader
    {
        protected override string AllColumnsSql
        {
            get
            {
                return
                @"SELECT DISTINCT 
                TABLE_NAME AS TableName
                , null AS [Schema]
                , 0 AS IsView, ORDINAL_POSITION AS [Order]
                , COLUMN_NAME AS ColumnName
                , DATA_TYPE AS ColumnType
                , CHARACTER_MAXIMUM_LENGTH AS Length
                , NUMERIC_PRECISION AS Precision
                , NUMERIC_SCALE AS Scale
                , AUTOINC_INCREMENT as IsGenerated
                , case when IS_NULLABLE ='YES' then 1 when is_nullable ='NO' then 0 end AS IsNullable
                , COLUMN_DEFAULT AS DefaultValue
                , DESCRIPTION AS Comment
                FROM      INFORMATION_SCHEMA.COLUMNS";
            }
        }

        protected override string AllConstraintsSql
        {
            get
            {
                return
                @"SELECT   TABLE_NAME AS TableName
                , COLUMN_NAME AS ColumnName
                , CONSTRAINT_NAME AS Name
                ,(CASE WHEN CONSTRAINT_NAME LIKE 'PK%' THEN 1 WHEN CONSTRAINT_NAME LIKE 'UQ%' THEN 2 WHEN CONSTRAINT_NAME
                 LIKE 'CK%' THEN 3 END) AS Type
                FROM      INFORMATION_SCHEMA.KEY_COLUMN_USAGE
                WHERE   (NOT (CONSTRAINT_NAME LIKE 'FK%'))
                ";
            }
        }

        protected override string AllFKsSql
        {
            get
            {
                return
                @"
                select 
                a.CONSTRAINT_NAME as Name,
                a.TABLE_NAME as ThisTableName,
                a.COLUMN_NAME as ThisKey,
                b.UNIQUE_CONSTRAINT_TABLE_NAME as OtherTableName,
                c.COLUMN_NAME as OtherKey
                from INFORMATION_SCHEMA.KEY_COLUMN_USAGE a
                inner join INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS b
                on a.CONSTRAINT_NAME = b.CONSTRAINT_NAME
                inner join INFORMATION_SCHEMA.KEY_COLUMN_USAGE c
                on b.UNIQUE_CONSTRAINT_NAME = c.CONSTRAINT_NAME
                where a.CONSTRAINT_NAME like 'FK%'
                ";
            }
        }
    }
}
