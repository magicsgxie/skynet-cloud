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
    
    public interface IBaseFormulaService 
    {
        /// <summary>
        /// 添加公式
        /// </summary>
        /// <param name="item"></param>
        
         long AddBaseFormula(BaseFormula item);

        /// <summary>
        /// 更新公式
        /// </summary>
        /// <param name="item"></param>
        
        int UpdateBaseFormula(BaseFormula item);

        /// <summary>
        /// 删除公式
        /// </summary>
        /// <param name="ids"></param>
        
        int DeleteBaseFormulaById(long[] ids);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="neType"></param>
        /// <param name="dataSourceType"></param>
        /// <param name="vendorVersion"></param>
        /// <param name="attCNName"></param>
        /// <returns></returns>
        IEnumerable<BaseFormula> GetSubByCondition(int neType, int dataSourceType, string vendorVersion, string attCNName);

        /// <summary>
        /// 获取性能指标配置信息
        /// </summary>
        /// <returns></returns>

        BaseFormula GetById(long attId);

        IEnumerable<BaseFormula> GetFormulas(IEnumerable<long> attids);

        /// <summary>
        /// 获取性能指标配置信息
        /// </summary>
        /// <returns></returns>

        IDictionary<long, BaseFormula> GetPerfFormulaDatas();
    }
}
