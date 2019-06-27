using System.Collections.Generic;

namespace UWay.Skynet.Cloud.Nacos
{
    public class Host
    {
        public bool Valid { get; set; }
        public bool Marked { get; set; }
        public string InstanceId { get; set; }
        public int Port { get; set; }
        public string Ip { get; set; }
        public double Weight { get; set; }
        public Dictionary<string, string> Metadata { get; set; }

        public string ClusterName { get; set; }
        public string ServiceName { get; set; }

        public bool Healthy { set; get; }
    }
}
