using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using UWay.Skynet.Cloud.Extensions;
using UWay.Skynet.Cloud.Data;
using Newtonsoft.Json;

namespace UWay.Skynet.Cloud.Security
{
    /// <summary>
    /// Claims extentions
    /// </summary>
    public static class ClaimsPrincipalExtentions
    {
        /// <summary>
        /// ClaimsPrincipal 转 UserIdentity
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static UserIdentity ToUserIdentity(this ClaimsPrincipal principal)
        {
            UserIdentity user = new UserIdentity();
            var claims = principal.Claims.ToList();
            user.UserId = claims.Single(m => m.Type == ClaimTypes.Sid).Value.ToInt64();
            user.UserName = claims.Single(m => m.Type == ClaimTypes.Name).Value;
            if (claims.SingleOrDefault(m => m.Type == ClaimTypes.Uri) != null)
            {
                user.HeadImg = claims.Single(m => m.Type == ClaimTypes.Uri).Value;
            }
            user.Sex = (UserSex)claims.Single(m => m.Type == ClaimTypes.Gender).Value.ToInt32();
            var other = claims.SingleOrDefault(m => m.Type == ClaimTypes.UserData);
            if (other != null)
            {
                user.Other = other.Value.JsonDeserialize();
            }
            return user;
        }

        /// <summary>
        /// UserIdentity 转 Claim
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <returns></returns>
        public static List<Claim> ToClaims(this UserIdentity userIdentity)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userIdentity.UserName));
            claims.Add(new Claim(ClaimTypes.Gender, ((int)userIdentity.Sex).ToString()));
            claims.Add(new Claim(ClaimTypes.Sid, userIdentity.UserId.ToString()));
            if (!string.IsNullOrEmpty(userIdentity.HeadImg))
            {
                claims.Add(new Claim(ClaimTypes.Uri, userIdentity.HeadImg));
            }
            if (userIdentity.Other != null)
            {
                claims.Add(new Claim(ClaimTypes.UserData, userIdentity.Other.JsonSerialize()));
            }
            return claims;
        }
    }
}
