using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Nom.Entity
{
    public class NeGroupItem
    {
        
        public int GroupID
        {
            set;get;
        }
        
        public string NeSysID
        {
            set;get;
        }
        
        public string NeName
        {
            set;get;
        }
        
        public string Vendor
        {
            set;get;
        }
        
        public string GroupName
        {
            set;get;
        }
        
        public int? CityID
        {
            set;get;
        }

    }
}
