Create Or Alter Procedure Fretter.GetConciliacaoRecotacao
AS Begin
Set Nocount On;

	Declare @BatchSize	Int = 1000		
	Declare @ConciliacaoRecotacao Table
	(
		ConciliacaoRecotacaoId	Bigint
		,ConciliacaoId			Bigint
		,EntregaId				Bigint
		,EmpresaId				Int
		,MicroServicoId			Int
		,CanalId				Int
		,CanalVenda				Varchar(512)
		,CanalCnpj				Varchar(64)
		,CodigoPedido			Varchar(256)
		,CepOrigem				Varchar(16)
		,CepDestino				Varchar(16)
		,CodigoSku				Varchar(32)
		,CodigoItem				Varchar(32)
		,Valor					Decimal(9,2)
		,Altura					Decimal(12,4)
		,Largura				Decimal(12,4)
		,Comprimento			Decimal(12,4)
		,Peso					Decimal(12,4)
		,PesoConsiderado		Decimal(12,4)
		,Quantidade				Int	
		,Token					Varchar(512)
	)

	Declare @RecotacaoTemp Table
	(
		ConciliacaoRecotacaoId	Bigint
		,ConciliacaoId			Bigint	
	)	

	Insert Into Fretter.ConciliacaoRecotacao
	(
		ConciliacaoId		
		,ValorCustoFrete	
		,ValorCustoAdicional
		,ValorCustoReal				
		,JsonValoresRecotacao
		,JsonValoresCte			
	)
	Output Inserted.ConciliacaoRecotacaoId,Inserted.ConciliacaoId
	Into @RecotacaoTemp
	Select Top(@BatchSize) 
		Co.ConciliacaoId		
		,Co.ValorCustoFrete	
		,Co.ValorCustoAdicional
		,Co.ValorCustoReal			
		,Co.JsonValoresRecotacao
		,Co.JsonValoresCte		
	From Fretter.Conciliacao	Co(Nolock)
	Join dbo.Tb_Edi_Entrega		Et(Nolock) On Co.EntregaId = Et.Cd_Id	
	Where ConciliacaoStatusId = 4
		And Not Exists (Select Top 1 1 From Fretter.ConciliacaoRecotacao Cc(Nolock) Where Cc.ConciliacaoId = Co.ConciliacaoId And Cc.Processado = 0)
	Order By Co.ConciliacaoId Asc

	INSERT INTO @RecotacaoTemp
	(ConciliacaoRecotacaoId, ConciliacaoId)	
	SELECT 
		top (CONVERT(INT, @BatchSize * 0.1))
		ConciliacaoRecotacaoId,
		ConciliacaoId
	FROM Fretter.ConciliacaoRecotacao
	WHERE Processado = 0
		  and DataCadastro between DATEADD(DAY, -40, CONVERT(DATE, GETDATE())) AND  CONVERT(DATE, GETDATE())
	
	DROP TABLE IF EXISTS #ConciliacaoPesoCte

	;With TabelaPeso As
	(
		Select Rt.ConciliacaoRecotacaoId
			   ,Chave = Rp.[key]
			   ,Valor = Rp.[value]			   
		From @RecotacaoTemp				  Rt
		Join Fretter.ConciliacaoRecotacao Re(Nolock) On Rt.ConciliacaoRecotacaoId = Re.ConciliacaoRecotacaoId
		Cross Apply OpenJson(Re.JsonValoresCte)	Rp			
	)
	Select 
		Tp.ConciliacaoRecotacaoId
		,ValorPeso =  ISNULL(TRY_CAST(Json_Value(Tp.Valor,'$.valor') AS decimal(9,4)),0)
	Into #ConciliacaoPesoCte
	From TabelaPeso					  Tp 
	Join Fretter.ConciliacaoRecotacao Re(Nolock) On Tp.ConciliacaoRecotacaoId = Re.ConciliacaoRecotacaoId
	Where Json_Value(Tp.Valor,'$.chave') In (Select Chave From Fretter.ConfiguracaoCteTipo (Nolock) Where ConfiguracaoCteTipoId In (1))	
	
	;With RecotacaoPesoMaior As
	(
		Select 
			ConciliacaoRecotacaoId
			,PesoMaior		= MAX(ValorPeso)
		From #ConciliacaoPesoCte
		Group By ConciliacaoRecotacaoId
	)
	Update Re
	Set ValorPesoCte = Tp.PesoMaior
	From RecotacaoPesoMaior			  Tp 
	Join Fretter.ConciliacaoRecotacao Re(Nolock) On Tp.ConciliacaoRecotacaoId = Re.ConciliacaoRecotacaoId

	Insert Into @ConciliacaoRecotacao
	(
		ConciliacaoRecotacaoId	
		,ConciliacaoId			
		,EntregaId				
		,EmpresaId	
		,MicroServicoId
		,CanalId				
		,CanalVenda				
		,CanalCnpj				
		,CodigoPedido			
		,CepOrigem				
		,CepDestino				
		,CodigoSku	
		,CodigoItem
		,Valor					
		,Altura					
		,Largura				
		,Comprimento			
		,Peso					
		,PesoConsiderado		
		,Quantidade				
		,Token
	)
	Select Distinct
		ConciliacaoRecotacaoId	= Rt.ConciliacaoRecotacaoId
		,ConciliacaoId			= Rt.ConciliacaoId
		,EntregaId				= Co.EntregaId
		,EmpresaId				= Et.Id_Empresa
		,MicroServicoId			= Et.Id_MicroServico
		,CanalId				= Et.Id_Canal
		,CanalVenda				= Ca.Ds_CanalVenda
		,CanalCnpj				= Cc.Cd_CnpjUnico
		,CodigoPedido			= Et.Cd_Pedido
		,CepOrigem				= Et.Cd_CepOrigem
		,CepDestino				= Et.Cd_CepDestino
		,CodigoSku				= ISNULL(ISNULL(Ed.Cd_Sku,Ei.Cd_Sku),'1234')
		,CodigoItem				= ISNULL(Ei.Cd_Item,'12345')
		,Valor					= ISNULL(Ed.Vl_Declarado, Ei.Vl_PrecoItem)
		,Altura					= ISNULL(Ed.Vl_Altura,	Ei.Vl_Altura)
		,Largura				= ISNULL(Ed.Vl_Largura,	Ei.Vl_Largura)
		,Comprimento			= ISNULL(Ed.Vl_Comprimento,	Ei.Vl_Comprimento)
		,Peso					= ISNULL(Cr.ValorPesoCte,ISNULL(ISNULL(Ed.Vl_Peso,Ei.Vl_PesoCubico),1))
		,PesoConsiderado		= ISNULL(Cr.ValorPesoCte,ISNULL(Ed.Vl_PesoConsiderado,0))
		,Quantidade				= Case When Et.Id_Empresa = 33915 Then 1 Else  ISNULL(Ei.Qtd_Itens,1) End
		,Token					= ISNULL(Eo.Id_Token,Ea.Id_TokenConsulta)
	From @RecotacaoTemp					Rt
	Join Fretter.Conciliacao			Co(Nolock) On Rt.ConciliacaoId = Co.ConciliacaoId
	Join Fretter.ConciliacaoRecotacao	Cr(Nolock) On Rt.ConciliacaoRecotacaoId = Cr.ConciliacaoRecotacaoId
	Join dbo.Tb_Edi_Entrega				Et(Nolock) On Co.EntregaId = Et.Cd_Id 
	Join dbo.Tb_Adm_CanalVenda			Ca(Nolock) ON Et.Id_CanalVenda = Ca.Cd_Id
	Join dbo.Tb_Adm_Canal				Cc(Nolock) ON Et.Id_Canal = Cc.Cd_Id
	Left Join dbo.Tb_Edi_EntregaItem	Ei(Nolock) On Et.Cd_Id = Ei.Id_Entrega
	Left Join dbo.Tb_Edi_EntregaDetalhe	Ed(Nolock) ON Et.Cd_Id = Ed.Id_Entrega
	Left Join dbo.Tb_Adm_EmpresaToken	Eo(Nolock) On Et.Id_Empresa = Eo.Id_Empresa And Eo.Fl_Ativo = 1 And Eo.Fl_Padrao = 1
	Left Join dbo.Tb_Adm_Empresa		Ea(Nolock) On Et.Id_Empresa = Ea.Cd_Id

	Select Distinct
		ConciliacaoRecotacaoId	
		,ConciliacaoId			
		,EntregaId				
		,EmpresaId			 
		,MicroServicoId     
		,CanalId			
		,CanalVenda			
		,CanalCnpj			
		,CodigoPedido			
		,CepOrigem			
		,CepDestino			
		,CodigoSku	
		,CodigoItem
		,Valor					
		,Altura					
		,Largura				
		,Comprimento		 	
		,Peso				 
		,PesoConsiderado		
		,Quantidade				
		,TipoServico			= 'Entrega'	 
		,Token				 
	From @ConciliacaoRecotacao
End