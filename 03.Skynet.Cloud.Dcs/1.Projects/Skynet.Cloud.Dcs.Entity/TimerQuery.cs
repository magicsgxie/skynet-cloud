using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    /// <summary>
    /// 时间查询条件
    /// </summary>
    
    public class TimerQuery
    {
        /// <summary>
        /// 时间粒度 1：小时 2：天 3：周 4：月 5：季度 6 ：年
        /// </summary>
        
        public int TimeGranularity
        {
            set;
            get;
        }

        /// <summary>
        /// 时间文本描述
        /// </summary>
        
        public string TimeText
        {
            set;
            get;
        }

        /// <summary>
        /// 时间过滤条件
        /// </summary>
        
        public string TimeCondition
        {
            set;
            get;
        }

        /// <summary>
        /// 是否需要忙时
        /// </summary>
        
        public bool IsBusy
        {
            set;
            get;
        }

        /// <summary>
        /// 忙时类型
        /// </summary>
        
        public int BusyType
        {
            set;
            get;
        }

        /// <summary>
        ///忙时时间类型（1：天忙时，2：早忙时，4晚忙时）
        /// </summary>
        
        public BusyTimeType BusyTimeStyle
        {
            set;
            get;
        }


        /// <summary>
        /// 时间聚合方式 0：不聚合 粒度同时间粒度
        /// </summary>
        
        public int TimeAggregation
        {
            set;
            get;
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        
        public string StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        
        public string EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// 增加返回的时间集合
        /// </summary>

        
        public List<string> TimeList
        {
            get;
            set;
        }
    }
}
