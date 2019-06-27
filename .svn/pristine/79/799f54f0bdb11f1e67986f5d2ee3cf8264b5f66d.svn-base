using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SqlCe
{
    class ToDate6FunctionView : IFunctionView
    {
        public void Render(ISqlBuilder builder, params Expression[] args)
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
            builder.Append(") + ' ' + ");
            builder.Append("Convert(nvarchar, ");
            builder.Visit(args[3]);
            builder.Append(") + ':' + ");
            builder.Append("Convert(nvarchar, ");
            builder.Visit(args[4]);
            builder.Append(") + ':' + ");
            builder.Append("Convert(nvarchar, ");
            builder.Visit(args[5]);
            builder.Append("))");

        }
    }
}
