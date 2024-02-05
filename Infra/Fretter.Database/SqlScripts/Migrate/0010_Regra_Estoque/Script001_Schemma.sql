If Object_Id('dbo.Tb_SKU_RegraEstoque') Is NULL
Begin
	Create Table Tb_SKU_RegraEstoque
	(
		 Cd_Id					Int Identity Constraint PK_Tb_SKU_RegraEstoque Primary Key
		,Id_Empresa				Int References Tb_Adm_Empresa(Cd_Id)
		,Id_CanalOrigem			Int References Tb_Adm_Canal(Cd_Id)
		,Id_CanalDestino		Int References Tb_Adm_Canal(Cd_Id)
		,Id_Grupo				Int References Tb_SKU_Grupo(Cd_Id)
		,Skus					Varchar(4000)
		,Dt_Cadastro			Datetime Default(Getdate())
		,Cd_UsuarioCadastro		Int
		,Dt_Alteracao			Datetime
		,Cd_UsuarioAlteracao	Int
		,Flg_Ativo				Bit Default(1)
	)
End
Go

If Object_Id('dbo.Tb_SKU_RegraEstoqueHistorico') Is NULL
Begin
	Create Table Tb_SKU_RegraEstoqueHistorico
	(
		 Cd_Id				Int Identity Constraint PK_Tb_SKU_RegraEstoqueHistorico Primary Key
		,Id_RegraEstoque	Int References Tb_SKU_RegraEstoque(Cd_Id)
		,Id_CanalOrigem		Int References Tb_Adm_Canal(Cd_Id)
		,Id_CanalDestino	Int References Tb_Adm_Canal(Cd_Id)
		,Id_Produto			Int References Tb_SKU_Produto(Cd_Id)
		,Nr_Qtd_Transferido	Int
		,Nr_Qtd_Final		Int
		,Flg_CriadoDestino	Bit
		,DataCadastro		DateTime
	)
End
Go


If Not Exists (Select Top 1 1 From Tb_Sis_Menu (Nolock) Where Ds_link = 'RegraEstoque')
Begin
	
	Declare @IdPai Int = (Select Top 1 Cd_Id From Tb_Sis_Menu (Nolock) Where Ds_Menu = 'Interfaces')
	Insert Into Tb_Sis_Menu 
	(
		 Ds_Menu
		,Id_Pai
		,Flg_Ativo
		,Ds_Link
		,Nr_Ordem
		,Tp_Perfil
		,Flg_Fretter
	)
	Values('Regras de Estoque', @IdPai, 1, 'RegraEstoque', 9, Null, 1)

End
Go

If Not Exists ( Select Top 1 1 
				From Tb_Sis_Menu		M  (Nolock) 
				Join Tb_Sis_Menu_Ico	MI (Nolock) On M.Cd_Id = MI.id_menu
				Where M.Ds_link = 'RegraEstoque' )
Begin
	Declare @IdMenu Int = (Select Top 1 Cd_Id From Tb_Sis_Menu (Nolock) Where Ds_link = 'RegraEstoque')
	Insert Into Tb_Sis_Menu_Ico
	Values (@IdMenu, 'fa fa-archive')
End
Go


