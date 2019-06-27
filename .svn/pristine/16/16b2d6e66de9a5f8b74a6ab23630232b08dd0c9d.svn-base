using System.Linq;
using System.Text;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Mapping;
namespace UWay.Skynet.Cloud.Data.Schema.Script.Generator
{
    class MySQLScriptGenerator : DatabaseScriptGenerator
    {
        protected override string BuildPKScript(Mapping.IMemberMapping[] members)
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
                sb.Append(",PRIMARY KEY( ")
                    .Append(mapping.PrimaryKeys.Select(p => Dialect.Quote(p.ColumnName)).ToCSV(","))
                    .Append(") ")
                    .AppendLine("")
                    ;
            }
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
                if (num > 0)
                    sb.Append(",");
                sb.AppendLine();

                sb.Append("\t").Append(Dialect.Quote(f.ColumnName));

                var sqlType = f.SqlType;
                sb.Append(" ").Append(GetDbType(f.SqlType));

                if (sqlType.Required || f.IsPrimaryKey)
                    sb.Append(" NOT NULL");
                if (f.IsPrimaryKey && !f.IsGenerated && mapping.PrimaryKeys.Length == 1)
                    sb.Append(" PRIMARY KEY");

                if (f.IsGenerated)
                    sb.Append(GetDefaultValue(f, sqlType));
                num++;
            }
            if (mapping.PrimaryKeys.Count() > 1)
            {
                sb.AppendLine().Append(CreatePKScript(mapping.Members))
                    .Append(") ENGINE=INNODB;");
                sb.AppendLine("");
            }
            else
            {
                sb.AppendLine()
                    .Append(") ENGINE=INNODB;");
                sb.AppendLine("");
            }

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
                        return " AUTO_INCREMENT PRIMARY KEY";
                    break;
            }
            return null;
        }

        protected override void RegisterColumnTypes()
        {
            base.RegisterColumnTypes();

            //binary type:
            RegisterColumnType(DBType.Binary, "LONGBLOB");
            RegisterColumnType(DBType.Binary, 127, "TINYBLOB");
            RegisterColumnType(DBType.Binary, 65535, "BLOB");
            RegisterColumnType(DBType.Binary, 16777215, "MEDIUMBLOB");
            RegisterColumnType(DBType.Binary, "VARBINARY($l)");


            //Numeric type:
            RegisterColumnType(DBType.Boolean, "TINYINT(1)"); // SELECT IF(0, 'true', 'false');
            RegisterColumnType(DBType.Byte, "TINYINT UNSIGNED");

            //string type
            RegisterColumnType(DBType.Char, "CHAR(255)");
            RegisterColumnType(DBType.Char, 255, "CHAR($l)");
            RegisterColumnType(DBType.Char, 65535, "TEXT");
            RegisterColumnType(DBType.Char, 16777215, "MEDIUMTEXT");
            RegisterColumnType(DBType.Currency, "NUMERIC(19,5)");

            RegisterColumnType(DBType.Decimal, "NUMERIC(19,5)");
            RegisterColumnType(DBType.Decimal, 19, "NUMERIC($p, $s)");
            RegisterColumnType(DBType.Double, "DOUBLE");
            RegisterColumnType(DBType.DateTime, "DATETIME");
            RegisterColumnType(DBType.Guid, "VARCHAR(40)");

            RegisterColumnType(DBType.Int16, "SMALLINT");
            RegisterColumnType(DBType.Int32, "INTEGER"); //alias INT
            RegisterColumnType(DBType.Int64, "BIGINT");
            RegisterColumnType(DBType.Image, "LONGBLOB");

            RegisterColumnType(DBType.NChar, "CHAR(255)");
            RegisterColumnType(DBType.NChar, 255, "CHAR($l)");
            RegisterColumnType(DBType.NChar, 65535, "TEXT");
            RegisterColumnType(DBType.NChar, 16777215, "MEDIUMTEXT");
            RegisterColumnType(DBType.NVarChar, "VARCHAR(255)");
            RegisterColumnType(DBType.NVarChar, 255, "VARCHAR($l)");
            RegisterColumnType(DBType.NVarChar, 65535, "TEXT");
            RegisterColumnType(DBType.NVarChar, 16777215, "MEDIUMTEXT");
            RegisterColumnType(DBType.NText, "Text");
            RegisterColumnType(DBType.Single, "FLOAT");


            RegisterColumnType(DBType.Text, "Text");
            //RegisterColumnType(DBType.Timestamp, "TINYBLOB");

            RegisterColumnType(DBType.VarChar, "VARCHAR(255)");
            RegisterColumnType(DBType.VarChar, 255, "VARCHAR($l)");
            RegisterColumnType(DBType.VarChar, 65535, "TEXT");
            RegisterColumnType(DBType.VarChar, 16777215, "MEDIUMTEXT");


            ////UNSINGED Numeric type:
            //RegisterColumnType(DbType.UInt16, "SMALLINT UNSIGNED");
            //RegisterColumnType(DbType.UInt32, "INTEGER UNSIGNED");
            //RegisterColumnType(DbType.UInt64, "BIGINT UNSIGNED");
            //there are no other SqlTypeCode unsigned...but mysql support Float unsigned, double unsigned, etc..

            //Date and time type:
            //RegisterColumnType(DBType.Date, "DATE");


            // RegisterCastTypes();
        }
    }
}
