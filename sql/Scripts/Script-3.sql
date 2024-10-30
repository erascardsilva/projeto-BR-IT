-- Renomeia a tabela atual
EXEC sp_rename 'NotaFiscal', 'NotaFiscal_Old';

-- Cria uma nova tabela com o campo NotaFiscalID como IDENTITY
CREATE TABLE NotaFiscal (
    NotaFiscalID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Numero NVARCHAR(50) NOT NULL,
    DataEmissao DATETIME NOT NULL,
    Valor DECIMAL(18, 2) NOT NULL,
    Descricao NVARCHAR(255),
    ClienteID INT NOT NULL
);

-- Insere os dados antigos de volta, se aplicável
INSERT INTO NotaFiscal (Numero, DataEmissao, Valor, Descricao, ClienteID)
SELECT Numero, DataEmissao, Valor, Descricao, ClienteID
FROM NotaFiscal_Old;

-- Exclui a tabela antiga
DROP TABLE NotaFiscal_Old;
