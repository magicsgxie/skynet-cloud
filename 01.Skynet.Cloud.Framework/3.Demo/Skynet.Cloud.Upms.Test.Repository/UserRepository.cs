using Skynet.Cloud.Upms.Test.Entity;
using System;
using System.Linq;
using UWay.Skynet.Cloud.Data;

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
    }
}
