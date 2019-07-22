using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    public enum BusyTimeType
    {
        /// <summary>
        /// 天忙时
        /// </summary>
        [Description("天忙时")]
        DayBusy = 1,
        /// <summary>
        /// 早忙时
        /// </summary>g
        [Description("早忙时")]
        MorningBusy = 2,
        /// <summary>
        /// 晚忙时
        /// </summary>
        [Description("晚忙时")]
        EveningBusy = 3
    }
}
