using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Security
{
    public interface IResourceProvider
    {
        Task<UserPermission> GetUserPermissionAsync(long userId);
    }
}
