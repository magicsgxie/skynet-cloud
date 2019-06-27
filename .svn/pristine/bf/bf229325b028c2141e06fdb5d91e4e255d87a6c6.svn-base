using System;
using System.Linq;
using System.Reflection;
using UWay.Skynet.Cloud.Linq;

namespace UWay.Skynet.Cloud.Data.Common
{
    class MethodRepository
    {
        public static readonly ConstructorInfo ExecutorCtor = (ConstructorInfo)Expressor.Member<InternalDbContext>(dbContext => new ExecutionService(dbContext));

        //public static readonly MethodInfo Like = Expressor.Method<string, string>((str, value) => SqlFunctions.Like(str, value));
        public static readonly MethodInfo Replace = Expressor.Method<string, string, string>((str, p0, p1) => SqlFunctions.Replace(str, p0, p1));
        public static readonly MethodInfo Concat = Expressor.Method<string[]>(array => SqlFunctions.Concat(array));
        public static readonly MethodInfo Reverse = Expressor.Method<string>(s => SqlFunctions.Reverse(s));
        public static readonly MethodInfo Substring = Expressor.Method<string, int?, int?>((str, p1, p2) => SqlFunctions.Substring(str, p1, p2));
        public static readonly MethodInfo ToLower = Expressor.Method<string>((str) => SqlFunctions.ToLower(str));


        static readonly MethodInfo GenericConvert = typeof(SqlFunctions).GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).Where(p => p.Name == "Convert").OrderBy(p => p.GetParameters().Length).FirstOrDefault();
        //public static readonly MethodInfo Convert = Expressor.Method<object, Type>((_, t) => SqlFunctions.Convert(_, t));
        //public static readonly MethodInfo Equals = Expressor.Method<object, object>((a, b) => object.Equals(a, b));
        public static readonly MethodInfo Len = Expressor.Method<byte[]>(arr => SqlFunctions.Length(arr));

        public static MethodInfo GetConvertMethod(Type fromType, Type toType)
        {
            return GenericConvert.MakeGenericMethod(fromType, toType);
        }

        public class CommandContext
        {
            public static readonly ConstructorInfo New = typeof(UWay.Skynet.Cloud.Data.Common.CommandContext).GetConstructors().FirstOrDefault();
        }
    }
}
