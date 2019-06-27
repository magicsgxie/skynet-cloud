using System;

namespace UWay.Skynet.Cloud.Data.Common
{
    class CommandContext : ISqlCommand
    {
        public readonly Type EntityType;
        public readonly object Instance;

        public string CommandText { get; private set; }
        public NamedParameter[] Parameters { get; private set; }
        object ISqlCommand.ParameterValues { get { return ParameterValues; } }

        public readonly object[] ParameterValues;
        public readonly OperationType OperationType;
        public readonly bool SupportsVersionCheck;

        public CommandContext(string sql, NamedParameter[] parameters, object[] parameterValues, Type entityType, OperationType op, bool supportsVersionCheck, object instance)
        {
            EntityType = entityType;
            Instance = instance;
            CommandText = sql;
            Parameters = parameters;
            ParameterValues = parameterValues;
            OperationType = op;
            SupportsVersionCheck = supportsVersionCheck;
        }


    }
}
