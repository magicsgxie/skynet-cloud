using System;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud.Request;
using UWay.Skynet.Cloud.Upms.Entity;

namespace UWay.Skynet.Cloud.Upms.Service.Interface
{
    public interface IOperateService
    {
        DataSourceResult GetByRequest(DataSourceRequest dataSourceRequest);

        OperationLog GetById(DateTime userNo);


        long Add(OperationLog user);


        int Update(OperationLog user);

        int DeleteByIds(DateTime[] arrayIds);
    }
}
