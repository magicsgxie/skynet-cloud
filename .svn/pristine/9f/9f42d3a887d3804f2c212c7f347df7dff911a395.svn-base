using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Steganogram
{
    public class AESCryption
    {
        private byte[] IV;//偏移向量
        private string KEY;//密钥
        Encoding encoding = new UTF8Encoding();
        public AESCryption(string key, string iv)
        {
            this.IV = encoding.GetBytes(iv);
            this.KEY = key;
        }

        public AESCryption(string key, byte[] iv)
        {
            this.IV = iv;
            this.KEY = key;
        }

        /// <summary>
        /// AES加密方法，key长度《=32
        /// </summary>
        /// <param name="key"></param>
        public AESCryption(string key)
        {
            this.IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            this.KEY = key;
        }

        /// <summary>
        /// 对称加密
        /// </summary>
        /// <param name="content">内容</param>
        public string Encryption(string content)
        {
            string encodeContent = string.Empty;
            byte[] bytData = encoding.GetBytes(content);
            byte[] bytSalt = IV;

            using (Aes aes = new AesManaged())
            {
                Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(KEY, bytSalt);
                aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
                aes.KeySize = aes.LegalKeySizes[0].MaxSize;
                aes.Key = deriveBytes.GetBytes(aes.KeySize / 8);
                aes.IV = deriveBytes.GetBytes(aes.BlockSize / 8);
                ICryptoTransform cryptor = aes.CreateEncryptor();
                MemoryStream stream = new MemoryStream();

                using (CryptoStream cs = new CryptoStream(stream, cryptor, CryptoStreamMode.Write))
                {
                    cs.Write(bytData, 0, bytData.Length);
                }
                encodeContent = Convert.ToBase64String(stream.ToArray());
            }

            return encodeContent;
        }

        /// <summary>
        /// 对称解密
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns>解密后内容</returns>
        public string Decryption(string content)
        {
            string decryptedContent = string.Empty;
            byte[] bytData = Convert.FromBase64String(content);
            byte[] bytSalt = IV;

            using (Aes aes = new AesManaged())
            {
                Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(KEY, bytSalt);
                aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
                aes.KeySize = aes.LegalKeySizes[0].MaxSize;
                aes.Key = deriveBytes.GetBytes(aes.KeySize / 8);
                aes.IV = deriveBytes.GetBytes(aes.BlockSize / 8);

                ICryptoTransform cryptor = aes.CreateDecryptor();
                MemoryStream stream = new MemoryStream();

                using (CryptoStream cs = new CryptoStream(stream, cryptor, CryptoStreamMode.Write))
                {
                    cs.Write(bytData, 0, bytData.Length);
                }

                byte[] decryptBytes = stream.ToArray();
                decryptedContent = encoding.GetString(decryptBytes, 0, decryptBytes.Length);
            }

            return decryptedContent;
        }
    }
}
