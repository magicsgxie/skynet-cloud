using System;
using UWay.Skynet.Cloud.Data;

namespace Skynet.Cloud.Upms.Test.Entity
{
    [Table("ufa_user_info")]
    public class User
    {
        [Column("USER_LEVEL")]
        public  int UserLevel
        {
            set;
            get;
        }

        [Column("INVALID", DbType = DBType.NVarChar)]
        public  int Invalid
        {
            set;
            get;
        }

        [Column("password")]
        public  string Password
        {
            set;
            get;
        }

        [Column("remark")]
        public  string Remark
        {
            set;
            get;
        }


        [Column("user_no")]
        public string UserNo
        {
            set;
            get;
        }

        [Id("user_id", IsDbGenerated =true, SequenceName ="seq_user_id")]
        public  int UserID
        {
            set;
            get;
        }
    }
}
