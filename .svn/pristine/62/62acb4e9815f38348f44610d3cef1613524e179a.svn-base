#region namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

#endregion

namespace UWay.Skynet.Cloud.Steganogram
{
    /// <summary>
    /// DSA加密
    /// </summary>
    public class DSACryption
    {
        DSACryptoServiceProvider dsa;
        const int length = 1024;
        /// <summary>
        /// 构造函数
        /// </summary>
        public DSACryption()
        {
            dsa = new DSACryptoServiceProvider(length);
        }

        /// <summary>
        /// 创建密钥
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="privateKey">私钥</param>
        public static void CreateKey(out string publicKey,out string privateKey)
        {
            var rsa = new DSACryptoServiceProvider(1024);
            
            publicKey = rsa.ToXmlString(false);
            
            privateKey = rsa.ToXmlString(true);
        }

        /// <summary>
        /// 使用公钥加密数据
        /// </summary>
        /// <param name="data">待加密字符</param>
        /// <param name="privateKey">公钥</param>
        /// <returns></returns>
        public byte[] SignData(string data, string privateKey)
        {
            var vals = UTF8Encoding.Default.GetBytes(data);
            return SignData(vals, privateKey);
        }

        /// <summary>
        /// 使用公钥加密数据
        /// </summary>
        /// <param name="data">待加密字节</param>
        /// <param name="privateKey">私钥</param>
        /// <returns></returns>
        public byte[] SignData(byte[] data, string privateKey)
        {
            dsa.FromXmlString(privateKey);
            return dsa.SignData(data);
        }

        /// <summary>
        /// 验证加密的数据是否正确
        /// </summary>
        /// <param name="data">加密之后的数据</param>
        /// <param name="signed">签名之后的数据</param>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public bool Verify(byte[] data, byte[] signed, string publicKey)
        {
            dsa.FromXmlString(publicKey);
            byte[] hash = new SHA1CryptoServiceProvider().ComputeHash(data);
            return dsa.VerifySignature(hash, signed);
        }

        /// <summary>
        /// 验证加密的数据是否正确
        /// </summary>
        /// <param name="data">加密之后的数据</param>
        /// <param name="signed">签名之后的数据</param>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public bool Verify(string data, byte[] signed, string publicKey)
        {
            return Verify(UTF8Encoding.Default.GetBytes(data), signed, publicKey);
        }
        
    }
}
