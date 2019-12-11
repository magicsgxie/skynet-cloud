namespace UWay.Skynet.Cloud.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// PermissionAuthorizationRequirement.
    /// </summary>
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Gets url And Button Type.
        /// </summary>
        public UrlAndButtonType UrlAndButtonType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionAuthorizationRequirement"/> class.
        /// </summary>
        /// <param name="strurl">url.</param>
        /// <param name="buttonType">button type.</param>
        /// <param name="isPage">is page or not.</param>
        public PermissionAuthorizationRequirement(string strurl, ButtonType buttonType, bool isPage)
        {
            this.UrlAndButtonType = new UrlAndButtonType()
            {
                Url = strurl,
                ButtonType = (byte)buttonType,
                IsPage = isPage,
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionAuthorizationRequirement"/> class.
        /// 权限验证..
        /// </summary>
        /// <param name="strurl">url.</param>
        /// <param name="buttonType">button type.</param>
        /// <param name="isPage">is page or not.</param>
        public PermissionAuthorizationRequirement(string strurl, byte buttonType, bool isPage)
        {
            this.UrlAndButtonType = new UrlAndButtonType()
            {
                Url = strurl,
                ButtonType = buttonType,
                IsPage = isPage,
            };
        }
    }
}
