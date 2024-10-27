#  SQL Server 2019
FROM mcr.microsoft.com/mssql/server:2019-latest

USER root

# pacotes necessários
RUN apt-get update && apt-get install -y \
    curl \
    gnupg \
    && curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - \
    && curl https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/prod.list > /etc/apt/sources.list.d/mssql-release.list \
    && apt-get update \
    && ACCEPT_EULA=Y apt-get install -y msodbcsql17 mssql-tools unixodbc-dev \
    && echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> /etc/profile

USER mssql

RUN chown -R mssql:mssql /var/opt/mssql

EXPOSE 1433

# Comando para iniciar 
CMD ["/opt/mssql/bin/sqlservr"]
