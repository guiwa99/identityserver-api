FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY ./src ./

RUN dotnet restore Senac.IdentityServer.Api/Senac.IdentityServer.Api.csproj
RUN dotnet publish -c Release -o out Senac.IdentityServer.Api/Senac.IdentityServer.Api.csproj

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "Senac.IdentityServer.Api.dll"]
