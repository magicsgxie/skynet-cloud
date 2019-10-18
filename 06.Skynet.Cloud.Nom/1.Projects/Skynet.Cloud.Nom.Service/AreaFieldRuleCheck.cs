using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UWay.Skynet.Cloud.Nom.Entity;
using UWay.Skynet.Cloud.Nom.Service.Interface;
using UWay.Ufa.Enterprise.Entity;

namespace UWay.Skynet.Cloud.Nom.Service
{
    public class AreaFieldRuleCheck : IRuleCheck

    {


        public bool CheckData(ImportDataTemplateField field, string value, DataRow dr)
        {
            if (field.Isneed.Equals("1") && value.IsNullOrEmpty())
            {
                dr.SetErrorInfo(string.Format("【{0}】不能为空", field.Fieldtext));
                return false;
            }

            

            return true;
        }
    }
}
