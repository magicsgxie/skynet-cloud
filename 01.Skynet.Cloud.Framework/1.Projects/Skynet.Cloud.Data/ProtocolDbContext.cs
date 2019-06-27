using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Protocal;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 连接字符数据库信息（包括HTTP，DbConnection,Ftp）
    /// </summary>
    public class ProtocolDbContext : DbContext
    {
        //连接字符串名称：基于Config文件中连接字符串的配置
        //const string connectionStringName = "DB_Oracle_ConnStr";
        //构造dbConfiguration 对象
        //static DbConfiguration dbConfiguration;

        //static ProtocolDbContext()
        //{
        //    DbContextOption dbContextOption = new DbContextOption() {
        //        Container = "DB_Oracle_ConnStr",
        //        ConnectionString = "",
        //        MappingFile = ""
        //    };
        //    if (!DbConfiguration.Items.TryGetValue(dbContextOption.Container, out dbConfiguration))
        //    {
        //        dbConfiguration = DbConfiguration
        //          .Configure(dbContextOption)
        //          // .SetSqlLogger(() =>SqlLog.Trace)
        //          .SetMappingConversion(MappingConversion.Plural)
        //          .AddClass<ProtocolInfo>()//注册映射类
        //          .AddClass<ProtocolCfgInfo>()//注册映射类
        //          ;

        //    }
        //}

        public ProtocolDbContext(DbContextOption dbContextOption):base(dbContextOption)
        {
            
        }

        //public ProtocolDbContext()
        //    : base(dbConfiguration)
        //{
        //}
        #region 
        //public readonly IDbSet<ProtocolInfo> ProtocolInfo;
        //public readonly IDbSet<ProtocolCfgInfo> ProtocolCfgInfo;
        #endregion
    }
}
