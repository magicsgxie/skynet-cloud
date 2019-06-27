using Microsoft.AspNetCore.Mvc;
using System;
using UWay.Skynet.Cloud.Security;

namespace UWay.Skynet.Cloud.Mvc
{
    public class BaseController : Controller
    {
        public UserIdentity UserIdentity
        {
            get
            {
                return User.ToUserIdentity();
            }
        }
    }
}
