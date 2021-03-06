FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

LABEL com.evoflare.product="evoflare"

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        gosu \
        curl \
        dos2unix \
        libcurl3 \
    && rm -rf /var/lib/apt/lists/*

ENV ASPNETCORE_URLS http://+:5000
WORKDIR /app
EXPOSE 5000
COPY obj/Docker/publish/Api .
COPY entrypoint.sh /
RUN dos2unix /entrypoint.sh
RUN chmod +x /entrypoint.sh

HEALTHCHECK CMD curl -f http://localhost:5000/alive || exit 1

ENTRYPOINT ["/entrypoint.sh"]
