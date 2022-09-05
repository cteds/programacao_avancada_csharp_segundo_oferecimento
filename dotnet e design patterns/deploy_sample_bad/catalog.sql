-- Cria o banco de dados
CREATE DATABASE [Catalog];
GO

-- Define qual banco de dados será utilizado
USE [Catalog];
GO

-- Cria a tabela Products
CREATE TABLE Products(
	IdProduct		    VARCHAR(255) NOT NULL UNIQUE,
	[Name]			    VARCHAR(255) NOT NULL,
	[Description]		TEXT NOT NULL,
	Price			    DECIMAL(18,2) NOT NULL
);
GO

-- Lista os dados da tabela
SELECT * FROM Products;
GO

-- Insere um registro na tabela
INSERT INTO Products (IdProduct, [Name], [Description], Price)
VALUES				 ('SM-G781BLVJZTO', 'Galaxy S20 FE 5G Violeta', 'Tons para deixar até o arco-íris com inveja. Todos os olhos voltados para a tela Infinity-O. A câmera de lente tripla de nível profissional.', 1299.99);
GO

-- Deleta um registro que coincida com o IdProduct informado
DELETE FROM Products WHERE IdProduct = 'SM-G781BLVJZTO';

CREATE TABLE Users(
	IdUser			    VARCHAR(255) NOT NULL UNIQUE,
	[Name]			    VARCHAR(255) NOT NULL,
	Email				VARCHAR(255) NOT NULL UNIQUE,
	[Password]		    VARCHAR(255) NOT NULL UNIQUE
);
GO

SELECT * FROM Users;
GO

INSERT INTO Users (IdUser, [Name], Email, [Password])
VALUES			  ('a532cb85-4d50-4656-9ea6-afced4a585da',
				   'User fake', 
				   'user.fake@email.com',
				   '123456');
GO

DELETE FROM Users;