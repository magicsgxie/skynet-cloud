using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Upms.Entity
{
    public class RoleAreaInfo
    {
        
        public virtual long RoleAreaId
        {
            set;
            get;
        }

        
        public virtual int RoleID
        {
            set;
            get;
        }

        public virtual int AreaType
        {
            set;
            get;
        }

        public virtual int AreaId
        {
            set;
            get;
        }

        
        public virtual string Author
        {
            set;
            get;
        }


        public virtual DateTime? CreateTime
        {
            set;
            get;
        }
    }
}
