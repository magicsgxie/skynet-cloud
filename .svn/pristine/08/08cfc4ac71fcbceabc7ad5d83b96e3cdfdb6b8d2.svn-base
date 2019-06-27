using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Access
{
    class AccessMathFunctions : IMathFunctions
    {
        public IFunctionView Random
        {
            get { return FunctionView.NoArgs("rand"); }
        }

        public IFunctionView Abs
        {
            get { return FunctionView.Standard("abs"); }
        }

        public IFunctionView Acos
        {
            get { return FunctionView.Template("\"acos(?1)\""); }//not support
        }

        public IFunctionView Asin
        {
            get { return FunctionView.Template("asin(?1)"); }//not support
        }

        public IFunctionView Atan
        {
            get { return FunctionView.Template("atn(?1)"); }
        }

        public IFunctionView Atan2
        {
            get { return FunctionView.Template("\"atan2(?1,?2)\""); }//not support
        }

        public IFunctionView Ceiling
        {
            get
            {
                //return FunctionView.Template("round(?1)");
                return FunctionView.Proxy((builder, args) =>
                    {
                        var arg = args[0];
                        builder.AppendFormat("IIF(");
                        builder.Visit(arg);
                        builder.Append(">round(");
                        builder.Visit(arg);
                        builder.Append("),round(");
                        builder.Visit(arg);
                        builder.Append(")+1,round(");
                        builder.Visit(arg);
                        builder.Append("))");
                    });
            }
        }

        public IFunctionView Cos
        {
            get { return FunctionView.Standard("cos"); }
        }

        public IFunctionView Cosh
        {
            get
            {
                var exp = FunctionView.Standard("exp");
                return FunctionView.Proxy((builder, args) =>
                {
                    builder.Append("((");
                    exp.Render(builder, args);
                    builder.Append("+");

                    args[0] = Expression.Negate(args[0]);
                    exp.Render(builder, args);
                    builder.Append(")/2)");
                });
            }
        }

        public IFunctionView Exp
        {
            get { return FunctionView.Template("exp(?1)"); }
        }

        public IFunctionView Floor
        {
            get { return FunctionView.Template("Fix(?1)"); }
        }

        public IFunctionView Log
        {
            get { return FunctionView.Standard("log"); }
        }

        public IFunctionView Log10
        {
            get
            {
                return FunctionView.Proxy((builder, args) =>
                {
                    builder.Append("log(");
                    builder.Visit(args[0]);
                    builder.Append(")/log(10)");
                });
            }
        }

        public IFunctionView Power
        {
            get { return FunctionView.VarArgs("", "^", ""); }
        }

        public IFunctionView Round
        {
            get { return FunctionView.Standard("round"); }
        }

        public IFunctionView Sign
        {
            get { return FunctionView.Template("sgn(?1)"); }
        }

        public IFunctionView Sin
        {
            get { return FunctionView.Standard("SIN"); }
        }

        public IFunctionView Sinh
        {
            get
            {
                var exp = FunctionView.Standard("exp");
                return FunctionView.Proxy((builder, args) =>
                {
                    var args2 = new Expression[] { Expression.Negate(args[0]) };
                    builder.Append("((");
                    exp.Render(builder, args);
                    builder.Append("-");
                    exp.Render(builder, args2);
                    builder.Append(")/2)");
                });
            }
        }

        public IFunctionView Sqrt
        {
            get { return FunctionView.Template("sqr(?1)"); }
        }

        public IFunctionView Tan
        {
            get { return FunctionView.Template("tan(?1)"); }
        }

        public IFunctionView Tanh
        {
            get
            {
                var exp = FunctionView.Standard("exp");
                return FunctionView.Proxy((builder, args) =>
                {
                    var args2 = new Expression[] { Expression.Negate(args[0]) };
                    builder.Append("((");
                    exp.Render(builder, args);
                    builder.Append("-");
                    exp.Render(builder, args2);
                    builder.Append(")/");
                    builder.Append("(");
                    exp.Render(builder, args);
                    builder.Append("+");
                    exp.Render(builder, args2);
                    builder.Append("))");
                });
            }
        }

        public IFunctionView Truncate
        {
            get { return FunctionView.Template("Fix(?1)"); }
        }
    }
}
