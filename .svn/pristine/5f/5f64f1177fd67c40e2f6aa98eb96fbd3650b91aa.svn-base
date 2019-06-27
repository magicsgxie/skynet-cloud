using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UWay.Skynet.Cloud.Data.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    interface ISQLExceptionConverter
    {
        /// <summary> 
        /// Convert the given <see cref="System.Data.Common.DbException"/> into custom Exception. 
        /// </summary>
        /// <param name="dbExceptionContext">Available information during exception throw.</param>
        /// <returns> The resulting Exception to throw. </returns>
        Exception Convert(DbExceptionContextInfo dbExceptionContext);
    }
}
