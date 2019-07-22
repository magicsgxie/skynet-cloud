using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;
using System.Drawing;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    /// <summary>
    /// 色阶区间
    /// </summary>
    [Table("CASP_COLOR_LEVEL_DYNAMIC_INFO")]
    public class ColorLevelDynamicInfo
    {
        /// <summary>
        /// 最大值
        /// </summary>
        [Column("LAGER_NUM")]
        public double? LagerNum
        {
            get;
            set;
        }
        /// <summary>
        /// 最小值
        /// </summary>
        [Column("SMALLER_NUM")]
        public double? SmallerNum
        {
            get;
            set;
        } 

        [Ignore]
        public System.Drawing.Color Color
        {
            get { return System.Drawing.Color.FromArgb(ColorA.Value, ColorR.Value, ColorG.Value, ColorB.Value); }
        }

        /// <summary>
        /// B
        /// </summary>
        [Column("COLOR_B")]
        public int? ColorB
        {
            get;
            set;
        }
        /// <summary>
        /// G
        /// </summary>
        [Column("COLOR_G")]
        public int? ColorG
        {
            get;
            set;
        }
        /// <summary>
        /// R
        /// </summary>
        [Column("COLOR_R")]
        public int? ColorR
        {
            get;
            set;
        }
        /// <summary>
        /// A
        /// </summary>
        [Column("COLOR_A")]
        public int? ColorA
        {
            get;
            set;
        }
        /// <summary>
        /// 描述
        /// </summary>
        [Column("SCOPE")]
        public string Scope
        {
            get;
            set;
        }
        /// <summary>
        /// 设置ID
        /// </summary>
        [Column("SETTING_ID")]
        public long SettingId
        {
            get;
            set;
        }
        /// <summary>
        /// 区间ID
        /// </summary>
        [Id("ID")]
        public long Id
        {
            get;
            set;
        }
    }
}
