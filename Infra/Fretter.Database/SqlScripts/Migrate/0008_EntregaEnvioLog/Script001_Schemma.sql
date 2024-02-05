If Object_Id('dbo.Tb_Edi_EntregaStageEnvioLog') Is NULL
	CREATE TABLE dbo.Tb_Edi_EntregaStageEnvioLog
	(
		 Cd_Id					 Int IDENTITY(1,1) CONSTRAINT Pk_Tb_Edi_EntregaStageEnvioLog_Cd_Id PRIMARY KEY
		,Id_EntregaConfiguracao	 Int Not NULL References Tb_Edi_EntregaConfiguracao(Cd_Id) 			
		,Cd_CodigoIntegracao	 Varchar(128) NULL	-- Valor referente a PK do Pedido do Cliente "commercial_id":"5088299920001",
		,Cd_EntregaEntrada		 Varchar(128) NULL	--Exemplo 5088299920001-A
		,Cd_EntregaSaida		 Varchar(128) NULL	--Exemplo 5088299920001-A
		,Dt_Processamento		 Datetime NOT NULL Constraint DF_Tb_Edi_EntregaStageEnvioLog_Dt_Processamento Default(Getdate())
		,Ds_Retorno				 Varchar(max) NULL
		,Ds_Json			     Varchar(max) NULL
		,Flg_Sucesso		     bit Constraint DF_Tb_Edi_EntregaStageEnvioLog_Sucesso Default(0)
		,Flg_Processado		     bit Constraint DF_Tb_Edi_EntregaStageEnvioLog_Processado Default(0)
		,Flg_Ativo				 Bit Constraint DF_Tb_Edi_EntregaStageEnvioLog_Ativo Default(1)
	)