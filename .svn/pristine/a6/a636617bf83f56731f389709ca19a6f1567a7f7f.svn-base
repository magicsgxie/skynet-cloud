using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UWay.Skynet.Cloud.Data.Linq;
using UWay.Skynet.Cloud.Data.Mapping;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data
{

    class DbSet<T> : Query<T>, IDbSet<T>
    {
        private readonly IEntityMapping entity;
        private readonly InternalDbContext dbContext;


        static Func InsertMethod;

        public DbSet(InternalDbContext dbContext, IEntityMapping entity)
            : base(dbContext, typeof(IDbSet<T>))
        {
            this.dbContext = dbContext;
            this.entity = entity;
            if (InsertMethod == null && entity.PrimaryKeys.Length == 1 && entity.PrimaryKeys[0].IsGenerated)
                InsertMethod = typeof(DbSet).GetMethod("Insert", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(typeof(T), entity.PrimaryKeys[0].MemberType).GetFunc();
        }
        /// <summary>
        /// IEntityModel接口对象
        /// </summary>
        public IEntityMapping Mapping
        {
            get { return this.entity; }
        }
        /// <summary>
        /// IDbContext接口对象
        /// </summary>
        public IDbContext DbContext
        {
            get { return this.dbContext; }
        }

        public DbConnection Connection
        {
            get { return dbContext.Connection; }
        }

        public IDbHelper DbHelper
        {
            get { return dbContext.DbHelper; }
        }

        /// <summary>
        /// 实体类型
        /// </summary>
        public Type EntityType
        {
            get { return this.entity.EntityType; }
        }

        public T Get(object id)
        {
            var dbProvider = this.dbContext;
            if (dbProvider != null)
            {
                IEnumerable<object> keys = id as IEnumerable<object>;
                if (keys == null)
                    keys = new object[] { id };
                Expression query = dbProvider.ExpressionBuilder.GetPrimaryKeyQuery(entity, this.Expression, keys.Select(v => Expression.Constant(v)).ToArray());
                return (this.DbContext as IQueryProvider).Execute<T>(query);
            }
            return default(T);
        }

        object IReqository.Get(object id)
        {
            return this.Get(id);
        }



        /// <summary>
        /// 向数据库中插入一条T型记录
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert(object instance)
        {
            Guard.NotNull(instance, "instance");
            if (entity.PrimaryKeys.Length == 1)
            {
                var pkModel = entity.PrimaryKeys[0];
                if (pkModel.IsGenerated)
                {
                    var instanceType = instance.GetType();
                    var pk = pkModel.Member;
                    var theSame = instanceType == entity.EntityType;
                    if (!theSame)
                        pk = instanceType.GetMember(pk.Name).FirstOrDefault();

                    if (pk != null)
                    {
                        var o = Expression.Parameter(entity.EntityType, "o");
                        LambdaExpression pkSelector = Expression.Lambda(Expression.MakeMemberAccess(o, pkModel.Member), o);

                        var pkValue = InsertMethod(null, this, instance, pkSelector);
                        if (pkValue == null)
                            return 0;
                        if (theSame)
                            pkModel.SetValue(instance, pkValue);
                        else
                            pk.GetSetter()(instance, pkValue);
                        return 1;
                    }

                }
            }
            return DbSet.Insert<T, int>(this, instance, null);
        }

        public S Insert<S>(object instance, Expression<Func<T, S>> resultSelector)
        {
            return DbSet.Insert<T, S>(this, instance, resultSelector);
        }

        public int Delete(object instance)
        {
            return DbSet.Delete(this, instance, null);
        }

        public int Delete(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Delete<T>(this, predicate);
        }

        public int Delete(object instance, Expression<Func<T, bool>> deleteCheck)
        {
            return DbSet.Delete<T>(this, instance, deleteCheck);
        }


        public int Update(object instance)
        {
            return DbSet.Update<T, int>(this, instance, null, null);
        }

        public int Update(object instance, Expression<Func<T, bool>> updateCheck)
        {
            return DbSet.Update<T, int>(this, instance, updateCheck, null);
        }

        public S Update<S>(object instance, Expression<Func<T, bool>> updateCheck, Expression<Func<T, S>> resultSelector)
        {
            return DbSet.Update<T, S>(this, instance, updateCheck, resultSelector);
        }

        public IEnumerable<int> Batch<M>(IEnumerable<M> instances, Expression<Func<IRepository<T>, M, int>> fnOperation)
        {
            return DbSet.Batch<T, M>(this, instances, fnOperation);
        }

        public IDbSet<T> Associate(Expression<Func<T, IEnumerable>> memerQuery)
        {
            dbContext.AssociateWith<T>(memerQuery);
            return this;
        }

        public IDbSet<T> Include(Expression<Func<T, object>> fnMember)
        {
            dbContext.IncludeWith<T>(fnMember);
            return this;
        }

        public IDbSet<T> Include(Expression<Func<T, object>> fnMember, bool deferLoad)
        {
            dbContext.IncludeWith<T>(fnMember, deferLoad);
            return this;
        }

        IDbSet IDbSet.Include(string memberName)
        {
            var member = this.entity.Members.FirstOrDefault(p => string.Equals(p.Member.Name, memberName, StringComparison.InvariantCultureIgnoreCase));
            if (member == null)
                throw new InvalidOperationException("Member name:'" + memberName + "' not exists in type '" + entity.EntityType.FullName + ".");

            dbContext.Include(member.Member);
            return this;
        }

        IDbSet IDbSet.Include(System.Reflection.MemberInfo member)
        {
            Guard.NotNull(member, "member");
            dbContext.Include(member);
            return this;
        }

        public IDbSet<T> IncludeWith<TEntity>(Expression<Func<TEntity, object>> fnMember)
        {
            dbContext.IncludeWith<TEntity>(fnMember);
            return this;
        }

        public IDbSet<T> IncludeWith<TEntity>(Expression<Func<TEntity, object>> fnMember, bool deferLoad)
        {
            dbContext.IncludeWith<TEntity>(fnMember, deferLoad);
            return this;
        }
    }
}
