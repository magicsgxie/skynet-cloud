
using UWay.Skynet.Cloud.Notify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace UWay.Skynet.Cloud.Tasks.Builder
{
    public class TaskBuilder
    {
        INotifier Notifier { get; set; }
        ILoggerFactory TaskManagerLogger { get; set; }
        //ILogger TaskManagerLogger { get; set; }
        Action<TaskBase, Action<TaskBase>> TaskRunAction { get; set; }
        Action<TaskBase> TaskCompleteAction { get; set; }
        Action<TaskManager> InitAction { get; set; }
        /// <summary>
        ///     创建实例
        /// </summary>
        /// <returns></returns>
        public static TaskBuilder Create()
        {
            return new TaskBuilder();
        }

        /// <summary>
        ///     设置通知器
        /// </summary>
        /// <param name="notifier">通知逻辑</param>
        /// <returns></returns>
        public TaskBuilder WithNotifier(INotifier notifier)
        {
            Notifier = notifier;
            return this;
        }

        /// <summary>
        ///     设置日志记录器
        /// </summary>
        /// <param name="taskLogger">任务日志记录器</param>
        /// <param name="taskManagerLogger">任务管理器日志记录器</param>
        /// <returns></returns>
        public TaskBuilder WithLoggers(ILoggerFactory loggerFactory)
        {
            TaskManagerLogger = loggerFactory;
            //TaskLogger = taskLogger;
            return this;
        }

        /// <summary>
        /// 设置任务执行函数逻辑
        /// </summary>
        /// <param name="taskRunAction">任务执行逻辑</param>
        /// <returns></returns>
        public TaskBuilder WithTaskRunAction(Action<TaskBase, Action<TaskBase>> taskRunAction)
        {
            TaskRunAction = taskRunAction;
            return this;
        }

        /// <summary>
        /// 设置执行完成处理逻辑函数
        /// </summary>
        /// <param name="taskCompleteAction">执行完成处理逻辑函数</param>
        /// <returns></returns>
        public TaskBuilder WithTaskCompleteAction(Action<TaskBase> taskCompleteAction)
        {
            TaskCompleteAction = taskCompleteAction;
            return this;
        }

        /// <summary>
        /// 设置任务管理器初始化逻辑
        /// </summary>
        /// <param name="initAction">初始化处理逻辑函数</param>
        /// <returns></returns>
        public TaskBuilder WithInitAction(Action<TaskManager> initAction)
        {
            InitAction = initAction;
            return this;
        }

        /// <summary>
        /// 编译执行，返回TaskManager对象
        /// </summary>
        /// <returns></returns>
        public TaskManager Build()
        {
            var taskManager = new TaskManager()
            {
                Notifier = Notifier,
                LoggerFactory = TaskManagerLogger,
                TaskManagerLogger = TaskManagerLogger.CreateLogger<TaskManager>(),
                TaskCompleteAction = TaskCompleteAction
                //TaskRunAction = TaskRunAction,
                //InitAction = InitAction
            };
            if (TaskRunAction != null)
            {
                taskManager.TaskRunAction = TaskRunAction;
            }
            if (InitAction != null)
                taskManager.InitAction = InitAction;

            taskManager.Init();
            return taskManager;
        }
    }
}
