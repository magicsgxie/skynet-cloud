using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Security
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public long RoleId { get; set; }
        /// <summary>
        /// 系统ID
        /// </summary>
        public long SystemId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
    }
}
