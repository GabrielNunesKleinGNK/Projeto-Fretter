IF COL_LENGTH('dbo.Tb_Edi_EntregaDevolucaoHistorico', 'Ds_XmlRetorno') IS NULL
BEGIN
    ALTER TABLE dbo.Tb_Edi_EntregaDevolucaoHistorico  ADD Ds_XmlRetorno Varchar(4096)
END
Go