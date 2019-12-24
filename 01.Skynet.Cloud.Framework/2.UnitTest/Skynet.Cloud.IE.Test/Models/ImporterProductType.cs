// ======================================================================
// 
//           Copyright (C) 2019-2030 深圳市优网科技有限公司
//           All rights reserved
// 
//           filename : ImporterProductType.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-11 13:51
//           
//           
//           
//          
// 
// ======================================================================

using System.ComponentModel.DataAnnotations;

namespace UWay.Skynet.Cloud.IE.Tests.Models
{
    public enum ImporterProductType
    {
        [Display(Name = "第一")] One,
        [Display(Name = "第二")] Two
    }
}