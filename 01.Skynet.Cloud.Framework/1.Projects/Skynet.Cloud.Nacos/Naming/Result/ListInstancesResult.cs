namespace UWay.Skynet.Cloud.Nacos
{
    using System.Collections.Generic;

    public class ListInstancesResult
    {
        /// <summary>
        /// user extended attributes
        /// </summary>
        public Dictionary<string, string> Metadata { get; set; }

        public string Dom { get; set; }
        public int CacheMillis { get; set; }
        public string UseSpecifiedURL { get; set; }
        public List<Host> Hosts { get; set; }
        public string Checksum { get; set; }
        public long LastRefTime { get; set; }
        public string Env { get; set; }
        public string Clusters { get; set; }
    }   
}
