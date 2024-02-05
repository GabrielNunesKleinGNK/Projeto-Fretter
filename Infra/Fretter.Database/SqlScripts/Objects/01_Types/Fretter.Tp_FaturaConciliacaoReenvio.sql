/******** REFERENCES ******************/
-- Stored Procedure: Fretter.SetFaturaConciliacaoReenvio
/*************************************/
If(TYPE_ID('Fretter.Tp_FaturaConciliacaoReenvio') Is Null)
	CREATE TYPE [Fretter].[Tp_FaturaConciliacaoReenvio] AS TABLE(
		[FaturaConciliacaoId]  [bigint] NULL,
		[FaturaId]             [int] NULL,
		[ConciliacaoId]        [bigint] NULL,
		[UsuarioCadastro]      [int] NULL
	)
GO