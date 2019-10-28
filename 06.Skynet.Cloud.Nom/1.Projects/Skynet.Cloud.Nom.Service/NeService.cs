using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Nom.Entity;
using UWay.Skynet.Cloud.Nom.Repository;
using UWay.Skynet.Cloud.Nom.Service.Interface;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Linq;
using UWay.Skynet.Cloud.Request;
using Skynet.Cloud.Noap;

namespace UWay.Skynet.Cloud.Nom.Services
{
    /// <summary>
    /// 网元信息类
    /// </summary>
    
    public class NeService : INeService
    {

        /// <summary>
        /// 根据城市获取厂家编码
        /// </summary>
        /// <param name="netGenerationType"></param>
        /// <param name="citys"></param>
        /// <returns></returns>
        public List<string> GetNeVendorCodesByCitys(NetType netType, int[] citys)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                return new NeRepository(context).GetNeBts().Where( p => citys.Contains(p.CityID)).Select(o => o.Vendor).ToList();
            }
        }

        
        /// <summary>
        /// 获取全网基站数量
        /// </summary>
        /// <param name="netType"></param>
        /// <returns></returns>
        public int GeStaticBtsAllCount(NetType netType)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                var q = new NeRepository(context).GetNeBts().Count();
                return q;
            }
        }
        /// <summary>
        /// 获取全网小区数量
        /// </summary>
        /// <param name="netType"></param>
        /// <returns></returns>
        public int GeStaticCellAllCount(NetType netType)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                var q = new NeRepository(context).GetNeCells().Count();
                return q;
            }
        }

        /// <summary>
        /// 统计城市基站数量
        /// </summary>
        /// <param name="netType">制式</param>
        /// <returns></returns>
        public List<Pair> GetStaticNeBtsCityCount(NetType netType)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                var q = new NeRepository(context).GetNeBts();
                var q1 = from p in q group p by p.CityID into g select new Pair { First = g.Key, Second = g.Count() };

                return q1.ToList();
            }
        }

        /// <summary>
        /// 统计城市扇区数量
        /// </summary>
        /// <param name="netType">制式</param>
        /// <returns></returns>
        public List<Pair> GetStaticNeCellCityCount(NetType netType)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                var q = new NeRepository(context).GetNeCells();
                var q1 = from p in q group p by p.CityID into g select new Pair { First = g.Key, Second = g.Count() };

                return q1.ToList();
            }
        }

        /// <summary>
        /// 统计行政区基站数量
        /// </summary>
        /// <param name="netType">制式</param>
        /// <returns></returns>
        public List<Pair> GetStaticNeBtsCountyCount(NetType netType)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                var q = new NeRepository(context).GetNeBts();
                var q1 = from p in q group p by  int.Parse(p.CountyID) into g select new Pair { First = g.Key, Second = g.Count() };

                return q1.ToList();
            }
        }

        /// <summary>
        /// 统计行政区扇区数量
        /// </summary>
        /// <param name="netType">制式</param>
        /// <returns></returns>
        public List<Pair> GetStaticNeCellCountyCount(NetType netType)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                var q = new NeRepository(context).GetNeCells();
                var q1 = from p in q group p by int.Parse(p.CountyID) into g select new Pair { First = g.Key, Second = g.Count() };

                return q1.ToList();
            }
        }

        /// <summary>
        /// 获取厂家编码
        /// </summary>
        /// <param name="netType">制式</param>
        /// <returns></returns>
        public string[] GetNeVendorCodes(NetType netType)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                return new NeRepository(context).GetNeBts().Select(p => p.Vendor).Distinct().ToArray();
            }
        }

        /// <summary>
        /// 获取厂家编码，根据查询级别
        /// </summary>
        /// <param name="netType">制式</param>
        /// <param name="neRelateionInfo">关联关系</param>
        /// <param name="extensionNeRelation">拓展关系</param>
        /// <param name="neWhere">条件</param>
        /// <returns></returns>
        public string[] GetNeVendorCodesByQueryLevel(NetType netType, string table, string neWhere)
        {
            

            if (string.IsNullOrWhiteSpace(neWhere))
            {
                neWhere = "1=1";
            }

            var sql = string.Format("select distinct a.vendor from ({0}) a where {1}",table, neWhere);


            //string sql = string.Empty;
            //if (extensionNeRelation != null && extensionNeRelation.Dest_Value != "-1" && !string.IsNullOrWhiteSpace(extensionNeRelation.ExtensionTableView))
            //{
            //    ;
            //}
            //else
            //{
            //    sql = string.Format(@" select distinct vendor from {0} where {1} ", neRelateionInfo.BaseDataTableName, neWhere);
            //}
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                var dt =  new NeRepository(context).GetVendorsBySql(sql);
                IList<string> vendors = new List<string>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    //return dt.AsEnumerable().Select(p => p[0].ToString()).ToArray();
                    foreach(DataRow dr in dt.Rows)
                    {
                        if(dr[0] != null)
                            vendors.Add(dr[0].ToString());
                    }
                }
                return vendors.ToArray();
            }

        }

        /// <summary>
        /// 基站
        /// </summary>
        /// <param name="neType">网络制式</param>
        /// <param name="conditions">条件集合</param>
        /// <returns></returns>
        public NeBts[] GetNeBtsByCondition(NetType netType, IList<IFilterDescriptor> conditions)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                return new NeRepository(context).GetNeBts().Where(conditions).ToArray();
            }
        }

        /// <summary>
        /// 分页获取基站信息
        /// </summary>
        /// <param name="neType">制式</param>
        /// <param name="conditions">条件集合</param>
        /// <param name="pagination">分页信息</param>
        /// <returns></returns>
        public DataSourceResult BtsPage(NetType netType, DataSourceRequest request)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                return new NeRepository(context).GetNeBts().ToDataSourceResult(request);
            }
        }

        /// <summary>
        /// 分页获取基站信息
        /// </summary>
        /// <param name="neType">制式</param>
        /// <param name="conditions">条件集合</param>
        /// <param name="pagination">分页信息</param>
        /// <returns></returns>
        public DataSourceResult<NeBts> GetNeBtsPageByCondition(NetType netType, DataSourceRequest request)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                return new NeRepository(context).GetNeBts().ToDataSoureceTResult<NeBts>(request);
            }
        }

        /// <summary>
        /// 获取小区信息
        /// </summary>
        /// <param name="neType">网络制式</param>
        /// <param name="conditions">条件集合</param>
        /// <returns></returns>
        private IEnumerable<NeCell> GetNeCellBySQL(NetType netType, string sql)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                var rep = new NeRepository(context);
                return rep.GetNeCellsBySql(sql);
            }
        }

        /// <summary>
        /// 获取小区信息
        /// </summary>
        /// <param name="neType">网络制式</param>
        /// <param name="conditions">条件集合</param>
        /// <returns></returns>
        public NeCell[] GetNeCellByCondition(NetType netType, IList<IFilterDescriptor> conditions)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                return new NeRepository(context).GetNeCells().Where(conditions).ToArray();
            }
        }
        /// <summary>
        /// 分页获取小区信息
        /// </summary>
        /// <param name="neType">制式</param>
        /// <param name="conditions">条件集合</param>
        /// <param name="pagination">分页信息</param>
        /// <returns></returns>
        public DataSourceResult GetNeCellPageByCondition(NetType netType, DataSourceRequest request)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                return new NeRepository(context).GetNeCells().ToDataSourceResult(request);
            }
        }


        /// <summary>
        /// 获取BSC(MME)级别的网络设备信息
        /// </summary>
        /// <param name="neType">制式</param>
        /// <param name="conditions">条件集合</param>
        /// <returns></returns>
        public NeBscOrMme[] GetNeBscOrMmeByCondition(NetType netType, IList<IFilterDescriptor> conditions)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                return new NeRepository(context).GetNeBscOrMme().Where(conditions).ToArray();
            }
        }

        /// <summary>
        /// 获取基站信息
        /// </summary>
        /// <param name="neType">制式</param>
        /// <param name="conditions">条件集合</param>
        /// <param name="topN">前多少</param>
        /// <returns></returns>
        public NeBts[] GetTopNeBtsByCondition(NetType netType, int topN, IList<IFilterDescriptor> conditions)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                return new NeRepository(context).GetNeBts().Where(conditions).Take(topN).ToArray();
            }
        }
        /// <summary>
        /// 获取小区信息
        /// </summary>
        /// <param name="neType">制式</param>
        /// <param name="conditions">条件集合</param>
        /// <param name="topN">前多少</param>
        /// <returns></returns>
        public NeCell[] GetTopNeCellByCondition(NetType netType, int topN, IList<IFilterDescriptor> conditions)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                return new NeRepository(context).GetNeCells().Where(conditions).Take(topN).ToArray();
            }
        }

        /// <summary>
        /// 获取C网或者L网信息
        /// </summary>
        /// <param name="neType">制式</param>
        /// <returns></returns>
        public List<NeCell> GetBasicNeCell(NetType netType)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                return new NeRepository(context).GetBasicNeCell().ToList();
            }
        }

        /// <summary>
        /// 获取C网或L网基站信息
        /// </summary>
        /// <param name="neType"></param>
        /// <returns></returns>
        public List<NeBts> GetBasicNeBts(NetType netType)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                return new NeRepository(context).GetNeBtsCOrL().ToList();
            }
        }

        /// <summary>
        /// 根据场景获取小区
        /// </summary>
        /// <param name="netType"></param>
        /// <param name="cellConditions"></param>
        /// <param name="groupConditions"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public List<NeCell> GetCellByGroup(NetType netType, IList<IFilterDescriptor> filters)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                using (var r = new NeRepository(context))
                {
                    var ner = new NeGroupItemRepository(context);

                    var q = from ne in r.GetBasicNeCell()
                            join tg in ner.GetNeGroupItems().Where(filters) on ne.NeCellID equals tg.NeSysID
                            select ne;
                            
                   
                    return q.ToList();
                }
            }
        }

        /// <summary>
        /// 根据场景获取小区
        /// </summary>
        /// <param name="netType"></param>
        /// <param name="cellConditions"></param>
        /// <param name="groupConditions"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public DataSourceResult GetCellByGroup(NetType netType, DataSourceRequest request)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                using (var r = new NeRepository(context))
                {
                    var ner = new NeGroupItemRepository(context);

                    var q = from ne in r.GetBasicNeCell()
                            join tg in ner.GetNeGroupItems().Where(request.Filters) on ne.NeCellID equals tg.NeSysID
                            select ne;

                    return q.ToDataSourceResult<NeCell, NeCell>(request.Sorts,request.Groups, request.Aggregates, request.Page, request.PageSize, null);
                }
            }
        }

        public NeCell GetBasicNeCellById(NetType netType, string neCellId)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                using (var r = new NeRepository(context))
                {
                    return r.GetNeCellById(neCellId);
                }
            }
        }

        public List<NeCell> GetBasicNeCellByIds(NetType netType, List<string> neCellIds)
        {
            using (var context = UnitOfWork.Get(netType.ToContainerName(DataBaseType.Normal)))
            {
                using (var r = new NeRepository(context))
                {
                    return r.GetNeCells().Where(t => neCellIds.Contains(t.NeCellID)).ToList();
                }
            }
        }

        //public IDictionary<NetType, IEnumerable<NeCell>> GetNeiCells(NetType neType, string neCellId)
        //{
        //    throw new NotImplementedException();
        //}

        ///// <summary>
        ///// 获取制定网元的邻区
        ///// </summary>
        ///// <param name="neType">制式</param>
        ///// <param name="neCellId">网元id</param>
        ///// <returns></returns>
        //public IDictionary<NetType, IEnumerable<NeCell>> GetNeiCells(NetType neType, string neCellId)
        //{
        //    List<NeiCell> neiCells = null;
        //    IDictionary<NetType, IEnumerable<NeCell>> cells = new Dictionary<NetType, IEnumerable<NeCell>>();
        //    string sql = string.Empty;
        //    string btsColumnName = string.Empty;
        //    string sectorColumnName = string.Empty;
        //    using (var context = UnitOfWork.Get(neType, DataBaseType.Normal))
        //    {
        //        using (var r = new NeRepository(context))
        //        {
        //            sql = r.SelectSQL();
        //            NeCell x = new NeCell();
        //            btsColumnName = r.GetColumnName<NeCell>("BtsID");
        //            sectorColumnName = r.GetColumnName<NeCell>("SectorID");

        //            if (neType == NetType.LTE)
        //            {
        //                DateTime dateTime = DateTime.Now.Date.AddDays(-1);
        //                neiCells = r.GetNeiCells().Where(t => t.StartTime.Equals(dateTime) && t.NeCellId.Equals(neCellId)).ToList();
        //            }
        //            else
        //            {
        //                neiCells = r.GetNeiCells().Where(t => t.NeCellId.Equals(neCellId)).ToList();
        //            }
        //        }
        //    }

        //    Dictionary<NetType, string> dicUnionSQL = new Dictionary<NetType, string>();

        //    foreach (NeiCell nei in neiCells)
        //    {
        //        if (!nei.NetType.HasValue)
        //        {
        //            nei.NetType = neType;
        //        }
        //        if (!dicUnionSQL.ContainsKey(nei.NetType.Value))
        //        {
        //            dicUnionSQL[nei.NetType.Value] = string.Empty;
        //        }

        //        dicUnionSQL[nei.NetType.Value] += string.Format(" {0} where {1}={2} and {3}={4} union", sql, btsColumnName, nei.NeiBtsId, sectorColumnName, nei.NeiSectorId);
        //    }

        //    foreach (var item in dicUnionSQL)
        //    {
        //        cells[item.Key] = GetNeCellBySQL(item.Key, item.Value.TrimEnd("union".ToArray()));
        //    }


        //    return cells;
        //}
    }
}
