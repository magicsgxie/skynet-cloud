using System;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MySQL
{
    class MySqlFunctionRegistry : FunctionRegistry
    {
        public MySqlFunctionRegistry()
        {
            SqlFunctions.Add(FunctionType.Case, FunctionView.Case);
            //SqlFunctions.Add(FunctionType.ToString, FunctionViews.VarArgs("CAST(", ",", " AS CHAR)"));
            SqlFunctions.Add(FunctionType.Compare, FunctionView.Compare);
            SqlFunctions.Add(FunctionType.NewGuid, FunctionView.Standard("Uuid"));
            SqlFunctions.Add(FunctionType.Length, FunctionView.Standard("Length"));
        }
        private IDateTimeFunctions dateTimeFunctions = new MySqlDateTimeFunctions();
        protected override IDateTimeFunctions DateTime
        {
            get { return dateTimeFunctions; }
        }

        private IDecimalFunctions decimalFunctions = new MySqlDecimalFunctions();
        protected override IDecimalFunctions Decimal
        {
            get { return decimalFunctions; }
        }

        private IMathFunctions mathFunctions = new MySqlMathFunctions();
        protected override IMathFunctions Math
        {
            get { return mathFunctions; }
        }

        private IStringFunctions stringFunctions = new MySqlStringFunctions();
        protected override IStringFunctions String
        {
            get { return stringFunctions; }
        }
    }
    class AddDateFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder ctx, params Expression[] args)
        {
            var flag = args.Length == 3 ? "DATE_ADD" : "DATE_SUB";
            var value = (TimeSpan)(args[2] as ConstantExpression).Value;
            ctx.AppendFormat("{0}({0}({0}({0}(", flag);
            ctx.Visit(args[1]);
            ctx.AppendFormat(",INTERVAL {0} SECOND),INTERVAL {1} MINUTE),INTERVAL {2} HOUR),INTERVAL {3} DAY)"
                              , value.Seconds
                              , value.Minutes
                              , value.Hours
                              , value.Days

                               );
        }
    }
}
