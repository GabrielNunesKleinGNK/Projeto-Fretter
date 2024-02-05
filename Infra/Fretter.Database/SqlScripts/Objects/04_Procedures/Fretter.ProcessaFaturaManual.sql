﻿Create Or Alter Procedure Fretter.ProcessaFaturaManual
(
	@entregaConciliacao Fretter.Tp_EntregaConciliacaoFatura READONLY,
	@userId INT
)
As Begin
Set Nocount ON;

If Exists(Select Top 1 1 From @entregaConciliacao)
Begin	
	
		Drop Table If Exists #FaturaTemp;
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
		
		Select 
			ConciliacaoId					= Co.ConciliacaoId
			,EmpresaId						= Co.EmpresaId
			,TransportadorId				= Co.TransportadorId
			,TransportadorCnpjId			= Co.TransportadorCnpjId
			,TransportadorCnpjCobrancaId	= Ct.TransportadorCnpjCobrancaId
			,FaturaPeriodoId				= Cast(NULL As int)
			,DevolvidoRemetente				= Co.DevolvidoRemetente
			,ValorCustoFrete				= Co.ValorCustoFrete
			,ValorCustoAdicional			= Co.ValorCustoAdicional
			,ValorCustoReal					= Co.ValorCustoReal			
			,JsonValoresCte					= Co.JsonValoresCte
		Into #ConciliacaoTemp		
		From Fretter.Conciliacao			Co(Nolock)
		Join Tb_Edi_Entrega					Et(Nolock) On Co.EntregaId = Et.Cd_Id
		Join Fretter.ContratoTransportador	Ct(Nolock) On Ct.TransportadorCnpjId = Co.TransportadorCnpjId And Ct.EmpresaId = Co.EmpresaId And Et.Id_MicroServico = Ct.MicroServicoId		
		Where Co.Ativo = 1
			And Ct.Ativo = 1
			And Co.ConciliacaoId In (select ConciliacaoId From @entregaConciliacao where Selecionado = 1)

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
			EmpresaId						= Co.EmpresaId
			,TransportadorId				= Co.TransportadorCnpjCobrancaId
			,FaturaPeriodoId				= null
			,ValorCustoFrete				= SUM(Co.ValorCustoFrete)
			,ValorCustoAdicional			= SUM(Co.ValorCustoAdicional)
			,ValorCustoReal					= SUM(Co.ValorCustoReal)
			,QuantidadeDevolvidoRemetente	= SUM(Cast(Co.DevolvidoRemetente As Int))
			,QuantidadeEntrega				= Count(Co.ConciliacaoId)
			,QuantidadeSucesso				= Count(Co.ConciliacaoId) - SUM(Cast(Co.DevolvidoRemetente As Int))
			,FaturaStatusId					= 3
			,DataVencimento					= DATEFROMPARTS(DatePart(Year,Getdate()), DatePart(Month,Getdate()),DatePart(Day,Getdate()))							
		From #ConciliacaoTemp				Co(Nolock)	
		Group BY Co.EmpresaId
				,Co.TransportadorCnpjCobrancaId					

				
		Declare @FaturaProcessada Table 
		(
			FaturaId			Int 
			,EmpresaId			Int
			,TransportadorId	Int
			,ValorCobradoCTE	Decimal(10,4)
		);

		Insert Into Fretter.Fatura
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
			,UsuarioCadastro
			,Ativo				
		)
		Output inserted.FaturaId, inserted.EmpresaId, inserted.TransportadorId, inserted.ValorCustoReal Into @FaturaProcessada (FaturaId,EmpresaId,TransportadorId,ValorCobradoCTE)
		Select 
			Fe.EmpresaId
			,Fe.TransportadorId
			,Fe.FaturaPeriodoId
			,Fe.ValorCustoFrete
			,Fe.ValorCustoAdicional
			,Fe.ValorCustoReal
			,ISNULL(Fe.QuantidadeDevolvidoRemetente,0)
			,ISNULL(Fe.QuantidadeEntrega,0)
			,ISNULL(Fe.QuantidadeSucesso,0)				
			,Fe.FaturaStatusId
			,Fe.DataVencimento								
			,DateAdd(Hour,-3,Getdate())
			,@userId
			,1
		FROM #FaturaTemp Fe		

		Select
			FaturaId			= CAST(0 As bigint)
			,EmpresaId
			,TransportadorId	= t.TransportadorCnpjCobrancaId
			,Chave				= JSON_VALUE(j.[value], '$.chave')
			,Valor				= SUM(CAST(JSON_VALUE(j.[value], '$.valor') AS Decimal(10,4)))
		Into #FaturaItemTemp
		From #ConciliacaoTemp					t
		Cross Apply OpenJson(t.JsonValoresCte)	j
		Cross Apply OpenJson(value,'$') With (tipo int '$.tipo') Where tipo = 1
		Group By  EmpresaId,t.TransportadorCnpjCobrancaId,JSON_VALUE(j.[value], '$.chave')
		Order By  EmpresaId,t.TransportadorCnpjCobrancaId,JSON_VALUE(j.[value], '$.chave')

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
			Join @FaturaProcessada	Fp On Fi.FaturaId = Fp.FaturaId
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
			,DataCadastro		= DateAdd(Hour,-3,Getdate())
			,DataAlteracao		= DateAdd(Hour,-3,Getdate())
			,Ativo				= 1
			,FaturaId
		From #FaturaItemTemp		

		Insert Into Fretter.FaturaHistorico
		(
			FaturaId
			,FaturaStatusId
			,Descricao
			,ValorCustoFrete
			,ValorCustoAdicional
			,ValorCustoReal
			,QuantidadeEntrega
			,QuantidadeSucesso
			,UsuarioCadastro
			,Ativo
		)
		SELECT FaturaId
			,FaturaStatusId
			,Concat('Fatura gerada manualmente - Total de entregas listadas: ',(Select Count(1) From @entregaConciliacao),'/ Selecionadas:',(Select Count(1) From @entregaConciliacao Where Selecionado=1))
			,ValorCustoFrete
			,ValorCustoAdicional
			,ValorCustoReal
			,QuantidadeEntrega
			,QuantidadeSucesso
			,UsuarioCadastro
			,Ativo
		From Fretter.Fatura
		Where FaturaId in(Select FaturaId From @FaturaProcessada)

		Select FaturaId As Id From @FaturaProcessada;  
	End
End
