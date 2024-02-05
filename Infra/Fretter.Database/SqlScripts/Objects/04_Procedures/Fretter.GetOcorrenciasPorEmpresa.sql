Create Or Alter Procedure Fretter.GetOcorrenciasPorEmpresa
(
	@EmpresaId	Int
)
As
Begin
	Select 
		 Id				= Oi.Cd_Id
		,Descricao		= Oi.Ds_Ocorrencia
		,Sigla			= Oi.Nm_Sigla
		,OcorrenciaNome = Oi.Nm_Sigla + ' - '  + Oi.Ds_Ocorrencia
	From Tb_Edi_OcorrenciaEmpresa		Oe(Nolock)
	Join Tb_Edi_OcorrenciaEmpresaItem	Oi(Nolock) ON Oe.Cd_Id = Oi.Id_OcorrenciaEmpresa
	Where Id_TipoServico = 1
	And Id_Empresa = @EmpresaId
	And Flg_Ativo = 1
End