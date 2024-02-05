Create Or Alter  Procedure Fretter.GetDashboardResumo
(
	 @DataInicio	        Date	= Null
	,@DataTermino	        Date	= Null
	,@CanalId				Int		= 0
	,@TransportadorId		Int		= 0
	,@EmpresaId				Int		= 0
)
As Begin
Set Nocount ON;

	If @DataInicio	Is Null	Set @DataInicio	 = DateAdd(MONTH,-3, Concat(Format(GetDate(),'yyyy-MM'),'-01'))
	If @DataTermino Is Null Set @DataTermino = GetDate()

	Select 
		EntregasConciliadas		= Cast(ISNULL(SUM(Ic.QtdSucesso),0) As Int)
		,EntregasComProblema	= Cast(ISNULL(SUM(Ic.QtdErro),0) As Int)
		,EntregasComDivergencia	= Cast(ISNULL(SUM(Ic.QtdDivergencia),0) As Int)
		,EntregasSemCte			= Cast(ISNULL(SUM(QtdEntrega - QtdCte),0) As Int) --Rever processamento da conciliacao
		,CteSemEntregas			= Cast(0 As Int) --Rever processamento da conciliacao
	From Fretter.IndicadorConciliacao Ic(Nolock)
	Where Ic.Data Between @DataInicio And @DataTermino
		And (Ic.EmpresaId = @EmpresaId Or @EmpresaId = 0)
		And (Ic.TransportadorId = @TransportadorId Or @TransportadorId = 0)
End