using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Cfgs.Entity
{
    /// <summary>
    /// 行政区
    /// </summary>
    public class CountyInfo
    {
        
        public int CountyID
        {
            set;
            get;
        }

        public int CityID
        {
            set;
            get;
        }
        
        public string CountyName
        {
            set;
            get;
        }
        
        public int ShowSerial
        {
            set;
            get;
        }


        public double? CountyArea
        {
            set;
            get;
        }

    }
}
