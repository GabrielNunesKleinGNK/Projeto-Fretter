If Object_Id('dbo.Tb_Edi_EntregaTransporteServico') Is NULL
	Create Table dbo.Tb_Edi_EntregaTransporteServico --SEDEX CONTRATO --PAC E ETC
	(
		Cd_Id							Int Identity(1,1) Constraint PK_EntregaTransporteServico Primary Key
		,Id_Empresa						Int Not Null Constraint FK_EntregaTransporteServico_Empresa References Tb_Adm_Empresa(Cd_Id) --Carrefour
		,Id_Transportador				Int Not NULL Constraint FK_EntregaTransporteServico_Transportador References Tb_Adm_Transportador(Cd_Id) --Correios
		,Ds_Nome						Varchar(64) --SEDEX CONTRATO
		,Ds_Usuario						Varchar(128)
		,Ds_Senha						Varchar(128)
		,Cd_CodigoContrato				Varchar(64)	--Numero Contrato Correio
		,Cd_CodigoIntegracao			Varchar(32) --Codigo Integracao Correio	
		,Cd_CodigoCartao				Varchar(32) --Codigo Cartao Correio
		,Ds_URLBase						Varchar(512)
		,Flg_Ativo						Bit Constraint DF_EntregaTransporteServico_Ativo Default(1)
	)
Go
If Object_Id('dbo.Tb_Edi_EntregaTransporteTipo') Is NULL --Autorizacao de Postagem (Logistica Reversa) -- Coleta
	Create Table dbo.Tb_Edi_EntregaTransporteTipo
	(
		Cd_Id							Int Identity(1,1) Constraint PK_EntregaTransporteTipo Primary Key
		,Id_EntregaTransporteServico	Int Not NULL Constraint FK_EntregaTransporteTipo_EntregaTransporteServico References Tb_Edi_EntregaTransporteServico(Cd_Id) --Correios --Carrefour
		,Ds_Nome						Varchar(64)
		,Ds_URL							Varchar(128)
		,Ds_Layout						Varchar(512)
		,Ds_ApiKey						Varchar(512)
		,Ds_Usuario						Varchar(128)
		,Ds_Senha						Varchar(128)
		,Nr_DiasValidadeMinimo			Smallint -- Qtde de Dias Minimo de Validade da Autorizacao de Postagem
		,Nr_DiasValidadeMaximo			Smallint -- Qtde de Dias Maximo de Validade da Autorizacao de Postagem
		,Cd_Alias						Varchar(32) --Codigo Integracao Correio	-- "A" -- Autorizacao Postagem "AC" --Autorizacao Coleta
		,Cd_CodigoIntegracao			Varchar(32) --Id do servico no correios --Codigo Servico Correios 
		,Flg_Producao					Bit Constraint DF_EntregaTransporteTipo_Flg_Producao Default(1)
		,Flg_Ativo						Bit Constraint DF_EntregaTransporteTipo_Ativo Default(1)
	)

Go
If Object_Id('dbo.Tb_Edi_EntregaDevolucaoStatus') Is NULL 
	Create Table dbo.Tb_Edi_EntregaDevolucaoStatus -- Aberta | Autorizada | Agendada | Em Transito | Finalizada //Status dos Transportador
	(
		Cd_Id							Int Identity(1,1) Constraint PK_EntregaDevolucaoStatus Primary Key
		,Id_EntregaTransporteTipo		Int Not NULL Constraint FK_EntregaDevolucaoStatus_EntregaTransporteTipo References Tb_Edi_EntregaTransporteTipo(Cd_Id) --Correios
		,Cd_CodigoIntegracao			Varchar(16)
		,Cd_Alias						Varchar(32)
		,Ds_Nome						Varchar(64)
		,Flg_MonitoraOcorrencia			Bit Constraint DF_EntregaDevolucaoStatus_MonitoraOcorrencia Default(0)
		,Flg_Ativo						Bit Constraint DF_EntregaDevolucaoStatus_Ativo Default(1)
	)
Go
If Object_Id('dbo.Tb_Edi_EntregaDevolucaoAcao') Is NULL 
	Create Table dbo.Tb_Edi_EntregaDevolucaoAcao -- Aprovar -- Rejeitar --Cancelar
	(
		Cd_Id							Int Identity(1,1) Constraint PK_EntregaDevolucaoAcao Primary Key		
		,Ds_Nome						Varchar(64)
		,Flg_Ativo						Bit Constraint DF_EntregaDevolucaoAcao_Ativo Default(1)
	)
Go
If Object_Id('dbo.Tb_Edi_EntregaDevolucaoStatusAcao') Is NULL 
	Create Table dbo.Tb_Edi_EntregaDevolucaoStatusAcao -- Aprovar -- Rejeitar --Cancelar
	(
		Cd_Id							Int Identity(1,1) Constraint PK_EntregaDevolucaoStatusAcao Primary Key
		,Id_EntregaTransporteTipo		Int Not NULL Constraint FK_EntregaDevolucaoStatusAcao_EntregaTransporteTipo References Tb_Edi_EntregaTransporteTipo(Cd_Id) --Correios
		,Id_EntregaDevolucaoStatus		Int Not NULL Constraint FK_EntregaDevolucaoStatusAcao_EntregaDevolucaoStatus References Tb_Edi_EntregaDevolucaoStatus(Cd_Id) --Em aberto
		,Id_EntregaDevolucaoAcao		Int Not NULL Constraint FK_EntregaDevolucaoStatusAcao_EntregaDevolucaoAcao References Tb_Edi_EntregaDevolucaoAcao(Cd_Id) --Aprovar
		,Id_EntregaDevolucaoResultado	Int Not NULL Constraint FK_EntregaDevolucaoStatusAcao_EntregaDevolucaoResultado References Tb_Edi_EntregaDevolucaoStatus(Cd_Id) --Aprovado		
		,Flg_Visivel					Bit Constraint DF_Tb_Edi_EntregaDevolucaoStatusAcao_Visivel Default(1) --Caso esteja habilitado abre modal pra digitar uma observacao sobre a Acão
		,Flg_InformaMotivo				Bit Constraint DF_Tb_Edi_EntregaDevolucaoStatusAcao_InformaMotivo Default(1) --Caso esteja habilitado abre modal pra digitar uma observacao sobre a Acão
		,Flg_Ativo						Bit Constraint DF_Tb_Edi_EntregaDevolucaoStatusAcao_Ativo Default(1)
	)
Go
If Object_Id('dbo.Tb_Edi_EntregaDevolucao') Is NULL 
	Create Table dbo.Tb_Edi_EntregaDevolucao
	(
		Cd_Id							Int Identity(1,1) Constraint PK_EntregaDevolucao Primary Key 
		,Id_Entrega						Int Not NULL Constraint FK_EntregaDevolucao_Entrega References Tb_Edi_Entrega(Cd_Id)
		,Id_EntregaTransporteTipo		Int Not NULL Constraint FK_EntregaDevolucao_EntregaTransporteTipo References Tb_Edi_EntregaTransporteTipo(Cd_Id)
		,Cd_CodigoColeta				Varchar(64)
		,Cd_CodigoRastreio				Varchar(64)
		,Dt_Validade					Date
		,Ds_Observacao					Varchar(128)	
		,Dt_UltimaOcorrencia			Datetime
		,Ds_UltimaOcorrenciaCodigo		Varchar(64)
		,Ds_UltimaOcorrencia			Varchar(512) NULL
		,Id_UsuarioCadastro				Int
		,Dt_Inclusao					Datetime Constraint DF_EntregaDevolucao_DtInclusao Default(Getdate())
		,Id_UsuarioAlteracao			Int
		,Dt_Alteracao					Datetime
		,Id_EntregaDevolucaoStatus		Int Not NULL Constraint FK_EntregaDevolucao_DevolucaoStatus References Tb_Edi_EntregaDevolucaoStatus(Cd_Id)
		,Flg_Processado					Bit Constraint DF_EntregaDevolucao_Processado Default(1)
		,Flg_Finalizado					Bit Constraint DF_EntregaDevolucao_Finalizado Default(1)
		,Flg_Ativo						Bit Constraint DF_EntregaDevolucao_Ativo Default(1)
	)
Go
If Object_Id('dbo.Tb_Edi_EntregaDevolucaoHistorico') Is NULL 
	Create Table dbo.Tb_Edi_EntregaDevolucaoHistorico
	(
		Cd_Id									Int Identity(1,1) Constraint PK_EntregaDevolucaoHistorico Primary Key 
		,Id_EntregaDevolucao					Int Not NULL Constraint FK_Tb_Edi_EntregaDevolucaoHistorico_EntregaDevolucao References Tb_Edi_EntregaDevolucao(Cd_Id)			
		,Cd_CodigoColeta						Varchar(64)
		,Cd_CodigoRastreio						Varchar(64)
		,Cd_CodigoStatusIntegracao				Varchar(64)
		,Dt_Validade							Date
		,Ds_Mensagem							Varchar(512)		
		,Ds_Retorno								Varchar(512)		
		,Id_EntregaDevolucaoStatusAnterior		Int NULL Constraint FK_EntregaDevolucaoHistorico_DevolucaoStatusAnterior References Tb_Edi_EntregaDevolucaoStatus(Cd_Id)
		,Id_EntregaDevolucaoStatusAtual 		Int NULL Constraint FK_EntregaDevolucaoHistorico_DevolucaoStatusAtual References Tb_Edi_EntregaDevolucaoStatus(Cd_Id)
		,Id_UsuarioCadastro						Int
		,Dt_Inclusao							Datetime Constraint DF_EntregaDevolucaoHistorico_DtInclusao Default(Getdate())				
		,Flg_Ativo								Bit Constraint DF_EntregaDevolucaoHistorico_Ativo Default(1)
	)
Go
If Object_Id('dbo.Tb_Edi_EntregaDevolucaoOcorrencia') Is NULL 
	Create Table dbo.Tb_Edi_EntregaDevolucaoOcorrencia
	(
		Cd_Id							Int Identity(1,1) Constraint PK_EntregaDevolucaoOcorrencia Primary Key 
		,Id_EntregaDevolucao			Int Not NULL Constraint FK_Tb_Edi_EntregaDevolucaoOcorrencia_EntregaDevolucao References Tb_Edi_EntregaDevolucao(Cd_Id)	
		,Id_OcorrenciaEmpresaItem		Int 	
		,Cd_CodigoIntegracao			Varchar(16)
		,Nm_Sigla						Varchar(32)	
		,Ds_Ocorrencia					Varchar(512)	
		,Ds_Observacao					Varchar(256)	
		,Dt_Ocorrencia					Datetime		
		,Dt_Inclusao					Datetime Constraint DF_EntregaDevolucaoOcorrencia_DtInclusao Default(Getdate())		
		,Flg_Ativo						Bit Constraint DF_EntregaDevolucaoOcorrencia_Ativo Default(1)
	)
Go
If TYPE_ID('dbo.Tp_Edi_EntregaDevolucaoItem') IS NULL
	CREATE TYPE dbo.Tp_Edi_EntregaDevolucaoItem AS TABLE
	(
		Id_Empresa							Int NULL
		,Id_EntregaDevolucao				int NULL
		,Id_DevolucaoCorreioRetornoTipo		Int
		,Id_Entrega							bigint NULL	  
		,Cd_Pedido							Varchar(128) Null
		,Cd_CodigoColeta					varchar(64) NULL
		,Cd_CodigoRastreio					varchar(64) NULL
		,Dt_Validade						date NULL
		,Ds_Mensagem						varchar(512) NULL
		,Ds_Retorno							varchar(512) NULL
		,Id_EntregaDevolucaoStatus			int NULL
		,Cd_CodigoStatusIntegracao			varchar(256) NULL
		,Flg_InsereOcorrencia				bit NULL
		,Flg_Sucesso						bit NULL
	)
Go
If TYPE_ID('dbo.Tp_Edi_EntregaDevolucaoOcorrenciaItem') IS NULL
	CREATE TYPE dbo.Tp_Edi_EntregaDevolucaoOcorrenciaItem AS TABLE
	(
		Id_EntregaDevolucao				Int NULL
		,Id_Entrega						Bigint NULL		
		,Cd_CodigoIntegracao			Varchar(64) NULL
		,Nm_Sigla						Varchar(64) NULL
		,Ds_Ocorrencia					Varchar(64) NULL
		,Ds_Observacao					Varchar(64) NULL
		,Dt_Ocorrencia					Datetime Null
		,Cd_CodigoColeta				Varchar(64) NULL
		,Cd_CodigoRastreio				Varchar(64) NULL
		,Vl_PesoCubico					Varchar(32) NULL			
		,Vl_PesoReal					Varchar(32) NULL
		,Vl_Postagem					Varchar(32) NULL
	)
Go
If Object_Id('dbo.Tb_Edi_EntregaDevolucaoLogTipo') Is NULL 
	Create Table dbo.Tb_Edi_EntregaDevolucaoLogTipo
	(
		Cd_Id						Smallint Identity(1,1) Constraint PK_Tb_Edi_EntregaDevolucaoLogTipo Primary Key
		,Ds_Nome					Varchar(16)
		,Flg_Ativo					Bit Constraint DF_EntregaDevolucaoLogTipo_Ativo Default(1)
	)
Go
If Object_Id('dbo.Tb_Edi_EntregaDevolucaoLog') Is NULL 
	Create Table dbo.Tb_Edi_EntregaDevolucaoLog
	(
		Cd_Id						Int Identity(1,1) Constraint PK_Tb_Edi_EntregaDevolucaoLog Primary Key
		,Id_EntregaDevolucaoLogTipo	Smallint Constraint FK_EntregaDevolucaoLog_Tipo References Tb_Edi_EntregaDevolucaoLogTipo(Cd_Id)
		,Id_EntregaDevolucao		Int Constraint FK_EntregaDevolucaoLog_EntregaDevolucao References Tb_Edi_EntregaDevolucao(Cd_Id)
		,Ds_JsonEnvio				Varchar(Max)
		,Ds_JsonRetorno				Varchar(Max)
		,Ds_Observacao				Varchar(1024)
		,Flg_Sucesso				Bit Constraint DF_EntregaDevolucaoLog_Sucesso Default(0)
		,Dt_Cadastro				Datetime Constraint DF_EntregaDevolucaoLog_DataCadastro Default(Getdate())
		,Flg_Ativo					Bit Constraint DF_EntregaDevolucaoLog_Ativo Default(1)
	)