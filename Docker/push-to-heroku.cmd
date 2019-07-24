rem heroku login
rem heroku container:login

echo "Push container evoflare-api to heroku"
rem docker tag evoflare-api registry.heroku.com/evoflare-api/web
rem docker push registry.heroku.com/evoflare-api/web
heroku container:push web -a evoflare-api --context-path ..\
heroku container:release web -a evoflare-api

