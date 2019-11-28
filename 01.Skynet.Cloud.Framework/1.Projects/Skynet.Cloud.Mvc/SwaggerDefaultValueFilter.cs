using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Mvc
{
    /// <summary>
    /// swagger过滤器
    /// </summary>
    public class SwaggerDefaultValueFilter : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            Guard.NotNull(operation, "operation");
            Guard.NotNull(context, "context");
            foreach (var parameter in operation.Parameters.OfType<NonBodyParameter>())
            {
                var description = context.ApiDescription.ParameterDescriptions.FirstOrDefault(p => p.Name == parameter.Name);
                if (description == null)
                    return;
                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata.Description;
                }

                if (description.RouteInfo != null)
                {
                    parameter.Required |= !description.RouteInfo.IsOptional;
                    if (parameter.Default == null)
                        parameter.Default = description.RouteInfo.DefaultValue;
                }
            }
        }
    }
}
