using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UWay.Skynet.Cloud.Excel.IE;
using UWay.Skynet.Cloud.Nom.Entity;
using UWay.Ufa.Enterprise.Entity;

namespace UWay.Skynet.Cloud.Nom.Service.Interface
{
    public interface IImportTemplateService
    {
        ImportMsgResult Import(string filename, int templateId, string user);

        ImportMsgResult Import(Stream fileStream, int templateId, string user);

        IEnumerable<ImportTemplate> GetImportDataTemplates();

        IEnumerable<ImportDataField> GetImportDataFields(int templateId);

    }
}