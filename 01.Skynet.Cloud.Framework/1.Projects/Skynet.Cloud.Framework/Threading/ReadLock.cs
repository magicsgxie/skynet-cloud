using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Threading.Internal;

namespace UWay.Skynet.Cloud.Threading
{
    /// <summary>
    /// 
    /// </summary>
    public static class ReadWriteLockSlimExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sem"></param>
        /// <param name="a"></param>
        public static void Readonly(this ReaderWriterLockSlim sem, Action a)
        {
            if (sem == null)
                throw new ArgumentNullException("sem");
            if (a == null)
                throw new ArgumentNullException("a");

            using (new ReadOnlyLock(sem))
                a();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sem"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static T Readonly<T>(this ReaderWriterLockSlim sem, Func<T> f)
        {
            if (sem == null)
                throw new ArgumentNullException("sem");
            if (f == null)
                throw new ArgumentNullException("f");

            using (new ReadOnlyLock(sem))
                return f();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sem"></param>
        /// <param name="a"></param>
        public static void Read(this ReaderWriterLockSlim sem, Action a)
        {
            if (sem == null)
                throw new ArgumentNullException("sem");
            if (a == null)
                throw new ArgumentNullException("a");

            using (new ReadLock(sem))
                a();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sem"></param>
        /// <param name="a"></param>
        public static void Write(this ReaderWriterLockSlim sem, Action a)
        {
            if (sem == null)
                throw new ArgumentNullException("sem");
            if (a == null)
                throw new ArgumentNullException("a");

            using (new WriteLock(sem))
                a();
        }
    }

}


namespace UWay.Skynet.Cloud.Threading.Internal
{
    static class Locks
    {
        public static void GetReadLock(ReaderWriterLockSlim locks)
        {
            bool lockAcquired = false;
            while (!lockAcquired)
                lockAcquired = locks.TryEnterUpgradeableReadLock(1);
        }

        public static void GetReadOnlyLock(ReaderWriterLockSlim locks)
        {
            bool lockAcquired = false;
            while (!lockAcquired)
                lockAcquired = locks.TryEnterReadLock(1);
        }

        public static void GetWriteLock(ReaderWriterLockSlim locks)
        {
            bool lockAcquired = false;
            while (!lockAcquired)
                lockAcquired = locks.TryEnterWriteLock(1);
        }

        public static void ReleaseReadOnlyLock(ReaderWriterLockSlim locks)
        {
            if (locks.IsReadLockHeld)
                locks.ExitReadLock();
        }

        public static void ReleaseReadLock(ReaderWriterLockSlim locks)
        {
            if (locks.IsUpgradeableReadLockHeld)
                locks.ExitUpgradeableReadLock();
        }

        public static void ReleaseWriteLock(ReaderWriterLockSlim locks)
        {
            if (locks.IsWriteLockHeld)
                locks.ExitWriteLock();
        }

        public static void ReleaseLock(ReaderWriterLockSlim locks)
        {
            ReleaseWriteLock(locks);
            ReleaseReadLock(locks);
            ReleaseReadOnlyLock(locks);
        }

        public static ReaderWriterLockSlim GetLockInstance()
        {
            return GetLockInstance(LockRecursionPolicy.SupportsRecursion);
        }

        public static ReaderWriterLockSlim GetLockInstance(LockRecursionPolicy recursionPolicy)
        {
            return new ReaderWriterLockSlim(recursionPolicy);
        }
    }

    internal struct ReadOnlyLock : IDisposable
    {
        private readonly ReaderWriterLockSlim Mutex;
        public ReadOnlyLock(ReaderWriterLockSlim mutex)
        {
            Mutex = mutex;
            Locks.GetReadOnlyLock(mutex);
        }

        public void Dispose()
        {
            Locks.ReleaseReadOnlyLock(Mutex);
        }
    }

    internal struct WriteLock : IDisposable
    {
        private readonly ReaderWriterLockSlim Mutex;
        public WriteLock(ReaderWriterLockSlim mutex)
        {
            Mutex = mutex;
            Locks.GetWriteLock(mutex);
        }

        public void Dispose()
        {
            Locks.ReleaseWriteLock(Mutex);
        }
    }

    internal struct ReadLock : IDisposable
    {
        private readonly ReaderWriterLockSlim Mutex;
        public ReadLock(ReaderWriterLockSlim mutex)
        {
            Mutex = mutex;
            Locks.GetReadLock(mutex);
        }

        public void Dispose()
        {
            Locks.ReleaseReadLock(Mutex);
        }
    }
}