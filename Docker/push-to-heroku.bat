rem heroku login
rem heroku container:login

docker tag evoflare-api registry.heroku.com/evoflare-api/web
rem docker push registry.heroku.com/evoflare-api/web

heroku container:push web -a evoflare-api
heroku container:release web -a evoflare-api

