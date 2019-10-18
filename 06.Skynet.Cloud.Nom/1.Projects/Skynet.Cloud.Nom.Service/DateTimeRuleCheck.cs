using System;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud.Nom.Service.Interface;
using UWay.Skynet.Cloud.Nom.Entity;
using System.Data;

namespace UWay.Skynet.Cloud.Nom.Service
{
    public class DateTimeRuleCheck : IRuleCheck

    {
        public bool CheckData(ImportDataTemplateField field, string value, DataRow dr)
        {
            if (field.Isneed.Equals("1") && value.IsNullOrEmpty())
            {
                dr.SetErrorInfo(string.Format("【{0}】不能为空", field.Fieldtext));
                return false;
            }

            DateTime dt;
            
            if(!DateTime.TryParse(value, out dt))
            {
                dr.SetErrorInfo(string.Format("【{0}】:日期格式错误", field.Fieldtext));
                return false;
            }
            dr.SetValue(field.Fieldname, dt);
            return true;
        }
    }

}
