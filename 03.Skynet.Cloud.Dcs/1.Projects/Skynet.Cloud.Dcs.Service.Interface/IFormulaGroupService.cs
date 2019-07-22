using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;

namespace UWay.Skynet.Cloud.Dcs.Service.Interface
{
    /// <summary>
    /// 基本公式接口
    /// </summary>
    
    public interface IFormulaGroupService
    {
        /// <summary>
        /// 获取指标分组
        /// </summary>
        /// <returns></returns>
        
        List<FormulaGroup> GetFormulaGroup();

        /// <summary>
        /// 添加指标分组
        /// </summary>
        /// <returns></returns>
        
        long AddFormulaGroup(FormulaGroup item);

        /// <summary>
        /// 更新指标分组
        /// </summary>
        /// <returns></returns>
        
        int UpdateFormulaGroup(FormulaGroup item);

        /// <summary>
        /// 更新指标分组
        /// </summary>
        /// <returns></returns>
        
        int DeleteFormulaGroup(long[] ids);


        /// <summary>
        /// 更新指标分组
        /// </summary>
        /// <returns></returns>
        
        FormulaGroup GetFormulaGroupById(long id);


    }
}
