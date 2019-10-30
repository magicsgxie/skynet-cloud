using System;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud.Request;
using UWay.Skynet.Cloud.Upms.Entity;

namespace UWay.Skynet.Cloud.Upms.Service.Interface
{
    public interface IRoleInfoService
    {
        IList<RoleInfo> GetRoleInfosByRequest(DataSourceRequest dataSourceRequest);

        RoleInfo GetRoleInfoByUserNo(string userNo);


        long AddRoleInfo(RoleInfo user);


        int UpdateRoleInfo(RoleInfo user);

        int DeleteRoleInfo(long[] arrayIds);
    }
}
