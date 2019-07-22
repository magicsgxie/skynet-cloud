/************************************************************************************
 * Copyright (c) 2016 优网科技 All Rights Reserved.
 * CLR版本： 4.0.30319.42000
 * 机器名称：UWAY15003
 * 公司名称：优网科技
 * 命名空间：UWay.Skynet.Cloud.Dcs.Services
 * 文件名：  ShareFormulaService
 * 版本号：  V1.0.0.0
 * 唯一标识：cbd5726c-d112-4358-8096-1a6325151374
 * 当前的用户域：UWAY15003
 * 创建人：  谢韶光
 * 电子邮箱：xiesg@uway.cn
 * 创建时间：2016/4/13 16:42:57
 * 描述：
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016/4/13 16:42:57
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
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Dcs.Repository;
using UWay.Skynet.Cloud.Dcs.Service.Interface;

namespace UWay.Skynet.Cloud.Dcs.Service
{
    /// <summary>
    /// 私有公式共享
    /// </summary>
    public class ShareFormulaService : IShareFormulaService
    {
        /// <summary>
        /// 根据地市和用户获取共享的私有公式
        /// </summary>
        /// <param name="citys"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public long[] GetShareFormulaIDsByCityAndUser(int netType,string citys, string userno)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                var r = new ShareFormulaRepository(context);
                if (!citys.IsNullOrEmpty() && !userno.IsNullOrEmpty())
                    return r.GetShareFormulaIDsByCityAndUser(new string[] { GetShareCity(netType), GetShareUser(netType) },citys, userno);
                else if (!userno.IsNullOrEmpty())
                    return r.GetShareFormulaIDsByUser(GetShareUser(netType), userno);
                else if (!citys.IsNullOrEmpty())
                    return r.GetShareFormulaIDsByCity(GetShareCity(netType), citys);
                else
                    return null;
            }
        }

        private string GetShareUser(int netType)
        {
            var tableName = "cfg_app_formula_share_user";
            if (netType == 1)
                tableName = "ds_cfg_formula_share_user";
            return tableName;
        }

        private string GetShareCity(int netType)
        {
            var tableName = "cfg_app_formula_share_city";
            if (netType == 1)
                tableName = "ds_cfg_formula_share_city";
            return tableName;
        }
    }
}
