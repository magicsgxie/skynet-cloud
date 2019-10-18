using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UWay.Skynet.Cloud.Nom.Entity;
using UWay.Ufa.Enterprise.Entity;

namespace UWay.Skynet.Cloud.Nom.Service.Interface
{
    public interface IRuleCheck
    {
        bool CheckData(ImportDataTemplateField dataTemplateField, string value, DataRow dr);
    }
}
