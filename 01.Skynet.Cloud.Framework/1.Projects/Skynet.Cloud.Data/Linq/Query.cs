// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Linq
{

    class Query<T> : IQueryable<T>, IQueryable, IEnumerable<T>, IEnumerable, IOrderedQueryable<T>, IOrderedQueryable
    {
        IQueryProvider provider;
        Expression expression;

        public Query(IQueryProvider provider)
            : this(provider, (Type)null)
        {
        }

        public Query(IQueryProvider provider, Type staticType)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("Provider");
            }
            this.provider = provider;
            this.expression = staticType != null ? Expression.Constant(this, staticType) : Expression.Constant(this);
        }

        public Query(IQueryProvider provider, Expression expression)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("Provider");
            }
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
            {
                throw new ArgumentOutOfRangeException("expression");
            }
            this.provider = provider;
            this.expression = expression;
        }

        public Expression Expression
        {
            get { return this.expression; }
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public IQueryProvider Provider
        {
            get { return this.provider; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)this.provider.Execute(this.expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.provider.Execute(this.expression)).GetEnumerator();
        }

        public override string ToString()
        {
            if (this.expression.NodeType == ExpressionType.Constant &&
                ((ConstantExpression)this.expression).Value == this)
                return "Query(" + typeof(T) + ")";
            else
            {
                var sql = SqlText;
                return sql ?? expression.ToString();
            }
        }

        static readonly string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public string Version
        {
            get { return version; }
        }

        public string SqlText
        {
            get
            {
                var dbContext = provider as InternalDbContext;
                if (dbContext != null)
                    return string.Format("ULinq Version :{0}{1}{2}{3}{4}"
                        , Version
                        , Environment.NewLine
                        , "Dialect :" + dbContext.Dialect.GetType().Name
                        , Environment.NewLine
                        , dbContext.GetSqlText(this.Expression));
                return null;
            }
        }

        public string ExecutePlan
        {
            get
            {
                var dbContext = provider as InternalDbContext;
                if (dbContext != null)
                {
                    var plan = ExecutorFinder.Find(dbContext.GetExecutionPlan(Expression));
                    return string.Format("ULinq Version :{0}{1}{2}{3}{4}"
                       , Version
                       , Environment.NewLine
                       , "Dialect :" + dbContext.Dialect.GetType().Name
                       , Environment.NewLine
                       , DbExpressionWriter.WriteToString(plan));
                }
                return null;
            }
        }

        class ExecutorFinder : DbExpressionVisitor
        {
            public Expression executeExpression;
            static readonly Type ExectorType = typeof(ExecutionService);

            public static Expression Find(Expression expression)
            {
                var gatherer = new ExecutorFinder();
                gatherer.Visit(expression);
                return gatherer.executeExpression;
            }

            protected override Expression VisitMethodCall(MethodCallExpression m)
            {
                if (ExectorType.IsAssignableFrom(m.Method.DeclaringType) && m.Method.Name == "List")
                {
                    executeExpression = m;
                    return m;
                }
                return base.VisitMethodCall(m);
            }


        }

    }
}
