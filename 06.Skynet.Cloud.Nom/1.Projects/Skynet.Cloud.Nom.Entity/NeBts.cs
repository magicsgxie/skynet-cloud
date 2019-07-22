using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Nom.Entity
{
    /// <summary>
    /// C网-基站信息
    /// </summary>
    
    //[Table("NE_BTS_C")]
    //[PrimaryKey("NE_SYS_ID")]
    public class NeBts:NeBscOrMme
    {
        [Ignore]
        
        public override string NeSysID
        {
            get
            {
                return NeBtsID;
            }
            set
            {
                NeBtsID = value;
            }
        }

        
        //[Column("NE_SYS_ID")]
        public virtual string NeBtsID
        {
            set;
            get;
        }


        /// <summary>
        /// BTSID/LAC
        /// </summary>
        
        //[Column("BTS_ID")]
        public int? BtsID
        {
            get;
            set;
        }


        /// <summary>
        /// 经度
        /// </summary>
        
        //[Column("longitude")]
        public double Longitude
        {
            get;
            set;
        }

        /// <summary>
        /// 纬度
        /// </summary>
        
        //[Column("latitude")]
        public double Latitude
        {
            get;
            set;
        }


        /// <summary>
        /// 行政区
        /// </summary>
        
        //[Column("ADM_AREA")]
        public string CountyID
        {
            get;
            set;
        }

        /// <summary>
        /// 行政区
        /// </summary>
        
        //[Column("COUNTY_NAME")]
        public string CountyName
        {
            get;
            set;
        }


        /// <summary>
        /// BSC名称
        /// </summary>
        
        //[Column("bsc_name")]
        public string BscOrMmeName
        {
            set;
            get;
        }

        /// <summary>
        /// NeBscID / NeMmeID
        /// </summary>
        
        //[Column("RELATED_BSC")]
        public override string NeBscID
        {
            set;
            get;
        }
    }
}
