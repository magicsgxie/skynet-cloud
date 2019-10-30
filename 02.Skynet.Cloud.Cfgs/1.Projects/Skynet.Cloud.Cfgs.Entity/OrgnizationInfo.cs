using System;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Upms.Entity
{
    
    public class OrgnizationInfo
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public int OrgID
        {
            set;
            get;
        }
        /// <summary>
        /// 上级部门ID
        /// </summary>
        public int ParentID
        {
            set;
            get;
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        
        public string OrgName
        {
            set;
            get;
        }


        /// <summary>
        /// 创建者
        /// </summary>
        //[Column("author")]
        
        public string Author
        {
            set;
            get;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        
        public DateTime? CreateTime
        {
            set;
            get;
        }

        /// <summary>
        /// 修改者
        /// </summary>
        
        public string Mender
        {
            set;
            get;
        }

        
        public string OrgDescription
        {
            set;
            get;
        }


        
        public int SeqNo
        {
            set;
            get;
        }

        
        public int Invalid
        {
            set;
            get;
        }

        /// <summary>
        /// 区域类型
        /// </summary>
        
        public int AreaType
        {
            set;
            get;
        }

        
        public DateTime? LastUpdateTime
        {
            set;
            get;
        }
    }
}
