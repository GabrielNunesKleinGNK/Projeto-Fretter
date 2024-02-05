If Not Exists(Select Top 1 1 From tb_sis_menu Where Ds_Link = '/logDashboard')
begin
	insert into tb_sis_menu
		(Ds_Menu
		,Id_Pai
		,Flg_Ativo
		,Ds_Link
		,Nr_Ordem
		,Tp_Perfil
		,Flg_Fretter)
	values(
		'Log Dashboard'
		,null
		,1
		,'/logDashboard'
		,5
		,null
		,1)


	insert into tb_sis_menu_ico
	(Id_Menu, Ds_Icone)
	values((select top 1 cd_id from tb_sis_menu where Ds_Menu = 'Log Dashboard') ,'fa fa-chart-line')

end
GO
