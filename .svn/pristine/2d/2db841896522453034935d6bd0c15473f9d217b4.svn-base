using System;
using System.Linq;
using System.Text;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Dialect;
using UWay.Skynet.Cloud.Data.Mapping;

namespace UWay.Skynet.Cloud.Data.Schema.Script.Generator
{
    /// <summary>
    /// 数据库脚本生成器，提供数据库、表、主键约束、外键约束、检查约束、Uniqule约束等脚本的创建工作
    /// </summary>
    abstract class DatabaseScriptGenerator : IDatabaseScriptGenerator
    {
        internal readonly TypeNames typeNames = new TypeNames();

        public DatabaseScriptGenerator()
        {
            RegisterColumnTypes();
        }

        #region 注册列类型
        protected virtual void RegisterColumnTypes()
        {
        }
        protected void RegisterColumnType(DBType code, int size, string name)
        {
            typeNames.Put(code, size, name);
        }
        protected void RegisterColumnType(DBType code, string name)
        {
            typeNames.Put(code, name);
        }
        #endregion

        /// <summary>
        /// 数据库名称
        /// </summary>
        protected string DatabaseName { get; private set; }
        /// <summary>
        /// 数据库方言
        /// </summary>
        protected IDialect Dialect { get; private set; }
        /// <summary>
        /// 建库脚本实体类
        /// </summary>
        protected DatabaseScriptEntry Entry { get; private set; }

        /// <summary>
        /// 构建数据库脚本
        /// </summary>
        protected virtual string BuildDatabaseScript(string databaseName)
        {
            return "Create Database " + databaseName;
        }
        /// <summary>
        /// 构建Schema脚本
        /// </summary>
        protected virtual string[] BuildSchemaScript(IEntityMapping[] mappings)
        {
            return new string[0];
        }

        /// <summary>
        /// 构建特定映射对应的主键脚本
        /// </summary>
        /// <param name="members"></param>
        /// <returns></returns>
        protected virtual string BuildPKScript(IMemberMapping[] members)
        {
            const string fmt = "ALTER TABLE {0} {1}\tADD CONSTRAINT PK_{2}  PRIMARY KEY CLUSTERED ({3}) ON [PRIMARY]{1}";
            var mapping = members[0].Entity;
            return string.Format(fmt
                                 , GetTableName(mapping)
                                 , Environment.NewLine
                                 , mapping.TableName.Replace(" ", "_")
                                 , members.Select(p => Dialect.Quote(p.ColumnName)).ToCSV(","));
        }
        /// <summary>
        /// 构建特定映射对应的外键脚本
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        protected virtual string BuildFKScript(IMemberMapping member)
        {
            var mapping = member.Entity;
            var m = member as MemberMapping;
            const string fmt = "ALTER TABLE {0}{1}  ADD CONSTRAINT FK_{2} FOREIGN KEY ({3}) REFERENCES {4}({5}){1}";
            return string.Format(fmt
                            , GetTableName(mapping)
                            , Environment.NewLine
                            , mapping.TableName.Replace(" ", "_") + "_" + member.Member.Name
                            , m.thisKey
                            , GetTableName(member.RelatedEntity)
                            , m.otherKey);
        }
        /// <summary>
        /// 构建特定映射对应的唯一性约束脚本
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        protected virtual string BuildUniquleConstraintScript(IMemberMapping member) { return null; }

        /// <summary>
        /// 构建特定映射对应的检查约束脚本
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        protected virtual string BuildCheckConstraintScript(IMemberMapping member) { return null; }

        /// <summary>
        /// 得到对应的数据库类型
        /// </summary>
        /// <param name="sqlType"></param>
        /// <returns></returns>
        public string GetDbType(SqlType sqlType)
        {
            return typeNames.Get(sqlType.DbType, sqlType.Length, sqlType.Precision, sqlType.Scale);
        }

        /// <summary>
        /// 得到表名
        /// </summary>
        /// <param name="mapping"></param>
        /// <returns></returns>
        protected virtual string GetTableName(IEntityMapping mapping)
        {
            if (Dialect.SupportSchema)
            {
                var tableName = mapping.Schema.HasValue()
                  ? Dialect.Quote(mapping.Schema) + "." + Dialect.Quote(mapping.TableName)
                  : Dialect.Quote(mapping.TableName);
            }
            return Dialect.Quote(mapping.TableName);
        }

        /// <summary>
        /// 得到缺省值
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="f"></param>
        /// <param name="sqlType"></param>
        protected virtual string GetDefaultValue(IMemberMapping f, SqlType sqlType)
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
                        return " IDENTITY";
                    break;
            }

            return null;
        }

        /// <summary>
        /// 构建建表脚本
        /// </summary>
        /// <param name="mapping"></param>
        /// <returns></returns>
        protected virtual string BuildTableScript(IEntityMapping mapping)
        {
            var tableName = GetTableName(mapping);
            var sb = new StringBuilder(512);
            sb.Append("CREATE TABLE ").Append(tableName).Append("(");

            int num = 0;
            foreach (var f in mapping.Members.Where(p => !p.IsRelationship && !p.IsComputed))
            {
                if (num > 0)
                    sb.Append(",");
                BuildColumn(sb, f);
                num++;
            }

            sb.AppendLine()
                .Append(")");
            sb.AppendLine("");
            return sb.ToString();
        }

        private void BuildColumn(StringBuilder sb, IMemberMapping f)
        {
            sb.AppendLine();
            sb.Append("\t").Append(Dialect.Quote(f.ColumnName));

            var sqlType = f.SqlType;
            sb.Append(" ").Append(GetDbType(f.SqlType));

            if (sqlType.Required || f.IsPrimaryKey)
                sb.Append(" NOT NULL");

            if (f.IsGenerated)
                sb.Append(GetDefaultValue(f, sqlType));
        }

        /// <summary>
        /// 生成数据库脚本
        /// </summary>
        /// <param name="dialect">数据库方言</param>
        /// <param name="mappings">映射元数据</param>
        /// <param name="dbName">数据库名称</param>
        /// <returns>返回数据库脚本</returns>
        public DatabaseScriptEntry Build(IDialect dialect, Mapping.IEntityMapping[] mappings, string databaseName)
        {
            Guard.NotNull(dialect, "dialect");
            Guard.NotNull(mappings, "mappings");
            if (mappings.Length == 0)
                throw new ArgumentNullException("mappings");
            this.Dialect = dialect;
            var Mappings = mappings;
            DatabaseName = databaseName;

            Entry = new DatabaseScriptEntry();

            BuildDatabaseScript(databaseName);

            if (dialect.SupportSchema)
                Entry.SchemaScripts = BuildSchemaScript(mappings)
                     .Where(p => p.HasValue())
                     .ToArray();

            var sequanceScript = BuildAllSequanceScripts(Mappings)
                .Where(p => p.HasValue())
                     .ToArray();


            var pkScript = Mappings
                        .Where(p => p.PrimaryKeys.Length > 0)
                        .Select(p => BuildPKScript(p.PrimaryKeys))
                        .Where(p => p.HasValue())
                        .ToArray();

            var checkScript = Mappings.Where(p => p.Members.Any(m => m.IsCheck))
                    .SelectMany(p => p.Members.Where(m => m.IsCheck))
                    .Select(p => BuildCheckConstraintScript(p))
                    .Where(p => p.HasValue())
                    .ToArray();

            var fkScript = Mappings.Where(p => p.Members.Any(m => m.IsManyToOne))
                    .SelectMany(p => p.Members.Where(m => m.IsManyToOne))
                    .Select(p => BuildFKScript(p))
                    .Where(p => p.HasValue())
                    .ToArray();

            var uniquleScript = Mappings.Where(p => p.Members.Any(m => m.IsUniqule))
                    .SelectMany(p => p.Members.Where(m => m.IsUniqule))
                    .Select(p => BuildUniquleConstraintScript(p))
                    .Where(p => p.HasValue())
                    .ToArray();

            if (Dialect.SupportMultipleCommands)
            {
                Entry.TableScripts = new string[]
                {
                   Mappings.Select(p=>BuildTableScript(p)).ToCSV(";")
                };
                if (pkScript.Length > 0)
                    Entry.PKConstraintScripts = new string[] { pkScript.ToCSV(";") };

                if (fkScript.Length > 0)
                    Entry.FKConstraintScripts = new string[] { fkScript.ToCSV(";") };


                if (checkScript.Length > 0)
                    Entry.CheckConstraintScript = new string[] { checkScript.ToCSV(";") };

                if (uniquleScript.Length > 0)
                    Entry.UniquleConstraintScripts = new string[] { uniquleScript.ToCSV(";") };

                if (sequanceScript.Length > 0)
                    Entry.SequenceScripts = new string[] { sequanceScript.ToCSV(";") };
            }
            else
            {
                Entry.TableScripts = Mappings.Select(p => BuildTableScript(p)).ToArray();
                Entry.PKConstraintScripts = pkScript;
                Entry.FKConstraintScripts = fkScript;
                Entry.CheckConstraintScript = checkScript;
                Entry.UniquleConstraintScripts = uniquleScript;
                Entry.SequenceScripts = sequanceScript;
            }

            return Entry;
        }

        private string[] BuildAllSequanceScripts(IEntityMapping[] Mappings)
        {
            var sequanceScript = Mappings
                        .Where(p => p.Members.Any(m => string.IsNullOrEmpty(m.SequenceName)))
                        .Select(p => BuildSequenceScript(p))
                        .Where(p => p.HasValue())
                        .ToArray();
            return sequanceScript;
        }

        /// <summary>
        /// 构建序列脚本
        /// </summary>
        /// <param name="members"></param>
        /// <returns></returns>
        protected virtual string BuildSequenceScript(Mapping.IEntityMapping mapping)
        {
            return null;
        }

    }
}
