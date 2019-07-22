using System;
using System.Collections.Generic;
using System.Linq;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Dcs.Entity;

namespace Skynet.Cloud.Dcs.Repository
{
    public class ColorSettingRepository : ObjectRepository
    {
        public ColorSettingRepository(IDbContext dbcontext) : base(dbcontext)
        {
        }

        public long Add(ColorLevelSetting colorLevelSetting)
        {
            return Add<ColorLevelSetting, long>(colorLevelSetting, p => p.Id);
        }

        public long Update(ColorLevelSetting colorLevelSetting)
        {
            return Update<ColorLevelSetting>(colorLevelSetting);
        }

        public long Delete(IEnumerable<long> idArray)
        {
            return base.Delete< ColorLevelSetting>(p => idArray.Contains(p.Id));
        }

        public long DeleteDynamic(IEnumerable<long> idArray)
        {
            return base.Delete<ColorLevelDynamicInfo>(p => idArray.Contains(p.Id));
        }

        public void Add(IEnumerable<ColorLevelSetting> colorLevelSettings)
        {
            Batch<ColorLevelSetting, ColorLevelSetting>(colorLevelSettings, (u, v) => u.Insert(v));
        }

        public void Add(IEnumerable<ColorLevelDynamicInfo> colorLevelSettings)
        {
            Batch<ColorLevelDynamicInfo, ColorLevelDynamicInfo>(colorLevelSettings, (u, v) => u.Insert(v));
        }

        public void Update(IEnumerable<ColorLevelSetting> colorLevelSettings)
        {
            Batch<ColorLevelSetting, ColorLevelSetting>(colorLevelSettings, (u, v) => u.Update(v));
        }

        public IQueryable<ColorLevelSetting> All()
        {
            return  CreateQuery<ColorLevelSetting>();
        }
    }
}
