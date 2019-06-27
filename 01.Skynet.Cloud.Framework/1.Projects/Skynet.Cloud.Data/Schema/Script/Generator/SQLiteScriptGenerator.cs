using System.Linq;
using System.Text;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Mapping;
namespace UWay.Skynet.Cloud.Data.Schema.Script.Generator
{
    class SQLiteScriptGenerator : DatabaseScriptGenerator
    {
        protected override string BuildPKScript(Mapping.IMemberMapping[] members)
        {
            return null;
        }
        protected override string BuildFKScript(Mapping.IMemberMapping member)
        {
            return null;
        }
        private string CreatePKScript(Mapping.IMemberMapping[] members)
        {
            var mapping = members[0].Entity;
            var tableName = GetTableName(mapping);

            var sb = new StringBuilder(512);
            if (mapping.PrimaryKeys != null && mapping.PrimaryKeys.Length > 0)
            {
                sb.Append(",primary key( ")
                    .Append(mapping.PrimaryKeys.Select(p => Dialect.Quote(p.ColumnName)).ToCSV(","))
                    .Append(") ")
                    .AppendLine("")
                    ;
            }
            return sb.ToString();
        }

        private string CreateFKScript(Mapping.IEntityMapping mapping)
        {
            var fks = mapping.Members.Where(p => p.IsManyToOne).ToArray();
            if (fks.Length == 0)
                return null;

            var sb = new StringBuilder(512);
            foreach (var fk in fks)
                sb.Append(string.Concat(",FOREIGN KEY ("
                    , Dialect.Quote(fk.ThisKeyMembers.FirstOrDefault().ColumnName)
                    , ") REFERENCES "
                    , GetTableName(fk.RelatedEntity)
                    , "("
                    , Dialect.Quote(fk.OtherKeyMembers.FirstOrDefault().ColumnName)
                    , ")"));
            return sb.ToString();
        }

        protected override string BuildTableScript(Mapping.IEntityMapping mapping)
        {
            Guard.NotNull(mapping, "mapping");

            var tableName = GetTableName(mapping);

            var sb = new StringBuilder(512);
            sb.Append("CREATE TABLE ").Append(tableName).Append("(");

            int num = 0;
            foreach (var f in mapping.Members.Where(p => !p.IsRelationship && !p.IsComputed))
            {
                sb.AppendLine("\t");
                if (num > 0)
                    sb.Append(",");
                sb.Append(Dialect.Quote(f.ColumnName));

                var sqlType = f.SqlType;
                sb.Append(" ").Append(GetDbType(f.SqlType));

                if (sqlType.Required && !f.IsPrimaryKey)
                    sb.Append(" NOT NULL");
                if (f.IsPrimaryKey && mapping.PrimaryKeys.Length == 1)
                    sb.Append(" PRIMARY KEY");

                if (f.IsGenerated)
                    sb.Append(GetDefaultValue(f, sqlType));
                num++;
            }
            if (mapping.PrimaryKeys.Count() > 1)
            {
                sb.AppendLine("\t").Append(CreatePKScript(mapping.Members))
              ;
            }

            var fks = mapping.Members.Where(p => p.IsManyToOne).ToArray();
            foreach (var fk in fks)
                sb.AppendLine(string.Concat("\t,FOREIGN KEY ("
                    , Dialect.Quote(fk.ThisKeyMembers.FirstOrDefault().ColumnName)
                    , ") REFERENCES "
                    , GetTableName(fk.RelatedEntity)
                    , "("
                    , Dialect.Quote(fk.OtherKeyMembers.FirstOrDefault().ColumnName)
                    , ")"));

            sb.AppendLine()
                    .Append(");");
            sb.AppendLine("");


            return sb.ToString();
        }

        protected override string GetDefaultValue(IMemberMapping f, SqlType sqlType)
        {
            switch (sqlType.DbType)
            {
                case DBType.DateTime:
                    break;
                case DBType.Guid:
                    return " DEFAULT NEWID()";
                case DBType.Int16:
                case DBType.Int32:
                case DBType.Int64:
                    if (f.IsPrimaryKey)
                        return " AUTOINCREMENT";
                    break;
            }
            return null;
        }

        protected override void RegisterColumnTypes()
        {
            RegisterColumnType(DBType.Boolean, "BIT");
            RegisterColumnType(DBType.Byte, "TINYINT");
            RegisterColumnType(DBType.Binary, "IMAGE");
            RegisterColumnType(DBType.Binary, 2147483647, "IMAGE");
            // RegisterColumnType(DBType.Binary, "VARBINARY(8000)");
            RegisterColumnType(DBType.Binary, 8000, "VARBINARY($l)");

            RegisterColumnType(DBType.Char, "CHAR(255)");
            RegisterColumnType(DBType.Char, 8000, "CHAR($l)");
            RegisterColumnType(DBType.Currency, "MONEY");

            RegisterColumnType(DBType.DateTime, "DATETIME");
            RegisterColumnType(DBType.Decimal, "DECIMAL(19,5)");
            RegisterColumnType(DBType.Decimal, 19, "DECIMAL($pair, $s)");
            RegisterColumnType(DBType.Double, "DOUBLE PRECISION"); //synonym for FLOAT(53)
            RegisterColumnType(DBType.Guid, "UNIQUEIDENTIFIER");


            RegisterColumnType(DBType.Image, "IMAGE");
            RegisterColumnType(DBType.Int16, "INTEGER");
            RegisterColumnType(DBType.Int32, "INTEGER");
            RegisterColumnType(DBType.Int64, "INTEGER");

            RegisterColumnType(DBType.NChar, "NCHAR(255)");
            RegisterColumnType(DBType.NChar, 4000, "NCHAR($l)");
            RegisterColumnType(DBType.NText, "NTEXT");
            RegisterColumnType(DBType.NVarChar, "NVARCHAR(255)");
            RegisterColumnType(DBType.NVarChar, 4000, "NVARCHAR($l)");
            RegisterColumnType(DBType.NVarChar, 1073741823, "NTEXT");
            RegisterColumnType(DBType.Single, "REAL");

            RegisterColumnType(DBType.Text, "Text");
            //RegisterColumnType(DBType.Timestamp, "Timestamp");

            RegisterColumnType(DBType.VarChar, "VARCHAR(255)");
            RegisterColumnType(DBType.VarChar, 8000, "VARCHAR($l)");
            RegisterColumnType(DBType.VarChar, 2147483647, "TEXT");
        }


    }
}
