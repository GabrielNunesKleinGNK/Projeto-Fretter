IF OBJECT_ID('Fretter.ConciliacaoTransacao') IS NULL
BEGIN
	CREATE TABLE Fretter.ConciliacaoTransacao
	(
		ConciliacaoTransacaoId INT IDENTITY(1,1) CONSTRAINT PK_Fretter_ConciliacaoTransacao_Id PRIMARY KEY,
		Descricao VARCHAR(64),
		ParametroJson VARCHAR(MAX),
		Quantidade INT,
		UsuarioCadastro INT,
		DataCadastro DATETIME CONSTRAINT DF_ConciliacaoTransacao_DataCadastro DEFAULT(GETDATE()),
		Ativo BIT CONSTRAINT DF_ConciliacaoTransacao_Ativo DEFAULT (1)
	)
END

IF OBJECT_ID('Fretter.ConciliacaoHistorico') IS NOT NULL
BEGIN
	DROP TABLE Fretter.ConciliacaoHistorico
END