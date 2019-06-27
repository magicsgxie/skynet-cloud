using System;
using System.Linq;
using System.Text;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Mapping;
namespace UWay.Skynet.Cloud.Data.Schema.Script.Generator
{
    class OracleScriptGenerator : DatabaseScriptGenerator
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
                    .Append(")")
                    .AppendLine("")
                    ;
            }
            return sb.ToString();
        }

        protected override string BuildTableScript(Mapping.IEntityMapping mapping)
        {
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

                if (f.IsGenerated)
                    sb.Append(GetDefaultValue(f, sqlType));
                num++;
            }

            sb.AppendLine()
                .Append(")");
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
            }

            return null;
        }

        protected override string BuildSequenceScript(Mapping.IEntityMapping mapping)
        {

            var sequenceName = mapping.Members.Where(p => p.IsGenerated).Select(p => p.SequenceName).Where(p => p.HasValue()).ToArray();
            var sb = new StringBuilder(512);
            if (sequenceName != null)
            {
                foreach (var s in sequenceName)
                {
                    sb.Append("CREATE  SEQUENCE ")
                        .Append(s)
                        .AppendLine()
                        .Append("\t MINVALUE 1 ")
                        .Append("\t MAXVALUE 99999999 ")
                        .Append("\t START WITH 1 ")
                        .Append("\t INCREMENT BY 1")
                        .Append("\t NOCACHE")
                        .Append("\t ORDER")
                        ;
                }
            }
            return sb.ToString();
        }

        protected override string BuildFKScript(Mapping.IMemberMapping member)
        {
            var mapping = member.Entity;
            var m = member as MemberMapping;
            var FKName = mapping.TableName.Replace(" ", "_") + "_" + member.Member.Name;
            if (FKName.Length > 28)
            {
                FKName = FKName.Substring(0, 27);
            }
            const string fmt = "ALTER TABLE {0}{1}  ADD CONSTRAINT FK_{2} FOREIGN KEY ({3}) REFERENCES {4}({5}){1}";
            return string.Format(fmt
                            , GetTableName(mapping)
                            , Environment.NewLine
                            , FKName
                            , m.thisKey
                            , GetTableName(member.RelatedEntity)
                            , m.otherKey);
        }

        protected override void RegisterColumnTypes()
        {
            base.RegisterColumnTypes();

            RegisterColumnType(DBType.Char, "CHAR(255)");
            RegisterColumnType(DBType.Char, 2000, "CHAR($l)");
            RegisterColumnType(DBType.VarChar, "VARCHAR2(255)");
            RegisterColumnType(DBType.VarChar, 4000, "VARCHAR2($l)");
            RegisterColumnType(DBType.NChar, "NCHAR(255)");
            RegisterColumnType(DBType.NChar, 2000, "NCHAR($l)");
            RegisterColumnType(DBType.NVarChar, "NVARCHAR2(255)");
            RegisterColumnType(DBType.NVarChar, 4000, "NVARCHAR2($l)");
            RegisterColumnType(DBType.Guid, "RAW(16)");
            RegisterColumnType(DBType.Boolean, "NUMBER(1,0)");
            RegisterColumnType(DBType.Byte, "NUMBER(3,0)");
            RegisterColumnType(DBType.Int16, "NUMBER(5,0)");
            RegisterColumnType(DBType.Int32, "NUMBER(10,0)");
            RegisterColumnType(DBType.Int64, "NUMBER(20,0)");
            RegisterColumnType(DBType.Currency, "NUMBER(19,5)");
            RegisterColumnType(DBType.Currency, 19, "NUMBER($pair,$s)");
            RegisterColumnType(DBType.Single, "FLOAT(24)");
            RegisterColumnType(DBType.Double, "DOUBLE PRECISION");
            RegisterColumnType(DBType.Double, 19, "NUMBER($pair,$s)");
            RegisterColumnType(DBType.Decimal, "NUMBER(19,5)");
            RegisterColumnType(DBType.Decimal, 19, "NUMBER($pair,$s)");
            RegisterColumnType(DBType.DateTime, "DATE");
            RegisterColumnType(DBType.Binary, "RAW(2000)");
            RegisterColumnType(DBType.Binary, 2000, "RAW($l)");
            RegisterColumnType(DBType.Binary, 2147483647, "BLOB");
            RegisterColumnType(DBType.VarChar, 2147483647, "CLOB");
            RegisterColumnType(DBType.NVarChar, 1073741823, "NCLOB");
            RegisterColumnType(DBType.Image, "BLOB");
            RegisterColumnType(DBType.NText, "NCLOB");
            RegisterColumnType(DBType.Text, "CLOB");
        }

    }
}
