If(TYPE_ID('Fretter.Tp_EntregaConciliacao') Is Null)
	CREATE TYPE Fretter.Tp_EntregaConciliacao AS TABLE
	(
		Id_EntregaConciliacao		Bigint NULL
		,Id_Entrega					Bigint NULL
		,Vl_Cobrado					Decimal(16,4) NULL
		,Vl_Altura					Decimal(16,4) NULL
		,Vl_Largura					Decimal(16,4) NULL
		,Vl_Comprimento				Decimal(16,4) NULL
		,Vl_Diametro				Decimal(16,4) NULL
		,Vl_Peso					Decimal(16,4) NULL
		,Vl_Cubagem					Decimal(16,4) NULL
		,Ds_Json					Varchar(Max) NULL	
		,Dt_Processamento			Datetime NULL
		,Ds_RetornoProcessamento	Varchar(2048) NULL	
		,Flg_Sucesso				Bit NULL
	)