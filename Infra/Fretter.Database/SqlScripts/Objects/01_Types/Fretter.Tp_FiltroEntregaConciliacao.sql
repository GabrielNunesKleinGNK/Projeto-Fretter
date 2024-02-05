/******** REFERENCES ******************/
-- Stored Procedure: GetEntregaConciliacao
/*************************************/
If(TYPE_ID('Fretter.Tp_FiltroEntregaConciliacao') Is Null)
CREATE TYPE [Fretter].[Tp_FiltroEntregaConciliacao] AS TABLE(
    [Id]					[Int] Null,
	[CNPJ]					[Varchar](20) Null,
    [CNPJFilial]			[Varchar](20) Null,
    [Serie]					[Varchar](50) Null,
    [NotaFiscal]			[Varchar](50) Null,
	[ValorFrete]			[Decimal](16,4) Null,
    [DataEmissao]			[Datetime2] NULL,
	[ConhecimentoNumero] 	[Varchar](32) Null,
	[ConhecimentoSerie] 	[Varchar](32) Null
)