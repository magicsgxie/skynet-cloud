using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Nom.Entity;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Linq;
using UWay.Skynet.Cloud.Request;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Nom.Repository
{
    public class NeRepository : ObjectRepository
    {
        //public NeRepository(string containerName)
        //    : base(containerName)
        //{

        //}
        public NeRepository(IDbContext uow)
            : base(uow)
        {

        }

        //private const string _sql = "Select distinct CASE WHEN TO_CHAR({0}) IS NULL THEN '{9}' || {1} ELSE TO_CHAR({0}) END  as ID,'{0}' as LocalID,{1} as Name,'{3}' as RelatedField,{2} as OrderIndex,'{5}' ImageUrl, {6} nodeLevel, {7} haschildren,'{8}'  theme, case when TO_CHAR({0}) is null then 0 else 1 end as ElementType from {4} ";

        //public List<Entity.TreeLayer> GetNeTreeInfos(Entity.TreeViewCfg cfg, int levelID, List<QueryParameter> conditions)
        //{
        //    IDictionary<string, object> parameters = new Dictionary<string, object>();
        //    var sql = GetQuerySql(cfg, levelID, conditions, parameters);
        //    if (sql == null)
        //    {
        //        return null;
        //    }
        //    using (var dt = ExecuteDataTable(sql, parameters))
        //    { 
        //        return dt.ToList<TreeLayer>().ToList();
        //    }
        //}

        //public IQueryable<int> GetCitysByCondition(DataSourceRequest request)
        //{

        //    return CreateQuery<NeCell>().Where(request.Filters).Select(p => p.CityID).Distinct();
        //}



        //public IQueryable<string> GetVendors(int[] citys)
        //{
        //    return CreateQuery<NeBts>().Where(m=>citys.Contains(m.CityID)).Select(p => p.Vendor).Distinct();
        //}

        //public IQueryable<string> GetVendors()
        //{
        //    return CreateQuery<NeBts>().Select(p => p.Vendor).Distinct();
        //}

        public NeCell GetNeCellById(string neCellId) {
            return GetByID<NeCell>(neCellId);
        }


        /// <summary>
        /// 根据外部传入SQL查询厂家信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetVendorsBySql(string sql)
        {
            var dt = ExecuteDataTable(sql);
            return dt;
        }

        /// <summary>
        /// 根据外部传入SQL查询网元
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IEnumerable<NeCell> GetNeCellsBySql(string sql)
        {
            return ExecuteDataTable(sql).ToList<NeCell>(); ;
        }

        //private string GetQuerySql(Entity.TreeViewCfg cfg, int levelID, List<Ufa.Query.QueryParameter> conditions, IDictionary<string, object> parameters)
        //{
        //    string sql = string.Empty;
        //    var fieldvalue = cfg.GetValue(string.Format("{0}{1}", "LayerCaption", levelID + 1));
        //    if(levelID == 0)
        //    {
        //        levelID = levelID + 1;
        //    }
        //    sql = string.Format(_sql, cfg.GetValue(string.Format("{0}{1}", "LayerField", levelID)),
        //                   cfg.GetValue(string.Format("{0}{1}", "LayerCaption", levelID)),
        //                   cfg.GetValue(string.Format("{0}{1}", "LayerOrderBy", levelID)),
        //                   cfg.GetValue(string.Format("{0}{1}", "RelatedField", levelID)),
        //                    cfg.SrcTblName,
        //                    cfg.GetValue(string.Format("{0}{1}", "LayerImage", levelID)),
        //                    levelID + 1,
        //                    fieldvalue == null?0:1,
        //                    cfg.Theme,
        //                    Guid.NewGuid().ToString("N")
        //                    );
        //    StringBuilder sb = new StringBuilder();
        //    var where = conditions.ToWhere(parameters);

        //    if (!where.IsNullOrEmpty())
        //    {
        //        return string.Format("{0} WHERE {1} {2}", sql, where, "order by OrderIndex");
        //    }
        //    return string.Format("{0} {1}", sql, "order by OrderIndex");
        //}



        //public List<TreeLayer> GetNeInfoByMatch(Entity.TreeViewCfg cfg, List<Ufa.Query.QueryParameter> conditions, string match)
        //{
        //    var sql = string.Format(cfg.FuzzySql1, match);
        //    StringBuilder sb = new StringBuilder();
        //    IDictionary<string, object> dic = new Dictionary<string, object>();
        //    if (conditions != null && conditions.Count > 0)
        //    {
        //        foreach (var item in conditions)
        //        {
        //            sb.AppendFormat(" {0} ", item.Where(dic));
        //        }
        //        sql = string.Format("{0} WHERE {1}", sql, sb.ToString());
        //    }

        //    List<TreeLayer> treeLayers = new List<TreeLayer>();
        //    TreeLayer treeLayer = null;
        //    using (var dt = ExecuteDataTable(sql, dic))
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            treeLayer = new TreeLayer();
        //            if (row["NE_SYS_ID"] != DBNull.Value)
        //            {
        //                treeLayer.ID = row["NE_SYS_ID"].ToString();
        //            }
        //            if (row[1] != DBNull.Value)
        //                treeLayer.Name = row[1].ToString();
        //            treeLayers.Add(treeLayer);
        //        }
        //    }
        //    return treeLayers;
        //    //return CreateQuery<TreeLayer>().ToList();
        //}


        public IQueryable<NeBscOrMme> GetNeBscOrMme()
        {
            return CreateQuery<NeBscOrMme>();
        }

        public IQueryable<NeBts> GetNeBts()
        {
            return CreateQuery<NeBts>();
        }

        public IQueryable<NeCell> GetNeCells()
        {
            return CreateQuery<NeCell>();
        }

        

        ///// <summary>
        ///// 获取SelectSQL
        ///// </summary>
        ///// <returns></returns>
        //public string SelectSQL()
        //{
        //    string sql = "select {0} from {1}";
        //    var mapping = this.GetEntityMapping<NeCell>();
        //    string cols = string.Empty;
        //    foreach (var col in mapping.Members)
        //    {
        //        if (!col.IsColumn)
        //            continue;
        //        cols += string.Format("{0} as {1},",col.ColumnName,col.Member.Name);
        //    }
        //    cols = cols.TrimEnd(',');
            
        //    sql = string.Format(sql, cols, mapping.TableName);

        //    return sql;
        //}

        ///// <summary>
        ///// 获取属性对应字段名
        ///// </summary>
        ///// <typeparam name="NeCell"></typeparam>
        ///// <param name="memberName"></param>
        ///// <returns></returns>
        //public new string GetColumnName<NeCell>(string memberName)
        //{
        //    return base.GetColumnName<NeCell>(memberName);
        //}
 

        public IQueryable GetNeCells(IList<IFilterDescriptor> conditions)
        {
            return CreateQuery<NeCell>().Where(conditions);
        }

        public IQueryable<NeCarr> GetNeCarrs()
        {
            return CreateQuery<NeCarr>();
        }

        /// <summary>
        /// 获取C网或者L网信息
        /// </summary>
        /// <param name="neType">制式</param>
        /// <returns></returns>
        public IQueryable<NeCell> GetBasicNeCell()  
        {
            return CreateQuery<NeCell>();
        }

        /// <summary>
        /// 获取C网或者L网基站信息
        /// </summary>
        /// <returns></returns>
        public IQueryable<NeBts> GetNeBtsCOrL()
        {
            return CreateQuery<NeBts>();
        }

        ///// <summary>
        ///// 获取邻区信息
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<NeiCell> GetNeiCells()
        //{
        //    return CreateQuery<NeiCell>();
        //}

    }
}
