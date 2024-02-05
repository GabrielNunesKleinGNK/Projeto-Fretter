Create or Alter Procedure Fretter.ProcessaConciliacaoOcorrencia
As Begin	
	
	Declare                
	    @DataI			Datetime          
	   ,@DataF			Datetime          
	   ,@Descricao		Varchar(4000)          
	   ,@Duracao		Int          
	   ,@Quantidade		Int          
	   ,@BatchSize		Int          
	   ,@IdInicial		BigInt          
	   ,@IdFinal		BigInt           
	   ,@ControleId		Int  
	   ,@Sql			NVarchar(4000)

	If Exists(Select Top 1 1 From Fretter.ControleProcessoOcorrencia  (Nolock) Where DataF Is NULL And DateDiff(Minute,DataI,Getdate()) > 60)    
	Begin    
		Update Fretter.ControleProcessoOcorrencia   
		Set DataF			= Getdate()    			
			,IdFinal		= IdInicial - 1						 
			,Processado		= 1    
		Where DataF Is NULL    
	End    	
	   
	If Exists(Select Top 1 1 From Fretter.ControleProcessoOcorrencia (nolock) Where Processado = 0 And DataF is null and DATEDIFF(Minute, DataI, Getdate()) > 40)
	Begin
		Select 
			 @IdInicial			= IdInicial
			,@IdFinal			= IdFinal
			,@ControleId		= ControleProcessoOcorrenciaId
			,@DataI				= Getdate()
		From Fretter.ControleProcessoOcorrencia (nolock)
		Where Processado = 0 And DataF is null and DATEDIFF(Minute, DataI, Getdate()) > 40
	
	End
	Else
	Begin
		If Exists(Select Top 1 1 From Fretter.ControleProcessoOcorrencia (nolock) Where Processado = 0 And DataF is null)
		Begin
			Print 'Ja existe uma instancia sendo executada.'          
			Return          
		End
	
		Select 
			 @IdInicial	= Isnull(Max(IdFinal),0)
			,@Batchsize		= 10000
			,@DataI			= Getdate()
		From Fretter.ControleProcessoOcorrencia (nolock)
		
		Select @IdFinal = Max(Cd_Id)
		From dbo.Tb_Edi_EntregaOcorrencia (nolock)
		Where Cd_Id >= @IdInicial				

		Set @IdFinal = Case When @IdFinal - @IdInicial >= @Batchsize Then @IdInicial + @Batchsize
							When @IdFinal - @IdInicial = 0			  Then 0
							When @IdFinal - @IdInicial < @BatchSize  Then @IdFinal
						  End		
		
		If @IdFinal = 0
		Begin
			Print 'Nenhum Registro a Processar'
			Return
		End
		
		Set @IdInicial = @IdInicial + 1
	
		Insert Into Fretter.ControleProcessoOcorrencia
		(
			 DataI
			,Quantidade
			,IdInicial
			,IdFinal
			,Duracao
			,Processado
		)
		Values
		(
			 @DataI
			,0
			,@IdInicial
			,@IdFinal
			,0
			,0
		)
		Set @ControleId = SCOPE_IDENTITY()
	End
	
	Drop Table If Exists #EntregaOcorrencia	
	Create Table #EntregaOcorrencia
	(
		Id							Int Identity(1,1)
		,EntregaOcorrenciaId		Bigint
		,EntregaId					Bigint	
		,Sigla						Varchar(16)	
		,ValorFrete					Decimal(9,4)
		,ValorFreteCalculado		Decimal(9,4)
		,Acrescimo					Bit Default(0)
		,RegraTipo					Int
		,Valor						Decimal(9,4)
		,EmpresaId					Int	
		,ConciliacaoTipoId			Int
		,TransportadorId			Int
		,TransportadorCnpjId		Int
		,DataEmissao				Date
		,ConciliacaoStatusId		Int
	)


	Set @Sql =
	N'Select Distinct
		EntregaOcorrenciaId					= Eo.Cd_Id
		,EntregaId							= EO.Id_Entrega
		,Sigla								= I.Nm_Sigla		
		,ValorFrete							= E.Vl_CustoFrete
		,ValorFreteCalculado				= 0
		,Acrescimo							= Cr.Acrescimo
		,RegraTipo							= Cr.ContratoTransportadorRegraTipoId --1 - Valor 2 - Percentual
		,Valor								= Cr.Valor		
		,EmpresaId							= E.Id_Empresa
		,ConciliacaoTipoId					= Cr.ConciliacaoTipoId
		,TransportadorId					= E.Id_Transportador
		,TransportadorCnpjId				= E.Id_TransportadorCnpj
		,DataEmissao						= E.Dt_EmissaoNF
		,ConciliacaoStatusId				= Null 
	From dbo.Tb_Edi_EntregaOcorrencia					Eo (Nolock)
	Inner Join dbo.Tb_Edi_Entrega						E  (Nolock) On E.Cd_Id = EO.Id_Entrega	
	Inner Join dbo.Tb_Edi_OcorrenciaEmpresaItem			I  (nolock) On I.Cd_Id = EO.Id_Ocorrencia
	Inner Join Fretter.ContratoTransportador			C  (Nolock) On C.TransportadorCnpjId = E.Id_TransportadorCnpj And C.Ativo = 1 And Getdate() Between C.VigenciaInicial And C.VigenciaFinal
	Inner Join Fretter.ContratoTransportadorRegra		Cr (Nolock) ON C.TransportadorId = Cr.TransportadorId And I.Cd_Id = Cr.OcorrenciaEmpresaItemId And Cr.Ativo = 1	
	Where EO.Cd_Id Between @PIdInicial And @PIdFinal And EO.Flg_OcorrenciaValidada = 1
		And Cr.ConciliacaoTipoId Not IN (1) --Entrega'		

	Insert Into #EntregaOcorrencia
	(
		EntregaOcorrenciaId			
		,EntregaId					
		,Sigla						
		,ValorFrete					
		,ValorFreteCalculado		
		,Acrescimo					
		,RegraTipo					
		,Valor						
		,EmpresaId				
		,ConciliacaoTipoId
		,TransportadorId			
		,TransportadorCnpjId
		,DataEmissao
		,ConciliacaoStatusId
	)
	Exec sp_executesql @Sql,
	N'@PIdInicial	Bigint
	 ,@PIdFinal		Bigint'
	 ,@PIdInicial	= @IdInicial
	 ,@PIdFinal		= @IdFinal

	
	--Remove caso j� possua alguma conciliacao em aberto do mesmo Tipo
	Delete Oc
	From #EntregaOcorrencia		Oc 
	Join Fretter.Conciliacao	Co(Nolock) ON Oc.EntregaId = Co.EntregaId And Oc.ConciliacaoTipoId = Co.ConciliacaoTipoId
	Where Co.ConciliacaoStatusId Not IN (1,4)
		And Co.ConciliacaoTipoId Not In (1) --Entrega

	;With TabelaOcorrenciaDuplicada As
	(
		Select 
			EntregaId
			,EntregaOcorrenciaId
			,RowId			= ROW_NUMBER() OVER (PARTITION BY EntregaId ORDER BY EntregaOcorrenciaId Desc)
		From #EntregaOcorrencia
	)
	Delete Eo
	From TabelaOcorrenciaDuplicada	Td
	Join #EntregaOcorrencia			Eo On Td.EntregaOcorrenciaId = Eo.EntregaOcorrenciaId
	Where Td.RowId > 1

	Update eo
	Set eo.ValorFrete = c.ValorCustoFrete
	From #EntregaOcorrencia eo
		Inner Join Fretter.Conciliacao c (Nolock) On c.EntregaId = eo.EntregaId And c.ConciliacaoTipoId = 1 And c.Ativo = 1
	Where eo.ValorFrete Is Null Or eo.ValorFrete = 0

	Update #EntregaOcorrencia
	Set ValorFreteCalculado = Case When RegraTipo = 1 Then Valor 
							When RegraTipo = 2 Then ValorFrete * (Valor/100)
						  Else 0 End

	Update eo 
	set eo.ConciliacaoStatusId		= Case
										When ct.PermiteTolerancia = 1 And ct.ToleranciaTipoId = 2 And c.ConciliacaoTipoId = 1 And eo.ValorFreteCalculado Between (c.ValorCustoReal - ct.ToleranciaInferior) And (c.ValorCustoReal + ct.ToleranciaSuperior) Then 2 
										When ct.PermiteTolerancia = 1 And ct.ToleranciaTipoId = 1 And c.ConciliacaoTipoId = 1 And eo.ValorFreteCalculado Between (c.ValorCustoReal - (c.ValorCustoReal * (ct.ToleranciaInferior/100))) And (c.ValorCustoReal + (c.ValorCustoReal * (ct.ToleranciaSuperior/100))) Then 2 
										When c.ValorCustoReal - eo.ValorFreteCalculado = 0 Then 2
									  Else 3 End
	from #EntregaOcorrencia eo
		Inner Join Fretter.Conciliacao c (Nolock) On c.EntregaId = eo.EntregaId And c.ConciliacaoTipoId =eo.ConciliacaoTipoId
		Inner Join Fretter.ContratoTransportador ct	(nolock) On ct.TransportadorCnpjId = c.TransportadorCnpjId And ct.EmpresaId = c.EmpresaId 

	Merge Fretter.Conciliacao	As D
	Using #EntregaOcorrencia	As O On O.EntregaId = D.EntregaId And O.ConciliacaoTipoId = D.ConciliacaoTipoId	
	When Matched
	Then Update
	Set D.ValorCustoFrete		= O.ValorFreteCalculado,
		D.ConciliacaoStatusId	= O.ConciliacaoStatusId
	When Not Matched
	Then Insert
	(
		 EntregaId
		,QuantidadeTentativas
		,DevolvidoRemetente
		,ConciliacaoStatusId
		,EmpresaId
		,TransportadorId
		,TransportadorCnpjId
		,ConciliacaoTipoId
		,ValorCustoFrete
		,DataEmissao
	)
	Values
	(
		 O.EntregaId
		,1
		,0
		,1
		,O.EmpresaId
		,O.TransportadorId
		,O.TransportadorCnpjId
		,O.ConciliacaoTipoId
		,O.ValorFreteCalculado		
		,O.DataEmissao
	);
	
	Select 
		 @Quantidade	= Count(Id)
		,@Duracao		= Datediff(Second, @DataI, Getdate())
	From #EntregaOcorrencia

	Update C
	Set  Processado = 1
		,Quantidade = @Quantidade
		,Duracao	= @Duracao
		,DataF		= Getdate()
	From Fretter.ControleProcessoOcorrencia C
	Where ControleProcessoOcorrenciaId = @ControleId

End