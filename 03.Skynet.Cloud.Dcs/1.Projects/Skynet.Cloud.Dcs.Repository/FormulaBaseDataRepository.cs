/************************************************************************************
 * Copyright (c) 2016 优网科技 All Rights Reserved.
 * CLR版本： 4.0.30319.36366
 * 机器名称：UWAY
 * 公司名称：优网科技
 * 命名空间：UWay.Skynet.Cloud.Dcs.Repository
 * 文件名：  FormulaBaseDataRepository
 * 版本号：  V1.0.0.0
 * 唯一标识：4e4c491b-9257-4544-a471-c25de88b39da
 * 当前的用户域：UWay
 * 创建人：  潘国
 * 电子邮箱：pang@uway.cn
 * 创建时间：2016/12/26 15:31:10
 * 描述：
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016/12/26 15:31:10
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
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Dcs.Entity;

namespace UWay.Skynet.Cloud.Dcs.Repository
{
    /// <summary>
    ///基础数据Repository
    /// </summary>
    public class FormulaBaseDataRepository : ObjectRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uow"></param>
        public FormulaBaseDataRepository(IDbContext uow)
            : base(uow)
        {

        }

        /// <summary>
        /// 获取所有基础数据
        /// </summary>
        /// <returns></returns>
        public BaseFormula GetById(long id)
        {
            return GetByID<BaseFormula>(id);
        }

        /// <summary>
        /// 获取所有基础数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<FormulaBaseData> GetFormulaBaseDatas()
        {
            return CreateQuery<FormulaBaseData>();
        }

        public Dictionary<long, BaseFormula> GetPerfFormulaDatas()
        {
            return CreateQuery<BaseFormula>().ToList().ToDictionary(t => t.AttID);
        }
    }
}
