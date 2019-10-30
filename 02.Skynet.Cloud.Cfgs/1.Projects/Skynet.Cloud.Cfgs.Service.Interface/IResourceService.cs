using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UWay.Skynet.Cloud.Upms.Entity;

namespace UWay.Skynet.Cloud.Upms.Service.Interface
{
    public interface IResourceService
    {
        List<ResourceInfo> GetResourceInfosByUserId(long userId);

        List<ResourceInfo> GetResourceInfosByRoleId(long roleId);
    }
}
