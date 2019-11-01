using Skynet.Cloud.Upms.Test.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Request;

namespace Skynet.Cloud.Upms.Test.Service.Interface
{
    public interface IRemoteTest
    {
        Task<R<User>> GetUser(long id);
    }
}
