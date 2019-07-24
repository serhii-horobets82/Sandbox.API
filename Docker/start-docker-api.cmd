docker stop evoflare-api
docker rm evoflare-api
docker rmi evoflare-api
docker build --pull -t evoflare-api -f Dockerfile.api ..\ 
docker run --name evoflare-api -e "PORT=5000" -p 5000:5000 -d -it evoflare-api 
docker attach --sig-proxy evoflare-api