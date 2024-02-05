If Object_ID('Fretter.EmpresaImportacaoArquivo') Is NULL
	Create Table Fretter.EmpresaImportacaoArquivo
	(
		EmpresaImportacaoArquivoId		Int Identity(1,1) Constraint Pk_Fretter_EmpresaImportacaoArquivo Primary Key 
		,Nome							Varchar(512) -- Nome do Arquivo
		,Descricao						Varchar(2048)
		,EmpresaId						Int --Empresa Logada no Fretter
		,ArquivoURL						Varchar(512)
		,QuantidadeEmpresa				Int Constraint DF_Fretter_EmpresaImportacaoArquivo_QuantidadeEmpresa Default(0)
		,Processado						Bit Constraint DF_Fretter_EmpresaImportacaoArquivo_Processado Default(0)
		,Sucesso						Bit Constraint DF_Fretter_EmpresaImportacaoArquivo_Sucesso Default(0)
		,DataCadastro					Datetime Constraint DF_Fretter_EmpresaImportacaoArquivo_DataCadastro Default(Getdate())
		,UsuarioCadastro				Int
		,Ativo							Bit Constraint DF_Fretter_EmpresaImportacaoArquivo_Ativo Default(1)
	)

Go
If Object_ID('Fretter.EmpresaImportacaoDetalhe') Is NULL
	Create Table Fretter.EmpresaImportacaoDetalhe
	(
		EmpresaImportacaoDetalheId		Int Identity(1,1) Constraint Pk_Fretter_EmpresaImportacaoDetalhe Primary Key 
		,EmpresaImportacaoArquivoId		Int Constraint FK_Fretter_EmpresaImportacaoDetalhe_EmpresaImportacaoArquivoId References Fretter.EmpresaImportacaoArquivo(EmpresaImportacaoArquivoId)
		,EmpresaId						Int
		,Token							Varchar(64)
		,Nome							Varchar(512)
		,Cnpj							Varchar(32)		
		,Cep							Varchar(16)
		,Email							Varchar(256) Null
		,UF								Varchar(16)
		,Descricao						Varchar(256)
		,CorreioBalcao					Bit Constraint DF_Fretter_EmpresaImportacaoDetalhe_CorreioBalcao Default(0)
		,ConsomeApiFrete				Bit Constraint DF_Fretter_EmpresaImportacaoDetalhe_ConsomeApiFrete Default(0)
		,SucessoEmpresa					Bit Constraint DF_Fretter_EmpresaImportacaoDetalhe_SucessoEmpresa Default(0)
		,SucessoCanal					Bit Constraint DF_Fretter_EmpresaImportacaoDetalhe_SucessoCanal Default(0)
		,SucessoPermissao				Bit Constraint DF_Fretter_EmpresaImportacaoDetalhe_SucessoPermissao Default(0)		
		,DataCadastro					Datetime Constraint DF_Fretter_EmpresaImportacaoDetalhe_DataCadastro Default(Getdate())
		,UsuarioCadastro				Int
		,Ativo							Bit Constraint DF_Fretter_EmpresaImportacaoDetalhe_Ativo Default(1)
	)

