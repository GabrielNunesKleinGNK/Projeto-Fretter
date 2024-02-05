Create Or Alter Procedure Fretter.GetCanaisUsuario
(
	@userId Int = 0,
	@empresaId Int = 0
)
As
Begin

	Declare @stringCanais Varchar(max);
	Select 
		@stringCanais =	CONCAT(@stringCanais, Replace(usc.ClaimValue, '_',';'), ';')
		from AspNetUsers us
		Join AspNetUserClaims usc On usc.UserId = us.Cd_Id
		Where 
		us.Cd_Id = @userId And 
	usc.ClaimType in ('Canal')

	Select Distinct 
		Cd_Id As Id, 
		Ds_NomeFantasia  as CanalNome,
		@empresaId as EmpresaId
	From
	(
		Select Distinct value as Cd_Id, can.Ds_NomeFantasia
		From 
		string_split(
			(@stringCanais), ';'
		) canaisId
		Inner Join Tb_Adm_Canal can On can.Cd_Id = value
		Union All
		Select Distinct
			can.Cd_Id, can.Ds_NomeFantasia 
		From Tb_Adm_Empresa em 
		Inner Join Tb_Adm_Canal can On can.Id_Empresa = em.Cd_Id And can.Cd_Cnpj = em.Cd_Cnpj
		Where em.Cd_Id = @empresaId
	) X
End
