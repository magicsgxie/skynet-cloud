using Skynet.Cloud.Upms.Test.Entity;
using System;
using System.Collections.Generic;

namespace Skynet.Cloud.Upms.Test.Service.Interface
{
    public interface IUserService
    {
        User GetById(int userId);

        User Single(string remark);

        IList<User> Page(string aa);
    }
}
