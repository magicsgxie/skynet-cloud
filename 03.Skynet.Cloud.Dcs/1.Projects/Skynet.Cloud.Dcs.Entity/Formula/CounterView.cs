using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    /// <summary>
    /// 指标明细信息
    /// </summary>
    
    public class CounterView
    {
        
        public string FormulaName
        {
            set;
            get;
        }

        
        public string CounterFormula
        {
            set;
            get;
        }

        private List<CounterViewItem> _counters = new List<CounterViewItem>();
        
        public List<CounterViewItem> Counters
        {
            get { return _counters; }
            set
            {
                _counters = value;
            }
        }
    }

    public class CounterViewItem
    {
        
        public string CounterName { get; set; }
        
        public string EnglishCounterName { get; set; }
        
        public double CounterValue { get; set; }

        
        public string NeAggregation{get;set;}

        
        public string TimeAggregation { get; set; }
    }
}
