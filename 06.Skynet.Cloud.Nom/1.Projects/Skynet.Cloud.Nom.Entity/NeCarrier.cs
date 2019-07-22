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
    /// 频点信息
    /// </summary>
    
    //[Table("ne_carrier_c")]
    //[PrimaryKey("NE_SYS_ID")]
    public class NeCarr:NeCell
    {
        
        [Ignore]
        public override string NeSysID
        {
            get
            {
                return NeCarrID;
            }
            set
            {
                NeCarrID = value;
            }
        }

        
        //[Column("NE_SYS_ID")]
        public string NeCarrID
        {
            set;
            get;
        }

        /// <summary>
        /// 频点
        /// </summary>
        
        //[Column("CARRIER_ID")]
        public int? CarrierID
        {
            set;
            get;
        }
    }
}
