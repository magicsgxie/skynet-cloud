/************************************************************************************
 * Copyright (c) 2016 优网科技 All Rights Reserved.
 * CLR版本： 4.0.30319.42000
 * 机器名称：UWAY15003
 * 公司名称：优网科技
 * 命名空间：UWay.Ufa.Mvc.Infrastructure.Implementation.Filtering.Parsing
 * 文件名：  NullNode
 * 版本号：  V1.0.0.0
 * 唯一标识：a10e2d9b-897b-4582-9da5-13730286a28e
 * 当前的用户域：UWAY15003
 * 创建人：  谢韶光
 * 电子邮箱：xiesg@uway.cn
 * 创建时间：2016/7/14 11:15:30
 * 描述：
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016/7/14 11:15:30
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq.Impl
{
    /// <summary>
    /// 空置节点 的摘要说明
    /// </summary>
    public class NullNode : IFilterNode, IValueNode
    {
        public object Value
        {
            get;
            set;
        }

        public void Accept(IFilterNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
