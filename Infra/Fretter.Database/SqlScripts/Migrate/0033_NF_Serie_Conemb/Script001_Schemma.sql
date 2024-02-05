IF COL_LENGTH('Fretter.ImportacaoCteNotaFiscal', 'CNPJEmissorNF') IS NULL
BEGIN
	Alter Table Fretter.ImportacaoCteNotaFiscal Add CNPJEmissorNF varchar(14) NULL
END
Go

IF COL_LENGTH('Fretter.ImportacaoCteNotaFiscal', 'NumeroNF') IS NULL
BEGIN
	Alter Table Fretter.ImportacaoCteNotaFiscal Add NumeroNF varchar(9) NULL
END
Go

IF COL_LENGTH('Fretter.ImportacaoCteNotaFiscal', 'SerieNF') IS NULL
BEGIN
	Alter Table Fretter.ImportacaoCteNotaFiscal Add SerieNF varchar(3) NULL
END
Go

IF COL_LENGTH('Fretter.ImportacaoCteNotaFiscal', 'DataEmissaoNF') IS NULL
BEGIN
	Alter Table Fretter.ImportacaoCteNotaFiscal Add DataEmissaoNF datetime NULL
END
Go