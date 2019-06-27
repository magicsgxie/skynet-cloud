using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UWay.Skynet.Cloud.Data.Schema
{
    class DatabaseSchema : IDatabaseSchema
    {
        public ITableSchema[] Tables { get; internal set; }
        public ITableSchema[] Views { get; internal set; }
    }

    [DebuggerDisplay("Schema={Schema},TableName={TableName},IsView={IsView}")]
    class TableSchema : ITableSchema
    {
        public bool IsView { get; set; }

        private List<IColumnSchema> columns = new List<IColumnSchema>();
        private List<IForeignKeySchema> fks = new List<IForeignKeySchema>();

        private List<IRelationSchema> children = new List<IRelationSchema>();

        public void AddColumn(IColumnSchema column)
        {
            columns.Add(column);
        }

        public void AddFK(ForeignKeySchema fk)
        {
            fks.Add(fk);
            (fk.OtherTable as TableSchema).children.Add(new ForeignKeySchema { OtherTable = this, OtherKey = fk.ThisKey, ThisTable = fk.OtherTable, ThisKey = fk.OtherKey });
        }

        public string Schema { get; set; }
        public string TableName { get; set; }
        public IColumnSchema[] AllColumns { get { return columns.ToArray(); } }
        public IColumnSchema[] Columns { get { return columns.Where(p => !p.IsPrimaryKey).ToArray(); } }
        public IForeignKeySchema[] ForeignKeys { get { return fks.ToArray(); } }

        public IColumnSchema[] PrimaryKeys { get { return columns.Where(p => p.IsPrimaryKey).ToArray(); } }
        public IRelationSchema[] Children { get { return children.OfType<IRelationSchema>().ToArray(); } }
    }

    [DebuggerDisplay("ColumnName={ColumnName},Type={Type.Name},IsPrimaryKey={IsPrimaryKey},IsGenerated={IsGenerated}")]
    class ColumnSchema : IColumnSchema
    {
        public string ColumnName { get; set; }
        public ITableSchema Table { get; set; }
        public bool IsUniqule { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsNullable { get; set; }

        public bool IsComputed { get; set; }
        public bool IsGenerated { get; set; }
        public string ColumnType { get; set; }
        public Type Type { get; set; }
        public DBType DbType { get; set; }

        public string DefaultValue { get; set; }
        public int Length { get; set; }
        public int Precision { get; set; }
        public int Scale { get; set; }
        public int Order { get; set; }

        public string Comment { get; set; }
    }

    [DebuggerDisplay("Name={Name},ThisTable={ThisTable.TableName},ThisKey={ThisKey.ColumnName},OtherTable={OtherTable.TableName},OtherKey={OtherKey.ColumnName}")]
    class ForeignKeySchema : IForeignKeySchema
    {
        public string Name { get; internal set; }
        public ITableSchema ThisTable { get; internal set; }
        public IColumnSchema ThisKey { get; internal set; }
        public ITableSchema OtherTable { get; internal set; }
        public IColumnSchema OtherKey { get; internal set; }
    }


}
