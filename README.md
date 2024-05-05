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

dotnet run --project .\Presentation\Presentation.csproj
```
## Docker
https://learn.microsoft.com/es-es/dotnet/core/docker/build-container?tabs=windows&pivots=dotnet-8-0

Atención! los paths de los volumenes tienen que ser absolutos
```
docker build --rm -t hotelbookingapi:v1 .
docker run -it hotelbookingapi:v1
docker run -v myvol1:/App/LocalStorage -it asalasher/hotelbooking:v1



docker tag local-image:tag username/repository:tag
docker push username/repository:tag




docker run -v mytestnamedvol3:/App/LocalStorage -it dad9982a2cce
docker run -v mytestnamedvol13:/App/LocalStorage -it asalasher/hotelbooking:v1

docker run -v C:/Users/Alberto/volumetest:/Presentation/bin/Debug/net8.0/LocalStorage -it 8b83eb49b384
docker run -v C:/Users/Alberto/volumetest:/LocalStorage/ -it 8b83eb49b384

docker run -v ${PWD}:/App/LocalStorage -it dad9982a2cce 5bdff3909711

docker run -v ${PWD}\volume:/App/LocalStorage -it dad9982a2cce

docker run -v $PWD/volume:/LocalStorage/ -it dad9982a2cce
docker run -v ${PWD}/volume:/LocalStorage/ -it dad9982a2cce


docker build --rm -t new-net/hotelbookingapi:v1 .

docker image ls | grap cloud-hotel

docker run --rm -p 5000:5000 -p 5001:5001 -e ASPNET_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5001 new-net/cloud-retro

docker run --rm -p 5000:5000 -p 5001:5001 -e ASPNET_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5001 new-net/cloud-hotel

docker run -it new-net-app/cloud-hotel
```

## Git
fastforward vs squas commit
git reflog -> te salva la vida
git checkout <hash del commit> -> para viajar a otro commit anterior

```
gitk - herramienta para ver los cambios
git bisec
git blame
git status
git stash
git checkout -> tambien puede ser para ir a un commit anterior
```