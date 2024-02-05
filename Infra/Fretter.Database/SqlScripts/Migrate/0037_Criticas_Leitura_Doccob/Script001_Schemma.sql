IF OBJECT_ID('Fretter.FaturaArquivo') IS NULL
    CREATE TABLE Fretter.FaturaArquivo
    (
	    FaturaArquivoId   INT PRIMARY KEY IDENTITY (1,1),
	    EmpresaId       INT,
	    NomeArquivo     VARCHAR(256),
	    UrlBlobStorage  VARCHAR(256),
	    QtdeRegistros   INT,
	    QtdeCriticas    INT,
	    ValorTotal      DECIMAL(16,2),
	    TransportadorId INT,
	    Faturado        BIT,

	    --Campos padrões
        DataCadastro      DATETIME CONSTRAINT DF_FaturaArquivo_DataCadastro DEFAULT(GETDATE()),
        UsuarioCadastro   INTEGER,
        DataAlteracao     DATETIME,
        UsuarioAlteracao  INTEGER,
        Ativo             BIT CONSTRAINT DF_FaturaArquivo_Ativo DEFAULT(1)
    )
GO

IF OBJECT_ID('Fretter.FaturaArquivoCritica') IS NULL
    CREATE TABLE Fretter.FaturaArquivoCritica
    (
	    FaturaArquivoCriticaId   INT PRIMARY KEY IDENTITY (1,1),
        FaturaArquivoId          INT CONSTRAINT FK_FaturaArquivoCritica_FaturaArquivo REFERENCES Fretter.FaturaArquivo (FaturaArquivoId),
        Linha                    INT,
        Posicao                  INT,
        Descricao                VARCHAR(128),

	    --Campos padrões
        DataCadastro       DATETIME CONSTRAINT DF_FaturaArquivoCritica_DataCadastro DEFAULT(GETDATE()),
        UsuarioCadastro    INTEGER,
        DataAlteracao      DATETIME,
        UsuarioAlteracao   INTEGER,
        Ativo              BIT CONSTRAINT DF_FaturaArquivoCritica_Ativo DEFAULT(1)
    )
GO

IF COL_LENGTH('Fretter.Fatura', 'FaturaArquivoId') IS NULL
	ALTER TABLE
		Fretter.Fatura 
	ADD
		FaturaArquivoId INT 
	CONSTRAINT
		FK_Fatura_FaturaArquivo
	REFERENCES
        Fretter.FaturaArquivo (FaturaArquivoId)
GO