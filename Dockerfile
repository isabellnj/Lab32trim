FROM microsoft/mssql-server-linux
COPY ./Create_database.sql .
COPY ./start.sh .
COPY ./entrypoint.sh .
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=Your!Passw0rd
RUN chmod +x ./start.sh
CMD /bin/bash ./entrypoint.sh