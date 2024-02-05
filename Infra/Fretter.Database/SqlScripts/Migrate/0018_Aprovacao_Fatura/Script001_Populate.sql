If  Object_Id('Fretter.FaturaAcao') Is NULL
	Begin
		Create Table Fretter.FaturaAcao
		(
			FaturaAcaoId		Int Identity(1,1) Constraint PK_Fretter_FaturaAcaoId Primary Key,
			Descricao			Varchar(128),
			Ativo				Bit Constraint DF_Fretter_FaturaAcao_Ativo Default(1)
		)
	End
Go
If  Object_Id('Fretter.FaturaStatusAcao') Is NULL
	Begin
		Create Table Fretter.FaturaStatusAcao
		(
			FaturaStatusAcaoId			Int Identity(1,1) Constraint PK_Fretter_FaturaStatusAcaoId Primary Key,
			FaturaStatusId				Int Constraint FK_Fretter_FaturaStatus References Fretter.FaturaStatus(FaturaStatusId),
			FaturaAcaoId				Int Constraint FK_Fretter_FaturaAcao References Fretter.FaturaAcao(FaturaAcaoId),
			FaturaStatusResultadoId		Int Constraint FK_Fretter_FaturaStatusResultado References Fretter.FaturaStatus(FaturaStatusId),
			Visivel						Bit,
			InformaMotivo				Bit,
			Ativo						Bit Constraint DF_Fretter_FaturaStatusAcao_Ativo Default(1)
		)
	End
Go

If Col_length('Fretter.FaturaHistorico', 'FaturaStatusIdAnterior') IS NULL
	Begin 
		Alter Table Fretter.FaturaHistorico
		Add FaturaStatusIdAnterior Int Constraint FK_Fretter_FaturaStatusAnterior References Fretter.FaturaStatus(FaturaStatusId)
	End