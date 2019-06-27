using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Security
{ 
    /// <summary>
    /// 用户权限
    /// </summary>
public class UserPermission
    {
        /// <summary>
        /// 菜单
        /// </summary>
        public IEnumerable<Menu> Menus { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public IEnumerable<Role> Roles { get; set; }

        /// <summary>
        /// 其他
        /// </summary>
        public object Others { get; set; }
    }
}
