using System;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.SQLite
{
	class AddDateFunctionView : IFunctionView
	{
		public void Render(ISqlBuilder ctx, params Expression[] args)
		{
			//datetime(?1, '?2 year','?3 month','?4 day','?5 hour','?6 minute','?7 second')
			var flag = args.Length == 3 ? "" : "-";
			var value = (TimeSpan)((args[2] as NamedValueExpression).Value as ConstantExpression).Value;
			ctx.Append("DATETIME(");
			ctx.Visit(args[1]);
			ctx.AppendFormat(", '{4}{0} day','{4}{1} hour','{4}{2} minute','{4}{3} second')"
			                 , value.Days
			                 , value.Hours
			                 , value.Minutes
			                 , value.Seconds
			                 , flag);
		}
	}
}
