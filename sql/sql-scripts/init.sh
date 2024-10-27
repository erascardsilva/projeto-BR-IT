#!/bin/bash

for file in /var/opt/mssql/scripts/*.sql; do
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "3727BRIT@" -i "$file"
done
