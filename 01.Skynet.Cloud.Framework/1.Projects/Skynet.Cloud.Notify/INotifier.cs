using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Notify
{
    /// <summary>
    /// 通知者
    /// </summary>
    public interface INotifier
    {
        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="notify"></param>
        /// <param name="group"></param>
        void NotifyTo(INotifyInfo notify, string group = null);
        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="notifies"></param>
        /// <param name="group"></param>
        void NotifyTo(IList<INotifyInfo> notifies, string group = null);

        ///// <summary>
        ///// 获取通知列表
        ///// </summary>
        ///// <param name="wherePredicate"></param>
        ///// <param name="pageIndex"></param>
        ///// <param name="pageSize"></param>
        ///// <returns></returns>
        //List<T> GetNofityList(Expression<Func<T, bool>> wherePredicate = null, int pageIndex = 0, int pageSize = 10);
    }
}
