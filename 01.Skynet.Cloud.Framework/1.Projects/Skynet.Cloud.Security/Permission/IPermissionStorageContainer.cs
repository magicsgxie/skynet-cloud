using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Security
{
    /// <summary>
    /// permission storage
    /// </summary>
    public interface IPermissionStorageContainer : IStorageContainer
    {

        /// <summary>
        /// 异步获取用户权限
        /// </summary>
        /// <returns></returns>
        Task<UserPermission> GetPermissionAsync();

        /// <summary>
        /// 异步初始化权限
        /// </summary>
        /// <returns></returns>
        Task InitAsync();
    }
}
