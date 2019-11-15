using UWay.Skynet.Cloud.Notify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace UWay.Skynet.Cloud.Tasks
{
    /// <summary>
    /// 任务接口
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// 任务标题
        /// </summary>
        string Title { get; }
        /// <summary>
        /// 任务关键字，为空则使用类名
        /// </summary>
        string Keyword { get; }

        /// <summary>
        /// 通知器
        /// </summary>
        INotifier Notifier { set; get; }

        /// <summary>
        /// 通知信息
        /// </summary>
        INotifyInfo NotifyInfo { get; set; }

        /// <summary>
        /// 日志记录器
        /// </summary>
        ILogger Logger { get; set; }

        /// <summary>
        /// 任务参数
        /// </summary>
        object Parameter { get; set; }

        /// <summary>
        /// 执行
        /// </summary>
        void Run();
    }
}
