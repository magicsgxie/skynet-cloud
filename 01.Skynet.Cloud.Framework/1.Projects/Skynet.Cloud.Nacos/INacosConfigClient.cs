using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Nacos
{
    /// <summary>
    /// 
    /// </summary>
    public interface INacosConfigClient
    {
        /// <summary>
        /// Gets configurations in Nacos
        /// </summary>
        /// <param name="request">request</param>        
        /// <returns></returns>        
        Task<string> GetConfigAsync(GetConfigRequest request);



        /// <summary>
        /// Publishes configurations in Nacos
        /// </summary>
        /// <param name="request">request</param>
        /// <returns></returns>
        Task<bool> PublishConfigAsync(PublishConfigRequest request);

        /// <summary>
        /// Deletes configurations in Nacos
        /// </summary>
        /// <param name="request">request</param>
        /// <returns></returns>
        Task<bool> RemoveConfigAsync(RemoveConfigRequest request);

        /// <summary>
        /// Listen configuration.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns></returns>
        Task AddListenerAsync(AddListenerRequest request);

        /// <summary>
        /// Delete Listening
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns></returns>
        Task RemoveListenerAsync(RemoveListenerRequest request);
    }
}
