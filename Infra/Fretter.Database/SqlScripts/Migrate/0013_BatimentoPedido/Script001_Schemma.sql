IF (EXISTS (SELECT Top 1 1  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Fretter' AND  TABLE_NAME = 'PedidoPendenteEmpresaConfig'))
BEGIN
    IF COL_LENGTH('Fretter.PedidoPendenteEmpresaConfig', 'HoraExecucao') IS NULL
    BEGIN
        ALTER TABLE Fretter.PedidoPendenteEmpresaConfig  
        ADD HoraExecucao Varchar(5) DEFAULT '07:00',
            PeriodoEmDias INT DEFAULT 30,
            TimeoutRequest INT DEFAULT 3000
    END
End
Go
IF (EXISTS (SELECT Top 1 1  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Fretter' AND  TABLE_NAME = 'PedidoPendenteBSeller'))
BEGIN
    IF COL_LENGTH('Fretter.PedidoPendenteBSeller', 'StatusOcorrenciaFusion') IS NULL
    BEGIN
        ALTER TABLE Fretter.PedidoPendenteBSeller  
        ADD StatusOcorrenciaFusion Varchar(200) NULL,
            DataOcorrenciaFusion DATETIME NULL
    END
End
Go
IF (EXISTS (SELECT Top 1 1  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Fretter' AND  TABLE_NAME = 'PedidoPendenteBSellerHistorico'))
BEGIN
    IF COL_LENGTH('Fretter.PedidoPendenteBSellerHistorico', 'StatusOcorrenciaFusion') IS NULL
    BEGIN
        ALTER TABLE Fretter.PedidoPendenteBSellerHistorico  
        ADD StatusOcorrenciaFusion Varchar(200) NULL,
            DataOcorrenciaFusion DATETIME NULL
    END
End
Go