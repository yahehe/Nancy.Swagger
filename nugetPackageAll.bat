echo %APPVEYOR_BUILD_NUMBER%

dotnet pack src/Nancy.Swagger --configuration Release
if %errorlevel% neq 0 exit /b %errorlevel%
dotnet pack src/Nancy.Swagger.Annotations --configuration Release
if %errorlevel% neq 0 exit /b %errorlevel%
dotnet pack src/Swagger.ObjectModel --configuration Release
if %errorlevel% neq 0 exit /b %errorlevel%