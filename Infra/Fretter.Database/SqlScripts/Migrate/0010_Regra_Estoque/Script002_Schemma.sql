
IF COL_LENGTH('dbo.Tb_SKU_RegraEstoqueHistorico', 'Nr_Qtd_TransferidoCrossdocking') IS NULL
BEGIN
    ALTER TABLE dbo.Tb_SKU_RegraEstoqueHistorico  ADD Nr_Qtd_TransferidoCrossdocking INT
END
Go

IF COL_LENGTH('dbo.Tb_SKU_RegraEstoqueHistorico', 'Nr_Qtd_FinalCrossdocking') IS NULL
BEGIN
    ALTER TABLE dbo.Tb_SKU_RegraEstoqueHistorico  ADD Nr_Qtd_FinalCrossdocking INT
END
Go