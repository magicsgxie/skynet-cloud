using Skynet.Cloud.Upms.Test.Entity;
using System;

namespace Skynet.Cloud.Upms.Test.Service.Interface
{
    public interface IUserService
    {
        User GetById(int userId);

        User Single(string remark);
    }
}
