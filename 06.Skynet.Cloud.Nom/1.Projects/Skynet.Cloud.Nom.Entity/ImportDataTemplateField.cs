
/************************************************************************************
 * Copyright (c) 2016-05-12 20:06:10 优网科技 All Rights Reserved.
 * CLR版本： V4.5
 * 公司名称：优网科技
 * 命名空间：.Entity
 * 文件名：  .Entity.cs
 * 版本号：  V1.0.0.0
 * 唯一标识：1905f394-3b7f-480e-b49a-02b24e3dd77f
 * 创建人：  谢韶光
 * 电子邮箱：xiesg@uway.cn
 * 创建时间：2016-05-12 20:06:10 
 * 描述： 
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016-05-12 20:06:10 
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
    
    public class ImportDataTemplateField
    {
        /// <summary>
        /// 是否GIS分析字段
        /// <summary>
      
        public int? IsGisAnalysis
        {
            get;
            set;
        }
        /// <summary>
        /// 是否分组
        /// <summary>
      
        public int? IsGroup
        {
            get;
            set;
        }
        /// <summary>
        /// 格式
        /// <summary>
      
        public string Format
        {
            get;
            set;
        }
        /// <summary>
        /// 更新标记
        /// <summary>
      
        public int? Updateflag
        {
            get;
            set;
        }
        /// <summary>
        /// 其他排序
        /// <summary>
      
        public int? Othersenq2
        {
            get;
            set;
        }
        /// <summary>
        /// 其他排序
        /// <summary>
      
        public int? Othersenq
        {
            get;
            set;
        }
        /// <summary>
        /// 错误文本更新
        /// <summary>
      
        public string Errortextupdate
        {
            get;
            set;
        }
        /// <summary>
        /// 错误文本
        /// <summary>
      
        public string Errortext
        {
            get;
            set;
        }
        /// <summary>
        /// 控件类型
        /// <summary>
      
        public int? Controltype
        {
            get;
            set;
        }
        /// <summary>
        /// 是否可编辑
        /// <summary>
      
        public int? Iseditable
        {
            get;
            set;
        }
        /// <summary>
        /// 是否主键
        /// <summary>
      
        public int? Iskey
        {
            get;
            set;
        }
        /// <summary>
        /// 是否工单关联
        /// <summary>
      
        public int? IsWorkorderRelate
        {
            get;
            set;
        }
        /// <summary>
        /// 验证表达式错误时提示信息
        /// <summary>
      
        public string Experssionerrorlog
        {
            get;
            set;
        }
        /// <summary>
        /// 是否必须字段（字段可以缺少）
        /// <summary>
      
        public string Isneed
        {
            get;
            set;
        }
        /// <summary>
        /// 验证表达式
        /// <summary>
      
        public string Experssion
        {
            get;
            set;
        }
        /// <summary>
        /// 是否允许为空
        /// <summary>
      
        public string Isnull
        {
            get;
            set;
        }
        /// <summary>
        /// 默认值
        /// <summary>
      
        public string Fieldvalue
        {
            get;
            set;
        }
        /// <summary>
        /// 字段长度
        /// <summary>
      
        public int? Fieldlength
        {
            get;
            set;
        }
        /// <summary>
        /// 字段类型
        /// <summary>
      
        public string Datatype
        {
            get;
            set;
        }
        /// <summary>
        /// 导入时是否显示（城市编号与报表编号为隐藏固定添加列）
        /// <summary>
      
        public string Isvisibleimp
        {
            get;
            set;
        }
        /// <summary>
        /// 查询、导出时候 是否显示（城市编号与报表编号为隐藏固定添加列）
        /// <summary>
      
        public string Isvisibleexp
        {
            get;
            set;
        }
        /// <summary>
        /// 排序字段
        /// <summary>
      
        public int? Seq
        {
            get;
            set;
        }
        /// <summary>
        /// 字段中文名称
        /// <summary>
      
        public string Fieldtext
        {
            get;
            set;
        }
        /// <summary>
        /// 字段名称
        /// <summary>
      
        public string Fieldname
        {
            get;
            set;
        }
        /// <summary>
        /// 字段ID
        /// <summary>
      
        public int FieldID
        {
            get;
            set;
        }
        /// <summary>
        /// 模板ID
        /// <summary>
      
        public int Templateid
        {
            get;
            set;
        }
    }
}


