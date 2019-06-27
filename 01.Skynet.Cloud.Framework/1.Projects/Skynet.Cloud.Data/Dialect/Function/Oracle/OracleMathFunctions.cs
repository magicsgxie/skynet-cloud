using System;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Mapping;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Oracle
{
    class OracleMathFunctions : IMathFunctions
    {
        public IFunctionView Random
        {
            get { return FunctionView.NotSupport("Random"); }
        }

        public IFunctionView Abs
        {
            get { return new CommonFunction { Name = "ABS" }; }
        }

        public IFunctionView Acos
        {
            get { return new CommonFunction { Name = "ACOS" }; }
        }

        public IFunctionView Asin
        {
            get { return new CommonFunction { Name = "ASIN" }; }
        }

        public IFunctionView Atan
        {
            get { return new CommonFunction { Name = "ATAN" }; }
        }

        public IFunctionView Atan2
        {
            get { return new CommonFunction { Name = "ATAN2" }; }
        }

        public IFunctionView Ceiling
        {
            get { return FunctionView.Standard("ceil"); }
        }

        public IFunctionView Cos
        {
            get { return new CommonFunction { Name = "COS" }; }
        }

        public IFunctionView Cosh
        {
            get { return FunctionView.Standard("cosh"); }
        }

        public IFunctionView Exp
        {
            get { return new CommonFunction { Name = "EXP" }; }
        }

        public IFunctionView Floor
        {
            get
            {
                return FunctionView.Proxy((builder, args) =>
                {
                    if (!ExecuteContext.Items.ContainsKey(OracleDateTimeFunctions.IgonreIntDoubleConvert))
                        FunctionView.Standard("Floor").Render(builder, args);

                    var arg = args[0] as ConstantExpression;
                    var constanceExpr = arg;
                    if (constanceExpr != null && constanceExpr.Value != null)
                    {
                        var v = Converter.Convert(constanceExpr.Value, Types.Int32);
                        arg = Expression.Constant(v, Types.Int32);
                    }
                    builder.Visit(arg);
                });
            }
            //get { return FunctionViews.Standard(""); }
        }

        public IFunctionView Log
        {
            get { return FunctionView.Template("cast(ln(?1) as NUMBER(19,9))"); }
        }

        public IFunctionView Log10
        {
            get { return FunctionView.Template("cast(log(10,?1) as NUMBER(19,9))"); }
        }

        public IFunctionView Power
        {
            get { return FunctionView.Standard("power"); }
        }

        public IFunctionView Round
        {
            get
            {
                return FunctionView.Proxy((builder, args) =>
                {
                    if (args.Length == 1)
                    {
                        builder.Append("ROUND(");
                        builder.Visit(args[0]);
                        builder.Append(", 0)");
                    }
                    else if (args.Length == 2 && args[1].Type == typeof(int))
                    {
                        builder.Append("ROUND(");
                        builder.Visit(args[0]);
                        builder.Append(", ");
                        builder.Visit(args[1]);
                        builder.Append(")");
                    }
                });
            }
        }

        public IFunctionView Sign
        {
            get { return new CommonFunction { Name = "SIGN" }; }
        }

        public IFunctionView Sin
        {
            get { return new CommonFunction { Name = "SIN" }; }
        }

        public IFunctionView Sinh
        {
            get { return FunctionView.Standard("sinh"); }
        }

        public IFunctionView Sqrt
        {
            get { return new CommonFunction { Name = "SQRT" }; }
        }

        public IFunctionView Tan
        {
            get { return new CommonFunction { Name = "TAN" }; }
        }

        public IFunctionView Tanh
        {
            get { return new CommonFunction { Name = "TANH" }; }
        }

        public IFunctionView Truncate
        {
            get { return FunctionView.Standard("trunc"); }
        }
    }
}
