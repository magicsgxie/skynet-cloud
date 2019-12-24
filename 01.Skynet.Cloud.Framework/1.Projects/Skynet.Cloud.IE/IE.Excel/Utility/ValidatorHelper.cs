// ======================================================================
// 
//           Copyright (C) 2019-2030 深圳市优网科技有限公司
//           All rights reserved
// 
//           filename : ValidatorHelper.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-11 13:51
//          
//          
//           QQ：279218456（编程交流）
//           
// 
// ======================================================================

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UWay.Skynet.Cloud.IE.Excel.Utility
{
    /// <summary>
    ///     数据验证帮助类
    /// </summary>
    public static class ValidatorHelper
    {
        public static bool TryValidate(object obj, out ICollection<ValidationResult> validationResults)
        {
            var context = new ValidationContext(obj, null, null);
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(obj, context, validationResults, true);
        }
    }
}