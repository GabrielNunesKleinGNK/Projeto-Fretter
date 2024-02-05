IF OBJECT_ID('Fretter.ConciliacaoReenvio') IS NULL
	CREATE TABLE [Fretter].[ConciliacaoReenvio]
	(
		ConciliacaoReenvioId	INT PRIMARY KEY IDENTITY(1,1),
		FaturaConciliacaoId		BIGINT,
		FaturaId				INT,
		ConciliacaoId			BIGINT,
		UsuarioCadastro         INT,
		DataCadastro			DATETIME,
	)
GO

IF OBJECT_ID('Fretter.ConciliacaoReenvioHistorico') IS NULL
	CREATE TABLE [Fretter].[ConciliacaoReenvioHistorico]
	(
		ConciliacaoReenvioHistoricoId	BIGINT PRIMARY KEY IDENTITY(1,1),
		ConciliacaoReenvioId			BIGINT,
		FaturaConciliacaoId				BIGINT,
		FaturaId						INT,
		ConciliacaoId			        BIGINT,
		UsuarioCadastro					INT,
		DataCadastro					DATETIME,
		DataReprocessamento				DATETIME
	)
GO