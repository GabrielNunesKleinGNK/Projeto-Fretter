CREATE  OR ALTER   Procedure [Fretter].[GetEntregaConciliacao]	
(
	@filtroDoccob Fretter.Tp_FiltroEntregaConciliacao	READONLY
	,@empresaId											Int
	,@filtroPeriodo										Bit
	,@dataInicio										Datetime = Null
	,@dataTermino										Datetime = Null
	,@transportadorId									Int = Null
	,@statusConciliacaoId								Int = Null
)
AS BEGIN   
SET NOCOUNT ON;

IF (@filtroPeriodo= 1) 
Begin
	If(@dataInicio Is NULL And @dataTermino Is NULL)
	Begin
		Set @dataInicio = DATEADD(Month,-1,Getdate());
		Set @dataTermino = DATEADD(Day,1,Getdate());
	End
	Else 
		Begin
			Set @dataInicio = DATEADD(Month,-1,@dataInicio);
			Set @dataTermino = DATEADD(Month,1,@dataTermino);
		End

	Select
		ConciliacaoId			= co.ConciliacaoId
		,ValorCustoFrete		= co.ValorCustoFrete
		,ValorCustoReal			= co.ValorCustoReal		
		,ValorFreteDoccob		= 0.0
		,TransportadorId		= E.Id_Transportador
		,Transportador			= tc.ds_nome  
		,CodigoPedido			= ISNULL(E.Cd_Pedido,'--')
		,NotaFiscal				= e.cd_notaFiscal
		,Serie					= e.cd_serie
		,StatusConciliacao		= cs.nome
		,Habilitado				= CAST(1 as bit)
		,Selecionado			= CAST(1 as bit)
		,DataCadastro			= co.DataCadastro
		,DataEmissao			= co.DataEmissao	
		,ValorFreteDoccob		= 0
		,LinhaNotaFiscal        = 0
		,DataEmissaoDivergente  = CAST(0 as bit)
		,NotaDivergente			= CAST(0 as bit)
		,ConciliacaoTipo		= Ct.Nome
		,ChaveCte				= Imp.ChaveCte
	From Fretter.Conciliacao			Co (nolock)
	Left Join Tb_Adm_TransportadorCnpj	Tc (nolock) On co.TransportadorCnpjId = tc.Cd_Id And tc.Flg_Ativo = 1
	Join Tb_Edi_Entrega					 E (nolock) On e.cd_id = co.entregaid 
	Join Fretter.ConciliacaoStatus		Cs (nolock) On cs.ConciliacaoStatusId = co.ConciliacaoStatusId
	Join Fretter.ConciliacaoTipo		Ct (Nolock) On Co.ConciliacaoTipoId = Ct.ConciliacaoTipoId 
	Join Fretter.ImportacaoCte			Imp(Nolock)	On Imp.ConciliacaoId = Co.ConciliacaoId
	Where co.EmpresaId = @empresaId 
		And (@transportadorId is null Or co.Transportadorid = @transportadorId Or @transportadorId = 0)
		And (@statusConciliacaoId is null Or co.ConciliacaoStatusId = @statusConciliacaoId Or @statusConciliacaoId = 0)
		And E.Dt_EmissaoNF between Cast(@dataInicio As date) and Cast(@dataTermino As date)
		And Co.ConciliacaoStatusId != 1
		and Co.Ativo = 1
End
Else 
	Begin
	
	If(@dataInicio Is NULL And @dataTermino Is NULL)
	Begin
		Set @dataInicio = DATEADD(Month,-4,Getdate());
		Set @dataTermino = DATEADD(Day,1,Getdate());
	End
	Else 
		Begin
			Set @dataInicio = DATEADD(Month,-1,@dataInicio);
			Set @dataTermino = DATEADD(Month,1,@dataTermino);
		End

	Declare @ConciliacaoTemp Table
	(
		Id						Int Identity(1,1) 
		,LinhaNotaFiscal		Int
		,EntregaId				Bigint
		,ImportacaoCteId		Bigint
		,ConciliacaoId			Bigint
		,ConciliacaoTipoId		Int
		,ConciliacaoTipo		Varchar(512)
		,ValorCustoFrete		Decimal(8,2)
		,ValorCustoReal			Decimal(8,2)
		,ValorFreteDoccob		Decimal(8,2)
		,TransportadorId		Int
		,TransportadorCNPJ		Varchar(36)
		,Transportador			Varchar(512)
		,CodigoPedido			Varchar(256)
		,NotaFiscal				Varchar(64)
		,Serie					Varchar(32)
		,ConhecimentoNumero		Varchar(64)
		,ConhecimentoSerie		Varchar(64)
		,Habilitado				Bit Default(0)
		,Selecionado			Bit Default(0)
		,DataEmissao			Date
		,DataEmissaoDivergente	Bit Default(0)
		,NotaDivergente			Bit Default(0)
		,ConciliacaoStatusId	Int
		,Classificada			Bit Default(0)
		,StatusConciliacao		Varchar(256)
		,DataCadastro			Datetime
		,ChaveCte				Varchar(64)
	)

	Insert Into @ConciliacaoTemp
	(
		LinhaNotaFiscal		
		,EntregaId				
		,ImportacaoCteId		
		,ConciliacaoId			
		,ConciliacaoTipoId		
		,ConciliacaoTipo		
		,ValorCustoFrete		
		,ValorCustoReal			
		,ValorFreteDoccob		
		,TransportadorId		
		,TransportadorCNPJ
		,Transportador			
		,CodigoPedido			
		,NotaFiscal				
		,Serie					
		,ConhecimentoNumero	
		,ConhecimentoSerie	
		,DataEmissao
	)
	Select 
		LinhaNotaFiscal		= Fd.Id		
		,EntregaId			= NULL
		,ImportacaoCteId	= NULL
		,ConciliacaoId		= NULL
		,ConciliacaoTipoId	= NULL
		,ConciliacaoTipo	= NULL
		,ValorCustoFrete	= NULL
		,ValorCustoReal		= NULL
		,ValorFreteDoccob	= Fd.ValorFrete
		,TransportadorId	= NULL
		,TransportadorCNPJ	= Fd.CNPJ
		,Transportador		= NULL
		,CodigoPedido		= NULL
		,NotaFiscal			= Fd.NotaFiscal
		,Serie				= Fd.Serie
		,ConhecimentoNumero	= CAST(Fd.ConhecimentoNumero AS INT)
		,ConhecimentoSerie	= CAST(Fd.ConhecimentoSerie AS INT)
		,DataEmissao		= Fd.DataEmissao
	From @filtroDoccob			Fd

	Declare @DataEmissaControle Date

	Select @DataEmissaControle = MIN(DataEmissao)
	From @ConciliacaoTemp

	Update Ct
	Set ConciliacaoId			= Co.ConciliacaoId
		,ImportacaoCteId		= Ci.ImportacaoCteId
		,EntregaId				= Co.EntregaId
		,Classificada			= 1
		,ConciliacaoTipoId		= Co.ConciliacaoTipoId
		,ValorCustoFrete		= Co.ValorCustoFrete
		,ValorCustoReal			= Co.ValorCustoReal
		,TransportadorId		= Td.Id_Transportador
		,CodigoPedido			= Td.Cd_Pedido
		,DataEmissaoDivergente	= Cast(0 AS BIT)
		,NotaDivergente			= Cast((Case When Concat(Ct.NotaFiscal,'_',Ct.Serie) != Td.Cd_NF_Serie Then 1 Else 0 End) AS BIT)
		,ConciliacaoStatusId	= Co.ConciliacaoStatusId
		,DataCadastro			= Co.DataCadastro
		,ChaveCte				= Ci.ChaveCte
	From @ConciliacaoTemp			Ct
	Join Fretter.ImportacaoCte		Ci(Nolock) On Ct.ConhecimentoNumero = Ci.Numero And Ct.ConhecimentoSerie = Ci.Serie And Ct.TransportadorCNPJ = Ci.CNPJTransportador
	Join Fretter.Conciliacao		Co(Nolock) On Ci.ImportacaoCteId = Co.ImportacaoCteId
	Join dbo.Tb_Edi_Entrega			Td(Nolock) On Co.EntregaId = Td.Cd_Id
	Where Td.Dt_EmissaoNF Between @dataInicio And @dataTermino	

	Update Ct
	Set ConciliacaoId			= Co.ConciliacaoId
		,ImportacaoCteId		= Ci.ImportacaoCteId
		,EntregaId				= Co.EntregaId
		,Classificada			= 1
		,ConciliacaoTipoId		= Co.ConciliacaoTipoId
		,ValorCustoFrete		= Co.ValorCustoFrete
		,ValorCustoReal			= Co.ValorCustoReal
		,TransportadorId		= Td.Id_Transportador
		,CodigoPedido			= Td.Cd_Pedido
		,DataEmissaoDivergente	= Cast(0 AS BIT)
		,NotaDivergente			= Cast((Case When Concat(Ct.NotaFiscal,'_',Ct.Serie) != Td.Cd_NF_Serie Then 1 Else 0 End) AS BIT)
		,ConciliacaoStatusId	= Co.ConciliacaoStatusId
		,DataCadastro			= Co.DataCadastro
		,ChaveCte				= Ci.ChaveCte
	From @ConciliacaoTemp			Ct
	Join Fretter.ImportacaoCte		Ci(Nolock) On Ct.ConhecimentoNumero = Ci.Numero And Ct.ConhecimentoSerie = Ci.Serie And Ct.TransportadorCNPJ = Ci.CNPJTransportador
	Join Fretter.Conciliacao		Co(Nolock) On Ci.ConciliacaoId = Co.ConciliacaoId
	Join dbo.Tb_Edi_Entrega			Td(Nolock) On Co.EntregaId = Td.Cd_Id
	Where Td.Dt_EmissaoNF Between @dataInicio And @dataTermino	
		And Ct.Classificada = 0

	Update Ct
	Set DataEmissaoDivergente = 0
	From @ConciliacaoTemp			Ct
	Where NotaDivergente = 1

	Update Ct
	Set StatusConciliacao = Cs.Nome
	From @ConciliacaoTemp			Ct
	Join Fretter.ConciliacaoStatus	Cs(Nolock) On Ct.ConciliacaoStatusId = Cs.ConciliacaoStatusId

	Update Ct
	Set Transportador		= Tc.Ds_Nome
		,TransportadorId	= Tc.Id_Transportador
	From @ConciliacaoTemp			Ct
	Join Tb_Adm_TransportadorCnpj	Tc (nolock) On tc.Cd_Cnpj = Ct.TransportadorCNPJ

	Drop table If Exists #ConciliacaoDanfeImportadaDuplicada

	;With ConciliacaoComDanfeDupliacada As
	(		
		Select 
			Ct.ConhecimentoNumero
			,Ct.ConhecimentoSerie
			,Danfe					= Ii.Chave			
			,ConciliacaoTipoId		= It.ConciliacaoTipoId
		From ConciliacaoTemp					Ct
		Join Fretter.ImportacaoCte				Ci(Nolock) On Ct.ConhecimentoNumero = Ci.Numero And Ct.ConhecimentoSerie = Ci.Serie And Ct.TransportadorCNPJ = Ci.CNPJTransportador
		Join Fretter.ImportacaoCteNotaFiscal	Ii(Nolock) On Ci.ImportacaoCteId = Ii.ImportacaoCteId		
		Join Fretter.ImportacaoArquivo			Ia(Nolock) On Ci.ImportacaoArquivoId = Ia.ImportacaoArquivoId
		Join Fretter.ImportacaoArquivoTipoItem	It(Nolock) On Ia.ImportacaoArquivoTipoItemId = It.ImportacaoArquivoTipoItemId
		Where Ct.Classificada = 0				
			And Ci.Ativo = 1
		Group by 
			Ct.ConhecimentoNumero
			,Ct.ConhecimentoSerie
			,Ii.Chave
			,It.ConciliacaoTipoId
	)
	Select 
		Cd.ConhecimentoNumero
		,Cd.ConhecimentoSerie
		,Cd.Danfe					
		,Cd.ConciliacaoTipoId		
		,DanfeQuantidade			= Count(Id.ImportacaoCteId)
	Into #ConciliacaoDanfeImportadaDuplicada
	From ConciliacaoComDanfeDupliacada		Cd
	Join Fretter.ImportacaoCteNotaFiscal	Id(Nolock) On Cd.Danfe = Id.Chave
	Group By 
			Cd.ConhecimentoNumero
		,Cd.ConhecimentoSerie
		,Cd.Danfe					
		,Cd.ConciliacaoTipoId		
	--Existe mais de 1 Danfe em CTes diferentes pra um unica entrega
	--Pode ser que se vier essa mesma Danfe com o Numero de Conhecimento correto em outro DOCCOB concilie a Entrega
	Update Ct
	Set StatusConciliacao	= 'Documento Invalido' 		
		,CodigoPedido		= ISNULL(Et.Cd_Pedido,Et.Cd_Entrega)
	From #ConciliacaoDanfeImportadaDuplicada Cd 
	Join @ConciliacaoTemp					 Ct On Cd.ConhecimentoNumero = Ct.ConhecimentoNumero And Cd.ConhecimentoSerie = Ct.ConhecimentoSerie
	Join dbo.Tb_Edi_Entrega					 Et On Cd.Danfe = Et.Cd_Danfe And Ct.TransportadorId = Et.Id_Transportador And Et.Dt_EmissaoNF Between @dataInicio And @dataTermino
	Where Ct.Classificada = 0
		And Cd.DanfeQuantidade > 1

	Update Ct
	Set ConciliacaoTipo = Co.Nome
	From @ConciliacaoTemp			Ct
	Join Fretter.ConciliacaoTipo	Co(Nolock) On Ct.ConciliacaoTipoId = Co.ConciliacaoTipoId
	
	Select
		ConciliacaoId		= ISNULL(Ct.ConciliacaoId,0)
		,ValorCustoFrete	
		,ValorCustoReal
		,TransportadorId
		,Transportador	
		,ConciliacaoTipo
		,CodigoPedido
		,NotaFiscal
		,Serie
		,StatusConciliacao	= CASE WHEN Ct.Classificada = 0 And Ct.StatusConciliacao Is NULL Then 'Nao Encontrada' ELSE Ct.StatusConciliacao END
		,Habilitado			= Cast((Case When Ct.Classificada = 0  Then 0 When Ct.ConciliacaoStatusId = 1 Then 0 Else 1 End)AS BIT)
		,Selecionado		= CAST((Case When Ct.Classificada = 0  Then 0 When Ct.ConciliacaoStatusId = 1 Then 0 Else 1 End)AS BIT)
		,DataCadastro
		,DataEmissao
		,ValorFreteDoccob
		,LinhaNotaFiscal
		,DataEmissaoDivergente = CAST((DataEmissaoDivergente) AS BIT)
		,NotaDivergente			= CAST((NotaDivergente) AS BIT)
		,ChaveCte
	FROM @ConciliacaoTemp Ct
	ORDER BY Selecionado

	End
End
