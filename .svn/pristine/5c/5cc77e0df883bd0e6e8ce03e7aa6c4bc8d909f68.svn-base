using System.Linq;
using System.Text;
namespace UWay.Skynet.Cloud.Data.Schema.Script.Generator
{
    class AccessScriptGenerator : DatabaseScriptGenerator
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
            RegisterColumnType(DBType.Binary, "IMAGE");
            RegisterColumnType(DBType.Binary, 2147483647, "IMAGE");
            RegisterColumnType(DBType.Boolean, "BIT");
            RegisterColumnType(DBType.Byte, "BYTE");
            RegisterColumnType(DBType.Char, "CHAR(255)");
            RegisterColumnType(DBType.Char, 255, "CHAR($l)");
            RegisterColumnType(DBType.Currency, "MONEY");
            RegisterColumnType(DBType.DateTime, "DATETIME");
            RegisterColumnType(DBType.Decimal, "DECIMAL(19,5)");
            RegisterColumnType(DBType.Decimal, 19, "DECIMAL(19, $l)");
            RegisterColumnType(DBType.Double, "FLOAT");
            RegisterColumnType(DBType.Guid, "GUID");
            RegisterColumnType(DBType.Image, "IMAGE");
            RegisterColumnType(DBType.Int16, "SMALLINT");
            RegisterColumnType(DBType.Int32, "INT");
            RegisterColumnType(DBType.Int64, "REAL");
            RegisterColumnType(DBType.NChar, "CHAR(255)");
            RegisterColumnType(DBType.NChar, 255, "CHAR($l)");
            RegisterColumnType(DBType.NChar, 1073741823, "MEMO");
            RegisterColumnType(DBType.NText, "MEMO");
            RegisterColumnType(DBType.NVarChar, "TEXT(255)");
            RegisterColumnType(DBType.NVarChar, 255, "TEXT($l)");
            RegisterColumnType(DBType.NVarChar, 1073741823, "MEMO");
            RegisterColumnType(DBType.Single, "REAL");
            RegisterColumnType(DBType.Text, "MEMO");
            //RegisterColumnType(DBType.Timestamp, "IMAGE");
            RegisterColumnType(DBType.VarChar, "TEXT(255)");
            RegisterColumnType(DBType.VarChar, 255, "TEXT($l)");
            RegisterColumnType(DBType.VarChar, 1073741823, "MEMO");
        }
    }
}
