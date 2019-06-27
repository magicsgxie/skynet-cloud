using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SQLite
{
    class ToTimeFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder builder, params Expression[] args)
        {

        }
    }
    internal class SQLiteFunctionManager : FunctionRegistry
    {
        public SQLiteFunctionManager()
        {
            SqlFunctions.Add(FunctionType.Case, FunctionView.Case);
            //SqlFunctions.Add(FunctionType.ToString, FunctionViews.Proxy((v, args) => v.Visit(args[0])));
            SqlFunctions.Add(FunctionType.Compare, FunctionView.Compare);
            SqlFunctions.Add(FunctionType.NewGuid, FunctionView.Standard("NewID"));
            SqlFunctions.Add(FunctionType.Length, FunctionView.Standard("Length"));
        }
        private IDateTimeFunctions dateTimeFunctions = new SQLiteDateTimeFunctions();
        protected override IDateTimeFunctions DateTime
        {
            get { return dateTimeFunctions; }
        }

        private IDecimalFunctions decimalFunctions = new SQLiteDecimalFunctions();
        protected override IDecimalFunctions Decimal
        {
            get { return decimalFunctions; }
        }

        private IMathFunctions mathFunctions = new SQLiteMathFunctions();
        protected override IMathFunctions Math
        {
            get { return mathFunctions; }
        }

        private IStringFunctions stringFunctions = new SQLiteStringFunctions();
        protected override IStringFunctions String
        {
            get { return stringFunctions; }
        }

    }
}
