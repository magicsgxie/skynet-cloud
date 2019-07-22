using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Request;
using UWay.Skynet.Cloud.Upms.Entity;

namespace UWay.Skynet.Cloud.Upms.Service.Interface
{
    public interface IOrgService
    {
        DataSourceResult GetByRequest(DataSourceRequest dataSourceRequest);

        OrgnizationInfo GetById(int orgId);


        long Add(OrgnizationInfo user);


        int Update(OrgnizationInfo user);

        int DeleteByIds(int[] arrayIds);
    }
}
