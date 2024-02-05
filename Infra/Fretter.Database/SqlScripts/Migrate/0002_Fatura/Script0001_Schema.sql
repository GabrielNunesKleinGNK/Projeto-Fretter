If Object_Id('Fretter.FaturaCicloTipo') Is NULL
Begin
	Create Table Fretter.FaturaCicloTipo
	(
		FaturaCicloTipoId	Int Identity(1,1) Constraint PK_Fretter_FaturaCicloTipoId Primary Key
		,Descricao			Varchar(128)
		,Ativo				Bit Constraint DF_Fretter_FaturaCicloTipo_Ativo Default(1)
	)
End
Go
If Not Exists(Select Top 1 1 From Fretter.FaturaCicloTipo)
Begin
	Insert Into Fretter.FaturaCicloTipo
	(
		Descricao
		,Ativo
	)
	Values
	(
		'Mensal'
		,1
	),
	(
		'Quinzenal'
		,1
	),
	(
		'Semanal'
		,1
	)

End
Go
If Object_Id('Fretter.FaturaCiclo') Is NULL
Begin
	Create Table Fretter.FaturaCiclo
	(
		FaturaCicloId		Int Identity(1,1) Constraint PK_Fretter_FaturaCicloId Primary Key
		,FaturaCicloTipoId	Int Constraint FK_Fretter_FaturaCiclo_Tipo References Fretter.FaturaCicloTipo(FaturaCicloTipoId)
		,DiaFechamento		Smallint
		,DiaVencimento		Smallint
		,Ativo				Bit Constraint DF_Fretter_FaturaCiclo_Ativo Default(1)
	)
End
Go
If Not Exists(Select Top 1 1 From Fretter.FaturaCiclo)
Begin
	Insert Into Fretter.FaturaCiclo
	(
		FaturaCicloTipoId	
		,DiaFechamento		
		,DiaVencimento
	)
	Select 
		FaturaCicloTipoId	= 1 --Mensal
		,DiaFechamento		= 5
		,DiaVencimento		= 20
End
Go
If Object_Id('Fretter.FaturaPeriodo') Is NULL
Begin
	Create Table Fretter.FaturaPeriodo
	(
		FaturaPeriodoId			Int Identity(1,1) Constraint PK_Fretter_FaturaPeriodoId Primary Key
		,FaturaCicloId			Int Constraint FK_Fretter_FaturaPeriodo_FaturaCiclo References Fretter.FaturaCiclo(FaturaCicloId)
		,DiaVencimento			SmallInt
		,DataInicio				Date
		,DataFim				Date
		,Processado				Bit Constraint DF_Fretter_FaturaPeriodo_Processado Default(0)
		,DataProcessamento		Datetime
		,QuantidadeProcessado	Int
		,Vigente				Bit Constraint DF_Fretter_FaturaPeriodo_Vigente Default(1)
		,Ativo					Bit Constraint DF_Fretter_FaturaPeriodo_Ativo Default(1)
	)
End
Go
If Object_Id('Fretter.FaturaStatus') Is NULL
Begin
	Create Table Fretter.FaturaStatus
	(
		FaturaStatusId		Int Identity(1,1) Constraint PK_Fretter_FaturaStatusId Primary Key
		,Decricao			Varchar(128)
		,Ativo				Bit Constraint DF_Fretter_FaturaStatus_Ativo Default(1)
	)
End
Go
If Not Exists(Select Top 1 1 From Fretter.FaturaStatus)
Begin
	Insert Into Fretter.FaturaStatus(Descricao)Values('Pendente'),('Em Processamento'),('Em Aberto'),('Liquidado'),('Cancelado')
End
Go
If Object_Id('Fretter.Fatura') Is NULL
Begin
	Create Table Fretter.Fatura
	(
		FaturaId						Int Identity(1,1) Constraint PK_Fretter_FaturaId Primary Key
		,EmpresaId						Int 
		,TransportadorId				Int
		,FaturaPeriodoId				Int Constraint FK_Fretter_Fatura_FaturaPeriodo References Fretter.FaturaPeriodo(FaturaPeriodoId)
		,ValorCustoFrete				Decimal(16,2)
		,ValorCustoAdicional			Decimal(16,2)
		,ValorCustoReal					Decimal(16,2)
		,QuantidadeDevolvidoRemetente	Int
		,QuantidadeEntrega				Int
		,QuantidadeSucesso				Int 
		,FaturaStatusId					Int Constraint FK_Fretter_Fatura_FaturaStatus References Fretter.FaturaStatus(FaturaStatusId)
		,DataVencimento					Date
		,UsuarioCadastro				Int
		,UsuarioAlteracao				Int
		,DataCadastro					Datetime Constraint DF_Fretter_Fatura_DataCadastro Default(Getdate())
		,DataAlteracao					Datetime 
		,Ativo							Bit Constraint DF_Fretter_Fatura_Ativo Default(1)	
	)
End
Go
If Object_Id('Fretter.FaturaItem') Is NULL
Begin
	Create Table Fretter.FaturaItem
	(
		FaturaItemId					Int Identity(1,1) Constraint PK_Fretter_FaturaItemId Primary Key
		,Valor							Decimal(16,2)
		,Descricao						Varchar(256)
		,UsuarioCadastro				Int
		,UsuarioAlteracao				Int
		,DataCadastro					Datetime Constraint DF_Fretter_FaturaItem_DataCadastro Default(Getdate())
		,DataAlteracao					Datetime 
		,Ativo							Bit Constraint DF_Fretter_FaturaItem_Ativo Default(1)	
	)
End
Go
IF COL_LENGTH('Fretter.ContratoTransportador', 'FaturaCicloId') IS NULL
BEGIN
    ALTER TABLE Fretter.ContratoTransportador  ADD FaturaCicloId INT
END
Go
IF COL_LENGTH('Fretter.Conciliacao', 'FaturaId') IS NULL
BEGIN
    ALTER TABLE Fretter.Conciliacao  ADD FaturaId INT
END
Go
If Object_Id('Fretter.FaturaHistorico') Is NULL
Begin
	Create Table Fretter.FaturaHistorico
	(
		FaturaHistoricoId				Int Identity(1,1) Constraint PK_Fretter_FaturaHistoricoId Primary Key
		,FaturaId						Int Constraint FK_Fretter_FaturaHistorico_Fatura References Fretter.Fatura(FaturaId)
		,FaturaStatusId					Int Constraint FK_Fretter_FaturaHistorico_FaturaStatus References Fretter.FaturaStatus(FaturaStatusId)
		,Descricao						Varchar(256)
		,ValorCustoFrete				Decimal(16,2)
		,ValorCustoAdicional			Decimal(16,2)
		,ValorCustoReal					Decimal(16,2)
		,QuantidadeEntrega				Int 
		,QuantidadeSucesso				Int 
		,UsuarioCadastro				Int
		,UsuarioAlteracao				Int
		,DataCadastro					Datetime Constraint DF_Fretter_FaturaHistorico_DataCadastro Default(Getdate())
		,DataAlteracao					Datetime 
		,Ativo							Bit Constraint DF_Fretter_FaturaHistorico_Ativo Default(1)	
	)
End