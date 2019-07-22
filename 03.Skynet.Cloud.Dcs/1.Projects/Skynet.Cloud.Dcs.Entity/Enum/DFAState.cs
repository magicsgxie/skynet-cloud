using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    public enum DFAState : int
    {
        S0 = 0,	///初态
        S1 = 1,	///整数串，不带小数点
        S2 = 2,	///浮点数串
        S3 = 3,	///字母串
        S4 = 4,	/// +
        S5 = 5,	/// -
        S6 = 6,	/// *
        S7 = 7,	/// /
        S8 = 8,	/// %
        S9 = 9,	/// !
        S10 = 10,	/// ^
        S11 = 11,	/// =
        S12 = 12,	/// (
        S13 = 13,	/// )
        S14 = 14, /// #
        S15 = 15, /// @
        SX = 16,	/// 未知态
        S17 = 17,	/// _
    }

    
}
