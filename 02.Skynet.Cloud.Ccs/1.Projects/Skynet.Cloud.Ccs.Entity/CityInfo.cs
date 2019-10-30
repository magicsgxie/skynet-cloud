using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Ccs.Entity
{
    /// <summary>
    /// 城市信息
    /// </summary>
    public class CityInfo
    {

        public int CityID
        {
            set; get;
        }

        public string CityName
        {
            set; get;
        }

        public int ShowSerial
        {
            set; get;
        }

        public double? LongitudeLB
        {
            set; get;
        }

        public double? LatitudeLB
        {
            set; get;
        }

        public double? LongitudeRB
        {
            set; get;
        }

        public double? LatitudeRB
        {
            set; get;
        }

        public double? LongitudeCenter
        {
            set; get;
        }

        public double? LatitudeCenter
        {
            set; get;
        }

        public Int64? NeSysIDCenter
        {
            set; get;
        }

        public bool? IsCal
        {
            set; get;
        }

        public int? GridStartID
        {
            set; get;
        }

        public int? Scale
        {
            set; get;
        }

        public string Vendor
        {
            set; get;
        }
        
        public int? PNinCrease
        {
            set; get;
        }
        /// <summary>
        /// 改城市长度最大的100米栅格数目
        /// </summary>
        
        public Int64? MaxGridM
        {
            set; get;
        }

        /// <summary>
        /// 改城市高度最大的100米栅格数目
        /// </summary>
        
        public Int64? MaxGridN
        {
            set; get;
        }

        /// <summary>
        /// 系统版本
        /// </summary>
        public string Version
        {
            set; get;
        }


        
        public Int64? Sid
        {
            set; get;
        }
        
        public string EnName
        {
            set; get;
        }
        
        public int OrderIndex
        {
            set; get;
        }
 
        public string CitySign
        {
            set; get;
        }

        /// <summary>
        /// 左上角经度
        /// </summary>
        public double? LngLT
        {
            set; get;
        }

        /// <summary>
        /// 左上角纬度
        /// </summary>

        public double? LatLT
        {
            set; get;
        }

        /// <summary>
        /// 右下角经度
        /// </summary>
        
        public double? LngRB
        {
            set; get;
        }

        /// <summary>
        /// 右下角纬度
        /// </summary>

        public double? LatRB
        {
            set; get;
        }

        public double? CityArea
        {
            set;
            get;
        }

    }
}
