:: 包搜索字符串
echo %1
:: 项目方案地址
echo %2

:: 包名称
set nupkg=""

:: 编译
dotnet msbuild %2 /p:Configuration=Release

:: 打包
dotnet pack %2 -c Release --output nupkgs

:: 更新包名称
for %%a in (dir /s /a /b "./nupkgs/%1") do (set nupkg=%%a)

:: 推送包
nuget push nupkgs/%nupkg% bc97b237-abc2-33c3-a0cd-c34dde4c5cf9 -source http://192.168.15.118:8099/repository/nuget-hosted/