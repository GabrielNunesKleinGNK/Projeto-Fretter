Create Or Alter Procedure Fretter.ProcessaFatura
As Begin
Set Nocount ON;

If Exists(Select Top 1 1 
			From Fretter.FaturaPeriodo	Fp(Nolock) 
			Join Fretter.FaturaCiclo	Fc(Nolock) On Fp.FaturaCicloId = Fc.FaturaCicloId
			Where Fp.Vigente = 1 And Fp.Processado = 0 And Fp.Ativo = 1 And Fc.DiaFechamento < DatePart(Day,Getdate()))
Begin	
	Declare @PeriodoProcessamento Table
	(
		Id					Int Identity(1,1) Primary Key
		,DataI				Date
		,DataF				Date
		,FaturaPeriodoId	Int
		,FaturaCicloId		Int
		,DiaVencimento		Smallint
	)

	Insert Into @PeriodoProcessamento
	(
		DataI
		,DataF
		,FaturaPeriodoId
		,FaturaCicloId
		,DiaVencimento
	)
	Select 
		DataI			 = Fp.DataInicio
		,DataF			 = Fp.DataFim
		,FaturaPeriodoId = Fp.FaturaPeriodoId
		,FaturaCicloId	 = Fp.FaturaCicloId
		,DiaVencimento	 = Fp.DiaVencimento
	From Fretter.FaturaPeriodo	Fp(Nolock) 
	Join Fretter.FaturaCiclo	Fc(Nolock) On Fp.FaturaCicloId = Fc.FaturaCicloId
	Where Fp.Vigente = 1 And Fp.Processado = 0 And Fp.Ativo = 1 And Fc.DiaFechamento < DatePart(Day,Getdate())

	Declare @DataI					Datetime
			,@DataF					Datetime
			,@DataIFinal			Datetime
			,@DataFFInal			Datetime
			,@CicloId				Int
			,@CicloMinId			Int = 1
			,@CicloMaxId			Int
			,@PeriodoId				Int
			,@DiaVencimento			SmallInt
			,@InicioProcessamento	Datetime = Getdate()

	Select @CicloMaxId = Count(1)
	From @PeriodoProcessamento

	While @CicloMinId <= @CicloMaxId
	Begin
		Select 
			@CicloId		= FaturaCicloId
			,@PeriodoId		= FaturaPeriodoId
			,@DataI			= DataI			
			,@DataIFinal	= DataI
			,@DataFFInal	= DataF
			,@DiaVencimento	= DiaVencimento
		From @PeriodoProcessamento
		Where Id = @CicloMinId

		While @DataIFinal <= @DataFFInal
		Begin
			Set @DataF = DateAdd(Second,-1,DateAdd(Day,1,@DataIFinal));

			Declare @FaturaProcessada Table 
			(
				FaturaId			Int 
				,EmpresaId			Int
				,TransportadorId	Int
				,ValorCobradoCTE	Decimal(10,4)
			);  

			Drop Table If Exists #ConciliacoesSelecionadas, #FaturaTemp, #FaturaItemTemp;			
			Create Table #ConciliacoesSelecionadas
			(
				FaturaTempId					Int Identity(1,1) 
				,EmpresaId						Int 
				,TransportadorCnpjCobrancaId	Int
				,ValorCustoFrete				Decimal(16,2)
				,ValorCustoAdicional			Decimal(16,2)
				,ValorCustoReal					Decimal(16,2)
				,DevolvidoRemetente				Int
				,ConciliacaoId					Int
				,JsonValoresCte					Varchar(Max)
			)
			
			Create Table #FaturaTemp
			(
				FaturaTempId					Int Identity(1,1) 
				,EmpresaId						Int 
				,TransportadorId				Int
				,FaturaPeriodoId				Int 
				,ValorCustoFrete				Decimal(16,2)
				,ValorCustoAdicional			Decimal(16,2)
				,ValorCustoReal					Decimal(16,2)
				,QuantidadeDevolvidoRemetente	Int
				,QuantidadeEntrega				Int
				,QuantidadeSucesso				Int 
				,FaturaStatusId					Int 
				,DataVencimento					Date								
			)

			Insert Into #ConciliacoesSelecionadas
			(
				EmpresaId					
				,TransportadorCnpjCobrancaId					
				,ValorCustoFrete			
				,ValorCustoAdicional		
				,ValorCustoReal				
				,DevolvidoRemetente			
				,ConciliacaoId							
				,JsonValoresCte				
			)
			Select 
				Co.EmpresaId
				,Ct.TransportadorCnpjCobrancaId
				,Co.ValorCustoFrete
				,Co.ValorCustoAdicional
				,Co.ValorCustoReal
				,Co.DevolvidoRemetente
				,Co.ConciliacaoId
				,JsonValoresCte
			From Fretter.Conciliacao			Co(Nolock)
			Join Fretter.ContratoTransportador	Ct(Nolock) On Ct.TransportadorCnpjCobrancaId = Co.TransportadorCnpjId 
			Join dbo.Tb_Edi_Entrega				Et(Nolock) On Co.EntregaId = Et.Cd_Id
			Where Co.EntregaId Is Not NULL
				And Co.ConciliacaoStatusId <> 1
				And Co.Ativo			= 1
				And Ct.FaturaCicloId	= @CicloId
				And Ct.FaturaAutomatica = 1
				And Et.Dt_Finalizado Between @DataIFinal And @DataF --Analisar qual Data				

			Insert Into #FaturaTemp
			(
				EmpresaId						
				,TransportadorId				
				,FaturaPeriodoId				
				,ValorCustoFrete				
				,ValorCustoAdicional			
				,ValorCustoReal					
				,QuantidadeDevolvidoRemetente	
				,QuantidadeEntrega				
				,QuantidadeSucesso				
				,FaturaStatusId					
				,DataVencimento					
			)			
			Select 
				EmpresaId						= EmpresaId
				,TransportadorId				= TransportadorCnpjCobrancaId
				,FaturaPeriodoId				= @PeriodoId
				,ValorCustoFrete				= SUM(ValorCustoFrete)
				,ValorCustoAdicional			= SUM(ValorCustoAdicional)
				,ValorCustoReal					= SUM(ValorCustoReal)
				,QuantidadeDevolvidoRemetente	= SUM(Cast(DevolvidoRemetente As Int))
				,QuantidadeEntrega				= Count(ConciliacaoId)
				,QuantidadeSucesso				= Count(ConciliacaoId) - SUM(Cast(DevolvidoRemetente As Int))
				,FaturaStatusId					= 3
				,DataVencimento					= DATEFROMPARTS(DatePart(Year,Getdate()), DatePart(Month,Getdate()), @DiaVencimento)
			From #ConciliacoesSelecionadas		(Nolock)
			Group BY 
				 EmpresaId
				 ,TransportadorCnpjCobrancaId				 

			Merge Fretter.Fatura As Ft
			Using #FaturaTemp	 As Fe On Ft.EmpresaId = Fe.EmpresaId And Ft.TransportadorId = Fe.TransportadorId And Ft.FaturaPeriodoId = Fe.FaturaPeriodoId		
			When Matched
			Then Update
			Set   Ft.ValorCustoFrete				+= ISNull(Fe.ValorCustoFrete,0)
				 ,Ft.ValorCustoAdicional			+= ISNull(Fe.ValorCustoAdicional,0)
				 ,Ft.ValorCustoReal					+= ISNull(Fe.ValorCustoReal,0)
				 ,Ft.QuantidadeDevolvidoRemetente	+= ISNull(Fe.QuantidadeDevolvidoRemetente,0)
				 ,Ft.QuantidadeEntrega				+= ISNull(Fe.QuantidadeEntrega,0)
				 ,Ft.QuantidadeSucesso				+= ISNull(Fe.QuantidadeSucesso,0)		
			When Not Matched
			Then Insert 
			(
				EmpresaId
				,TransportadorId
				,FaturaPeriodoId
				,ValorCustoFrete
				,ValorCustoAdicional
				,ValorCustoReal
				,QuantidadeDevolvidoRemetente
				,QuantidadeEntrega
				,QuantidadeSucesso				
				,FaturaStatusId
				,DataVencimento				
				,DataCadastro				
				,Ativo				
			)
			Values
			(
				Fe.EmpresaId
				,Fe.TransportadorId
				,Fe.FaturaPeriodoId
				,Fe.ValorCustoFrete
				,Fe.ValorCustoAdicional
				,Fe.ValorCustoReal
				,Fe.QuantidadeDevolvidoRemetente
				,Fe.QuantidadeEntrega
				,Fe.QuantidadeSucesso				
				,Fe.FaturaStatusId
				,Fe.DataVencimento								
				,Getdate()				
				,1
			)			
			Output inserted.FaturaId, inserted.EmpresaId, inserted.TransportadorId, inserted.ValorCustoReal Into @FaturaProcessada (FaturaId,EmpresaId,TransportadorId,ValorCobradoCTE);						

			Select
				FaturaId			= CAST(0 As bigint)
				,EmpresaId
				,TransportadorId	= TransportadorCnpjCobrancaId
				,Chave				= JSON_VALUE(j.[value], '$.chave')
				,Valor				= SUM(CAST(JSON_VALUE(j.[value], '$.valor') AS Decimal(10,4)))
			Into #FaturaItemTemp
			From #ConciliacoesSelecionadas			t
			Cross Apply OpenJson(t.JsonValoresCte)	j
			Cross Apply OpenJson(value,'$') With (tipo int '$.tipo') Where tipo = 1
			Group By  EmpresaId,TransportadorCnpjCobrancaId,JSON_VALUE(j.[value], '$.chave')
			Order By  EmpresaId,TransportadorCnpjCobrancaId,JSON_VALUE(j.[value], '$.chave')

			Update Fi
			Set FaturaId = Fp.FaturaId
			From #FaturaItemTemp	Fi
			Join @FaturaProcessada	Fp On Fi.EmpresaId = Fp.EmpresaId And Fi.TransportadorId = Fp.TransportadorId				

			;With FaturaItemValor As
			(				
				Select 
					Fp.FaturaId
					,ValorItem		= SUM(Fi.Valor)
				From #FaturaItemTemp	Fi
				Join @FaturaProcessada	Fp On Fi.EmpresaId = Fp.EmpresaId And Fi.TransportadorId = Fp.TransportadorId
				Group BY Fp.FaturaId
			)
			Insert Into #FaturaItemTemp
			(
				FaturaId
				,EmpresaId
				,TransportadorId
				,Chave 
				,Valor 
			)
			Select
				FaturaId		 = Fi.FaturaId
				,EmpresaId		 = Fp.EmpresaId
				,TransportadorId = Fp.TransportadorId
				,Chave			 = 'Demais valores'
				,Valor			 = Fp.ValorCobradoCTE - Fi.ValorItem
			From @FaturaProcessada	Fp
			Join FaturaItemValor	Fi On Fp.FaturaId = Fi.FaturaId

			Insert Into Fretter.FaturaItem
			(
				Valor,
				Descricao,
				UsuarioCadastro,
				UsuarioAlteracao,
				DataCadastro,
				DataAlteracao,
				Ativo,
				FaturaId
			)
			Select 
				Valor				= Valor
				,Descricao			= Chave
				,UsuarioCadastro	= 0
				,UsuarioAlteracao	= 0
				,DataCadastro		= GETDATE()
				,DataAlteracao		= Getdate()
				,Ativo				= 1
				,FaturaId
			From #FaturaItemTemp		

			Set @DataIFinal = DateAdd(Day,1,@DataIFinal);
		End


		Update Fretter.FaturaPeriodo 
		Set DataProcessamento		= Getdate()
			,Processado				= 1
			,DuracaoProcessamento	= DateDiff(Second,@InicioProcessamento,Getdate())
		Where FaturaPeriodoId		= @PeriodoId

		Set @CicloMinId += 1;
	End		
End


End