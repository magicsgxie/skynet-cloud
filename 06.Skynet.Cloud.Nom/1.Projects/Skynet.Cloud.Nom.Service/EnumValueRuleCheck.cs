using System;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud.Nom.Service.Interface;
using UWay.Skynet.Cloud.Nom.Entity;
using System.Data;
using System.Linq;

namespace UWay.Skynet.Cloud.Nom.Service
{
    public class EnumValueRuleCheck : IRuleCheck

    {
        public bool CheckData(ImportDataTemplateField field, string value, DataRow dr)
        {
            if (field.Isneed.Equals("1") && value.IsNullOrEmpty())
            {
                dr.SetErrorInfo(string.Format("【{0}】不能为空", field.Fieldtext));
                return false;
            }
            
            //if(!field.EnumValues.Select(p => p.Name).Contains(value))
            //{
            //    dr.SetErrorInfo(string.Format("【{0}】值不存在【{1}】", field.Fieldtext,value));
            //    return false;
            //}

            return true;
        }
    }
}
