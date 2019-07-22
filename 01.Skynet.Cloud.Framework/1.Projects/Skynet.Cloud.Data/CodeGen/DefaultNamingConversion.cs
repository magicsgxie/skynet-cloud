using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace UWay.Skynet.Cloud.Data.CodeGen
{
    /// <summary>
    /// 命名约定
    /// </summary>
    public static class NamingConversion
    {
        /// <summary>
        /// 缺省命名约定，类名和表名完全一致，如果表名中有' '或'-' 字符用'_'代替
        /// </summary>
        public static readonly INamingConversion Default = new DefaultNamingConversion();
        /// <summary>
        /// 缺省类名为单数原则
        /// </summary>
        public static readonly INamingConversion Singular = new TableNameSingularNamingConversion();
    }

    class DefaultNamingConversion : INamingConversion
    {
        internal static Regex regex1 = new Regex(@"([A-Z]+)([A-Z][a-z])");
        internal static Regex regex2 = new Regex(@"([a-z\d])([A-Z])");
        internal static Regex regex3 = new Regex(@"[-\s]");
        internal static Regex regex4 = new Regex(@"\b([a-z])");

        public string QueryableName(string tableName)
        {
            return Inflector.Plural(MemberName(tableName).FirstUpper());
        }

        public virtual string ClassName(string tableName)
        {
            var className = tableName.Trim();
            className = className.Replace(" ", "_").Replace("-", "_").FirstUpper();
            return className;
        }



        public string FieldName(string columnName)
        {
            return MemberName(columnName).FirstLower();
        }

        public string PropertyName(string columnName)
        {
            return MemberName(columnName).FirstUpper();
        }

        protected virtual string MemberName(string columnName)
        {
            var memberName = columnName.Trim();
            memberName = memberName.Replace(" ", "_").Replace("-", "_");
            return memberName;
        }

        public string DataType(Schema.IColumnSchema col)
        {
            var typeName = col.Type.Name;
            return col.IsNullable && col.Type.IsValueType ? typeName + "?" : typeName;
        }

        public virtual string ManyToOneName(Schema.IRelationSchema fk)
        {
            var fkColumn = fk.ThisKey.ColumnName;

            fkColumn = regex1.Replace(fkColumn, "$1_$2");
            fkColumn = regex2.Replace(fkColumn, "$1_$2");
            fkColumn = regex3.Replace(fkColumn, "_");

            var parts = fkColumn.Split('_').Where(p => p.HasValue()).ToArray();

            if (parts.Length > 1)
            {
                if (parts[parts.Length - 1].ToLower().Trim() == "id")
                    parts[parts.Length - 1] = "";
            }

            fkColumn = parts.ToCSV("");

            while (fk.ThisTable.AllColumns.FirstOrDefault(p => p.ColumnName.ToLower() == fkColumn.ToLower()) != null)
                fkColumn = fkColumn + "1";

            fkColumn = regex4.Replace(fkColumn, match => match.Captures[0].Value.ToUpper());
            fkColumn = fkColumn.Replace(" ", "").FirstUpper();

            return fkColumn;
        }
    }

    class TableNameSingularNamingConversion : DefaultNamingConversion
    {


        protected override string MemberName(string memberName)
        {

            memberName = regex1.Replace(memberName, "$1_$2");
            memberName = regex2.Replace(memberName, "$1_$2");
            memberName = regex3.Replace(memberName, "_").ToLower();
            memberName = memberName.Replace(@"_", " ");
            memberName = regex4.Replace(memberName, match => match.Captures[0].Value.ToUpper());
            memberName = memberName.Replace(" ", "");
            return memberName;
        }

        public override string ClassName(string tableName)
        {
            return Inflector.Singular(MemberName(tableName).FirstUpper());
        }


    }
}
