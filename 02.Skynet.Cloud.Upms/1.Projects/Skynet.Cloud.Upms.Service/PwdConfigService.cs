using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Upms.Entity;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Request;
using UWay.Skynet.Cloud.Linq;
using UWay.Skynet.Cloud.Upms.Service.Interface;
using UWay.Skynet.Cloud.Upms.Repository;

namespace UWay.Skynet.Cloud.Upms.Services
{
    public class PwdConfigService : IUserPwdConfigService
    {
        public void Add(PwdConfigInfo pwdConfig)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                new UserPwdConfigRepository(uow).Add(pwdConfig);
            }
            
        }



        public PwdConfigInfo GetPwdConfig()
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                return new UserPwdConfigRepository(uow).Query().SingleOrDefault();
            }
            
        }

        public void Update(PwdConfigInfo pwdConfig)
        {
            using (var uow = UnitOfWork.Get(Unity.ContainerName))
            {
                new UserPwdConfigRepository(uow).Update(pwdConfig);
            }
            
        }
    }
}
