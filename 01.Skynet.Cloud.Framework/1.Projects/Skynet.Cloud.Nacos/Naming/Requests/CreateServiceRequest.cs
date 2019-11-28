namespace UWay.Skynet.Cloud.Nacos
{
    using UWay.Skynet.Cloud.Nacos.Utilities;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class CreateServiceRequest : BaseRequest
    {
        /// <summary>
        /// service name
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// namespace id
        /// </summary>
        public string NamespaceId { get; set; }

        /// <summary>
        /// Protect threshold, set value from 0 to 1, default 0
        /// </summary>
        public float? ProtectThreshold { get; set; }

        /// <summary>
        /// visit strategy, a JSON string
        /// </summary>
        public string Selector { get; set; }

        /// <summary>
        /// metadata of service
        /// </summary>
        public string Metadata { get; set; }

        /// <summary>
        /// group name
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override void CheckParam()
        {
            ParamUtil.CheckServiceName(ServiceName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToQueryString()
        {
            var sb = new StringBuilder(1024);
            sb.Append($"serviceName={ServiceName}");

            if (!string.IsNullOrWhiteSpace(NamespaceId))
            {             
                sb.Append($"&namespaceId={NamespaceId}");
            }
         
            if (!string.IsNullOrWhiteSpace(Metadata))
            {
                sb.Append($"&metadata={Metadata}");
            }

            if (!string.IsNullOrWhiteSpace(GroupName))
            {
                sb.Append($"&groupName={GroupName}");
            }

            if (!string.IsNullOrWhiteSpace(Selector))
            {
                sb.Append($"&selector={Selector}");
            }

            if (ProtectThreshold.HasValue)
            {
                sb.Append($"&protectThreshold={ProtectThreshold}");
            }      

            return sb.ToString();
        }
    }
}
