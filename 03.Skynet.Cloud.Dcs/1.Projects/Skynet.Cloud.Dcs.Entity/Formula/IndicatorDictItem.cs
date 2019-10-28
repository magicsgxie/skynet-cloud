using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    [Table("cfg_indicator_dictitem")]
    public class IndicatorDictItem
    {
        private string _Dict_Code = "";
        public string Dict_Code
        {
            get
            {
                return _Dict_Code;
            }
            set
            {
                _Dict_Code = value;

            }
        }

        private string _Dict_Name = "";
        public string Dict_Name
        {
            get
            {
                return _Dict_Name;
            }
            set
            {
                _Dict_Name = value;

            }
        }

        private string _Dict_Type = "";
        public string Dict_Type
        {
            get
            {
                return _Dict_Type;
            }
            set
            {
                _Dict_Type = value;

            }
        }

        private string _Description = "";
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;

            }
        }

        private string _Relate_Info = "";
        /// <summary>
        /// 关联信息
        /// </summary>
        public string Relate_Info
        {
            get
            {
                return _Relate_Info;
            }
            set
            {
                _Relate_Info = value;

            }
        }

        private string _REMARK = "";
        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK
        {
            get
            {
                return _REMARK;
            }
            set
            {
                _REMARK = value;
            }
        }
    }
}
