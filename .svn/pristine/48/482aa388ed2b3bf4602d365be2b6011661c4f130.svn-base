using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Reflection.Dynamic.Internal.Caching
{
    /// <summary>
    /// Represents an anonymous disposable.
    /// </summary>
    internal sealed class AnonymousDisposable : IDisposable
    {
        /// <summary>
        /// The dispose action.
        /// </summary>
        private readonly Action<bool> _dispose;

        /// <summary>
        /// A value indicating whether this <see cref="AnonymousDisposable"/> was already disposed.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousDisposable"/> class.
        /// </summary>
        /// <param name="dispose">The dispose action.</param>
        public AnonymousDisposable(Action<bool> dispose)
        {
            if (dispose == null)
                throw new ArgumentNullException("dispose");

            _dispose = dispose;

            _disposed = false;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="AnonymousDisposable"/> is reclaimed by garbage collection.
        /// </summary>
        ~AnonymousDisposable()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose this <see cref="AnonymousDisposable"/>.
        /// </summary>
        /// <param name="disposing">A value indicating whether disposing is in progress.</param>
        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _dispose(disposing);

            _disposed = true;
        }

        #region IDisposable Members

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
