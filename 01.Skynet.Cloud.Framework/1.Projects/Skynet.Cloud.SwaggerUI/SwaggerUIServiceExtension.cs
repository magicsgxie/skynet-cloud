

namespace UWay.Skynet.Cloud.SwaggerUI
{
    using UWay.Skynet.Cloud.SwaggerUI.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Swashbuckle.AspNetCore.Swagger;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Swagger UI Service Extension.
    /// </summary>
    public static class SwaggerUIServiceExtension
    {
        /// <summary>
        /// 添加自定义API文档生成(支持文档配置)
        /// </summary>
        /// <param name="services">服务.</param>
        /// <param name="configuration">配置.</param>
        public static void AddCustomSwaggerGen(this IServiceCollection services, IConfiguration configuration)
        {
            var docConfigInfo = GetApiDocsConfigInfo(configuration);
            if (docConfigInfo == null)
            {
                return;
            }

            var webRootDirectory = GetRootPath();

            // 设置API文档生成.
            services.AddSwaggerGen(options =>
            {
                // 将所有枚举显示为字符串
                if (docConfigInfo.DescribeAllEnumsAsStrings)
                {
                    options.DescribeAllEnumsAsStrings();
                }

                if (docConfigInfo.Authorize)
                {
                    // 以便于在界面上显示验证（Authorize）按钮，验证按钮处理逻辑基于 wwwroot/swagger/ui/index.html
                    options.AddSecurityDefinition("Bearer", new BasicAuthScheme());
                }

                if (docConfigInfo.UseFullNameForSchemaId)
                {
                    // 使用全名作为架构id
                    options.CustomSchemaIds(p => p.FullName);
                }

                foreach (var doc in docConfigInfo.SwaggerDocInfos)
                {
                    options.SwaggerDoc(doc.GroupName ?? doc.Version, new Info
                    {
                        Title = doc.Title,
                        Version = doc.Version,
                        Description = doc.Description,
                        Contact = new Contact
                        {
                            Name = doc.Contact?.Name,
                            Email = doc.Contact?.Email,
                        },
                    });
                }

                // 遍历所有xml并加载
                var paths = new List<string>();
                var plusPath = Path.Combine(webRootDirectory ?? throw new InvalidOperationException(), "PlugIns");
                if (Directory.Exists(plusPath))
                {
                    var xmlFiles = new DirectoryInfo(plusPath).GetFiles("*.xml");
                    paths.AddRange(xmlFiles.Select(item => item.FullName));
                }

                var binXmlFiles = new DirectoryInfo(webRootDirectory).GetFiles("*.xml", SearchOption.TopDirectoryOnly);
                paths.AddRange(binXmlFiles.Select(item => item.FullName));

                foreach (var filePath in paths)
                {
                    options.IncludeXmlComments(filePath);
                }

                // 接口分组和隐藏处理
                options.DocInclusionPredicate((docName, apiDescription) =>
                {
                    // 全局API隐藏逻辑处理
                    if (IsHiddenApi(docConfigInfo.HiddenApi, apiDescription))
                    {
                        return false;
                    }

                    // 只有一个配置则不分组
                    if (docConfigInfo.SwaggerDocInfos.Count <= 1)
                    {
                        return true;
                    }

                    var doc = docConfigInfo.SwaggerDocInfos.FirstOrDefault(p =>
                        p.GroupName == docName);
                    if (doc == null)
                    {
                        return false;
                    }

                    // 分组API隐藏逻辑处理
                    if (IsHiddenApi(doc.HiddenApi, apiDescription))
                    {
                        return false;
                    }

                    // API分组处理
                    if (!string.IsNullOrEmpty(doc.GroupUrlPrefix))
                    {
                        // 分组处理:隐藏组Url不一致的接口
                        return apiDescription.RelativePath.StartsWith(
                            doc.GroupUrlPrefix,
                            StringComparison.OrdinalIgnoreCase);
                    }

                    return false;
                });
            });
        }

        /// <summary>
        /// 是否隐藏此API.
        /// </summary>
        /// <param name="hiddenApiConfigInfo">隐藏.</param>
        /// <param name="apiDescription">Api 描述.</param>
        /// <returns>bool.</returns>
        private static bool IsHiddenApi(HiddenApiConfigInfo hiddenApiConfigInfo, ApiDescription apiDescription)
        {
            if (hiddenApiConfigInfo != null && hiddenApiConfigInfo.IsEnabled && hiddenApiConfigInfo.Urls != null && hiddenApiConfigInfo.Urls.Any())
            {
                foreach (var item in hiddenApiConfigInfo.Urls)
                {
                    if (apiDescription.RelativePath.IndexOf(item.Url?.Trim(), StringComparison.OrdinalIgnoreCase) != -1 && (item.HttpMethod == "*" || apiDescription.HttpMethod.Equals(item.HttpMethod, StringComparison.OrdinalIgnoreCase)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 获取文件根路径.
        /// </summary>
        /// <returns>string.</returns>
        private static string GetRootPath()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            if (string.IsNullOrWhiteSpace(path))
            {
                path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            }

            return path;
        }

        /// <summary>
        /// 获取配置信息.
        /// </summary>
        /// <param name="configuration">配置.</param>
        /// <returns>SwaggerConfigInfo.</returns>
        private static SwaggerConfigInfo GetApiDocsConfigInfo(IConfiguration configuration)
        {
            var configs = configuration?["SwaggerDoc:IsEnabled"] != null
                ? configuration.GetSection("SwaggerDoc").Get<SwaggerConfigInfo>()
                : null;
            if (configs.SwaggerDocInfos != null)
            {
                foreach (var item in configs.SwaggerDocInfos)
                {
                    if (!string.IsNullOrWhiteSpace(item.GroupUrlPrefix))
                    {
                        item.GroupUrlPrefix = item.GroupUrlPrefix.Trim().TrimStart('/');
                    }
                }
            }

            return configs;
        }

        /// <summary>
        /// 启用自定义API文档(支持文档配置).
        /// </summary>
        /// <param name="app">app.</param>
        /// <param name="configuration">配置.</param>
        public static void UseCustomSwaggerUI(
            this IApplicationBuilder app,
            IConfiguration configuration)
        {
            var docConfigInfo = GetApiDocsConfigInfo(configuration);
            if (docConfigInfo == null)
            {
                return;
            }

            app.UseSwagger(c =>
            {
                if (docConfigInfo.SwaggerDocInfos.Count > 1)
                {
                    c.RouteTemplate = "swagger/{documentName}/swagger.json";
                }
            });

            // 加载swagger-ui 资源 (HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                foreach (var doc in docConfigInfo.SwaggerDocInfos)
                {
                    options.SwaggerEndpoint($"/swagger/{doc.GroupName ?? doc.Version}/swagger.json", doc.Title ?? "App API V1");
                }

                // 允许通过嵌入式资源配置首页
                if (!string.IsNullOrWhiteSpace(docConfigInfo.ManifestResourceUrl) && !string.IsNullOrWhiteSpace(docConfigInfo.ManifestResourceAssembly))
                {
                    options.IndexStream = () =>
                        Assembly.Load(docConfigInfo.ManifestResourceAssembly)
                            .GetManifestResourceStream(docConfigInfo.ManifestResourceUrl);
                }
            });
        }
    }
}
