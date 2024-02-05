
IF COL_LENGTH('Fretter.ImportacaoConfiguracao', 'DiretorioSucesso') IS NULL
BEGIN
    Alter Table Fretter.ImportacaoConfiguracao Add DiretorioSucesso Varchar(256)
END
Go
IF COL_LENGTH('Fretter.ImportacaoConfiguracao', 'DiretorioErro') IS NULL
BEGIN
    Alter Table Fretter.ImportacaoConfiguracao Add DiretorioErro Varchar(256)
END
