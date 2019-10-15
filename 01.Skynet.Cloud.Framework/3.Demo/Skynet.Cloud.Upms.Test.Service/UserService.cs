using Microsoft.Extensions.Logging;
using Skynet.Cloud.Upms.Test.Entity;
using Skynet.Cloud.Upms.Test.Repository;
using Skynet.Cloud.Upms.Test.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using UWay.Skynet.Cloud;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Extensions;
using UWay.Skynet.Cloud.Linq;

namespace Skynet.Cloud.Upms.Test.Service
{
    public class UserService : IUserService
    {

        //public UserService(LoggerFactory)

        public User GetById(int userId)
        {
            using (var dbContext = UnitOfWork.Get(DbContainer.ContainerName))
            {
                return new UserRepository(dbContext).GetByID(userId);
            }
        }

        public User Single(string aa)
        {
            using (var dbContext = UnitOfWork.Get(DbContainer.ContainerName))
            {
                return new UserRepository(dbContext).Query().Where(o => o.UserNo.Equals(aa)).FirstOrDefault();
            }
        }

        public IList<User> Page(string aa)
        {
            using (var dbContext = UnitOfWork.Get(DbContainer.ContainerName))
            {
                Pagination pagination = new Pagination();
                pagination.Paging = true;
                pagination.PageSize = 10;
                pagination.CurrentPageIndex = 1; 
                return new UserRepository(dbContext).Page(pagination);
            }
        }
    }
}
