using System;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Nom.Entity;
using UWay.Skynet.Cloud.Upms.Entity;
using UWay.Ufa.Enterprise.Entity;

namespace UWay.Skynet.Cloud.Nom.Service
{
    internal interface IImport
    {
        void Import(NetType netType,ExcelInfo excel, ImportDataTemplate template, ImportDataTemplateField fields, IList<CityInfo> cityIds, string user);
    }
}
