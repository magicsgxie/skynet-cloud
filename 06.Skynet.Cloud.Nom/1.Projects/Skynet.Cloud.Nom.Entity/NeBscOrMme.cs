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
    /// C网-BSC基本信息
    /// </summary>
    
    //[Table("NE_BSC_C")]
    //[PrimaryKey("NE_SYS_ID")]
    public class NeBscOrMme:BaseNeInfo
    {
        
        [Ignore]
        public override string NeSysID
        {
            get
            {
                return NeBscID;
            }
            set
            {
                NeBscID = value;
            }
        }

        /// <summary>
        /// Bsc or MME Inner ID
        /// </summary>
        [Ignore]
        //[Column("NE_SYS_ID")]
        public virtual string NeBscID
        {
            set;
            get;
        }

        /// <summary>
        /// BSCID
        /// </summary>
        
        //[Column("BSC_ID")]
        public int? BscOrMmeID
        {
            get;
            set;
        }

        /// <summary>
        /// OMC 编号
        /// </summary>
        
        //[Column("RELATED_MSC")]
        public Int64? NeOmcID
        {
            set;
            get;
        }
    }
}
