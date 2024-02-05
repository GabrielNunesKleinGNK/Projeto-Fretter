IF (COL_LENGTH('Fretter.ConfiguracaoCteTipo','Chave') IS NULL)
	Alter table Fretter.ConfiguracaoCteTipo 
	Add Chave varchar(64)
Go
IF (COL_LENGTH('Fretter.ImportacaoCte','JsonComposicaoValores') IS NULL)
	ALTER TABLE Fretter.ImportacaoCte ADD JsonComposicaoValores VARCHAR(Max) NULL
Go
