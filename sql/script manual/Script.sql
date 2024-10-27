-- Criar banco de dados
CREATE DATABASE NEWDESENV;
GO

-- Usar o banco de dados criado
USE NEWDESENV;
GO

-- Criar tabela Clientes
CREATE TABLE Clientes (
    ClienteID INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Telefone NVARCHAR(20) NOT NULL
);
GO

-- Criar tabela Fornecedores
CREATE TABLE Fornecedores (
    FornecedorID INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    Contato NVARCHAR(100),
    Telefone NVARCHAR(20)
);
GO

-- Criar tabela Produtos
CREATE TABLE Produtos (
    ProdutoID INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    Preco DECIMAL(10, 2) NOT NULL,
    FornecedorID INT,
    FOREIGN KEY (FornecedorID) REFERENCES Fornecedores(FornecedorID)
);
GO

CREATE TABLE Produtos (
    ProdutoID INT PRIMARY KEY IDENTITY,
    Nome NVARCHAR(255) NOT NULL,
    Preco DECIMAL(18, 2) NOT NULL,
    Estoque INT NOT NULL,
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
    DataAtualizacao DATETIME NOT NULL DEFAULT GETDATE()
);


-- Criar tabela Nota Fiscal
CREATE TABLE NotaFiscal (
    NotaFiscalID INT PRIMARY KEY IDENTITY(1,1),
    ClienteID INT,
    DataVenda DATETIME NOT NULL,
    Total DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID)
);
GO

-- Criar tabela Posts
CREATE TABLE Posts (
    UserId INT NOT NULL,
    Id INT PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Body NVARCHAR(MAX) NOT NULL
);
GO

-- Inserir dados na tabela Posts
INSERT INTO Posts (UserId, Id, Title, Body) VALUES
(1, 1, 'sunt aut facere repellat provident occaecati excepturi optio reprehenderit', 'quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto'),
(1, 2, 'qui est esse', 'est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla'),
(1, 3, 'ea molestias quasi exercitationem repellat qui ipsa sit aut', 'et iusto sed quo iure\nvoluptatem occaecati omnis eligendi aut ad\nvoluptatem doloribus vel accusantium quis pariatur\nmolestiae porro eius odio et labore et velit aut'),
(1, 4, 'eum et est occaecati', 'ullam et saepe reiciendis voluptatem adipisci\nsit amet autem assumenda provident rerum culpa\nquis hic commodi nesciunt rem tenetur doloremque ipsam iure\nquis sunt voluptatem rerum illo velit'),
(1, 5, 'nesciunt quas odio', 'repudiandae veniam quaerat sunt sed\nalias aut fugiat sit autem sed est\nvoluptatem omnis possimus esse voluptatibus quis\nest aut tenetur dolor neque'),
(1, 6, 'dolorem eum magni eos aperiam quia', 'ut aspernatur corporis harum nihil quis provident sequi\nmollitia nobis aliquid molestiae\nperspiciatis et ea nemo ab reprehenderit accusantium quas\nvoluptate dolores velit et doloremque molestiae'),
(1, 7, 'magnam facilis autem', 'dolore placeat quibusdam ea quo vitae\nmagni quis enim qui quis quo nemo aut saepe\nquidem repellat excepturi ut quia\nsunt ut sequi eos ea sed quas');
GO

-- Stored Procedure para Inserir Cliente
CREATE PROCEDURE sp_InserirCliente
    @Nome NVARCHAR(100),
    @Email NVARCHAR(100),
    @Telefone NVARCHAR(20)
AS
BEGIN
    INSERT INTO Clientes (Nome, Email, Telefone)
    VALUES (@Nome, @Email, @Telefone);
END;
GO

-- Exemplo de Stored Procedure para Buscar Clientes
CREATE PROCEDURE sp_BuscarClientes
AS
BEGIN
    SELECT * FROM Clientes;
END;
GO

-- Buscar Cliente por ID
CREATE PROCEDURE sp_BuscarClientePorID
    @ClienteID INT
AS
BEGIN
    SELECT * FROM Clientes WHERE ClienteID = @ClienteID;
END;
GO

-- Exemplo de Stored Procedure para Deletar Cliente
CREATE PROCEDURE sp_DeletarCliente
    @ClienteID INT
AS
BEGIN
    DELETE FROM Clientes WHERE ClienteID = @ClienteID;
END;
GO

-- Exemplo de Stored Procedure para Atualizar Cliente
CREATE PROCEDURE sp_AtualizarCliente
    @ClienteID INT,
    @Nome NVARCHAR(100),
    @Email NVARCHAR(100),
    @Telefone NVARCHAR(20)
AS
BEGIN
    UPDATE Clientes
    SET Nome = @Nome, Email = @Email, Telefone = @Telefone
    WHERE ClienteID = @ClienteID;
END;
GO

-- Stored Procedure para obter todos os Posts
CREATE PROCEDURE sp_GetAllPosts
AS
BEGIN
    SELECT * FROM Posts;
END;
GO

-- Stored Procedure para obter Post por ID
CREATE PROCEDURE sp_GetPostById
    @Id INT
AS
BEGIN
    SELECT * FROM Posts WHERE Id = @Id;
END;
GO

-- Stored Procedure para inserir um novo Post
CREATE PROCEDURE sp_InsertPost
    @UserId INT,
    @Title NVARCHAR(255),
    @Body NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Posts (UserId, Title, Body) VALUES (@UserId, @Title, @Body);
END;
GO

-- Stored Procedure para atualizar um Post
CREATE PROCEDURE sp_UpdatePost
    @Id INT,
    @UserId INT,
    @Title NVARCHAR(255),
    @Body NVARCHAR(MAX)
AS
BEGIN
    UPDATE Posts
    SET UserId = @UserId, Title = @Title, Body = @Body
    WHERE Id = @Id;
END;
GO

-- Stored Procedure para deletar um Post
CREATE PROCEDURE sp_DeletePost
    @Id INT
AS
BEGIN
    DELETE FROM Posts WHERE Id = @Id;
END;
GO

-- Stored Procedures para Fornecedores

-- Stored Procedure para Inserir Fornecedor
CREATE PROCEDURE sp_InserirFornecedor
    @Nome NVARCHAR(100),
    @Contato NVARCHAR(100),
    @Telefone NVARCHAR(20)
AS
BEGIN
    INSERT INTO Fornecedores (Nome, Contato, Telefone)
    VALUES (@Nome, @Contato, @Telefone);
END;
GO

-- Exemplo de Stored Procedure para Buscar Fornecedores
CREATE PROCEDURE sp_BuscarFornecedores
AS
BEGIN
    SELECT * FROM Fornecedores;
END;
GO

-- Buscar Fornecedor por ID
CREATE PROCEDURE sp_BuscarFornecedorPorID
    @FornecedorID INT
AS
BEGIN
    SELECT * FROM Fornecedores WHERE FornecedorID = @FornecedorID;
END;
GO

-- Exemplo de Stored Procedure para Deletar Fornecedor
CREATE PROCEDURE sp_DeletarFornecedor
    @FornecedorID INT
AS
BEGIN
    DELETE FROM Fornecedores WHERE FornecedorID = @FornecedorID;
END;
GO

-- Exemplo de Stored Procedure para Atualizar Fornecedor
CREATE PROCEDURE sp_AtualizarFornecedor
    @FornecedorID INT,
    @Nome NVARCHAR(100),
    @Contato NVARCHAR(100),
    @Telefone NVARCHAR(20)
AS
BEGIN
    UPDATE Fornecedores
    SET Nome = @Nome, Contato = @Contato, Telefone = @Telefone
    WHERE FornecedorID = @FornecedorID;
END;
GO

-- Stored Procedures para Nota Fiscal

-- Stored Procedure para Inserir Nota Fiscal
CREATE PROCEDURE sp_InserirNotaFiscal
    @ClienteID INT,
    @DataVenda DATETIME,
    @Total DECIMAL(10, 2)
AS
BEGIN
    INSERT INTO NotaFiscal (ClienteID, DataVenda, Total)
    VALUES (@ClienteID, @DataVenda, @Total);
END;
GO

-- Exemplo de Stored Procedure para Buscar Notas Fiscais
CREATE PROCEDURE sp_BuscarNotasFiscais
AS
BEGIN
    SELECT * FROM NotaFiscal;
END;
GO

-- Buscar Nota Fiscal por ID
CREATE PROCEDURE sp_BuscarNotaFiscalPorID
    @NotaFiscalID INT
AS
BEGIN
    SELECT * FROM NotaFiscal WHERE NotaFiscalID = @NotaFiscalID;
END;
GO

-- Exemplo de Stored Procedure para Deletar Nota Fiscal
CREATE PROCEDURE sp_DeletarNotaFiscal
    @NotaFiscalID INT
AS
BEGIN
    DELETE FROM NotaFiscal WHERE NotaFiscalID = @NotaFiscalID;
END;
GO

-- Exemplo de Stored Procedure para Atualizar Nota Fiscal
CREATE PROCEDURE sp_AtualizarNotaFiscal
    @NotaFiscalID INT,
    @ClienteID INT,
    @DataVenda DATETIME,
    @Total DECIMAL(10, 2)
AS
BEGIN
    UPDATE NotaFiscal
    SET ClienteID = @ClienteID, DataVenda = @DataVenda, Total = @Total
    WHERE NotaFiscalID = @NotaFiscalID;
END;
GO


-- Stored Procedures para Produtos
CREATE PROCEDURE sp_InserirProduto
    @Nome NVARCHAR(100),
    @Preco DECIMAL(18,2),
    @Estoque INT,
    @FornecedorID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Insere o novo produto
    INSERT INTO Produtos (Nome, Preco, Estoque, FornecedorID)
    VALUES (@Nome, @Preco, @Estoque, @FornecedorID);
END
GO


-- Exemplo de Stored Procedure para Buscar Produtos
CREATE PROCEDURE sp_BuscarProdutos
AS
BEGIN
    SELECT * FROM Produtos;
END;
GO

-- Buscar Produto por ID
CREATE PROCEDURE sp_BuscarProdutoPorID
    @ProdutoID INT
AS
BEGIN
    SELECT * FROM Produtos WHERE ProdutoID = @ProdutoID;
END;
GO

-- Exemplo de Stored Procedure para Deletar Produto
CREATE PROCEDURE sp_DeletarProduto
    @ProdutoID INT
AS
BEGIN
    DELETE FROM Produtos WHERE ProdutoID = @ProdutoID;
END;
GO

-- Exemplo de Stored Procedure para Atualizar Produto
CREATE PROCEDURE sp_AtualizarProduto
    @ProdutoID INT,
    @Nome NVARCHAR(100),
    @Preco DECIMAL(10, 2),
    @FornecedorID INT
AS
BEGIN
    UPDATE Produtos
    SET Nome = @Nome, Preco = @Preco, FornecedorID = @FornecedorID
    WHERE ProdutoID = @ProdutoID;
END;
GO


CREATE PROCEDURE sp_ListarProdutos
AS
BEGIN
    SELECT * FROM Produtos;
END;
