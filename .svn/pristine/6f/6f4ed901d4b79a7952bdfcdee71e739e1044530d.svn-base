using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace UWay.Skynet.Cloud.Data
{
    internal static class DataSetUtil
    {
        internal static void CheckArgumentNull<T>(T argumentValue, string argumentName) where T : class
        {
            if (null == argumentValue)
            {
                throw ArgumentNull(argumentName);
            }
        }

        private static T TraceException<T>(string trace, T e)
        {
            Debug.Assert(null != e, "TraceException: null Exception");
            return e;
        }

        private static T TraceExceptionAsReturnValue<T>(T e)
        {
            return TraceException("<comm.ADP.TraceException|ERR|THROW> '%ls'\n", e);
        }

        internal static ArgumentException Argument(string message)
        {
            return TraceExceptionAsReturnValue(new ArgumentException(message));
        }

        internal static ArgumentNullException ArgumentNull(string message)
        {
            return TraceExceptionAsReturnValue(new ArgumentNullException(message));
        }

        internal static ArgumentOutOfRangeException ArgumentOutOfRange(string message, string parameterName)
        {
            return TraceExceptionAsReturnValue(new ArgumentOutOfRangeException(parameterName, message));
        }

        internal static InvalidCastException InvalidCast(string message)
        {
            return TraceExceptionAsReturnValue(new InvalidCastException(message));
        }

        internal static InvalidOperationException InvalidOperation(string message)
        {
            return TraceExceptionAsReturnValue(new InvalidOperationException(message));
        }

        internal static NotSupportedException NotSupported(string message)
        {
            return TraceExceptionAsReturnValue(new NotSupportedException(message));
        }

        static internal ArgumentOutOfRangeException InvalidEnumerationValue(Type type, int value)
        {
            return ArgumentOutOfRange(string.Format("{0}DataSetLinq:{1}InvalidEnumerationValue:{0}", type.Name, value.ToString(System.Globalization.CultureInfo.InvariantCulture)), type.Name);
        }

        static internal ArgumentOutOfRangeException InvalidDataRowState(DataRowState value)
        {
#if DEBUG
            switch (value)
            {
                case DataRowState.Detached:
                case DataRowState.Unchanged:
                case DataRowState.Added:
                case DataRowState.Deleted:
                case DataRowState.Modified:
                    Debug.Assert(false, "valid DataRowState " + value.ToString());
                    break;
            }
#endif
            return InvalidEnumerationValue(typeof(DataRowState), (int)value);
        }

        static internal ArgumentOutOfRangeException InvalidLoadOption(LoadOption value)
        {
#if DEBUG
            switch (value)
            {
                case LoadOption.OverwriteChanges:
                case LoadOption.PreserveChanges:
                case LoadOption.Upsert:
                    Debug.Assert(false, "valid LoadOption " + value.ToString());
                    break;
            }
#endif
            return InvalidEnumerationValue(typeof(LoadOption), (int)value);
        }

        // only StackOverflowException & ThreadAbortException are sealed classes
        private static readonly Type s_stackOverflowType = typeof(StackOverflowException);
        private static readonly Type s_outOfMemoryType = typeof(OutOfMemoryException);
        private static readonly Type s_threadAbortType = typeof(System.Threading.ThreadAbortException);
        private static readonly Type s_nullReferenceType = typeof(NullReferenceException);
        private static readonly Type s_accessViolationType = typeof(AccessViolationException);
        private static readonly Type s_securityType = typeof(System.Security.SecurityException);

        static internal bool IsCatchableExceptionType(Exception e)
        {
            // a 'catchable' exception is defined by what it is not.
            Type type = e.GetType();

            return ((type != s_stackOverflowType) &&
                     (type != s_outOfMemoryType) &&
                     (type != s_threadAbortType) &&
                     (type != s_nullReferenceType) &&
                     (type != s_accessViolationType) &&
                     !s_securityType.IsAssignableFrom(type));
        }
    }
}
