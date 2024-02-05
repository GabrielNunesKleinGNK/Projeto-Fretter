/****** Object:  StoredProcedure [Fretter].[GetSisMenu]    Script Date: 14/02/2022 18:11:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
Create  Or Alter  Procedure [Fretter].[GetSisMenu]
(
	 @UserId			Int = 0,
	 @IsAdmin			bit = 0,
	 @IsFretter			bit = 0
)
As Begin
Set Nocount On;

	SELECT DISTINCT 
			me.Cd_Id IdMenu,
		   Ds_Menu DsMenu,
		   Id_Pai IdPai,
		   Ds_Link DsLink,
		   Nr_Ordem NrOrdem,
	   mei.ds_icone Icone
      FROM Tb_Sis_Menu		me (nolock)
	  JOIN Tb_Sis_Menu_Ico	mei(nolock)  ON me.Cd_Id = mei.id_menu
	   where Flg_Fretter = @IsFretter and
		flg_ativo=1 and(
			@IsAdmin =1 or -- isAdminitrador
			me.cd_id in(
				Select m.Id_Menu
				From Tb_Sis_RoleMenu m (Nolock)
				where m.Id_Role in(
						select a.RoleId
						From AspNetUserRoles a (Nolock)
						where a.UserId =@UserId
				)
			)
			OR (CAST(me.Cd_Id AS nvarchar) in(
				 SELECT C.ClaimValue
					FROM AspNetUserClaims C (NOLOCK)
					WHERE C.UserId = @UserId
					And c.ClaimType = 'Menu'
			 ))
		)
	UNION
	SELECT DISTINCT me.Cd_Id IdMenu,
			Ds_Menu DsMenu,
			Id_Pai IdPai,
			Ds_Link DsLink,
			Nr_Ordem NrOrdem,
			mei.ds_icone Icone
      FROM Tb_Sis_Menu		me(nolock)
	  JOIN Tb_Sis_Menu_Ico mei(nolock)ON me.Cd_Id = mei.id_menu
	  Where
		me.Cd_Id in(
		SELECT  Id_Pai
		FROM Tb_Sis_Menu me (Nolock)
		where
		Flg_Fretter = @IsFretter and
		flg_ativo=1 and(
			@IsAdmin =1 or -- isAdminitrador
			cd_id in(
				Select m.Id_Menu
				From Tb_Sis_RoleMenu m (Nolock)
				where m.Id_Role in(
						select a.RoleId
						From AspNetUserRoles a
						where a.UserId =@UserId
				)
			)
			OR (CAST(me.Cd_Id AS nvarchar) in(
				 SELECT C.ClaimValue
					FROM AspNetUserClaims C (NOLOCK)
					WHERE C.UserId = @UserId
					And c.ClaimType = 'Menu'
			 ))
		)
		)
End