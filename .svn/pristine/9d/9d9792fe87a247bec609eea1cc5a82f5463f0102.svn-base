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
    /// 非对称加密
    /// </summary>
    public sealed class RSACryption
    {
        private RSACryptoServiceProvider _rsa;
        private int _length = 1024;

        /// <summary>
        /// 构造函数
        /// </summary>
        public RSACryption(int length) : this()
        {
            _length = length;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public RSACryption()
        {
            _rsa = new RSACryptoServiceProvider(_length);
        }

        /// <summary>
        /// 创建密钥
        /// </summary>
        /// <param name="exponent">公钥</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="modulus">模</param>
        public void CreateKey(out string exponent, out string privateKey, out string modulus)
        {
            privateKey = _rsa.ToXmlString(true);
            RSAParameters parameter = _rsa.ExportParameters(true);
            exponent = BytesToHexString(parameter.Exponent);
            modulus = BytesToHexString(parameter.Modulus);
        }

        /// <summary>
        /// 使用公钥加密数据
        /// </summary>
        /// <param name="data">待加密字节</param>
        /// <param name="exponent">公钥</param>
        /// <param name="modulus">模</param>
        /// <returns></returns>
        public string EncryptData(string data, string exponent, string modulus)
        {
            RSAParameters rsaParameters = new RSAParameters()
            {
                Exponent = HexStringToBytes(exponent),
                Modulus = HexStringToBytes(modulus),
            };
            _rsa.ImportParameters(rsaParameters);
            byte[] sample = _rsa.Encrypt(Encoding.Default.GetBytes(data), false);
            return BytesToHexString(sample);
        }

        /// <summary>
        /// 使用私钥解密数据
        /// </summary>
        /// <param name="data">待解密字节</param>
        /// <param name="privateKey">私钥</param>
        /// <returns></returns>
        public byte[] DecryptData(string data, string privateKey)
        {
            return DecryptData(HexStringToBytes(data), privateKey);
        }

        /// <summary>
        /// 使用私钥解密数据
        /// </summary>
        /// <param name="data">待解密字节</param>
        /// <param name="privateKey">私钥</param>
        /// <returns></returns>
        public byte[] DecryptData(byte[] data, string privateKey)
        {
            _rsa.FromXmlString(privateKey);
            return _rsa.Decrypt(data, false);
        }

        /// <summary>
        /// 签名数据
        /// </summary>
        /// <param name="data">待签名字节</param>
        /// <param name="privateKey">私钥</param>
        /// <returns></returns>
        public byte[] Sign(byte[] data, string privateKey)
        {
            _rsa.FromXmlString(privateKey);
            return _rsa.SignData(data, "MD5");
        }

        /// <summary>
        /// 验证加密的数据是否正确
        /// </summary>
        /// <param name="data">私钥加密之后的数据</param>
        /// <param name="Signature">签名之后的数据</param>
        /// <param name="exponent">公钥</param>
        /// <returns></returns>
        public bool Verify(byte[] data, byte[] Signature, string exponent)
        {
            _rsa.FromXmlString(exponent);
            return _rsa.VerifyData(data, "MD5", Signature);
        }

        /// <summary>
        /// 字节转16进制字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string BytesToHexString(byte[] input)
        {
            StringBuilder hexString = new StringBuilder(64);

            for (int i = 0; i < input.Length; i++)
            {
                hexString.Append(String.Format("{0:X2}", input[i]));
            }
            return hexString.ToString();
        }

        /// <summary>
        /// 16进制转字节
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        private static byte[] HexStringToBytes(string hex)
        {
            if (hex.Length == 0)
            {
                return new byte[] { 0 };
            }

            if (hex.Length % 2 == 1)
            {
                hex = "0" + hex;
            }

            byte[] result = new byte[hex.Length / 2];

            for (int i = 0; i < hex.Length / 2; i++)
            {
                result[i] = byte.Parse(hex.Substring(2 * i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }

            return result;
        }
    }
}
