docker stop evoflare-db
docker rm evoflare-db
docker rmi evoflare-db
docker build --pull -t evoflare-db -f Dockerfile.sql .
docker run --name evoflare-db -p 14330:1433 -d -it -v ./mssql:/var/opt/mssql/data evoflare-db
