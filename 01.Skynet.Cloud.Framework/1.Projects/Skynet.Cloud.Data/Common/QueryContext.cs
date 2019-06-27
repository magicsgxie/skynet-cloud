using System;

namespace UWay.Skynet.Cloud.Data.Common
{
    class QueryContext<T> : ISqlCommand
    {
        public string CommandText { get; private set; }
        public NamedParameter[] Parameters { get; private set; }
        object ISqlCommand.ParameterValues { get { return ParameterValues; } }

        public readonly object[] ParameterValues;
        public readonly Func<FieldReader, T> FnProjector;

        public QueryContext(string sql, NamedParameter[] parameters, object[] paramValues, Func<FieldReader, T> fnProjector)
        {
            this.CommandText = sql;
            this.Parameters = parameters;
            this.ParameterValues = paramValues;
            this.FnProjector = fnProjector;
        }
    }

}
