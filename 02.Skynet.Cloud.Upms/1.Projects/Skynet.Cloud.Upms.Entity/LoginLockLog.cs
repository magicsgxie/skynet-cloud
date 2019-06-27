using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Upms.Entity
{
    public class LoginLockLog
    {

        public long LoginLockID
        {
            set;
            get;
        }

        public long UserID
        {
            set;
            get;
        }


        public LockType UserLockCode
        {
            set;
            get;
        }

        public int PwdErrorCounter
        {
            set;
            get;
        }

        public int UserLockTimeout
        {
            set;
            get;
        }
  
        public string LoginIP
        {
            set;
            get;
        }
  
        public DateTime? LockDate
        {
            set;
            get;
        }

        public DateTime? CreateDate
        {
            set;
            get;
        }

        public DateTime? UpdateDate
        {
            set;
            get;
        }
        public string Remark
        {
            set;
            get;
        }
    }
}
