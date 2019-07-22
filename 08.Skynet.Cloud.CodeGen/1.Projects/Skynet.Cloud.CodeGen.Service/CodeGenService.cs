using UWay.Skynet.Cloud.CodeGen.Entity;
using UWay.Skynet.Cloud.CodeGen.Repository;
using UWay.Skynet.Cloud.CodeGen.Service.Interface;
using System;
using System.Collections.Generic;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Request;
using System.Data;
using UWay.Skynet.Cloud.FileHandling;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;

namespace UWay.Skynet.Cloud.CodeGen.Service
{
    public class CodeGenService : ICodeGenService
    {
        private readonly ILogger<CodeGenService> _logger;
        private readonly IFileHandler _handler;
        public CodeGenService(IFileHandler fileHandler, ILogger<CodeGenService> logger)
        {
            _handler = fileHandler;
            _logger = logger;
        }

        public byte[] GeneratorCode(GenConfig genConfig)
        {
            var assmeblyName = genConfig.Namespace.Substring(genConfig.Namespace.IndexOf(".")+1);

            var path = assmeblyName + "." + genConfig.ModuleName;
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {

                var r = new CodeGenRepository(context);
                //查询表信息
                DataTable table = r.QueryTable(genConfig.TablePrefix);

                var entityProject = GenUtils.GeneratorEntityProject(genConfig);


                _handler.AddFile(path + ".Entity.csproj", path + "/" + path + ".Entity", entityProject.ToString());
                var repositoryProject = GenUtils.GeneratorRepositoryProject(genConfig);
                _handler.AddFile(path + ".Repository.csproj", path + "/" + path + ".Repository", repositoryProject.ToString());

                var serviceProject = GenUtils.GeneratorServiceProject(genConfig);
                _handler.AddFile(path + ".Service.csproj", path + "/" + path + ".Service", serviceProject.ToString());

                var interfaceProject = GenUtils.GeneratorServiceInterfaceProject(genConfig);
                _handler.AddFile(path + ".Service.Interface.csproj", path + "/" + path + ".Service.Interface", interfaceProject.ToString());
                var sb = new StringBuilder();
                sb.Append(GenUtils.GeneratorXmlHeader(assmeblyName+".Entity", genConfig.Namespace+"."+genConfig.ModuleName+".Entity"));
                foreach (DataRow dr in table.Rows)
                {
                    DataTable columns = r.QueryColumns(dr["tableName"].ToString());
                    var dic = GenUtils.GeneratorCode(genConfig, dr, columns, provider:context.DbConfiguration.DbProviderName);
                    foreach(var item in dic)
                    {
                        if(item.Key.StartsWith("xml"))
                        {
                            sb.Append(item.Value);
                        } else
                        {
                            var keys = item.Key.Split('_');
                            _handler.AddFile(keys[1] + ".cs", path + "/" + path + "." + keys[0], item.Value.ToString());
                        }
                        
                    }
                }
                sb.Append(GenUtils.GeneratorXmlFooter());
                if(genConfig.IsGeneratorMapping)
                    _handler.AddFile(string.Format("uway.{0}.mapping.xml", genConfig.ModuleName), path + "/" + path + ".Entity", sb.ToString());
                _handler.AddFile("Unity.cs", path + "/" + path + ".Service", GenUtils.GeneratorUnity(genConfig.Namespace, genConfig.ModuleName));
            }

            _handler.Zip(path + ".zip", path);

            using (var stream = _handler.OpenFile(path + ".zip", FileMode.Open))
            {
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                // 设置当前流的位置为流的开始
                stream.Seek(0, SeekOrigin.Begin);
                return bytes;
            }
        }

        public DataSourceResult Query(DataSourceRequest request)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                var r = new CodeGenRepository(context);
                return r.QueryPage(request);
            }
        }
    }
}
