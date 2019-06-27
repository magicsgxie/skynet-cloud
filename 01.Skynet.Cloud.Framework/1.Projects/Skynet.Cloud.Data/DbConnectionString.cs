using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Data
{
    public class DbConnectionString
    {
        public string ContainerName
        { set; get; }

        public string ConnectionString
        {
            get;
            set;
        }

        public string Provider
        {
            set;
            get;
        }
    }
}
