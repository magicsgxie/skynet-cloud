using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace UWay.Skynet.Cloud.Linq
{
    /// <summary>
    /// 查询分页定义
    /// </summary>
    
    [Serializable]
    public class Pagination : ICloneable
    {
        private bool _paging = true;
        /// <summary>
        /// 构造函数
        /// </summary>
        public Pagination()
        {
            CurrentPageIndex = 1;
            PageSize = 10;
            SortExpress = new List<string>();
            GroupExpress = new List<string>();
            // QueryParameters = new List<QueryParameter>();
        }

        /// <summary>
        /// 是否分页
        /// </summary>
       
        public bool Paging
        {
            get { return _paging && PageSize > 0; }
            set { _paging = value; }
        }

        /// <summary>
        /// 系统ID
        /// </summary>
       
        public int ApplicationId { get; set; }

        /// <summary>
        /// 数据数量，涉及总行数变更的查询时,赋值为null。备注：为NULL是查询总行数
        /// </summary>
       
        public long? RowCount { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
       
        public int PageSize { get; set; }

        private int _currentPageIndex;

        /// <summary>
        /// 当前页
        /// </summary>
       
        public int CurrentPageIndex { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (RowCount.HasValue)
                    return (int)(RowCount.Value / PageSize) + ((RowCount.Value % PageSize) == 0 ? 0 : 1);
                else
                    return 0;
            }
        }

        /// <summary>
        /// 排序字段
        /// </summary>
       
        public List<string> SortExpress { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
       
        public List<string> GroupExpress { get; set; }


        /// <summary>
        /// 按照指定字段排序，如果字段已经存在排序表达式中，则逆反排序方向,只适用Linq
        /// </summary>
        /// <param name="expression">排序字段</param>
        /// <returns></returns>
        public Pagination OrderBy(string expression)
        {
            //var item = SortExpress.Select( c=>c.Trim().Split(' ')).Where( c=>c[0] == expression).FirstOrDefault();

            //bool desc = false;
            //if (item != null)
            //{
            //    desc = item[1] != "desc";
            //}
            //SortExpress = new List<string>() { expression + (desc ? " desc" : " asc") };
            if (SortExpress == null)
            {
                SortExpress = new List<string>();
            }
            SortExpress.Add(expression + " asc");
            return this;
        }

        /// <summary>
        /// 排序，只适用于EF框架
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public Pagination OrderBy(string expression, bool desc)
        {
            //SortExpress = new List<string>() { expression + (desc ? " desc" : " asc") };
            if (SortExpress == null)
            {
                SortExpress = new List<string>();
            }
            SortExpress.Add(expression + " desc");
            return this;
        }

        /// <summary>
        /// 增加一个排序项，只适用于EF框架
        /// </summary>
        /// <typeparam name="T">排序类型</typeparam>
        /// <param name="expression">排序表达式</param>
        /// <returns><see cref="Pagination"/></returns>
        /// <example>
        /// <code><![CDATA[
        ///    var paging = new Pagination();
        ///    paging.Paging = true;
        ///    paging.PageSize = 25;
        ///    paging.OrderBy<Customer>(c => c.ContactName);
        /// ]]>
        /// </code>
        /// </example>
        public Pagination OrderBy<T>(params Expression<Func<T, object>>[] expression)
        {
            return OrderBy<T>(expression, false);
        }


        /// <summary>
        /// 只适用于EF框架
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        private Pagination GroupBy<T>(Expression<Func<T, object>>[] expression)
        {
            foreach (var item in expression)
            {
                MemberExpression mexp = null;
                if (item.Body.NodeType == ExpressionType.Convert)
                {
                    var temp = (UnaryExpression)item.Body;
                    if (temp.Operand.NodeType == ExpressionType.MemberAccess)
                        mexp = (MemberExpression)temp.Operand;
                }
                else
                    mexp = item.Body as MemberExpression;

                if (mexp != null && mexp.Member != null)
                {
                    //var s = string.Format("{0} {1}", mexp.Member.Name, desc ? "desc" : "asc");
                    GroupExpress.Add(mexp.Member.Name);
                }
                else
                {
                    throw new ArgumentException("没有找到合适的属性");
                }
            }
            return this;
        }

        /// <summary>
        /// 增加一个倒排序项,只适用于EF框架
        /// </summary>
        /// <typeparam name="T">排序类型</typeparam>
        /// <param name="expression">排序表达式</param>
        /// <returns><see cref="Pagination"/></returns>
        /// <example>
        /// <code><![CDATA[
        ///    var paging = new Pagination();
        ///    paging.Paging = true;
        ///    paging.PageSize = 25;
        ///    paging.OrderByDesc<Customer>(c => c.ContactName);
        /// ]]>
        /// </code>
        /// </example>
        public Pagination OrderByDesc<T>(params Expression<Func<T, object>>[] expression)
        {
            return OrderBy<T>(expression, true);
        }

        /// <summary>
        /// 只适用于EF框架
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        private Pagination OrderBy<T>(Expression<Func<T, object>>[] expression, bool desc)
        {
            foreach (var item in expression)
            {
                MemberExpression mexp = null;
                if (item.Body.NodeType == ExpressionType.Convert)
                {
                    var temp = (UnaryExpression)item.Body;
                    if (temp.Operand.NodeType == ExpressionType.MemberAccess)
                        mexp = (MemberExpression)temp.Operand;
                }
                else
                    mexp = item.Body as MemberExpression;

                if (mexp != null && mexp.Member != null)
                {
                    var s = string.Format("{0} {1}", mexp.Member.Name, desc ? "desc" : "asc");
                    SortExpress.Add(s);
                }
                else
                {
                    throw new ArgumentException("没有找到合适的属性");
                }
            }
            return this;
        }
        /// <summary>
        /// 只适用于EF框架
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<T> ParseQueryPaging<T>(IQueryable query)
        {
            return ParseQuery(query) as IQueryable<T>;
        }


        /// <summary>
        /// 将排序应用到<seealso cref="IQueryable"/>只适用于EF框架
        /// </summary>
        /// <param name="query"><seealso cref="IQueryable"/></param>
        /// <returns><seealso cref="IQueryable"/></returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        ///  public List<CustomerOrderView> GetCustomerOrders( UWay.Ufa.Framework.Query.Pagination pagenation)
        ///  {
        ///      var cs = CreateQuery<Customer>();//EF需要先将查询对象定义成变量，否则会出现无法解析的错误
        ///      var os = CreateQuery<Order>();//EF需要先将查询对象定义成变量，否则会出现无法解析的错误
        ///  
        ///      var q = from c in cs
        ///              from o in os
        ///              where c.CustomerID == o.CustomerID
        ///              select new { o.CustomerID, o.OrderID, c.ContactName }; //先查询一个匿名对象，不支持直接返回视图对象。
        ///  
        ///      var count = q.Count();
        ///  
        ///      q = q.Paging(pagenation);
        ///  
        ///      return (from c in q
        ///              select new CustomerOrderView { CustomerId = c.CustomerID,
        ///                                             CustomerName = c.ContactName, 
        ///                                             OrderId = c.OrderID }
        ///             ).ToList();//返回视图对象。
        ///  }
        ///  
        /// //.....
        /// using (var scope = new DbConnectionScope(ConnectionName))
        /// {
        ///     using (var repository = new CustomerRepository())
        ///     {
        ///         var p = new UWay.Ufa.Framework.Query.Pagination() { Paging = true, 
        ///                                                             PageSize = 25 };
        ///         p.OrderBy<Customer>(c => c.ContactName);
        ///
        ///         var result = repository.GetCustomerOrders(p);
        ///         
        ///         Assert.IsTrue(p.RowCount.HasValue && p.RowCount.Value > 0);
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example> 
        public IQueryable ParseQuery(IQueryable query)
        {
            if (Paging)
            {
                if (!RowCount.HasValue)
                    RowCount = query.Count();
                if (CurrentPageIndex - 1 < 0)
                    throw new ArgumentOutOfRangeException("当前页不能小于0");
                var q = query.Skip((CurrentPageIndex - 1) * PageSize).Take(PageSize);
//#if DEBUG
//                System.Diagnostics.Trace.TraceInformation(q.ToString());
//#endif
                return q;
            }
            else
                return query;
        }

        /// <summary>
        /// 将当前分页信息复制到另一个分页实例
        /// </summary>
        /// <param name="paging">目标实例</param>
        public void ApplyTo(Pagination paging)
        {
            paging.ApplicationId = this.ApplicationId;
            paging.CurrentPageIndex = this.CurrentPageIndex;
            paging.PageSize = this.PageSize;
            paging.RowCount = this.RowCount;
            paging.SortExpress = this.SortExpress;
            paging.GroupExpress = this.GroupExpress;
        }

        #region ICloneable Members

        /// <summary>
        /// 浅表Clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            //string exp = this.SortExpress == null ? string.Empty : SortExpress.Concat(" ");
            //string gp = this.GroupExpress == null ? string.Empty : GroupExpress.Concat(" "); ;
            string val = string.Format("{0},{1},{2},{3},{4},{5}",
                            this.ApplicationId, this.CurrentPageIndex,
                            this.PageCount, this.PageSize, this.Paging, this.RowCount);
            return val.GetHashCode();

        }
    }

    
    
}
