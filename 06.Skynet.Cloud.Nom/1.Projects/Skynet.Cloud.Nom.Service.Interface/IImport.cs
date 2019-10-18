using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UWay.Skynet.Cloud.Excel.IE;
using UWay.Skynet.Cloud.Nom.Entity;
using UWay.Ufa.Enterprise.Entity;

namespace UWay.Skynet.Cloud.Nom.Service.Interface
{
    public interface IImport
    {
        ImportMsgResult Import(string filename, ImportDataTemplate template, string user);

        ImportMsgResult Import(Stream fileStream, ImportDataTemplate template, string user);

        ImportMsgResult Import(ExcelInfo excel, ImportDataTemplate template, string user);
    }
}