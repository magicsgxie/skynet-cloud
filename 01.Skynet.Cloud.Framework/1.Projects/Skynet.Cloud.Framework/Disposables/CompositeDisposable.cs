using System;
using System.Collections.Generic;

namespace UWay.Skynet.Cloud.Disposables
{
    /// <summary>
    /// 可复合的Disposable对象接口
    /// </summary>
    public interface ICompositeDisposable:IDisposable
    {
        /// <summary>
        /// 添加Disposable对象
        /// </summary>
        /// <param name="item"></param>
        void AddDisposable(IDisposable item);
    }

    /// <summary>
    /// 可复合的Disposable对象
    /// </summary>
    [Serializable]
    public class CompositeDisposable : BooleanDisposable, ICompositeDisposable
    {
        private readonly Stack<WeakReference> Items;
        /// <summary>
        /// 构造对象
        /// </summary>
        public CompositeDisposable()
        {
            Items = new Stack<WeakReference>();
        }

        /// <summary>
        /// 构造对象
        /// </summary>
        /// <param name="capacity"></param>
        public CompositeDisposable(int capacity)
        {

            Items = new Stack<WeakReference>(capacity);
        }

        /// <summary>
        /// 添加Disposable对象
        /// </summary>
        /// <param name="item"></param>
        public void AddDisposable(IDisposable item)
        {
            CheckNotDisposed();
            
            if (item != null)
                Items.Push(new WeakReference(item));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                while (Items.Count > 0)
                {
                    WeakReference reference = Items.Pop();
                    IDisposable item = (IDisposable)reference.Target;
                    if (reference.IsAlive)
                        item.Dispose();
                }
            }
        }
    }
}
