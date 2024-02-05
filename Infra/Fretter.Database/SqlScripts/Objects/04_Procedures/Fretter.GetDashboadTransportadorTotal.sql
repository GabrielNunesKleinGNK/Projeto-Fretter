Create or Alter Procedure Fretter.GetDashboardTransportadorTotal
(
	 @DataInicio		Date		= Null
	,@DataTermino		Date		= Null
	,@TransportadorId	Int			= 0
	,@CanalId			Int			= 0
	,@UsuarioId			Int			= 0
	,@EmpresaId			Int			= 0
)
As Begin

	If @DataInicio	Is Null
	Set @DataInicio	 = DateAdd(MONTH,-3, Concat(Format(GetDate(),'yyyy-MM'),'-01'))
	If @DataTermino Is Null Set @DataTermino = GetDate()

	select 
	tp.Ds_NomeFantasia Transportador,
	sum(ic.QtdEntrega) QtdEntrega,
	sum(ic.QtdCte) QtdCte,
	sum(ic.QtdSucesso) QtdSucesso,
	sum(ic.QtdErro) QtdErro,
	sum(ic.QtdDivergencia) QtdDivergencia,
	sum(ic.QtdDivergenciaPeso) QtdDivergenciaPeso,
	sum(ic.QtdDivergenciaTarifa) QtdDivergenciaTarifa
	from Fretter.IndicadorConciliacao (nolock) Ic
	join Tb_Adm_Transportador(nolock) tp on tp.Cd_Id = ic.TransportadorId
	Where Data Between @DataInicio And @DataTermino    
		And (Ic.EmpresaId		= @EmpresaId)
		And (Ic.TransportadorId = @TransportadorId Or @TransportadorId = 0)
	group by tp.Ds_NomeFantasia

End

