If Not Exists(Select Top 1 1 From tb_sis_menu Where Ds_Link = '/arquivoImportacaoLog')
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
		'Log dos arquivos de importação'
		,null
		,1
		,'/arquivoImportacaoLog'
		,6
		,null
		,1)


	insert into tb_sis_menu_ico
	(Id_Menu, Ds_Icone)
	values((select top 1 cd_id from tb_sis_menu where Ds_Menu = 'Log dos arquivos de importação') ,'fa fa-list')

end
GO
