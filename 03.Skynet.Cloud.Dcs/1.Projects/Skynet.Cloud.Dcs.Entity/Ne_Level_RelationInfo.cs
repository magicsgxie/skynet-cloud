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
    /// 网元粒度关联的信息
    /// </summary>
    
    public class NeLevelRelInfo
    {
        /// <summary>
        /// 网元粒度
        /// </summary>
        
        public int NeType { get; set; }
        /// <summary>
        /// 查询粒度
        /// </summary>
        
        public int QueryNeLevel { get; set; }
        /// <summary>
        /// 数据粒度
        /// </summary>
        
        public int DataNeLevel { get; set; }
        /// <summary>
        /// 基础数据表明
        /// </summary>
        
        public string BaseDataTableName { get; set; }

        ///<summary>
        /// 对外无效字段，不能使用,用于数据转化
        /// </summary>
        public string RelFields { get; set; }

        //private IEnumerable<string> _tableRelationFields = new List<string>();
        ///// <summary>
        ///// 表关键字段
        ///// </summary>
        [Ignore]
        
        public IEnumerable<string> TableRelationFields
        {
            get
            {
                return RelFields.Split('|');
            }
            
        }
        /// <summary>
        /// Counter表使用表
        /// </summary>
        
        public string CounterTableName { get; set; }

        /// <summary>
        /// 时间字段名称
        /// </summary>
        
        public string TimeFieldName { get; set; }

        /// <summary>
        /// 级别是否显示
        /// 0显示，1不显示
        /// </summary>
        
        public int IsVisible { get; set; }
    }
}
