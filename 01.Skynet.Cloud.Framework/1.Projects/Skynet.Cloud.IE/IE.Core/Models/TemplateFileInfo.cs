// ======================================================================
// 
//           Copyright (C) 2019-2030 深圳市优网科技有限公司
//           All rights reserved
// 
//           filename : TemplateFileInfo.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-11 13:51
//           
//           
//           QQ：279218456（编程交流）
//           
// 
// ======================================================================

namespace UWay.Skynet.Cloud.IE.Core.Models
{
    public class TemplateFileInfo
    {
        public TemplateFileInfo()
        {
        }

        public TemplateFileInfo(string fileName, string fileType)
        {
            FileName = fileName;
            FileType = fileType;
        }

        public string FileName { get; set; }

        public string FileType { get; set; }
    }
}