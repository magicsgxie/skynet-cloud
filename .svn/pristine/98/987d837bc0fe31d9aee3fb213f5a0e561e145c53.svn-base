using System;
using System.Linq.Expressions;
using System.Reflection;
using UWay.Skynet.Cloud.Linq;

namespace UWay.Skynet.Cloud.Data.Mapping.Fluent
{
    /// <summary>
    /// 实体关系映射类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ClassMap<TEntity> : ClassMap
    {
        /// <summary>
        /// 
        /// </summary>
        public ClassMap()
        {
            EntityType = typeof(TEntity);
        }

        /// <summary>
        /// 设置表名
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public TableExpression TableName(string tableName)
        {
            Guard.NotNullOrEmpty(tableName, "tableName");
            var exp = new TableExpression();
            table = exp.Attribute;
            table.Name = tableName;
            return exp;
        }

        private static MemberInfo GetMember<T>(Expression<Func<T, object>> fnMember)
        {
            Guard.NotNull(fnMember, "fnMember");
            var member = Expressor.Member(fnMember);
            Guard.NotNull(member, "member");
            var f = member as FieldInfo;

            if (f != null)
            {
                if (f.IsInitOnly)
                    throw new ArgumentException(string.Format("Field '{0}.{1}' must be can read and write!", typeof(T).Name, f.Name));
                return f;
            }

            var p = member as PropertyInfo;
            if (p != null)
            {
                if (!p.CanRead || !p.CanWrite)
                    throw new ArgumentException(string.Format("Property '{0}.{1}' must be can read and write!", typeof(T).Name, p.Name));
                return p;
            }

            throw new ArgumentException(string.Format("'{0}.{1}' must be field or property", typeof(T).Name, member.Name));
        }
        /// <summary>
        /// 忽略成员映射
        /// </summary>
        /// <param name="fnMember"></param>
        /// <returns></returns>
        public ClassMap<TEntity> Ignore(Expression<Func<TEntity, object>> fnMember)
        {
            IgnoreMembers.Add(GetMember(fnMember));
            return this;
        }

        /// <summary>
        /// 忽略成员映射
        /// </summary>
        /// <param name="fnMember"></param>
        /// <returns></returns>
        public ClassMap<TEntity> Ignore(MemberInfo member)
        {
            Guard.NotNull(member, "member");
            IgnoreMembers.Add(member);
            return this;
        }

        /// <summary>
        /// 设置主键
        /// </summary>
        /// <param name="fnMember"></param>
        /// <returns></returns>
        public IdExpression Id(Expression<Func<TEntity, object>> fnMember)
        {
            var exp = new IdExpression();
            members[GetMember(fnMember)] = exp.attribute;
            return exp;
        }

        public IdExpression Id(MemberInfo idMember)
        {
            Guard.NotNull(idMember, "idMember");
            var exp = new IdExpression();
            members[idMember] = exp.attribute;
            return exp;
        }

        /// <summary>
        /// 设置序列主键
        /// </summary>
        /// <param name="fnMember"></param>
        /// <returns></returns>
        public SequenceExpression SequenceId(Expression<Func<TEntity, object>> fnMember)
        {
            var exp = new SequenceExpression();
            members[GetMember(fnMember)] = exp.attribute;
            return exp;
        }

        /// <summary>
        /// 设置列
        /// </summary>
        /// <param name="fnMember"></param>
        /// <returns></returns>
        public ColumnExpression Column(Expression<Func<TEntity, object>> fnMember)
        {
            var exp = new ColumnExpression();
            members[GetMember(fnMember)] = exp.attribute;
            return exp;
        }

        /// <summary>
        /// 设置列
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public ColumnExpression Column(MemberInfo member)
        {
            Guard.NotNull(member, "member");
            var exp = new ColumnExpression();
            members[member] = exp.attribute;
            return exp;
        }

        /// <summary>
        /// 设置版本列
        /// </summary>
        /// <param name="fnMember"></param>
        /// <returns></returns>
        public VersionExression Version(Expression<Func<TEntity, object>> fnMember)
        {
            var exp = new VersionExression();
            members[GetMember(fnMember)] = exp.attribute;
            return exp;
        }

        /// <summary>
        /// 设置列
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public VersionExression Version(MemberInfo member)
        {
            Guard.NotNull(member, "member");
            var exp = new VersionExression();
            members[member] = exp.attribute;
            return exp;
        }

        /// <summary>
        /// 设置计算列
        /// </summary>
        /// <param name="fnMember"></param>
        /// <returns></returns>
        internal ComputedColumnExpressison ComputedColumn(Expression<Func<TEntity, object>> fnMember)
        {
            var exp = new ComputedColumnExpressison();
            members[GetMember(fnMember)] = exp.attribute;
            return exp;
        }



        /// <summary>
        /// 设置一对多映射关系
        /// </summary>
        /// <typeparam name="TOtherEntity"></typeparam>
        /// <param name="fnMember"></param>
        /// <returns></returns>
        public OneToManyExpression<TOtherEntity> OneToMany<TOtherEntity>(Expression<Func<TEntity, object>> fnMember)
        {
            var associationMember = GetMember(fnMember);
            var exp = new OneToManyExpression<TOtherEntity>();
            members[associationMember] = exp.attribute;
            return exp;
        }

        /// <summary>
        /// 设置一对一映射关系
        /// </summary>
        /// <typeparam name="TOtherEntity"></typeparam>
        /// <param name="fnMember"></param>
        /// <returns></returns>
        public OneToOneExpression<TOtherEntity> OneToOne<TOtherEntity>(Expression<Func<TEntity, object>> fnMember)
        {
            var associationMember = GetMember(fnMember);
            var exp = new OneToOneExpression<TOtherEntity>();
            members[associationMember] = exp.attribute;
            return exp;
        }

        /// <summary>
        /// 设置多对一映射关系
        /// </summary>
        /// <typeparam name="TOtherEntity"></typeparam>
        /// <param name="fnMember"></param>
        /// <returns></returns>
        public ManyToOneExpression<TOtherEntity> ManyToOne<TOtherEntity>(Expression<Func<TEntity, object>> fnMember)
        {
            var associationMember = GetMember(fnMember);
            var exp = new ManyToOneExpression<TOtherEntity>();
            members[associationMember] = exp.attribute;
            return exp;
        }

        /// <summary>
        /// 关系映射表达式
        /// </summary>
        /// <typeparam name="TOtherEntity"></typeparam>
        /// <typeparam name="TAssociatonExpression"></typeparam>
        /// <typeparam name="TAssociationAttribute"></typeparam>
        public abstract class AssociationExpression<TOtherEntity, TAssociatonExpression, TAssociationAttribute> : MemberExpression<TAssociatonExpression, TAssociationAttribute>
            where TAssociationAttribute : AbstractAssociationAttribute, new()
            where TAssociatonExpression : AssociationExpression<TOtherEntity, TAssociatonExpression, TAssociationAttribute>
        {
            /// <summary>
            /// 设置ThisKey member
            /// </summary>
            /// <param name="fnMember"></param>
            /// <returns></returns>
            public TAssociatonExpression ThisKey(Expression<Func<TEntity, object>> fnMember)
            {
                var member = GetMember(fnMember);
                attribute.ThisKey = member.Name;
                return this as TAssociatonExpression;
            }

            /// <summary>
            /// 设置OtherKey member
            /// </summary>
            /// <param name="fnOtherKeyMember"></param>
            /// <returns></returns>
            public TAssociatonExpression OtherKey(Expression<Func<TOtherEntity, object>> fnOtherKeyMember)
            {
                var member = GetMember(fnOtherKeyMember);
                attribute.OtherKey = member.Name;
                return this as TAssociatonExpression;
            }
        }

        /// <summary>
        /// 一对多映射表达式
        /// </summary>
        /// <typeparam name="TOtherEntity"></typeparam>
        public class OneToManyExpression<TOtherEntity> : AssociationExpression<TOtherEntity, OneToManyExpression<TOtherEntity>, OneToManyAttribute>
        {
        }

        /// <summary>
        /// 一对一映射表达式
        /// </summary>
        /// <typeparam name="TOtherEntity"></typeparam>
        public class OneToOneExpression<TOtherEntity> : AssociationExpression<TOtherEntity, OneToOneExpression<TOtherEntity>, OneToOneAttribute>
        {
        }

        /// <summary>
        /// 多对一映射表达式
        /// </summary>
        /// <typeparam name="TOtherEntity"></typeparam>
        public class ManyToOneExpression<TOtherEntity> : AssociationExpression<TOtherEntity, ManyToOneExpression<TOtherEntity>, ManyToOneAttribute>
        {
        }
    }
}
