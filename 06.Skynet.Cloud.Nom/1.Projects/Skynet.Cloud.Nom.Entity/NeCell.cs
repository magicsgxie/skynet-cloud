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
    /// C网--小区信息
    /// </summary>
    
    //[Table("NE_CELL_C")]
    //[PrimaryKey("NE_SYS_ID")]
    public class NeCell:NeBts
    {
        
        [Ignore]
        public override string NeSysID
        {
            get
            {
                return NeCellID;
            }
            set
            {
                NeCellID = value;
            }
        }

        
        //[Column("NE_SYS_ID")]
        public virtual string NeCellID
        {
            set;
            get;
        }

        /// <summary>
        /// 扇区ID/CI
        /// </summary>
        
        //[Column("SECTOR_ID")]
        public int? SectorID
        {
            get;
            set;
        }

        /// <summary>
        /// 扇区侧面ID
        /// </summary>
        
        //[Column("LOCAL_CELL_ID")]
        public int? LocalCellID
        {
            get;
            set;
        }

        /// <summary>
        /// 方向角
        /// </summary>

        
        //[Column("ANT_AZIMUTH")]
        public double? AntAzimuth
        {
            set;
            get;
        }

        /// <summary>
        /// Bts名称
        /// </summary>
        
        //[Column("BTS_NAME")]
        public string BtsName
        {
            get;
            set;
        }

        /// <summary>
        /// 基站ID
        /// </summary>
        
        //[Column("RELATED_BSC")]
        public override string NeBtsID
        {
            set;
            get;
        }

        /// <summary>
        /// L网：FDD OR TDD
        /// C网：1x Or DO， 只有在载频中才有该字段的值
        /// </summary>

        
        public string BusType
        {
            get;
            set;
        }
        /// <summary>
        /// L网 ：室内外
        /// C网 ：没有这个字段
        /// </summary>
        
        public string LocationType
        {
            get;
            set;
        }

        /// <summary>
        /// 物理标示
        /// LTE:PCI
        /// CDMA:PN
        /// </summary>
        
        public int? PhysicalSigns
        {
            get;
            set;
        }

        /// <summary>
        /// 覆盖类型
        /// </summary>
        
        public string CellType
        {
            get;
            set;
        }

        /// <summary>
        /// 边界属性
        /// </summary>
        
        public string BorderType
        {
            get;
            set;
        }

        public string FrequencyBand
        {
            get;
            set;
        }
    }
}
