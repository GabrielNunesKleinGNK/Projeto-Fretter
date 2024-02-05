If OBJECT_ID('Fretter.ConciliacaoRecotacao') Is NULL
	Create Table Fretter.ConciliacaoRecotacao
	(
		ConciliacaoRecotacaoId		Bigint Identity(1,1) Constraint PK_Fretter_ConciliacaoRecotacao_Id Primary Key
		,ConciliacaoId				Bigint Constraint FK_Fretter_ConciliacaoRecotacao_Conciliacao References Fretter.Conciliacao(ConciliacaoId)
		,ValorCustoFrete			Decimal(9,2)
		,ValorCustoAdicional		Decimal(9,2)
		,ValorCustoReal				Decimal(9,2)
		,Protocolo					Varchar(64)
		,JsonValoresRecotacao		Varchar(Max)
		,JsonValoresCte				Varchar(Max)
		,JsonRetornoRecotacao		Varchar(Max)
		,Processado					Bit Constraint FK_Fretter_ConciliacaoRecotacao_Processado Default(0)
		,Sucesso					Bit Constraint FK_Fretter_ConciliacaoRecotacao_Sucesso Default(0)
		,DataCadastro				Datetime Constraint FK_Fretter_ConciliacaoRecotacao_DataCadastro Default(Getdate())
		,DataProcessamento			Datetime 
		,Ativo						Bit Constraint FK_Fretter_ConciliacaoRecotacao_Ativo Default(1)
	)
Go
IF COL_LENGTH('Fretter.ContratoTransportador', 'RecotaPesoTransportador') IS NULL
BEGIN
	Alter Table Fretter.ContratoTransportador Add RecotaPesoTransportador Bit Constraint DF_Fretter_ContratoTransportador_RecotaTransportador Default(0)
END
Go