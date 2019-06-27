using System.Linq;
using System.Text;
namespace UWay.Skynet.Cloud.Data.Schema.Script.Generator
{
    class SqlCeScriptGenerator : DatabaseScriptGenerator
    {
        protected override string BuildPKScript(Mapping.IMemberMapping[] members)
        {
            var mapping = members[0].Entity;
            var tableName = GetTableName(mapping);

            var sb = new StringBuilder(512);
            if (mapping.PrimaryKeys != null && mapping.PrimaryKeys.Length > 0)
            {
                sb.Append("ALTER TABLE ")
                    .Append(tableName)
                     .AppendLine()
                    .Append("\tADD CONSTRAINT ")

                    .Append(" PK_" + mapping.TableName.Replace(" ", "_"))
                    .Append("  PRIMARY KEY (")
                    .Append(mapping.PrimaryKeys.Select(p => Dialect.Quote(p.ColumnName)).ToCSV(","))
                    .Append("); ")
                    .AppendLine("")
                    ;
            }
            return sb.ToString();
        }

        protected override void RegisterColumnTypes()
        {
            base.RegisterColumnTypes();

            RegisterColumnType(DBType.Binary, "VARBINARY(8000)");
            RegisterColumnType(DBType.Binary, 8000, "VARBINARY($l)");
            RegisterColumnType(DBType.Binary, 2147483647, "IMAGE");
            RegisterColumnType(DBType.Boolean, "BIT");
            RegisterColumnType(DBType.Byte, "TINYINT");


            RegisterColumnType(DBType.Char, "CHAR(255)");
            RegisterColumnType(DBType.Char, 8000, "CHAR($l)");
            RegisterColumnType(DBType.Currency, "MONEY");

            RegisterColumnType(DBType.DateTime, "DATETIME");
            RegisterColumnType(DBType.Decimal, "DECIMAL(19,5)");
            RegisterColumnType(DBType.Decimal, 19, "DECIMAL($pair, $s)");
            RegisterColumnType(DBType.Double, "REAL"); //synonym for FLOAT(53)
            RegisterColumnType(DBType.Guid, "UNIQUEIDENTIFIER");


            RegisterColumnType(DBType.Image, "IMAGE");
            RegisterColumnType(DBType.Int16, "SMALLINT");
            RegisterColumnType(DBType.Int32, "INT");
            RegisterColumnType(DBType.Int64, "BIGINT");

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
