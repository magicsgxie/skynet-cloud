using System;

namespace UWay.Skynet.Cloud.Disposables
{
    /// <summary>
    /// 资源回收器接口
    /// </summary>
    public interface IDisposeCollector : IDisposable
    {
        /// <summary>
        /// 可回收的资源列表
        /// </summary>
        ICompositeDisposable Disposes { get; }
    }
}
