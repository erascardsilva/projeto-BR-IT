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

```plaintext
bash
Copiar código
git clone git@github.com:erascardsilva/projeto-BR-IT.git
cd projeto-BR-IT
Execute o Docker Compose:

bash
Copiar código

docker-compose up ---build 

```
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

http://localhost:8080

-------------------------------------------------------------

# Estrutura do Banco de Dados e Stored Procedures

Este documento descreve as tabelas e stored procedures utilizadas no projeto.

---

## Estrutura das Tabelas

###  Tabelas

Guarda informações dos clientes da aplicação.

```sql
CREATE TABLE Clientes (
    ClienteID INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Telefone NVARCHAR(20) NOT NULL
);

CREATE TABLE Produtos (
    ProdutoID INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    Preco DECIMAL(10, 2) NOT NULL,
    FornecedorID INT,
    FOREIGN KEY (FornecedorID) REFERENCES Fornecedores(FornecedorID)
);

CREATE TABLE NotaFiscal (
    NotaFiscalID INT PRIMARY KEY IDENTITY(1,1),
    ClienteID INT,
    DataVenda DATETIME NOT NULL,
    Total DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID)
);

CREATE TABLE Posts (
    UserId INT NOT NULL,
    Id INT PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Body NVARCHAR(MAX) NOT NULL
);

```

### Procedures encontrar e analisar na pasta sql 


