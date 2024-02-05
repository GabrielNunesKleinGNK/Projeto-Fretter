/******** REFERENCES ******************/
-- Stored Procedure: ProcessaFaturaImportacao
/*************************************/
If(TYPE_ID('Fretter.Tp_FaturaConciliacao') Is Null)
	CREATE TYPE Fretter.Tp_FaturaConciliacao AS TABLE
	(
		ConciliacaoId		Bigint		NULL,
		TransportadorId		Int			NULL,
		NotaFiscal			Varchar(16) NULL,
		Serie				Varchar(16) NULL,
		DataEmissao			Datetime2	NULL,
		Observacao			Varchar(256) NULL,
		ValorFrete			Decimal(16,4) NULL,
		ValorAdicional		Decimal(16,4) NULL,
		Selecionado			Bit NULL
	)

