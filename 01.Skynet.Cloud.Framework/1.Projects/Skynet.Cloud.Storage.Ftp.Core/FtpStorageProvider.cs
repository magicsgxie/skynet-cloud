using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Storage.Core;

namespace Skynet.Cloud.Storage.Ftp.Core
{
    /// <summary>
    /// FTP登录接口
    /// </summary>
    public class FtpStorageProvider : IStorageProvider
    {
        public string ProviderName => throw new NotImplementedException();

        public Task DeleteBlob(string containerName, string blobName)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContainer(string containerName)
        {
            throw new NotImplementedException();
        }

        public Task<BlobFileInfo> GetBlobFileInfo(string containerName, string blobName)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> GetBlobStream(string containerName, string blobName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetBlobUrl(string containerName, string blobName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetBlobUrl(string containerName, string blobName, DateTime expiry, bool isDownload = false, string fileName = null, string contentType = null, BlobUrlAccess access = BlobUrlAccess.Read)
        {
            throw new NotImplementedException();
        }

        public Task<IList<BlobFileInfo>> ListBlobs(string containerName)
        {
            throw new NotImplementedException();
        }

        public Task SaveBlobStream(string containerName, string blobName, Stream source)
        {
            throw new NotImplementedException();
        }
    }
}
