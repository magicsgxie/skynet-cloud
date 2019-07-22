using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Data.Render;

namespace UWay.Skynet.Cloud.Dcs.Service.Interface
{
    
    public interface IUqlService
    {


        /// <summary>
        /// 拼装SQL
        /// </summary>
        /// <param name="uqlType">拼装类型</param>
        /// <param name="useType">应用类型</param> 
        /// <param name="template">模板</param>
        /// <returns></returns>
        string BuildSQL(UqlType uqlType, int userType, QueryTemplateView template, ISqlOmRenderer render, bool isUseOrderby = true);

   
        /// <summary>
        /// 拼装SQL
        /// </summary>
        /// <param name="useType">应用类型</param>
        /// <param name="template">模板</param>
        /// <param name="render">模板</param>
        /// <returns></returns>
        SelectQuery BuildSQL(UqlType uqlType, int userType, QueryTemplateView template, IDictionary<string, object> parameters, bool isUseOrderby = true);

    }
}
