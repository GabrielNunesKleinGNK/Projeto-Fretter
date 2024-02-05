If Object_Id('dbo.Tb_Oco_Integracao') Is NULL
	Create Table dbo.Tb_Oco_Integracao 
	(
		Cd_Id int,
		Id_IntegracaoTipo int,
		Id_EmpresaIntegracao int,
		Ds_URL varchar(1024),
		Ds_Verbo varchar(64),
		Ds_LayoutHeader varchar(512),
		Ds_Layout varchar(2048),
		Qtd_Registros int,
		Qtd_Paralelo int,
		Id_EnvioConfig int,
		Flg_Lote bit,
		Flg_ProcessamentoSucesso bit,
		Flg_Producao bit,
		Flg_EnvioBody bit,
		Flg_EnviaOcorrenciaHistorico bit,
		Flg_IntegracaoGatilho bit,
		Flg_Ativo bit,
		Dt_Processamento datetime
	)
Go

If Object_Id('dbo.Tb_Oco_IntegracaoTipo') Is NULL
	Create Table dbo.Tb_Oco_IntegracaoTipo
	(
		Cd_Id int,
		Ds_Nome varchar(256),
		Flg_Ativo bit
	)
