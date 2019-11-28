using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Trigger
{
    /// <summary>
    /// Job Executor
    /// </summary>
    public interface IJobExecutor
    {
        /// <summary>
        /// 开始任务
        /// </summary>
        void StartJob();

        /// <summary>
        ///  结束任务
        /// </summary>
        void StopJob();
    }
}
