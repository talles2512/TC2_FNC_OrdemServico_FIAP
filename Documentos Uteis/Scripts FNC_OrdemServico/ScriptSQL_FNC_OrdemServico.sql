USE master;
CREATE DATABASE OrdemFunctions;
USE OrdemFunctions;

CREATE TABLE [dbo].[Ordem]
(
    [Id] uniqueidentifier NOT NULL PRIMARY KEY,
    [NomeCliente] VARCHAR(255) NOT NULL,
    [Endereco] VARCHAR(255) NOT NULL,
    [NumeroTelefone] VARCHAR(13) NOT NULL,
    [Email] VARCHAR(255) NOT NULL,
    [TipoProduto] VARCHAR(50) NOT NULL,
    [MarcaProduto] VARCHAR(50) NOT NULL,
    [ModeloEquipamento] VARCHAR(50) NOT NULL,
    [NumeroSerie] VARCHAR(100) NOT NULL,
    [TipoDefeito] VARCHAR(50) NOT NULL,
    [DescricaoProblema] VARCHAR(100) NOT NULL,
    [DataAquisicao] DATETIME2,
    [DataCriacao] DATETIME2 NOT NULL DEFAULT(GETDATE())
);

CREATE TABLE [dbo].[ProcessamentoOrdem]
(
    [Id] uniqueidentifier NOT NULL PRIMARY KEY,
    [OrdemId] uniqueidentifier NOT NULL FOREIGN KEY REFERENCES Ordem(Id),
    [StatusOrdem] VARCHAR(20) NOT NULL,
    [EstaNaGarantia] BIT NOT NULL,
    [PrazoConclusaoDiasUteis] INT NOT NULL,
    [DataCriacao] DATETIME2 NOT NULL DEFAULT(GETDATE()),
    [DataAtualizacao] DATETIME2 NOT NULL,
    [MotivoRecusa] VARCHAR(100),
    [Ativo] BIT NOT NULL
);