//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using UWay.Skynet.Cloud.Cache;
//using UWay.Skynet.Cloud.Cache.Redis;
//using UWay.Skynet.Cloud.Security;

//namespace UWay.Skynet.Cloud.Mvc.Filters
//{
//    /// <summary>
//    /// permission storage
//    /// </summary>
//    public class PermissionStorageService : IPermissionStorageContainer
//    {
//        private IHttpContextAccessor _httpContextAccessor;
//        private IResourceProvider _resourceProvider;
//        private ICachingProvider _cacheFactory;
//        public PermissionStorageService(
//            ICachingProvider cacheFactory,
//            IHttpContextAccessor httpContextAccessor,
//            IResourceProvider resourceProvider)
//        {
//            _cacheFactory = cacheFactory;
//            _httpContextAccessor = httpContextAccessor;
//            _resourceProvider = resourceProvider;
//        }

//        private UserIdentity UserIdentity()
//        {
//            return _httpContextAccessor.HttpContext.User.ToUserIdentity();
//        }
//        /// <summary>
//        /// 权限KEY
//        /// </summary>
//        public string Key => "PERMISSION_" + UserIdentity().UserId;

//        /// <summary>
//        /// 存储数据
//        /// </summary>
//        /// <param name="obj"></param>
//        public void Store(object obj)
//        {
//            string json = obj.JsonSerialize();
//            _cacheFactory.Set(this.Key, json);
//        }

//        /// <summary>
//        /// 获取缓存
//        /// </summary>
//        /// <returns></returns>
//        public object Get()
//        {
//            var res = _cacheFactory.Get(this.Key);
//            return res;
//        }

//        /// <summary>
//        /// 异步获取用户权限
//        /// </summary>
//        /// <returns></returns>
//        public async Task<UserPermission> GetPermissionAsync()
//        {
//            long userid = UserIdentity().UserId;
//            string res = this.Get() as string;
//            if (string.IsNullOrEmpty(res))
//            {
//                var permission = await _resourceProvider.GetUserPermissionAsync(userid);
//                this.Store(permission);
//                return permission;
//            }
//            return res.JsonDeserialize<UserPermission>();
//        }



//        /// <summary>
//        /// 异步初始化权限
//        /// </summary>
//        /// <returns></returns>
//        public async Task InitAsync()
//        {
//            long userid = UserIdentity().UserId;
//            var permission = await _resourceProvider.GetUserPermissionAsync(userid);
//            this.Store(permission);
//        }
//    }


//    public class RedisPermissionStorageService : IPermissionStorageContainer
//    {

//        private static RedisClient _redisClient;
//        private IHttpContextAccessor _httpContextAccessor;
//        private IResourceProvider _resourceProvider;
//        public RedisPermissionStorageService(
//            IHttpContextAccessor httpContextAccessor,
//            IConfiguration Configuration,
//            IResourceProvider resourceProvider)
//        {
//            _httpContextAccessor = httpContextAccessor;
//            _resourceProvider = resourceProvider;

//            _redisClient = RedisClientSingleton.GetInstance(Configuration);
//        }
//        private UserIdentity UserIdentity()
//        {
//            return _httpContextAccessor.HttpContext.User.ToUserIdentity();
//        }

//        /// <summary>
//        /// 权限KEY
//        /// </summary>
//        public string Key => "PERMISSION_" + UserIdentity().UserId;

//        /// <summary>
//        /// 存储数据
//        /// </summary>
//        /// <param name="obj"></param>
//        public void Store(object obj)
//        {
//            string json = obj.JsonSerialize();
//            _redisClient.GetDefaultDatabase().StringSet(this.Key, json);
//        }

//        /// <summary>
//        /// 获取缓存
//        /// </summary>
//        /// <returns></returns>
//        public object Get()
//        {
//            var res = _redisClient.GetDefaultDatabase().StringGet(this.Key);
//            return res;
//        }

//        /// <summary>
//        /// 异步获取用户权限
//        /// </summary>
//        /// <returns></returns>
//        public async Task<UserPermission> GetPermissionAsync()
//        {
//            long userid = UserIdentity().UserId;
//            string res = this.Get() as string;
//            if (string.IsNullOrEmpty(res))
//            {
//                var permission = await _resourceProvider.GetUserPermissionAsync(userid);
//                this.Store(permission);
//                return permission;
//            }
//            return res.JsonDeserialize<UserPermission>();
//        }



//        /// <summary>
//        /// 异步初始化权限
//        /// </summary>
//        /// <returns></returns>
//        public async Task InitAsync()
//        {
//            long userid = UserIdentity().UserId;
//            var permission = await _resourceProvider.GetUserPermissionAsync(userid);
//            this.Store(permission);
//        }
//    }
//}
