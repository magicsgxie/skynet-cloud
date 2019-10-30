using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    /// <summary>
    /// 指标查询字段
    /// </summary>
    
    public class QueryField: QueryBase
    {
        
        
        /// <summary>
        /// 模板ID（模板时使用）
        /// </summary>
        public long TemplateID
        {
            set;
            get;
        }
        
        /// <summary>
        /// 排序
        /// </summary>
        public OrderByDirection OrderBy
        {
            set;
            get;
        }

      

        /// <summary>
        /// 字段顺序
        /// </summary>
        
        public int OrderNo
        {
            set; get;
        }


        public int UserInBSC { get; set; }
        public int UserInBTS { get; set; }
        public int UserInCARR { get; set; }
        public int UserInCELL { get; set; }
    }
}
