/************************************************************************************
 * Copyright (c) 2016-03-10 09:52:55 优网科技 All Rights Reserved.
 * CLR版本： V4.5
 * 公司名称：优网科技
 * 命名空间：UWay.Ufa.Enterprise.Entity
 * 文件名：  UWay.Ufa.Enterprise.Entity.cs
 * 版本号：  V1.0.0.0
 * 唯一标识：fa39e2ce-ad90-450e-beaa-41000a94b4e3
 * 创建人：  谢韶光
 * 电子邮箱：xiesg@uway.cn
 * 创建时间：2016-03-10 09:52:55 
 * 描述： 
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016-03-10 09:52:55 
 * 修改人： 谢韶光
 * 版本号： V1.0.0.0
 * 描述：
 * 
 * 
 * 
 * 
 ************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UWay.Skynet.Cloud.Nom.Entity
{    
    public class ImportDataTemplate
    {
      
        public int? IsAutoApproval
        {
            get;
            set;
        }
      
        public string KeyField
        {
            get;
            set;
        }
      
        public string FuzzyQueryFields
        {
            get;
            set;
        }
      
        public string SummaryFields
        {
            get;
            set;
        }
      
        public string TimeFieldName
        {
            get;
            set;
        }
      
        public string CityFieldName
        {
            get;
            set;
        }


      
        public int? QueryImportTime
        {
            get;
            set;
        }

        /// <summary>
        /// 是否生效
        /// <summary>
      
        public int? IsEnable
        {
            get;
            set;
        }

        /// <summary>
        /// 模版组ID
        /// <summary>
      
        public int? GroupID
        {
            get;
            set;
        }

        /// <summary>
        /// 排序
        /// <summary>
      
        public int? Seq
        {
            get;
            set;
        }

        /// <summary>
        /// 导入时，是否可选择城市
        /// <summary>
      
        public int? IsShowCity
        {
            get;
            set;
        }

        /// <summary>
        /// 产生变化记录时，标示记录行的字段
        /// <summary>
      
        public string SavechengedFieldCombination
        {
            get;
            set;
        }

        /// <summary>
        /// 是否系统的
        /// <summary>
      
        public int? IsSystem
        {
            get;
            set;
        }

        /// <summary>
        /// 是否产生变化记录
        /// <summary>
      
        public string IsSaveChenged
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人
        /// <summary>
      
        public string CreateMan
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// <summary>
      
        public DateTime? CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 导入完成后使用的存储过程
        /// <summary>
      
        public string ExpedProcedure
        {
            get;
            set;
        }

        /// <summary>
        /// 导入前使用的存储过程 $IMPID,IMPCITY,IMPTIME,DATATEMPLATEID,DATATEMPLATENAME,DATATEMPLATETABLENAME
        /// <summary>
      
        public string ExpingProcedure
        {
            get;
            set;
        }

        /// <summary>
        /// 数据表的 Key Field组合  以,隔开
        /// <summary>
      
        public string KeyfieldCombination
        {
            get;
            set;
        }

        /// <summary>
        /// 导入使用的表名称
        /// <summary>
      
        public string ImpTablename
        {
            get;
            set;
        }

        /// <summary>
        /// 查询使用的表名称
        /// <summary>
      
        public string ExpTablename
        {
            get;
            set;
        }

        /// <summary>
        /// 模板名称
        /// <summary>
      
        public string TemplateName
        {
            get;
            set;
        }

        /// <summary>
        /// 模板ID
        /// <summary>
      
        public int TemplateID
        {
            get;
            set;
        }

        /// <summary>
        /// 网络类型
        /// </summary>
      
        public int GenerationType
        {
            get;
            set;
        }

        /// <summary>
        /// 模板对应的实例化工厂类类名称
        /// </summary>
      
        public string FactoryName
        {
            get;
            set;
        }


        /// <summary>
        /// 前台显示的图片名称
        /// </summary>
      
        public string TemplateIcon
        {
            get;
            set;
        }

        /// <summary>
        /// 上传文件到FTP文件路径格式
        /// </summary>
      
        public string FtpFormat
        {
            get;
            set;
        }

        public string DataSourceId
        {
            set;
            get;
        }

        public IList<ImportDataTemplateField> DataFields
        {
            set;
            get;
        }

    }
}
