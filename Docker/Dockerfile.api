FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

EXPOSE $PORT

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
CMD ASPNETCORE_ENVIRONMENT=Docker ASPNETCORE_URLS=http://*:$PORT dotnet Evoflare.API.dll
 
