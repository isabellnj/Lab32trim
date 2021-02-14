docker pull mcr.microsoft.com/mssql/server:2019-latest&&docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Pa$$w0rdSQL' -p 1500:1433 --name sqltestcontainer -d mcr.microsoft.com/mssql/server:2019-latest 
