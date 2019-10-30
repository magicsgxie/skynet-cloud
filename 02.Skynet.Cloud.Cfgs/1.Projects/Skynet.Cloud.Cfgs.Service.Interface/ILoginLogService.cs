using System;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud.Request;
using UWay.Skynet.Cloud.Upms.Entity;

namespace UWay.Skynet.Cloud.Upms.Service.Interface
{
    public interface ILoginLogService
    {
        DataSourceResult GetByRequest(DataSourceRequest dataSourceRequest);

        LoginLog GetById(long loginID);


        long Add(LoginLog user);


    }
}
