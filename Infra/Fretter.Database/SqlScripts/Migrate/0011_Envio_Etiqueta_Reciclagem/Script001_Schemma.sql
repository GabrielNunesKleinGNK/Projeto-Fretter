If Object_Id('dbo.Tb_Edi_EnvioEtiquetaReciclagem') Is NULL
Create Table Tb_Edi_EnvioEtiquetaReciclagem
(
	Cd_Id Int Identity Constraint PK_Tb_Edi_EnvioEtiquetaReciclagem Primary Key,
	Id_EntregaStage Int,
	Dt_EnvioReciclagem Date,
	Flg_Processado Bit Default(0),
	Ds_Erro varchar(max) NULL
)