
Create Or Alter Procedure Fretter.GetMicroServicoPorEmpresa
(
	@EmpresaId	Int
)
As Begin
Set Nocount On;

	Select Distinct
		 Id			= MS.Cd_Id
		,Descricao	= MS.Cd_Servico
		,Alias		= Case When MS.Cd_Servico2 Is Not NULl And  MS.Cd_Servico2 <> '' Then CONCAT(MS.Cd_Servico,'-',MS.Cd_Servico2) Else MS.Cd_Servico End
	From dbo.Tb_Adm_MicroServico				MS  (Nolock) 
	Join dbo.Tb_Adm_EmpresaTransportadorConfig	ETC (Nolock) On MS.Id_EmpresaTransportador = ETC.Cd_Id
	Where ETC.Id_Empresa = @EmpresaId
	And MS.Flg_Ativo = 1

End