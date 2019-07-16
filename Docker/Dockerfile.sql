FROM microsoft/mssql-server-linux:latest
ENV SA_PASSWORD=DatgE66VbHy7
ENV ACCEPT_EULA=Y

ADD ./data /var/opt/mssql/data/