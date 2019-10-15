using Skynet.Cloud.Upms.Test.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Linq;
using UWay.Skynet.Cloud;

namespace Skynet.Cloud.Upms.Test.Repository
{
    public class UserRepository : ObjectRepository
    {
        public UserRepository(IDbContext dbcontext) : base(dbcontext)
        {
           
        }

        public User GetByID(object id)
        {
            return GetByID<User>(id);
        }

        public IQueryable<User> Query()
        {
            return CreateQuery<User>();
        }

        public IList<User> Page(Pagination pagination)
        {
            return CreateQuery<User>().Paging(pagination).ToList();
        }
    }
}
