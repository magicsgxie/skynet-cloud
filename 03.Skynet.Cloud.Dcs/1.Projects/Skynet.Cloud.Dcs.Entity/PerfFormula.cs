using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    
    public class PerfFormula
    {
        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Id("id")]
        public int ID
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("china_name")]
        public string ChinaName
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("group_id")]
        public int GroupID
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("ne_type")]
        public int Netype
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("formula")]
        public string Formula
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("formula1")]
        public string Formula1
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("formula2")]
        public string Formula2
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("formularemark")]
        public string FormulareMark
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("formulasource")]
        public string FormulaSource
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("formulagrouptime")]
        public string FormulaGroupTime
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("formulagroupne")]
        public string FormulaGroupNe
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("formulagroupvendor")]
        public string FormulaGroupVendor
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("sort_id")]
        public int SortID
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("operater")]
        public string Operater
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("operate_time")]
        public DateTime OperateTime
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("businesstype")]
        public string BusinessType
        {
            set;
            get;
        }
        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("userinbsc")]
        public int Userinbsc
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("userinbts")]
        public int Userinbts
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("userincell")]
        public int Userincell
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("userincarr")]
        public int Userincarr
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("is_showinapp")]
        public int IsShowInApp
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("is_enable")]
        public int IsEnable
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("is_used")]
        public int IsUsed
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("is_system")]
        public int IsSystem
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("vendorcode")]
        public string VendorCode
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("ifzero")]
        public int Ifzero
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("digitaldigit")]
        public int DigitalDigit
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("module_type")]
        public int ModuleType
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("is_rate")]
        public int IsRate
        {
            set;
            get;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        
        [Column("value_range")]
        public string ValueRange
        {
            set;
            get;
        }
    }
}
