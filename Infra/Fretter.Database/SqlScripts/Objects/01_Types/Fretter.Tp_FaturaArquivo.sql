/******** REFERENCES ******************/
-- Stored Procedure: SetFaturaArquivo
/*************************************/
IF TYPE_ID('Fretter.Tp_FaturaArquivo') IS NULL
BEGIN
    CREATE TYPE [Fretter].[Tp_FaturaArquivo] AS TABLE
    (
		FaturaArquivoId   INT NULL,
		EmpresaId         INT NULL,
		NomeArquivo       VARCHAR(256) NULL,
		UrlBlobStorage    VARCHAR(256) NULL,
		QtdeRegistros     INT NULL,
		QtdeCriticas      INT NULL,
		ValorTotal        DECIMAL(16,2) NULL,
		TransportadorCnpj VARCHAR(14) NULL,
		Faturado          BIT NULL,
		UsuarioCadastro   INT NULL
    )
END