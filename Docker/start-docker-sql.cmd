docker build --pull -t evoflare-db -f Dockerfile.sql .
docker stop evoflare-db
docker rm evoflare-db
docker run --name evoflare-db -p 14330:1433 -d -it -v ./data:/var/opt/mssql/data evoflare-db
