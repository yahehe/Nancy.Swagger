echo %APPVEYOR_BUILD_NUMBER%

dotnet pack src/Nancy.Swagger --configuration Release --version-suffix "alpha"
dotnet pack src/Nancy.Swagger.Annotations --configuration Release --version-suffix "alpha"
dotnet pack src/Swagger.ObjectModel --configuration Release --version-suffix "alpha"