using UWay.Skynet.Cloud.Notify;
using UWay.Skynet.Cloud.Tasks.Help;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace UWay.Skynet.Cloud.Tasks
{
    public class TaskManager
    {
        /// <summary>
        ///     日志
        /// </summary>
        public ILoggerFactory LoggerFactory { get; set; }

        /// <summary>
        ///     任务日志记录器
        /// </summary>
        public ILogger TaskManagerLogger { get; set; }

        /// <summary>
        ///     通知器
        /// </summary>
        public INotifier Notifier { get; set; }

        /// <summary>
        ///     任务日志记录器
        /// </summary>
        public ILogger TaskLogger { get; set; }

        /// <summary>
        ///     任务类型集合
        /// </summary>

        public ConcurrentDictionary<string, Type> TaskTypes =
           new ConcurrentDictionary<string, Type>();

        /// <summary>
        /// 任务执行
        /// </summary>
        public Action<TaskBase, Action<TaskBase>> TaskRunAction = (instance, taskCompleteAction) => Task.Run(() =>
        {
            try
            {
                instance.Run();
            }
            catch (Exception ex)
            {
                instance.Exception = ex;
                instance.Logger.LogError(ex,"任务原型");
            }
            finally
            {
                if (taskCompleteAction != null)
                    taskCompleteAction.Invoke(instance);
            }
        });

        /// <summary>
        /// 任务执行完成
        /// </summary>
        public Action<TaskBase> TaskCompleteAction { get; set; }

        /// <summary>
        /// 初始化执行
        /// </summary>
        public Action<TaskManager> InitAction = (taskManager) =>
        {
            foreach (
                    var currentassembly in
                    AppDomain.CurrentDomain.GetAssemblies().Where(p => p.FullName.StartsWith("Skynet.Cloud.")))
                try
                {
                    var baseType = typeof(TaskBase);
                    currentassembly.GetTypes()
                        .Where(p => p.IsClass && p.IsSubclassOf(baseType))
                        .Each(t =>
                        {
                            var instance = (TaskBase)Activator.CreateInstance(t);
                            if (instance == null)
                            {
                                taskManager.TaskManagerLogger.LogError(
                                    "CreateInstance 失败！ AssemblyFullName:{0}\tFullName:{1}", t.Assembly.FullName,
                                    t.FullName);
                            }
                            else
                            {
                                var key = string.IsNullOrEmpty(instance.Keyword) ? t.Name : instance.Keyword;
                                taskManager.TaskTypes.TryAdd(key, t);
                                taskManager.TaskManagerLogger.LogDebug(
                                    "TaskTypes Add Key:{0}\tFullName:{1}", key,
                                    t.FullName);
                            }
                        });
                }
                catch (ReflectionTypeLoadException ex)
                {
                    taskManager.TaskManagerLogger.LogError( ex, "映射错误：{}", currentassembly.FullName);
                    if (ex.LoaderExceptions.Length <= 0) return;
                    foreach (var loaderEx in ex.LoaderExceptions)
                        taskManager.TaskManagerLogger.LogError(loaderEx, ex.Message);
                }
                catch (Exception ex)
                {
                    taskManager.TaskManagerLogger.LogError( ex, "初始化执行出错");
                }
        };

        /// <summary>
        /// 启动任务
        /// </summary>
        /// <param name="keyword">任务Key</param>
        /// <param name="parameter">参数</param>
        /// <param name="notifyInfo">通知参数</param>
        public virtual void Start(string keyword, object parameter = null, INotifyInfo notifyInfo = null)
        {
            if (TaskTypes.ContainsKey(keyword))
            {
                var type = TaskTypes[keyword];
                var instance = (TaskBase)Activator.CreateInstance(type);
                instance.Logger = TaskLogger;
                instance.Notifier = Notifier;
                if (notifyInfo != null)
                {
                    instance.NotifyInfo = notifyInfo;
                    //当通知标题没有赋值时，使用Task Title
                    if (string.IsNullOrEmpty(instance.NotifyInfo.Title))
                        instance.NotifyInfo.Title = instance.Title;
                }
                instance.Parameter = parameter;
                //执行
                TaskRunAction.Invoke(instance, TaskCompleteAction);
            }
            else
                throw new KeyNotFoundException(keyword + " 不存在！");
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {
            InitAction.Invoke(this);
        }
    }
}
