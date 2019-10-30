using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Request;
using UWay.Skynet.Cloud.Ccs.Entity;
namespace UWay.Skynet.Cloud.Ccs.Service.Interface
{
    public interface IHolidaysService
    {
        DataSourceResult GetHolidaysByRequest(DataSourceRequest dataSourceRequest);


        int AddHoliday(Holiday user);

        int AddHoliday(IList<Holiday> user);


        int UpdateHoliday(Holiday user);

        int DeleteHoliday(long[] arrayIds);

        int UpdateHolidays(IList<Holiday> user);
    }
}
