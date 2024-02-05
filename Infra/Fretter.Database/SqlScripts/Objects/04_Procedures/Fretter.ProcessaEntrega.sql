Create Or Alter Procedure Fretter.ProcessaEntrega
As
Begin

	Declare                
	    @DataI			Datetime          
	   ,@DataF			Datetime          
	   ,@Descricao		Varchar(4000)          
	   ,@Duracao		Int          
	   ,@Quantidade		Int          
	   ,@BatchSize		Int          
	   ,@CteIdInicial	BigInt          
	   ,@CteIdFinal		BigInt           
	   ,@ControleId		Int            
	   
	If Exists(Select Top 1 1 From Fretter.ControleProcessoEntrega (nolock) Where Processado = 0 And DataF is null and DATEDIFF(Minute, DataI, Getdate()) > 40)
	Begin
		Select 
			 @CteIdInicial = CteIdInicial
			,@CteIdFinal = CteIdFinal
			,@ControleId = ControleProcessoEntregaId
			,@DataI = Getdate()
		From Fretter.ControleProcessoEntrega (nolock)
		Where Processado = 0 And DataF is null and DATEDIFF(Minute, DataI, Getdate()) > 40
	
	End
	Else
	Begin
		If Exists(Select Top 1 1 From Fretter.ControleProcessoEntrega (nolock) Where Processado = 0 And DataF is null)
		Begin
			Print 'Ja existe uma instancia sendo executada.'          
			Return          
		End
	
		Select 
			 @CteIdInicial = Isnull(Max(CteIdFinal),0)
			,@Batchsize = 100000
			,@DataI = Getdate()
		From Fretter.ControleProcessoEntrega (nolock)
		
		Select 
			@CteIdFinal = Max(Cd_Id)
		From dbo.Tb_Edi_Entrega (nolock)
		Where Cd_Id >= @CteIdInicial
		
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
	
		Insert Into Fretter.ControleProcessoEntrega
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
	
	Drop Table If Exists #Conciliacao
	
	Select 
		 EmpresaId					= E.Id_Empresa
		,EntregaId					= E.Cd_Id
		,TransportadorId			= E.Id_Transportador
		,TransportadorCnpjId		= E.Id_TransportadorCnpj
		,ValorCustoFrete			= E.Vl_CustoFrete --validar isnull
		,ConciliacaoStatusId		= 1
		,Ativo						= 1
		,UsuarioCadastro			= ''
		,UsuarioAlteracao			= ''
		,DataCadastro				= Getdate()
		,DataAlteracao				= ''
		,DataImportacao				= E.Dt_Importacao
		,DataFinalizacao			= E.Dt_Finalizado
		,DataEmissao				= Cast(E.Dt_EmissaoNF As date)
		,ConciliacaoTipoId			= 1
	Into #Conciliacao
	From dbo.Tb_Edi_Entrega					 E (nolock)
	Inner Join Fretter.ContratoTransportador C (Nolock) On C.TransportadorCnpjId = E.Id_TransportadorCnpj And C.Ativo = 1 And E.Id_MicroServico = C.MicroServicoId And E.Id_Empresa = C.EmpresaId And C.VigenciaFinal >= Getdate() --trazer apenas entregas que contenham contrato ativo, Manter como esta para ter os dados para poder ofertar a clientes novos
	Where E.Cd_Id Between @CteIdInicial And @CteIdFinal
	
	Merge Fretter.Conciliacao As D
	Using #Conciliacao As O
	On O.EntregaId = D.EntregaId	
	When Matched
	Then Update
	Set  D.EmpresaId				= O.EmpresaId					
		,D.EntregaId				= O.EntregaId					
		,D.TransportadorId			= O.TransportadorId		
		,D.TransportadorCnpjId		= O.TransportadorCnpjId
		,D.ValorCustoFrete			= O.ValorCustoFrete			
		--,D.ConciliacaoStatusId		= O.ConciliacaoStatusId		
		,D.Ativo					= O.Ativo						
		,D.UsuarioCadastro			= O.UsuarioCadastro			
		,D.UsuarioAlteracao			= O.UsuarioAlteracao			
		,D.DataCadastro				= O.DataCadastro				
		,D.DataAlteracao			= O.DataAlteracao
		,D.DataFinalizacao			= O.DataFinalizacao
		,D.DataEmissao				= O.DataEmissao
		,D.ConciliacaoTipoId		= O.ConciliacaoTipoId
		
	When Not Matched
	Then Insert 
	(
		 EmpresaId					
		,EntregaId					
		,TransportadorId	
		,TransportadorCnpjId
		,ValorCustoFrete			
		,ConciliacaoStatusId		
		,Ativo						
		,UsuarioCadastro			
		,UsuarioAlteracao			
		,DataCadastro				
		,DataAlteracao
		,DataFinalizacao
		,DataEmissao
		,ConciliacaoTipoId
	)
	Values
	(
		 O.EmpresaId					
		,O.EntregaId					
		,O.TransportadorId	
		,O.TransportadorCnpjId
		,O.ValorCustoFrete			
		,O.ConciliacaoStatusId		
		,O.Ativo						
		,O.UsuarioCadastro			
		,O.UsuarioAlteracao			
		,O.DataCadastro				
		,O.DataAlteracao	
		,O.DataFinalizacao
		,O.DataEmissao
		,O.ConciliacaoTipoId
	);
	
	
	Select 
		 @Quantidade = Count(1)
		,@Duracao = Datediff(Second, @DataI, Getdate())
	
	Update C
	Set  Processado		= 1
		,Quantidade		= @Quantidade
		,Duracao		= @Duracao
		,DataF			= Getdate()
	From Fretter.ControleProcessoEntrega C
	Where ControleProcessoEntregaId = @ControleId

End