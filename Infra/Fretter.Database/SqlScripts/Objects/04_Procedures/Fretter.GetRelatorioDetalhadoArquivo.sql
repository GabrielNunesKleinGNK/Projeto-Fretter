Create Or Alter Procedure Fretter.GetRelatorioDetalhadoArquivo
(
	@PageSelected		Int = 0
	,@PageSize			Int = 50		
	,@OrderByDirection	NVarchar(4) = 'Asc'
	,@EmpresaId			Int = 33915
	,@DataInicial		DateTime = '2023-01-01'
	,@DataFinal			DateTime = '2023-01-12'
	,@TransportadorId	Int = 0
	,@StatusId			Int = 0
	,@FaturaId			Int = 0
	,@CodigoEntrega		Varchar(100) = Null
	,@CodigoPedido		Varchar(100) = Null
	,@CodigoDanfe		Varchar(44) = Null
)
As Begin
Set Nocount On;
	

If(@CodigoEntrega Is Not NULL Or @CodigoPedido Is Not NULL)
	Select @DataInicial = DATEADD(MONTH,-2,GETDATE()), @DataFinal = GETDATE();

Drop Table If Exists #ConciliacaoRelatorio
Create Table #ConciliacaoRelatorio 
(
	ConciliacaoId				Bigint
	,ImportacaoCteId			Bigint
	,CodigoEntrega				Varchar(100)
	,CodigoPedido				Varchar(100)	
	,Transportador				Varchar(512)	
	,DataCadastro				Datetime
	,DataEmissao				Datetime	
	,DataFinalizacao			Datetime		
	,EntregaPeso				Decimal(10,4)
	,EntregaAltura				Decimal(10,4)
	,EntregaComprimento			Decimal(10,4)
	,EntregaLargura				Decimal(10,4)
	,EntregaValorDeclarado		Decimal(10,4)
	,MicroServicoId				Int
	,CanalId					Int
	,CanalVendaId				Int
	,ValorCustoFrete			Decimal(10,4)
	,ValorCustoAdicional		Decimal(10,4)
	,ValorCustoReal				Decimal(10,4)
	,ValorICMS					Decimal(10,4)
	,ValorGRIS					Decimal(10,4)
	,ValorADValorem				Decimal(10,4)
	,ValorPedagio				Decimal(10,4)
	,ValorFretePeso				Decimal(10,4)
	,ValorTaxaTRT				Decimal(10,4)
	,ValorTaxaTDE				Decimal(10,4)
	,ValorTaxaTDA				Decimal(10,4)
	,ValorTaxaCTe				Decimal(10,4)
	,ValorTaxaRisco				Decimal(10,4)
	,ValorSuframa				Decimal(10,4)
	,PossuiDivergenciaPeso		Bit
	,PossuiDivergenciaTarifa	Bit
	,StatusConciliacao			Varchar(256)
)

Insert Into #ConciliacaoRelatorio
(
	ConciliacaoId			
	,ImportacaoCteId			
	,CodigoEntrega				
	,CodigoPedido				
	,Transportador				
	,ValorCustoFrete			
	,ValorCustoAdicional		
	,ValorCustoReal				
	,PossuiDivergenciaPeso		
	,PossuiDivergenciaTarifa	
	,StatusConciliacao			
	,DataEmissao				
	,DataCadastro				
	,DataFinalizacao				
	,EntregaPeso				
	,EntregaAltura				
	,EntregaLargura				
	,EntregaComprimento			
	,EntregaValorDeclarado		
	,MicroServicoId				
	,CanalId					
	,CanalVendaId				
)

Select 
	ConciliacaoId				= C.ConciliacaoId
	,ImportacaoCteId			= C.ImportacaoCteId
	,CodigoEntrega				= E.Cd_Entrega
	,CodigoPedido				= E.Cd_Pedido
	,Transportador				= T.Ds_NomeFantasia
	,ValorCustoFrete			= C.ValorCustoFrete
	,ValorCustoAdicional		= C.ValorCustoAdicional
	,ValorCustoReal				= C.ValorCustoReal	 	
	,PossuiDivergenciaPeso		= C.PossuiDivergenciaPeso
	,PossuiDivergenciaTarifa	= C.PossuiDivergenciaTarifa
	,StatusConciliacao			= CS.Nome
	,DataEmissao				= C.DataEmissao
	,DataCadastro				= C.DataCadastro
	,DataFinalizacao			= C.DataFinalizacao	
	,EntregaPeso				= Ed.Vl_Peso
	,EntregaAltura				= Ed.Vl_Altura
	,EntregaLargura				= Ed.Vl_Largura
	,EntregaComprimento			= Ed.Vl_Comprimento
	,EntregaValorDeclarado		= Ed.Vl_Declarado
	,MicroServicoId				= E.Id_MicroServico
	,CanalId					= E.Id_Canal
	,CanalVendaId				= E.Id_CanalVenda	
From Fretter.Conciliacao		C  (Nolock) 
Join Fretter.ConciliacaoStatus  CS (Nolock) On C.ConciliacaoStatusId = CS.ConciliacaoStatusId
Join dbo.Tb_Edi_Entrega			E  (Nolock) On C.EntregaId = E.Cd_Id
Join dbo.Tb_Adm_Transportador	T  (Nolock) On C.TransportadorId = T.Cd_Id
Join dbo.Tb_Edi_EntregaDetalhe  Ed (Nolock) On Ed.Id_Entrega = C.EntregaId
Where   C.EmpresaId = @EmpresaId
	And (C.FaturaId = @FaturaId Or @FaturaId = 0)
	And ((ISNULL(C.DataEmissao,E.Dt_Importacao) Between @DataInicial And @DataFinal) Or (@DataInicial Is NULL And @DataFinal Is NULL) Or (@DataInicial = '' And @DataFinal = ''))
	And (T.Cd_Id = @TransportadorId Or @TransportadorId = 0)
	And (C.ConciliacaoStatusId = @StatusId Or @StatusId = 0)
	And (E.Cd_Pedido = @CodigoPedido Or @CodigoPedido Is NULL Or @CodigoPedido = '')
	And (E.Cd_Danfe	= @CodigoDanfe Or @CodigoDanfe Is NULL Or @CodigoDanfe = '')
	And (E.Cd_Entrega = @CodigoEntrega Or @CodigoEntrega Is NULL Or @CodigoEntrega = '')
	And C.ConciliacaoId <= ( SELECT max(ConciliacaoFinalId) FROM Fretter.ControleProcessoIndicador )
	And C.Ativo = 1

Drop Table If Exists #ConciliacaoRelatorioComposicao
Create Table #ConciliacaoRelatorioComposicao
(
	ConciliacaoId			Bigint
	,ImportacaoCteId		Bigint
	,ComposicaoNome			Varchar(256)
	,ComposicaoValor		Decimal(10,2)
	,ComposicaoTipoId		Int
)

Insert Into #ConciliacaoRelatorioComposicao
(
	 ConciliacaoId	
	,ImportacaoCteId
	,ComposicaoNome	
	,ComposicaoValor
	,ComposicaoTipoId
)
Select 
	 ConciliacaoId		= Cp.ConciliacaoId
	,ImportacaoCteId	= Cp.ImportacaoCteId
	,ComposicaoNome		= Tc.Nome
	,ComposicaoValor	= Tc.Valor
	,ComposicaoTipoId	= Tc.ConfiguracaoCteTipoId
From Fretter.ImportacaoCteComposicao Tc(Nolock)
Join #ConciliacaoRelatorio			 Cp(Nolock) ON Tc.ImportacaoCteId = Cp.ImportacaoCteId
Where Tc.ImportacaoCteId Is Not NULL

;With ICMS As
(	
	Select 
		Cr.ConciliacaoId
		,Valor = SUM(ComposicaoValor)
	From #ConciliacaoRelatorio				Cr
	Join #ConciliacaoRelatorioComposicao	Uc ON Cr.ConciliacaoId = Uc.ConciliacaoId
	Where ComposicaoNome IN (Select Distinct Alias From Fretter.ConfiguracaoCteTransportador Where ConfiguracaoCteTipoId = 3 And EmpresaId = @EmpresaId)
	Group By Cr.ConciliacaoId	
)
Update Cr
Set ValorICMS = Fp.Valor
From #ConciliacaoRelatorio	Cr
Join ICMS					Fp On Cr.ConciliacaoId = Fp.ConciliacaoId

;With GRIS As
(	
	Select 
		Cr.ConciliacaoId
		,Valor = SUM(ComposicaoValor)
	From #ConciliacaoRelatorio				Cr
	Join #ConciliacaoRelatorioComposicao	Uc ON Cr.ConciliacaoId = Uc.ConciliacaoId
	Where ComposicaoNome IN (Select Distinct Alias From Fretter.ConfiguracaoCteTransportador Where ConfiguracaoCteTipoId = 4 And EmpresaId = @EmpresaId)
	Group By Cr.ConciliacaoId	
)
Update Cr
Set ValorGRIS = Fp.Valor
From #ConciliacaoRelatorio	Cr
Join GRIS					Fp On Cr.ConciliacaoId = Fp.ConciliacaoId

;With ADValorem As
(	
	Select 
		Cr.ConciliacaoId
		,Valor = SUM(ComposicaoValor)
	From #ConciliacaoRelatorio				Cr
	Join #ConciliacaoRelatorioComposicao	Uc ON Cr.ConciliacaoId = Uc.ConciliacaoId
	Where ComposicaoNome IN (Select Distinct Alias From Fretter.ConfiguracaoCteTransportador Where ConfiguracaoCteTipoId = 5 And EmpresaId = @EmpresaId)
	Group By Cr.ConciliacaoId	
)
Update Cr
Set ValorADValorem = Fp.Valor
From #ConciliacaoRelatorio	Cr
Join ADValorem				Fp On Cr.ConciliacaoId = Fp.ConciliacaoId


;With Pedagio As
(	
	Select 
		Cr.ConciliacaoId
		,Valor = SUM(ComposicaoValor)
	From #ConciliacaoRelatorio				Cr
	Join #ConciliacaoRelatorioComposicao	Uc ON Cr.ConciliacaoId = Uc.ConciliacaoId
	Where ComposicaoNome IN (Select Distinct Alias From Fretter.ConfiguracaoCteTransportador Where ConfiguracaoCteTipoId = 6 And EmpresaId = @EmpresaId)
	Group By Cr.ConciliacaoId	
)
Update Cr
Set ValorPedagio = Fp.Valor
From #ConciliacaoRelatorio	Cr
Join Pedagio				Fp On Cr.ConciliacaoId = Fp.ConciliacaoId

;With FretePeso As
(	
	Select 
		Cr.ConciliacaoId
		,Valor = SUM(ComposicaoValor)
	From #ConciliacaoRelatorio				Cr
	Join #ConciliacaoRelatorioComposicao	Uc ON Cr.ConciliacaoId = Uc.ConciliacaoId
	Where ComposicaoNome IN (Select Distinct Alias From Fretter.ConfiguracaoCteTransportador Where ConfiguracaoCteTipoId = 7 And EmpresaId = @EmpresaId)
	Group By Cr.ConciliacaoId	
)
Update Cr
Set ValorFretePeso = Fp.Valor
From #ConciliacaoRelatorio	Cr
Join FretePeso				Fp On Cr.ConciliacaoId = Fp.ConciliacaoId

;With TaxaTRT As
(	
	Select 
		Cr.ConciliacaoId
		,Valor = SUM(ComposicaoValor)
	From #ConciliacaoRelatorio				Cr
	Join #ConciliacaoRelatorioComposicao	Uc ON Cr.ConciliacaoId = Uc.ConciliacaoId
	Where ComposicaoNome IN (Select Distinct Alias From Fretter.ConfiguracaoCteTransportador Where ConfiguracaoCteTipoId = 8 And EmpresaId = @EmpresaId)
	Group By Cr.ConciliacaoId	
)
Update Cr
Set ValorTaxaTRT = Fp.Valor
From #ConciliacaoRelatorio	Cr
Join TaxaTRT				Fp On Cr.ConciliacaoId = Fp.ConciliacaoId

;With TaxaTDE As
(	
	Select 
		Cr.ConciliacaoId
		,Valor = SUM(ComposicaoValor)
	From #ConciliacaoRelatorio				Cr
	Join #ConciliacaoRelatorioComposicao	Uc ON Cr.ConciliacaoId = Uc.ConciliacaoId
	Where ComposicaoNome IN (Select Distinct Alias From Fretter.ConfiguracaoCteTransportador Where ConfiguracaoCteTipoId = 9 And EmpresaId = @EmpresaId)
	Group By Cr.ConciliacaoId	
)
Update Cr
Set ValorTaxaTDE = Fp.Valor
From #ConciliacaoRelatorio	Cr
Join TaxaTDE				Fp On Cr.ConciliacaoId = Fp.ConciliacaoId

;With TaxaTDA As
(	
	Select 
		Cr.ConciliacaoId
		,Valor = SUM(ComposicaoValor)
	From #ConciliacaoRelatorio				Cr
	Join #ConciliacaoRelatorioComposicao	Uc ON Cr.ConciliacaoId = Uc.ConciliacaoId
	Where ComposicaoNome IN (Select Distinct Alias From Fretter.ConfiguracaoCteTransportador Where ConfiguracaoCteTipoId = 10 And EmpresaId = @EmpresaId)
	Group By Cr.ConciliacaoId	
)
Update Cr
Set ValorTaxaTDA = Fp.Valor
From #ConciliacaoRelatorio	Cr
Join TaxaTDA				Fp On Cr.ConciliacaoId = Fp.ConciliacaoId

;With TaxaCTe As
(	
	Select 
		Cr.ConciliacaoId
		,Valor = SUM(ComposicaoValor)
	From #ConciliacaoRelatorio				Cr
	Join #ConciliacaoRelatorioComposicao	Uc ON Cr.ConciliacaoId = Uc.ConciliacaoId
	Where ComposicaoNome IN (Select Distinct Alias From Fretter.ConfiguracaoCteTransportador Where ConfiguracaoCteTipoId = 11 And EmpresaId = @EmpresaId)
	Group By Cr.ConciliacaoId	
)
Update Cr
Set ValorTaxaCTe = Fp.Valor
From #ConciliacaoRelatorio	Cr
Join TaxaCTe				Fp On Cr.ConciliacaoId = Fp.ConciliacaoId

;With TaxaRisco As
(	
	Select 
		Cr.ConciliacaoId
		,Valor = SUM(ComposicaoValor)
	From #ConciliacaoRelatorio				Cr
	Join #ConciliacaoRelatorioComposicao	Uc ON Cr.ConciliacaoId = Uc.ConciliacaoId
	Where ComposicaoNome IN (Select Distinct Alias From Fretter.ConfiguracaoCteTransportador Where ConfiguracaoCteTipoId = 12 And EmpresaId = @EmpresaId)
	Group By Cr.ConciliacaoId	
)
Update Cr
Set ValorTaxaRisco = Fp.Valor
From #ConciliacaoRelatorio	Cr
Join TaxaRisco				Fp On Cr.ConciliacaoId = Fp.ConciliacaoId

;With Suframa As
(	
	Select 
		Cr.ConciliacaoId
		,Valor = SUM(ComposicaoValor)
	From #ConciliacaoRelatorio				Cr
	Join #ConciliacaoRelatorioComposicao	Uc ON Cr.ConciliacaoId = Uc.ConciliacaoId
	Where ComposicaoNome IN (Select Distinct Alias From Fretter.ConfiguracaoCteTransportador Where ConfiguracaoCteTipoId = 13 And EmpresaId = @EmpresaId)
	Group By Cr.ConciliacaoId	
)
Update Cr
Set ValorSuframa = Fp.Valor
From #ConciliacaoRelatorio	Cr
Join Suframa				Fp On Cr.ConciliacaoId = Fp.ConciliacaoId

Select 
	ConciliacaoId				
	,Transportador				
	,CodigoEntrega				
	,CodigoPedido									
	,DataEmissao					
	,EntregaPeso				
	,EntregaAltura				
	,EntregaComprimento			
	,EntregaLargura				
	,EntregaValorDeclarado			
	,ValorCustoFrete			
	,ValorCustoAdicional		
	,ValorCustoReal				
	,ValorICMS					
	,ValorGRIS					
	,ValorADValorem				
	,ValorPedagio				
	,ValorFretePeso				
	,ValorTaxaTRT				
	,ValorTaxaTDE				
	,ValorTaxaTDA				
	,ValorTaxaCTe				
	,ValorTaxaRisco				
	,ValorSuframa					
	,StatusConciliacao			
From #ConciliacaoRelatorio

End