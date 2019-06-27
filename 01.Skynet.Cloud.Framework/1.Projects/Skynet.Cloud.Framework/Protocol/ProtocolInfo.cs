using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Protocal
{
    [Table("ufa_connection_db_info")]
    public class ProtocolCfgInfo
    {
        [Id("ID", SequenceName = "SEQ_UFA_CONNECTION_DB_INFO_ID", IsDbGenerated = true)]
        public int CfgID { get; set; }

        [Column("Url")]
        public string Url { get; set; }

        [Column("Driver")]
        public string Driver { get; set; }

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

        [Column("SERVER_NAME")]
        public string ServerName { get; set; }

        [Column("USER_NAME")]
        public string UserName { set; get; }

        [Column("PASS_WORD")]
        public string Password { get; set; }


        [Ignore]
        public string DesUserID
        {
            get
            {
                return UserName.DesDecryption();
            }

        }

        [Ignore]
        public string DesPassword
        {
            get
            {
                return Password.DesDecryption(); 
            }
        }

        [Column("Description")]
        public string Description { get; set; }

        [Column("CONNET_POOL_MAXWAIT")]
        public int CONNET_POOL_MAXWAIT { get; set; }

        [Column("CONNET_POOL_MAXACTIVE")]
        public int CONNET_POOL_MAXACTIVE { get; set; }

        [Column("CONNET_POOL_MAXIDLE")]
        public int CONNET_POOL_MAXIDLE { get; set; }

        [Column("CONNET_POOL_VALIDATEQUERY")]
        public string CONNET_POOL_VALIDATEQUERY { get; set; }

        [Column("PORT")]
        public int Port
        {
            set;
            get;
        }
      
    }

    public static class DbProviderHelper
    {
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
