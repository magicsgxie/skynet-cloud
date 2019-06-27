// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Linq.Internal
{
    /// <summary>
    /// Finds the first sub-expression that is of a specified type
    /// </summary>
    class TypedSubtreeFinder : ExpressionVisitor
    {
        Expression root;
        Type type;
        ExpressionType? expressionType;

        private TypedSubtreeFinder(Type type)
        {
            this.type = type;
        }

        public static Expression Find(Expression expression, Type type)
        {
            TypedSubtreeFinder finder = new TypedSubtreeFinder(type);
            finder.Visit(expression);
            return finder.root;
        }

        public static Expression Find(Expression expression, ExpressionType type)
        {
            TypedSubtreeFinder finder = new TypedSubtreeFinder(null) { expressionType = type };
            finder.Visit(expression);
            return finder.root;
        }

        public override Expression Visit(Expression exp)
        {
            Expression result = base.Visit(exp);

            // remember the first sub-expression that produces an IQueryable
            if (this.root == null && result != null)
            {
                if (type != null)
                {
                    if (this.type.IsAssignableFrom(result.Type))
                        this.root = result;
                }
                else
                {
                    if (result.NodeType == expressionType)
                        this.root = result;
                }
            }

            return result;
        }
    }
}