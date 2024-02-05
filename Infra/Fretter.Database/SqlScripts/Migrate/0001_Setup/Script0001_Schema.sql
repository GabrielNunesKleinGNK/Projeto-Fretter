If Object_Id('Fretter.ImportacaoArquivoStatus') Is NULL   
    Create Table Fretter.ImportacaoArquivoStatus
    (
         ImportacaoArquivoStatusId			Int Identity Constraint PK_Fretter_ImportacaoArquivoStatusId Primary KEY
        ,Nome								Varchar(64)
        ,Ativo								Bit Default(1)
        ,UsuarioCadastro					Int
        ,UsuarioAlteracao					Int
        ,DataCadastro						DateTime Default(GetDate())
        ,DataAlteracao						DateTime
    )
go
If Object_Id('Fretter.ImportacaoArquivoTipo') Is NULL   
    Create Table Fretter.ImportacaoArquivoTipo
    (
         ImportacaoArquivoTipoId			Int Identity Constraint PK_Fretter_ImportacaoArquivoTipoId Primary KEY
        ,Nome								Varchar(64)
        ,Ativo								Bit Default(1)
        ,UsuarioCadastro					Int
        ,UsuarioAlteracao					Int
        ,DataCadastro						DateTime Default(GetDate())
        ,DataAlteracao						DateTime
    )
go
If Object_Id('Fretter.ImportacaoConfiguracaoTipo') Is NULL
	Create Table Fretter.ImportacaoConfiguracaoTipo
	(
		ImportacaoConfiguracaoTipoId		Int IDENTITY(1,1) CONSTRAINT PK_Fretter_ImportacaoConfiguracaoTipo_Id PRIMARY KEY
		,Nome								Varchar(32) NULL
		,Ativo								Bit Constraint DF_Fretter_ImportacaoConfiguracaoTipo_Ativo Default(1)
	)
Go
If Not Exists(Select Top 1 1 From Fretter.ImportacaoConfiguracaoTipo)
Begin
	Insert Into Fretter.ImportacaoConfiguracaoTipo
	(
		Nome	
	)
	Values
	(
		'FTP'
	),
	(
		'Api'
	)
End
Go
If Object_Id('Fretter.ImportacaoConfiguracao') Is NULL
	Create Table Fretter.ImportacaoConfiguracao
	(
		ImportacaoConfiguracaoId			Int IDENTITY(1,1) CONSTRAINT [PK_Fretter_ImportacaoConfiguracao_Id] PRIMARY KEY
		,ImportacaoConfiguracaoTipoId		Int Constraint FK_Fretter_ImportacaoConfiguracaoTipo FOREIGN KEY([ImportacaoConfiguracaoTipoId]) REFERENCES [Fretter].[ImportacaoConfiguracaoTipo] ([ImportacaoConfiguracaoTipoId])
		,EmpresaId							Int NULL
		,TransportadorId					Int NULL
		,ImportacaoArquivoTipoId			Int Constraint FK_Fretter_ImportacaoConfiguracao_ArquivoTipoId FOREIGN KEY([ImportacaoArquivoTipoId])REFERENCES [Fretter].[ImportacaoArquivoTipo] ([ImportacaoArquivoTipoId])
		,Diretorio							Varchar(512) NULL
		,Usuario							Varchar(256) NULL
		,Senha								Varchar(256) NULL
		,Outro								Varchar(512) NULL
		,UltimaExecucao						Datetime NULL
		,UltimoRetorno						Varchar(512) NULL
		,Sucesso							Bit Constraint DF_Fretter_ImportacaoConfiguracao_Sucesso Default(0)
		,UsuarioCadastro					Int NULL
		,UsuarioAlteracao					Int NULL
		,DataCadastro						Datetime Constraint DF_Fretter_ImportacaoConfiguracao_DataCadastro Default(Getdate())
		,DataAlteracao						Datetime NULL
		,Compactado							Bit Constraint DF_Fretter_ImportacaoConfiguracao_Compactado Default(0)
		,Ativo								Bit Constraint DF_Fretter_ImportacaoConfiguracao_Ativo Default(1)
	)	
Go
If Object_Id('Fretter.ImportacaoArquivoCategoria') Is NULL   
    Create Table Fretter.ImportacaoArquivoCategoria
    (
        ImportacaoArquivoCategoriaId		Int Identity Constraint PK_Fretter_ImportacaoArquivoCategoriaId Primary KEY
        ,Nome								Varchar(64)
        ,ImportacaoArquivoTipoId			Int Not Null References Fretter.ImportacaoArquivoTipo(ImportacaoArquivoTipoId)
        ,Codigo								Varchar(64)
        ,Ativo								Bit Default(1)
        ,UsuarioCadastro					Int
        ,UsuarioAlteracao					Int
        ,DataCadastro						DateTime Default(GetDate())
        ,DataAlteracao						DateTime
    )
go
If Not Exists(Select Top 1 1 From Fretter.ImportacaoArquivoStatus)
    Insert Into Fretter.ImportacaoArquivoStatus(Nome,UsuarioCadastro)Values('Pendente',1),('Processando',1),('Concluido',1),('Falha',1)
go
If Not Exists(Select Top 1 1 From Fretter.ImportacaoArquivoTipo)
    Insert Into Fretter.ImportacaoArquivoTipo(Nome,UsuarioCadastro)Values('CTe',1),('CONEMB',1)
go
If Object_Id('Fretter.ImportacaoArquivo') Is NULL   
    Create Table Fretter.ImportacaoArquivo
    (
         ImportacaoArquivoId        Int Identity Constraint PK_Fretter_ImportacaoArquivoId Primary Key
        ,Nome                       Varchar(128)
		,Mensagem					Varchar(512)
        ,EmpresaId                  Int
        ,TransportadorId            Int
        ,ImportacaoArquivoTipoId    Int Not Null References Fretter.ImportacaoArquivoTipo(ImportacaoArquivoTipoId)
        ,Identificador              Varchar(256)
        ,Diretorio                  Varchar(512)						
		,DataProcessamento			Datetime		
		,ImportacaoArquivoStatusId  Int Not Null References Fretter.ImportacaoArquivoStatus(ImportacaoArquivoStatusId)
        ,Ativo                      Bit Default(1)
        ,UsuarioCadastro            Int
        ,UsuarioAlteracao           Int
        ,DataCadastro               DateTime Default(GetDate())
        ,DataAlteracao              DateTime
    )
GO
If Object_Id('Fretter.ImportacaoCte') Is NULL   
    Create Table Fretter.ImportacaoCte
    (
         ImportacaoCteId            Int Identity Constraint PK_Fretter_ImportacaoCteId Primary Key
        ,TipoAmbiente               Int
        ,ImportacaoArquivoId        Int
        ,TipoCte                    Int
        ,TipoServico                Int
        ,Chave                      Varchar(64)
		,ChaveComplementar			Varchar(64)
        ,Codigo                     Varchar(16)
        ,Numero                     Varchar(64)
        ,DigitoVerificador          Int
        ,Serie                      Varchar(8)
        ,DataEmissao                Date
        ,ValorPrestacaoServico      Decimal(9,2)		
		,Processado					Bit Default(0)
		,CNPJTransportador			Varchar(14)
		,CNPJTomador				Varchar(14) 
		,CNPJEmissor				Varchar(14)
		,Ativo						Bit Default(1)
    )
Go
If Object_Id('Fretter.ConfiguracaoCteTipo') Is NULL   
	CREATE TABLE Fretter.ConfiguracaoCteTipo
	(
		ConfiguracaoCteTipoId		Int IDENTITY(1,1) Constraint PK_Fretter_ConfiguracaoCteTipoId NOT NULL Primary Key
		,Descricao					Varchar(256) NULL
		,Ativo						Bit Default(1)
	)
Go
If Object_Id('Fretter.ConfiguracaoCteTransportador') Is NULL  
Begin
	CREATE TABLE Fretter.ConfiguracaoCteTransportador
	(
		[ConfiguracaoCteTransportadorId] [int] IDENTITY(1,1) NOT NULL,
		[ConfiguracaoCteTipoId] [int] NULL,
		[Alias] [varchar](32) NULL,
		[TransportadorCnpjId] [int] NULL,
		[DataCadastro] [datetime] NULL,
		[DataAlteracao] [datetime] NULL,
		[Ativo] [bit] NOT NULL,
	 CONSTRAINT [PK_Fretter_ConfiguracaoCteTransportador] PRIMARY KEY CLUSTERED 
	(
		[ConfiguracaoCteTransportadorId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]	

	ALTER TABLE [Fretter].[ConfiguracaoCteTransportador] ADD  DEFAULT ((1)) FOR [Ativo]	

	ALTER TABLE [Fretter].[ConfiguracaoCteTransportador]  WITH CHECK ADD FOREIGN KEY([ConfiguracaoCteTipoId])
	REFERENCES [Fretter].[ConfiguracaoCteTipo] ([ConfiguracaoCteTipoId])	

	ALTER TABLE [Fretter].[ConfiguracaoCteTransportador]  WITH CHECK ADD FOREIGN KEY([TransportadorCnpjId])
	REFERENCES [dbo].[Tb_Adm_TransportadorCnpj] ([Cd_Id])
End
Go
If Object_Id('Fretter.ImportacaoCteCarga') Is NULL   
    Create Table Fretter.ImportacaoCteCarga
    (
         ImportacaoCteCargaId			Int Identity Constraint PK_Fretter_ImportacaoCteCarga Primary Key
        ,ImportacaoCteId				Int Not Null References Fretter.ImportacaoCte(ImportacaoCteId)
        ,Tipo							Varchar(64)
        ,Codigo							Varchar(64)
        ,Quantidade						Int
		,ConfiguracaoCteTipoId			Int Null References Fretter.ConfiguracaoCteTipo (ConfiguracaoCteTipoId)
    )
go
If Object_Id('Fretter.ImportacaoCteNotaFiscal') Is NULL   
    Create Table Fretter.ImportacaoCteNotaFiscal
    (
         ImportacaoCteNotaFiscalId		Int Identity Constraint PK_Fretter_ImportacaoCteNotaFiscal Primary Key
        ,ImportacaoCteId				Int Not Null References Fretter.ImportacaoCte(ImportacaoCteId)
        ,Chave							Varchar(64)
    )
go
If Object_Id('Fretter.ConciliacaoStatus') Is Null     
    Create Table Fretter.ConciliacaoStatus
    (
        ConciliacaoStatusId             Int Identity Constraint PK_ConciliacaoStatus Primary Key
        ,Nome                           Varchar(64)
        ,Ativo                          Bit Default(1)
        ,UsuarioCadastro                Int
        ,UsuarioAlteracao               Int
        ,DataCadastro                   DateTime Default(GetDate())
        ,DataAlteracao                  DateTime
    )
GO
If Not Exists(Select Top 1 1 From Fretter.ConciliacaoStatus)
    Insert Into Fretter.ConciliacaoStatus(Nome,UsuarioCadastro)Values('Pedente',1),('Valida',1),('Divergente',1)
Go
If Object_Id('Fretter.ConciliacaoMediacao') Is Null     
    Create Table Fretter.ConciliacaoMediacao
    (
         ConciliacaoMediacao            BigInt Identity Constraint PK_Fretter_ConciliacaoMediacaoId Primary Key
        ,ImportacaoArquivoId            Int Not Null References Fretter.ImportacaoArquivo(ImportacaoArquivoId)
        ,ImportacaoArquivoCategoriaId   Int Not Null References Fretter.ImportacaoArquivoCategoria(ImportacaoArquivoCategoriaId)
        ,TipoServico                    Int
        ,Chave                          Varchar(64)
        ,Codigo                         Varchar(16)
        ,Numero                         Varchar(64)
        ,DigitoVerificador              Int
        ,Serie                          Varchar(8)
        ,DataEmissao                    Date
        ,ValorPrestacaoServico          Decimal(9,2)
        ,Ativo                          Bit Default(1)
        ,UsuarioCadastro                Int
        ,UsuarioAlteracao               Int
        ,DataCadastro                   DateTime Default(GetDate())
        ,DataAlteracao                  DateTime
    )
Go
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
		,DuracaoProcessamento	Int
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
		,Descricao			Varchar(128)
		,Ativo				Bit Constraint DF_Fretter_FaturaStatus_Ativo Default(1)
	)
End
Go
If Not Exists(Select Top 1 1 From Fretter.FaturaStatus)
	Insert Into Fretter.FaturaStatus(Descricao)Values('Pendente'),('Em Processamento'),('Em Aberto'),('Liquidado'),('Cancelado')
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
Go
If Object_Id('Fretter.Conciliacao') Is Null     
    Create Table Fretter.Conciliacao
    (
         ConciliacaoId                  BigInt Identity Constraint PK_Fretter_ConciliacaoId Primary Key
        ,EmpresaId                      Int
        ,EntregaId                      Int
        ,TransportadorId                Int
        ,TransportadorCnpjId            Int        
        ,ValorCustoFrete                Decimal(9,2)
        ,ValorCustoAdicional            Decimal(9,2)
        ,ValorCustoReal                 Decimal(9,2)
        ,ValorCustoDivergencia          Decimal(9,2)         
        ,QuantidadeTentativas           Int
        ,PossuiDivergenciaPeso          Bit
        ,PossuiDivergenciaTarifa        Bit
        ,DevolvidoRemetente             Bit
        ,ConciliacaoStatusId            Int Not Null -- Validada / Divergencia / Pendente
        ,DataEmissao                    Date 
        ,DataFinalizacao                DateTime  --Data da Ultima Ocorrencia "Finalizadora"
        ,FaturaId                       Int NULL References Fretter.Fatura(FaturaId)
        ,ProcessadoIndicador            Bit Constraint DF_Fretter_Conciliacao_Indicador Default(0)        
        ,Ativo                          Bit Default(1)
        ,UsuarioCadastro                Int
        ,UsuarioAlteracao               Int
        ,DataCadastro                   DateTime Default(GetDate())
        ,DataAlteracao                  DateTime
    )
GO
If Object_Id('Fretter.ConciliacaoHistorico') Is Null     
    Create Table Fretter.ConciliacaoHistorico
    (
         ConciliacaoHistoricoId         BigInt Identity(1,1) Constraint PK_Fretter_ConciliacaoHistoricoId Primary Key
        ,ConciliacaoId                  BigInt Not Null References Fretter.Conciliacao(ConciliacaoId)
        ,Descricao                      Varchar(256)
        ,Ativo                          Bit Default(1)
        ,UsuarioCadastro                Int
        ,UsuarioAlteracao               Int
        ,DataCadastro                   DateTime Default(GetDate())
        ,DataAlteracao                  DateTime
    )
go
If Object_Id('Fretter.ContratoTransportador') Is Null     
    Create Table Fretter.ContratoTransportador
    (
         ContratoTransportadorId        Int Identity Constraint PK_Fretter_ContratoTransportadorId Primary Key
        ,TransportadorId                Int
        ,TransportadorCnpjId			Int
        ,TransportadorCnpjCobrancaId	Int
        ,EmpresaId                      Int 
        ,Descricao                      Varchar(256)
        ,QuantidadeTentativas           Int
        ,TaxaTentativaAdicional         Decimal(9,2)
        ,TaxaRetornoRemetente           Decimal(9,2)
        ,VigenciaInicial                Date
        ,VigenciaFinal                  Date
		,FaturaCicloId					Int        
        ,Ativo                          Bit Default(1)
        ,UsuarioCadastro                Int
        ,UsuarioAlteracao               Int
        ,DataCadastro                   DateTime Default(GetDate())
        ,DataAlteracao                  DateTime
    )
Go
If Object_Id('Fretter.ControleProcessoIndicador') Is NULL
	Create Table Fretter.ControleProcessoIndicador
	(
		ControleProcessoIndicadorId			Int Identity Constraint PK_ControleProcessoIndicadorId Primary key 
		,ConciliacaoInicialId				Bigint
		,ConciliacaoFinalId					Bigint
		,QtdConciliacao						Int
		,QtdConciliacaoPendente				Int
		,DataInicio							Datetime
		,DataTermino						Datetime
		,Duracao							Int
		,DataFinalizacaoMinima				Datetime
		,DataFinalizacaoMaxima				Datetime
		,Processado							Bit Constraint DF_ControleProcessoIndicador_Processado Default(0)
		,Sucesso							Bit Constraint DF_ControleProcessoIndicador_Sucesso Default(0)
		,Mensagem							Varchar(1024)
		,Ativo								Bit Constraint DF_ControleProcessoIndicador_Ativo Default(1)
	)
Go
If Object_Id('Fretter.IndicadorConciliacao') Is Null    
    Create Table Fretter.IndicadorConciliacao
    (
	    IndicadorConciliacaoId		Int Identity Constraint PK_Fretter_IndicadorConciliacaoId Primary Key
	    ,Data						Date
	    ,EmpresaId					Int
	    ,TransportadorId			Int
	    ,TransportadorCnpjId		Int
	    ,QtdEntrega					Int Constraint DF_Fretter_IndicadorConciliacao_QtdEntrega Default(0) 
	    ,QtdCte						Int Constraint DF_Fretter_IndicadorConciliacao_QtdCte Default(0)
	    ,QtdSucesso					Int Constraint DF_Fretter_IndicadorConciliacao_QtdSucesso Default(0)
	    ,QtdErro					Int Constraint DF_Fretter_IndicadorConciliacao_QtdErro Default(0)
	    ,QtdDivergencia				Int Constraint DF_Fretter_IndicadorConciliacao_QtdDivergencia Default(0)
	    ,QtdDivergenciaPeso			Int Constraint DF_Fretter_IndicadorConciliacao_QtdDivergenciaPeso Default(0)
	    ,QtdDivergenciaTarifa		Int Constraint DF_Fretter_IndicadorConciliacao_QtdDivergenciaTarifa Default(0)
	    ,ValorCustoFreteEstimado	Decimal(10,2) Constraint DF_Fretter_IndicadorConciliacao_ValorCustoFreteEsperado Default(0)
	    ,ValorCustoFreteReal		Decimal(10,2) Constraint DF_Fretter_IndicadorConciliacao_ValorCustoFreteReal Default(0)
		,ValorTarifaPesoEstimado	Decimal(10,2) Constraint DF_Fretter_IndicadorConciliacao_ValorTarifaPesoEstimado Default(0)
	    ,ValorTarifaPesoReal		Decimal(10,2) Constraint DF_Fretter_IndicadorConciliacao_ValorTarifaPesoReal Default(0)
	    ,DataProcessamento			Datetime Constraint DF_Fretter_IndicadorConciliacao_DataProcessamento Default(Getdate())
	    ,Ativo						Bit Constraint DF_Fretter_IndicadorConciliacao_Ativo Default(1)
    )
Go
If Object_Id('Fretter.ImportacaoCteComposicao') Is Null  
Begin
	CREATE TABLE [Fretter].[ImportacaoCteComposicao](
		[ImportacaoCteComposicaoId] [int] IDENTITY(1,1) NOT NULL,
		[ImportacaoCteId] [int] NULL,
		[Nome] [varchar](128) NULL,
		[Valor] [decimal](8, 2) NULL,
		[ConfiguracaoCteTipoId] [int] NULL,
	PRIMARY KEY CLUSTERED 
	(
		[ImportacaoCteComposicaoId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]	

	ALTER TABLE [Fretter].[ImportacaoCteComposicao]  WITH CHECK ADD FOREIGN KEY([ConfiguracaoCteTipoId])
	REFERENCES [Fretter].[ConfiguracaoCteTipo] ([ConfiguracaoCteTipoId])	

	ALTER TABLE [Fretter].[ImportacaoCteComposicao]  WITH CHECK ADD FOREIGN KEY([ImportacaoCteId])
	REFERENCES [Fretter].[ImportacaoCte] ([ImportacaoCteId])
End
