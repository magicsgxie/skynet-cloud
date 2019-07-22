using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    /// <summary>
    /// 色阶配置表
    /// </summary>
    [Table("CASP_COLOR_LEVEL_SETTING")]

    public class ColorLevelSetting
    {
        /// <summary>
        /// 最大值
        /// </summary>
        [Column("MAX_VALUE")]

        public int? MaxValue
        {
            get;
            set;
        }

        /// <summary>
        /// 最小值
        /// </summary>
        [Column("MIN_VALUE")]

        public int? MinValue
        {
            get;
            set;
        }

        /// <summary>
        /// 类型
        /// </summary>
        [Column("LEVEL_TYPE")]

        public int? LevelType
        {
            get;
            set;
        }

        /// <summary>
        /// 是否默认
        /// </summary>
        [Column("IS_DEFAULT")]

        public int? IsDefault
        {
            get;
            set;
        }

        /// <summary>
        /// 区间数
        /// </summary>
        [Column("LEVEL_COUNT")]

        public int? LevelCount
        {
            get;
            set;
        }

        /// <summary>
        /// 色阶名称,
        /// 建议格式：模板ID + "-" +  字段ID（指标ID） + "-" +模板名称
        /// </summary>
        [Column("SETTING_NAME")]

        public string SettingName
        {
            get;
            set;
        }

        /// <summary>
        /// 模块ID，用于标记一个独立色阶模块或分组，建议格式：5+menuID+000, menuID建议四位 000表示三位流水码
        /// </summary>
        [Column("MODULE_ID")]

        public long ModuleId
        {
            get;
            set;
        }

        /// <summary>
        /// 主键
        /// </summary>
        [Id("ID")]

        public long Id
        {
            get;
            set;
        }

        /// <summary>
        /// 区间
        /// </summary>
        [Ignore]
        public List<ColorLevelDynamicInfo> SectionList { get; set; }


        /// <summary>
        /// 枚举
        /// </summary>
        [Ignore]
        public List<ColorLevelEnumInfo> SectionEnumList { get; set; }


    }
}
