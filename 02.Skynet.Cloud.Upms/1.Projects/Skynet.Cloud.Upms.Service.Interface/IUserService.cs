using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Request;
using UWay.Skynet.Cloud.Upms.Entity;

namespace UWay.Skynet.Cloud.Upms.Service.Interface
{
    public interface IUserService
    {

        DataSourceResult GetUsersByRequest(DataSourceRequest dataSourceRequest);

        UserInfo GetUserByUserNo(string userNo);


        long AddUser(UserInfo user);


        int UpdateUser(UserInfo user);

        void DeleteUser(long[] arrayIds);
    }
}
