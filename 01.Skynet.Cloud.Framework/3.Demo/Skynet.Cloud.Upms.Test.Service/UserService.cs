using Microsoft.Extensions.Logging;
using Skynet.Cloud.Upms.Test.Entity;
using Skynet.Cloud.Upms.Test.Repository;
using Skynet.Cloud.Upms.Test.Service.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using UWay.Skynet.Cloud;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Extensions;
using UWay.Skynet.Cloud.Linq;
using UWay.Skynet.Cloud.Request;

namespace Skynet.Cloud.Upms.Test.Service
{
    public class UserService : IUserService
    {

        //public UserService(LoggerFactory)

        public User GetById(int userId)
        {
            using (var dbContext = UnitOfWork.Get(DbContainer.ContainerName))
            {
                return new UserRepository(dbContext).GetByID(userId);
            }
        }

        public User Single(string aa)
        {
            using (var dbContext = UnitOfWork.Get(DbContainer.ContainerName))
            {
                return new UserRepository(dbContext).Query().Where(o => o.UserNo.Equals(aa)).FirstOrDefault();
            }
        }

        public IList<User> Page(string aa)
        {
            using (var dbContext = UnitOfWork.Get(DbContainer.ContainerName))
            {
                Pagination pagination = new Pagination();
                pagination.Paging = true;
                pagination.PageSize = 10;
                pagination.CurrentPageIndex = 1; 
                return new UserRepository(dbContext).Page(pagination);
            }
        }

        public DataSourceTableResult Page()
        {
            using (var dbContext = UnitOfWork.Get(DbContainer.ContainerName))
            {
                //Pagination pagination = new Pagination();
                //pagination.Paging = true;
                //pagination.PageSize = 10;
                //pagination.CurrentPageIndex = 1;
                IList<SortDescriptor> sorts = new List<SortDescriptor>();
                sorts.Add(new SortDescriptor { Member = "CITY_ID", SortDirection = ListSortDirection.Ascending });
                DataSourceRequest request = new DataSourceRequest { 
                    Page = 1,
                    PageSize = 2,
                    Sorts = sorts
                };
                var sql = @"SELECT 
  PROVINCE_NAME,
  CITY_NAME,
  COUNTY_NAME,
  ENB_ID,
  ENB_NAME,
  LONGITUDE,
  LATITUDE,
  ADDRESS,
  VENDOR,
  IPV4,
  IPV4GATEWAY,
  IPV6,
  IPV6GATEWAY,
  SUB_MASK,
  MME_ID,
  S_GW_ID,
  TRANSMISSION_POWER,
  STATION_GRADE,
  PYLON_TYPE,
  PYLON_HIGH,
  PLATFORM_CNT,
  IS_SHARED,
  BUILD_VENDOR,
  MAINTAIN_DEPT,
  OPEN_TIME,
  ROOM_INFORMATION,
  SUBSTATION_LONGITUDE,
  SUBSTATION_LATITUDE,
  SUBSTATION_NAME,
  LAST_UPDATE_TIME,
  LAST_UPDATE_NAME,
  REMARK,
  RESERVE_FIELD1,
  RESERVE_FIELD2,
  RESERVE_FIELD3,
  RESERVE_FIELD4,
  RESERVE_FIELD5,
  NE_ENB_ID,
  CITY_ID,
  COUNTY_ID
FROM
  UFA_TAIZHANG_ENB WHERE CITY_ID IN (510) 
  AND VENDOR IN ('ZY0808') 
  AND ENB_ID IN (286016, 286020, 286022)";

                return new UserRepository(dbContext).ToPage(sql, request);
            }
        }
    }
}
