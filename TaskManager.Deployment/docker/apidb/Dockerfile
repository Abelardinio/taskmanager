FROM microsoft/mssql-server-linux

RUN mkdir -p /opt/mssql-scripts
COPY ["CreateDatabaseAndUser.sql", "/opt/mssql-scripts"]

ENV MSSQL_SA_PASSWORD=P@ssw0rd
ENV ACCEPT_EULA=Y

RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 10 \
	&& export DATABASE_NAME=TaskManager \
	&& export USER_NAME=TaskManagerUser \
	&& export PASSWORD=nNx4vp3R3vsh8d2 \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'P@ssw0rd' -i /opt/mssql-scripts/CreateDatabaseAndUser.sql \ 
    && pkill sqlservr 