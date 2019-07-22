using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;
using UWay.Skynet.Cloud.Cache;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Dcs.Repository
{
    public class NeLevelRelRepository:ObjectRepository
    {
        //internal const string CACHE_NAME = "default";
        //static Lazy<UWay.Ufa.Cache.ICache> _lazy = new Lazy<UWay.Ufa.Cache.ICache>(GetCache);
        //static ICache GetCache()
        //{
        //    return UWay.Ufa.Cache.CacheManager.GetCache(CACHE_NAME);
        //}
        //static private ICache Cache { get { return _lazy.Value; } }

        //public NeLevelRelRepository(string containerName):base(containerName)
        //{

        //}

        public NeLevelRelRepository(IDbContext uow)
            : base( uow)
        {

        }
        
        public IQueryable<NeLevelRelInfo> GetNeLevelRelInfos()
        {
            return CreateQuery<NeLevelRelInfo>();
        }       

    }
}
