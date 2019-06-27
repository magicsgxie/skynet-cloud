// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System.Collections.Generic;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Linq.Expressions
{
    /// <summary>
    ///  returns the set of all aliases produced by a query source
    /// </summary>
    class TableAliasGatherer : DbExpressionVisitor
    {
        HashSet<TableAlias> aliases;

        private TableAliasGatherer()
        {
            this.aliases = new HashSet<TableAlias>(TableAlias.Comparer);
        }

        public static HashSet<TableAlias> Gather(Expression source)
        {
            var gatherer = new TableAliasGatherer();
            gatherer.Visit(source);
            return gatherer.aliases;
        }

        protected override Expression VisitSelect(SelectExpression select)
        {
            this.aliases.Add(select.Alias);
            return select;
        }

        protected override Expression VisitTable(TableExpression table)
        {
            this.aliases.Add(table.Alias);
            return table;
        }
    }
}
