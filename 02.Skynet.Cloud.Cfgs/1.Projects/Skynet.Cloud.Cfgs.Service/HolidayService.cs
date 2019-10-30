using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Cfgs.Entity;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Request;
using UWay.Skynet.Cloud.Linq;
using UWay.Skynet.Cloud.Cfgs.Service.Interface;
using UWay.Skynet.Cloud.Cfgs.Repository;

namespace UWay.Skynet.Cloud.Cfgs.Services
{
    /// <summary>
    /// 城市信息
    /// </summary>
    public class HolidayService : IHolidaysService
    {
        public int AddHoliday(Holiday user)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                new HolidaysRepository(uow).Add(user);
            }
            return 1;
        }

        public int AddHoliday(IList<Holiday> user)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                new HolidaysRepository(uow).Add(user);
            }

            return 1;
        }

        public int DeleteHoliday(long[] arrayIds)
        {
            throw new NotImplementedException();
        }

        public DataSourceResult GetHolidaysByRequest(DataSourceRequest request)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                return new HolidaysRepository(uow).Query().ToDataSourceResult(request);
            }

        }

        public int UpdateHoliday(Holiday user)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                 new HolidaysRepository(uow).Update(user); ;
            }
            return 1;
        }

        public int UpdateHolidays(IList<Holiday> user)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                new HolidaysRepository(uow).Update(user); ;
            }
            return 1;
        }
    }
}
