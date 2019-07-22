using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    /// <summary>
    /// 聚合方式 0:不聚合,1:公式聚合,2:指标聚合
    /// </summary>
    public enum AggregationWay
    {
        None = 0,
        Counter = 1,
        Formula = 2
    }

    /// <summary>
    /// 时间粒度
    /// </summary>
    public enum TimeGranularity
    {
        Hour = 1,
        Day = 2,
        Week = 3,
        Month = 4,
    }

    public static class AggregationWayExtension
    {

    }

    public static class TimeGranularityExtension
    {

    }
}
