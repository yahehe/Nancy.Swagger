echo %APPVEYOR_BUILD_NUMBER%

dotnet pack src/Nancy.Swagger --configuration Release
dotnet pack src/Nancy.Swagger.Annotations --configuration Release
dotnet pack src/Swagger.ObjectModel --configuration Release