Create Or Alter Procedure Fretter.ProcessaConciliacao (@Batchsize Int = 10000)
As Begin

	Declare                
	    @DataI			Datetime          
	   ,@DataF			Datetime          
	   ,@Descricao		Varchar(4000)          
	   ,@Duracao		Int          
	   ,@Quantidade		Int          
	   ,@CteIdInicial	BigInt          
	   ,@CteIdFinal		BigInt           
	   ,@ControleId		Int            
	   
	If Exists(Select Top 1 1 From Fretter.ControleProcessoCte (nolock) Where Processado = 0 And DataF is null and DATEDIFF(Minute, DataI, Getdate()) > 40)
	Begin
		Select 
			 @CteIdInicial	= CteIdInicial
			,@CteIdFinal	= CteIdFinal
			,@ControleId	= ControleProcessoCteId
			,@DataI			= Getdate()
		From Fretter.ControleProcessoCte (nolock)
		Where Processado = 0 And DataF is null and DATEDIFF(Minute, DataI, Getdate()) > 40
	
		End
	Else
	Begin
		If Exists(Select Top 1 1 From Fretter.ControleProcessoCte (nolock) Where Processado = 0 And DataF is null)
		Begin
			Print 'Ja existe uma instancia sendo executada.'          
			Return          
		End
	
		Select @CteIdInicial	= Isnull(Max(CteIdFinal),0)
			,@DataI				= Getdate()
		From Fretter.ControleProcessoCte (nolock)
		
		Select @CteIdFinal		= Max(ImportacaoCteId)
		From Fretter.ImportacaoCte (nolock)
		Where ImportacaoCteId >= @CteIdInicial
		
		Set @CteIdFinal = Case When @CteIdFinal - @CteIdInicial >= @Batchsize Then @CteIdInicial + @Batchsize
							   When @CteIdFinal - @CteIdInicial = 0			  Then 0
							   When @CteIdFinal - @CteIdInicial < @BatchSize  Then @CteIdFinal
						  End
		
		If @CteIdFinal = 0
		Begin
			Print 'Nenhum Registro a Processar'
			Return
		End
	
		Set @CteIdInicial = @CteIdInicial + 1
	
		Insert Into Fretter.ControleProcessoCte
		(
			 DataI
			,Quantidade
			,CteIdInicial
			,CteIdFinal
			,Duracao
			,Processado
		)
		Values
		(
			 @DataI
			,0
			,@CteIdInicial
			,@CteIdFinal
			,0
			,0
		)
		Set @ControleId = SCOPE_IDENTITY()
	End

	Declare @SQL NVarchar(4000)

	Drop Table IF Exists #ImportacaoCte
	Create Table #ImportacaoCte
	(
		ImportacaoCteId					Int NOT NULL
		,TipoAmbiente					Int NULL
		,ImportacaoArquivoId			Int NULL
		,ImportacaoArquivoTipoItemId	Int NULL
		,TipoCte						Int NULL
		,TipoServico					Int NULL
		,Chave							Varchar(64) NULL
		,Codigo							Varchar(16) NULL
		,Numero							Varchar(64) NULL
		,DigitoVerificador				Int NULL
		,Serie							Varchar(8) NULL
		,DataEmissao					Date NULL
		,ValorPrestacaoServico			Decimal(9,2) NULL
		,Ativo							Bit NULL
		,Processado						Bit NULL
		,CNPJTransportador				Varchar(14) NULL
		,CNPJTomador					Varchar(14) NULL
		,CNPJEmissor					Varchar(14) NULL
		,ChaveComplementar				Varchar(64) NULL
		,JsonComposicaoValores			Varchar(max) NULL
		,ProcessaConciliacao			Bit NULL
		,EmpresaId						Int NULL
		,TransportadorId				Int NULL
		,MicroServicoId					Int NULL
		,TransportadorCnpjId			Int NULL
		,ConciliacaoTipoId				Int NULL
	)

	Set @SQL = 
	N'
	Select Distinct
		I.ImportacaoCteId
		,I.TipoAmbiente
		,I.ImportacaoArquivoId
		,A.ImportacaoArquivoTipoItemId
		,I.TipoCte
		,I.TipoServico
		,I.Chave
		,I.Codigo
		,I.Numero
		,I.DigitoVerificador
		,I.Serie
		,DataEmissao					= Cast(I.DataEmissao As date)
		,I.ValorPrestacaoServico
		,I.Ativo
		,I.Processado
		,I.CNPJTransportador
		,I.CNPJTomador
		,I.CNPJEmissor
		,I.ChaveComplementar
		,I.JsonComposicaoValores		
		,ProcessaConciliacao			= Cast(1 As bit)
		,A.EmpresaId
		,TransportadorId				= CAST(0 As int)		
		,MicroServicoId					= CAST(0 As int)	
		,TransportadorCnpjId			= CAST(0 As int)
		,ConciliacaoTipoId				= T.ConciliacaoTipoId
	From Fretter.ImportacaoCte					I (Nolock)
	Join Fretter.ImportacaoArquivo				A (Nolock) On I.ImportacaoArquivoId = A.ImportacaoArquivoId
	Join Fretter.ImportacaoArquivoTipoItem		T (Nolock) ON A.ImportacaoArquivoTipoItemId = T.ImportacaoArquivoTipoItemId
	Where I.ImportacaoCteId Between ' + Convert(NVarchar(10), @CteIdInicial) + ' And ' + Convert(Nvarchar(10), @CteIdFinal) + '
		And I.Processado = 0'

	Insert Into #ImportacaoCte
	(
		 ImportacaoCteId
		,TipoAmbiente
		,ImportacaoArquivoId
		,ImportacaoArquivoTipoItemId
		,TipoCte
		,TipoServico
		,Chave
		,Codigo
		,Numero
		,DigitoVerificador
		,Serie
		,DataEmissao
		,ValorPrestacaoServico
		,Ativo
		,Processado
		,CNPJTransportador
		,CNPJTomador
		,CNPJEmissor
		,ChaveComplementar
		,JsonComposicaoValores
		,ProcessaConciliacao
		,EmpresaId
		,TransportadorId
		,MicroServicoId
		,TransportadorCnpjId
		,ConciliacaoTipoId
	)
	Exec Sp_ExecuteSql @SQL,
	N'@PCteIdInicial Int
	 ,@PCteIdFinal Int'
	 ,@PCteIdInicial = @CteIdInicial
	 ,@PCteIdFinal = @CteIdFinal

	Create Clustered Index IDX_ImportacaoCteId On #ImportacaoCte (ImportacaoCteId)

	--Remove do Lote Caso possua Complementar Tipo 3 Substituicao	
	Update Ic
	Set ProcessaConciliacao = 0
	From #ImportacaoCte Ic 
	Where Ic.Chave In (Select ChaveComplementar From #ImportacaoCte It Where It.TipoCte = 3 And It.ChaveComplementar Is Not NULL)
		And Ic.TipoCte Not IN (3)

	Update Ic
	Set TransportadorId	= Cd.Id_Transportador
	From #ImportacaoCte				Ic 
	Join Tb_Adm_TransportadorCnpj	Cd(Nolock) on Ic.CNPJTransportador = Cd.Cd_Cnpj

	Drop Table If Exists #ConciliacaoFinal
	
	Update I
	Set MicroServicoId			= E.Id_MicroServico
		,TransportadorCnpjId	= E.Id_TransportadorCnpj
	From #ImportacaoCte								I	(nolock)	
	Inner Join Fretter.ImportacaoCteNotaFiscal		N	(nolock) On N.ImportacaoCteId = I.ImportacaoCteId
	Inner Join dbo.Tb_Edi_Entrega					E	(nolock) On E.Cd_Danfe = N.Chave And I.EmpresaId = E.Id_Empresa And I.TransportadorId = E.Id_Transportador And I.DataEmissao >= E.Dt_EmissaoNF

	Update I
	Set MicroServicoId			= E.Id_MicroServico
		,TransportadorCnpjId	= E.Id_TransportadorCnpj
	From #ImportacaoCte								I	(nolock)	
	Inner Join Fretter.ImportacaoCteNotaFiscal		N	(nolock) On N.ImportacaoCteId = I.ImportacaoCteId
	Inner Join dbo.Tb_Edi_Entrega					E	(nolock) On E.Cd_NotaFiscal = N.NumeroNF AND E.Cd_Serie = N.SerieNF And I.EmpresaId = E.Id_Empresa And I.TransportadorId = E.Id_Transportador And I.DataEmissao >= E.Dt_EmissaoNF
	Inner Join dbo.Tb_Adm_Canal						CA  (nolock) On CA.Cd_Id = E.Id_Canal and CA.Cd_Cnpj = N.CNPJEmissorNF
	Where I.MicroServicoId Is NULL OR I.MicroServicoId = 0
		

	--Não remover condição : plano de execução da query abaixo se perde se colocar no filtro ProcessaConciliacao = 1
	Delete From #ImportacaoCte Where ProcessaConciliacao = 0
	Drop Table If Exists #ConciliacaoFinal
		
	Create Table #ConciliacaoFinal 
	(
		ImportacaoCteId				Int
		,EmpresaId					Int 
		,EntregaId					Int 
		,TipoCte					Int
		,Chave						Varchar(64)
		,ChaveComplementar			Varchar(64)
		,TransportadorId			Int
		,TransportadorCnpjId		Int
		,ValorCustoFrete			Decimal(18,4)
		,ValorCustoReal				Decimal(9,2)
		,PossuiDivergenciaPeso		Bit
		,PossuiDivergenciaTarifa	Bit
		,ConciliacaoStatusId		Int
		,Ativo						Bit
		,UsuarioCadastro			Varchar(200)
		,UsuarioAlteracao			Varchar(200)
		,DataCadastro				Datetime
		,DataAlteracao				Datetime
		,DataImportacao				Datetime
		,ContratoQtd				Int
		,TaxaRetornoRemetente		Decimal(9,2)
		,TaxaTentativaAdicional		Decimal(9,2)
		,DataEmissao				Datetime
		,JsonValoresCte				Varchar(max)
		,JsonValoresRecotacao		Varchar(max)
		,ConciliacaoTipoId			Int
	)	

	;With CargaMax As
	(
		Select 
			 I.ImportacaoCteId
			,Quantidade = Max(CC.Quantidade)
		From #ImportacaoCte				I  (Nolock) 
		Join Fretter.ImportacaoCteCarga	CC (nolock) On CC.ImportacaoCteId = I.ImportacaoCteId 
		Where CC.ConfiguracaoCteTipoId = 1
		Group By I.ImportacaoCteId
	)
	Insert Into #ConciliacaoFinal
	(
		ImportacaoCteId
		,EmpresaId 
		,EntregaId
		,TipoCte
		,Chave
		,ChaveComplementar
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
		,DataImportacao
		,ContratoQtd
		,TaxaRetornoRemetente
		,TaxaTentativaAdicional
		,DataEmissao
		,JsonValoresCte
		,JsonValoresRecotacao
		,ConciliacaoTipoId
	)
	Select Distinct
		 ImportacaoCteId			= I.ImportacaoCteId
		,EmpresaId					= E.Id_Empresa
		,EntregaId					= E.Cd_Id
		,TipoCte					= I.TipoCte
		,Chave						= I.Chave
		,ChaveComplementar			= I.ChaveComplementar		
		,TransportadorId			= E.Id_Transportador
		,TransportadorCnpjId		= E.Id_TransportadorCnpj
		,ValorCustoFrete			= E.Vl_CustoFrete --validar isnull
		,ValorCustoReal				= I.ValorPrestacaoServico 
		,PossuiDivergenciaPeso		= Case When CC.Quantidade <> Convert(Decimal(8,4), ISNULL(D.Vl_PesoConsiderado,0)) Then 1 Else 0 End --verificar tipo de dados
		,PossuiDivergenciaTarifa	= Case When I.ValorPrestacaoServico - E.Vl_CustoFrete <> 0 Then 1 Else 0 End --validar
		,ConciliacaoStatusId		= Case
										When C.RecotaPesoTransportador = 1 Then 4
										When C.PermiteTolerancia = 1 And C.ToleranciaTipoId = 2 And I.ConciliacaoTipoId = 1 And E.Vl_CustoFrete Between (I.ValorPrestacaoServico - C.ToleranciaInferior) And (I.ValorPrestacaoServico + c.ToleranciaSuperior) Then 2 
										When C.PermiteTolerancia = 1 And C.ToleranciaTipoId = 1 And I.ConciliacaoTipoId = 1 And E.Vl_CustoFrete Between (I.ValorPrestacaoServico - (I.ValorPrestacaoServico * (C.ToleranciaInferior/100))) And (I.ValorPrestacaoServico + (I.ValorPrestacaoServico * (C.ToleranciaSuperior/100))) Then 2 
										When C.PermiteTolerancia = 1 And C.ToleranciaTipoId In (1,2) And I.ConciliacaoTipoId In (2,3) Then 1
										When I.ValorPrestacaoServico - E.Vl_CustoFrete = 0 Then 2
									  Else 3 End
		,Ativo						= 1
		,UsuarioCadastro			= ''
		,UsuarioAlteracao			= ''
		,DataCadastro				= Getdate()
		,DataAlteracao				= Getdate()
		,DataImportacao				= E.Dt_Importacao
		,ContratoQtd				= C.QuantidadeTentativas   
		,TaxaRetornoRemetente		= C.TaxaRetornoRemetente
		,TaxaTentativaAdicional		= C.TaxaTentativaAdicional
		,DataEmissao				= I.DataEmissao
		,JsonValoresCte				= I.JsonComposicaoValores
		,JsonValoresRecotacao		= D.Ds_JsonComposicaoValoresCotacao
		,ConciliacaoTipoId			= I.ConciliacaoTipoId
	From #ImportacaoCte								I	(nolock)	
	Inner Join Fretter.ImportacaoCteNotaFiscal		N	(nolock) On N.ImportacaoCteId = I.ImportacaoCteId
	Inner Join dbo.Tb_Edi_Entrega					E	(nolock) On E.Cd_Danfe = N.Chave And I.EmpresaId = E.Id_Empresa And I.TransportadorId = E.Id_Transportador And I.DataEmissao >= E.Dt_EmissaoNF -- Alterar Tabela	
	Inner Join Fretter.ContratoTransportador		C	(nolock) On C.TransportadorCnpjId = I.TransportadorCnpjId And C.EmpresaId = I.EmpresaId 
	Left Join CargaMax								CC	(nolock) On CC.ImportacaoCteId = I.ImportacaoCteId
	Left Join dbo.Tb_Edi_EntregaDetalhe			    D	(Nolock) On D.Id_Entrega = E.Cd_Id
	Where C.Ativo = 1 And I.DataEmissao Between C.VigenciaInicial And C.VigenciaFinal
		--And I.ProcessaConciliacao = 1
		And I.MicroServicoId = C.MicroServicoId 
	Union All
	Select Distinct
		 ImportacaoCteId			= I.ImportacaoCteId
		,EmpresaId					= E.Id_Empresa
		,EntregaId					= E.Cd_Id
		,TipoCte					= I.TipoCte
		,Chave						= I.Chave
		,ChaveComplementar			= I.ChaveComplementar		
		,TransportadorId			= E.Id_Transportador
		,TransportadorCnpjId		= E.Id_TransportadorCnpj
		,ValorCustoFrete			= E.Vl_CustoFrete --validar isnull
		,ValorCustoReal				= I.ValorPrestacaoServico 
		,PossuiDivergenciaPeso		= Case When CC.Quantidade <> Convert(Decimal(8,4), ISNULL(D.Vl_PesoConsiderado,0)) Then 1 Else 0 End --verificar tipo de dados
		,PossuiDivergenciaTarifa	= Case When I.ValorPrestacaoServico - E.Vl_CustoFrete <> 0 Then 1 Else 0 End --validar
		,ConciliacaoStatusId		= Case
										When C.RecotaPesoTransportador = 1 Then 4
										When C.PermiteTolerancia = 1 And C.ToleranciaTipoId = 2 And I.ConciliacaoTipoId = 1 And E.Vl_CustoFrete Between (I.ValorPrestacaoServico - C.ToleranciaInferior) And (I.ValorPrestacaoServico + c.ToleranciaSuperior) Then 2 
										When C.PermiteTolerancia = 1 And C.ToleranciaTipoId = 1 And I.ConciliacaoTipoId = 1 And E.Vl_CustoFrete Between (I.ValorPrestacaoServico - (I.ValorPrestacaoServico * (C.ToleranciaInferior/100))) And (I.ValorPrestacaoServico + (I.ValorPrestacaoServico * (C.ToleranciaSuperior/100))) Then 2 
										When C.PermiteTolerancia = 1 And C.ToleranciaTipoId In (1,2) And I.ConciliacaoTipoId In (2,3) Then 1
										When I.ValorPrestacaoServico - E.Vl_CustoFrete = 0 Then 2
									  Else 3 End
		,Ativo						= 1
		,UsuarioCadastro			= ''
		,UsuarioAlteracao			= ''
		,DataCadastro				= Getdate()
		,DataAlteracao				= Getdate()
		,DataImportacao				= E.Dt_Importacao
		,ContratoQtd				= C.QuantidadeTentativas   
		,TaxaRetornoRemetente		= C.TaxaRetornoRemetente
		,TaxaTentativaAdicional		= C.TaxaTentativaAdicional
		,DataEmissao				= I.DataEmissao
		,JsonValoresCte				= I.JsonComposicaoValores
		,JsonValoresRecotacao		= D.Ds_JsonComposicaoValoresCotacao
		,ConciliacaoTipoId			= I.ConciliacaoTipoId
	From #ImportacaoCte								I	(nolock)	
	Inner Join Fretter.ImportacaoCteNotaFiscal		N	(nolock) On N.ImportacaoCteId = I.ImportacaoCteId
	Inner Join dbo.Tb_Edi_Entrega					E	(nolock) On E.Cd_NotaFiscal = N.NumeroNF and E.Cd_Serie = N.SerieNF And I.EmpresaId = E.Id_Empresa And I.TransportadorId = E.Id_Transportador And I.DataEmissao >= E.Dt_EmissaoNF -- Alterar Tabela	
	Inner Join dbo.Tb_Adm_Canal						CA  (nolock) On CA.Cd_Id = E.Id_Canal and CA.Cd_Cnpj = N.CNPJEmissorNF
	Inner Join Fretter.ContratoTransportador		C	(nolock) On C.TransportadorCnpjId = I.TransportadorCnpjId And C.EmpresaId = I.EmpresaId 
	Left Join CargaMax								CC	(nolock) On CC.ImportacaoCteId = I.ImportacaoCteId
	Left Join dbo.Tb_Edi_EntregaDetalhe			    D	(Nolock) On D.Id_Entrega = E.Cd_Id
	Where C.Ativo = 1 And I.DataEmissao Between C.VigenciaInicial And C.VigenciaFinal
		--And I.ProcessaConciliacao = 1
		And I.MicroServicoId = C.MicroServicoId

	Create Clustered Index IDX_EntregaId On #ConciliacaoFinal (EntregaId)

	Drop Table If Exists #ConcilicaoTemp
	Create Table #ConcilicaoTemp
	(	
		ActionNome				Varchar(256) Null
		,ConciliacaoId			Bigint Primary Key NOT NULL		
		,ImportacaoCteId		Int NULL
	)	

	Drop Table If Exists #ConciliacaoFinalEntregaUnica

	;With TabelaEntregaUnica As
	(
		Select
			Id					= ROW_NUMBER() OVER (PARTITION BY ImportacaoCteId ORDER BY JsonValoresRecotacao Desc)
			,ImportacaoCteId			
			,EmpresaId					
			,EntregaId					
			,TipoCte					
			,Chave
			,ChaveComplementar		
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
			,DataImportacao				
			,ContratoQtd				
			,TaxaRetornoRemetente		
			,TaxaTentativaAdicional		
			,DataEmissao				
			,JsonValoresCte				
			,JsonValoresRecotacao	
			,ConciliacaoTipoId
		From #ConciliacaoFinal
	)
	Select 
		ImportacaoCteId		
		,EmpresaId				
		,EntregaId				
		,TipoCte				
		,Chave
		,ChaveComplementar		
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
		,DataImportacao			
		,ContratoQtd			
		,TaxaRetornoRemetente	
		,TaxaTentativaAdicional	
		,DataEmissao			
		,JsonValoresCte			
		,JsonValoresRecotacao	
		,ConciliacaoTipoId
	Into #ConciliacaoFinalEntregaUnica
	From TabelaEntregaUnica 
	Where Id = 1

	Drop Table If Exists #ConciliacaoFinalCteUnica

	;With TabelaCteUnica As
	(
		Select
			Id					= ROW_NUMBER() OVER (PARTITION BY EntregaId,ConciliacaoTipoId ORDER BY JsonValoresRecotacao Desc)
			,ImportacaoCteId			
			,EmpresaId					
			,EntregaId					
			,TipoCte					
			,Chave
			,ChaveComplementar		
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
			,DataImportacao				
			,ContratoQtd				
			,TaxaRetornoRemetente		
			,TaxaTentativaAdicional		
			,DataEmissao				
			,JsonValoresCte				
			,JsonValoresRecotacao		
			,ConciliacaoTipoId
		From #ConciliacaoFinalEntregaUnica
	)
	Select 
		ImportacaoCteId		
		,EmpresaId				
		,EntregaId				
		,TipoCte				
		,Chave
		,ChaveComplementar		
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
		,DataImportacao			
		,ContratoQtd			
		,TaxaRetornoRemetente	
		,TaxaTentativaAdicional	
		,DataEmissao			
		,JsonValoresCte			
		,JsonValoresRecotacao	
		,ConciliacaoTipoId
	Into #ConciliacaoFinalCteUnica
	From TabelaCteUnica 
	Where Id = 1

	Merge Fretter.Conciliacao			As D
	Using #ConciliacaoFinalCteUnica		As O On O.EntregaId = D.EntregaId And O.TipoCte = 0 And O.ConciliacaoTipoId = D.ConciliacaoTipoId	
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
		,D.Ativo					= O.Ativo						
		,D.UsuarioCadastro			= O.UsuarioCadastro			
		,D.UsuarioAlteracao			= O.UsuarioAlteracao			
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
		,O.Ativo						
		,O.UsuarioCadastro			
		,O.UsuarioAlteracao			
		,O.DataCadastro				
		,O.DataAlteracao	
		,O.DataEmissao
		,O.JsonValoresRecotacao
		,O.JsonValoresCte
		,O.ImportacaoCteId
		,O.ConciliacaoTipoId
	)
	OUTPUT
		$action As ActionName,
		inserted.ConciliacaoId,
		inserted.ImportacaoCteId
	Into #ConcilicaoTemp;		
	
	--Cte complementar Tipo 1 Acrescenta
	Update F
	Set F.ValorCustoReal = F.ValorCustoReal + C.ValorCustoReal
	From #ConciliacaoFinalCteUnica			C (nolock)
	Inner Join Fretter.Conciliacao			F (nolock) On C.EntregaId = F.EntregaId
	Where C.TipoCte = 1
	
	--Cte complementar Tipo 2 Cancela ou Zera Valor ??
	Update F
	Set F.ValorCustoReal = 0
	From #ConciliacaoFinalCteUnica		C (nolock)
	Inner Join Fretter.Conciliacao		F (nolock) On C.EntregaId = F.EntregaId
	Where C.TipoCte = 2

	Update I
	Set Processado		= 1
		,ConciliacaoId	= T.ConciliacaoId
	From Fretter.ImportacaoCte	I(Nolock)
	Join #ConcilicaoTemp		T(Nolock) On I.ImportacaoCteId = T.ImportacaoCteId				

	Select 
		 @Quantidade	= Count(1)
		,@Duracao		= Datediff(Second, @DataI, Getdate())
	From #ConciliacaoFinal
	
	Update C
	Set  Processado = 1
		,Quantidade = @Quantidade
		,Duracao	= @Duracao
		,DataF		= Getdate()
	From Fretter.ControleProcessoCte C
	Where ControleProcessoCteId = @ControleId


	Select @Quantidade As Quantidade;
End
GO