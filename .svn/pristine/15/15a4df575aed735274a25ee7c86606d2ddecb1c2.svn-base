using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Mapping;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data
{
    class RowMapper
    {
        const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;

        static Dictionary<Type, MethodInfo> GetValueMethods = new Dictionary<Type, MethodInfo>();
        static Type readerType = typeof(IDataReader);
        static MethodInfo isDBNullMethod = typeof(IDataRecord).GetMethod("IsDBNull");
        static MethodInfo getValueMethod = typeof(IDataRecord).GetMethod("GetValue");

        static RowMapper()
        {
            GetValueMethods[Types.Boolean] = typeof(IDataRecord).GetMethod("GetBoolean");
            GetValueMethods[Types.Byte] = typeof(IDataRecord).GetMethod("GetByte");
            GetValueMethods[Types.SByte] = typeof(IDataRecord).GetMethod("GetByte");
            GetValueMethods[Types.Char] = typeof(IDataRecord).GetMethod("GetChar");
            GetValueMethods[Types.DateTime] = typeof(IDataRecord).GetMethod("GetDateTime");
            GetValueMethods[Types.Decimal] = typeof(IDataRecord).GetMethod("GetDecimal");
            GetValueMethods[Types.Double] = typeof(IDataRecord).GetMethod("GetDouble");
            GetValueMethods[typeof(float)] = typeof(IDataRecord).GetMethod("GetFloat");
            GetValueMethods[Types.Guid] = typeof(IDataRecord).GetMethod("GetGuid");

            GetValueMethods[Types.Int16] = typeof(IDataRecord).GetMethod("GetInt16");
            GetValueMethods[Types.Int32] = typeof(IDataRecord).GetMethod("GetInt32");
            GetValueMethods[Types.Int64] = typeof(IDataRecord).GetMethod("GetInt64");
            GetValueMethods[Types.String] = typeof(IDataRecord).GetMethod("GetString");
        }

        static Dictionary<int, Delegate> GetRowMapperByReaderCache = new Dictionary<int, Delegate>();
        public static Func<IDataReader, T> GetRowMapper<T>(IDataReader reader)
        {
            var entityType = typeof(T);
            int fieldCount = reader.FieldCount, hash = fieldCount;
            unchecked
            {

                for (int i = 0; i < fieldCount; i++)
                {
                    var tmp = reader.GetName(i);
                    hash = (hash * 31) ^ (tmp == null ? 0 : tmp.GetHashCode());
                }
                hash = hash ^ entityType.TypeHandle.Value.GetHashCode();
            }

            Delegate @delegate;
            if (GetRowMapperByReaderCache.TryGetValue(hash, out @delegate))
                return @delegate as Func<IDataReader, T>;

            @delegate = CreateRowMapper<T>(reader, entityType, fieldCount);
            lock (GetRowMapperByReaderCache)
                GetRowMapperByReaderCache[hash] = @delegate;

            return @delegate as Func<IDataReader, T>;
        }

        private static Func<IDataReader, T> CreateRowMapper<T>(IDataReader reader, Type entityType, int fieldCount)
        {
            var members = entityType
                           .GetFields(bindingFlags | BindingFlags.SetField)
                           .Where(p => !p.HasAttribute<IgnoreAttribute>(true))
                           .Where(f => !f.IsInitOnly)//确保可写
                           .Where(p => !p.Name.Contains("k__BackingField"))
                           .Cast<MemberInfo>()
                           .Union(entityType
                               .GetProperties(bindingFlags)
                               .Where(p => p.CanWrite)
                               .Where(p => !p.HasAttribute<IgnoreAttribute>(true))
                               .Cast<MemberInfo>()
                               )
                            .ToArray();

            NewExpression newExpression = Expression.New(entityType.GetConstructor(Type.EmptyTypes));
            var memberBindings = new List<MemberBinding>();
            var readerExp = Expression.Parameter(readerType, "reader");

            for (int i = 0; i < fieldCount; i++)
            {
                var columnName = reader.GetName(i).Trim();
                var member = members.FirstOrDefault(p => string.Equals(p.Name, columnName, StringComparison.OrdinalIgnoreCase)); //elementType.GetMember(columnName, flags).FirstOrDefault();
                if (member == null)
                    continue;
                var memberType = member.GetMemberType();
                var columnType = reader.GetFieldType(i);

                Expression getRawValueExp = null;
                MethodInfo getValueMethod = null;
                if (GetValueMethods.TryGetValue(columnType, out getValueMethod))
                    getRawValueExp = Expression.Call(readerExp, getValueMethod, Expression.Constant(i));
                else
                    getRawValueExp = Expression.Convert(Expression.Call(readerExp, RowMapper.getValueMethod, Expression.Constant(i)), columnType);

                var isDBNullExp = Expression.Call(readerExp, isDBNullMethod, Expression.Constant(i)) /*: null*/;

                var memberAssignment = Expression.Bind(member,
                    Expression.Condition(isDBNullExp,
                        Skynet.Cloud.Linq.DefaultExpressionExpressions.GetDefaultExpression(memberType),
                        Converter.GetConvertExpression(columnType, memberType, getRawValueExp)));
                memberBindings.Add(memberAssignment);
            }

            var memberInitExpr = Expression.MemberInit(newExpression, memberBindings.ToArray());
            var lambda = LambdaExpression.Lambda<Func<IDataReader, T>>(memberInitExpr, readerExp);

            return lambda.Compile();
        }
    }
}
