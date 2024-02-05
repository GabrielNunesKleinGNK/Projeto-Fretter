Create Or Alter Procedure Fretter.GetCanaisPorEmpresa
(
	 @EmpresaId			Int = 0
)
As
Begin

	Create Table #Canais
	(
		Id			Int
		,CanalNome	varchar(256)
	);
	

	if((Select count(1) From #Canais) = 0)
		Insert into #Canais (Id, CanalNome)
		Select 
			c.Cd_Id,
			c.Ds_NomeFantasia
		From dbo.Tb_Adm_Segmento s
		Join dbo.Tb_Adm_Canal c On c.Id_Segmento = s.Cd_Id
		Where s.Id_Empresa = @EmpresaId
		Order By c.Ds_NomeFantasia


	Select Id, CanalNome From #Canais
End