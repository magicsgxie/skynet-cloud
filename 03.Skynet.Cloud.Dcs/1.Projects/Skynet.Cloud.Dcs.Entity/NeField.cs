using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    /// <summary>
    /// 网元字段
    /// </summary>
    
    public class NeField
    {
        /// <summary>
        /// 网络类型
        /// </summary>
        
        public int NeType
        {
            set;
            get;
        }

        /// <summary>
        /// 网络级别，0=表示包含多个级别 NE_COMBINATION
        /// </summary>
        
        public int NeLevel
        {
            set;
            get;
        }

        /// <summary>
        /// 厂家
        /// </summary>
        
        public string VendorVersion
        {
            set;
            get;
        }

        /// <summary>
        /// 业务类型
        /// </summary>

        public int BusinessType
        {
            set;
            get;
        }

        /// <summary>
        /// 数据源类型 1：性能数据 ...
        /// </summary>
        
        public int DataSource
        {
            set;
            get;
        }
    }
}
