#!/usr/bin/env bash
set -e

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

echo -e "\n## Building API"

echo -e "\nBuilding app"
echo ".NET Core version $(dotnet --version)"
echo "Restore"
dotnet restore $DIR/Evoflare.API.csproj
echo "Clean"
dotnet clean $DIR/Evoflare.API.csproj -c "Release" -o $DIR/obj/Docker/publish/Api
echo "Publish"
dotnet publish $DIR/Evoflare.API.csproj -c "Release" -o $DIR/obj/Docker/publish/Api

echo -e "\nBuilding docker image"
docker --version
docker build -t evoflare-api $DIR/.

# TODO remove
docker image tag evoflare-api evoflare.docker:50000/api
docker push evoflare.docker:50000/api

# docker hub
docker tag evoflare.docker:50000/api evoflare/api
docker push evoflare/api