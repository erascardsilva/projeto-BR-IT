#!/bin/sh

# Iniciar o Nginx em segundo plano
nginx &

# Esperar um pouco para garantir que o Nginx esteja totalmente iniciado
sleep 2

# Exibir a URL de acesso
echo "Acesse seu aplicativo em: http://localhost"

# Manter o contêiner em execução
tail -f /dev/null


## NAO VOU UTILIZAR APAGAR