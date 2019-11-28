namespace UWay.Skynet.Cloud.Nacos
{
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public interface ILocalConfigInfoProcessor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="group"></param>
        /// <param name="tenant"></param>
        /// <returns></returns>
        Task<string> GetFailoverAsync(string dataId, string group, string tenant);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="group"></param>
        /// <param name="tenant"></param>
        /// <returns></returns>
        Task<string> GetSnapshotAync(string dataId, string group, string tenant);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="group"></param>
        /// <param name="tenant"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        Task SaveSnapshotAsync(string dataId, string group, string tenant, string config);
    }    
}
