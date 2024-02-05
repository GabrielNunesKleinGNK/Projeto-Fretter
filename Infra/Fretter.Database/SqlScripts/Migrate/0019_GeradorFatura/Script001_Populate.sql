If Not Exists(Select Top 1 1 From tb_sis_menu Where Ds_Link = '/gerarFatura')
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
		'Gerar Fatura'
		,(select top 1 cd_id from tb_sis_menu where ds_menu like'%Conciliação%' and Flg_fretter=1)
		,1
		,'/gerarFatura'
		,5
		,null
		,1)


	insert into tb_sis_menu_ico
	(Id_Menu, Ds_Icone)
	values((select top 1 cd_id from tb_sis_menu where Ds_Menu = 'Gerar Fatura') ,'fa fa-file-contract')

end
GO
