# project to test dotnet publish issue from cli

- clone the project
- dotnet build from the csproj location
- Donot use VS publish option. If so delete the file from bin folder
- dotnetpublishrepo>dotnet publish -c Release -r win-x64 --self-contained -f netcoreapp2.1
