using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SQLite
{
    class AddDateTemplateFunctionView : IFunctionView
    {
        private string type;

        private AddDateTemplateFunctionView()
        {
        }

        public static readonly IFunctionView AddDays = new AddDateTemplateFunctionView { type = "day" };
        public static readonly IFunctionView AddHours = new AddDateTemplateFunctionView { type = "hour" };
        //public static readonly IFunctionView AddMilliseconds = new AddDateTemplateFunctionView { type = "day" };
        public static readonly IFunctionView AddMinutes = new AddDateTemplateFunctionView { type = "minute" };
        public static readonly IFunctionView AddMonths = new AddDateTemplateFunctionView { type = "month" };
        public static readonly IFunctionView AddSeconds = new AddDateTemplateFunctionView { type = "second" };
        public static readonly IFunctionView AddYears = new AddDateTemplateFunctionView { type = "year" };

        public void Render(ISqlBuilder ctx, params Expression[] args)
        {
            ctx.Append("DATETIME(");
            ctx.Visit(args[0]);
            ctx.Append(",");
            //ctx.Append((args[0] as ConstantExpression).Value);
            ExecuteContext.Items.Add(SQLiteDateTimeFunctions.IgonreIntDoubleConvert, null);
            
            ctx.Append("CAST(");
            ctx.Visit(args[1]);
            ExecuteContext.Items.Remove(SQLiteDateTimeFunctions.IgonreIntDoubleConvert);
            ctx.Append(" AS TEXT) || ' ");
            ctx.Append(type);
            ctx.Append("')");
        }
    }
}
