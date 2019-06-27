using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Schema.Loader
{
    //class OledbSchemaLoader : SchemaLoader
    //{

    //    public override IDatabaseSchema Load(DbConfiguration cfg)
    //    {
    //        var databaseSchema = new DatabaseSchema();
    //        DataTable allColumns = null;
    //        DataTable pkTable = null;
    //        DataTable fkTable = null;
    //        DataTable viewTable = null;
    //        //using (var conn = new OleDbConnection(cfg.ConnectionString))
    //        //{
    //        //    conn.Open();
    //        //    allColumns = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, null);
    //        //    pkTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys, null);
    //        //    fkTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, null);
    //        //    viewTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Views, null);
    //        //}

    //        Dictionary<string, TableSchema> tables = new Dictionary<string, TableSchema>();

    //        foreach (var item in allColumns.Rows.Cast<DataRow>())
    //        {
    //            var tableName = item["TABLE_NAME"] as string;
    //            var tableSchema = item["TABLE_SCHEMA"] as string;
    //            var tableCatalog = item["TABLE_CATALOG"] as string;

    //            var c = new ColumnSchema();
    //            c.ColumnName = item["COLUMN_NAME"] as string;
    //            c.Order = Convert.ToInt32(item["ORDINAL_POSITION"]);
    //            if (string.IsNullOrEmpty(item["NUMERIC_PRECISION"] as string))
    //                c.Precision = 0;
    //            else
    //                c.Precision = (int)item["NUMERIC_PRECISION"];
    //            if (string.IsNullOrEmpty(item["NUMERIC_SCALE"] as string))
    //                c.Scale = 0;
    //            else
    //                c.Scale = (int)(item["NUMERIC_SCALE"]);
    //            if (string.IsNullOrEmpty(item["CHARACTER_MAXIMUM_LENGTH"] as string))
    //                c.Length = 0;
    //            else
    //                c.Length = (int)item["CHARACTER_MAXIMUM_LENGTH"];
    //            c.DefaultValue = item["COLUMN_DEFAULT"] as string;
    //            c.Comment = item["DESCRIPTION"] as string;
    //            c.DbType = ParseDbType((int)item["DATA_TYPE"]);
    //            c.Type = ParseType(c.DbType);
    //            foreach (var p in pkTable.Rows.Cast<DataRow>())
    //            {
    //                if (tableName == p["TABLE_NAME"] as string && c.ColumnName == p["COLUMN_NAME"] as string)
    //                {
    //                    c.IsPrimaryKey = true;
    //                    break;
    //                }
    //            }

    //            if (!item.IsNull("IS_NULLABLE"))
    //                c.IsNullable = (bool)item["IS_NULLABLE"];

    //            TableSchema table = null;
    //            string key = string.Concat(tableCatalog, tableSchema, tableName);

    //            if (!tables.TryGetValue(key, out table))
    //            {
    //                table = new TableSchema { TableName = tableName, Schema = tableSchema };
    //                tables[tableName] = table;
    //            }

    //            table.AddColumn(c);
    //        }

    //        foreach (var item in fkTable.Rows.Cast<DataRow>())
    //        {
    //            var tableSchema = item["PK_TABLE_SCHEMA"] as string;
    //            var tableCatalog = item["PK_TABLE_CATALOG"] as string;

    //            TableSchema thisTable = null;
    //            TableSchema otherTable = null;
    //            IColumnSchema thisKey = null;
    //            IColumnSchema otherKey = null;
    //            var key = string.Concat(tableCatalog, tableSchema, item["FK_TABLE_NAME"] as string);
    //            if (tables.TryGetValue(key, out thisTable))
    //                thisKey = thisTable.AllColumns.FirstOrDefault(p => p.ColumnName == item["FK_COLUMN_NAME"] as string);

    //            key = string.Concat(tableCatalog, tableSchema, item["PK_TABLE_NAME"] as string);
    //            if (tables.TryGetValue(key, out otherTable))
    //                otherKey = otherTable.AllColumns.FirstOrDefault(p => p.ColumnName == item["PK_COLUMN_NAME"] as string);

    //            thisTable.AddFK(new ForeignKeySchema
    //            {
    //                ThisTable = thisTable,
    //                Name = item["FK_NAME"] as string,
    //                ThisKey = thisKey,
    //                OtherTable = otherTable,
    //                OtherKey = otherKey
    //            });


    //        }

    //        foreach (var item in viewTable.Rows.Cast<DataRow>())
    //        {
    //            var tableName = item["TABLE_NAME"] as string;
    //            var tableSchema = item["TABLE_SCHEMA"] as string;
    //            var tableCatalog = item["TABLE_CATALOG"] as string;
    //            TableSchema table = null;
    //            var key = string.Concat(tableCatalog, tableSchema, tableName);
    //            if (tables.TryGetValue(key, out table))
    //                table.IsView = true;
    //        }

    //        databaseSchema.Tables = tables.Values.Where(p => !p.IsView).ToArray();
    //        databaseSchema.Views = tables.Values.Where(p => p.IsView).ToArray();

    //        //allColumns.WriteXmlSchema(Console.Out);
    //        //pkTable.WriteXmlSchema(Console.Out);
    //        //fkTable.WriteXmlSchema(Console.Out);
    //        //viewTable.WriteXmlSchema(Console.Out);
    //        return databaseSchema;
    //    }


    //    protected DBType ParseDbType(int strColumnType)
    //    {
    //        TypeMappingInfo item;
    //        if (TypeMapping.OleDbMap.TryGetValue(strColumnType, out item))
    //            return item.DbType;
    //        return DBType.Unkonw;
    //    }

    //    protected override Type ParseType(DBType dbType)
    //    {
    //        return TypeMapping.OleDbMap.Values.FirstOrDefault(p => p.DbType == dbType).CLRType;
    //    }

    //    protected override string AllColumnsSql
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    protected override string AllConstraintsSql
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    protected override string AllFKsSql
    //    {
    //        get { throw new NotImplementedException(); }
    //    }
    //}
}
