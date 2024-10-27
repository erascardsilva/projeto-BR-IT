#Erasmo Cardoso  testar atraso

#!/bin/bash
# Verifica se o SQL Server está disponível
while ! nc -z sqlserver-db 1433; do   
  sleep 1 # espera um segundo
done
echo "SQL Server está pronto!"

# Inicia a API
dotnet AcessoAPI.dll
