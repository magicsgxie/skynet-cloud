using System;
using System.Collections.Generic;

namespace UWay.Skynet.Cloud.Data.Dialect.Function
{
    abstract class FunctionRegistry : IFunctionRegistry
    {
        protected internal readonly IDictionary<string, IFunctionView> sqlFunctions = new Dictionary<string, IFunctionView>(StringComparer.InvariantCultureIgnoreCase);
        protected abstract IStringFunctions String { get; }
        protected abstract IMathFunctions Math { get; }
        protected abstract IDecimalFunctions Decimal { get; }
        protected abstract IDateTimeFunctions DateTime { get; }

        public IDictionary<string, IFunctionView> SqlFunctions
        {
            get { return sqlFunctions; }
        }


        public FunctionRegistry()
        {
            SqlFunctions.Add(FunctionType.Equals, FunctionView.Equal);

            foreach (var f in typeof(FunctionType.String).GetFields())
            {
                var p = this.String.GetType().GetProperty(f.Name);
                if (p != null)
                    SqlFunctions[f.GetValue(null) as string] = p.GetValue(String, null) as IFunctionView;
            }
            foreach (var f in typeof(FunctionType.Math).GetFields())
            {
                var p = this.Math.GetType().GetProperty(f.Name);
                if (p != null)
                    SqlFunctions[f.GetValue(null) as string] = p.GetValue(this.Math, null) as IFunctionView;
            }
            foreach (var f in typeof(FunctionType.Decimal).GetFields())
            {
                var p = this.Decimal.GetType().GetProperty(f.Name);
                if (p != null)
                    SqlFunctions[f.GetValue(null) as string] = p.GetValue(this.Decimal, null) as IFunctionView;
            }
            foreach (var f in typeof(FunctionType.DateTime).GetFields())
            {
                var p = this.DateTime.GetType().GetProperty(f.Name);
                if (p != null)
                    SqlFunctions[f.GetValue(null) as string] = p.GetValue(this.DateTime, null) as IFunctionView;
            }

            //foreach (var f in typeof(SqlMethods).GetMethods())
            //{
            //    SqlFunctions[f.DeclaringType.Name + "." + f.Name] 
            //}

            SqlFunctions["Pow"] = SqlFunctions[FunctionType.Math.Power];

            SqlFunctions[FunctionType.Equal] = FunctionView.Equal;
            SqlFunctions[FunctionType.NotEqual] = FunctionView.NotEqual;
        }

        public void RegisterFunction(string fnName, IFunctionView fn)
        {
            sqlFunctions[fnName] = fn;
        }

        public bool TryGetFunction(string fnName, out IFunctionView fn)
        {
            if (sqlFunctions.TryGetValue(fnName, out fn))
                return fn != null;
            return false;
        }



        //protected interface ISqlFunctions
        //{
        //    IFunctionView Ascii { get; }
        //    IFunctionView Unicode { get; }
        //    IFunctionView Char { get; }
        //    IFunctionView NChar { get; }

        //    IFunctionView NewGuid { get; }
        //    IFunctionView CharIndex { get; }

        //    IFunctionView Degrees { get; }
        //    IFunctionView Radians { get; }
        //    IFunctionView Rand { get; }
        //    IFunctionView Length { get; }
        //}

    }

    interface IStringFunctions
    {
        IFunctionView Contains { get; }

        IFunctionView StartsWith { get; }

        IFunctionView EndsWith { get; }

        IFunctionView Concat { get; }
        IFunctionView IndexOf { get; }
        IFunctionView Insert { get; }
        IFunctionView LastIndexOf { get; }
        IFunctionView Like { get; }
        IFunctionView LeftOf { get; }
        IFunctionView Length { get; }
        IFunctionView PadLeft { get; }
        IFunctionView PadRight { get; }
        IFunctionView Remove { get; }
        IFunctionView Replace { get; }
        IFunctionView Reverse { get; }
        IFunctionView RightOf { get; }
        IFunctionView Substring { get; }
        IFunctionView ToLower { get; }
        IFunctionView ToUpper { get; }
        IFunctionView Trim { get; }
        IFunctionView TrimEnd { get; }
        IFunctionView TrimStart { get; }
        IFunctionView IsNullOrEmpty { get; }
        IFunctionView IsNullOrWhiteSpace { get; }
    }

    interface IMathFunctions
    {
        IFunctionView Random { get; }
        /// <summary>
        /// 返回64位有符号整数的绝对值
        /// </summary>
        IFunctionView Abs { get; }
        /// <summary>
        /// 返回余弦值为指定数字的角度
        /// </summary>
        IFunctionView Acos { get; }
        /// <summary>
        /// 返回正弦值为指定数字的角度
        /// </summary>
        IFunctionView Asin { get; }
        /// <summary>
        /// 返回正切值为指定数字的角度
        /// </summary>
        IFunctionView Atan { get; }
        /// <summary>
        /// 返回正切值为两个指定数字的商的角度
        /// </summary>
        IFunctionView Atan2 { get; }
        /// <summary>
        /// 返回大于或等于指定的双经度浮点数的最小整数值
        /// </summary>
        IFunctionView Ceiling { get; }
        /// <summary>
        /// 返回指定角度的余弦值
        /// </summary>
        IFunctionView Cos { get; }
        /// <summary>
        /// 返回指定角度的双曲余弦值
        /// </summary>
        IFunctionView Cosh { get; }


        /// <summary>
        /// 返回e的指定次幂
        /// </summary>
        IFunctionView Exp { get; }

        ///// <summary>
        ///// 
        ///// </summary>
        // IFunctionView IEEERemainder { get; }
        /// <summary>
        /// 返回小于或等于指定的双经度浮点数的最大整数值
        /// </summary>
        IFunctionView Floor { get; }
        /// <summary>
        /// 返回指定数字的自然对手（底为e）
        /// </summary>
        IFunctionView Log { get; }
        /// <summary>
        /// 返回指定数字以10为底的对数
        /// </summary>
        IFunctionView Log10 { get; }
        /// <summary>
        /// 返回指定数字的指定次幂
        /// </summary>
        IFunctionView Power { get; }
        /// <summary>
        /// 双精度浮点值按照指定小数位数舍入
        /// </summary>
        IFunctionView Round { get; }
        /// <summary>
        /// 返回一个值，表示64位有符号整数的符号
        /// </summary>
        IFunctionView Sign { get; }
        /// <summary>
        /// 返回指定角度的正弦值
        /// </summary>
        IFunctionView Sin { get; }
        /// <summary>
        /// 返回指定角度的双曲正弦值
        /// </summary>
        IFunctionView Sinh { get; }
        /// <summary>
        /// 返回指定数字的平方根
        /// </summary>
        IFunctionView Sqrt { get; }
        /// <summary>
        /// 返回指定角度的正切值
        /// </summary>
        IFunctionView Tan { get; }
        /// <summary>
        /// 返回指定角度的双曲正切值
        /// </summary>
        IFunctionView Tanh { get; }
        /// <summary>
        /// 返回指定双精度浮点数的整数部分
        /// </summary>
        IFunctionView Truncate { get; }
    }

    interface IDecimalFunctions
    {
        /// <summary>
        /// 计算两个 System.Decimal 值相除后的余数
        /// </summary>
        IFunctionView Remainder { get; }
    }

    interface IDateTimeFunctions
    {
        IFunctionView Now { get; }
        IFunctionView ToDate { get; }
        IFunctionView ToDateTime { get; }
        IFunctionView ToTime { get; }
        IFunctionView DateAdd { get; }
        IFunctionView DateDiff { get; }
        IFunctionView DatePart { get; }
    }
}
