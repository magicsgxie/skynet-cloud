using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    /// <summary>
    /// 基本公式
    /// </summary>
    
    public class BaseFormula : NeField
    {
        

        /// <summary>
        /// 指标编号
        /// </summary>
        
        public long AttID
        {
            set;
            get;
        }

        /// <summary>
        /// 分组编号
        /// </summary>
        
        public long GroupID
        {
            set;
            get;
        }

        /// <summary>
        /// 指标英文名
        /// </summary>
        
        public string AttEnName
        {
            set;
            get;
        }

        /// <summary>
        /// 指标中文名
        /// </summary>
        
        public string AttCnName
        {
            set;
            get;
        }

        /// <summary>
        /// 总公式，无映射
        /// </summary>
        
        public string Formula
        {
            set
            {
                var length = value.Length;
                if (length > 8000)
                {
                    Formula3 = value.Substring(8000, length - 8000);
                    length = 8000;
                }
                if (value.Length > 4000)
                {
                    Formula2 = value.Substring(4000, length - 4000);
                    length = 4000;
                }
                Formula1 = value.Substring(0, length);

            }
            get
            {
                return string.Format("{0}{1}{2}", Formula1, Formula2, Formula3);
            }
        }


        /// <summary>
        /// 公式1
        /// </summary>
        
        public string Formula1
        {
            set;
            get;
        }

        /// <summary>
        /// 公式2
        /// </summary>
              
        public string Formula2
        {
            set;
            get;
        }

        /// <summary>
        /// 公式3
        /// </summary>
        
        public string Formula3
        {
            set;
            get;
        }


        /// <summary>
        /// 网元粒度组合，power(2,粒度-1) ,bitand()
        /// </summary>
        
        public Int64 NeCombination
        {
            set;
            get;
        }

        /// <summary>
        /// 网元聚合方式
        /// </summary>
        
        public string NeAggregation
        {
            set;
            get;
        }

        /// <summary>
        /// 时间聚合方式
        /// </summary>
        
        public string TimeAggregation
        {
            set;
            get;
        }

        /// <summary>
        /// 厂家聚合方式
        /// </summary>
        
        public string VendorAggregation
        {
            set;
            get;
        }

        /// <summary>
        /// 是否是"0"
        /// </summary>
        
        public decimal IsZero
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        
        public Int16 DigitalDigit
        {
            set;
            get;
        }

        /// <summary>
        /// 使用方式，用二进制位表示
        /// </summary>
        
        public int UseType
        {
            set;
            get;
        }

        /// <summary>
        /// 是否可用
        /// </summary>
        
        [Ignore]
        public bool IsEnabled
        {
            set;
            get;
        }

        /// <summary>
        /// 是否系统内置
        /// </summary>
        
        public bool IsSystem
        {
            set; get;
        }

        /// <summary>
        /// 是否系统内置
        /// </summary>
        
        public bool IsShowWinApp
        {
            set; get;
        }

        /// <summary>
        /// 是否BSC指标
        /// </summary>

        [Ignore]
        
        public virtual int UserInBSC
        {
            get;
            set;
        }
        /// <summary>
        /// 是否BTS指标
        /// </summary>

        [Ignore]
        
        public virtual int UserInBTS
        {
            get;
            set;
        }

        /// <summary>
        /// 是否小区指标
        /// </summary>
        [Ignore]
        
        public virtual int UserInCELL
        {
            get;
            set;
        }

        /// <summary>
        /// 是否载频指标
        /// </summary>
        [Ignore]
        
        public virtual int UserInCARR
        {
            get;
            set;
        }

        /// <summary>
        /// 是否显示
        /// </summary>
        [Ignore]
        
        public virtual bool IsVisibility
        {
            get;
            set;
        }
    }
}
