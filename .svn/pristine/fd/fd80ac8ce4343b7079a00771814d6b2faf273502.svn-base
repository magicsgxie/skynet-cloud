using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SQLite
{
    class DatePartQuarterFunctionView : IFunctionView
    {

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
        	//TODO:BUG
            builder.Append("((CAST(strftime('%m', orderdate) AS INT) - 1)/3 + 1)");
        }
    }
}
