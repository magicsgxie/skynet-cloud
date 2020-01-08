// ======================================================================
// 
//           filename : ImporterHeaderInfo.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-11 13:51
//           
//          
//           
//           
// 
// ======================================================================

using System.Collections.Generic;
using UWay.Skynet.Cloud.IE.Core;

namespace UWay.Skynet.Cloud.IE.Excel
{
    /// <summary>
    ///     导入列头设置
    /// </summary>
    public class ImporterHeaderInfo
    {
        /// <summary>
        ///     是否必填
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        ///     列名称
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        ///     列属性
        /// </summary>
        public ImporterHeaderAttribute ExporterHeader { get; set; }

        /// <summary>
        /// </summary>
        public Dictionary<string, dynamic> MappingValues { get; set; } = new Dictionary<string, dynamic>();

        /// <summary>
        ///     是否存在
        /// </summary>
        internal bool IsExist { get; set; }
    }
}