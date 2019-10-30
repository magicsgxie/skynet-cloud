using Skynet.Cloud.Noap;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace UWay.Skynet.Cloud.Nom.Entity
{

    public static class DataRowExtention
    {
        private const string NET_TYPE = "NET_TYPE";
        public static void AddNetType(this DataTable dt, NetType netType)
        {
            dt.ExtendedProperties.Add(NET_TYPE, netType);
        }

        
        public static NetType? NetType(this DataTable dt)
        {
   
           return  dt.ExtendedProperties[NET_TYPE] as NetType?;
        }

    }
}
