using System;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Linq.Expressions;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SQLite
{
    class SQLiteMathFunctions : IMathFunctions
    {
        public IFunctionView Random
        {
            get { return FunctionView.NotSupport("Random"); }
        }

        public IFunctionView Abs
        {
            get { return FunctionView.Standard("abs"); }
        }

        public IFunctionView Acos
        {
            get { return FunctionView.Standard("acos"); }
        }

        public IFunctionView Asin
        {
            get { return FunctionView.Standard("asin"); }
        }

        public IFunctionView Atan
        {
            get { return FunctionView.Standard("atan"); }
        }

        public IFunctionView Atan2
        {
            get { return FunctionView.Standard("atan2"); }
        }

        public IFunctionView Ceiling
        {
            get { return FunctionView.Standard("ceil"); }
        }

        public IFunctionView Cos
        {
            get { return FunctionView.Standard("cos"); }
        }

        public IFunctionView Cosh
        {
            get { return FunctionView.Standard("cosh"); }
        }

        public IFunctionView Exp
        {
            get { return FunctionView.Standard("exp"); }
        }

        public IFunctionView Floor
        {
            get
            {
                return FunctionView.Proxy((builder, args) =>
                {
                    if (!ExecuteContext.Items.ContainsKey(SQLiteDateTimeFunctions.IgonreIntDoubleConvert))
                        FunctionView.Standard("Floor").Render(builder, args);

                    var arg = args[0] as ConstantExpression;
                    if(arg == null)
                    	arg = (args[0] as NamedValueExpression).Value as ConstantExpression;
                    var constanceExpr = arg;
                    if (constanceExpr != null && constanceExpr.Value != null)
                    {
                        var v = Cloud.Mapping.Converter.Convert(constanceExpr.Value, Types.Int32);
                        arg = Expression.Constant(v, Types.Int32);
                    }
                    builder.Visit(arg);
                });
            }
        }

        public IFunctionView Log
        {
            get { return FunctionView.Standard("log"); }
        }

        public IFunctionView Log10
        {
            get { return FunctionView.Template("log10(?1)"); }
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
            get { return FunctionView.Standard("sign"); }
        }

        public IFunctionView Sin
        {
            get { return FunctionView.Standard("sin"); }
        }

        public IFunctionView Sinh
        {
            get { return FunctionView.Standard("sinh"); }
        }

        public IFunctionView Sqrt
        {
            get { return FunctionView.Standard("sqrt"); }
        }

        public IFunctionView Tan
        {
            get { return FunctionView.Standard("tan"); }
        }

        public IFunctionView Tanh
        {
            get { return FunctionView.Standard("tanh"); }
        }

        public IFunctionView Truncate
        {
            get { return FunctionView.NotSupport("Truncate"); }
        }
    }
}
