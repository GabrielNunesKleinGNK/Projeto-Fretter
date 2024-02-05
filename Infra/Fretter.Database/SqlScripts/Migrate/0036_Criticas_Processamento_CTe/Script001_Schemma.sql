IF OBJECT_ID('Fretter.ImportacaoArquivoCritica') IS NULL
    CREATE TABLE Fretter.ImportacaoArquivoCritica
    (
	    ImportacaoArquivoCriticaId INT PRIMARY KEY IDENTITY (1,1),
        ImportacaoArquivoId INT CONSTRAINT FK_ImportacaoArquivoCritica_ImportacaoArquivo REFERENCES Fretter.ImportacaoArquivo (ImportacaoArquivoId),
        Descricao VARCHAR(128),
		Linha INTEGER,
		Campo VARCHAR(64),

	    --Campos padrões
        DataCadastro DATETIME CONSTRAINT DF_ImportacaoArquivoCritica_DataCadastro DEFAULT(GETDATE()),
        UsuarioCadastro INTEGER,
        DataAlteracao DATETIME,
        UsuarioAlteracao INTEGER,
        Ativo BIT CONSTRAINT DF_ImportacaoArquivoCritica_Ativo DEFAULT(1)
    )
GO
