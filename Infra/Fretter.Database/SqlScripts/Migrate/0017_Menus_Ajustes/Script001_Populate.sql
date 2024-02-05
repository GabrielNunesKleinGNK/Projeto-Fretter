If Exists(Select Top 1 1 From dbo.Tb_Sis_Menu (nolock) Where Ds_Link = '/logDashboard' And Flg_Ativo = 1 And Id_Pai Is NULL)
Begin

	Declare @SisMenuPai			Int = 0
	Declare @SisMenuFilho		Int = 0

	Insert Into dbo.Tb_Sis_Menu
	(
		Ds_Menu
		,Id_Pai
		,Flg_Ativo
		,Ds_Link
		,Nr_Ordem
		,Flg_Fretter
	)
	Select 
		Ds_Menu			= 'Monitoria'
		,Id_Pai			= NULL
		,Flg_Ativo		= 1
		,Ds_Link		= ''
		,Nr_Ordem		= (Select COUNT(1) From dbo.Tb_Sis_Menu (Nolock) Where Id_Pai Is NULL And Flg_Fretter = 1 And Flg_Ativo = 1) + 1
		,Flg_Fretter	= 1

	Select @SisMenuPai = SCOPE_IDENTITY();

	Insert Into dbo.Tb_Sis_Menu_Ico
	(
		id_menu
		,ds_icone
	)
	Select
		id_menu		 = @SisMenuFilho
		,ds_icone	 = 'fa fa-laptop'

	Insert Into dbo.Tb_Sis_Menu
	(
		Ds_Menu
		,Id_Pai
		,Flg_Ativo
		,Ds_Link
		,Nr_Ordem
		,Flg_Fretter
		,Tp_Perfil
	)
	Select 
		Ds_Menu			= 'Log Dashboard'
		,Id_Pai			= @SisMenuPai
		,Flg_Ativo		= 1
		,Ds_Link		= '/logDashboard'
		,Nr_Ordem		= 1
		,Flg_Fretter	= 1
		,Tp_Perfil		= 'Administrador'

	Select @SisMenuFilho = SCOPE_IDENTITY();


	Insert Into dbo.Tb_Sis_Menu_Ico
	(
		id_menu
		,ds_icone
	)
	Select
		id_menu		 = @SisMenuFilho
		,ds_icone	 = 'fa fa-gauge-circle-minus'

	Insert Into dbo.Tb_Sis_Menu
	(
		Ds_Menu
		,Id_Pai
		,Flg_Ativo
		,Ds_Link
		,Nr_Ordem
		,Flg_Fretter
		,Tp_Perfil
	)
	Select 
		Ds_Menu			= 'Imp. Arquivo'
		,Id_Pai			= @SisMenuPai
		,Flg_Ativo		= 1
		,Ds_Link		= '/arquivoImportacaoLog'
		,Nr_Ordem		= 2
		,Flg_Fretter	= 1
		,Tp_Perfil		= 'Administrador'

	Select @SisMenuFilho = SCOPE_IDENTITY();

	Insert Into dbo.Tb_Sis_Menu_Ico
	(
		id_menu
		,ds_icone
	)
	Select
		id_menu		 = @SisMenuFilho
		,ds_icone	 = 'fa fa-th-list'

	Insert Into dbo.Tb_Sis_Menu
	(
		Ds_Menu
		,Id_Pai
		,Flg_Ativo
		,Ds_Link
		,Nr_Ordem
		,Flg_Fretter
		,Tp_Perfil
	)
	Select 
		Ds_Menu			= 'CEP Indisponivel'
		,Id_Pai			= @SisMenuPai
		,Flg_Ativo		= 1
		,Ds_Link		= '/logCotacaoFrete'
		,Nr_Ordem		= 3
		,Flg_Fretter	= 1
		,Tp_Perfil		= NULL

	Select @SisMenuFilho = SCOPE_IDENTITY();

	Insert Into dbo.Tb_Sis_Menu_Ico
	(
		id_menu
		,ds_icone
	)
	Select
		id_menu		 = @SisMenuFilho
		,ds_icone	 = 'fa fa-object-group'


	Insert Into dbo.Tb_Sis_Menu
	(
		Ds_Menu
		,Id_Pai
		,Flg_Ativo
		,Ds_Link
		,Nr_Ordem
		,Flg_Fretter
		,Tp_Perfil
	)
	Select 
		Ds_Menu			= 'Transporte Config.'
		,Id_Pai			= @SisMenuPai
		,Flg_Ativo		= 1
		,Ds_Link		= '/empresaTransporte'
		,Nr_Ordem		= 4
		,Flg_Fretter	= 1
		,Tp_Perfil		= NULL

	Select @SisMenuFilho = SCOPE_IDENTITY();

	Insert Into dbo.Tb_Sis_Menu_Ico
	(
		id_menu
		,ds_icone
	)
	Select
		id_menu		 = @SisMenuFilho
		,ds_icone	 = 'flaticon-cogwheel'		


	Delete From dbo.Tb_Sis_Menu_Ico Where id_menu IN (Select cd_id From dbo.Tb_Sis_Menu (Nolock) Where Ds_Link = '/logDashboard' And Flg_Ativo = 1 And Flg_Fretter = 1 And Id_Pai Is NULL)
	Delete From dbo.Tb_Sis_Menu Where Ds_Link = '/logDashboard' And Flg_Ativo = 1 And Flg_Fretter = 1 And Id_Pai Is NULL

	Delete From dbo.Tb_Sis_Menu_Ico Where id_menu IN (Select cd_id From dbo.Tb_Sis_Menu (Nolock) Where Ds_Link = '/arquivoImportacaoLog' And Flg_Ativo = 1 And Flg_Fretter = 1 And Id_Pai Is NULL)
	Delete From dbo.Tb_Sis_Menu Where Ds_Link = '/arquivoImportacaoLog' And Flg_Ativo = 1 And Flg_Fretter = 1 And Id_Pai Is NULL

	Delete From dbo.Tb_Sis_Menu_Ico Where id_menu IN (Select cd_id From dbo.Tb_Sis_Menu (Nolock) Where Ds_Link = '/empresaIntegracao' And Flg_Ativo = 1 And Flg_Fretter = 1)
	Delete From dbo.Tb_Sis_Menu Where Ds_Link = '/empresaIntegracao' And Flg_Ativo = 1 And Flg_Fretter = 1

End
