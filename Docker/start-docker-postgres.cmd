docker stop evoflare-db-pg
docker rm evoflare-db-pg
docker rmi evoflare-db-pg
docker build --pull -t evoflare-db-pg -f ./postgres/Dockerfile .
docker run --name evoflare-db-pg -p 54320:5432 -d -it -v ./postgres:/var/lib/postgresql/data evoflare-db-pg