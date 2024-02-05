/******** REFERENCES ******************/
-- Stored Procedure: ProcessaFaturaManual
/*************************************/
If(TYPE_ID('Fretter.Tp_EntregaConciliacaoFatura') Is Null)
CREATE TYPE [Fretter].[Tp_EntregaConciliacaoFatura] AS TABLE(
	[ConciliacaoId] [int],
    [Selecionado] bit
)

