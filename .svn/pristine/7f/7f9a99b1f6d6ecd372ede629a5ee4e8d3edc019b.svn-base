using System;
using System.Collections.Generic;
using UWay.Skynet.Cloud.Data.Dialect;

namespace UWay.Skynet.Cloud.Data
{
    class ExecuteContext
    {
        [ThreadStatic]
        public static Dictionary<string, object> Items;

        [ThreadStatic]
        public static IDialect Dialect;

        [ThreadStatic]
        public static InternalDbContext DbContext;
    }
}
