using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace UWay.Skynet.Cloud.Steganogram
{
    /// <summary>
    /// 对称密码体制加密算法
    /// </summary>
    public class DESCryption : IDisposable
    {
        DESCryptoServiceProvider des;
        Encoding encoding = new UnicodeEncoding();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">随机密钥</param>
        /// <param name="iv">随机初始化向量</param>
        public DESCryption(string key, string iv)
        {
            des = new DESCryptoServiceProvider();
            des.Key = Convert.FromBase64String(key);
            des.IV = Convert.FromBase64String(iv);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">随机密钥</param>
        /// <param name="iv">随机初始化向量</param>
        public DESCryption(byte[] key, byte[] iv)
        {
            des = new DESCryptoServiceProvider();
            des.Key = key;
            des.IV = iv;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">密钥</param>
        public DESCryption(string key)
        {
            des = new DESCryptoServiceProvider();
            des.Key = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            des.IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        }

        /// <summary>
        /// 释放对象
        /// </summary>
        public void Dispose()
        {
            des.Clear();
        }

        /// <summary>
        /// 创建密钥
        /// </summary>
        /// <param name="key">随机密钥</param>
        /// <param name="iv">随机初始化向量</param>
        public static void GenerateKey(out string key, out string iv)
        {
            key = "";
            iv = "";
            using (DESCryptoServiceProvider des_o = new DESCryptoServiceProvider())
            {
                des_o.GenerateIV();
                des_o.GenerateKey();
                iv = Convert.ToBase64String(des_o.IV);
                key = Convert.ToBase64String(des_o.Key);
            }
        }
        #region 加密
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text">待加密文本</param> 
        /// <returns>加密后的文本</returns> 
        public string Encrypt(string Text)
        {
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cs);
            sw.Write(Text);
            sw.Close();
            cs.Close();
            byte[] buffer = ms.ToArray();
            ms.Close();
            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// 加密字节流
        /// </summary>
        /// <param name="buffers">字节流</param>
        /// <returns>加密后的字节流</returns>
        public ArraySegment<byte> Encrypt(ArraySegment<byte> buffers)
        {
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(buffers.Array, 0, buffers.Count);
            cs.Close();
            byte[] buffer = ms.ToArray();
            ms.Close();
            ArraySegment<byte> bytes = new ArraySegment<byte>(buffer);
            return bytes;
        }

        /// <summary>
        /// 加密文件流
        /// </summary>
        /// <param name="stream">待加密流</param>
        /// <returns>加密之后的流</returns>
        public Stream Encrypt(Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            cs.Write(buffer, 0, buffer.Length);
            cs.Close();
            return ms;
        }

        #endregion

        #region 解密
        /// <summary> 
        /// 解密文本数据 
        /// </summary> 
        /// <param name="text">文本</param> 
        /// <returns></returns> 
        public string Decrypt(string text)
        {
            byte[] inputByteArray = Convert.FromBase64String(text);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(inputByteArray);
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            string val = sr.ReadLine();
            cs.Close();
            ms.Close();
            des.Clear();
            return val;
        }
        /// <summary>
        /// 解密一段字节流
        /// </summary>
        /// <param name="buffers">字节流</param>
        /// <returns>解密后的字节流</returns>
        public ArraySegment<byte> Decrypt(ArraySegment<byte> buffers)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(buffers.Array, 0, buffers.Count);
            ms.Seek(0, SeekOrigin.Begin);
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
            byte[] buffer = RetrieveBytesFromStream(cs, 1024);
            ms.Close();
            ArraySegment<byte> bytes = new ArraySegment<byte>(buffer);
            return bytes;
        }
        /// <summary>
        /// 解密文件流
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <returns>解密之后的流</returns>
        public Stream Decrypt(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            MemoryStream ms = new MemoryStream();
            Stream compressStream = new CryptoStream(stream, des.CreateDecryptor(), CryptoStreamMode.Read);
            byte[] newByteArray = RetrieveBytesFromStream(compressStream, 1);
            compressStream.Close();
            return new MemoryStream(newByteArray);
        }

        private static byte[] RetrieveBytesFromStream(Stream stream, int bytesblock)
        {

            List<byte> lst = new List<byte>();
            byte[] data = new byte[1024];
            int totalCount = 0;
            while (true)
            {
                int bytesRead = stream.Read(data, 0, data.Length);
                if (bytesRead == 0)
                {
                    break;
                }
                byte[] buffers = new byte[bytesRead];
                Array.Copy(data, buffers, bytesRead);
                lst.AddRange(buffers);
                totalCount += bytesRead;
            }
            return lst.ToArray();
        }
        #endregion

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            if (des != null)
            {
                des.Clear();
            }
        }

        #endregion
    }
}
