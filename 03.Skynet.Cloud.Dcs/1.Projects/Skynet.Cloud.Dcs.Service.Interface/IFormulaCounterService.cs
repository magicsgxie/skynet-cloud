using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;

namespace UWay.Skynet.Cloud.Dcs.Service.Interface
{
    /// <summary>
    /// 基本公式接口
    /// </summary>
    
    public interface IFormulaCounterService
    {
        IEnumerable<FormulaCounter> GetFormulaCounter();


        IEnumerable<FormulaCounter> GetFormulaCounterBy(int type, int dataSourcetType, string vendor, string phraseResult);
        //DataTable GetFormulaCounterDataTable();
    }
}
