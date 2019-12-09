using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.Logging;
using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.Writers;
using SharpCompress.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UWay.Skynet.Cloud.FileHandling
{
    /// <summary>
    /// <see cref="IFileHandler"/>文件存储实现
    /// </summary>
    public class FileHandler : IFileHandler
    {
        private static readonly string SKYNET_CLOUD = "skynet:cloud";
        private static readonly string SKYNET_CLOUD_ROOT = "filepath";

        /// <summary>
        /// 根路径
        /// </summary>
        protected string RootPath { get; private set; }

        private readonly ILogger<FileHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileHandler"/> class.
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public FileHandler(IConfiguration configuration, ILogger<FileHandler> logger)
        {
            RootPath = GetRootPath(configuration);
            _logger = logger;
        }



        ///// <summary>
        ///// 将一个文件存储到系统中
        ///// </summary>
        ///// <param name="file">文件路径</param>
        ///// <returns>存储完毕的实际文件名称，文件名称会重新生成</returns>
        //public string SaveFile(string file,string folder)
        //{
        //    return SaveFile(file,string.Empty,true);
        //}

        /// <summary>
        /// 将一个文件存储到系统中
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <param name="rename">是否重新命名文件</param>
        /// <param name="folder">文件夹</param>
        /// <returns>存储完毕的实际文件名称</returns>
        public string SaveFile(string file, string folder, bool rename)
        {
            var fileinfo = new FileInfo(file);
            if (!fileinfo.Exists)
                throw new FileNotFoundException(file);
            string newfile;

            var fullFileName = GetAbsolutePath(fileinfo.Name, folder, rename, out newfile);

            fileinfo.CopyTo(fullFileName, true);

            if (rename)
                return newfile;
            else
                return fileinfo.Name;
        }

        /// <summary>
        /// 将一个文件流存储到系统中
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="inputStream">文件流</param>
        /// <param name="rename">是否重新命名</param>
        /// <param name="folder">文件夹</param>
        /// <returns></returns>
        public string SaveFile(string fileName, string folder, System.IO.Stream inputStream, bool rename)
        {
            string newfile;

            var fullFileName = GetAbsolutePath(fileName, folder, rename, out newfile);

            using (var sw = new FileStream(fullFileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (var sr = new BufferedStream(inputStream))
                {
                    var buff = new byte[1024];
                    while (sr.CanSeek)
                    {
                        var len = sr.Read(buff, 0, 1024);
                        sw.Write(buff, 0, len);
                        if (len < 1024)
                            break;
                    }
                }
                sw.Flush();
                sw.Close();
            }
            return newfile;
        }

        /// <summary>
        /// 打开一个文件流
        /// </summary>
        /// <param name="name">文件名称</param>
        /// <returns>文件流</returns>
        public System.IO.Stream OpenFile( string name)
        {
            return OpenFile(name, FileMode.Open);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public Stream OpenFile( string name, FileMode mode)
        {
            string fileName = Path.GetFileName(name);
            string folder = Path.GetDirectoryName(name);

            var path = GetPath(folder, true);
            var file = Path.Combine(path, fileName);
            if (FileMode.OpenOrCreate == mode
                || mode == FileMode.CreateNew
                || mode == FileMode.Create)
                return new FileStream(file, mode, FileAccess.Write);

            if (FileMode.Append == mode)
            {
                if (File.Exists(file))
                    return new FileStream(file, mode, FileAccess.Write);
                else
                    return null;
            }

            if (File.Exists(file))
                return new FileStream(file, mode, FileAccess.ReadWrite);
            else
                return null;
        }


        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="name">文件名称</param>
        /// <returns>文件信息</returns>
        public FileInfomation GetFileInformation(string name)
        {
            var path = GetPath(string.Empty, false);
            FileInfo file;
            try
            {
                file = new FileInfo(Path.Combine(path, name.TrimStart('\\')));
            }
            catch (System.ArgumentException are)
            {
                throw new ArgumentException("invalide file name;" + name, are);
            }

            if (file.Exists)
            {

                return new FileInfomation()
                {
                    FullName = file.FullName,
                    Name = name,
                    Exists = file.Exists,
                    Length = file.Length,
                    Extension = file.Extension,
                    LastModifyDate = file.LastWriteTimeUtc

                };
            }
            else
            {
                return new FileInfomation()
                {
                    Name = name,
                    Exists = file.Exists
                };
            }
        }

        /// <summary>
        /// 获取根路径
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private string GetRootPath(IConfiguration root)
        {
            return root.GetSection(SKYNET_CLOUD).GetValue<string>(SKYNET_CLOUD_ROOT, "/");
        }

        /// <summary>
        /// 移除一个文件
        /// </summary>
        /// <param name="name">文件名称</param>
        /// <returns></returns>
        public bool Remove(string name)
        {

            string fileName = Path.GetFileName(name);
            string folder = Path.GetDirectoryName(name);
            var path = GetPath( folder, true);
            var file = new FileInfo(Path.Combine(path, fileName));
            if(!file.Exists)
            {
                return true;
            }
            try
            {
                file.Delete();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private string GetPath(string subFolder, bool createFolder = true)
        {
            if (subFolder.StartsWith(@"\"))
                subFolder = subFolder.Remove(0, 1);
            var path = Path.Combine(RootPath, subFolder);

            if (!Directory.Exists(path) && createFolder)
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    throw new IOException("create folder error:" + path);
                }
            }
            return path;
        }

        /// <summary>
        /// 获取绝对路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="folder"></param>
        /// <param name="rename"></param>
        /// <param name="newFileName"></param>
        /// <returns></returns>
        string GetAbsolutePath(string fileName, string folder, bool rename, out string newFileName)
        {
            var ext = GetFileExtendName(fileName);
            newFileName = string.Empty;
            string path = GetPath(folder, true);
            if (rename)
                newFileName = fileName = Guid.NewGuid() + "." + ext;
            return Path.Combine(path, fileName);
        }

        /// <summary>
        /// 获取扩展名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string GetFileExtendName(string fileName)
        {
            var temp = fileName.Split('.');
            return temp[temp.Length - 1];
        }

        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="oldFileName"></param>
        /// <param name="newFileName"></param>
        /// <returns></returns>
        public bool Rename(string oldFileName, string newFileName)
        {
            string fileName = Path.GetFileName(oldFileName);
            string folder = Path.GetDirectoryName(oldFileName);

            var path = GetPath(folder, true);
            var file = Path.Combine(path, fileName);
            if (!File.Exists(file))
                return false;
            string nfileName = Path.GetFileName(newFileName);
            nfileName = Path.Combine(path, nfileName);
            try
            {
                File.Move(file, nfileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取路径下所有文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public FileInfo[] GetAllFiles( string path)
        {
            path = GetPath(path, true);
            var dir = new DirectoryInfo(path);
            return dir.GetFiles();
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="buffer"></param>
        public void Append(Stream stream, byte[] buffer)
        {
            stream.Seek(0, SeekOrigin.End);
            stream.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="subfolder"></param>
        /// <param name="compressOrginalPaths"></param>
        /// <returns></returns>
        public string Zip(string fileName, string subfolder, IEnumerable<string> compressOrginalPaths)
        {
            if (subfolder.IsNullOrEmpty())
                subfolder = Guid.NewGuid().ToString().Replace("-", "");
            var outPath = GetPath(subfolder, true);
            outPath = Path.Combine(outPath, fileName);
          
            using (var zip = File.OpenWrite(outPath))
            {
                using (var zipWriter = WriterFactory.Open(zip, ArchiveType.Zip, CompressionType.BZip2))
                {
                    foreach (var filePath in compressOrginalPaths)
                    {
                        zipWriter.Write(Path.GetFileName(filePath), filePath);
                    }
                }
            }
            return subfolder+ @"\" + fileName;
        }

        /// <summary>
        /// 解压zip
        /// </summary>
        /// <param name="compressfilepath"></param>
        /// <param name="uncompressdir"></param>
        public void UnZip(string compressfilepath, string uncompressdir)
        {
            uncompressdir = GetPath(uncompressdir);
            using (var archive = ArchiveFactory.Open(compressfilepath))
            {
                foreach (var entry in archive.Entries)
                {
                    if (!entry.IsDirectory)
                    {
                        entry.WriteToDirectory(uncompressdir, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                    }
                }
            }
        }


        /// <summary>
        /// 添加文档
        /// </summary>
        /// <param name="name"></param>
        /// <param name="folder"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public string AddFile(string name, string folder, string content)
        {
            if(folder.EndsWith("\\") || folder.EndsWith("/"))
            {
                name = folder  + name;
            } 
            else
            {
                name = folder + "/" + name;
            }

            Remove(name);

            using(var stream = OpenFile(name, FileMode.CreateNew))
            {
                var bytes = Encoding.Default.GetBytes(content.ToString());
                Append(stream, bytes);
                //stream.Seek(0, SeekOrigin.End);
                //stream.Write(bytes);
            }
            return name;
        }


        /// <summary>
        /// 压缩文档
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="directory"></param>
        public void Zip(string fileName, string directory)
        {
            var outPath = GetPath("");
            outPath = Path.Combine(outPath, fileName);
            using (var zip = File.OpenWrite(outPath))
            {
                using (var zipWriter = WriterFactory.Open(zip, ArchiveType.Zip, CompressionType.BZip2))
                {
                    zipWriter.WriteAll(GetPath(directory), searchPattern:"*", option: SearchOption.AllDirectories);
                }
            }
        }
    }
}
