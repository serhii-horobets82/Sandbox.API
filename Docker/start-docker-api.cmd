docker build --pull -t evoflare-api .
docker run --name evoflare-api -e "PORT=5000" -p 5000:5000 --rm -it evoflare-api