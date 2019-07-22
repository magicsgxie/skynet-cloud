using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UWay.Skynet.Cloud.FileHandling
{
    /// <summary>
    /// 文件管理接口
    /// </summary>
    public interface IFileHandler
    {
        string AddFile(string fileName, string folder, string content);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file">文件名称</param>
        /// <param name="rename">是否重命名</param>
        /// <param name="folder">目标文件夹名称</param>
        /// <returns></returns>
        string SaveFile(string file, string folder, bool rename);

        /// <summary>
        /// 将一个文件流存储到系统中
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="inputStream">文件流</param>
        /// <param name="rename">是否重新命名</param>
        /// <param name="folder"></param>
        /// <returns></returns>
        string SaveFile(string fileName, string folder, Stream inputStream, bool rename);

        /// <summary>
        /// 移除一个文件
        /// </summary>
        /// <param name="name">文件名称</param>
        /// <returns></returns>
        bool Remove(string name);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Stream OpenFile(string name);

        /// <summary>
        /// 找开文件
        /// </summary>
        /// <param name="name">文件名</param>
        /// <param name="mode"></param>
        /// <returns></returns>
        Stream OpenFile(string name, FileMode mode);

        /// <summary>
        /// 扩展数据流添加字节数据的方法
        /// </summary>
        /// <param name="stream">stream</param>
        /// <param name="buffer">buffer</param>
        void Append(Stream stream, byte[] buffer);


        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="name">文件名称</param>
        /// <returns>文件信息</returns>
        FileInfomation GetFileInformation(string name);

        /// <summary>
        /// 给文件重新命名
        /// </summary>
        /// <param name="oldFileName">旧的文件名</param>
        /// <param name="newFileName">新的文件名</param>
        /// <returns>成功返回true,否则返回false</returns>
        bool Rename(string oldFileName, string newFileName);

        /// <summary>
        /// 获取路径下所有文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        FileInfo[] GetAllFiles(string path);

        string Zip(string fileName, string subfolder, IEnumerable<string> compressOrginalPaths);

        void Zip(string fileName, string directory);

        void UnZip(string compressfilepath, string uncompressdir);
    }
}
