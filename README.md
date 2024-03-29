# HotelBooking

## Comandos de interés:

```
dotnet new sln
dotnet new console -n MyConsoleApp
dotnet sln add MyConsoleApp/MyConsoleApp.csproj

dotnet new classlib -n NameOfProject

dotnet build
dotnet run --project .\Retrotracker.Presentation\Retrotracker.Presentation.csproj

dotnet add reference path/to/ProjectA/ProjectA.csproj

dotnet add package Microsoft.Extensions.DependencyInjection --version 8.0.0

dotnet run --project .\Retrotracker.Presentation\Retrotracker.Presentation.csproj
```
## Docker
https://learn.microsoft.com/es-es/dotnet/core/docker/build-container?tabs=windows&pivots=dotnet-8-0

```
docker build --rm -t new-net/cloud-retro:latest .

docker image ls | grap cloud-retro

docker run --rm -p 5000:5000 -p 5001:5001 -e ASPNET_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5001 new-net/cloud-retro

docker run -it new-net/cloud-retro
```

## Git

```
See https://aka.ms/new-console-template for more information
git checkout <hash del commit> -> para viajar a otro commit anterior
gitk - herramienta para ver los cambios
fastforward vs squas commit
git bisec
git blame
git reflog -> te salva la vida
git status
git stash
git checkout -> tambien puede ser para ir a un commit anterior
```