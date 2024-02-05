CREATE OR ALTER Procedure [Fretter].[ProcessaEntregaReprocessamento]
(
	@DataI			Date
	,@DataF			Date
)
As Begin	
	
	If(DateDiff(Day,@DataI,@DataF) > 10)
	Begin
		RAISERROR('Periodo não pode ser maior que 10 dias', 17, 1);
		RETURN;
	End

	Drop Table If Exists #Conciliacao
	Drop Table If Exists #ConciliacaoFinal
	Drop Table If Exists #ConciliacaoFinalUnicaTemp
	
	Select 
		 EmpresaId					= E.Id_Empresa
		,EntregaId					= E.Cd_Id
		,TransportadorId			= E.Id_Transportador
		,TransportadorCnpjId		= E.Id_TransportadorCnpj
		,MicroServicoId				= E.Id_MicroServico
		,ValorCustoFrete			= E.Vl_CustoFrete --validar isnull
		,ConciliacaoStatusId		= 1
		,Ativo						= 1
		,UsuarioCadastro			= ''
		,UsuarioAlteracao			= ''
		,DataCadastro				= Getdate()
		,DataAlteracao				= ''
		,DataImportacao				= E.Dt_Importacao
		,DataFinalizacao			= E.Dt_Finalizado
		,DataEmissao				= Cast(ISNULL(E.Dt_EmissaoNF,E.Dt_Importacao) As date)
		,Danfe						= E.Cd_Danfe	
		,JsonValoresRecotacao		= D.Ds_JsonComposicaoValoresCotacao
		,RecotaPesoTransportador	= C.RecotaPesoTransportador
		,PermiteTolerancia			= C.PermiteTolerancia
		,ToleranciaInferior			= C.ToleranciaInferior
		,ToleranciaSuperior			= C.ToleranciaSuperior
		,ToleranciaTipoId			= C.ToleranciaTipoId		
		,ContratoQtd				= C.QuantidadeTentativas   
		,TaxaRetornoRemetente		= C.TaxaRetornoRemetente
		,TaxaTentativaAdicional		= C.TaxaTentativaAdicional
		,ValorPesoConsiderado		= D.Vl_PesoConsiderado
		,ConciliacaoTipoId			= 1
	Into #Conciliacao
	From dbo.Tb_Edi_Entrega					 E (Nolock)
	Inner Join Fretter.ContratoTransportador C (Nolock) On C.TransportadorCnpjId = E.Id_TransportadorCnpj And C.Ativo = 1 And E.Id_MicroServico = C.MicroServicoId And E.Id_Empresa = C.EmpresaId And C.VigenciaFinal >= Getdate() --trazer apenas entregas que contenham contrato ativo, Manter como esta para ter os dados para poder ofertar a clientes novos
	Left Join dbo.Tb_Edi_EntregaDetalhe		 D (Nolock) On D.Id_Entrega = E.Cd_Id
	Where E.Dt_Importacao Between @DataI And @DataF

	CREATE TABLE #ConciliacaoFinal
	(
		[ImportacaoCteId]			[int] NOT NULL,
		[EmpresaId]					[int] NOT NULL,
		[EntregaId]					[int] NOT NULL,
		[TipoCte]					[int] NULL,
		[Chave]						[varchar](64) NULL,
		[ChaveComplementar]			[varchar](64) NULL,
		[TransportadorId]			[int] NULL,
		[TransportadorCnpjId]		[int] NOT NULL,
		[ValorCustoFrete]			[decimal](18, 4) NULL,
		[ValorCustoReal]			[decimal](9, 2) NULL,
		[PossuiDivergenciaPeso]		[int] NOT NULL,
		[PossuiDivergenciaTarifa]	[int] NOT NULL,
		[ConciliacaoStatusId]		[int] NOT NULL,				
		[DataCadastro]				[datetime] NOT NULL,
		[DataAlteracao]				[datetime] NOT NULL,
		[DataImportacao]			[datetime] NOT NULL,
		[ContratoQtd]				[int] NULL,
		[TaxaRetornoRemetente]		[decimal](9, 2) NULL,
		[TaxaTentativaAdicional]	[decimal](9, 2) NULL,
		[DataEmissao]				[date] NULL,
		[JsonValoresCte]			[varchar](max) NULL,
		[JsonValoresRecotacao]		[varchar](max) NULL,
		[EntregaNova]				Bit Default(0),
		[ConciliacaoTipoId]			[int] null
	) 

	Insert Into #ConciliacaoFinal
	Select Distinct
		ImportacaoCteId				= C.ImportacaoCteId
		,EmpresaId					= I.EmpresaId
		,EntregaId					= I.EntregaId
		,TipoCte					= C.TipoCte
		,Chave						= C.Chave
		,ChaveComplementar			= C.ChaveComplementar		
		,TransportadorId			= I.TransportadorId
		,TransportadorCnpjId		= I.TransportadorCnpjId
		,ValorCustoFrete			= I.ValorCustoFrete
		,ValorCustoReal				= C.ValorPrestacaoServico 
		,PossuiDivergenciaPeso		= Case When CC.Quantidade <> Convert(Decimal(8,4), ISNULL(I.ValorPesoConsiderado,0)) Then 1 Else 0 End --verificar tipo de dados
		,PossuiDivergenciaTarifa	= Case When C.ValorPrestacaoServico - I.ValorCustoFrete <> 0 Then 1 Else 0 End --validar
		,ConciliacaoStatusId		= Case
										When I.RecotaPesoTransportador = 1 Then 4
										When I.PermiteTolerancia = 1 And I.ToleranciaTipoId = 2 And I.ValorCustoFrete Between (C.ValorPrestacaoServico - I.ToleranciaInferior) And (C.ValorPrestacaoServico + I.ToleranciaSuperior) Then 2 
										When I.PermiteTolerancia = 1 And I.ToleranciaTipoId = 1 And I.ValorCustoFrete Between (C.ValorPrestacaoServico - (C.ValorPrestacaoServico * (I.ToleranciaInferior/100))) And (C.ValorPrestacaoServico + (C.ValorPrestacaoServico * (I.ToleranciaSuperior/100))) Then 2 
										When C.ValorPrestacaoServico - I.ValorCustoFrete = 0 Then 2
									  Else 3 End				
		,DataCadastro				= Getdate()
		,DataAlteracao				= Getdate()
		,DataImportacao				= I.DataImportacao
		,ContratoQtd				= I.ContratoQtd   
		,TaxaRetornoRemetente		= I.TaxaRetornoRemetente
		,TaxaTentativaAdicional		= I.TaxaTentativaAdicional
		,DataEmissao				= I.DataEmissao
		,JsonValoresCte				= C.JsonComposicaoValores
		,JsonValoresRecotacao		= I.JsonValoresRecotacao
		,EntregaNova				= 1
		,ConciliacaoTipoId			= I.ConciliacaoTipoId
	From #Conciliacao								I(nolock)	
	Inner Join Fretter.ImportacaoCteNotaFiscal		N(nolock) On I.Danfe = N.Chave 
	Inner Join Fretter.ImportacaoCte				C(Nolock) On N.ImportacaoCteId = C.ImportacaoCteId
	Inner Join Fretter.ImportacaoArquivo			A(Nolock) On I.EmpresaId = A.EmpresaId And C.ImportacaoArquivoId = A.ImportacaoArquivoId
	Inner Join Fretter.ImportacaoCteCarga		   CC(Nolock) On C.ImportacaoCteId = CC.ImportacaoCteId
	Left Join Fretter.Conciliacao					O(Nolock) On I.EntregaId = O.EntregaId
	Where O.EntregaId Is NULL
		And (C.ProtocoloAutorizacao Is Not NULL Or C.ProtocoloAutorizacao <> '')

	Insert Into #ConciliacaoFinal
	Select Distinct
		ImportacaoCteId				= C.ImportacaoCteId
		,EmpresaId					= I.EmpresaId
		,EntregaId					= I.EntregaId
		,TipoCte					= C.TipoCte
		,Chave						= C.Chave
		,ChaveComplementar			= C.ChaveComplementar		
		,TransportadorId			= I.TransportadorId
		,TransportadorCnpjId		= I.TransportadorCnpjId
		,ValorCustoFrete			= I.ValorCustoFrete
		,ValorCustoReal				= C.ValorPrestacaoServico 
		,PossuiDivergenciaPeso		= Case When CC.Quantidade <> Convert(Decimal(8,4), ISNULL(I.ValorPesoConsiderado,0)) Then 1 Else 0 End --verificar tipo de dados
		,PossuiDivergenciaTarifa	= Case When C.ValorPrestacaoServico - I.ValorCustoFrete <> 0 Then 1 Else 0 End --validar
		,ConciliacaoStatusId		= Case
										When I.RecotaPesoTransportador = 1 Then 4
										When I.PermiteTolerancia = 1 And I.ToleranciaTipoId = 2 And I.ValorCustoFrete Between (C.ValorPrestacaoServico - I.ToleranciaInferior) And (C.ValorPrestacaoServico + I.ToleranciaSuperior) Then 2 
										When I.PermiteTolerancia = 1 And I.ToleranciaTipoId = 1 And I.ValorCustoFrete Between (C.ValorPrestacaoServico - (C.ValorPrestacaoServico * (I.ToleranciaInferior/100))) And (C.ValorPrestacaoServico + (C.ValorPrestacaoServico * (I.ToleranciaSuperior/100))) Then 2 
										When C.ValorPrestacaoServico - I.ValorCustoFrete = 0 Then 2
									  Else 3 End				
		,DataCadastro				= Getdate()
		,DataAlteracao				= Getdate()
		,DataImportacao				= I.DataImportacao
		,ContratoQtd				= I.ContratoQtd   
		,TaxaRetornoRemetente		= I.TaxaRetornoRemetente
		,TaxaTentativaAdicional		= I.TaxaTentativaAdicional
		,DataEmissao				= I.DataEmissao
		,JsonValoresCte				= C.JsonComposicaoValores
		,JsonValoresRecotacao		= I.JsonValoresRecotacao
		,EntregaNova				= 0
		,ConciliacaoTipoId			= I.ConciliacaoTipoId
	From #Conciliacao								I(nolock)	
	Inner Join Fretter.ImportacaoCteNotaFiscal		N(nolock) On I.Danfe = N.Chave 
	Inner Join Fretter.ImportacaoCte				C(Nolock) On N.ImportacaoCteId = C.ImportacaoCteId
	Inner Join Fretter.ImportacaoArquivo			A(Nolock) On I.EmpresaId = A.EmpresaId And C.ImportacaoArquivoId = A.ImportacaoArquivoId
	Inner Join Fretter.ImportacaoCteCarga		   CC(Nolock) On C.ImportacaoCteId = CC.ImportacaoCteId
	Left Join Fretter.Conciliacao					O(Nolock) On I.EntregaId = O.EntregaId And O.ConciliacaoStatusId Not IN (1,4)
	Where O.EntregaId Is NULL
		And (C.ProtocoloAutorizacao Is Not NULL Or C.ProtocoloAutorizacao <> '')


	;With ConciliacaoFinalUnica As
	(
		Select 
			Id					= ROW_NUMBER() OVER(Partition by [EntregaId] ORDER BY [ImportacaoCteId] Desc) 
			,[ImportacaoCteId]			
			,[EmpresaId]					
			,[EntregaId]					
			,[TipoCte]					
			,[Chave]						
			,[ChaveComplementar]			
			,[TransportadorId]			
			,[TransportadorCnpjId]		
			,[ValorCustoFrete]			
			,[ValorCustoReal]			
			,[PossuiDivergenciaPeso]		
			,[PossuiDivergenciaTarifa]	
			,[ConciliacaoStatusId]		
			,[DataCadastro]				
			,[DataAlteracao]				
			,[DataImportacao]			
			,[ContratoQtd]				
			,[TaxaRetornoRemetente]		
			,[TaxaTentativaAdicional]	
			,[DataEmissao]				
			,[JsonValoresCte]			
			,[JsonValoresRecotacao]		
			,[EntregaNova]
			,[ConciliacaoTipoId]
		From #ConciliacaoFinal
	)
	Select 
		[ImportacaoCteId]			
		,[EmpresaId]					
		,[EntregaId]					
		,[TipoCte]					
		,[Chave]						
		,[ChaveComplementar]			
		,[TransportadorId]			
		,[TransportadorCnpjId]		
		,[ValorCustoFrete]			
		,[ValorCustoReal]			
		,[PossuiDivergenciaPeso]		
		,[PossuiDivergenciaTarifa]	
		,[ConciliacaoStatusId]		
		,[DataCadastro]				
		,[DataAlteracao]				
		,[DataImportacao]			
		,[ContratoQtd]				
		,[TaxaRetornoRemetente]		
		,[TaxaTentativaAdicional]	
		,[DataEmissao]				
		,[JsonValoresCte]			
		,[JsonValoresRecotacao]		
		,[EntregaNova]
		,[ConciliacaoTipoId]
	Into #ConciliacaoFinalUnicaTemp
	From ConciliacaoFinalUnica
	Where Id = 1	
	
	Merge Fretter.Conciliacao			As D
	Using #ConciliacaoFinalUnicaTemp	As O
	On O.EntregaId = D.EntregaId And O.TipoCte = 0	
	When Matched
	Then Update
	Set  D.EmpresaId				= O.EmpresaId					
		,D.EntregaId				= O.EntregaId					
		,D.TransportadorId			= O.TransportadorId	
		,D.TransportadorCnpjId		= O.TransportadorCnpjId
		,D.ValorCustoFrete			= O.ValorCustoFrete			
		,D.ValorCustoReal			= O.ValorCustoReal
		,D.ValorCustoAdicional		= Case 
										When D.QuantidadeTentativas > O.ContratoQtd
											Then (D.QuantidadeTentativas - O.ContratoQtd) * O.TaxaTentativaAdicional
										Else 0
									  End --Validar
		,D.PossuiDivergenciaPeso	= O.PossuiDivergenciaPeso		
		,D.PossuiDivergenciaTarifa	= O.PossuiDivergenciaTarifa	
		,D.ConciliacaoStatusId		= O.ConciliacaoStatusId				
		,D.DataCadastro				= O.DataCadastro				
		,D.DataAlteracao			= O.DataAlteracao
		,D.DataEmissao				= O.DataEmissao
		,D.JsonValoresRecotacao		= O.JsonValoresRecotacao
		,D.JsonValoresCte			= O.JsonValoresCte
		,D.ImportacaoCteId			= O.ImportacaoCteId
		,D.ConciliacaoTipoId		= O.ConciliacaoTipoId
		
	When Not Matched And O.TipoCte = 0
	Then Insert 
	(
		 EmpresaId					
		,EntregaId					
		,TransportadorId	
		,TransportadorCnpjId
		,ValorCustoFrete			
		,ValorCustoReal				
		,PossuiDivergenciaPeso		
		,PossuiDivergenciaTarifa	
		,ConciliacaoStatusId		
		,Ativo						
		,UsuarioCadastro			
		,UsuarioAlteracao			
		,DataCadastro				
		,DataAlteracao	
		,DataEmissao
		,JsonValoresRecotacao
		,JsonValoresCte
		,ImportacaoCteId
		,ConciliacaoTipoId
	)
	Values
	(
		 O.EmpresaId					
		,O.EntregaId					
		,O.TransportadorId
		,O.TransportadorCnpjId
		,O.ValorCustoFrete			
		,O.ValorCustoReal				
		,O.PossuiDivergenciaPeso		
		,O.PossuiDivergenciaTarifa	
		,O.ConciliacaoStatusId		
		,1						
		,NULL			
		,NULL			
		,O.DataCadastro				
		,O.DataAlteracao	
		,O.DataEmissao
		,O.JsonValoresRecotacao
		,O.JsonValoresCte
		,O.ImportacaoCteId
		,O.ConciliacaoTipoId
	);

	Select 		
		[QtdEntregaNova]		 = SUM(Case When EntregaNova = 1 Then 1 Else 0 End)
		,[QtdEntregaAtualizacao] = SUM(Case When EntregaNova = 0 Then 1 Else 0 End) 
	From #ConciliacaoFinalUnicaTemp

End
GO