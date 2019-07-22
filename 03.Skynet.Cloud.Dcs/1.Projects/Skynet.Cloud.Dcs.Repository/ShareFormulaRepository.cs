/************************************************************************************
 * Copyright (c) 2016 优网科技 All Rights Reserved.
 * CLR版本： 4.0.30319.42000
 * 机器名称：UWAY15003
 * 公司名称：优网科技
 * 命名空间：UWay.Skynet.Cloud.Dcs.Repository
 * 文件名：  ShareFormulaRepository
 * 版本号：  V1.0.0.0
 * 唯一标识：efb99e53-7fd7-45ec-abb6-d720071b2359
 * 当前的用户域：UWAY15003
 * 创建人：  谢韶光
 * 电子邮箱：xiesg@uway.cn
 * 创建时间：2016/4/13 16:29:24
 * 描述：
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016/4/13 16:29:24
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
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Dcs.Repository
{
    /// <summary>
    /// 私有公式共享
    /// </summary>
    public class ShareFormulaRepository:ObjectRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context">数据库操作上下文</param>
        public ShareFormulaRepository(IDbContext context) : base(context)
        {
        }


        /// <summary>
        /// 城市用户共享
        /// </summary>
        /// <param name="tableName">操作表数组</param>
        /// <param name="citys">城市</param>
        /// <param name="userid">用户</param>
        /// <returns></returns>
        public long[] GetShareFormulaIDsByCityAndUser(string[] tableName,string citys, string userid)
        {
            var sql = string.Format(@"select distinct formulaid from (
                    select formulaid from {0} where instr(','|| @citys || ',',','||city_id||',') > 0
                    union select formulaid from {1} where LOGINNAME = @userid) ", tableName[0], tableName[1]);
            var dt = ExecuteDataTable(sql, new { citys = citys, userid = userid });
            if (dt.Rows.Count > 0)
            {
                var list = new List<long>();
                foreach(DataRow dr in dt.Rows)
                {
                    list.Add(dr[0].ToString().ToLong());
                }
                return list.ToArray();
            }
            else
                return null;
        }

        /// <summary>
        /// 城市共享
        /// </summary>
        /// <param name="tableName">城市共享表名</param>
        /// <param name="citys">城市列表</param>
        /// <returns></returns>
        public long[] GetShareFormulaIDsByCity(string tableName, string citys)
        {
            var sql = string.Format(@"select distinct formulaid from {0} where instr(','|| @citys || ',',','||city_id||',') > 0 ", tableName);
            var dt = ExecuteDataTable(sql, new { citys = citys});
            if (dt.Rows.Count > 0) {
                var list = new List<long>();
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(dr[0].ToString().ToLong());
                }
                return list.ToArray();
            }
            else
                return null;
        }

        /// <summary>
        /// 用户共享
        /// </summary>
        /// <param name="tableName">用户共享表名</param>
        /// <param name="userID">用户信息</param>
        /// <returns></returns>
        public long[] GetShareFormulaIDsByUser(string tableName,string userID)
        {
            var sql = string.Format(@" select distinct formulaid from {0} where LOGINNAME = @userid ", tableName);
            var dt = ExecuteDataTable(sql, new { userid = userID });
            if (dt.Rows.Count > 0) {
                var list = new List<long>();
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(dr[0].ToString().ToLong());
                }
                return list.ToArray();
            }
            else
                return null;
        }
    }
}
