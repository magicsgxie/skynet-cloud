using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Upms.Entity;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Request;
using UWay.Skynet.Cloud.Linq;
using UWay.Skynet.Cloud.Upms.Service.Interface;
using UWay.Skynet.Cloud.Upms.Repository;

namespace UWay.Skynet.Cloud.Upms.Services
{
    /// <summary>
    /// 城市信息
    /// </summary>
    public class ResourceService : IResourceService
    {
        public List<ResourceInfo> GetResourceInfosByRoleId(long roleId)
        {
            throw new NotImplementedException();
        }

        public List<ResourceInfo> GetResourceInfosByUserId(long userId)
        {
            throw new NotImplementedException();
        }
    }
}
