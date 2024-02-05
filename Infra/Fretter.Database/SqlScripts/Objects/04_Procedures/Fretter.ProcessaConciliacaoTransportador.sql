Create Or Alter Procedure Fretter.ProcessaConciliacaoTransportador
(
	@Itens	Fretter.Tp_EntregaConciliacao READONLY
)
AS BEGIN   
SET NOCOUNT ON;                                  
SET DATEFORMAT YMD; 

Update Ec
Set Vl_Cobrado				= It.Vl_Cobrado				
	,Vl_Altura				= It.Vl_Altura				
	,Vl_Largura				= It.Vl_Largura				
	,Vl_Comprimento			= It.Vl_Comprimento			
	,Vl_Diametro			= It.Vl_Diametro			
	,Vl_Peso				= It.Vl_Peso				
	,Vl_Cubagem				= It.Vl_Cubagem				
	,Ds_Json				= It.Ds_Json				
	,Ds_RetornoProcessamento= It.Ds_RetornoProcessamento
	,Flg_Sucesso			= It.Flg_Sucesso
	,Flg_Processado			= 1	
	,Dt_Processamento		= ISNULL(It.Dt_Processamento,Getdate())
From @Itens							It
Join dbo.Tb_Edi_EntregaConciliacao	Ec (Nolock) On It.Id_EntregaConciliacao = Ec.Cd_Id

Create Table #ConciliacaoTemp
(
	EmpresaId					Int
	,EntregaId					Bigint
	,TransportadorId			Int
	,TransportadorCnpjId		Int
	,ValorCustoFrete			Decimal(10,4)
	,ValorCustoAdicional		Decimal(10,4)
	,ValorCustoReal				Decimal(10,4)
	,ValorCustoDivergencia		Decimal(10,4)
	,QuantidadeTentativas		Int
	,PossuiDivergenciaPeso		Bit
	,PossuiDivergenciaTarifa	Bit	
	,ConciliacaoStatusId		Int
	,DataPostagem				Datetime
	,JsonValoresRecotacao		Varchar(Max)
	,JsonValoresCte				Varchar(Max)
)

Insert Into #ConciliacaoTemp
(
	EmpresaId				
	,EntregaId				
	,TransportadorId		
	,TransportadorCnpjId	
	,ValorCustoFrete		
	,ValorCustoAdicional	
	,ValorCustoReal			
	,ValorCustoDivergencia	
	,QuantidadeTentativas	
	,PossuiDivergenciaPeso	
	,PossuiDivergenciaTarifa
	,ConciliacaoStatusId	
	,DataPostagem			
	,JsonValoresRecotacao	
	,JsonValoresCte			
)
Select Distinct
	EmpresaId					= Et.Id_Empresa
	,EntregaId					= It.Id_Entrega
	,TransportadorId			= Et.Id_Transportador
	,TransportadorCnpjId		= Et.Id_TransportadorCNPJ
	,ValorCustoFrete			= Et.Vl_CustoFrete
	,ValorCustoAdicional		= 0
	,ValorCustoReal				= It.Vl_Cobrado
	,ValorCustoDivergencia		= 0
	,QuantidadeTentativas		= 0 
	,PossuiDivergenciaPeso		= Case When Ed.Vl_Peso <> It.Vl_Peso Then 1 Else 0 End
	,PossuiDivergenciaTarifa	= Case When Et.Vl_CustoFrete <> It.Vl_Cobrado Then 1 Else 0 End
	,ConciliacaoStatusId		= NULL
	,DataPostagem				= Et.Dt_Postagem
	,JsonValoresRecotacao		= Ed.Ds_JsonComposicaoValoresCotacao
	,JsonValoresCte				= It.Ds_Json
From @Itens						It
Join dbo.Tb_Edi_Entrega			Et (Nolock) ON It.Id_Entrega = Et.Cd_Id
Join dbo.Tb_Edi_EntregaDetalhe	Ed (Nolock) ON Et.Cd_Id = Ed.Id_Entrega

Update #ConciliacaoTemp
Set ConciliacaoStatusId = Case When PossuiDivergenciaPeso = 1 Or PossuiDivergenciaTarifa = 1 Then 3 Else 2 End

Merge Fretter.Conciliacao As D
Using #ConciliacaoTemp	  As O On O.EntregaId = D.EntregaId 	
When Matched
Then Update
Set  D.EmpresaId				= O.EmpresaId					
	,D.EntregaId				= O.EntregaId					
	,D.TransportadorId			= O.TransportadorId	
	,D.TransportadorCnpjId		= O.TransportadorCnpjId
	,D.ValorCustoFrete			= O.ValorCustoFrete			
	,D.ValorCustoReal			= O.ValorCustoReal
	,D.ValorCustoAdicional		= 0
	,D.PossuiDivergenciaPeso	= O.PossuiDivergenciaPeso		
	,D.PossuiDivergenciaTarifa	= O.PossuiDivergenciaTarifa	
	,D.ConciliacaoStatusId		= O.ConciliacaoStatusId		
	,D.Ativo					= 1
	,D.UsuarioAlteracao			= NULL
	,D.DataAlteracao			= Getdate()
	,D.DataEmissao				= O.DataPostagem
	,D.JsonValoresRecotacao		= O.JsonValoresRecotacao
	,D.JsonValoresCte			= O.JsonValoresCte
	,D.ImportacaoCteId			= NULL		
When Not Matched
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
	,DataCadastro					
	,DataEmissao
	,JsonValoresRecotacao
	,JsonValoresCte	
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
	,Getdate()					
	,O.DataPostagem
	,O.JsonValoresRecotacao
	,O.JsonValoresCte	
);


Select Count(1) As QuantidadeProcessado
From #ConciliacaoTemp (Nolock)

End