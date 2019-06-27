using System;

namespace UWay.Skynet.Cloud.Upms.Entity
{
    /// <summary>
    /// 用户级别-0：省级用户，1：地市用户
    /// </summary>
    public enum UserLevelEnum
    {
        /// <summary>
        /// 省级
        /// </summary>
        Province = 0,
        /// <summary>
        /// 地市
        /// </summary>
        City = 1,
        /// <summary>
        /// 第三方
        /// </summary>
        ThirdParty = 3,
        /// <summary>
        /// 无
        /// </summary>
        None = 4,
    }
}
