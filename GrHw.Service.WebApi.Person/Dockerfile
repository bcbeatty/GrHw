FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY *.sln ./
COPY GrHw.Service.WebApi.Person/GrHw.Service.WebApi.Person.csproj GrHw.Service.WebApi.Person/
RUN dotnet restore
COPY . .
WORKDIR /src/GrHw.Service.WebApi.Person
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GrHw.Service.WebApi.Person.dll"]
