Create Or Alter Procedure Fretter.ReProcessaConciliacao
As Begin
Set Nocount On;

	Declare                    
	    @Sql			NVarchar(4000)
	   ,@BatchSize		Int        
	   ,@IDI			Bigint
	   ,@IDF			Bigint
	   ,@MaxId			Bigint
	   
	Drop Table If Exists #ImportacaoProcessar 
	Select 
		 Id = Identity(Int, 1, 1)
		,ImportacaoCteId = Convert(Int, ImportacaoCteId)
		,ValorPrestacaoServico
		,DataEmissao
	Into #ImportacaoProcessar
	From Fretter.ImportacaoCte (nolock)
	Where Processado = 0
		And DataEmissao >= DATEADD(Day, -30, Getdate())
	
	Create Clustered Index IDX_ID On #ImportacaoProcessar (Id, ImportacaoCteId)
	
	Select 
		 @IDI			= 1
		,@BatchSize		= 1000
		,@MaxId			= Max(Id)
	From #ImportacaoProcessar
	
	While @IDI <= @MaxId
	Begin
		
		Set @IDF = Case When @IDI + @BatchSize > @MaxId Then @MaxId Else @IDI + @BatchSize End
	
		Drop Table If Exists #ImportacaoCte
	
		Select 
			 I.ImportacaoCteId
			,I.TipoAmbiente
			,I.ImportacaoArquivoId
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
		Into #ImportacaoCte
		From Fretter.ImportacaoCte		I (nolock)
		Join Fretter.ImportacaoArquivo	A (Nolock) On I.ImportacaoArquivoId = A.ImportacaoArquivoId
		Where I.ImportacaoCteId In (Select ImportacaoCteId From #ImportacaoProcessar Where Id Between @IDI And @IDF)
	
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
		
	
		Drop Table If Exists #ConciliacaoFinal
		
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
		Select Distinct
			 ImportacaoCteId			= I.ImportacaoCteId
			,EmpresaId					= E.Id_Empresa
			,EntregaId					= E.Cd_Id
			,TipoCte					= I.TipoCte
			,I.Chave
			,I.ChaveComplementar		
			,TransportadorId			= E.Id_Transportador
			,TransportadorCnpjId		= E.Id_TransportadorCnpj
			,ValorCustoFrete			= E.Vl_CustoFrete --validar isnull
			,ValorCustoReal				= I.ValorPrestacaoServico 
			,PossuiDivergenciaPeso		= Case When CC.Quantidade <> Convert(Decimal(8,4), ISNULL(D.Vl_PesoConsiderado,0)) Then 1 Else 0 End --verificar tipo de dados
			,PossuiDivergenciaTarifa	= Case When I.ValorPrestacaoServico - E.Vl_CustoFrete <> 0 Then 1 Else 0 End --validar
			,ConciliacaoStatusId		= Case
											When C.RecotaPesoTransportador = 1 Then 4
											When C.PermiteTolerancia = 1 And C.ToleranciaTipoId = 2 And E.Vl_CustoFrete Between (I.ValorPrestacaoServico - C.ToleranciaInferior) And (I.ValorPrestacaoServico + c.ToleranciaSuperior) Then 2 
											When C.PermiteTolerancia = 1 And C.ToleranciaTipoId = 1 And E.Vl_CustoFrete Between (I.ValorPrestacaoServico - (I.ValorPrestacaoServico * (C.ToleranciaInferior/100))) And (I.ValorPrestacaoServico + (I.ValorPrestacaoServico * (C.ToleranciaSuperior/100))) Then 2 
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
		Into #ConciliacaoFinal	
		From #ImportacaoCte								I	(nolock)	
		Inner Join Fretter.ImportacaoCteNotaFiscal		N	(nolock) On N.ImportacaoCteId = I.ImportacaoCteId
		Inner Join dbo.Tb_Edi_Entrega					E	(nolock) On E.Cd_Danfe = N.Chave And I.EmpresaId = E.Id_Empresa And I.TransportadorId = E.Id_Transportador And I.DataEmissao >= E.Dt_EmissaoNF -- Alterar Tabela
		Inner Join Fretter.ContratoTransportador		C	(nolock) On C.TransportadorCnpjId = I.TransportadorCnpjId And C.EmpresaId = I.EmpresaId 
		Left Join CargaMax								CC	(nolock) On CC.ImportacaoCteId = I.ImportacaoCteId
		Left Join dbo.Tb_Edi_EntregaDetalhe			    D	(Nolock) On D.Id_Entrega = E.Cd_Id
		Where C.Ativo = 1 And I.DataEmissao Between C.VigenciaInicial And C.VigenciaFinal
			And I.ProcessaConciliacao = 1
			And I.MicroServicoId = C.MicroServicoId 
		
		Drop Table If Exists #ConcilicaoTemp
		Drop Table If Exists #ConciliacaoFinalEntregaUnica
		Drop Table If Exists #ConciliacaoFinalCteUnica

		Create Table #ConcilicaoTemp
		(	
			ActionNome				Varchar(256) Null
			,ConciliacaoId			Bigint Primary Key NOT NULL		
			,ImportacaoCteId		Int NULL
		)	

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
		Into #ConciliacaoFinalEntregaUnica
		From TabelaEntregaUnica 
		Where Id = 1

		;With TabelaCteUnica As
		(
			Select
				Id					= ROW_NUMBER() OVER (PARTITION BY EntregaId ORDER BY JsonValoresRecotacao Desc)
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
		Into #ConciliacaoFinalCteUnica
		From TabelaCteUnica 
		Where Id = 1
		
		--Deleta caso já exista conciliacao processada na Tabela Final--
		Delete Ct 
		From #ConciliacaoFinalCteUnica	Ct
		Join Fretter.Conciliacao		Co (Nolock) On Ct.EntregaId = Co.EntregaId 
		Where Co.ConciliacaoStatusId Not IN (1)

		Merge Fretter.Conciliacao			As D
		Using #ConciliacaoFinalCteUnica		As O On O.EntregaId = D.EntregaId And O.TipoCte = 0	
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
	
		Set @IDI = @IDI + @BatchSize
	
	End

End