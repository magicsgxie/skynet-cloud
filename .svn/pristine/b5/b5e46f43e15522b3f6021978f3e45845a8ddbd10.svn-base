using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Schema.Loader
{
    class SQLiteSchemaLoader : SchemaLoader
    {
        static Func GetSchemaTableMethod = null;
        static readonly object Mutext = new object();

        public override IDatabaseSchema Load(DbConfiguration cfg)
        {
            var databaseSchema = new DatabaseSchema();

            Dictionary<string, List<ForeignKeyInfo>> allFks = new Dictionary<string, List<ForeignKeyInfo>>();
            Dictionary<string, DataTable> allColumns = new Dictionary<string, DataTable>();
            var tables = LoadData(cfg, allFks, allColumns);

            foreach (var tb in tables)
            {
                foreach (var row in allColumns[tb.TableName].Rows.Cast<DataRow>())
                {
                    var typeName = row["DataTypeName"] as string;
                    if (string.IsNullOrEmpty(typeName))
                        continue;
                    var type = row["DataType"] as Type;
                    var dbType = ParseDbType(typeName);

                    int precision, scale;
                    if (string.IsNullOrEmpty(row["NumericPrecision"] as string))
                        precision = 0;
                    else
                        precision = (int)row["NumericPrecision"];

                    if (string.IsNullOrEmpty(row["NumericScale"] as string))
                        scale = 0;
                    else
                        scale = (int)row["NumericScale"];

                    var c = new ColumnSchema
                    {
                        ColumnName = row["ColumnName"] as string,
                        IsPrimaryKey = (bool)row["IsKey"],
                        IsGenerated = (bool)row["IsAutoIncrement"],
                        Table = tb,
                        IsUniqule = (bool)row["IsUnique"],
                        //Comment = row["DESCRIPTION"] as string,
                        DefaultValue = row["DefaultValue"] as string,
                        Order = (int)row["ColumnOrdinal"],
                        Precision = precision,
                        Scale = scale,
                        IsNullable = (bool)row["AllowDBNull"],
                        DbType = dbType,
                        Type = type,
                    };
                    tb.AddColumn(c);
                }
            }

            foreach (var tbName in allFks.Keys)
            {
                var items = allFks[tbName];
                var tb = tables.FirstOrDefault(p => p.TableName == tbName);
                foreach (var item in items)
                {
                    var otherTable = tables.FirstOrDefault(p => p.TableName == item.OtherTableName);

                    tb.AddFK(new ForeignKeySchema
                    {
                        ThisTable = tb,
                        ThisKey = tb.AllColumns.FirstOrDefault(p => p.ColumnName == item.ThisKey),
                        OtherTable = otherTable,
                        OtherKey = otherTable.AllColumns.FirstOrDefault(p => p.ColumnName == item.OtherKey),
                    });
                }
            }

            databaseSchema.Tables = tables.Where(p => !p.IsView).ToArray();
            databaseSchema.Views = tables.Where(p => p.IsView).ToArray();
            return databaseSchema;
        }


        private static TableSchema[] LoadData(DbConfiguration cfg, Dictionary<string, List<ForeignKeyInfo>> allFks, Dictionary<string, DataTable> allColumns)
        {
            TableSchema[] tables = null;
            using (var conn = cfg.DbProviderFactory.CreateConnection())
            {
                const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

                if (GetSchemaTableMethod == null)
                    lock (Mutext)
                        GetSchemaTableMethod = conn.GetType().Module
                            .GetType("System.Data.SQLite.SQLiteDataReader")
                            .GetMethod("GetSchemaTable", flags)
                            .GetFunc();

                conn.ConnectionString = cfg.ConnectionString;
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
case when  type LIKE 'view' then 1 else 0 end as Isview
, name as TableName FROM main.sqlite_master 
WHERE ([type] LIKE 'table' OR [type] LIKE 'view') and [name] not like  'sqlite%'";

                    using (var reader = cmd.ExecuteReader())
                        tables = reader.ToList<TableSchema>().ToArray();

                    foreach (var t in tables)
                    {

                        cmd.CommandText = string.Format("select * from main.[{0}]", t.TableName);
                        using (var reader = cmd.ExecuteReader(System.Data.CommandBehavior.SchemaOnly))
                            allColumns.Add(t.TableName, GetSchemaTableMethod(reader, true, true) as DataTable);
                        cmd.CommandText = string.Format("PRAGMA main.foreign_key_list([{0}])", t.TableName);
                        using (var reader = cmd.ExecuteReader())
                        {
                            var fks = new List<ForeignKeyInfo>();
                            allFks.Add(t.TableName, fks);
                            while (reader.Read())
                                fks.Add(new ForeignKeyInfo
                                {
                                    ThisTableName = t.TableName,
                                    ThisKey = reader.GetString(3),
                                    OtherTableName = reader.GetString(2),
                                    OtherKey = reader.GetString(4),
                                });
                        }
                    }
                }
            }
            return tables;
        }

        protected override Dictionary<string, TypeMappingInfo> TypeMappings
        {
            get
            {
                return TypeMapping.SQLiteDbMap;
            }
        }

        protected override string AllColumnsSql
        {
            get { throw new NotImplementedException(); }
        }

        protected override string AllConstraintsSql
        {
            get { throw new NotImplementedException(); }
        }

        protected override string AllFKsSql
        {
            get { throw new NotImplementedException(); }
        }

    }
}
