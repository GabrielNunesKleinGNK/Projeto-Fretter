

IF COL_LENGTH('dbo.Tb_Edi_EntregaDevolucao', 'Cd_EntregaSaidaItem') IS NULL
BEGIN
	Alter Table dbo.Tb_Edi_EntregaDevolucao 
	Add Cd_EntregaSaidaItem varchar(64) null
End
Go


