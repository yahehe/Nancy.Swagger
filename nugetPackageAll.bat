echo %APPVEYOR_BUILD_NUMBER%
powershell -Command "(gc src/Nancy.Swagger/project.json) -replace '.0-\*', '.%APPVEYOR_BUILD_NUMBER%-*' | Out-File src/Nancy.Swagger/project.json"
powershell -Command "(gc src/Nancy.Swagger.Annotations/project.json) -replace '.0-\*', '.%APPVEYOR_BUILD_NUMBER%-*' | Out-File src/Nancy.Swagger.Annotations/project.json"
powershell -Command "(gc src/Swagger.ObjectModel/project.json) -replace '.0-\*', '.%APPVEYOR_BUILD_NUMBER%-*' | Out-File src/Swagger.ObjectModel/project.json"

dotnet pack src/Nancy.Swagger --configuration Release --output src/Nancy.Swagger/NuGet --version-suffix "alpha"
dotnet pack src/Nancy.Swagger.Annotations --configuration Release --output src/Nancy.Swagger.Annotations/NuGet --version-suffix "alpha"
dotnet pack src/Swagger.ObjectModel --configuration Release --output src/Swagger.ObjectModel/NuGet --version-suffix "alpha"