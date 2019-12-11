using FluentFTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Storage.Core;
using UWay.Skynet.Cloud.Storage.Ftp.Core;

namespace Skynet.Cloud.Storage.Ftp.Core
{
    /// <summary>
    /// FTP提供程序.
    /// </summary>
    public class FtpStorageProvider : IStorageProvider
    {
        private readonly FtpConfig cfg;


        /// <summary>
        /// FtpClient.
        /// </summary>
        private FtpClient ftpClient = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="FtpStorageProvider"/> class.
        /// </summary>
        /// <param name="ftpConfig">配置信息.</param>
        public FtpStorageProvider(FtpConfig ftpConfig)
        {
            this.cfg = ftpConfig;
            if (string.IsNullOrEmpty(this.cfg.LocalRootPath))
            {
                this.cfg.LocalRootPath = AppDomain.CurrentDomain.BaseDirectory;
            }

            this.GetFtpClient();
        }

        /// <inheritdoc/>
        public string ProviderName => "FtpClient";

        /// <summary>
        /// 创建ftp客户端.
        /// </summary>
        private void GetFtpClient()
        {
           this.ftpClient = new FtpClient(this.cfg.FtpUri, this.cfg.FtpPort, this.cfg.FtpUserID, this.cfg.FtpPassword);
           this.ftpClient.RetryAttempts = this.cfg.RetryTimes;
        }

        /// <summary>
        /// 连接FTP.
        /// </summary>
        /// <returns>bool.</returns>
        protected bool Connect()
        {
            bool result = false;
            if (this.ftpClient != null)
            {
                if (this.ftpClient.IsConnected)
                {
                    return true;
                }
                else
                {
                    this.ftpClient.Connect();
                    return true;
                }
            }

            return result;
        }

        /// <summary>
        /// 断开FTP.
        /// </summary>
        protected void DisConnect()
        {
            if (this.ftpClient != null)
            {
                if (this.ftpClient.IsConnected)
                {
                    this.ftpClient.Disconnect();
                }
            }
        }

        /// <inheritdoc/>
        public async Task DeleteBlob(string containerName, string blobName)
        {
            await Task.Run(() =>
            {
                this.ExceptionHandling(() =>
                {
                    if (this.Connect())
                    {
                        containerName = this.GetFileName(containerName, blobName);
                        this.ftpClient.DeleteFile(containerName);
                    }
                });
            });
        }

        /// <inheritdoc/>
        public async Task DeleteContainer(string containerName)
        {
            await Task.Run(() =>
            {
                this.ExceptionHandling(() =>
                {
                    if (this.Connect())
                    {
                        containerName = this.MatchContainerName(containerName);
                        this.ftpClient.DeleteDirectory(containerName);
                    }
                });
            });
        }

        /// <inheritdoc/>
        public async Task<BlobFileInfo> GetBlobFileInfo(string containerName, string blobName)
        {
            return await Task.Run(() => this.ExceptionHandling(() =>
            {
                if (this.Connect())
                {
                    containerName = this.GetFileName(containerName, blobName);
                    FtpListItem file = this.ftpClient.GetFilePermissions(containerName);

                    return new BlobFileInfo
                    {
                        ContentMD5 = string.Empty,
                        ETag = string.Empty,
                        ContentType = file.Name.GetMimeType(),
                        Container = containerName,
                        LastModified = file.Modified,
                        Length = file.Size,
                        Name = file.Name,
                        Url = file.FullName,
                    };
                }

                return null;
            }));
        }

        /// <summary>
        /// The ExceptionHandling.
        /// </summary>
        /// <param name="ioAction">The ioAction<see cref="Action"/></param>
        private void ExceptionHandling(Action ioAction)
        {
            try
            {
                ioAction();
            }
            catch (Exception ex)
            {
                throw new StorageException(StorageErrorCode.GenericException.ToStorageError(), ex);
            }
            finally
            {
                this.DisConnect();
            }
        }

        /// <summary>
        /// The ExceptionHandling.
        /// </summary>
        /// <typeparam name="T"><see cref="T"/>.</typeparam>
        /// <param name="ioFunc">The ioFunc<see cref="Func{T}"/>.</param>
        /// <returns>The <see cref="T"/>.</returns>
        private T ExceptionHandling<T>(Func<T> ioFunc)
        {
            try
            {
                return ioFunc();
            }
            catch (Exception ex)
            {
                throw new StorageException(StorageErrorCode.GenericException.ToStorageError(), ex);
            }
            finally
            {
                this.DisConnect();
            }
        }

        /// <inheritdoc/>
        public async Task<Stream> GetBlobStream(string containerName, string blobName)
        {
            return await Task.Run(() => this.ExceptionHandling(() =>
            {
                containerName = this.GetFileName(containerName, blobName);
                if (this.Connect())
                {
                    if (this.ftpClient.FileExists(containerName))
                    {
                        return this.ftpClient.OpenRead(containerName);
                    }
                }

                return Stream.Null;
            }));
        }

        /// <inheritdoc/>
        public async Task<string> GetBlobUrl(string containerName, string blobName)
        {
            return await Task.Run(() => this.ExceptionHandling(() =>
            {
                string rootPath = string.Empty;
                rootPath = this.GetRootFileDirectory(containerName);

                // 取下载文件的文件名
                containerName = this.GetFileName(containerName, blobName);

                // 本地目录不存在，则自动创建
                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }

                // 拼接本地路径
                rootPath = Path.Combine(rootPath, blobName);

                if (this.Connect())
                {
                    this.ftpClient.DownloadFile(rootPath, containerName, FtpLocalExists.Overwrite);
                }

                return rootPath;
            }));
        }

        /// <inheritdoc/>
        public Task<string> GetBlobUrl(string containerName, string blobName, DateTime expiry, bool isDownload = false, string fileName = null, string contentType = null, BlobUrlAccess access = BlobUrlAccess.Read)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<IList<BlobFileInfo>> ListBlobs(string containerName)
        {
            return await Task.Run(() => this.ExceptionHandling(() =>
            {
                List<BlobFileInfo> list = new List<BlobFileInfo>();
                if (this.Connect())
                {
                    containerName = this.MatchContainerName(containerName);
                    FtpListItem[] files = this.ftpClient.GetListing(containerName);
                    foreach (FtpListItem file in files)
                    {
                        if (file.Type == FtpFileSystemObjectType.File)
                        {
                            list.Add(new BlobFileInfo
                            {
                                ContentMD5 = string.Empty,
                                ETag = string.Empty,
                                ContentType = file.Name.GetMimeType(),
                                Container = containerName,
                                LastModified = file.Modified,
                                Length = file.Size,
                                Name = file.Name,
                                Url = file.FullName,
                            });
                        }
                    }
                }

                return list;
            }));
        }

        /// <inheritdoc/>
        public async Task SaveBlobStream(string containerName, string blobName, Stream source)
        {
            await Task.Run(() => this.ExceptionHandling(() =>
            {
                containerName = this.GetFileName(containerName, blobName);
                if (this.Connect())
                {
                    // 重名覆盖
                    this.ftpClient.Upload(source, containerName, FtpExists.Overwrite, true);
                }
            }));
        }

        private string GetRootFileDirectory(string containerName)
        {
            containerName = this.MatchContainerName(containerName);
            return this.cfg.LocalRootPath + containerName;
        }

        private string MatchContainerName(string containerName)
        {
            // 远端路径校验
            if (string.IsNullOrEmpty(containerName))
            {
                containerName = "/";
            }

            if (!containerName.StartsWith("/"))
            {
                containerName = "/" + containerName;
            }

            if (!containerName.EndsWith("/"))
            {
                containerName += "/";
            }

            return containerName;
        }

        private string GetFileName(string containerName, string blobName)
        {
            containerName = this.MatchContainerName(containerName);

            // 拼接远端路径
            containerName += blobName;
            return containerName;
        }
    }
}
