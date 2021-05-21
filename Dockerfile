#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build-env

WORKDIR /app

COPY . ./
WORKDIR /app/Fitverse.MembersService
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal
WORKDIR /app
EXPOSE 5003
COPY --from=build-env /app/Fitverse.MembersService/out .
ENTRYPOINT ["dotnet", "Fitverse.MembersService.dll"] 

