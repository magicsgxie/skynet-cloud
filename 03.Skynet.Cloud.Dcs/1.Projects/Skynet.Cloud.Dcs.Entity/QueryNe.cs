/************************************************************************************
 * Copyright (c) 2016 优网科技 All Rights Reserved.
 * CLR版本： 4.0.30319.42000
 * 机器名称：UWAY15003
 * 公司名称：优网科技
 * 命名空间：UWay.Skynet.Cloud.Dcs.Entity.Perf
 * 文件名：  QueryNe
 * 版本号：  V1.0.0.0
 * 唯一标识：2c202cdd-28a4-4def-9f34-6340823de92a
 * 当前的用户域：UWAY15003
 * 创建人：  谢韶光
 * 电子邮箱：xiesg@uway.cn
 * 创建时间：2016/6/28 9:54:06
 * 描述：
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016/6/28 9:54:06
 * 修改人： 谢韶光
 * 版本号： V1.0.0.0
 * 描述：查询网元
 * 
 * 
 * 
 * 
 ************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    /// <summary>
    /// 查询网元
    /// </summary>
    
    public class QueryNe
    {

        
        public long TemplateID
        {
            set;
            get;
        }

        
        public string NeValue
        {
            set
            {
                var length = value.Length;
                    
                if (length > 12000)
                {
                    NeValue4 = value.Substring(12000, length - 12000);
                    length = 12000;
                }
                    
                if (length <= 12000 && length > 8000)
                {
                    NeValue3 = value.Substring(8000, length -8000);
                    length = 8000;
                }
                if (length <= 8000 && length > 4000)
                {
                    NeValue2 = value.Substring(4000, length -4000);
                    length = 4000;
                }
    
                if (length > 0 && length <= 4000)
                {
                    NeValue1 = value.Substring(0, length);
                }


            }
            get
            {
                var str = string.Format("{0}{1}{2}{3}", NeValue1, NeValue2, NeValue3, NeValue4);
                return str; 
            }
        }

        
        public string NeValue1
        {
            set;get;
        }
        
        public string NeValue2
        {
            set; get;
        }
        
        public string NeValue3
        {
            set; get;
        }
        
        public string NeValue4
        {
            set; get;
        }


        
        public string NeName
        {
            set; get;
        }
    }
}
