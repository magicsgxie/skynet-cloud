using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Nom.Entity
{
    public class NeGroup
    {
        
        public int GroupID
        {
            set;
            get;
        }
        
        
        public string GroupName
        {
            set;
            get;
        }
        
        //[Column("group_level")]
        public int GroupLevel
        {
            set;
            get;
        }
        
        //[Column("parentid")]
        public int ParentID
        {
            set;
            get;
        }
        
        //[Column("user_name")]
        public string UserName
        {
            set;
            get;
        }
        
        //[Column("remark")]
        public string Remark
        {
            set;
            get;
        }
        
        //[Column("time_stamp")]
        public DateTime? TimeStamp
        {
            set;
            get;
        }
        
        //[Column("city_id")]
        public int CityID
        {
            set;
            get;
        }
        
        //[Column("level_id")]
        public int LevelID
        {
            set;
            get;
        }
        
        //[Column("sharetype")]
        public int ShareType
        {
            set;
            get;
        }

        
        //[Column("sharetype")]
        public string SceneNo
        {
            set;
            get;
        }


        
        [Ignore]
        //[ResultColumn("HasGroup")]
        public bool HasGroup
        {
            set;
            get;
        }

       
    }
}
