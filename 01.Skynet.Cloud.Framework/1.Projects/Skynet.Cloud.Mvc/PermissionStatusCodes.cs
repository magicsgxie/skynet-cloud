namespace UWay.Skynet.Cloud.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// 权限访问状态返回.
    /// </summary>
    public static class PermissionStatusCodes
    {
        /// <summary>
        /// Status No.
        /// </summary>
        public const int Status0No = 0;

        /// <summary>
        /// Status Ok.
        /// </summary>
        public const int Status1Ok = 1;

        /// <summary>
        /// Status 403.
        /// </summary>
        public const int Status2Unauthorized = 403;
    }
}
