// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Dialect;
using UWay.Skynet.Cloud.Mapping;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Linq.Expressions
{
    class Parameterizer : DbExpressionVisitor
    {
        IDialect dialect;
        Dictionary<TypeAndValue, NamedValueExpression> map = new Dictionary<TypeAndValue, NamedValueExpression>();
        Dictionary<HashedExpression, NamedValueExpression> pmap = new Dictionary<HashedExpression, NamedValueExpression>();

        private Parameterizer(IDialect dialect)
        {
            this.dialect = dialect;
        }

        public static Expression Parameterize(IDialect dialect, Expression expression)
        {
            return new Parameterizer(dialect).Visit(expression);
        }

        protected override Expression VisitProjection(ProjectionExpression proj)
        {
            // don't parameterize the projector or aggregator!
            SelectExpression select = (SelectExpression)this.Visit(proj.Select);
            return this.UpdateProjection(proj, select, proj.Projector, proj.Aggregator);
        }

        protected override Expression VisitUnary(UnaryExpression u)
        {
            if (u.NodeType == ExpressionType.Convert && u.Operand.NodeType == ExpressionType.ArrayIndex)
            {
                var b = (BinaryExpression)u.Operand;
                if (IsConstantOrParameter(b.Left) && IsConstantOrParameter(b.Right))
                {
                    return this.GetNamedValue(u);
                }
            }
            return base.VisitUnary(u);
        }

        private static bool IsConstantOrParameter(Expression e)
        {
            return e != null && e.NodeType == ExpressionType.Constant || e.NodeType == ExpressionType.Parameter;
        }



        protected override Expression VisitBinary(BinaryExpression b)
        {
            Expression left = this.Visit(b.Left);
            Expression right = this.Visit(b.Right);
            if (left.NodeType == (ExpressionType)DbExpressionType.NamedValue
             && right.NodeType == (ExpressionType)DbExpressionType.Column)
            {
                NamedValueExpression nv = (NamedValueExpression)left;
                ColumnExpression c = (ColumnExpression)right;
                left = new NamedValueExpression(nv.Name, c.SqlType, nv.Value);
            }
            else if (right.NodeType == (ExpressionType)DbExpressionType.NamedValue
             && left.NodeType == (ExpressionType)DbExpressionType.Column)
            {
                NamedValueExpression nv = (NamedValueExpression)right;
                ColumnExpression c = (ColumnExpression)left;
                right = new NamedValueExpression(nv.Name, c.SqlType, nv.Value);
            }
            return this.UpdateBinary(b, left, right, b.Conversion, b.IsLiftedToNull, b.Method);
        }

        protected override ColumnAssignment VisitColumnAssignment(ColumnAssignment ca)
        {
            ca = base.VisitColumnAssignment(ca);
            Expression expression = ca.Expression;
            NamedValueExpression nv = null;

            if (expression.NodeType == ExpressionType.Constant)
            {
                var value = (expression as ConstantExpression).Value;
                if (value == null)
                {
                    TypeAndValue tv = new TypeAndValue(ca.Column.Type, null);
                    if (!this.map.TryGetValue(tv, out nv))
                    {
                        string name = "p" + (iParam++);
                        nv = new NamedValueExpression(name, SqlType.Get(ca.Column.Type), Expression.Constant(null, ca.Column.Type));
                        this.map.Add(tv, nv);
                    }
                    expression = nv;
                }
                else
                {
                    var columnType = ca.Column.Type;
                    if (columnType.IsNullable())
                        columnType = Nullable.GetUnderlyingType(columnType);

                    if (columnType != value.GetType())
                    {
                        value = Converter.Convert(value, columnType);
                        expression = Expression.Constant(value);
                    }
                }
            }

            nv = expression as NamedValueExpression;
            if (nv != null)
            {

                expression = new NamedValueExpression(nv.Name, ca.Column.SqlType, nv.Value);
            }
            return this.UpdateColumnAssignment(ca, ca.Column, expression);
        }

        int iParam = 0;
        protected override Expression VisitConstant(ConstantExpression c)
        {
            if (c.Value != null)
            {
                var type = c.Type;
                //var realType = c.Value.GetType();
                
                if (/*!IsNumeric(type) && type != Types.TimeSpan &&*/ c.Type != typeof(Type) && c.Value.GetType() != typeof(UWay.Skynet.Cloud.Data.Common.DateParts))
                {
                    NamedValueExpression nv;
                    TypeAndValue tv = new TypeAndValue(type, c.Value);
                    if (!this.map.TryGetValue(tv, out nv))
                    {
                        string name = "p" + (iParam++);
                        nv = new NamedValueExpression(name, SqlType.Get(type), c);
                        this.map.Add(tv, nv);
                    }
                    return nv;
                }
                //else
                //{

                //}
            }
            return c;
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return this.GetNamedValue(p);
        }

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            m = (MemberExpression)base.VisitMemberAccess(m);
            NamedValueExpression nv = m.Expression as NamedValueExpression;
            if (nv != null)
            {
                Expression x = Expression.MakeMemberAccess(nv.Value, m.Member);
                return GetNamedValue(x);
            }
            return m;
        }

        private Expression GetNamedValue(Expression e)
        {
            NamedValueExpression nv;
            HashedExpression he = new HashedExpression(e);
            if (!this.pmap.TryGetValue(he, out nv))
            {
                string name = "p" + (iParam++);
                nv = new NamedValueExpression(name, SqlType.Get(e.Type), e);
                this.pmap.Add(he, nv);
            }
            return nv;
        }

        static bool IsNumeric(Type type)
        {
            if (type.IsNullable())
                type = Nullable.GetUnderlyingType(type);
            if (type.IsEnum)
                type = Enum.GetUnderlyingType(type);

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                default:
                    return false;
            }
        }

        struct TypeAndValue : IEquatable<TypeAndValue>
        {
            Type type;
            object value;
            int hash;

            public TypeAndValue(Type type, object value)
            {
                this.type = type;
                this.value = value;
                this.hash = type.GetHashCode();

                if (value != null)
                    this.hash = this.hash ^ value.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (!(obj is TypeAndValue))
                    return false;
                return this.Equals((TypeAndValue)obj);
            }

            public bool Equals(TypeAndValue vt)
            {
                return vt.type == this.type && object.Equals(vt.value, this.value);
            }

            public override int GetHashCode()
            {
                return this.hash;
            }
        }

        struct HashedExpression : IEquatable<HashedExpression>
        {
            Expression expression;
            int hashCode;

            public HashedExpression(Expression expression)
            {
                this.expression = expression;
                this.hashCode = Hasher.ComputeHash(expression);
            }

            public override bool Equals(object obj)
            {
                if (!(obj is HashedExpression))
                    return false;
                return this.Equals((HashedExpression)obj);
            }

            public bool Equals(HashedExpression other)
            {
                return this.hashCode == other.hashCode &&
                    DbExpressionComparer.AreEqual(this.expression, other.expression);
            }

            public override int GetHashCode()
            {
                return this.hashCode;
            }

            class Hasher : DbExpressionVisitor
            {
                int hc;

                internal static int ComputeHash(Expression expression)
                {
                    var hasher = new Hasher();
                    hasher.Visit(expression);
                    return hasher.hc;
                }

                protected override Expression VisitConstant(ConstantExpression c)
                {
                    if (c.Value != null)
                        hc = hc ^ c.Value.GetHashCode();
                    //hc = hc + ((c.Value != null) ? c.Value.GetHashCode() : 0);
                    return c;
                }
            }
        }
    }
}