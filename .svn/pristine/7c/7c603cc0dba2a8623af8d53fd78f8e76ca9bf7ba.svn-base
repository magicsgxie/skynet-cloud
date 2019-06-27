using System;
using System.Linq;
using UWay.Skynet.Cloud.Data.Mapping;

namespace UWay.Skynet.Cloud.Data.Schema.Script.Generator
{
    class SqlServerScriptGenerator : DatabaseScriptGenerator
    {
        protected override string[] BuildSchemaScript(IEntityMapping[] mappings)
        {
            return new string[]{ 
                mappings
                     .Where(p => p.Schema.HasValue()
                         && p.Schema.Trim().ToUpper() != "DBO"
                         && p.Schema.Trim().ToUpper() != "[DBO]")
                     .Select(p => p.Schema.Trim().ToLower())
                     .Distinct()
                     .Select(p => string.Format("{0} CREATE SCHEMA {1}{0} ", Environment.NewLine, p))
                     .ToCSV(";")
            };
        }

        protected override void RegisterColumnTypes()
        {
            base.RegisterColumnTypes();

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
