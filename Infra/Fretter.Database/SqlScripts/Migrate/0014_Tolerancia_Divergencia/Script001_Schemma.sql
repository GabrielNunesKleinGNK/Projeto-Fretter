IF (EXISTS (SELECT Top 1 1  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Fretter' AND  TABLE_NAME = 'ContratoTransportador'))
BEGIN
	If Object_Id('Fretter.ToleranciaTipo') Is NULL
	Begin
		Create Table Fretter.ToleranciaTipo
		(
			 ToleranciaTipoId	Int identity Primary Key
			,Descricao			Varchar(32)
			,Ativo				Bit Default(1) Not Null
		)
	End
End
Go
IF (EXISTS (SELECT Top 1 1  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Fretter' AND  TABLE_NAME = 'ContratoTransportador'))
BEGIN
	If Object_Id('Fretter.ContratoTransportadorHistorico') Is NULL
	Begin
		Create Table Fretter.ContratoTransportadorHistorico
		(
			 ContratoTransportadorHistoricoId	Int Identity Primary Key
			,ContratoTransportadorId			Int References Fretter.ContratoTransportador(ContratoTransportadorId)
			,TransportadorId					Int
			,TransportadorCnpjId				Int
			,TransportadorCnpjCobrancaId		Int
			,EmpresaId							Int
			,Descricao							Varchar(256)
			,QuantidadeTentativas				Int
			,TaxaTentativaAdicional				Decimal(9,2)
			,TaxaRetornoRemetente				Decimal(9,2)
			,VigenciaInicial					DateTime
			,VigenciaFinal						DateTime
			,FaturaCicloId						Int References Fretter.FaturaCiclo(FaturaCicloId)
			,PermiteTolerancia					Bit
			,ToleranciaSuperior					Int
			,ToleranciaInferior					Int
			,ToleranciaTipoId					Int References Fretter.ToleranciaTipo(ToleranciaTipoId)
			,MicroServicoId						Int References dbo.Tb_Adm_MicroServico(Cd_Id)
			,UsuarioCadastro					Int
			,DataCadastro						Datetime
			,Ativo								Bit Default(1) Not Null
		)
	End
End
Go
IF (EXISTS (SELECT Top 1 1  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Fretter' AND  TABLE_NAME = 'ContratoTransportador'))
BEGIN
	IF COL_LENGTH('Fretter.ContratoTransportador', 'MicroServicoId') IS NULL
	BEGIN
		Alter Table Fretter.ContratoTransportador Add MicroServicoId Int References dbo.Tb_Adm_MicroServico(Cd_Id)
	END
End
Go
IF (EXISTS (SELECT Top 1 1  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Fretter' AND  TABLE_NAME = 'ContratoTransportador'))
BEGIN
	IF COL_LENGTH('Fretter.ContratoTransportador', 'PermiteTolerancia') IS NULL
	BEGIN
		ALTER TABLE Fretter.ContratoTransportador  ADD PermiteTolerancia Bit Default(0) Not Null
	END
End
Go
IF (EXISTS (SELECT Top 1 1  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Fretter' AND  TABLE_NAME = 'ContratoTransportador'))
BEGIN
	IF COL_LENGTH('Fretter.ContratoTransportador', 'ToleranciaSuperior') IS NULL
	BEGIN
		ALTER TABLE Fretter.ContratoTransportador  ADD ToleranciaSuperior Int Default(0) Not Null
	END
End
Go
IF (EXISTS (SELECT Top 1 1  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Fretter' AND  TABLE_NAME = 'ContratoTransportador'))
BEGIN
	IF COL_LENGTH('Fretter.ContratoTransportador', 'ToleranciaInferior') IS NULL
	BEGIN
		ALTER TABLE Fretter.ContratoTransportador  ADD ToleranciaInferior Int Default(0) Not Null
	END
End
Go
IF (EXISTS (SELECT Top 1 1  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Fretter' AND  TABLE_NAME = 'ContratoTransportador'))
BEGIN
	IF COL_LENGTH('Fretter.ContratoTransportador', 'ToleranciaTipoId') IS NULL
	BEGIN
		ALTER TABLE Fretter.ContratoTransportador  ADD ToleranciaTipoId Int References Fretter.ToleranciaTipo(ToleranciaTipoId)
	END
End
Go
IF (EXISTS (SELECT Top 1 1  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Fretter' AND  TABLE_NAME = 'ContratoTransportador'))
BEGIN
	IF COL_LENGTH('Fretter.ContratoTransportador', 'MicroServicoId') IS NULL
	BEGIN
		Alter Table Fretter.ContratoTransportador Add MicroServicoId Int References dbo.Tb_Adm_MicroServico(Cd_Id)
	END
End
Go
IF (EXISTS (SELECT Top 1 1  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Fretter' AND  TABLE_NAME = 'ContratoTransportadorHistorico'))
BEGIN
	IF COL_LENGTH('Fretter.ContratoTransportadorHistorico', 'MicroServicoId') IS NULL
	BEGIN
		Alter Table Fretter.ContratoTransportadorHistorico Add MicroServicoId Int References dbo.Tb_Adm_MicroServico(Cd_Id)
	END
End
Go
IF (EXISTS (SELECT Top 1 1  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Fretter' AND  TABLE_NAME = 'Conciliacao'))
BEGIN
	IF COL_LENGTH('Fretter.Conciliacao', 'JsonValoresRecotacao') IS NULL
	BEGIN
		Alter Table Fretter.Conciliacao Add JsonValoresRecotacao Varchar(MAX)
	END
End
Go
IF (EXISTS (SELECT Top 1 1  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Fretter' AND  TABLE_NAME = 'Conciliacao'))
BEGIN
	IF COL_LENGTH('Fretter.Conciliacao', 'JsonValoresCte') IS NULL
	BEGIN
		Alter Table Fretter.Conciliacao Add JsonValoresCte Varchar(MAX)
	END
End
Go
IF (EXISTS (SELECT Top 1 1  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Fretter' AND  TABLE_NAME = 'Conciliacao'))
BEGIN
	IF COL_LENGTH('Fretter.Conciliacao', 'ImportacaoCteId') IS NULL
	BEGIN
		Alter Table Fretter.Conciliacao Add ImportacaoCteId Int References Fretter.ImportacaoCte(ImportacaoCteId)
	END
End
Go
