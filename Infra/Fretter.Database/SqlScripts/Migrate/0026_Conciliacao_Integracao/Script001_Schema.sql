If Object_Id('Fretter.FaturaConciliacao') Is NULL
Begin
	Create Table Fretter.FaturaConciliacao
	(
		FaturaConciliacaoId			Bigint Identity(1,1) Constraint PK_FaturaConciliacao_ConciliacaoId Primary Key 
		,FaturaId					Int Constraint FK_FaturaConciliacao_FaturaId References Fretter.Fatura(FaturaId)
		,ConciliacaoId				Bigint Constraint FK_FaturaConciliacao_ConciliacaoId References Fretter.Conciliacao(ConciliacaoId)
		,Cnpj						Varchar(32)
		,NotaFiscal					Varchar(16)
		,Serie						Varchar(16)
		,DataEmissao				Datetime
		,Observacao					Varchar(256)
		,ValorFrete					Decimal(10,4)
		,ValorAdicional				Decimal(10,4)
		,DataCadastro				Datetime Constraint DF_FaturaConciliacao_DataCadastro Default (Getdate())
		,UsuarioAlteracao			Int
		,DataAlteracao				Datetime
		,Ativo						Bit Constraint DF_FaturaConciliacao_Ativo Default(1)
	)
End
Go
Drop Procedure If Exists [Fretter].[GetEntregaConciliacao]	
Drop Type If Exists [Fretter].[Tp_FiltroEntregaConciliacao]
Go
If COL_LENGTH('Fretter.Fatura', 'ValorDocumento') IS NULL
	Alter Table Fretter.Fatura Add ValorDocumento Decimal(16,2) Default(0)
Go
If COL_LENGTH('Fretter.ImportacaoCte', 'CFOP') IS NULL
	Alter Table Fretter.ImportacaoCte Add CFOP Varchar(8)
Go
If COL_LENGTH('Fretter.ImportacaoCte', 'VersaoProcesso') IS NULL
	Alter Table Fretter.ImportacaoCte Add VersaoProcesso Varchar(36)
Go
If COL_LENGTH('Fretter.ImportacaoArquivo', 'ImportacaoConfiguracaoId') IS NULL
	Alter Table Fretter.ImportacaoArquivo Add ImportacaoConfiguracaoId Int
