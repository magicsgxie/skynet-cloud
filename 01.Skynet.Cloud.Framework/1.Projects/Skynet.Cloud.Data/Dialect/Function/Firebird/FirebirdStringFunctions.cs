
namespace UWay.Skynet.Cloud.Data.Dialect.Function.Firebird
{
    class FirebirdStringFunctions : IStringFunctions
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
            get { return FunctionView.NotSupport("Insert"); }
        }

        public IFunctionView LastIndexOf
        {
            get { return FunctionView.NotSupport("LastIndexOf"); }
        }

        public IFunctionView LeftOf
        {
            get { return FunctionView.Template("substr(?1, 1, ?2)"); }
        }

        public IFunctionView Length
        {
            get { return FunctionView.Standard("Char_Length"); }
        }

        public IFunctionView PadLeft
        {
            get { return FunctionView.NotSupport("PadLeft"); }
        }

        public IFunctionView PadRight
        {
            get { return FunctionView.NotSupport("PadRight"); }
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
            get { return FunctionView.NotSupport("Reverse"); }
        }

        public IFunctionView RightOf
        {
            get { return FunctionView.Template("substr(?1, -?2)"); }
        }

        public IFunctionView Substring
        {
            get { return FunctionView.Standard("substr"); }
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
            get { return FunctionView.Standard("rtrim"); }
        }

        public IFunctionView TrimStart
        {
            get { return FunctionView.Standard("ltrim"); }
        }

        public IFunctionView IsNullOrWhiteSpace
        {
            get { return FunctionView.IsNullOrWhiteSpace; }
        }

        public IFunctionView IsNullOrEmpty
        {
            get { return FunctionView.IsNullOrEmpty; }
        }
    }
}
