cd .\Demos.MusicTheory\

dotnet build -c Release .\Demos.MusicTheory.csproj

dotnet pack -c Release -p:NuspecFile=.\.nuspec -o .\..\NugetPackageBuilds\

@echo off

echo.
echo We're done. Find the result nuget package in: \NugetPackageBuild
echo.
echo When testing locally, make sure to clear %user%/.nuget/packages entry if the matching version for current package already exists.

pause