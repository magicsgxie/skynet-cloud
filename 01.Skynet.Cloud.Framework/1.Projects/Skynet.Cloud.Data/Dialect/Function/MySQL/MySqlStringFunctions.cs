
namespace UWay.Skynet.Cloud.Data.Dialect.Function.MySQL
{
    class MySqlStringFunctions : IStringFunctions
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
            get { return FunctionView.Standard("concat"); }
        }

        public IFunctionView IndexOf
        {
            get { return new IndexOfFunctionView(); }
        }

        public IFunctionView Insert
        {
            get { return new InsertFunctionView(); }
        }

        public IFunctionView LastIndexOf
        {
            get { return new LastIndexOfFunctionView(); }
        }

        public IFunctionView LeftOf
        {
            get { return new LeftFunctionView(); }
        }

        public IFunctionView Length
        {
            get { return FunctionView.Standard("length"); }
        }

        public IFunctionView PadLeft
        {
            get { return new PadLeftFunctionView(); }
        }

        public IFunctionView PadRight
        {
            get { return new PadRightFunctionView(); }
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
            get { return new RightFunctionView(); }
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
            get { return FunctionView.LRTrim; }
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
            get { return FunctionView.IsNullOrWhiteSpace; }
        }


        public IFunctionView IsNullOrEmpty
        {
            get { return FunctionView.IsNullOrEmpty; }
        }
    }
}
