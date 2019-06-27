using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SqlCe
{
    class SqlCeFunctionRegistry : FunctionRegistry
    {
        public SqlCeFunctionRegistry()
        {
            // SqlFunctions.Add(FunctionType.ToString, FunctionViews.VarArgs("CONVERT(NVARCHAR,", ",", ")"));
            SqlFunctions.Add(FunctionType.Compare, FunctionView.Compare);
            SqlFunctions.Add(FunctionType.NewGuid, FunctionView.Standard("NewID"));
            SqlFunctions.Add(FunctionType.Length, FunctionView.Standard("DataLength"));
        }
        private IDateTimeFunctions dateTimeFunctions = new SqlCEDateTimeFunctions();
        protected override IDateTimeFunctions DateTime
        {
            get { return dateTimeFunctions; }
        }

        private IDecimalFunctions decimalFunctions = new SqlCEDecimalFunctions();
        protected override IDecimalFunctions Decimal
        {
            get { return decimalFunctions; }
        }

        private IMathFunctions mathFunctions = new SqlCEMathFunctions();
        protected override IMathFunctions Math
        {
            get { return mathFunctions; }
        }

        private IStringFunctions stringFunctions = new SqlCEStringFunctions();
        protected override IStringFunctions String
        {
            get { return stringFunctions; }
        }
    }
}
