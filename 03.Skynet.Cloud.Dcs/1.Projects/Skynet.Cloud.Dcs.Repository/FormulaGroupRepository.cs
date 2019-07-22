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
    /// 公式分组数据库仓储
    /// </summary>
    public class FormulaGroupRepository:ObjectRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uow">数据库上下文</param>
        public FormulaGroupRepository(IDbContext uow):base(uow)
        {

        }

        /// <summary>
        /// 查询所有公式分组
        /// </summary>
        /// <returns></returns>
        public IQueryable<FormulaGroup> GetFormulaGroup()
        {
            return CreateQuery<FormulaGroup>();
        }

        /// <summary>
        /// 更新公式分组
        /// </summary>
        /// <returns></returns>
        public int UpdateFormulaGroup(FormulaGroup item)
        {
            return Update<FormulaGroup>(item);
        }


        /// <summary>
        /// 添加公式分组
        /// </summary>
        /// <returns></returns>
        public long AddFormulaGroup(FormulaGroup item)
        {
            return Add<FormulaGroup,long>(item, p=> p.GroupID);
        }


        /// <summary>
        /// 删除公式分组
        /// </summary>
        /// <returns></returns>
        public int DeleteFormulaGroup(long[] ids)
        {
            return Delete<FormulaGroup>(p => ids.Contains(p.GroupID) || ids.Contains(p.Parent_ID));
        }

        /// <summary>
        /// 删除公式分组
        /// </summary>
        /// <returns></returns>
        public FormulaGroup GetFormulaGroupById(long id)
        {
            return GetByID<FormulaGroup>(id);
        }
    }
}
