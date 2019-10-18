using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.IE.Core
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
