using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Protocal
{
    /// <summary>
    /// 协议类型
    /// </summary>
    public enum ProtocalType
    {
        /// <summary>
        /// DB
        /// </summary>
        DB = 0,
        /// <summary>
        /// Ftp
        /// </summary>
        Ftp = 1,
        /// <summary>
        /// Memory
        /// </summary>
        Memory = 2,
        /// <summary>
        /// File
        /// </summary>
        File = 3,
        /// <summary>
        /// Http
        /// </summary>
        Http = 4
    }

    /// <summary>
    /// 协议信息定义
    /// </summary>
    [Table("ufa_connection_info")]
    public class ProtocolInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [Id("ID", SequenceName = "SEQ_UFA_CONNECTION_INFO_ID", IsDbGenerated = true)]
        public int ProtocolID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("Type")]
        public ProtocalType ProtocalType { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Column("CONN_KEY")]
        public string ContainerName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("CONN_RELATE_ID")]
        public int CfgID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("IS_CONN_POOL")]
        public bool IsConnPool { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("UPDATE_TIME")]
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("DATABASE_NAME")]
        public string DataBaseName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("DESCRIPTION", DbType = DBType.NText)]
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Ignore]
        public ProtocolCfgInfo Cfg { set; get; }

    }
}
