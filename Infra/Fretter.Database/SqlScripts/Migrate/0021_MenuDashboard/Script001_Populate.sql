If Exists(Select Top 1 1 From tb_sis_menu Where Ds_Menu = 'Dashboard' and Flg_Fretter =1 and Ds_Link='/')
begin

	UPDATE Tb_Sis_Menu
	Set
		Ds_link='/dashboard',
		Id_Pai=(select Cd_Id from tb_sis_menu where Ds_Menu='Conciliação' and Flg_Fretter=1), 
		Nr_Ordem=1
	where Ds_Menu = 'Dashboard' and Flg_Fretter =1 and Ds_Link='/'

	UPDATE Tb_Sis_Menu
	Set Nr_Ordem=2
	where Ds_Menu = 'Faturas' and Flg_Fretter =1 and Id_Pai=(select Cd_Id from tb_sis_menu where Ds_Menu='Conciliação' and Flg_Fretter=1)

	UPDATE Tb_Sis_Menu
	Set Nr_Ordem=3
	where Ds_Menu = 'Importação CTe' and Flg_Fretter =1 and Id_Pai=(select Cd_Id from tb_sis_menu where Ds_Menu='Conciliação' and Flg_Fretter=1)

	UPDATE Tb_Sis_Menu
	Set Nr_Ordem=4
	where Ds_Menu = 'Transportador' and Flg_Fretter =1 and Id_Pai=(select Cd_Id from tb_sis_menu where Ds_Menu='Conciliação' and Flg_Fretter=1)

	UPDATE Tb_Sis_Menu
	Set Nr_Ordem=5
	where Ds_Menu = 'Relatórios' and Flg_Fretter =1 and Id_Pai=(select Cd_Id from tb_sis_menu where Ds_Menu='Conciliação' and Flg_Fretter=1)
end
GO
