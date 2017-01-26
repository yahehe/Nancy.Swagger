:: Create the Swagger.ObjectModel builds and package them
msbuild ./src/Swagger.ObjectModel/Swagger.ObjectModel.csproj /t:build /p:configuration=ReleaseNet40;Platform=AnyCPU /clp:ErrorsOnly
msbuild ./src/Swagger.ObjectModel/Swagger.ObjectModel.csproj /t:build /p:configuration=ReleaseNet45;Platform=AnyCPU /clp:ErrorsOnly
msbuild ./src/Swagger.ObjectModel/Swagger.ObjectModel.csproj /t:build,NugetPackage /p:configuration=ReleaseNet462;Platform=AnyCPU /clp:ErrorsOnly

:: Create the Nancy.Swagger builds and package them
msbuild ./src/Nancy.Swagger/Nancy.Swagger.csproj /t:build /p:configuration=ReleaseNet40;Platform=AnyCPU /clp:ErrorsOnly
msbuild ./src/Nancy.Swagger/Nancy.Swagger.csproj /t:build /p:configuration=ReleaseNet45;Platform=AnyCPU /clp:ErrorsOnly
msbuild ./src/Nancy.Swagger/Nancy.Swagger.csproj /t:build,NugetPackage /p:configuration=ReleaseNet462;Platform=AnyCPU /clp:ErrorsOnly

:: Create the Nancy.Swagger.Annotations builds and package them
msbuild ./src/Nancy.Swagger.Annotations/Nancy.Swagger.Annotations.csproj /t:build /p:configuration=ReleaseNet40;Platform=AnyCPU /clp:ErrorsOnly
msbuild ./src/Nancy.Swagger.Annotations/Nancy.Swagger.Annotations.csproj /t:build /p:configuration=ReleaseNet45;Platform=AnyCPU /clp:ErrorsOnly
msbuild ./src/Nancy.Swagger.Annotations/Nancy.Swagger.Annotations.csproj /t:build,NugetPackage /p:configuration=ReleaseNet462;Platform=AnyCPU /clp:ErrorsOnly