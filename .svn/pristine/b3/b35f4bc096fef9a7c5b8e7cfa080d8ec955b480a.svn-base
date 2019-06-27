using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud
{
    public static partial class Guard
    {
        [DebuggerStepThrough]
        public static void NotNull(object argumentValue,
                                         string argumentName)
        {
            if (argumentValue == null) throw new ArgumentNullException(argumentName);
        }


        [DebuggerStepThrough]
        public static void NotNullOrEmpty(string argumentValue,
                                                 string argumentName)
        {
            if (argumentValue == null || argumentValue.Length == 0) throw new ArgumentNullException(argumentName);
        }



        [DebuggerStepThrough]
        internal static void IsFalse(bool condition)
        {
            if (condition)
            {
                Fail(null);
            }
        }

        [DebuggerStepThrough]
        internal static void IsTrue(bool condition)
        {
            if (!condition)
            {
                Fail(null);
            }
        }

        [DebuggerStepThrough]
        internal static void IsTrue(bool condition, [Localizable(false)]string message)
        {
            if (!condition)
            {
                Fail(message);
            }
        }

        [DebuggerStepThrough]
        internal static void Fail([Localizable(false)]string message)
        {
            throw new Exception(message);
        }
    }



    internal class Null
    {
        private Null() { }
        public static readonly Null Value = new Null();
    }

    /// <summary>
    /// 组件生命周期类型
    /// </summary>
    public class LifestyleType
    {
        /// <summary>
        /// 得到或设置组件的确实生命周期类型
        /// </summary>
        public static LifestyleFlags Default { get; set; }

        static LifestyleType()
        {
            Default = LifestyleFlags.Singleton;
        }

        internal static LifestyleFlags GetGenericLifestyle(LifestyleFlags lifestyle)
        {
            if (lifestyle == LifestyleFlags.Singleton)
                return LifestyleFlags.GenericSingleton;
            if (lifestyle == LifestyleFlags.Transient)
                return LifestyleFlags.GenericTransient;
            if (lifestyle == LifestyleFlags.Thread)
                return LifestyleFlags.GenericThread;
            return lifestyle;
        }

        internal static LifestyleFlags GetLifestyle(LifestyleFlags lifestyle)
        {
            if (lifestyle == LifestyleFlags.GenericSingleton)
                return LifestyleFlags.Singleton;
            if (lifestyle == LifestyleFlags.GenericTransient)
                return LifestyleFlags.Transient;
            if (lifestyle == LifestyleFlags.GenericThread)
                return LifestyleFlags.Thread;
            return lifestyle;
        }
    }

    /// <summary>
    /// 组件生命周期类型枚举
    /// </summary>
    public enum LifestyleFlags
    {
        /// <summary>
        /// 单利
        /// </summary>
        Singleton,
        /// <summary>
        /// 线程内单利
        /// </summary>
        Thread,
        /// <summary>
        /// 临时
        /// </summary>
        Transient,

        /// <summary>
        /// 泛型单利
        /// </summary>
        GenericSingleton,
        /// <summary>
        /// 泛型线程内单利
        /// </summary>
        GenericThread,
        /// <summary>
        /// 泛型临时
        /// </summary>
        GenericTransient,
    }
}
