﻿using System;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud.Nom.Service.Interface;
using UWay.Skynet.Cloud.Nom.Entity;
using System.Data;

namespace UWay.Skynet.Cloud.Nom.Service
{
    public class FloatNumberCheck : IRuleCheck

    {
        public bool CheckData(ImportDataTemplateField field, string value, DataRow dr)
        {
            if (field.Isneed.Equals("1") && value.IsNullOrEmpty())
            {
                dr.SetErrorInfo(string.Format("【{0}】不能为空", field.Fieldtext));
                return false;
            }

            decimal dec;
            if (!decimal.TryParse(value, out dec))
            {
                dr.SetErrorInfo(string.Format("【{0}】:浮点数格式错误", field.Fieldtext));
                return false;
            }
            dr.SetValue(field.Fieldname, dec);
            return true;
        }
    }
}
