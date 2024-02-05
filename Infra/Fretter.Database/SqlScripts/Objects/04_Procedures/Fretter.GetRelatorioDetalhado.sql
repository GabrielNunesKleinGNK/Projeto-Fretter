Create Or Alter Procedure Fretter.GetRelatorioDetalhado
(
	@PageSelected		Int = 0
	,@PageSize			Int = 50		
	,@OrderByDirection	NVarchar(4) = 'Asc'
	,@EmpresaId			Int = 0
	,@DataInicial		DateTime = Null
	,@DataFinal			DateTime = Null
	,@TransportadorId	Int = 0
	,@StatusId			Int = 0
	,@FaturaId			Int = 0
	,@CodigoEntrega		Varchar(100) = Null
	,@CodigoPedido		Varchar(100) = Null
	,@CodigoDanfe		Varchar(44) = Null
)
As Begin
Set Nocount On;

DECLARE @SqlQuery				NVarchar(4000)		
		,@StartRecord			Int	
		,@EndRecord				Int
		,@RecordCountTotal		Int		

Set @PageSelected += 1;
Set @StartRecord = (@PageSelected - 1) * @PageSize + 1 
Set @EndRecord = @PageSelected * @PageSize 

If(@CodigoEntrega Is Not NULL Or @CodigoPedido Is Not NULL)
	Select @DataInicial = null, @DataFinal = null;

SET @SqlQuery = N'
Select @RecordCountTotal = Count(C.ConciliacaoId)	
From Fretter.Conciliacao		C  (Nolock) 
Join Fretter.ConciliacaoStatus  CS (Nolock) On C.ConciliacaoStatusId = CS.ConciliacaoStatusId
Join dbo.Tb_Edi_Entrega			E  (Nolock) On C.EntregaId = E.Cd_Id
Join dbo.Tb_Adm_Transportador	T  (Nolock) On C.TransportadorId = T.Cd_Id
Join dbo.Tb_Edi_EntregaDetalhe  Ed (Nolock) On Ed.Id_Entrega = C.EntregaId
Where   (C.EmpresaId	= @PEmpresaId)
	And (C.FaturaId		= @PFaturaId Or @PFaturaId = 0)
	And ((C.DataEmissao Between @PDataInicial And @PDataFinal) Or (@PDataInicial Is NULL And @PDataFinal Is NULL) Or (@PDataInicial = '''' And @PDataFinal = ''''))
	And (T.Cd_Id		= @PTransportadorId Or @PTransportadorId = 0)
	And (C.ConciliacaoStatusId = @PStatusId Or @PStatusId = 0)
	And (E.Cd_Pedido	= @PCodigoPedido Or @PCodigoPedido Is NULL Or @PCodigoPedido = '''')
	And (E.Cd_Danfe		= @PCodigoDanfe Or @PCodigoDanfe Is NULL Or @PCodigoDanfe = '''')
	And (E.Cd_Entrega	= @PCodigoEntrega Or @PCodigoEntrega Is NULL Or @PCodigoEntrega = '''')
	And c.Ativo = 1'
			
EXEC sp_executesql @SqlQuery
	,N'@RecordCountTotal	Int OUTPUT
	,@PEmpresaId			Int
	,@PDataInicial			DateTime = Null
	,@PDataFinal			DateTime = Null
	,@PTransportadorId		Int = 0
	,@PStatusId				Int = 0
	,@PFaturaId				Int = 0
	,@PCodigoEntrega		Varchar(100) = Nul
	,@PCodigoPedido			Varchar(100) = Nul
	,@PCodigoDanfe			Varchar(44) = Null'
	,@RecordCountTotal	 = @RecordCountTotal OUTPUT
	,@PEmpresaId		 = @EmpresaId
	,@PDataInicial		 = @DataInicial		
	,@PDataFinal		 = @DataFinal			
	,@PTransportadorId	 = @TransportadorId	
	,@PStatusId			 = @StatusId			
	,@PFaturaId			 = @FaturaId			
	,@PCodigoEntrega	 = @CodigoEntrega		
	,@PCodigoPedido		 = @CodigoPedido		
	,@PCodigoDanfe		 = @CodigoDanfe		 

Create Table #ConciliacaoPaginada 
(
	CodigoConciliacao			Bigint
	,CodigoEntrega				Varchar(100)
	,CodigoPedido				Varchar(100)
	,Transportador				Varchar(512)
	,ValorCustoFrete			Decimal(10,4)
	,ValorCustoAdicional		Decimal(10,4)
	,ValorCustoReal				Decimal(10,4)
	,QtdTentativas				Int
	,PossuiDivergenciaPeso		Bit
	,PossuiDivergenciaTarifa	Bit
	,StatusConciliacao			Varchar(256)
	,StatusConciliacaoId		Int
	,DataCadastro				Datetime
	,DataEmissao				Datetime
	,QtdRegistrosQuery			Int
	,Finalizado					Datetime
	,ProcessadoIndicador		Bit
	,EntregaPeso				Decimal(10,4)
	,EntregaAltura				Decimal(10,4)
	,EntregaComprimento			Decimal(10,4)
	,EntregaLargura				Decimal(10,4)
	,EntregaValorDeclarado		Decimal(10,4)
	,MicroServicoId				Int
	,CanalId					Int
	,CanalVendaId				Int
	,JsonValoresRecotacao		Varchar(Max)
	,JsonValoresCte				Varchar(Max)
	,TipoCobranca               Varchar(64)
)

SET @SqlQuery = N'
;With ConciliacaoTmp As (
Select
	RowNumber					= ROW_NUMBER() OVER (ORDER BY C.ConciliacaoId ' + @OrderByDirection +' )
	,CodigoConciliacao			= C.ConciliacaoId
	,CodigoEntrega				= E.Cd_Entrega
	,CodigoPedido				= E.Cd_Pedido
	,Transportador				= T.Ds_NomeFantasia
	,ValorCustoFrete			= C.ValorCustoFrete
	,ValorCustoAdicional		= C.ValorCustoAdicional
	,ValorCustoReal				= C.ValorCustoReal	 
	,QtdTentativas				= IsNull(C.QuantidadeTentativas,0)
	,PossuiDivergenciaPeso		= C.PossuiDivergenciaPeso
	,PossuiDivergenciaTarifa	= C.PossuiDivergenciaTarifa
	,StatusConciliacao			= CS.Nome
	,StatusConciliacaoId		= CS.ConciliacaoStatusId
	,DataEmissao				= C.DataEmissao
	,DataCadastro				= C.DataCadastro
	,DataFinalizacao			= C.DataFinalizacao
	,FaturaId					= C.FaturaId
	,ProcessadoIndicador		= C.ProcessadoIndicador
	,EntregaPeso				= Ed.Vl_Peso
	,EntregaAltura				= Ed.Vl_Altura
	,EntregaLargura				= Ed.Vl_Largura
	,EntregaComprimento			= Ed.Vl_Comprimento
	,EntregaValorDeclarado		= Ed.Vl_Declarado
	,MicroServicoId				= E.Id_MicroServico
	,CanalId					= E.Id_Canal
	,CanalVendaId				= E.Id_CanalVenda
	,JsonValoresRecotacao		= C.JsonValoresRecotacao
	,JsonValoresCte				= C.JsonValoresCte
	,TipoCobranca               = CT.Nome
From Fretter.Conciliacao			C  (Nolock)
Join Fretter.ConciliacaoStatus		CS (Nolock) On C.ConciliacaoStatusId = CS.ConciliacaoStatusId
Join dbo.Tb_Edi_Entrega				E  (Nolock) On C.EntregaId = E.Cd_Id
Join dbo.Tb_Adm_Transportador		T  (Nolock) On C.TransportadorId = T.Cd_Id
Join dbo.Tb_Edi_EntregaDetalhe		Ed (Nolock) On Ed.Id_Entrega = C.EntregaId
Left Join Fretter.ConciliacaoTipo	CT (Nolock) On C.ConciliacaoTipoId = CT.ConciliacaoTipoId
Where   C.EmpresaId = @PEmpresaId
	And (C.FaturaId = @PFaturaId Or @PFaturaId = 0)
	And ((C.DataEmissao Between @PDataInicial And @PDataFinal) Or (@PDataInicial Is NULL And @PDataFinal Is NULL) Or (@PDataInicial = '''' And @PDataFinal = ''''))
	And (T.Cd_Id = @PTransportadorId Or @PTransportadorId = 0)
	And (C.ConciliacaoStatusId = @PStatusId Or @PStatusId = 0)
	And (E.Cd_Pedido = @PCodigoPedido Or @PCodigoPedido Is NULL Or @PCodigoPedido = '''')
	And (E.Cd_Danfe	= @PCodigoDanfe Or @PCodigoDanfe Is NULL Or @PCodigoDanfe = '''')
	And (E.Cd_Entrega = @PCodigoEntrega Or @PCodigoEntrega Is NULL Or @PCodigoEntrega = '''')
	And c.Ativo = 1
	)
Insert #ConciliacaoPaginada
Select
	CodigoConciliacao
	,CodigoEntrega
	,CodigoPedido
	,Transportador
	,ValorCustoFrete
	,ValorCustoAdicional
	,ValorCustoReal
	,QtdTentativas
	,PossuiDivergenciaPeso
	,PossuiDivergenciaTarifa
	,StatusConciliacao
	,StatusConciliacaoId
	,DataCadastro
	,DataEmissao
	,QtdRegistrosQuery		= @RecordCountTotal
	,DataFinalizacao
	,ProcessadoIndicador
	,EntregaPeso
	,EntregaAltura
	,EntregaLargura
	,EntregaComprimento
	,EntregaValorDeclarado
	,MicroServicoId
	,CanalId
	,CanalVendaId
	,JsonValoresRecotacao
	,JsonValoresCte
	,TipoCobranca
From ConciliacaoTmp 
Where RowNumber >= ' + CAST(@StartRecord AS Varchar(20)) +
' And RowNumber <= ' + CAST(@EndRecord AS Varchar(20)) 


		
EXEC sp_executesql @SqlQuery
	,N'@RecordCountTotal	Int
	,@PEmpresaId			Int
	,@PDataInicial			DateTime = Null
	,@PDataFinal			DateTime = Null
	,@PTransportadorId		Int = 0
	,@PStatusId				Int = 0
	,@PFaturaId				Int = 0
	,@PCodigoEntrega		Varchar(100) = Null
	,@PCodigoPedido			Varchar(100) = Null
	,@PCodigoDanfe			Varchar(44) = Null'
	,@RecordCountTotal		= @RecordCountTotal
	,@PEmpresaId			= @EmpresaId
	,@PDataInicial			= @DataInicial
	,@PDataFinal			= @DataFinal
	,@PTransportadorId		= @TransportadorId
	,@PStatusId				= @StatusId
	,@PFaturaId				= @FaturaId
	,@PCodigoEntrega		= @CodigoEntrega
	,@PCodigoPedido			= @CodigoPedido
	,@PCodigoDanfe			= @CodigoDanfe

Select
	CodigoConciliacao
	,CodigoEntrega
	,CodigoPedido
	,Transportador
	,ValorCustoFrete
	,ValorCustoAdicional
	,ValorCustoReal
	,QtdTentativas
	,PossuiDivergenciaPeso
	,PossuiDivergenciaTarifa
	,StatusConciliacao
	,StatusConciliacaoId
	,DataCadastro	
	,DataEmissao
	,QtdRegistrosQuery
	,Finalizado
	,ProcessadoIndicador
	,EntregaPeso
	,EntregaAltura
	,EntregaComprimento
	,EntregaLargura
	,EntregaValorDeclarado
	,MicroServicoId
	,MicroServico			= Am.Cd_Servico
	,CanalId
	,CanalCNPJ				= Cast(Ca.Cd_CnpjUnico As varchar(32))
	,CanalVendaId
	,CanalVenda				= Cv.Ds_CanalVendaUnico
	,JsonValoresRecotacao
	,JsonValoresCte
	,TipoCobranca
From #ConciliacaoPaginada	Cp
Join Tb_Adm_MicroServico	Am(Nolock) On Cp.MicroServicoId = Am.Cd_Id
Join Tb_Adm_Canal			Ca(Nolock) On Cp.CanalId = Ca.Cd_Id
Join Tb_Adm_CanalVenda		Cv(Nolock) On Cp.CanalVendaId = Cv.Cd_Id

End