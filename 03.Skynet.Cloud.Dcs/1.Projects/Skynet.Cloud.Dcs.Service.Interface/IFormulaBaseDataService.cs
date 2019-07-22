/************************************************************************************
 * Copyright (c) 2016 优网科技 All Rights Reserved.
 * CLR版本： 4.0.30319.36366
 * 机器名称：UWAY
 * 公司名称：优网科技
 * 命名空间：UWay.Skynet.Cloud.Dcs.Services.Interface.Perf
 * 文件名：  IFormulaBaseDataService
 * 版本号：  V1.0.0.0
 * 唯一标识：837ec28a-9077-464d-a348-ee69d0724516
 * 当前的用户域：UWay
 * 创建人：  潘国
 * 电子邮箱：pang@uway.cn
 * 创建时间：2016/12/26 15:36:25
 * 描述：
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016/12/26 15:36:25
 * 修改人： 潘国
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
using UWay.Skynet.Cloud.Dcs.Entity;

namespace UWay.Skynet.Cloud.Dcs.Service.Interface
{
    /// <summary>
    /// 基础数据接口
    /// </summary>
    
    public interface IFormulaBaseDataService 
    {
        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <returns></returns>
         
        IList<FormulaBaseData> GetFormulaBaseDatas();

       
    }
}
