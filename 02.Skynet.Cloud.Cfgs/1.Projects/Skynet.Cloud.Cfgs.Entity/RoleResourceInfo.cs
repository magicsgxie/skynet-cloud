using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Upms.Entity
{
    public class RoleResourceInfo
    {
        /// <summary>
        /// 自增字段
        /// </summary>
        public int ID
        {
            set;
            get;
        }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID
        {
            set;
            get;
        }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public int MenuID
        {
            set;
            get;
        }
    }
}
