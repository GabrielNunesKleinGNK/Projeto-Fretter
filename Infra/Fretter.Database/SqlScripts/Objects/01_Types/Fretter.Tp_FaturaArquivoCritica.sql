/******** REFERENCES ******************/
-- Stored Procedure: SetFaturaArquivo
/*************************************/
IF TYPE_ID('Fretter.Tp_FaturaArquivoCritica') IS NULL
BEGIN
    CREATE TYPE [Fretter].[Tp_FaturaArquivoCritica] AS TABLE
    (
		FaturaArquivoCriticaId   INT NULL,
		FaturaArquivoId          INT NULL,
		Linha                    INT NULL,
		Posicao                  INT NULL,
		Descricao                VARCHAR(128) NULL,
		UsuarioCadastro          INT NULL
    )
END