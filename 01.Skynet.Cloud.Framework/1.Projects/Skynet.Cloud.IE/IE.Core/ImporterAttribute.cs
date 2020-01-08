// ======================================================================
// 
//           filename : ImporterAttribute.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-11 13:51
// 
// ======================================================================

using System;

namespace UWay.Skynet.Cloud.IE.Core
{
    /// <summary>
    /// 导入
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ImporterAttribute : Attribute
    {
        /// <summary>
        ///     表头位置
        /// </summary>
        public int HeaderRowIndex { get; set; } = 1;
    }
}