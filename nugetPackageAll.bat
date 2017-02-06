dotnet pack src/Nancy.Swagger --configuration Release --output src/Nancy.Swagger/NuGet --version-suffix "alpha"
dotnet pack src/Nancy.Swagger.Annotations --configuration Release --output src/Nancy.Swagger.Annotations/NuGet --version-suffix "alpha"
dotnet pack src/Swagger.ObjectModel --configuration Release --output src/Swagger.ObjectModel/NuGet --version-suffix "alpha"