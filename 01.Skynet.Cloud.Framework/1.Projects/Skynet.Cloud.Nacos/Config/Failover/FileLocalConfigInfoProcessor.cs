namespace UWay.Skynet.Cloud.Nacos
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class FileLocalConfigInfoProcessor : ILocalConfigInfoProcessor
    {
        private readonly string FAILOVER_BASE = Path.Combine(Directory.GetCurrentDirectory(), "UWay.Skynet.Cloud.Nacos-data", "data");
        private readonly string SNAPSHOT_BASE = Path.Combine(Directory.GetCurrentDirectory(), "UWay.Skynet.Cloud.Nacos-data", "snapshot");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="group"></param>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public async Task<string> GetFailoverAsync(string dataId, string group, string tenant)
        {
            string failoverFile;
            if (!string.IsNullOrEmpty(tenant))
            {
                failoverFile = Path.Combine(SNAPSHOT_BASE, "config-data-tenant", tenant, group);
            }
            else
            {
                failoverFile = Path.Combine(SNAPSHOT_BASE, "config-data", group);
            }
            var file = new FileInfo(failoverFile + dataId);

            if (!file.Exists)
            {
                return null;
            }

            var config = File.ReadAllText(file.FullName);

            return await Task.FromResult(config);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="group"></param>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public async Task<string> GetSnapshotAync(string dataId, string group, string tenant)
        {
            FileInfo file = GetSnapshotFile(dataId, group, tenant);

            if (!file.Exists)
            {
                return null;
            }

            var config = File.ReadAllText(file.FullName);

            return await Task.FromResult(config);
        }

        private FileInfo GetSnapshotFile(string dataId, string group, string tenant)
        {
            string snapshotFile;
            if (!string.IsNullOrEmpty(tenant))
            {
                snapshotFile = Path.Combine(SNAPSHOT_BASE, "snapshot-tenant", tenant, group);
            }
            else
            {
                snapshotFile = Path.Combine(SNAPSHOT_BASE, "snapshot", group);
            }
            var file = new FileInfo(snapshotFile + dataId);
            return file;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="group"></param>
        /// <param name="tenant"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task SaveSnapshotAsync(string dataId, string group, string tenant, string config)
        {
            FileInfo snapshotFile = GetSnapshotFile(dataId, group, tenant);
            if (string.IsNullOrEmpty(config))
            {
                if (snapshotFile.Exists)
                {
                    snapshotFile.Delete();
                }
            }
            else
            {
                if (snapshotFile.Directory != null && !snapshotFile.Directory.Exists)
                {
                    snapshotFile.Directory.Create();
                }

                File.WriteAllText(snapshotFile.FullName, config);
            }

            await Task.Yield();
        }
    }
}
