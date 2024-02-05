Create Or ALTER  Procedure Fretter.GetDashboadEntregasGrafico
(
	@DataInicio			Date		= Null
	,@DataTermino		Date		= Null
	,@TransportadorId	Int			= 0
	,@CanalId			Int			= 0
	,@UsuarioId			Int			= 0
	,@EmpresaId			Int			= 0
)
As Begin
Set Nocount On;

	If @DataInicio	Is Null	Set @DataInicio	 = DateAdd(MONTH,-3, Concat(Format(GetDate(),'yyyy-MM'),'-01'))
	If @DataTermino Is Null Set @DataTermino = GetDate()
		
	Drop Table If Exists #StatusTemp
	Create Table #StatusTemp
	(
		StatusId	Int Identity(1,1)
		,Status		Varchar(64)		
	)

	Insert Into #StatusTemp
	(
		Status		
	)
	Values
	(
		'QtdEntrega'		
	),
	(
		'QtdSucesso'		
	),
	(
		'QtdErro'		
	),
	(
		'QtdDivergencia'		
	),
	(
		'QtdEntregaSemCte'		
	)
	
	;WITH DateCte AS 
	(
		Select  Status
				,DataI	= @DataInicio 
				,DataF	= @DataTermino
		From #StatusTemp
	), Cte AS (
		Select Status, DataI
		From DateCte
		Union ALL
		Select Ct.Status, DATEADD(Day,1,Ct.DataI)
		From Cte			Ct
		Inner Join DateCte	Ge On Ge.Status = Ct.Status
		Where Ct.DataI < Ge.DataF
	)	
	Select 
		Data		= Ct1.DataI	
		,Status		= Ct1.Status					
		,Quantidade = 0
	Into #CteTemp
	From Cte	Ct1	
	Order By Ct1.DataI,Ct1.Status
	Option (MAXRECURSION 0);	
	
	SELECT Data, Status, Quantidade = Cast(Quantidade As Int)
	Into #IndicadorTemp
	FROM  (
		SELECT 
			Data
			,QtdEntrega		  = SUM(QtdEntrega)
			,QtdSucesso		  = SUM(QtdSucesso)
			,QtdErro		  = SUM(QtdErro)
			,QtdDivergencia	  = SUM(QtdDivergencia)
			,QtdEntregaSemCte = SUM(QtdEntrega - QtdCte)
		FROM Fretter.IndicadorConciliacao IC(nolock)
		Where Ic.Data Between @DataInicio And @DataTermino
			And IC.EmpresaId = @EmpresaId
			And (IC.TransportadorId = @TransportadorId Or @TransportadorId = 0)
		Group By Data
	   ) p  
	UNPIVOT (Quantidade FOR Status IN   (QtdEntrega, QtdSucesso, QtdErro, QtdDivergencia, QtdEntregaSemCte))AS unpvt;    

    Update Ct
	Set Quantidade = It.Quantidade
	From #CteTemp			Ct
	Join #IndicadorTemp		It On Ct.Status = It.Status And Ct.Data = It.Data

	Select 
		Data		
		,Status		
		,Quantidade
	From #CteTemp
	Order by Data Asc
End