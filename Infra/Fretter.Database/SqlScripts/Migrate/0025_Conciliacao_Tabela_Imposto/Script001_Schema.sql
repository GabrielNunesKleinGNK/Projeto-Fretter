IF COL_LENGTH('Fretter.ContratoTransportador', 'ToleranciaSuperior') IS Not NULL
BEGIN	
	Declare @Command   Nvarchar(1000)

	SELECT Top 1 @Command = 'ALTER TABLE Fretter.ContratoTransportador Drop CONSTRAINT ' +sa.name + ' ;'
	FROM sys.all_columns				co
	INNER JOIN sys.tables				ta ON co.object_id = ta.object_id
	INNER JOIN sys.schemas				sc ON ta.schema_id = sc.schema_id
	INNER JOIN sys.default_constraints	sa ON co.default_object_id = sa.object_id
	WHERE sc.name = 'Fretter'
	    AND ta.name = 'ContratoTransportador'
	    AND co.name = 'ToleranciaSuperior'

	Execute(@Command)	
	Alter Table Fretter.ContratoTransportador Alter Column ToleranciaSuperior Decimal(6,2)
	Alter Table Fretter.ContratoTransportador Add Constraint DF_ContratoTransportador_ToleranciaSuperior Default(0) For ToleranciaSuperior
	Alter Table Fretter.ContratoTransportadorHistorico Alter Column ToleranciaSuperior Decimal(6,2) 
END
Go

IF COL_LENGTH('Fretter.ContratoTransportador', 'ToleranciaInferior') IS Not NULL
BEGIN	
	Declare @Command   Nvarchar(1000)

	SELECT Top 1 @Command = 'ALTER TABLE Fretter.ContratoTransportador Drop CONSTRAINT ' +sa.name + ' ;'
	FROM sys.all_columns				co
	INNER JOIN sys.tables				ta ON co.object_id = ta.object_id
	INNER JOIN sys.schemas				sc ON ta.schema_id = sc.schema_id
	INNER JOIN sys.default_constraints	sa ON co.default_object_id = sa.object_id
	WHERE sc.name = 'Fretter'
	    AND ta.name = 'ContratoTransportador'
	    AND co.name = 'ToleranciaInferior'

	Execute(@Command)	
	Alter Table Fretter.ContratoTransportador Alter Column ToleranciaInferior Decimal(6,2)
	Alter Table Fretter.ContratoTransportador Add Constraint Df_ContratoTransportador_ToleranciaInferior Default(0) For ToleranciaInferior
	Alter Table Fretter.ContratoTransportadorHistorico Alter Column ToleranciaInferior Decimal(6,2)
END
Go
If OBJECT_ID('Fretter.ImportacaoCteImposto') Is NULL
	Create Table Fretter.ImportacaoCteImposto
	(
		ImportacaoCteImpostoId		Int Identity(1,1) Constraint PK_Fretter_ImportacaoCteImposto Primary Key
		,ImportacaoCteId			Int Constraint FK_Fretter_ImportacaoCteImposto_CteId FOREIGN KEY(ImportacaoCteId) REFERENCES Fretter.ImportacaoCte(ImportacaoCteId)
		,Classificacao				Varchar(32)
		,ValorBaseCalculo			Decimal(20,4)
		,Aliquota					Decimal(12,4)
		,Valor						Decimal(12,4)	
	)
Go

If Col_length('Fretter.ImportacaoCte', 'Modal') IS NULL
Begin
	Alter Table Fretter.ImportacaoCte Add Modal Varchar(4) NULL --modal
	Alter Table Fretter.ImportacaoCte Add CodigoMunicipioEnvio Int NULL--cMunEnv
	Alter Table Fretter.ImportacaoCte Add MunicipioEnvio Varchar(64) NULL --xMunEnv
	Alter Table Fretter.ImportacaoCte Add UFEnvio Varchar(2) NULL --UFEnv
	Alter Table Fretter.ImportacaoCte Add CodigoMunicipioInicio Int NULL --cMunIni
	Alter Table Fretter.ImportacaoCte Add MunicipioInicio Varchar(64) NULL --xMunIni
	Alter Table Fretter.ImportacaoCte Add UFInicio Varchar(2) NULL --UFIni
	Alter Table Fretter.ImportacaoCte Add CodigoMunicipioFim Int NULL --cMunFim
	Alter Table Fretter.ImportacaoCte Add MunicipioFim Varchar(64) NULL --xMunFim
	Alter Table Fretter.ImportacaoCte Add UFFim Varchar(2) NULL --UFFim
	Alter Table Fretter.ImportacaoCte Add IETomadorIndicador Smallint NULL --indIEToma
	Alter Table Fretter.ImportacaoCte Add ValorTributo	Decimal(12,4) Default(0)	
	Alter Table Fretter.ImportacaoCte Alter Column DataEmissao Datetime NULL
	Alter Table Fretter.ImportacaoCteNotaFiscal Add DataPrevista Date NULL
End