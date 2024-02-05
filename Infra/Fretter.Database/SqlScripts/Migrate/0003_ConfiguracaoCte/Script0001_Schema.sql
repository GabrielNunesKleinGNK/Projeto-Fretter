
IF COL_LENGTH('Fretter.ConfiguracaoCteTransportador', 'EmpresaId') IS NULL
BEGIN
    ALTER TABLE Fretter.ConfiguracaoCteTransportador  ADD EmpresaId INT
END
Go
