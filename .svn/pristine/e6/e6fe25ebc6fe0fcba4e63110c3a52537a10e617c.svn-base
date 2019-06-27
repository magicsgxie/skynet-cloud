using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MsSql
{
    class ToDate3FunctionView : IFunctionView
    {
        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args.Length == 1)
            {
                builder.Append("CONVERT(VARCHAR(10),");
                builder.Visit(args[0]);
                builder.Append(",120)");
            }
            else
            {
                builder.Append("Convert(DateTime, ");
                builder.Append("Convert(nvarchar, ");
                builder.Visit(args[0]);
                builder.Append(") + '/' + ");
                builder.Append("Convert(nvarchar, ");
                builder.Visit(args[1]);
                builder.Append(") + '/' + ");
                builder.Append("Convert(nvarchar, ");
                builder.Visit(args[2]);
                builder.Append("))");
            }
        }
    }
}
