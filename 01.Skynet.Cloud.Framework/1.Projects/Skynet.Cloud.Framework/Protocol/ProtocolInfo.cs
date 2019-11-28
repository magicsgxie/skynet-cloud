using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Protocal
{
    /// <summary>
    /// 协议配置信息
    /// </summary>
    [Table("ufa_connection_db_info")]
    public class ProtocolCfgInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [Id("ID", SequenceName = "SEQ_UFA_CONNECTION_DB_INFO_ID", IsDbGenerated = true)]
        public int CfgID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("Url")]
        public string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("Driver")]
        public string Driver { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Ignore]
        public string ProviderName {  get
            {
                if (!string.IsNullOrEmpty(Driver))
                {
                    return Driver.GetProviderName();
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Column("SERVER_NAME")]
        public string ServerName { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Column("USER_NAME")]
        public string UserName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Column("PASS_WORD")]
        public string Password { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Ignore]
        public string DesUserID
        {
            get
            {
                return UserName.DesDecryption();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        [Ignore]
        public string DesPassword
        {
            get
            {
                return Password.DesDecryption(); 
            }
        }


        /// <summary>
        /// 
        /// </summary>
        [Column("Description")]
        public string Description { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Column("CONNET_POOL_MAXWAIT")]
        public int CONNET_POOL_MAXWAIT { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Column("CONNET_POOL_MAXACTIVE")]
        public int CONNET_POOL_MAXACTIVE { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Column("CONNET_POOL_MAXIDLE")]
        public int CONNET_POOL_MAXIDLE { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Column("CONNET_POOL_VALIDATEQUERY")]
        public string CONNET_POOL_VALIDATEQUERY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("PORT")]
        public int Port
        {
            set;
            get;
        }
      
    }

    /// <summary>
    /// 
    /// </summary>
    public static class DbProviderHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static string GetProviderName(this string driver)
        {
            if (driver.ToUpper().Contains("ManagedDataAccess".ToUpper()))
            {
                return DbProviderNames.Oracle_Managed_ODP;
            }
            else if (driver.ToUpper().Contains("ORACLE_ODP"))
            {
                return DbProviderNames.Oracle_ODP;
            }
            else if (driver.ToUpper().Contains("ORACLE"))
            {
                return DbProviderNames.Oracle;
            }
            else if (driver.ToUpper().Contains("MYSQL"))
            {
                return DbProviderNames.MySQL;
            }
            else if (driver.ToUpper().Contains("SQLCLIENT"))
            {
                return DbProviderNames.SqlServer;
            }
            return driver;
        }
    }
}
