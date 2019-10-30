using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Cfgs.Entity
{
    public class Holiday
    {
        public long HolidayId { set; get; }
        /// <summary>
        /// 是否改变当天的状态(0:不改变 1：改变)
        /// </summary>
        public int? IsChange
        {
            get;
            set;
        }
        /// <summary>
        /// 节假日备注
        /// </summary>
        
        public string HolidaysRemark
        {
            get;
            set;
        }
        /// <summary>
        /// 是否节假日(0:工作日 1：休息日)
        /// </summary>
        public int? IsHolidays
        {
            get;
            set;
        }
        /// <summary>
        /// 星期几
        /// </summary>
        public int? WeekDay
        {
            get;
            set;
        }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? CalendarDate
        {
            get;
            set;
        }
    }
}
