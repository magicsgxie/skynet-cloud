using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Upms.Entity
{
    public class SystemInfo
    {
        public virtual long SystemId { set; get; }

        public virtual string SystemName { set; get; }

        public virtual string SystemCode { set; get; }

        public virtual string SystemUrl { set; get; }
    }
}
