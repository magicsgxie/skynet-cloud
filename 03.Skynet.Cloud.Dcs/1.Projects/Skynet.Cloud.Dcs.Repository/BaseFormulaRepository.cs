using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Dcs.Repository
{
    /// <summary>
    /// 公式仓储表
    /// </summary>
    public class BaseFormulaRepository:ObjectRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uow"></param>
        public BaseFormulaRepository(IDbContext uow):base(uow)
        {

        }

        /// <summary>
        /// 添加公式
        /// </summary>
        /// <param name="item">公式实体</param>
        /// <returns></returns>
        public long AddBaseFormula(BaseFormula item)
        {
            return Add<BaseFormula, long>(item, p => p.AttID);
        }

        /// <summary>
        /// 更新公式
        /// </summary>
        /// <param name="item">公式实体</param>
        /// <returns></returns>
        public int UpdateBaseFormula(BaseFormula item)
        {
            return Update<BaseFormula>(item);
        }

        /// <summary>
        /// 批量删除公式
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteBaseFormulaById(long[] ids)
        {
            return Delete<BaseFormula>(p => ids.Contains(p.AttID));
        }

        /// <summary>
        /// 获取公式列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<BaseFormula> GetFormulas()
        {
            return CreateQuery<BaseFormula>();
        }

        /// <summary>
        /// 根据中文名获取公式
        /// </summary>
        /// <param name="cnName">中文名</param>
        /// <returns></returns>
        public IQueryable<BaseFormula> GetFormulasByFormulasCnName(string cnName)
        {
            return CreateQuery<BaseFormula>().Where(p => p.AttCnName.Equals( cnName, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// 根据ID获取公式
        /// </summary>
        /// <param name="ID">公式ID</param>
        /// <returns></returns>
        public BaseFormula GetFormulaByID(long ID)
        {
            return GetByID<BaseFormula>(ID);
        }

        

    }
}
    