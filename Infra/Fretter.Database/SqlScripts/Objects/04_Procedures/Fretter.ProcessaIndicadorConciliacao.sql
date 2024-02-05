Create Or Alter Procedure Fretter.ProcessaIndicadorConciliacao
As Begin
Set Nocount ON;

Declare @ConciliacaoInicial		Bigint	= 0
		,@ConciliacaoFinal		Bigint	= 0
		,@BatchSize				Int		= 20000
		,@ControleProcessoId	Bigint  = 0
		
If Exists(Select Top 1 1 From Fretter.ControleProcessoIndicador  (Nolock) Where DataTermino Is NULL And Ativo = 1 And DateDiff(Minute,DataInicio,Getdate()) > 30)    
Begin    
	Update Fretter.ControleProcessoIndicador   
	Set DataTermino			= Getdate()    
		,Mensagem			= 'Controle Finalizado Manualmente Intervalo > 30 Min'    
		,ConciliacaoFinalId	= ConciliacaoInicialId - 1
		,QtdConciliacao		= 0    
		,Sucesso			= 0    
		,Processado			= 1    
	Where DataTermino Is NULL    
End    

Select @ConciliacaoInicial = ISNull(Max(ConciliacaoFinalId),1)
From Fretter.ControleProcessoIndicador (Nolock) 
Where Ativo = 1 And Processado = 1

Select @ConciliacaoFinal = ISNULL(MAX(ConciliacaoId),0) - @ConciliacaoInicial
From Fretter.Conciliacao (Nolock)
Where ConciliacaoId > @ConciliacaoInicial		

Select @ConciliacaoFinal	= Case			
								When(@ConciliacaoFinal > @BatchSize)						Then @ConciliacaoInicial + @BatchSize
								When(@ConciliacaoFinal <= 0 Or @ConciliacaoFinal Is Null)	Then 0 
								Else @ConciliacaoInicial + @ConciliacaoFinal
							End						

If(Exists(Select Top 1 1 From Fretter.ControleProcessoIndicador(nolock) Where Processado = 0  And Ativo = 1))          
Begin     	
	Select @ControleProcessoId = Max(ControleProcessoIndicadorId)           
	From Fretter.ControleProcessoIndicador(nolock)           
	Where Processado = 0  And Ativo = 1
        
	Select           
		@ConciliacaoInicial	= ConciliacaoInicialId           
		,@ConciliacaoFinal	= ConciliacaoFinalId
	From Fretter.ControleProcessoIndicador(nolock)          
	Where ControleProcessoIndicadorId = @ControleProcessoId          
End        
Else          
	Begin          
		If(@ConciliacaoFinal = 0)          
		Begin          
			Print 'Nenhum registro a ser Processado.'          
			Return          
		End          

		Set @ConciliacaoInicial += 1; 

		
        
		Insert Into Fretter.ControleProcessoIndicador
		(
			ConciliacaoInicialId
			,ConciliacaoFinalId
			,QtdConciliacao
			,QtdConciliacaoPendente
			,DataInicio
			,DataTermino
			,Duracao
			,Processado
			,Sucesso
			,Mensagem
			,Ativo
		)
		Select 
			ConciliacaoInicialId	= @ConciliacaoInicial
			,ConciliacaoFinalId		= @ConciliacaoFinal
			,QtdConciliacao			= 0
			,QtdConciliacaoPendente = 0
			,DataInicio				= Getdate()
			,DataTermino			= NULL
			,Duracao				= NULL
			,Processado				= 0
			,Sucesso				= 0
			,Mensagem				= NULL
			,Ativo					= 1
			

		Set @ControleProcessoId = SCOPE_IDENTITY();
	End         

Begin Try	
	Create Table #IndicadorConciliacaoTemp
    (
	    Id							Int Identity 
	    ,Data						Date
	    ,EmpresaId					Int
	    ,TransportadorId			Int
	    ,TransportadorCnpjId		Int
	    ,QtdEntrega					Int Default(0) 
	    ,QtdCte						Int Default(0)
	    ,QtdSucesso					Int Default(0)
	    ,QtdErro					Int Default(0)
	    ,QtdDivergencia				Int Default(0)
	    ,QtdDivergenciaPeso			Int Default(0)
	    ,QtdDivergenciaTarifa		Int Default(0)
	    ,ValorCustoFreteEstimado	Decimal(10,2) Default(0)
	    ,ValorCustoFreteReal		Decimal(10,2) Default(0)
		,ValorTarifaPesoEstimado	Decimal(10,2) Default(0)
	    ,ValorTarifaPesoReal		Decimal(10,2) Default(0)
	    ,ConciliacaoStatusId		Int	    
    )

	Create Table #ConciliacaoTEmp
    (
         ConciliacaoId                  BigInt 
		,ImportacaoCteId				Bigint
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
        ,FaturaId                       Int NULL --References Fretter.Fatura(FaturaId)
        ,ProcessadoIndicador            Bit Default(0) --Constraint DF_Fretter_Conciliacao_Indicador Default(0)  
	)

	Insert #ConciliacaoTemp
	Select 
		 ConciliacaoId  
		,ImportacaoCteId
		,EmpresaId                     
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
		,DevolvidoRemetente            
		,ConciliacaoStatusId           
		,DataEmissao                   
		,DataFinalizacao               
		,FaturaId                      
		,ProcessadoIndicador           
	From Fretter.Conciliacao (Nolock)
	Where ConciliacaoId Between @ConciliacaoInicial And @ConciliacaoFinal	

	--Pega as conciliacoes que estavam em processamento em algum lote anterior
	Insert #ConciliacaoTemp
	Select 
		 ConciliacaoId  
		,ImportacaoCteId
		,EmpresaId                     
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
		,DevolvidoRemetente            
		,ConciliacaoStatusId           
		,DataEmissao                   
		,DataFinalizacao               
		,FaturaId                      
		,ProcessadoIndicador           
	From Fretter.Conciliacao (Nolock)
	Where ConciliacaoId < @ConciliacaoInicial 
		And ProcessadoIndicador = 0
		And ConciliacaoStatusId Not In (1,4)

	Insert Into #IndicadorConciliacaoTemp
	(
		Data					
		,EmpresaId				
		,TransportadorId		
		,TransportadorCnpjId	
		,QtdEntrega				
		,QtdCte					
		,QtdSucesso										
		,QtdDivergencia			
		,QtdDivergenciaPeso		
		,QtdDivergenciaTarifa	
		,ValorCustoFreteEstimado
		,ValorCustoFreteReal			
	)
	Select		 
		[Data]                          = ISNULL(C.DataEmissao,E.Dt_Importacao)
		,EmpresaId						= C.EmpresaId		
		,TransportadorId				= C.TransportadorId
		,TransportadorCnpjId			= C.TransportadorCnpjId				
		,QuantidadeEntregas             = Count(C.EntregaId)
		,QuantidadeCte                  = Sum(Case When C.ImportacaoCteId > 0 Then 1 Else 0 End)
		,QuantidadeSucesso              = Sum(Case When C.ConciliacaoStatusId = 2 Then 1 Else 0 End)
		,QuantidadeDivergencia          = Sum(Case When C.ConciliacaoStatusId = 3 Then 1 Else 0 End)
		,QuantidadeDivergenciaPeso      = Sum(Case When C.PossuiDivergenciaPeso = 1 Then 1 Else 0 End)
		,QuantidadeDivergenciaTarifa    = Sum(Case When C.PossuiDivergenciaTarifa = 1 Then 1 Else 0 End)
		,ValorCustoFreteEstimado        = Sum(C.ValorCustoFrete)
		,ValorCustoFreteReal            = Sum(C.ValorCustoReal)		
	From #ConciliacaoTemp	C (nolock)		
	Join Tb_Edi_Entrega		E (nolock) On C.EntregaId = E.Cd_Id
	Where C.ConciliacaoStatusId Not In (4) --Ag. Recotacao
	Group By ISNULL(C.DataEmissao,E.Dt_Importacao)
			 ,C.EmpresaId		
			 ,C.TransportadorId
			 ,C.TransportadorCnpjId				

	Update #IndicadorConciliacaoTemp
	Set QtdErro = QtdCte - QtdSucesso

	Merge Fretter.IndicadorConciliacao	As Ic
	Using #IndicadorConciliacaoTemp		As Ie On Ic.Data = Ie.Data And Ic.EmpresaId = Ie.EmpresaId And Ic.TransportadorId = Ie.TransportadorId And Ic.TransportadorCnpjId = Ie.TransportadorCnpjId
	When Matched
	Then Update
	Set  Ic.QtdEntrega						+= Ie.QtdEntrega             
		 ,Ic.QtdCte							+= Ie.QtdCte                   
		 ,Ic.QtdSucesso						+= Ie.QtdSucesso               
		 ,Ic.QtdErro						+= Ie.QtdErro
		 ,Ic.QtdDivergencia					+= Ie.QtdDivergencia           
		 ,Ic.QtdDivergenciaPeso				+= Ie.QtdDivergenciaPeso       
		 ,Ic.QtdDivergenciaTarifa			+= Ie.QtdDivergenciaTarifa     
		 ,Ic.ValorCustoFreteEstimado		+= Ie.ValorCustoFreteEstimado         
		 ,Ic.ValorCustoFreteReal			+= Ie.ValorCustoFreteReal  
		 ,Ic.DataProcessamento				= Getdate()
	When Not Matched
	Then Insert 
	(
		Data
		,EmpresaId
		,TransportadorId
		,TransportadorCnpjId
		,QtdEntrega
		,QtdCte
		,QtdSucesso
		,QtdErro
		,QtdDivergencia
		,QtdDivergenciaPeso
		,QtdDivergenciaTarifa
		,ValorCustoFreteEstimado
		,ValorCustoFreteReal
		,ValorTarifaPesoEstimado
		,ValorTarifaPesoReal
		,DataProcessamento
		,Ativo
	)
	Values
	(
		Ie.Data
		,Ie.EmpresaId
		,Ie.TransportadorId
		,Ie.TransportadorCnpjId
		,Ie.QtdEntrega
		,Ie.QtdCte
		,Ie.QtdSucesso
		,Ie.QtdErro
		,Ie.QtdDivergencia
		,Ie.QtdDivergenciaPeso
		,Ie.QtdDivergenciaTarifa
		,Ie.ValorCustoFreteEstimado
		,Ie.ValorCustoFreteReal
		,ISNULL(Ie.ValorTarifaPesoEstimado,0)
		,ISNULL(Ie.ValorTarifaPesoReal,0)
		,Getdate()
		,1
	);			

	Declare @QtdConciliacao				Int
			,@QtdConciliacaoPendente	Int
			,@DataFinalizacaoMinima		Datetime
			,@DataFinalizacaoMaxima		Datetime

	Select 
		@QtdConciliacao				= Count(1)		
		,@DataFinalizacaoMinima		= Min(DataFinalizacao)
		,@DataFinalizacaoMaxima		= Max(DataFinalizacao)
	From #ConciliacaoTEmp

	Select @QtdConciliacaoPendente	= Count(1)		
	From #ConciliacaoTEmp 
	Where ConciliacaoStatusId In (1,4)

	Update Fretter.ControleProcessoIndicador
	Set Mensagem						= 'Controle processado com sucesso.'
		,Sucesso						= 1
		,Processado						= 1		
		,DataTermino					= Getdate()
		,Duracao						= DATEDIFF(SS,DataInicio,Getdate())
		,QtdConciliacao					= @QtdConciliacao
		,QtdConciliacaoPendente			= @QtdConciliacaoPendente
		,DataFinalizacaoMinima			= @DataFinalizacaoMinima
		,DataFinalizacaoMaxima			= @DataFinalizacaoMaxima
	Where ControleProcessoIndicadorId	= @ControleProcessoId

	Update Cc
	Set ProcessadoIndicador = 1
	From Fretter.Conciliacao	Cc (Nolock)
	Join #ConciliacaoTEmp		Ct (Nolock) On Cc.ConciliacaoId = Ct.ConciliacaoId
	Where Ct.ConciliacaoStatusId Not In (1,4)

End Try
Begin Catch
	Declare @ErrorMessage	Varchar(2048) = 'Erro Numero: ' + Cast(ERROR_NUMBER() As Varchar(256)) + ', Mensagem: ' + ERROR_MESSAGE() + + ', Linha Numero: ' + Cast(ERROR_LINE() As Varchar(256))		

	Update Fretter.ControleProcessoIndicador
	Set Mensagem						= @ErrorMessage
		,Sucesso						= 0
		,Processado						= 1		
		,DataTermino					= Getdate()
		,Duracao						= DATEDIFF(SS,DataInicio,Getdate())
	Where ControleProcessoIndicadorId	= @ControleProcessoId
End Catch

Select @QtdConciliacao As QtdConciliacao;
	
	
End