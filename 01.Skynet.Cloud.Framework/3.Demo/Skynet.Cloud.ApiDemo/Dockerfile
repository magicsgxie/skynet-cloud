FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.ApiDemo/Skynet.Cloud.ApiDemo.csproj", "01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.ApiDemo/"]
COPY ["01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Mvc/Skynet.Cloud.Mvc.csproj", "01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Mvc/"]
COPY ["01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Data/Skynet.Cloud.Data.csproj", "01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Data/"]
COPY ["01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Framework/Skynet.Cloud.Framework.csproj", "01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Framework/"]
COPY ["01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.ConnectorBase/Skynet.Cloud.ConnectorBase.csproj", "01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.ConnectorBase/"]
COPY ["01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Upms.Test.Repository/Skynet.Cloud.Upms.Test.Repository.csproj", "01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Upms.Test.Repository/"]
COPY ["01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Upms.Test.Entity/Skynet.Cloud.Upms.Test.Entity.csproj", "01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Upms.Test.Entity/"]
COPY ["01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Upms.Test.Service/Skynet.Cloud.Upms.Test.Service.csproj", "01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Upms.Test.Service/"]
COPY ["01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Upms.Test.Service.Interface/Skynet.Cloud.Upms.Test.Service.Interface.csproj", "01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.Upms.Test.Service.Interface/"]
COPY ["01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Discovery.Abstract/Skynet.Cloud.Discovery.Abstract.csproj", "01.Skynet.Cloud.Framework/1.Projects/Skynet.Cloud.Discovery.Abstract/"]
RUN dotnet restore "01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.ApiDemo/Skynet.Cloud.ApiDemo.csproj"
COPY . .
WORKDIR "/src/01.Skynet.Cloud.Framework/3.Demo/Skynet.Cloud.ApiDemo"
RUN dotnet build "Skynet.Cloud.ApiDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Skynet.Cloud.ApiDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Skynet.Cloud.ApiDemo.dll"]