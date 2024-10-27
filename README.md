# Projeto de Sistema de Gestão Integrado com Docker Compose

Este projeto é uma aplicação de gestão para clientes, fornecedores, produtos, notas fiscais e posts, utilizando **Docker Compose** para orquestrar os containers. A aplicação é composta por um backend em **C# (.NET 8)**, um banco de dados **SQL Server** e um frontend simples em **HTML** servido via **Nginx**.

## Estrutura do Projeto

```plaintext
projeto-BR-IT/
├── AcessoAPI/                # Diretório com o backend em C# (.NET 8)
│   ├── Controllers/          # Contém os controladores da API
│   ├── Data/                 # Configuração do banco de dados
│   ├── Repositories/         # Acesso ao banco e uso de stored procedures
│   ├── AcessoAPI.csproj      # Projeto principal em C#
│   ├── Program.cs            # Ponto de entrada do backend
│   └── Dockerfile            # Dockerfile para o backend
├── FrontEnd/                 # HTML básico servido via Nginx
├── sql/                      # Scripts SQL para criar e popular o banco
│   ├── Dockerfile            # Dockerfile para o SQL Server
│   ├── script manual/        # Scripts SQL de configuração manual do banco
│   └── sql-scripts/          # Scripts SQL principais de criação e população
├── docker-compose.yml        # Arquivo de configuração do Docker Compose
└── README.md                 # Documentação do projeto
```

Tecnologias Utilizadas
Backend: C# (.NET 8)
Banco de Dados: SQL Server (com stored procedures para operações CRUD)
Frontend: HTML básico servido via Nginx
Orquestração: Docker Compose
Configuração e Execução
Pré-Requisitos
Docker e Docker Compose instalados.
Como Executar
Usando Docker Compose
Clone este repositório:

bash
Copiar código
git clone <URL_DO_REPOSITORIO>
cd projeto-BR-IT
Execute o Docker Compose:

bash
Copiar código
docker-compose up -d
Esse comando inicializará:

O backend em C# que fornece a API REST.
O SQL Server, onde o banco de dados será criado automaticamente.
O Nginx, que servirá o frontend HTML.
Nota: O banco de dados será automaticamente configurado pelos scripts SQL na pasta sql/sql-scripts ao iniciar o container SQL Server.

Executando Manualmente (Sem Docker Compose)
Instale o SQL Server, .NET SDK e Nginx em sua máquina local.
Crie o banco de dados usando o script SQL disponível na pasta sql/script manual/.
Inicie o backend:
bash
Copiar código
cd AcessoAPI
dotnet run
Inicie o Nginx ou configure outro servidor para servir o frontend.
Endpoints da API
Clientes

POST /clientes - Insere um cliente.
GET /clientes - Retorna todos os clientes.
GET /clientes/{id} - Retorna um cliente por ID.
PUT /clientes/{id} - Atualiza um cliente por ID.
DELETE /clientes/{id} - Deleta um cliente por ID.
Fornecedores, Produtos, Nota Fiscal e Posts

Endpoints similares com métodos para CRUD, conforme necessário.
Observações
Banco de Dados: Todas as operações no banco são feitas exclusivamente via stored procedures, garantindo maior segurança e organização no acesso aos dados.
Script Manual: Caso deseje configurar o banco manualmente, os scripts SQL estão na pasta sql/script manual/.