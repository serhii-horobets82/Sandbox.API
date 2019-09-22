docker stop evoflare-db-pg
docker rm evoflare-db-pg
docker rmi evoflare-db-pg
docker pull postgres
rem docker run --rm  --name evoflare-db-pg -e POSTGRES_PASSWORD=qwerty -e POSTGRES_USER=evoflare -p 54320:5432 -v ./postgres:/var/lib/postgresql/data postgres 
docker run --rm  --name evoflare-db-pg -e POSTGRES_PASSWORD=DatgE66VbHy7 -e POSTGRES_USER=evoflare -p 54320:5432 postgres 