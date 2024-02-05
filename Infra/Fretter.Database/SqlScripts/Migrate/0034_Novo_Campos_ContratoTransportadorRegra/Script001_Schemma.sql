IF COL_LENGTH('Fretter.ContratoTransportadorRegra', 'ImportacaoArquivoTipoId') IS NULL
BEGIN
	Alter Table Fretter.ContratoTransportadorRegra
	Add ImportacaoArquivoTipoId Int References  Fretter.ImportacaoArquivoTipo(ImportacaoArquivoTipoId)
END
Go
IF COL_LENGTH('Fretter.ImportacaoArquivo', 'ImportacaoArquivoTipoItemId') IS NULL
BEGIN
	Alter Table Fretter.ImportacaoArquivo
	Add ImportacaoArquivoTipoItemId Int References Fretter.ImportacaoArquivoTipoItem(ImportacaoArquivoTipoItemId)
END
Go
If OBJECT_ID('Fretter.ImportacaoArquivoTipoItem') IS NULL
	Create Table Fretter.ImportacaoArquivoTipoItem
	(
		ImportacaoArquivoTipoItemId		Int Identity(1,1) Constraint PK_Fretter_ImportacaoArquivoTipoItem  Primary Key 
		,ImportacaoArquivoTipoId		Int Not NULL Constraint FK_Fretter_ImportacaoArquivoTipoItem_ImportacaoArquivoTipo References Fretter.ImportacaoArquivoTipo(ImportacaoArquivoTipoId)
		,Descricao						Varchar(64)
		,DataCadastro					Datetime Not NULL Constraint DF_Fretter_ImportacaoArquivoTipoItem_DataCadastro Default(Getdate())
		,UsuarioCadastro				Int 
		,Ativo							Bit Not NULL Constraint DF_Fretter_ImportacaoArquivoTipoItem_Ativo Default(1)
	)
Go

If Not Exists(Select Top 1 1 From Fretter.ImportacaoArquivoTipoItem)
Begin
	SET IDENTITY_INSERT Fretter.ImportacaoArquivoTipoItem ON
	Insert Into Fretter.ImportacaoArquivoTipoItem
	(
		ImportacaoArquivoTipoItemId
		,ImportacaoArquivoTipoId		
		,Descricao						
		,DataCadastro					
		,UsuarioCadastro				
		,Ativo							
	)	
	Select
		ImportacaoArquivoTipoItemId		= 1
		,ImportacaoArquivoTipoId		= 1	
		,Descricao						= 'ENTREGA'
		,DataCadastro					= GETDATE()
		,UsuarioCadastro				= NULL
		,Ativo							= 1
	Union All
	Select
		ImportacaoArquivoTipoItemId		= 2
		,ImportacaoArquivoTipoId		= 1	
		,Descricao						= 'DEVOLUCAO'
		,DataCadastro					= GETDATE()
		,UsuarioCadastro				= NULL
		,Ativo							= 1
	Union All
	Select
		ImportacaoArquivoTipoItemId		= 3
		,ImportacaoArquivoTipoId		= 1	
		,Descricao						= 'REENTREGA'
		,DataCadastro					= GETDATE()
		,UsuarioCadastro				= NULL
		,Ativo							= 1
	Union All
	Select
		ImportacaoArquivoTipoItemId		= 4
		,ImportacaoArquivoTipoId		= 2	
		,Descricao						= 'ENTREGA'
		,DataCadastro					= GETDATE()
		,UsuarioCadastro				= NULL
		,Ativo							= 1
	SET IDENTITY_INSERT Fretter.ImportacaoArquivoTipoItem OFF
End
Go

If OBJECT_ID('Fretter.ContratoTransportadorArquivoTipo') IS NULL
	Create Table Fretter.ContratoTransportadorArquivoTipo
	(
		 ContratoTransportadorArquivoTipoId	Int Identity(1,1) Constraint PK_Fretter_ContratoTransportadorArquivoTipo  Primary Key 
		,EmpresaId							Int Not NULL Constraint FK_Fretter_ContratoTransportadorArquivoTipo_EmpresaId References dbo.Tb_Adm_Empresa(Cd_Id)
		,TransportadorId					Int Not NULL Constraint FK_Fretter_ContratoTransportadorArquivoTipo_TransportadorId References dbo.Tb_Adm_Transportador(Cd_Id)
		,ImportacaoArquivoTipoItemId		Int Not NULL Constraint FK_Fretter_ContratoTransportadorArquivoTipo_ImportacaoArquivoTipoItemId References Fretter.ImportacaoArquivoTipoItem(ImportacaoArquivoTipoItemId)
		,Alias								Varchar(64)
		,DataCadastro						Datetime Not NULL Constraint DF_Fretter_ContratoTransportadorArquivoTipo_DataCadastro Default(Getdate())
		,UsuarioCadastro					Int 
		,DataAlteracao						Datetime
		,UsuarioAlteracao					Int
		,Ativo								Bit Not NULL Constraint DF_Fretter_ContratoTransportadorArquivoTipo_Ativo Default(1)
	)
