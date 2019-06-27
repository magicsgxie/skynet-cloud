using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.MySQL
{
    class ToTimeFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            if (args.Length == 1)
            {
                builder.Append("DATE_FORMAT(");
                builder.Visit(args[0]);
                builder.Append("，'%H:%i:%s')");
            }
        }
    }
}
