using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.LinqToSql;
using UWay.Skynet.Cloud.Data.Mapping.Fluent;
using UWay.Skynet.Cloud.Reflection;


namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// LinqToSql 到ULinq的映射转换器类，把LinqToSql的实体映射翻译为ULinq的实体映射
    /// </summary>
    public partial class DbConfiguration
    {

        //把LinqToSql的实体映射翻译为ULinq的实体映射，然后再把该映射注册到DbConfiguration 中
        private void AddULinqClass(Type entityType, Attribute dlinqTableAttribute)
        {
            var mapping = new ClassMap { EntityType = entityType };
            var table = new TableAttribute();
            mapping.table = table;

            var tableName = ULinq.Instance.Table.Name(dlinqTableAttribute) as string;
            if (!string.IsNullOrEmpty(tableName))
            {
                var parts = tableName.Split('.').Where(p => !string.IsNullOrEmpty(p)).ToArray();
                switch (parts.Length)
                {
                    case 1:
                        table.Name = tableName;
                        break;
                    case 2:
                        table.Schema = parts[0];
                        table.Name = parts[1].TrimStart('[').TrimEnd(']');
                        break;
                    case 3:
                        table.DatabaseName = parts[0];
                        table.Schema = parts[1];
                        table.Name = parts[2].TrimStart('[').TrimEnd(']');
                        break;
                    case 4:
                        table.Server = parts[0];
                        table.DatabaseName = parts[1];
                        table.Schema = parts[2];
                        table.Name = parts[3].TrimStart('[').TrimEnd(']');
                        break;
                    default:
                        throw new MappingException("Invalid table name '" + tableName + "'");
                }
            }

            foreach (var p in entityType.GetProperties().Cast<MemberInfo>()
                .Union(entityType.GetFields().Cast<MemberInfo>()))
            {
                //Column
                var c = p.GetCustomAttributes(ULinq.Instance.Column.Type, false).FirstOrDefault();
                if (c != null)
                {

                    PopulateULinqColumn(entityType, mapping, p, c);
                    continue;
                }

                var a = p.GetCustomAttributes(ULinq.Instance.Association.Type, false).FirstOrDefault();
                if (a != null)
                {
                    PopulateULinqAssociation(mapping, p, a);
                    continue;
                }
            }

            var entity = mapping.CreateMapping();
            Guard.NotNull(entity, "entity");

            AutoMapping(entity.entityType, entity);

            RegistyMapping(entity);
        }

        private static void PopulateULinqAssociation(ClassMap mapping, MemberInfo p, object a)
        {
            var thisKey = ULinq.Instance.Association.ThisKey(a) as string;
            var otherKey = ULinq.Instance.Association.OtherKey(a) as string;
            var isForeignKey = (bool)ULinq.Instance.Association.IsForeignKey(a);

            var associationMapper = isForeignKey
                ? new UWay.Skynet.Cloud.Data.ManyToOneAttribute() as AbstractAssociationAttribute
                : new UWay.Skynet.Cloud.Data.OneToManyAttribute();

            if (!string.IsNullOrEmpty(thisKey))
                associationMapper.ThisKey = thisKey;
            if (!string.IsNullOrEmpty(otherKey))
                associationMapper.OtherKey = otherKey;
            mapping.members.Add(p, associationMapper);
        }

        private static void PopulateULinqColumn(Type entityType, ClassMap mapping, MemberInfo p, object c)
        {
            var name = ULinq.Instance.Column.Name(c) as string;
            var canBeNull = (bool)ULinq.Instance.Column.CanBeNull(c);
            var isPrimaryKey = (bool)ULinq.Instance.Column.IsPrimaryKey(c);
            var isDbGenerated = (bool)ULinq.Instance.Column.IsDbGenerated(c);
            var isVersion = (bool)ULinq.Instance.Column.IsVersion(c);
            var strDbType = ULinq.Instance.Column.DbType(c) as string;
            var storage = ULinq.Instance.Column.Storage(c) as string;
            //const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var member = p;

            DBType? dbType = null;
            int? length = null;
            byte? precision = null;
            byte? scale = null;

            string[] parts = null;
            var strType = strDbType;
            if (strDbType.HasValue())
            {
                parts = strDbType.Split(' ').Where(s => s.HasValue()).Select(s => s.Trim()).ToArray();
                strType = parts[0];
                var index = strType.IndexOf("(");

                if (index > 0)
                {
                    var str = strType.Matches("(", ")").FirstOrDefault();
                    var parts2 = str.Split(',').Where(s => s.HasValue()).Select(s => s.Trim()).ToArray();
                    if (parts2.Length == 1)
                    {
                        length = int.Parse(parts2[0]);
                    }
                    else if (parts2.Length == 2)
                    {
                        precision = byte.Parse(parts2[0]);
                        scale = byte.Parse(parts2[1]);
                    }

                    strType = strType.Substring(0, index);
                }


                TypeMappingInfo info;
                if (TypeMapping.SqlDbMap.TryGetValue(strType, out info))
                {
                    dbType = info.DbType;
                }

                if (parts.Length >= 3)
                {
                    if (parts[1].ToUpper() == "NOT" && parts[2].ToUpper() == "NULL")
                        canBeNull = false;

                    if (parts.Length == 4 && parts[3].ToUpper() == "IDENTITY")
                        isDbGenerated = true;
                }

            }

            ColumnAttribute columnMapper = null;

            if (isPrimaryKey)
            {
                var idMapper = new IdAttribute();
                if (isDbGenerated)
                    idMapper.IsDbGenerated = true;
                if (!string.IsNullOrEmpty(name))
                    idMapper.Name = name;
                mapping.members.Add(member, idMapper);
                columnMapper = idMapper;

            }
            else if (isVersion)
            {
                columnMapper = new ColumnAttribute();
                if (!string.IsNullOrEmpty(name))
                    columnMapper.Name = name;
                columnMapper.IsNullable = canBeNull;
                mapping.members.Add(member, columnMapper);
            }
            else
            {
                columnMapper = new ColumnAttribute();
                if (!string.IsNullOrEmpty(name))
                    columnMapper.Name = name;
                columnMapper.IsNullable = canBeNull;
                mapping.members.Add(member, columnMapper);
            }

            if (dbType.HasValue)
                columnMapper.DbType = dbType.Value;
            if (length.HasValue)
                columnMapper.Length = length.Value;
            if (precision.HasValue)
                columnMapper.Precision = precision.Value;
            if (scale.HasValue)
                columnMapper.Scale = scale.Value;
            if (storage.HasValue())
                columnMapper.Storage = storage;

        }
    }

    namespace LinqToSql
    {
        partial class ULinq
        {
            public static readonly string StrTableAttributeType = "System.Data.Linq.Mapping.TableAttribute";
            public static readonly string StrColumnAttributeType = "System.Data.Linq.Mapping.ColumnAttribute";
            public static readonly string StrAssociationAttributeType = "System.Data.Linq.Mapping.AssociationAttribute";
            public static readonly string StrEntitySetType = "System.Data.Linq.EntitySet`1";
            public static readonly string StrEntityRefType = "System.Data.Linq.EntityRef`1";
            public static readonly string StrBinaryType = "System.Data.Linq.Binary";
            public static readonly string StrSqlMethhodsType = "System.Data.Linq.SqlClient.SqlMethods";
            public static readonly string StrAssemblyName = "System.Data.Linq";

            public const string EntitySetTypeName = "System.Data.Linq.EntitySet`1";
            internal static Type EntitySetType;
            internal static Type EntityRefType;
            static readonly Dictionary<int, DefaultConstructorHandler> EntitySetCtorCache = new Dictionary<int, DefaultConstructorHandler>();
            static readonly Dictionary<int, ConstructorHandler> EntityRefCtorCache = new Dictionary<int, ConstructorHandler>();

            public static readonly MethodInfo ToEntitySetMethod = typeof(ULinq).GetMethod("ToEntitySet", BindingFlags.NonPublic | BindingFlags.Static);
            public static readonly MethodInfo ToEntityRefMethod = typeof(ULinq).GetMethod("ToEntityRef", BindingFlags.NonPublic | BindingFlags.Static);

            private static object ToEntitySet<T>(IEnumerable<T> items)
            {
                var entitySetType = EntitySetType.MakeGenericType(typeof(T));
                var key = entitySetType.TypeHandle.Value.GetHashCode();
                DefaultConstructorHandler ctor = null;
                if (!EntitySetCtorCache.TryGetValue(key, out ctor))
                {
                    ctor = entitySetType.GetDefaultCreator();
                    lock (EntitySetCtorCache)
                        EntitySetCtorCache[key] = ctor;
                }
                var entitySet = ctor() as IList<T>;
                foreach (var item in items)
                    entitySet.Add(item);
                return entitySet;
            }

            private static object ToEntityRef<T>(IEnumerable<T> items)
            {
                var entityRefType = EntityRefType.MakeGenericType(typeof(T));
                var key = entityRefType.TypeHandle.Value.GetHashCode();
                ConstructorHandler ctor = null;
                if (!EntityRefCtorCache.TryGetValue(key, out ctor))
                {
                    ctor = entityRefType.GetConstructor(new Type[] { typeof(T) }).GetCreator(); ;
                    lock (EntityRefCtorCache)
                        EntityRefCtorCache[key] = ctor;
                }
                var entityRef = ctor(items.FirstOrDefault());
                return entityRef;
            }

            public static object ToBinary(byte[] bytes)
            {
                return BinaryCtor(bytes);
            }

            public static Type BinaryType;
            public static UWay.Skynet.Cloud.Reflection.ConstructorHandler BinaryCtor;

            public static ULinq Instance { get; private set; }
            public static void Init(Assembly asm)
            {
                var instance = new ULinq();

                //var asm = linqToSqlClassType.Assembly;
                EntityRefType = asm.GetType(StrEntityRefType);
                EntitySetType = asm.GetType(StrEntitySetType);
                BinaryType = asm.GetType(StrBinaryType);
                BinaryCtor = BinaryType.GetConstructor(new Type[] { typeof(byte[]) }).GetCreator();
                SqlType.TypeMap[BinaryType.TypeHandle.Value.GetHashCode()] = DBType.Binary;

                instance.Table = new TableAttribute();
                instance.Table.Type = asm.GetType(ULinq.StrTableAttributeType);
                instance.Table.Name = instance.Table.Type.GetProperty("Name").GetGetter();

                instance.Column = new ColumnAttribute();
                instance.Column.Type = asm.GetType(ULinq.StrColumnAttributeType);
                instance.Column.Name = instance.Column.Type.GetProperty("Name").GetGetter();
                instance.Column.CanBeNull = instance.Column.Type.GetProperty("CanBeNull").GetGetter();
                instance.Column.IsDbGenerated = instance.Column.Type.GetProperty("IsDbGenerated").GetGetter();
                instance.Column.IsPrimaryKey = instance.Column.Type.GetProperty("IsPrimaryKey").GetGetter();
                instance.Column.IsVersion = instance.Column.Type.GetProperty("IsVersion").GetGetter();
                instance.Column.AutoSync = instance.Column.Type.GetProperty("AutoSync").GetGetter();
                instance.Column.DbType = instance.Column.Type.GetProperty("DbType").GetGetter();
                instance.Column.Expression = instance.Column.Type.GetProperty("Expression").GetGetter();
                instance.Column.IsDiscriminator = instance.Column.Type.GetProperty("IsDiscriminator").GetGetter();

                instance.Column.Storage = instance.Column.Type.GetProperty("Storage").GetGetter();
                instance.Column.UpdateCheck = instance.Column.Type.GetProperty("UpdateCheck").GetGetter();

                instance.Association = new AssociationAttribute();
                instance.Association.Type = asm.GetType(ULinq.StrAssociationAttributeType);
                instance.Association.ThisKey = instance.Association.Type.GetProperty("ThisKey").GetGetter();
                instance.Association.OtherKey = instance.Association.Type.GetProperty("OtherKey").GetGetter();
                instance.Association.IsForeignKey = instance.Association.Type.GetProperty("IsForeignKey").GetGetter();

                var sqlMethodsType = asm.GetType(ULinq.StrSqlMethhodsType);
                //var flags = BindingFlags.Public | BindingFlags.Static;
                var dateDiffDay = sqlMethodsType.GetMethod("DateDiffDay", new Type[] { typeof(DateTime), typeof(DateTime) });
                MethodMapping.Mappings[dateDiffDay] = MethodMapping.Lambda<DateTime, DateTime, int>((startTime, endTime) => SqlFunctions.DateDiff(DateParts.Day, startTime, endTime));

                var dateDiffHour = sqlMethodsType.GetMethod("DateDiffHour", new Type[] { typeof(DateTime), typeof(DateTime) });
                MethodMapping.Mappings[dateDiffHour] = MethodMapping.Lambda<DateTime, DateTime, int>((startTime, endTime) => SqlFunctions.DateDiff(DateParts.Hour, startTime, endTime));

                var dateDiffMicrosecond = sqlMethodsType.GetMethod("DateDiffMicrosecond", new Type[] { typeof(DateTime), typeof(DateTime) });
                MethodMapping.Mappings[dateDiffMicrosecond] = MethodMapping.Lambda<DateTime, DateTime, int>((startTime, endTime) => SqlFunctions.DateDiff(DateParts.Microsecond, startTime, endTime));

                var dateDiffMillisecond = sqlMethodsType.GetMethod("DateDiffMillisecond", new Type[] { typeof(DateTime), typeof(DateTime) });
                MethodMapping.Mappings[dateDiffMillisecond] = MethodMapping.Lambda<DateTime, DateTime, int>((startTime, endTime) => SqlFunctions.DateDiff(DateParts.Millisecond, startTime, endTime));

                var dateDiffMinute = sqlMethodsType.GetMethod("DateDiffMinute", new Type[] { typeof(DateTime), typeof(DateTime) });
                MethodMapping.Mappings[dateDiffMinute] = MethodMapping.Lambda<DateTime, DateTime, int>((startTime, endTime) => SqlFunctions.DateDiff(DateParts.Minute, startTime, endTime));

                var dateDiffMonth = sqlMethodsType.GetMethod("DateDiffMonth", new Type[] { typeof(DateTime), typeof(DateTime) });
                MethodMapping.Mappings[dateDiffMonth] = MethodMapping.Lambda<DateTime, DateTime, int>((startTime, endTime) => SqlFunctions.DateDiff(DateParts.Month, startTime, endTime));

                var dateDiffNanosecond = sqlMethodsType.GetMethod("DateDiffNanosecond", new Type[] { typeof(DateTime), typeof(DateTime) });
                MethodMapping.Mappings[dateDiffNanosecond] = MethodMapping.Lambda<DateTime, DateTime, int>((startTime, endTime) => SqlFunctions.DateDiff(DateParts.Nanosecond, startTime, endTime));

                var dateDiffSecond = sqlMethodsType.GetMethod("DateDiffSecond", new Type[] { typeof(DateTime), typeof(DateTime) });
                MethodMapping.Mappings[dateDiffSecond] = MethodMapping.Lambda<DateTime, DateTime, int>((startTime, endTime) => SqlFunctions.DateDiff(DateParts.Second, startTime, endTime));

                var dateDiffYear = sqlMethodsType.GetMethod("DateDiffYear", new Type[] { typeof(DateTime), typeof(DateTime) });
                MethodMapping.Mappings[dateDiffYear] = MethodMapping.Lambda<DateTime, DateTime, int>((startTime, endTime) => SqlFunctions.DateDiff(DateParts.Year, startTime, endTime));

                //
                var dateDiffDayForNullable = sqlMethodsType.GetMethod("DateDiffDay", new Type[] { typeof(DateTime?), typeof(DateTime?) });
                MethodMapping.Mappings[dateDiffDayForNullable] = MethodMapping.Lambda<DateTime?, DateTime?, int?>((startTime, endTime) => SqlFunctions.DateDiff(DateParts.Day, startTime, endTime));

                var dateDiffHourForNullable = sqlMethodsType.GetMethod("DateDiffHour", new Type[] { typeof(DateTime?), typeof(DateTime?) });
                MethodMapping.Mappings[dateDiffHourForNullable] = MethodMapping.Lambda<DateTime?, DateTime?, int?>((startTime, endTime) => SqlFunctions.DateDiff(DateParts.Hour, startTime, endTime));

                var dateDiffMicrosecondForNullable = sqlMethodsType.GetMethod("DateDiffMicrosecond", new Type[] { typeof(DateTime?), typeof(DateTime?) });
                MethodMapping.Mappings[dateDiffMicrosecondForNullable] = MethodMapping.Lambda<DateTime?, DateTime?, int?>((startTime, endTime) => SqlFunctions.DateDiff(DateParts.Microsecond, startTime, endTime));

                var dateDiffMillisecondForNullable = sqlMethodsType.GetMethod("DateDiffMillisecond", new Type[] { typeof(DateTime?), typeof(DateTime?) });
                MethodMapping.Mappings[dateDiffMillisecondForNullable] = MethodMapping.Lambda<DateTime?, DateTime?, int?>((startTime, endTime) => SqlFunctions.DateDiff(DateParts.Millisecond, startTime, endTime));

                var dateDiffMinuteForNullable = sqlMethodsType.GetMethod("DateDiffMinute", new Type[] { typeof(DateTime?), typeof(DateTime?) });
                MethodMapping.Mappings[dateDiffMinuteForNullable] = MethodMapping.Lambda<DateTime?, DateTime?, int?>((startTime, endTime) => SqlFunctions.DateDiff(DateParts.Minute, startTime, endTime));

                var dateDiffMonthForNullable = sqlMethodsType.GetMethod("DateDiffMonth", new Type[] { typeof(DateTime?), typeof(DateTime?) });
                MethodMapping.Mappings[dateDiffMonthForNullable] = MethodMapping.Lambda<DateTime?, DateTime?, int?>((startTime, endTime) => SqlFunctions.DateDiff(DateParts.Month, startTime, endTime));

                var dateDiffNanosecondForNullable = sqlMethodsType.GetMethod("DateDiffNanosecond", new Type[] { typeof(DateTime?), typeof(DateTime?) });
                MethodMapping.Mappings[dateDiffNanosecondForNullable] = MethodMapping.Lambda<DateTime?, DateTime?, int?>((startTime, endTime) => SqlFunctions.DateDiff(DateParts.Nanosecond, startTime, endTime));

                var dateDiffSecondForNullable = sqlMethodsType.GetMethod("DateDiffSecond", new Type[] { typeof(DateTime?), typeof(DateTime?) });
                MethodMapping.Mappings[dateDiffSecondForNullable] = MethodMapping.Lambda<DateTime?, DateTime?, int?>((startTime, endTime) => SqlFunctions.DateDiff(DateParts.Second, startTime, endTime));

                var dateDiffYearForNullable = sqlMethodsType.GetMethod("DateDiffYear", new Type[] { typeof(DateTime?), typeof(DateTime?) });
                MethodMapping.Mappings[dateDiffYearForNullable] = MethodMapping.Lambda<DateTime?, DateTime?, int?>((startTime, endTime) => SqlFunctions.DateDiff(DateParts.Year, startTime, endTime));

                Instance = instance;
            }

            public TableAttribute Table { get; private set; }
            public ColumnAttribute Column { get; private set; }
            public AssociationAttribute Association { get; private set; }


        }

        class TableAttribute
        {
            public Type Type;
            public Getter Name;
        }

        //详细信息可一参考：http://www.cnblogs.com/yuyijq/archive/2008/08/06/1261572.html
        class ColumnAttribute
        {
            public Type Type;
            public Getter Name;
            public Getter CanBeNull;
            public Getter IsDbGenerated;
            public Getter IsPrimaryKey;
            public Getter IsVersion;
            public Getter Storage;

            /// <summary>
            /// 
            /// </summary>
            public Getter AutoSync;
            public Getter DbType;
            public Getter Expression;
            public Getter IsDiscriminator;
            public Getter UpdateCheck;
        }

        class AssociationAttribute
        {
            public Type Type;
            public Getter ThisKey;
            public Getter OtherKey;
            public Getter IsForeignKey;
        }

        //class FunctionAttribute
        //{
        //    public Type Type;
        //    public Getter Name;
        //    public Getter IsComposable;
        //}

        //class ParameterAttribute
        //{
        //    public Type Type;
        //    public Getter Name;
        //    public Getter DbType;
        //}

        //class ResultTypeAttribute
        //{
        //    public Type Type;
        //    public Getter ResultType;
        //}
    }
}
