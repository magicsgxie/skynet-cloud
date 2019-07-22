using System;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Nom.Entity
{
    /// <summary>
    /// 网元信息
    /// </summary>
    public abstract class BaseNeInfo
    {
        /// <summary>
        /// 网元唯一标识
        /// </summary>
        [Ignore]

        public virtual string NeSysID
        {
            set;
            get;
        }


        /// <summary>
        /// 网元名称
        /// </summary>

        //[Column("CHINA_NAME")]
        public virtual string NeName
        {
            get;
            set;
        }

        /// <summary>
        /// 网元级别
        /// </summary>

        [Ignore]
        public int NeLevel
        {
            get;
            set;
        }


        private string _verndor = string.Empty;
        /// <summary>
        /// 厂家
        /// </summary>

        //[Column("VENDOR")]
        public string Vendor
        {
            set
            {
                _verndor = value;
            }
            get
            {
                return _verndor;

            }
        }

        private string _verndorName = string.Empty;
        /// <summary>
        /// 厂家
        /// </summary>

        //[Column("VENDOR_NAME")]
        public string VendorName
        {
            set
            {
                _verndorName = value;
            }
            get
            {
                return _verndorName;

            }
        }

        /// <summary>
        /// 地市ID
        /// </summary>

        //[Column("CITY_ID")]
        public int CityID
        {
            get;
            set;
        }

        /// <summary>
        /// 城市名称
        /// </summary>

        //[Column("CITY_NAME")]
        public string CityName
        {
            set;
            get;
        }


        /// <summary>
        /// 网络制式
        /// </summary>

        [Ignore]
        public int NetType
        {
            set;
            get;
        }
    }
}
