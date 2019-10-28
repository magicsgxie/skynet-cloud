using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    
    public class PerfFormulaCounter : BaseFormula
    {
        
        public new int UserInBSC
        {
            get;
            set;
        }

        
        public override int UserInBTS
        {
            get;
            set;
        }

        
        public override int UserInCELL
        {
            get;
            set;
        }

        
        public override int UserInCARR
        {
            get;
            set;
        }

        
        public override bool IsVisibility
        {
            get;
            set;
        }

        public void CopyFrom(BaseFormula item)
        {
            this.AttCnName = item.AttCnName;
            this.AttEnName = item.AttEnName;
            this.AttID = item.AttID;
            this.BusinessType = item.BusinessType;
            this.DataSource = item.DataSource;
            this.DigitalDigit = item.DigitalDigit;
            this.Formula = item.Formula;
            this.Formula1 = item.Formula1;
            this.Formula2 = item.Formula2;
            this.Formula3 = item.Formula3;
            this.GroupID = item.GroupID;
            this.IsEnabled = item.IsEnabled;
            this.IsShowWinApp = item.IsShowWinApp;
            this.IsSystem = item.IsSystem;
            this.IsZero = item.IsZero;
            this.NeAggregation = item.NeAggregation;
            this.NeCombination = item.NeCombination;
            this.NeLevel = item.NeLevel;
            this.NeType = item.NeType;
            this.TimeAggregation = item.TimeAggregation;
            this.UseType = item.UseType;
            this.VendorAggregation = item.VendorAggregation;
            this.VendorVersion = item.VendorVersion;
        }
    }
}
