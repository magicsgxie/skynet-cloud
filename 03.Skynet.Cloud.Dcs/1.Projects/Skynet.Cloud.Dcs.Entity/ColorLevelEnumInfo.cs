/************************************************************************************
 * Copyright (c) 2016-08-05 20:43:52 优网科技 All Rights Reserved.
 * CLR版本： V4.5
 * 公司名称：优网科技
 * 命名空间：UWay.Dcs..Entity
 * 文件名：  UWay.Dcs..Entity.cs
 * 版本号：  V1.0.0.0
 * 唯一标识：34cddd3f-629d-46c2-a4c5-48ffe272096c
 * 创建人：  谢韶光
 * 电子邮箱：xiesg@uway.cn
 * 创建时间：2016-08-05 20:43:52 
 * 描述： 
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016-08-05 20:43:52 
 * 修改人： 谢韶光
 * 版本号： V1.0.0.0
 * 描述：
 * 
 * 
 * 
 * 
 ************************************************************************************/
using System;
using System.Runtime.Serialization;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Dcs.Entity
{

    [Table("CASP_COLOR_LEVEL_ENUM_INFO")]
    public class ColorLevelEnumInfo
    {
        [Column("COLOR_B")]
        public int? ColorB
        {
            get;
            set;
        }

        [Column("COLOR_G")]
        public int? ColorG
        {
            get;
            set;
        }

        [Column("COLOR_R")]
        public int? ColorR
        {
            get;
            set;
        }

        [Column("COLOR_A")]
        public int? ColorA
        {
            get;
            set;
        }

        [Column("ENUM_NAME")]
        public string EnumName
        {
            get;
            set;
        }

        [Column("SETTING_ID")]
        public long SettingId
        {
            get;
            set;
        }

        [Id("ID")]
        public long Id
        {
            get;
            set;
        }
    }
}
