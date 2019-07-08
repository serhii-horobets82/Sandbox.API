docker build --pull -t evoflare-db -f Dockerfile.sql .
docker run --name evoflare-db -p 14330:1433 --rm -it evoflare-db