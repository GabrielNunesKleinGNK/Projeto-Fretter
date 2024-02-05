If(TYPE_ID('Fretter.Tp_ConciliacaoRecotacaoRetorno') Is Null)
	CREATE TYPE Fretter.Tp_ConciliacaoRecotacaoRetorno AS TABLE
	(
		ConciliacaoRecotacaoId		Bigint NULL
		,ConciliacaoId				Bigint NULL
		,Protocolo					Varchar(64) NULL
		,JsonValoresRecotacao		Varchar(Max) NULL
		,JsonRetornoRecotacao		Varchar(Max) NULL
		,Sucesso					Bit Null
		,ValorCustoFrete			Decimal(16,4) NULL
		,ValorCustoAdicional		Decimal(16,4) NULL
		,TabelaId                   INT NULL
	)