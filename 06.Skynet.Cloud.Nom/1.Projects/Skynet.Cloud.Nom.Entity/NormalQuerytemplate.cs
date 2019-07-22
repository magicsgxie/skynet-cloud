/************************************************************************************
 * Copyright (c) 2016-03-10 09:52:55 优网科技 All Rights Reserved.
 * CLR版本： V4.5
 * 公司名称：优网科技
 * 命名空间：UWay.Ufa.Enterprise.Entity
 * 文件名：  UWay.Ufa.Enterprise.Entity.cs
 * 版本号：  V1.0.0.0
 * 唯一标识：d760bb35-1455-4b43-b770-6bf9d5b811ee
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
using System.Runtime.Serialization;

namespace UWay.Ufa.Enterprise.Entity
{
    [DataContract]
    public class NormalQuerytemplate
    {
        /// <summary>
        /// DisplayIndex：查询模版排序
        /// <summary>
        /// <summary>
        /// DisplayIndex：查询模版排序
        /// <summary>
      
        public int? Displayindex
        {
            get;
            set;
        }

        /// <summary>
        /// ImportTemplateID：导入模版主键
        /// <summary>
      
        public int Importtemplateid
        {
            get;
            set;
        }
        /// <summary>
        /// TemplateName：查询模版名称
        /// <summary>
        /// <summary>
        /// TemplateName：查询模版名称
        /// <summary>
      
        public string Templatename
        {
            get;
            set;
        }
        /// <summary>
        /// QueryTemplateID：查询字段模版主键
        /// <summary>
        /// <summary>
        /// QueryTemplateID：查询字段模版主键
        /// <summary>
      
        public int Querytemplateid
        {
            get;
            set;
        }
    }
}
