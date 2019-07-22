using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    public enum PriorityCmpType
    {
        Unknown = 0,	//无法比较
        Higher = 1,	//高于
        Lower = 2,	//低于
        Equal = 3		//等于
    }
}
