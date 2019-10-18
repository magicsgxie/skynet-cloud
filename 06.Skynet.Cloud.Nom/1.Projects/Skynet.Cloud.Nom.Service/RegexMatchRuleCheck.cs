using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UWay.Skynet.Cloud.Nom.Service.Interface;
using UWay.Skynet.Cloud.Nom.Entity;
using System.Data;

namespace UWay.Skynet.Cloud.Nom.Service
{
    public class RegexMatchRuleCheck : IRuleCheck

    {
        public bool CheckData(ImportDataTemplateField field, string value, DataRow dr)
        {
            if(field.Isneed.Equals("1") && value.IsNullOrEmpty())
            {
                dr.SetErrorInfo(string.Format("【{0}】不能为空", field.Fieldtext));
                return false;
            }


            if (value.Length > field.Fieldlength)
            {
                dr.SetErrorInfo(string.Format("【{0}】:长度超过{1}", field.Fieldtext, field.Fieldlength));
                return false;
            }

            if(!field.Experssion.IsNullOrEmpty())
            {
                var rx = new Regex(field.Experssion, RegexOptions.Compiled);
                if (rx != null && !rx.IsMatch(value))
                {
                    dr.SetErrorInfo(string.Format("【{0}】:{1}", field.Fieldtext, field.Experssionerrorlog));
                    return false;
                }
            }

            dr.SetValue(field.Fieldname, value);
            return true;
        }
    }
}
