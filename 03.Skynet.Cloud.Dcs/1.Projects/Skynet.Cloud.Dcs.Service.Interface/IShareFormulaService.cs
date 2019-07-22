/************************************************************************************
 * Copyright (c) 2016 优网科技 All Rights Reserved.
 * CLR版本： 4.0.30319.42000
 * 机器名称：UWAY15003
 * 公司名称：优网科技
 * 命名空间：UWay.Skynet.Cloud.Dcs.Services.Interface.Perf
 * 文件名：  IShareFormulaService
 * 版本号：  V1.0.0.0
 * 唯一标识：30ae8256-57a9-40c2-9d60-1691f3459c2c
 * 当前的用户域：UWAY15003
 * 创建人：  谢韶光
 * 电子邮箱：xiesg@uway.cn
 * 创建时间：2016/4/13 16:43:17
 * 描述：
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016/4/13 16:43:17
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

namespace UWay.Skynet.Cloud.Dcs.Service.Interface
{
    /// <summary>
    /// 私有公式共享
    /// </summary>
    
    public interface IShareFormulaService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="citys"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        long[] GetShareFormulaIDsByCityAndUser(int netType,string citys, string userno);
    }
}
