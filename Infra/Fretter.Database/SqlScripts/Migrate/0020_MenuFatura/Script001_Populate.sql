If Not Exists(Select Top 1 1 From tb_sis_menu Where Ds_Menu = 'Faturas' and Ds_Link is null)
begin

	UPDATE Tb_Sis_Menu
	Set Ds_link=''
	where Ds_Menu='Faturas' and Flg_Fretter=1

	UPDATE Tb_Sis_Menu
	Set Id_Pai=(select Cd_Id from tb_sis_menu where Ds_Menu='Faturas' and Flg_Fretter=1),Ds_Menu ='Gerar Manual', Nr_Ordem=2
	where Ds_Link='/gerarFatura' and Flg_Fretter=1

	if Exists (Select Top 1 1 From tb_sis_menu where ds_menu like'Faturas' and Flg_fretter=1)
	Begin
		Declare @MenuId Int = 0

		insert into tb_sis_menu
			(Ds_Menu
			,Id_Pai
			,Flg_Ativo
			,Ds_Link
			,Nr_Ordem
			,Tp_Perfil
			,Flg_Fretter)
		values(
			'Detalhado'
			,(select top 1 cd_id from tb_sis_menu where ds_menu like'Faturas' and Flg_fretter=1)
			,1
			,'/faturas'
			,1
			,null
			,1)

		Select @MenuId = SCOPE_IDENTITY();

		insert into tb_sis_menu_ico(Id_Menu, Ds_Icone)
		values(@MenuId,'fa fa-list')
	End

end
GO