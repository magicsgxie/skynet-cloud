using System;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Upms.Entity
{
    [Table("CFG_DICTITEM")]
    public class DictItem
    {

        [Id("DICT_CODE")]
        public string DictCode { get; set; }


        [Column("DICT_NAME")]
        public string DictName { get; set; }


        [Id("DICT_TYPE")]
        public string DictType { get; set; }


        [Column("DESCRIPTION")]
        public string Description { get; set; }


        [Column("REMARK")]
        public string Remark { get; set; }


        //[Column("REMARK_EX")]
        [Ignore]
        public string RemarkEx { get; set; }
    }
}
