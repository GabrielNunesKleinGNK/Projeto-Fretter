Create Or Alter  Procedure Fretter.GetTransportadoresDaPorEmpresa
(
	 @EmpresaId			int = 0
)
As
Begin

	Select 
		ti.Cd_Id as Id,
		ti.Ds_NomeFantasia as TransportadorNome
	from dbo.Tb_Adm_EmpresaTransportadorConfig	tc(Nolock)
	Join dbo.Tb_Adm_Transportador				ti(Nolock) On ti.Cd_Id = tc.Id_Transportador
	Where tc.Id_Empresa = @EmpresaId
	Order By ti.Ds_NomeFantasia

End

