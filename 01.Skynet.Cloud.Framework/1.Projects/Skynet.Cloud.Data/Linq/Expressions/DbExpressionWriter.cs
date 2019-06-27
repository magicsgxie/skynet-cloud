// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;


namespace UWay.Skynet.Cloud.Data.Linq.Expressions
{

    class DbExpressionWriter : ExpressionWriter
    {
        InternalDbContext dbContext;
        Dictionary<TableAlias, int> aliasMap = new Dictionary<TableAlias, int>();

        protected DbExpressionWriter(TextWriter writer)
            : base(writer)
        {
            this.dbContext = ExecuteContext.DbContext;
        }

        public new static void Write(TextWriter writer, Expression expression)
        {
            var visitor = new DbExpressionWriter(writer);
            visitor.Visit(expression);
        }

        public static new string WriteToString(Expression expression)
        {
            StringWriter sw = new StringWriter();
            Write(sw, expression);
            return sw.ToString();
        }

        public override Expression Visit(Expression exp)
        {
            if (exp == null)
                return null;

            switch ((DbExpressionType)exp.NodeType)
            {
                case DbExpressionType.Projection:
                    return this.VisitProjection((ProjectionExpression)exp);
                case DbExpressionType.ClientJoin:
                    return this.VisitClientJoin((ClientJoinExpression)exp);
                case DbExpressionType.Select:
                    return this.VisitSelect((SelectExpression)exp);
                case DbExpressionType.OuterJoined:
                    return this.VisitOuterJoined((OuterJoinedExpression)exp);
                case DbExpressionType.Column:
                    return this.VisitColumn((ColumnExpression)exp);
                case DbExpressionType.Insert:
                case DbExpressionType.Update:
                case DbExpressionType.Delete:
                case DbExpressionType.If:
                case DbExpressionType.Block:
                case DbExpressionType.Declaration:
                    return this.VisitCommand((CommandExpression)exp);
                case DbExpressionType.Batch:
                    return this.VisitBatch((BatchExpression)exp);
                case DbExpressionType.Function:
                    return this.VisitFunction((FunctionExpression)exp);
                case DbExpressionType.Entity:
                    return this.VisitEntity((EntityExpression)exp);
                default:
                    if (exp is DbExpression)
                    {
                        this.Write(this.FormatQuery(exp));
                        return exp;
                    }
                    else
                    {
                        return base.Visit(exp);
                    }
            }
        }

        protected void AddAlias(TableAlias alias)
        {
            if (!this.aliasMap.ContainsKey(alias))
            {
                this.aliasMap.Add(alias, this.aliasMap.Count);
            }
        }

        protected virtual Expression VisitProjection(ProjectionExpression projection)
        {
            this.AddAlias(projection.Select.Alias);
            this.Write("Project(");
            this.WriteLine(Indentation.Inner);
            this.Write("@\"");
            this.Visit(projection.Select);
            this.Write("\",");
            this.WriteLine(Indentation.Same);
            this.Visit(projection.Projector);
            this.Write(",");
            this.WriteLine(Indentation.Same);
            this.Visit(projection.Aggregator);
            this.WriteLine(Indentation.Outer);
            this.Write(")");
            return projection;
        }

        protected virtual Expression VisitClientJoin(ClientJoinExpression join)
        {
            this.AddAlias(join.Projection.Select.Alias);
            this.Write("ClientJoin(");
            this.WriteLine(Indentation.Inner);
            this.Write("OuterKey(");
            this.VisitExpressionList(join.OuterKey);
            this.Write("),");
            this.WriteLine(Indentation.Same);
            this.Write("InnerKey(");
            this.VisitExpressionList(join.InnerKey);
            this.Write("),");
            this.WriteLine(Indentation.Same);
            this.Visit(join.Projection);
            this.WriteLine(Indentation.Outer);
            this.Write(")");
            return join;
        }

        protected virtual Expression VisitOuterJoined(OuterJoinedExpression outer)
        {
            this.Write("Outer(");
            this.WriteLine(Indentation.Inner);
            this.Visit(outer.Test);
            this.Write(", ");
            this.WriteLine(Indentation.Same);
            this.Visit(outer.Expression);
            this.WriteLine(Indentation.Outer);
            this.Write(")");
            return outer;
        }

        protected virtual Expression VisitSelect(SelectExpression select)
        {
            this.Write(dbContext.BuildSql(select));
            return select;
        }

        protected virtual Expression VisitCommand(CommandExpression command)
        {
            this.Write(this.FormatQuery(command));
            return command;
        }

        protected virtual string FormatQuery(Expression query)
        {
            return dbContext.BuildSql(query);
        }

        protected virtual Expression VisitBatch(BatchExpression batch)
        {
            this.Write("Batch(");
            this.WriteLine(Indentation.Inner);
            this.Visit(batch.Input);
            this.Write(",");
            this.WriteLine(Indentation.Same);
            this.Visit(batch.Operation);
            this.Write(")");
            return batch;
        }

        protected virtual Expression VisitVariable(VariableExpression vex)
        {
            this.Write(this.FormatQuery(vex));
            return vex;
        }

        protected virtual Expression VisitFunction(FunctionExpression function)
        {
            this.Write("FUNCTION ");
            this.Write(function.Name);
            if (function.Arguments.Count > 0)
            {
                this.Write("(");
                this.VisitExpressionList(function.Arguments);
                this.Write(")");
            }
            return function;
        }

        protected virtual Expression VisitEntity(EntityExpression entity)
        {
            this.Visit(entity.Expression);
            return entity;
        }

        protected virtual Expression VisitColumn(ColumnExpression column)
        {
            int iAlias;
            string aliasName =
                this.aliasMap.TryGetValue(column.Alias, out iAlias)
                ? "A" + iAlias
                : "A" + (column.Alias != null ? column.Alias.GetHashCode().ToString() : "") + "?";

            this.Write(aliasName);
            this.Write(".");
            this.Write("Column(\"");
            this.Write(column.Name);
            this.Write("\")");
            return column;
        }
    }
}
