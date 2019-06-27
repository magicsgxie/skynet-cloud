using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UWay.Skynet.Cloud.Data.Common;
namespace UWay.Skynet.Cloud.Data.Schema.Loader
{
    class OracleSchemaLoader : SchemaLoader
    {

        const string AllColumnsSqlFormat = @"SELECT 
       t.TABLE_NAME as TableName, 
       t.COLUMN_NAME as ColumnName, 
       t.COLUMN_ID AS {0}Order{1}, 
       t.DATA_TYPE AS ColumnType,
       t.DATA_LENGTH AS Length, 
       t.DATA_PRECISION AS Precision, 
       t.DATA_SCALE AS Scale, 
       case when t.NULLABLE ='Y' then 1 else 0 end as IsNullable,
       t.data_default AS DefaultValue
       
FROM ALL_TAB_COLUMNS t 
where t.OWNER ='{2}'     
ORDER BY t.OWNER, t.TABLE_NAME";

        const string AllFKsSqlFormat = @"SELECT 
       tl.CONSTRAINT_NAME as Name, 
       t.TABLE_NAME as ThisTableName, 
       c.column_name as ThisKey, 
       tl.table_name as OtherTableName,
       cl.column_name as OtherKey,
       t.constraint_type as Type
FROM ALL_CONSTRAINTS t
     inner join sys.all_cons_columns c
     on t.owner = c.owner and t.constraint_name = c.constraint_name and t.table_name = c.table_name
     left join ALL_CONSTRAINTS tl 
     on t.owner = tl.owner and t.constraint_name = tl.r_constraint_name
     left join sys.all_cons_columns cl
     on tl.owner = cl.owner and tl.constraint_name = cl.constraint_name and tl.table_name = cl.table_name
where t.Owner = '{0}' and t.constraint_type <>'C' 
order by t.table_name";

        string dbName = null;
        protected override void InitConnection(System.Data.Common.DbConnection conn)
        {
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            var connectionStringBuilder = new DbConnectionStringBuilder();
            connectionStringBuilder.ConnectionString = conn.ConnectionString;
            dbName = GetDatabaseName(connectionStringBuilder);
        }

        public override IDatabaseSchema Load(DbConfiguration cfg)
        {
            var databaseSchema = new DatabaseSchema();

            ColumnInfo[] allColumns = null;

            ForeignKeyInfo[] allFks = null;
            using (var ctx = cfg.CreateDbContext())
            {
                InitConnection(ctx.Connection);
                using (var reader = ctx.DbHelper.ExecuteReader(AllColumnsSql))
                    allColumns = reader.ToList<ColumnInfo>().ToArray();

                using (var reader = ctx.DbHelper.ExecuteReader(AllFKsSql))
                    allFks = reader.ToList<ForeignKeyInfo>().ToArray();
            }

            Dictionary<string, TableSchema> tables = new Dictionary<string, TableSchema>();

            foreach (var c in allColumns)
            {
                TableSchema table = null;
                if (!tables.TryGetValue(c.TableName, out table))
                {
                    table = new TableSchema { TableName = c.TableName, IsView = c.IsView };
                    tables[c.TableName] = table;
                }

                var key = allFks.FirstOrDefault(p => p.Type == "P"
                    && p.ThisTableName == c.TableName
                    && p.ThisKey == c.ColumnName);
                c.IsPrimaryKey = key != null;



                var column = ToColumn(c);
                table.AddColumn(column);
            }

            foreach (var item in allFks.Where(p => p.OtherTableName.HasValue()))
            {
                TableSchema thisTable = tables[item.OtherTableName];
                TableSchema otherTable = tables[item.ThisTableName];
                IColumnSchema thisKey = thisTable.AllColumns.FirstOrDefault(p => p.ColumnName == item.OtherKey);
                IColumnSchema otherKey = otherTable.AllColumns.FirstOrDefault(p => p.ColumnName == item.ThisKey);

                thisTable.AddFK(new ForeignKeySchema
                {
                    ThisTable = thisTable,
                    Name = item.Name,
                    ThisKey = thisKey,
                    OtherTable = otherTable,
                    OtherKey = otherKey
                });

            }

            databaseSchema.Tables = tables.Values.Where(p => !p.IsView).ToArray();
            databaseSchema.Views = tables.Values.Where(p => p.IsView).ToArray();

            return databaseSchema;
        }

        protected override string AllColumnsSql
        {
            get { return string.Format(AllColumnsSqlFormat, "\"", "\"", dbName); }
        }

        protected override string AllConstraintsSql
        {
            get { throw new NotImplementedException(); }
        }

        protected override string AllFKsSql
        {
            get { return string.Format(AllFKsSqlFormat, dbName); }
        }

        internal static string GetDatabaseName(System.Data.Common.DbConnectionStringBuilder connectionStringBuilder)
        {
            object objDbName;
            string dbName = null;
            if (connectionStringBuilder.TryGetValue("USER ID", out objDbName))
                dbName = objDbName.ToString();
            else if (connectionStringBuilder.TryGetValue("UID", out objDbName))
                dbName = objDbName.ToString();
            return dbName.ToUpper();
        }

        protected override Dictionary<string, Common.TypeMappingInfo> TypeMappings
        {
            get
            {
                return TypeMapping.OracleDbMap;
            }
        }
    }
}
