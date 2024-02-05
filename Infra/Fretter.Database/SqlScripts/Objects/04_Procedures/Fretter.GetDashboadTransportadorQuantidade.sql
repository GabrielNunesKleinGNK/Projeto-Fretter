Create Or Alter  Procedure Fretter.GetDashboadTransportadorQuantidade
(
	 @DataInicio		Date		= Null
	,@DataTermino		Date		= Null
	,@TransportadorId	Int			= 0
	,@CanalId			Int			= 0
	,@UsuarioId			Int			= 0
	,@EmpresaId			Int			= 0
)
As Begin

	If @DataInicio	Is Null	Set @DataInicio	 = DateAdd(MONTH,-3, Concat(Format(GetDate(),'yyyy-MM'),'-01'))
	If @DataTermino Is Null Set @DataTermino = GetDate()

    Create Table #Result
    (
        Transportador	Varchar(256)
        ,Quantidade		Int
        ,Valor			Float
    )   
 
    Insert Into #Result
	(
		Transportador
		,Quantidade
		,Valor
	)
	Select 
		Transportador	= ISNULL(Tp.Ds_NomeFantasia,Tp.Ds_RazaoSocial)
		,Quantidade		= Sum(QtdEntrega)
		,Valor			= Sum(ISNULL(ValorCustoFreteReal,0))
	From Fretter.IndicadorConciliacao	Ic(Nolock)
	Join dbo.Tb_Adm_Transportador		Tp(Nolock) On Ic.TransportadorId = Tp.Cd_Id
	Where Data Between @DataInicio And @DataTermino    
		And (Ic.EmpresaId		= @EmpresaId)
		And (Ic.TransportadorId = @TransportadorId Or @TransportadorId = 0)
	Group By ISNULL(Tp.Ds_NomeFantasia,Tp.Ds_RazaoSocial)

    Select Top 5 
			Transportador
			,Quantidade
			,Valor
	From #Result 
	Order By Quantidade Desc

End