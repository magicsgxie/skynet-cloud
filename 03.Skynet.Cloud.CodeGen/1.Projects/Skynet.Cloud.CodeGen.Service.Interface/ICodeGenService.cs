using UWay.Skynet.Cloud.CodeGen.Entity;
using System;
using UWay.Skynet.Cloud.Request;

namespace UWay.Skynet.Cloud.CodeGen.Service.Interface
{
    public interface ICodeGenService
    {
        byte[] GeneratorCode(GenConfig genConfig);

        DataSourceResult Query(DataSourceRequest request);
    }
}
