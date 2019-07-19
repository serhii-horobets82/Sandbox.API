rem heroku login
rem heroku container:login

rem docker tag evoflare-api registry.heroku.com/evoflare-api/web
rem docker push registry.heroku.com/evoflare-api/web
heroku container:push api -a evoflare-api --context-path ..\
heroku container:release api -a evoflare-api

