using System;
using System.Collections.Generic;

namespace UWay.Skynet.Cloud.Data.Common
{
    class BatchContext : ISqlCommand
    {
        public readonly Type EntityType;
        public string CommandText { get; private set; }
        public NamedParameter[] Parameters { get; private set; }
        object ISqlCommand.ParameterValues { get { return ParameterSets; } }

        public readonly IEnumerable<object[]> ParameterSets;

        public BatchContext(string sql, NamedParameter[] parameters, IEnumerable<object[]> paramSets, Type entityType)
        {
            EntityType = entityType;
            CommandText = sql;
            Parameters = parameters;
            ParameterSets = paramSets;
        }
    }

    class BatchContext<TResult> : ISqlCommand
    {
        public string CommandText { get; private set; }
        public NamedParameter[] Parameters { get; private set; }
        object ISqlCommand.ParameterValues { get { return ParameterSets; } }

        public readonly IEnumerable<object[]> ParameterSets;
        public readonly Func<FieldReader, TResult> FnProjector;

        public BatchContext(string sql, NamedParameter[] parameters, IEnumerable<object[]> paramSets, Func<FieldReader, TResult> fnProjector)
        {
            this.CommandText = sql;
            this.Parameters = parameters;
            this.ParameterSets = paramSets;
            this.FnProjector = fnProjector;
        }
    }
}
