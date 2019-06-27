using System;
using System.Collections.Generic;
using System.Linq;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Upms.Entity;

namespace UWay.Skynet.Cloud.Upms.Repository
{
    public class OperateLogRepository : ObjectRepository
    {
        public OperateLogRepository(IDbContext dbcontext) : base(dbcontext)
        {
        }

        public int Add(OperationLog info)
        {
            return base.Add<OperationLog>(info);
        }


        public long Update(OperationLog info)
        {
            return base.Update<OperationLog>(info);
        }

        public OperationLog GetById(DateTime loginID)
        {
            return base.GetByID<OperationLog>(loginID);
        }


        public long Delete(DateTime[] ids)
        {
            return base.Delete<OperationLog>(p => ids.Contains(p.LogTime));
        }


        public IQueryable<OperationLog> Query()
        {
            return CreateQuery<OperationLog>();
        }
    }
}
