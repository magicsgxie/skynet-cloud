using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Mvc
{
    /// <summary>
    /// 权限访问状态返回
    /// </summary>
    public static class PermissionStatusCodes
    {
        public const int Status0No = 0;
        public const int Status1Ok = 1;
        public const int Status2Unauthorized = 403;
    }
}
