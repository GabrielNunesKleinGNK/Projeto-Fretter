If Object_Id('dbo.Tb_Edi_EntregaConfiguracaoTipo') Is NULL --Ativo --Passivo
	Create Table dbo.Tb_Edi_EntregaConfiguracaoTipo
	(
		Cd_Id							Int Identity(1,1) Constraint PK_Tb_Edi_EntregaConfiguracaoTipo_Cd_Id Primary key 
		,Ds_Tipo						Varchar(64)
	)
Go
If Object_Id('dbo.Tb_Edi_EntregaConfiguracao') Is NULL
	Create Table dbo.Tb_Edi_EntregaConfiguracao
	(
		 Cd_Id							Int Identity(1,1) Constraint PK_Tb_Edi_EntregaConfiguracao_Cd_Id Primary Key 
		,Id_EntregaConfiguracaoTipo		Int Not NULL References Tb_Edi_EntregaConfiguracaoTipo(Cd_Id)
		,Id_Empresa						Int Not NULL References Tb_Adm_Empresa(Cd_Id)
		,Ds_Caminho						Varchar(512)
		,Ds_Verbo						Varchar(32)
		,Ds_Layout						Varchar(1024) --CodigoPedido:$costumer.order.orderId$,DataEntrega:$$
		,Ds_LayoutHeader				Varchar(512)
		,Ds_ApiKey						Varchar(256)
		,Ds_URLStageCallBack			Varchar(512)
		,Ds_URLEtiquetaCallBack			Varchar(512)
		,Flg_Lote						Bit Constraint DF_Tb_Edi_EntregaConfiguracao_Lote Default(0)
		,Qtd_Registro					Int 
		,Qtd_Paralelo					Int Constraint DF_Tb_Edi_EntregaConfiguracao_Qtd_Paralelo Default(1)
		,Flg_ProcessamentoSucesso		Bit Constraint DF_Tb_Edi_EntregaConfiguracao_ProcessamentoSucesso Default(0)		
		,Dt_Processamento				Datetime 		
		,Cd_IntervaloExecucao			Smallint -- Minutos
		,Dt_ProximaExecucao				Datetime
		,Flg_Ativo						Bit Constraint DF_Tb_Edi_EntregaConfiguracao_Ativo Default(1)
	)
Go
If Object_Id('dbo.Tb_Edi_EntregaConfiguracaoHistorico') Is NULL
	Create Table dbo.Tb_Edi_EntregaConfiguracaoHistorico
	(
		Cd_Id							Bigint Identity(1,1) Constraint Tb_Edi_EntregaConfiguracaoHistorico_Cd_Id Primary Key 
		,Id_EntregaConfiguracao			Int Not NULL References Tb_Edi_EntregaConfiguracao(Cd_Id) 			
		,Qtd_Processado					Int
		,Dt_EntregaMinima				Datetime
		,Dt_EntregaMaxima				Datetime
		,Dt_PeriodoInicial				Datetime
		,Dt_PeriodoFinal				Datetime
		,Nr_ControleInicial				Bigint
		,Nr_ControleFinal				Bigint
		,Ds_MensagemRetorno				Varchar(2048)
		,Flg_Sucesso					Bit Constraint DF_Tb_Edi_EntregaConfiguracaoHistorico_Flg_Sucesso Default(0)
		,Flg_Ativo						Bit Constraint DF_Tb_Edi_EntregaConfiguracaoHistorico_Ativo Default(1)
	)
Go
If Object_Id('dbo.Tb_Edi_EntregaStageLog') Is NULL
	CREATE TABLE dbo.Tb_Edi_EntregaStageLog
	(
		Cd_Id							Bigint IDENTITY(1,1) CONSTRAINT [Pk_Tb_Edi_EntregaStageLog_Cd_Id] PRIMARY KEY CLUSTERED 
		,Dt_Inclusao					Datetime NOT NULL Constraint DF_Tb_Edi_EntregaStageLog_DtInclusao Default(Getdate())
		,Ds_Hash						Uniqueidentifier NULL
		,Ds_Log							Varchar(4096)	NULL
		,Ds_Exception					Varchar(4096)	NULL
		,Ds_Complemento					Varchar(1024)	NULL
		,Ds_Referencia					Varchar(128)	NULL	 
		,Ds_IP							Varchar(128)	NULL	 
		,Ds_URL							Varchar(256)	NULL	 
		,Ds_Verbo						Varchar(16)		NULL	 
		,Ds_Requisicao					Varchar(2048)	NULL
		,Cd_UsuarioCadastro				Int NULL
		,Flg_Ativo						Bit Constraint DF_Tb_Edi_EntregaStageLog_Ativo Default(1)
	)
GO
If Object_Id('dbo.Tb_Edi_EntregaStageErro') Is NULL
begin
	CREATE TABLE [dbo].[Tb_Edi_EntregaStageErro](
		[Cd_Id] [int] IDENTITY(1,1) NOT NULL,
		[Dt_Importacao] [datetime] NOT NULL,
		[Id_Arquivo] [int] NULL,
		[Ds_Retorno] [varchar](max) NULL,
		[Ds_JsonEntrada] [varchar](max) NULL,
		[Ds_JsonProcessamento] [varchar](max) NULL,
		[Id_CodigoErro] [int] NULL,
		[Cd_EntregaSaida] [varchar](128) NULL,
		[Flg_Ativo] [bit] NULL,
		[Nr_Tentativas] [int] NULL,
	 CONSTRAINT [Pk_Tb_Edi_EntregaStageErro_Cd_Id] PRIMARY KEY CLUSTERED 
	(
		[Cd_Id] ASC
	)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	ALTER TABLE [dbo].[Tb_Edi_EntregaStageErro] ADD  CONSTRAINT [DF_Tb_Edi_EntregaStageErro_DtInclusao]  DEFAULT (getdate()) FOR [Dt_Importacao]

	ALTER TABLE [dbo].[Tb_Edi_EntregaStageErro] ADD  CONSTRAINT [DF_Tb_Edi_EntregaStageErro_Ativo]  DEFAULT ((1)) FOR [Flg_Ativo]

end
Go
If Object_Id('dbo.Tb_Edi_EntregaStageEntrada') Is NULL
	CREATE TABLE dbo.Tb_Edi_EntregaStageEntrada
	(
		Cd_Id						Int IDENTITY(1,1) NOT NULL CONSTRAInt Pk_Tb_Edi_EntregaStageEntrada_Cd_Id PRIMARY KEY CLUSTERED 
		,Dt_Inclusao				Datetime NOT NULL Constraint DF_Tb_Edi_EntregaStageEntrada_DtInclusao Default(Getdate())
		,Ds_Hash					Uniqueidentifier NULL
		,Cd_EntregaSaida			Varchar(128) NULL
		,Ds_Json					Varchar(max) NULL
		,Flg_Validada				Bit NULL
		,Id_EntregaStage			Int NULL
		,Flg_Ativo					Bit Constraint DF_Tb_Edi_EntregaStageEntrada_Ativo Default(1)
	)
Go
If Object_Id('dbo.Tb_Edi_EntregaStage') Is NULL
	CREATE TABLE dbo.Tb_Edi_EntregaStage
	(
		 Cd_Id							Int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Tb_Edi_EntregaStage_Id PRIMARY KEY
		,Id_CanalVenda					Int NULL CONSTRAINT [FK_Tb_Edi_EntregaStage_Tb_Adm_CanalVenda] FOREIGN KEY([Id_CanalVenda]) REFERENCES [dbo].[Tb_Adm_CanalVenda] ([Cd_Id])
		,Id_Empresa						Int NULL CONSTRAINT [FK_Tb_Edi_EntregaStage_Tb_Adm_Empresa] FOREIGN KEY([Id_EmpresaMarketplace]) REFERENCES [dbo].[Tb_Adm_Empresa] ([Cd_Id])
		,Id_Canal						Int NULL CONSTRAINT [FK_Tb_Edi_EntregaStage_Tb_Adm_Canal] FOREIGN KEY([Id_Canal])REFERENCES [dbo].[Tb_Adm_Canal] ([Cd_Id])
		,Id_EmpresaMarketplace			Int NULL CONSTRAINT [FK_Tb_Edi_EntregaStage_Tb_Adm_Empresa_2] FOREIGN KEY([Id_EmpresaMarketplace]) REFERENCES [dbo].[Tb_Adm_Empresa] ([Cd_Id])
		,Id_Transportador				Int NULL CONSTRAINT [FK_Tb_Edi_EntregaStage_Tb_Adm_Transportador] FOREIGN KEY([Id_Transportador])REFERENCES [dbo].[Tb_Adm_Transportador] ([Cd_Id])
		,Id_TransportadorCnpj			Int NULL CONSTRAINT [FK_Tb_Edi_EntregaStage_Tb_Adm_TransportadorCnpj] FOREIGN KEY([Id_TransportadorCnpj]) REFERENCES [dbo].[Tb_Adm_TransportadorCnpj] ([Cd_Id])
		,Id_MicroServico				Int NULL 
		,Id_TipoServico					TinyInt NULL
		,Id_Lojista						Int NULL
		,Id_PLP							Int NULL
		,Cd_Danfe						Varchar(128) NULL 
		,Cd_CodigoIntegracao			Varchar(128) NULL	-- Valor referente a PK do Pedido do Cliente "commercial_id":"5088299920001",
		,Cd_EntregaEntrada				Varchar(128) NULL	--Exemplo 5088299920001-A
		,Cd_EntregaSaida				Varchar(128) NULL	--Exemplo 5088299920001-A
		,Cd_Sro							Varchar(128) NULL
		,Dt_PedidoCriacao				Datetime			--"created_date":"2021-08-02T20:13:08Z",
		,Dt_Postagem					SmallDatetime NULL	--"delivery_date":{"earliest":"2021-08-09T20:10:03.375Z","latest":"2021-08-09T20:10:03.375Z"}
		,Dt_PrevistaEntrega				SmallDatetime NULL
		,Vl_PrazoTransportadorEstatico	Int NULL
		,Vl_PrazoTransportadorDinamico	Int NULL
		,Vl_PrazoCliente				Int NULL
		,Vl_Custo						Decimal(18, 4) NULL
		,Vl_Cobrado						Decimal(18, 4) NULL
		,Vl_Global						Decimal(18, 4) NULL
		,Flg_ServicoDisponivel			Bit Constraint DF_Tb_Edi_EntregaStage_Flg_ServicoDisponivel Default(0)
		,Ds_ServicoDisponivel			Varchar(512) NULL
		,Flg_PostagemVerificada			Bit Constraint DF_Tb_Edi_EntregaStage_Flg_PostagemVerificada Default(0)
		,Flg_EntregaImportada			Bit Constraint DF_Tb_Edi_EntregaStage_Flg_EntregaImportada Default(0)
		,Id_Tabela						Int NULL
		,Ds_Tomador						Varchar(128) NULL
		,Vl_Altura						Decimal(18, 4) NULL
		,Vl_Comprimento					Decimal(18, 4) NULL
		,Vl_Largura						Decimal(18, 4) NULL
		,Vl_Peso						Decimal(18, 4) NULL
		,Vl_Cubagem						Decimal(18, 4) NULL
		,Vl_Diametro					Decimal(18, 4) NULL
		,Vl_Declarado					Decimal(18, 4) NULL
		,Vl_Receita						Decimal(18, 4) NULL
		,Vl_Total						Decimal(18, 4) NULL	
		,Flg_EtiquetaGerada				Bit Constraint DF_Tb_Edi_EntregaStage_Flg_EtiquetaGerada Default(0)
		,Dt_EtiquetaGerada				Datetime NULL		
		,Dt_ValidadeInicioEtiqueta		Datetime
		,Dt_ValidadeFimEtiqueta			Datetime
		,Flg_Rastreada					Bit Constraint DF_Tb_Edi_EntregaStage_Flg_Rastreada Default(0)
		,Nr_Volume						Int NULL
		,Ds_LinkEtiquetaPDF				Varchar(512)	
		,Ds_LinkEtiquetaPNG				Varchar(512)	
		,Ds_LinkEtiquetaZPL				Varchar(512)		
		,Dt_Inclusao					Datetime NOT NULL Constraint DF_Tb_Edi_EntregaStage_DtInclusao Default(Getdate())
		,Dt_UltimaAlteracao				Datetime	--"last_updated_date":"2021-08-02T20:21:08Z",
		,Flg_Ativo						Bit Constraint DF_Tb_Edi_EntregaStage_Ativo Default(1)
	)
Go
If Object_Id('dbo.Tb_Edi_EntregaStageItem') Is NULL
	CREATE TABLE dbo.Tb_Edi_EntregaStageItem
	(
		Cd_Id							Int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Tb_Edi_EntregaStageItem_Id PRIMARY KEY
		,Dt_Inclusao					Datetime NOT NULL Constraint DF_Tb_Edi_EntregaStageItem_DtInclusao Default(Getdate())
		,Id_EntregaStage				Int Not NULL Constraint FK_Tb_Edi_EntregaStageItem_EntregaStage References Tb_Edi_EntregaStage(Cd_Id)				
		,Cd_EntregaEntrada				Varchar(128) NULL --Exemplo Item Carrefour 5088299920001-A-1
		,Cd_EntregaSaida				Varchar(128) NULL --Exemplo Item Carrefour 5088299920001-A-1
		,Cd_Sro							Varchar(128) NULL
		,Cd_SroReversa					Varchar(128) NULL
		,Dt_Postagem					SmallDatetime NULL
		,Dt_PostagemReversa				SmallDatetime NULL
		,Dt_PrevistaEntrega				SmallDatetime NULL				
		,Dt_PrevistaEntregaReversa		SmallDatetime NULL				
		,Vl_Item						Decimal(18, 4) NULL	-- "price":299.00,
		,Vl_ItemTotal					Decimal(18, 4) NULL	-- "total_price":315.13 --Vl Item + Taxas
		,Vl_Frete						Decimal(18, 4) NULL -- "shipping_price":16.13
		,Vl_FreteCobrado				Decimal(18, 4) NULL	-- valor cobrado real da transportadora			
		,Flg_PostagemVerificada			Bit Constraint DF_Tb_Edi_EntregaStageItem_Flg_PostagemVerificada Default(0)						
		,Flg_Rastreada					Bit Constraint DF_Tb_Edi_EntregaStageItem_Flg_Rastreada Default(0)		
		,Ds_Informacao					Varchar(256)
		,Dt_SoliticacaoReversa			Datetime
		,Dt_ValidadeReversa				Datetime
		,Flg_PendenteReversa			Bit Constraint DF_Tb_Edi_EntregaStageItem_Flg_Flg_PendenteReversa Default(0)		
		,Flg_Ativo						Bit Constraint DF_Tb_Edi_EntregaStageItem_Ativo Default(1)
	)
Go
If Object_Id('dbo.Tb_Edi_EntregaStageRemetente') Is NULL
	CREATE TABLE dbo.Tb_Edi_EntregaStageRemetente
	(
		Cd_Id							Int IDENTITY(1,1) NOT NULL CONSTRAINT Pk_Tb_Edi_EntregaStageRemetente_Cd_Id PRIMARY KEY
		,Id_EntregaStage				Int NOT NULL CONSTRAINT FK_Tb_Edi_EntregaStageRemetente_Tb_Edi_EntregaStage FOREIGN KEY(Id_EntregaStage) REFERENCES dbo.Tb_Edi_EntregaStage (Cd_Id)
		,Ds_Nome						Varchar(256) NOT NULL
		,Cd_CodigoIntegracao			Varchar(64) NULL --Caso exista algum codigo de integracao
		,Cd_Cep							Varchar(8) NOT NULL
		,Ds_Endereco					Varchar(256) NOT NULL
		,Ds_Numero						Varchar(64) NULL
		,Ds_Complemento					Varchar(512) NULL
		,Ds_Bairro						Varchar(128) NULL
		,Ds_Cidade						Varchar(128) NOT NULL
		,Cd_UF							Varchar(16) NOT NULL	
		,Dt_Alteracao					Datetime NULL
		,Flg_Ativo						Bit Constraint DF_Tb_Edi_EntregaStageRemetente_Ativo Default(1)
	)
Go
If Object_Id('dbo.Tb_Edi_EntregaStageDestinatario') Is NULL
	CREATE TABLE dbo.Tb_Edi_EntregaStageDestinatario
	(
		Cd_Id							Int IDENTITY(1,1) NOT NULL  CONSTRAINT Pk_Tb_Edi_EntregaStageDestinatario_Cd_Id PRIMARY KEY CLUSTERED 
		,Id_EntregaStage				Int NOT NULL CONSTRAINT [FK_Tb_Edi_EntregaStageDestinatario_Tb_Edi_EntregaStage] FOREIGN KEY([Id_EntregaStage]) REFERENCES [dbo].[Tb_Edi_EntregaStage] ([Cd_Id])
		,Ds_Nome						Varchar(256) NOT NULL
		,Cd_CodigoIntegracao			Varchar(64) NULL --Caso exista algum codigo de integracao
		,Cd_CpfCnpj						Varchar(14) NULL
		,Cd_InscricaoEstadual			Varchar(128) NULL		
		,Cd_Cep							Varchar(8) NOT NULL
		,Ds_Endereco					Varchar(256) NOT NULL
		,Ds_Numero						Varchar(64) NULL
		,Ds_Complemento					Varchar(256) NULL
		,Ds_Bairro						Varchar(128) NULL
		,Ds_Cidade						Varchar(128) NOT NULL
		,Cd_UF							Varchar(16) NOT NULL
		,Dt_Alteracao					Datetime NULL
		,Flg_Ativo						Bit Constraint DF_Tb_Edi_EntregaStageDestinatario_Ativo Default(1)
	)
Go
if object_id('dbo.Tb_Adm_StageConfigEtiquetaTipo') is null
	CREATE TABLE [dbo].[Tb_Adm_StageConfigEtiquetaTipo](
		[Cd_Id] [int] NOT NULL,
		[Ds_Descricao] [varchar](30) NULL,
		[Flg_GerarSro] BIT DEFAULT(1)
	PRIMARY KEY CLUSTERED 
	(
		[Cd_Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
GO
if object_id('dbo.Tb_Edi_EntregaStageErroTipo') is null
	CREATE TABLE [dbo].[Tb_Edi_EntregaStageErroTipo](
		[Cd_Id] [int] identity(1,1) NOT NULL,
		[Ds_Descricao] [varchar](100) NULL,
		[Flg_EnviarPorCallback] BIT DEFAULT(0),
		[Nr_QtdeTentativas] [int] NULL
	PRIMARY KEY CLUSTERED 
	(
		[Cd_Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
GO

If Object_Id('dbo.Tb_Edi_EntregaStageSku') Is NULL
	CREATE TABLE dbo.Tb_Edi_EntregaStageSku
	(
		Cd_Id							Int IDENTITY(1,1) NOT NULL CONSTRAINT Pk_Tb_Edi_EntregaStageSku_Cd_Id PRIMARY KEY CLUSTERED 
		,Id_EntregaStageItem			Int NOT NULL CONSTRAINT [FK_Tb_Edi_EntregaStageSku_Tb_Edi_EntregaStageItem] FOREIGN KEY(Id_EntregaStageItem) REFERENCES [dbo].[Tb_Edi_EntregaStageItem] ([Cd_Id])
		,Ds_Item						Varchar(512) NULL --"product_title":"Cafeteira Arno Gran Perfectta Thermo CFX2 16 a 24 Xícaras 800W com Jarra Térmica Inquebrável Preto - 220V",
		,Cd_Sku							Varchar(64) NULL --"product_sku":"9902910",
		,Cd_CodigoIntegracao			Varchar(64) NULL --Caso exista algum codigo de integracao
		,Vl_Produto						Decimal(18, 4) NULL	--"price":299.00,
		,Vl_ProdutoUnitario				Decimal(18, 4) NULL	--"price_unit":299.00
		,Vl_Altura						Decimal(18, 4) NULL	
		,Vl_Comprimento					Decimal(18, 4) NULL
		,Vl_Largura						Decimal(18, 4) NULL
		,Vl_Peso						Decimal(18, 4) NULL
		,Vl_Cubagem						Decimal(18, 4) NULL
		,Vl_Diametro					Decimal(18, 4) NULL		
		,Vl_Quantidade					Int -- "quantity":1,
		,Dt_Alteracao					Datetime NULL
		,Flg_Ativo						Bit Constraint DF_Tb_Edi_EntregaStageSku_Ativo Default(1)	
	)
Go
If Object_Id('dbo.Tb_Edi_EntregaStageLog') Is NULL
Begin
	CREATE TABLE [dbo].[Tb_Edi_EntregaStageEtiqueta]
	(
		[Cd_Id] [int] IDENTITY(1,1) NOT NULL,
		[Dt_Inclusao] [datetime] NOT NULL,
		[Id_MicroServico] [int] NOT NULL,
		[Id_EntregaStage] [int] NULL,
		[Cd_Etiqueta] [varchar](13) NOT NULL,
		[Flg_Cancelada] [bit] NULL,
		[Ds_Cancelamento] [varchar](500) NULL,
		[Id_Plp] [int] NULL,
	 CONSTRAINT [Pk_Tb_Edi_EntregaStageEtiqueta_Cd_Id] PRIMARY KEY CLUSTERED 
	(
		[Cd_Id] ASC
	)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, FILLFACTOR = 80) ON [PRIMARY]
	) ON [PRIMARY]	

	ALTER TABLE [dbo].[Tb_Edi_EntregaStageEtiqueta] ADD  CONSTRAINT [DF_Tb_Edi_EntregaStageEtiqueta___Dt_Inclusao]  DEFAULT (getdate()) FOR [Dt_Inclusao]	

	ALTER TABLE [dbo].[Tb_Edi_EntregaStageEtiqueta] ADD  CONSTRAINT [DF_Tb_Edi_EntregaStageEtiqueta___Flg_Cancelada]  DEFAULT ((0)) FOR [Flg_Cancelada]	

	ALTER TABLE [dbo].[Tb_Edi_EntregaStageEtiqueta]  WITH CHECK ADD  CONSTRAINT [FK_Tb_Edi_EntregaStageEtiqueta_Tb_Adm_MicroServico] FOREIGN KEY([Id_MicroServico])	REFERENCES [dbo].[Tb_Adm_MicroServico] ([Cd_Id])	

	ALTER TABLE [dbo].[Tb_Edi_EntregaStageEtiqueta] CHECK CONSTRAINT [FK_Tb_Edi_EntregaStageEtiqueta_Tb_Adm_MicroServico]	

	ALTER TABLE [dbo].[Tb_Edi_EntregaStageEtiqueta]  WITH CHECK ADD  CONSTRAINT [FK_Tb_Edi_EntregaStageEtiqueta_Tb_Edi_EntregaStage] FOREIGN KEY([Id_EntregaStage])
	REFERENCES [dbo].[Tb_Edi_EntregaStage] ([Cd_Id])	

	ALTER TABLE [dbo].[Tb_Edi_EntregaStageEtiqueta] CHECK CONSTRAINT [FK_Tb_Edi_EntregaStageEtiqueta_Tb_Edi_EntregaStage]
	
End
Go
IF Object_Id('dbo.Tb_Adm_StageConfig') IS NULL
BEGIN
	CREATE TABLE [dbo].[Tb_Adm_StageConfig](
		[Cd_Id] [int] IDENTITY(1,1) NOT NULL,
		[Dt_Inclusao] [datetime] NOT NULL,
		[Id_MicroServico] [int] NOT NULL,
		[Id_Servico] [int] NULL,
		[Nr_Contrato] [varchar](50) NULL,
		[Nr_Diretoria] [varchar](50) NULL,
		[Cd_Administrativo] [int] NULL,
		[Cd_Cnpj] [varchar](14) NULL,
		[Cd_Servico] [varchar](20) NULL,
		[Ds_Login] [varchar](50) NULL,
		[Ds_Senha] [varchar](50) NULL,
		[Ds_CartaoPostagem] [varchar](50) NULL,
		[Ds_LogoCliente] [varchar](max) NULL,
		[Cd_ServicoAdicional] [varchar](15) NULL,
		[Cd_ServicoEtiqueta] [varchar](2) NULL,
		[Id_StageConfigEtiquetaTipo] [int] NULL,
		[Ds_Endereco] [varchar](200) NULL,
		[Ds_SiglaEtiqueta] [varchar](5) NULL,
		[Id_ExpedicaoEnvioCorreiosTipo] [int] NULL,
		[Flg_UsarCnpjDanfe] [bit] NULL,
		[Id_CotacaoFreteConfig] [int] NULL,
	 CONSTRAINT [Pk_Tb_Adm_StageConfig_Cd_Id] PRIMARY KEY CLUSTERED 
	(
		[Cd_Id] ASC
	)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	ALTER TABLE [dbo].[Tb_Adm_StageConfig] ADD  CONSTRAINT [DF_Tb_Adm_StageConfig___Dt_Inclusao]  DEFAULT (getdate()) FOR [Dt_Inclusao]

	ALTER TABLE [dbo].[Tb_Adm_StageConfig] ADD  DEFAULT ((0)) FOR [Flg_UsarCnpjDanfe]

	ALTER TABLE [dbo].[Tb_Adm_StageConfig]  WITH CHECK ADD  CONSTRAINT [FK_Tb_Adm_StageConfig_Tb_Adm_MicroServico] FOREIGN KEY([Id_MicroServico])
	REFERENCES [dbo].[Tb_Adm_MicroServico] ([Cd_Id])

	ALTER TABLE [dbo].[Tb_Adm_StageConfig] CHECK CONSTRAINT [FK_Tb_Adm_StageConfig_Tb_Adm_MicroServico]

	ALTER TABLE [dbo].[Tb_Adm_StageConfig]  WITH CHECK ADD  CONSTRAINT [FK_Tb_Adm_StageConfig_Tb_Adm_StageConfigEtiquetaTipo] FOREIGN KEY([Id_StageConfigEtiquetaTipo])
	REFERENCES [dbo].[Tb_Adm_StageConfigEtiquetaTipo] ([Cd_Id])

	ALTER TABLE [dbo].[Tb_Adm_StageConfig] CHECK CONSTRAINT [FK_Tb_Adm_StageConfig_Tb_Adm_StageConfigEtiquetaTipo]
END
GO

IF Object_Id('dbo.Tb_Edi_EntregaStagePLP') IS NULL
BEGIN
	CREATE TABLE [dbo].[Tb_Edi_EntregaStagePLP](
		[Cd_Id] [int] IDENTITY(1,1) NOT NULL,
		[Dt_Inclusao] [datetime] NOT NULL,
		[Id_EntregaStage] [int] NULL,
		[Ds_EnvelopePLP] [varchar](8000) NOT NULL,
		[Flg_Postada] [bit] NULL,
		[Ds_RetornoPostagem] [varchar](200) NULL,
	 CONSTRAINT [Pk_Tb_Edi_EntregaStagePLP_Cd_Id] PRIMARY KEY CLUSTERED 
	(
		[Cd_Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[Tb_Edi_EntregaStagePLP] ADD  CONSTRAINT [DF_Tb_Edi_EntregaStagePLP___Dt_Inclusao]  DEFAULT (getdate()) FOR [Dt_Inclusao]
END
GO

If TYPE_ID('Tp_Edi_EntregaStageEtiqueta') IS NULL
	CREATE TYPE Tp_Edi_EntregaStageEtiqueta AS TABLE(
		 Cd_Id				int			 NULL
		,Dt_Inclusao		datetime	 NULL
		,Id_MicroServico	int			 NULL
		,Id_EntregaStage	int			 NULL
		,Cd_Etiqueta		varchar(13)	 NULL
		,Flg_Cancelada		bit			 NULL
		,Ds_Cancelamento	varchar(500) NULL
		,Id_Plp				int			 NULL
	)

IF (COL_LENGTH('Tb_edi_EntregaStageErro','Nr_Tentativas') IS NULL)
	ALTER TABLE Tb_edi_EntregaStageErro ADD Nr_Tentativas int null
Go
IF (COL_LENGTH('Tb_Edi_EntregaStagePlp','Ds_RetornoPostagem') IS NULL)
	ALTER TABLE Tb_Edi_EntregaStagePlp ADD Ds_RetornoPostagem VARCHAR(200) NULL
Go
IF (COL_LENGTH('Tb_Edi_EntregaStageEtiqueta','Flg_Cancelada') IS NULL)
	ALTER TABLE Tb_Edi_EntregaStageEtiqueta ADD Flg_Cancelada bit DEFAULT(0)
Go
IF (COL_LENGTH('Tb_Edi_EntregaStageEtiqueta','Id_PLP') IS NULL)
	ALTER TABLE Tb_Edi_EntregaStageEtiqueta ADD Id_PLP INT NULL
Go
IF (COL_LENGTH('Tb_Adm_TransportadorCnpj','Flg_Default') IS NULL)
	ALTER TABLE Tb_Adm_TransportadorCnpj ADD Flg_Default BIT DEFAULT(0)
Go
IF (COL_LENGTH('Tb_Edi_Entrega','Id_EntregaOrigemImportacao') IS NULL)
	ALTER TABLE Tb_Edi_Entrega ADD Id_EntregaOrigemImportacao INT NULL




--EntregaImportacao				-- Equivalente a EntregaStageEntrada --Recebe a Entrega envia para a Fila de processamento de Entrega -- Antes insere na EntregaImportacao e gera um hash para enviar para Entrega
--Tb_Edi_Entrega
--Tb_Edi_EntregaDetalhe			--Destinatario e os Dados do Volume
--Tb_Edi_EntregaDetalheItem		-- Itens da Entrega (Produtos 1 - N) SKU, Qtde, Dimensoes e etc ??? Necessário
--Tb_Edi_EntregaHistorico			-- Salva as Alterações de Ocorrencias ?? Temporal Table ??

--Select * From Tb_Edi_Entrega

--EntregaDevolucaoStatus			-- Aberta | Autorizada | Agendada | Em Transito | Finalizada
--EntregaDevolucao				-- Logistica reversa
--	- CodigoTicket				-- Api do Correio Retorna
--	- CodigoColeta				-- Api do Correio Retorna
--	- DataValidade				


