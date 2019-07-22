using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    public class BusyInfo
    {
        /// <summary>
        /// 网络类型
        /// </summary>
        public int Ne_Type { get; set; }
        /// <summary>
        /// 网元粒度
        /// </summary>
        public int Ne_Level { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public int Business_Type { get; set; }
        /// <summary>
        /// 忙时类型
        /// </summary>
        public int Busy_Type { get; set; }
        /// <summary>
        /// 忙时描述
        /// </summary>
        public string Busy_Type_Desp { get; set; }
        /// <summary>
        /// 天忙时表
        /// </summary>
        public string Busy_Time_Day_Table_Name { get; set; }
        /// <summary>
        /// 早忙时表
        /// </summary>
        public string Busy_Time_Morning_Table_Name { get; set; }
        /// <summary>
        /// 晚忙时表
        /// </summary>
        public string Busy_Time_Evening_Table_Name { get; set; }
        /// <summary>
        /// 天忙时字段
        /// </summary>
        public string Busy_Time_Day_Field_Name { get; set; }
        /// <summary>
        /// 早忙时字段
        /// </summary>
        public string Busy_Time_Morning_Field_Name { get; set; }
        /// <summary>
        /// 晚忙时字段
        /// </summary>
        public string Busy_Time_Evening_Field_Name { get; set; }
        /// <summary>
        /// 网元字段名称
        /// </summary>
        public string Ne_Field_Name { get; set; }
        /// <summary>
        /// 时间字段名称
        /// </summary>
        public string Time_Field_Name { get; set; }
        /// <summary>
        /// 天忙时数据库忙时类型值
        /// </summary>
        public string Busy_Group_Day_Busy_Type { get; set; }
        /// <summary>
        /// 早忙时数据库忙时类型值
        /// </summary>
        public string Busy_Group_Morning_Busy_Type { get; set; }
        /// <summary>
        /// 晚忙时数据库忙时类型值
        /// </summary>
        public string Busy_Group_Evening_Busy_Type { get; set; }
    }
}
