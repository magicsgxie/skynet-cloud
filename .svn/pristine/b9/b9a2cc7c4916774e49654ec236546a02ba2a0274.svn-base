
namespace UWay.Skynet.Cloud.Data.Dialect.Function.Oracle
{
    class OracleStringFunctions : IStringFunctions
    {
        public IFunctionView Contains
        {
            get
            {
                return FunctionView.Contains;
            }
        }

        public IFunctionView StartsWith
        {
            get
            {
                return FunctionView.StartsWith;
            }
        }

        public IFunctionView EndsWith
        {
            get
            {
                return FunctionView.EndsWith;
            }
        }
        public IFunctionView Like
        {
            get { return FunctionView.Like; }
        }

        public IFunctionView Concat
        {
            get { return FunctionView.VarArgs("(", "||", ")"); }
        }

        public IFunctionView IndexOf
        {
            get { return new LocateFunction(); }
        }

        public IFunctionView Insert
        {
            get { return new UWay.Skynet.Cloud.Data.Dialect.Function.SQLite.InsertFunctionView(); }
        }

        public IFunctionView LastIndexOf
        {
            get { return new LastIndexOfFunctionView(); }
        }

        public IFunctionView LeftOf
        {
            get { return FunctionView.Template("substr(?1, 1, (INSTR(?1,?2) - 1))"); }
        }

        public IFunctionView Length
        {
            get { return FunctionView.Standard("length"); }
        }

        public IFunctionView PadLeft
        {
            get { return FunctionView.Standard("lpad"); }
        }

        public IFunctionView PadRight
        {
            get { return FunctionView.Standard("rpad"); }
        }

        public IFunctionView Remove
        {
            get { return new RemoveFunctionView(); }
        }

        public IFunctionView Replace
        {
            get { return FunctionView.Standard("replace"); }
        }

        public IFunctionView Reverse
        {
            get { return FunctionView.Standard("reverse"); }
        }

        public IFunctionView RightOf
        {
            get { return FunctionView.Template("substr(?1, -(LENGTH(?1)-(INSTR(?1,?2)+LENGTH(?2))+1))"); }
        }

        public IFunctionView Substring
        {
            get { return new SubstringFunctionView(); }
        }

        public IFunctionView ToLower
        {
            get { return FunctionView.Standard("lower"); }
        }

        public IFunctionView ToUpper
        {
            get { return FunctionView.Standard("upper"); }
        }

        public IFunctionView Trim
        {
            get { return FunctionView.Standard("trim"); }
        }

        public IFunctionView TrimEnd
        {
            get { return FunctionView.TrimEnd; }
        }

        public IFunctionView TrimStart
        {
            get { return FunctionView.TrimStart; }
        }

        public IFunctionView IsNullOrWhiteSpace
        {
            get { return new IsNullOrWhiteSpaceFunctionView(); }
        }


        public IFunctionView IsNullOrEmpty
        {
            get { return FunctionView.IsNullOrEmpty; }
        }
    }
}
