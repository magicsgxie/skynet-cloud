using System;
using System.Collections.Generic;
using System.Linq;
using CR=System.Security.Cryptography;
using System.Text;


namespace UWay.Skynet.Cloud.Steganogram
{
    /// <summary>
    /// 扩展方法帮助类MD5Cryption
    /// </summary>
    public static class MD5Cryption
    {
        /// <summary>
        /// 获取字符的MD5
        /// </summary>
        /// <param name="val">字符串</param>
        /// <returns></returns>
        public static string MD5(this string val)
        {
            if (string.IsNullOrEmpty(val))
                return string.Empty;
            string result = string.Empty;
            CR.MD5 md5 = CR.MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(val));

            //result = BitConverter.ToString(s).Replace("-", string.Empty);
            StringBuilder sBuilder = new StringBuilder(32);
            for (int i = 0; i < s.Length; i++)
                sBuilder.Append(s[i].ToString("x2"));

            return sBuilder.ToString();
        }
    }
}
