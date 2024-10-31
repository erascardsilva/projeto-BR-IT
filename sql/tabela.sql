-- DROP SCHEMA dbo;

CREATE SCHEMA dbo;
-- NEWDESENV.dbo.Clientes definição

-- Drop table

-- DROP TABLE NEWDESENV.dbo.Clientes;

CREATE TABLE NEWDESENV.dbo.Clientes (
	ClienteID int IDENTITY(1,1) NOT NULL,
	Nome nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Email nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Telefone nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK__Clientes__71ABD0A7BE8F84F8 PRIMARY KEY (ClienteID)
);


-- NEWDESENV.dbo.Fornecedores definição

-- Drop table

-- DROP TABLE NEWDESENV.dbo.Fornecedores;

CREATE TABLE NEWDESENV.dbo.Fornecedores (
	FornecedorID int IDENTITY(1,1) NOT NULL,
	Nome nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Contato nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Telefone nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK__Forneced__494B8C3087FFDD98 PRIMARY KEY (FornecedorID)
);


-- NEWDESENV.dbo.NotaFiscal definição

-- Drop table

-- DROP TABLE NEWDESENV.dbo.NotaFiscal;

CREATE TABLE NEWDESENV.dbo.NotaFiscal (
	NotaFiscalID int IDENTITY(1,1) NOT NULL,
	Numero nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	DataEmissao datetime NOT NULL,
	Valor decimal(18,2) NOT NULL,
	Descricao nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ClienteID int NOT NULL,
	CONSTRAINT PK__NotaFisc__F82B6CD64E88E548 PRIMARY KEY (NotaFiscalID)
);


-- NEWDESENV.dbo.Posts definição

-- Drop table

-- DROP TABLE NEWDESENV.dbo.Posts;

CREATE TABLE NEWDESENV.dbo.Posts (
	UserId int NOT NULL,
	Id int NOT NULL,
	Title nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Body nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK__Posts__3214EC07F5EEF4AF PRIMARY KEY (Id)
);


-- NEWDESENV.dbo.Produtos definição

-- Drop table

-- DROP TABLE NEWDESENV.dbo.Produtos;

CREATE TABLE NEWDESENV.dbo.Produtos (
	ProdutoID int IDENTITY(1,1) NOT NULL,
	Nome nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Preco decimal(10,2) NOT NULL,
	FornecedorID int NULL,
	CONSTRAINT PK__Produtos__9C8800C3BE2F0A11 PRIMARY KEY (ProdutoID),
	CONSTRAINT FK__Produtos__Fornec__3A81B327 FOREIGN KEY (FornecedorID) REFERENCES NEWDESENV.dbo.Fornecedores(FornecedorID)
);


