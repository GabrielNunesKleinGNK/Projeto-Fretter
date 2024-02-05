IF COL_LENGTH('Fretter.ImportacaoCte', 'ConciliacaoId') IS NULL
BEGIN
	Alter Table Fretter.ImportacaoCte Add ConciliacaoId Bigint
END
Go
If	Col_Length('dbo.tb_Edi_EntregaDetalhe', 'Ds_JsonComposicaoValoresCotacao') Is Null
Begin
    Alter Table dbo.tb_Edi_EntregaDetalhe  
	Add Ds_JsonComposicaoValoresCotacao varchar(max)
End