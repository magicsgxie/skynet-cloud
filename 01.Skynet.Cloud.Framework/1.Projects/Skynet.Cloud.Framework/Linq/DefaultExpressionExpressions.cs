using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Linq
{

    public static class DefaultExpressionExpressions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Expression GetDefaultExpression(Type type)
        {
            return Expression.Default(type);
        }
    }


}
