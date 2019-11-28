#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1809 AS build
WORKDIR /src
COPY ["01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Cloud.CloudFoundryDemo/Skynet.Cloud.Cloud.CloudFoundryDemo.csproj", "01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Cloud.CloudFoundryDemo/"]
COPY ["01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Mvc/Skynet.Cloud.Mvc.csproj", "01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Mvc/"]
COPY ["01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Data/Skynet.Cloud.Data.csproj", "01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Data/"]
COPY ["01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Framework/Skynet.Cloud.Framework.csproj", "01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Framework/"]
COPY ["01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.ConnectorBase/Skynet.Cloud.ConnectorBase.csproj", "01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.ConnectorBase/"]
COPY ["01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Upms.Test.Repository/Skynet.Cloud.Upms.Test.Repository.csproj", "01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Upms.Test.Repository/"]
COPY ["01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Upms.Test.Entity/Skynet.Cloud.Upms.Test.Entity.csproj", "01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Upms.Test.Entity/"]
COPY ["01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Upms.Test.Service/Skynet.Cloud.Upms.Test.Service.csproj", "01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Upms.Test.Service/"]
COPY ["01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Upms.Test.Service.Interface/Skynet.Cloud.Upms.Test.Service.Interface.csproj", "01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Upms.Test.Service.Interface/"]
COPY ["01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Discovery.Abstract/Skynet.Cloud.Discovery.Abstract.csproj", "01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Discovery.Abstract/"]
COPY ["01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Dicovery.Core/Skynet.Cloud.Dicovery.Core.csproj", "01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Dicovery.Core/"]
COPY ["01.Skynet.Cloud.Framework/1.Projects/Steeltoe.Configuration.NacosServerBase/Steeltoe.Configuration.NacosServerBase.csproj", "01.Skynet.Cloud.Framework/1.Projects/Steeltoe.Configuration.NacosServerBase/"]
COPY ["01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Nacos/Skynet.Cloud.Nacos.csproj", "01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Nacos/"]
COPY ["01.Skynet.Cloud.Framework/1.Projects/Steeltoe.Discovery.NacosBase/Steeltoe.Discovery.NacosBase.csproj", "01.Skynet.Cloud.Framework/1.Projects/Steeltoe.Discovery.NacosBase/"]
RUN dotnet restore "01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Cloud.CloudFoundryDemo/Skynet.Cloud.Cloud.CloudFoundryDemo.csproj"
COPY . .
WORKDIR "/src/01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Cloud.CloudFoundryDemo"
RUN dotnet build "Skynet.Cloud.Cloud.CloudFoundryDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Skynet.Cloud.Cloud.CloudFoundryDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Skynet.Cloud.Cloud.CloudFoundryDemo.dll"]