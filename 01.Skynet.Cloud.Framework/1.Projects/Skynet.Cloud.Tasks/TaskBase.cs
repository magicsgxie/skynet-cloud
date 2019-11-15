
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
    /// 任务基类
    /// </summary>
    public abstract class TaskBase : ITask
    {
        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception { get; internal set; }
        /// <summary>
        /// 任务关键字，为空则使用类名
        /// </summary>
        public virtual string Keyword
        {
            get
            {
                return null;
            }
        }
        /// <summary>
        /// 任务标题
        /// </summary>
        public virtual string Title
        {
            get
            {
                return null;
            }
        }
        /// <summary>
        /// 通知器
        /// </summary>
        public INotifier Notifier
        {
            get; set;
        }
        /// <summary>
        /// 通知信息
        /// </summary>
        public INotifyInfo NotifyInfo
        {
            get; set;
        }
        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger Logger { get; set; }
        /// <summary>
        /// <summary>
        /// 任务参数
        /// </summary>
        public virtual object Parameter { get; set; }

        /// <summary>
        /// 执行程序逻辑
        /// </summary>
        public virtual void Run()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 报告进度
        /// </summary>
        /// <param name="percentage">进度百分比</param>
        /// <param name="message">消息文本</param>
        public virtual void ReportProgress(int? percentage, string message = null)
        {
            if (Notifier != null && NotifyInfo != null)
            {
                if (percentage.HasValue)
                    NotifyInfo.TaskPercentage = percentage.Value;

                if (NotifyInfo.TaskPercentage.HasValue)
                {
                    if (NotifyInfo.TaskPercentage.Value > 100)
                    {
                        NotifyInfo.TaskPercentage = 100;
                    }
                    if (NotifyInfo.TaskPercentage.Value < 0)
                    {
                        NotifyInfo.TaskPercentage = 0;
                    }
                }

                if (!string.IsNullOrEmpty(message))
                {
                    NotifyInfo.Message = message;
                }
                NotifyInfo.UpdateTime = DateTime.Now;
                Notifier.NotifyTo(NotifyInfo, NotifyInfo.Receiver);
            }

        }
    }
}
